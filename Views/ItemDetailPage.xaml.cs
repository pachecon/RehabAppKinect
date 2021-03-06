namespace RehabTest5
{
    using Activiti;
    using Coding4Fun.Kinect.KinectService.WinRTClient;
    using HealthApp;
    using HealthApp.Common;
    using HealthApp.Exercise;
    using RehabTest5.Data;
    using RehabTest5.Interfaces;
    using RehabTest5.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;
    using Windows.Foundation;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Media.Imaging;
    using Windows.UI.Xaml.Shapes;

    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to
    /// flip through other items belonging to the same group.
    /// </summary>

    public sealed partial class ItemDetailPage : LayoutAwarePage
    {
        #region GlobalVariables
        private readonly DepthClient depthClient; //For getting the depth information from the Kinect.
        private readonly SkeletonClient skClient; //Get the skeleton information from the Kinect.
        private WriteableBitmap dBitmap = new WriteableBitmap(1, 1);
        private GetSkeleton skeletonData = new GetSkeleton(); //For showing the skeleton depending on the size of the window.

        private string exerciseId; //It is the Unique ID for each exercise.
        private bool animation; //To start the animation.
        private bool positionReady; //The user is ready when he is in front of the Kinect. 
        
        private ExerciseDetailVM exercise = new ExerciseDetailVM(); //For starting the specific exercise depending on its Unique ID.
        private InitializePosture userReady = new InitializePosture(); //For detecting if the user is in front of the Kinect.
        private ListsAnglesVM listJoints = new ListsAnglesVM(); //For getting the value of the lists of the Angles Measurement.


        private double repetitions = 6.0; //Repeat Behavior of the animation. Number of times the animation will be started again.
        private double speedAnimation = 1.5; //Speed of Ratio of the animation.
        private int taskId = -1;
        
        private bool flagTrainingCycle; //It is used as a flag for checking if Training Cycle has finished.
        private IList<Skeleton> skeletonTrack;
        
        //It is used as a flag to check if the animation has finished and if the Average of the Angles is completed
        public bool ExerciseFinished { get; set; }

        public double SpeedAnimation
        {
            get { return speedAnimation ; }
            set { speedAnimation = value; }
        }
        
        public double Repetitions
        {
            get { return repetitions ; }
            set { repetitions = value; }
        }

        //For getting the Final List of the Average Value of the Angle of each Joint depending on 
        //the specific Image of the Animation.
        public Dictionary<string, string> ListFinal { get; set; }
        #endregion

        /// <summary>
        /// DepthClient and SkeletonClient:
        /// From Library "Coding4Fun.Kinect.KinectService.WinRTClient" 
        /// to communicate Kinect with Windows 8 store application.
        /// </summary>
        /// <param name="depthFrameReady"> Event that fires when a new depth 
        /// frame is available in the DepthStream. </param>
        /// <param name="skeletonFrameReady"> Event that fires when a new 
        /// skeleton frame is available in the SkeletonStream. </param>
        public ItemDetailPage()
        {
          this.InitializeComponent();
          depthClient = new DepthClient();
          depthClient.DepthFrameReady += depthFrameReady;
          skClient = new SkeletonClient();
          skClient.SkeletonFrameReady += skeletonFrameReady;
          CheckActivitiTasks();
        }

        public async System.Threading.Tasks.Task CheckActivitiTasks()
        {
            ExerciseThing et = new ExerciseThing();
            Boolean success = await et.GetTask();
            repetitions = et.Repetition;
            speedAnimation = et.Speed;
            taskId = et.TaskId;
            return;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
           // Allow saved page state to override the initial item to display
            if (pageState != null && pageState.ContainsKey("SelectedItem"))
            {
                navigationParameter = pageState["SelectedItem"];
            }

            startCommunication(navigationParameter);
            var item = ResourceDataSource.GetItem((String)navigationParameter);
            this.DefaultViewModel["Group"] = item.Group;
            this.DefaultViewModel["Items"] = item.Group.Items;
            pageTitle.Text = item.Group.ToString();

            exerciseId = item.UniqueId;
            ItemDetailVM start = new ItemDetailVM(exerciseId);
            start.InstructionDialog();
            animation = true;
            exercise.TitleExercise(exerciseId);
            AngleTitle1.Text = exercise.TitleJoint1;
            AngleTitle2.Text = exercise.TitleJoint2;
            AngleTitle3.Text = exercise.TitleJoint3;
            AngleTitle4.Text = exercise.TitleJoint4;
        }
        
        /// <summary>
        /// It is used to compare the duration time of the animation and the number of times of the animation
        /// for a repetition of 3 times, which it is the Trainging Cycle. After this training the speed of the 
        /// animation will increase.
        /// </summary>
        /// <param name="TrainingCycle">The number of times the animation will be repeated for the Training Cycle</param>
        /// <param name="TimeFourJoints">The time when the animation is 15 seconds. It is used when four joints are 
        /// measured. For example, before this time the angle of the Joints of the Left side will be displayed on the screen and
        /// after this time only The Angles of the Joints of the Rigth side will be displayed on the User Interface. </param>
        public enum AnimationTime
        {
            TrainingCycle = 2,
            TimeFourJoints = 15,
        }

        #region KinectandSkeletonTracking      
        /// <summary>
        ///  To connect Kinect with Windows 8 store application. 
        ///  <param name="127.0.0.1">Local Address</param>
        ///  <param name="2222">Port number for the skeleton Tracking</param>
        ///  <param name="2221">Port number for the depthFrame</param>
        ///  If it is not connected, an error will be showed.
        /// </summary>
        private async void startCommunication(object sender)
        {
            try
            {
                if (!depthClient.IsConnected && !skClient.IsConnected)
                {
                    await skClient.Connect("127.0.0.1", 2222);
                    await depthClient.Connect("127.0.0.1", 2221);
                }
                else
                {
                    depthClient.Disconnect();
                    skClient.Disconnect();
                }
            }
            catch (Exception ex)
            {
                new MessageDialog(ex.Message).ShowAsync();
            }
        }

        /// <summary>
        /// For getting a BitmapSource object that represents the depth image.
        /// <param name="dBitmap">Provides a BitmapSource depending on the size of the ImageFrame</param>
        /// </summary>
        private void depthFrameReady(object sender, DepthFrameData e)
        {
            if (dBitmap == null || dBitmap.PixelWidth != e.ImageFrame.Width || dBitmap.PixelHeight != e.ImageFrame.Height)
            {
                dBitmap = new WriteableBitmap(e.ImageFrame.Width, e.ImageFrame.Height);
                imgDepth.Source = this.dBitmap;
            }
            byte[] convertedDepthBits = skeletonData.ConvertDepthFrame(e.DepthData.ToArray(), e);

            InMemoryRandomAccessStream dStream = new InMemoryRandomAccessStream();
            DataWriter dWriter = new DataWriter(dStream.GetOutputStreamAt(0));
            dWriter.WriteBytes(convertedDepthBits);

            Stream s = dBitmap.PixelBuffer.AsStream();
            s.Write(convertedDepthBits, 0, convertedDepthBits.Length);
            dBitmap.Invalidate();
        }
             
        /// <summary>
        /// For Skeleton Tracking and drawing the skeleton.
        /// </summary>
        /// <param name="skeletons"> Stablish only one skeleton (Patient) </param>
        private void skeletonFrameReady(object sender, SkeletonFrameData e)
        {
            skeletonTrack = e.Skeletons;

           //Skeleton[] skeletons = new Skeleton[skeletonCount] where skeletonCount =1 for tracking only one user.
           Skeleton[] skeletons = new Skeleton[1];
           Skeleton skeleton = (from s in e.Skeletons
                        where s.TrackingState == SkeletonTrackingState.Tracked
                        select s).FirstOrDefault();

           if (positionReady && skeleton != null) //For verifying if the user has been tracked and if he is infront of the Kinect
            {
                if (!flagTrainingCycle && animation)
                {
                    //The Animation start with the defaul speed of Ratio of 1. 
                    ImageAnimation();
                    Animation1.RepeatBehavior = new RepeatBehavior((int)AnimationTime.TrainingCycle);
                    Animation1.Begin();
                    animation = false;
                    countingRepetitions.Text = "Training Cycle";
                }
                else if (animation)
                {
                    //For increasing the Speed of Ratio of the animation after the Training Cycle. 
                    Animation1.RepeatBehavior = new RepeatBehavior(repetitions);
                    Animation1.SpeedRatio = speedAnimation;
                    Animation1.BeginTime = TimeSpan.FromSeconds(0);
                    Animation1.Begin();
                    animation = false;
                } 
               DisplayInformationAngles(skeleton);
            }

          if (skeleton == null)
            {
                return;
            }
            else
                {
                    SkeletonCanvas.Children.Clear();
                    SetEllipsePosition(headEllipse, skeleton.Joints[(int)JointType.Head]);
                    SetEllipsePosition(shoulderEllipse, skeleton.Joints[(int)JointType.ShoulderCenter]);
                    SetEllipsePosition(spineEllipse, skeleton.Joints[(int)JointType.Spine]);
                    SetEllipsePosition(lHandEllipse, skeleton.Joints[(int)JointType.HandLeft]);
                    SetEllipsePosition(rHandEllipse, skeleton.Joints[(int)JointType.HandRight]);
                    SetEllipsePosition(rShoulderEllipse, skeleton.Joints[(int)JointType.ShoulderRight]);
                    SetEllipsePosition(lShoulderEllipse, skeleton.Joints[(int)JointType.ShoulderLeft]);
                    SetEllipsePosition(rElbowEllipse, skeleton.Joints[(int)JointType.ElbowRight]);
                    SetEllipsePosition(lElbowEllipse, skeleton.Joints[(int)JointType.ElbowLeft]);
                    SetEllipsePosition(rWristEllipse, skeleton.Joints[(int)JointType.WristRight]);
                    SetEllipsePosition(lWristEllipse, skeleton.Joints[(int)JointType.WristLeft]);
                    SetEllipsePosition(rHipEllipse, skeleton.Joints[(int)JointType.HipRight]);
                    SetEllipsePosition(lHipEllipse, skeleton.Joints[(int)JointType.HipLeft]);
                    SetEllipsePosition(rKneeEllipse, skeleton.Joints[(int)JointType.KneeRight]);
                    SetEllipsePosition(lKneeEllipse, skeleton.Joints[(int)JointType.KneeLeft]);
                    SetEllipsePosition(rAnkleEllipse, skeleton.Joints[(int)JointType.AnkleRight]);
                    SetEllipsePosition(lAnkleEllipse, skeleton.Joints[(int)JointType.AnkleLeft]);
                    SetEllipsePosition(hipEllipse, skeleton.Joints[(int)JointType.HipCenter]);
                    SetEllipsePosition(rFootEllipse, skeleton.Joints[(int)JointType.FootRight]);
                    SetEllipsePosition(lFootEllipse, skeleton.Joints[(int)JointType.FootLeft]);
                    drawBone(skeleton.Joints[(int)JointType.Head], skeleton.Joints[(int)JointType.ShoulderCenter]);
                    drawBone(skeleton.Joints[(int)JointType.ShoulderCenter], skeleton.Joints[(int)JointType.Spine]);
                    drawBone(skeleton.Joints[(int)JointType.Spine], skeleton.Joints[(int)JointType.HipCenter]);
                    drawBone(skeleton.Joints[(int)JointType.ShoulderCenter], skeleton.Joints[(int)JointType.ShoulderRight]);
                    drawBone(skeleton.Joints[(int)JointType.ShoulderRight], skeleton.Joints[(int)JointType.ElbowRight]);
                    drawBone(skeleton.Joints[(int)JointType.ElbowRight], skeleton.Joints[(int)JointType.WristRight]);
                    drawBone(skeleton.Joints[(int)JointType.WristRight], skeleton.Joints[(int)JointType.HandRight]);
                    drawBone(skeleton.Joints[(int)JointType.HipCenter], skeleton.Joints[(int)JointType.HipRight]);
                    drawBone(skeleton.Joints[(int)JointType.HipRight], skeleton.Joints[(int)JointType.KneeRight]);
                    drawBone(skeleton.Joints[(int)JointType.KneeRight], skeleton.Joints[(int)JointType.AnkleRight]);
                    drawBone(skeleton.Joints[(int)JointType.AnkleRight], skeleton.Joints[(int)JointType.FootRight]);
                    drawBone(skeleton.Joints[(int)JointType.ShoulderCenter], skeleton.Joints[(int)JointType.ShoulderLeft]);
                    drawBone(skeleton.Joints[(int)JointType.ShoulderLeft], skeleton.Joints[(int)JointType.ElbowLeft]);
                    drawBone(skeleton.Joints[(int)JointType.ElbowLeft], skeleton.Joints[(int)JointType.WristLeft]);
                    drawBone(skeleton.Joints[(int)JointType.WristLeft], skeleton.Joints[(int)JointType.HandLeft]);
                    drawBone(skeleton.Joints[(int)JointType.HipCenter], skeleton.Joints[(int)JointType.HipLeft]);
                    drawBone(skeleton.Joints[(int)JointType.HipLeft], skeleton.Joints[(int)JointType.KneeLeft]);
                    drawBone(skeleton.Joints[(int)JointType.KneeLeft], skeleton.Joints[(int)JointType.AnkleLeft]);
                    drawBone(skeleton.Joints[(int)JointType.AnkleLeft], skeleton.Joints[(int)JointType.FootLeft]);
                    positionReady = userReady.UserTowardsSensor(skeleton);    
          }
        }

        /// <summary>
        /// For drawing the line of the skeleton.
        /// </summary>
        private void drawBone(Joint joint1, Joint joint2)
        {
            Line skeletonBone = new Line();

            skeletonBone.X1 = joint1.Position.X;
            skeletonBone.Y1 = joint1.Position.Y;
            skeletonBone.X2 = joint2.Position.X;
            skeletonBone.Y2 = joint2.Position.Y;
            skeletonBone.Stroke = new SolidColorBrush(Colors.Black);
            skeletonBone.StrokeThickness = 3;
            SkeletonCanvas.Children.Add(skeletonBone);
        }

        /// <summary>
        ///This method is used to position the ellipses on the canvas
        ///according to correct movements of the tracked joints.
        ///It also converts the value to X/Y and scale to width/height of window
        ///IMPORTANT NOTE: Code for vector scaling was imported from the Coding4Fun Kinect Toolkit
        ///available here: http://c4fkinect.codeplex.com/
        /// </summary>
        /// <param name="joint">The joint to scale.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        private void SetEllipsePosition(FrameworkElement ellipse, Joint joint)
        {
            var scaledJoint = skeletonData.ScaleTo(joint, (int)Skeleton.Width, (int)Skeleton.Height, 1.0f, 1.0f);
            
            Canvas.SetLeft(ellipse, scaledJoint.Position.X);
            Canvas.SetTop(ellipse, scaledJoint.Position.Y);
        }
        #endregion
        
        /// <summary>
        /// Load the images and start the animation.
        /// </summary>
        /// <param name="exerciseId">Specific Id depending on the exercise</param>
        /// <param name="imageAn">For getting the specific image for the animation</param>
        /// <param name="Image1 to 6">Name of the specific Image.</param>
        private void ImageAnimation()
        {
            InstructionsVM imageAn = new InstructionsVM(exerciseId);
            Image1.Source = imageAn.AnimationImg1;
            Image2.Source = imageAn.AnimationImg2;
            Image3.Source = imageAn.AnimationImg3;
            Image4.Source = imageAn.AnimationImg4;
            Image5.Source = imageAn.AnimationImg5;
            Image6.Source = imageAn.AnimationImg6;
   
            freeze.Visibility = Visibility.Collapsed;
        }
        
        /// <summary>
        /// Display the information of the angles on the GUI. 
        /// </summary>
        /// <param name="duratin">For getting the actual time of the animation. This will be used to
        /// save the value of the angle of the joints during depending on spefic period of time</param>
        /// <param name="exercise.Skeleton">To save the value of the skeleton tracked by the Kinect</param>
        /// <param name="exercise.typeOfExercise">For loading the specific exercise</param>
        /// <param name="listJoints.FourJoints">It will be used as a flag to specifiy that the exercise
        /// is inolving four Joints. Then it will be used for saving the data of the angles.</param>
        /// <param name="listJoints.SaveDataJoints">For setting the value of the Joint depending on the animation time.</param> 
        /// <param name="CountingRemainingRepetitions">For decreasing the number of repetition after finishing one cycle.</param>
        private void DisplayInformationAngles(Skeleton skeleton)
        {
            var actualTime = Animation1.GetCurrentTime();

            exercise.Skeleton = skeleton;
            exercise.TypeOfExercise(exerciseId);

            if (exerciseId == "FourJoints" || exerciseId == "ShoulderAndLeg")
            {
                listJoints.FourJoints = true;
                FourJointsDetection(actualTime);
            }
            else
            {
                listJoints.DegreeJoint1 = DetectionOfJoints(actualTime, exercise.DegreeJoint1, JointAngle1);
                listJoints.DegreeJoint2 = DetectionOfJoints(actualTime, exercise.DegreeJoint2, JointAngle2);
            }

            listJoints.SaveDataJoints(actualTime, repetitions, exerciseId);
            CountingRemainingRepetitions();
            IncreaseSpeedAnimation();
            EndOfExercise();
        }
        
        /// <summary>
        /// Display the information of the angles of only two joints.
        /// </summary>
        private double DetectionOfJoints(TimeSpan duration, double jointAngle, TextBlock textAngle)
        {
            textAngle.Text = String.Format("{0}", Math.Truncate(jointAngle));
            ExercisesStyleVM angleColor = new ExercisesStyleVM(exerciseId, duration, jointAngle);
            textAngle.Style = angleColor.FontAngleStyle;
            return jointAngle;
        }

        /// <summary>
        /// Display the information of the angles for more joints.
        /// </summary>
        private void FourJointsDetection(TimeSpan duration)
        {
            if (duration.Seconds <= (int)AnimationTime.TimeFourJoints)
            {
                listJoints.DegreeJoint1 = DetectionOfJoints(duration, exercise.DegreeJoint1, JointAngle1);
                listJoints.DegreeJoint2 = DetectionOfJoints(duration, exercise.DegreeJoint2, JointAngle2);
            }

            else
            {
                listJoints.DegreeJoint3 = DetectionOfJoints(duration, exercise.DegreeJoint3, JointAngle3);
                listJoints.DegreeJoint4 = DetectionOfJoints(duration, exercise.DegreeJoint4, JointAngle4);
            }
        }

        /// <summary>
        /// For displaying the number of remaining repetition, after the end of the Training Cycle.
        /// </summary>
        private void CountingRemainingRepetitions()
        {
            if (flagTrainingCycle)
            {
                countingRepetitions.Text = "Remaining repetitions: " + listJoints.RemainingRepetitions();
            }
        }

        /// <summary>
        /// For increasing the speed of the Animation. After the Training cycle.
        /// </summary>
        private void IncreaseSpeedAnimation()
        {
            if (!flagTrainingCycle && listJoints.RepeatCycle == (int)AnimationTime.TrainingCycle)
            {
                animation = true;
                flagTrainingCycle = true;
                Animation1.SkipToFill();
                Animation1.Stop();
            }
        }

        /// <summary>
        /// Display the final results of the exercises and to disconnect the application with the Kinect
        /// </summary>
        /// <param name="FinalFinish">To check if the animation has finished the last cycle.</param>
        /// <param name="ListFinal">Get the final angle of each joint.</param>
        /// <param name="start.FinalResultDialog">Open a window with the images and their respective angle</param>
        /// <param name="freeze.Visibility">Show the first image after the end of the animation</param>
        /// <param name="countingRepetition">Get the final angle of each joint.</param>
        /// <param name="ListFinal">Get the final angle of each joint.</param>
        private void EndOfExercise()
        {
            ExerciseFinished = listJoints.ExerciseFinish;
             //It is for checking if the exercises has been completed. If it is true a new window with the final
                //resutls will be loaded.
                if (ExerciseFinished)
                {
                    Animation1.SkipToFill();
                    skClient.Disconnect();
                    freeze.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    ListFinal = listJoints.ListFinal;
                    ItemDetailVM start = new ItemDetailVM(exerciseId);
                    start.FinalResultDialog(ListFinal);
                    if (taskId > 0)
                    {
                        SaveExerciseAngels();
                    }
                    //var a1j1 = ListFinal["Angle1Joint1"];
                }

        }

        private async void SaveExerciseAngels()
        {
            ExerciseResult exercise = new ExerciseResult();
            exercise.angel1Joint1 = Convert.ToInt32(ListFinal["Angle1Joint1"]);
            exercise.angel1Joint2 = Convert.ToInt32(ListFinal["Angle1Joint2"]);

            exercise.angel2Joint1 = Convert.ToInt32(ListFinal["Angle2Joint1"]);
            exercise.angel2Joint2 = Convert.ToInt32(ListFinal["Angle2Joint2"]);

            exercise.angel3Joint1 = Convert.ToInt32(ListFinal["Angle3Joint1"]);
            exercise.angel3Joint2 = Convert.ToInt32(ListFinal["Angle3Joint2"]);

            exercise.angel4Joint1 = Convert.ToInt32(ListFinal["Angle4Joint1"]);
            exercise.angel4Joint2 = Convert.ToInt32(ListFinal["Angle4Joint2"]);

            exercise.angel5Joint1 = Convert.ToInt32(ListFinal["Angle5Joint1"]);
            exercise.angel5Joint2 = Convert.ToInt32(ListFinal["Angle5Joint2"]);

            Configuration configuration = new Configuration();
            ActivitiControl activiti = new ActivitiControl(configuration["activiti"], configuration["activitiUser"], configuration["activitiPass"]);
            Boolean success = await activiti.login();
            if (success)
            {
                await activiti.commitExerciseTask(taskId, exercise);
            }
        }

        /// <summary>
        /// Open the page of the instructions depending on the type of exercise.
        /// </summary>
        /// <param name="exerciseId">Specific Id depending on the exercise.</param>
        /// <param name="OpenDialog_Executed">Method of the class Instruction.</param>
        private void Instructions(object sender, RoutedEventArgs e)
        {
            ItemDetailVM start = new ItemDetailVM(exerciseId);
            start.InstructionDialog();
        }

        /// <summary>
        /// For not receiving more information from the Kinect and delete the information
        /// saved regarding the skeleton and the exercises
        /// </summary>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            //Animation1.SkipToFill();
            Animation1.Stop();
            //Animation1.Children.Clear();
            //skClient.Disconnect();
            depthClient.Disconnect();
            skeletonTrack = null;
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }

        private void buttonClick(object sender, RoutedEventArgs e)
        {
            GoBack(sender, e);
        }
    }
}