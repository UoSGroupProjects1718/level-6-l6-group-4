using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TerrainSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnTiles;
    [SerializeField]
    List<GameObject> GlobalList;
    [SerializeField]
    int tilesToSpawn = 3;
    
    [SerializeField]
    float SpawnXDiff = 100;
    [SerializeField]
    float SpawnYDiff = 100;
    MapTileList[,] map;
    List<Vector2> NextSpawn = new List<Vector2>();
    GameObject parentObj;

    public IEnumerator SpawnNewWorld()
    {
        parentObj = new GameObject();
        parentObj.name = "Terrain";
        int ArraySize = tilesToSpawn * 2;
        ArraySize += 1;
        //+1 centre 
        map = new MapTileList[ArraySize, ArraySize];

        SpawnOrigin(tilesToSpawn + 1);
        AddNeighboursToList(tilesToSpawn + 1, tilesToSpawn + 1);
        while (NextSpawn.Count > 0)
        {
            if(NextSpawn.Count > 1000)
            {
                break;//sometimes breaks
            }
            SpawnTile(NextSpawn[0]);
            NextSpawn.RemoveAt(0);
        }

        map[0, 0].GetComponent<NavMeshSurface>().BuildNavMesh();
      //  StartCoroutine(buildNavMesh());
        SpawnTiles = null;
        map = null;
        yield return null;
    }

    public IEnumerator LoadWorld()
    {
        yield return null;
    }

    void AddNeighboursToList(int x, int y)
    {
        if(x > tilesToSpawn*2 || x<0 || y > tilesToSpawn * 2 || y < 0)
        {
            return;
        }
            try
        {
            if (map[x + 1, y] == null)
            {
                NextSpawn.Add(new Vector2(x + 1, y));
            }
        }
        catch { }
        try
        {
            if (map[x - 1, y] == null)
            {
                NextSpawn.Add(new Vector2(x - 1, y));
            }
        }
        catch { }

        try
        {
            if (map[x, y - 1] == null)
            {
                NextSpawn.Add(new Vector2(x, y - 1));
            }
        }
        catch { }
        try
        {
            if (map[x, y + 1] == null)
            {
                NextSpawn.Add(new Vector2(x, y + 1));
            }
        }
        catch { }
    }

    void SpawnOrigin(int Middle)
    {
        map[Middle, Middle] = Instantiate(SpawnTiles[Random.Range(0, SpawnTiles.Length - 1)], new Vector3(0, 0, 0), transform.rotation, parentObj.transform).GetComponent<MapTileList>();
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
            Debug.Log("No Neighbours at spawn. Problems have occoured at " + x + ", "+ y);
            try
            {
                Debug.Log("Left:" + map[x - 1, y].gameObject.name);
            }
            catch { }
            try
            {
                Debug.Log("Right:" + map[x + 1, y].gameObject.name);
            }
            catch { }
            try
            {
                Debug.Log("Up:" + map[x, y + 1].gameObject.name);
            }
            catch { }
            try
            {
                Debug.Log("Down:" + map[x, y - 1].gameObject.name);
            }
            catch { }
            int temp = Random.Range(0, GlobalList.Count);
            try
            {
                GameObject tempObj = Instantiate(GlobalList[temp], new Vector3((x - tilesToSpawn - 1) * SpawnXDiff, 0, (y - tilesToSpawn - 1) * SpawnYDiff), Quaternion.identity, parentObj.transform);
            
                map[x, y] = tempObj.GetComponent<MapTileList>();
            }
            catch
            {
            }
            
        }
        else
        {
            map[x, y] = Instantiate(PossibleList[Random.Range(0, PossibleList.Count)], new Vector3((x - tilesToSpawn-1) * SpawnXDiff, 0, (y- tilesToSpawn-1) *SpawnYDiff), Quaternion.identity, parentObj.transform).GetComponent<MapTileList>();
           
        }
        AddNeighboursToList(x, y);
    }

    IEnumerator buildNavMesh()
    {
        int limit = tilesToSpawn * 2 + 1;
        for (int i = 0; i < limit; i++)
        {
            for (int j = 0; j < limit; j++)
            {
                map[i, j].GetComponent<NavMeshSurface>().BuildNavMesh();
            }
        }
        yield return null;
    }

    List<GameObject> GetPossibleList(List<GameObject> Up, List<GameObject> Down, List<GameObject> Left, List<GameObject> Right)
    {   
        
       
        List<GameObject> ReturnList = new List<GameObject>();// Up;
        if (Up == null)
        {

            foreach  (GameObject tile in GlobalList)
            {
                ReturnList.Add(tile);
            }
        }


        else
        {
            if (Up.Count > 0)
                ReturnList = Up;
            else
            {
                foreach (GameObject tile in GlobalList)
                {
                    ReturnList.Add(tile);
                }
            }
        }

        int forloopLimit = ReturnList.Count;
        //foreach (GameObject tile in ReturnList)//check each tile in up. if not in Let, Right or down, remove it.
        //{ }

        for (int i = 0; i < forloopLimit; i++)
        {
            GameObject tile = ReturnList[i];
        

            if (Down != null)
            {
                if (!ListContains(Down,tile.name) && Down.Count > 0)
                {
                    ReturnList.Remove(tile);
                    i--;
                    forloopLimit--;
                    continue;
                }
            }

            if (Left != null)
            {
                if (!ListContains(Left, tile.name) && Left.Count > 0)
                {
                    ReturnList.Remove(tile);
                    i--;
                    forloopLimit--;
                    continue;
                }
            }
            if (Right != null)
            {
                if (!ListContains(Right, tile.name) && Right.Count > 0)
                {
                    ReturnList.Remove(tile);
                    i--;
                    forloopLimit--;
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

