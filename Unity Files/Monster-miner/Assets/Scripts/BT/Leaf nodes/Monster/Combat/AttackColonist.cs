using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Combat/Attack")]

public class AttackColonist : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {
        if (Monster.currentState == MonsterController.MovementState.Chase)
        {
            if (Time.time > Monster.nextAttack)
            {
                Monster.nextAttack = Time.time + Monster.attackSpeed;
                Monster.currentTarget.GetComponent<ColonistController>().takeDamage(Monster.damage);
                return Status.SUCCESS;
            }
        }
        return Status.FAILURE;
    }
}
