using System;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Move to job")]
        public class MoveToJob : BehaviourBase
        { 

            public override Status UpdateFunc(ColonistController Colonist)
            {
                //just in case it sneaks through
                if (Colonist.currentJob == null)
                    return Status.FAILURE;

               
                Debug.DrawRay(Colonist.transform.position, Colonist.currentJob.jobLocation - Colonist.transform.position, Color.red);
                if(Colonist.hasPath == false)
                {
                    Colonist.NavMeshAgent.SetDestination(Colonist.currentJob.jobLocation);
                }
                if(!Colonist.NavMeshAgent.pathPending)
                {
                    Colonist.hasPath = true;
                    if(pathComplete(Colonist))
                    {
                            Colonist.hasPath = false;
                            return Status.SUCCESS;
                    }
                }
                return Status.RUNNING;
            }

            private bool pathComplete(ColonistController colonist)
            {
                    if(Mathf.RoundToInt(Vector3.Distance(colonist.NavMeshAgent.destination,colonist.transform.position)) <= colonist.NavMeshAgent.stoppingDistance)
                    {
                        if (!colonist.NavMeshAgent.hasPath || colonist.NavMeshAgent.velocity.sqrMagnitude == 0f)
                            return true;
                    }
                return false;
            }
            
        }
       
 
    }
}