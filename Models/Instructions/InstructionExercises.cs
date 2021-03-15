namespace RehabTest5.Models
{
    using HealthApp.Common;
    using System;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// This is for the PopUp. The first time that the PopUp will open, the instruction information will be displayed.
    /// The second time that it will be presented, the information of the Final Results will be displayed.
    /// </summary>
    /// <param name="Name">Property of the name of each image for the Instruction</param>
    /// <param name="Image">Property to loading each image depending on the path. This is for the Instruction and 
    /// for the FinalResult popup</param>
    /// <param name="FinalJoint1 to 2">Property of the value of the angles of each Joint. This information
    /// is for the FinalResult popup</param>
 
    public class InstructionExercises : BindableBase
    {
        private string name;
        private string finalJoint1;
        private string finalJoint2;
        public string ImagePath { get; set; }

        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref name, value);}
        }
        
        public string FinalJoint1
        {
            get { return finalJoint1; }
            set { this.SetProperty(ref finalJoint1, value); }
        }

        public string FinalJoint2
        {
            get { return finalJoint2; }
            set { this.SetProperty(ref finalJoint2, value); }
        }

        public ImageSource Image
        {
            get
            {
                return new BitmapImage(new Uri("ms-appx:///" + this.ImagePath));
            }
        }
    }
}
