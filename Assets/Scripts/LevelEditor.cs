using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class LevelEditor : MonoBehaviour
{
    Dictionary<string, GameObject> tiles = new Dictionary<string, GameObject>();
    public static LevelEditor Instance;
    public GameObject carPrefab;
    GameObject cursor;
    GameObject CursorPrefab;
    void Awake()
    {
        //singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        EventManager.OnSimulationStart += SimulationStart;
        EventManager.OnSimulationStop += SimulationStop;
    }

    void OnDisable()
    {
        EventManager.OnSimulationStart -= SimulationStart;
        EventManager.OnSimulationStop -= SimulationStop;
    }

    public void SpawCar(){
        var element = tiles.ElementAt((int)UnityEngine.Random.Range(0, tiles.Count-1)).Value;
        if(element != null){
            var position = element.transform.position;
            var instance = Instantiate(carPrefab,  position+ Vector3.up, Quaternion.identity);
        }
    }


    public void RegisterTile(string id, GameObject tile)
    {
        tiles.Add(id, tile);
    }

    public void UnregisterTile(string id)
    {
        if (tiles.ContainsKey(id))
        {
            tiles.Remove(id);
        }
    }

    public GameObject getTile(string id)
    {
        tiles.TryGetValue(id, out GameObject tile);
        return tile;
    }
    public void SetTile(GameObject Prefab)
    {
        Destroy(cursor);
        cursor = Instantiate(Prefab);
        CursorPrefab = Prefab;
    }

    void SimulationStart()
    {
        Destroy(cursor);
    }
    void SimulationStop(){
        if(CursorPrefab == null){
            return;
        }
        cursor = Instantiate(CursorPrefab);
    }


    void Update()
    {
        MoveCursor();
        RotateCursor();
        PlaceCursor();
    }

    void PlaceCursor()
    {
        if (CursorPrefab == null)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //check if mouse is over canvas
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                var instance = Instantiate(CursorPrefab);
                instance.transform.position = cursor.transform.position;
                instance.transform.rotation = cursor.transform.rotation;
                if (GameObject.Find("Map") == null)
                {
                    new GameObject("Map");
                }
                instance.transform.parent = GameObject.Find("Map").transform;
            }
        }
    }

    void RotateCursor()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cursor.transform.localEulerAngles = new Vector3(0, cursor.transform.localEulerAngles.y + 90, 0);
        }
    }
    void MoveCursor()
    {
        if (cursor == null)
        {
            return;
        }
        //set cursor position to mouse position rounded to nearest grid
        var cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(Mathf.Round(cursorWorldPos.x), 0, Mathf.Round(cursorWorldPos.z));

    }
}
