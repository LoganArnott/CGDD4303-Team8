using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallEasierGrab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "GrabArea")
        {
            this.gameObject.GetComponent<SphereCollider>().radius = 1.5f;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "GrabArea")
        {
            this.gameObject.GetComponent<SphereCollider>().radius = 0.5f;
        }
    }
}
