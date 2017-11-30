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
                        //this may want to be altered into a switch statement for clarity further down the line
                        if (desiredJob == JobType.Gathering)
                        {
                            ItemType itemType = JobManager.Instance.JobDocket[i].interactionObject.GetComponent<Item>().item.type;
                            //if there are no granaries or stockpiles then dont pick it up
                            if (JobManager.Instance.JobDocket[i].interactionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
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
                            Colonist.target = Colonist.currentJob.interactionObject.GetComponent<MonsterController>();
                            return Status.SUCCESS;
                        }
                        else if (desiredJob == JobType.Building)
                        {
                            if (JobManager.Instance.JobDocket[i].RequiredItems == null)
                                continue;
#pragma warning disable CS0162 // Unreachable code detected
                            for (int j = 0; j < JobManager.Instance.JobDocket[i].RequiredItems.Length; j++)
#pragma warning restore CS0162 // Unreachable code detected
                            {
                                if (Stockpile.Instance.inventoryDictionary[JobManager.Instance.JobDocket[i].RequiredItems[j].resource] <= 0)
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
                        else if (desiredJob == JobType.Crafting)
                        {
                            if (BehaviourTreeManager.Blacksmiths.Count > 0 && Stockpile.Instance.currWearablesInInventory < Stockpile.Instance.armourySpace)
                            {
                                if (JobManager.Instance.JobDocket[i].jobType == desiredJob)
                                {
                                    RequiredItem[] requiredItems = JobManager.Instance.JobDocket[i].RequiredItems;
                                    int enoughResources = 0;
                                    //check through all of the resources in this job and make sure that we have enough resources (if we have already gathered enough resources for one 
                                    //of the required items, we can just ingore it
                                    for (int j = 0; j < requiredItems.Length; j++)
                                    {
                                        if (requiredItems[j].requiredAmount == 0)
                                            continue;
                                        //however if we can take some resources from a pile, we increase the counter
                                        if (Stockpile.Instance.inventoryDictionary[requiredItems[j].resource] > 0)
                                            enoughResources++;
                                    }
                                    //then if we have enough resources to take something from one of the piles, we get the closest blacksmith and add to the recipie
                                    if (enoughResources > 0)
                                    {
                                        Transform closestBlacksmith = FindClosestBlacksmith(Colonist.transform);
                                        JobManager.Instance.JobDocket[i].interactionObject = closestBlacksmith.gameObject;
                                        //then set the job's location to that blacksmiths location
                                        JobManager.Instance.JobDocket[i].jobLocation = closestBlacksmith.position;
                                        //then we set the colonists job, remove the job from the docket and return
                                        Colonist.currentJob = JobManager.Instance.JobDocket[i];
                                        JobManager.Instance.JobDocket.Remove(JobManager.Instance.JobDocket[i]);
                                    }
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

            private Transform FindClosestBlacksmith(Transform colonist)
            {
                Transform closest = null;
                float lowestDist = float.MaxValue;
                for (int i = 0; i < BehaviourTreeManager.Blacksmiths.Count; i++)
                {
                    float dist = Vector3.Distance(colonist.position, BehaviourTreeManager.Blacksmiths[i].transform.position);
                    if (dist < lowestDist && !BehaviourTreeManager.Blacksmiths[i].beingWorked)
                    {
                        lowestDist = dist;
                        BehaviourTreeManager.Blacksmiths[i].beingWorked = true;
                        closest = BehaviourTreeManager.Blacksmiths[i].transform;
                    }
                }
                return closest;
            }

        }
    }
}
