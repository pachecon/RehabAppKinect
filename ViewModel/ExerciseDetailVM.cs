namespace RehabTest5
{
    using Coding4Fun.Kinect.KinectService.WinRTClient;
    using HealthApp.Common;
    using RehabTest5.Interfaces;
    using RehabTest5.Models;
    using System;

    /// <summary>
    /// This class is used to load the specific exercise and save the values of the Angle in degree of 1, 2, 3 or 4 Joints. 
    /// </summary>
    /// <param name="skeleton">It is used for getting the position of each Joint. </param>
    /// <param name="degreeJoint1 to 4">For saving the value of the Angle of each Joint. Maximum 4 joints.</param>
    /// <param name="txtJoint1 to 4">For writting the title of the corresponding Joint. </param>
    
    public class ExerciseDetailVM : BindableBase, ITwoJoints, IFourJoints
    {
        //Create an object of each exercise. 
        Shoulders moveShoulder = new Shoulders();
        Elbows moveElbow = new Elbows();
        Knees moveKnees = new Knees();
        Legs moveLegs = new Legs();

        public Skeleton Skeleton { get; set; }

        public double DegreeJoint1 { get; set; }
        public double DegreeJoint2 { get; set; }
        public double DegreeJoint3 { get; set; }
        public double DegreeJoint4 { get; set; }

        private string titleJoint1;
        public string TitleJoint1
        {
            get { return titleJoint1; }
            set { titleJoint1 = value; }
        }

        private string titleJoint2;
        public string TitleJoint2
        {
            get { return titleJoint2; }
            set { titleJoint2 = value; }
        }

        private string titleJoint3 = "";
        public string TitleJoint3
        {
            get { return titleJoint3; }
            set { titleJoint3 = value; }
        }

        private string titleJoint4 = "";
        public string TitleJoint4
        {
            get { return titleJoint4; }
            set { titleJoint4 = value; }
        }

        public ExerciseDetailVM()
        {

        }

        /// <summary>
        /// It is used to calculate the angle of each Joint
        /// </summary>
        /// <param name="DegreeJoint1 to 4"> For saving the corresponding angle.</param>
        internal void TypeOfExercise(string exerciseID)
        {
            switch (exerciseID)
            {
                case "Shoulders":
                    DegreeJoint1 = moveShoulder.CalculateAngleJoint1(Skeleton);  // Obtaining the angle of Shoulder Left
                    DegreeJoint2 = moveShoulder.CalculateAngleJoint2(Skeleton); // Obtaining the angle of Shoulder Right
                    break;

                case "Elbows":
                    DegreeJoint1 = moveElbow.CalculateAngleJoint1(Skeleton); // Obtaining the angle of Elbow Left
                    DegreeJoint2 = moveElbow.CalculateAngleJoint2(Skeleton); // Obtaining the angle of Elbow Right
                    break;

                case "ShoulderAndLeg":
                    DegreeJoint1 = moveShoulder.CalculateAngleJoint1(Skeleton); //Obtaining the angle of Shoulder Left
                    DegreeJoint2 = moveLegs.CalculateAngleJoint1(Skeleton); //Obtaining the angle of Leg Left
                    DegreeJoint3 = moveShoulder.CalculateAngleJoint2(Skeleton); //Shoulder Right
                    DegreeJoint4 = moveLegs.CalculateAngleJoint2(Skeleton);//Leg Right
                    break;

                case "FourJoints":
                    DegreeJoint1 = moveShoulder.CalculateAngleJoint2(Skeleton); //Shoulder Right
                    DegreeJoint2 = moveLegs.CalculateAngleJoint1(Skeleton); //Obtaining the angle of Leg Left
                    DegreeJoint3 = moveShoulder.CalculateAngleJoint1(Skeleton); //Obtaining the angle of Shoulder Left
                    DegreeJoint4 = moveLegs.CalculateAngleJoint2(Skeleton);//Leg Right
                    break;


                case "Knees":
                    DegreeJoint1 = moveKnees.CalculateAngleJoint1(Skeleton); // Obtaining the angle of Knee Left
                    DegreeJoint2 = moveKnees.CalculateAngleJoint2(Skeleton); // Obtaining the angle of Knee Right
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// It is used to set the specific Title of each Joint depending on the exercise
        /// </summary>
        /// <param name="TitleJoint1 to 4"> For saving the corresponding title name.</param>
        internal void TitleExercise(string exerciseID)
        {
            switch (exerciseID)
            {
                case "Shoulders":
                    TitleJoint1 = "Left Shoulder Angle";
                    TitleJoint2 = "Right Shoulder Angle";
                    break;

                case "Elbows":
                    TitleJoint1 = "Left Elbow Angle";
                    TitleJoint2 = "Right Elbow Angle";
                    break;

                case "ShoulderAndLeg":
                    TitleJoint1 = "Left Shoulder Angle";
                    TitleJoint2 = "Left Leg Angle";
                    TitleJoint3 = "Right Shoulder Angle";
                    TitleJoint4 = "Right Leg Angle";

                    break;

                case "FourJoints":
                    TitleJoint1 = "Right Shoulder Angle";
                    TitleJoint2 = "Left Leg Angle";
                    TitleJoint3 = "Left Shoulder Angle";
                    TitleJoint4 = "Right Leg Angle";
                    break;


                case "Knees":
                    TitleJoint1 = "Left Knee Angle";
                    TitleJoint2 = "Right Knee Angle";
                    break;

                default:
                    break;
            }
        }
     }
}
