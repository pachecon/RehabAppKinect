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
    public class Knees : JointsPosition, ITwoJoints
    {
        private SkeletonPoint joint1, joint2, joint3; //The three points needed for measuring the Angle of Knee Left
        private SkeletonPoint joint4, joint5, joint6; //For measuring the Angle of Knee Right
        private double degreeJoint1; //Angle of Knee Left
        private double degreeJoint2; //Angle of Knee Right

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
            //Left Knee
            SetPosition(skeleton.Joints[(int)JointType.HipLeft]);
            joint1 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeLeft]);
            joint2 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.AnkleLeft]);
            joint3 = jointPosition.Position;

            OneDegreeAngle getBody = new OneDegreeAngle(joint1, joint2, joint3);
            //getBody.ReverseCoordinates = true;

            degreeJoint1 = getBody.GetAngle();
            return degreeJoint1;
        }


        public override double CalculateAngleJoint2(Skeleton skeleton)
        {
            //Right Knee
            SetPosition(skeleton.Joints[(int)JointType.AnkleRight]);
            joint4 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeRight]);
            joint5 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.HipRight]);
            joint6 = jointPosition.Position;

            OneDegreeAngle getBody = new OneDegreeAngle(joint4, joint5, joint6);
            //getBody.ReverseCoordinates = true;

            degreeJoint2 = getBody.GetAngle();
            return degreeJoint2;
        }
    }
}
