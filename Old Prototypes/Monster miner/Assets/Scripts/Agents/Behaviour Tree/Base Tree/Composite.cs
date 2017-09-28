using UnityEngine;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Composite : BehaviourBase
    {
        [SerializeField]
        protected List<BehaviourBase> Children = new List<BehaviourBase>();

        public void addChild(BehaviourBase child)
        {
            if (Children == null)
                Children = new List<BehaviourBase>();
            Children.Add(child);
        }
        public void removeChild(BehaviourBase child) { Children.Remove(child); }

        public override void OnInstantiate(AgentInfo agent)
        {
            //because of how scriptable objects work, we need to make clones of each of it's children
            //so we need to keep track of what the children were
            BehaviourBase[] tempChild = Children.ToArray();

            //then clear the list
            Children.Clear();

            //then we need to loop through the array and clone them into the list of children
            for(int i = 0; i < tempChild.Length; i++)
            {
                addChild(Instantiate(tempChild[i]));
            }
            //then following this we need to call their respective oninstantiate methods
            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].OnInstantiate(agent);
            }
        }

    }
}
