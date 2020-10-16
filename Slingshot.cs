using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa odpowiada za celowanie procą oraz strzelanie
public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
    public GameObject prefabPocisk;
    public float velocityMult=8f;
    public GameObject launchPoint; // punkt generowania efektu halo, przy najechaniu myszką na Collider procy
    public Vector3 launchPos;
    public GameObject pocisk;
    public bool isAiming;
    private Rigidbody pociskRigidbody;
    //statyczna właściwość tylko do odczytu
    static public Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }
    // Start is called before the first frame update
    private void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    void Start()
    {
    }
    void OnMouseEnter()
    {
        // wyświetlenie efektu halo
        launchPoint.SetActive(true);
    }
    void OnMouseExit()
    {
        launchPoint.SetActive(false);
    }
    //przy nacisnięciu przycisku myszy
    private void OnMouseDown()
    {
        isAiming = true;
        pocisk = Instantiate(prefabPocisk) as GameObject;
        pocisk.transform.position = launchPos;
        pociskRigidbody = pocisk.GetComponent<Rigidbody>();
        pociskRigidbody.isKinematic = true;
    }
    // Update is called once per frame
    void Update()
    {
        //wykonuje się tylko w trybie celowania
        if (!isAiming) return;
        //pocisk w trybie celowania powinien podążać za kursorem, jednak tylko do określonej wartości 
        // odległości od procy
        Vector3 mousePos2D = Input.mousePosition;
        //wyrównanie położenia względem procy - położenie kamery na osi Z wynosi -10, a procy 0
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //obliczenie położenia kursoru względem procy (punktu wystrzału)
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude>maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //utrzymanie pocisku w ograniczonej odległości od procy
        Vector3 projPos = launchPos + mouseDelta;
        pocisk.transform.position = projPos;
        //gdy zwolniono przycisk myszy
        if(Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            pociskRigidbody.isKinematic = false;
            //prędkość pocisku w zależności od jego odległości względem procy
            pociskRigidbody.velocity = -mouseDelta * velocityMult;
            CamFollow.POI = pocisk; //nowy punkt podążania kamery
            pocisk = null; //pocisk został wystrzelony
            ShotTheGoal.ShotFired();
            //ProjectileLine.S.poi = pocisk; //nowy punkt (Klasa ProjectileLine wyszukuje pocisk w metodzie FixedUpdate()
        }
    }
}
