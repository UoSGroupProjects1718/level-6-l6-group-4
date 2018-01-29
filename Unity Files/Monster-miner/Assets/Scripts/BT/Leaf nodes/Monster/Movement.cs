using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;

[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Movement")]
public class Movement : BehaviourBase
{
    public float WonderRadius;

    

    public override Status UpdateFunc(MonsterController Monster)
    {
        switch (Monster.currentState)
        {
            case MonsterController.MovementState.Wander:
                Wonder(Monster);
                break;
            case MonsterController.MovementState.Flee:
                Monster.Movement.MoveToPoint(Flee(Monster));
                break;
            case MonsterController.MovementState.Chase:
                Monster.Movement.MoveToPoint(Chase(Monster));
                break;
            case MonsterController.MovementState.Still:
                Monster.Movement.MoveToPoint(Monster.transform.position);
                break;
            case MonsterController.MovementState.MakeLove:
                Monster.Movement.MoveToPoint(new Vector3(Monster.currentTarget.position.x, Monster.currentTarget.position.y, Monster.currentTarget.position.z));
                break;
            default:
                return Status.INVALID;
        }
        return Status.SUCCESS;

    }

    void Wonder(MonsterController monster) {
        monster.wanderTimer += Time.deltaTime;
        if (monster.wanderTimer >= monster.wanderRepathTimer)
        {
            //get a new position
            Vector3 newPos = Utils.RandomNavSphere(monster.transform.position, 30f, -1);
            //path there
            monster.Movement.MoveToPoint(newPos);
            //and set the timers to defaults
            monster.wanderTimer = 0;
            monster.wanderRepathTimer = Random.Range(2f, 6f);
        }
    }

    Vector3 Flee(MonsterController Monster) {
        return 2 * Monster.transform.position - new Vector3(Monster.currentTarget.position.x, Monster.currentTarget.position.y, Monster.currentTarget.position.z);
    }

    Vector3 Chase(MonsterController Monster) {
        return Monster.currentTarget.transform.position;
    }
}
