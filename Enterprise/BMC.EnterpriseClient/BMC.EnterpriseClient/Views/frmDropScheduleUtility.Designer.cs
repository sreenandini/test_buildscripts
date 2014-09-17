namespace BMC.EnterpriseClient.Views
{
    partial class frmDropScheduleUtility
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDropScheduleUtility));
            this.tabDropSchedule = new System.Windows.Forms.TabControl();
            this.tbAutomaticschedule = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHourMinute = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.nudASStackerLevel = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPercAutomactic = new System.Windows.Forms.Label();
            this.lblStackerlevelauto = new System.Windows.Forms.Label();
            this.dtpScheduletime = new System.Windows.Forms.DateTimePicker();
            this.lblSchedule = new System.Windows.Forms.Label();
            this.tableSchedulePanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbMonthly = new System.Windows.Forms.RadioButton();
            this.rdbDaily = new System.Windows.Forms.RadioButton();
            this.rdbWeekly = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ucMS = new BMC.EnterpriseClient.ucMonthlySchedule();
            this.ucWS = new BMC.EnterpriseClient.Views.ucWeeklySchedule();
            this.ucRR = new BMC.EnterpriseClient.Views.ucRecurrenceRange();
            this.tbManualschedule = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.nudMSStackerLevel = new System.Windows.Forms.NumericUpDown();
            this.lblPercManual = new System.Windows.Forms.Label();
            this.lblStackerlevelmanual = new System.Windows.Forms.Label();
            this.cmbSite = new BMC.Common.Utilities.BmcComboBox();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSendDropAlert = new System.Windows.Forms.Button();
            this.btnPrintschedule = new System.Windows.Forms.Button();
            this.tabDropSchedule.SuspendLayout();
            this.tbAutomaticschedule.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudASStackerLevel)).BeginInit();
            this.tableSchedulePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbManualschedule.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMSStackerLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // tabDropSchedule
            // 
            this.tabDropSchedule.Controls.Add(this.tbAutomaticschedule);
            this.tabDropSchedule.Controls.Add(this.tbManualschedule);
            this.tabDropSchedule.Location = new System.Drawing.Point(0, 0);
            this.tabDropSchedule.Name = "tabDropSchedule";
            this.tabDropSchedule.Padding = new System.Drawing.Point(50, 3);
            this.tabDropSchedule.SelectedIndex = 0;
            this.tabDropSchedule.Size = new System.Drawing.Size(560, 400);
            this.tabDropSchedule.TabIndex = 0;
            this.tabDropSchedule.SelectedIndexChanged += new System.EventHandler(this.DropScheduleTab_SelectedIndexChanged);
            // 
            // tbAutomaticschedule
            // 
            this.tbAutomaticschedule.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbAutomaticschedule.Controls.Add(this.tableLayoutPanel1);
            this.tbAutomaticschedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbAutomaticschedule.Location = new System.Drawing.Point(4, 22);
            this.tbAutomaticschedule.Name = "tbAutomaticschedule";
            this.tbAutomaticschedule.Padding = new System.Windows.Forms.Padding(3);
            this.tbAutomaticschedule.Size = new System.Drawing.Size(552, 374);
            this.tbAutomaticschedule.TabIndex = 0;
            this.tbAutomaticschedule.Text = "Automatic Schedule";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableSchedulePanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ucRR, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(550, 360);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblHourMinute);
            this.panel1.Controls.Add(this.chkIsActive);
            this.panel1.Controls.Add(this.nudASStackerLevel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblPercAutomactic);
            this.panel1.Controls.Add(this.lblStackerlevelauto);
            this.panel1.Controls.Add(this.dtpScheduletime);
            this.panel1.Controls.Add(this.lblSchedule);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(536, 66);
            this.panel1.TabIndex = 0;
            // 
            // lblHourMinute
            // 
            this.lblHourMinute.AutoSize = true;
            this.lblHourMinute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourMinute.Location = new System.Drawing.Point(226, 42);
            this.lblHourMinute.Name = "lblHourMinute";
            this.lblHourMinute.Size = new System.Drawing.Size(44, 13);
            this.lblHourMinute.TabIndex = 3;
            this.lblHourMinute.Text = "HH:MM";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsActive.Location = new System.Drawing.Point(462, 41);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(67, 17);
            this.chkIsActive.TabIndex = 7;
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            this.chkIsActive.Visible = false;
            // 
            // nudASStackerLevel
            // 
            this.nudASStackerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudASStackerLevel.Location = new System.Drawing.Point(357, 38);
            this.nudASStackerLevel.Name = "nudASStackerLevel";
            this.nudASStackerLevel.Size = new System.Drawing.Size(72, 20);
            this.nudASStackerLevel.TabIndex = 5;
            this.nudASStackerLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Automatic Scheduler for all Regions and Sites";
            // 
            // lblPercAutomactic
            // 
            this.lblPercAutomactic.AutoSize = true;
            this.lblPercAutomactic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercAutomactic.Location = new System.Drawing.Point(433, 42);
            this.lblPercAutomactic.Name = "lblPercAutomactic";
            this.lblPercAutomactic.Size = new System.Drawing.Size(15, 13);
            this.lblPercAutomactic.TabIndex = 6;
            this.lblPercAutomactic.Text = "%";
            // 
            // lblStackerlevelauto
            // 
            this.lblStackerlevelauto.AutoSize = true;
            this.lblStackerlevelauto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStackerlevelauto.Location = new System.Drawing.Point(279, 42);
            this.lblStackerlevelauto.Name = "lblStackerlevelauto";
            this.lblStackerlevelauto.Size = new System.Drawing.Size(76, 13);
            this.lblStackerlevelauto.TabIndex = 4;
            this.lblStackerlevelauto.Text = "Stacker Level:";
            // 
            // dtpScheduletime
            // 
            this.dtpScheduletime.CustomFormat = "HH:mm";
            this.dtpScheduletime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpScheduletime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduletime.Location = new System.Drawing.Point(115, 38);
            this.dtpScheduletime.Name = "dtpScheduletime";
            this.dtpScheduletime.ShowUpDown = true;
            this.dtpScheduletime.Size = new System.Drawing.Size(110, 20);
            this.dtpScheduletime.TabIndex = 2;
            this.dtpScheduletime.Value = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            // 
            // lblSchedule
            // 
            this.lblSchedule.AutoSize = true;
            this.lblSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchedule.Location = new System.Drawing.Point(7, 42);
            this.lblSchedule.Name = "lblSchedule";
            this.lblSchedule.Size = new System.Drawing.Size(102, 13);
            this.lblSchedule.TabIndex = 1;
            this.lblSchedule.Text = "Generate Schedule:";
            // 
            // tableSchedulePanel
            // 
            this.tableSchedulePanel.ColumnCount = 2;
            this.tableSchedulePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableSchedulePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableSchedulePanel.Controls.Add(this.groupBox1, 0, 0);
            this.tableSchedulePanel.Controls.Add(this.panel2, 1, 0);
            this.tableSchedulePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableSchedulePanel.Location = new System.Drawing.Point(3, 75);
            this.tableSchedulePanel.Name = "tableSchedulePanel";
            this.tableSchedulePanel.RowCount = 1;
            this.tableSchedulePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableSchedulePanel.Size = new System.Drawing.Size(544, 120);
            this.tableSchedulePanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbMonthly);
            this.groupBox1.Controls.Add(this.rdbDaily);
            this.groupBox1.Controls.Add(this.rdbWeekly);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(102, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // rdbMonthly
            // 
            this.rdbMonthly.AutoSize = true;
            this.rdbMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMonthly.Location = new System.Drawing.Point(10, 80);
            this.rdbMonthly.Name = "rdbMonthly";
            this.rdbMonthly.Size = new System.Drawing.Size(62, 17);
            this.rdbMonthly.TabIndex = 2;
            this.rdbMonthly.Text = "Monthly";
            this.rdbMonthly.UseVisualStyleBackColor = true;
            this.rdbMonthly.CheckedChanged += new System.EventHandler(this.rdMonthly_CheckedChanged);
            // 
            // rdbDaily
            // 
            this.rdbDaily.AutoSize = true;
            this.rdbDaily.Checked = true;
            this.rdbDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDaily.Location = new System.Drawing.Point(10, 20);
            this.rdbDaily.Name = "rdbDaily";
            this.rdbDaily.Size = new System.Drawing.Size(48, 17);
            this.rdbDaily.TabIndex = 0;
            this.rdbDaily.TabStop = true;
            this.rdbDaily.Text = "Daily";
            this.rdbDaily.UseVisualStyleBackColor = true;
            this.rdbDaily.CheckedChanged += new System.EventHandler(this.rdDaily_CheckedChanged);
            // 
            // rdbWeekly
            // 
            this.rdbWeekly.AutoSize = true;
            this.rdbWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbWeekly.Location = new System.Drawing.Point(10, 50);
            this.rdbWeekly.Name = "rdbWeekly";
            this.rdbWeekly.Size = new System.Drawing.Size(61, 17);
            this.rdbWeekly.TabIndex = 1;
            this.rdbWeekly.Text = "Weekly";
            this.rdbWeekly.UseVisualStyleBackColor = true;
            this.rdbWeekly.CheckedChanged += new System.EventHandler(this.rdWeekly_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ucMS);
            this.panel2.Controls.Add(this.ucWS);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(111, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 114);
            this.panel2.TabIndex = 1;
            // 
            // ucMS
            // 
            this.ucMS.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucMS.DateOfMonth = 1;
            this.ucMS.Day = "System.Windows.Forms.ComboBox, Items.Count: 10";
            this.ucMS.Location = new System.Drawing.Point(4, 1);
            this.ucMS.MonthDuration = 1;
            this.ucMS.Name = "ucMS";
            this.ucMS.Size = new System.Drawing.Size(384, 110);
            this.ucMS.TabIndex = 0;
            this.ucMS.Week = "System.Windows.Forms.ComboBox, Items.Count: 5";
            // 
            // ucWS
            // 
            this.ucWS.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ucWS.Location = new System.Drawing.Point(3, 3);
            this.ucWS.Name = "ucWS";
            this.ucWS.SelectedWeekDays = 0;
            this.ucWS.Size = new System.Drawing.Size(360, 108);
            this.ucWS.TabIndex = 0;
            // 
            // ucRR
            // 
            this.ucRR.EndAfterOccurrence = false;
            this.ucRR.EndByDate = false;
            this.ucRR.EndDate = new System.DateTime(2012, 7, 10, 0, 0, 0, 0);
            this.ucRR.EndOption = BMC.EnterpriseBusiness.Entities.EndOptions.NoEndDate;
            this.ucRR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucRR.Location = new System.Drawing.Point(3, 201);
            this.ucRR.Name = "ucRR";
            this.ucRR.NoEndDate = true;
            this.ucRR.Occurrence = 0;
            this.ucRR.Size = new System.Drawing.Size(543, 119);
            this.ucRR.StartDate = new System.DateTime(2012, 7, 10, 0, 0, 0, 0);
            this.ucRR.TabIndex = 2;
            // 
            // tbManualschedule
            // 
            this.tbManualschedule.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbManualschedule.Controls.Add(this.panel3);
            this.tbManualschedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbManualschedule.Location = new System.Drawing.Point(4, 22);
            this.tbManualschedule.Name = "tbManualschedule";
            this.tbManualschedule.Padding = new System.Windows.Forms.Padding(3);
            this.tbManualschedule.Size = new System.Drawing.Size(552, 374);
            this.tbManualschedule.TabIndex = 1;
            this.tbManualschedule.Text = "Manual Schedule";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.nudMSStackerLevel);
            this.panel3.Controls.Add(this.lblPercManual);
            this.panel3.Controls.Add(this.lblStackerlevelmanual);
            this.panel3.Controls.Add(this.cmbSite);
            this.panel3.Controls.Add(this.cmbRegion);
            this.panel3.Controls.Add(this.lblSite);
            this.panel3.Controls.Add(this.lblRegion);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(550, 117);
            this.panel3.TabIndex = 0;
            // 
            // nudMSStackerLevel
            // 
            this.nudMSStackerLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMSStackerLevel.Location = new System.Drawing.Point(93, 64);
            this.nudMSStackerLevel.Name = "nudMSStackerLevel";
            this.nudMSStackerLevel.Size = new System.Drawing.Size(72, 20);
            this.nudMSStackerLevel.TabIndex = 8;
            this.nudMSStackerLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMSStackerLevel.ValueChanged += new System.EventHandler(this.nudMSStackerLevel_ValueChanged);
            // 
            // lblPercManual
            // 
            this.lblPercManual.AutoSize = true;
            this.lblPercManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercManual.Location = new System.Drawing.Point(171, 68);
            this.lblPercManual.Name = "lblPercManual";
            this.lblPercManual.Size = new System.Drawing.Size(15, 13);
            this.lblPercManual.TabIndex = 7;
            this.lblPercManual.Text = "%";
            // 
            // lblStackerlevelmanual
            // 
            this.lblStackerlevelmanual.AutoSize = true;
            this.lblStackerlevelmanual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStackerlevelmanual.Location = new System.Drawing.Point(8, 64);
            this.lblStackerlevelmanual.Name = "lblStackerlevelmanual";
            this.lblStackerlevelmanual.Size = new System.Drawing.Size(76, 13);
            this.lblStackerlevelmanual.TabIndex = 5;
            this.lblStackerlevelmanual.Text = "Stacker Level:";
            // 
            // cmbSite
            // 
            this.cmbSite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Items.AddRange(new object[] {
            "All"});
            this.cmbSite.Location = new System.Drawing.Point(318, 16);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(121, 21);
            this.cmbSite.TabIndex = 3;
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.cmbSite_SelectedIndexChanged);
            // 
            // cmbRegion
            // 
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(93, 16);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(121, 21);
            this.cmbRegion.TabIndex = 2;
            this.cmbRegion.SelectedIndexChanged += new System.EventHandler(this.cmbRegion_SelectedIndexChanged);
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(268, 19);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(28, 13);
            this.lblSite.TabIndex = 1;
            this.lblSite.Text = "Site:";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegion.Location = new System.Drawing.Point(8, 19);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(44, 13);
            this.lblRegion.TabIndex = 0;
            this.lblRegion.Text = "Region:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(344, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(450, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Cl&ose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSendDropAlert
            // 
            this.btnSendDropAlert.Location = new System.Drawing.Point(13, 357);
            this.btnSendDropAlert.Name = "btnSendDropAlert";
            this.btnSendDropAlert.Size = new System.Drawing.Size(100, 23);
            this.btnSendDropAlert.TabIndex = 1;
            this.btnSendDropAlert.Text = "&Send Drop Alert";
            this.btnSendDropAlert.UseVisualStyleBackColor = true;
            this.btnSendDropAlert.Click += new System.EventHandler(this.btnSenddropalert_Click);
            // 
            // btnPrintschedule
            // 
            this.btnPrintschedule.Location = new System.Drawing.Point(119, 357);
            this.btnPrintschedule.Name = "btnPrintschedule";
            this.btnPrintschedule.Size = new System.Drawing.Size(100, 23);
            this.btnPrintschedule.TabIndex = 2;
            this.btnPrintschedule.Text = "&Print Schedule";
            this.btnPrintschedule.UseVisualStyleBackColor = true;
            // 
            // frmDropScheduleUtility
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(558, 397);
            this.Controls.Add(this.btnPrintschedule);
            this.Controls.Add(this.btnSendDropAlert);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabDropSchedule);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDropScheduleUtility";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drop Schedule Utility";
            this.Load += new System.EventHandler(this.frmDropScheduleUtility_Load);
            this.tabDropSchedule.ResumeLayout(false);
            this.tbAutomaticschedule.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudASStackerLevel)).EndInit();
            this.tableSchedulePanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tbManualschedule.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMSStackerLevel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabDropSchedule;
        private System.Windows.Forms.TabPage tbAutomaticschedule;
        private System.Windows.Forms.TabPage tbManualschedule;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblSchedule;
        private System.Windows.Forms.DateTimePicker dtpScheduletime;
        private System.Windows.Forms.Label lblStackerlevelauto;
        private System.Windows.Forms.Label lblPercAutomactic;
        private System.Windows.Forms.RadioButton rdbMonthly;
        private System.Windows.Forms.RadioButton rdbWeekly;
        private System.Windows.Forms.RadioButton rdbDaily;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableSchedulePanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private ucRecurrenceRange ucRR;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSendDropAlert;
        private System.Windows.Forms.Button btnPrintschedule;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblStackerlevelmanual;
        private BMC.Common.Utilities.BmcComboBox cmbSite;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label label1;
        private ucWeeklySchedule ucWS;
        private ucMonthlySchedule ucMS;
        private System.Windows.Forms.NumericUpDown nudASStackerLevel;
        private System.Windows.Forms.NumericUpDown nudMSStackerLevel;
        private System.Windows.Forms.Label lblPercManual;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblHourMinute;
    }
}