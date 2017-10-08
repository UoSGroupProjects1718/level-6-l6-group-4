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
            LayerMask monsterLayer;

            public void OnEnable()
            {
                monsterLayer = LayerMask.NameToLayer("Monster");
            }

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.target == null)
                    return Status.FAILURE;


                if(InRange(Colonist,Colonist.target))
                {
                    Colonist.agentMovement.navMeshAgent.destination = Colonist.transform.position;
                    return Status.SUCCESS;
                }
               else
                {
                    Colonist.agentMovement.MoveToPoint(Colonist.target.transform.position);
                }
                return Status.RUNNING;
            }
            private bool InRange(ColonistController Colonist, MonsterController monster)
            {
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Colonist.transform.position,monster.transform.position - Colonist.transform.position, out hit, Colonist.ColonistWeapon.Range, monsterLayer))
                {
                    if(hit.collider.gameObject == monster.gameObject)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
