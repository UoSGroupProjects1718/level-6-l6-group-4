//Oliver
using UnityEngine;


public enum JobType
{
    Farmer,
    Gathering,
    Harvesting,
    Crafting,
    Building,
    Hunter,
}

[System.Serializable]
public struct RequiredItem
{
    public ItemType resource;
    public int requiredAmount;
}

[CreateAssetMenu(menuName = "Scriptable Objects/Job")]
public class Job : ScriptableObject {

    public string jobName;
    [SerializeField]
    public float maxWorkAmount;

//    [HideInInspector]
    public float currentWorkAmount;

    public JobType jobType;

    [HideInInspector]
    public Vector3 jobLocation;

    public GameObject InteractionObject;

    public RequiredItem[] RequiredItems;

    public void Awake()
    {
        currentWorkAmount = maxWorkAmount;
    }


}
