namespace RehabTest5
{
    using HealthApp.Common;
    using System;
    using System.Collections.Generic;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    
    /// <summary>
    /// A page that displays details for the instruction of the exercise.
    /// </summary>
    public sealed partial class Instructions : LayoutAwarePage
    {
        public event EventHandler CloseRequested;

        /// <summary>
        /// Initialize the page.
        /// </summary>
        /// <param name="bounds">Used to stablish the Width and Height of the Page.</param>
        public Instructions()
        {
            this.InitializeComponent();
            var bounds = Window.Current.Bounds;
            this.RootPanel.Width = bounds.Width;
            this.RootPanel.Height = bounds.Height;
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
            if (pageState != null && pageState.ContainsKey("InstructionItem"))
            {
                navigationParameter = pageState["InstructionItem"];
            }
         }

        /// <summary>
        /// Close the instruction window.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.CloseRequested != null)
            {
                this.CloseRequested(this, EventArgs.Empty);
            }
        }
    }
}
