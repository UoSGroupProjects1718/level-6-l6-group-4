
using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/RepeatDecorator")]
        public class RepeatDecorator : Decorator
        {
            protected int Limit;
            protected int Counter;

            public void setCount(int count)
            {
                Limit = count;
            }
            public override void onInitialise()
            {
                Counter = 0;
            }
            public override Status Update()
            {
                for (int counter = Counter; counter < Limit; counter++)
                {
                    Counter = counter;
                    Child.tick();
                    if (Child.GetStatus() == Status.RUNNING) break;
                    if (Child.GetStatus() == Status.FAILURE) return Status.FAILURE;
                    if (Counter == Limit) return Status.SUCCESS;
                }
                return Status.INVALID;
            }




        }
    }
}