namespace Comets.Application.Common.Controls.DateAndTime
{
	partial class SelectDateControl
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
			this.btnSelectDate = new System.Windows.Forms.Button();
			this.dateTimeMenuControl = new DateTimeMenuControl();
			this.SuspendLayout();
			// 
			// btnSelectDate
			// 
			this.btnSelectDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			this.btnSelectDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.btnSelectDate.Location = new System.Drawing.Point(0, 0);
			this.btnSelectDate.Name = "btnSelectDate";
			this.btnSelectDate.Size = new System.Drawing.Size(149, 24);
			this.btnSelectDate.TabIndex = 0;
			this.btnSelectDate.Text = "<datetime>";
			this.btnSelectDate.UseVisualStyleBackColor = true;
			this.btnSelectDate.Click += this.btnSelectDate_Click;
			// 
			// dateTimeMenuControl
			// 
			this.dateTimeMenuControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.dateTimeMenuControl.DefaultDateTime = null;
			this.dateTimeMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.dateTimeMenuControl.Location = new System.Drawing.Point(148, 0);
			this.dateTimeMenuControl.Name = "dateTimeMenuControl";
			this.dateTimeMenuControl.PerihelionDate = null;
			this.dateTimeMenuControl.ReferenceControl = null;
			this.dateTimeMenuControl.SelectedDateTime = new System.DateTime(0L);
			this.dateTimeMenuControl.Size = new System.Drawing.Size(24, 24);
			this.dateTimeMenuControl.TabIndex = 1;
			this.dateTimeMenuControl.Title = "▼";
			// 
			// SelectDateControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSelectDate);
			this.Controls.Add(this.dateTimeMenuControl);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Name = "SelectDateControl";
			this.Size = new System.Drawing.Size(172, 24);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnSelectDate;
		private DateTimeMenuControl dateTimeMenuControl;
	}
}
