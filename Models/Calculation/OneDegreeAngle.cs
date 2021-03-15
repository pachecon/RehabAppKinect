namespace RehabTest5.Models
{
    using System;
    using Coding4Fun.Kinect.KinectService.WinRTClient;
    using TCD.Mathematics;

    /// <summary>
    /// This class will be used for calculating the value of the Angle of the Joint.
    /// </summary>
    /// <param name="joint1 to 3">Indicates the position of each Joint</param>
    /// <param name="Degree">Saving the value of the Joint Angle in Degrees.</param>
    /// <param name="reverse">For inverting the coordinate system in case it is needed.</param>
    /// <param name="rotation">For rotate the axis if it is needed.</param>

    public class OneDegreeAngle 
    {
         protected SkeletonPoint joint1, joint2, joint3;
        private double degree;
        private int rotation = 0;
        private bool reverse = false;
        

        public double Degree
        {
            get
            {
                return this.degree;
            }
            set
            {
                this.degree = value;
            }
        }

        public OneDegreeAngle()
        {
        }

        public OneDegreeAngle(SkeletonPoint joint1, SkeletonPoint joint2, SkeletonPoint joint3)
        {
            this.joint1 = joint1;
            this.joint2 = joint2;
            this.joint3 = joint3;
        }

        public int RotationOffset
        {
            get { return rotation; }
            set
            {
                rotation = value % 360;
            }
        }

        public bool ReverseCoordinates
        {
            get { return reverse; }
            set { reverse = value; }
        }

        protected double CalculateReverseCoordinates(double degrees)
        {
            degrees = (-degrees + 180) % 360;
            return Math.Truncate(degrees);
        }

        public virtual double GetAngle()
        {
            Vector3D vector1 = new Vector3D(joint1.X
                - joint2.X, joint1.Y - joint2.Y, joint1.Z - joint2.Z);
            Vector3D vector2 = new Vector3D(joint2.X - joint3.X, joint2.Y - joint3.Y, joint2.Z - joint3.Z);

            try
            {
                vector1.Normalize();
                vector2.Normalize();

                Vector3D crossProduct = Vector3D.CrossProduct(vector1, vector2);
                double crossProductLength = crossProduct.Z;
                double dotProduct = Vector3D.DotProduct(vector1, vector2);
                double segmentAngle = Math.Atan2(crossProductLength, dotProduct);

                double degrees = segmentAngle * (180 / Math.PI); //Change it from rad to degree
                degree = Math.Truncate(100 * degrees) / 100;

                // Convert the value calculated above to a range from 0 to 360.
                degree = (degree + rotation) % 360;

                if (crossProductLength < 0)
                {
                    degree = Math.Abs(degree);
                }

                // Calculate whether the coordinates should be reversed to account for different sides 
                if (reverse)
                {
                    degree = CalculateReverseCoordinates(degree);
                }
                return degree;
            }
            catch (NullVectorException)
            {
                return degree;
            }
        }
    }
}
