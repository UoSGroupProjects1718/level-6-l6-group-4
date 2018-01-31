using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/SmallHerbivourSelectState")]

public class SmallHerbivourState : BehaviourBase
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

        Monster.currentState = MonsterController.MovementState.Flee;

        Transform pos = Monster.transform;

        //LC = Large Carnivore
        float closeMonDist = float.MaxValue;
        Transform closestMon = null;

        for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
        {
            
            MonsterController currentMonster = BehaviourTreeManager.Monsters[i];
            if (currentMonster.isDead)
                continue;
            if (currentMonster.monsterType == MonsterTypes.TypeOfMonster.LargeCarnivore || currentMonster.monsterType == MonsterTypes.TypeOfMonster.SmallCarnivore)
            {
                float thisDist = (currentMonster.transform.position - pos.position).magnitude;
                if (thisDist < closeMonDist)
                {
                    closeMonDist = thisDist;
                    closestMon = currentMonster.transform;
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
        //colonist is closer
        if (closeColonistDist < closeMonDist)
        {
            if (closeColonistDist < Monster.viewRange)
            {
                Monster.currentTarget = closestColonist;
                return Status.SUCCESS;
            }
        }

        else
        {
            if (closeMonDist < Monster.viewRange)
            {
                Monster.currentTarget = closestMon;
                return Status.SUCCESS;
            }
        }

        Monster.currentState = MonsterController.MovementState.Wander;
        return Status.SUCCESS;
    }
}
