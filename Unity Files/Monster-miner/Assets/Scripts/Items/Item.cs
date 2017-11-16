using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Item : MonoBehaviour {


    public ItemInfo item;
    private GameTime timeSpawned;
    private float currentItemDurability;

    //do item related stuff
    private void Start()
    {
        timeSpawned = TimeManager.Instance.IngameTime;
        item.attachedGameObject = gameObject;
        currentItemDurability = item.maxItemDurability;
    }

    private void FixedUpdate()
    {
       if(timeSpawned.hours != TimeManager.Instance.IngameTime.hours)
        {
            currentItemDurability -= item.decayPerHour;
            timeSpawned = TimeManager.Instance.IngameTime;
            if(currentItemDurability <= 0)
            {
                Debug.Log("item has decayed");
            }
        }
    }



}
