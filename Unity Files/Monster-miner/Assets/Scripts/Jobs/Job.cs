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
[CreateAssetMenu(menuName = "Scriptable Objects/Job")]
public class Job : ScriptableObject {

    public string jobName;
    [SerializeField]
    private float maxWorkAmount;

    [HideInInspector]
    public float currentWorkAmount;

    public JobType jobType;

    [HideInInspector]
    public Vector3 jobLocation;

    public GameObject InteractionObject;


    public void Awake()
    {
        currentWorkAmount = maxWorkAmount;
    }


}
