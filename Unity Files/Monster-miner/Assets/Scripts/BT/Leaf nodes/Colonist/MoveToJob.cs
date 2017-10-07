using System;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Move to job")]
        public class MoveToJob : BehaviourBase
        {
            [SerializeField]
            private float MinDistForPathSuccess = .5f;

            public override Status UpdateFunc(ColonistController Colonist)
            {
                //just in case it sneaks through
                if (Colonist.currentJob == null)
                    return Status.FAILURE;

                RaycastHit hit = new RaycastHit();
                Debug.DrawRay(Colonist.transform.position, Colonist.currentJob.jobLocation - Colonist.transform.position,Color.red);
                if(Physics.Raycast(Colonist.transform.position,Colonist.currentJob.jobLocation - Colonist.transform.position,out hit, MinDistForPathSuccess) /*&& hit.collider.GetInstanceID() == Colonist.currentJob.InteractionObject.GetInstanceID()*/)
                {
                    Debug.Log("You have reached your destination");
                    return Status.SUCCESS;
                }
                if(Colonist.hasPath == false)
                {
                    Colonist.agentMovement.MoveToPoint(Colonist.currentJob.jobLocation);
                    return Status.RUNNING;
                }
                return Status.RUNNING;
            }
            
        }
       
 
    }
}