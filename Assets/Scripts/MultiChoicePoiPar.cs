using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiChoicePoiPar : PoiPar
{
   
    public override GameObject[] getPois(){
        var path =  pois[(int)Random.Range(0, pois.Length)];

        List<GameObject> _pois = new List<GameObject>();
        foreach(Transform child in path.transform){
            _pois.Add(child.gameObject);
        }
        return _pois.ToArray();

    }
}
