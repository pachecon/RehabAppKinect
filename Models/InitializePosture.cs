using Coding4Fun.Kinect.KinectService.WinRTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabTest5.Models
{
    /// <summary>
    /// To detect if the user is in front of the Kinect.
    /// </summary>
 
    public class InitializePosture
    {
       public float Threshold { get; set; }
       public float ThresholdHeight { get; set; }
       public bool InfrontKinect { get; set; }

       /// <summary>
       /// It is used to determine the direction of the user.
       /// The animation and the calculation of the angles will start when the user is in fornt of the sensor.
       /// For getting this information, it is possible to determine if the two shoulders of the skeleton are at the same 
       /// distance (given a threshold) from the sensor.
       /// If the absolute difference is within a small threshold, the joints are considered to be within the same plane.
       /// </summary>
       /// <param name="Threshold"> For determining that the two shoulders are in the same plane </param>
       /// <param name="ThresholdHeight"> For determining that the head and one ankle are in the same plane </param>
       /// <param name="FirstTime"> For determining the posture of the user only one time </param>
       
        public InitializePosture() 
       { 
           Threshold = 0.05f;
           ThresholdHeight = 1.5f;
           InfrontKinect = false;
       } 
              
       public bool UserTowardsSensor(Skeleton skeleton) 
       {
           if (!InfrontKinect)
           {
               var head = Vector3.ToVector3(skeleton.Joints.Where(j => j.JointType
                   == JointType.Head).First().Position);
               var ankleRight = Vector3.ToVector3(skeleton.Joints.Where(j => j.JointType
                   == JointType.AnkleRight).First().Position);
               var leftShoulderPosition = Vector3.ToVector3(skeleton.Joints.Where(j => j.JointType
                   == JointType.ShoulderLeft).First().Position);
               var rightShoulderPosition = Vector3.ToVector3(skeleton.Joints.Where(j => j.JointType
                   == JointType.ShoulderRight).First().Position);

               var headDistance = head.Z;
               var ankleRightDistance = ankleRight.Z;
               var leftDistance = leftShoulderPosition.Z;
               var rightDistance = rightShoulderPosition.Z;

               if (headDistance > ThresholdHeight && ankleRightDistance > ThresholdHeight)
               {
                   if (Math.Abs(leftDistance - rightDistance) > Threshold) 
                       return false;

                   InfrontKinect = true;
                   return true;
               }
               return false;
           }
           else
           {
               InfrontKinect = true;
               return true;
           }
       } 
    }
}
