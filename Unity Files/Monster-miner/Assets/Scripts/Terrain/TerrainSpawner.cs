using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour {

    public GameObject[] SpawnTiles;

    public int MinSmallArea;
    public int MaxSmallArea;
    public int MinMedArea;
    public int MaxMedArea;
    public int MinLargeArea;
    public int MaxLargeArea;

    public float SpawnXDiff;
    public float SpawnYDiff;
    // Use this for initialization
    public List<GameObject> GlobalList;
    MapTileList[,] map;

    List<Vector2> NextSpawn = new List<Vector2>();

    IEnumerator SpawnWorld()
    {
        //Set Variables
        int SmallAreaLayers = Random.Range(MinSmallArea, MaxSmallArea);
        int MedAreaLayers = Random.Range(MinMedArea, MaxMedArea);
        int LargeAreaLayers = Random.Range(MinLargeArea, MaxLargeArea);

        int ForLoopLimit = SmallAreaLayers + MedAreaLayers+ LargeAreaLayers;
        int ArraySize= ForLoopLimit *2;
        ArraySize += 1;
        //+1 centre 
        map = new MapTileList[ArraySize,ArraySize];
        
        SpawnSpawn(ForLoopLimit+1);
        AddNeighboursToList(ForLoopLimit + 1, ForLoopLimit + 1);
        while (NextSpawn.Count > 0)
        {
            SpawnTile(NextSpawn[0]);
            NextSpawn.RemoveAt(0);

        }

        SpawnTiles = null;
        map = null;
        return null;
    }

    void AddNeighboursToList(int x, int y) {
        NextSpawn.Add(new Vector2(x+1, y));
        NextSpawn.Add(new Vector2(x-1, y));
        NextSpawn.Add(new Vector2(x, y-1));
        NextSpawn.Add(new Vector2(x, y+1));
    }

    void SpawnSpawn(int Middle)
    {
        map[Middle, Middle] = Instantiate(SpawnTiles[Random.Range(0, SpawnTiles.Length - 1)], new Vector3(0, 0, 0), transform.rotation).GetComponent<MapTileList>();
    }

    void SpawnTile(Vector2 gridPos) {
        int x = (int)gridPos.x;
        int y = (int)gridPos.y;
        try
        {
            if (map[x, y] != null)
            {
                return;
            }
        }

        catch
        {
            return;
        }

        List<GameObject> UpList, DownList, LeftList, RightList;
        UpList = DownList = LeftList = RightList = new List<GameObject>();
        try
        {
            UpList = map[x, y - 1].GetUnwantedList(GlobalList);
        }
        catch { }
        try
        {
            DownList = map[x, y + 1].GetUnwantedList(GlobalList); 
        }
        catch { }
        try {
            LeftList = map[x-1, y].GetUnwantedList(GlobalList); 
        }
        catch { }
        try {RightList = map[x+1, y].GetUnwantedList(GlobalList);  }
        catch { }



        List<GameObject> PossibleList = GetPossibleList(UpList, DownList, LeftList, RightList);


        if (PossibleList.Count == 0)
        {
            Debug.Log("No Neighbours at spawn. Problems have occoured");
        }
        else
        {
            map[x, y] = Instantiate(PossibleList[Random.Range(0, PossibleList.Count)], new Vector3(0, 0, 0), transform.rotation).GetComponent<MapTileList>();
        }
        AddNeighboursToList(x, y);
    }




    List<GameObject> GetPossibleList(List<GameObject> Up, List<GameObject> Down, List<GameObject> Left, List<GameObject> Right) {
        List<GameObject> ReturnList = GlobalList;

        foreach (GameObject tile in Up)
        {
            ReturnList.Remove(tile);
        }

        foreach (GameObject tile in Down)
        {
            ReturnList.Remove(tile);
        }

        foreach (GameObject tile in Left)
        {
            ReturnList.Remove(tile);
        }

        foreach (GameObject tile in Right)
        {
            ReturnList.Remove(tile);
        }

        return ReturnList;

    }


    
}
