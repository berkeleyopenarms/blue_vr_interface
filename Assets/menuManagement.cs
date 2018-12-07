using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 This script will be used to manage the menu system globally.
 */

public class menuManagement : MonoBehaviour {

    public GameObject vrCamera;
    public GameObject clutch;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0.3f, 0.7f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Set the global transform of the clutch to be the transform of the player plus a fixed transform
    public void centerClutchUnderPlayer()
    {
        Debug.Log("center clutch under player");
        clutch.transform.position = vrCamera.transform.position - offset;
        clutch.transform.rotation = vrCamera.transform.rotation;
    }

}
