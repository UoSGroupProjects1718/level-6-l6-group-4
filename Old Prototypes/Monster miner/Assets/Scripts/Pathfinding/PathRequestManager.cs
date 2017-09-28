using System.Collections.Generic;
using System;
using UnityEngine;

public class PathRequestManager : MonoBehaviour {

    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currentPathRequest;

    static PathRequestManager instance;

    Pathfinding pathfinding;

    bool isProcessingPath;

    private void Awake()
    {
        //set instance for singleton
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }
    /// <summary>
    ///pathing to an object in the world which does not have a size of multiple nodes/ requires an interaction spot
    /// </summary>
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        //create new path
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        //enqueue it
        instance.pathRequestQueue.Enqueue(newRequest);
        //then see if we can process a new path
        instance.TryProcessNext();
    }
    /// <summary>
    ///variant for pathing to an object which has a size of multiple nodes, will find a path to the edge of the world object
    /// </summary>
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd,Vector2 buildingSize, Action<Vector3[], bool> callback)
    {
        //create new path
        PathRequest newRequest = new PathRequest(pathStart, pathEnd,buildingSize, callback);
        //enqueue it
        instance.pathRequestQueue.Enqueue(newRequest);
        //then see if we can process a new path
        instance.TryProcessNext();
    }


    private void TryProcessNext()
    {
        //if we arent processing a path and the queue has something inside of it
        if(!isProcessingPath && pathRequestQueue.Count > 0 )
        {
            //then we take the front object off of the queue and make that the current request
            currentPathRequest = pathRequestQueue.Dequeue();
            //then we say that we are now processing a path
            isProcessingPath = true;
            //then we tell the pathfinding script to start finding us a path
            if (currentPathRequest.buildingSize.x > 0 && currentPathRequest.buildingSize.y > 0)
            {
                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd, currentPathRequest.buildingSize);
                return;
            }
             
                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }
    //then when the path has been finished processing, the pathfinding script will tell us this
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        //we then start the callback of the current request
        currentPathRequest.callback(path, success);
        //tell the system that we are no longer processing
        isProcessingPath = false;
        //and process the next item in the queue
        TryProcessNext();
    }


    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Vector2 buildingSize;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
            buildingSize = new Vector2(0, 0);
        }
        public PathRequest(Vector3 _start, Vector3 _end, Vector2 _buildingSize,Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
            buildingSize = _buildingSize;
        }

    }
}
