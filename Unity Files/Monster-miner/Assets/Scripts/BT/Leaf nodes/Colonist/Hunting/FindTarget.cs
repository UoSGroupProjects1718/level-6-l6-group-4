using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Find Target")]
        public class FindTarget : BehaviourBase
        {

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if(Colonist.target == null)
                Colonist.target = FindClosest(Colonist.transform);
                if (Colonist.target != null)
                    return Status.SUCCESS;
                return Status.FAILURE;
            }

            private MonsterController FindClosest(Transform colonist)
            {
                MonsterController currentClosest = null;
                float lowestDist = float.MaxValue;
                foreach (MonsterController monster in BehaviourTreeManager.Monsters)
                {
                    float dist = Vector3.Distance(colonist.position, monster.transform.position);
                    if (dist < lowestDist)
                    {
                        lowestDist = dist;
                        currentClosest = monster;
                    }
                }
                return currentClosest;
            }
        }
    }
}
