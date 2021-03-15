using HealthApp.Common;
using RehabTest5.Interfaces;
using RehabTest5.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace RehabTest5
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
    
    public class ListsAnglesVM : BindableBase, ITwoJoints, IFourJoints
    {
       private bool endCycleAnimation;
       private double countingTimes;
       
       public TimeSpan ActualTime { get; set; }
       
       public double DegreeJoint1 { get; set; }
       public double DegreeJoint2 { get; set; }
       public double DegreeJoint3 { get; set; }
       public double DegreeJoint4 { get; set; }
       public bool FourJoints { get; set; }
       public double RepeatCycle { get; set; }
       public Dictionary<string, string> ListFinal { get; set; }
       public bool ExerciseFinish { get; set; }

       private ListsAngles listJoints = new ListsAngles();
 
       public ListsAnglesVM()
       {
            
       }
       /// <summary>
       /// This methos is used to give the parameters to the class ListsAngles for saving the data of the angles
       /// depending on the time of the animation (actualTime) and the number of repetition of the cycles.
       /// It will also check if the exercise has finished, if yes it will show a flag and give the final list with
       /// the final results of the averages of each Joint.
       /// </summary>
       /// <param name="ActualTime">To set the actual time of the animation</param>
       /// <param name="countingRepetition">To set the total number of repetitions</param>
       /// <param name="controOfData">To save the value of the angles in their respectively list 
       /// depending on the actual time of the animation</param>
       /// <param name="RepeatCycle">For checking if the animation is in the TrainingCycle</param>
       /// <param name="ExeciseFinish">It is used as a flag to check if the exercise has finished. If yes to present 
       /// the final result </param>
       internal void SaveDataJoints(TimeSpan actualTime, double countingRepetition, string exerciseId)
       {
           this.ActualTime = actualTime;

           if (FourJoints)
           {
               listJoints.DegreeJoint3 = DegreeJoint3;
               listJoints.DegreeJoint4 = DegreeJoint4;
           }
           listJoints.Repetition = countingRepetition;
           listJoints.ControlOfData(actualTime, DegreeJoint1, DegreeJoint2, FourJoints, exerciseId);
           RepeatCycle = listJoints.AnimationCycle;
           ExerciseFinish = listJoints.ExerciseFinish;

           if (ExerciseFinish)
             ListFinal = listJoints.ListFinal;
           
       }

       /// <summary>
       /// For counting the repetitions left.
       /// </summary>
       internal string RemainingRepetitions()
       {
           double x = CountingRepetitions();
           double y = listJoints.Repetition;
           double calculation = y - x;
           return System.Convert.ToString(calculation);
       }

       /// <summary>
       /// At the end of each cycle, the number of repetition will decrease and the information will be presented on the User Interface
       /// </summary>
       private double CountingRepetitions()
       {
           if (ActualTime.Seconds == 27 && !endCycleAnimation)
           {
               ++countingTimes;
               endCycleAnimation = true;
               return countingTimes;
           }
           else
               if (ActualTime.Seconds == 26)
               {
                   endCycleAnimation = false;
                   return countingTimes;
               }
           return countingTimes;
       }
    }
}
