using UnityEngine;


public class MonsterController : MonoBehaviour {

    public enum MovementState
    {
        Wander,
        Flee,
        Chase,
        Still,
        MakeLove
    }

    #region Variables

    public MovementState currentState;
    public Transform currentTarget;

    public MonsterTypes.TypeOfMonster monsterType;
    public string monsterName;

    public int numHunters;

    public float monsterSpeed;

    public float health;
    public float maxHealth;
    public float combatRange;
    public float viewRange;
    public float damage;
    public float attackSpeed;
    public float nextAttack;
    [HideInInspector]
    public float lastMatingTime;
    public float matingCooldown;
    [HideInInspector]
    public float hunger;
    public float maxHunger;
    [Range(0,1)]
    public float hungerAttackPercentage;
    public float hungerDamage;
    public float hungerLossPerSecond;
    public float naturalRegen;
    [HideInInspector]
    public MonsterMovement Movement;
    [HideInInspector]
    public new Collider collider;
    public DropTable dropTable;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool selected;
    [HideInInspector]
    public bool beingHunted;
    [HideInInspector]
    [Header("Selection Circle must be first child")]
    public Projector SelectionCircle;
    [HideInInspector]
    public float wanderTimer;
    [HideInInspector]
    public float wanderRepathTimer = 0;

    #endregion

    void Start () {
        BehaviourTreeManager.Monsters.Add(this);
        Movement = GetComponent<MonsterMovement>();
        collider = GetComponentInChildren<Collider>();
        lastMatingTime = Time.time;
        Movement.navMeshAgent.speed = monsterSpeed;
        //set the selection cirlce
        SelectionCircle = transform.GetComponentInChildren<Projector>();
    }

     public void TakeDamage(float damage)
    {
        health -= damage;
        CheckDead();
    }

    public bool CheckDead()
    {
        if (health < 0) { 
            Death();
            return true;
        }
        return false;
    }

    public void Death() {
        isDead = true;
        Debug.Log(monsterName + " has died.");
    }
    
    public void GetMonster()
    {
        isDead = false;      
        lastMatingTime = Time.time;
        health = maxHealth;
        hunger = maxHunger;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
