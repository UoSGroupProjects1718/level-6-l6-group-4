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
            public JobType desiredJob;

            public override Status UpdateFunc(ColonistController Colonist)
            {
                //return failure if the colonist has a job and they are not looking for this type of job
                if (Colonist.currentJob != null &&  Colonist.currentJob.jobType != desiredJob)
                    return Status.FAILURE;
                //or if the colonist has a job and they want this type of job, succeed
                else if(Colonist.currentJob != null && Colonist.currentJob.jobType == desiredJob)
                    return Status.SUCCESS;

                //loop through all of the current jobs
                for (int i = 0; i < JobManager.Instance.JobDocket.Count; i++)
                {
                    //if the job is the right type
                    if (JobManager.Instance.JobDocket[i].jobType == desiredJob)
                    {
                        //job specific job handling

                        if (desiredJob == JobType.Gathering)
                        {
                            ItemType itemType = JobManager.Instance.JobDocket[i].InteractionObject.GetComponent<Item>().item.type;
                            //if there are no granaries or stockpiles then dont pick it up
                            if (JobManager.Instance.JobDocket[i].InteractionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
                            {
                                if (BehaviourTreeManager.Granaries.Count > 0 && Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] < Stockpile.Instance.nutritionSpace)
                                {
                                    Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                                    JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                                    return Status.SUCCESS;
                                }
                            }

                            else if (itemType < ItemType.Nutrition && Stockpile.Instance.currResourceAmount < Stockpile.Instance.resourceSpace)
                            {
                                if (BehaviourTreeManager.Stockpiles.Count > 0)
                                    Colonist.currentJob = Instantiate(JobManager.Instance.JobDocket[i]);
                                JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                                return Status.SUCCESS;
                            }
                        }
                        else if (desiredJob == JobType.Hunter)
                        {
                            Colonist.currentJob = (JobManager.Instance.JobDocket[i]);
                            JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                            Colonist.target = Colonist.currentJob.InteractionObject.GetComponent<MonsterController>();
                            return Status.SUCCESS;
                        }
                        else if(desiredJob == JobType.Building)
                        {
                            if (JobManager.Instance.JobDocket[i].RequiredItems == null)
                                continue;
#pragma warning disable CS0162 // Unreachable code detected
                            for (int j = 0; j <  JobManager.Instance.JobDocket[i].RequiredItems.Length; j++)
#pragma warning restore CS0162 // Unreachable code detected
                            {
                                if(Stockpile.Instance.inventoryDictionary[JobManager.Instance.JobDocket[i].RequiredItems[j].resource] <= 0)
                                {
                                    break;
                                }
                                else
                                {
                                    Colonist.currentJob = (JobManager.Instance.JobDocket[i]);
                                    JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                                    return Status.SUCCESS;
                                }
                            }
                        }
                        else
                        {
                            Colonist.currentJob = (JobManager.Instance.JobDocket[i]);
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
