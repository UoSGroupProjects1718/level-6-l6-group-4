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
            public JobType DesiredJob;

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob != null &&  Colonist.currentJob.jobType != DesiredJob)
                    return Status.FAILURE;
                else if(Colonist.currentJob != null && Colonist.currentJob.jobType == DesiredJob)
                    return Status.SUCCESS;

                for (int i = 0; i < JobManager.Instance.JobDocket.Count; i++)
                {
                    if (JobManager.Instance.JobDocket[i].jobType == DesiredJob)
                    {
                        if (DesiredJob == JobType.Gathering && JobManager.Instance.JobDocket[i].InteractionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
                        {
                            if (BehaviourTreeManager.Granaries.Count > 0)
                            {
                                Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                                JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                                return Status.SUCCESS;
                            }
                        }
                        else if (DesiredJob == JobType.Gathering && JobManager.Instance.JobDocket[i].InteractionObject.GetComponent<Item>().item.type == ItemType.Resource)
                        {
                            Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                            JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                            return Status.SUCCESS;
                        }
                        else
                        {
                            Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                            JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                            return Status.SUCCESS;
                        }
                    }
                }
                    return Status.FAILURE;
            }

        }
    }
}
