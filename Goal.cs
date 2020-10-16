using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//skrypt przypisany do celu, odpowiada za graficzną informację o trafieniu do celu (zmiana koloru),
//oraz umożliwienie zmiany poziomu przez klasę ShotTheGoal, na podstawie zmiennej goalMet
public class Goal : MonoBehaviour
{
    static public bool goalMet = false; //czy trafiono w cel
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Projectile")
        {
            goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
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
