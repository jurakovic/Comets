namespace Comets
{
    partial class FiltersForm
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
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.chName = new System.Windows.Forms.CheckBox();
            this.chPerihDate = new System.Windows.Forms.CheckBox();
            this.chEcc = new System.Windows.Forms.CheckBox();
            this.chAscNode = new System.Windows.Forms.CheckBox();
            this.chLongPeric = new System.Windows.Forms.CheckBox();
            this.chIncl = new System.Windows.Forms.CheckBox();
            this.chPeriod = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.comboPerihDate = new System.Windows.Forms.ComboBox();
            this.comboEcc = new System.Windows.Forms.ComboBox();
            this.comboAscNode = new System.Windows.Forms.ComboBox();
            this.comboLongPeric = new System.Windows.Forms.ComboBox();
            this.comboIncl = new System.Windows.Forms.ComboBox();
            this.comboPeriod = new System.Windows.Forms.ComboBox();
            this.tbEcc = new System.Windows.Forms.TextBox();
            this.tbAscNode = new System.Windows.Forms.TextBox();
            this.tbLongPeric = new System.Windows.Forms.TextBox();
            this.tbIncl = new System.Windows.Forms.TextBox();
            this.tbPeriod = new System.Windows.Forms.TextBox();
            this.comboName = new System.Windows.Forms.ComboBox();
            this.tbPerihDateD = new System.Windows.Forms.TextBox();
            this.tbPerihDateM = new System.Windows.Forms.TextBox();
            this.tbPerihDateY = new System.Windows.Forms.TextBox();
            this.btnPerihDateNow = new System.Windows.Forms.Button();
            this.tbPerihDist = new System.Windows.Forms.TextBox();
            this.chPerihDist = new System.Windows.Forms.CheckBox();
            this.comboPerihDist = new System.Windows.Forms.ComboBox();
            this.labelPerihDist = new System.Windows.Forms.Label();
            this.labelAcsNode = new System.Windows.Forms.Label();
            this.labelLongPeric = new System.Windows.Forms.Label();
            this.labelIncl = new System.Windows.Forms.Label();
            this.labelPeriod = new System.Windows.Forms.Label();
            this.btnCancelFilters = new System.Windows.Forms.Button();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.gbFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.labelPeriod);
            this.gbFilters.Controls.Add(this.labelIncl);
            this.gbFilters.Controls.Add(this.labelLongPeric);
            this.gbFilters.Controls.Add(this.labelAcsNode);
            this.gbFilters.Controls.Add(this.labelPerihDist);
            this.gbFilters.Controls.Add(this.comboPerihDist);
            this.gbFilters.Controls.Add(this.chPerihDist);
            this.gbFilters.Controls.Add(this.tbPerihDist);
            this.gbFilters.Controls.Add(this.btnPerihDateNow);
            this.gbFilters.Controls.Add(this.tbPerihDateY);
            this.gbFilters.Controls.Add(this.tbPerihDateM);
            this.gbFilters.Controls.Add(this.tbPerihDateD);
            this.gbFilters.Controls.Add(this.comboName);
            this.gbFilters.Controls.Add(this.tbPeriod);
            this.gbFilters.Controls.Add(this.tbIncl);
            this.gbFilters.Controls.Add(this.tbLongPeric);
            this.gbFilters.Controls.Add(this.tbAscNode);
            this.gbFilters.Controls.Add(this.tbEcc);
            this.gbFilters.Controls.Add(this.comboPeriod);
            this.gbFilters.Controls.Add(this.comboIncl);
            this.gbFilters.Controls.Add(this.comboLongPeric);
            this.gbFilters.Controls.Add(this.comboAscNode);
            this.gbFilters.Controls.Add(this.comboEcc);
            this.gbFilters.Controls.Add(this.comboPerihDate);
            this.gbFilters.Controls.Add(this.tbName);
            this.gbFilters.Controls.Add(this.chPeriod);
            this.gbFilters.Controls.Add(this.chIncl);
            this.gbFilters.Controls.Add(this.chLongPeric);
            this.gbFilters.Controls.Add(this.chAscNode);
            this.gbFilters.Controls.Add(this.chEcc);
            this.gbFilters.Controls.Add(this.chPerihDate);
            this.gbFilters.Controls.Add(this.chName);
            this.gbFilters.Location = new System.Drawing.Point(9, 4);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(490, 242);
            this.gbFilters.TabIndex = 274;
            this.gbFilters.TabStop = false;
            // 
            // chName
            // 
            this.chName.AutoSize = true;
            this.chName.Location = new System.Drawing.Point(15, 19);
            this.chName.Name = "chName";
            this.chName.Size = new System.Drawing.Size(54, 17);
            this.chName.TabIndex = 0;
            this.chName.Text = "Name";
            this.chName.UseVisualStyleBackColor = true;
            this.chName.CheckedChanged += new System.EventHandler(this.chName_CheckedChanged);
            // 
            // chPerihDate
            // 
            this.chPerihDate.AutoSize = true;
            this.chPerihDate.Location = new System.Drawing.Point(15, 46);
            this.chPerihDate.Name = "chPerihDate";
            this.chPerihDate.Size = new System.Drawing.Size(96, 17);
            this.chPerihDate.TabIndex = 2;
            this.chPerihDate.Text = "Perihelion date";
            this.chPerihDate.UseVisualStyleBackColor = true;
            this.chPerihDate.CheckedChanged += new System.EventHandler(this.chPerihDate_CheckedChanged);
            // 
            // chEcc
            // 
            this.chEcc.AutoSize = true;
            this.chEcc.Location = new System.Drawing.Point(15, 100);
            this.chEcc.Name = "chEcc";
            this.chEcc.Size = new System.Drawing.Size(81, 17);
            this.chEcc.TabIndex = 14;
            this.chEcc.Text = "Eccentricity";
            this.chEcc.UseVisualStyleBackColor = true;
            this.chEcc.CheckedChanged += new System.EventHandler(this.chEcc_CheckedChanged);
            // 
            // chAscNode
            // 
            this.chAscNode.AutoSize = true;
            this.chAscNode.Location = new System.Drawing.Point(15, 127);
            this.chAscNode.Name = "chAscNode";
            this.chAscNode.Size = new System.Drawing.Size(165, 17);
            this.chAscNode.TabIndex = 17;
            this.chAscNode.Text = "Longitude of Ascending node";
            this.chAscNode.UseVisualStyleBackColor = true;
            this.chAscNode.CheckedChanged += new System.EventHandler(this.chAscNode_CheckedChanged);
            // 
            // chLongPeric
            // 
            this.chLongPeric.AutoSize = true;
            this.chLongPeric.Location = new System.Drawing.Point(15, 154);
            this.chLongPeric.Name = "chLongPeric";
            this.chLongPeric.Size = new System.Drawing.Size(134, 17);
            this.chLongPeric.TabIndex = 20;
            this.chLongPeric.Text = "Argument of Pericenter";
            this.chLongPeric.UseVisualStyleBackColor = true;
            this.chLongPeric.CheckedChanged += new System.EventHandler(this.chLongPeric_CheckedChanged);
            // 
            // chIncl
            // 
            this.chIncl.AutoSize = true;
            this.chIncl.Location = new System.Drawing.Point(15, 181);
            this.chIncl.Name = "chIncl";
            this.chIncl.Size = new System.Drawing.Size(74, 17);
            this.chIncl.TabIndex = 23;
            this.chIncl.Text = "Inclination";
            this.chIncl.UseVisualStyleBackColor = true;
            this.chIncl.CheckedChanged += new System.EventHandler(this.chIncl_CheckedChanged);
            // 
            // chPeriod
            // 
            this.chPeriod.AutoSize = true;
            this.chPeriod.Location = new System.Drawing.Point(15, 208);
            this.chPeriod.Name = "chPeriod";
            this.chPeriod.Size = new System.Drawing.Size(56, 17);
            this.chPeriod.TabIndex = 26;
            this.chPeriod.Text = "Period";
            this.chPeriod.UseVisualStyleBackColor = true;
            this.chPeriod.CheckedChanged += new System.EventHandler(this.chPeriod_CheckedChanged);
            // 
            // tbName
            // 
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(295, 17);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(166, 20);
            this.tbName.TabIndex = 1;
            // 
            // comboPerihDate
            // 
            this.comboPerihDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerihDate.Enabled = false;
            this.comboPerihDate.FormattingEnabled = true;
            this.comboPerihDate.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPerihDate.Location = new System.Drawing.Point(178, 44);
            this.comboPerihDate.Name = "comboPerihDate";
            this.comboPerihDate.Size = new System.Drawing.Size(111, 21);
            this.comboPerihDate.TabIndex = 3;
            // 
            // comboEcc
            // 
            this.comboEcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEcc.Enabled = false;
            this.comboEcc.FormattingEnabled = true;
            this.comboEcc.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboEcc.Location = new System.Drawing.Point(178, 98);
            this.comboEcc.Name = "comboEcc";
            this.comboEcc.Size = new System.Drawing.Size(111, 21);
            this.comboEcc.TabIndex = 15;
            // 
            // comboAscNode
            // 
            this.comboAscNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAscNode.Enabled = false;
            this.comboAscNode.FormattingEnabled = true;
            this.comboAscNode.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboAscNode.Location = new System.Drawing.Point(178, 125);
            this.comboAscNode.Name = "comboAscNode";
            this.comboAscNode.Size = new System.Drawing.Size(111, 21);
            this.comboAscNode.TabIndex = 18;
            // 
            // comboLongPeric
            // 
            this.comboLongPeric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLongPeric.Enabled = false;
            this.comboLongPeric.FormattingEnabled = true;
            this.comboLongPeric.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboLongPeric.Location = new System.Drawing.Point(178, 152);
            this.comboLongPeric.Name = "comboLongPeric";
            this.comboLongPeric.Size = new System.Drawing.Size(111, 21);
            this.comboLongPeric.TabIndex = 21;
            // 
            // comboIncl
            // 
            this.comboIncl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIncl.Enabled = false;
            this.comboIncl.FormattingEnabled = true;
            this.comboIncl.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboIncl.Location = new System.Drawing.Point(178, 179);
            this.comboIncl.Name = "comboIncl";
            this.comboIncl.Size = new System.Drawing.Size(111, 21);
            this.comboIncl.TabIndex = 24;
            // 
            // comboPeriod
            // 
            this.comboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPeriod.Enabled = false;
            this.comboPeriod.FormattingEnabled = true;
            this.comboPeriod.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPeriod.Location = new System.Drawing.Point(178, 206);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(111, 21);
            this.comboPeriod.TabIndex = 27;
            // 
            // tbEcc
            // 
            this.tbEcc.Enabled = false;
            this.tbEcc.Location = new System.Drawing.Point(295, 99);
            this.tbEcc.Name = "tbEcc";
            this.tbEcc.Size = new System.Drawing.Size(113, 20);
            this.tbEcc.TabIndex = 16;
            // 
            // tbAscNode
            // 
            this.tbAscNode.Enabled = false;
            this.tbAscNode.Location = new System.Drawing.Point(295, 125);
            this.tbAscNode.Name = "tbAscNode";
            this.tbAscNode.Size = new System.Drawing.Size(113, 20);
            this.tbAscNode.TabIndex = 19;
            // 
            // tbLongPeric
            // 
            this.tbLongPeric.Enabled = false;
            this.tbLongPeric.Location = new System.Drawing.Point(295, 151);
            this.tbLongPeric.Name = "tbLongPeric";
            this.tbLongPeric.Size = new System.Drawing.Size(113, 20);
            this.tbLongPeric.TabIndex = 22;
            // 
            // tbIncl
            // 
            this.tbIncl.Enabled = false;
            this.tbIncl.Location = new System.Drawing.Point(295, 180);
            this.tbIncl.Name = "tbIncl";
            this.tbIncl.Size = new System.Drawing.Size(113, 20);
            this.tbIncl.TabIndex = 25;
            // 
            // tbPeriod
            // 
            this.tbPeriod.Enabled = false;
            this.tbPeriod.Location = new System.Drawing.Point(295, 207);
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(113, 20);
            this.tbPeriod.TabIndex = 28;
            // 
            // comboName
            // 
            this.comboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboName.Enabled = false;
            this.comboName.FormattingEnabled = true;
            this.comboName.Items.AddRange(new object[] {
            "Contains",
            "Does not contain"});
            this.comboName.Location = new System.Drawing.Point(178, 17);
            this.comboName.Name = "comboName";
            this.comboName.Size = new System.Drawing.Size(111, 21);
            this.comboName.TabIndex = 0;
            // 
            // tbPerihDateD
            // 
            this.tbPerihDateD.Enabled = false;
            this.tbPerihDateD.Location = new System.Drawing.Point(295, 44);
            this.tbPerihDateD.Name = "tbPerihDateD";
            this.tbPerihDateD.Size = new System.Drawing.Size(25, 20);
            this.tbPerihDateD.TabIndex = 4;
            // 
            // tbPerihDateM
            // 
            this.tbPerihDateM.Enabled = false;
            this.tbPerihDateM.Location = new System.Drawing.Point(326, 44);
            this.tbPerihDateM.Name = "tbPerihDateM";
            this.tbPerihDateM.Size = new System.Drawing.Size(25, 20);
            this.tbPerihDateM.TabIndex = 5;
            // 
            // tbPerihDateY
            // 
            this.tbPerihDateY.Enabled = false;
            this.tbPerihDateY.Location = new System.Drawing.Point(357, 44);
            this.tbPerihDateY.Name = "tbPerihDateY";
            this.tbPerihDateY.Size = new System.Drawing.Size(50, 20);
            this.tbPerihDateY.TabIndex = 6;
            // 
            // btnPerihDateNow
            // 
            this.btnPerihDateNow.Enabled = false;
            this.btnPerihDateNow.Location = new System.Drawing.Point(411, 46);
            this.btnPerihDateNow.Name = "btnPerihDateNow";
            this.btnPerihDateNow.Size = new System.Drawing.Size(15, 15);
            this.btnPerihDateNow.TabIndex = 7;
            this.btnPerihDateNow.UseVisualStyleBackColor = true;
            this.btnPerihDateNow.Click += new System.EventHandler(this.btnPerihDateNow_Click);
            // 
            // tbPerihDist
            // 
            this.tbPerihDist.Enabled = false;
            this.tbPerihDist.Location = new System.Drawing.Point(295, 71);
            this.tbPerihDist.Name = "tbPerihDist";
            this.tbPerihDist.Size = new System.Drawing.Size(113, 20);
            this.tbPerihDist.TabIndex = 10;
            // 
            // chPerihDist
            // 
            this.chPerihDist.AutoSize = true;
            this.chPerihDist.Location = new System.Drawing.Point(15, 73);
            this.chPerihDist.Name = "chPerihDist";
            this.chPerihDist.Size = new System.Drawing.Size(115, 17);
            this.chPerihDist.TabIndex = 8;
            this.chPerihDist.Text = "Perihelion distance";
            this.chPerihDist.UseVisualStyleBackColor = true;
            this.chPerihDist.CheckedChanged += new System.EventHandler(this.chPerihDist_CheckedChanged);
            // 
            // comboPerihDist
            // 
            this.comboPerihDist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerihDist.Enabled = false;
            this.comboPerihDist.FormattingEnabled = true;
            this.comboPerihDist.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPerihDist.Location = new System.Drawing.Point(178, 71);
            this.comboPerihDist.Name = "comboPerihDist";
            this.comboPerihDist.Size = new System.Drawing.Size(111, 21);
            this.comboPerihDist.TabIndex = 9;
            // 
            // labelPerihDist
            // 
            this.labelPerihDist.AutoSize = true;
            this.labelPerihDist.Enabled = false;
            this.labelPerihDist.Location = new System.Drawing.Point(409, 74);
            this.labelPerihDist.Name = "labelPerihDist";
            this.labelPerihDist.Size = new System.Drawing.Size(22, 13);
            this.labelPerihDist.TabIndex = 33;
            this.labelPerihDist.Text = "AU";
            // 
            // labelAcsNode
            // 
            this.labelAcsNode.AutoSize = true;
            this.labelAcsNode.Enabled = false;
            this.labelAcsNode.Location = new System.Drawing.Point(409, 128);
            this.labelAcsNode.Name = "labelAcsNode";
            this.labelAcsNode.Size = new System.Drawing.Size(11, 13);
            this.labelAcsNode.TabIndex = 34;
            this.labelAcsNode.Text = "°";
            // 
            // labelLongPeric
            // 
            this.labelLongPeric.AutoSize = true;
            this.labelLongPeric.Enabled = false;
            this.labelLongPeric.Location = new System.Drawing.Point(409, 154);
            this.labelLongPeric.Name = "labelLongPeric";
            this.labelLongPeric.Size = new System.Drawing.Size(11, 13);
            this.labelLongPeric.TabIndex = 35;
            this.labelLongPeric.Text = "°";
            // 
            // labelIncl
            // 
            this.labelIncl.AutoSize = true;
            this.labelIncl.Enabled = false;
            this.labelIncl.Location = new System.Drawing.Point(409, 183);
            this.labelIncl.Name = "labelIncl";
            this.labelIncl.Size = new System.Drawing.Size(11, 13);
            this.labelIncl.TabIndex = 36;
            this.labelIncl.Text = "°";
            // 
            // labelPeriod
            // 
            this.labelPeriod.AutoSize = true;
            this.labelPeriod.Enabled = false;
            this.labelPeriod.Location = new System.Drawing.Point(409, 210);
            this.labelPeriod.Name = "labelPeriod";
            this.labelPeriod.Size = new System.Drawing.Size(32, 13);
            this.labelPeriod.TabIndex = 37;
            this.labelPeriod.Text = "years";
            // 
            // btnCancelFilters
            // 
            this.btnCancelFilters.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCancelFilters.Location = new System.Drawing.Point(386, 252);
            this.btnCancelFilters.Name = "btnCancelFilters";
            this.btnCancelFilters.Size = new System.Drawing.Size(113, 23);
            this.btnCancelFilters.TabIndex = 276;
            this.btnCancelFilters.Text = "Cancel";
            this.btnCancelFilters.UseVisualStyleBackColor = true;
            this.btnCancelFilters.Click += new System.EventHandler(this.btnCancelFilters_Click);
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnApplyFilters.Location = new System.Drawing.Point(9, 252);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(113, 23);
            this.btnApplyFilters.TabIndex = 275;
            this.btnApplyFilters.Text = "Apply";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
            // 
            // FiltersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 285);
            this.Controls.Add(this.btnCancelFilters);
            this.Controls.Add(this.btnApplyFilters);
            this.Controls.Add(this.gbFilters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiltersForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filters";
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.Label labelPeriod;
        private System.Windows.Forms.Label labelIncl;
        private System.Windows.Forms.Label labelLongPeric;
        private System.Windows.Forms.Label labelAcsNode;
        private System.Windows.Forms.Label labelPerihDist;
        private System.Windows.Forms.ComboBox comboPerihDist;
        private System.Windows.Forms.CheckBox chPerihDist;
        private System.Windows.Forms.TextBox tbPerihDist;
        private System.Windows.Forms.Button btnPerihDateNow;
        private System.Windows.Forms.TextBox tbPerihDateY;
        private System.Windows.Forms.TextBox tbPerihDateM;
        private System.Windows.Forms.TextBox tbPerihDateD;
        private System.Windows.Forms.ComboBox comboName;
        private System.Windows.Forms.TextBox tbPeriod;
        private System.Windows.Forms.TextBox tbIncl;
        private System.Windows.Forms.TextBox tbLongPeric;
        private System.Windows.Forms.TextBox tbAscNode;
        private System.Windows.Forms.TextBox tbEcc;
        private System.Windows.Forms.ComboBox comboPeriod;
        private System.Windows.Forms.ComboBox comboIncl;
        private System.Windows.Forms.ComboBox comboLongPeric;
        private System.Windows.Forms.ComboBox comboAscNode;
        private System.Windows.Forms.ComboBox comboEcc;
        private System.Windows.Forms.ComboBox comboPerihDate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.CheckBox chPeriod;
        private System.Windows.Forms.CheckBox chIncl;
        private System.Windows.Forms.CheckBox chLongPeric;
        private System.Windows.Forms.CheckBox chAscNode;
        private System.Windows.Forms.CheckBox chEcc;
        private System.Windows.Forms.CheckBox chPerihDate;
        private System.Windows.Forms.CheckBox chName;
        private System.Windows.Forms.Button btnCancelFilters;
        private System.Windows.Forms.Button btnApplyFilters;


    }
}