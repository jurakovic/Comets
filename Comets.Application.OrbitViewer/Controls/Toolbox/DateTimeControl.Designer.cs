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
			this.pnlDateTime = new System.Windows.Forms.Panel();
			this.selectDateControl = new Comets.Application.Common.Controls.DateAndTime.SelectDateControl();
			this.pnlDateTime.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlDateTime
			// 
			this.pnlDateTime.BackColor = System.Drawing.SystemColors.ControlDark;
			this.pnlDateTime.Controls.Add(this.selectDateControl);
			this.pnlDateTime.Location = new System.Drawing.Point(0, 0);
			this.pnlDateTime.Name = "pnlDateTime";
			this.pnlDateTime.Size = new System.Drawing.Size(173, 32);
			this.pnlDateTime.TabIndex = 0;
			// 
			// selectDateControl
			// 
			this.selectDateControl.DefaultDateTime = null;
			this.selectDateControl.Location = new System.Drawing.Point(4, 4);
			this.selectDateControl.Name = "selectDateControl";
			this.selectDateControl.PerihelionDate = null;
			this.selectDateControl.SelectedDateTime = new System.DateTime(((long)(0)));
			this.selectDateControl.Size = new System.Drawing.Size(165, 23);
			this.selectDateControl.TabIndex = 0;
			// 
			// DateTimeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlDateTime);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "DateTimeControl";
			this.Size = new System.Drawing.Size(173, 32);
			this.pnlDateTime.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlDateTime;
		private Common.Controls.DateAndTime.SelectDateControl selectDateControl;
	}
}
