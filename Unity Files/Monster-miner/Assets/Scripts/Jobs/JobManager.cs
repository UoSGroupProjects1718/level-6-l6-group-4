//Oliver
using System.Collections.Generic;

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
}
