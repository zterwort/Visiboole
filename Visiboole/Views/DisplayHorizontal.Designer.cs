namespace VisiBoole.Views
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
            this.btnTick = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlEditorControls.SuspendLayout();
            this.pnlOutputControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlEditorControls, 0, 1);
            this.pnlMain.Controls.Add(this.pnlOutputControls, 0, 3);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 4;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlEditorControls
            // 
            this.pnlEditorControls.Controls.Add(this.btnRun);
            this.pnlEditorControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEditorControls.Location = new System.Drawing.Point(3, 267);
            this.pnlEditorControls.Name = "pnlEditorControls";
            this.pnlEditorControls.Size = new System.Drawing.Size(794, 30);
            this.pnlEditorControls.TabIndex = 2;
            // 
            // btnRun
            // 
            this.btnRun.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRun.Location = new System.Drawing.Point(715, 4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // pnlOutputControls
            // 
            this.pnlOutputControls.Controls.Add(this.btnTick);
            this.pnlOutputControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputControls.Location = new System.Drawing.Point(3, 567);
            this.pnlOutputControls.Name = "pnlOutputControls";
            this.pnlOutputControls.Size = new System.Drawing.Size(794, 30);
            this.pnlOutputControls.TabIndex = 3;
            // 
            // btnTick
            // 
            this.btnTick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTick.Location = new System.Drawing.Point(715, 5);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(75, 23);
            this.btnTick.TabIndex = 0;
            this.btnTick.Text = "Tick";
            this.btnTick.UseVisualStyleBackColor = true;
            // 
            // DisplayHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "DisplayHorizontal";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.ResumeLayout(false);
            this.pnlEditorControls.ResumeLayout(false);
            this.pnlOutputControls.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel pnlMain;
		private System.Windows.Forms.Panel pnlEditorControls;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Panel pnlOutputControls;
		private System.Windows.Forms.Button btnTick;
	}
}
