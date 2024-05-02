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
			this.btnOk = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.linkGithub = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(272, 106);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(100, 23);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			this.pictureBox1.Location = new System.Drawing.Point(17, 17);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.lblVersion.Location = new System.Drawing.Point(99, 17);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(113, 13);
			this.lblVersion.TabIndex = 2;
			this.lblVersion.Text = "Comets <version>";
			// 
			// lblAuthor
			// 
			this.lblAuthor.AutoSize = true;
			this.lblAuthor.Location = new System.Drawing.Point(99, 37);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(140, 13);
			this.lblAuthor.TabIndex = 7;
			this.lblAuthor.Text = "Copyright © 2024 jurakovic";
			// 
			// linkGithub
			// 
			this.linkGithub.ActiveLinkColor = System.Drawing.Color.Blue;
			this.linkGithub.AutoSize = true;
			this.linkGithub.Location = new System.Drawing.Point(99, 57);
			this.linkGithub.Name = "linkGithub";
			this.linkGithub.Size = new System.Drawing.Size(184, 13);
			this.linkGithub.TabIndex = 8;
			this.linkGithub.TabStop = true;
			this.linkGithub.Text = "https://github.com/jurakovic/Comets";
			this.linkGithub.VisitedLinkColor = System.Drawing.Color.Blue;
			this.linkGithub.LinkClicked += this.linkGithub_LinkClicked;
			// 
			// FormAbout
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(384, 141);
			this.ControlBox = false;
			this.Controls.Add(this.linkGithub);
			this.Controls.Add(this.lblAuthor);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "FormAbout";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.Load += this.FormAbout_Load;
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblAuthor;
		private System.Windows.Forms.LinkLabel linkGithub;
	}
}
