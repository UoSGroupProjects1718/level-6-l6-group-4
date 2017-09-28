using UnityEngine;

namespace BehaviourTree
{
    public enum Status
    {
        BT_INVALID,
        BT_SUCCESS,
        BT_FAILURE,
        BT_RUNNING,
        BT_ABORTED,
    };
    public class BehaviourBase : ScriptableObject
    {

        private Status status;
        //public AgentInfo Agent;

        //function to affect what happens on the instantiation of an object (this should only affect the leaf nodes really)
        //giving it an agent input as a basic argument because we mostly want this for passing down the agent blackboard
        public virtual void OnInstantiate(AgentInfo agent) { }
        public virtual Status updateFunc() { return Status.BT_INVALID; }
        public virtual void onInitialise() { }
        public virtual void onTerminate(Status status) { }

        public Status tick()
        {
            if (status != Status.BT_RUNNING)
                onInitialise();

            status = updateFunc();

            if (status != Status.BT_RUNNING)
                onTerminate(status);

            return status;
        }

        public void reset() { status = Status.BT_INVALID; }
        public void abort() { onTerminate(Status.BT_ABORTED); status = Status.BT_ABORTED; }
        public bool isTerminated() { return status == Status.BT_SUCCESS || status == Status.BT_FAILURE; }
        public bool isRunning() { return status == Status.BT_RUNNING; }
        public Status getStatus() { return status; }
    }
}
