using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
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
            public override Status Update()
            {
                //loop through every child
                for (int i = 0; i < Children.Count; i++)
                {
                    //then call their tick function
                    Status childStatus = Children[i].tick();
                    //if it is running, return running
                    if (childStatus == Status.RUNNING)
                    {
                        return Status.RUNNING;
                    }
                    //or if it succeeds then return the same
                    else if (childStatus == Status.SUCCESS)
                        return Status.SUCCESS;
                }
                //if none of the child behaviours succeed then the selector must do the same
                return Status.FAILURE;
            }
        }    
    }
}
