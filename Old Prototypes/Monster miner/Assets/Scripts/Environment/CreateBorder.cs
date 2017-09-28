using UnityEngine;


public class CreateBorder : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D[] colliders = new BoxCollider2D[4];

    private Grid grid;
    private void Start()
    {
        grid = GameObject.Find("Pathfinding").GetComponent<Grid>();
        SetBounds();
    }
    private void SetBounds()
    {
        for(float i = 0; i < colliders.Length; i++)
        {
            if((i+1)%2 == 0)
            {
                colliders[(int)i].size = new Vector2(grid.gridWorldSize.x,grid.nodeRadius*4);
                if(i == 1)
                {
                    colliders[(int)i].transform.position = new Vector2(0,(grid.gridWorldSize.y/2) - colliders[(int)i].size.y/2);
                }
                else
                {
                    colliders[(int)i].transform.position = new Vector2(0, -(grid.gridWorldSize.y / 2) + colliders[(int)i].size.y / 2);
                }
            }
            else
            {
                colliders[(int)i].size = new Vector2(grid.nodeRadius*4, grid.gridWorldSize.y);

                if( i == 0)
                {
                    colliders[(int)i].transform.position = new Vector2((grid.gridWorldSize.x / 2) - colliders[(int)i].size.x / 2,0);
                }
                else
                {
                    colliders[(int)i].transform.position = new Vector2(-(grid.gridWorldSize.x / 2) + colliders[(int)i].size.x / 2, 0);
                }
            }
        }
    }

}
