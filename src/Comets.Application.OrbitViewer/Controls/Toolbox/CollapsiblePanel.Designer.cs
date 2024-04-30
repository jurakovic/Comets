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
			groupbox = new System.Windows.Forms.GroupBox();
			linkLabel = new System.Windows.Forms.LinkLabel();
			panel = new System.Windows.Forms.Panel();
			groupbox.SuspendLayout();
			SuspendLayout();
			// 
			// groupbox
			// 
			groupbox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupbox.BackColor = System.Drawing.SystemColors.Control;
			groupbox.Controls.Add(linkLabel);
			groupbox.Controls.Add(panel);
			groupbox.Location = new System.Drawing.Point(4, -2);
			groupbox.Name = "groupbox";
			groupbox.Size = new System.Drawing.Size(192, 199);
			groupbox.TabIndex = 0;
			groupbox.TabStop = false;
			groupbox.Tag = "91";
			// 
			// linkLabel
			// 
			linkLabel.ActiveLinkColor = System.Drawing.SystemColors.ControlText;
			linkLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			linkLabel.Cursor = System.Windows.Forms.Cursors.Hand;
			linkLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			linkLabel.LinkColor = System.Drawing.SystemColors.ControlText;
			linkLabel.Location = new System.Drawing.Point(3, 11);
			linkLabel.Name = "linkLabel";
			linkLabel.Size = new System.Drawing.Size(184, 13);
			linkLabel.TabIndex = 0;
			linkLabel.TabStop = true;
			linkLabel.Text = "▲  Text";
			linkLabel.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
			linkLabel.LinkClicked += linkLabel_LinkClicked;
			// 
			// panel
			// 
			panel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panel.BackColor = System.Drawing.SystemColors.Control;
			panel.Location = new System.Drawing.Point(4, 28);
			panel.Name = "panel";
			panel.Size = new System.Drawing.Size(184, 165);
			panel.TabIndex = 1;
			// 
			// CollapsiblePanel
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(groupbox);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "CollapsiblePanel";
			Size = new System.Drawing.Size(200, 200);
			groupbox.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox groupbox;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Panel panel;
	}
}
