using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public enum MovementState
    {
        Wonder,
        Flee,
        Chase,
        Still
    }

    public MovementState currentState;
    public Transform currentTarget;

    public string MonsterName;
    public float MonsterSpeed;

    public MonsterMovement Movement;

    // Use this for initialization
    void Awake () {
        BehaviourTreeManager.Monsters.Add(this);
        currentState = MovementState.Wonder;
        Movement = GetComponent<MonsterMovement>();
    }
}
