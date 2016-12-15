using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	/// <summary>
	/// The horizontally-split display that is hosted by the MainWindow
	/// </summary>
	public partial class DisplayHorizontal : UserControl, IDisplay
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
				return Globals.DisplayType.HORIZONTAL;
			}
		}

		/// <summary>
		/// Constucts an instance of DisplayHorizontal
		/// </summary>
		public DisplayHorizontal()
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
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 0, 2);
				browser.Dock = DockStyle.Fill;
			}
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
