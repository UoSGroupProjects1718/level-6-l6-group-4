using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModelSwap : MonoBehaviour {
    
    public Mesh scafoldingMesh;
    Mesh currentMesh;

    private void Awake()
    {
        currentMesh = transform.GetChild(0).GetComponent<MeshFilter>().mesh;
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = Instantiate(scafoldingMesh);
        scafoldingMesh = null;
    }

    public void UpdateObject()
    {
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = Instantiate(currentMesh);

    }
}
