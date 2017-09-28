using UnityEngine;

namespace BehaviourTree
{
    public class Leaf : BehaviourBase
    {
        [HideInInspector]
       public AgentInfo Agent;

        public override void OnInstantiate(AgentInfo agent)
        {
            Agent = agent;
        }

    }
}
