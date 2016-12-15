using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisiBoole.Controllers;
using VisiBoole.Views;

namespace VisiBoole
{
	public partial class MainWindow : Form, IMainWindow
	{
		private IMainWindowController controller;

		public MainWindow()
		{
			InitializeComponent();
		}

		#region "Utility Methods"

		public void AddNavTreeNode(string path)
		{
			string filename = path.Substring(path.LastIndexOf("\\") + 1);

			TreeNode node = new TreeNode(filename);
			node.Name = filename;

			if (NavTree.Nodes.ContainsKey(filename)) Globals.DisplayException(new Exception(string.Concat("Node ", filename, " already exists in 'My SubDesings'.")));
			NavTree.Nodes[0].Nodes.Add(node);

			NavTree.ExpandAll();
		}

		public void AttachController(IMainWindowController controller)
		{
			this.controller = controller;
		}

		public void ConfirmExit(bool isDirty)
		{
			if (isDirty == true)
			{
				System.Media.SystemSounds.Asterisk.Play();
				DialogResult response = MessageBox.Show("You have made changes that have not been saved - do you wish to continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				if (response == DialogResult.Yes)
				{
					Application.Exit();
				}
			}
			else
			{
				Application.Exit();
			}
		}

		public void LoadDisplay(IDisplay previous, IDisplay current)
		{
			if (current == null) Globals.DisplayException(new ArgumentNullException("Unable to load given display - the given display is null."));

			if (this.MainLayoutPanel.Controls.Contains((Control)previous)) this.MainLayoutPanel.Controls.Remove((Control)previous);
			if (this.MainLayoutPanel.Controls.Contains(OpenFileLinkLabel)) this.MainLayoutPanel.Controls.Remove(OpenFileLinkLabel);

			Control c = (Control)current;
			c.Dock = DockStyle.Fill;
			this.MainLayoutPanel.Controls.Add(c);
		}

		public void SaveFileSuccess(bool fileSaved)
		{
			if (fileSaved == true)
			{
				MessageBox.Show("File save successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("File save failed.", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

		#region "Click Events"

		private void NavTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			controller.SelectTabPage(e.Node.Name);
            controller.ReturnToSingleDisplay();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult response = saveFileDialog1.ShowDialog();
			if (response != DialogResult.OK) return;

			controller.ProcessNewFile(saveFileDialog1.FileName, true);
			saveFileDialog1.FileName = "newFile1.vbi";
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult response = openFileDialog1.ShowDialog();
			if (response == DialogResult.OK)
			{
				controller.ProcessNewFile(openFileDialog1.FileName);
				openFileDialog1.FileName = string.Empty;
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.SaveFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult response = saveFileDialog1.ShowDialog();
			if (response == DialogResult.OK)
			{
				controller.SaveFileAs(saveFileDialog1.FileName);
				saveFileDialog1.FileName = "newFile1.vbi";
			}
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.ExitApplication();
		}

		private void standardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.LoadDisplay(Globals.DisplayType.SINGLE);
		}

		private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.LoadDisplay(Globals.DisplayType.HORIZONTAL);
		}

		private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			controller.LoadDisplay(Globals.DisplayType.VERTICAL);
		}

		private void OpenFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			DialogResult response = openFileDialog1.ShowDialog();
			if (response != DialogResult.OK) return;

			controller.ProcessNewFile(openFileDialog1.FileName);
			openFileDialog1.FileName = string.Empty;
		}

		#endregion
	}
}
