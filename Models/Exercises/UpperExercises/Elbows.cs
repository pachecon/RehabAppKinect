using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coding4Fun.Kinect.KinectService.WinRTClient;
using Windows.UI.Xaml;
using RehabTest5.Interfaces;


namespace RehabTest5.Models
{
    public class Elbows : JointsPosition, ITwoJoints
    {
        private SkeletonPoint joint1, joint2, joint3; //The three points needed for measuring the Angle of Elbows Left
        private SkeletonPoint joint4, joint5, joint6; //For measuring the Angle of Elbows Right
        private double degreeJoint1; //Angle of Elbows Left
        private double degreeJoint2; //Angle of Elbows Right
        
        public double DegreeJoint1
        {
            get
            {
                return degreeJoint1;
            }
            set
            {
                degreeJoint1 = value;
            }
        }

        public double DegreeJoint2
        {
            get
            {
                return degreeJoint2;
            }
            set
            {
                degreeJoint2 = value;
            }
        }

        public override double CalculateAngleJoint1(Skeleton skeleton)
        {
            // Reverse the order of the body segments in the left arm relative to the right arm
            // to account for the fact that they are on the opposite side.

            //Left Elbow
            SetPosition(skeleton.Joints[(int)JointType.ShoulderLeft]);
            joint1 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ElbowLeft]);
            joint2 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.WristLeft]);
            joint3 = jointPosition.Position;
            OneDegreeAngle getBody = new OneDegreeAngle(joint1, joint2, joint3);

            degreeJoint1 = getBody.GetAngle();
            return degreeJoint1;
        }


        public override double CalculateAngleJoint2(Skeleton skeleton)
        {
            //Right Elbow
            SetPosition(skeleton.Joints[(int)JointType.WristRight]);
            joint4 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ElbowRight]);
            joint5 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ShoulderRight]);
            joint6 = jointPosition.Position;

            OneDegreeAngle getBody = new OneDegreeAngle(joint4, joint5, joint6);

            degreeJoint2 = getBody.GetAngle();
            return degreeJoint2;
        }
   }
}
