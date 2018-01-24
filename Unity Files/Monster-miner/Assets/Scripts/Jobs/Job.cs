//Oliver
using UnityEngine;

[System.Serializable]
public enum JobType
{
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

    public float maxWorkAmount;

    [HideInInspector]
    public float currentWorkAmount;

    public JobType jobType;

    [HideInInspector]
    public Vector3 jobLocation;

    public GameObject interactionObject;
    public ItemInfo interactionItem;

    public RequiredItem[] RequiredItems;

    public void Awake()
    {
        currentWorkAmount = maxWorkAmount;
    }


}
