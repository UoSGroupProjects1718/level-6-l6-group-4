using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ColonistJobType
{
    Hunter,
    Builder,
    Scout,
    Farmer,
    Blacksmith,
}

[RequireComponent(typeof(NavMeshAgent))]
public class ColonistController : MonoBehaviour {

    #region Variables
    public string ColonistName;
    public int RequiredNutritionPerDay = 25;
    public ColonistJobType ColonistJob;
    public Job currentJob;


    public float ColonistBaseMoveSpeed = 5f;
    public float ColonistMoveSpeed;

    public float ColonistBaseWorkSpeed = 15f;
    public float ColonistWorkSpeed;

    public int maxHealth = 100;
    public float Health;

    public Weapon colonistWeapon;
    public float nextAttack;
    public MonsterController target;

    public GameTime lastWorked;
    public GameTime TimeOfNextMeal;

    [HideInInspector]
    public bool hasPath;

    [HideInInspector]
    public AgentMovement agentMovement;
    [HideInInspector]
    public new Collider collider;

    [HideInInspector]
    public GameObject GathererStockpile;

    NavMeshAgent agent;

    [HideInInspector]
    public bool selected;

    [Header("Selection Circle must be first child")]
    public Projector SelectionCircle;

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

    private void Start()
    {
        BehaviourTreeManager.Colonists.Add(this);

        agentMovement = GetComponent<AgentMovement>();
        collider = gameObject.GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();

        lastWorked = TimeManager.Instance.IngameTime;


        Health = maxHealth;
        ColonistMoveSpeed = ColonistBaseMoveSpeed;
        ColonistWorkSpeed = ColonistBaseWorkSpeed;
        UpdateMoveSpeed(ColonistBaseMoveSpeed);
        //set the selection cirlce
        SelectionCircle = transform.GetChild(0).GetComponent<Projector>();
        SetTimeOfNextMeal();
    }

    public void OnDrawGizmos()
    {
        if(colonistWeapon != null && ColonistJob == ColonistJobType.Hunter)
        Gizmos.DrawWireSphere(transform.position, colonistWeapon.Range);
    }

    public void SetTimeOfNextMeal()
    {
        GameTime time = TimeManager.Instance.IngameTime;
        time.Date.x++;
        //figure out if we need to increment the date at all
        if (time.Date.x >= 28)
        {
            time.Date.y += 1;
            time.Date.x = 1;
            //if we get to 1 year
            if (time.Date.y >= 12)
            {
                time.Date.z += 1;
                time.Date.y = 1;
            }
        }
        time.hours = 6;

        TimeOfNextMeal = time;
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
    public void UpdateMoveSpeed(float MoveSpeed)
    {
        //store the old move base move speed
        float OldBase = ColonistBaseMoveSpeed;
        //then update the move speed
        ColonistBaseMoveSpeed = MoveSpeed;
        //figure out the difference between the old and current move speeds
        float diffBaseCurrent = OldBase - ColonistMoveSpeed;
        //then apply the new move speed with the current move speed penalty enacted
        ColonistMoveSpeed = ColonistBaseMoveSpeed - diffBaseCurrent;
        //and update the navmeshagent
        GetComponent<NavMeshAgent>().speed = ColonistMoveSpeed;
    }
    public void UpdateWorkSpeed(float WorkSpeed)
    {
        //store the old base work speed
        float OldBase = ColonistBaseWorkSpeed;
        //update the base work speed
        ColonistBaseWorkSpeed = WorkSpeed;
        //then figure out the difference between the old base and the current speed
        float diffBaseCurrent = OldBase - ColonistWorkSpeed;
        //and apply the  new work speed with the current pentalty enacted
        ColonistWorkSpeed = ColonistBaseWorkSpeed - diffBaseCurrent;
    }
    public void UpdateDamageResistance()
    {

    }

}
