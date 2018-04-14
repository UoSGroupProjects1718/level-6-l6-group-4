using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/LargeCarnivorSelectState")]
public class LargeCarnivorState : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {
        if (Monster.isDead)
        {
            Monster.currentState = MonsterController.MovementState.Still;
            
            return Status.FAILURE;
        }

        if (Monster.hunger < 0)
        {
            Monster.TakeDamage(Monster.hungerDamage * Time.deltaTime,true); //this was removing hunger damage every frame rather than every second so I multiplied by deltatime
        }
        else
        {
            Monster.hunger -= Monster.hungerLossPerSecond * Time.deltaTime;
        }

        //check hunger var
        if(Monster.hunger < Monster.maxHunger * Monster.hungerAttackPercentage/100f || (Time.time-Monster.lastDamageTime ) > MonsterController.attackTimeAfterDamage)
        {
            Transform pos = Monster.transform;
            float Dist = float.MaxValue;
            Transform Closest = null;
            //check for closest monsters
            for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
            {
                MonsterController currentMonster = BehaviourTreeManager.Monsters[i];
                if (currentMonster.isDead)
                    continue;
                if (!currentMonster.isActiveAndEnabled || currentMonster.monsterType ==Monster.monsterType)
                    continue;
                float thisDist = (currentMonster.transform.position - pos.position).magnitude;
                if (thisDist < Dist)
                {
                    Dist = thisDist;
                    Closest = currentMonster.transform;
                }
                if (Dist < Monster.viewRange)
                {
                    //go hunt
                    Monster.currentTarget = Closest;
                    Monster.currentState = MonsterController.MovementState.Chase;
                    return Status.SUCCESS;
                }
            }

            //check for closest colonist
            for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
            {
                ColonistController currentColonist = BehaviourTreeManager.Colonists[i];
                if (currentColonist.isDead)
                    continue;
                if (!currentColonist.isActiveAndEnabled)
                {
                    continue;
                }
                float thisDist = (currentColonist.transform.position - pos.position).magnitude;
                if (thisDist < Dist)
                {
                    Dist = thisDist;
                    Closest = currentColonist.transform;
                }
            }
            if (Dist < Monster.viewRange)
            {
                //go hunt
                Monster.currentTarget = Closest;
                Monster.currentState = MonsterController.MovementState.Chase;
                return Status.SUCCESS;
            }
        }

        //Check love making
        if (Time.time - Monster.lastMatingTime < Monster.matingCooldown)
        {
            Monster.currentState = MonsterController.MovementState.MakeLove;
            return Status.SUCCESS;
        }


        //if nothing, wander
        Monster.currentState = MonsterController.MovementState.Wander;
        return Status.SUCCESS;
    }
}
