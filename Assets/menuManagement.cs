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

    public GameObject leftWrist;

    public blueInputSystem leftHandInputs;
    public blueInputSystem rightHandInputs;

    private Vector3 offset;
    private int frameStore;
    private bool summon;
    private bool initialize;

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
            summonLeftClutch();
        }

        if (rightHandInputs.getGrip())
        {
            Debug.Log("Here!");
            summonRightClutch();
        }
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


    public void summonRightClutch()
    {
        //Check to make sure that proper function is called
        Debug.Log("Summoning Right Clutch");

        if (summon == false)
        {
            Vector3 controllerPos = new Vector3((float)rightHandInputs.getControllerPosition().x,
                                                (float)rightHandInputs.getControllerPosition().y,
                                                (float)rightHandInputs.getControllerPosition().z);
            //new Vector3((float)rightHandInputs.getControllerPosition().x + 0.3f, (float)rightHandInputs.getControllerPosition().y + 0.1630991f, (float)rightHandInputs.getControllerPosition().z - 1.3534097f);
            Debug.Log(controllerPos.ToString("F6"));
            centerClutchUnderPlayer();
            /**
            rightGripper.transform.position = new Vector3((float)rightHandInputs.getControllerPosition().x + 0.1f,
                                                (float)rightHandInputs.getControllerPosition().y + .3f,
                                                (float)rightHandInputs.getControllerPosition().z - 0.8f);
    */
            rightGripper.transform.position = new Vector3((float)rightHandInputs.getControllerPosition().x,
                                                (float)rightHandInputs.getControllerPosition().y + 0.3f,
                                                (float)rightHandInputs.getControllerPosition().z - 0.95f);
            summon = true;
        } else
        {
            summon = false;
        }
    }

    public void summonLeftClutch()
    {
        if (initialize == false)
        {
            initialize = true;
            Vector3 initialRotation = leftWrist.transform.eulerAngles;
            leftGripper.transform.eulerAngles = new Vector3(initialRotation.x, initialRotation.y, initialRotation.z);      
            leftGripper.transform.position = leftWrist.transform.position;
        }
        
        else
        {
            //initialize = false;
            centerClutchUnderPlayer();
            leftGripper.transform.rotation = leftHandInputs.getControllerRotation();

            leftGripper.transform.position = new Vector3((float)leftHandInputs.getControllerPosition().x - 0,
                                                (float)leftHandInputs.getControllerPosition().y + 0.3f,
                                                (float)leftHandInputs.getControllerPosition().z - 0.95f);

            Vector3 rotation = leftGripper.transform.rotation.eulerAngles;

            rotation = new Vector3(rotation.x, rotation.y += 180, rotation.z);
            leftGripper.transform.eulerAngles = rotation;

        }
    }

    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
