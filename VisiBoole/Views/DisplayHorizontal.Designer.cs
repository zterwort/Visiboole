namespace VisiBoole
{
    partial class DisplayHorizontal
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
            this.pnlOutputControls = new System.Windows.Forms.Panel();
            this.IndependentVars = new System.Windows.Forms.RichTextBox();
            this.updTickCount = new System.Windows.Forms.NumericUpDown();
            this.btnTick = new System.Windows.Forms.Button();
            this.outputBrowser = new System.Windows.Forms.WebBrowser();
            this.EditorTabControl = new System.Windows.Forms.TabControl();
            this.pnlMain.SuspendLayout();
            this.pnlEditorControls.SuspendLayout();
            this.pnlOutputControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.EditorTabControl, 0, 0);
            this.pnlMain.Controls.Add(this.pnlEditorControls, 0, 1);
            this.pnlMain.Controls.Add(this.pnlOutputControls, 0, 3);
            this.pnlMain.Controls.Add(this.outputBrowser, 0, 2);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 4;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.Size = new System.Drawing.Size(2139, 1436);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlEditorControls
            // 
            this.pnlEditorControls.Controls.Add(this.btnRun);
            this.pnlEditorControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorControls.Location = new System.Drawing.Point(8, 638);
            this.pnlEditorControls.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pnlEditorControls.Name = "pnlEditorControls";
            this.pnlEditorControls.Size = new System.Drawing.Size(2123, 72);
            this.pnlEditorControls.TabIndex = 2;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRun.Location = new System.Drawing.Point(1912, 10);
            this.btnRun.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(200, 55);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // pnlOutputControls
            // 
            this.pnlOutputControls.Controls.Add(this.IndependentVars);
            this.pnlOutputControls.Controls.Add(this.updTickCount);
            this.pnlOutputControls.Controls.Add(this.btnTick);
            this.pnlOutputControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputControls.Location = new System.Drawing.Point(8, 1355);
            this.pnlOutputControls.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.pnlOutputControls.Name = "pnlOutputControls";
            this.pnlOutputControls.Size = new System.Drawing.Size(2123, 74);
            this.pnlOutputControls.TabIndex = 3;
            // 
            // IndependentVars
            // 
            this.IndependentVars.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.IndependentVars.Location = new System.Drawing.Point(11, 13);
            this.IndependentVars.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.IndependentVars.Multiline = false;
            this.IndependentVars.Name = "IndependentVars";
            this.IndependentVars.Size = new System.Drawing.Size(1663, 47);
            this.IndependentVars.TabIndex = 2;
            this.IndependentVars.Text = "";
            // 
            // updTickCount
            // 
            this.updTickCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.updTickCount.Location = new System.Drawing.Point(1757, 18);
            this.updTickCount.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.updTickCount.Name = "updTickCount";
            this.updTickCount.Size = new System.Drawing.Size(139, 38);
            this.updTickCount.TabIndex = 1;
            // 
            // btnTick
            // 
            this.btnTick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTick.Location = new System.Drawing.Point(1912, 13);
            this.btnTick.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(200, 55);
            this.btnTick.TabIndex = 0;
            this.btnTick.Text = "Tick";
            this.btnTick.UseVisualStyleBackColor = true;
            // 
            // outputBrowser
            // 
            this.outputBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBrowser.Location = new System.Drawing.Point(8, 724);
            this.outputBrowser.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.outputBrowser.MinimumSize = new System.Drawing.Size(53, 48);
            this.outputBrowser.Name = "outputBrowser";
            this.outputBrowser.Size = new System.Drawing.Size(2123, 617);
            this.outputBrowser.TabIndex = 4;
            // 
            // EditorTabControl
            // 
            this.EditorTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorTabControl.Font = new System.Drawing.Font("Garamond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditorTabControl.Location = new System.Drawing.Point(8, 7);
            this.EditorTabControl.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.EditorTabControl.Name = "EditorTabControl";
            this.EditorTabControl.SelectedIndex = 0;
            this.EditorTabControl.Size = new System.Drawing.Size(2123, 617);
            this.EditorTabControl.TabIndex = 0;
            // 
            // DisplayHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "DisplayHorizontal";
            this.Size = new System.Drawing.Size(2139, 1436);
            this.pnlMain.ResumeLayout(false);
            this.pnlEditorControls.ResumeLayout(false);
            this.pnlOutputControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlEditorControls;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Panel pnlOutputControls;
        private System.Windows.Forms.NumericUpDown updTickCount;
        private System.Windows.Forms.Button btnTick;
        private System.Windows.Forms.RichTextBox IndependentVars;
        public System.Windows.Forms.WebBrowser outputBrowser;
        public System.Windows.Forms.TabControl EditorTabControl;
    }
}
