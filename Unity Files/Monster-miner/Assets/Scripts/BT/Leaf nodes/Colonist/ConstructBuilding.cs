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

                if(Colonist.currentJob.currentWorkAmount <= 0)
                {
                    Colonist.currentJob.InteractionObject.GetComponent<BuildingFunction>().Built = true;
                    Debug.Log("Building Completed: " + Colonist.currentJob.InteractionObject.name);
                    Colonist.currentJob = null;
                    return Status.SUCCESS;
                }
                Colonist.currentJob.currentWorkAmount -= Colonist.ColonistWorkSpeed * Time.deltaTime;
                return Status.RUNNING;
            }
        }
    }
}
