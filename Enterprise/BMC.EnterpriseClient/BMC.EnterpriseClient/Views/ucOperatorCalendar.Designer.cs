namespace BMC.EnterpriseClient.Views
{
    partial class ucOperatorCalendar
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
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtNextYrEnd = new System.Windows.Forms.TextBox();
            this.txtNextPeriod = new System.Windows.Forms.TextBox();
            this.lblCurrentWeek = new System.Windows.Forms.Label();
            this.lblCurrentPeriod = new System.Windows.Forms.Label();
            this.lblNextYrEnd = new System.Windows.Forms.Label();
            this.lblNextPeriod = new System.Windows.Forms.Label();
            this.txtCurrentWeek = new System.Windows.Forms.TextBox();
            this.txtCurrentPeriod = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExportCalendar = new System.Windows.Forms.Button();
            this.lblCalendarHistory = new System.Windows.Forms.Label();
            this.lstCompanyCalendarHistory = new System.Windows.Forms.ListBox();
            this.txtCurrentCalendar = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCurrentCalendar = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.lvCompaniesCalendars = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lstSupplierCalendarHistory = new System.Windows.Forms.ListBox();
            this.lblCalHistory = new System.Windows.Forms.Label();
            this.txtSuppliersCurrentCalendar = new System.Windows.Forms.TextBox();
            this.lblCurrentCal = new System.Windows.Forms.Label();
            this.grpOperatorDetails = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSuppliersNextYearEnd = new System.Windows.Forms.TextBox();
            this.txtSuppliersNextPeriodEnd = new System.Windows.Forms.TextBox();
            this.txtSuppliersCurrentPeriod = new System.Windows.Forms.TextBox();
            this.lblCntWeek = new System.Windows.Forms.Label();
            this.txtSuppliersCurrentWeek = new System.Windows.Forms.TextBox();
            this.lblCntPeriod = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ExportCalendar = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.lvSuppliersCalendar = new System.Windows.Forms.ListView();
            this.CalendarId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSuppliersCalendarApply = new System.Windows.Forms.Button();
            this.lstSuppliers = new System.Windows.Forms.ListBox();
            this.grpDetails.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpOperatorDetails.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.tableLayoutPanel1);
            this.grpDetails.Location = new System.Drawing.Point(3, 9);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(337, 163);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtNextYrEnd, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtNextPeriod, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentWeek, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPeriod, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblNextYrEnd, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblNextPeriod, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtCurrentWeek, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCurrentPeriod, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.31461F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.68539F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 130);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // txtNextYrEnd
            // 
            this.txtNextYrEnd.Location = new System.Drawing.Point(101, 101);
            this.txtNextYrEnd.Name = "txtNextYrEnd";
            this.txtNextYrEnd.Size = new System.Drawing.Size(144, 20);
            this.txtNextYrEnd.TabIndex = 7;
            // 
            // txtNextPeriod
            // 
            this.txtNextPeriod.Location = new System.Drawing.Point(101, 69);
            this.txtNextPeriod.Name = "txtNextPeriod";
            this.txtNextPeriod.Size = new System.Drawing.Size(144, 20);
            this.txtNextPeriod.TabIndex = 6;
            // 
            // lblCurrentWeek
            // 
            this.lblCurrentWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentWeek.AutoSize = true;
            this.lblCurrentWeek.Location = new System.Drawing.Point(3, 9);
            this.lblCurrentWeek.Name = "lblCurrentWeek";
            this.lblCurrentWeek.Size = new System.Drawing.Size(76, 13);
            this.lblCurrentWeek.TabIndex = 0;
            this.lblCurrentWeek.Text = "Current Week:";
            this.lblCurrentWeek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPeriod
            // 
            this.lblCurrentPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentPeriod.AutoSize = true;
            this.lblCurrentPeriod.Location = new System.Drawing.Point(3, 42);
            this.lblCurrentPeriod.Name = "lblCurrentPeriod";
            this.lblCurrentPeriod.Size = new System.Drawing.Size(77, 13);
            this.lblCurrentPeriod.TabIndex = 1;
            this.lblCurrentPeriod.Text = "Current Period:";
            this.lblCurrentPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNextYrEnd
            // 
            this.lblNextYrEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNextYrEnd.AutoSize = true;
            this.lblNextYrEnd.Location = new System.Drawing.Point(3, 107);
            this.lblNextYrEnd.Name = "lblNextYrEnd";
            this.lblNextYrEnd.Size = new System.Drawing.Size(79, 13);
            this.lblNextYrEnd.TabIndex = 3;
            this.lblNextYrEnd.Text = "Next Year End:";
            // 
            // lblNextPeriod
            // 
            this.lblNextPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNextPeriod.AutoSize = true;
            this.lblNextPeriod.Location = new System.Drawing.Point(3, 75);
            this.lblNextPeriod.Name = "lblNextPeriod";
            this.lblNextPeriod.Size = new System.Drawing.Size(87, 13);
            this.lblNextPeriod.TabIndex = 2;
            this.lblNextPeriod.Text = "Next Period End:";
            this.lblNextPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCurrentWeek
            // 
            this.txtCurrentWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCurrentWeek.Location = new System.Drawing.Point(101, 6);
            this.txtCurrentWeek.Name = "txtCurrentWeek";
            this.txtCurrentWeek.Size = new System.Drawing.Size(144, 20);
            this.txtCurrentWeek.TabIndex = 4;
            // 
            // txtCurrentPeriod
            // 
            this.txtCurrentPeriod.Location = new System.Drawing.Point(101, 35);
            this.txtCurrentPeriod.Name = "txtCurrentPeriod";
            this.txtCurrentPeriod.Size = new System.Drawing.Size(144, 20);
            this.txtCurrentPeriod.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExportCalendar);
            this.panel1.Controls.Add(this.lblCalendarHistory);
            this.panel1.Controls.Add(this.lstCompanyCalendarHistory);
            this.panel1.Location = new System.Drawing.Point(-85, 183);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(224, 185);
            this.panel1.TabIndex = 7;
            // 
            // btnExportCalendar
            // 
            this.btnExportCalendar.Location = new System.Drawing.Point(12, 155);
            this.btnExportCalendar.Name = "btnExportCalendar";
            this.btnExportCalendar.Size = new System.Drawing.Size(160, 25);
            this.btnExportCalendar.TabIndex = 6;
            this.btnExportCalendar.Text = "Export Calendar";
            this.btnExportCalendar.UseVisualStyleBackColor = true;
            // 
            // lblCalendarHistory
            // 
            this.lblCalendarHistory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCalendarHistory.AutoSize = true;
            this.lblCalendarHistory.Location = new System.Drawing.Point(9, 8);
            this.lblCalendarHistory.Name = "lblCalendarHistory";
            this.lblCalendarHistory.Size = new System.Drawing.Size(84, 13);
            this.lblCalendarHistory.TabIndex = 5;
            this.lblCalendarHistory.Text = "Calendar History";
            // 
            // lstCompanyCalendarHistory
            // 
            this.lstCompanyCalendarHistory.FormattingEnabled = true;
            this.lstCompanyCalendarHistory.Location = new System.Drawing.Point(3, 28);
            this.lstCompanyCalendarHistory.Name = "lstCompanyCalendarHistory";
            this.lstCompanyCalendarHistory.Size = new System.Drawing.Size(218, 121);
            this.lstCompanyCalendarHistory.TabIndex = 4;
            // 
            // txtCurrentCalendar
            // 
            this.txtCurrentCalendar.Location = new System.Drawing.Point(98, 3);
            this.txtCurrentCalendar.Name = "txtCurrentCalendar";
            this.txtCurrentCalendar.Size = new System.Drawing.Size(239, 20);
            this.txtCurrentCalendar.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grpDetails);
            this.panel3.Location = new System.Drawing.Point(145, 224);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(346, 177);
            this.panel3.TabIndex = 6;
            // 
            // lblCurrentCalendar
            // 
            this.lblCurrentCalendar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentCalendar.AutoSize = true;
            this.lblCurrentCalendar.Location = new System.Drawing.Point(3, 30);
            this.lblCurrentCalendar.Name = "lblCurrentCalendar";
            this.lblCurrentCalendar.Size = new System.Drawing.Size(49, 39);
            this.lblCurrentCalendar.TabIndex = 0;
            this.lblCurrentCalendar.Text = "Current Calendar:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Controls.Add(this.lvCompaniesCalendars);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(265, 232);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // lvCompaniesCalendars
            // 
            this.lvCompaniesCalendars.Location = new System.Drawing.Point(2, 49);
            this.lvCompaniesCalendars.Name = "lvCompaniesCalendars";
            this.lvCompaniesCalendars.Size = new System.Drawing.Size(340, 177);
            this.lvCompaniesCalendars.TabIndex = 1;
            this.lvCompaniesCalendars.UseCompatibleStateImageBehavior = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.94118F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.05882F));
            this.tableLayoutPanel2.Controls.Add(this.lblCurrentCalendar, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lstSupplierCalendarHistory
            // 
            this.lstSupplierCalendarHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSupplierCalendarHistory.FormattingEnabled = true;
            this.lstSupplierCalendarHistory.Location = new System.Drawing.Point(3, 268);
            this.lstSupplierCalendarHistory.Name = "lstSupplierCalendarHistory";
            this.lstSupplierCalendarHistory.Size = new System.Drawing.Size(226, 52);
            this.lstSupplierCalendarHistory.TabIndex = 3;
            // 
            // lblCalHistory
            // 
            this.lblCalHistory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCalHistory.AutoSize = true;
            this.lblCalHistory.Location = new System.Drawing.Point(3, 243);
            this.lblCalHistory.Name = "lblCalHistory";
            this.lblCalHistory.Size = new System.Drawing.Size(84, 13);
            this.lblCalHistory.TabIndex = 2;
            this.lblCalHistory.Text = "Calendar History";
            this.lblCalHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSuppliersCurrentCalendar
            // 
            this.txtSuppliersCurrentCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.SetColumnSpan(this.txtSuppliersCurrentCalendar, 2);
            this.txtSuppliersCurrentCalendar.Location = new System.Drawing.Point(97, 7);
            this.txtSuppliersCurrentCalendar.Name = "txtSuppliersCurrentCalendar";
            this.txtSuppliersCurrentCalendar.ReadOnly = true;
            this.txtSuppliersCurrentCalendar.Size = new System.Drawing.Size(244, 20);
            this.txtSuppliersCurrentCalendar.TabIndex = 1;
            // 
            // lblCurrentCal
            // 
            this.lblCurrentCal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentCal.AutoSize = true;
            this.lblCurrentCal.Location = new System.Drawing.Point(3, 4);
            this.lblCurrentCal.Name = "lblCurrentCal";
            this.lblCurrentCal.Size = new System.Drawing.Size(52, 26);
            this.lblCurrentCal.TabIndex = 0;
            this.lblCurrentCal.Text = "Current Calendar:";
            // 
            // grpOperatorDetails
            // 
            this.grpOperatorDetails.Controls.Add(this.tableLayoutPanel4);
            this.grpOperatorDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOperatorDetails.Location = new System.Drawing.Point(235, 238);
            this.grpOperatorDetails.Name = "grpOperatorDetails";
            this.tableLayoutPanel5.SetRowSpan(this.grpOperatorDetails, 3);
            this.grpOperatorDetails.Size = new System.Drawing.Size(344, 113);
            this.grpOperatorDetails.TabIndex = 5;
            this.grpOperatorDetails.TabStop = false;
            this.grpOperatorDetails.Text = "Details";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69F));
            this.tableLayoutPanel4.Controls.Add(this.txtSuppliersNextYearEnd, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.txtSuppliersNextPeriodEnd, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.txtSuppliersCurrentPeriod, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblCntWeek, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtSuppliersCurrentWeek, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblCntPeriod, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(338, 94);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // txtSuppliersNextYearEnd
            // 
            this.txtSuppliersNextYearEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSuppliersNextYearEnd.Location = new System.Drawing.Point(107, 72);
            this.txtSuppliersNextYearEnd.Name = "txtSuppliersNextYearEnd";
            this.txtSuppliersNextYearEnd.ReadOnly = true;
            this.txtSuppliersNextYearEnd.Size = new System.Drawing.Size(212, 20);
            this.txtSuppliersNextYearEnd.TabIndex = 7;
            // 
            // txtSuppliersNextPeriodEnd
            // 
            this.txtSuppliersNextPeriodEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSuppliersNextPeriodEnd.Location = new System.Drawing.Point(107, 49);
            this.txtSuppliersNextPeriodEnd.Name = "txtSuppliersNextPeriodEnd";
            this.txtSuppliersNextPeriodEnd.ReadOnly = true;
            this.txtSuppliersNextPeriodEnd.Size = new System.Drawing.Size(212, 20);
            this.txtSuppliersNextPeriodEnd.TabIndex = 5;
            // 
            // txtSuppliersCurrentPeriod
            // 
            this.txtSuppliersCurrentPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSuppliersCurrentPeriod.Location = new System.Drawing.Point(107, 26);
            this.txtSuppliersCurrentPeriod.Name = "txtSuppliersCurrentPeriod";
            this.txtSuppliersCurrentPeriod.ReadOnly = true;
            this.txtSuppliersCurrentPeriod.Size = new System.Drawing.Size(77, 20);
            this.txtSuppliersCurrentPeriod.TabIndex = 3;
            // 
            // lblCntWeek
            // 
            this.lblCntWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCntWeek.AutoSize = true;
            this.lblCntWeek.Location = new System.Drawing.Point(3, 5);
            this.lblCntWeek.Name = "lblCntWeek";
            this.lblCntWeek.Size = new System.Drawing.Size(76, 13);
            this.lblCntWeek.TabIndex = 0;
            this.lblCntWeek.Text = "Current Week:";
            // 
            // txtSuppliersCurrentWeek
            // 
            this.txtSuppliersCurrentWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSuppliersCurrentWeek.Location = new System.Drawing.Point(107, 3);
            this.txtSuppliersCurrentWeek.Name = "txtSuppliersCurrentWeek";
            this.txtSuppliersCurrentWeek.ReadOnly = true;
            this.txtSuppliersCurrentWeek.Size = new System.Drawing.Size(77, 20);
            this.txtSuppliersCurrentWeek.TabIndex = 1;
            // 
            // lblCntPeriod
            // 
            this.lblCntPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCntPeriod.AutoSize = true;
            this.lblCntPeriod.Location = new System.Drawing.Point(3, 28);
            this.lblCntPeriod.Name = "lblCntPeriod";
            this.lblCntPeriod.Size = new System.Drawing.Size(77, 13);
            this.lblCntPeriod.TabIndex = 2;
            this.lblCntPeriod.Text = "Current Period:";
            // 
            // lbl
            // 
            this.lbl.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(3, 51);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(87, 13);
            this.lbl.TabIndex = 4;
            this.lbl.Text = "Next Period End:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Next Year End:";
            // 
            // ExportCalendar
            // 
            this.ExportCalendar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ExportCalendar.Location = new System.Drawing.Point(129, 326);
            this.ExportCalendar.Name = "ExportCalendar";
            this.ExportCalendar.Size = new System.Drawing.Size(100, 25);
            this.ExportCalendar.TabIndex = 4;
            this.ExportCalendar.Text = "&Export Calendar";
            this.ExportCalendar.UseVisualStyleBackColor = true;
            this.ExportCalendar.Click += new System.EventHandler(this.ExportCalendar_Click);
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.tableLayoutPanel5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(582, 354);
            this.panel5.TabIndex = 4;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.Controls.Add(this.ExportCalendar, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.lblCalHistory, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.grpOperatorDetails, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lstSupplierCalendarHistory, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lstSuppliers, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(582, 354);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tableLayoutPanel6.Controls.Add(this.lbl_Status, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.lblCurrentCal, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lvSuppliersCalendar, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.btnSuppliersCalendarApply, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.txtSuppliersCurrentCalendar, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(235, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(344, 229);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // lbl_Status
            // 
            this.lbl_Status.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Status.AutoSize = true;
            this.tableLayoutPanel6.SetColumnSpan(this.lbl_Status, 2);
            this.lbl_Status.ForeColor = System.Drawing.Color.Red;
            this.lbl_Status.Location = new System.Drawing.Point(3, 202);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(0, 13);
            this.lbl_Status.TabIndex = 3;
            // 
            // lvSuppliersCalendar
            // 
            this.lvSuppliersCalendar.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.CalendarId,
            this.OperName,
            this.StartDate,
            this.EndDate});
            this.tableLayoutPanel6.SetColumnSpan(this.lvSuppliersCalendar, 3);
            this.lvSuppliersCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSuppliersCalendar.FullRowSelect = true;
            this.lvSuppliersCalendar.GridLines = true;
            this.lvSuppliersCalendar.Location = new System.Drawing.Point(3, 38);
            this.lvSuppliersCalendar.MultiSelect = false;
            this.lvSuppliersCalendar.Name = "lvSuppliersCalendar";
            this.lvSuppliersCalendar.Size = new System.Drawing.Size(338, 148);
            this.lvSuppliersCalendar.TabIndex = 2;
            this.lvSuppliersCalendar.UseCompatibleStateImageBehavior = false;
            this.lvSuppliersCalendar.View = System.Windows.Forms.View.Details;
            this.lvSuppliersCalendar.SelectedIndexChanged += new System.EventHandler(this.lvSuppliersCalendar_SelectedIndexChanged);
            // 
            // CalendarId
            // 
            this.CalendarId.Text = "CalendarId";
            this.CalendarId.Width = 1;
            // 
            // OperName
            // 
            this.OperName.Text = "Name";
            this.OperName.Width = 200;
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
            // btnSuppliersCalendarApply
            // 
            this.btnSuppliersCalendarApply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSuppliersCalendarApply.Location = new System.Drawing.Point(241, 195);
            this.btnSuppliersCalendarApply.Name = "btnSuppliersCalendarApply";
            this.btnSuppliersCalendarApply.Size = new System.Drawing.Size(100, 28);
            this.btnSuppliersCalendarApply.TabIndex = 4;
            this.btnSuppliersCalendarApply.Text = "&Apply";
            this.btnSuppliersCalendarApply.UseVisualStyleBackColor = true;
            this.btnSuppliersCalendarApply.Click += new System.EventHandler(this.btnSuppliersCalendarApply_Click);
            // 
            // lstSuppliers
            // 
            this.lstSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSuppliers.FormattingEnabled = true;
            this.lstSuppliers.Location = new System.Drawing.Point(3, 3);
            this.lstSuppliers.Name = "lstSuppliers";
            this.lstSuppliers.Size = new System.Drawing.Size(226, 229);
            this.lstSuppliers.TabIndex = 0;
            this.lstSuppliers.SelectedIndexChanged += new System.EventHandler(this.lstSuppliers_SelectedIndexChanged);
            // 
            // ucOperatorCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel5);
            this.Name = "ucOperatorCalendar";
            this.Size = new System.Drawing.Size(582, 354);
            this.Load += new System.EventHandler(this.ucOperatorCalendar_Load);
            this.grpDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.grpOperatorDetails.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtNextYrEnd;
        private System.Windows.Forms.TextBox txtNextPeriod;
        private System.Windows.Forms.Label lblCurrentWeek;
        private System.Windows.Forms.Label lblCurrentPeriod;
        private System.Windows.Forms.Label lblNextYrEnd;
        private System.Windows.Forms.Label lblNextPeriod;
        private System.Windows.Forms.TextBox txtCurrentWeek;
        private System.Windows.Forms.TextBox txtCurrentPeriod;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExportCalendar;
        private System.Windows.Forms.Label lblCalendarHistory;
        private System.Windows.Forms.ListBox lstCompanyCalendarHistory;
        private System.Windows.Forms.TextBox txtCurrentCalendar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblCurrentCalendar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ListView lvCompaniesCalendars;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lstSupplierCalendarHistory;
        private System.Windows.Forms.Label lblCalHistory;
        private System.Windows.Forms.Label lblCurrentCal;
        private System.Windows.Forms.TextBox txtSuppliersCurrentCalendar;
        private System.Windows.Forms.GroupBox grpOperatorDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblCntWeek;
        private System.Windows.Forms.TextBox txtSuppliersCurrentWeek;
        private System.Windows.Forms.Label lblCntPeriod;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSuppliersNextYearEnd;
        private System.Windows.Forms.TextBox txtSuppliersNextPeriodEnd;
        private System.Windows.Forms.TextBox txtSuppliersCurrentPeriod;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnSuppliersCalendarApply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button ExportCalendar;
        private System.Windows.Forms.ListBox lstSuppliers;
        private System.Windows.Forms.ListView lvSuppliersCalendar;
        private System.Windows.Forms.ColumnHeader CalendarId;
        private System.Windows.Forms.ColumnHeader OperName;
        private System.Windows.Forms.ColumnHeader StartDate;
        private System.Windows.Forms.ColumnHeader EndDate;
        private System.Windows.Forms.Label lbl_Status;
    }
}
