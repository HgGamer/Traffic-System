using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationUI : MonoBehaviour
{

    void OnEnable()
    {
        EventManager.OnSimulationStart += Show;
        EventManager.OnSimulationStop += Hide;
    }
    void OnDisable()
    {
        EventManager.OnSimulationStart -= Show;
        EventManager.OnSimulationStop -= Hide;
    }
    void Show(){
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Hide(){
        transform.GetChild(0).gameObject.SetActive(false);
    }


    public void StopSimulation()
    {
        EventManager.SimulationStop();
    }
}
