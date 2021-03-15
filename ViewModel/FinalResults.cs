using HealthApp.Common;
using RehabTest5.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace RehabTest5
{
    public class FinalResults : BindableBase
    {
        ObservableCollection<InstructionExercises> instruction = new ObservableCollection<InstructionExercises>();
        InstructionExercises imageSource = new InstructionExercises();

        public ImageSource AnimationImg1 { get; set; }
        public ImageSource AnimationImg2 { get; set; }
        public ImageSource AnimationImg3 { get; set; }
        public ImageSource AnimationImg4 { get; set; }
        public ImageSource AnimationImg5 { get; set; }
        public ImageSource AnimationImg6 { get; set; }

        public ImageSource Image { get; set; }
        public Dictionary<string, string> ListFinal { get; set; }
        public string Title { get; set; }

        public ObservableCollection<InstructionExercises> Instruction
        {
            get { return instruction; }
        }

        public FinalResults(string exerciseID, Dictionary<string, string> _ListFinal)
        {
            this.ListFinal = _ListFinal;
            Title = "Final Result";
            imageSource.Name = "";
            finalImages(exerciseID);
        }

        /// <summary>
        /// Setting the each image  of the animation with the respectively final angle of each Joint. 
        /// The total number of joints, from most of the exercises, is two. 
        /// </summary>
        /// <param name="animationImg1 to 6">For getting the Image Path.</param>
        /// <param name="FinalJoint1 to 2"> Add the final angle depending on the image and the Joint</param>
        private void finalImages(string exerciseId)
        {
            switch (exerciseId)
            {
                case "Shoulders":
                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms2.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle1Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Shoulder Right: {0}°", ListFinal["Angle1Joint2"]);
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms3.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle2Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Shoulder Right: {0}°", ListFinal["Angle2Joint2"]);
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms4.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle3Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Shoulder Right: {0}°", ListFinal["Angle3Joint2"]);
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms5.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle4Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Shoulder Right: {0}°", ListFinal["Angle4Joint2"]);
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });
                    break;

                case "Elbows":
                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow2.png";
                    imageSource.FinalJoint1 = String.Format("Elbow Left: {0}°", ListFinal["Angle1Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Elbow Right: {0}°", ListFinal["Angle1Joint2"]);
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow3.png";
                    imageSource.FinalJoint1 = String.Format("Elbow Left: {0}°", ListFinal["Angle2Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Elbow Right: {0}°", ListFinal["Angle2Joint2"]);
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow4.png";
                    imageSource.FinalJoint1 = String.Format("Elbow Left: {0}°", ListFinal["Angle3Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Elbow Right: {0}°", ListFinal["Angle3Joint2"]);
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow5.png";
                    imageSource.FinalJoint1 = String.Format("Elbow Left: {0}°", ListFinal["Angle4Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Elbow Right: {0}°", ListFinal["Angle4Joint2"]);
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow6.png";
                    imageSource.FinalJoint1 = String.Format("Elbow Left: {0}°", ListFinal["Angle5Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Elbow Right: {0}°", ListFinal["Angle5Joint2"]);
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });
                    break;

                case "ShoulderAndLeg":
                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    //Left Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St2.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle1Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Left: {0}°", ListFinal["Angle1Joint2"]);
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    //Left Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St3.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle2Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Left: {0}°", ListFinal["Angle2Joint2"]);
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    //Right Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St5.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Right: {0}°", ListFinal["Angle4Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Right: {0}°", ListFinal["Angle4Joint2"]);
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    //Right Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St6.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Right: {0}°", ListFinal["Angle5Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Right: {0}°", ListFinal["Angle5Joint2"]);
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });
                    break;

                case "FourJoints":
                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    //Left Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St2.png";
                    imageSource.FinalJoint2 = String.Format("Leg Left: {0}°", ListFinal["Angle1Joint2"]);
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint2 = imageSource.FinalJoint2 });

                    //Shoulder Right, Leg Left
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St3.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Right: {0}°", ListFinal["Angle2Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Left: {0}°", ListFinal["Angle2Joint2"]);
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });

                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    //Right Side
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St5.png";
                    imageSource.FinalJoint2 = String.Format("Leg Right: {0}°", ListFinal["Angle4Joint2"]);
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint2 = imageSource.FinalJoint2 });

                    //Shoulder Left, Leg Right
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St6.png";
                    imageSource.FinalJoint1 = String.Format("Shoulder Left: {0}°", ListFinal["Angle5Joint1"]);
                    imageSource.FinalJoint2 = String.Format("Leg Right: {0}°", ListFinal["Angle5Joint2"]);
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1, FinalJoint2 = imageSource.FinalJoint2 });
                    break;

                case "Knees":
                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee2.png";
                    imageSource.FinalJoint1 = String.Format("Knee Right: {0}°", ListFinal["Angle1Joint2"]);
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1 });

                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee3.png";
                    imageSource.FinalJoint1 = String.Format("Knee Right: {0}°", ListFinal["Angle2Joint2"]);
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1 });

                    //Zero Position
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath });

                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee5.png";
                    imageSource.FinalJoint1 = String.Format("Knee Left: {0}°", ListFinal["Angle4Joint1"]);
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1 });

                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee6.png";
                    imageSource.FinalJoint1 = String.Format("Knee Left: {0}°", ListFinal["Angle5Joint1"]);
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { ImagePath = imageSource.ImagePath, FinalJoint1 = imageSource.FinalJoint1 });
                    break;

                default:
                    break;
            }
        }
    }
}
