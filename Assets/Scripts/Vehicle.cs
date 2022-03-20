using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
   public float currentoffset;
    public GameObject FutureVehicle;
    public float futureoffset = 0.2f;
    List<GameObject> pois = new List<GameObject>();
    List<Vector3> futurepois = new List<Vector3>();
    GameObject lastPoi;
    Vector3 target;
    Vector3 FutureTarget;
    bool speedup = true;
    public float speed = 2;
    float oldspeed;
    bool waiting = false;
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
    IEnumerator WaitAndGo(float time = 0){
        if(!waiting){
            waiting = true;
            float lastspeed = speed;
            speed = 0;
            yield return new WaitForSeconds(1.0f+time);
            speed = lastspeed;
             waiting = false;
        }
        
    }

    IEnumerator WaitForBlock(List<GameObject> zones,bool stop){
        float lastspeed = speed;
        speed = 0;
        if(stop){
             yield return new WaitForSeconds(1f);
        }
       
        Debug.Log("WaitForBlock");
        while(ZonesBlocked(zones)){
            Debug.Log("Blocked zones");
            yield return new WaitForSeconds(1f);
        }
        speed = lastspeed;
    }
    bool ZonesBlocked(List<GameObject> zones){
        foreach(GameObject zone in zones){
            if(zone.GetComponent<CheckZone>().blocked){
                return true;
            }
        }
        return false;
    }
    void FillPath(){
        foreach (var poi in lastPoi.GetComponent<OutPoi>().ClosestPoi.GetComponent<PoiPar>().getPois())
        {
            pois.Add(poi);
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
        if(GetCurrentTile() == null || GetCurrentTile().GetPois(transform.position) == null){
            DestroyImmediate(gameObject);
            return;
        }
        foreach (var poi in GetCurrentTile().GetPois(transform.position))
        {
            pois.Add(poi);
            futurepois.Add(poi.transform.position);
            lastPoi = poi;
        }
        speed = Random.Range(1.0f,2.5f);
        target = new Vector3(pois[0].transform.position.x, transform.position.y, pois[0].transform.position.z);
        FutureTarget = target;
        transform.position = target + Vector3.up;
        FutureVehicle.transform.parent = transform.parent;
        FutureVehicle.GetComponent<FutureCar>().Vehicle = gameObject;
        oldspeed = speed;
    }
     void Move(){
        
        

        
        if(target == Vector3.zero){
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.LookAt(target);
        if(Vector3.Distance(transform.position, target) < 0.1f){
            pois.RemoveAt(0);
            if(pois.Count>0){
                target = new Vector3(pois[0].transform.position.x, transform.position.y, pois[0].transform.position.z);
            }else{
                target = Vector3.zero;
            }
        }       
    }

    void MoveFutureVehicle(int call = 0){
        if(call>10){
            return;
        }
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
            MoveFutureVehicle(++call);
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

    public void Stop(){
        oldspeed = speed;
        speedup = false;
        speed = 0;
    }

    public void VehicleFront(float newspeed){
        if(newspeed == 0){
            StartCoroutine(WaitAndGo(0.5f));
            return;
        }

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
    void OnTriggerEnter(Collider other){
       
        if(other.gameObject.tag == "InterSectionBlock"){
            GameObject nextOutPoi = null;
            foreach(GameObject poi in pois){
                if(poi.tag == "OutPoi"){
                    nextOutPoi = poi;
                    break;
                }
            }
           
            var InterSectionBlock = other.gameObject.GetComponent<InterSectionNavigator>();
            var zones = InterSectionBlock.GetBlockZone(nextOutPoi);
            if(zones.Count == 0){
                Debug.Log("No zones");
                if(InterSectionBlock.StopSign){
                    StartCoroutine(WaitAndGo());
                }
                return;
            }
            StartCoroutine(WaitForBlock(zones,InterSectionBlock.StopSign));
            
        }
    }

}
