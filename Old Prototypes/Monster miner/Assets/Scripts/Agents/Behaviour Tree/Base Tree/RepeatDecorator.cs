using UnityEngine;

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
        public override Status updateFunc()
        {
            for (int counter = Counter; counter < Limit; counter++)
            {
                Counter = counter;
                Child.tick();
                if (Child.getStatus() == Status.BT_RUNNING) break;
                if (Child.getStatus() == Status.BT_FAILURE) return Status.BT_FAILURE;
                if (Counter == Limit) return Status.BT_SUCCESS;
            }
            return Status.BT_INVALID;
        }




    }
}
