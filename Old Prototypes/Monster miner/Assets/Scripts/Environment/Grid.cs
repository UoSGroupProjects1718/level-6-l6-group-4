using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    [Header("Unwalkable Layer Mask")]
    public LayerMask WallLayerMask;

    [Header("Grid Attributes")]
    public Vector2 gridWorldSize;
    [Range(0,2)]
    public float nodeRadius;
    [HideInInspector]
    public Node[,] grid;

    private float nodeDiameter;
    [HideInInspector]
    public int gridSizeX;
    [HideInInspector]
    public int gridSizeY;
    [HideInInspector]
    public int maxSize;

    public bool DrawGridGizmos;


	// Use this for initialization
	void Awake () {
        nodeDiameter = nodeRadius * 2;
        //round the sizes to ints because we cant have half nodes etc
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        maxSize = gridSizeX * gridSizeY;
        CreateGrid();
	}
	
	void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        //get the bottom left of the world
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        //loop through and create the grid
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //get the position to create the node (it would be vector3.forward if we were using 3d space)
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                //set the node as walkable or not using overlap circle (in 3d this would be physics.checksphere)
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius,WallLayerMask));
                //then set its position in the multi-dimensional array, and while we do this create a node with this position and walkable state
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // we convert the world position into the percentage of how far along the grid it is
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        //then we need to clamp them to between 0 and 1
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //because arrays are zero indexed, we subtract 1 from the size of the grid
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        //then we return the grid coordinate that we need 
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        //set an empty list 
        List<Node> Neighbours = new List<Node>();

        //loop through the 9 surrounding nodes
        for( int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //if the x and y are both 0, it is the searching node so skip it
                if(x == 0 && y == 0)
                {
                    continue;
                }
                //then check the x and y to give their position in the grid
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //if checkX is greater than 0 and less than gridside, and likewise for y
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    //add it to the list
                    Neighbours.Add(grid[checkX, checkY]);
                }

            }
        }
        //then outside of the for loops, return the list of neighbours
        return Neighbours;
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if(grid != null && DrawGridGizmos)
        {
            foreach (Node node in grid)
            {
                //is the node walkable? yes white, otherwise make it red
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                
                //then draw the cube at the position
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - 0.01f));
            }
        }
    }
}
