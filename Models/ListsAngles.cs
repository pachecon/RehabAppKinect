using HealthApp.Common;
using RehabTest5.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Animation;

namespace RehabTest5.Models
{
    /// <summary>
    /// After doing the calculation of the exercises. First all the values of each repetition will be stored (List1 to 10). 
    /// The values will be stored depending on th actual time of the animation and the type of Joint.
    /// Then the maximum or minimum value of each list (List 1 to 10) will be checked and then this values will be store 
    /// in other lists (CycleList1 to 10). 
    /// After the complete exercise has finished, the average of the maximum or minimum values of each CycleList will be calculated
    /// and stored in the final List (ListFinal). This is a dictionary and this list will be use to showed the data in the 
    /// User Interface and for the Physician.
    /// </summary>
    public class ListsAngles : BindableBase, IFourJoints
    {
        //List for saving the values of the Angles of two Joints depending on the actual time of the animation.
        private List<double> list1Joint1 = new List<double>();
        private List<double> list1Joint2 = new List<double>();
        private List<double> list2Joint1 = new List<double>();
        private List<double> list2Joint2 = new List<double>();
        private List<double> list3Joint1 = new List<double>();
        private List<double> list3Joint2 = new List<double>();
        private List<double> list4Joint1 = new List<double>();
        private List<double> list4Joint2 = new List<double>();
        private List<double> list5Joint1 = new List<double>();
        private List<double> list5Joint2 = new List<double>();

        //List for saving the maximum or minimum values of the Angles of two Joints after finishing the cycle of the animation.
        //Cycle is the repetition of the Animation. For example 1 cycle = 1 repetition, 2 cycles = 2 repetitions, so on.
        private List<double> cycleList1 = new List<double>();
        private List<double> cycleList2 = new List<double>();
        private List<double> cycleList3 = new List<double>();
        private List<double> cycleList4 = new List<double>();
        private List<double> cycleList5 = new List<double>();
        private List<double> cycleList6 = new List<double>();
        private List<double> cycleList7 = new List<double>();
        private List<double> cycleList8 = new List<double>();
        private List<double> cycleList9 = new List<double>();
        private List<double> cycleList10 = new List<double>();

        private bool firstTime;

        private Dictionary<string, string> listFinal = new Dictionary<string, string>();

        public double DegreeJoint3 { get; set; }
        public double DegreeJoint4 { get; set; }
        public double AnimationCycle { get; set; }
        public bool ExerciseFinish { get; set; }
        public double Repetition { get; set; }
        public TimeSpan ActualTime { get; set; }

        //The Final List to show it to the doctor and the user.
        public Dictionary<string, string> ListFinal
        {
            get { return listFinal; }
            set { listFinal = value; }
        }

        #region PropertyListsForSavingAngles

        public List<double> List1
        {
            get { return list1Joint1; }
            set { list1Joint1 = value; }
        }

        public List<double> List2
        {
            get { return list1Joint2; }
            set { list1Joint2 = value; }
        }

        public List<double> List3
        {
            get { return list2Joint1; }
            set { list2Joint1 = value; }
        }

        public List<double> List4
        {
            get { return list2Joint2; }
            set { list2Joint2 = value; }
        }

        public List<double> List5
        {
            get { return list3Joint1; }
            set { list3Joint1 = value; }
        }

        public List<double> List6
        {
            get { return list3Joint2; }
            set { list3Joint2 = value; }
        }

        public List<double> List7
        {
            get { return list4Joint1; }
            set { list4Joint1 = value; }
        }

        public List<double> List8
        {
            get { return list4Joint2; }
            set { list4Joint2 = value; }
        }

        public List<double> List9
        {
            get { return list5Joint1; }
            set { list5Joint1 = value; }
        }

        public List<double> List10
        {
            get { return list5Joint2; }
            set { list5Joint2 = value; }
        }
        #endregion

        #region PropertyListsForSavingMaximumAngles
        public List<double> CycleList1
        {
            get { return cycleList1; }
            set { cycleList1 = value; }
        }

        public List<double> CycleList2
        {
            get { return cycleList2; }
            set { cycleList2 = value; }
        }

        public List<double> CycleList3
        {
            get { return cycleList3; }
            set { cycleList3 = value; }
        }

        public List<double> CycleList4
        {
            get { return cycleList4; }
            set { cycleList4 = value; }
        }

        public List<double> CycleList5
        {
            get { return cycleList5; }
            set { cycleList5 = value; }
        }

        public List<double> CycleList6
        {
            get { return cycleList6; }
            set { cycleList6 = value; }
        }

        public List<double> CycleList7
        {
            get { return cycleList7; }
            set { cycleList7 = value; }
        }

        public List<double> CycleList8
        {
            get { return cycleList8; }
            set { cycleList8 = value; }
        }

        public List<double> CycleList9
        {
            get { return cycleList9; }
            set { cycleList9 = value; }
        }

        public List<double> CycleList10
        {
            get { return cycleList10; }
            set { cycleList10 = value; }
        }
        #endregion

        public ListsAngles()
        {

        }

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
        /// All the values calculated will be stored in a list depending on the Joint (Joint 1 or Joint 2) and on the actual time
        /// of the animation. After storing and after finishing one repetition. The maximum or minimum values of each list will
        /// be checked and store in another list (CycleList). After the complete excercise has already finished, the average of all 
        /// the maximum or minimum values form the CycleList will be calculated and save as the FinalList.
        /// </summary>
        internal void ControlOfData(TimeSpan duration, double jointAngle1, double jointAngle2, bool fourJoints, string exerciseId)
        {
            ActualTime = duration;
            //Image 1 no because it is the reference.
            //Image 2
            if (duration.Seconds >= (int)AnimationTime.AnimTime1 && duration.Seconds <= (int)AnimationTime.AnimTime2)
            {
                list1Joint1.Add(jointAngle1);
                list1Joint2.Add(jointAngle2);
            }

            //Image 3
            if (duration.Seconds >= (int)AnimationTime.AnimTime3 && duration.Seconds <= (int)AnimationTime.AnimTime4)
            {
                list2Joint1.Add(jointAngle1);
                list2Joint2.Add(jointAngle2);
            }

            //Image 4
            if (duration.Seconds >= (int)AnimationTime.AnimTime5 && duration.Seconds <= (int)AnimationTime.AnimTime6)
            {
                list3Joint1.Add(jointAngle1);
                list3Joint2.Add(jointAngle2);
            }

            //Image 5
            if (duration.Seconds >= (int)AnimationTime.AnimTime7 && duration.Seconds <= (int)AnimationTime.AnimTime8)
            {
                if (fourJoints)
                {
                    list4Joint1.Add(DegreeJoint3);
                    list4Joint2.Add(DegreeJoint4);
                }
                else
                {
                    list4Joint1.Add(jointAngle1);
                    list4Joint2.Add(jointAngle2);
                }
            }

            //Image 6
            if (duration.Seconds >= (int)AnimationTime.AnimTime9 && duration.Seconds <= (int)AnimationTime.AnimTime10)
            {
                if (fourJoints)
                {
                    list5Joint1.Add(DegreeJoint3);
                    list5Joint2.Add(DegreeJoint4);
                }
                else
                {
                    list5Joint1.Add(jointAngle1);
                    list5Joint2.Add(jointAngle2);
                }
            }

            if (duration.Seconds == (int)AnimationTime.AnimTime10)
                ++AnimationCycle;

            CoutingCycles(exerciseId);
            EndOfExercise();
        }

        //For checking if the animation is in the last image of one repetition. If yes, the software will 
        //check and save the maximum or minimum Value of the List. This value will represent the desire value of the angle.
        private void CoutingCycles(string exerciseId)
        {
            //if (ActualTime.Seconds >= 27 && ActualTime.Seconds < 28)
            if (ActualTime.Seconds == 27 && !firstTime)
            {
                CheckingValues(exerciseId);
                firstTime = true;
            }
            else
                if (ActualTime.Seconds == 26)
                {
                    firstTime = false;
                }
        }

        /// <summary>
        /// It is used to specify if the image the maximum value of the Joints is calculated or the minimum value.
        /// </summary>
       private void CheckingValues(string exerciseId)
        {
            switch (exerciseId)
            {
                case "Elbows":
                case "Knees":
                cycleList1.Add(List1.Min()); //Image 2 Joint1
                cycleList2.Add(List2.Min()); //Image 2 Joint2
                cycleList5.Add(List5.Max()); //Image 4 Joint1
                cycleList6.Add(List6.Max()); //Image 4 Joint2
                cycleList7.Add(List7.Min()); //Image 5 Joint1
                cycleList8.Add(List8.Min()); //Image 5 Joint2
                MaximumValue();
                break;

                case "Shoulders":
                cycleList1.Add(List1.Max()); //Image 2 Joint1
                cycleList2.Add(List2.Max()); //Image 2 Joint2
                cycleList5.Add(List5.Min()); //Image 4 Joint1
                cycleList6.Add(List6.Min()); //Image 4 Joint2
                cycleList7.Add(List7.Min()); //Image 5 Joint1
                cycleList8.Add(List8.Min()); //Image 5 Joint2
                MaximumValue();
                    break;
                default:
                cycleList1.Add(List1.Max()); //Image 2 Joint1
                cycleList2.Add(List2.Max()); //Image 2 Joint2
                cycleList5.Add(List5.Max()); //Image 4 Joint1
                cycleList6.Add(List6.Max()); //Image 4 Joint2
                cycleList7.Add(List7.Max()); //Image 5 Joint1
                cycleList8.Add(List8.Max()); //Image 5 Joint2
                    MaximumValue();
                    break;
            }
 
        }

       /// <summary>
       /// In this images, all the exercises are calculating the maximum value of the angle of both Joints.
       /// </summary>
       private void MaximumValue()
       {
           //Image 1 is the reference
           cycleList3.Add(List3.Max()); //Image 3 Joint1
           cycleList4.Add(List4.Max()); //Image 3 Joint2
           cycleList9.Add(List9.Max()); //Image 6 Joint1
           cycleList10.Add(List10.Max()); //Image 6 Joint2 
           ClearList();
       }
        
        //For deleting the information of the last repetition and to re use the List for the new repetition.
        private void ClearList()
        {
            List1.Clear();
            List2.Clear();
            List3.Clear();
            List4.Clear();
            List5.Clear();
            List6.Clear();
            List7.Clear();
            List8.Clear();
            List9.Clear();
            List10.Clear();
        }

        //For checking if the exercise has finished. If yes to get the average of the angles.
        private void EndOfExercise()
        {
            if (Repetition == AnimationCycle)
            {
                FinalList();
                ExerciseFinish = true;
            }
        }

        //For calculating the average of the maximum or minimum value of each image of each repetition.
        private void FinalList()
        {
            string list = CalculatingAverage(CycleList1);
            listFinal.Add("Angle1Joint1", list);
            list = CalculatingAverage(CycleList2);
            listFinal.Add("Angle1Joint2", list);
            list = CalculatingAverage(CycleList3);
            listFinal.Add("Angle2Joint1", list);
            list = CalculatingAverage(CycleList4);
            listFinal.Add("Angle2Joint2", list);
            list = CalculatingAverage(CycleList5);
            listFinal.Add("Angle3Joint1", list);
            list = CalculatingAverage(CycleList6);
            listFinal.Add("Angle3Joint2", list);
            list = CalculatingAverage(CycleList7);
            listFinal.Add("Angle4Joint1", list);
            list = CalculatingAverage(CycleList8);
            listFinal.Add("Angle4Joint2", list);
            list = CalculatingAverage(CycleList9);
            listFinal.Add("Angle5Joint1", list);
            list = CalculatingAverage(CycleList10);
            listFinal.Add("Angle5Joint2", list);
            ListFinal = listFinal;
        }

        private string CalculatingAverage(List<double> list)
        {
            try
            {
                double lista = list.Average();
                return System.Convert.ToString(Math.Truncate(lista));
            }
            catch (NullReferenceException)
            {
                return "Empty List";
            }
        }
    }
}
