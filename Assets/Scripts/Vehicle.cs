using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{

    List<Vector3> pois = new List<Vector3>();
    GameObject lastPoi;
    Vector3 target;
    Tile GetCurrentTile(){
        //raycast down to find the tile
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit)){
            if(hit.collider.gameObject.tag == "Tile"){
                return hit.collider.transform.parent.gameObject.GetComponent<Tile>();
            }
        }
        return null;
    }


    void FillPath(){
        foreach (var poi in lastPoi.GetComponent<OutPoi>().ClosestPoi.GetComponent<PoiPar>().pois)
        {
            pois.Add(poi.transform.position);
            lastPoi = poi;
        }
    }

    void SimulationStop(){
        Destroy(gameObject);
    }
    void OnEnable(){
        EventManager.OnSimulationStop += SimulationStop;
    }
    void OnDisable(){
        EventManager.OnSimulationStop -= SimulationStop;
    }
    void Start(){
        
        foreach (var poi in GetCurrentTile().GetPois(transform.position))
        {
            pois.Add(poi.transform.position);
            lastPoi = poi;
        }
       
        target = new Vector3(pois[0].x, transform.position.y, pois[0].z);
        transform.position = target + Vector3.up;
        
    }
    void Move(){
        if(target == Vector3.zero){
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 2);
        transform.LookAt(target);
        if(Vector3.Distance(transform.position, target) < 0.1f){
            pois.RemoveAt(0);
            if(pois.Count>0){
                target = new Vector3(pois[0].x, transform.position.y, pois[0].z);
            }else{
                target = Vector3.zero;
            }
        }       
    }
    void Update(){
        if(pois.Count<6){
            FillPath();
        }
        Move();
    }
    
}
