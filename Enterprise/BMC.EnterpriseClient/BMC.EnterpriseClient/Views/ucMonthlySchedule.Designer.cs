namespace BMC.EnterpriseClient
{
    partial class ucMonthlySchedule
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
            this.rdThefirstdayofmonth = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudDayOfMonth = new System.Windows.Forms.NumericUpDown();
            this.lblDay = new System.Windows.Forms.Label();
            this.cmbMonth2 = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbdayofweek = new System.Windows.Forms.ComboBox();
            this.cmbweekofmonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDayOfMonth)).BeginInit();
            this.SuspendLayout();
            // 
            // rdThefirstdayofmonth
            // 
            this.rdThefirstdayofmonth.AutoSize = true;
            this.rdThefirstdayofmonth.Location = new System.Drawing.Point(3, 37);
            this.rdThefirstdayofmonth.Name = "rdThefirstdayofmonth";
            this.rdThefirstdayofmonth.Size = new System.Drawing.Size(44, 17);
            this.rdThefirstdayofmonth.TabIndex = 1;
            this.rdThefirstdayofmonth.TabStop = true;
            this.rdThefirstdayofmonth.Text = "The";
            this.rdThefirstdayofmonth.UseVisualStyleBackColor = true;
            this.rdThefirstdayofmonth.Visible = false;
            this.rdThefirstdayofmonth.CheckedChanged += new System.EventHandler(this.rdThefirstdayofmonth_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudDayOfMonth);
            this.panel1.Controls.Add(this.lblDay);
            this.panel1.Controls.Add(this.cmbMonth2);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbdayofweek);
            this.panel1.Controls.Add(this.cmbweekofmonth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rdThefirstdayofmonth);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 58);
            this.panel1.TabIndex = 2;
            // 
            // nudDayOfMonth
            // 
            this.nudDayOfMonth.Location = new System.Drawing.Point(35, 7);
            this.nudDayOfMonth.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudDayOfMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDayOfMonth.Name = "nudDayOfMonth";
            this.nudDayOfMonth.Size = new System.Drawing.Size(70, 20);
            this.nudDayOfMonth.TabIndex = 14;
            this.nudDayOfMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Location = new System.Drawing.Point(3, 11);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(26, 13);
            this.lblDay.TabIndex = 13;
            this.lblDay.Text = "Day";
            // 
            // cmbMonth2
            // 
            this.cmbMonth2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth2.FormattingEnabled = true;
            this.cmbMonth2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbMonth2.Location = new System.Drawing.Point(289, 31);
            this.cmbMonth2.Name = "cmbMonth2";
            this.cmbMonth2.Size = new System.Drawing.Size(41, 21);
            this.cmbMonth2.TabIndex = 12;
            this.cmbMonth2.Visible = false;
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.cmbMonth.Location = new System.Drawing.Point(264, 7);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(41, 21);
            this.cmbMonth.TabIndex = 11;
            this.cmbMonth.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "month(s)";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(242, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "of every";
            this.label3.Visible = false;
            // 
            // cmbdayofweek
            // 
            this.cmbdayofweek.Enabled = false;
            this.cmbdayofweek.FormattingEnabled = true;
            this.cmbdayofweek.Items.AddRange(new object[] {
            "day",
            "weekday",
            "weekend day",
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.cmbdayofweek.Location = new System.Drawing.Point(138, 33);
            this.cmbdayofweek.Name = "cmbdayofweek";
            this.cmbdayofweek.Size = new System.Drawing.Size(98, 21);
            this.cmbdayofweek.TabIndex = 7;
            this.cmbdayofweek.Visible = false;
            // 
            // cmbweekofmonth
            // 
            this.cmbweekofmonth.Enabled = false;
            this.cmbweekofmonth.FormattingEnabled = true;
            this.cmbweekofmonth.Items.AddRange(new object[] {
            "first",
            "second",
            "third",
            "fourth",
            "last"});
            this.cmbweekofmonth.Location = new System.Drawing.Point(54, 32);
            this.cmbweekofmonth.Name = "cmbweekofmonth";
            this.cmbweekofmonth.Size = new System.Drawing.Size(78, 21);
            this.cmbweekofmonth.TabIndex = 6;
            this.cmbweekofmonth.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "1 month";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "of every";
            // 
            // ucMonthlySchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.panel1);
            this.Name = "ucMonthlySchedule";
            this.Size = new System.Drawing.Size(384, 110);
            this.Load += new System.EventHandler(this.ucMonthlySchedule_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDayOfMonth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdThefirstdayofmonth;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbweekofmonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbdayofweek;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbMonth2;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.NumericUpDown nudDayOfMonth;


    }
}
