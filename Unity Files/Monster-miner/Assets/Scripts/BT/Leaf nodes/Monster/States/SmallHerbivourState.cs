using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/SmallHerbivourSelectState")]

public class SmallHerbivourState : BehaviourBase
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

        if (Closest == null || Dist > Monster.viewRange)
        {
            Monster.currentState = MonsterController.MovementState.Wander;
            return Status.SUCCESS;
        }
        
        Monster.currentTarget = Closest;
        Monster.currentState = MonsterController.MovementState.Flee;
        return Status.SUCCESS;
    }
}
