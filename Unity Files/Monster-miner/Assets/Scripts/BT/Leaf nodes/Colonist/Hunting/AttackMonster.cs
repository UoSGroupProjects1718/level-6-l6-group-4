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
                            CreateJob(JobType.Harvesting, 50, Colonist.target.gameObject, Colonist.target.transform.position, "Harvest" + Colonist.target.monsterName);
                            Colonist.target.Movement.navMeshAgent.isStopped = true;
                            Colonist.target.collider.enabled = false;
                            BehaviourTreeManager.Monsters.Remove(Colonist.target);
                            Colonist.target = null;
                        }
                    }
                    //RaycastHit hit = new RaycastHit();
                    //Colonist.nextAttack = Time.time + Colonist.ColonistWeapon.AttackSpeed;
                    ////raycast to see if we have hit the target, if we have then make it take damage, if it dies set the target to null
                    //if(Physics.Raycast(Colonist.transform.position,Colonist.target.transform.position - Colonist.transform.position,out hit,Colonist.ColonistWeapon.Range,monsterLayerMask))
                    //{
                    //    if(hit.collider == Colonist.target.collider)
                    //    {
                    //        Colonist.target.takeDamage(Colonist.ColonistWeapon.Damage);
                    //        if(Colonist.target.checkDead())
                    //        {
                    //            Colonist.target.GetComponent<MeshRenderer>().material.color = Color.red;
                    //            CreateJob(JobType.Harvesting, 50, Colonist.target.gameObject, Colonist.target.transform.position, "Harvest" + Colonist.target.monsterName);
                    //            Colonist.target.Movement.navMeshAgent.isStopped = true;
                    //            Colonist.target.collider.enabled = false;
                    //            BehaviourTreeManager.Monsters.Remove(Colonist.target);
                    //            Colonist.target = null;
                    //        }
                    //        return Status.SUCCESS;
                    //    }
                    //}
                }
                return Status.FAILURE;
            }
            //make this a function in the job manager when it is not half past midnight
            private void CreateJob(JobType jobType, int MaxWorkAmount, GameObject interactionObject, Vector3 jobLocation, string JobName)
            {
                Job newJob = (Job)CreateInstance("Job");
                newJob.jobName = JobName;
                newJob.InteractionObject = interactionObject;
                newJob.maxWorkAmount = MaxWorkAmount;
                newJob.jobLocation = jobLocation;
                newJob.jobType = jobType;
                JobManager.Instance.QueueJob(newJob);
            }
            private bool HasHit(ColonistController Colonist)
            {
                float hitChance = Random.Range(0, 100);
                return (hitChance <= Colonist.ColonistWeapon.Accuracy);

            }
        }
    }
}
