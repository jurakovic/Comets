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
			txtControls = new TextBox();
			btnOk = new Button();
			SuspendLayout();
			// 
			// txtControls
			// 
			txtControls.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			txtControls.Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
			txtControls.Location = new Point(0, 0);
			txtControls.Multiline = true;
			txtControls.Name = "txtControls";
			txtControls.ReadOnly = true;
			txtControls.ScrollBars = ScrollBars.Both;
			txtControls.Size = new Size(564, 580);
			txtControls.TabIndex = 0;
			// 
			// btnOk
			// 
			btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(452, 596);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(100, 23);
			btnOk.TabIndex = 1;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// FormControls
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnOk;
			ClientSize = new Size(564, 631);
			Controls.Add(btnOk);
			Controls.Add(txtControls);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "FormControls";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
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