using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/LeafNodes/Labourer/Work On Job")]
    public class WorkOnJob : Leaf
    {


        public override void OnInstantiate(AgentInfo agent)
        {
            Agent = agent;
        }

        public override Status updateFunc()
        {
            //if the work amount is above 0, we need to reduce it by 1* the agents work speed
            if (Agent.currentJob.workAmount > 0)
            {
                Agent.currentJob.workAmount -= 1 * Agent.AgentworkSpeed * Time.deltaTime;
            }
            //or if it's work amount is equal to or less than 0, run the job complete function and return success
            if (Agent.currentJob.workAmount <= 0)
            {
                Agent.currentJob.OnJobComplete();
                //make a reference to the current job
                Job currentJob = Agent.currentJob;
                //then set it to null
                Agent.currentJob = null;
                //then we destroy the object from memory
                Destroy(currentJob);
                return Status.BT_SUCCESS;
            }
            //if we cant return success then we will be running
            return Status.BT_RUNNING;
        }

    }
}
