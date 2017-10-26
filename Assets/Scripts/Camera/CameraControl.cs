using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public CameraBehaviour cameraBehaviour;
    public CameraBehaviour[] cameraBehaviours;
    public float transitionSpeed;

    private int activeCamera;
    private bool isMoving;

    private void Awake()
    {
        activeCamera = cameraBehaviour.cameraType;
        isMoving = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !isMoving)
        {
            StartCoroutine(ChangeCamera());
        }

        cameraBehaviour.GetCameraBeahviour();
    }

    /*void LateUpdate()
    {
        cameraBehaviour.GetCameraBeahviour();   
    }*/

    public IEnumerator ChangeCamera()
    {

        switch(activeCamera)
        {
            case 0:

                GameManager.currentState = GameManager.GameState.BUILDING;

                cameraBehaviour.transform.position = transform.position;
                cameraBehaviour.transform.rotation = transform.rotation;

               // Camera.main.GetComponent<CameraCollision>().enabled = false;

                activeCamera++;
                cameraBehaviour = cameraBehaviours[activeCamera];

                yield return StartCoroutine(MoveCameraSmooth(cameraBehaviour.transform.position, cameraBehaviour.transform.rotation));

                isMoving = false;

                break;

            case 1:
                
                activeCamera = 0;

                yield return StartCoroutine(MoveCameraSmooth(cameraBehaviours[0].transform.position, cameraBehaviours[0].transform.rotation));

                cameraBehaviour = cameraBehaviours[activeCamera];

                GameManager.currentState = GameManager.GameState.EXPLORATION;

                isMoving = false;

                break;
        }

    }


    public IEnumerator MoveCameraSmooth(Vector3 toPosition, Quaternion toRotation)
    {

        isMoving = true;

        for (float t = 0f; t <= 1; t += Time.deltaTime / transitionSpeed)
        {
            transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, toRotation, t);
            transform.position = Vector3.Lerp(transform.position, toPosition, t);
            yield return null;
        }

    }
}
