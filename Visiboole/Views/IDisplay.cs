using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;
using static VisiBoole.Globals;

namespace VisiBoole.Views
{
	/// <summary>
	/// Exposes methods for the four displays hosted by the MainWindow
	/// </summary>
	public interface IDisplay
	{
		/// <summary>
		/// Returns the type of this display
		/// </summary>
		DisplayType TypeOfDisplay { get; }

		/// <summary>
		/// Saves the handle to the controller for this display
		/// </summary>
		/// <param name="controller">The handle to the controller to save</param>
		void AttachController(IDisplayController controller);

		/// <summary>
		/// Loads the given tabcontrol into this display
		/// </summary>
		/// <param name="tc">The tabcontrol that will be loaded by this display</param>
		void LoadTabControl(TabControl tc);

		/// <summary>
		/// Loads the given web browser into this display
		/// </summary>
		/// <param name="browser">The browser that will be loaded by this display</param>
		void LoadWebBrowser(WebBrowser browser);
	}
}
