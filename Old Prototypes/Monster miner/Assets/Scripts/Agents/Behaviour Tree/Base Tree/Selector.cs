using UnityEngine;

namespace BehaviourTree
{
    /// <summary>
    ///The selector will return failure only when none of it's child behaviours return sucess. Otherwise known as the
    ///"Fallback" node, if a child returns failure, it will check the next child in the list. Checking in order of priority from the first
    ///child to the last (left to right)
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Selector")]
    public class Selector : Composite
    {
        protected BehaviourBase Current;
        private int index;
        public override void onInitialise()
        {
            index = 0;
            Current = Children[index];
        }
        public override Status updateFunc()
        {
            //loop through every child
            for (int i = 0; i < Children.Count; i++)
            {
                //then call their tick function
                Status childStatus = Children[i].tick();
                //if it is running, return running
                if (childStatus == Status.BT_RUNNING)
                {
                    return Status.BT_RUNNING;
                }
                //or if it succeeds then return the same
                else if (childStatus == Status.BT_SUCCESS)
                    return Status.BT_SUCCESS;
            }
            //if none of the child behaviours succeed then the selector must do the same
            return Status.BT_FAILURE;
        }
    }
}
