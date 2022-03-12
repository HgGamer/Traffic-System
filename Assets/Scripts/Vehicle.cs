using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float currentoffset;
    public GameObject FutureVehicle;
    public float futureoffset = 0.2f;
    List<Vector3> pois = new List<Vector3>();
    List<Vector3> futurepois = new List<Vector3>();
    GameObject lastPoi;
    Vector3 target;
    Vector3 FutureTarget;
    bool speedup = true;
    public float speed = 2;
    float oldspeed;
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
        foreach (var poi in lastPoi.GetComponent<OutPoi>().ClosestPoi.GetComponent<PoiPar>().getPois())
        {
            pois.Add(poi.transform.position);
            futurepois.Add(poi.transform.position);
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
            futurepois.Add(poi.transform.position);
            lastPoi = poi;
        }
        speed = Random.Range(1.0f,2.5f);
        target = new Vector3(pois[0].x, transform.position.y, pois[0].z);
        FutureTarget = target;
        transform.position = target + Vector3.up;
        FutureVehicle.transform.parent = transform.parent;
        FutureVehicle.GetComponent<FutureCar>().Vehicle = gameObject;
        oldspeed = speed;
    }
    public void VehicleFront(float newspeed){
        if(newspeed>speed){
            return;
        }
        oldspeed = speed;
        speed = newspeed;
    }
    public void VehicleBack(){
        speed = oldspeed;
        speedup = true;
    }
    public void Stop(){
        oldspeed = speed;
        speedup = false;
        speed = 0;
    }
    
    
    void Move(){
        
        if(speedup && oldspeed>speed && Random.Range(0,100)>95){
            speed += 0.1f;
        }

        
        if(target == Vector3.zero){
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
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

    void MoveFutureVehicle(){
        if(FutureTarget == Vector3.zero){
            return;
        }
        if(Vector3.Distance(transform.position,FutureVehicle.transform.position)>0.6f){
            FutureVehicle.transform.position = transform.position;
        }
        FutureVehicle.transform.position = Vector3.MoveTowards(FutureVehicle.transform.position, FutureTarget, Time.deltaTime * speed);
        FutureVehicle.transform.LookAt(FutureTarget);
       
        if(Vector3.Distance(FutureVehicle.transform.position, FutureTarget) < 0.1f){
            futurepois.RemoveAt(0);
            if(futurepois.Count>0){
                FutureTarget = new Vector3(futurepois[0].x, transform.position.y-0.4f, futurepois[0].z);
            }else{
                FutureTarget = Vector3.zero;
            }
        }       
        if(Vector3.Distance(transform.position,FutureVehicle.transform.position)<futureoffset){
            MoveFutureVehicle();
        }
        currentoffset= Vector3.Distance(transform.position,FutureVehicle.transform.position);
    }
    void Update(){
        if(pois.Count<10){
            FillPath();
        }
        Move();
        MoveFutureVehicle();
    }
    
}
