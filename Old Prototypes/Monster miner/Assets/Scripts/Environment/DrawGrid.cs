using System.Collections;
using UnityEngine;

//reference http://catlikecoding.com/unity/tutorials/procedural-grid/

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class DrawGrid : MonoBehaviour {

    private Grid grid;
    private Vector3[] vertices;
    private Mesh mesh;
    [HideInInspector]
    public int xSize, ySize;


    private void Start()
    {
        grid = GameObject.Find("Pathfinding").GetComponent<Grid>();
        xSize = grid.gridSizeX;
        ySize = grid.gridSizeY;

        //generate the grid
        GenerateGrid();

    }

    private void GenerateGrid()
    {

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Grid";
        //initialise the vert array
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        //then we need to create the uv coordinates for the mesh
        Vector2[] uv = new Vector2[vertices.Length];
        for(int i = 0, y = 0; y <= ySize; y++)
        {
            for(int x = 0; x <= xSize; x++, i++)
            {
                //when we create it, we must make sure to change the position of the vertex to reduce its position by half of the x and y size, to get the origin (bottom left corner) to appear in the bottom left
                vertices[i] = new Vector3(x - ((xSize/2) + grid.nodeRadius), y - ((ySize/2 + grid.nodeRadius)));
                uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
            }
        }
        //now we need to set up the triangles for the mesh to render.
        mesh.vertices = vertices;
        //and we need to give it the UV coordinates
        mesh.uv = uv;
        int[] triangles = new int[xSize * ySize *  6];
        for(int ti = 0, vi = 0, y = 0; y < xSize; y++, vi++)
        {
            for(int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        //then set the triangles for the mesh
        mesh.triangles = triangles;
    }
}
