//Daniel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public NavMeshAgent navMeshAgent
    {
        get
        {
            return agent;
        }
        private set
        {
            agent = value;
        }
    }
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
