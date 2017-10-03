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
        agentMovement = GetComponent<AgentMovement>();
        FindObjectOfType<BehaviourTreeManager>().Colonists.Add(this);
        gameObject.GetComponent<NavMeshAgent>().speed = ColonistSpeed;
    }


}
