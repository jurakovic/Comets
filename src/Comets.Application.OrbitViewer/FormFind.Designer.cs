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
			txtName = new TextBox();
			lbxFilter = new ListBox();
			btnCancelHidden = new Button();
			btnOkHidden = new Button();
			SuspendLayout();
			// 
			// txtName
			// 
			txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtName.BackColor = Color.Black;
			txtName.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtName.ForeColor = Color.White;
			txtName.Location = new Point(1, 1);
			txtName.Name = "txtName";
			txtName.Size = new Size(278, 22);
			txtName.TabIndex = 1;
			txtName.TextChanged += txtName_TextChanged;
			// 
			// lbxFilter
			// 
			lbxFilter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			lbxFilter.BackColor = Color.Black;
			lbxFilter.DrawMode = DrawMode.OwnerDrawFixed;
			lbxFilter.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			lbxFilter.ForeColor = Color.White;
			lbxFilter.FormattingEnabled = true;
			lbxFilter.ItemHeight = 14;
			lbxFilter.Location = new Point(1, 22);
			lbxFilter.Name = "lbxFilter";
			lbxFilter.Size = new Size(278, 102);
			lbxFilter.TabIndex = 2;
			lbxFilter.DrawItem += lbxFilter_DrawItem;
			lbxFilter.SelectedIndexChanged += lbxFilter_SelectedIndexChanged;
			lbxFilter.MouseDoubleClick += lbxFilter_MouseDoubleClick;
			// 
			// btnCancelHidden
			// 
			btnCancelHidden.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnCancelHidden.DialogResult = DialogResult.Cancel;
			btnCancelHidden.Location = new Point(193, 90);
			btnCancelHidden.Name = "btnCancelHidden";
			btnCancelHidden.Size = new Size(75, 23);
			btnCancelHidden.TabIndex = 3;
			btnCancelHidden.TabStop = false;
			btnCancelHidden.Text = "CancelHidden";
			btnCancelHidden.UseVisualStyleBackColor = true;
			// 
			// btnOkHidden
			// 
			btnOkHidden.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOkHidden.DialogResult = DialogResult.OK;
			btnOkHidden.Location = new Point(112, 90);
			btnOkHidden.Name = "btnOkHidden";
			btnOkHidden.Size = new Size(75, 23);
			btnOkHidden.TabIndex = 4;
			btnOkHidden.TabStop = false;
			btnOkHidden.Text = "OKHidden";
			btnOkHidden.UseVisualStyleBackColor = true;
			// 
			// FormFind
			// 
			AcceptButton = btnOkHidden;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnCancelHidden;
			ClientSize = new Size(280, 125);
			Controls.Add(txtName);
			Controls.Add(lbxFilter);
			Controls.Add(btnOkHidden);
			Controls.Add(btnCancelHidden);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.None;
			KeyPreview = true;
			Name = "FormFind";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.Manual;
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