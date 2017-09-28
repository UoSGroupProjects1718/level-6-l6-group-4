using UnityEngine;

public class Job : ScriptableObject {



    public string JobName;
    public float workAmount;
    [HideInInspector]
    public bool beingWorked;
    [HideInInspector]
    public Vector3 jobLocation;

    public JobType jobType;


    public virtual void Initialise() { return; }
    public virtual void OnJobComplete() { return; }

}
