using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoiZone
{
    //todo refactor to multiple zone support for a single poi
    public GameObject poi;
    public GameObject zone;
    public GameObject zone2;
}

public class InterSectionNavigator : MonoBehaviour
{
    public PoiZone[] poiZones;
    public bool StopSign = true;
    public List<GameObject> GetBlockZone(GameObject Outpoi){
        if(Outpoi == null){
            return new List<GameObject>();
        }
        List<GameObject> blockZones = new List<GameObject>();
        foreach (PoiZone poiZone in poiZones)
        {
            if (poiZone.poi == Outpoi)
            {
                if(poiZone.zone != null)
                    blockZones.Add(poiZone.zone);
                if(poiZone.zone2 != null)
                    blockZones.Add(poiZone.zone2);
            }
        }
        return blockZones;
    }
}
