namespace BMC.EnterpriseClient.Views
{
    partial class ucCalenders
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
            this.tbl_MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gb_CalendarDetails = new System.Windows.Forms.GroupBox();
            this.tbl_CalendarDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_CalendarName = new System.Windows.Forms.Label();
            this.txt_CalendarName = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.DTCalendarYearStart = new System.Windows.Forms.DateTimePicker();
            this.btnCalendarApply = new System.Windows.Forms.Button();
            this.btnCalendarNew = new System.Windows.Forms.Button();
            this.DTCalendarYearEnd = new System.Windows.Forms.DateTimePicker();
            this.lb = new System.Windows.Forms.Label();
            this.grpPeriods = new System.Windows.Forms.GroupBox();
            this.tbl_CalendarPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_PeriodEndDate = new System.Windows.Forms.Label();
            this.lbl_PeriodStartdate = new System.Windows.Forms.Label();
            this.lbl_CalendarPeriodNo = new System.Windows.Forms.Label();
            this.lvCalendarPeriods = new System.Windows.Forms.ListView();
            this.Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTCalendarPeriodStartDate = new System.Windows.Forms.DateTimePicker();
            this.DTCalendarPeriodEndDate = new System.Windows.Forms.DateTimePicker();
            this.txtCalendarPeriodNumber = new System.Windows.Forms.TextBox();
            this.lbl_CalendarPeriodBasedOn = new System.Windows.Forms.Label();
            this.txt_CalendarPeriodBasedOn = new System.Windows.Forms.TextBox();
            this.flp_PeriodButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCalendarPeriodApply = new System.Windows.Forms.Button();
            this.btn_UpdatePeriod = new System.Windows.Forms.Button();
            this.btnCalendarPeriodAddNew = new System.Windows.Forms.Button();
            this.grpWeek = new System.Windows.Forms.GroupBox();
            this.tbl_CalendarWeeks = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_WeekEndDate = new System.Windows.Forms.Label();
            this.lbl_WeekStartDate = new System.Windows.Forms.Label();
            this.lbl_WeekNumber = new System.Windows.Forms.Label();
            this.txt_CalendarWeekBasedOn = new System.Windows.Forms.TextBox();
            this.lbl_CalendarWeekBasedOn = new System.Windows.Forms.Label();
            this.lvCalendarWeeks = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DTCalendarWeekEndDate = new System.Windows.Forms.DateTimePicker();
            this.DTCalendarWeekStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtCalendarWeekNumber = new System.Windows.Forms.TextBox();
            this.flp_WeeksButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCalendarWeekApply = new System.Windows.Forms.Button();
            this.btn_UpdateWeek = new System.Windows.Forms.Button();
            this.btnCalendarWeekAddNew = new System.Windows.Forms.Button();
            this.lb_Calendars = new System.Windows.Forms.ListBox();
            this.tbl_MainPanel.SuspendLayout();
            this.gb_CalendarDetails.SuspendLayout();
            this.tbl_CalendarDetails.SuspendLayout();
            this.grpPeriods.SuspendLayout();
            this.tbl_CalendarPeriod.SuspendLayout();
            this.flp_PeriodButtons.SuspendLayout();
            this.grpWeek.SuspendLayout();
            this.tbl_CalendarWeeks.SuspendLayout();
            this.flp_WeeksButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbl_MainPanel
            // 
            this.tbl_MainPanel.ColumnCount = 2;
            this.tbl_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.tbl_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_MainPanel.Controls.Add(this.gb_CalendarDetails, 1, 0);
            this.tbl_MainPanel.Controls.Add(this.grpPeriods, 1, 1);
            this.tbl_MainPanel.Controls.Add(this.grpWeek, 1, 2);
            this.tbl_MainPanel.Controls.Add(this.lb_Calendars, 0, 0);
            this.tbl_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_MainPanel.Location = new System.Drawing.Point(0, 0);
            this.tbl_MainPanel.Name = "tbl_MainPanel";
            this.tbl_MainPanel.RowCount = 3;
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_MainPanel.Size = new System.Drawing.Size(919, 732);
            this.tbl_MainPanel.TabIndex = 0;
            // 
            // gb_CalendarDetails
            // 
            this.gb_CalendarDetails.Controls.Add(this.tbl_CalendarDetails);
            this.gb_CalendarDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_CalendarDetails.Location = new System.Drawing.Point(234, 3);
            this.gb_CalendarDetails.Name = "gb_CalendarDetails";
            this.gb_CalendarDetails.Size = new System.Drawing.Size(682, 105);
            this.gb_CalendarDetails.TabIndex = 1;
            this.gb_CalendarDetails.TabStop = false;
            this.gb_CalendarDetails.Text = "Current Calendar";
            // 
            // tbl_CalendarDetails
            // 
            this.tbl_CalendarDetails.ColumnCount = 6;
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tbl_CalendarDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tbl_CalendarDetails.Controls.Add(this.lbl_CalendarName, 0, 0);
            this.tbl_CalendarDetails.Controls.Add(this.txt_CalendarName, 1, 0);
            this.tbl_CalendarDetails.Controls.Add(this.lblYear, 2, 0);
            this.tbl_CalendarDetails.Controls.Add(this.DTCalendarYearStart, 3, 0);
            this.tbl_CalendarDetails.Controls.Add(this.btnCalendarApply, 5, 1);
            this.tbl_CalendarDetails.Controls.Add(this.btnCalendarNew, 4, 1);
            this.tbl_CalendarDetails.Controls.Add(this.DTCalendarYearEnd, 5, 0);
            this.tbl_CalendarDetails.Controls.Add(this.lb, 4, 0);
            this.tbl_CalendarDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_CalendarDetails.Location = new System.Drawing.Point(3, 16);
            this.tbl_CalendarDetails.Name = "tbl_CalendarDetails";
            this.tbl_CalendarDetails.RowCount = 2;
            this.tbl_CalendarDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_CalendarDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_CalendarDetails.Size = new System.Drawing.Size(676, 86);
            this.tbl_CalendarDetails.TabIndex = 0;
            // 
            // lbl_CalendarName
            // 
            this.lbl_CalendarName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CalendarName.AutoSize = true;
            this.lbl_CalendarName.Location = new System.Drawing.Point(3, 15);
            this.lbl_CalendarName.Name = "lbl_CalendarName";
            this.lbl_CalendarName.Size = new System.Drawing.Size(86, 13);
            this.lbl_CalendarName.TabIndex = 0;
            this.lbl_CalendarName.Text = "Calendar Name :";
            // 
            // txt_CalendarName
            // 
            this.txt_CalendarName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CalendarName.Location = new System.Drawing.Point(103, 11);
            this.txt_CalendarName.Name = "txt_CalendarName";
            this.txt_CalendarName.Size = new System.Drawing.Size(170, 20);
            this.txt_CalendarName.TabIndex = 1;
            // 
            // lblYear
            // 
            this.lblYear.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(279, 15);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(70, 13);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "Year Begins :";
            // 
            // DTCalendarYearStart
            // 
            this.DTCalendarYearStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarYearStart.CustomFormat = "";
            this.DTCalendarYearStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarYearStart.Location = new System.Drawing.Point(362, 11);
            this.DTCalendarYearStart.Name = "DTCalendarYearStart";
            this.DTCalendarYearStart.Size = new System.Drawing.Size(101, 20);
            this.DTCalendarYearStart.TabIndex = 3;
            // 
            // btnCalendarApply
            // 
            this.btnCalendarApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalendarApply.Location = new System.Drawing.Point(573, 50);
            this.btnCalendarApply.Name = "btnCalendarApply";
            this.btnCalendarApply.Size = new System.Drawing.Size(100, 28);
            this.btnCalendarApply.TabIndex = 7;
            this.btnCalendarApply.Text = "Apply";
            this.btnCalendarApply.UseVisualStyleBackColor = true;
            this.btnCalendarApply.Click += new System.EventHandler(this.btnCalendarApply_Click);
            // 
            // btnCalendarNew
            // 
            this.btnCalendarNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalendarNew.Location = new System.Drawing.Point(469, 50);
            this.btnCalendarNew.Name = "btnCalendarNew";
            this.btnCalendarNew.Size = new System.Drawing.Size(97, 28);
            this.btnCalendarNew.TabIndex = 6;
            this.btnCalendarNew.Text = "N&ew";
            this.btnCalendarNew.UseVisualStyleBackColor = true;
            this.btnCalendarNew.Click += new System.EventHandler(this.btnCalendarNew_Click);
            // 
            // DTCalendarYearEnd
            // 
            this.DTCalendarYearEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarYearEnd.CustomFormat = "";
            this.DTCalendarYearEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarYearEnd.Location = new System.Drawing.Point(572, 11);
            this.DTCalendarYearEnd.Name = "DTCalendarYearEnd";
            this.DTCalendarYearEnd.Size = new System.Drawing.Size(101, 20);
            this.DTCalendarYearEnd.TabIndex = 5;
            // 
            // lb
            // 
            this.lb.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(469, 15);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(54, 13);
            this.lb.TabIndex = 4;
            this.lb.Text = "Year End:";
            // 
            // grpPeriods
            // 
            this.grpPeriods.Controls.Add(this.tbl_CalendarPeriod);
            this.grpPeriods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPeriods.Location = new System.Drawing.Point(234, 114);
            this.grpPeriods.Name = "grpPeriods";
            this.grpPeriods.Size = new System.Drawing.Size(682, 304);
            this.grpPeriods.TabIndex = 2;
            this.grpPeriods.TabStop = false;
            this.grpPeriods.Text = "Periods";
            // 
            // tbl_CalendarPeriod
            // 
            this.tbl_CalendarPeriod.ColumnCount = 5;
            this.tbl_CalendarPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_CalendarPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tbl_CalendarPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tbl_CalendarPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarPeriod.Controls.Add(this.lbl_PeriodEndDate, 3, 3);
            this.tbl_CalendarPeriod.Controls.Add(this.lbl_PeriodStartdate, 3, 2);
            this.tbl_CalendarPeriod.Controls.Add(this.lbl_CalendarPeriodNo, 3, 1);
            this.tbl_CalendarPeriod.Controls.Add(this.lvCalendarPeriods, 0, 0);
            this.tbl_CalendarPeriod.Controls.Add(this.DTCalendarPeriodStartDate, 4, 2);
            this.tbl_CalendarPeriod.Controls.Add(this.DTCalendarPeriodEndDate, 4, 3);
            this.tbl_CalendarPeriod.Controls.Add(this.txtCalendarPeriodNumber, 4, 1);
            this.tbl_CalendarPeriod.Controls.Add(this.lbl_CalendarPeriodBasedOn, 3, 0);
            this.tbl_CalendarPeriod.Controls.Add(this.txt_CalendarPeriodBasedOn, 4, 0);
            this.tbl_CalendarPeriod.Controls.Add(this.flp_PeriodButtons, 0, 5);
            this.tbl_CalendarPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_CalendarPeriod.Location = new System.Drawing.Point(3, 16);
            this.tbl_CalendarPeriod.Name = "tbl_CalendarPeriod";
            this.tbl_CalendarPeriod.RowCount = 6;
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tbl_CalendarPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_CalendarPeriod.Size = new System.Drawing.Size(676, 285);
            this.tbl_CalendarPeriod.TabIndex = 0;
            // 
            // lbl_PeriodEndDate
            // 
            this.lbl_PeriodEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_PeriodEndDate.AutoSize = true;
            this.lbl_PeriodEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PeriodEndDate.Location = new System.Drawing.Point(472, 98);
            this.lbl_PeriodEndDate.Name = "lbl_PeriodEndDate";
            this.lbl_PeriodEndDate.Size = new System.Drawing.Size(58, 13);
            this.lbl_PeriodEndDate.TabIndex = 7;
            this.lbl_PeriodEndDate.Text = "End Date :";
            this.lbl_PeriodEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_PeriodStartdate
            // 
            this.lbl_PeriodStartdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_PeriodStartdate.AutoSize = true;
            this.lbl_PeriodStartdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PeriodStartdate.Location = new System.Drawing.Point(472, 68);
            this.lbl_PeriodStartdate.Name = "lbl_PeriodStartdate";
            this.lbl_PeriodStartdate.Size = new System.Drawing.Size(61, 13);
            this.lbl_PeriodStartdate.TabIndex = 5;
            this.lbl_PeriodStartdate.Text = "Start Date :";
            this.lbl_PeriodStartdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_CalendarPeriodNo
            // 
            this.lbl_CalendarPeriodNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CalendarPeriodNo.AutoSize = true;
            this.lbl_CalendarPeriodNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CalendarPeriodNo.Location = new System.Drawing.Point(472, 38);
            this.lbl_CalendarPeriodNo.Name = "lbl_CalendarPeriodNo";
            this.lbl_CalendarPeriodNo.Size = new System.Drawing.Size(88, 13);
            this.lbl_CalendarPeriodNo.TabIndex = 3;
            this.lbl_CalendarPeriodNo.Text = "Periods Number :";
            this.lbl_CalendarPeriodNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvCalendarPeriods
            // 
            this.lvCalendarPeriods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Number,
            this.StartDate,
            this.EndDate});
            this.tbl_CalendarPeriod.SetColumnSpan(this.lvCalendarPeriods, 3);
            this.lvCalendarPeriods.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCalendarPeriods.FullRowSelect = true;
            this.lvCalendarPeriods.GridLines = true;
            this.lvCalendarPeriods.Location = new System.Drawing.Point(3, 3);
            this.lvCalendarPeriods.MultiSelect = false;
            this.lvCalendarPeriods.Name = "lvCalendarPeriods";
            this.tbl_CalendarPeriod.SetRowSpan(this.lvCalendarPeriods, 5);
            this.lvCalendarPeriods.Size = new System.Drawing.Size(463, 239);
            this.lvCalendarPeriods.TabIndex = 0;
            this.lvCalendarPeriods.UseCompatibleStateImageBehavior = false;
            this.lvCalendarPeriods.View = System.Windows.Forms.View.Details;
            this.lvCalendarPeriods.SelectedIndexChanged += new System.EventHandler(this.lvCalendarPeriods_SelectedIndexChanged);
            this.lvCalendarPeriods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvCalendarPeriods_MouseDown);
            // 
            // Number
            // 
            this.Number.Text = "Number";
            this.Number.Width = 150;
            // 
            // StartDate
            // 
            this.StartDate.Text = "StartDate";
            this.StartDate.Width = 100;
            // 
            // EndDate
            // 
            this.EndDate.Text = "EndDate";
            this.EndDate.Width = 100;
            // 
            // DTCalendarPeriodStartDate
            // 
            this.DTCalendarPeriodStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarPeriodStartDate.CustomFormat = "";
            this.DTCalendarPeriodStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarPeriodStartDate.Location = new System.Drawing.Point(574, 65);
            this.DTCalendarPeriodStartDate.Name = "DTCalendarPeriodStartDate";
            this.DTCalendarPeriodStartDate.Size = new System.Drawing.Size(99, 20);
            this.DTCalendarPeriodStartDate.TabIndex = 6;
            // 
            // DTCalendarPeriodEndDate
            // 
            this.DTCalendarPeriodEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarPeriodEndDate.CustomFormat = "";
            this.DTCalendarPeriodEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarPeriodEndDate.Location = new System.Drawing.Point(574, 95);
            this.DTCalendarPeriodEndDate.Name = "DTCalendarPeriodEndDate";
            this.DTCalendarPeriodEndDate.Size = new System.Drawing.Size(99, 20);
            this.DTCalendarPeriodEndDate.TabIndex = 8;
            // 
            // txtCalendarPeriodNumber
            // 
            this.txtCalendarPeriodNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCalendarPeriodNumber.Location = new System.Drawing.Point(574, 35);
            this.txtCalendarPeriodNumber.Name = "txtCalendarPeriodNumber";
            this.txtCalendarPeriodNumber.ReadOnly = true;
            this.txtCalendarPeriodNumber.Size = new System.Drawing.Size(99, 20);
            this.txtCalendarPeriodNumber.TabIndex = 4;
            // 
            // lbl_CalendarPeriodBasedOn
            // 
            this.lbl_CalendarPeriodBasedOn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CalendarPeriodBasedOn.AutoSize = true;
            this.lbl_CalendarPeriodBasedOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CalendarPeriodBasedOn.Location = new System.Drawing.Point(472, 8);
            this.lbl_CalendarPeriodBasedOn.Name = "lbl_CalendarPeriodBasedOn";
            this.lbl_CalendarPeriodBasedOn.Size = new System.Drawing.Size(93, 13);
            this.lbl_CalendarPeriodBasedOn.TabIndex = 1;
            this.lbl_CalendarPeriodBasedOn.Text = "Periods Based on:";
            this.lbl_CalendarPeriodBasedOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_CalendarPeriodBasedOn
            // 
            this.txt_CalendarPeriodBasedOn.Enabled = false;
            this.txt_CalendarPeriodBasedOn.Location = new System.Drawing.Point(574, 3);
            this.txt_CalendarPeriodBasedOn.Name = "txt_CalendarPeriodBasedOn";
            this.txt_CalendarPeriodBasedOn.ReadOnly = true;
            this.txt_CalendarPeriodBasedOn.Size = new System.Drawing.Size(99, 20);
            this.txt_CalendarPeriodBasedOn.TabIndex = 2;
            // 
            // flp_PeriodButtons
            // 
            this.tbl_CalendarPeriod.SetColumnSpan(this.flp_PeriodButtons, 5);
            this.flp_PeriodButtons.Controls.Add(this.btnCalendarPeriodApply);
            this.flp_PeriodButtons.Controls.Add(this.btn_UpdatePeriod);
            this.flp_PeriodButtons.Controls.Add(this.btnCalendarPeriodAddNew);
            this.flp_PeriodButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_PeriodButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flp_PeriodButtons.Location = new System.Drawing.Point(3, 248);
            this.flp_PeriodButtons.Name = "flp_PeriodButtons";
            this.flp_PeriodButtons.Size = new System.Drawing.Size(670, 34);
            this.flp_PeriodButtons.TabIndex = 9;
            // 
            // btnCalendarPeriodApply
            // 
            this.btnCalendarPeriodApply.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCalendarPeriodApply.Location = new System.Drawing.Point(567, 3);
            this.btnCalendarPeriodApply.Name = "btnCalendarPeriodApply";
            this.btnCalendarPeriodApply.Size = new System.Drawing.Size(100, 28);
            this.btnCalendarPeriodApply.TabIndex = 2;
            this.btnCalendarPeriodApply.Text = "A&pply";
            this.btnCalendarPeriodApply.UseVisualStyleBackColor = true;
            this.btnCalendarPeriodApply.Visible = false;
            this.btnCalendarPeriodApply.Click += new System.EventHandler(this.btnCalendarPeriodApply_Click);
            // 
            // btn_UpdatePeriod
            // 
            this.btn_UpdatePeriod.Location = new System.Drawing.Point(461, 3);
            this.btn_UpdatePeriod.Name = "btn_UpdatePeriod";
            this.btn_UpdatePeriod.Size = new System.Drawing.Size(100, 28);
            this.btn_UpdatePeriod.TabIndex = 1;
            this.btn_UpdatePeriod.Text = "&Update";
            this.btn_UpdatePeriod.UseVisualStyleBackColor = true;
            this.btn_UpdatePeriod.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCalendarPeriodAddNew
            // 
            this.btnCalendarPeriodAddNew.Location = new System.Drawing.Point(355, 3);
            this.btnCalendarPeriodAddNew.Name = "btnCalendarPeriodAddNew";
            this.btnCalendarPeriodAddNew.Size = new System.Drawing.Size(100, 28);
            this.btnCalendarPeriodAddNew.TabIndex = 0;
            this.btnCalendarPeriodAddNew.Text = "&Add New";
            this.btnCalendarPeriodAddNew.UseVisualStyleBackColor = true;
            this.btnCalendarPeriodAddNew.Click += new System.EventHandler(this.btnCalendarPeriodAddNew_Click);
            // 
            // grpWeek
            // 
            this.grpWeek.Controls.Add(this.tbl_CalendarWeeks);
            this.grpWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWeek.Location = new System.Drawing.Point(234, 424);
            this.grpWeek.Name = "grpWeek";
            this.grpWeek.Size = new System.Drawing.Size(682, 305);
            this.grpWeek.TabIndex = 3;
            this.grpWeek.TabStop = false;
            this.grpWeek.Text = "Weeks";
            // 
            // tbl_CalendarWeeks
            // 
            this.tbl_CalendarWeeks.ColumnCount = 5;
            this.tbl_CalendarWeeks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_CalendarWeeks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarWeeks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarWeeks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarWeeks.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_CalendarWeeks.Controls.Add(this.lbl_WeekEndDate, 3, 3);
            this.tbl_CalendarWeeks.Controls.Add(this.lbl_WeekStartDate, 3, 2);
            this.tbl_CalendarWeeks.Controls.Add(this.lbl_WeekNumber, 3, 1);
            this.tbl_CalendarWeeks.Controls.Add(this.txt_CalendarWeekBasedOn, 4, 0);
            this.tbl_CalendarWeeks.Controls.Add(this.lbl_CalendarWeekBasedOn, 3, 0);
            this.tbl_CalendarWeeks.Controls.Add(this.lvCalendarWeeks, 0, 0);
            this.tbl_CalendarWeeks.Controls.Add(this.DTCalendarWeekEndDate, 4, 3);
            this.tbl_CalendarWeeks.Controls.Add(this.DTCalendarWeekStartDate, 4, 2);
            this.tbl_CalendarWeeks.Controls.Add(this.txtCalendarWeekNumber, 4, 1);
            this.tbl_CalendarWeeks.Controls.Add(this.flp_WeeksButton, 0, 5);
            this.tbl_CalendarWeeks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_CalendarWeeks.Location = new System.Drawing.Point(3, 16);
            this.tbl_CalendarWeeks.Name = "tbl_CalendarWeeks";
            this.tbl_CalendarWeeks.RowCount = 6;
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tbl_CalendarWeeks.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_CalendarWeeks.Size = new System.Drawing.Size(676, 286);
            this.tbl_CalendarWeeks.TabIndex = 0;
            // 
            // lbl_WeekEndDate
            // 
            this.lbl_WeekEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WeekEndDate.AutoSize = true;
            this.lbl_WeekEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WeekEndDate.Location = new System.Drawing.Point(469, 98);
            this.lbl_WeekEndDate.Name = "lbl_WeekEndDate";
            this.lbl_WeekEndDate.Size = new System.Drawing.Size(58, 13);
            this.lbl_WeekEndDate.TabIndex = 8;
            this.lbl_WeekEndDate.Text = "End Date :";
            this.lbl_WeekEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WeekStartDate
            // 
            this.lbl_WeekStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WeekStartDate.AutoSize = true;
            this.lbl_WeekStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WeekStartDate.Location = new System.Drawing.Point(469, 68);
            this.lbl_WeekStartDate.Name = "lbl_WeekStartDate";
            this.lbl_WeekStartDate.Size = new System.Drawing.Size(61, 13);
            this.lbl_WeekStartDate.TabIndex = 6;
            this.lbl_WeekStartDate.Text = "Start Date :";
            this.lbl_WeekStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_WeekNumber
            // 
            this.lbl_WeekNumber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WeekNumber.AutoSize = true;
            this.lbl_WeekNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_WeekNumber.Location = new System.Drawing.Point(469, 38);
            this.lbl_WeekNumber.Name = "lbl_WeekNumber";
            this.lbl_WeekNumber.Size = new System.Drawing.Size(82, 13);
            this.lbl_WeekNumber.TabIndex = 4;
            this.lbl_WeekNumber.Text = "Week Number :";
            this.lbl_WeekNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_CalendarWeekBasedOn
            // 
            this.txt_CalendarWeekBasedOn.Enabled = false;
            this.txt_CalendarWeekBasedOn.Location = new System.Drawing.Point(574, 3);
            this.txt_CalendarWeekBasedOn.Name = "txt_CalendarWeekBasedOn";
            this.txt_CalendarWeekBasedOn.ReadOnly = true;
            this.txt_CalendarWeekBasedOn.Size = new System.Drawing.Size(99, 20);
            this.txt_CalendarWeekBasedOn.TabIndex = 3;
            // 
            // lbl_CalendarWeekBasedOn
            // 
            this.lbl_CalendarWeekBasedOn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CalendarWeekBasedOn.AutoSize = true;
            this.lbl_CalendarWeekBasedOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CalendarWeekBasedOn.Location = new System.Drawing.Point(469, 8);
            this.lbl_CalendarWeekBasedOn.Name = "lbl_CalendarWeekBasedOn";
            this.lbl_CalendarWeekBasedOn.Size = new System.Drawing.Size(92, 13);
            this.lbl_CalendarWeekBasedOn.TabIndex = 2;
            this.lbl_CalendarWeekBasedOn.Text = "Weeks Based on:";
            this.lbl_CalendarWeekBasedOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvCalendarWeeks
            // 
            this.lvCalendarWeeks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.tbl_CalendarWeeks.SetColumnSpan(this.lvCalendarWeeks, 3);
            this.lvCalendarWeeks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCalendarWeeks.FullRowSelect = true;
            this.lvCalendarWeeks.GridLines = true;
            this.lvCalendarWeeks.Location = new System.Drawing.Point(3, 3);
            this.lvCalendarWeeks.MultiSelect = false;
            this.lvCalendarWeeks.Name = "lvCalendarWeeks";
            this.tbl_CalendarWeeks.SetRowSpan(this.lvCalendarWeeks, 5);
            this.lvCalendarWeeks.Size = new System.Drawing.Size(460, 240);
            this.lvCalendarWeeks.TabIndex = 1;
            this.lvCalendarWeeks.UseCompatibleStateImageBehavior = false;
            this.lvCalendarWeeks.View = System.Windows.Forms.View.Details;
            this.lvCalendarWeeks.SelectedIndexChanged += new System.EventHandler(this.lvCalendarWeeks_SelectedIndexChanged);
            this.lvCalendarWeeks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvCalendarWeeks_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Number";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "StartDate";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "EndDate";
            this.columnHeader3.Width = 100;
            // 
            // DTCalendarWeekEndDate
            // 
            this.DTCalendarWeekEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarWeekEndDate.CustomFormat = "";
            this.DTCalendarWeekEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarWeekEndDate.Location = new System.Drawing.Point(574, 95);
            this.DTCalendarWeekEndDate.Name = "DTCalendarWeekEndDate";
            this.DTCalendarWeekEndDate.Size = new System.Drawing.Size(99, 20);
            this.DTCalendarWeekEndDate.TabIndex = 9;
            // 
            // DTCalendarWeekStartDate
            // 
            this.DTCalendarWeekStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.DTCalendarWeekStartDate.CustomFormat = "";
            this.DTCalendarWeekStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTCalendarWeekStartDate.Location = new System.Drawing.Point(574, 65);
            this.DTCalendarWeekStartDate.Name = "DTCalendarWeekStartDate";
            this.DTCalendarWeekStartDate.Size = new System.Drawing.Size(99, 20);
            this.DTCalendarWeekStartDate.TabIndex = 7;
            // 
            // txtCalendarWeekNumber
            // 
            this.txtCalendarWeekNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCalendarWeekNumber.Location = new System.Drawing.Point(574, 35);
            this.txtCalendarWeekNumber.Name = "txtCalendarWeekNumber";
            this.txtCalendarWeekNumber.ReadOnly = true;
            this.txtCalendarWeekNumber.Size = new System.Drawing.Size(99, 20);
            this.txtCalendarWeekNumber.TabIndex = 5;
            // 
            // flp_WeeksButton
            // 
            this.tbl_CalendarWeeks.SetColumnSpan(this.flp_WeeksButton, 5);
            this.flp_WeeksButton.Controls.Add(this.btnCalendarWeekApply);
            this.flp_WeeksButton.Controls.Add(this.btn_UpdateWeek);
            this.flp_WeeksButton.Controls.Add(this.btnCalendarWeekAddNew);
            this.flp_WeeksButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_WeeksButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flp_WeeksButton.Location = new System.Drawing.Point(3, 249);
            this.flp_WeeksButton.Name = "flp_WeeksButton";
            this.flp_WeeksButton.Size = new System.Drawing.Size(670, 34);
            this.flp_WeeksButton.TabIndex = 9;
            // 
            // btnCalendarWeekApply
            // 
            this.btnCalendarWeekApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalendarWeekApply.Location = new System.Drawing.Point(567, 3);
            this.btnCalendarWeekApply.Name = "btnCalendarWeekApply";
            this.btnCalendarWeekApply.Size = new System.Drawing.Size(100, 28);
            this.btnCalendarWeekApply.TabIndex = 2;
            this.btnCalendarWeekApply.Text = "App&ly";
            this.btnCalendarWeekApply.UseVisualStyleBackColor = true;
            this.btnCalendarWeekApply.Visible = false;
            this.btnCalendarWeekApply.Click += new System.EventHandler(this.btnCalendarWeekApply_Click);
            // 
            // btn_UpdateWeek
            // 
            this.btn_UpdateWeek.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_UpdateWeek.Location = new System.Drawing.Point(461, 3);
            this.btn_UpdateWeek.Name = "btn_UpdateWeek";
            this.btn_UpdateWeek.Size = new System.Drawing.Size(100, 28);
            this.btn_UpdateWeek.TabIndex = 1;
            this.btn_UpdateWeek.Text = "Up&date";
            this.btn_UpdateWeek.UseVisualStyleBackColor = true;
            this.btn_UpdateWeek.Click += new System.EventHandler(this.btn_UpdateWeek_Click);
            // 
            // btnCalendarWeekAddNew
            // 
            this.btnCalendarWeekAddNew.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCalendarWeekAddNew.Location = new System.Drawing.Point(355, 3);
            this.btnCalendarWeekAddNew.Name = "btnCalendarWeekAddNew";
            this.btnCalendarWeekAddNew.Size = new System.Drawing.Size(100, 28);
            this.btnCalendarWeekAddNew.TabIndex = 0;
            this.btnCalendarWeekAddNew.Text = "Add &New";
            this.btnCalendarWeekAddNew.UseVisualStyleBackColor = true;
            this.btnCalendarWeekAddNew.Click += new System.EventHandler(this.btnCalendarWeekAddNew_Click_1);
            // 
            // lb_Calendars
            // 
            this.lb_Calendars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Calendars.FormattingEnabled = true;
            this.lb_Calendars.Location = new System.Drawing.Point(3, 3);
            this.lb_Calendars.Name = "lb_Calendars";
            this.tbl_MainPanel.SetRowSpan(this.lb_Calendars, 3);
            this.lb_Calendars.Size = new System.Drawing.Size(225, 726);
            this.lb_Calendars.TabIndex = 0;
            this.lb_Calendars.SelectedIndexChanged += new System.EventHandler(this.lb_Calendars_SelectedIndexChanged);
            // 
            // ucCalenders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbl_MainPanel);
            this.Name = "ucCalenders";
            this.Size = new System.Drawing.Size(919, 732);
            this.Load += new System.EventHandler(this.ucCalenders_Load);
            this.tbl_MainPanel.ResumeLayout(false);
            this.gb_CalendarDetails.ResumeLayout(false);
            this.tbl_CalendarDetails.ResumeLayout(false);
            this.tbl_CalendarDetails.PerformLayout();
            this.grpPeriods.ResumeLayout(false);
            this.tbl_CalendarPeriod.ResumeLayout(false);
            this.tbl_CalendarPeriod.PerformLayout();
            this.flp_PeriodButtons.ResumeLayout(false);
            this.grpWeek.ResumeLayout(false);
            this.tbl_CalendarWeeks.ResumeLayout(false);
            this.tbl_CalendarWeeks.PerformLayout();
            this.flp_WeeksButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbl_MainPanel;
        private System.Windows.Forms.DateTimePicker DTCalendarYearEnd;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.GroupBox grpWeek;
        private System.Windows.Forms.DateTimePicker DTCalendarWeekEndDate;
        private System.Windows.Forms.DateTimePicker DTCalendarWeekStartDate;
        private System.Windows.Forms.TextBox txtCalendarWeekNumber;
        private System.Windows.Forms.ListView lvCalendarWeeks;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.DateTimePicker DTCalendarYearStart;
        private System.Windows.Forms.TableLayoutPanel tbl_CalendarWeeks;
        private System.Windows.Forms.Button btnCalendarWeekApply;
        private System.Windows.Forms.Button btnCalendarWeekAddNew;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btn_UpdateWeek;
        private System.Windows.Forms.TableLayoutPanel tbl_CalendarDetails;
        private System.Windows.Forms.GroupBox gb_CalendarDetails;
        private System.Windows.Forms.GroupBox grpPeriods;
        private System.Windows.Forms.ListView lvCalendarPeriods;
        private System.Windows.Forms.ColumnHeader Number;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader EndDate;
        private System.Windows.Forms.TableLayoutPanel tbl_CalendarPeriod;
        private System.Windows.Forms.TextBox txtCalendarPeriodNumber;
        private System.Windows.Forms.DateTimePicker DTCalendarPeriodEndDate;
        private System.Windows.Forms.DateTimePicker DTCalendarPeriodStartDate;
        private System.Windows.Forms.Button btnCalendarPeriodApply;
        private System.Windows.Forms.Button btnCalendarPeriodAddNew;
        private System.Windows.Forms.Button btn_UpdatePeriod;
        private System.Windows.Forms.Label lbl_CalendarName;
        private System.Windows.Forms.TextBox txt_CalendarName;
        private System.Windows.Forms.Label lbl_CalendarPeriodBasedOn;
        private System.Windows.Forms.Label lbl_CalendarWeekBasedOn;
        private System.Windows.Forms.TextBox txt_CalendarPeriodBasedOn;
        private System.Windows.Forms.TextBox txt_CalendarWeekBasedOn;
        private System.Windows.Forms.ListBox lb_Calendars;
        private System.Windows.Forms.Button btnCalendarApply;
        private System.Windows.Forms.Button btnCalendarNew;
        private System.Windows.Forms.FlowLayoutPanel flp_PeriodButtons;
        private System.Windows.Forms.FlowLayoutPanel flp_WeeksButton;
        private System.Windows.Forms.Label lbl_CalendarPeriodNo;
        private System.Windows.Forms.Label lbl_PeriodEndDate;
        private System.Windows.Forms.Label lbl_PeriodStartdate;
        private System.Windows.Forms.Label lbl_WeekNumber;
        private System.Windows.Forms.Label lbl_WeekEndDate;
        private System.Windows.Forms.Label lbl_WeekStartDate;
    }
}
