using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/GetResourcesForJob")]
        public class GetResourcesForJob : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                int hasEnough = 0;
                //loop through the required items and remove the number of required items from the stockpile
               for(int i = 0; i <  Colonist.currentJob.RequiredItems.Length; i++)
                {
                    Colonist.currentJob.RequiredItems[i].requiredAmount -= Stockpile.Instance.RemoveResource(Colonist.currentJob.RequiredItems[i].resource, Colonist.currentJob.RequiredItems[i].requiredAmount);
                    if (Colonist.currentJob.RequiredItems[i].requiredAmount == 0)
                    {
                        //if this element of the required items array has been satisfied, increase the counter
                        //to signify that one item has enough resources
                        hasEnough++;
                    }
                }
               //if has enough is equal to the number of required items, i.e. all elements of the array have had their requirements satisfied
               if(hasEnough == Colonist.currentJob.RequiredItems.Length)
                {
                    //then update the stockpile's info
                    UIController.Instance.UpdateStockpile();
                    //and return
                    return Status.SUCCESS;
                }

                //then update the stockpile's info
                UIController.Instance.UpdateStockpile();
                //and if we were crafting we need to set being worked to false
                if(Colonist.currentJob.jobType == JobType.Crafting)
                    Colonist.currentJob.interactionObject.GetComponent<BlacksmithFunction>().beingWorked = false;
                //if this is not true, then we add the job back to the list, set the current job to null and return failure
                JobManager.Instance.JobDocket.Add(Colonist.currentJob);
                Colonist.currentJob = null;
                return Status.FAILURE;
            }

        }
    }
}

