using System;
using System.Collections;
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
    public GameObject rightGripper;
    public GameObject leftGripper;

    // Roll link elements of URDF
    public GameObject leftWrist;
    public GameObject rightWrist;

    // Get controller inputs
    public blueInputSystem leftHandInputs;
    public blueInputSystem rightHandInputs;

    private Vector3 offset;
    private int frameStore;
    private bool summon;
    private bool initialize;


    // Grip states to prevent to ensure summon called once
    private bool leftTriggerState;
    private bool leftTriggerPrev;

    private bool rightTriggerState;
    private bool rightTriggerPrev;


    // Use this for initialization
    void Start () {
        offset = new Vector3(0.3f, 0.7f, 0.0f);
        frameStore = Time.frameCount;
        summon = false;
        initialize = false;
        leftTriggerState = false;
        leftTriggerPrev = false;

        rightTriggerState = false;
        rightTriggerPrev = false;
    }

    // Update is called once per frame
    void Update () {

        leftTriggerPrev = leftTriggerState;
        leftTriggerState = leftHandInputs.getTouchPad_Down();

        rightTriggerPrev = rightTriggerState;
        rightTriggerState = rightHandInputs.getGrip();

        if (!leftTriggerPrev && leftTriggerState)
        {
            Debug.Log("wut");
            reInitialize();
            //summonClutch();
        }
        /**
        if (!rightTriggerPrev && rightTriggerState)
        {
            Debug.Log("Here!");
            summonRightClutch();
        }
        */
    }

    // Set the global transform of the clutch to be the transform of the player plus a fixed transform
    public void centerClutchUnderPlayer()
    {
        Debug.Log("center clutch under player");
        //Ensuring to only move in 
        //move position in XY plane
        clutch.transform.position = new Vector3(vrCamera.transform.position.x - offset.x, vrCamera.transform.position.y - 0.82f, vrCamera.transform.position.z - offset.z);

        //Moving rotation in Y direction only
        clutch.transform.rotation = Quaternion.Euler(0, vrCamera.transform.localEulerAngles.y, 0);
    }


    public void reInitialize()
    {
        if (initialize == false)
        {
            

            Vector3 initialRotationL = leftWrist.transform.eulerAngles;
            leftGripper.transform.eulerAngles = new Vector3(initialRotationL.x + 180, initialRotationL.y, initialRotationL.z + 80);
            leftGripper.transform.position = leftWrist.transform.position;


            Vector3 initialRotationR = rightWrist.transform.eulerAngles;
            rightGripper.transform.eulerAngles = new Vector3(initialRotationR.x - 180, initialRotationR.y, initialRotationR.z - 80);
            rightGripper.transform.position = rightWrist.transform.position;


            initialize = true;
            toggleUrdf();
        } else
        {
            centerClutchUnderPlayer();
            initialize = false;
        }
    }

    public void summonClutch()
    {
        // Allow robot to start from any position
        if (initialize == false)
        {
            toggleUrdf();
            
            Vector3 initialRotationL = leftWrist.transform.eulerAngles;
            leftGripper.transform.eulerAngles = new Vector3(initialRotationL.x + 180, initialRotationL.y, initialRotationL.z + 80);      
            leftGripper.transform.position = leftWrist.transform.position;


            Vector3 initialRotationR = rightWrist.transform.eulerAngles;
            rightGripper.transform.eulerAngles = new Vector3(initialRotationR.x - 180, initialRotationR.y, initialRotationR.z - 80);
            rightGripper.transform.position = rightWrist.transform.position;


            initialize = true;
            toggleUrdf();
        }
        
        else
        {
            // Toggle real time feedback
            toggleUrdf();

            centerClutchUnderPlayer();

            leftGripper.transform.rotation = leftHandInputs.getControllerRotation();
            Vector3 rotation = leftGripper.transform.rotation.eulerAngles;

            rotation = new Vector3(rotation.x, rotation.y + 120, rotation.z - 30);
            leftGripper.transform.eulerAngles = rotation;

            // Apply offsets due to parenting
            leftGripper.transform.position = new Vector3((float)leftHandInputs.getControllerPosition().x,
                                                (float)leftHandInputs.getControllerPosition().y + 0.30f,
                                                (float)leftHandInputs.getControllerPosition().z - 1.1f);
            /**
            Vector3 rotation = leftGripper.transform.rotation.eulerAngles;

            rotation = new Vector3(rotation.x, rotation.y += 180, rotation.z);
            leftGripper.transform.eulerAngles = rotation;
            */
            rightGripper.transform.rotation = rightHandInputs.getControllerRotation();
            Vector3 rotation2 = rightGripper.transform.rotation.eulerAngles;

            rotation2 = new Vector3(rotation2.x, rotation2.y += 180, rotation2.z);
            rightGripper.transform.eulerAngles = rotation2;

            // Apply offsets due to parenting
            rightGripper.transform.position = new Vector3((float)rightHandInputs.getControllerPosition().x,
                                                (float)rightHandInputs.getControllerPosition().y + 0.3f,
                                                (float)rightHandInputs.getControllerPosition().z - 1.1f);
    
            toggleUrdf();

        }
    }

    // Toggle Realt
    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
