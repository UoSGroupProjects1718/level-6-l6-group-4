using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Work on job")]
        public class WorkOnJob : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob == null)
                    return Status.FAILURE;

                if(Colonist.currentJob.currentWorkAmount <= 0)
                {
                    return Status.SUCCESS;
                }
                if(Colonist.lastWorked.minutes != TimeManager.Time.minutes)
                {
                    Colonist.lastWorked = TimeManager.Time;
                    Colonist.currentJob.currentWorkAmount -= (Colonist.ColonistWorkSpeed / TimeManager.Instance.DeltaTime);
                }
                return Status.RUNNING;
            }
        }
    }
}
