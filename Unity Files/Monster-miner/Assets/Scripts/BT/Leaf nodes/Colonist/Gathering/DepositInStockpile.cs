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
          //if we can add the item to the stockpile, send the item back to the pool, and reset the current job
                if(Stockpile.Instance.AddResource(Colonist.currentJob.InteractionObject.GetComponent<Item>().item as Resource))
                {
                    Colonist.currentJob.InteractionObject.GetComponent<Item>().pickedUp = false;
                    Colonist.currentJob.InteractionObject.SetActive(false);
                    Colonist.currentJob = null;
                    Colonist.gathererStockpile = null;
                }
                //otherwise put what we can into the stockpile and create a new job, and put the item on the colonist's position
                else
                {
                    Colonist.currentJob.InteractionObject.transform.position = Colonist.transform.position;
                    Colonist.currentJob.InteractionObject.GetComponent<MeshRenderer>().enabled = true;
                    Colonist.currentJob.InteractionObject.GetComponent<Item>().pickedUp = false;
                    ItemInfo item = Colonist.currentJob.InteractionObject.GetComponent<Item>().item;
                    JobManager.CreateJob(JobType.Gathering,(item as Resource).GatherWorkPerItem * item.currentStackAmount,item.attachedGameObject,item.attachedGameObject.transform.position,Colonist.currentJob.jobName);
                    Colonist.currentJob = null;
                    Colonist.gathererStockpile = null;

                }
                UIController.Instance.UpdateStockpile();
                return Status.SUCCESS;
            }
        }
	}
}
