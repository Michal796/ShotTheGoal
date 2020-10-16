using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa odpowiedzialna za wygenerowanie chmury korzystając z prefabrykantu cloudSphere (kuli)
public class Cloud : MonoBehaviour
{
    [Header("definiowanie ręczne w panelu inspektor")]
    public GameObject cloudSphere; //pojedynczy obiekt, na podstawie którego generowana jest chmura
    public int numSpheresMin; //min/max liczba obiektów cloudSphere w chmurze
    public int numSpheresMax;
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1); //odleglości maksymalne cloudSphere od srodka obiektu chmury
    public Vector2 sphereScaleRangeX = new Vector2(4, 8); //(min, max zakresy skalowania pojedynczej kuli)
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    // większa odległość na osi x od środka obiektu Cloud powoduje proporcjonalne zmniejszanie wartości skali Y 
    //obiektu cloudSphere
    //obiekty cloudSphere położone dalej od środka (względem osi x) będą miały mniejszą wartość sphereScaleRangeY
    // minimalna skala wartości Y dla obiektu cloudSphere
    public float scaleYMin = 2f;

    private List<GameObject> spheres;
    // Start is called before the first frame update
    void Start()
    {
        //wygenerowanie chmury z obiektu cloudSphere
        spheres = new List<GameObject>();

        int num = Random.Range(numSpheresMin, numSpheresMax);
        for (int i=0; i<num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform); //rodzicem obiektu cloudSphere jest chmura Cloud

            //przypisz losowe położenie
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset; // położenie względem obiektu cloud

            //przypisz losowa skale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y); //(warrosci min, max)
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //dopasuj skale y na podstawie odleglosci x od obiektu Cloud
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);
            spTrans.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
     //   if (Input.GetKeyDown(KeyCode.Space))
    //    {
     //       Restart();
     //   }
    }
    void Restart()
    { foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }
        Start();
    }
}
