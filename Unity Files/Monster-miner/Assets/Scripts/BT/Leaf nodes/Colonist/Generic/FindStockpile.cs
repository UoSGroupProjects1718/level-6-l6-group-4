using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ find stockpile")]

        public class FindStockpile : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.gathererStockpile != null)
                    return Status.SUCCESS;

                Colonist.gathererStockpile = GetStockpile(Colonist);
                //if the stockpile is null that means there isnt one
                if (Colonist.gathererStockpile == null)
                {
                    //so if the colonist is doing a crafting job
                    if (Colonist.currentJob.jobType == JobType.Crafting)
                    {
                        //we need to spawn a new item to the world
                        GameObject newItem = ItemDatabase.SpawnItemToWorld(Colonist.currentJob.interactionItem.name);
                        newItem.transform.position = Colonist.transform.position;
                        //then create a new gathering job and remove the colonists job
                        int gatherWork = Colonist.currentJob.interactionItem.GatherWorkPerItem * Colonist.currentJob.interactionItem.currentStackAmount;
                        JobManager.CreateJob(JobType.Gathering, gatherWork, Colonist.currentJob.interactionItem, newItem, newItem.transform.position, "Gather " + Colonist.currentJob.interactionItem.itemName);
                        Colonist.currentJob.interactionObject.GetComponent<BlacksmithFunction>().beingWorked = false;
                        Colonist.currentJob = null;
                        return Status.FAILURE;

                    }
                    //otherwise we are gathering
                    else
                    {
                       //so do the same with gathering
                        int gatherWork = Colonist.currentJob.interactionItem.GatherWorkPerItem * Colonist.currentJob.interactionItem.currentStackAmount;
                        JobManager.CreateJob(JobType.Crafting, gatherWork, Colonist.currentJob.interactionItem, Colonist.currentJob.interactionObject, Colonist.currentJob.interactionObject.transform.position, "Gather " + Colonist.currentJob.interactionItem.itemName);
                        Colonist.currentJob = null;
                        Colonist.currentJob.interactionObject.transform.position = Colonist.transform.position;
                        return Status.FAILURE;
                    }
                }
                Colonist.currentJob.jobLocation = Colonist.gathererStockpile.transform.position;
                return Status.SUCCESS;
            }

            //find the closest stockpile to the agent
            public GameObject GetStockpile(ColonistController Colonist)
            {
                float closestDist = float.MaxValue;
                GameObject currentClosest = null;
                ItemType itemType = Colonist.currentJob.interactionItem.type;
                if (itemType == ItemType.Nutrition)
                {

                    for (int i = 0; i < BehaviourTreeManager.Granaries.Count; i++)
                    {
                        float dist = Vector3.Distance(Colonist.transform.position, BehaviourTreeManager.Granaries[i].transform.position);
                        if (dist < closestDist)
                        {
                            currentClosest = BehaviourTreeManager.Granaries[i].gameObject;
                            closestDist = dist;
                        }
                    }
                }
                else if(itemType == ItemType.Wearable)
                {
                    for(int i = 0; i < BehaviourTreeManager.Armouries.Count; i++)
                    {
                        float dist = Vector3.Distance(Colonist.transform.position, BehaviourTreeManager.Armouries[i].transform.position);
                        if (dist < closestDist)
                        {
                            currentClosest = BehaviourTreeManager.Armouries[i].gameObject;
                            closestDist = dist;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BehaviourTreeManager.Stockpiles.Count; i++)
                    {
                        float dist = Vector3.Distance(Colonist.transform.position, BehaviourTreeManager.Stockpiles[i].transform.position);
                        if (dist < closestDist)
                        {
                            currentClosest = BehaviourTreeManager.Stockpiles[i].gameObject;
                            closestDist = dist;
                        }
                    }
                }
                return currentClosest;
            }
            private bool pathComplete(ColonistController colonist)
            {
                if (Vector3.Distance(colonist.NavMeshAgent.destination, colonist.transform.position) <= colonist.NavMeshAgent.stoppingDistance)
                {
                    if (!colonist.NavMeshAgent.hasPath || colonist.NavMeshAgent.velocity.sqrMagnitude == 0f)
                        return true;
                }
                return false;
            }
        }
    }
}
