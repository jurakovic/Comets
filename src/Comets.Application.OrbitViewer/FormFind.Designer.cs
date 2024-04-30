namespace Comets.OrbitViewer
{
	partial class FormFind
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			txtName = new System.Windows.Forms.TextBox();
			lbxFilter = new System.Windows.Forms.ListBox();
			btnCancelHidden = new System.Windows.Forms.Button();
			btnOkHidden = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// txtName
			// 
			txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtName.BackColor = System.Drawing.Color.Black;
			txtName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtName.ForeColor = System.Drawing.Color.White;
			txtName.Location = new System.Drawing.Point(1, 1);
			txtName.Name = "txtName";
			txtName.Size = new System.Drawing.Size(278, 22);
			txtName.TabIndex = 1;
			txtName.TextChanged += txtName_TextChanged;
			// 
			// lbxFilter
			// 
			lbxFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			lbxFilter.BackColor = System.Drawing.Color.Black;
			lbxFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			lbxFilter.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			lbxFilter.ForeColor = System.Drawing.Color.White;
			lbxFilter.FormattingEnabled = true;
			lbxFilter.ItemHeight = 14;
			lbxFilter.Location = new System.Drawing.Point(1, 22);
			lbxFilter.Name = "lbxFilter";
			lbxFilter.Size = new System.Drawing.Size(278, 102);
			lbxFilter.TabIndex = 2;
			lbxFilter.DrawItem += lbxFilter_DrawItem;
			lbxFilter.SelectedIndexChanged += lbxFilter_SelectedIndexChanged;
			lbxFilter.MouseDoubleClick += lbxFilter_MouseDoubleClick;
			// 
			// btnCancelHidden
			// 
			btnCancelHidden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancelHidden.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancelHidden.Location = new System.Drawing.Point(193, 90);
			btnCancelHidden.Name = "btnCancelHidden";
			btnCancelHidden.Size = new System.Drawing.Size(75, 23);
			btnCancelHidden.TabIndex = 3;
			btnCancelHidden.TabStop = false;
			btnCancelHidden.Text = "CancelHidden";
			btnCancelHidden.UseVisualStyleBackColor = true;
			// 
			// btnOkHidden
			// 
			btnOkHidden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOkHidden.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOkHidden.Location = new System.Drawing.Point(112, 90);
			btnOkHidden.Name = "btnOkHidden";
			btnOkHidden.Size = new System.Drawing.Size(75, 23);
			btnOkHidden.TabIndex = 4;
			btnOkHidden.TabStop = false;
			btnOkHidden.Text = "OKHidden";
			btnOkHidden.UseVisualStyleBackColor = true;
			// 
			// FormFind
			// 
			AcceptButton = btnOkHidden;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnCancelHidden;
			ClientSize = new System.Drawing.Size(280, 125);
			Controls.Add(txtName);
			Controls.Add(lbxFilter);
			Controls.Add(btnOkHidden);
			Controls.Add(btnCancelHidden);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			KeyPreview = true;
			Name = "FormFind";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Find";
			Load += FormFind_Load;
			KeyDown += FormFind_KeyDown;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ListBox lbxFilter;
		private System.Windows.Forms.Button btnCancelHidden;
		private System.Windows.Forms.Button btnOkHidden;
	}
}
