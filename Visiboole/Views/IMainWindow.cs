using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisiBoole.Controllers;

namespace VisiBoole.Views
{
	public interface IMainWindow
	{
		void AddNavTreeNode(string path);
		void AttachController(IMainWindowController controller);
		void LoadDisplay(IDisplay previous, IDisplay current);
		void SaveFileSuccess(bool fileSaved);
		void ConfirmExit(bool isDirty);
	}
}
