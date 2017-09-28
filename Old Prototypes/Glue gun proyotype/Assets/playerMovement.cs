using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public KeyCode Forward;
    public KeyCode Backward;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Jump;
    public KeyCode Action;
    public KeyCode Glue;
    public float speed;
    public float xRotate;
    public float yRotate;
    public Vector3 Dir;
    public bool haveObject;
    public GameObject grabbedGameObject;
    public List<GameObject> gluedObjects;
    public GameObject EMPTYPREFAB;
    public Rigidbody rb;

    // Use this for initialization
    CursorLockMode wantedMode;

    Vector2 MousePos;

    private void Start()
    {
        MousePos = Input.mousePosition;
        rb = GetComponent<Rigidbody>();
    }

    void OnGUI(){
        wantedMode = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Locked;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(Forward))
        {
            transform.position = new Vector3(
                                            transform.position.x + (speed * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)), 
                                            transform.position.y, 
                                            transform.position.z + speed * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        }

        if (Input.GetKey(Backward))
        {
            transform.position = new Vector3(
                                            transform.position.x -(speed * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)), 
                                            transform.position.y,
                                            transform.position.z - speed * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        }

        if (Input.GetKey(Left))
        {
            transform.position = new Vector3(
                                            transform.position.x - speed * Mathf.Cos(transform.rotation.eulerAngles.y *Mathf.Deg2Rad), 
                                            transform.position.y, 
                                            transform.position.z + speed * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        }

        if (Input.GetKey(Right))
        {
            transform.position = new Vector3(
                                            transform.position.x +(speed * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)), 
                                            transform.position.y, 
                                            transform.position.z - speed * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad));
        }

        if (Input.GetKey(Jump) && rb.velocity.y==0)
        {
            rb.AddForce(0, 300, 0);
        }
            if (Input.GetKeyDown(Action))
        {
            if (grabbedGameObject == null)
            {
                RaycastHit hit;
                Dir = new Vector3(
                                            Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad),
                                            -Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.Deg2Rad),
                                            Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)) * 100 + transform.position;
                if (Physics.Raycast(transform.position, Dir, out hit))
                {
                    try
                    {
                        if (hit.collider.gameObject.GetComponent<objectGrabControl>().canMove == true)
                        {
                            grabbedGameObject = hit.collider.gameObject;
                            while (grabbedGameObject.transform.parent != null)
                            {
                                grabbedGameObject = grabbedGameObject.transform.parent.gameObject;
                            }
                            if (grabbedGameObject.GetComponent<objectGrabControl>().hasGravity == true)
                            {
                                grabbedGameObject.GetComponent<Rigidbody>().useGravity = false;
                            }
                        }
                    }
                    catch { Debug.Log("ERROR"); }
                }
            }

            else
            {
                if (grabbedGameObject.GetComponent<objectGrabControl>().hasGravity)
                {
                    grabbedGameObject.GetComponent<Rigidbody>().useGravity = true;
                }
                grabbedGameObject = null;
            }
        }

        if (Input.GetKeyDown(Glue))
        {
            RaycastHit hit;
            Dir = new Vector3(
                                        Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad),
                                        -Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.Deg2Rad),
                                        Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)) * 100 + transform.position;

            if (Physics.Raycast(transform.position, Dir, out hit))
            {
                try
                {
                    if (hit.collider.gameObject.GetComponent<objectGrabControl>())
                    {
                        GameObject selectedGameObject = hit.collider.gameObject;
                        while (selectedGameObject.transform.parent != null)
                        {
                            selectedGameObject = selectedGameObject.transform.parent.gameObject;
                        }


                        if (gluedObjects.Count > 0)
                        {
                            if(selectedGameObject != gluedObjects[0] )
                            {
                                if(Normalize(selectedGameObject.transform.position - gluedObjects[0].transform.position) < 2)
                                gluedObjects.Add(selectedGameObject);

                                
                                
                                if(gluedObjects[0].GetComponent<objectGrabControl>().hasGravity && gluedObjects[1].GetComponent<objectGrabControl>().hasGravity)
                                {
                                    GameObject empty = Instantiate(EMPTYPREFAB, MeanPosition(gluedObjects[0].transform.position, gluedObjects[1].transform.position),new Quaternion(0,0,0,0));
                                    gluedObjects[0].transform.SetParent(empty.transform);
                                    gluedObjects[1].transform.SetParent(empty.transform);
                                    empty.GetComponent<Rigidbody>().useGravity = true;
                                    empty.GetComponent<objectGrabControl>().hasGravity = true;
                                    HingeJoint H1 = empty.AddComponent<HingeJoint>();
                                    HingeJoint H2 = empty.AddComponent<HingeJoint>();
                                    H1.connectedBody = gluedObjects[0].GetComponent<Rigidbody>();
                                    H2.connectedBody = gluedObjects[1].GetComponent<Rigidbody>();
                                }
                                else {
                                    GameObject empty = Instantiate(EMPTYPREFAB, MeanPosition(gluedObjects[0].transform.position, gluedObjects[1].transform.position), new Quaternion(0, 0, 0, 0));
                                    gluedObjects[0].transform.SetParent(empty.transform);
                                    gluedObjects[1].transform.SetParent(empty.transform);
                                    empty.GetComponent<Rigidbody>().useGravity = false;
                                    HingeJoint H1 = empty.AddComponent<HingeJoint>();
                                    HingeJoint H2 = empty.AddComponent<HingeJoint>();
                                    H1.connectedBody = gluedObjects[0].GetComponent<Rigidbody>();
                                    H2.connectedBody = gluedObjects[1].GetComponent<Rigidbody>();
                                }

                                for (int i = 0; i < 2; i++)
                                {
                                    try
                                    {
                                        gluedObjects[i].GetComponent<Rigidbody>().useGravity = false;
                                    }
                                    catch { Debug.Log("Error"); }
                                }

                                gluedObjects.Clear();
                            }
                        }

                        else
                        {
                            gluedObjects.Add(selectedGameObject);
                        }
                    }
                }
                catch { Debug.Log("Error"); }
            }
        }

        Vector3 Rotation = new Vector3(
            transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * yRotate,
            transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * xRotate,
            0
            );
  
        transform.eulerAngles = Rotation;

        Dir = new Vector3(
                                        Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad),
                                        -Mathf.Sin(transform.rotation.eulerAngles.x * Mathf.Deg2Rad),
                                        Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad)) *3+ transform.position;
        Debug.DrawRay(transform.position, Dir, Color.green);

        if(grabbedGameObject!=null)
            grabbedGameObject.transform.position = Dir;
     }

    float Normalize(Vector3 input)
    {
        float squares = Mathf.Pow(input.x, 2) + Mathf.Pow(input.y, 2) + Mathf.Pow(input.z, 2);
        return Mathf.Sqrt(squares);
    }


    Vector3 MeanPosition(Vector3 T1, Vector3 T2) {
        Vector3 returnvalue = new Vector3(0, 0, 0);

        returnvalue.x = (T1.x + T2.x) / 2;
        returnvalue.y = (T1.y + T2.y) / 2;
        returnvalue.z = (T1.z + T2.z) / 2;
        return returnvalue;
    }
}
