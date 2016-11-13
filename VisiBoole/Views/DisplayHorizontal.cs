using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBooleAbstract;

namespace VisiBoole
{
    /// <summary>
    /// The Horizontal View for the main menu display
    /// </summary>
    public partial class DisplayHorizontal : DisplayBase
    {
        /// <summary>
        /// Constructs an instance of ctlDisplayHorizontal
        /// </summary>
        public DisplayHorizontal()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            //rtfOutput.Text = Globals.html["basic.vbi"].ToString();
            /*box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;



            rtfOutput.AppendText("[" + DateTime.Now.ToShortTimeString() + "]", Color.Red);
            rtfOutput.AppendText(" ");
            rtfOutput.AppendText(userid, Color.Green);
            rtfOutput.AppendText(": ");
            rtfOutput.AppendText(message, Color.Blue);
            rtfOutput.AppendText(Environment.NewLine);*/
            this.Run(Globals.subDesigns[((tabEditor.SelectedTab.ToString().Substring(0)).Split('{'))[1].TrimEnd('}')]);
        }
    }
}
