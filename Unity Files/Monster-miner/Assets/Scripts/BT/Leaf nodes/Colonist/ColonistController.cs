using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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
    [Header("Basic info")]
    public string colonistName;
    public float colonistBaseMoveSpeed = 5f;
    public float colonistMoveSpeed;

    public float colonistBaseWorkSpeed = 15f;
    public float colonistWorkSpeed;

    public int maxHealth = 100;
    public float health;

    public int requiredNutritionPerDay = 25;
    public GameTime timeOfNextMeal;
    public bool isDead = false;
    #endregion
    #region Job
    [Header("Job info")]
    public ColonistJobType colonistJob;
    public Job currentJob;
    public GameTime lastWorked;
    #endregion
    #region Equipment and combat
    [Header("Equipment and combat")]
    public float nextAttack;
    public MonsterController target;

  

    public Equipment colonistEquipment;

    #endregion
    #region misc
    [Header("Misc")]
    [SerializeField]
    private float corpseCleanupDelay = 2.0f;
    [SerializeField]
    private ParticleSystem damageBloodFX;
    [SerializeField]
    private ParticleSystem deathBloodFX;

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
        health = maxHealth;
        colonistMoveSpeed = colonistBaseMoveSpeed;
        colonistWorkSpeed = colonistBaseWorkSpeed;
        UpdateMoveSpeed(colonistBaseMoveSpeed);
        //set the selection cirlce
        selectionCircle = transform.GetChild(0).GetComponent<Projector>();
        selectionCircle.enabled = false;
        SetTimeOfNextMeal();        
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
        damageBloodFX.Play();

        if (CheckDead())
            Death();
        //update colonist info panel
        if(UIController.Instance.focusedColonist == this)
        {
            UIController.Instance.UpdateColonistInfoPanel(this);
        }
    }

    public bool CheckDead()
    {
        if (health < 0)
            return true;
        return false;
    }

    void Death()
    {
        isDead = true;
        deathBloodFX.Play();
        Debug.Log(colonistName + " has died.");

        if (currentJob != null)
        {
            //return colonist job back to pool
            switch (currentJob.jobType)
            {
                case JobType.Gathering:
                    JobManager.CreateJob(currentJob.jobType, (int)currentJob.currentWorkAmount, currentJob.interactionItem, currentJob.interactionObject, currentJob.jobLocation, currentJob.jobName);
                    break;
                case JobType.Harvesting:
                    JobManager.CreateJob(currentJob.jobType, (int)currentJob.currentWorkAmount, currentJob.interactionObject, currentJob.jobLocation, currentJob.jobName);
                    break;
                case JobType.Crafting:
                    JobManager.CreateJob(currentJob.jobType, currentJob.RequiredItems, (int)currentJob.currentWorkAmount, currentJob.interactionItem, currentJob.jobLocation, currentJob.jobName);
                    break;
                case JobType.Building:
                    JobManager.CreateJob(currentJob.jobType, currentJob.RequiredItems, (int)currentJob.currentWorkAmount, currentJob.interactionObject, currentJob.jobLocation, currentJob.jobName);
                    break;
                case JobType.Hunter:
                    JobManager.CreateJob(currentJob.jobType, (int)currentJob.currentWorkAmount, currentJob.interactionObject, currentJob.interactionObject.transform.position, currentJob.jobName);
                    break;
                default:
                    break;
            }
        }

        //put the colonist on its side
        transform.Rotate(90, 0, 90);

        //start the corpse cleanup enumerator
        StartCoroutine(CorpseCleanup(corpseCleanupDelay));
    }

    private IEnumerator CorpseCleanup(float cleanupDelay)
    {
        yield return new WaitForSeconds(cleanupDelay);
        ColonistSpawner.Instance.ReturnColonistToPool(gameObject);
    }


    public void UpdateMoveSpeed(float modifier)
    {
        colonistMoveSpeed += modifier;
        colonistMoveSpeed = Mathf.Clamp(colonistMoveSpeed, colonistBaseMoveSpeed, 100);
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

    public void EquipDefaultGear()
    {
        
        switch (colonistJob)
        {
            case ColonistJobType.Hunter:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Hunter Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Wooden Bow") as Wearable);
                break;
            case ColonistJobType.Crafter:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Crafter Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Wooden Bow") as Wearable);
                break;
            case ColonistJobType.Scout:
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Scout Head") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Scout Chest") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Scout Legs") as Wearable);
                colonistEquipment.EquipWearable(ItemDatabase.GetItem("Wooden Bow") as Wearable);
                break;
            default:
                break;
        }
    }
}
