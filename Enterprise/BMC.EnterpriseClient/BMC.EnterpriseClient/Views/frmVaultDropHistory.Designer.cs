namespace BMC.EnterpriseClient.Views
{
    partial class frmVaultDropHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVaultDropHistory));
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gbAuditDetails = new System.Windows.Forms.GroupBox();
            this.dgvVaultDropHistoryDetails = new System.Windows.Forms.DataGridView();
            this.clmDropId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSiteDropRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBleeds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAdjustment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBmcTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVaultTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmActualTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBmcVaraince = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVaultVaraince = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDropDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDeclarationDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbSiteDetails = new System.Windows.Forms.GroupBox();
            this.tlpSiteDetails = new System.Windows.Forms.TableLayoutPanel();
            this.txtManufacturer = new System.Windows.Forms.Label();
            this.txtTypePrefix = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblVault = new System.Windows.Forms.Label();
            this.lblTypePrefix = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.txtVaultName = new System.Windows.Forms.Label();
            this.grpDropDateDetails = new System.Windows.Forms.GroupBox();
            this.tlpDropDateDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblVaraince = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEnddate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbVarainceType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tlpButtonsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.tlpMainPanel.SuspendLayout();
            this.gbAuditDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaultDropHistoryDetails)).BeginInit();
            this.gbSiteDetails.SuspendLayout();
            this.tlpSiteDetails.SuspendLayout();
            this.grpDropDateDetails.SuspendLayout();
            this.tlpDropDateDetails.SuspendLayout();
            this.tlpButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 1;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Controls.Add(this.gbAuditDetails, 0, 3);
            this.tlpMainPanel.Controls.Add(this.gbSiteDetails, 0, 1);
            this.tlpMainPanel.Controls.Add(this.grpDropDateDetails, 0, 2);
            this.tlpMainPanel.Controls.Add(this.lblStatus, 0, 4);
            this.tlpMainPanel.Controls.Add(this.tlpButtonsPanel, 0, 5);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 7;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainPanel.Size = new System.Drawing.Size(1205, 608);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // gbAuditDetails
            // 
            this.gbAuditDetails.Controls.Add(this.dgvVaultDropHistoryDetails);
            this.gbAuditDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAuditDetails.Location = new System.Drawing.Point(3, 123);
            this.gbAuditDetails.Name = "gbAuditDetails";
            this.gbAuditDetails.Size = new System.Drawing.Size(1199, 387);
            this.gbAuditDetails.TabIndex = 2;
            this.gbAuditDetails.TabStop = false;
            // 
            // dgvVaultDropHistoryDetails
            // 
            this.dgvVaultDropHistoryDetails.AllowUserToAddRows = false;
            this.dgvVaultDropHistoryDetails.AllowUserToDeleteRows = false;
            this.dgvVaultDropHistoryDetails.AllowUserToResizeRows = false;
            this.dgvVaultDropHistoryDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVaultDropHistoryDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVaultDropHistoryDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVaultDropHistoryDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVaultDropHistoryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVaultDropHistoryDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDropId,
            this.clmSiteDropRef,
            this.clmFills,
            this.clmBleeds,
            this.clmAdjustment,
            this.clmBmcTotal,
            this.clmVaultTotal,
            this.clmActualTotal,
            this.clmBmcVaraince,
            this.clmVaultVaraince,
            this.clmDropDateTime,
            this.clmDeclarationDateTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVaultDropHistoryDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVaultDropHistoryDetails.EnableHeadersVisualStyles = false;
            this.dgvVaultDropHistoryDetails.Location = new System.Drawing.Point(3, 16);
            this.dgvVaultDropHistoryDetails.MultiSelect = false;
            this.dgvVaultDropHistoryDetails.Name = "dgvVaultDropHistoryDetails";
            this.dgvVaultDropHistoryDetails.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVaultDropHistoryDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVaultDropHistoryDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVaultDropHistoryDetails.Size = new System.Drawing.Size(1193, 368);
            this.dgvVaultDropHistoryDetails.TabIndex = 0;
            this.dgvVaultDropHistoryDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVaultDropHistoryDetails_CellDoubleClick);
            this.dgvVaultDropHistoryDetails.SelectionChanged += new System.EventHandler(this.dgvVaultDropHistoryDetails_SelectionChanged);
            // 
            // clmDropId
            // 
            this.clmDropId.DataPropertyName = "Drop_ID";
            this.clmDropId.HeaderText = "Drop ID";
            this.clmDropId.Name = "clmDropId";
            this.clmDropId.ReadOnly = true;
            // 
            // clmSiteDropRef
            // 
            this.clmSiteDropRef.DataPropertyName = "Site_Drop_Ref";
            this.clmSiteDropRef.HeaderText = "Site Drop Ref";
            this.clmSiteDropRef.Name = "clmSiteDropRef";
            this.clmSiteDropRef.ReadOnly = true;
            // 
            // clmFills
            // 
            this.clmFills.DataPropertyName = "FillAmount";
            this.clmFills.HeaderText = "Fills";
            this.clmFills.Name = "clmFills";
            this.clmFills.ReadOnly = true;
            // 
            // clmBleeds
            // 
            this.clmBleeds.DataPropertyName = "BleedAmount";
            this.clmBleeds.HeaderText = "Bleeds";
            this.clmBleeds.Name = "clmBleeds";
            this.clmBleeds.ReadOnly = true;
            // 
            // clmAdjustment
            // 
            this.clmAdjustment.DataPropertyName = "AdjustmentAmount";
            this.clmAdjustment.HeaderText = "Adjustment";
            this.clmAdjustment.Name = "clmAdjustment";
            this.clmAdjustment.ReadOnly = true;
            // 
            // clmBmcTotal
            // 
            this.clmBmcTotal.DataPropertyName = "Meter_Balance";
            this.clmBmcTotal.HeaderText = "BMC Total";
            this.clmBmcTotal.Name = "clmBmcTotal";
            this.clmBmcTotal.ReadOnly = true;
            // 
            // clmVaultTotal
            // 
            this.clmVaultTotal.DataPropertyName = "Vault_Balance";
            this.clmVaultTotal.HeaderText = "Vault Total";
            this.clmVaultTotal.Name = "clmVaultTotal";
            this.clmVaultTotal.ReadOnly = true;
            // 
            // clmActualTotal
            // 
            this.clmActualTotal.DataPropertyName = "Declared_Balance";
            this.clmActualTotal.HeaderText = "Actual Total";
            this.clmActualTotal.Name = "clmActualTotal";
            this.clmActualTotal.ReadOnly = true;
            // 
            // clmBmcVaraince
            // 
            this.clmBmcVaraince.DataPropertyName = "BMCVariance";
            this.clmBmcVaraince.HeaderText = "BMC Variance";
            this.clmBmcVaraince.Name = "clmBmcVaraince";
            this.clmBmcVaraince.ReadOnly = true;
            // 
            // clmVaultVaraince
            // 
            this.clmVaultVaraince.DataPropertyName = "VaultVariance";
            this.clmVaultVaraince.HeaderText = "Vault Variance";
            this.clmVaultVaraince.Name = "clmVaultVaraince";
            this.clmVaultVaraince.ReadOnly = true;
            // 
            // clmDropDateTime
            // 
            this.clmDropDateTime.DataPropertyName = "CreatedDate";
            this.clmDropDateTime.HeaderText = "Drop Date\\Time";
            this.clmDropDateTime.Name = "clmDropDateTime";
            this.clmDropDateTime.ReadOnly = true;
            // 
            // clmDeclarationDateTime
            // 
            this.clmDeclarationDateTime.DataPropertyName = "ModifiedDate";
            this.clmDeclarationDateTime.HeaderText = "Declaration Date\\Time";
            this.clmDeclarationDateTime.Name = "clmDeclarationDateTime";
            this.clmDeclarationDateTime.ReadOnly = true;
            // 
            // gbSiteDetails
            // 
            this.gbSiteDetails.Controls.Add(this.tlpSiteDetails);
            this.gbSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSiteDetails.Location = new System.Drawing.Point(3, 18);
            this.gbSiteDetails.Name = "gbSiteDetails";
            this.gbSiteDetails.Size = new System.Drawing.Size(1199, 44);
            this.gbSiteDetails.TabIndex = 0;
            this.gbSiteDetails.TabStop = false;
            // 
            // tlpSiteDetails
            // 
            this.tlpSiteDetails.ColumnCount = 8;
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.243745F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.34425F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.243744F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.34425F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.06776F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.34425F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.06776F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.34425F));
            this.tlpSiteDetails.Controls.Add(this.txtManufacturer, 7, 0);
            this.tlpSiteDetails.Controls.Add(this.txtTypePrefix, 5, 0);
            this.tlpSiteDetails.Controls.Add(this.lblSite, 0, 0);
            this.tlpSiteDetails.Controls.Add(this.lblVault, 2, 0);
            this.tlpSiteDetails.Controls.Add(this.lblTypePrefix, 4, 0);
            this.tlpSiteDetails.Controls.Add(this.lblManufacturer, 6, 0);
            this.tlpSiteDetails.Controls.Add(this.cmbSite, 1, 0);
            this.tlpSiteDetails.Controls.Add(this.txtVaultName, 3, 0);
            this.tlpSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSiteDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpSiteDetails.Name = "tlpSiteDetails";
            this.tlpSiteDetails.RowCount = 1;
            this.tlpSiteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSiteDetails.Size = new System.Drawing.Size(1193, 25);
            this.tlpSiteDetails.TabIndex = 0;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtManufacturer.BackColor = System.Drawing.Color.Gainsboro;
            this.txtManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtManufacturer.Location = new System.Drawing.Point(985, 0);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(205, 25);
            this.txtManufacturer.TabIndex = 7;
            this.txtManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTypePrefix
            // 
            this.txtTypePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTypePrefix.BackColor = System.Drawing.Color.Gainsboro;
            this.txtTypePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTypePrefix.Location = new System.Drawing.Point(659, 0);
            this.txtTypePrefix.Name = "txtTypePrefix";
            this.txtTypePrefix.Size = new System.Drawing.Size(200, 25);
            this.txtTypePrefix.TabIndex = 5;
            this.txtTypePrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(28, 6);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(31, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "Site :";
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVault
            // 
            this.lblVault.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVault.AutoSize = true;
            this.lblVault.Location = new System.Drawing.Point(290, 6);
            this.lblVault.Name = "lblVault";
            this.lblVault.Size = new System.Drawing.Size(37, 13);
            this.lblVault.TabIndex = 2;
            this.lblVault.Text = "Vault :";
            this.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTypePrefix
            // 
            this.lblTypePrefix.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTypePrefix.AutoSize = true;
            this.lblTypePrefix.Location = new System.Drawing.Point(587, 6);
            this.lblTypePrefix.Name = "lblTypePrefix";
            this.lblTypePrefix.Size = new System.Drawing.Size(66, 13);
            this.lblTypePrefix.TabIndex = 4;
            this.lblTypePrefix.Text = "Type Prefix :";
            this.lblTypePrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(903, 6);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(76, 13);
            this.lblManufacturer.TabIndex = 6;
            this.lblManufacturer.Text = "Manufacturer :";
            this.lblManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSite
            // 
            this.cmbSite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(65, 3);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(200, 21);
            this.cmbSite.TabIndex = 1;
            this.cmbSite.DropDown += new System.EventHandler(this.cmbSite_DropDown);
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.cmbSite_SelectedIndexChanged);
            // 
            // txtVaultName
            // 
            this.txtVaultName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVaultName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtVaultName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVaultName.Location = new System.Drawing.Point(333, 0);
            this.txtVaultName.Name = "txtVaultName";
            this.txtVaultName.Size = new System.Drawing.Size(200, 25);
            this.txtVaultName.TabIndex = 3;
            this.txtVaultName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpDropDateDetails
            // 
            this.grpDropDateDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDropDateDetails.Controls.Add(this.tlpDropDateDetails);
            this.grpDropDateDetails.Location = new System.Drawing.Point(3, 68);
            this.grpDropDateDetails.Name = "grpDropDateDetails";
            this.grpDropDateDetails.Size = new System.Drawing.Size(1199, 49);
            this.grpDropDateDetails.TabIndex = 1;
            this.grpDropDateDetails.TabStop = false;
            // 
            // tlpDropDateDetails
            // 
            this.tlpDropDateDetails.ColumnCount = 8;
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.937888F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.3913F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.937888F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.3913F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.937888F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.52795F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.937888F));
            this.tlpDropDateDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.937888F));
            this.tlpDropDateDetails.Controls.Add(this.lblVaraince, 4, 0);
            this.tlpDropDateDetails.Controls.Add(this.dtpEndDate, 3, 0);
            this.tlpDropDateDetails.Controls.Add(this.lblStartDate, 0, 0);
            this.tlpDropDateDetails.Controls.Add(this.lblEnddate, 2, 0);
            this.tlpDropDateDetails.Controls.Add(this.dtpStartDate, 1, 0);
            this.tlpDropDateDetails.Controls.Add(this.btnGo, 7, 0);
            this.tlpDropDateDetails.Controls.Add(this.cmbVarainceType, 5, 0);
            this.tlpDropDateDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDropDateDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpDropDateDetails.Name = "tlpDropDateDetails";
            this.tlpDropDateDetails.RowCount = 1;
            this.tlpDropDateDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDropDateDetails.Size = new System.Drawing.Size(1193, 30);
            this.tlpDropDateDetails.TabIndex = 0;
            // 
            // lblVaraince
            // 
            this.lblVaraince.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVaraince.AutoSize = true;
            this.lblVaraince.Location = new System.Drawing.Point(710, 8);
            this.lblVaraince.Name = "lblVaraince";
            this.lblVaraince.Size = new System.Drawing.Size(55, 13);
            this.lblVaraince.TabIndex = 4;
            this.lblVaraince.Text = "Variance :";
            this.lblVaraince.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(446, 3);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(201, 20);
            this.dtpEndDate.TabIndex = 3;
            this.dtpEndDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyUp);
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(26, 8);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(89, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date/Time :";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEnddate
            // 
            this.lblEnddate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEnddate.AutoSize = true;
            this.lblEnddate.Location = new System.Drawing.Point(354, 8);
            this.lblEnddate.Name = "lblEnddate";
            this.lblEnddate.Size = new System.Drawing.Size(86, 13);
            this.lblEnddate.TabIndex = 2;
            this.lblEnddate.Text = "End Date/Time :";
            this.lblEnddate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(121, 3);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(201, 20);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyUp);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(1074, 3);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(116, 24);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "&Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbVarainceType
            // 
            this.cmbVarainceType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVarainceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVarainceType.FormattingEnabled = true;
            this.cmbVarainceType.Location = new System.Drawing.Point(771, 3);
            this.cmbVarainceType.Name = "cmbVarainceType";
            this.cmbVarainceType.Size = new System.Drawing.Size(179, 21);
            this.cmbVarainceType.TabIndex = 5;
            this.cmbVarainceType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyUp);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(3, 513);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(1199, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpButtonsPanel
            // 
            this.tlpButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpButtonsPanel.ColumnCount = 8;
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99999F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtonsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpButtonsPanel.Controls.Add(this.btnClose, 7, 0);
            this.tlpButtonsPanel.Controls.Add(this.btnPrint, 5, 0);
            this.tlpButtonsPanel.Controls.Add(this.btnAdjust, 4, 0);
            this.tlpButtonsPanel.Controls.Add(this.btnDetails, 6, 0);
            this.tlpButtonsPanel.Location = new System.Drawing.Point(3, 541);
            this.tlpButtonsPanel.Name = "tlpButtonsPanel";
            this.tlpButtonsPanel.RowCount = 1;
            this.tlpButtonsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtonsPanel.Size = new System.Drawing.Size(1199, 44);
            this.tlpButtonsPanel.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1099, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 38);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(899, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(94, 38);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAdjust
            // 
            this.btnAdjust.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdjust.Location = new System.Drawing.Point(799, 3);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(94, 38);
            this.btnAdjust.TabIndex = 0;
            this.btnAdjust.Text = "&Adjust";
            this.btnAdjust.UseVisualStyleBackColor = true;
            this.btnAdjust.Visible = false;
            this.btnAdjust.Click += new System.EventHandler(this.btnAdjust_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.Location = new System.Drawing.Point(999, 3);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(94, 38);
            this.btnDetails.TabIndex = 2;
            this.btnDetails.Text = "Print De&tails";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Visible = false;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // frmVaultDropHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 608);
            this.Controls.Add(this.tlpMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVaultDropHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vault Drop History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVaultDropHistory_FormClosing);
            this.Load += new System.EventHandler(this.frmVaultDropHistory_Load);
            this.tlpMainPanel.ResumeLayout(false);
            this.tlpMainPanel.PerformLayout();
            this.gbAuditDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVaultDropHistoryDetails)).EndInit();
            this.gbSiteDetails.ResumeLayout(false);
            this.tlpSiteDetails.ResumeLayout(false);
            this.tlpSiteDetails.PerformLayout();
            this.grpDropDateDetails.ResumeLayout(false);
            this.tlpDropDateDetails.ResumeLayout(false);
            this.tlpDropDateDetails.PerformLayout();
            this.tlpButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.GroupBox gbAuditDetails;
        private System.Windows.Forms.DataGridView dgvVaultDropHistoryDetails;
        private System.Windows.Forms.GroupBox gbSiteDetails;
        private System.Windows.Forms.TableLayoutPanel tlpSiteDetails;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblVault;
        private System.Windows.Forms.Label lblTypePrefix;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label txtManufacturer;
        private System.Windows.Forms.Label txtTypePrefix;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.Label txtVaultName;
        private System.Windows.Forms.GroupBox grpDropDateDetails;
        private System.Windows.Forms.TableLayoutPanel tlpDropDateDetails;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEnddate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TableLayoutPanel tlpButtonsPanel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnAdjust;
        private System.Windows.Forms.Label lblVaraince;
        private System.Windows.Forms.ComboBox cmbVarainceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDropId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSiteDropRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFills;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBleeds;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAdjustment;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBmcTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVaultTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmActualTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBmcVaraince;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVaultVaraince;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDropDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeclarationDateTime;
        private System.Windows.Forms.Button btnDetails;
    }
}