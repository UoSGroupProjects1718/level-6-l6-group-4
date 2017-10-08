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

    public MovementState currentState;
    public Transform currentTarget;

    public string MonsterName;
    public float MonsterSpeed;
    public float Health;
    public float combatRange;
    public float Damage;

    public MonsterMovement Movement;

    // Use this for initialization
    void Awake () {
        BehaviourTreeManager.Monsters.Add(this);
        currentState = MovementState.Wander;
        Movement = GetComponent<MonsterMovement>();
    }

    public void takeDamage(float damage)
    {
        Health -= damage;
        if (checkDead())
            Death();
    }

    bool checkDead()
    {
        if (Health < 0)
            return true;
        return false;
    }

    void Death() {

    }
}
