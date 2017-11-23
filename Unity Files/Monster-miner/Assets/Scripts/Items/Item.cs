using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(MeshRenderer),typeof(MeshFilter))]
public class Item : MonoBehaviour {


    public ItemInfo item;
    private GameTime timeSpawned;
    [SerializeField]
    private float currentItemDurability;
    [HideInInspector]
    public Job correspondingJob;
    [HideInInspector]
    public bool pickedUp;

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
            if(!pickedUp)
            {
                currentItemDurability -= item.decayPerHour;
                timeSpawned = TimeManager.Instance.IngameTime;
                if(currentItemDurability <= 0)
                {
                    if(JobManager.Instance.JobDocket.Contains(correspondingJob))
                    {
                        JobManager.Instance.JobDocket.Remove(correspondingJob);
                    }
                    gameObject.SetActive(false);
                    Debug.Log("item has decayed");
                }
            }
        }
    }



}
