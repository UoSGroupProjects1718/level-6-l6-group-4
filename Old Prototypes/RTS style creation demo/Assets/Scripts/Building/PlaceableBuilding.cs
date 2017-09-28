using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    [HideInInspector]
    public List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Building")
        {
            colliders.Add(other);        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Building")
        {
            if(colliders.Contains(other))
                colliders.Remove(other);
        }
    }
}
