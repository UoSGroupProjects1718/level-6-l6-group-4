using UnityEngine;
namespace BehaviourTree
{

    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/LeafNodes/Labourer/Find Job")]
    public class FindJob : Leaf
    {


        public override void OnInstantiate(AgentInfo agent)
        {
            Agent = agent;
        }

        public override Status updateFunc()
        {
            //if the agents current job isnt null then we can return success as there is no need to run this leaf node
            //or there are no jobs to be done, just say that we are running so that it can again            
            if (Agent.currentJob != null)
                return Status.BT_SUCCESS;
            //if (GlobalBlackboard.JobDocket.Count == 0)
            //    Debug.Log("Returning Fail(0)");
            //    return Status.BT_RUNNING;

            //if we havent returned failure, we need to look through the job docket for a job that corresponds to the current situation
            for (int i = 0; i < GlobalBlackboard.JobDocket.Count; i++)
            {
                //if the job isnt being worked and is the same type as the agent
                if (GlobalBlackboard.JobDocket[i].beingWorked == false && GlobalBlackboard.JobDocket[i].jobType == Agent.WorkerType)
                {
                    //set the job
                    Agent.currentJob = GlobalBlackboard.JobDocket[i];
                    Agent.currentJob.beingWorked = true;
                    //and return success
                    return Status.BT_SUCCESS;
                }
            }
            return Status.BT_FAILURE;
        }

    }
}
