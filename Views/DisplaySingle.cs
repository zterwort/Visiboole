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
	public partial class DisplaySingle : UserControl, IDisplay
	{
		private IDisplayController controller;

		public DisplaySingle()
		{
			InitializeComponent();
		}

		public Globals.DisplayType TypeOfDisplay
		{
			get
			{
				return Globals.DisplayType.SINGLE;
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

		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			controller.Run();
		}
	}
}
