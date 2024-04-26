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
			groupbox = new GroupBox();
			linkLabel = new LinkLabel();
			panel = new Panel();
			groupbox.SuspendLayout();
			SuspendLayout();
			// 
			// groupbox
			// 
			groupbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			groupbox.BackColor = SystemColors.Control;
			groupbox.Controls.Add(linkLabel);
			groupbox.Controls.Add(panel);
			groupbox.Location = new Point(4, -2);
			groupbox.Name = "groupbox";
			groupbox.Size = new Size(192, 199);
			groupbox.TabIndex = 0;
			groupbox.TabStop = false;
			groupbox.Tag = "91";
			// 
			// linkLabel
			// 
			linkLabel.ActiveLinkColor = SystemColors.ControlText;
			linkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			linkLabel.Cursor = Cursors.Hand;
			linkLabel.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			linkLabel.LinkBehavior = LinkBehavior.NeverUnderline;
			linkLabel.LinkColor = SystemColors.ControlText;
			linkLabel.Location = new Point(3, 11);
			linkLabel.Name = "linkLabel";
			linkLabel.Size = new Size(184, 13);
			linkLabel.TabIndex = 0;
			linkLabel.TabStop = true;
			linkLabel.Text = "▲  Text";
			linkLabel.VisitedLinkColor = SystemColors.ControlText;
			linkLabel.LinkClicked += linkLabel_LinkClicked;
			// 
			// panel
			// 
			panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			panel.BackColor = SystemColors.Control;
			panel.Location = new Point(4, 28);
			panel.Name = "panel";
			panel.Size = new Size(184, 165);
			panel.TabIndex = 1;
			// 
			// CollapsiblePanel
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(groupbox);
			Font = new Font("Tahoma", 8.25F);
			Name = "CollapsiblePanel";
			Size = new Size(200, 200);
			groupbox.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.GroupBox groupbox;
		private System.Windows.Forms.LinkLabel linkLabel;
		private System.Windows.Forms.Panel panel;
	}
}
