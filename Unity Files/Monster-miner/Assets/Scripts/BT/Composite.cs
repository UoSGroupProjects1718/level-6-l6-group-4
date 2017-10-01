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

            public void AddChild(BehaviourBase child)
            {
                if(Children == null)
                {
                    Children = new List<BehaviourBase>();
                    
                }
                BehaviourBase tempChild = child;

                Children.Add(tempChild);
            }

            public void RemoveChild(BehaviourBase child) { Children.Remove(child); }

        }
    }
}
