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

    MapTileList[,] map;
	
	IEnumerator SpawnWorld()
    {
        //Set Variables
        int SmallAreaLayers = Random.Range(MinSmallArea, MaxSmallArea);
        int MedAreaLayers = Random.Range(MinMedArea, MaxMedArea);
        int LargeAreaLayers = Random.Range(MinLargeArea, MaxLargeArea);

        int ForLoopLimit = SmallAreaLayers + MedAreaLayers+ LargeAreaLayers + 2;
        int ArraySize= ForLoopLimit *2;
        ArraySize += 1;
        //+5 for gates and centre 
        map = new MapTileList[ArraySize,ArraySize];
        
        SpawnSpawn(ForLoopLimit+1);
        for (int i = 0; i < ForLoopLimit; i++)
        {
            SpawnEdges(i);
            for (int j = 0; j < i; j++)
            {
                if (i == 0 || j == 0)
                    continue;
                else
                {
                    SpawnTile(i, j);
                }
            }
        }
        SpawnTiles = null;
        map = null;
        return null;
    }

    void SpawnSpawn(int Middle)
    {
        map[Middle, Middle] = Instantiate(SpawnTiles[Random.Range(0, SpawnTiles.Length - 1)], new Vector3(0, 0, 0), transform.rotation).GetComponent<MapTileList>();
    }
    void SpawnTile(int x, int y) {
        List<GameObject> UpList, DownList, LeftList, RightList;
        UpList = DownList = LeftList = RightList = new List<GameObject>();
        try
        {
            UpList = map[x, y - 1].GetComponent<MapTileList>().MyPossibleNeighbours;
        }
        catch { }
        try
        {
            DownList = map[x, y + 1].GetComponent<MapTileList>().MyPossibleNeighbours;
        }
        catch { }
        try {
            LeftList = map[x-1, y].GetComponent<MapTileList>().MyPossibleNeighbours;
        }
        catch { }
        try {RightList = map[x+1, y].GetComponent<MapTileList>().MyPossibleNeighbours; }
        catch { }



        List<GameObject> PossibleList = GetPossibleList(UpList, DownList, LeftList, RightList);

        


    }




    List<GameObject> GetPossibleList(List<GameObject> Up, List<GameObject> Down, List<GameObject> Left, List<GameObject> Right) {
        List<GameObject> ReturnList;
        

        return ReturnList;

    }

    void SpawnEdges(int edge) {
        SpawnTile(0, edge);
        SpawnTile(0, -edge);
        SpawnTile(edge,0);
        SpawnTile(-edge,0);
    }
    
}
