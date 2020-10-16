using UnityEngine;

//skrypt odpowiada za śledzenie wystrzelonego pocisku kamerą, oraz powrót do widoku procy gdy pocisk
//przestanie się ruszać
public class CamFollow : MonoBehaviour
{
    public static GameObject POI; //punkt docelowy, za którym podąza kamera (pocisk)
    public float camZ; //wartość Z położenia kamery
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero; // minimalne położenie kamery, aby nie przesuwała się w lewo lub w dół

    private void Awake()
    {
        camZ = transform.position.z;
    }
    private void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;

            if (POI.tag == "Projectile")
            {
                //gdy pocisk przestał się ruszać, wróc do procy
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }
        //ograniczenie położenia kamery
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //interpolacja nowego punktu położenia kamery na podstawie średniej ważonej wartości 
        //jej aktualnego położenia oraz położenia punktu docelowego, przy użyciu parametru średniej easing
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination;
        // w celu utrzymania pocisku w zasięgu widoku kamery, jej rozmiar jest uzależniony od y wektora
        //destination
        //im wyżej znajduje się pocisk, tym większy wymiar Y kamery
        Camera.main.orthographicSize = destination.y + 10;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
