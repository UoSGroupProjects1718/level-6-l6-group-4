using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/SmallCarnivorSelectState")]
public class SmallCarnivorState : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {

        if (Monster.isDead)
        {
            Monster.currentState = MonsterController.MovementState.Still;

            return Status.SUCCESS;
        }

        if (Monster.hunger < 0)
        {
            Monster.TakeDamage(Monster.hungerDamage,true);
        }
        else
        {
            Monster.hunger -= Monster.hungerLossPerSecond * Time.deltaTime;
        }

        Transform pos = Monster.transform;

        //LC = Large Carnivore
        //SH = Small Herbivore
        float closeLCDist = float.MaxValue;
        Transform closestLC = null;

        float closeSHDist = float.MaxValue;
        Transform closestSH = null;

        for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
        {
            MonsterController currentMonster = BehaviourTreeManager.Monsters[i];
            //intended error. needs to be put into other check functions
            if (currentMonster.isDead)
                continue;

            if (currentMonster.monsterType == MonsterTypes.TypeOfMonster.LargeCarnivore)
            {
                float thisDist = (currentMonster.transform.position - pos.position).magnitude;
                if (thisDist < closeLCDist)
                {
                    closeLCDist = thisDist;
                    closestLC =currentMonster.transform;
                }
            }

            else if (currentMonster.monsterType == MonsterTypes.TypeOfMonster.SmallHerbivore)
            {
                float thisDist = (currentMonster.transform.position - pos.position).magnitude;
                if (thisDist < closeSHDist)
                {
                    closeSHDist = thisDist;
                    closestSH = currentMonster.transform;
                }
            }
        }



        float closeColonistDist = float.MaxValue;
        Transform closestColonist = null;
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            ColonistController currentColonist = BehaviourTreeManager.Colonists[i];
            if (currentColonist.isDead)
                continue;
            float thisDist = (currentColonist.transform.position - pos.position).magnitude;
            if (thisDist < closeColonistDist)
            {
                closeColonistDist = thisDist;
                closestColonist = currentColonist.transform;
            }
        }

        //hunt for hunger

        if(Monster.hunger < Monster.maxHunger * Monster.hungerAttackPercentage/100f)
        {
            Monster.currentState = MonsterController.MovementState.Chase;
            if (closeSHDist < Monster.viewRange)//if close herb, chase it
            {
                Monster.currentTarget = closestSH;
                return Status.SUCCESS;
            }

            if(closeColonistDist < Monster.viewRange)//if close colonist, chase it
            {
                Monster.currentTarget = closestColonist;
                return Status.SUCCESS;
            }
        }


        //flee

        if(Monster.health < Monster.maxHealth / 4)
        {
            Monster.currentState = MonsterController.MovementState.Flee;
            if (closeLCDist < Monster.viewRange)//if close herb, chase it
            {
                Monster.currentTarget = closestLC;
                return Status.SUCCESS;
            }

            if (closeColonistDist < Monster.viewRange)//if close colonist, chase it
            {
                Monster.currentTarget = closestColonist;
                return Status.SUCCESS;
            }
        }

        //love

        if (Time.time - Monster.lastMatingTime < Monster.matingCooldown)
        {
            Monster.currentState = MonsterController.MovementState.MakeLove;
            return Status.SUCCESS;
        }

        Monster.currentState = MonsterController.MovementState.Wander;
        return Status.SUCCESS;

    }

    
}
