namespace Cometary_Workshop
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbAphDist = new System.Windows.Forms.TextBox();
            this.comboPerihDist = new System.Windows.Forms.ComboBox();
            this.chPerihDist = new System.Windows.Forms.CheckBox();
            this.comboAphDist = new System.Windows.Forms.ComboBox();
            this.tbPerihDist = new System.Windows.Forms.TextBox();
            this.chAphDist = new System.Windows.Forms.CheckBox();
            this.btnPerihDateNow = new System.Windows.Forms.Button();
            this.tbPerihDateY = new System.Windows.Forms.TextBox();
            this.tbPerihDateM = new System.Windows.Forms.TextBox();
            this.tbPerihDateD = new System.Windows.Forms.TextBox();
            this.comboName = new System.Windows.Forms.ComboBox();
            this.tbPeriod = new System.Windows.Forms.TextBox();
            this.tbIncl = new System.Windows.Forms.TextBox();
            this.tbLongPeric = new System.Windows.Forms.TextBox();
            this.tbAscNode = new System.Windows.Forms.TextBox();
            this.tbEcc = new System.Windows.Forms.TextBox();
            this.comboPeriod = new System.Windows.Forms.ComboBox();
            this.comboIncl = new System.Windows.Forms.ComboBox();
            this.comboLongPeric = new System.Windows.Forms.ComboBox();
            this.comboAscNode = new System.Windows.Forms.ComboBox();
            this.comboEcc = new System.Windows.Forms.ComboBox();
            this.comboPerihDate = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.chPeriod = new System.Windows.Forms.CheckBox();
            this.chIncl = new System.Windows.Forms.CheckBox();
            this.chLongPeric = new System.Windows.Forms.CheckBox();
            this.chAscNode = new System.Windows.Forms.CheckBox();
            this.chEcc = new System.Windows.Forms.CheckBox();
            this.chPerihDate = new System.Windows.Forms.CheckBox();
            this.chName = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbAphDist);
            this.groupBox1.Controls.Add(this.comboPerihDist);
            this.groupBox1.Controls.Add(this.chPerihDist);
            this.groupBox1.Controls.Add(this.comboAphDist);
            this.groupBox1.Controls.Add(this.tbPerihDist);
            this.groupBox1.Controls.Add(this.chAphDist);
            this.groupBox1.Controls.Add(this.btnPerihDateNow);
            this.groupBox1.Controls.Add(this.tbPerihDateY);
            this.groupBox1.Controls.Add(this.tbPerihDateM);
            this.groupBox1.Controls.Add(this.tbPerihDateD);
            this.groupBox1.Controls.Add(this.comboName);
            this.groupBox1.Controls.Add(this.tbPeriod);
            this.groupBox1.Controls.Add(this.tbIncl);
            this.groupBox1.Controls.Add(this.tbLongPeric);
            this.groupBox1.Controls.Add(this.tbAscNode);
            this.groupBox1.Controls.Add(this.tbEcc);
            this.groupBox1.Controls.Add(this.comboPeriod);
            this.groupBox1.Controls.Add(this.comboIncl);
            this.groupBox1.Controls.Add(this.comboLongPeric);
            this.groupBox1.Controls.Add(this.comboAscNode);
            this.groupBox1.Controls.Add(this.comboEcc);
            this.groupBox1.Controls.Add(this.comboPerihDate);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.chPeriod);
            this.groupBox1.Controls.Add(this.chIncl);
            this.groupBox1.Controls.Add(this.chLongPeric);
            this.groupBox1.Controls.Add(this.chAscNode);
            this.groupBox1.Controls.Add(this.chEcc);
            this.groupBox1.Controls.Add(this.chPerihDate);
            this.groupBox1.Controls.Add(this.chName);
            this.groupBox1.Location = new System.Drawing.Point(9, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 280);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tbAphDist
            // 
            this.tbAphDist.Enabled = false;
            this.tbAphDist.Location = new System.Drawing.Point(366, 102);
            this.tbAphDist.Name = "tbAphDist";
            this.tbAphDist.Size = new System.Drawing.Size(113, 21);
            this.tbAphDist.TabIndex = 61;
            // 
            // comboPerihDist
            // 
            this.comboPerihDist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerihDist.Enabled = false;
            this.comboPerihDist.FormattingEnabled = true;
            this.comboPerihDist.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPerihDist.Location = new System.Drawing.Point(249, 73);
            this.comboPerihDist.Name = "comboPerihDist";
            this.comboPerihDist.Size = new System.Drawing.Size(111, 21);
            this.comboPerihDist.TabIndex = 42;
            // 
            // chPerihDist
            // 
            this.chPerihDist.AutoSize = true;
            this.chPerihDist.Location = new System.Drawing.Point(14, 75);
            this.chPerihDist.Name = "chPerihDist";
            this.chPerihDist.Size = new System.Drawing.Size(115, 17);
            this.chPerihDist.TabIndex = 30;
            this.chPerihDist.Text = "Perihelion distance";
            this.chPerihDist.UseVisualStyleBackColor = true;
            this.chPerihDist.CheckedChanged += new System.EventHandler(this.chFilterPerihDist_CheckedChanged);
            // 
            // comboAphDist
            // 
            this.comboAphDist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAphDist.Enabled = false;
            this.comboAphDist.FormattingEnabled = true;
            this.comboAphDist.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboAphDist.Location = new System.Drawing.Point(249, 102);
            this.comboAphDist.Name = "comboAphDist";
            this.comboAphDist.Size = new System.Drawing.Size(111, 21);
            this.comboAphDist.TabIndex = 60;
            // 
            // tbPerihDist
            // 
            this.tbPerihDist.Enabled = false;
            this.tbPerihDist.Location = new System.Drawing.Point(366, 73);
            this.tbPerihDist.Name = "tbPerihDist";
            this.tbPerihDist.Size = new System.Drawing.Size(113, 21);
            this.tbPerihDist.TabIndex = 48;
            // 
            // chAphDist
            // 
            this.chAphDist.AutoSize = true;
            this.chAphDist.Location = new System.Drawing.Point(14, 104);
            this.chAphDist.Name = "chAphDist";
            this.chAphDist.Size = new System.Drawing.Size(110, 17);
            this.chAphDist.TabIndex = 59;
            this.chAphDist.Text = "Aphelion distance";
            this.chAphDist.UseVisualStyleBackColor = true;
            this.chAphDist.CheckedChanged += new System.EventHandler(this.chFilterAphDist_CheckedChanged);
            // 
            // btnPerihDateNow
            // 
            this.btnPerihDateNow.Enabled = false;
            this.btnPerihDateNow.Location = new System.Drawing.Point(482, 46);
            this.btnPerihDateNow.Name = "btnPerihDateNow";
            this.btnPerihDateNow.Size = new System.Drawing.Size(15, 15);
            this.btnPerihDateNow.TabIndex = 58;
            this.btnPerihDateNow.UseVisualStyleBackColor = true;
            this.btnPerihDateNow.Click += new System.EventHandler(this.btnPerihDateNow_Click);
            // 
            // tbPerihDateY
            // 
            this.tbPerihDateY.Enabled = false;
            this.tbPerihDateY.Location = new System.Drawing.Point(428, 44);
            this.tbPerihDateY.Name = "tbPerihDateY";
            this.tbPerihDateY.Size = new System.Drawing.Size(50, 21);
            this.tbPerihDateY.TabIndex = 57;
            // 
            // tbPerihDateM
            // 
            this.tbPerihDateM.Enabled = false;
            this.tbPerihDateM.Location = new System.Drawing.Point(397, 44);
            this.tbPerihDateM.Name = "tbPerihDateM";
            this.tbPerihDateM.Size = new System.Drawing.Size(25, 21);
            this.tbPerihDateM.TabIndex = 56;
            // 
            // tbPerihDateD
            // 
            this.tbPerihDateD.Enabled = false;
            this.tbPerihDateD.Location = new System.Drawing.Point(366, 44);
            this.tbPerihDateD.Name = "tbPerihDateD";
            this.tbPerihDateD.Size = new System.Drawing.Size(25, 21);
            this.tbPerihDateD.TabIndex = 55;
            // 
            // comboName
            // 
            this.comboName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboName.Enabled = false;
            this.comboName.FormattingEnabled = true;
            this.comboName.Items.AddRange(new object[] {
            "Contains",
            "Does not contain"});
            this.comboName.Location = new System.Drawing.Point(249, 16);
            this.comboName.Name = "comboName";
            this.comboName.Size = new System.Drawing.Size(111, 21);
            this.comboName.TabIndex = 54;
            // 
            // tbPeriod
            // 
            this.tbPeriod.Enabled = false;
            this.tbPeriod.Location = new System.Drawing.Point(366, 248);
            this.tbPeriod.Name = "tbPeriod";
            this.tbPeriod.Size = new System.Drawing.Size(113, 21);
            this.tbPeriod.TabIndex = 53;
            // 
            // tbIncl
            // 
            this.tbIncl.Enabled = false;
            this.tbIncl.Location = new System.Drawing.Point(366, 219);
            this.tbIncl.Name = "tbIncl";
            this.tbIncl.Size = new System.Drawing.Size(113, 21);
            this.tbIncl.TabIndex = 52;
            // 
            // tbLongPeric
            // 
            this.tbLongPeric.Enabled = false;
            this.tbLongPeric.Location = new System.Drawing.Point(366, 188);
            this.tbLongPeric.Name = "tbLongPeric";
            this.tbLongPeric.Size = new System.Drawing.Size(113, 21);
            this.tbLongPeric.TabIndex = 51;
            // 
            // tbAscNode
            // 
            this.tbAscNode.Enabled = false;
            this.tbAscNode.Location = new System.Drawing.Point(366, 160);
            this.tbAscNode.Name = "tbAscNode";
            this.tbAscNode.Size = new System.Drawing.Size(113, 21);
            this.tbAscNode.TabIndex = 50;
            // 
            // tbEcc
            // 
            this.tbEcc.Enabled = false;
            this.tbEcc.Location = new System.Drawing.Point(366, 132);
            this.tbEcc.Name = "tbEcc";
            this.tbEcc.Size = new System.Drawing.Size(113, 21);
            this.tbEcc.TabIndex = 49;
            // 
            // comboPeriod
            // 
            this.comboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPeriod.Enabled = false;
            this.comboPeriod.FormattingEnabled = true;
            this.comboPeriod.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPeriod.Location = new System.Drawing.Point(249, 247);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(111, 21);
            this.comboPeriod.TabIndex = 47;
            // 
            // comboIncl
            // 
            this.comboIncl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIncl.Enabled = false;
            this.comboIncl.FormattingEnabled = true;
            this.comboIncl.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboIncl.Location = new System.Drawing.Point(249, 218);
            this.comboIncl.Name = "comboIncl";
            this.comboIncl.Size = new System.Drawing.Size(111, 21);
            this.comboIncl.TabIndex = 46;
            // 
            // comboLongPeric
            // 
            this.comboLongPeric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLongPeric.Enabled = false;
            this.comboLongPeric.FormattingEnabled = true;
            this.comboLongPeric.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboLongPeric.Location = new System.Drawing.Point(249, 189);
            this.comboLongPeric.Name = "comboLongPeric";
            this.comboLongPeric.Size = new System.Drawing.Size(111, 21);
            this.comboLongPeric.TabIndex = 45;
            // 
            // comboAscNode
            // 
            this.comboAscNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAscNode.Enabled = false;
            this.comboAscNode.FormattingEnabled = true;
            this.comboAscNode.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboAscNode.Location = new System.Drawing.Point(249, 160);
            this.comboAscNode.Name = "comboAscNode";
            this.comboAscNode.Size = new System.Drawing.Size(111, 21);
            this.comboAscNode.TabIndex = 44;
            // 
            // comboEcc
            // 
            this.comboEcc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEcc.Enabled = false;
            this.comboEcc.FormattingEnabled = true;
            this.comboEcc.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboEcc.Location = new System.Drawing.Point(249, 131);
            this.comboEcc.Name = "comboEcc";
            this.comboEcc.Size = new System.Drawing.Size(111, 21);
            this.comboEcc.TabIndex = 43;
            // 
            // comboPerihDate
            // 
            this.comboPerihDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPerihDate.Enabled = false;
            this.comboPerihDate.FormattingEnabled = true;
            this.comboPerihDate.Items.AddRange(new object[] {
            "Greather than (>)",
            "Less than (<)"});
            this.comboPerihDate.Location = new System.Drawing.Point(249, 44);
            this.comboPerihDate.Name = "comboPerihDate";
            this.comboPerihDate.Size = new System.Drawing.Size(111, 21);
            this.comboPerihDate.TabIndex = 37;
            // 
            // tbName
            // 
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(366, 16);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(201, 21);
            this.tbName.TabIndex = 36;
            // 
            // chPeriod
            // 
            this.chPeriod.AutoSize = true;
            this.chPeriod.Location = new System.Drawing.Point(14, 249);
            this.chPeriod.Name = "chPeriod";
            this.chPeriod.Size = new System.Drawing.Size(56, 17);
            this.chPeriod.TabIndex = 35;
            this.chPeriod.Text = "Period";
            this.chPeriod.UseVisualStyleBackColor = true;
            this.chPeriod.CheckedChanged += new System.EventHandler(this.chFilterPeriod_CheckedChanged);
            // 
            // chIncl
            // 
            this.chIncl.AutoSize = true;
            this.chIncl.Location = new System.Drawing.Point(14, 220);
            this.chIncl.Name = "chIncl";
            this.chIncl.Size = new System.Drawing.Size(75, 17);
            this.chIncl.TabIndex = 34;
            this.chIncl.Text = "Inclination";
            this.chIncl.UseVisualStyleBackColor = true;
            this.chIncl.CheckedChanged += new System.EventHandler(this.chFilterIncl_CheckedChanged);
            // 
            // chLongPeric
            // 
            this.chLongPeric.AutoSize = true;
            this.chLongPeric.Location = new System.Drawing.Point(14, 191);
            this.chLongPeric.Name = "chLongPeric";
            this.chLongPeric.Size = new System.Drawing.Size(138, 17);
            this.chLongPeric.TabIndex = 33;
            this.chLongPeric.Text = "Longitude of Pericenter";
            this.chLongPeric.UseVisualStyleBackColor = true;
            this.chLongPeric.CheckedChanged += new System.EventHandler(this.chFilterLongPeric_CheckedChanged);
            // 
            // chAscNode
            // 
            this.chAscNode.AutoSize = true;
            this.chAscNode.Location = new System.Drawing.Point(14, 162);
            this.chAscNode.Name = "chAscNode";
            this.chAscNode.Size = new System.Drawing.Size(165, 17);
            this.chAscNode.TabIndex = 32;
            this.chAscNode.Text = "Longitude of Ascending node";
            this.chAscNode.UseVisualStyleBackColor = true;
            this.chAscNode.CheckedChanged += new System.EventHandler(this.chFilterAscNode_CheckedChanged);
            // 
            // chEcc
            // 
            this.chEcc.AutoSize = true;
            this.chEcc.Location = new System.Drawing.Point(14, 133);
            this.chEcc.Name = "chEcc";
            this.chEcc.Size = new System.Drawing.Size(81, 17);
            this.chEcc.TabIndex = 31;
            this.chEcc.Text = "Eccentricity";
            this.chEcc.UseVisualStyleBackColor = true;
            this.chEcc.CheckedChanged += new System.EventHandler(this.chFilterEcc_CheckedChanged);
            // 
            // chPerihDate
            // 
            this.chPerihDate.AutoSize = true;
            this.chPerihDate.Location = new System.Drawing.Point(14, 46);
            this.chPerihDate.Name = "chPerihDate";
            this.chPerihDate.Size = new System.Drawing.Size(97, 17);
            this.chPerihDate.TabIndex = 29;
            this.chPerihDate.Text = "Perihelion date";
            this.chPerihDate.UseVisualStyleBackColor = true;
            this.chPerihDate.CheckedChanged += new System.EventHandler(this.chFilterPerihDate_CheckedChanged);
            // 
            // chName
            // 
            this.chName.AutoSize = true;
            this.chName.Location = new System.Drawing.Point(14, 19);
            this.chName.Name = "chName";
            this.chName.Size = new System.Drawing.Size(53, 17);
            this.chName.TabIndex = 28;
            this.chName.Text = "Name";
            this.chName.UseVisualStyleBackColor = true;
            this.chName.CheckedChanged += new System.EventHandler(this.chFilterName_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnApply.Location = new System.Drawing.Point(8, 290);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(90, 23);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(495, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FiltersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 323);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FiltersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filters";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboName;
        private System.Windows.Forms.TextBox tbPeriod;
        private System.Windows.Forms.TextBox tbIncl;
        private System.Windows.Forms.TextBox tbLongPeric;
        private System.Windows.Forms.TextBox tbAscNode;
        private System.Windows.Forms.TextBox tbEcc;
        private System.Windows.Forms.TextBox tbPerihDist;
        private System.Windows.Forms.ComboBox comboPeriod;
        private System.Windows.Forms.ComboBox comboIncl;
        private System.Windows.Forms.ComboBox comboLongPeric;
        private System.Windows.Forms.ComboBox comboAscNode;
        private System.Windows.Forms.ComboBox comboEcc;
        private System.Windows.Forms.ComboBox comboPerihDist;
        private System.Windows.Forms.ComboBox comboPerihDate;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnPerihDateNow;
        private System.Windows.Forms.TextBox tbPerihDateY;
        private System.Windows.Forms.TextBox tbPerihDateM;
        private System.Windows.Forms.TextBox tbPerihDateD;
        private System.Windows.Forms.TextBox tbAphDist;
        private System.Windows.Forms.ComboBox comboAphDist;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chPeriod;
        private System.Windows.Forms.CheckBox chIncl;
        private System.Windows.Forms.CheckBox chLongPeric;
        private System.Windows.Forms.CheckBox chAscNode;
        private System.Windows.Forms.CheckBox chEcc;
        private System.Windows.Forms.CheckBox chPerihDist;
        private System.Windows.Forms.CheckBox chPerihDate;
        private System.Windows.Forms.CheckBox chName;
        private System.Windows.Forms.CheckBox chAphDist;
    }
}