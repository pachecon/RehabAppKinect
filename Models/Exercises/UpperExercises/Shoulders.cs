namespace RehabTest5.Models
{
    using Coding4Fun.Kinect.KinectService.WinRTClient;
    using RehabTest5.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// T
    /// 
    /// </summary>
    /// <param name=""> </param>

    public class Shoulders : JointsPosition, ITwoJoints
    {
        private SkeletonPoint joint1, joint2, joint3; //For Shoulder Left
        private SkeletonPoint joint4, joint5, joint6; //For Shoulder Right
        private double degreeJoint1;
        private double degreeJoint2;
              
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
            //Left Shoulder
            SetPosition(skeleton.Joints[(int)JointType.HipLeft]);
            joint1 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ShoulderLeft]);
            joint2 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ElbowLeft]);
            joint3 = jointPosition.Position;

            OneDegreeAngle getBody = new OneDegreeAngle(joint1, joint2, joint3);
            getBody.ReverseCoordinates = true;
            degreeJoint1 = getBody.GetAngle();
            return degreeJoint1;
        }


        public override double CalculateAngleJoint2(Skeleton skeleton)
        {
            //Right Shoulder
            SetPosition(skeleton.Joints[(int)JointType.HipRight]);
            joint4 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ShoulderRight]);
            joint5 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.ElbowRight]);
            joint6 = jointPosition.Position;

            OneDegreeAngle getBody = new OneDegreeAngle(joint4, joint5, joint6);
            getBody.ReverseCoordinates = true;
            degreeJoint2 = getBody.GetAngle();
            return degreeJoint2;
        }
    }
}
