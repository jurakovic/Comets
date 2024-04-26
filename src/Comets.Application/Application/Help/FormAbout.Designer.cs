namespace Comets.Application.Help
{
	partial class FormAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
			btnOk = new Button();
			pictureBox1 = new PictureBox();
			lblVersion = new Label();
			lblMore = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// btnOk
			// 
			btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			btnOk.DialogResult = DialogResult.OK;
			btnOk.Location = new Point(292, 127);
			btnOk.Name = "btnOk";
			btnOk.Size = new Size(100, 23);
			btnOk.TabIndex = 0;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new Point(17, 17);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new Size(64, 64);
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			// 
			// lblVersion
			// 
			lblVersion.AutoSize = true;
			lblVersion.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 238);
			lblVersion.Location = new Point(99, 17);
			lblVersion.Name = "lblVersion";
			lblVersion.Size = new Size(80, 13);
			lblVersion.TabIndex = 2;
			lblVersion.Text = "Comets 0.8.1";
			// 
			// lblMore
			// 
			lblMore.AutoSize = true;
			lblMore.Location = new Point(99, 41);
			lblMore.Name = "lblMore";
			lblMore.Size = new Size(27, 13);
			lblMore.TabIndex = 7;
			lblMore.Text = "(...)";
			// 
			// FormAbout
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = btnOk;
			ClientSize = new Size(404, 162);
			ControlBox = false;
			Controls.Add(lblMore);
			Controls.Add(lblVersion);
			Controls.Add(pictureBox1);
			Controls.Add(btnOk);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Name = "FormAbout";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "About";
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblMore;
	}
}