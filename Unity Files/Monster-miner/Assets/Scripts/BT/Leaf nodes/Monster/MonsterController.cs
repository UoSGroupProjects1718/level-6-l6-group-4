using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour {

    public enum MovementState
    {
        Wander,
        Flee,
        Chase,
        Still
    }

    #region Variables

    public MovementState currentState;
    public Transform currentTarget;

    public MonsterTypes.TypeOfMonster monsterType;
    public string monsterName;
    public float monsterSpeed;
    //[HideInInspector]
    public float health;
    public float maxHealth;
    public float combatRange;
    public float viewRange;
    public float damage;
    public float attackSpeed;
    public float nextAttack;

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

    void Awake () {
        BehaviourTreeManager.Monsters.Add(this);
        currentState = MovementState.Wander;
        Movement = GetComponent<MonsterMovement>();
        collider = GetComponent<Collider>();
        health = maxHealth;

        //set the selection cirlce
        SelectionCircle = transform.GetChild(0).GetComponent<Projector>();
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
        FindObjectOfType<MonsterTypes>().getMonsterData(
            GetMonsterName(),out health, out attackSpeed, out damage, 
            out combatRange, out attackSpeed, out tempMesh, out dropTable);
        maxHealth = health;
        Instantiate(tempMesh);
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
