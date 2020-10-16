using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Klasa pocisk odpowiada ze niszczenie drewnianych klocków, gdy prędkość pocisku w chwili uderzenia jest 
//odpowiednio wysoka
public class Projectile : MonoBehaviour
{
    [Header("definiowanie ręczne w panelu inspector")]
    public float velToDestroyWood;
    [Header("definiowanie dynamiczne")]
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.other.tag == "Wood" && rigid.velocity.magnitude >velToDestroyWood)
        {
            Destroy(collision.other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //print(rigid.velocity.x);
    }
}
