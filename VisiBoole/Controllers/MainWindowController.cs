using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Events;

namespace VisiBoole.Controllers
{
    /// <summary>
    /// The controller for the MainWindow view
    /// </summary>
    public class MainWindowController
    {
        /// <summary>
        /// A handle to the view that this class controls
        /// </summary>
        private IMainWindow View;

        /// <summary>
        /// The Display that is currently being hosted by the MainWindow
        /// </summary>
        private DisplayBase CurrentDisplay;

        /// <summary>
        /// Constants corresponding to the three different display types hosted by the MainWindow
        /// </summary>
        private enum DisplayType
        {
            SINGLE,
            HORIZONTAL,
            VERTICAL
        }

        /// <summary>
        /// All of the Displays - single, horizontal, and vertical - that are hosted by the MainWindow
        /// </summary>
        private Dictionary<DisplayType, DisplayBase> AllDisplays;

        /// <summary>
        /// Constructs an instance of MainWindowController
        /// </summary>
        /// <param name="View">A handle to the view that this class controls</param>
        public MainWindowController(IMainWindow View)
        {
            this.View = View;

            // Initialize our MainWindow displays
            DisplayBase Single = new DisplaySingleEditor();
            DisplayBase Horizontal = new DisplayHorizontal();
            DisplayBase Vertical = new DisplayVertical();

            // Add them to our local Dictionary
            AllDisplays = new Dictionary<DisplayType, DisplayBase>();
            AllDisplays.Add(DisplayType.SINGLE, Single);
            AllDisplays.Add(DisplayType.HORIZONTAL, Horizontal);
            AllDisplays.Add(DisplayType.VERTICAL, Vertical);

            // Load up the Single display
            LoadDisplay(DisplayType.SINGLE);

            // Wire up our event handlers
            View.ProcessNewFile += new ProcessNewFileHandler(View_ProcessNewFile);
        }

        /// <summary>
        /// Constructs, loads, and displays the SubDesign created from the given data
        /// </summary>
        private void View_ProcessNewFile(object sender, ProcessNewFileEventArgs e)
        {
            // Get the filename from OpenFileDialog - Done

            // Create a SubDesign object from the filename - Done
            SubDesign sd = CreateNewSubDesign(e.FileName);

            // Add that SubDesign to our global collection of SubDesigns - Done

            // Create a new TabPage with the SubDesign - Done
            CurrentDisplay.CreateNewTab(sd);

            // Add that TabPage to the TabControl of DisplayBase - Done

            // Change focus to that TabPage - Done
            CurrentDisplay.SelectTabPage(sd.TabPageIndex);

            // Display the new file in the View for this controller - Done
            View.ShowDisplay(CurrentDisplay);
        }

        /// <summary>
        /// Creates a SubDesign and adds it to our global SubDesigns Dictionary
        /// </summary>
        /// <param name="filename">The path of the file that will be consumed by the new SubDesign</param>
        /// <returns>Returns a new SubDesign on success; Returns null if otherwise</returns>
        private SubDesign CreateNewSubDesign(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return null;

            try
            {
                SubDesign sd = new SubDesign(filename);
                if (!Globals.SubDesigns.ContainsKey(sd.FileSourceName)) Globals.SubDesigns.Add(sd.FileSourceName, sd);

                return sd;
            }
            catch (Exception ex)
            {
                View.DisplayErrorMessage(ex);
                return null;
            }
        }

        /// <summary>
        /// Loads the display of the given type into our CurrentDisplay
        /// </summary>
        /// <param name="displayType">The type of display to fetch from our AllDisplays Dictionary</param>
        /// <returns>Returns the display that was loaded on success; Throws exception otherwise</returns>
        private DisplayBase LoadDisplay(DisplayType displayType)
        {
            if (AllDisplays.Count != 3) throw new Exception("MainWindow Displays have been loaded incorrectly.");
            if (!AllDisplays.ContainsKey(displayType)) throw new Exception(String.Concat("MainWindowController contains no reference to ", displayType.ToString()));

            CurrentDisplay = AllDisplays[displayType];
            CurrentDisplay.Dock = DockStyle.Fill;

            return CurrentDisplay;
        }
    }
}
