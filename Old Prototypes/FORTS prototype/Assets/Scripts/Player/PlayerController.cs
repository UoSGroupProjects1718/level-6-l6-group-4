using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player {

    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode jumpKey = KeyCode.Space;

    public CursorLockMode desiredCursorMode;

    private Rigidbody player;

    public float hCamRotSpeed = 5;

    private float yaw = 0.0f;

    // Use this for initialization
    void Start ()
    {
        player = GetComponent<Rigidbody>();
    }

    private void OnGUI()
    {
        desiredCursorMode = CursorLockMode.Locked;
        Cursor.lockState = desiredCursorMode;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        #region movement
        Vector3 desiredVelocity = Vector3.zero;

        //move left
        if(Input.GetKey(moveLeft))
        {
            desiredVelocity.x = -1 * movementSpeed * Time.deltaTime;
        }
        //move right
        if (Input.GetKey(moveRight))
        {
            desiredVelocity.x = 1 * movementSpeed * Time.deltaTime;
        }
        //move up
        if (Input.GetKey(moveUp))
        {
            desiredVelocity.z = 1 * movementSpeed * Time.deltaTime;
        }
        //move down
        if (Input.GetKey(moveDown))
        {
            desiredVelocity.z = -1 * movementSpeed * Time.deltaTime;
        }
        //jump
        if(Input.GetKeyDown(jumpKey) && Physics.Raycast(player.transform.position ,Vector3.down, 1))
        {
            player.velocity  +=  Vector3.up * (movementSpeed);
        }

        transform.Translate(desiredVelocity);
        #endregion movement


        #region rotation
        yaw += hCamRotSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0, yaw, 0.0f);
        #endregion rotation
    }
}
