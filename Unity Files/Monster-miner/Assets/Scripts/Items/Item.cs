using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Item : MonoBehaviour {

    public ItemInfo item;
    private GameTime timeSpawned;

    //do item related stuff
    private void Awake()
    {
        item = Instantiate(item);
        if(item.itemMesh != null)
            GetComponent<MeshFilter>().mesh = Instantiate(item.itemMesh);
        timeSpawned = TimeManager.Time;
    }

    private void FixedUpdate()
    {
       if(timeSpawned.hours != TimeManager.Time.hours)
        {
            item.currentItemDurability -= item.decaySpeed;
            timeSpawned = TimeManager.Time;
            if(item.currentItemDurability <= 0)
            {
                Debug.Log("item has decayed");
            }
        }
    }


}
