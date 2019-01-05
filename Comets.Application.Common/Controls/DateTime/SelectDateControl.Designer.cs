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
			this.dateTimeMenuControl = new Comets.Application.Common.Controls.DateAndTime.DateTimeMenuControl();
			this.SuspendLayout();
			// 
			// btnSelectDate
			// 
			this.btnSelectDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSelectDate.Location = new System.Drawing.Point(0, 0);
			this.btnSelectDate.Name = "btnSelectDate";
			this.btnSelectDate.Size = new System.Drawing.Size(149, 23);
			this.btnSelectDate.TabIndex = 0;
			this.btnSelectDate.Text = "<datetime>";
			this.btnSelectDate.UseVisualStyleBackColor = true;
			this.btnSelectDate.Click += new System.EventHandler(this.btnSelectDate_Click);
			// 
			// dateTimeMenuControl
			// 
			this.dateTimeMenuControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dateTimeMenuControl.Location = new System.Drawing.Point(148, 0);
			this.dateTimeMenuControl.Name = "dateTimeMenuControl";
			this.dateTimeMenuControl.Size = new System.Drawing.Size(24, 23);
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
			this.Size = new System.Drawing.Size(172, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSelectDate;
		private DateTimeMenuControl dateTimeMenuControl;
	}
}
