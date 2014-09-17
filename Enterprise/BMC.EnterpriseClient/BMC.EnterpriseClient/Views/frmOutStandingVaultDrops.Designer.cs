namespace BMC.EnterpriseClient.Views
{
    partial class frmOutstandingVaultDrops
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutstandingVaultDrops));
            this.tlpOuterMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gvOutstandingVaultDrops = new System.Windows.Forms.DataGridView();
            this.clmDropID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSiteDropRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBleed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAdjustments = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBmcTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVaultTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDropDataTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNotesCounter = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.gbSiteDetails = new System.Windows.Forms.GroupBox();
            this.tlpSiteDetails = new System.Windows.Forms.TableLayoutPanel();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblVault = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtVaultName = new System.Windows.Forms.Label();
            this.lblTypePrefix = new System.Windows.Forms.Label();
            this.txtTypePrefix = new System.Windows.Forms.Label();
            this.txtDeclarationLabel = new System.Windows.Forms.TextBox();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlpOuterMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOutstandingVaultDrops)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbSiteDetails.SuspendLayout();
            this.tlpSiteDetails.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpOuterMainPanel
            // 
            this.tlpOuterMainPanel.ColumnCount = 1;
            this.tlpOuterMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterMainPanel.Controls.Add(this.gvOutstandingVaultDrops, 0, 3);
            this.tlpOuterMainPanel.Controls.Add(this.tableLayoutPanel1, 0, 4);
            this.tlpOuterMainPanel.Controls.Add(this.gbSiteDetails, 0, 1);
            this.tlpOuterMainPanel.Controls.Add(this.txtDeclarationLabel, 0, 2);
            this.tlpOuterMainPanel.Controls.Add(this.ssStatus, 0, 5);
            this.tlpOuterMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOuterMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpOuterMainPanel.Name = "tlpOuterMainPanel";
            this.tlpOuterMainPanel.RowCount = 6;
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpOuterMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpOuterMainPanel.Size = new System.Drawing.Size(1050, 643);
            this.tlpOuterMainPanel.TabIndex = 0;
            // 
            // gvOutstandingVaultDrops
            // 
            this.gvOutstandingVaultDrops.AllowUserToAddRows = false;
            this.gvOutstandingVaultDrops.AllowUserToDeleteRows = false;
            this.gvOutstandingVaultDrops.AllowUserToOrderColumns = true;
            this.gvOutstandingVaultDrops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvOutstandingVaultDrops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvOutstandingVaultDrops.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gvOutstandingVaultDrops.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOutstandingVaultDrops.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvOutstandingVaultDrops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvOutstandingVaultDrops.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmDropID,
            this.clmSiteDropRef,
            this.clmFills,
            this.clmBleed,
            this.clmAdjustments,
            this.clmBmcTotal,
            this.clmVaultTotal,
            this.clmDropDataTime});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvOutstandingVaultDrops.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvOutstandingVaultDrops.EnableHeadersVisualStyles = false;
            this.gvOutstandingVaultDrops.Location = new System.Drawing.Point(3, 108);
            this.gvOutstandingVaultDrops.MultiSelect = false;
            this.gvOutstandingVaultDrops.Name = "gvOutstandingVaultDrops";
            this.gvOutstandingVaultDrops.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvOutstandingVaultDrops.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvOutstandingVaultDrops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvOutstandingVaultDrops.Size = new System.Drawing.Size(1044, 472);
            this.gvOutstandingVaultDrops.StandardTab = true;
            this.gvOutstandingVaultDrops.TabIndex = 3;
            this.gvOutstandingVaultDrops.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvOutstandingVaultDrops_CellDoubleClick);
            this.gvOutstandingVaultDrops.SelectionChanged += new System.EventHandler(this.gvOutstandingVaultDrops_SelectionChanged);
            // 
            // clmDropID
            // 
            this.clmDropID.DataPropertyName = "Drop_ID";
            this.clmDropID.HeaderText = "Drop ID";
            this.clmDropID.Name = "clmDropID";
            this.clmDropID.ReadOnly = true;
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
            // clmBleed
            // 
            this.clmBleed.DataPropertyName = "BleedAmount";
            this.clmBleed.HeaderText = "Bleeds";
            this.clmBleed.Name = "clmBleed";
            this.clmBleed.ReadOnly = true;
            // 
            // clmAdjustments
            // 
            this.clmAdjustments.DataPropertyName = "AdjustmentAmount";
            this.clmAdjustments.HeaderText = "Adjustments";
            this.clmAdjustments.Name = "clmAdjustments";
            this.clmAdjustments.ReadOnly = true;
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
            // clmDropDataTime
            // 
            this.clmDropDataTime.DataPropertyName = "CreatedDate";
            this.clmDropDataTime.HeaderText = "Drop Date\\Time";
            this.clmDropDataTime.Name = "clmDropDataTime";
            this.clmDropDataTime.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNotesCounter, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDetails, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 586);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1044, 29);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Location = new System.Drawing.Point(910, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(123, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNotesCounter
            // 
            this.btnNotesCounter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnNotesCounter.Location = new System.Drawing.Point(765, 3);
            this.btnNotesCounter.Name = "btnNotesCounter";
            this.btnNotesCounter.Size = new System.Drawing.Size(123, 23);
            this.btnNotesCounter.TabIndex = 2;
            this.btnNotesCounter.Text = "&Declare";
            this.btnNotesCounter.UseVisualStyleBackColor = true;
            this.btnNotesCounter.Click += new System.EventHandler(this.btnNotesCounter_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPrint.Location = new System.Drawing.Point(475, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(123, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDetails.Location = new System.Drawing.Point(620, 3);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(123, 23);
            this.btnDetails.TabIndex = 4;
            this.btnDetails.Text = "Print De&tails";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // gbSiteDetails
            // 
            this.gbSiteDetails.Controls.Add(this.tlpSiteDetails);
            this.gbSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSiteDetails.Location = new System.Drawing.Point(3, 23);
            this.gbSiteDetails.Name = "gbSiteDetails";
            this.gbSiteDetails.Size = new System.Drawing.Size(1044, 49);
            this.gbSiteDetails.TabIndex = 1;
            this.gbSiteDetails.TabStop = false;
            // 
            // tlpSiteDetails
            // 
            this.tlpSiteDetails.ColumnCount = 9;
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.77624F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.77846F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.77624F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66908F));
            this.tlpSiteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tlpSiteDetails.Controls.Add(this.cmbSite, 3, 0);
            this.tlpSiteDetails.Controls.Add(this.cmbRegion, 1, 0);
            this.tlpSiteDetails.Controls.Add(this.lblSite, 2, 0);
            this.tlpSiteDetails.Controls.Add(this.btnRefresh, 8, 0);
            this.tlpSiteDetails.Controls.Add(this.lblVault, 4, 0);
            this.tlpSiteDetails.Controls.Add(this.lblRegion, 0, 0);
            this.tlpSiteDetails.Controls.Add(this.txtVaultName, 5, 0);
            this.tlpSiteDetails.Controls.Add(this.lblTypePrefix, 6, 0);
            this.tlpSiteDetails.Controls.Add(this.txtTypePrefix, 7, 0);
            this.tlpSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSiteDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpSiteDetails.Name = "tlpSiteDetails";
            this.tlpSiteDetails.RowCount = 1;
            this.tlpSiteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSiteDetails.Size = new System.Drawing.Size(1038, 30);
            this.tlpSiteDetails.TabIndex = 0;
            // 
            // cmbSite
            // 
            this.cmbSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(281, 4);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(182, 21);
            this.cmbSite.TabIndex = 3;
            this.cmbSite.DropDown += new System.EventHandler(this.cmbSite_DropDown);
            this.cmbSite.SelectedIndexChanged += new System.EventHandler(this.cbSite_SelectedIndexChanged);
            // 
            // cmbRegion
            // 
            this.cmbRegion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(56, 4);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(182, 21);
            this.cmbRegion.TabIndex = 1;
            this.cmbRegion.DropDown += new System.EventHandler(this.cmbRegion_DropDown);
            this.cmbRegion.SelectedIndexChanged += new System.EventHandler(this.cmbRegion_SelectedIndexChanged);
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(244, 8);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(31, 13);
            this.lblSite.TabIndex = 2;
            this.lblSite.Text = "Site :";
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.Location = new System.Drawing.Point(885, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 24);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblVault
            // 
            this.lblVault.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVault.AutoSize = true;
            this.lblVault.Location = new System.Drawing.Point(469, 8);
            this.lblVault.Name = "lblVault";
            this.lblVault.Size = new System.Drawing.Size(37, 13);
            this.lblVault.TabIndex = 4;
            this.lblVault.Text = "Vault :";
            this.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(3, 8);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(47, 13);
            this.lblRegion.TabIndex = 0;
            this.lblRegion.Text = "Region :";
            this.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVaultName
            // 
            this.txtVaultName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVaultName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtVaultName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVaultName.ForeColor = System.Drawing.Color.Black;
            this.txtVaultName.Location = new System.Drawing.Point(512, 5);
            this.txtVaultName.Name = "txtVaultName";
            this.txtVaultName.Size = new System.Drawing.Size(182, 20);
            this.txtVaultName.TabIndex = 5;
            this.txtVaultName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTypePrefix
            // 
            this.lblTypePrefix.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTypePrefix.AutoSize = true;
            this.lblTypePrefix.Location = new System.Drawing.Point(700, 8);
            this.lblTypePrefix.Name = "lblTypePrefix";
            this.lblTypePrefix.Size = new System.Drawing.Size(66, 13);
            this.lblTypePrefix.TabIndex = 6;
            this.lblTypePrefix.Text = "Type Prefix :";
            this.lblTypePrefix.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTypePrefix
            // 
            this.txtTypePrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTypePrefix.BackColor = System.Drawing.Color.Gainsboro;
            this.txtTypePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTypePrefix.ForeColor = System.Drawing.Color.Black;
            this.txtTypePrefix.Location = new System.Drawing.Point(772, 5);
            this.txtTypePrefix.Name = "txtTypePrefix";
            this.txtTypePrefix.Size = new System.Drawing.Size(107, 20);
            this.txtTypePrefix.TabIndex = 7;
            this.txtTypePrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeclarationLabel
            // 
            this.txtDeclarationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeclarationLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.txtDeclarationLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtDeclarationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeclarationLabel.ForeColor = System.Drawing.Color.White;
            this.txtDeclarationLabel.Location = new System.Drawing.Point(3, 78);
            this.txtDeclarationLabel.Name = "txtDeclarationLabel";
            this.txtDeclarationLabel.ReadOnly = true;
            this.txtDeclarationLabel.ShortcutsEnabled = false;
            this.txtDeclarationLabel.Size = new System.Drawing.Size(1044, 23);
            this.txtDeclarationLabel.TabIndex = 2;
            this.txtDeclarationLabel.Text = "Undeclared Vault Drops";
            // 
            // ssStatus
            // 
            this.ssStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ssStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatus});
            this.ssStatus.Location = new System.Drawing.Point(0, 618);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(1050, 25);
            this.ssStatus.TabIndex = 0;
            // 
            // tsslStatus
            // 
            this.tsslStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tsslStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(130, 20);
            this.tsslStatus.Text = "toolStripStatusLabel1";
            // 
            // frmOutstandingVaultDrops
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 643);
            this.Controls.Add(this.tlpOuterMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutstandingVaultDrops";
            this.Text = "Outstanding Vault Drops";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOutstandingVaultDrops_FormClosing);
            this.Load += new System.EventHandler(this.frmOutstandingVaultDrops_Load);
            this.tlpOuterMainPanel.ResumeLayout(false);
            this.tlpOuterMainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvOutstandingVaultDrops)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbSiteDetails.ResumeLayout(false);
            this.tlpSiteDetails.ResumeLayout(false);
            this.tlpSiteDetails.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOuterMainPanel;
        private System.Windows.Forms.DataGridView gvOutstandingVaultDrops;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNotesCounter;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.GroupBox gbSiteDetails;
        private System.Windows.Forms.TableLayoutPanel tlpSiteDetails;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label txtTypePrefix;
        private System.Windows.Forms.Label txtVaultName;
        private System.Windows.Forms.TextBox txtDeclarationLabel;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblVault;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblTypePrefix;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDropID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSiteDropRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFills;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBleed;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAdjustments;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBmcTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVaultTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDropDataTime;
        private System.Windows.Forms.Button btnDetails;
    }
}