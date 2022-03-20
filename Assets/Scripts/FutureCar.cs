using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutureCar : MonoBehaviour
{
    public GameObject Vehicle;

    void OnTriggerStay(Collider other){
        //Debug.Log(other);
        if(other.gameObject != Vehicle && other.tag == "Car")
        {
            Vehicle.GetComponent<Vehicle>().VehicleFront(other.GetComponent<Vehicle>().speed);
        }
        
        if(other.tag == "CrossingBlock" && other.transform.parent.transform.parent.GetComponent<CrossingTile>().locked == false){
            Vehicle.GetComponent<Vehicle>().VehicleBack();
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "CrossingBlock" && other.transform.parent.transform.parent.GetComponent<CrossingTile>().locked == true){
            Vehicle.GetComponent<Vehicle>().Stop();
        }
    }
    void OnTriggerExit(Collider other){
        if(other.gameObject != Vehicle && other.tag == "Car")
        {
            Vehicle.GetComponent<Vehicle>().VehicleBack();
        }
    }


}
