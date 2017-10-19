//Oliver
using System.Collections.Generic;
using UnityEngine;

public class JobManager : SingletonClass<JobManager>
{
    public List<Job> JobDocket;


    public override void Awake()
    {
        base.Awake();
        JobDocket = new List<Job>();
    }

    public void QueueJob(Job jobToAdd)
    {
        JobDocket.Add(jobToAdd);
    }
    public static void CreateJob(JobType jobType, int MaxWorkAmount, GameObject interactionObject, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.jobName = JobName;
        newJob.InteractionObject = interactionObject;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;

        Instance.QueueJob(newJob);
    }
}
