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
               for(int i = 0; i <  Colonist.currentJob.RequiredItems.Length; i++)
                {
                    Colonist.currentJob.RequiredItems[i].requiredAmount -= Stockpile.Instance.RemoveResource(Colonist.currentJob.RequiredItems[i].resource, Colonist.currentJob.RequiredItems[i].requiredAmount);
                    if (Colonist.currentJob.RequiredItems[i].requiredAmount == 0)
                    {
                        hasEnough++;
                    }
                }
               if(hasEnough == Colonist.currentJob.RequiredItems.Length)
                {
                    return Status.SUCCESS;
                }
                JobManager.Instance.JobDocket.Add(Colonist.currentJob);
                Colonist.currentJob = null;
                return Status.FAILURE;
            }

        }
    }
}

