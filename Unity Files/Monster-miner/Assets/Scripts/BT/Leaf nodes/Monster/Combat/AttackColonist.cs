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
            if (!((Monster.currentTarget.position - Monster.transform.position).magnitude < Monster.combatRange && Time.time > Monster.nextAttack))
            {
                return Status.SUCCESS;
            }



            MonsterController targetMonster = Monster.currentTarget.GetComponent<MonsterController>();
            ColonistController targetColonist = Monster.currentTarget.GetComponent<ColonistController>();
            if (targetColonist != null)
            {
                targetColonist.TakeDamage(Monster.damage);
            }
            else if (targetMonster != null)
            {
                targetMonster.TakeDamage(Monster.damage);
            }
            else
            {
                Debug.LogError("Monster is Chasing but has nothing to attack");
            }


            Monster.nextAttack = Time.time + Monster.attackSpeed;
            return Status.SUCCESS;

        }
        return Status.FAILURE;
    }
}
