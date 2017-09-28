using System.Collections;
using UnityEngine;


public class RequestPath : MonoBehaviour
{
    private Vector3[] path;
    private AgentInfo agent;

    private int targetIndex;

    private void Awake()
    {
        agent = GetComponent<AgentInfo>();
    }

    public void Request(Vector3 targetPos)
    {
        PathRequestManager.RequestPath(agent.transform.position, targetPos, OnPathFound);

    }
    public void Request(Vector3 targetPos,Vector2 buildingSize)
    {
        PathRequestManager.RequestPath(agent.transform.position, targetPos,buildingSize, OnPathFound);

    }

    //callback function
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else
        {
            agent.currentJob.beingWorked = false;
            agent.currentJob = null;

        }
    }
    public IEnumerator FollowPath()
    {
        targetIndex = 0;
        //set the current waypoint to the first waypoint in the path array
        Vector3 currentWaypoint = path[0];

        while(true)
        {
            ////get the direction to shoot the ray in 
            //Vector3 direction = currentWaypoint - transform.position;
            //Debug.DrawRay(transform.position, direction);
            ////do a raycast to see if we hit something
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude);
            ////if we do, start trying to get a new path
            //if (hit.collider != null)
            //{
            //    //get the target to hand off to the recalculation
            //    Vector3 targetPos = path[path.Length - 1];
            //    path = null;
            //    //this should be reset at the top of the function but it never hurts to be safe
            //    targetIndex = 0;
            //    //and request a new path around the object
            //    Request(targetPos);
            //    yield return null;
            //}

            //if we are at the current waypoint, increase the waypoint index
            if (Vector3.Distance(gameObject.transform.position, currentWaypoint) < 0.1f)
            {
                targetIndex++;
                //if we are at the end of the path, break out of the loop
                if(targetIndex >= path.Length)
                {
                    yield break;
                }
                //then alter the current waypoint to be the next one
                currentWaypoint = path[targetIndex];
            }
           
            //move the agent closer to the waypoint 
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, currentWaypoint, agent.Speed * Time.deltaTime);
            //wait for the next frame
            yield return null;
        }
    }

  
    private void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one / 5);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(agent.transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}