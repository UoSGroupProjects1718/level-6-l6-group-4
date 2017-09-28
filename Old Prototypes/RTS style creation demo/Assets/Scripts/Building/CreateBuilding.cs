using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuilding : MonoBehaviour {

    private Transform currentBuilding;
    private bool hasPlaced;
    private Grid grid;
    private PlaceableBuilding placeableBuilding = null;
    public LayerMask buildingMask;
    private int nodesToCheck = 5;//half width of the nodes to check when trying to find unwalkable


    // Use this for initialization
    void Start () {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
        //if the building isnt null and hasplaced is false
		if(currentBuilding != null && !hasPlaced)
        {
            //get the mouse po
            Vector3 m = Input.mousePosition;
            //get add in the camera's position
            m = new Vector3(m.x, m.y, Camera.main.transform.position.y);
            //modify it to world space
            Vector3 p = Camera.main.ScreenToWorldPoint(m);
            //then find a node based on this information
            Node node = grid.NodeFromWorldPoint(p);
            //then set the buildings central position to the node
            currentBuilding.position = node.worldPosition;
            //if mouse is down
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                //and the position is legal
                if(IsLegalPosition())
                {
                    //set hasplaced to true
                    hasPlaced = true;
                    List<Node> nodes = grid.FindUnwalkable(node, nodesToCheck);
                    //loop through all nodes within a certain radius of the grid
                    foreach(Node n in nodes)
                    {
                        if(Physics.Raycast(new Vector3(n.worldPosition.x,n.worldPosition.y - grid.nodeRadius,n.worldPosition.z),Vector3.up,Mathf.Infinity,buildingMask))
                        {
                            n.walkable = false;
                        }
                    }
                }
            }
        }
	}

    private bool IsLegalPosition()
    {
        if(placeableBuilding.colliders.Count > 0)
        {
            return false;
        }
        return true;
    }

    public void SetItem(GameObject b)
    {
        //set hasplaced to false
        hasPlaced = false;
        //set the current building
        currentBuilding = ((GameObject)Instantiate(b)).transform;
        //then set placeable building to the script on the current building
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
    }
   
}
