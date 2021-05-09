using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CameraRotation;
    private float Mouse_X;
    private float Mouse_Y;
    public float MouseSensitivity;
    public float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_X = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        Mouse_Y = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation = xRotation - Mouse_Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        CameraRotation.Rotate(Vector3.up * Mouse_X);
        this.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
