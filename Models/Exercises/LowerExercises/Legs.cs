using Coding4Fun.Kinect.KinectService.WinRTClient;
using RehabTest5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RehabTest5.Models
{
    public class Legs : JointsPosition, ITwoJoints
    {
        private SkeletonPoint joint1, joint2, joint3; //The three points needed for measuring the Angle of Leg Left
        private SkeletonPoint joint4, joint5, joint6; //For measuring the Angle of Leg Right
        private double degreeJoint1; //Angle of Leg Left
        private double degreeJoint2; //Angle of Leg Right

        private int firstTime; //It is used as a flag because the first time that the class is called. All the initial
                               //values will be saved as a reference of the axis(origin).

        private List<double> referenceLeft = new List<double>(); //It is a reference for the initial position, 
                                                                 //it will be used as a reference axis for left side. 
        private List<double> referenceRight = new List<double>(); //Distance Reference axis for the Right side.
        public List<double> ReferenceLeft
        {
            get
            { 
                return referenceLeft; 
            }

            set
            {
                referenceLeft = value;
            }
        }

        public List<double> ReferenceRight
        {
            get
            { 
                return referenceRight; 
            }

            set
            {
                referenceRight = value;
            }
        }
        private SkeletonPoint jointLegLeft; //Initial position of the left joint. It is used as a reference for the axis.
        private SkeletonPoint jointLegRight; //Initial position of the right joint. It is used as a reference for the axis.

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
              

        public SkeletonPoint JointLegLeft
        {
            get { return jointLegLeft; }
            set { jointLegLeft = value; }
        }
        public SkeletonPoint JointLegRight
        {
            get { return jointLegRight; }
            set { jointLegRight = value; }
        }

        public override double CalculateAngleJoint1(Skeleton skeleton)
        {
            //Leg Left
            SetPosition(skeleton.Joints[(int)JointType.HipLeft]);
            joint1 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeLeft]);
            joint2 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeLeft]);
            joint3 = jointPosition.Position;

            if (firstTime == 0)
            {
                referenceLeft = new List<double>();
                LegDegreeAngle getBody = new LegDegreeAngle(joint1, joint2, joint3, referenceLeft, firstTime, jointLegLeft);
                JointLegLeft = joint2;
                degreeJoint1 = getBody.GetAngle();
                return degreeJoint1;
            }

            LegDegreeAngle getBody2 = new LegDegreeAngle(joint1, joint2, joint3, referenceLeft, firstTime, jointLegLeft);
            degreeJoint1 = getBody2.GetAngle();
            return degreeJoint1;
        }

        public override double CalculateAngleJoint2(Skeleton skeleton)
        {
            //Leg Right
            SetPosition(skeleton.Joints[(int)JointType.HipRight]);
            joint4 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeRight]);
            joint5 = jointPosition.Position;
            SetPosition(skeleton.Joints[(int)JointType.KneeRight]);
            joint6 = jointPosition.Position;

            if (firstTime == 0)
            {
                referenceRight = new List<double>();
                LegDegreeAngle getBody = new LegDegreeAngle(joint4, joint5, joint6, referenceRight, firstTime, jointLegRight);
                firstTime++;
                JointLegRight = joint5;
                degreeJoint1 = getBody.GetAngle();
                return degreeJoint1;
            }

            LegDegreeAngle getBody2 = new LegDegreeAngle(joint4, joint5, joint6, referenceRight, firstTime, jointLegRight);
            degreeJoint2 = getBody2.GetAngle();
           return degreeJoint2;
         }
    }
}
