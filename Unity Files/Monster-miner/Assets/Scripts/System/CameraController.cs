using UnityEngine;
using UnityEngine.EventSystems;
public class CameraController : MonoBehaviour
{
    [Header("Interaction speed")]
    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float CamRotationSpeed = 5;
    [SerializeField]
    [Range(0,0.5f)]
    private float zoomSpeed;
    [Header("Minimum and maximum values")]
    [SerializeField]
    private float camMinZoom;
    [SerializeField]
    private float camMaxZoom;
    [SerializeField]
    private Vector2 CameraBounds;

    [Space]
    [SerializeField]
    private float CameraScreenBorder = 10;

    [Space]
    [SerializeField]
    private LayerMask floorLayerMask;


    private float zoomDifferentialOver100;



    private float expectedYPosition;

    private Vector2 middleClickStart;

    [SerializeField]
    private float mousePanBoxSize = 10f; // size of the box in PIXELS

    private void Start()
    {
        zoomDifferentialOver100 = (camMaxZoom - camMinZoom) / 100;
        zoomSpeed *= 100;
        expectedYPosition = transform.position.y;
    }
    float distFromFloor;
    void Update()
    {
        Vector3 CamPosition = transform.position;
        Vector3 CamRotation = transform.localEulerAngles;


        #region mouseScroll
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            //if the mouse scroll is greater than 0 then we will increase new_zPos otherwise we will reduce it
            if (mouseScroll > 0)
            {
                //transform.Translate(new Vector3(0, -zoomDifferentialOver100 * zoomSpeed,0));
                //CamPosition += new Vector3(0, -zoomDifferentialOver100 * zoomSpeed, 0);
                expectedYPosition += -zoomDifferentialOver100 * zoomSpeed;
            }

            else if (mouseScroll < 0)
            {
                //transform.Translate(new Vector3(0, zoomDifferentialOver100 * zoomSpeed, 0));
                //CamPosition += new Vector3(0, zoomDifferentialOver100 * zoomSpeed, 0);
                expectedYPosition += zoomDifferentialOver100 * zoomSpeed;
            }


        }
        #endregion



        #region camera movement
        //translation
        if (Input.GetKey(Keybinds.Instance.MoveCameraUp) )//|| Input.mousePosition.y >= Screen.height - CameraScreenBorder)
          //transform.Translate((transform.forward * panSpeed) * Time.deltaTime, Space.World);
          CamPosition += (transform.forward * panSpeed) * Time.deltaTime;

        if (Input.GetKey(Keybinds.Instance.MoveCameraDown))// || Input.mousePosition.y <= CameraScreenBorder)
           //transform.Translate((-transform.forward *  panSpeed)* Time.deltaTime, Space.World);
           CamPosition += (-transform.forward * panSpeed)*Time.deltaTime;
        if (Input.GetKey(Keybinds.Instance.MoveCameraLeft))// || Input.mousePosition.x <= CameraScreenBorder)
            //transform.Translate((-transform.right * panSpeed)* Time.deltaTime, Space.World);
            CamPosition += (-transform.right * panSpeed) * Time.deltaTime;

        if (Input.GetKey(Keybinds.Instance.MoveCameraRight))// || Input.mousePosition.x >= Screen.width - CameraScreenBorder)
            //transform.Translate((transform.right * panSpeed) * Time.deltaTime,Space.World );
            CamPosition += (transform.right * panSpeed) * Time.deltaTime;

        //rotation
        if (Input.GetKey(Keybinds.Instance.RotateCameraLeft))
           CamRotation += -Vector3.up * CamRotationSpeed;

        if (Input.GetKey(Keybinds.Instance.RotateCameraRight))
            CamRotation +=  Vector3.up * CamRotationSpeed;


        if(Input.GetKeyDown(KeyCode.Mouse2))
        {
            middleClickStart = Input.mousePosition;
        }
        //then get the mouse position while the mmb is still pressed
        if(Input.GetKey(KeyCode.Mouse2))
        {
            //look up
            if(Input.mousePosition.x > middleClickStart.x + mousePanBoxSize)
            {
               CamRotation += Vector3.up * CamRotationSpeed;
            }
            //look down
            if(Input.mousePosition.x < middleClickStart.x - mousePanBoxSize)
            {
               CamRotation += -Vector3.up * CamRotationSpeed;
            }
            //look right
            if(Input.mousePosition.y > middleClickStart.y + mousePanBoxSize)
            {
                transform.GetChild(0).localEulerAngles -= Vector3.right * (CamRotationSpeed /2);
            }
            //look left
            if(Input.mousePosition.y < middleClickStart.y - mousePanBoxSize)
            {
                transform.GetChild(0).localEulerAngles -= -Vector3.right * (CamRotationSpeed/2);
            }
           

        }
        
        #endregion


        CamPosition.x = Mathf.Clamp(CamPosition.x, (-CameraBounds.x)/2, (CameraBounds.x)/2);
        CamPosition.z = Mathf.Clamp(CamPosition.z, (-CameraBounds.y)/2, (CameraBounds.y)/2);

        //we need to make sure that the camera's position is at a static distance from the floor rather than a world space position
        RaycastHit rayHit = new RaycastHit();
        if(Physics.Raycast(CamPosition,Vector3.down, out rayHit, float.MaxValue,floorLayerMask))
        {
            distFromFloor = Vector3.Distance(CamPosition, rayHit.point);
        }

        float expectedDistFromFloor = CamPosition.y - distFromFloor;
        CamPosition.y = expectedYPosition + expectedDistFromFloor;
        CamPosition.y = Mathf.Clamp(CamPosition.y, camMinZoom + expectedDistFromFloor, camMaxZoom + expectedDistFromFloor);



        transform.position = CamPosition;

        transform.GetChild(0).localEulerAngles = new Vector3(Mathf.Clamp(transform.GetChild(0).localEulerAngles.x, 20, 80),transform.GetChild(0).localEulerAngles.y, transform.GetChild(0).localEulerAngles.z);

        transform.localEulerAngles = CamRotation;


    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(CameraBounds.x, 0, CameraBounds.y));
    }
}
