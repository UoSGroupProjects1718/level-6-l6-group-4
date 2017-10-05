using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentMovement))]
public class ColonistController : MonoBehaviour {

    public string ColonistName;
    public float ColonistSpeed;
    public float ColonistWorkSpeed;
    public JobType ColonistJob;
    public Job currentJob;

    [HideInInspector]
    public bool hasPath;

    [HideInInspector]
    public AgentMovement agentMovement;

    private void Awake()
    {
        BehaviourTreeManager.Colonists.Add(this);
        agentMovement = GetComponent<AgentMovement>();
        gameObject.GetComponent<NavMeshAgent>().speed = ColonistSpeed;
    }


}
