using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorTileButton : MonoBehaviour
{
    public GameObject Prefab;
    public void Click() {
        if(Prefab == null){
            return;
        }
        LevelEditor.Instance.SetTile(Prefab);
    }
    void Awake(){
        if(GetComponent<Button>()!=null){
            GetComponent<Button>().onClick.AddListener(Click);
        }
    }
}
