using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerWithKey : MonoBehaviour
{
    public Transform CameraRotation;
    private float Key_X;
    private float Key_Y;
    public float Sensitivity;
    public float xRotation;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Key_X= Input.GetAxis("Horizontal") * Sensitivity * Time.deltaTime;
        Key_Y= Input.GetAxis("Vertical") * Sensitivity * Time.deltaTime;
        xRotation = xRotation - Key_Y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        CameraRotation.Rotate(Vector3.up * Key_X);
        this.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
