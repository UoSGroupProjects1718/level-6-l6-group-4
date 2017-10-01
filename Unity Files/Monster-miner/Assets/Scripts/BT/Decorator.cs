using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        public class Decorator : BehaviourBase
        {
            [SerializeField]
            protected BehaviourBase Child;

            public override void OnInstantiate()
            {
                //because of how scriptable objects work we need to clone the child to make an instance of it
                //so we need to keep track of what the child was
                BehaviourBase tempChild = Child;

                //then instantiate a new clone to take its place
                Child = Instantiate(tempChild);
            }

        }
    }
}
