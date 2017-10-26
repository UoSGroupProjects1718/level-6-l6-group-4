using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileList : MonoBehaviour {
    [SerializeField]
    List<GameObject> MyPossibleNeighbours;

    public List<GameObject> GetUnwantedList(List<GameObject> GlobaList) {
        
        foreach (GameObject tile in MyPossibleNeighbours)
        {
            GlobaList.Remove(tile);
        }
        return GlobaList;
    }
}
