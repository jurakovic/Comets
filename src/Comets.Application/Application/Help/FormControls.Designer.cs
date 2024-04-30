namespace Comets.Application.Help
{
	partial class FormControls
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
			txtControls = new System.Windows.Forms.TextBox();
			btnOk = new System.Windows.Forms.Button();
			SuspendLayout();
			// 
			// txtControls
			// 
			txtControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			txtControls.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			txtControls.Location = new System.Drawing.Point(0, 0);
			txtControls.Multiline = true;
			txtControls.Name = "txtControls";
			txtControls.ReadOnly = true;
			txtControls.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			txtControls.Size = new System.Drawing.Size(564, 580);
			txtControls.TabIndex = 0;
			// 
			// btnOk
			// 
			btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOk.Location = new System.Drawing.Point(452, 596);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(100, 23);
			btnOk.TabIndex = 1;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// FormControls
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnOk;
			ClientSize = new System.Drawing.Size(564, 631);
			Controls.Add(btnOk);
			Controls.Add(txtControls);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormControls";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Controls";
			Load += FormControls_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox txtControls;
		private System.Windows.Forms.Button btnOk;
	}
}
