﻿using System;
using System.Windows.Forms;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	/// <summary>
	/// The vertically-split display that is hosted by the MainWindow
	/// </summary>
	public partial class DisplayVertical : UserControl, IDisplay
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
				return Globals.DisplayType.VERTICAL;
			}
		}

		/// <summary>
		/// Constructs an instance of DisplayVertical
		/// </summary>
		public DisplayVertical()
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
			this.pnlMain.Controls.Add(tc, 0, 0);
			tc.Dock = DockStyle.Fill;
		}

		/// <summary>
		/// Loads the given web browser into this display
		/// </summary>
		/// <param name="browser">The browser that will be loaded by this display</param>
		public void LoadWebBrowser(WebBrowser browser)
		{
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 1, 0);
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
