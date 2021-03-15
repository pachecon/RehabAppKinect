namespace RehabTest5.Models
{
    using Coding4Fun.Kinect.KinectService.WinRTClient;

    /// <summary>
    /// This class will be used for getting the value of the position of each joint.
    /// It is abstract because each exercise will calculate the angle of the joints (Left and Right side). 
    /// </summary>
    /// <param name="j">Property of the joint to detect.</param>
    /// <param name="DrawLeft">Abstract method for the left side of the Skeleton.</param>
    /// <param name="DrawRight">Right side of the Skeleton.</param>
   
    public abstract class JointsPosition
    {
        protected Joint jointPosition { get; set; }
        public abstract double CalculateAngleJoint1(Skeleton skeleton);
        public abstract double CalculateAngleJoint2(Skeleton skeleton);

        /// <summary>
        /// To obtain the value of the position (X; Y; Z) of the specific Joint
        /// </summary>
        /// <param name="pos">From Library "Coding4Fun.Kinect.KinectService.WinRTClient"
        /// it is possible to get the point of each joint of the skeleton.</param>
        /// <returns>The coordinate (X,Y,Z) value of each joint.</returns>
        public Joint SetPosition(Joint joint)
        {
            SkeletonPoint pos = new SkeletonPoint()
            {
                X = joint.Position.X,
                Y = joint.Position.Y, 
                Z = joint.Position.Z,
            };

            jointPosition = new Joint()
            {
                Position = pos //X; Y; Z;
            };
            return jointPosition;
        }
     }
}
