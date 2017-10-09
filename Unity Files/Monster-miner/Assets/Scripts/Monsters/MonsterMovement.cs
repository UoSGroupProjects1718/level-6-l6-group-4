using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MonsterMovement : MonoBehaviour {
    public float range = 10;
    public float radius = 5;
    NavMeshAgent meshAgent;

    public NavMeshAgent navMeshAgent
    {
        get
        {
            return meshAgent;
        }
        private set
        {
            meshAgent = value;
        }
    }


    // Use this for initialization
    void Awake () {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
