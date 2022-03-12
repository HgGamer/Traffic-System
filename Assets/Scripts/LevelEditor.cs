using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LevelEditor : MonoBehaviour
{
    public static LevelEditor Instance;

    GameObject cursor;
    GameObject CursorPrefab;
    void Awake(){
        //singleton
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public void SetTile(GameObject Prefab){
        Destroy(cursor);
        cursor = Instantiate(Prefab);
        CursorPrefab = Prefab;
    }
 
    
    void Update()
    {
        MoveCursor();
        RotateCursor();
        PlaceCursor();
    }

    void PlaceCursor(){
        if(CursorPrefab == null){
            return;
        }
        
        if(Input.GetMouseButtonDown(0)){
           //check if mouse is over canvas
              if(!EventSystem.current.IsPointerOverGameObject()){
                var instance = Instantiate(CursorPrefab);
                instance.transform.position = cursor.transform.position;
                instance.transform.rotation = cursor.transform.rotation;
              }
        }
    }

    void RotateCursor(){
        if(Input.GetMouseButtonDown(1)){
            cursor.transform.localEulerAngles = new Vector3(0,cursor.transform.localEulerAngles.y+90,0);
        }
    }
    void MoveCursor(){
        if(cursor==null){
            return;
        }
        //set cursor position to mouse position rounded to nearest grid
        var cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(Mathf.Round(cursorWorldPos.x),0, Mathf.Round(cursorWorldPos.z));
       
    }
}
