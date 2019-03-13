using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{

    public class BaseLinkInitializer : MonoBehaviour
    {

        public GameObject left_baselink;
        public GameObject right_baselink;

        public GameObject leftWrist;
        public GameObject rightWrist;

        public GameObject left_clutch;
        public GameObject right_clutch;

        private bool initialize;

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
            initialize = false;
            //transformConversion();

        }

        private void Update()
        {
            if (initialize)
            {
               // transformConversion();
            }
        }
        
        void transformConversion()
        {

            Quaternion blue_left_end_rot = new Quaternion(0.0f, 0.0f, -0.7071f, 0.7071f);
            //Vector3 blue_left_end_pos = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 blue_left_end_pos = new Vector3(-0.022845f, -0.011566f, 1.000f);
            
            //Vector3 blue_left_end_pos = new Vector3(-0.012845f, -0.011566f, 1.000f);
            //Vector3 blue_left_end_pos = new Vector3(0f, 0f, 0f);

        
            Quaternion blue_right_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            //Vector3 blue_right_end_pos = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 blue_right_end_pos = new Vector3(-0.06500053f, 0.08899984f, 0.8890002f);
            //Vector3 blue_right_end_pos = new Vector3(0f, 0f, 0f);

            left_clutch.transform.localPosition = blue_left_end_pos.Ros2Unity();
            right_clutch.transform.localPosition = blue_right_end_pos.Ros2Unity();

            left_clutch.transform.localRotation = blue_left_end_rot.Ros2Unity();
            right_clutch.transform.localRotation = blue_right_end_rot.Ros2Unity();

            //****************************


            Quaternion blue_left_init_orientation = new Quaternion(0.24684f, 0.29046f, 0.92122f, -0.077833f);
            Vector3 blue_left_init_pos = new Vector3(0.0f, 0.0f, 0.0f);
            //Vector3 blue_left_init_pos = new Vector3(-0.01400f, 0.11289f, 0.0f);

            Quaternion blue_right_init_orientation = new Quaternion(0.24684f, -0.29046f, 0.92122f, 0.077833f);
            Vector3 blue_right_init_pos = new Vector3(0.0f, 0.0f, 0.0f);
            //Vector3 blue_right_init_pos = new Vector3(-0.116f, 0.881f, -0.0009999871f);
            //Vector3 blue_right_init_pos = new Vector3(-0.01400f, -0.11289f, 0.0f);

            blue_left_init_orientation = blue_left_init_orientation.Ros2Unity();
            blue_right_init_orientation = blue_right_init_orientation.Ros2Unity();

            blue_left_init_pos = blue_left_init_pos.Ros2Unity();
            blue_right_init_pos = blue_right_init_pos.Ros2Unity();

            left_baselink.transform.localPosition = blue_left_init_pos;
            right_baselink.transform.localPosition = blue_right_init_pos;

            left_baselink.transform.localRotation = blue_left_init_orientation;
            right_baselink.transform.localRotation = blue_right_init_orientation;
        }
    
    /**
        void transformConversion()
        {
            /**
            Quaternion blue_left_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Vector3 blue_left_end_pos = new Vector3(-0.022845f, -0.011566f, 1.000f);
            */


            //****************************

            /**
            Quaternion blue_left_init_orientation = new Quaternion(0.24684f, 0.29046f, 0.92122f, -0.077833f);
            Vector3 blue_left_init_pos = new Vector3(0f, 0f, 0f);
            
            
            Quaternion blue_left_init_orientation = leftWrist.transform.localRotation;
            float xl = leftWrist.transform.position.x;
            float yl = leftWrist.transform.position.y;
            float zl = leftWrist.transform.position.z;


            Vector3 blue_left_init_pos = new Vector3(xl, yl, zl);

            //Quaternion blue_right_init_orientation = new Quaternion(0.24684f, -0.29046f, 0.92122f, 0.077833f);
            //Vector3 blue_right_init_pos = new Vector3(-0.01400f, -0.11289f, 0.0f);
            Quaternion blue_right_init_orientation = rightWrist.transform.localRotation;
            Vector3 blue_right_init_pos = rightWrist.transform.localPosition;

            blue_left_init_orientation = blue_left_init_orientation.Ros2Unity();
            blue_right_init_orientation = blue_right_init_orientation.Ros2Unity();

            blue_left_init_pos = blue_left_init_pos.Ros2Unity();
            blue_right_init_pos = blue_right_init_pos.Ros2Unity();

            left_baselink.transform.localPosition = blue_left_init_pos;
            right_baselink.transform.localPosition = blue_right_init_pos;

            left_baselink.transform.localRotation = blue_left_init_orientation;
            right_baselink.transform.localRotation = blue_right_init_orientation;

            Quaternion blue_left_end_rot = leftWrist.transform.rotation;
            float x = leftWrist.transform.position.x;
            float y = leftWrist.transform.position.y;
            float z = leftWrist.transform.position.z;


            Vector3 blue_left_end_pos = leftWrist.transform.position;

            Quaternion blue_right_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Vector3 blue_right_end_pos = new Vector3(-0.022845f, -0.011566f, 1.0195f);

            left_clutch.transform.localPosition = blue_left_end_pos.Ros2Unity();
            right_clutch.transform.localPosition = blue_right_end_pos.Ros2Unity();

            left_clutch.transform.localRotation = blue_left_end_rot.Ros2Unity();
            right_clutch.transform.localRotation = blue_right_end_rot.Ros2Unity();

        }
    */

    }

}
