using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ColonistJobType
{
    Hunter,
    Builder,
    Scout,
}

[RequireComponent(typeof(NavMeshAgent))]
public class ColonistController : MonoBehaviour {

    #region Variables
    public string ColonistName;
    public float ColonistSpeed;
    public float ColonistWorkSpeed;
    public ColonistJobType ColonistJob;
    public Job currentJob;

    public float Health = 100;

    public Weapon ColonistWeapon;
    public float nextAttack;
    public MonsterController target;

    public GameTime lastWorked;

    [HideInInspector]
    public bool hasPath;

    [HideInInspector]
    public AgentMovement agentMovement;
    [HideInInspector]
    public new Collider collider;

    [HideInInspector]
    public GameObject GathererStockpile;

    NavMeshAgent agent;

    public NavMeshAgent NavMeshAgent
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

    #endregion

    private void Awake()
    {
        BehaviourTreeManager.Colonists.Add(this);
        agentMovement = GetComponent<AgentMovement>();
        gameObject.GetComponent<NavMeshAgent>().speed = ColonistSpeed;
        collider = gameObject.GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        lastWorked = TimeManager.Time;
      
    }

    public void OnDrawGizmos()
    {
        if(ColonistWeapon != null && ColonistJob == ColonistJobType.Hunter)
        Gizmos.DrawWireSphere(transform.position, ColonistWeapon.Range);
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
        if (checkDead())
            Death();
    }

    public bool checkDead()
    {
        if (Health < 0)
            return true;
        return false;
    }

    void Death()
    {
        Debug.Log(ColonistName + " has died.");
    }

}
