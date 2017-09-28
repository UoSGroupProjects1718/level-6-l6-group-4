using UnityEngine;

namespace BehaviourTree
{

    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/LeafNodes/Debug/Is Docket Empty")]
    public class _DEBUG_isDocketEmpty : Leaf
    {
        public override Status updateFunc()
        {
            if(GlobalBlackboard.JobDocket.Count == 0)
            {
                return Status.BT_SUCCESS;
            }
            return Status.BT_FAILURE;
        }
    }
}
