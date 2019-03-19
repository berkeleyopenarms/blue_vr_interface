using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{

    public class BaseLinkInitializer : MonoBehaviour
    {

        public GameObject left_baselink;
        public GameObject right_baselink;

        public GameObject leftUrdf;
        public GameObject rightUrdf;

        public GameObject left_clutch;
        public GameObject right_clutch;

        public GameObject urdfLeftWrist;
        public GameObject urdfRightWrist;


        /*
        private Quaternion blue_left_end_rot
        private Quaternion

        private Vector3
        private Vector3

        private Quaternion
        private Quaternion

        private Vector3
        private Vector3
        */

        // Use this for initialization
        void Start()
        {
            /* Initializes baselink unity controllers o simulation's initial position
             Can be changed in the future to initialize to robot's actual initial position from subscription*/

            transformConversion();

        }

        void transformConversion()
        {
            Quaternion blue_left_init_orientation = new Quaternion(0.24684f, 0.29046f, 0.92122f, -0.077833f);
            Vector3 blue_left_init_pos = new Vector3(0f, 0f, 0.0f);

            Quaternion blue_right_init_orientation = new Quaternion(0.24684f, -0.29046f, 0.92122f, 0.077833f);
            Vector3 blue_right_init_pos = new Vector3(-0.01400f, -0.11289f, 0.0f);

            blue_left_init_orientation = blue_left_init_orientation;
            blue_right_init_orientation = blue_right_init_orientation.Ros2Unity();

            blue_left_init_pos = blue_left_init_pos.Ros2Unity();
            blue_right_init_pos = blue_right_init_pos.Ros2Unity();

            left_baselink.transform.position = leftUrdf.transform.position;
            right_baselink.transform.position = rightUrdf.transform.position;

            left_baselink.transform.rotation = leftUrdf.transform.rotation;
            right_baselink.transform.rotation = rightUrdf.transform.rotation;

            //Quaternion blue_left_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Quaternion blue_left_end_rot = new Quaternion(0.0f, 0.9f, -0.80712f, 0.7071f);
            Vector3 blue_left_end_pos = new Vector3(-0.022845f, -0.011566f, 1.000f);
            //Vector3 hackyFix = urdfWrist.transform.localPosition;
            //Vector3 differentCoordSystem = new Vector3(hackyFix.x, hackyFix.y, hackyFix.z);
            //Debug.Log(urdfWrist.transform.position);
            //Debug.Log(Quaternion.Inverse(urdfWrist.transform.localRotation));
            //Quaternion wristRot = urdfWrist.transform.rotation;
            //Debug.Log(wristRot.x);
            //Debug.Log(wristRot.y);
            //Debug.Log(wristRot.z);
            //Debug.Log(wristRot.w);
            //Debug.Log(blue_left_end_rot);


            Quaternion blue_right_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Vector3 blue_right_end_pos = new Vector3(-0.022845f, -0.011566f, 1.0195f);

           // Vector3 absPosition = urdfWrist.transform.position + transform.parent.transform.position;

            left_clutch.transform.position = urdfLeftWrist.transform.position;
            right_clutch.transform.position = urdfRightWrist.transform.position;
            //left_clutch.transform.localPosition = blue_left_end_pos.Ros2Unity();

            left_clutch.transform.rotation = urdfLeftWrist.transform.rotation;
            right_clutch.transform.rotation = urdfRightWrist.transform.rotation;

            //****************************


            
        }

    }

}
