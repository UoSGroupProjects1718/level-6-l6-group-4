using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Decorators/IsJobType")]
        public class IsJobTypeDecorator : Decorator
        {
            [SerializeField]
            private ColonistJobType WorkerType;

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if(Colonist.ColonistJob == WorkerType)
                {
                    Child.tick(Colonist);
                    return Status.SUCCESS;
                }
                return Status.FAILURE;
            }


        }
    }
}
