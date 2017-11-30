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


                Colonist.currentJob.interactionObject.GetComponent<MeshRenderer>().enabled = false;
                Colonist.currentJob.interactionObject.GetComponent<Item>().pickedUp = true;
                return Status.SUCCESS;
            }
        }
    }
}
