using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTile : Tile
{

    public override List<GameObject> GetPois(Vector3 position){

        List<GameObject> pois = new List<GameObject>();
        GameObject closestPoi = GetClosesInPoi(position);
        pois.Add(closestPoi);
        pois.Add(closestPoi.GetComponent<PoiPar>().pois[0]);
        return pois;

    }

    
}
