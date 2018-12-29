# Background:

Prior to this interface, the previous “blue” VR interface was largely based upon the technology and code carried over from the Mind Mind project (https://www.youtube.com/watch?v=kZlg0QvKkQQ).

This MindMeld project was largely built from the ground up and in the last few years, very capable VR SDKs and hardware has become more ubiquitously available. With 6-DOF controllers and inside out VR HMD tracking, people can more easily create powerful VR based applications. With these newly available technologies and the desire to create a maintainable, modern VR teleoperation interface that would be compatible with the latest VR technology, Berkeley Open Robotics decided to rebuild their VR interface.

Now with this interface based upon Steam VR, ROS#, and Unity, the Berkeley Open Robotics robotic manipulators can be controlled through basically any VR system compatible with Steam VR. The information below gives an initial overview of the features of this system, how to use this system, some of the technical details, and some potential future additions.

## Core Features:

- RealSense PointCloud and RGB Streaming
- ROS# and SteamVR Integration
- Extensible and Flexible for future additions and VR technology


The below directions are for VR interface which works with the full blue URDF.

An alternative branch will be created for the mobile Windows Mixed Reality setup. This has some minor additions which are not currently documented here.

## Technical Details

  INSERT HIERARCHY IMAGE HERE
 
<ul>
<li>Let’s break down the hierarchy first:</li>
<ul>
<li> Note: if you see something in your version of the hierarchy that isn’t represented below, then it is not important in this version and likely can be ignored plus disabled.</li>
<li> RosConnector: </li>
<ul>
<li> This is where all ROS# operations occur and is where the ROS nodes communicate with the Unity objects.</li>
<li> Refer to the ROS# documentation for how this is all working. I highly recommend going through the tutorials.   </li>
</ul>
<li> RsProcessingPipe: </li>
<ul>
<li> This is where the processing of the RealSense point cloud occurs.   </li>
</ul>
<li> MenuManagementObject: </li>
<ul>
<li> This object controls the functionality of the menu system with its attached script. This is explained more in the scripts section below.
</li>
</ul>
<li> ViveCameraRig: </li>
<ul>
<li> This is the object that integrates with the Vive Trackers and allows them to be in the scene. </li>
</ul>
<li> Player: </li>
  INSERT PLAYER IMAGE HERE
<ul>
<li> The Player object contains much of the default SteamVR objects in addition to much of the teleoperation interface.  </li>
<li> Under the left hand, the RobotHandCanvas is where all of the buttons on the menu are displayed. These buttons are linked to functions written in the MenuManagement object. </li>
<li> The Clutch contain much of the scene which controls the teleoperation. </li>
<ul>
<li> The left and right baselink frames contain left and right gripper models under them. The BaseLinkInitializer script attached to the Clutch has hardcoded transforms which it uses to initialize the relative transform between the baselinks and grippers.  </li>
<li> The RsDevice represents the RealSense.  </li>
<li> The canvas represents a Unity canvas which is used to project the RealSense camera stream onto.  </li>
<li> blue_full represents the URDF of the entire blue robotic manipulator.   </li>
</ul>
</ul>
</ul>
</li>
</ul>

### Scripts

- **BaseLinkInitalizer:** This script hardcodes the initial transforms between the grippers and the baselinks.
- **blueInputSystem:** This script communicates with the SteamVR input system. This script is then used in menuManagement to control various things.
- **clutchCollisionControl:** The Interactable script on the left and right gripper models used for teleoperation requires a non-kinematic rigidbody to be on the model. This leads to issues with the grippers colliding into each other and flying away. The grippers would also collide into other things and this was problematic. Therefore, I decided to ignore collisions by layers in the scene. This solution works for now but I would recommend investigating better solutions if time permits.
- **menuManagement:** This script listens for button presses on the left and right blueInputSystems. 
- **PoseStampedPublisher:** Publishes pose of grippers as placed in the Unity scene. 
- **JointStatePatcher:** Enables animation of URDF via JointStates topic.
- **JointStateSubscriber:** Subscribes to ROS’ JointStates topic.

## How to use the interface:

1. Set up ROS nodes such as blue_vr_teleop and others. This step will be handled by Phillipp and Brent at this point. 
2. If testing the VR interface without the robot, ensure that the RosConnector is disabled in the scene
3. If testing with the robot, under the ROS Connector, ensure that the RosBridgeServerUrl is set to the correct IP address.
4. Ensure Steam VR is turned on. 
5. Press Play in the Unity editor
6. Now follow the controls below, ensure the RealSense is plugged in, and everything *should* work.
7. Controls:
- Trigger to grab and release grippers
- Grip button to enable and disable menu.
- Point, push controller into button, and press trigger to select button on hand attached menu  

## Future Additions:

- Multiple RealSense scan merging for easier viewing.
- Recording and playback of robot actions.
- Make it easy and simple to control the robotic manipulator via Vive controllers without having to use VR if desired.
- Potentially making this a general robotic solution where any URDF can be added and then the system will adapt to that specific robotic manipulator.
- Much more HRI investigation!

## Notes / Known Problems: 
- Downsampled URDF (too high definition at the moment leading to dropped VR frames)

- Occasional problems arose during testing where the Vive controllers did not appear in the scene. Seems to be a poblem with SteamVR or the existing setup on the lab computer. The best solution as of now was to turn everything, PC and Vive, on and off again. Make sure to unplug Vive and plug it back in. 

Please feel free to contact Bryce Schmidtchen (bryceschmidtchen@gmail.com) with any problems and questions that arise. 
