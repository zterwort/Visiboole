using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole;
using VisiBoole.Models;
using VisiBoole.Views;

namespace VisiBoole.Controllers
{
    /// <summary>
    /// Handles the logic, and communication with other objects for the displays hosted by the MainWindow
    /// </summary>
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public class DisplayController : IDisplayController
	{		
		#region "Class Fields and Properties"

		/// <summary>
		/// No-split input view that is hosted by the MainWindow
		/// </summary>
		private IDisplay single;

		/// <summary>
		/// Horizontal-split view that is hosted by the MainWindow
		/// </summary>
		private IDisplay horizontal;

		/// <summary>
		/// Vertical-split view that is hosted by the MainWindow
		/// </summary>
		private IDisplay vertical;

		/// <summary>
		/// No-split output view that is hosted by the MainWindow
		/// </summary>
		private IDisplay singleOutput;

		/// <summary>
		/// No-split view that is hosted by the MainWindow
		/// </summary>
		private Dictionary<Globals.DisplayType, IDisplay> allDisplays;

		/// <summary>
		/// Handle to the controller for the MainWindow
		/// </summary>
		private IMainWindowController mwController;

		/// <summary>
		/// The TabControl that shows the input that is shared amongst the displays that are hosted by the MainWindow
		/// </summary>
		private TabControl tabControl;

		/// <summary>
		/// The WebBrowser that shows the output that is shared amongst the displays that are hosted by the MainWindow
		/// </summary>
		private WebBrowser browser;

		/// <summary>
		/// Handle to the output parser that parses the output that is viewed by the user
		/// </summary>
		private OutputParser parseOut;

		/// <summary>
		/// The display that was hosted by the MainWindow before the current one
		/// </summary>
		public IDisplay PreviousDisplay { get; set; }

		/// <summary>
		/// The display that is currently hosted by the MainWindow
		/// </summary>
		private IDisplay _CurrentDisplay;

		/// <summary>
		/// The display that is currently hosted by the MainWindow
		/// </summary>
		public IDisplay CurrentDisplay
		{
			get
			{
				return _CurrentDisplay;
			}
			set
			{
				value.LoadTabControl(tabControl);
				value.LoadWebBrowser(browser);
				_CurrentDisplay = value;
			}
		}

		#endregion

		#region "Class Initialization

		/// <summary>
		/// Constructs an instance of DisplayController with a handle to the four displays
		/// </summary>
		/// <param name="single">Handle to the no-split input view hosted by the MainWindow</param>
		/// <param name="horizontal">Handle to the horizontally-split view hosted by the MainWindow</param>
		/// <param name="vertical">Handle to the vertically-split view hosted by the MainWindow</param>
		/// <param name="singleOutput">Handle to the no-split output view hosted by the MainWindow</param>
		public DisplayController(IDisplay single, IDisplay horizontal, IDisplay vertical, IDisplay singleOutput)
		{
			tabControl = new TabControl();
			browser = new WebBrowser();
			parseOut = new OutputParser();

			this.single = single;
			this.horizontal = horizontal;
			this.vertical = vertical;
			this.singleOutput = singleOutput;

			allDisplays = new Dictionary<Globals.DisplayType, IDisplay>();
			allDisplays.Add(Globals.DisplayType.SINGLE, single);
			allDisplays.Add(Globals.DisplayType.HORIZONTAL, horizontal);
			allDisplays.Add(Globals.DisplayType.VERTICAL, vertical);
			allDisplays.Add(Globals.DisplayType.OUTPUT, singleOutput);

			CurrentDisplay = single;
		}

		/// <summary>
		/// Saves the handle to the controller for the MainWindow
		/// </summary>
		/// <param name="mwController"></param>
		public void AttachMainWindowController(IMainWindowController mwController)
		{
			this.mwController = mwController;
		}

		#endregion

		#region "TabControl Interaction"

		/// <summary>
		/// Saves the file that is associated with the currently selected tabpage
		/// </summary>
		/// <returns></returns>
		public bool SaveActiveTab()
		{
			SubDesign sd = tabControl.SelectedTab.SubDesign();

			if (sd == null)
			{
				return false;
			}
			else
			{
				sd.SaveTextToFile();
				return true;
			}
		}

		/// <summary>
		/// Selects the tabpage with matching name
		/// </summary>
		/// <param name="fileName">The name of the tabpage to select</param>
		/// <returns>Returns the tabpage that matches the given string</returns>
		public bool SelectTabPage(string fileName)
		{
			if (tabControl.TabPages.IndexOfKey(fileName) != -1)
			{
				tabControl.SelectTab(fileName);
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Creates a new tab on the TabControl
		/// </summary>
		/// <param name="sd">The SubDesign that is displayed in the new tab</param>
		/// <returns>Returns true if a new tab was successfully created</returns>
		public bool CreateNewTab(SubDesign sd)
		{
			TabPage tab = new TabPage(sd.FileSourceName);

            tab.Name = sd.FileSourceName;
			tab.Controls.Add(sd);
			sd.Dock = DockStyle.Fill;

			if (tabControl.TabPages.ContainsKey(sd.FileSourceName))
			{
				int index = tabControl.TabPages.IndexOfKey(sd.FileSourceName);

				tabControl.TabPages.RemoveByKey(sd.FileSourceName);
				tabControl.TabPages.Insert(index, tab);
				sd.TabPageIndex = tabControl.TabPages.IndexOf(tab);
				tabControl.SelectTab(tab);
				return false;
			}
			else
			{
				tabControl.TabPages.Add(tab);
				sd.TabPageIndex = tabControl.TabPages.IndexOf(tab);
				tabControl.SelectTab(tab);
				return true;
			}
		}

		/// <summary>
		/// Returns the TabPage that is currently selected
		/// </summary>
		/// <returns>Returns the TabPage that is currently selected</returns>
		public TabPage GetActiveTabPage()
		{
			return tabControl.SelectedTab;
		}

		#endregion

		#region "Display Interaction

		/// <summary>
		/// Returns a handle to the display of the matching type
		/// </summary>
		/// <param name="dType">The type of the display to return</param>
		/// <returns>Returns a handle to the display of the matching type</returns>
		public IDisplay GetDisplayOfType(Globals.DisplayType dType)
		{
			switch (dType)
			{
				case Globals.DisplayType.SINGLE:
					return single;
				case Globals.DisplayType.OUTPUT:
					return singleOutput;
				case Globals.DisplayType.HORIZONTAL:
					return horizontal;
				case Globals.DisplayType.VERTICAL:
					return vertical;
				default:
					return null;
			}
		}

		/// <summary>
		/// Handles the event that occurs when the user runs the parser
		/// </summary>
		public void Run()
		{
			SubDesign sd = tabControl.SelectedTab.SubDesign();
            InputParser parseIn = new InputParser(sd);
            parseIn.ParseInput(null);
			parseOut.Input = sd.Text;
            List<string> outputText = parseOut.GenerateOutput();
            HtmlBuilder html = new HtmlBuilder(outputText, sd.FileSourceName, sd.Variables, sd.Expressions);
			string htmlOutput = html.GetHTML();

			browser.ObjectForScripting = this;
			html.DisplayHtml(htmlOutput, browser);

			if (CurrentDisplay is DisplaySingle)
			{
				mwController.LoadDisplay(Globals.DisplayType.OUTPUT);
			}
		}

		/// <summary>
		/// Handles the event that occurs when the user clicks on an independent variable
		/// </summary>
		/// <param name="variableName">The name of the variable that was clicked by the user</param>
		public void Variable_Click(string variableName)
		{
			SubDesign sd = tabControl.SelectedTab.SubDesign();
            InputParser parseIn = new InputParser(sd);
            parseIn.ParseInput(variableName);
			parseOut.Input = sd.Text;
            List<string> outputText = parseOut.GenerateOutput();
            HtmlBuilder html = new HtmlBuilder(outputText, sd.FileSourceName, sd.Variables, sd.Expressions);
			string htmlOutput = html.GetHTML();

			browser.ObjectForScripting = this;
			html.DisplayHtml(htmlOutput, browser);

			if (CurrentDisplay is DisplaySingle)
			{
				mwController.LoadDisplay(Globals.DisplayType.OUTPUT);
			}
		}

		#endregion		
	}
}
