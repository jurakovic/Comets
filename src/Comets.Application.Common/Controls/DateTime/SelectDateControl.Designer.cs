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
			btnSelectDate = new System.Windows.Forms.Button();
			dateTimeMenuControl = new DateTimeMenuControl();
			SuspendLayout();
			// 
			// btnSelectDate
			// 
			btnSelectDate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			btnSelectDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			btnSelectDate.Location = new System.Drawing.Point(0, 0);
			btnSelectDate.Name = "btnSelectDate";
			btnSelectDate.Size = new System.Drawing.Size(149, 23);
			btnSelectDate.TabIndex = 0;
			btnSelectDate.Text = "<datetime>";
			btnSelectDate.UseVisualStyleBackColor = true;
			btnSelectDate.Click += btnSelectDate_Click;
			// 
			// dateTimeMenuControl
			// 
			dateTimeMenuControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			dateTimeMenuControl.DefaultDateTime = null;
			dateTimeMenuControl.Font = new System.Drawing.Font("Tahoma", 8.25F);
			dateTimeMenuControl.Location = new System.Drawing.Point(148, 0);
			dateTimeMenuControl.Name = "dateTimeMenuControl";
			dateTimeMenuControl.PerihelionDate = null;
			dateTimeMenuControl.ReferenceControl = null;
			dateTimeMenuControl.SelectedDateTime = new System.DateTime(0L);
			dateTimeMenuControl.Size = new System.Drawing.Size(24, 23);
			dateTimeMenuControl.TabIndex = 1;
			dateTimeMenuControl.Title = "▼";
			// 
			// SelectDateControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(btnSelectDate);
			Controls.Add(dateTimeMenuControl);
			Font = new System.Drawing.Font("Tahoma", 8.25F);
			Name = "SelectDateControl";
			Size = new System.Drawing.Size(172, 23);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button btnSelectDate;
		private DateTimeMenuControl dateTimeMenuControl;
	}
}
