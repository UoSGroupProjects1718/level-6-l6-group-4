using UnityEngine;
using UnityEngine.EventSystems;
public class CameraController : MonoBehaviour
{
    [Header("Interaction speed")]
    [SerializeField]
    private float panSpeed = 20f;
    [SerializeField]
    private float CamRotationSpeed = 5;
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
    [SerializeField]
    private AnimationCurve curve;



    private float cam_yPos = 0;
    [SerializeField]
    private float zoomDifferentialOver100;

    [SerializeField]
    [Range(0,0.5f)]
    private float zoomSpeed;

    private void Start()
    {
        zoomDifferentialOver100 = (camMaxZoom - camMinZoom) / 100;
        zoomSpeed *= 100;
    }

    void Update()
    {
        Vector3 CamPosition = transform.position;

        #region mouseScroll
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            //if the mouse scroll is greater than 0 then we will increase new_zPos otherwise we will reduce it
            if (mouseScroll > 0)
                //transform.Translate(new Vector3(0, -zoomDifferentialOver100 * zoomSpeed,0));
                CamPosition += new Vector3(0, -zoomDifferentialOver100 * zoomSpeed, 0);

            else if (mouseScroll < 0)
                //transform.Translate(new Vector3(0, zoomDifferentialOver100 * zoomSpeed, 0));
                CamPosition += new Vector3(0, zoomDifferentialOver100 * zoomSpeed, 0);


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
            transform.Rotate((-transform.up * CamRotationSpeed));

        if (Input.GetKey(Keybinds.Instance.RotateCameraRight))
            transform.Rotate((transform.up * CamRotationSpeed));

        #endregion


        CamPosition.x = Mathf.Clamp(CamPosition.x, (-CameraBounds.x)/2, (CameraBounds.x)/2);
        CamPosition.y = Mathf.Clamp(CamPosition.y, camMinZoom, camMaxZoom);
        CamPosition.z = Mathf.Clamp(CamPosition.z, (-CameraBounds.y)/2, (CameraBounds.y)/2);

        transform.position = CamPosition;



    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(CameraBounds.x, 0, CameraBounds.y));
    }
}
