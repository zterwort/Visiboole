using System.Windows.Forms;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	/// <summary>
	/// The horizontally-split display that is hosted by the MainWindow
	/// </summary>
	public partial class DisplaySingleOutput : UserControl, IDisplay
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
				return Globals.DisplayType.OUTPUT;
			}
		}

		/// <summary>
		/// Constucts an instance of DisplaySingleOutput
		/// </summary>
		public DisplaySingleOutput()
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
		/// This method is not implemented because this display contains no tabcontrol
		/// </summary>
		/// <param name="tc"></param>
		public void LoadTabControl(TabControl tc)
		{
			return;
		}

		/// <summary>
		/// Loads the given web browser into this display
		/// </summary>
		/// <param name="browser">The browser that will be loaded by this display</param>
		public void LoadWebBrowser(WebBrowser browser)
		{
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 0, 0);
				browser.Dock = DockStyle.Fill;
			}
		}

        private void btnTick_Click(object sender, System.EventArgs e)
        {
            controller.Tick();
        }
    }
}
