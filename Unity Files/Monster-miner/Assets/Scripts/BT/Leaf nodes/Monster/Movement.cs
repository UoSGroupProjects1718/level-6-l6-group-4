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
            case MonsterController.MovementState.Wonder:
                Monster.Movement.MoveToPoint(Wonder(Monster.transform));
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
            default:
                return Status.INVALID;
                break;
        }
        return Status.SUCCESS;

    }

    Vector3 Wonder(Transform transform) {
        Vector3 circleCentre = transform.forward.normalized * 5 +transform.position;
        float Angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 ret = new Vector3(circleCentre.x + WonderRadius * Mathf.Sin(Angle), circleCentre.y, circleCentre.z + WonderRadius * Mathf.Cos(Angle));
        return ret;
    }

    Vector3 Flee(MonsterController Monster) {
        return 2 * Monster.transform.position - new Vector3(Monster.currentTarget.position.x, Monster.currentTarget.position.y, Monster.currentTarget.position.z);
    }

    Vector3 Chase(MonsterController Monster) {
        return Monster.currentTarget.transform.position;
    }
}
