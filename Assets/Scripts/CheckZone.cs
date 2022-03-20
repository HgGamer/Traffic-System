using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZone : MonoBehaviour
{
    public Material freeMaterial;
    public Material blockedMaterial;

    public bool blocked = false;
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Car")
        {
            Debug.Log("CAR IN BLOCK ZONE");
            blocked = true;
            GetComponent<Renderer>().material = blockedMaterial;
        }   
    }
    void FixedUpdate()
    {
        blocked = false;
        GetComponent<Renderer>().material = freeMaterial;
    }
}
