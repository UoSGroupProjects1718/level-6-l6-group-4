using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TerrainSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnTiles;
    public List<GameObject> GlobalList;
    [SerializeField]
    int tilesToSpawn = 3;
    
    [SerializeField]
    float SpawnXDiff = 100;
    [SerializeField]
    float SpawnYDiff = 100;
    MapTileList[,] map;
    List<Vector2> NextSpawn = new List<Vector2>();

    private void Start()
    {
        StartCoroutine(SpawnWorld());
    }

    IEnumerator SpawnWorld()
    {
        int ArraySize = tilesToSpawn * 2;
        ArraySize += 1;
        //+1 centre 
        map = new MapTileList[ArraySize, ArraySize];

        SpawnOrigin(tilesToSpawn + 1);
        AddNeighboursToList(tilesToSpawn + 1, tilesToSpawn + 1);
        while (NextSpawn.Count > 0)
        {
            SpawnTile(NextSpawn[0]);
            NextSpawn.RemoveAt(0);
        }
        

        SpawnTiles = null;
        map = null;
        yield return null;
    }

    void AddNeighboursToList(int x, int y)
    {
        NextSpawn.Add(new Vector2(x + 1, y));
        NextSpawn.Add(new Vector2(x - 1, y));
        NextSpawn.Add(new Vector2(x, y - 1));
        NextSpawn.Add(new Vector2(x, y + 1));
    }

    void SpawnOrigin(int Middle)
    {
        map[Middle, Middle] = Instantiate(SpawnTiles[Random.Range(0, SpawnTiles.Length - 1)], new Vector3(0, 0, 0), transform.rotation).GetComponent<MapTileList>();
    }

    void SpawnTile(Vector2 gridPos)
    {

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
        try
        {
            LeftList = map[x - 1, y].GetEast();
        }
        catch { }
        try { RightList = map[x + 1, y].GetWest(); }
        catch { }



        List<GameObject> PossibleList = GetPossibleList(UpList, DownList, LeftList, RightList);

        
        if (PossibleList.Count == 0)
        {
            Debug.Log("No Neighbours at spawn. Problems have occoured");
            map[x, y] = new MapTileList();
        }
        else
        {
            map[x, y] = Instantiate(PossibleList[Random.Range(0, PossibleList.Count)], new Vector3((x - tilesToSpawn-1) * SpawnXDiff, 0, (y- tilesToSpawn-1) *SpawnYDiff), Quaternion.identity).GetComponent<MapTileList>();
        }
        AddNeighboursToList(x, y);
    }

    List<GameObject> GetPossibleList(List<GameObject> Up, List<GameObject> Down, List<GameObject> Left, List<GameObject> Right)
    {   
        
       
        List<GameObject> ReturnList = new List<GameObject>();// Up;
        if (Up == null)
        {
            ReturnList = GlobalList;
        }


        else
        {
            if (Up.Count > 0)
                ReturnList = Up;
            else
            {
                ReturnList = GlobalList;
            }
        }


        foreach (GameObject tile in ReturnList)//check each tile in up. if not in Let, Right or down, remove it.
        {
            if (Down != null)
            {
                if (!ListContains(Down,tile.name) && Down.Count > 0)
                {
                    ReturnList.Remove(tile);
                    continue;
                }
            }

            if (Left != null)
            {
                if (!ListContains(Left, tile.name) && Left.Count > 0)
                {
                    ReturnList.Remove(tile);
                    continue;
                }
            }
            if (Right != null)
            {
                if (!ListContains(Right, tile.name) && Right.Count > 0)
                {
                    ReturnList.Remove(tile);
                    continue;
                }
            }
        }

        return ReturnList;

    }

    bool ListContains(List<GameObject> list, string name)
    {
        foreach  (GameObject tile in list)
        {
            if (tile.name == name || tile.name + "(Clone)"==name || tile.name == name + "(Clone)")
                return true;
        }
        return false;
    }

    
}

