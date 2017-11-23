using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileList : MonoBehaviour {
    
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
        return NorthList;
    }

    public List<GameObject> GetEast()
    {
        return EastList;

    }

    public List<GameObject> GetSouth()
    {
        return SouthList;
    }

    public List<GameObject> GetWest()
    {
        return WestList;

    }
}
