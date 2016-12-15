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
	public partial class DisplayHorizontal : UserControl, IDisplay
	{
		public DisplayHorizontal()
		{
			InitializeComponent();
		}

		private IDisplayController controller;

		public Globals.DisplayType TypeOfDisplay
		{
			get
			{
				return Globals.DisplayType.HORIZONTAL;
			}
		}

		public void AttachController(IDisplayController controller)
		{
			this.controller = controller;
		}

		public void LoadTabControl(TabControl tc)
		{
			if (!(tc == null))
			{
			this.pnlMain.Controls.Add(tc, 0, 0);
			tc.Dock = DockStyle.Fill;
			}
		}

		public void LoadWebBrowser(WebBrowser browser)
		{
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 0, 2);
				browser.Dock = DockStyle.Fill;
			}
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			controller.Run();
		}
	}
}
