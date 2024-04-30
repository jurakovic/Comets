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
			btnOk = new System.Windows.Forms.Button();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lblVersion = new System.Windows.Forms.Label();
			lblMore = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			// 
			// btnOk
			// 
			btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			btnOk.Location = new System.Drawing.Point(292, 127);
			btnOk.Name = "btnOk";
			btnOk.Size = new System.Drawing.Size(100, 23);
			btnOk.TabIndex = 0;
			btnOk.Text = "OK";
			btnOk.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(17, 17);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(64, 64);
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			// 
			// lblVersion
			// 
			lblVersion.AutoSize = true;
			lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			lblVersion.Location = new System.Drawing.Point(99, 17);
			lblVersion.Name = "lblVersion";
			lblVersion.Size = new System.Drawing.Size(80, 13);
			lblVersion.TabIndex = 2;
			lblVersion.Text = "Comets 0.8.1";
			// 
			// lblMore
			// 
			lblMore.AutoSize = true;
			lblMore.Location = new System.Drawing.Point(99, 41);
			lblMore.Name = "lblMore";
			lblMore.Size = new System.Drawing.Size(27, 13);
			lblMore.TabIndex = 7;
			lblMore.Text = "(...)";
			// 
			// FormAbout
			// 
			AcceptButton = btnOk;
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			CancelButton = btnOk;
			ClientSize = new System.Drawing.Size(404, 162);
			ControlBox = false;
			Controls.Add(lblMore);
			Controls.Add(lblVersion);
			Controls.Add(pictureBox1);
			Controls.Add(btnOk);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Name = "FormAbout";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
