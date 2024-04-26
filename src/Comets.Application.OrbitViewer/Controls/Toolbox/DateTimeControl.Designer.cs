namespace Comets.Application.OrbitViewer.Controls
{
	partial class DateTimeControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pnlDateTime = new Panel();
			selectDateControl = new Common.Controls.DateAndTime.SelectDateControl();
			pnlDateTime.SuspendLayout();
			SuspendLayout();
			// 
			// pnlDateTime
			// 
			pnlDateTime.BackColor = SystemColors.ControlDark;
			pnlDateTime.Controls.Add(selectDateControl);
			pnlDateTime.Dock = DockStyle.Fill;
			pnlDateTime.Location = new Point(0, 0);
			pnlDateTime.Name = "pnlDateTime";
			pnlDateTime.Size = new Size(173, 31);
			pnlDateTime.TabIndex = 0;
			// 
			// selectDateControl
			// 
			selectDateControl.DefaultDateTime = null;
			selectDateControl.Font = new Font("Tahoma", 8.25F);
			selectDateControl.Location = new Point(4, 4);
			selectDateControl.Name = "selectDateControl";
			selectDateControl.PerihelionDate = null;
			selectDateControl.SelectedDateTime = new DateTime(0L);
			selectDateControl.Size = new Size(165, 23);
			selectDateControl.TabIndex = 0;
			// 
			// DateTimeControl
			// 
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(pnlDateTime);
			Font = new Font("Tahoma", 8.25F);
			Name = "DateTimeControl";
			Size = new Size(173, 31);
			pnlDateTime.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlDateTime;
		private Common.Controls.DateAndTime.SelectDateControl selectDateControl;
	}
}
