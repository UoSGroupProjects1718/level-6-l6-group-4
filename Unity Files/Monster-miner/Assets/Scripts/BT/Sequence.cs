using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Sequence")]
        public class Sequence : Composite
        {
            

            public override void onInitialise()
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    Children[i].OnInstantiate();
                }

            }
            public override Status Update()
            {
                //loop through all children
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i].GetStatus() == Status.SUCCESS)
                    {
                        continue;
                    }
                    //and execute their tick function
                    Status childStatus = Children[i].tick();
                    //if they are running, return that
                    if (childStatus == Status.RUNNING)
                    {
                        return Status.RUNNING;
                    }
                    //or if they fail, do the same
                    else if (childStatus == Status.FAILURE)
                    {
                        return Status.FAILURE;
                    }
                }
                //if neither of these are true, then they have succeeded, we are at the end of the list of children and the sequencer may return success
                onInitialise();
                return Status.SUCCESS;
            }

        }
    }
}

