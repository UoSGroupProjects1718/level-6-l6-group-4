using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Decorators/Repeat")]
        public class RepeatDecorator : Decorator
        {
            [SerializeField]
            protected int Limit;
            
            public override Status UpdateFunc(ColonistController Colonist)
            {
                for (int counter = 0; counter < Limit; counter++)
                {
                    Child.tick(Colonist);
                    if (counter == Limit-1)
                        return Status.SUCCESS;
                }
                return Status.INVALID;
            }

            public override Status UpdateFunc(MonsterController Monster)
            {
                for (int counter = 0; counter < Limit; counter++)
                {
                    Child.tick(Monster);
                    if (counter == Limit - 1)
                        return Status.SUCCESS;
                }
                return Status.INVALID;
            }
        }
    }
}