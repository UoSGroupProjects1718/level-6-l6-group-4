using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ Deposit in stockpile")]
        public class DepositInStockpile : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob.jobType == JobType.Gathering)
                {
                    //if we can add the item to the stockpile, send the item back to the pool, and reset the current job
                    if (Stockpile.Instance.AddResource(Colonist.currentJob.interactionObject.GetComponent<Item>().item as Resource))
                    {
                        Colonist.currentJob.interactionObject.GetComponent<Item>().pickedUp = false;
                        Colonist.currentJob.interactionObject.SetActive(false);
                        Colonist.currentJob = null;
                        Colonist.gathererStockpile = null;
                    }
                    //otherwise put what we can into the stockpile and create a new job, and put the item on the colonist's position
                    else
                    {
                        Colonist.currentJob.interactionObject.transform.position = Colonist.transform.position;
                        Colonist.currentJob.interactionObject.GetComponent<MeshRenderer>().enabled = true;
                        Colonist.currentJob.interactionObject.GetComponent<Item>().pickedUp = false;
                        ItemInfo item = Colonist.currentJob.interactionObject.GetComponent<Item>().item;
                        JobManager.CreateJob(JobType.Gathering, (item as Resource).GatherWorkPerItem * item.currentStackAmount, item.attachedGameObject, item.attachedGameObject.transform.position, Colonist.currentJob.jobName);
                        Colonist.currentJob = null;
                        Colonist.gathererStockpile = null;

                    }
                }
                //or if the job is a crafting job
                else if(Colonist.currentJob.jobType == JobType.Crafting)
                {
                    //and we can add the item then we need to set the blacksmith to no longer being worked and reset the colonist job
                      if(Stockpile.Instance.AddWearable(Colonist.currentJob.interactionItem as Wearable))
                      {
                            
                        Colonist.currentJob.interactionObject.GetComponent<BlacksmithFunction>().beingWorked = false;
                        Colonist.currentJob = null;
                        Colonist.gathererStockpile = null;
                      }
                      else
                    {
                        //spawn anitem to the world
                        GameObject newItem = ItemDatabase.SpawnItemToWorld(Colonist.currentJob.interactionItem.itemName);
                        newItem.transform.position = Colonist.transform.position;
                        //we will give a base of 5 work amount to pick up one piece of armour
                        JobManager.CreateJob(JobType.Gathering, 5, newItem.GetComponent<Item>().item,newItem, newItem.transform.position, "Gather" + Colonist.currentJob.interactionItem.itemName);
                        Colonist.currentJob = null;
                        Colonist.gathererStockpile = null;
                    }
                }
                
                UIController.Instance.UpdateStockpile();
                return Status.SUCCESS;
            }
        }
	}
}
