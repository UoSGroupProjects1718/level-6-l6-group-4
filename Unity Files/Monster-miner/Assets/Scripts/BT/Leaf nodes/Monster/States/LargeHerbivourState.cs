using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/LargeHerbivourSelectState")]
public class LargeHerbivourState : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {

        if (Monster.isDead)
        {
            Monster.currentState = MonsterController.MovementState.Still;

            return Status.SUCCESS;
        }
        if (Monster.health < Monster.maxHealth)
            Monster.health += Monster.naturalRegen * Time.deltaTime;


        //check hurt
        if (Monster.health < Monster.maxHealth)
        {
            Transform pos = Monster.transform;

            //LC = Large Carnivore
            //SH = Small Herbivore
            float closeLCDist = float.MaxValue;
            Transform closestLC = null;

            for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
            {
                MonsterController currentMonster = BehaviourTreeManager.Monsters[i];
                if (currentMonster.monsterType == MonsterTypes.TypeOfMonster.LargeCarnivore)
                {
                    float thisDist = (currentMonster.transform.position - pos.position).magnitude;
                    if (thisDist < closeLCDist)
                    {
                        closeLCDist = thisDist;
                        closestLC = currentMonster.transform;
                    }
                }
            }

            float closeColonistDist = float.MaxValue;
            Transform closestColonist = null;
            for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
            {
                ColonistController currentColonist = BehaviourTreeManager.Colonists[i];
                float thisDist = (currentColonist.transform.position - pos.position).magnitude;
                if (thisDist < closeColonistDist)
                {
                    closeColonistDist = thisDist;
                    closestColonist = currentColonist.transform;
                }
            }

            Monster.currentState = MonsterController.MovementState.Chase;
            if (closeLCDist < 5)
            {
                Monster.currentTarget = closestLC;
                return Status.SUCCESS;
            }

            if (closeColonistDist < 25)
            {
                Monster.currentTarget = closestColonist;
                return Status.SUCCESS;
            }
        }

        //Check love making
        if (Time.time - Monster.lastMatingTime < Monster.matingCooldown)
        {
            Monster.currentState = MonsterController.MovementState.MakeLove;
            return Status.SUCCESS;
        }

        Monster.currentState = MonsterController.MovementState.Wander;
        return Status.SUCCESS;
    }
}