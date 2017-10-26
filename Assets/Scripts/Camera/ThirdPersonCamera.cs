using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : CameraBehaviour
{

     public Transform player_1;
     public Transform pivot;                                                                 // Pivot become the parent of the Player
     public float rotateSpeed;                                                               // How fast the Camera Rotate

     public float maxViewAngle;                                                              // Set the max distance view of the Camera
     public float minViewAngle;                                                              // Set the min distance view of the Camera

     public bool invertY;                                                                    // Option to invert Camera Y

    private Vector3 offset;
    private bool useOffsetValues;

    void Start()
     {

         cameraType = 0;

         // Use the bool on the Inspector if you want to use Vector3 values
         if (!useOffsetValues)
         {
             offset = player_1.position - Camera.main.transform.parent.position;
         }

         pivot.transform.position = player_1.transform.position;
         pivot.transform.parent = player_1.transform;

         // Hide the cursor on the Camera during Playmode
         Cursor.lockState = CursorLockMode.Locked;

     }

     public override void GetCameraBeahviour()
     {

         // Rotate the Camera on X axis
         float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
         player_1.Rotate(0, horizontal, 0);

         // Rotate the Camera on Y axis
         float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

         // Invert Y axis?
         if (invertY)
         {
             pivot.Rotate(-vertical, 0, 0);
         }
         else
         {
             pivot.Rotate(vertical, 0, 0);
         }

         // Limit up/down Camera rotation
         if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
         {
             pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
            Debug.Log(maxViewAngle);
         }

         if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
         {
             pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
            Debug.Log(minViewAngle);
         }

         float desiredYAngle = player_1.eulerAngles.y;
         float desiredXAngle = pivot.eulerAngles.x;
         Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
         Camera.main.transform.parent.position = player_1.position - (rotation * offset);

         //It makes the Camera don't overlapp the ground
         if (Camera.main.transform.parent.position.y < player_1.position.y)
         {
             Camera.main.transform.parent.position = new Vector3(Camera.main.transform.parent.position.x, player_1.position.y - 0.5f, Camera.main.transform.parent.position.z);
         }

         Camera.main.transform.parent.LookAt(player_1);

     }
}
