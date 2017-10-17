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
                    if(HasHit(Colonist))
                    {
                        Colonist.nextAttack = Time.time + Colonist.ColonistWeapon.AttackSpeed;
                        Colonist.target.takeDamage(Colonist.ColonistWeapon.Damage);
                        if (Colonist.target.checkDead())
                        {
                            Colonist.target.GetComponent<MeshRenderer>().material.color = Color.red;
                            JobManager.Instance.CreateJob(JobType.Harvesting, 50, Colonist.target.gameObject, Colonist.target.transform.position, "Harvest" + Colonist.target.monsterName);
                            Colonist.target.Movement.navMeshAgent.isStopped = true;
                            Colonist.target.collider.enabled = false;
                            BehaviourTreeManager.Monsters.Remove(Colonist.target);
                            Colonist.target = null;
                        }
                    }
                   
                }
                return Status.FAILURE;
            }
       
            private bool HasHit(ColonistController Colonist)
            {
                float hitChance = Random.Range(0, 100);
                return (hitChance <= Colonist.ColonistWeapon.Accuracy);

            }
        }
    }
}
