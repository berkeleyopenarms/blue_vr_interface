using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{

    public class BaseLinkInitializer : MonoBehaviour
    {

        public GameObject left_baselink;
        public GameObject right_baselink;

        public GameObject left_clutch;
        public GameObject right_clutch;

        // Use this for initialization
        void Start()
        {
            /* Initializes baselink unity controllers o simulation's initial position
             Can be changed in the future to initialize to robot's actual initial position from subscription*/

            transformConversion();
        }

        void transformConversion()
        {
            Quaternion blue_left_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Vector3 blue_left_end_pos = new Vector3(-0.022845f, -0.011566f, 1.000f);

            Quaternion blue_right_end_rot = new Quaternion(0.0f, 0.0f, -0.70712f, 0.7071f);
            Vector3 blue_right_end_pos = new Vector3(-0.022845f, -0.011566f, 1.0195f);

            left_clutch.transform.localPosition = blue_left_end_pos.Ros2Unity();
            right_clutch.transform.localPosition = blue_right_end_pos.Ros2Unity();

            left_clutch.transform.localRotation = blue_left_end_rot.Ros2Unity();
            right_clutch.transform.localRotation = blue_right_end_rot.Ros2Unity();


            Quaternion blue_left_init_orientation = new Quaternion(0.24684f, 0.29046f, 0.92122f, -0.077833f);
            Vector3 blue_left_init_pos = new Vector3(-0.01400f, 0.11289f, 0.0f);

            Quaternion blue_right_init_orientation = new Quaternion(0.24684f, -0.29046f, 0.92122f, 0.077833f);
            Vector3 blue_right_init_pos = new Vector3(-0.01400f, -0.11289f, 0.0f);

            blue_left_init_orientation = blue_left_init_orientation.Ros2Unity();
            blue_right_init_orientation = blue_right_init_orientation.Ros2Unity();

            blue_left_init_pos = blue_left_init_pos.Ros2Unity();
            blue_right_init_pos = blue_right_init_pos.Ros2Unity();

            left_baselink.transform.localPosition = blue_left_init_pos;
            right_baselink.transform.localPosition = blue_right_init_pos;

            left_baselink.transform.localRotation = blue_left_init_orientation;
            right_baselink.transform.localRotation = blue_right_init_orientation;
            right_baselink.transform.localEulerAngles = new Vector3(0.0f, right_baselink.transform.localEulerAngles.y, 0.0f);


        }

    }

}
