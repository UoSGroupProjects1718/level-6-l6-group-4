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

    public int monsterType;
    public string monsterName;
    public float monsterSpeed;
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

    #endregion

    void Awake () {
        BehaviourTreeManager.Monsters.Add(this);
        currentState = MovementState.Wander;
        Movement = GetComponent<MonsterMovement>();
        collider = GetComponent<Collider>();
    }

     public void takeDamage(float damage)
    {
        health -= damage;
        if (checkDead())
            Death();
    }

    public bool checkDead()
    {
        if (health < 0)
            return true;
        return false;
    }

    void Death() {
        Debug.Log(monsterName + " has died.");
    }
}
