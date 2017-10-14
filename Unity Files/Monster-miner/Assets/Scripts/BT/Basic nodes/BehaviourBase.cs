using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        public enum Status
        {
            SUCCESS,
            FAILURE,
            RUNNING,
            INVALID,
        };
        
        public abstract class BehaviourBase : ScriptableObject
        {
            private Status status;

            public virtual Status UpdateFunc(ColonistController Colonist) { return Status.INVALID; }
            public virtual Status UpdateFunc(MonsterController Monster) { return Status.INVALID; }
            public virtual void onInitialise() { }
            public virtual void onTerminate(Status status) { }
            public virtual void OnInstantiate() { }
            public Status tick(ColonistController Colonist)
            {
                if (status != Status.RUNNING)
                    onInitialise();
                status = UpdateFunc(Colonist);

                if (status != Status.RUNNING)
                    onTerminate(status);
                return status;
            }

            public Status tick(MonsterController Monster)
            {
                if (status != Status.RUNNING)
                    onInitialise();
                status = UpdateFunc(Monster);

                if (status != Status.RUNNING)
                    onTerminate(status);
                return status;
            }

            public bool isTerminated() { return status == Status.SUCCESS || status == Status.FAILURE; }
            public bool isRunning() { return status == Status.RUNNING; }
            public Status GetStatus() { return status; }

            /*
             vector3 Target;
             float range;

            bool reachedTarget(gameobj obj){
                return((Target-transform.position).magnitude < range);
            }
            
             */
            
        }
    }
}
