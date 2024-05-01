namespace Comets.Application.OrbitViewer.Controls
{
	partial class CollapsiblePanel
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
			this.groupbox = new System.Windows.Forms.GroupBox();
			this.label = new System.Windows.Forms.Label();
			this.panel = new System.Windows.Forms.Panel();
			this.groupbox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupbox
			// 
			this.groupbox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.groupbox.BackColor = System.Drawing.SystemColors.Control;
			this.groupbox.Controls.Add(this.label);
			this.groupbox.Controls.Add(this.panel);
			this.groupbox.Location = new System.Drawing.Point(4, -2);
			this.groupbox.Name = "groupbox";
			this.groupbox.Size = new System.Drawing.Size(192, 199);
			this.groupbox.TabIndex = 0;
			this.groupbox.TabStop = false;
			this.groupbox.Tag = "91";
			// 
			// label
			// 
			this.label.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.label.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.label.Location = new System.Drawing.Point(3, 11);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(184, 13);
			this.label.TabIndex = 0;
			this.label.TabStop = true;
			this.label.Text = "▲  Text";
			this.label.Click += this.label_Click;
			// 
			// panel
			// 
			this.panel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.panel.BackColor = System.Drawing.SystemColors.Control;
			this.panel.Location = new System.Drawing.Point(4, 28);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(184, 165);
			this.panel.TabIndex = 1;
			// 
			// CollapsiblePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupbox);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "CollapsiblePanel";
			this.Size = new System.Drawing.Size(200, 200);
			this.groupbox.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox groupbox;
		private System.Windows.Forms.Label label;
		private System.Windows.Forms.Panel panel;
	}
}
