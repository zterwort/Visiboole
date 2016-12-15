using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.Views;
using System.IO;

namespace VisiBoole.Controllers
{
	public class MainWindowController : IMainWindowController
	{
		private IMainWindow view;
		private IDisplayController displayController;

		public MainWindowController(IMainWindow view, IDisplayController displayController)
		{
			this.view = view;
			view.AttachController(this);

			this.displayController = displayController;
			
		}

        public void ReturnToSingleDisplay()
        {
            if (displayController.CurrentDisplay is DisplaySingleOutput)
            {
                LoadDisplay(Globals.DisplayType.SINGLE);
            }
        }

		public IDisplayController IDisplayController
		{
			get
			{
				throw new System.NotImplementedException();
			}

			set
			{
			}
		}

		public IMainWindow IMainWindow
		{
			get
			{
				throw new System.NotImplementedException();
			}

			set
			{
			}
		}

		private SubDesign CreateNewSubDesign(string filename)
		{
			try
			{
				SubDesign sd = new SubDesign(filename);
				if (!Globals.SubDesigns.ContainsKey(sd.FileSourceName)) Globals.SubDesigns.Add(sd.FileSourceName, sd);

				return sd;
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
				return null;
			}
		}

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

		public void LoadDisplay(Globals.DisplayType dType)
		{
			displayController.PreviousDisplay = displayController.CurrentDisplay;
			displayController.CurrentDisplay = displayController.GetDisplayOfType(dType);
			view.LoadDisplay(displayController.PreviousDisplay, displayController.CurrentDisplay);
		}

		public void ProcessNewFile(string path, bool overwriteExisting = false)
		{
			try
			{
				if (overwriteExisting == true && File.Exists(path)) File.Delete(path);
				SubDesign sd = CreateNewSubDesign(path);
				if (displayController.CreateNewTab(sd) == true) view.AddNavTreeNode(sd.FileSourceName);
				LoadDisplay(displayController.CurrentDisplay.TypeOfDisplay);
			}
			catch (Exception ex)
			{
				Globals.DisplayException(ex);
			}
		}

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
