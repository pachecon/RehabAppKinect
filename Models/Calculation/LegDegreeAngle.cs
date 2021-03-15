using Coding4Fun.Kinect.KinectService.WinRTClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCD.Mathematics;

namespace RehabTest5.Models
{
    public class LegDegreeAngle : OneDegreeAngle
    {
        private double rotationOffset;
        private bool reverseCoordinates;
        private double degree;
        private double distanceOne;
       
        private List<double> distanceReference = new List<double>();
        public List<double> DistanceReference
        {
            get
            { return distanceReference; }

            set
            {
                distanceReference = value;
            }
        }
        
        private SkeletonPoint jointKnee; //The position of the Knee represents the movement of the Leg.
        public SkeletonPoint JointKnee
        {
            get { return jointKnee; }
            set { jointKnee = value; }
        }
        
        private int firstTime; //For setting the first value as a reference.
        public int FirstTime
        {
            get { return firstTime; }
            set { firstTime = value; }
        }


        public LegDegreeAngle(SkeletonPoint joint1, SkeletonPoint joint2, SkeletonPoint joint3, List<double> distanceReference, int firstTime, SkeletonPoint jointKnee) 
            : base(joint1, joint2, joint3 )
        {
            rotationOffset = RotationOffset;
            reverseCoordinates = ReverseCoordinates;
            degree = Degree;
            DistanceReference = distanceReference;
            FirstTime = firstTime;
            JointKnee = jointKnee;
        }

        /// <summary>
        /// 
        ///  
        /// </summary>
        /// <param name="firstTime">For setting the first value as a reference.</param>
        /// <param name="distanceReference"> The first value when the Leg is in the Zero Position</param>
        /// <param name="">.</param>
        public override double GetAngle()
        {
          try
            {
                if (firstTime == 0)
                {
                    distanceReference.Add(FirstTimeCalculation( joint1,  joint2));
                 }
               foreach ( double x in DistanceReference)
               {
                   distanceOne = x;
               }
                 
                double distanceSecond = Math.Sqrt(Math.Pow((joint2.X - jointKnee.X), 2) +
                                                         Math.Pow((joint2.Y - jointKnee.Y), 2));
                
                double radians = CalculateAngle(distanceOne, distanceSecond);

                double degrees = radians * (180 / Math.PI); //Change it from rad to degree
                degree = Math.Truncate(100 * degrees) / 100;
                
                if (double.IsNaN(degree))
                {
                    degree = 0.0;
                }

                return degree;
            }

            catch (NullVectorException)
            {
                return degree;
            }
        }

        private double CalculateAngle(double distance, double distance2)
        {
            double angle = Math.Acos((2 * Math.Pow(distance, 2) - Math.Pow(distance2, 2))/(2 * Math.Pow(distance, 2)));
                return angle;
        }
        private double FirstTimeCalculation(SkeletonPoint joint1, SkeletonPoint joint2)
        {
         double distanceReference = Math.Sqrt(Math.Pow((joint1.X - joint2.X), 2) +
                                                         Math.Pow((joint1.Y - joint2.Y), 2));
         JointKnee = joint2;
         firstTime++;
                    
        return distanceReference;
        }
    }
}
