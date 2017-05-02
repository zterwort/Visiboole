namespace VisiBoole.Views
{
	partial class DisplaySingleOutput
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
            this.pnlOutputControls = new System.Windows.Forms.Panel();
            this.updTickCount = new System.Windows.Forms.NumericUpDown();
            this.btnTick = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlOutputControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlMain.Controls.Add(this.pnlOutputControls, 0, 1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlMain.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlOutputControls
            // 
            this.pnlOutputControls.Controls.Add(this.updTickCount);
            this.pnlOutputControls.Controls.Add(this.btnTick);
            this.pnlOutputControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOutputControls.Location = new System.Drawing.Point(3, 567);
            this.pnlOutputControls.Name = "pnlOutputControls";
            this.pnlOutputControls.Size = new System.Drawing.Size(794, 30);
            this.pnlOutputControls.TabIndex = 0;
            // 
            // updTickCount
            // 
            this.updTickCount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.updTickCount.Location = new System.Drawing.Point(658, 7);
            this.updTickCount.Name = "updTickCount";
            this.updTickCount.Size = new System.Drawing.Size(52, 20);
            this.updTickCount.TabIndex = 1;
            // 
            // btnTick
            // 
            this.btnTick.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnTick.Location = new System.Drawing.Point(716, 4);
            this.btnTick.Name = "btnTick";
            this.btnTick.Size = new System.Drawing.Size(75, 23);
            this.btnTick.TabIndex = 0;
            this.btnTick.Text = "Tick";
            this.btnTick.UseVisualStyleBackColor = true;
            this.btnTick.Click += new System.EventHandler(this.btnTick_Click);
            // 
            // DisplaySingleOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "DisplaySingleOutput";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.ResumeLayout(false);
            this.pnlOutputControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updTickCount)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel pnlMain;
		private System.Windows.Forms.Panel pnlOutputControls;
		private System.Windows.Forms.NumericUpDown updTickCount;
		private System.Windows.Forms.Button btnTick;
	}
}
