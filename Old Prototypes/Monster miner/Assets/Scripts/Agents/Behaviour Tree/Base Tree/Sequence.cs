using UnityEngine;


namespace BehaviourTree
{
    /// <summary>
    /// The sequence node will return failure if any of it's child nodes return failure, only when a child node suceeds
    /// will it continue to the next node in the sequence.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Sequence")]

    public class Sequence : Composite
    {
        protected BehaviourBase currentChild;
        //private int childIndex;
        public override void onInitialise()
        {
            //childIndex = 0;
            //always make sure that it is the very front of the list of children
            currentChild = Children[0];
        }
        public override Status updateFunc()
        {
            //loop through all children
            for (int i = 0; i < Children.Count; i++)
            {
                //and execute their tick function
                Status childStatus = Children[i].tick();
                //if they are running, return that
                if (childStatus == Status.BT_RUNNING)
                {
                    return Status.BT_RUNNING;
                }
                //or if they fail, do the same
                else if (childStatus == Status.BT_FAILURE)
                {
                    return Status.BT_FAILURE;
                }
            }
            //if neither of these are true, then they have succeeded, we are at the end of the list of children and the sequencer may return success
            return Status.BT_SUCCESS;
        }
    }
}
