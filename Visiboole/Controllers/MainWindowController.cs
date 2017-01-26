using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.Views;
using System.IO;

namespace VisiBoole.Controllers
{
	/// <summary>
	/// Handles the logic and communication with other objects for the actions in the MainWindow
	/// </summary>
	public class MainWindowController : IMainWindowController
	{
		/// <summary>
		/// Handle to the MainWindow which is the view for this controller
		/// </summary>
		private IMainWindow view;

		/// <summary>
		/// Handle to the controller for the displays that are hosted by the MainWindow
		/// </summary>
		private IDisplayController displayController;

		/// <summary>
		/// Constructs an instance of MainWindowController with handles to its view and the display controller
		/// </summary>
		/// <param name="view">Handle to the MainWindow which is the view for this controller</param>
		/// <param name="displayController">Handle to the controller for the displays that are hosted by the MainWindow</param>
		public MainWindowController(IMainWindow view, IDisplayController displayController)
		{
			this.view = view;
			view.AttachController(this);
			this.displayController = displayController;			
		}

        /// <summary>
        /// Used to check if the display is the output, if it is, change it to editor.
        /// </summary>
        public void checkSingleViewChange()
        {
            if (displayController.CurrentDisplay is DisplaySingleOutput)
            {
                LoadDisplay(Globals.DisplayType.SINGLE);
            }
        }

        /// <summary>
        /// Creates a new SubDesign with a file created from the given filename
        /// </summary>
        /// <param name="filename">The filename of the file to create the new SubDesign with</param>
        /// <returns>Returns the SubDesign created from the given filename</returns>
        private SubDesign CreateNewSubDesign(string filename)
		{
			try
			{
				SubDesign newSubDesign = new SubDesign(filename);
                if (!Globals.SubDesigns.ContainsKey(newSubDesign.FileSourceName))
                {
                    Globals.SubDesigns.Add(newSubDesign.FileSourceName, newSubDesign);
                }

				return newSubDesign;
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
				return null;
			}
		}

		/// <summary>
		/// Performs a dirty check and confirms application exit with the user
		/// </summary>
		public void ExitApplication()
		{
			bool isDirty = false;

			foreach (KeyValuePair<string, SubDesign> kvp in Globals.SubDesigns)
			{
				SubDesign sd = kvp.Value;

				if (sd.isDirty)
				{
					isDirty = true;
				}
			}
			view.ConfirmExit(isDirty);
		}

		/// <summary>
		/// Loads into the MainWindow the display of the given type
		/// </summary>
		/// <param name="dType">The type of display that should be loaded</param>
		public void LoadDisplay(Globals.DisplayType dType)
		{
			displayController.PreviousDisplay = displayController.CurrentDisplay;
			displayController.CurrentDisplay = displayController.GetDisplayOfType(dType);
			view.LoadDisplay(displayController.PreviousDisplay, displayController.CurrentDisplay);
		}

		/// <summary>
		/// Processes a new file that is created or opened by the user
		/// </summary>
		/// <param name="path">The path of the file that was created or opened by the user</param>
		/// <param name="overwriteExisting">True if the file at the given path should be overwritten</param>
		public void ProcessNewFile(string path, bool overwriteExisting = false)
		{
			try
			{
                if (overwriteExisting == true && File.Exists(path))
                {
                    File.Delete(path);
                }

				SubDesign sd = CreateNewSubDesign(path);

                if (displayController.CreateNewTab(sd) == true)
                {
                    view.AddNavTreeNode(sd.FileSourceName);
                }

				LoadDisplay(displayController.CurrentDisplay.TypeOfDisplay);
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
			}
		}

		/// <summary>
		/// Saves the file that is currently active in the selected tabpage
		/// </summary>
		public void SaveFile()
		{
			try
			{
				view.SaveFileSuccess(displayController.SaveActiveTab());
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
			}
		}

		/// <summary>
		/// Saves the file that is currently active in the selected tabpage with the filename chosen by the user
		/// </summary>
		/// <param name="path">The new file path to save the active file to</param>
		public void SaveFileAs(string path)
		{
			try
			{
				// Write the contents of the active tab in a new file at location of the selected path
				string content = displayController.GetActiveTabPage().SubDesign().Text;
				File.WriteAllText(Path.ChangeExtension(path, ".vbi"), content);

				// Process the new file as usual
				ProcessNewFile(path);
				view.SaveFileSuccess(true);
			}
			catch (Exception ex)
			{
				view.SaveFileSuccess(false);
				Globals.DisplayException(ex);
			}
		}

		/// <summary>
		/// Selects the tabpage in the tabcontrol with name matching the given string
		/// </summary>
		/// <param name="fileName">The name of the tabpage to select</param>
		public void SelectTabPage(string fileName)
		{
			try
			{
				displayController.SelectTabPage(fileName);
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
			}
		}
	}
}
