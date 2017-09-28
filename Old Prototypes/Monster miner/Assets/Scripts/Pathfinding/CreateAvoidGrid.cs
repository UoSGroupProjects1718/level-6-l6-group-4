using UnityEngine;

public class CreateAvoidGrid : MonoBehaviour {
    private static Grid grid;

    [SerializeField]
    private LayerMask unwalkableLayerMask;

#region singleton
    private static CreateAvoidGrid instance;

    public static CreateAvoidGrid GetInstance()
    {
        if (instance == null)
            instance = FindObjectOfType<CreateAvoidGrid>();
        return instance;
    }
    #endregion

    private void Awake()
    {
        grid = GameObject.Find("Pathfinding").GetComponent<Grid>();
    }

    //change all nodes underneath the building to being unwalkable
    public void CheckNodes(Vector3 startPosition)
    {
        //find the node 
        Node centreNode = grid.NodeFromWorldPoint(startPosition);
        //then set it to non walkable
        centreNode.walkable = false;
        //then check all of it's neighbours to see if they are walkable now
        CheckNeighbours(centreNode);
    }
    private void CheckNeighbours(Node startNode)
    {
        //check through each neighbour of the current node
        foreach (Node neighbour in grid.GetNeighbours(startNode))
        {
            //if the neighbour is walkable
            if (neighbour.walkable)
            {
                //then cast a circle and check if there is something on top of it which makes it unwalkable
                if (Physics2D.OverlapCircle(neighbour.worldPosition, grid.nodeRadius, unwalkableLayerMask))
                {
                    //if there is, mark it unwalkable
                    neighbour.walkable = false;

                    //then check its neighbours
                    CheckNeighbours(neighbour);
                }
            }
                //if the node was not walkable, then we will not check any of this and instantly return out of the function
                //this ensures that only nodes which have a building on top of them will check their neighbours for being unwalkable 
        }
    }
}
