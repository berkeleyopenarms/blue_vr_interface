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
    public GameObject left_clutch;
    public GameObject right_clutch;

    public GameObject urdfLeftWrist;
    public GameObject urdfRightWrist;

    public Collider myColliderL;
    public Collider myColliderR;

    public blueInputSystem leftHandInputs;
    public blueInputSystem rightHandInputs;

    public bool ready;
    private int firstCall;

    private bool leftTriggerState;
    private bool leftTriggerPrev;

    private bool rightTriggerState;
    private bool rightTriggerPrev;

    private bool initialized;
    public bool publishing;

    public RosSharp.RosBridgeClient.JointStatePatcher patcher;

    private Vector3 offset;
    private int frameStore;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0.3f, 0.7f, 0.0f);
        frameStore = Time.frameCount;
        firstCall = 0;

        leftTriggerState = false;
        leftTriggerPrev = false;

        rightTriggerState = false;
        rightTriggerPrev = false;

        ready = false;
        initialized = false;
        publishing = false;
    }

    // Update is called once per frame
    void Update () {
        //Debug.LoHandInputs.getGrip());
        //Debug.Log(rightHandInputs.getGrip());

        leftTriggerPrev = leftTriggerState;
        leftTriggerState = leftHandInputs.getTouchPad_Down();

        rightTriggerPrev = rightTriggerState;
        rightTriggerState = rightHandInputs.getGrip();
        /**
        if (leftHandInputs.getGrip() && (Time.frameCount - frameStore) > 30)
        {
            menu.active = !menu.active;
            frameStore = Time.frameCount;

            ready = false;
        }
        */
        if (leftHandInputs.getGrip())
        {
            publishing = true;
            patcher.SetPublishJointStates(true);
        }

        if (!leftTriggerPrev && leftTriggerState)
        {
            if (firstCall == 1)
            {
                ready = true;
            }
            Debug.Log("wut");
            reInitialize();
        }
    }

    public void reInitialize()
    {
        if (initialized == false)
        {
            if (firstCall == 0)
            {
                firstCall = 1;
            }
            else if (firstCall == 1)
            {
                ready = true;
            }
            publishing = false;
            //patcher.SetPublishJointStates(false);
            patcher.SetPublishJointStates(false);
            left_clutch.transform.position = urdfLeftWrist.transform.position;
            left_clutch.transform.rotation = urdfLeftWrist.transform.rotation;
            right_clutch.transform.position = urdfRightWrist.transform.position;
            right_clutch.transform.rotation = urdfRightWrist.transform.rotation;
            

            /**
            Vector3 initialRotationR = rightWrist.transform.eulerAngles;
            rightGripper.transform.eulerAngles = new Vector3(initialRotationR.x - 180, initialRotationR.y, initialRotationR.z - 80);
            rightGripper.transform.position = rightWrist.transform.position;
            */


            initialized = true;
        }
        else
        {
            centerClutchUnderPlayer();
            initialized = false;
            publishing = true;
            patcher.SetPublishJointStates(true);
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

    public void toggleUrdf()
    {
        blueUrdf.active = !blueUrdf.active;
    }

}
