using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Combat/TargetDistance")]

public class TargetDistance : BehaviourBase {
    public float Limit = 1;

    public override Status UpdateFunc(MonsterController Monster)
    {
        if (getDistance(Monster) && getAngle(Monster))
            return Status.SUCCESS;
        return Status.FAILURE;
    }

    bool getDistance(MonsterController Monster)
    {
        return ((Monster.currentTarget.position - Monster.transform.position).magnitude < Monster.combatRange);
    }

    bool getAngle(MonsterController Monster)
    {
        Vector3 diffVector =(Monster.transform.position - Monster.currentTarget.transform.position);
        float angle = (Monster.transform.rotation.z - Mathf.Asin(diffVector.x / diffVector.z));
        return (-Limit < angle && angle < Limit);
    }
}
