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
			richTextBox = new RichTextBox();
			SuspendLayout();
			// 
			// richTextBox
			// 
			richTextBox.BackColor = SystemColors.Window;
			richTextBox.Dock = DockStyle.Fill;
			richTextBox.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
			richTextBox.Location = new Point(0, 0);
			richTextBox.Name = "richTextBox";
			richTextBox.ReadOnly = true;
			richTextBox.Size = new Size(784, 561);
			richTextBox.TabIndex = 0;
			richTextBox.Text = "";
			richTextBox.WordWrap = false;
			// 
			// FormEphemeris
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(784, 561);
			Controls.Add(richTextBox);
			Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
			Name = "FormEphemeris";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Ephemeris";
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox;
	}
}