namespace Comets.Application.ModulOrbit
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
			this.txtInfoName = new System.Windows.Forms.TextBox();
			this.lbxFilter = new System.Windows.Forms.ListBox();
			this.btnCancelHidden = new System.Windows.Forms.Button();
			this.btnOkHidden = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtInfoName
			// 
			this.txtInfoName.BackColor = System.Drawing.Color.Black;
			this.txtInfoName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.txtInfoName.ForeColor = System.Drawing.Color.White;
			this.txtInfoName.Location = new System.Drawing.Point(1, 1);
			this.txtInfoName.Name = "txtInfoName";
			this.txtInfoName.Size = new System.Drawing.Size(300, 22);
			this.txtInfoName.TabIndex = 1;
			this.txtInfoName.TextChanged += new System.EventHandler(this.txtInfoName_TextChanged);
			// 
			// lbxFilter
			// 
			this.lbxFilter.BackColor = System.Drawing.Color.Black;
			this.lbxFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbxFilter.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lbxFilter.ForeColor = System.Drawing.Color.White;
			this.lbxFilter.FormattingEnabled = true;
			this.lbxFilter.ItemHeight = 14;
			this.lbxFilter.Location = new System.Drawing.Point(1, 22);
			this.lbxFilter.Name = "lbxFilter";
			this.lbxFilter.Size = new System.Drawing.Size(300, 102);
			this.lbxFilter.TabIndex = 2;
			this.lbxFilter.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbxFilter_DrawItem);
			this.lbxFilter.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxFilter_MouseDoubleClick);
			// 
			// btnCancelHidden
			// 
			this.btnCancelHidden.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancelHidden.Location = new System.Drawing.Point(215, 90);
			this.btnCancelHidden.Name = "btnCancelHidden";
			this.btnCancelHidden.Size = new System.Drawing.Size(75, 23);
			this.btnCancelHidden.TabIndex = 3;
			this.btnCancelHidden.TabStop = false;
			this.btnCancelHidden.Text = "CancelHidden";
			this.btnCancelHidden.UseVisualStyleBackColor = true;
			// 
			// btnOkHidden
			// 
			this.btnOkHidden.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOkHidden.Location = new System.Drawing.Point(134, 90);
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
			this.ClientSize = new System.Drawing.Size(302, 125);
			this.Controls.Add(this.lbxFilter);
			this.Controls.Add(this.txtInfoName);
			this.Controls.Add(this.btnOkHidden);
			this.Controls.Add(this.btnCancelHidden);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FormFind";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Find";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFind_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtInfoName;
		private System.Windows.Forms.ListBox lbxFilter;
		private System.Windows.Forms.Button btnCancelHidden;
		private System.Windows.Forms.Button btnOkHidden;
	}
}