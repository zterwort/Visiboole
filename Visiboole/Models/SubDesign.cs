using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Ionic;

namespace VisiBoole.Models
{
    /// <summary>
    /// A User-Created SubDesign
    /// </summary>
    public class SubDesign : RichTextBoxEx
    {
        /// <summary>
        /// Dependent variables associated with the (independent) Variables dictionary
        /// </summary>
        public Dictionary<string, List<string>> Dependencies { get; set; }

        /// <summary>
        /// Independent variables associated with the (dependent) Dependencies dictionary
        /// </summary>
        public Dictionary<string, int> Variables { get; set; }

        /// <summary>
        /// Expressions constructed from independent and dependent variables
        /// </summary>
        public Dictionary<string, string> Expressions { get; set; }
        /// <summary>
        /// The index of the TabControl that this occupies
        /// </summary>
        public int TabPageIndex { get; set; }

        /// <summary>
        /// The file location that this SubDesign is saved in
        /// </summary>
        public FileInfo FileSource { get; set; }

        /// <summary>
        /// The short filename of the FileSource
        /// </summary>
        public string FileSourceName { get; set; }

		/// <summary>
		/// Returns True if this SubDesign Text does not match the FileSource contents
		/// </summary>
		public bool isDirty { get; set; }

        /// <summary>
        /// Constructs a new SubDesign object
        /// </summary>
        /// <param name="filename">The path of the file source for this SubDesign</param>
        public SubDesign(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("Invalid filename");
            }

            FileSource = new FileInfo(filename);
            this.FileSourceName = FileSource.Name;

            if (!File.Exists(filename))
            {
                FileSource.Create().Close();
            }

            SaveFileToText();
			isDirty = false;

			this.TextChanged += SubDesign_TextChanged;

            this.Variables = new Dictionary<string, int>();
            this.Expressions = new Dictionary<string, string>();
            this.Dependencies = new Dictionary<string, List<string>>();

	        this.ShowLineNumbers = true;
        }

		/// <summary>
		/// Sets the dirty flag when the contents of this SubDesign have changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubDesign_TextChanged(object sender, EventArgs e)
		{
			isDirty = true;
		}

		/// <summary>
		/// Copies the file contents of this subdesign filesource to this Text property
		/// </summary>
		private void SaveFileToText()
        {
            string text = string.Empty;

            using (StreamReader reader = this.FileSource.OpenText())
            {
                string nextLine = string.Empty;

                while ((nextLine = reader.ReadLine()) != null)
                {
                    text += nextLine + Environment.NewLine;
                }
            }
            this.Text = text;
        }

        /// <summary>
        /// Saves the contents of this Text property to the FileSource contents
        /// </summary>
        public void SaveTextToFile()
        {
            File.WriteAllText(this.FileSource.FullName, this.Text);
			isDirty = false;
        }
    }
}