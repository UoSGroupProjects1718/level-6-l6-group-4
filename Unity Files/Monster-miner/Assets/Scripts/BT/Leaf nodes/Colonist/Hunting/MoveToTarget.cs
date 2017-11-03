using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Move to target")]
        public class MoveToTarget : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.target == null)
                    return Status.FAILURE;


                if(InRange(Colonist,Colonist.target))
                {
                    Colonist.NavMeshAgent.destination = Colonist.transform.position;
                    return Status.SUCCESS;
                }
               else
                {
                    Colonist.NavMeshAgent.SetDestination(Colonist.target.transform.position);
                }
                return Status.RUNNING;
            }
            private bool InRange(ColonistController Colonist, MonsterController monster)
            {

                if(Vector3.Distance(Colonist.transform.position,monster.transform.position) < Colonist.colonistWeapon.Range - 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
