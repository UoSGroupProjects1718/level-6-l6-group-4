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
                //find the closest target, once we have looped through all monsters we then return the one we found to be the closest
                MonsterController currentClosest = null;
                float lowestDist = float.MaxValue;
                foreach (MonsterController monster in BehaviourTreeManager.Monsters)
                {
                    float dist = Vector3.Distance(colonist.position, monster.transform.position);
                    if (!monster.checkDead())
                    {
                        if (dist < lowestDist)
                        {
                            lowestDist = dist;
                            currentClosest = monster;
                        }
                    }
                }
                return currentClosest;
            }
        }
    }
}
