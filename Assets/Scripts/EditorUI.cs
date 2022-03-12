using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorUI : MonoBehaviour
{
     void OnEnable()
    {
        EventManager.OnSimulationStart += Hide;
        EventManager.OnSimulationStop += Show;
    }
    void OnDisable()
    {
        EventManager.OnSimulationStart -= Hide;
        EventManager.OnSimulationStop -= Show;
    }
    void Show(){
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Hide(){
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void StartSimulation()
    {
        EventManager.SimulationStart();
    }
}
