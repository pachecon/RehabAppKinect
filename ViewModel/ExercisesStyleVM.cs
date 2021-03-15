namespace RehabTest5
{
    using HealthApp.Common;
   // using RehabTest5.Common;
    using RehabTest5.Interfaces;
    using RehabTest5.Models;
    using System;
    using Windows.UI;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// For setting the style to the values of the angles. It will compare the value of the angle
    /// with the goal. 
    /// Each image has its own goal, therefore the comparison will also depend on the actual time of the animation.
    /// </summary>
    public class ExercisesStyleVM : BindableBase
    {
        private Style fontAngleStyle;
        public Style FontAngleStyle
        {
            get
            {
                return this.fontAngleStyle;
            }
            set
            {
                if (value == this.fontAngleStyle) return;
                this.fontAngleStyle = value;
            }
        }
        public TimeSpan Duration { get; set; }
        public double GoalAngle1 { get; set; }
        public double GoalAngle2 { get; set; }
        public double GoalAngle3 { get; set; }
        public double GoalAngle4 { get; set; }
        public double GoalAngle5 { get; set; }

        public ExercisesStyleVM(string exerciseId, TimeSpan actualTime, double angle)
        {
            FontAngleStyle = Application.Current.Resources["tblAngleStyle"] as Style;
            GoalAngle(exerciseId);
            ControlOfData(actualTime, angle);
        }

        /// <summary>
        /// Each image has a duration time in seconds. The enum is used to stablish the initial time and final time of each image.
        /// </summary>
        public enum AnimationTime
        {
            AnimTime1 = 4,
            AnimTime2 = 9,
            AnimTime3,
            AnimTime4 = 14,
            AnimTime5,
            AnimTime6 = 19,
            AnimTime7,
            AnimTime8 = 25,
            AnimTime9,
            AnimTime10 = 28
        }

        /// <summary>
        /// For getting the goal angle of each exercise and the corresponding image. The first image of the animation
        /// of all exercises is the reference, it is image number 0. Therefore the total goals are 5 (from Image 1 to 5).
        /// </summary>
        /// <param name="GoalAngle1 to 5"> Corresponding goal angle value depending on the image of the animation </param>
        private void GoalAngle(string exerciseId)
        {
            switch (exerciseId)
            {
                case "Shoulders":
                    GoalAngle1 = 90.0;
                    GoalAngle2 = 150.0;
                    GoalAngle3 = 90.0;
                    GoalAngle4 = 20.0;
                    break;

                case "Elbows":
                    GoalAngle2 = 90.0;
                    GoalAngle3 = 140.0;
                    GoalAngle5 = 50.0;
                    break;

                case "ShoulderAndLeg":
                    GoalAngle1 = 20.0;
                    GoalAngle2 = 35.0;
                    GoalAngle4 = 20.0;
                    GoalAngle5 = 35.0;
                    break;

                case "Knees":
                    GoalAngle1 = 5.0;
                    GoalAngle2 = 60.0;
                    GoalAngle4 = 5.0;
                    GoalAngle5 = 60.0;
                    break;

                case "FourJoints":
                    GoalAngle1 = 20.0;
                    GoalAngle2 = 35.0;
                    GoalAngle4 = 20.0;
                    GoalAngle5 = 35.0;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// For setting the style to the values of the angles depending on the actual time of the animation.
        /// </summary>
        /// <param name="jointAngle"> It is the value of the angle of one Joint</param>
        /// <param name="actualTime"> It is the actual time of the animation</param>
        private void ControlOfData (TimeSpan actualTime, double jointAngle)
        {
            //Image 1 no because it is reference. 
            //Image 2
            if (actualTime.Seconds >= (int)AnimationTime.AnimTime1 && actualTime.Seconds <= (int)AnimationTime.AnimTime2)
                ColorChanging(jointAngle, GoalAngle1);

            //Image 3
            if (actualTime.Seconds >= (int)AnimationTime.AnimTime3 && actualTime.Seconds <= (int)AnimationTime.AnimTime4)
                ColorChanging(jointAngle, GoalAngle2);

            //Image 4
            if (actualTime.Seconds >= (int)AnimationTime.AnimTime5 && actualTime.Seconds <= (int)AnimationTime.AnimTime6)
                ColorChanging(jointAngle, GoalAngle3);
 
            //Image 5
            if (actualTime.Seconds >= (int)AnimationTime.AnimTime7 && actualTime.Seconds <= (int)AnimationTime.AnimTime8)
                ColorChanging(jointAngle, GoalAngle4);

            //Image 6
            if (actualTime.Seconds >= (int)AnimationTime.AnimTime9 && actualTime.Seconds <= (int)AnimationTime.AnimTime10)
                ColorChanging(jointAngle, GoalAngle5);
         }

        /// <summary>
        /// For setting the style to the values of the angles. 
        /// If the value ot the angle is equal or higher to the maxValue, the color of the font will be green.
        /// If the value of the angle is higher than the 60% of the maxValue but lower to the maxValue, the color will be yellow
        /// The rest will be red.
        /// </summary>
        /// <param name="maxValue"> It is the value of the GoalAngle of each image</param>
        /// <param name="angle"> It is the value of the angle of each image</param>
        private void ColorChanging(double angle, double maxValue)
        {
            var res = new ResourceDictionary { Source = new Uri("ms-appx:///Common/StandardStyles.xaml", UriKind.Absolute) };

            Style style = res["tblAngleStyle"] as Style;

            if (angle >= maxValue)
            {
                style.Setters.Add(new Setter(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Green)));
            }
            else
                if ((0.60 * maxValue) < angle && angle < maxValue)
            {
              style.Setters.Add(new Setter(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Yellow)));
            }
            
            else
            {
                style.Setters.Add(new Setter(TextBlock.ForegroundProperty, new SolidColorBrush(Colors.Red)));
            }

            FontAngleStyle = style;
        }

    }
}
