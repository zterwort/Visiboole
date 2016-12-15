using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole;
using VisiBoole.Views;

namespace VisiBoole.Controllers
{
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public class DisplayController : IDisplayController
	{
		private IDisplay single;
		private IDisplay horizontal;
		private IDisplay vertical;
		private IDisplay singleOutput;
		private Dictionary<Globals.DisplayType, IDisplay> allDisplays;

		private IMainWindowController mwController;

		private TabControl tabControl;
		private WebBrowser browser;
		private InputParser parseIn;
		private OutputParser parseOut;

		public IDisplay PreviousDisplay { get; set; }

		private IDisplay _CurrentDisplay;
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

		public DisplayController(IDisplay single, IDisplay horizontal, IDisplay vertical, IDisplay singleOutput)
		{
			tabControl = new TabControl();
			browser = new WebBrowser();
			parseIn = new InputParser();
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

		public void AttachMainWindowController(IMainWindowController mwController)
		{
			this.mwController = mwController;
		}

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

		public TabPage GetActiveTabPage()
		{
			return tabControl.SelectedTab;
		}

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

		public void Run()
		{
			SubDesign sd = tabControl.SelectedTab.SubDesign();
			parseIn.ParseInput(sd, null);
			parseOut.Input = sd.Text;
			List<string> outputText = parseOut.GenerateOutput();
			HtmlBuilder html = new HtmlBuilder(outputText, sd.FileSourceName, parseIn.Variables, parseIn.Expressions);
			string htmlOutput = html.GetHTML();
			browser.ObjectForScripting = this;
			html.DisplayHtml(htmlOutput, browser);
			if (CurrentDisplay is DisplaySingle)
			{
				mwController.LoadDisplay(Globals.DisplayType.OUTPUT);
			}
		}

		public void Variable_Click(string variableName)
		{
			SubDesign sd = tabControl.SelectedTab.SubDesign();
			parseIn.ParseInput(sd, variableName);
			parseOut.Input = sd.Text;
			List<string> outputText = parseOut.GenerateOutput();
			HtmlBuilder html = new HtmlBuilder(outputText, sd.FileSourceName, parseIn.Variables, parseIn.Expressions);
			string htmlOutput = html.GetHTML();
			browser.ObjectForScripting = this;
			html.DisplayHtml(htmlOutput, browser);
			if (CurrentDisplay is DisplaySingle)
			{
				mwController.LoadDisplay(Globals.DisplayType.OUTPUT);
			}
		}
	}
}
