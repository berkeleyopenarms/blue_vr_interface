using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    [SteamVR_DefaultAction("Teleport")]

    public SteamVR_Action_Boolean squeezeAction;

    public SteamVR_Action_Vector2 touchPadAction;

    // Update is called once per frame
    void Update()
    {
        /*
        if (SteamVR_Input._default.inActions.Teleport.GetState(SteamVR_Input_Sources.Any))
        {
            print("Teleport down");
        }

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("grab pinch up");
        }
        */
        /*
        bool triggerValue = squeezeAction.GetStateDown(SteamVR_Input_Sources.Any);

        if (triggerValue)
        {

            print("triggered");

        }

        Vector2 touchPadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);

        if (touchPadValue != Vector2.zero)
        {
            print(touchPadValue);
        }
        */
    }

}
