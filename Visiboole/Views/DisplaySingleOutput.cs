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
	public partial class DisplaySingleOutput : UserControl, IDisplay
	{
		private IDisplayController controller;

		public DisplaySingleOutput()
		{
			InitializeComponent();
		}

		public Globals.DisplayType TypeOfDisplay
		{
			get
			{
				return Globals.DisplayType.OUTPUT;
			}
		}

		public void AttachController(IDisplayController controller)
		{
			this.controller = controller;
		}

		public void LoadTabControl(TabControl tc)
		{
			return;
		}

		public void LoadWebBrowser(WebBrowser browser)
		{
			if (!(browser == null))
			{
				this.pnlMain.Controls.Add(browser, 0, 0);
				browser.Dock = DockStyle.Fill;
			}
		}
	}
}
