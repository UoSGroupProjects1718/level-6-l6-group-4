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
            }
            public override Status Update()
            {
                Children[index].tick();
                switch (Children[index].GetStatus())
                {
                    case Status.SUCCESS:
                        index = 0;
                        return Status.SUCCESS;

                    case Status.FAILURE:
                        index++;
                        if (index == Children.Count)
                        {
                            index = 0;
                            return Status.FAILURE;
                        }
                        return Status.RUNNING;
                    case Status.RUNNING:
                        return Status.RUNNING;
                    case Status.INVALID:
                        return Status.INVALID;
                    default:
                        break;
                }

                return Status.FAILURE;


                ////loop through every child
                //for (int i = 0; i < Children.Count; i++)
                //{
                //    //then call their tick function
                //    Status childStatus = Children[i].tick();
                //    //if it is running, return running
                //    if (childStatus == Status.RUNNING)
                //    {
                //        return Status.RUNNING;
                //    }
                //    //or if it succeeds then return the same
                //    else if (childStatus == Status.SUCCESS)
                //        return Status.SUCCESS;
                //}
                ////if none of the child behaviours succeed then the selector must do the same
                //return Status.FAILURE;
            }
        }    
    }
}
