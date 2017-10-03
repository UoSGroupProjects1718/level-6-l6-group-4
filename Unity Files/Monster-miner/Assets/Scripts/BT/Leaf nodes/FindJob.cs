using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Find Job")]
        public class FindJob : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob != null)
                    return Status.SUCCESS;

                for (int i = 0; i < JobManager.Instance.JobDocket.Count; i++)
                {
                    if (JobManager.Instance.JobDocket[i].jobType == Colonist.ColonistJob)
                    {
                        Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                        JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                        Debug.Log("Have found job: " + Colonist.currentJob.jobName);
                        return Status.SUCCESS;
                    }
                }
                    return Status.FAILURE;
            }

        }
    }
}
