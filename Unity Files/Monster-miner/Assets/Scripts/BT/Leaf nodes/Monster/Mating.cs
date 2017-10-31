using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/Mating")]
public class Mating : BehaviourBase {

    

	public override Status UpdateFunc(MonsterController Monster) {
        Transform ClosestOfSameType = null;
        if (Monster.currentState != MonsterController.MovementState.MakeLove)
        {
            return Status.SUCCESS;
        }
        float Dist = Monster.viewRange;

        foreach (MonsterController CheckingMonster in BehaviourTreeManager.Monsters)
        {
            if (CheckingMonster.monsterType == Monster.monsterType && Monster!=CheckingMonster) { 
                if (getDist(Monster.transform, CheckingMonster.transform) < Dist && Time.time - CheckingMonster.lastMatingTime > CheckingMonster.matingCooldown )
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

        if(Dist < 5)
        {
            //MAKE A BABY
            Debug.Log("Sexy noises");
            Monster.lastMatingTime = Time.time;
            Monster.currentTarget.GetComponent<MonsterController>().lastMatingTime = Time.time;
        }
        return Status.SUCCESS;
    } 

    float getDist(Transform Monster, Transform Checking) {
        return (Monster.position - Checking.position).magnitude;
    }
}
