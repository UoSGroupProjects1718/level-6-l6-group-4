using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Colonist/Hunter/Attack monster")]
        public class AttackMonster : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.target == null)
                    return Status.FAILURE;

                if(Time.time > Colonist.nextAttack)
                {
                    if(HasHit(Colonist))
                    {
                        Colonist.nextAttack = Time.time + Colonist.colonistWeapon.AttackSpeed;
                        Colonist.target.takeDamage(Colonist.colonistWeapon.Damage);
                        if (Colonist.target.checkDead())
                        {
                            Colonist.target.transform.GetChild(Colonist.target.transform.childCount - 1).Rotate(new Vector3(0, 0, 90));
                            JobManager.CreateJob(JobType.Harvesting, 50, Colonist.target.gameObject, Colonist.target.transform.position, "Harvest" + Colonist.target.monsterName);
                            Colonist.target.Movement.navMeshAgent.isStopped = true;
                            Colonist.target.collider.enabled = false;
                            BehaviourTreeManager.Monsters.Remove(Colonist.target);
                            Colonist.target = null;
                            Colonist.currentJob = null;
                        }
                    }
                   
                }
                return Status.FAILURE;
            }
       
            private bool HasHit(ColonistController Colonist)
            {
                float hitChance = Random.Range(0, 100);
                return (hitChance <= Colonist.colonistWeapon.Accuracy);

            }
        }
    }
}
