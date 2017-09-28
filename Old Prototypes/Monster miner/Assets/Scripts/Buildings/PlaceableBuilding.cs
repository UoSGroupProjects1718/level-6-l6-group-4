using System.Collections.Generic;
using UnityEngine;

public class PlaceableBuilding : MonoBehaviour {

    [HideInInspector]
    public List<Collider2D> colliders = new List<Collider2D>();

    [Header("Size in nodes")]
    public Vector2 BuildingSize;

    [HideInInspector]
    public bool built;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the object collides with a building, add it to the list
        if(collision.tag == "Unbuildable" || collision.tag == "Colonist")
        {
            colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if the object leaves the collider of a building, remove it from the list
        if(collision.tag == "Unbuildable" || collision.tag == "Colonist")
        {
            if(colliders.Contains(collision))
            {
                colliders.Remove(collision);
            }
        }
    }
}
