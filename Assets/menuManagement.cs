﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


/*
 This script will be used to manage the menu system globally.
 */

public class menuManagement : MonoBehaviour
{

    public GameObject vrCamera;
    public GameObject clutch;
    public GameObject menu;
    public GameObject blueUrdf;
    public GameObject left_clutch;
    public GameObject right_clutch;

    public GameObject left_root;
    public GameObject right_root;


    //Wrist Link of the Robot
    public GameObject urdfLeftWrist;
    public GameObject urdfRightWrist;

    //Controller inputs
    public blueInputSystem leftHandInputs;
    public blueInputSystem rightHandInputs;

    // Boolean variables to toggle on and off JointStatePublisher of the RosConnector
    public bool ready;
    private int firstCall;
    public bool initialized;
    public bool publishing;

    // State of left hand
    private bool leftTriggerState;
    private bool leftTriggerPrev;

    // State of right hand
    private bool rightTriggerState;
    private bool rightTriggerPrev;

    // JointStatePatcher object
    public RosSharp.RosBridgeClient.JointStatePatcher patcher;

    // VR camera offset
    private Vector3 offset;
    
    // Positional left hand trackers
    private Vector3 deltaL;
    private Vector3 leftPrev;
    private Vector3 leftNext;
    private bool resetL;

    // Rotational left hand trackers
    private Quaternion deltaLR;
    private Quaternion leftPrevR;
    private Quaternion leftNextR;
    private bool rotateL;


    // Positional left hand trackers
    private Vector3 deltaR;
    private Vector3 rightPrev;
    private Vector3 rightNext;
    private bool resetR;

    // Rotational left hand trackers
    private Quaternion deltaRR;
    private Quaternion rightPrevR;
    private Quaternion rightNextR;
    private bool rotateR;


    // Use this for initialization
    void Start()
    {
        //VR camera offset
        offset = new Vector3(0.3f, 0.7f, 0.0f);
        
        // Numerical value used to determine where grippers should render in the scene
        firstCall = 0;

        // Track the grip status of the left controller
        leftTriggerState = false;
        leftTriggerPrev = false;

        // Track the grip status of the right controller
        rightTriggerState = false;
        rightTriggerPrev = false;

        ready = false;
        initialized = false;
        publishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the controller grip states
        leftTriggerPrev = leftTriggerState;
        leftTriggerState = leftHandInputs.getTouchPad_Down();
        rightTriggerPrev = rightTriggerState;
        rightTriggerState = rightHandInputs.getGrip();

        // Calculate difference in position to be applied in servo control
        leftPrev = leftNext;
        leftNext = leftHandInputs.getControllerPosition();
        deltaL = leftNext - leftPrev;

        // Calculate difference in rotation to be applied in servo control
        /**
        leftPrevR = leftNextR;
        leftNextR = leftHandInputs.getControllerRotation();
        deltaLR = leftNextR * Quaternion.Inverse(leftPrevR);
        */
        rightPrev = rightNext;
        rightNext = rightHandInputs.getControllerPosition();
        deltaR = rightNext - rightPrev;

        // Calculate difference in rotation to be applied in servo control
        /**
        rightPrevR = rightNextR;
        rightNextR = rightHandInputs.getControllerRotation();
        deltaRR = rightPrevR * Quaternion.Inverse(rightNextR);
        */
        if (!leftHandInputs.getGrip() && initialized)
        {
            if (resetL)
            {
                left_root.transform.position = urdfLeftWrist.transform.position;
                left_clutch.transform.position = urdfLeftWrist.transform.position;
                rotateL = false;
                resetL = false;
            }
        }

        if (!rightHandInputs.getGrip() && initialized)
        {
            if (resetR) { 
                right_root.transform.position = urdfRightWrist.transform.position;
                right_clutch.transform.localPosition = urdfRightWrist.transform.localPosition;
                rotateR = false;
                resetR = false;
            }
        }

        // Use grip to control robot
        if (leftHandInputs.getGrip())
        {
            if (rotateL)
            {
                //left_clutch.transform.rotation *= deltaLR;
                left_root.transform.rotation = leftHandInputs.getControllerRotation();
            }
            rotateL = true;
            left_root.transform.position += deltaL;
        }

        if (rightHandInputs.getGrip())
        {
            if (rotateR)
            {
                //right_clutch.transform.rotation *= deltaRR;
                right_root.transform.rotation = rightHandInputs.getControllerRotation();
            }
            rotateR = true;
            right_root.transform.position += deltaR;
        }

        // Used to reposition the virtual grippers
        if ((!leftTriggerPrev && leftTriggerState))
        {
            /**
            if (firstCall == 1)
            {
                ready = true;
                left_root.transform.position = urdfLeftWrist.transform.position;
                left_clutch.transform.localPosition = urdfLeftWrist.transform.localPosition;
                left_clutch.transform.rotation = urdfLeftWrist.transform.rotation;

                right_root.transform.position = urdfRightWrist.transform.position;
                right_clutch.transform.localPosition = urdfRightWrist.transform.localPosition;
                right_clutch.transform.rotation = urdfRightWrist.transform.rotation;
                //reInitialize();
            }
            */
            reInitialize();
        }
    }

     // Reposition the clutchable grippers to be location of the urdf object wrist links.
    public void reInitialize()
    {
        Debug.Log("here");
        resetL = true;
        resetR = true;
        if (initialized == false)
        {
            //Turn off Urdf GameObject to prevent collisions
            toggleUrdf();

            /** Reposition the clutchable grippers. 
             * Fix new pivot. Recenter and orient controllable grippers in appropriate position
             * */
            left_root.transform.position = urdfLeftWrist.transform.position;
            right_root.transform.position = urdfRightWrist.transform.position;
            
            left_clutch.transform.localPosition = urdfLeftWrist.transform.localPosition;
            left_clutch.transform.rotation = urdfLeftWrist.transform.rotation;

            //right_root.transform.position = urdfRightWrist.transform.position;
            right_clutch.transform.localPosition = urdfRightWrist.transform.localPosition;
            right_clutch.transform.rotation = urdfRightWrist.transform.rotation;


            initialized = true;
            toggleUrdf();
        }
    }


    // Set the global transform of the clutch to be the transform of the player plus a fixed transform
    public void centerClutchUnderPlayer()
    {
        //Ensuring to only move in
        //move position in XY plane
        clutch.transform.position = new Vector3(vrCamera.transform.position.x - offset.x, vrCamera.transform.position.y - 0.82f, vrCamera.transform.position.z - offset.z);

        //Moving rotation in Y direction only
        clutch.transform.rotation = Quaternion.Euler(0, vrCamera.transform.localEulerAngles.y, 0);
    }

    // Turn on or off the urdf gameobject
    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
