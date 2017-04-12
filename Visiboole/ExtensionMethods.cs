using System.Windows.Forms;
using VisiBoole.Models;

namespace VisiBoole
{
	/// <summary>
	/// Extension methods for this application
	/// </summary>
	public static class ExtensionMethods
	{
		/// <summary>
		/// Returns the SubDesign displayed by this tabpage
		/// </summary>
		/// <param name="tab">The parent control for the subdesign</param>
		/// <returns>Returns the SubDesign for this tabpage</returns>
		public static SubDesign SubDesign(this TabPage tab)
		{
			foreach (Control c in tab.Controls)
			{
                if ((c as SubDesign) != null)
                {
                    return c as SubDesign;
                }
			}
			return null;
		}
	}
}
