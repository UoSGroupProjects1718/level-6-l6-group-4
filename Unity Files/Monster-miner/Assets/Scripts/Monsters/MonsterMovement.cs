using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MonsterMovement : MonoBehaviour {
    public float range = 10;
    public float radius = 5;
    NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Awake () {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
