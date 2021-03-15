namespace RehabTest5
{
    using HealthApp.Common;
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml.Controls.Primitives;

    /// <summary>
    /// Open or close the Popup which represents the Intrusction Page. 
    /// Displays content on top of existing content, within the bounds of the application window.
    /// </summary>
    /// <param name="gameID">Depending on the exercise ID, the specific instruction page will be loaded</param>
    /// <param name="instruction">Loading the ObservableCollection of the Names and Images</param>
    /// <param name="dialog">Loading the Instruction page depending on its Width and Height.</param>
 
    public class ItemDetailVM : BindableBase
    {
        private Popup instPopup;
        private string exerciseID;

        public string ExerciseID
        {
            get { return exerciseID; }
            set { exerciseID = value; }
        }
        

        public ItemDetailVM(string exeId)
        {
            this.exerciseID = exeId;   
        }
                
       public void InstructionDialog()
       {
          InstructionsVM instruction = new InstructionsVM(exerciseID);
          OpenDialog(instruction);
       }

       public void FinalResultDialog(Dictionary<string, string> ListFinal)
       {
          // this.instPopup.IsOpen = false;
           FinalResults result = new FinalResults(exerciseID, ListFinal);
           OpenDialog(result);
       }

       private void OpenDialog(object dataWindow)
       {
           Instructions dialog = new Instructions();
           dialog.DataContext = dataWindow;
           dialog.CloseRequested += Dialog_CloseRequested;
           this.instPopup = new Popup();
           this.instPopup.Child = dialog;
           this.instPopup.IsOpen = true;
       }

        private void Dialog_CloseRequested(object sender, EventArgs e)
        {
            //this.dialog = null;
            //this.instPopup.Child = null;
            this.instPopup.IsOpen = false;
        }
    }
}
