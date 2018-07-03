using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainCamera : MonoBehaviour {
    public Camera maincamera;
    public Camera followcamera;

    void Start(){
        maincamera.enabled = true;
        followcamera.enabled = false;
    }
    // Update is called once per frame
    void Update(){
        if (Input.GetKey("space"))
        {
            maincamera.enabled = false;
            followcamera.enabled = true;
        }
        else {
            maincamera.enabled = true;
            followcamera.enabled = false;
        }
    }
}
