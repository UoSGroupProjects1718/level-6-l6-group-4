using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ Gather Object")]
        public class GatherObject : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob == null || Colonist.currentJob.jobType != JobType.Gathering)
                    return Status.FAILURE;

                if (Colonist.currentJob.currentWorkAmount <= 0)
                { 
                   Colonist.currentJob.InteractionObject.GetComponent<MeshRenderer>().enabled = false;
                   return Status.SUCCESS;
                }
                Colonist.currentJob.currentWorkAmount -= Colonist.ColonistWorkSpeed * Time.deltaTime;
                return Status.RUNNING;
            }

        }	
	}
}
