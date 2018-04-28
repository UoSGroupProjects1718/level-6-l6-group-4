using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huntingIconRotate : MonoBehaviour {

    [SerializeField]
    private float rotationAnglePerSecond;

    private void Update()
    {
        transform.Rotate(0, rotationAnglePerSecond * Time.deltaTime, 0);
    }
}
