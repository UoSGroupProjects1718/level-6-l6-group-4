using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;
[CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Decorators/Monster/TypeDetector")]
public class MonsterTypeDetector : Decorator  {

    [SerializeField]
    List<int> Types;

    public override Status UpdateFunc(MonsterController Monster)
    {
        if (Types.Contains(Monster.monsterType))
        {
            Child.tick(Monster);
            return Status.SUCCESS;
        }
        return Status.FAILURE;
    }
}
