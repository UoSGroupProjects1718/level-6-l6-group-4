using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/testLEaf")]
public class TestLeaf : BehaviourBase {
    
    public override Status UpdateFunc(ColonistController Colonist)
    {
        Debug.Log(Colonist.gameObject.name);
        return Status.SUCCESS;
    }

    public override Status UpdateFunc(MonsterController Colonist)
    {
        Debug.Log(Colonist.gameObject.name);
        return Status.SUCCESS;
    }
}
