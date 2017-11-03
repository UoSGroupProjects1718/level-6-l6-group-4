using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ find stockpile")]

        public class FindStockpile : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.gathererStockpile != null)
                    return Status.SUCCESS;

                Colonist.gathererStockpile = GetStockpile(Colonist);
                if (Colonist.gathererStockpile == null)
                    return Status.FAILURE;
                Colonist.currentJob.jobLocation = Colonist.gathererStockpile.transform.position;
                return Status.SUCCESS;
            }

            //find the closest stockpile to the agent
            public GameObject GetStockpile(ColonistController Colonist)
            {
                float closestDist = float.MaxValue;
                GameObject currentClosest = null;
                if (Colonist.currentJob.InteractionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
                {

                    for (int i = 0; i < BehaviourTreeManager.Granaries.Count; i++)
                    {
                        float dist = Vector3.Distance(Colonist.transform.position, BehaviourTreeManager.Granaries[i].transform.position);
                        if (dist < closestDist)
                        {
                            currentClosest = BehaviourTreeManager.Granaries[i].gameObject;
                            closestDist = dist;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BehaviourTreeManager.Stockpiles.Count; i++)
                    {
                        float dist = Vector3.Distance(Colonist.transform.position, BehaviourTreeManager.Stockpiles[i].transform.position);
                        if (dist < closestDist)
                        {
                            currentClosest = BehaviourTreeManager.Stockpiles[i].gameObject;
                            closestDist = dist;
                        }
                    }
                }
                return currentClosest;
            }
            private bool pathComplete(ColonistController colonist)
            {
                if (Vector3.Distance(colonist.NavMeshAgent.destination, colonist.transform.position) <= colonist.NavMeshAgent.stoppingDistance)
                {
                    if (!colonist.NavMeshAgent.hasPath || colonist.NavMeshAgent.velocity.sqrMagnitude == 0f)
                        return true;
                }
                return false;
            }
        }
    }
}
