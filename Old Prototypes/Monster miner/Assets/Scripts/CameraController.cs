
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float panSpeed = 20f;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight= KeyCode.D;
    [SerializeField]
    private float camMinZoom;
    [SerializeField]
    private float camMaxZoom;


    private Camera cam;
    private float verticalExtent;
    private float horizontalExtent;
    private int gridSizeX;
    private int gridSizeY;

    public float zoomAmount;
    private void Awake()
    {
        cam = Camera.main;
        gridSizeX = GameObject.Find("Pathfinding").GetComponent<Grid>().gridSizeX;
        gridSizeY = GameObject.Find("Pathfinding").GetComponent<Grid>().gridSizeY;
    }

    // Update is called once per frame
    void Update () {

        ///                            ///
        ///Camera movement///
        ///                           ///

        //create position variable
        Vector3 position = transform.position;

        //alter position variable
		if(Input.GetKey(moveUp))
        {
            position.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(moveDown))
        {
            position.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(moveLeft))
        {
            position.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(moveRight))
        {
            position.x += panSpeed * Time.deltaTime;
        }

        verticalExtent = cam.orthographicSize;
        horizontalExtent = verticalExtent * Screen.width / Screen.height;


        position.x = Mathf.Clamp(position.x, (-(gridSizeX / 2)) + horizontalExtent , (gridSizeX / 2) - horizontalExtent);
        position.y = Mathf.Clamp(position.y, (-(gridSizeY / 2)) + verticalExtent, (gridSizeY / 2) - verticalExtent);
        //apply position variable
        transform.position = position;


        ///           ///
        ///Camera Zoom///
        ///           ///
        zoomAmount = cam.orthographicSize;
        //zoom in
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            zoomAmount += 1;
        }
        //zoom out
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            zoomAmount -= 1;
        }

        zoomAmount = Mathf.Clamp(zoomAmount, camMinZoom, camMaxZoom);
        cam.orthographicSize = zoomAmount;

    }
}
