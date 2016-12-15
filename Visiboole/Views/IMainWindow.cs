using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	/// <summary>
	/// Exposes the methods for the MainWindow
	/// </summary>
	public interface IMainWindow
	{
		/// <summary>
		/// Adds a new node in the TreeView
		/// </summary>
		/// <param name="path">The filepath string that will be parsed to obtain the name of this treenode</param>
		void AddNavTreeNode(string path);

		/// <summary>
		/// Saves the handle to the controller for this view
		/// </summary>
		/// <param name="controller">The handle to the controller for this view</param>
		void AttachController(IMainWindowController controller);

		/// <summary>
		/// Loads the given IDisplay
		/// </summary>
		/// <param name="previous">The display to replace</param>
		/// <param name="current">The display to be loaded</param>
		void LoadDisplay(IDisplay previous, IDisplay current);

		/// <summary>
		/// Displays file-save success message to the user
		/// </summary>
		/// <param name="fileSaved">True if the file was saved successfully</param>
		void SaveFileSuccess(bool fileSaved);

		/// <summary>
		/// Confirms exit with the user if the application is dirty
		/// </summary>
		/// <param name="isDirty">True if any open SubDesigns have been modified since last save</param>
		void ConfirmExit(bool isDirty);
	}
}
