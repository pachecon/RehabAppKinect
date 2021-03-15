namespace RehabTest5
{
    using HealthApp.Common;
    //using RehabTest5.Common;
    using RehabTest5.Models;
    using System;
    using System.Collections.ObjectModel;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// This class is used for setting the images for the animation and for the instructions depending on the type of exercise.
    /// </summary>
    /// <param name="AnimationImg1 to 6">Property for each image.</param>
    /// <param name="Instruction">Collection of the images for each exercise.</param>
    /// <param name="exeImages">Method for loading the images depending on the Id of the exercise.</param>
    
 public class InstructionsVM : BindableBase
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
        public string Title { get; set; }

        public ObservableCollection<InstructionExercises> Instruction
        {
            get { return instruction; }
        }
             
        public InstructionsVM(string exerciseID)
        {
          Title = "Instruction";
          exerciseImages(exerciseID);
        }
 
        /// <summary>
        /// Each exercise has different Name(imageSource.Name) and Path (imageSource.ImagePath). 
        /// </summary>
        /// <param name="animationImg1to6">For getting the Image Path.</param>
        /// <param name="instruction"> Add the collection of the images.</param>
      private void exerciseImages(string exerciseId)
        {
           switch (exerciseId)
            {
                case "Shoulders":
                    imageSource.Name = "One";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath});
                   
                    imageSource.Name = "Two";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms2.png";
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Three";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms3.png";
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Four";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Five";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms5.png";
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Six";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel1/Arms6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    break;

                case "Elbows":
                    imageSource.Name = "One";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name , ImagePath =imageSource.ImagePath });
                    
                    imageSource.Name = "Two";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow2.png";
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Three";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow3.png";
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Four";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Five";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow5.png";
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Six";
                    imageSource.ImagePath = @"Assets/Images/upperLimb/upperLevel2/Elbow6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    break;

                case "ShoulderAndLeg":
                    imageSource.Name = "One";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name , ImagePath =imageSource.ImagePath });
                    
                    imageSource.Name = "Two";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St2.png";
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Three";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St3.png";
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Four";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Five";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St5.png";
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Six";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL1/St6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    break;

                case "FourJoints":
                    imageSource.Name = "One";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name , ImagePath =imageSource.ImagePath });
                    
                    imageSource.Name = "Two";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St2.png";
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Three";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St3.png";
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Four";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                   
                    imageSource.Name = "Five";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St5.png";
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Six";
                    imageSource.ImagePath = @"Assets/Images/standing/StandingL2/St6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    break;
 
                case "Knees":
                    imageSource.Name = "One";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee1.png";
                    AnimationImg1 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name , ImagePath =imageSource.ImagePath });
                    
                    imageSource.Name = "Two";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee2.png";
                    AnimationImg2 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Three";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee3.png";
                    AnimationImg3 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Four";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee4.png";
                    AnimationImg4 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                   
                    imageSource.Name = "Five";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee5.png";
                    AnimationImg5 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    
                    imageSource.Name = "Six";
                    imageSource.ImagePath = @"Assets/Images/lowerLimb/LowerLevel1/Knee6.png";
                    AnimationImg6 = imageSource.Image;
                    instruction.Add(new InstructionExercises { Name = imageSource.Name, ImagePath = imageSource.ImagePath });
                    break;
               
               default:
                   break;
           }
        }
  
    }
}
