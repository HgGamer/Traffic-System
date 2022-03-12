using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTile : Tile
{
    public override List<GameObject> GetPois(Vector3 position){

        List<GameObject> pois = new List<GameObject>();
        GameObject closestPoi = GetClosesInPoi(position);
        

       
        pois.Add(closestPoi);
        foreach(GameObject poi in closestPoi.GetComponent<PoiPar>().pois){
            pois.Add(poi);
        }
        return pois;

    }
}
