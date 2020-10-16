using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//klasa odpowiada za wygenerowanie chmur na podstawie obiektu cloudPrefab (z przypisanym skryptem Cloud)
public class CloudCrafter : MonoBehaviour
{
    public int numClouds = 40; //chmury do wygenerwoania
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10); //zakres możliwego położenia dla pojedynczej chmury
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1; //wartości zakresu skali dla chmury
    public float cloudScaleMax = 3;
    public float cloudSpeedMult = 0.5f; //predkosc chmur
    private GameObject[] cloudInstances;

    private void Awake() //tworzenie chmur i ich polozenia
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("CloudAnchor"); //rodzicem wszystkich chmur jest CloudAnchor
        GameObject cloud;
        for (int i=0;i<numClouds;i++)//wygeneruj chmury w petli
        {
            cloud = Instantiate<GameObject>(cloudPrefab);
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);//ustalenie polozenia chmury
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
            //rozmiar chmury
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU); //interpolacja liniowa
            //mniejsze chmury (z mniejszym scaleU) powinny byc blizej powierzchni ziemi
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
            //mniejsze chmury powinny byc dalej
            cPos.z = 100 - 90 * scaleU;
            //przypisz ustalone położenie do obiektu chmury
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            //uczyn chmure potomkiem obiektu cloudanchor
            cloud.transform.SetParent(anchor.transform);
            //dodaj chmure do tablicy
            cloudInstances[i] = cloud;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() //przesuwanie chmur w lewo; gdy chmura przekroczy dopuszczalną wartość położenia x,
        //zostanie przesunięta do wskazanej pozycji początkowej cloudPosMax
    {
     //przetworz wytworzone chmury
     foreach (GameObject cloud in cloudInstances)
        {
            //odczytaj skalowanie i polozenie
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            //niech wieksze chmury poruszaja sie szybciej
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
            //jesli chmura poruszyła się zbyt daleko w lewo
            if (cPos.x <= cloudPosMin.x)
            {
                //przemiesc ja w prawo
                cPos.x = cloudPosMax.x;
            }
            //przypisz nowe położenie chmury
            cloud.transform.position = cPos;
        }

    }
}
