namespace Comets.Application.Ephemeris
{
	partial class FormEphemeris
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
			richTextBox = new System.Windows.Forms.RichTextBox();
			SuspendLayout();
			// 
			// richTextBox
			// 
			richTextBox.BackColor = System.Drawing.SystemColors.Window;
			richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			richTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			richTextBox.Location = new System.Drawing.Point(0, 0);
			richTextBox.Name = "richTextBox";
			richTextBox.ReadOnly = true;
			richTextBox.Size = new System.Drawing.Size(784, 561);
			richTextBox.TabIndex = 0;
			richTextBox.Text = "";
			richTextBox.WordWrap = false;
			// 
			// FormEphemeris
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(784, 561);
			Controls.Add(richTextBox);
			Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			Name = "FormEphemeris";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Ephemeris";
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox;
	}
}
