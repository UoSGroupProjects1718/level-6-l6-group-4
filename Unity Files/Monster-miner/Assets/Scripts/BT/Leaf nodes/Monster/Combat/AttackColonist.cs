using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Combat/Attack")]

public class AttackColonist : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {
        //Uncomment when takeDamage is 
        //Monster.currentTarget.GetComponent<ColonistController>().takeDamage(Monster.Damage);
        return Status.SUCCESS;
    }
}
