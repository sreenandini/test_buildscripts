namespace BMC.ComponentVerification.UI
{
    partial class ViewComponents
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewComponents));
            this.pnlComponentScreen = new System.Windows.Forms.Panel();
            this.grpBoxCompVeriDetails = new System.Windows.Forms.GroupBox();
            this.grpBoxReqVer = new System.Windows.Forms.GroupBox();
            this.btnRVClear = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.cmbSerial = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.cmbComp = new System.Windows.Forms.ComboBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblComp = new System.Windows.Forms.Label();
            this.grpSearchCriteria = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gpSite = new System.Windows.Forms.GroupBox();
            this.cmbMachineSerial = new System.Windows.Forms.ComboBox();
            this.lblSiteName = new System.Windows.Forms.Label();
            this.cmbSiteName = new System.Windows.Forms.ComboBox();
            this.cmbCompName = new System.Windows.Forms.ComboBox();
            this.lblSerialNo = new System.Windows.Forms.Label();
            this.lblCompName = new System.Windows.Forms.Label();
            this.gpDates = new System.Windows.Forms.GroupBox();
            this.cmbVerType = new System.Windows.Forms.ComboBox();
            this.lblVerType = new System.Windows.Forms.Label();
            this.cmbCompType = new System.Windows.Forms.ComboBox();
            this.lblCompType = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtPickerTo = new System.Windows.Forms.DateTimePicker();
            this.dtPickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dgCompDetails = new System.Windows.Forms.DataGridView();
            this.SiteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineSerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstallationNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComponentType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComponentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerificationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerificationStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerificationTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpBoxCompDetails = new System.Windows.Forms.GroupBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEditComponent = new System.Windows.Forms.Button();
            this.tvComponetDetails = new System.Windows.Forms.TreeView();
            this.pnlComponentScreen.SuspendLayout();
            this.grpBoxCompVeriDetails.SuspendLayout();
            this.grpBoxReqVer.SuspendLayout();
            this.grpSearchCriteria.SuspendLayout();
            this.gpSite.SuspendLayout();
            this.gpDates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompDetails)).BeginInit();
            this.grpBoxCompDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlComponentScreen
            // 
            this.pnlComponentScreen.Controls.Add(this.grpBoxCompVeriDetails);
            this.pnlComponentScreen.Controls.Add(this.grpBoxCompDetails);
            this.pnlComponentScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlComponentScreen.Location = new System.Drawing.Point(0, 0);
            this.pnlComponentScreen.Name = "pnlComponentScreen";
            this.pnlComponentScreen.Size = new System.Drawing.Size(1049, 669);
            this.pnlComponentScreen.TabIndex = 0;
            // 
            // grpBoxCompVeriDetails
            // 
            this.grpBoxCompVeriDetails.Controls.Add(this.grpBoxReqVer);
            this.grpBoxCompVeriDetails.Controls.Add(this.grpSearchCriteria);
            this.grpBoxCompVeriDetails.Controls.Add(this.dgCompDetails);
            this.grpBoxCompVeriDetails.Location = new System.Drawing.Point(258, 1);
            this.grpBoxCompVeriDetails.Name = "grpBoxCompVeriDetails";
            this.grpBoxCompVeriDetails.Size = new System.Drawing.Size(786, 663);
            this.grpBoxCompVeriDetails.TabIndex = 4;
            this.grpBoxCompVeriDetails.TabStop = false;
            this.grpBoxCompVeriDetails.Text = "Component Verification Details";
            // 
            // grpBoxReqVer
            // 
            this.grpBoxReqVer.Controls.Add(this.btnRVClear);
            this.grpBoxReqVer.Controls.Add(this.btnVerify);
            this.grpBoxReqVer.Controls.Add(this.cmbSerial);
            this.grpBoxReqVer.Controls.Add(this.lblSite);
            this.grpBoxReqVer.Controls.Add(this.cmbSite);
            this.grpBoxReqVer.Controls.Add(this.cmbComp);
            this.grpBoxReqVer.Controls.Add(this.lblSerial);
            this.grpBoxReqVer.Controls.Add(this.lblComp);
            this.grpBoxReqVer.Location = new System.Drawing.Point(530, 16);
            this.grpBoxReqVer.Name = "grpBoxReqVer";
            this.grpBoxReqVer.Size = new System.Drawing.Size(250, 188);
            this.grpBoxReqVer.TabIndex = 18;
            this.grpBoxReqVer.TabStop = false;
            this.grpBoxReqVer.Text = "Request Verification";
            // 
            // btnRVClear
            // 
            this.btnRVClear.Location = new System.Drawing.Point(129, 152);
            this.btnRVClear.Name = "btnRVClear";
            this.btnRVClear.Size = new System.Drawing.Size(112, 29);
            this.btnRVClear.TabIndex = 23;
            this.btnRVClear.Text = "Clear";
            this.btnRVClear.UseVisualStyleBackColor = true;
            this.btnRVClear.Click += new System.EventHandler(this.btnRVClear_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(9, 152);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(114, 29);
            this.btnVerify.TabIndex = 22;
            this.btnVerify.Text = "Request Verification";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // cmbSerial
            // 
            this.cmbSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerial.Enabled = false;
            this.cmbSerial.FormattingEnabled = true;
            this.cmbSerial.Location = new System.Drawing.Point(104, 56);
            this.cmbSerial.Name = "cmbSerial";
            this.cmbSerial.Size = new System.Drawing.Size(137, 21);
            this.cmbSerial.TabIndex = 20;
            this.cmbSerial.SelectedIndexChanged += new System.EventHandler(this.cmbSerial_SelectedIndexChanged);
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(6, 29);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(59, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "Site Name:";
            // 
            // cmbSite
            // 
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(104, 25);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(137, 21);
            this.cmbSite.TabIndex = 19;
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.cmbSite_SelectedIndexChanged);
            // 
            // cmbComp
            // 
            this.cmbComp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComp.Enabled = false;
            this.cmbComp.FormattingEnabled = true;
            this.cmbComp.Location = new System.Drawing.Point(104, 86);
            this.cmbComp.Name = "cmbComp";
            this.cmbComp.Size = new System.Drawing.Size(137, 21);
            this.cmbComp.TabIndex = 21;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(6, 59);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(97, 13);
            this.lblSerial.TabIndex = 2;
            this.lblSerial.Text = "Machine Serial No:";
            // 
            // lblComp
            // 
            this.lblComp.AutoSize = true;
            this.lblComp.Location = new System.Drawing.Point(6, 89);
            this.lblComp.Name = "lblComp";
            this.lblComp.Size = new System.Drawing.Size(91, 13);
            this.lblComp.TabIndex = 4;
            this.lblComp.Text = "Component Type:";
            // 
            // grpSearchCriteria
            // 
            this.grpSearchCriteria.Controls.Add(this.btnClear);
            this.grpSearchCriteria.Controls.Add(this.btnSearch);
            this.grpSearchCriteria.Controls.Add(this.gpSite);
            this.grpSearchCriteria.Controls.Add(this.gpDates);
            this.grpSearchCriteria.Location = new System.Drawing.Point(6, 16);
            this.grpSearchCriteria.Name = "grpSearchCriteria";
            this.grpSearchCriteria.Size = new System.Drawing.Size(518, 188);
            this.grpSearchCriteria.TabIndex = 5;
            this.grpSearchCriteria.TabStop = false;
            this.grpSearchCriteria.Text = "Search Criteria";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(326, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(112, 29);
            this.btnClear.TabIndex = 16;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(70, 152);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 29);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gpSite
            // 
            this.gpSite.Controls.Add(this.cmbMachineSerial);
            this.gpSite.Controls.Add(this.lblSiteName);
            this.gpSite.Controls.Add(this.cmbSiteName);
            this.gpSite.Controls.Add(this.cmbCompName);
            this.gpSite.Controls.Add(this.lblSerialNo);
            this.gpSite.Controls.Add(this.lblCompName);
            this.gpSite.Location = new System.Drawing.Point(258, 11);
            this.gpSite.Name = "gpSite";
            this.gpSite.Size = new System.Drawing.Size(253, 135);
            this.gpSite.TabIndex = 11;
            this.gpSite.TabStop = false;
            // 
            // cmbMachineSerial
            // 
            this.cmbMachineSerial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineSerial.FormattingEnabled = true;
            this.cmbMachineSerial.Location = new System.Drawing.Point(105, 45);
            this.cmbMachineSerial.Name = "cmbMachineSerial";
            this.cmbMachineSerial.Size = new System.Drawing.Size(137, 21);
            this.cmbMachineSerial.TabIndex = 13;
            this.cmbMachineSerial.SelectedIndexChanged += new System.EventHandler(this.cmbMachineSerial_SelectedIndexChanged);
            // 
            // lblSiteName
            // 
            this.lblSiteName.AutoSize = true;
            this.lblSiteName.Location = new System.Drawing.Point(6, 18);
            this.lblSiteName.Name = "lblSiteName";
            this.lblSiteName.Size = new System.Drawing.Size(59, 13);
            this.lblSiteName.TabIndex = 22;
            this.lblSiteName.Text = "Site Name:";
            // 
            // cmbSiteName
            // 
            this.cmbSiteName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteName.FormattingEnabled = true;
            this.cmbSiteName.Location = new System.Drawing.Point(105, 15);
            this.cmbSiteName.Name = "cmbSiteName";
            this.cmbSiteName.Size = new System.Drawing.Size(137, 21);
            this.cmbSiteName.TabIndex = 12;
            this.cmbSiteName.SelectedIndexChanged += new System.EventHandler(this.cmbSiteName_SelectedIndexChanged);
            // 
            // cmbCompName
            // 
            this.cmbCompName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompName.FormattingEnabled = true;
            this.cmbCompName.Location = new System.Drawing.Point(105, 75);
            this.cmbCompName.Name = "cmbCompName";
            this.cmbCompName.Size = new System.Drawing.Size(137, 21);
            this.cmbCompName.TabIndex = 14;
            // 
            // lblSerialNo
            // 
            this.lblSerialNo.AutoSize = true;
            this.lblSerialNo.Location = new System.Drawing.Point(6, 48);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(97, 13);
            this.lblSerialNo.TabIndex = 24;
            this.lblSerialNo.Text = "Machine Serial No:";
            // 
            // lblCompName
            // 
            this.lblCompName.AutoSize = true;
            this.lblCompName.Location = new System.Drawing.Point(6, 78);
            this.lblCompName.Name = "lblCompName";
            this.lblCompName.Size = new System.Drawing.Size(95, 13);
            this.lblCompName.TabIndex = 26;
            this.lblCompName.Text = "Component Name:";
            // 
            // gpDates
            // 
            this.gpDates.Controls.Add(this.cmbVerType);
            this.gpDates.Controls.Add(this.lblVerType);
            this.gpDates.Controls.Add(this.cmbCompType);
            this.gpDates.Controls.Add(this.lblCompType);
            this.gpDates.Controls.Add(this.lblTo);
            this.gpDates.Controls.Add(this.lblFrom);
            this.gpDates.Controls.Add(this.dtPickerTo);
            this.gpDates.Controls.Add(this.dtPickerFrom);
            this.gpDates.Location = new System.Drawing.Point(6, 11);
            this.gpDates.Name = "gpDates";
            this.gpDates.Size = new System.Drawing.Size(246, 135);
            this.gpDates.TabIndex = 6;
            this.gpDates.TabStop = false;
            // 
            // cmbVerType
            // 
            this.cmbVerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVerType.FormattingEnabled = true;
            this.cmbVerType.Location = new System.Drawing.Point(98, 105);
            this.cmbVerType.Name = "cmbVerType";
            this.cmbVerType.Size = new System.Drawing.Size(137, 21);
            this.cmbVerType.TabIndex = 10;
            // 
            // lblVerType
            // 
            this.lblVerType.AutoSize = true;
            this.lblVerType.Location = new System.Drawing.Point(6, 108);
            this.lblVerType.Name = "lblVerType";
            this.lblVerType.Size = new System.Drawing.Size(89, 13);
            this.lblVerType.TabIndex = 18;
            this.lblVerType.Text = "Verification Type:";
            // 
            // cmbCompType
            // 
            this.cmbCompType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompType.FormattingEnabled = true;
            this.cmbCompType.Location = new System.Drawing.Point(98, 75);
            this.cmbCompType.Name = "cmbCompType";
            this.cmbCompType.Size = new System.Drawing.Size(137, 21);
            this.cmbCompType.TabIndex = 9;
            // 
            // lblCompType
            // 
            this.lblCompType.AutoSize = true;
            this.lblCompType.Location = new System.Drawing.Point(6, 78);
            this.lblCompType.Name = "lblCompType";
            this.lblCompType.Size = new System.Drawing.Size(91, 13);
            this.lblCompType.TabIndex = 16;
            this.lblCompType.Text = "Component Type:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(6, 48);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(49, 13);
            this.lblTo.TabIndex = 7;
            this.lblTo.Text = "To Date:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(6, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(59, 13);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "From Date:";
            // 
            // dtPickerTo
            // 
            this.dtPickerTo.Location = new System.Drawing.Point(98, 45);
            this.dtPickerTo.Name = "dtPickerTo";
            this.dtPickerTo.Size = new System.Drawing.Size(137, 20);
            this.dtPickerTo.TabIndex = 8;
            // 
            // dtPickerFrom
            // 
            this.dtPickerFrom.Location = new System.Drawing.Point(98, 15);
            this.dtPickerFrom.Name = "dtPickerFrom";
            this.dtPickerFrom.Size = new System.Drawing.Size(137, 20);
            this.dtPickerFrom.TabIndex = 7;
            // 
            // dgCompDetails
            // 
            this.dgCompDetails.AllowUserToAddRows = false;
            this.dgCompDetails.AllowUserToDeleteRows = false;
            this.dgCompDetails.AllowUserToResizeColumns = false;
            this.dgCompDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCompDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCompDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCompDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SiteName,
            this.MachineSerialNo,
            this.InstallationNo,
            this.ComponentType,
            this.ComponentName,
            this.VerificationType,
            this.VerificationStatus,
            this.VerificationTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCompDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCompDetails.Location = new System.Drawing.Point(6, 210);
            this.dgCompDetails.Name = "dgCompDetails";
            this.dgCompDetails.ReadOnly = true;
            this.dgCompDetails.RowHeadersVisible = false;
            this.dgCompDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgCompDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCompDetails.Size = new System.Drawing.Size(774, 447);
            this.dgCompDetails.TabIndex = 17;
            this.dgCompDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.dgCompDetails_Paint_1);
            // 
            // SiteName
            // 
            this.SiteName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SiteName.DataPropertyName = "Site_Name";
            this.SiteName.HeaderText = "Site Name";
            this.SiteName.MinimumWidth = 135;
            this.SiteName.Name = "SiteName";
            this.SiteName.ReadOnly = true;
            this.SiteName.Width = 135;
            // 
            // MachineSerialNo
            // 
            this.MachineSerialNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MachineSerialNo.DataPropertyName = "Machine_Serial_No";
            this.MachineSerialNo.HeaderText = "Machine Serial No";
            this.MachineSerialNo.MinimumWidth = 90;
            this.MachineSerialNo.Name = "MachineSerialNo";
            this.MachineSerialNo.ReadOnly = true;
            this.MachineSerialNo.Width = 90;
            // 
            // InstallationNo
            // 
            this.InstallationNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InstallationNo.DataPropertyName = "Installation_No";
            this.InstallationNo.HeaderText = "Installation No";
            this.InstallationNo.MinimumWidth = 65;
            this.InstallationNo.Name = "InstallationNo";
            this.InstallationNo.ReadOnly = true;
            this.InstallationNo.Width = 65;
            // 
            // ComponentType
            // 
            this.ComponentType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ComponentType.DataPropertyName = "Component_Type";
            this.ComponentType.HeaderText = "Component Type";
            this.ComponentType.MinimumWidth = 95;
            this.ComponentType.Name = "ComponentType";
            this.ComponentType.ReadOnly = true;
            this.ComponentType.Width = 95;
            // 
            // ComponentName
            // 
            this.ComponentName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ComponentName.DataPropertyName = "Component_Name";
            this.ComponentName.HeaderText = "Component Name";
            this.ComponentName.MinimumWidth = 135;
            this.ComponentName.Name = "ComponentName";
            this.ComponentName.ReadOnly = true;
            this.ComponentName.Width = 135;
            // 
            // VerificationType
            // 
            this.VerificationType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VerificationType.DataPropertyName = "Verification_Type";
            this.VerificationType.HeaderText = "Verification Type";
            this.VerificationType.MinimumWidth = 85;
            this.VerificationType.Name = "VerificationType";
            this.VerificationType.ReadOnly = true;
            this.VerificationType.Width = 85;
            // 
            // VerificationStatus
            // 
            this.VerificationStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VerificationStatus.DataPropertyName = "Verification_Status";
            this.VerificationStatus.HeaderText = "Verification Status";
            this.VerificationStatus.MinimumWidth = 65;
            this.VerificationStatus.Name = "VerificationStatus";
            this.VerificationStatus.ReadOnly = true;
            this.VerificationStatus.Width = 65;
            // 
            // VerificationTime
            // 
            this.VerificationTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.VerificationTime.DataPropertyName = "Verification_Time";
            this.VerificationTime.HeaderText = "Verification Time";
            this.VerificationTime.MinimumWidth = 100;
            this.VerificationTime.Name = "VerificationTime";
            this.VerificationTime.ReadOnly = true;
            // 
            // grpBoxCompDetails
            // 
            this.grpBoxCompDetails.Controls.Add(this.btnCreate);
            this.grpBoxCompDetails.Controls.Add(this.btnEditComponent);
            this.grpBoxCompDetails.Controls.Add(this.tvComponetDetails);
            this.grpBoxCompDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxCompDetails.Location = new System.Drawing.Point(3, 1);
            this.grpBoxCompDetails.Name = "grpBoxCompDetails";
            this.grpBoxCompDetails.Size = new System.Drawing.Size(249, 663);
            this.grpBoxCompDetails.TabIndex = 0;
            this.grpBoxCompDetails.TabStop = false;
            this.grpBoxCompDetails.Text = "Component Details";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(20, 627);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(95, 29);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEditComponent
            // 
            this.btnEditComponent.Location = new System.Drawing.Point(134, 627);
            this.btnEditComponent.Name = "btnEditComponent";
            this.btnEditComponent.Size = new System.Drawing.Size(95, 29);
            this.btnEditComponent.TabIndex = 3;
            this.btnEditComponent.Text = "Edit";
            this.btnEditComponent.UseVisualStyleBackColor = true;
            this.btnEditComponent.Click += new System.EventHandler(this.btnEditComponent_Click_1);
            // 
            // tvComponetDetails
            // 
            this.tvComponetDetails.Location = new System.Drawing.Point(6, 19);
            this.tvComponetDetails.Name = "tvComponetDetails";
            this.tvComponetDetails.Size = new System.Drawing.Size(238, 602);
            this.tvComponetDetails.TabIndex = 1;
            // 
            // ViewComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 669);
            this.Controls.Add(this.pnlComponentScreen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1057, 703);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1057, 703);
            this.Name = "ViewComponents";
            this.Text = "View Components";
            this.Load += new System.EventHandler(this.ViewComponents_Load);
            this.pnlComponentScreen.ResumeLayout(false);
            this.grpBoxCompVeriDetails.ResumeLayout(false);
            this.grpBoxReqVer.ResumeLayout(false);
            this.grpBoxReqVer.PerformLayout();
            this.grpSearchCriteria.ResumeLayout(false);
            this.gpSite.ResumeLayout(false);
            this.gpSite.PerformLayout();
            this.gpDates.ResumeLayout(false);
            this.gpDates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompDetails)).EndInit();
            this.grpBoxCompDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlComponentScreen;
        private System.Windows.Forms.GroupBox grpBoxCompDetails;
        private System.Windows.Forms.TreeView tvComponetDetails;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEditComponent;
        private System.Windows.Forms.GroupBox grpBoxCompVeriDetails;
        private System.Windows.Forms.DataGridView dgCompDetails;
        private System.Windows.Forms.GroupBox grpSearchCriteria;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox gpSite;
        private System.Windows.Forms.ComboBox cmbMachineSerial;
        private System.Windows.Forms.Label lblSiteName;
        private System.Windows.Forms.ComboBox cmbSiteName;
        private System.Windows.Forms.ComboBox cmbCompName;
        private System.Windows.Forms.Label lblSerialNo;
        private System.Windows.Forms.Label lblCompName;
        private System.Windows.Forms.GroupBox gpDates;
        private System.Windows.Forms.ComboBox cmbVerType;
        private System.Windows.Forms.Label lblVerType;
        private System.Windows.Forms.ComboBox cmbCompType;
        private System.Windows.Forms.Label lblCompType;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtPickerTo;
        private System.Windows.Forms.DateTimePicker dtPickerFrom;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.GroupBox grpBoxReqVer;
        private System.Windows.Forms.ComboBox cmbSerial;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.ComboBox cmbComp;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblComp;
        private System.Windows.Forms.Button btnRVClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineSerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstallationNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComponentType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComponentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerificationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerificationStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn VerificationTime;
    }
}

