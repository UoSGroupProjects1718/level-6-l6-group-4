using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/LeafNodes/Debug/StandStill")]

    public class _DEBUG_standStill : Leaf
    {
        public override Status updateFunc()
        {
            Agent.transform.position = Agent.transform.position;
            return Status.BT_SUCCESS;
        }
    }
    
}
