using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Attack monster")]
        public class AttackMonster : BehaviourBase
        {

            private LayerMask monsterLayerMask;

            private void OnEnable()
            {
                monsterLayerMask = LayerMask.NameToLayer("Monster");
            }

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.target == null)
                    return Status.FAILURE;

                if(Time.time > Colonist.nextAttack)
                {
                    RaycastHit hit = new RaycastHit();
                    Colonist.nextAttack = Time.time + Colonist.ColonistWeapon.AttackSpeed;
                    //raycast to see if we have hit the target, if we have then make it take damage, if it dies set the target to null
                    if(Physics.Raycast(Colonist.transform.position,Colonist.target.transform.position - Colonist.transform.position,out hit,Colonist.ColonistWeapon.Range,monsterLayerMask))
                    {
                        if(hit.collider == Colonist.target.collider)
                        {
                            Colonist.target.takeDamage(Colonist.ColonistWeapon.Damage);
                            if(Colonist.target.checkDead())
                            {
                                Colonist.target = null;
                            }
                            return Status.SUCCESS;
                        }
                    }
                }
                return Status.FAILURE;
            }
        }
    }
}
