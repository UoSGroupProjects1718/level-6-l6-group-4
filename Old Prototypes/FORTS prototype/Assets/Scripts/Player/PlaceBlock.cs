using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlock : MonoBehaviour {

    public GameObject cubePrefab;

    private Grid grid;

    public LayerMask cubeMask;
    public float MaxRayDistance = 7;

    public GameObject blockPoolParent;

    public Queue<GameObject> CubePool = new Queue<GameObject>();

    private void Start()
    {
        grid = GameObject.Find("pathfinding").GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update () {
        //if LMB is pressed
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //cast a ray
            RaycastHit rayHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if the ray hits something
            if (Physics.Raycast(ray, out rayHit,MaxRayDistance))
            {
                //if we hit a cube
                if(rayHit.collider.tag == "Cube")
                {

                    Block cube = rayHit.collider.GetComponent<Block>();



                    //check to see if there is anything in the pool
                    if(CubePool.Count == 0)
                    {
                        //if there isnt we need to instantiate a new one
                        GameObject block = Instantiate(cubePrefab, rayHit.transform.position + GetCubeSpawnPos(CalculateCubeSide(rayHit)), Quaternion.identity) as GameObject;
                        if (grid.NodeFromWorldPoint(block.transform.position).buildable)
                        {
                            //set the parent node
                            block.GetComponent<Block>().parentNode = cube.parentNode;
                            //get the node the cube is above
                            Node node = grid.NodeFromWorldPoint(block.transform.position);
                            //send out a raycast, if it hits a block, set it to unwalkable
                            if (Physics.Raycast(new Vector3(node.worldPosition.x, node.worldPosition.y - grid.nodeRadius, node.worldPosition.z), Vector3.up, (cubePrefab.transform.localScale.y * 2) - 0.1f, cubeMask))
                            {
                                print("Raycast hit a block");
                                node.walkable = false;
                            }
                        }
                        else
                        {
                            block.SetActive(false);
                            CubePool.Enqueue(block);
                        }
                    }
                    else
                    {
                        //take it off of the queue
                        GameObject block = CubePool.Dequeue();
                        block.transform.parent = null;
                        //set its position to the node
                        block.transform.position = rayHit.transform.position + GetCubeSpawnPos(CalculateCubeSide(rayHit));
                        block.GetComponent<Block>().parentNode = cube.parentNode;
                        //make it active
                        block.SetActive(true);
                        //get the node that the cube is above
                        Node node = grid.NodeFromWorldPoint(block.transform.position);

                        //send out a raycast, if it hits a block, set it to unwalkable
                        if (Physics.Raycast(new Vector3(node.worldPosition.x, node.worldPosition.y - grid.nodeRadius, node.worldPosition.z), Vector3.up, (cubePrefab.transform.localScale.y * 2) - 0.1f, cubeMask))
                        {
                            print("Raycast hit a block");
                            node.walkable = false;
                        }
                    }


                }
                else
                {
                    Node node = grid.NodeFromWorldPoint(rayHit.point);
                    if (node != null && node.buildable)
                    {
                        //if the pool is empty
                        if (CubePool.Count == 0)
                        {
                            //instantiate a new cube
                            GameObject cube = Instantiate(cubePrefab, new Vector3(node.worldPosition.x, node.worldPosition.y + (cubePrefab.transform.localScale.y / 2), node.worldPosition.z), Quaternion.identity) as GameObject;
                            cube.GetComponent<Block>().parentNode = node;
                            node.walkable = false;

                        }
                        //otherwise if the pool is greater than 0
                        else
                        {
                            //take it off of the queue
                            GameObject cube = CubePool.Dequeue();
                            cube.transform.parent = null;
                            //set its position to the node
                            cube.transform.position = new Vector3(node.worldPosition.x, node.worldPosition.y + (cubePrefab.transform.localScale.y / 2), node.worldPosition.z);
                            //make it active
                            cube.SetActive(true);
                            //make the node non walkable
                            node.walkable = false;
                        }
                    }
                }
            }
        }
        
        //if RMB  is pressed
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //cast a ray
            RaycastHit rayHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          
            //if the ray hits something
            if (Physics.Raycast(ray, out rayHit))
            {
                //and its colliders tag is cube
                if (rayHit.collider.tag == "Cube")
                {
                    GameObject cube = rayHit.collider.gameObject;
                    Node node = grid.NodeFromWorldPoint(rayHit.transform.position);

                    //then set active to false
                    cube.gameObject.SetActive(false);
                    //set its transform to the parent
                    cube.transform.parent = blockPoolParent.transform;
                    //then enqueue it
                    CubePool.Enqueue(cube);
                     //we then fire a node up from the grid position node, if it doesnt hit something, we make the node walkable
                    if(!Physics.Raycast(new Vector3(node.worldPosition.x,node.worldPosition.y - grid.nodeRadius,node.worldPosition.z),Vector3.up,(cubePrefab.transform.localScale.y * 2)- 0.1f,cubeMask))
                    {
                        print("Node hit nothing");
                        node.walkable = true;
                    }
                    else
                    {
                        print("node hit something");
                    }
                }
            }
        }
    
    }

    private int CalculateCubeSide(RaycastHit hit)
    {
        //we get the dot product, equals 1 for the top of the cube and -1 for the bottom
        float dotProduct = Vector3.Dot(hit.normal,hit.transform.up);

        if(dotProduct == 0)
        {
            float angle = Vector3.Angle(hit.transform.right, hit.normal);
            //if the normal is the backward normal, just set the angle to 270 because the previous calculation returns 90 for both forward and backward normals
            if (hit.normal == -hit.transform.forward)
            {
                angle = 270;
            }
            return Mathf.RoundToInt(angle);
        }
        return Mathf.RoundToInt(dotProduct);
    }
    private Vector3 GetCubeSpawnPos(int Side)
    {
        Vector3 spawnPos = Vector3.zero;
        switch(Side)
        {
            case 1:
                //up
                spawnPos = (Vector3.up * (cubePrefab.transform.localScale.y));
                break;
            case -1:
                //down
                spawnPos = (-Vector3.up * (cubePrefab.transform.localScale.y));
                break;
            case 0:
                //front
                spawnPos = (Vector3.right * (cubePrefab.transform.localScale.x));
                break;
            case 90:
                //right
                spawnPos = (Vector3.forward * (cubePrefab.transform.localScale.z));
                break;
            case 180:
                //back
                spawnPos = (-Vector3.right * (cubePrefab.transform.localScale.x));
                break;
            case 270:
                //left
                spawnPos = (-Vector3.forward * (cubePrefab.transform.localScale.z));
                break;
        }
        return spawnPos;
    }

}
