using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Sequence")]
        public class Sequence : Composite
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                //loop through all children
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i].tick(Colonist) != Status.SUCCESS)
                        return Children[i].GetStatus();
                }
                return Status.SUCCESS;
            }

            public override Status UpdateFunc(MonsterController Monster)
            {
                //loop through all children
                for (int i = 0; i < Children.Count; i++)
                {
                    if (Children[i].tick(Monster) != Status.SUCCESS)
                        return Children[i].GetStatus();
                }
                return Status.SUCCESS;
            }
        }
    }
}