
using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/RepeatDecorator")]
        public class RepeatDecorator : Decorator
        {
            protected int Limit;

            public void setCount(int count)
            {
                Limit = count;
            }
            public override void onInitialise()
            {
                
            }

            public override Status Update()
            {
                for (int counter = 0; counter < Limit; counter++)
                {
                    Child.tick();
                    
                    if (Child.GetStatus() == Status.FAILURE)
                        return Status.FAILURE;
                    if (counter == Limit-1)
                        return Status.SUCCESS;
                }
                return Status.INVALID;
            }
        }
    }
}