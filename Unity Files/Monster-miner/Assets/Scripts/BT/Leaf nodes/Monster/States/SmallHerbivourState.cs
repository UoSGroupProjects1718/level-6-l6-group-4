﻿using System.Collections;
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
            Monster.health += Monster.naturalRegen;

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
            if(Time.time - Monster.lastMatingTime > Monster.matingCooldown)
            {
                Monster.currentState = MonsterController.MovementState.MakeLove;
                return Status.SUCCESS;
            }
            Monster.currentState = MonsterController.MovementState.Wander;
            return Status.SUCCESS;
        }
        
        Monster.currentTarget = Closest;
        Monster.currentState = MonsterController.MovementState.Flee;
        return Status.SUCCESS;
    }
}
