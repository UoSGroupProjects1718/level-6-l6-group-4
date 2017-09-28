using UnityEngine;

namespace BehaviourTree
{
    [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/LeafNodes/Labourer/Move To Job")]

    public class MoveToBuild : Leaf
    {

        bool hasRequested;

        public override void OnInstantiate(AgentInfo agent)
        {
            
            Agent = agent;
        }


        public override Status updateFunc()
        {
            //if the job is null that means a path couldnt be found, so we need to return failure
            if (Agent.currentJob == null)
                return Status.BT_FAILURE;
            //this leaf may cause problems, expect bug to do with not being able to reach an area
            #region getting building bounds
            Vector2 xBounds;
            Vector2 yBounds;

            Node centralNode = GlobalBlackboard.pathfindingGrid.NodeFromWorldPoint(Agent.currentJob.jobLocation);

            xBounds.x = Mathf.CeilToInt(centralNode.gridX - ((BuildJob)Agent.currentJob).BF.BuildingSize.x / 2 -1);
            xBounds.y = Mathf.CeilToInt(centralNode.gridX + ((BuildJob)Agent.currentJob).BF.BuildingSize.x / 2 + 1);

            yBounds.x = Mathf.CeilToInt(centralNode.gridY - ((BuildJob)Agent.currentJob).BF.BuildingSize.y / 2  - 1);
            yBounds.y = Mathf.CeilToInt(centralNode.gridY + ((BuildJob)Agent.currentJob).BF.BuildingSize.y / 2 + 1);

            #endregion





            Node playerNode = GlobalBlackboard.pathfindingGrid.NodeFromWorldPoint(Agent.transform.position);
           


            //if the distance is equal to or less than the diameter of a 
            if (playerNode.gridX >= xBounds.x)
                if(playerNode.gridX <= xBounds.y)
                    if(playerNode.gridY >= yBounds.x)
                        if(playerNode.gridY <= yBounds.y)
                        {
                            hasRequested = false;
                            return Status.BT_SUCCESS;
                        }
 
            if(!hasRequested)
            {
                if(Agent.currentJob.jobType == JobType.Build)
                {
                    Agent.pathRequest.Request(Agent.currentJob.jobLocation, ((BuildJob)Agent.currentJob).BF.BuildingSize);
                }
                else
                {
                    Agent.pathRequest.Request(Agent.currentJob.jobLocation);
                }
                hasRequested = true;
            }
           
            return Status.BT_RUNNING;
        }
    }
}
