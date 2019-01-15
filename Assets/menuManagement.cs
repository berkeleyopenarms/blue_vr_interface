﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


/*
 This script will be used to manage the menu system globally.
 */

public class menuManagement : MonoBehaviour {

    public GameObject vrCamera;
    public GameObject clutch;
    public GameObject menu;
    public GameObject blueUrdf;

    public blueInputSystem leftHandInputs;
    public blueInputSystem rightHandInputs;

    private Vector3 offset;
    private int frameStore;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0.3f, 0.7f, 0.0f);
        frameStore = Time.frameCount;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(leftHandInputs.getGrip());
        //Debug.Log(rightHandInputs.getGrip());

        if (leftHandInputs.getGrip() && (Time.frameCount - frameStore) > 30)
        {
            menu.active = !menu.active;
            frameStore = Time.frameCount;
        }
  
    }

    // Set the global transform of the clutch to be the transform of the player plus a fixed transform
    public void centerClutchUnderPlayer()
    {
        Debug.Log("center clutch under player");
        //Ensuring to only move in 
        //move position in XY plane
        clutch.transform.position = new Vector3(vrCamera.transform.position.x - offset.x, vrCamera.transform.position.y - 0.57f, vrCamera.transform.position.z - offset.z);

        //Moving rotation in Y direction only
        clutch.transform.rotation = Quaternion.Euler(0, vrCamera.transform.localEulerAngles.y, 0);
    }

    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
