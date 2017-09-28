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

            public virtual Status Update() { return Status.INVALID; }
            public virtual void onInitialise() { }
            public virtual void onTerminate(Status status) { }
            public virtual void OnInstantiate() { }
            public Status tick()
            {
                if (status != Status.RUNNING)
                    onInitialise();
                status = Update();

                if (status != Status.RUNNING)
                    onTerminate(status);
                return status;
            }

            public bool isTerminated() { return status == Status.SUCCESS || status == Status.FAILURE; }
            public bool isRunning() { return status == Status.RUNNING; }
            public Status GetStatus() { return status; }
         
        }
    }
}
