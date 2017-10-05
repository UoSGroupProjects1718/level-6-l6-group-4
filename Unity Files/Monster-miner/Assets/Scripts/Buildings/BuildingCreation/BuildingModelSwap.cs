using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingModelSwap : MonoBehaviour {
    public KeyCode key;
    public Mesh newObject;
    public MeshFilter Rend;

    void UpdateObject()
    {
        //Instantiate(newObject, transform);

        Mesh oldmesh = GetComponent<MeshFilter>().mesh;
        oldmesh.Clear();
        oldmesh.triangles = newObject.triangles;
        oldmesh.uv = newObject.uv;
        oldmesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = oldmesh;





    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Debug.Log("AAAAAAAAAAAAAAA");
            UpdateObject();
        }
    }

}
