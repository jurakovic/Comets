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
			pnlDateTime = new System.Windows.Forms.Panel();
			selectDateControl = new Common.Controls.DateAndTime.SelectDateControl();
			pnlDateTime.SuspendLayout();
			SuspendLayout();
			// 
			// pnlDateTime
			// 
			pnlDateTime.BackColor = System.Drawing.SystemColors.ControlDark;
			pnlDateTime.Controls.Add(selectDateControl);
			pnlDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlDateTime.Location = new System.Drawing.Point(0, 0);
			pnlDateTime.Name = "pnlDateTime";
			pnlDateTime.Size = new System.Drawing.Size(173, 31);
			pnlDateTime.TabIndex = 0;
			// 
			// selectDateControl
			// 
			selectDateControl.DefaultDateTime = null;
			selectDateControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			selectDateControl.Location = new System.Drawing.Point(4, 4);
			selectDateControl.Name = "selectDateControl";
			selectDateControl.PerihelionDate = null;
			selectDateControl.SelectedDateTime = new System.DateTime(0L);
			selectDateControl.Size = new System.Drawing.Size(165, 23);
			selectDateControl.TabIndex = 0;
			// 
			// DateTimeControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(pnlDateTime);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "DateTimeControl";
			Size = new System.Drawing.Size(173, 31);
			pnlDateTime.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlDateTime;
		private Common.Controls.DateAndTime.SelectDateControl selectDateControl;
	}
}
