namespace VisiBoole
{
    partial class ctlDisplayVertical
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctlDisplayVertical));
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEditorControls = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.pnlOutputControls = new System.Windows.Forms.Panel();
            this.tlbVariables = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.updTickCount = new System.Windows.Forms.NumericUpDown();
            this.btnTick = new System.Windows.Forms.Button();
            this.tabEditor = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtfOutput = new System.Windows.Forms.RichTextBox();
            this.pnlMain.SuspendLayout();
            this.pnlEditorControls.SuspendLayout();
            this.pnlOutputControls.SuspendLayout();
            this.tlbVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).BeginInit();
            this.tabEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.Controls.Add(this.pnlEditorControls, 0, 1);
            this.pnlMain.Controls.Add(this.pnlOutputControls, 1, 1);
            this.pnlMain.Controls.Add(this.tabEditor, 0, 0);
            this.pnlMain.Controls.Add(this.rtfOutput, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlEditorControls
            // 
            this.pnlEditorControls.Controls.Add(this.btnRun);
            this.pnlEditorControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorControls.Location = new System.Drawing.Point(3, 567);
            this.pnlEditorControls.Name = "pnlEditorControls";
            this.pnlEditorControls.Size = new System.Drawing.Size(394, 30);
            this.pnlEditorControls.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRun.Location = new System.Drawing.Point(315, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // pnlOutputControls
            // 
            this.pnlOutputControls.Controls.Add(this.tlbVariables);
            this.pnlOutputControls.Controls.Add(this.updTickCount);
            this.pnlOutputControls.Controls.Add(this.btnTick);
            this.pnlOutputControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputControls.Location = new System.Drawing.Point(403, 567);
            this.pnlOutputControls.Name = "pnlOutputControls";
            this.pnlOutputControls.Size = new System.Drawing.Size(394, 30);
            this.pnlOutputControls.TabIndex = 1;
            // 
            // tlbVariables
            // 
            this.tlbVariables.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tlbVariables.Dock = System.Windows.Forms.DockStyle.None;
            this.tlbVariables.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5});
            this.tlbVariables.Location = new System.Drawing.Point(0, 4);
            this.tlbVariables.Name = "tlbVariables";
            this.tlbVariables.Size = new System.Drawing.Size(158, 25);
            this.tlbVariables.TabIndex = 2;
            this.tlbVariables.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // updTickCount
            // 
            this.updTickCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.updTickCount.Location = new System.Drawing.Point(261, 7);
            this.updTickCount.Name = "updTickCount";
            this.updTickCount.Size = new System.Drawing.Size(49, 20);
            this.updTickCount.TabIndex = 1;
            // 
            // btnTick
            // 
            this.btnTick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTick.Location = new System.Drawing.Point(316, 4);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(75, 23);
            this.btnTick.TabIndex = 0;
            this.btnTick.Text = "Tick";
            this.btnTick.UseVisualStyleBackColor = true;
            // 
            // tabEditor
            // 
            this.tabEditor.Controls.Add(this.tabPage1);
            this.tabEditor.Controls.Add(this.tabPage2);
            this.tabEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabEditor.Location = new System.Drawing.Point(3, 3);
            this.tabEditor.Name = "tabEditor";
            this.tabEditor.SelectedIndex = 0;
            this.tabEditor.Size = new System.Drawing.Size(394, 558);
            this.tabEditor.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(386, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(386, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtfOutput
            // 
            this.rtfOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfOutput.Location = new System.Drawing.Point(403, 3);
            this.rtfOutput.Name = "rtfOutput";
            this.rtfOutput.Size = new System.Drawing.Size(394, 558);
            this.rtfOutput.TabIndex = 3;
            this.rtfOutput.Text = "";
            // 
            // ctlDisplayVertical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlMain);
            this.Name = "ctlDisplayVertical";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.ResumeLayout(false);
            this.pnlEditorControls.ResumeLayout(false);
            this.pnlOutputControls.ResumeLayout(false);
            this.pnlOutputControls.PerformLayout();
            this.tlbVariables.ResumeLayout(false);
            this.tlbVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).EndInit();
            this.tabEditor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlEditorControls;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Panel pnlOutputControls;
        private System.Windows.Forms.ToolStrip tlbVariables;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.NumericUpDown updTickCount;
        private System.Windows.Forms.Button btnTick;
        private System.Windows.Forms.TabControl tabEditor;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtfOutput;
        private System.Windows.Forms.ToolStripButton toolStripButton5;

    }
}
