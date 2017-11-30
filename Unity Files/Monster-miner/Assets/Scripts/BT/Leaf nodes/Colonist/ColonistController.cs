using UnityEngine;
using UnityEngine.AI;

public enum ColonistJobType
{
    Hunter,
    Crafter,
    Scout,
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Equipment))]
public class ColonistController : MonoBehaviour {

    #region Variables
    #region basic info
    public string colonistName;
    public float colonistBaseMoveSpeed = 5f;
    public float colonistMoveSpeed;

    public float colonistBaseWorkSpeed = 15f;
    public float colonistWorkSpeed;

    public int maxHealth = 100;
    public float health;

    public int requiredNutritionPerDay = 25;
    public GameTime timeOfNextMeal;
    #endregion
    #region Job
    public ColonistJobType colonistJob;
    public Job currentJob;
    public GameTime lastWorked;
    #endregion
    #region Equipment and combat
    public float nextAttack;
    public MonsterController target;

  

    public Equipment colonistEquipment;
    
    #endregion
    #region misc
    [HideInInspector]
    public bool hasPath;

    [HideInInspector]
    public new Collider collider;

    [HideInInspector]
    public GameObject gathererStockpile;

    NavMeshAgent agent;

    [HideInInspector]
    public bool selected;

    [Header("Selection Circle must be first child")]
    public Projector selectionCircle;

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
    [HideInInspector]
    public float wanderRepathTimer;
    [HideInInspector]
    public float wanderTimer;

    public float wanderRadius = 4f;
    #endregion
    #endregion

    private void Start()
    {
        BehaviourTreeManager.Colonists.Add(this);

        collider = gameObject.GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();

        lastWorked = TimeManager.Instance.IngameTime;

        colonistEquipment = GetComponent<Equipment>();

        health = maxHealth;
        colonistMoveSpeed = colonistBaseMoveSpeed;
        colonistWorkSpeed = colonistBaseWorkSpeed;
        UpdateMoveSpeed(colonistBaseMoveSpeed);
        //set the selection cirlce
        selectionCircle = transform.GetChild(0).GetComponent<Projector>();
        selectionCircle.gameObject.SetActive(false);
        SetTimeOfNextMeal();

        EquipDefaultGear();
        
    }

    public void OnDrawGizmos()
    {
        if(colonistEquipment != null && colonistEquipment.weapon != null && colonistJob == ColonistJobType.Hunter)
            Gizmos.DrawWireSphere(transform.position, colonistEquipment.weapon.Range);
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

        timeOfNextMeal = time;
    }

    public void TakeDamage(float damage)
    {
        //find the damage we resisted (converting damage reduction to a value between 0 and 1)
        float resistedDamage = damage * (colonistEquipment.damageReduction / 100);

        health -= (damage - resistedDamage);

        if (CheckDead())
            Death();
    }

    public bool CheckDead()
    {
        if (health < 0)
            return true;
        return false;
    }

    void Death()
    {
        Debug.Log(colonistName + " has died.");
    }

    public void UpdateMoveSpeed(float MoveSpeed)
    {
        //store the old move base move speed
        float OldBase = colonistBaseMoveSpeed;
        //then update the move speed
        colonistBaseMoveSpeed = MoveSpeed;
        //figure out the difference between the old and current move speeds
        float diffBaseCurrent = OldBase - colonistMoveSpeed;
        //then apply the new move speed with the current move speed penalty enacted
        colonistMoveSpeed = colonistBaseMoveSpeed - diffBaseCurrent;
        //and update the navmeshagent
        GetComponent<NavMeshAgent>().speed = colonistMoveSpeed;
    }
    public void UpdateWorkSpeed(float WorkSpeed)
    {
        //store the old base work speed
        float OldBase = colonistBaseWorkSpeed;
        //update the base work speed
        colonistBaseWorkSpeed = WorkSpeed;
        //then figure out the difference between the old base and the current speed
        float diffBaseCurrent = OldBase - colonistWorkSpeed;
        //and apply the  new work speed with the current pentalty enacted
        colonistWorkSpeed = colonistBaseWorkSpeed - diffBaseCurrent;
    }

    private void EquipDefaultGear()
    {
        switch (colonistJob)
        {
            case ColonistJobType.Hunter:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crossbow") as Wearable);
                break;
            case ColonistJobType.Crafter:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crossbow") as Wearable);
                break;
            case ColonistJobType.Scout:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crossbow") as Wearable);
                break;
            default:
                break;
        }
    }
}
