using UnityEngine;
using System.Collections.Generic;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        public class Composite : BehaviourBase
        {
            [SerializeField]
            protected List<BehaviourBase> Children = new List<BehaviourBase>();

        }
    }
}
