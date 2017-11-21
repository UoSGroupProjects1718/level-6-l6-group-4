using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileList : MonoBehaviour {
    public static List<GameObject> GlobalList;
    [SerializeField]
    List<GameObject> NorthList;
    [SerializeField]
    List<GameObject> EastList;
    [SerializeField]
    List<GameObject> SouthList;
    [SerializeField]
    List<GameObject> WestList;

    public List<GameObject> GetNorth()
    {
        List<GameObject> currentList = GlobalList;

        foreach (GameObject tile in NorthList)
        {
            currentList.Remove(tile);
        }
        return currentList;

    }

    public List<GameObject> GetEast()
    {
        List<GameObject> currentList = GlobalList;

        foreach (GameObject tile in EastList)
        {
            currentList.Remove(tile);
        }
        return currentList;

    }

    public List<GameObject> GetSouth()
    {
        List<GameObject> currentList = GlobalList;

        foreach (GameObject tile in SouthList)
        {
            currentList.Remove(tile);
        }
        return currentList;

    }

    public List<GameObject> GetWest()
    {
        List<GameObject> currentList = GlobalList;

        foreach (GameObject tile in NorthList)
        {
            currentList.Remove(tile);
        }
        return currentList;

    }
}
