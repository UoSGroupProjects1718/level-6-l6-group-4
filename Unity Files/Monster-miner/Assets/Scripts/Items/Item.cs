using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Item : MonoBehaviour {


    public ItemInfo item;
    private GameTime timeSpawned;

    //do item related stuff
    private void Start()
    {
        timeSpawned = TimeManager.Instance.IngameTime;
        item.attachedGameObject = gameObject;
    }

    private void FixedUpdate()
    {
       if(timeSpawned.hours != TimeManager.Instance.IngameTime.hours)
        {
            item.currentItemDurability -= item.decayPerHour;
            timeSpawned = TimeManager.Instance.IngameTime;
            if(item.currentItemDurability <= 0)
            {
                Debug.Log("item has decayed");
            }
        }
    }
    public void UpdateMesh()
    {
        if (item.itemMesh != null)
            GetComponent<MeshFilter>().mesh = Instantiate(item.itemMesh);
    }


}
