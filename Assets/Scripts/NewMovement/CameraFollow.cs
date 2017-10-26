using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform CameraFollowObj;

    public float CameraMoveSpeed = 120.0f;
    public float minClampAngle = 10.0f;
    public float maxClampAngle = 50.0f;
    public float inputSensitivity = 150.0f;                 // Mouse Speed

    private float rotX = 0.0f;
    private float rotY = 0.0f;


    void Start()
    {

        Vector3 rot = transform.localRotation.eulerAngles;

        rotX = rot.x;
        rotY = rot.y;

        // Cursor

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {

        rotY += Input.GetAxis("Mouse X") * inputSensitivity * Time.deltaTime;
        rotX += Input.GetAxis("Mouse Y") * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -minClampAngle, maxClampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

    }

    private void LateUpdate()
    {
        // Set the target object to follow

        Transform target = CameraFollowObj.transform;

        // Move towards the game object that is the target

        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Rotate the Camera on X axis
        float horizontal = Input.GetAxis("Mouse X") * inputSensitivity * Time.deltaTime;
        CameraFollowObj.Rotate(0, horizontal, 0);

    }
}
