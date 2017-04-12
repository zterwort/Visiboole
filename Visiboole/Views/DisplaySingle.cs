using System;
using System.Windows.Forms;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	/// <summary>
	/// The no-split input display that is hosted by the MainWindow
	/// </summary>
	public partial class DisplaySingle : UserControl, IDisplay
	{
		/// <summary>
		/// Handle to the controller for this display
		/// </summary>
		private IDisplayController controller;

		/// <summary>
		/// Returns the type of this display
		/// </summary>
		public Globals.DisplayType TypeOfDisplay
		{
			get
			{
				return Globals.DisplayType.SINGLE;
			}
		}

		/// <summary>
		/// Constructs an instance of DisplaySingle
		/// </summary>
		public DisplaySingle()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Saves the handle to the controller for this display
		/// </summary>
		/// <param name="controller">The handle to the controller to save</param>
		public void AttachController(IDisplayController controller)
		{
			this.controller = controller;
		}

		/// <summary>
		/// Loads the given tabcontrol into this display
		/// </summary>
		/// <param name="tc">The tabcontrol that will be loaded by this display</param>
		public void LoadTabControl(TabControl tc)
		{
			if (!(tc == null))
			{
				this.pnlMain.Controls.Add(tc, 0, 0);
				tc.Dock = DockStyle.Fill;
			}
		}

		/// <summary>
		/// Loads the given web browser into this display
		/// </summary>
		/// <param name="browser">The browser that will be loaded by this display</param>
		public void LoadWebBrowser(WebBrowser browser)
		{

		}

		/// <summary>
		/// Handles the event that occurs when run button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRun_Click(object sender, EventArgs e)
		{
			controller.Run();
		}
	}
}
