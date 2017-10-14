using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Selector")]
        public class Selector : Composite
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                //loop through every child
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i].tick(Colonist) != Status.FAILURE)
                        return Children[i].GetStatus();
                }
                //if none of the child behaviours succeed then the selector must do the same
                return Status.FAILURE;
            }

            public override Status UpdateFunc(MonsterController Monster)
            {
                //loop through every child
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i].tick(Monster) != Status.FAILURE)
                        return Children[i].GetStatus();
                }
                //if none of the child behaviours succeed then the selector must do the same
                return Status.FAILURE;
            }
        }    
    }
}
