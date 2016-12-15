using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisiBoole
{
	public static class Globals
	{
		public static Dictionary<string, SubDesign> SubDesigns = new Dictionary<string, SubDesign>();

		public enum DisplayType
		{
			SINGLE,
			HORIZONTAL,
			VERTICAL,
			OUTPUT
		}

		public static void DisplayException(Exception e)
		{
			Cursor.Current = Cursors.Default;
			MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
