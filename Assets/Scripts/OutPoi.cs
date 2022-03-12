using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPoi : MonoBehaviour
{
    public GameObject ClosestPoi;
    void OnEnable(){
        EventManager.OnSimulationStart +=SimulationStart; 
    }
    void OnDisable(){
        EventManager.OnSimulationStart -=SimulationStart; 
    }
    void SimulationStart(){
        ClosestPoi = GetClosestInPoi();
        Debug.Log("OutPoi Start");
    }
    GameObject GetClosestInPoi(){
        
        var tiles = transform.parent.parent.GetComponent<Tile>().Neighbours();
        float minDistance = float.MaxValue;
        GameObject closestPoi = null;
        foreach(var tile in tiles){
            foreach(Transform child in tile.transform.Find("Pois")){
                if(child.tag != "InPoi"){
                    continue;
                }
                float distance = Vector3.Distance(transform.position, child.position);
                if(distance < minDistance){
                    minDistance = distance;
                    closestPoi = child.gameObject;
                }
            }
        }
        return closestPoi;
    }
}
