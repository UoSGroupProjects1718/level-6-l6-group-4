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

                    Children.Add(child);
                }
            }

            public void RemoveChild(BehaviourBase child) { Children.Remove(child); }

        }
    }
}
