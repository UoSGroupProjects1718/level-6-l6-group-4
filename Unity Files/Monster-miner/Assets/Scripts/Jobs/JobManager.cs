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

#region create job overloads
    public static void CreateJob(JobType jobType, int MaxWorkAmount, GameObject interactionObject, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.jobName = JobName;
        newJob.interactionObject = interactionObject;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;
        newJob.currentWorkAmount = MaxWorkAmount;

        Instance.QueueJob(newJob);
    }
    public static void CreateJob(JobType jobType,RequiredItem[] requiredItems, int MaxWorkAmount, GameObject interactionObject, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.RequiredItems = requiredItems;
        newJob.jobName = JobName;
        newJob.interactionObject = interactionObject;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;
        newJob.currentWorkAmount = MaxWorkAmount;
        Instance.QueueJob(newJob);
    }
    public static void CreateJob(JobType jobType, RequiredItem[] requiredItems, int MaxWorkAmount, ItemInfo interactionItem, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.RequiredItems = requiredItems;
        newJob.jobName = JobName;
        newJob.interactionItem = interactionItem;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;
        newJob.currentWorkAmount = MaxWorkAmount;

        Instance.QueueJob(newJob);
    }
    public static void CreateJob(JobType jobType, RequiredItem[] requiredItems, int MaxWorkAmount, ItemInfo interactionItem,GameObject interactionObject, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.RequiredItems = requiredItems;
        newJob.jobName = JobName;
        newJob.interactionItem = interactionItem;
        newJob.interactionObject = interactionObject;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;
        newJob.currentWorkAmount = MaxWorkAmount;

        Instance.QueueJob(newJob);
    }

    public static void CreateJob(JobType jobType, int MaxWorkAmount, ItemInfo interactionItem, GameObject interactionObject, Vector3 jobLocation, string JobName)
    {
        Job newJob = ScriptableObject.CreateInstance("Job") as Job;
        newJob.jobName = JobName;
        newJob.interactionItem = interactionItem;
        newJob.interactionObject = interactionObject;
        newJob.maxWorkAmount = MaxWorkAmount;
        newJob.jobLocation = jobLocation;
        newJob.jobType = jobType;
        newJob.currentWorkAmount = MaxWorkAmount;

        Instance.QueueJob(newJob);
    }
#endregion
}
