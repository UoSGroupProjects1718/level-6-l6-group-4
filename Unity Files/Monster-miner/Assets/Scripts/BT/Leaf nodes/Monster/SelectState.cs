using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Monster/SelectState")]
public class SelectState : BehaviourBase {

	public override Status UpdateFunc(MonsterController Monster)
    {

        if(Monster.CheckDead())
        {
            Monster.currentState = MonsterController.MovementState.Still;
            return Status.SUCCESS;
        }
        Transform pos = Monster.transform;
        float Dist = float.MaxValue;
        Transform Closest = null;
        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            float thisDist = (BehaviourTreeManager.Colonists[i].transform.position - pos.position).magnitude;
            if(thisDist < Dist)
            {
                Dist = thisDist;
                Closest = BehaviourTreeManager.Colonists[i].transform;
            }
        }

        if (Closest==null || Dist > Monster.combatRange) {
            Monster.currentState = MonsterController.MovementState.Wander;
            return Status.SUCCESS;
        }

        /*
         DEPENDS ON WHAT THE DINO IS LIKE
         
         */
        Monster.currentTarget = Closest;
        Monster.currentState = MonsterController.MovementState.Flee;
        return Status.SUCCESS;
    }
}
