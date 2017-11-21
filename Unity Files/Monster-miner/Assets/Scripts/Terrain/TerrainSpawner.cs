using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour {

    [SerializeField]
    GameObject[] SpawnTiles;

    [SerializeField]
    int MinSmallArea;
    [SerializeField]
    int MaxSmallArea;
    [SerializeField]
    int MinMedArea;
    [SerializeField]
    int MaxMedArea;
    [SerializeField]
    int MinLargeArea;
    [SerializeField]
    int MaxLargeArea;

    [SerializeField]
    float SpawnXDiff=100;
    [SerializeField]
    float SpawnYDiff=100;
    // Use this for initialization
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
            UpList = map[x, y - 1].GetNorth();
        }
        catch { }
        try
        {
            DownList = map[x, y + 1].GetSouth(); 
        }
        catch { }
        try {
            LeftList = map[x-1, y].GetEast(); 
        }
        catch { }
        try {RightList = map[x+1, y].GetWest();  }
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
        List<GameObject> ReturnList = Up;
        if (ReturnList == null)
        {
            ReturnList = MapTileList.GlobalList;
        }


        foreach (GameObject tile in Up)//check each tile in up. if not in Let, Right or down, remove it.
        {
            if (Down != null) { 
                if (!Down.Contains(tile))
                {
                    Up.Remove(tile);
                    continue;
                }
           }

            if (Left != null)
            {


                if (!Left.Contains(tile))
                {
                    Up.Remove(tile);
                    continue;
                }
            }
            if (Right != null)
            {
                if (!Up.Contains(tile))
                {
                    Right.Remove(tile);
                    continue;
                }
            }
        }

        return ReturnList;

    }


    
}
