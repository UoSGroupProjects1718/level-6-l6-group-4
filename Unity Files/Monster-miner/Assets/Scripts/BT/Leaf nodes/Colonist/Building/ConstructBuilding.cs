using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Construct Building")]
        public class ConstructBuilding : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob == null)
                    return Status.FAILURE;

                Colonist.currentJob.InteractionObject.GetComponent<BuildingFunction>().Built = true;
                Colonist.currentJob.InteractionObject.GetComponent<BuildingModelSwap>().UpdateObject();
                //uncomment when model swapping for all buildings is correctly implemented + the scaffolding scale is correct
                //  Colonist.currentJob.InteractionObject.GetComponent<BuildingModelSwap>().UpdateObject();
                Debug.Log("Building Completed: " + Colonist.currentJob.InteractionObject.name);
                if (Colonist.currentJob.jobName == "Build Granary")
                {
                    BehaviourTreeManager.Granaries.Add(Colonist.currentJob.InteractionObject.GetComponent<GranaryFunction>());
                }
                else if (Colonist.currentJob.jobName == "Build Stockpile")
                {
                    //add the stockpile here
                }
                Colonist.currentJob = null;
                return Status.SUCCESS;
            }
        }
    }
}
