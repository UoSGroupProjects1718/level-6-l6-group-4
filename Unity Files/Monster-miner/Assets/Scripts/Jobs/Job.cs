//Oliver
using UnityEngine;


public enum JobType
{
    Farmer,
    Scout,
    Crafting,
    Building,
    Hunter,
}

public abstract class Job : ScriptableObject {

    public string jobName;
    [SerializeField]
    private float maxWorkAmount;

    [HideInInspector]
    public float currentWorkAmount;

    public JobType jobType;

    [HideInInspector]
    public Vector3 jobLocation;

    public abstract void OnJobComplete();

    public void Awake()
    {
        currentWorkAmount = maxWorkAmount;
    }


}
