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

    Vector3 pos;


    // Use this for initialization
    void Start () {
        offset = new Vector3(0.3f, 0.7f, 0.0f);
        frameStore = Time.frameCount;
        summon = false;
        initialize = false;
        pos = leftHandInputs.getControllerPosition();
    }

    // Update is called once per frame
    void Update () {
        
        //Debug.Log(leftHandInputs.getGrip());
        //Debug.Log(rightHandInputs.getGrip());

        //if (leftHandInputs.getGrip() && (Time.frameCount - frameStore) > 30)
        if (leftHandInputs.getGrip())
        {
            /**
            menu.active = !menu.active;
            frameStore = Time.frameCount;
            */
            summonLeftClutch();
            pos = leftHandInputs.getControllerPosition();
        }

        if (leftHandInputs.getTouchPad())
        {
            if (Math.Abs(leftHandInputs.getControllerPosition().magnitude - pos.magnitude)  > 0.2f)
            {
                Debug.Log("here");
            }
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
            leftGripper.transform.localPosition = leftWrist.transform.localPosition;
            leftGripper.transform.rotation = leftHandInputs.getControllerRotation();
            leftHandInputs.getTouchPad();
        }
        
        else
        {
            //initialize = false;
            centerClutchUnderPlayer();
            leftGripper.transform.rotation = leftHandInputs.getControllerRotation();
            leftGripper.transform.position = new Vector3((float)leftHandInputs.getControllerPosition().x - 0,
                                                (float)leftHandInputs.getControllerPosition().y + 0.3f,
                                                (float)leftHandInputs.getControllerPosition().z - 0.95f);

        }
    }

    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
