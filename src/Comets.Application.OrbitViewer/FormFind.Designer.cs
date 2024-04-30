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
			this.txtName = new System.Windows.Forms.TextBox();
			this.lbxFilter = new System.Windows.Forms.ListBox();
			this.btnCancelHidden = new System.Windows.Forms.Button();
			this.btnOkHidden = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.txtName.BackColor = System.Drawing.Color.Black;
			this.txtName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.txtName.ForeColor = System.Drawing.Color.White;
			this.txtName.Location = new System.Drawing.Point(1, 1);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(278, 22);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += this.txtName_TextChanged;
			// 
			// lbxFilter
			// 
			this.lbxFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.lbxFilter.BackColor = System.Drawing.Color.Black;
			this.lbxFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbxFilter.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.lbxFilter.ForeColor = System.Drawing.Color.White;
			this.lbxFilter.FormattingEnabled = true;
			this.lbxFilter.ItemHeight = 14;
			this.lbxFilter.Location = new System.Drawing.Point(1, 22);
			this.lbxFilter.Name = "lbxFilter";
			this.lbxFilter.Size = new System.Drawing.Size(278, 102);
			this.lbxFilter.TabIndex = 2;
			this.lbxFilter.DrawItem += this.lbxFilter_DrawItem;
			this.lbxFilter.SelectedIndexChanged += this.lbxFilter_SelectedIndexChanged;
			this.lbxFilter.MouseDoubleClick += this.lbxFilter_MouseDoubleClick;
			// 
			// btnCancelHidden
			// 
			this.btnCancelHidden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnCancelHidden.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelHidden.Location = new System.Drawing.Point(193, 90);
			this.btnCancelHidden.Name = "btnCancelHidden";
			this.btnCancelHidden.Size = new System.Drawing.Size(75, 23);
			this.btnCancelHidden.TabIndex = 3;
			this.btnCancelHidden.TabStop = false;
			this.btnCancelHidden.Text = "CancelHidden";
			this.btnCancelHidden.UseVisualStyleBackColor = true;
			// 
			// btnOkHidden
			// 
			this.btnOkHidden.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnOkHidden.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOkHidden.Location = new System.Drawing.Point(112, 90);
			this.btnOkHidden.Name = "btnOkHidden";
			this.btnOkHidden.Size = new System.Drawing.Size(75, 23);
			this.btnOkHidden.TabIndex = 4;
			this.btnOkHidden.TabStop = false;
			this.btnOkHidden.Text = "OKHidden";
			this.btnOkHidden.UseVisualStyleBackColor = true;
			// 
			// FormFind
			// 
			this.AcceptButton = this.btnOkHidden;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancelHidden;
			this.ClientSize = new System.Drawing.Size(280, 125);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lbxFilter);
			this.Controls.Add(this.btnOkHidden);
			this.Controls.Add(this.btnCancelHidden);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "FormFind";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Find";
			this.Load += this.FormFind_Load;
			this.KeyDown += this.FormFind_KeyDown;
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.ListBox lbxFilter;
		private System.Windows.Forms.Button btnCancelHidden;
		private System.Windows.Forms.Button btnOkHidden;
	}
}
