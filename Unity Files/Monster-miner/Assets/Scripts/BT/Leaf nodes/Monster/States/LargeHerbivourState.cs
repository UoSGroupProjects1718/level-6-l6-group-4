using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/LargeHerbivourSelectState")]
public class LargeHerbivourState : BehaviourBase
{
    public override Status UpdateFunc(MonsterController Monster)
    {

        if (Monster.checkDead())
        {
            Monster.currentState = MonsterController.MovementState.Still;
            if (Monster.deathCount == 0)
            {
                Monster.deathCount = 200;
                Monster.Death();
            }
            else if (Monster.deathCount-- == 1)
            {
                Monster.GetMonster();
            }
            return Status.SUCCESS;
        }

        if (Monster.health < Monster.maxHealth)//if attacked
        {
            Transform pos = Monster.transform;
            float Dist = float.MaxValue;
            Transform Closest = null;
            for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
            {
                float thisDist = (BehaviourTreeManager.Colonists[i].transform.position - pos.position).magnitude;
                if (thisDist < Dist)
                {
                    Dist = thisDist;
                    Closest = BehaviourTreeManager.Colonists[i].transform;
                }
            }

            if (Closest == null || Dist > Monster.viewRange)//if not near enemy, then Wander
            {
                Monster.currentState = MonsterController.MovementState.Wander;
                return Status.SUCCESS;
            }

            Monster.currentTarget = Closest;//start attacking
            Monster.currentState = MonsterController.MovementState.Chase;
            return Status.SUCCESS;
        }

        Monster.currentState = MonsterController.MovementState.Wander;
        return Status.SUCCESS;
    }
}