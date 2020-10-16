using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//klasa odpowiada za rysowanie linii będącej śladem lotu kuli
//wykorzystuje komponent LineRenderer
public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S; //singleton
    public float minDist = 0.5f; // minimalna odległość punktów rysowanej linii
    private LineRenderer line;
    private GameObject _poi; //punkt docelowy
    private List<Vector3> points; // lista punktów łączonych linią

    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }
    public GameObject poi
    {
        get
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi !=null) //zresetowanie po przypisanieu czegos do _poi
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }
    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void AddPoint()
    {
        //dodanie punktow
        Vector3 pt = _poi.transform.position;
        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)//wroc jesli punkt nie jest odpowiednio daleko od popredniego
        {
            return;
        }
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            //ponieważ przy strzale w prawą stronę launchPosDiff ma wartość ujemną, początek linii pojawi się
            //za pociskiem
            points.Add(pt + launchPosDiff); 
            points.Add(pt); //drugim punktem linii jest położenie pocisku w chwili wystrzału
            line.positionCount = 2;
            //ustaw dwa pierwsze punkty
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            //wlacz line renderer
            line.enabled = true;           
        }
        else
        {
            //dodawanie kolejnych punktów
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint); // przypisanie kolejnego punktu
            line.enabled = true;
        }
    }
    public Vector3 lastPoint //właściwość tylko do odczytu
    {
        get
        {
            if (points==null)
            {
                //jesli nie ma wektorow zwroc vektor zero
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);// wartość ostatniego punktu listy points
        }
    }
    private void FixedUpdate()
    {
        if(poi==null)
        {
            if (CamFollow.POI!=null)
            {
                if (CamFollow.POI.tag=="Projectile")
                {
                    poi = CamFollow.POI;
                }
                else { return; }
            }
            else { return; }
        }
        //jeśli poi istnieje, następuje próba dodania jego pozycji do listy points
        AddPoint();
        if(CamFollow.POI==null)
        {
            poi = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
