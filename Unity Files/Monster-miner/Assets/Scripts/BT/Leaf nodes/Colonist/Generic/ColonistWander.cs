using UnityEngine;
using UnityEngine.AI;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        //https://forum.unity.com/threads/solved-random-wander-ai-using-navmesh.327950/
        [CreateAssetMenu(menuName = "BehaviourTree/Leaf nodes/Colonist/Generic/Wander")]
        public class ColonistWander : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                Colonist.wanderTimer += Time.deltaTime;
                if (Colonist.wanderTimer >= Colonist.wanderRepathTimer)
                {
                    //get a new position
                    Vector3 newPos = RandomNavSphere(Vector3.zero, Colonist.wanderRadius, -1);
                    //path there
                    Colonist.NavMeshAgent.SetDestination(newPos);
                    //and set the timers to defaults
                    Colonist.wanderTimer = 0;
                    Colonist.wanderRepathTimer = Random.Range(2f, 6f);
                    return Status.SUCCESS;
                }
                return Status.FAILURE;
            }
            //find a random point within a unit sphere
            private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
            {

                Vector3 randomDirection = Random.insideUnitSphere * distance;


                randomDirection += origin;
                NavMeshHit navHit;
                //sample whether we can hit a point
                NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
                //then return the position
                return navHit.position;
            }
        }
    }
}
