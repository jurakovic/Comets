namespace Comets.Application.Common.Controls.Common
{
	partial class SelectCometControl
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
			this.gbxSelectComet = new System.Windows.Forms.GroupBox();
			this.btnFilter = new System.Windows.Forms.Button();
			this.lblPeriod = new System.Windows.Forms.Label();
			this.lblPerihelionDistance = new System.Windows.Forms.Label();
			this.lblPerihelionDate = new System.Windows.Forms.Label();
			this.cbComet = new System.Windows.Forms.ComboBox();
			this.lblPerihelionDistanceValue = new System.Windows.Forms.Label();
			this.lblPerihelionDateValue = new System.Windows.Forms.Label();
			this.lblPeriodValue = new System.Windows.Forms.Label();
			this.gbxSelectComet.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxSelectComet
			// 
			this.gbxSelectComet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.gbxSelectComet.Controls.Add(this.lblPeriodValue);
			this.gbxSelectComet.Controls.Add(this.lblPerihelionDateValue);
			this.gbxSelectComet.Controls.Add(this.lblPerihelionDistanceValue);
			this.gbxSelectComet.Controls.Add(this.btnFilter);
			this.gbxSelectComet.Controls.Add(this.lblPeriod);
			this.gbxSelectComet.Controls.Add(this.lblPerihelionDistance);
			this.gbxSelectComet.Controls.Add(this.lblPerihelionDate);
			this.gbxSelectComet.Controls.Add(this.cbComet);
			this.gbxSelectComet.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.gbxSelectComet.Location = new System.Drawing.Point(0, 0);
			this.gbxSelectComet.Name = "gbxSelectComet";
			this.gbxSelectComet.Size = new System.Drawing.Size(325, 135);
			this.gbxSelectComet.TabIndex = 0;
			this.gbxSelectComet.TabStop = false;
			this.gbxSelectComet.Text = "Select comet";
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(244, 19);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(75, 24);
			this.btnFilter.TabIndex = 1;
			this.btnFilter.Text = "Filter";
			this.btnFilter.UseVisualStyleBackColor = true;
			this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
			// 
			// lblPeriod
			// 
			this.lblPeriod.AutoSize = true;
			this.lblPeriod.Location = new System.Drawing.Point(10, 104);
			this.lblPeriod.Name = "lblPeriod";
			this.lblPeriod.Size = new System.Drawing.Size(41, 13);
			this.lblPeriod.TabIndex = 6;
			this.lblPeriod.Text = "Period:";
			// 
			// lblPerihDist
			// 
			this.lblPerihelionDistance.AutoSize = true;
			this.lblPerihelionDistance.Location = new System.Drawing.Point(10, 77);
			this.lblPerihelionDistance.Name = "lblPerihDist";
			this.lblPerihelionDistance.Size = new System.Drawing.Size(100, 13);
			this.lblPerihelionDistance.TabIndex = 4;
			this.lblPerihelionDistance.Text = "Perihelion distance:";
			// 
			// lblPerihDate
			// 
			this.lblPerihelionDate.AutoSize = true;
			this.lblPerihelionDate.Location = new System.Drawing.Point(10, 50);
			this.lblPerihelionDate.Name = "lblPerihDate";
			this.lblPerihelionDate.Size = new System.Drawing.Size(82, 13);
			this.lblPerihelionDate.TabIndex = 2;
			this.lblPerihelionDate.Text = "Perihelion date:";
			// 
			// cbComet
			// 
			this.cbComet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComet.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.cbComet.FormattingEnabled = true;
			this.cbComet.IntegralHeight = false;
			this.cbComet.Location = new System.Drawing.Point(12, 20);
			this.cbComet.MaxDropDownItems = 21;
			this.cbComet.Name = "cbComet";
			this.cbComet.Size = new System.Drawing.Size(226, 22);
			this.cbComet.TabIndex = 0;
			this.cbComet.SelectedIndexChanged += new System.EventHandler(this.cbComet_SelectedIndexChanged);
			// 
			// label1
			// 
			this.lblPerihelionDistanceValue.AutoSize = true;
			this.lblPerihelionDistanceValue.Location = new System.Drawing.Point(140, 77);
			this.lblPerihelionDistanceValue.Name = "label1";
			this.lblPerihelionDistanceValue.Size = new System.Drawing.Size(49, 13);
			this.lblPerihelionDistanceValue.TabIndex = 5;
			this.lblPerihelionDistanceValue.Text = "<value>";
			// 
			// label2
			// 
			this.lblPerihelionDateValue.AutoSize = true;
			this.lblPerihelionDateValue.Location = new System.Drawing.Point(140, 50);
			this.lblPerihelionDateValue.Name = "label2";
			this.lblPerihelionDateValue.Size = new System.Drawing.Size(49, 13);
			this.lblPerihelionDateValue.TabIndex = 3;
			this.lblPerihelionDateValue.Text = "<value>";
			// 
			// label3
			// 
			this.lblPeriodValue.AutoSize = true;
			this.lblPeriodValue.Location = new System.Drawing.Point(140, 104);
			this.lblPeriodValue.Name = "label3";
			this.lblPeriodValue.Size = new System.Drawing.Size(49, 13);
			this.lblPeriodValue.TabIndex = 7;
			this.lblPeriodValue.Text = "<value>";
			// 
			// SelectCometControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gbxSelectComet);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SelectCometControl";
			this.Size = new System.Drawing.Size(325, 135);
			this.gbxSelectComet.ResumeLayout(false);
			this.gbxSelectComet.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbxSelectComet;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Label lblPeriod;
		private System.Windows.Forms.Label lblPerihelionDistance;
		private System.Windows.Forms.Label lblPerihelionDate;
		private System.Windows.Forms.ComboBox cbComet;
		private System.Windows.Forms.Label lblPerihelionDistanceValue;
		private System.Windows.Forms.Label lblPeriodValue;
		private System.Windows.Forms.Label lblPerihelionDateValue;
	}
}
