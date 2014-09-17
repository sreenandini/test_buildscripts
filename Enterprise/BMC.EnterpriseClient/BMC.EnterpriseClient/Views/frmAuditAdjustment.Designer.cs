namespace BMC.EnterpriseClient.Views
{
    partial class frmAuditAdjustment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuditAdjustment));
            this.gbSiteDetails = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblVault = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.Label();
            this.txtVault = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.Label();
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbAuditDetails = new System.Windows.Forms.GroupBox();
            this.tplVaultAuditDetails = new System.Windows.Forms.TableLayoutPanel();
            this.txtDeclaredTotal = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtVaultVaraince = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtVaultValue = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtBmcVaraince = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lblBMCTotal = new System.Windows.Forms.Label();
            this.lblVaultTotal = new System.Windows.Forms.Label();
            this.lblBMCVaraince = new System.Windows.Forms.Label();
            this.lblVaultVaraince = new System.Windows.Forms.Label();
            this.lblDeclaredTotal = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtBmcTotal = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.chkFreeze = new System.Windows.Forms.CheckBox();
            this.gb_Cassettedetails = new System.Windows.Forms.GroupBox();
            this.tlp_Cassettes = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gbSiteDetails.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlpMainPanel.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.gbAuditDetails.SuspendLayout();
            this.tplVaultAuditDetails.SuspendLayout();
            this.gb_Cassettedetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSiteDetails
            // 
            this.gbSiteDetails.Controls.Add(this.tableLayoutPanel2);
            this.gbSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSiteDetails.Location = new System.Drawing.Point(3, 3);
            this.gbSiteDetails.Name = "gbSiteDetails";
            this.gbSiteDetails.Size = new System.Drawing.Size(830, 57);
            this.gbSiteDetails.TabIndex = 0;
            this.gbSiteDetails.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.115783F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.21755F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.115783F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.21755F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.115783F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.21755F));
            this.tableLayoutPanel2.Controls.Add(this.lblSite, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblType, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblVault, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtSite, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtVault, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtType, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(824, 38);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSite.Location = new System.Drawing.Point(3, 0);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(44, 38);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "Site :";
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblType.Location = new System.Drawing.Point(551, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(44, 38);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type :";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVault
            // 
            this.lblVault.AutoSize = true;
            this.lblVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVault.Location = new System.Drawing.Point(277, 0);
            this.lblVault.Name = "lblVault";
            this.lblVault.Size = new System.Drawing.Size(44, 38);
            this.lblVault.TabIndex = 2;
            this.lblVault.Text = "Vault :";
            this.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSite
            // 
            this.txtSite.BackColor = System.Drawing.Color.Silver;
            this.txtSite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSite.Location = new System.Drawing.Point(53, 0);
            this.txtSite.Name = "txtSite";
            this.txtSite.Size = new System.Drawing.Size(218, 38);
            this.txtSite.TabIndex = 1;
            this.txtSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVault
            // 
            this.txtVault.BackColor = System.Drawing.Color.Silver;
            this.txtVault.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVault.Location = new System.Drawing.Point(327, 0);
            this.txtVault.Name = "txtVault";
            this.txtVault.Size = new System.Drawing.Size(218, 38);
            this.txtVault.TabIndex = 3;
            this.txtVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtType
            // 
            this.txtType.BackColor = System.Drawing.Color.Silver;
            this.txtType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtType.Location = new System.Drawing.Point(601, 0);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(220, 38);
            this.txtType.TabIndex = 5;
            this.txtType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 1;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Controls.Add(this.tlpButtons, 0, 3);
            this.tlpMainPanel.Controls.Add(this.gbSiteDetails, 0, 0);
            this.tlpMainPanel.Controls.Add(this.gbAuditDetails, 0, 1);
            this.tlpMainPanel.Controls.Add(this.lblStatus, 0, 2);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 4;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.31059F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.07001F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.385996F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tlpMainPanel.Size = new System.Drawing.Size(836, 557);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // tlpButtons
            // 
            this.tlpButtons.AutoSize = true;
            this.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpButtons.ColumnCount = 5;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpButtons.Controls.Add(this.btnSave, 3, 0);
            this.tlpButtons.Controls.Add(this.btnClose, 4, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(3, 503);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(830, 51);
            this.tlpButtons.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(555, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(693, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(134, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbAuditDetails
            // 
            this.gbAuditDetails.AutoSize = true;
            this.gbAuditDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbAuditDetails.Controls.Add(this.tplVaultAuditDetails);
            this.gbAuditDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbAuditDetails.Location = new System.Drawing.Point(3, 66);
            this.gbAuditDetails.Name = "gbAuditDetails";
            this.gbAuditDetails.Size = new System.Drawing.Size(830, 401);
            this.gbAuditDetails.TabIndex = 1;
            this.gbAuditDetails.TabStop = false;
            // 
            // tplVaultAuditDetails
            // 
            this.tplVaultAuditDetails.ColumnCount = 4;
            this.tplVaultAuditDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tplVaultAuditDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tplVaultAuditDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tplVaultAuditDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tplVaultAuditDetails.Controls.Add(this.txtDeclaredTotal, 1, 2);
            this.tplVaultAuditDetails.Controls.Add(this.txtVaultVaraince, 3, 1);
            this.tplVaultAuditDetails.Controls.Add(this.txtVaultValue, 1, 1);
            this.tplVaultAuditDetails.Controls.Add(this.txtBmcVaraince, 3, 0);
            this.tplVaultAuditDetails.Controls.Add(this.lblBMCTotal, 0, 0);
            this.tplVaultAuditDetails.Controls.Add(this.lblVaultTotal, 0, 1);
            this.tplVaultAuditDetails.Controls.Add(this.lblBMCVaraince, 2, 0);
            this.tplVaultAuditDetails.Controls.Add(this.lblVaultVaraince, 2, 1);
            this.tplVaultAuditDetails.Controls.Add(this.lblDeclaredTotal, 0, 2);
            this.tplVaultAuditDetails.Controls.Add(this.lblNotes, 0, 4);
            this.tplVaultAuditDetails.Controls.Add(this.txtBmcTotal, 1, 0);
            this.tplVaultAuditDetails.Controls.Add(this.txtNotes, 1, 4);
            this.tplVaultAuditDetails.Controls.Add(this.chkFreeze, 3, 2);
            this.tplVaultAuditDetails.Controls.Add(this.gb_Cassettedetails, 0, 3);
            this.tplVaultAuditDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplVaultAuditDetails.Location = new System.Drawing.Point(3, 16);
            this.tplVaultAuditDetails.Name = "tplVaultAuditDetails";
            this.tplVaultAuditDetails.RowCount = 5;
            this.tplVaultAuditDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.35079F));
            this.tplVaultAuditDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.947644F));
            this.tplVaultAuditDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.685863F));
            this.tplVaultAuditDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.04712F));
            this.tplVaultAuditDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.48822F));
            this.tplVaultAuditDetails.Size = new System.Drawing.Size(824, 382);
            this.tplVaultAuditDetails.TabIndex = 0;
            // 
            // txtDeclaredTotal
            // 
            this.txtDeclaredTotal.AllowDecimal = true;
            this.txtDeclaredTotal.AllowNegative = false;
            this.txtDeclaredTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDeclaredTotal.DecimalLength = 2;
            this.txtDeclaredTotal.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDeclaredTotal.Enabled = false;
            this.txtDeclaredTotal.Location = new System.Drawing.Point(126, 97);
            this.txtDeclaredTotal.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtDeclaredTotal.MaxLength = 11;
            this.txtDeclaredTotal.Name = "txtDeclaredTotal";
            this.txtDeclaredTotal.ShortcutsEnabled = false;
            this.txtDeclaredTotal.Size = new System.Drawing.Size(282, 20);
            this.txtDeclaredTotal.TabIndex = 9;
            this.txtDeclaredTotal.TextChanged += new System.EventHandler(this.txtActualTotal_TextChanged);
            this.txtDeclaredTotal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtActualTotal_KeyUp);
            // 
            // txtVaultVaraince
            // 
            this.txtVaultVaraince.AllowDecimal = false;
            this.txtVaultVaraince.AllowNegative = false;
            this.txtVaultVaraince.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVaultVaraince.BackColor = System.Drawing.Color.White;
            this.txtVaultVaraince.DecimalLength = 2;
            this.txtVaultVaraince.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVaultVaraince.Enabled = false;
            this.txtVaultVaraince.Location = new System.Drawing.Point(537, 60);
            this.txtVaultVaraince.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtVaultVaraince.MaxLength = 10;
            this.txtVaultVaraince.Name = "txtVaultVaraince";
            this.txtVaultVaraince.ReadOnly = true;
            this.txtVaultVaraince.ShortcutsEnabled = false;
            this.txtVaultVaraince.Size = new System.Drawing.Size(284, 20);
            this.txtVaultVaraince.TabIndex = 7;
            // 
            // txtVaultValue
            // 
            this.txtVaultValue.AllowDecimal = true;
            this.txtVaultValue.AllowNegative = false;
            this.txtVaultValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVaultValue.DecimalLength = 2;
            this.txtVaultValue.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtVaultValue.Enabled = false;
            this.txtVaultValue.Location = new System.Drawing.Point(126, 60);
            this.txtVaultValue.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtVaultValue.MaxLength = 11;
            this.txtVaultValue.Name = "txtVaultValue";
            this.txtVaultValue.ShortcutsEnabled = false;
            this.txtVaultValue.Size = new System.Drawing.Size(282, 20);
            this.txtVaultValue.TabIndex = 5;
            this.txtVaultValue.TextChanged += new System.EventHandler(this.txtVaultValue_TextChanged);
            // 
            // txtBmcVaraince
            // 
            this.txtBmcVaraince.AllowDecimal = false;
            this.txtBmcVaraince.AllowNegative = false;
            this.txtBmcVaraince.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBmcVaraince.BackColor = System.Drawing.Color.White;
            this.txtBmcVaraince.DecimalLength = 2;
            this.txtBmcVaraince.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBmcVaraince.Enabled = false;
            this.txtBmcVaraince.Location = new System.Drawing.Point(537, 15);
            this.txtBmcVaraince.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtBmcVaraince.MaxLength = 10;
            this.txtBmcVaraince.Name = "txtBmcVaraince";
            this.txtBmcVaraince.ReadOnly = true;
            this.txtBmcVaraince.ShortcutsEnabled = false;
            this.txtBmcVaraince.Size = new System.Drawing.Size(284, 20);
            this.txtBmcVaraince.TabIndex = 3;
            // 
            // lblBMCTotal
            // 
            this.lblBMCTotal.AutoSize = true;
            this.lblBMCTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBMCTotal.Location = new System.Drawing.Point(3, 0);
            this.lblBMCTotal.Name = "lblBMCTotal";
            this.lblBMCTotal.Size = new System.Drawing.Size(117, 51);
            this.lblBMCTotal.TabIndex = 0;
            this.lblBMCTotal.Text = "BMC Total ";
            this.lblBMCTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVaultTotal
            // 
            this.lblVaultTotal.AutoSize = true;
            this.lblVaultTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVaultTotal.Location = new System.Drawing.Point(3, 51);
            this.lblVaultTotal.Name = "lblVaultTotal";
            this.lblVaultTotal.Size = new System.Drawing.Size(117, 38);
            this.lblVaultTotal.TabIndex = 4;
            this.lblVaultTotal.Text = "Vault Total ";
            this.lblVaultTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBMCVaraince
            // 
            this.lblBMCVaraince.AutoSize = true;
            this.lblBMCVaraince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBMCVaraince.Location = new System.Drawing.Point(414, 0);
            this.lblBMCVaraince.Name = "lblBMCVaraince";
            this.lblBMCVaraince.Size = new System.Drawing.Size(117, 51);
            this.lblBMCVaraince.TabIndex = 2;
            this.lblBMCVaraince.Text = "BMC Variance ";
            this.lblBMCVaraince.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVaultVaraince
            // 
            this.lblVaultVaraince.AutoSize = true;
            this.lblVaultVaraince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVaultVaraince.Location = new System.Drawing.Point(414, 51);
            this.lblVaultVaraince.Name = "lblVaultVaraince";
            this.lblVaultVaraince.Size = new System.Drawing.Size(117, 38);
            this.lblVaultVaraince.TabIndex = 6;
            this.lblVaultVaraince.Text = "Vault Variance ";
            this.lblVaultVaraince.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDeclaredTotal
            // 
            this.lblDeclaredTotal.AutoSize = true;
            this.lblDeclaredTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeclaredTotal.Location = new System.Drawing.Point(3, 89);
            this.lblDeclaredTotal.Name = "lblDeclaredTotal";
            this.lblDeclaredTotal.Size = new System.Drawing.Size(117, 37);
            this.lblDeclaredTotal.TabIndex = 8;
            this.lblDeclaredTotal.Text = "Declared Total ";
            this.lblDeclaredTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotes.Location = new System.Drawing.Point(3, 321);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(117, 61);
            this.lblNotes.TabIndex = 12;
            this.lblNotes.Text = "* Notes ";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBmcTotal
            // 
            this.txtBmcTotal.AllowDecimal = true;
            this.txtBmcTotal.AllowNegative = false;
            this.txtBmcTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtBmcTotal.DecimalLength = 2;
            this.txtBmcTotal.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtBmcTotal.Enabled = false;
            this.txtBmcTotal.Location = new System.Drawing.Point(126, 15);
            this.txtBmcTotal.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtBmcTotal.MaxLength = 11;
            this.txtBmcTotal.Name = "txtBmcTotal";
            this.txtBmcTotal.ShortcutsEnabled = false;
            this.txtBmcTotal.Size = new System.Drawing.Size(282, 20);
            this.txtBmcTotal.TabIndex = 1;
            this.txtBmcTotal.TextChanged += new System.EventHandler(this.txtBmcTotal_TextChanged);
            // 
            // txtNotes
            // 
            this.tplVaultAuditDetails.SetColumnSpan(this.txtNotes, 3);
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(126, 324);
            this.txtNotes.MaxLength = 500;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(695, 55);
            this.txtNotes.TabIndex = 13;
            // 
            // chkFreeze
            // 
            this.chkFreeze.AutoSize = true;
            this.chkFreeze.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFreeze.Location = new System.Drawing.Point(537, 92);
            this.chkFreeze.Name = "chkFreeze";
            this.chkFreeze.Size = new System.Drawing.Size(284, 31);
            this.chkFreeze.TabIndex = 10;
            this.chkFreeze.Text = "Freeze";
            this.chkFreeze.UseVisualStyleBackColor = true;
            // 
            // gb_Cassettedetails
            // 
            this.gb_Cassettedetails.AutoSize = true;
            this.gb_Cassettedetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tplVaultAuditDetails.SetColumnSpan(this.gb_Cassettedetails, 4);
            this.gb_Cassettedetails.Controls.Add(this.tlp_Cassettes);
            this.gb_Cassettedetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Cassettedetails.Location = new System.Drawing.Point(3, 129);
            this.gb_Cassettedetails.Name = "gb_Cassettedetails";
            this.gb_Cassettedetails.Size = new System.Drawing.Size(818, 189);
            this.gb_Cassettedetails.TabIndex = 11;
            this.gb_Cassettedetails.TabStop = false;
            this.gb_Cassettedetails.Text = "Cassette/Hopper Details : ";
            // 
            // tlp_Cassettes
            // 
            this.tlp_Cassettes.AutoScroll = true;
            this.tlp_Cassettes.ColumnCount = 4;
            this.tlp_Cassettes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlp_Cassettes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Cassettes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Cassettes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_Cassettes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Cassettes.Location = new System.Drawing.Point(3, 16);
            this.tlp_Cassettes.Name = "tlp_Cassettes";
            this.tlp_Cassettes.Size = new System.Drawing.Size(812, 170);
            this.tlp_Cassettes.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(3, 470);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAuditAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 557);
            this.Controls.Add(this.tlpMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAuditAdjustment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Audit Adjustment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuditAdjustment_FormClosing);
            this.Load += new System.EventHandler(this.frmAuditAdjustment_Load);
            this.gbSiteDetails.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlpMainPanel.ResumeLayout(false);
            this.tlpMainPanel.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.gbAuditDetails.ResumeLayout(false);
            this.tplVaultAuditDetails.ResumeLayout(false);
            this.tplVaultAuditDetails.PerformLayout();
            this.gb_Cassettedetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSiteDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label txtType;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblVault;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label txtSite;
        private System.Windows.Forms.Label txtVault;
        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.GroupBox gbAuditDetails;
        private System.Windows.Forms.TableLayoutPanel tplVaultAuditDetails;
        private System.Windows.Forms.Label lblBMCTotal;
        private System.Windows.Forms.Label lblVaultTotal;
        private System.Windows.Forms.Label lblBMCVaraince;
        private System.Windows.Forms.Label lblVaultVaraince;
        private System.Windows.Forms.Label lblDeclaredTotal;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkFreeze;
        private NumberTextBox txtDeclaredTotal;
        private NumberTextBox txtVaultVaraince;
        private NumberTextBox txtVaultValue;
        private NumberTextBox txtBmcVaraince;
        private NumberTextBox txtBmcTotal;
        private System.Windows.Forms.GroupBox gb_Cassettedetails;
        private System.Windows.Forms.DataGridView dgvDropDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCassetteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDenom;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDeclaredAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVaultAmount;
        private System.Windows.Forms.TableLayoutPanel tlp_Cassettes;
        
    }
}