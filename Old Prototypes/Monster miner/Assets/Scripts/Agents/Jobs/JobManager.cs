using UnityEngine;


public class JobManager : MonoBehaviour {

    public static JobManager instance;

    private void Awake()
    {
        instance = this;
    }


    //will need alternate voids to account for the different job types.
    public void CreateJob(Job job)
    {
        GlobalBlackboard.JobDocket.Add(job);
    }

}
