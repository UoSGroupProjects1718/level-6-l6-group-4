//Oliver
using UnityEngine;

[CreateAssetMenu( menuName = "Scriptable Objects/Jobs/BuildJob")]
public class BuildJob : Job
{
    public GameObject Building;

    public override void OnJobComplete()
    {
        //do the logic required by the callback function
        Debug.Log(jobName + " has been built.");
    }

}
