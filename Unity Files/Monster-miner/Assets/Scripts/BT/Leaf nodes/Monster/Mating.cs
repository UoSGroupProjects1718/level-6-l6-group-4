using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Mating")]
public class Mating : BehaviourBase {

    Transform ClosestOfSameType;

	public override Status UpdateFunc(MonsterController Monster) {
        if (Monster.currentState != MonsterController.MovementState.MakeLove)
        {
            return Status.SUCCESS;
        }
        float Dist = Monster.viewRange;

        foreach (MonsterController CheckingMonster in BehaviourTreeManager.Monsters)
        {
            if (CheckingMonster.monsterName == Monster.monsterName) { 
                if (getDist(Monster.transform, CheckingMonster.transform) < Dist)
                {
                    ClosestOfSameType = CheckingMonster.transform;
                    Dist = getDist(Monster.transform, CheckingMonster.transform);
                }
           }
        }
        if (ClosestOfSameType == null)
        {
            Monster.currentState = MonsterController.MovementState.Wander;
            return Status.SUCCESS;
        }

        Monster.currentTarget = ClosestOfSameType;

        if(Dist > 2)
        {
            //MAKE A BABY
        }
        return Status.SUCCESS;
    } 

    float getDist(Transform Monster, Transform Checking) {
        return (Monster.position - Checking.position).magnitude;
    }
}
