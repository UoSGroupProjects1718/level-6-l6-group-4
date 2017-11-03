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
    //[HideInInspector]
    public float health;
    public float maxHealth;
    public float combatRange;
    public float viewRange;
    public float damage;
    public float attackSpeed;
    public float nextAttack;
    public float lastMatingTime;
    public float matingCooldown;

    public MonsterMovement Movement;
    [HideInInspector]
    public new Collider collider;
    public DropTable dropTable;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool selected;
    public bool beingHunted;

    [Header("Selection Circle must be first child")]
    public Projector SelectionCircle;

    #endregion

    void Start () {
        BehaviourTreeManager.Monsters.Add(this);
        currentState = MovementState.Wander;
        Movement = GetComponent<MonsterMovement>();
        collider = GetComponent<Collider>();
        lastMatingTime = Time.time;
        GetMonster();
        //set the selection cirlce
        SelectionCircle = transform.GetComponentInChildren<Projector>();
    }

     public void takeDamage(float damage)
    {
        health -= damage;
    }

    public bool checkDead()
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
        Mesh tempMesh = null;
        Material[] materials;
        MonsterTypes.Instance.getMonsterData(
            monsterType,out health, out attackSpeed, out damage, 
            out combatRange, out attackSpeed, out tempMesh, out materials, out dropTable, out matingCooldown, out numHunters, out viewRange);
        lastMatingTime = Time.time;
        transform.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh =  Instantiate(tempMesh);
        transform.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
        maxHealth = health;
        tempMesh = null;
        //MAke a spawn point
    }

    string GetMonsterName()
    {
        return "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRange);
    }
}
