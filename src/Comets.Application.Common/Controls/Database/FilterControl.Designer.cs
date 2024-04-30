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
			btnAddNew = new System.Windows.Forms.Button();
			btnClose = new System.Windows.Forms.Button();
			btnApply = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// btnAddNew
			// 
			btnAddNew.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			btnAddNew.Location = new System.Drawing.Point(20, 7);
			btnAddNew.Name = "btnAddNew";
			btnAddNew.Size = new System.Drawing.Size(25, 23);
			btnAddNew.TabIndex = 0;
			btnAddNew.Text = "+";
			btnAddNew.UseVisualStyleBackColor = true;
			btnAddNew.Click += btnAddNew_Click;
			// 
			// btnClose
			// 
			btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnClose.Location = new System.Drawing.Point(449, 337);
			btnClose.Name = "btnClose";
			btnClose.Size = new System.Drawing.Size(100, 23);
			btnClose.TabIndex = 2;
			btnClose.Text = "Close";
			btnClose.UseVisualStyleBackColor = true;
			btnClose.Click += btnClose_Click;
			// 
			// btnApply
			// 
			btnApply.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			btnApply.Location = new System.Drawing.Point(345, 337);
			btnApply.Name = "btnApply";
			btnApply.Size = new System.Drawing.Size(100, 23);
			btnApply.TabIndex = 1;
			btnApply.Text = "Apply";
			btnApply.UseVisualStyleBackColor = true;
			btnApply.Click += btnApply_Click;
			// 
			// FilterControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(btnAddNew);
			Controls.Add(btnClose);
			Controls.Add(btnApply);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			Name = "FilterControl";
			Size = new System.Drawing.Size(549, 360);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnAddNew;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnApply;
	}
}
