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
	public partial class DisplayVertical : UserControl, IDisplay
	{
		private IDisplayController controller;

		public DisplayVertical()
		{
			InitializeComponent();
		}

		public Globals.DisplayType TypeOfDisplay
		{
			get
			{
				return Globals.DisplayType.VERTICAL;
			}
		}

		public void AttachController(IDisplayController controller)
		{
			this.controller = controller;
		}

		public void LoadTabControl(TabControl tc)
		{
			this.pnlMain.Controls.Add(tc, 0, 0);
			tc.Dock = DockStyle.Fill;
		}

		public void LoadWebBrowser(WebBrowser browser)
		{
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 1, 0);
			}
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			controller.Run();
		}
	}
}
