namespace VisiBoole
{
    partial class DisplaySingleEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEditorControls = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlEditorControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.pnlMain.Controls.Add(this.pnlEditorControls, 0, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.Size = new System.Drawing.Size(2133, 1431);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlEditorControls
            // 
            this.pnlEditorControls.Controls.Add(this.btnRun);
            this.pnlEditorControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorControls.Location = new System.Drawing.Point(8, 1352);
            this.pnlEditorControls.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pnlEditorControls.Name = "pnlEditorControls";
            this.pnlEditorControls.Size = new System.Drawing.Size(2117, 72);
            this.pnlEditorControls.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRun.Location = new System.Drawing.Point(1907, 10);
            this.btnRun.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(200, 55);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // DisplaySingleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "DisplaySingleEditor";
            this.Size = new System.Drawing.Size(2133, 1431);
            this.pnlMain.ResumeLayout(false);
            this.pnlEditorControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlEditorControls;
        private System.Windows.Forms.Button btnRun;

    }
}
