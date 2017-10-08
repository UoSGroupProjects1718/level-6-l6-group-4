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


    public float Health = 100;


    public Weapon ColonistWeapon;
    public float nextAttack;
    public MonsterController target;


    [HideInInspector]
    public bool hasPath;

    [HideInInspector]
    public AgentMovement agentMovement;
    [HideInInspector]
    public new Collider collider;


    private void Awake()
    {
        BehaviourTreeManager.Colonists.Add(this);
        agentMovement = GetComponent<AgentMovement>();
        gameObject.GetComponent<NavMeshAgent>().speed = ColonistSpeed;
        collider = gameObject.GetComponent<Collider>();
      
    }

    public void OnDrawGizmos()
    {
        if(ColonistWeapon != null && ColonistJob == JobType.Hunter)
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
