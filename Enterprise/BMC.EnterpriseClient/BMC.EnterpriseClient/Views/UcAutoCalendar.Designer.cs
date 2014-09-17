namespace BMC.EnterpriseClient.Views
{
    partial class UcAutoCalendar
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
            this.tlp_MainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gb_ProfileDetails = new System.Windows.Forms.GroupBox();
            this.tlp_ProfileDetails = new System.Windows.Forms.TableLayoutPanel();
            this.cb_IsAutoCalendarEnabled = new System.Windows.Forms.CheckBox();
            this.txt_ProfileName = new System.Windows.Forms.TextBox();
            this.nud_CreateBeforeDays = new System.Windows.Forms.NumericUpDown();
            this.nud_AlertBeforeDays = new System.Windows.Forms.NumericUpDown();
            this.nud_AlertRecurrenceDays = new System.Windows.Forms.NumericUpDown();
            this.lbl_AlertRecurrence = new System.Windows.Forms.Label();
            this.lbl_AlertBefore = new System.Windows.Forms.Label();
            this.lbl_CreateBeforeDays = new System.Windows.Forms.Label();
            this.lbl_Profile = new System.Windows.Forms.Label();
            this.cb_IsCalendarBasedonDays = new System.Windows.Forms.CheckBox();
            this.cbo_Days = new System.Windows.Forms.ComboBox();
            this.cb_SetNewCalendarActive = new System.Windows.Forms.CheckBox();
            this.tlp_SubcompanyDetails = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_SubCompanyButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Unassign = new System.Windows.Forms.Button();
            this.btn_Assign = new System.Windows.Forms.Button();
            this.btn_AssignAll = new System.Windows.Forms.Button();
            this.btn_UnassignAll = new System.Windows.Forms.Button();
            this.lbl_UnAssignedSubCompany = new System.Windows.Forms.Label();
            this.lbl_AssignedSubCompany = new System.Windows.Forms.Label();
            this.lb_UnassignedSubCompany = new System.Windows.Forms.ListBox();
            this.lb_AssignedSubCompany = new System.Windows.Forms.ListBox();
            this.lb_CalendarProfiles = new System.Windows.Forms.ListBox();
            this.tbl_Buttons = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_NewAutoCalendarProfile = new System.Windows.Forms.Button();
            this.tlp_MainPanel.SuspendLayout();
            this.gb_ProfileDetails.SuspendLayout();
            this.tlp_ProfileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_CreateBeforeDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AlertBeforeDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AlertRecurrenceDays)).BeginInit();
            this.tlp_SubcompanyDetails.SuspendLayout();
            this.tlp_SubCompanyButtons.SuspendLayout();
            this.tbl_Buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_MainPanel
            // 
            this.tlp_MainPanel.ColumnCount = 2;
            this.tlp_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.71134F));
            this.tlp_MainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.28866F));
            this.tlp_MainPanel.Controls.Add(this.gb_ProfileDetails, 1, 0);
            this.tlp_MainPanel.Controls.Add(this.tlp_SubcompanyDetails, 1, 1);
            this.tlp_MainPanel.Controls.Add(this.lb_CalendarProfiles, 0, 0);
            this.tlp_MainPanel.Controls.Add(this.tbl_Buttons, 1, 2);
            this.tlp_MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_MainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlp_MainPanel.Name = "tlp_MainPanel";
            this.tlp_MainPanel.RowCount = 3;
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.14652F));
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.85348F));
            this.tlp_MainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_MainPanel.Size = new System.Drawing.Size(840, 587);
            this.tlp_MainPanel.TabIndex = 0;
            // 
            // gb_ProfileDetails
            // 
            this.gb_ProfileDetails.Controls.Add(this.tlp_ProfileDetails);
            this.gb_ProfileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_ProfileDetails.Location = new System.Drawing.Point(202, 3);
            this.gb_ProfileDetails.Name = "gb_ProfileDetails";
            this.gb_ProfileDetails.Size = new System.Drawing.Size(635, 104);
            this.gb_ProfileDetails.TabIndex = 1;
            this.gb_ProfileDetails.TabStop = false;
            this.gb_ProfileDetails.Text = "Auto Calendar Profile Details";
            // 
            // tlp_ProfileDetails
            // 
            this.tlp_ProfileDetails.ColumnCount = 5;
            this.tlp_ProfileDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tlp_ProfileDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tlp_ProfileDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_ProfileDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tlp_ProfileDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlp_ProfileDetails.Controls.Add(this.cb_IsAutoCalendarEnabled, 0, 1);
            this.tlp_ProfileDetails.Controls.Add(this.txt_ProfileName, 1, 0);
            this.tlp_ProfileDetails.Controls.Add(this.nud_CreateBeforeDays, 4, 0);
            this.tlp_ProfileDetails.Controls.Add(this.nud_AlertBeforeDays, 4, 1);
            this.tlp_ProfileDetails.Controls.Add(this.nud_AlertRecurrenceDays, 4, 2);
            this.tlp_ProfileDetails.Controls.Add(this.lbl_AlertRecurrence, 3, 2);
            this.tlp_ProfileDetails.Controls.Add(this.lbl_AlertBefore, 3, 1);
            this.tlp_ProfileDetails.Controls.Add(this.lbl_CreateBeforeDays, 3, 0);
            this.tlp_ProfileDetails.Controls.Add(this.lbl_Profile, 0, 0);
            this.tlp_ProfileDetails.Controls.Add(this.cb_IsCalendarBasedonDays, 0, 2);
            this.tlp_ProfileDetails.Controls.Add(this.cbo_Days, 1, 2);
            this.tlp_ProfileDetails.Controls.Add(this.cb_SetNewCalendarActive, 1, 1);
            this.tlp_ProfileDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_ProfileDetails.Location = new System.Drawing.Point(3, 16);
            this.tlp_ProfileDetails.Name = "tlp_ProfileDetails";
            this.tlp_ProfileDetails.RowCount = 3;
            this.tlp_ProfileDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_ProfileDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_ProfileDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_ProfileDetails.Size = new System.Drawing.Size(629, 85);
            this.tlp_ProfileDetails.TabIndex = 0;
            // 
            // cb_IsAutoCalendarEnabled
            // 
            this.cb_IsAutoCalendarEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_IsAutoCalendarEnabled.AutoSize = true;
            this.cb_IsAutoCalendarEnabled.Checked = true;
            this.cb_IsAutoCalendarEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_IsAutoCalendarEnabled.Location = new System.Drawing.Point(3, 33);
            this.cb_IsAutoCalendarEnabled.Name = "cb_IsAutoCalendarEnabled";
            this.cb_IsAutoCalendarEnabled.Size = new System.Drawing.Size(108, 17);
            this.cb_IsAutoCalendarEnabled.TabIndex = 2;
            this.cb_IsAutoCalendarEnabled.Text = "Is Profile Enabled";
            this.cb_IsAutoCalendarEnabled.UseVisualStyleBackColor = true;
            // 
            // txt_ProfileName
            // 
            this.txt_ProfileName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_ProfileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ProfileName.Location = new System.Drawing.Point(168, 4);
            this.txt_ProfileName.MaxLength = 20;
            this.txt_ProfileName.Name = "txt_ProfileName";
            this.txt_ProfileName.Size = new System.Drawing.Size(167, 20);
            this.txt_ProfileName.TabIndex = 1;
            // 
            // nud_CreateBeforeDays
            // 
            this.nud_CreateBeforeDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nud_CreateBeforeDays.Location = new System.Drawing.Point(482, 4);
            this.nud_CreateBeforeDays.Name = "nud_CreateBeforeDays";
            this.nud_CreateBeforeDays.Size = new System.Drawing.Size(71, 20);
            this.nud_CreateBeforeDays.TabIndex = 7;
            // 
            // nud_AlertBeforeDays
            // 
            this.nud_AlertBeforeDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nud_AlertBeforeDays.Location = new System.Drawing.Point(482, 32);
            this.nud_AlertBeforeDays.Name = "nud_AlertBeforeDays";
            this.nud_AlertBeforeDays.Size = new System.Drawing.Size(71, 20);
            this.nud_AlertBeforeDays.TabIndex = 9;
            // 
            // nud_AlertRecurrenceDays
            // 
            this.nud_AlertRecurrenceDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nud_AlertRecurrenceDays.Location = new System.Drawing.Point(482, 60);
            this.nud_AlertRecurrenceDays.Name = "nud_AlertRecurrenceDays";
            this.nud_AlertRecurrenceDays.Size = new System.Drawing.Size(71, 20);
            this.nud_AlertRecurrenceDays.TabIndex = 11;
            // 
            // lbl_AlertRecurrence
            // 
            this.lbl_AlertRecurrence.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_AlertRecurrence.AutoSize = true;
            this.lbl_AlertRecurrence.Location = new System.Drawing.Point(345, 64);
            this.lbl_AlertRecurrence.Name = "lbl_AlertRecurrence";
            this.lbl_AlertRecurrence.Size = new System.Drawing.Size(124, 13);
            this.lbl_AlertRecurrence.TabIndex = 10;
            this.lbl_AlertRecurrence.Text = "Alert Recurrence (days) :";
            // 
            // lbl_AlertBefore
            // 
            this.lbl_AlertBefore.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_AlertBefore.AutoSize = true;
            this.lbl_AlertBefore.Location = new System.Drawing.Point(345, 35);
            this.lbl_AlertBefore.Name = "lbl_AlertBefore";
            this.lbl_AlertBefore.Size = new System.Drawing.Size(99, 13);
            this.lbl_AlertBefore.TabIndex = 8;
            this.lbl_AlertBefore.Text = "Alert Before (days) :";
            // 
            // lbl_CreateBeforeDays
            // 
            this.lbl_CreateBeforeDays.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CreateBeforeDays.AutoSize = true;
            this.lbl_CreateBeforeDays.Location = new System.Drawing.Point(345, 7);
            this.lbl_CreateBeforeDays.Name = "lbl_CreateBeforeDays";
            this.lbl_CreateBeforeDays.Size = new System.Drawing.Size(109, 13);
            this.lbl_CreateBeforeDays.TabIndex = 6;
            this.lbl_CreateBeforeDays.Text = "Create Before (days) :";
            // 
            // lbl_Profile
            // 
            this.lbl_Profile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Profile.AutoSize = true;
            this.lbl_Profile.Location = new System.Drawing.Point(3, 7);
            this.lbl_Profile.Name = "lbl_Profile";
            this.lbl_Profile.Size = new System.Drawing.Size(73, 13);
            this.lbl_Profile.TabIndex = 0;
            this.lbl_Profile.Text = "Profile Name :";
            // 
            // cb_IsCalendarBasedonDays
            // 
            this.cb_IsCalendarBasedonDays.AutoSize = true;
            this.cb_IsCalendarBasedonDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_IsCalendarBasedonDays.Location = new System.Drawing.Point(3, 59);
            this.cb_IsCalendarBasedonDays.Name = "cb_IsCalendarBasedonDays";
            this.cb_IsCalendarBasedonDays.Size = new System.Drawing.Size(159, 23);
            this.cb_IsCalendarBasedonDays.TabIndex = 4;
            this.cb_IsCalendarBasedonDays.Text = "Is CalendarBased on Days";
            this.cb_IsCalendarBasedonDays.UseVisualStyleBackColor = true;
            this.cb_IsCalendarBasedonDays.CheckedChanged += new System.EventHandler(this.cb_IsCalendarBasedonDays_CheckedChanged);
            // 
            // cbo_Days
            // 
            this.cbo_Days.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbo_Days.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Days.Enabled = false;
            this.cbo_Days.FormattingEnabled = true;
            this.cbo_Days.Location = new System.Drawing.Point(168, 60);
            this.cbo_Days.Name = "cbo_Days";
            this.cbo_Days.Size = new System.Drawing.Size(146, 21);
            this.cbo_Days.TabIndex = 5;
            // 
            // cb_SetNewCalendarActive
            // 
            this.cb_SetNewCalendarActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_SetNewCalendarActive.AutoSize = true;
            this.cb_SetNewCalendarActive.Location = new System.Drawing.Point(168, 33);
            this.cb_SetNewCalendarActive.Name = "cb_SetNewCalendarActive";
            this.cb_SetNewCalendarActive.Size = new System.Drawing.Size(145, 17);
            this.cb_SetNewCalendarActive.TabIndex = 3;
            this.cb_SetNewCalendarActive.Text = "Set New Calendar Active";
            this.cb_SetNewCalendarActive.UseVisualStyleBackColor = true;
            // 
            // tlp_SubcompanyDetails
            // 
            this.tlp_SubcompanyDetails.ColumnCount = 3;
            this.tlp_SubcompanyDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SubcompanyDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_SubcompanyDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SubcompanyDetails.Controls.Add(this.tlp_SubCompanyButtons, 1, 1);
            this.tlp_SubcompanyDetails.Controls.Add(this.lbl_UnAssignedSubCompany, 0, 0);
            this.tlp_SubcompanyDetails.Controls.Add(this.lbl_AssignedSubCompany, 2, 0);
            this.tlp_SubcompanyDetails.Controls.Add(this.lb_UnassignedSubCompany, 0, 1);
            this.tlp_SubcompanyDetails.Controls.Add(this.lb_AssignedSubCompany, 2, 1);
            this.tlp_SubcompanyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_SubcompanyDetails.Location = new System.Drawing.Point(202, 113);
            this.tlp_SubcompanyDetails.Name = "tlp_SubcompanyDetails";
            this.tlp_SubcompanyDetails.RowCount = 2;
            this.tlp_SubcompanyDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.tlp_SubcompanyDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.87755F));
            this.tlp_SubcompanyDetails.Size = new System.Drawing.Size(635, 430);
            this.tlp_SubcompanyDetails.TabIndex = 2;
            // 
            // tlp_SubCompanyButtons
            // 
            this.tlp_SubCompanyButtons.ColumnCount = 1;
            this.tlp_SubCompanyButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_SubCompanyButtons.Controls.Add(this.btn_Unassign, 0, 3);
            this.tlp_SubCompanyButtons.Controls.Add(this.btn_Assign, 0, 2);
            this.tlp_SubCompanyButtons.Controls.Add(this.btn_AssignAll, 0, 1);
            this.tlp_SubCompanyButtons.Controls.Add(this.btn_UnassignAll, 0, 4);
            this.tlp_SubCompanyButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_SubCompanyButtons.Location = new System.Drawing.Point(295, 29);
            this.tlp_SubCompanyButtons.Name = "tlp_SubCompanyButtons";
            this.tlp_SubCompanyButtons.RowCount = 6;
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_SubCompanyButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_SubCompanyButtons.Size = new System.Drawing.Size(44, 398);
            this.tlp_SubCompanyButtons.TabIndex = 2;
            // 
            // btn_Unassign
            // 
            this.btn_Unassign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Unassign.Location = new System.Drawing.Point(3, 202);
            this.btn_Unassign.Name = "btn_Unassign";
            this.btn_Unassign.Size = new System.Drawing.Size(37, 34);
            this.btn_Unassign.TabIndex = 2;
            this.btn_Unassign.Text = "<";
            this.btn_Unassign.UseVisualStyleBackColor = true;
            this.btn_Unassign.Click += new System.EventHandler(this.btn_Unassign_Click);
            // 
            // btn_Assign
            // 
            this.btn_Assign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Assign.Location = new System.Drawing.Point(3, 162);
            this.btn_Assign.Name = "btn_Assign";
            this.btn_Assign.Size = new System.Drawing.Size(37, 34);
            this.btn_Assign.TabIndex = 1;
            this.btn_Assign.Text = ">";
            this.btn_Assign.UseVisualStyleBackColor = true;
            this.btn_Assign.Click += new System.EventHandler(this.btn_Assign_Click);
            // 
            // btn_AssignAll
            // 
            this.btn_AssignAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_AssignAll.Location = new System.Drawing.Point(3, 122);
            this.btn_AssignAll.Name = "btn_AssignAll";
            this.btn_AssignAll.Size = new System.Drawing.Size(37, 34);
            this.btn_AssignAll.TabIndex = 0;
            this.btn_AssignAll.Text = ">>";
            this.btn_AssignAll.UseVisualStyleBackColor = true;
            this.btn_AssignAll.Click += new System.EventHandler(this.btn_AssignAll_Click);
            // 
            // btn_UnassignAll
            // 
            this.btn_UnassignAll.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_UnassignAll.Location = new System.Drawing.Point(3, 242);
            this.btn_UnassignAll.Name = "btn_UnassignAll";
            this.btn_UnassignAll.Size = new System.Drawing.Size(37, 34);
            this.btn_UnassignAll.TabIndex = 3;
            this.btn_UnassignAll.Text = "<<";
            this.btn_UnassignAll.UseVisualStyleBackColor = true;
            this.btn_UnassignAll.Click += new System.EventHandler(this.btn_UnassignAll_Click);
            // 
            // lbl_UnAssignedSubCompany
            // 
            this.lbl_UnAssignedSubCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_UnAssignedSubCompany.AutoSize = true;
            this.lbl_UnAssignedSubCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UnAssignedSubCompany.Location = new System.Drawing.Point(3, 6);
            this.lbl_UnAssignedSubCompany.Name = "lbl_UnAssignedSubCompany";
            this.lbl_UnAssignedSubCompany.Size = new System.Drawing.Size(115, 13);
            this.lbl_UnAssignedSubCompany.TabIndex = 0;
            this.lbl_UnAssignedSubCompany.Text = "Other SubCompany";
            // 
            // lbl_AssignedSubCompany
            // 
            this.lbl_AssignedSubCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_AssignedSubCompany.AutoSize = true;
            this.lbl_AssignedSubCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AssignedSubCompany.Location = new System.Drawing.Point(345, 6);
            this.lbl_AssignedSubCompany.Name = "lbl_AssignedSubCompany";
            this.lbl_AssignedSubCompany.Size = new System.Drawing.Size(135, 13);
            this.lbl_AssignedSubCompany.TabIndex = 3;
            this.lbl_AssignedSubCompany.Text = "Assigned SubCompany";
            // 
            // lb_UnassignedSubCompany
            // 
            this.lb_UnassignedSubCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_UnassignedSubCompany.FormattingEnabled = true;
            this.lb_UnassignedSubCompany.Location = new System.Drawing.Point(3, 29);
            this.lb_UnassignedSubCompany.Name = "lb_UnassignedSubCompany";
            this.lb_UnassignedSubCompany.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lb_UnassignedSubCompany.Size = new System.Drawing.Size(286, 398);
            this.lb_UnassignedSubCompany.TabIndex = 1;
            // 
            // lb_AssignedSubCompany
            // 
            this.lb_AssignedSubCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_AssignedSubCompany.FormattingEnabled = true;
            this.lb_AssignedSubCompany.Location = new System.Drawing.Point(345, 29);
            this.lb_AssignedSubCompany.Name = "lb_AssignedSubCompany";
            this.lb_AssignedSubCompany.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lb_AssignedSubCompany.Size = new System.Drawing.Size(287, 398);
            this.lb_AssignedSubCompany.TabIndex = 4;
            // 
            // lb_CalendarProfiles
            // 
            this.lb_CalendarProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_CalendarProfiles.FormattingEnabled = true;
            this.lb_CalendarProfiles.Location = new System.Drawing.Point(3, 3);
            this.lb_CalendarProfiles.Name = "lb_CalendarProfiles";
            this.tlp_MainPanel.SetRowSpan(this.lb_CalendarProfiles, 2);
            this.lb_CalendarProfiles.Size = new System.Drawing.Size(193, 540);
            this.lb_CalendarProfiles.TabIndex = 0;
            this.lb_CalendarProfiles.SelectedIndexChanged += new System.EventHandler(this.lb_CalendarProfiles_SelectedIndexChanged);
            // 
            // tbl_Buttons
            // 
            this.tbl_Buttons.ColumnCount = 5;
            this.tbl_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tbl_Buttons.Controls.Add(this.btn_Delete, 4, 0);
            this.tbl_Buttons.Controls.Add(this.btn_Update, 3, 0);
            this.tbl_Buttons.Controls.Add(this.btn_NewAutoCalendarProfile, 2, 0);
            this.tbl_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbl_Buttons.Location = new System.Drawing.Point(202, 549);
            this.tbl_Buttons.Name = "tbl_Buttons";
            this.tbl_Buttons.RowCount = 1;
            this.tbl_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbl_Buttons.Size = new System.Drawing.Size(635, 35);
            this.tbl_Buttons.TabIndex = 3;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Delete.Location = new System.Drawing.Point(533, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(99, 28);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "&Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Update.Location = new System.Drawing.Point(428, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(99, 28);
            this.btn_Update.TabIndex = 1;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_NewAutoCalendarProfile
            // 
            this.btn_NewAutoCalendarProfile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_NewAutoCalendarProfile.Location = new System.Drawing.Point(323, 3);
            this.btn_NewAutoCalendarProfile.Name = "btn_NewAutoCalendarProfile";
            this.btn_NewAutoCalendarProfile.Size = new System.Drawing.Size(99, 28);
            this.btn_NewAutoCalendarProfile.TabIndex = 0;
            this.btn_NewAutoCalendarProfile.Text = "&New";
            this.btn_NewAutoCalendarProfile.UseVisualStyleBackColor = true;
            this.btn_NewAutoCalendarProfile.Click += new System.EventHandler(this.btn_NewAutoCalendarProfile_Click);
            // 
            // UcAutoCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlp_MainPanel);
            this.Name = "UcAutoCalendar";
            this.Size = new System.Drawing.Size(840, 587);
            this.Load += new System.EventHandler(this.UcAutoCalendar_Load);
            this.tlp_MainPanel.ResumeLayout(false);
            this.gb_ProfileDetails.ResumeLayout(false);
            this.tlp_ProfileDetails.ResumeLayout(false);
            this.tlp_ProfileDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_CreateBeforeDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AlertBeforeDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_AlertRecurrenceDays)).EndInit();
            this.tlp_SubcompanyDetails.ResumeLayout(false);
            this.tlp_SubcompanyDetails.PerformLayout();
            this.tlp_SubCompanyButtons.ResumeLayout(false);
            this.tbl_Buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_MainPanel;
        private System.Windows.Forms.GroupBox gb_ProfileDetails;
        private System.Windows.Forms.TableLayoutPanel tlp_SubcompanyDetails;
        private System.Windows.Forms.TableLayoutPanel tlp_SubCompanyButtons;
        private System.Windows.Forms.Button btn_Unassign;
        private System.Windows.Forms.Button btn_Assign;
        private System.Windows.Forms.Label lbl_UnAssignedSubCompany;
        private System.Windows.Forms.Label lbl_AssignedSubCompany;
        private System.Windows.Forms.ListBox lb_UnassignedSubCompany;
        private System.Windows.Forms.ListBox lb_AssignedSubCompany;
        private System.Windows.Forms.Button btn_NewAutoCalendarProfile;
        private System.Windows.Forms.ListBox lb_CalendarProfiles;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TableLayoutPanel tbl_Buttons;
        private System.Windows.Forms.TableLayoutPanel tlp_ProfileDetails;
        private System.Windows.Forms.Label lbl_AlertBefore;
        private System.Windows.Forms.Label lbl_CreateBeforeDays;
        private System.Windows.Forms.NumericUpDown nud_CreateBeforeDays;
        private System.Windows.Forms.Label lbl_AlertRecurrence;
        private System.Windows.Forms.NumericUpDown nud_AlertBeforeDays;
        private System.Windows.Forms.NumericUpDown nud_AlertRecurrenceDays;
        private System.Windows.Forms.CheckBox cb_IsAutoCalendarEnabled;
        private System.Windows.Forms.Label lbl_Profile;
        private System.Windows.Forms.TextBox txt_ProfileName;
        private System.Windows.Forms.Button btn_UnassignAll;
        private System.Windows.Forms.Button btn_AssignAll;
        private System.Windows.Forms.CheckBox cb_IsCalendarBasedonDays;
        private System.Windows.Forms.ComboBox cbo_Days;
        private System.Windows.Forms.CheckBox cb_SetNewCalendarActive;
    }
}
