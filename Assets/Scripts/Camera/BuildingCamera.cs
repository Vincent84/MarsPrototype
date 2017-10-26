using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCamera : CameraBehaviour {

    void Start()
    {
        cameraType = 1;    
    }

    // Update is called once per frame
    void Update () {
		
	}

    public override void GetCameraBeahviour()
    {
        print("ciao");
    }

}
