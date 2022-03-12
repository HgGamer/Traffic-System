using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    protected string TileId(int offsetx = 0, int offsety = 0){
        return ((int)transform.position.x+offsetx) + "," + ((int)transform.position.z+offsety);
    }
    void Start()
    {
        
    }
    
    public virtual void Place(){
        if(LevelEditor.Instance.getTile(TileId()) != null){
            Destroy(gameObject);
        }
        LevelEditor.Instance.RegisterTile(TileId(),gameObject);
        transform.name = TileId();
    }


    void OnDisable()
    {
        LevelEditor.Instance.UnregisterTile(TileId());
    }
    public virtual List<GameObject> Neighbours()
    {
        List<GameObject> neighbours = new List<GameObject>();
        GameObject  neighbour = LevelEditor.Instance.getTile(TileId(1,0));
        if(neighbour != null){
            neighbours.Add(neighbour);
        }
        neighbour = LevelEditor.Instance.getTile(TileId(-1,0));
        if(neighbour != null){
            neighbours.Add(neighbour);
        }
        neighbour = LevelEditor.Instance.getTile(TileId(0,1));
        if(neighbour != null){
            neighbours.Add(neighbour);
        }
        neighbour = LevelEditor.Instance.getTile(TileId(0,-1));
        if(neighbour != null){
            neighbours.Add(neighbour);
        }
        return neighbours;
    }
     public virtual List<GameObject> GetPois(Vector3 position){

        List<GameObject> pois = new List<GameObject>();
        GameObject closestPoi = GetClosesInPoi(position);
        pois.Add(closestPoi);
        foreach(GameObject poi in closestPoi.GetComponent<PoiPar>().getPois()){
            pois.Add(poi);
        }
        return pois;

    }
    protected virtual GameObject GetClosesInPoi(Vector3 position){
        
        float minDistance = float.MaxValue;
        GameObject closestPoi = null;
        foreach (Transform child in transform.Find("Pois").transform)
        {
            if(child.tag != "InPoi"){
                continue;
            }
            float distance = Vector3.Distance(position, child.position);
            if(distance < minDistance){
                minDistance = distance;
                closestPoi = child.gameObject;
            }
        }
        return closestPoi;
    }
}
