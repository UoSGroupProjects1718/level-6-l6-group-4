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



    private float cam_zPos = 0;

    void Update()
    {
        Vector3 CamPosition = transform.position;

        #region mouseScroll
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            //if the mouse scroll is greater than 0 then we will increase new_zPos otherwise we will reduce it
            if (mouseScroll > 0)
                cam_zPos += 0.1f;

            else if (mouseScroll < 0)
                cam_zPos -= 0.1f;

            //clamp the camera's new z position
            cam_zPos = Mathf.Clamp(cam_zPos, 0, 1);

            //then set it, based on the information from our animation curve
            CamPosition.y = (curve.Evaluate(cam_zPos) * camMaxZoom);
            CamPosition.y = Mathf.Clamp(CamPosition.y, camMinZoom, camMaxZoom);
        }
        #endregion
        #region camera movement
        //translation
        if (Input.GetKey(Keybinds.Instance.MoveCameraUp) )//|| Input.mousePosition.y >= Screen.height - CameraScreenBorder)
            CamPosition += (transform.up * panSpeed) * Time.deltaTime;

        if (Input.GetKey(Keybinds.Instance.MoveCameraDown))// || Input.mousePosition.y <= CameraScreenBorder)
            CamPosition -= (transform.up *  panSpeed)* Time.deltaTime;

        if (Input.GetKey(Keybinds.Instance.MoveCameraLeft))// || Input.mousePosition.x <= CameraScreenBorder)
            CamPosition -= (transform.right * panSpeed)* Time.deltaTime;

        if (Input.GetKey(Keybinds.Instance.MoveCameraRight))// || Input.mousePosition.x >= Screen.width - CameraScreenBorder)
            CamPosition += (transform.right * panSpeed) * Time.deltaTime;


        //rotation
        if (Input.GetKey(Keybinds.Instance.RotateCameraLeft))
            transform.Rotate((Vector3.down * CamRotationSpeed), Space.World);

        if (Input.GetKey(Keybinds.Instance.RotateCameraRight))
            transform.Rotate((Vector3.up * CamRotationSpeed) , Space.World);

        #endregion


        CamPosition.x = Mathf.Clamp(CamPosition.x, -CameraBounds.x, CameraBounds.x);
        CamPosition.z = Mathf.Clamp(CamPosition.z, -CameraBounds.y, CameraBounds.y);


        transform.position = CamPosition;



    }
}
