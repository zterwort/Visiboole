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
        /// All of the Displays - single, horizontal, and vertical - that are hosted by the MainWindow
        /// </summary>
        private Dictionary<Globals.DisplayType, DisplayBase> AllDisplays;

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
            DisplayBase SingleOutput = new DisplaySingleOutput();

            // Add them to our local Dictionary
            AllDisplays = new Dictionary<Globals.DisplayType, DisplayBase>();
            AllDisplays.Add(Globals.DisplayType.SINGLE, Single);
            AllDisplays.Add(Globals.DisplayType.HORIZONTAL, Horizontal);
            AllDisplays.Add(Globals.DisplayType.VERTICAL, Vertical);
            AllDisplays.Add(Globals.DisplayType.SINGLEOUTPUT, SingleOutput);

            // Load up the Single display
            LoadDisplay(Globals.DisplayType.SINGLE);

            // Wire up our event handlers
            View.ProcessNewFile += new ProcessNewFileHandler(View_ProcessNewFile);
            View.LoadDisplay += new LoadDisplayHandler(View_LoadDisplay);
            View.SaveFile += new SaveFileHandler(View_SaveFile);
            View.SaveAs += new SaveAsHandler(View_SaveAs);

            foreach (KeyValuePair<Globals.DisplayType, DisplayBase> kvp in AllDisplays)
            {
                DisplayBase display = kvp.Value;
                display.ShowSingleOutput += new ShowSingleOutputHandler(Display_ShowSingleOutput);
            }
        }

        private void Display_ShowSingleOutput(object sender, EventArgs e)
        {
            SubDesign info = Globals.SubDesigns[Globals.tabControl.SelectedTab.Name];

            InputParser parser = new InputParser(info);
            OutputParser output = new OutputParser(info.Text);
            List<string> outputText = output.GenerateOutput();
            HtmlBuilder html = new HtmlBuilder(outputText, info.FileSourceName);
            string htmlOutput = html.GetHTML();

            if (Globals.CurrentDisplay != null)
            {
                if (Globals.CurrentDisplay is DisplaySingleEditor)
                {
                    WebBrowser browser = new WebBrowser();
                    DisplaySingleOutput singleOutput = new DisplaySingleOutput();
                    html.DisplayHtml(htmlOutput, browser);
                    singleOutput.Controls.Add(browser);
                    Globals.CurrentDisplay = singleOutput;
                    LoadDisplay(Globals.DisplayType.SINGLEOUTPUT);
                }
                else if (Globals.CurrentDisplay is DisplayVertical)
                {
                    WebBrowser browser = ((VisiBoole.DisplayVertical)Globals.CurrentDisplay).outputBrowser;
                    html.DisplayHtml(htmlOutput, browser);
                }
                else if (Globals.CurrentDisplay is DisplayHorizontal)
                {
                    WebBrowser browser = ((VisiBoole.DisplayHorizontal)Globals.CurrentDisplay).outputBrowser;
                    html.DisplayHtml(htmlOutput, browser);
                }
            }
        }

        #region "Event Handlers"

        /// <summary>
        /// Constructs, loads, and displays the SubDesign created from the given data
        /// </summary>
        private void View_ProcessNewFile(object sender, ProcessNewFileEventArgs e)
        {
            e.PreviousDisplay = Globals.CurrentDisplay;

            SubDesign sd = CreateNewSubDesign(e.FilePath);
            Globals.CurrentDisplay.CreateNewTab(sd);
            e.FileName = sd.FileSourceName;

            e.CurrentDisplay = Globals.CurrentDisplay;
        }

        /// <summary>
        /// Loads the Globals.CurrentDisplay with the display of the given type and returns the current display through the event args
        /// </summary>
        private void View_LoadDisplay(object sender, LoadDisplayEventArgs e)
        {
            e.PreviousDisplay = Globals.CurrentDisplay;
            DisplayBase db = LoadDisplay(e.DisplayType);
            e.CurrentDisplay = Globals.CurrentDisplay;
        }

        /// <summary>
        /// Saves the contents of the SubDesign matching the given tabpage index
        /// </summary>
        private void View_SaveFile(object sender, SaveFileEventArgs e)
        {
            try
            {
                foreach (KeyValuePair<string, SubDesign> kvp in Globals.SubDesigns)
                {
                    if (kvp.Value.TabPageIndex == e.tabPageIndex)
                    {
                        kvp.Value.SaveTextToFile();
                    }
                }
            }
            catch (Exception ex)
            {
                View.DisplayErrorMessage(ex);
            }
        }

        /// <summary>
        /// Saves the contents of the SubDesign matching the given tabpage index in a location of the user's choosing
        /// </summary>
        private void View_SaveAs(object sender, SaveAsEventArgs e)
        {
            try
            {
                foreach (KeyValuePair<string, SubDesign> kvp in Globals.SubDesigns)
                {
                    if (kvp.Value.TabPageIndex == Globals.tabControl.SelectedIndex)
                    {
                        // Extract the short filename
                        string fileName = e.FilePath.Substring(e.FilePath.LastIndexOf("\\") + 1);
                        e.FileName = fileName;

                        // Write the contents of the file at the location of the given path
                        File.WriteAllText(Path.ChangeExtension(e.FilePath, ".vbi"), kvp.Value.Text);

                        // Create a new SubDesign, then a new TabPage with it, then select that TabPage
                        SubDesign sd = new SubDesign(e.FilePath);
                        Globals.CurrentDisplay.CreateNewTab(sd);
                        Globals.tabControl.SelectTab(sd.TabPageIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                View.DisplayErrorMessage(ex);
            }
        }

        #endregion

        #region "Utility Functions"

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
        /// Loads the display of the given type into our Globals.CurrentDisplay
        /// </summary>
        /// <param name="displayType">The type of display to fetch from our AllDisplays Dictionary</param>
        /// <returns>Returns the display that was loaded on success; Throws exception otherwise</returns>
        private DisplayBase LoadDisplay(Globals.DisplayType displayType)
        {
            if (AllDisplays.Count != 4) throw new Exception("MainWindow Displays have been loaded incorrectly.");
            if (!AllDisplays.ContainsKey(displayType)) throw new Exception(String.Concat("MainWindowController contains no reference to ", displayType.ToString()));

            Globals.CurrentDisplay = AllDisplays[displayType];

            Globals.tabControl.Multiline = true;
            Globals.tabControl.Anchor = AnchorStyles.Left & AnchorStyles.Right & AnchorStyles.Bottom & AnchorStyles.Top;
            Globals.tabControl.Dock = DockStyle.Fill;

            Globals.CurrentDisplay.Controls["pnlMain"].Controls.Add(Globals.tabControl);
            Globals.CurrentDisplay.Dock = DockStyle.Fill;

            return Globals.CurrentDisplay;
        }

        #endregion
    }
}
