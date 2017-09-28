using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPitch : MonoBehaviour {


    public float vRotationSpeed = 5.0f;

    private float pitch = 0.0f;

    void FixedUpdate ()
    {
        pitch -= vRotationSpeed * Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -50, 100);
        transform.eulerAngles = new Vector3(pitch, transform.parent.eulerAngles.y, 0);
	}
}
