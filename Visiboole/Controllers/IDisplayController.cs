using System.Windows.Forms;
using VisiBoole.Models;
using VisiBoole.Views;

namespace VisiBoole.Controllers
{
	/// <summary>
	/// Exposes methods in the controller for the four displays
	/// </summary>
	public interface IDisplayController
	{
		/// <summary>
		/// The display that was hosted by the MainWindow before the current one
		/// </summary>
		IDisplay PreviousDisplay { get; set; }

		/// <summary>
		/// The display that is currently hosted by the MainWindow
		/// </summary>
		IDisplay CurrentDisplay { get; set; }

		/// <summary>
		/// Returns a handle to the display of the matching type
		/// </summary>
		/// <param name="dType">The type of the display to return</param>
		/// <returns>Returns a handle to the display of the matching type</returns>
		IDisplay GetDisplayOfType(Globals.DisplayType dType);

		/// <summary>
		/// Saves the handle to the controller for the MainWindow
		/// </summary>
		void AttachMainWindowController(IMainWindowController mwController);

		/// <summary>
		/// Creates a new tab on the TabControl
		/// </summary>
		/// <param name="sd">The SubDesign that is displayed in the new tab</param>
		/// <returns>Returns true if a new tab was successfully created</returns>
		bool CreateNewTab(SubDesign sd);

		/// <summary>
		/// Saves the file that is associated with the currently selected tabpage
		/// </summary>
		/// <returns></returns>
		bool SaveActiveTab();

		/// <summary>
		/// Returns the TabPage that is currently selected
		/// </summary>
		/// <returns>Returns the TabPage that is currently selected</returns>
		TabPage GetActiveTabPage();

		/// <summary>
		/// Selects the tabpage with matching name
		/// </summary>
		/// <param name="fileName">The name of the tabpage to select</param>
		/// <returns>Returns the tabpage that matches the given string</returns>
		bool SelectTabPage(string fileName);

		/// <summary>
		/// Handles the event that occurs when the user runs the parser
		/// </summary>
		void Run();

        void Tick();
	}
}
