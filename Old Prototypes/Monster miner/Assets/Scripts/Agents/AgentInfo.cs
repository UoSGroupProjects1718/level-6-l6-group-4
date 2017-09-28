using UnityEngine;

[RequireComponent(typeof(AgentBehaviourTree))]
public class AgentInfo : MonoBehaviour {

    [Header("Agent Attributes")]
    
    public string Name;
    
    public float Speed;

    public Job currentJob;

    public float AgentworkSpeed;

    public JobType WorkerType;

    [HideInInspector]
    public RequestPath pathRequest;

    [HideInInspector]
    public AgentBehaviourTree BT;

    private void Awake()
    {
        BT = GetComponent<AgentBehaviourTree>();
        pathRequest = GetComponent<RequestPath>();
    }
    private void Start()
    {
        GlobalBlackboard.Agents.Add(this);
    }



}
