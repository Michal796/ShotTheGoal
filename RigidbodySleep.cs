using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa przypisana do obiektów składających się na zamki, aby zamek był stabilny do momentu uderzenia
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.Sleep();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
