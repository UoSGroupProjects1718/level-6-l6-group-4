//Oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingFunction : MonoBehaviour {

    public List<Collider> colliders = new List<Collider>();


    public virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Building")
        {
            colliders.Add(collision.collider);
        }
    }
    public virtual void OnCollisionExit(Collision collision)
    {

        if(collision.collider.tag == "Building")
        {
            colliders.Remove(collision.collider);
        }
    }
    public abstract void Function();
}
