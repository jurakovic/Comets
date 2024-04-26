namespace Comets.Application.Common.Controls.Database
{
	partial class FilterControl
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
			btnAddNew = new Button();
			btnClose = new Button();
			btnApply = new Button();
			SuspendLayout();
			// 
			// btnAddNew
			// 
			btnAddNew.Font = new Font("Tahoma", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			btnAddNew.Location = new Point(20, 7);
			btnAddNew.Name = "btnAddNew";
			btnAddNew.Size = new Size(25, 23);
			btnAddNew.TabIndex = 0;
			btnAddNew.Text = "+";
			btnAddNew.UseVisualStyleBackColor = true;
			btnAddNew.Click += btnAddNew_Click;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnClose.Location = new Point(449, 337);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(100, 23);
			btnClose.TabIndex = 2;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// btnApply
			// 
			btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnApply.Location = new Point(345, 337);
			btnApply.Name = "btnApply";
			btnApply.Size = new Size(100, 23);
			btnApply.TabIndex = 1;
			btnApply.Text = "Apply";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += btnApply_Click;
			// 
			// FilterControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnAddNew);
			Controls.Add(btnClose);
			Controls.Add(btnApply);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Name = "FilterControl";
			Size = new Size(549, 360);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnAddNew;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnApply;
	}
}
