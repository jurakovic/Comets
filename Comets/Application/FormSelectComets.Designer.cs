namespace Comets.Application
{
	partial class FormSelectComets
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
			this.lbxLeft = new System.Windows.Forms.ListBox();
			this.lbxRight = new System.Windows.Forms.ListBox();
			this.btnFilter = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnAddAll = new System.Windows.Forms.Button();
			this.btnAddSelected = new System.Windows.Forms.Button();
			this.btnRemoveSelected = new System.Windows.Forms.Button();
			this.btnRemoveAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbxLeft
			// 
			this.lbxLeft.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lbxLeft.FormattingEnabled = true;
			this.lbxLeft.ItemHeight = 14;
			this.lbxLeft.Location = new System.Drawing.Point(12, 12);
			this.lbxLeft.Name = "lbxLeft";
			this.lbxLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbxLeft.Size = new System.Drawing.Size(238, 396);
			this.lbxLeft.TabIndex = 0;
			this.lbxLeft.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxLeft_MouseDoubleClick);
			// 
			// lbxRight
			// 
			this.lbxRight.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lbxRight.FormattingEnabled = true;
			this.lbxRight.ItemHeight = 14;
			this.lbxRight.Location = new System.Drawing.Point(439, 12);
			this.lbxRight.Name = "lbxRight";
			this.lbxRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbxRight.Size = new System.Drawing.Size(238, 396);
			this.lbxRight.TabIndex = 1;
			this.lbxRight.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxRight_MouseDoubleClick);
			// 
			// btnFilter
			// 
			this.btnFilter.Location = new System.Drawing.Point(269, 12);
			this.btnFilter.Name = "btnFilter";
			this.btnFilter.Size = new System.Drawing.Size(152, 23);
			this.btnFilter.TabIndex = 2;
			this.btnFilter.Text = "Filter";
			this.btnFilter.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(269, 385);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(152, 23);
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnAddAll
			// 
			this.btnAddAll.Location = new System.Drawing.Point(269, 99);
			this.btnAddAll.Name = "btnAddAll";
			this.btnAddAll.Size = new System.Drawing.Size(152, 23);
			this.btnAddAll.TabIndex = 3;
			this.btnAddAll.Text = "-->>";
			this.btnAddAll.UseVisualStyleBackColor = true;
			this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
			// 
			// btnAddSelected
			// 
			this.btnAddSelected.Location = new System.Drawing.Point(269, 128);
			this.btnAddSelected.Name = "btnAddSelected";
			this.btnAddSelected.Size = new System.Drawing.Size(152, 23);
			this.btnAddSelected.TabIndex = 4;
			this.btnAddSelected.Text = "->";
			this.btnAddSelected.UseVisualStyleBackColor = true;
			this.btnAddSelected.Click += new System.EventHandler(this.btnAddSelected_Click);
			// 
			// btnRemoveSelected
			// 
			this.btnRemoveSelected.Location = new System.Drawing.Point(269, 186);
			this.btnRemoveSelected.Name = "btnRemoveSelected";
			this.btnRemoveSelected.Size = new System.Drawing.Size(152, 23);
			this.btnRemoveSelected.TabIndex = 5;
			this.btnRemoveSelected.Text = "<-";
			this.btnRemoveSelected.UseVisualStyleBackColor = true;
			this.btnRemoveSelected.Click += new System.EventHandler(this.btnRemoveSelected_Click);
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.Location = new System.Drawing.Point(269, 215);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(152, 23);
			this.btnRemoveAll.TabIndex = 6;
			this.btnRemoveAll.Text = "<<--";
			this.btnRemoveAll.UseVisualStyleBackColor = true;
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// FormSelectComets
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(689, 422);
			this.Controls.Add(this.btnRemoveAll);
			this.Controls.Add(this.btnRemoveSelected);
			this.Controls.Add(this.btnAddSelected);
			this.Controls.Add(this.btnAddAll);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnFilter);
			this.Controls.Add(this.lbxRight);
			this.Controls.Add(this.lbxLeft);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectComets";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select comets";
			this.Load += new System.EventHandler(this.FormSelectComets_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox lbxLeft;
		private System.Windows.Forms.ListBox lbxRight;
		private System.Windows.Forms.Button btnFilter;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnAddAll;
		private System.Windows.Forms.Button btnAddSelected;
		private System.Windows.Forms.Button btnRemoveSelected;
		private System.Windows.Forms.Button btnRemoveAll;
	}
}