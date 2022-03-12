using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoiPar : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] pois;

    public virtual GameObject[] getPois(){
        return pois;
    }

}
