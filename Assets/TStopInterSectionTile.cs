using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TStopInterSectionTile : Tile
{
        public override List<GameObject> Neighbours()
    {
       
        List<GameObject> neighbours = new List<GameObject>();
        GameObject  neighbour = null;
        foreach(var arr in TileIds()){
            neighbour = LevelEditor.Instance.getTile(TileId(1+arr[0],0+arr[1]));
            if(neighbour != null){
                neighbours.Add(neighbour);
            }
            if(neighbour != null){
                neighbours.Add(neighbour);
            }
            neighbour = LevelEditor.Instance.getTile(TileId(-1+arr[0],0+arr[1]));
            if(neighbour != null){
                neighbours.Add(neighbour);
            }
            neighbour = LevelEditor.Instance.getTile(TileId(0+arr[0],1+arr[1]));
            if(neighbour != null){
                neighbours.Add(neighbour);
            }
            neighbour = LevelEditor.Instance.getTile(TileId(0+arr[0],-1+arr[1]));
            if(neighbour != null){
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }

    public override void Place(){
        string name = "";
        foreach(var arr in TileIds()){
            if(LevelEditor.Instance.getTile(TileId(arr[0],arr[1])) != null){
               // Destroy(gameObject);
               Debug.Log("Destroy");
                
            }
            LevelEditor.Instance.RegisterTile(TileId(arr[0],arr[1]),gameObject);
            name += TileId(arr[0],arr[1]) + " ";
        }
        transform.name = name;
    }
    protected int[][] TileIds()
    {

        
        switch(Mathf.RoundToInt(transform.localEulerAngles.y/90)%4){
            case 0:
                return new int[][]{
                    new int[]{0,0},
                    new int[]{1,0},
                    new int[]{-1,0},
                    new int[]{0,1}
                };
            case 1:
                return new int[][]{
                    new int[]{0,0},
                    new int[]{1,0},
                    new int[]{0,-1},
                    new int[]{0,1}
                };
            case 2:
                return new int[][]{
                    new int[]{0,0},
                    new int[]{1,0},
                    new int[]{-1,0},
                    new int[]{0,-1}
                };
            case 3:
                return new int[][]{
                new int[]{0,0},
                new int[]{-1,0},
                new int[]{0,-1},
                new int[]{0,1}
            };
        }

       
        Debug.LogError("Wrong rotation");
        return null;
    }
}
