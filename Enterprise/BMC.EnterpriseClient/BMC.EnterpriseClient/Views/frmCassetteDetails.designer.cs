namespace BMC.EnterpriseClient.Views
{
    partial class frmCassetteDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCassetteDetails));
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOuterPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgvCasetteDetails = new System.Windows.Forms.DataGridView();
            this.gbCasetteDetails = new System.Windows.Forms.GroupBox();
            this.tlpCasetteDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblCassetteName = new System.Windows.Forms.Label();
            this.txtCassetteName = new System.Windows.Forms.TextBox();
            this.lblAlertLevel = new System.Windows.Forms.Label();
            this.txtAlertLevel = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lblDenom = new System.Windows.Forms.Label();
            this.cmbDemon = new System.Windows.Forms.ComboBox();
            this.lblStandardFillAmount = new System.Windows.Forms.Label();
            this.txtStandardFillAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lblMaxFillAmount = new System.Windows.Forms.Label();
            this.txtMaxFillAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lbl_IsActive = new System.Windows.Forms.Label();
            this.cbIsActive = new System.Windows.Forms.CheckBox();
            this.tlp_Header = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.clmCasetteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIsActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDenom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAlertLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmStandardFillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMaxFillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMainPanel.SuspendLayout();
            this.tlpOuterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCasetteDetails)).BeginInit();
            this.gbCasetteDetails.SuspendLayout();
            this.tlpCasetteDetails.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.tlp_Header.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 1;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainPanel.Controls.Add(this.tlpOuterPanel, 0, 0);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 1;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainPanel.Size = new System.Drawing.Size(759, 586);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // tlpOuterPanel
            // 
            this.tlpOuterPanel.ColumnCount = 1;
            this.tlpOuterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterPanel.Controls.Add(this.dgvCasetteDetails, 0, 1);
            this.tlpOuterPanel.Controls.Add(this.gbCasetteDetails, 0, 2);
            this.tlpOuterPanel.Controls.Add(this.tlp_Header, 0, 0);
            this.tlpOuterPanel.Controls.Add(this.lbl_Status, 0, 3);
            this.tlpOuterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOuterPanel.Location = new System.Drawing.Point(3, 3);
            this.tlpOuterPanel.Name = "tlpOuterPanel";
            this.tlpOuterPanel.RowCount = 4;
            this.tlpOuterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpOuterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tlpOuterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpOuterPanel.Size = new System.Drawing.Size(753, 580);
            this.tlpOuterPanel.TabIndex = 0;
            // 
            // dgvCasetteDetails
            // 
            this.dgvCasetteDetails.AllowUserToAddRows = false;
            this.dgvCasetteDetails.AllowUserToDeleteRows = false;
            this.dgvCasetteDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgvCasetteDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvCasetteDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCasetteDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCasetteName,
            this.clmIsActive,
            this.clmDenom,
            this.clmAlertLevel,
            this.clmStandardFillAmount,
            this.clmMaxFillAmount});
            this.dgvCasetteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCasetteDetails.Location = new System.Drawing.Point(3, 28);
            this.dgvCasetteDetails.MultiSelect = false;
            this.dgvCasetteDetails.Name = "dgvCasetteDetails";
            this.dgvCasetteDetails.ReadOnly = true;
            this.dgvCasetteDetails.RowTemplate.ReadOnly = true;
            this.dgvCasetteDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCasetteDetails.Size = new System.Drawing.Size(747, 169);
            this.dgvCasetteDetails.TabIndex = 0;
            this.dgvCasetteDetails.SelectionChanged += new System.EventHandler(this.dgvCasetteDetails_SelectionChanged);
            // 
            // gbCasetteDetails
            // 
            this.gbCasetteDetails.Controls.Add(this.tlpCasetteDetails);
            this.gbCasetteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCasetteDetails.Location = new System.Drawing.Point(3, 203);
            this.gbCasetteDetails.Name = "gbCasetteDetails";
            this.gbCasetteDetails.Size = new System.Drawing.Size(747, 349);
            this.gbCasetteDetails.TabIndex = 1;
            this.gbCasetteDetails.TabStop = false;
            // 
            // tlpCasetteDetails
            // 
            this.tlpCasetteDetails.ColumnCount = 2;
            this.tlpCasetteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpCasetteDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tlpCasetteDetails.Controls.Add(this.lblCassetteName, 0, 0);
            this.tlpCasetteDetails.Controls.Add(this.txtCassetteName, 1, 0);
            this.tlpCasetteDetails.Controls.Add(this.lblAlertLevel, 0, 2);
            this.tlpCasetteDetails.Controls.Add(this.txtAlertLevel, 1, 2);
            this.tlpCasetteDetails.Controls.Add(this.lblDenom, 0, 1);
            this.tlpCasetteDetails.Controls.Add(this.cmbDemon, 1, 1);
            this.tlpCasetteDetails.Controls.Add(this.lblStandardFillAmount, 0, 3);
            this.tlpCasetteDetails.Controls.Add(this.txtStandardFillAmount, 1, 3);
            this.tlpCasetteDetails.Controls.Add(this.lblMaxFillAmount, 0, 4);
            this.tlpCasetteDetails.Controls.Add(this.txtMaxFillAmount, 1, 4);
            this.tlpCasetteDetails.Controls.Add(this.tlpButtons, 1, 8);
            this.tlpCasetteDetails.Controls.Add(this.txtDescription, 1, 6);
            this.tlpCasetteDetails.Controls.Add(this.lblDescription, 0, 6);
            this.tlpCasetteDetails.Controls.Add(this.lbl_IsActive, 0, 5);
            this.tlpCasetteDetails.Controls.Add(this.cbIsActive, 1, 5);
            this.tlpCasetteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpCasetteDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpCasetteDetails.Name = "tlpCasetteDetails";
            this.tlpCasetteDetails.RowCount = 9;
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpCasetteDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpCasetteDetails.Size = new System.Drawing.Size(741, 330);
            this.tlpCasetteDetails.TabIndex = 0;
            // 
            // lblCassetteName
            // 
            this.lblCassetteName.AutoSize = true;
            this.lblCassetteName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCassetteName.Location = new System.Drawing.Point(3, 0);
            this.lblCassetteName.Name = "lblCassetteName";
            this.lblCassetteName.Size = new System.Drawing.Size(142, 36);
            this.lblCassetteName.TabIndex = 0;
            this.lblCassetteName.Text = "* Name :";
            this.lblCassetteName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCassetteName
            // 
            this.txtCassetteName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCassetteName.Location = new System.Drawing.Point(151, 8);
            this.txtCassetteName.MaxLength = 20;
            this.txtCassetteName.Name = "txtCassetteName";
            this.txtCassetteName.Size = new System.Drawing.Size(266, 20);
            this.txtCassetteName.TabIndex = 2;
            // 
            // lblAlertLevel
            // 
            this.lblAlertLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAlertLevel.AutoSize = true;
            this.lblAlertLevel.Location = new System.Drawing.Point(3, 83);
            this.lblAlertLevel.Name = "lblAlertLevel";
            this.lblAlertLevel.Size = new System.Drawing.Size(87, 13);
            this.lblAlertLevel.TabIndex = 2;
            this.lblAlertLevel.Text = "* Alert Level (%) :";
            this.lblAlertLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAlertLevel
            // 
            this.txtAlertLevel.AllowDecimal = false;
            this.txtAlertLevel.AllowNegative = false;
            this.txtAlertLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtAlertLevel.DecimalLength = 2;
            this.txtAlertLevel.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAlertLevel.Location = new System.Drawing.Point(151, 80);
            this.txtAlertLevel.MaxLength = 3;
            this.txtAlertLevel.Name = "txtAlertLevel";
            this.txtAlertLevel.ShortcutsEnabled = false;
            this.txtAlertLevel.Size = new System.Drawing.Size(266, 20);
            this.txtAlertLevel.TabIndex = 4;
            this.txtAlertLevel.Enter += new System.EventHandler(this.txtAlertLevel_Enter);
            this.txtAlertLevel.Leave += new System.EventHandler(this.txtAlertLevel_Leave);
            // 
            // lblDenom
            // 
            this.lblDenom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDenom.AutoSize = true;
            this.lblDenom.Location = new System.Drawing.Point(3, 47);
            this.lblDenom.Name = "lblDenom";
            this.lblDenom.Size = new System.Drawing.Size(54, 13);
            this.lblDenom.TabIndex = 4;
            this.lblDenom.Text = "* Denom :";
            this.lblDenom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDemon
            // 
            this.cmbDemon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDemon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDemon.FormattingEnabled = true;
            this.cmbDemon.Location = new System.Drawing.Point(151, 43);
            this.cmbDemon.Name = "cmbDemon";
            this.cmbDemon.Size = new System.Drawing.Size(122, 21);
            this.cmbDemon.TabIndex = 3;
            this.cmbDemon.SelectedIndexChanged += new System.EventHandler(this.cmbDemon_SelectedIndexChanged);
            // 
            // lblStandardFillAmount
            // 
            this.lblStandardFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStandardFillAmount.AutoSize = true;
            this.lblStandardFillAmount.Location = new System.Drawing.Point(3, 119);
            this.lblStandardFillAmount.Name = "lblStandardFillAmount";
            this.lblStandardFillAmount.Size = new System.Drawing.Size(117, 13);
            this.lblStandardFillAmount.TabIndex = 6;
            this.lblStandardFillAmount.Text = "* Standard Fill Amount :";
            this.lblStandardFillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStandardFillAmount
            // 
            this.txtStandardFillAmount.AllowDecimal = false;
            this.txtStandardFillAmount.AllowNegative = false;
            this.txtStandardFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtStandardFillAmount.DecimalLength = 2;
            this.txtStandardFillAmount.Denom = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtStandardFillAmount.Location = new System.Drawing.Point(151, 116);
            this.txtStandardFillAmount.MaxLength = 6;
            this.txtStandardFillAmount.Name = "txtStandardFillAmount";
            this.txtStandardFillAmount.ShortcutsEnabled = false;
            this.txtStandardFillAmount.Size = new System.Drawing.Size(266, 20);
            this.txtStandardFillAmount.TabIndex = 5;
            this.txtStandardFillAmount.Enter += new System.EventHandler(this.txtStandardFillAmount_Enter);
            this.txtStandardFillAmount.Leave += new System.EventHandler(this.txtStandardFillAmount_Leave);
            // 
            // lblMaxFillAmount
            // 
            this.lblMaxFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMaxFillAmount.AutoSize = true;
            this.lblMaxFillAmount.Location = new System.Drawing.Point(3, 155);
            this.lblMaxFillAmount.Name = "lblMaxFillAmount";
            this.lblMaxFillAmount.Size = new System.Drawing.Size(94, 13);
            this.lblMaxFillAmount.TabIndex = 8;
            this.lblMaxFillAmount.Text = "* Max Fill Amount :";
            this.lblMaxFillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMaxFillAmount
            // 
            this.txtMaxFillAmount.AllowDecimal = false;
            this.txtMaxFillAmount.AllowNegative = false;
            this.txtMaxFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtMaxFillAmount.DecimalLength = 2;
            this.txtMaxFillAmount.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtMaxFillAmount.Location = new System.Drawing.Point(151, 152);
            this.txtMaxFillAmount.MaxLength = 6;
            this.txtMaxFillAmount.Name = "txtMaxFillAmount";
            this.txtMaxFillAmount.ShortcutsEnabled = false;
            this.txtMaxFillAmount.Size = new System.Drawing.Size(266, 20);
            this.txtMaxFillAmount.TabIndex = 6;
            // 
            // tlpButtons
            // 
            this.tlpButtons.ColumnCount = 4;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tlpButtons.Controls.Add(this.btnClose, 3, 0);
            this.tlpButtons.Controls.Add(this.btnSave, 2, 0);
            this.tlpButtons.Controls.Add(this.btnAdd, 1, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(151, 291);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.Size = new System.Drawing.Size(587, 36);
            this.tlpButtons.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(490, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 24);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(383, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 24);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAdd.Location = new System.Drawing.Point(278, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 24);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Tag = "Add";
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(151, 219);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 3, 325, 3);
            this.txtDescription.MaxLength = 150;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.tlpCasetteDetails.SetRowSpan(this.txtDescription, 2);
            this.txtDescription.Size = new System.Drawing.Size(265, 66);
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.TabIndex = 8;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 227);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(73, 13);
            this.lblDescription.TabIndex = 12;
            this.lblDescription.Text = "* Description :";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_IsActive
            // 
            this.lbl_IsActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_IsActive.AutoSize = true;
            this.lbl_IsActive.Location = new System.Drawing.Point(3, 191);
            this.lbl_IsActive.Name = "lbl_IsActive";
            this.lbl_IsActive.Size = new System.Drawing.Size(54, 13);
            this.lbl_IsActive.TabIndex = 13;
            this.lbl_IsActive.Text = "Is Active :";
            // 
            // cbIsActive
            // 
            this.cbIsActive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbIsActive.AutoSize = true;
            this.cbIsActive.Location = new System.Drawing.Point(151, 191);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(15, 14);
            this.cbIsActive.TabIndex = 7;
            this.cbIsActive.UseVisualStyleBackColor = true;
            // 
            // tlp_Header
            // 
            this.tlp_Header.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_Header.ColumnCount = 1;
            this.tlp_Header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Header.Controls.Add(this.lbl_Header, 0, 0);
            this.tlp_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Header.Location = new System.Drawing.Point(3, 3);
            this.tlp_Header.Name = "tlp_Header";
            this.tlp_Header.RowCount = 1;
            this.tlp_Header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Header.Size = new System.Drawing.Size(747, 19);
            this.tlp_Header.TabIndex = 2;
            // 
            // lbl_Header
            // 
            this.lbl_Header.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.White;
            this.lbl_Header.Location = new System.Drawing.Point(3, 1);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(0, 17);
            this.lbl_Header.TabIndex = 0;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.ForeColor = System.Drawing.Color.Red;
            this.lbl_Status.Location = new System.Drawing.Point(3, 555);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(747, 25);
            this.lbl_Status.TabIndex = 3;
            // 
            // clmCasetteName
            // 
            this.clmCasetteName.DataPropertyName = "Cassette_Name";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmCasetteName.DefaultCellStyle = dataGridViewCellStyle1;
            this.clmCasetteName.FillWeight = 200F;
            this.clmCasetteName.HeaderText = "Name";
            this.clmCasetteName.Name = "clmCasetteName";
            this.clmCasetteName.ReadOnly = true;
            this.clmCasetteName.Width = 160;
            // 
            // clmIsActive
            // 
            this.clmIsActive.DataPropertyName = "IsActive";
            this.clmIsActive.FillWeight = 45F;
            this.clmIsActive.HeaderText = "IsActive";
            this.clmIsActive.Name = "clmIsActive";
            this.clmIsActive.ReadOnly = true;
            this.clmIsActive.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmIsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmIsActive.Width = 75;
            // 
            // clmDenom
            // 
            this.clmDenom.DataPropertyName = "Denom";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDenom.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmDenom.HeaderText = "Denom";
            this.clmDenom.Name = "clmDenom";
            this.clmDenom.ReadOnly = true;
            this.clmDenom.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmDenom.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.clmDenom.Width = 65;
            // 
            // clmAlertLevel
            // 
            this.clmAlertLevel.DataPropertyName = "AlertLevel";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmAlertLevel.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmAlertLevel.HeaderText = "Alert Level (%)";
            this.clmAlertLevel.Name = "clmAlertLevel";
            this.clmAlertLevel.ReadOnly = true;
            this.clmAlertLevel.Width = 95;
            // 
            // clmStandardFillAmount
            // 
            this.clmStandardFillAmount.DataPropertyName = "StandardFillAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmStandardFillAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmStandardFillAmount.HeaderText = "Standard Fill Amount";
            this.clmStandardFillAmount.Name = "clmStandardFillAmount";
            this.clmStandardFillAmount.ReadOnly = true;
            this.clmStandardFillAmount.Width = 145;
            // 
            // clmMaxFillAmount
            // 
            this.clmMaxFillAmount.DataPropertyName = "MaxFillAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.clmMaxFillAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.clmMaxFillAmount.HeaderText = "Max Fill Amount";
            this.clmMaxFillAmount.Name = "clmMaxFillAmount";
            this.clmMaxFillAmount.ReadOnly = true;
            this.clmMaxFillAmount.Width = 145;
            // 
            // frmCassetteDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 586);
            this.Controls.Add(this.tlpMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCassetteDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmCassetteDetails_Load);
            this.tlpMainPanel.ResumeLayout(false);
            this.tlpOuterPanel.ResumeLayout(false);
            this.tlpOuterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCasetteDetails)).EndInit();
            this.gbCasetteDetails.ResumeLayout(false);
            this.tlpCasetteDetails.ResumeLayout(false);
            this.tlpCasetteDetails.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tlp_Header.ResumeLayout(false);
            this.tlp_Header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.TableLayoutPanel tlpOuterPanel;
        private System.Windows.Forms.DataGridView dgvCasetteDetails;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tlpCasetteDetails;
        private System.Windows.Forms.Label lblCassetteName;
        private System.Windows.Forms.Label lblAlertLevel;
        private System.Windows.Forms.Label lblStandardFillAmount;
        private System.Windows.Forms.Label lblDenom;
        private System.Windows.Forms.Label lblMaxFillAmount;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private NumberTextBox txtStandardFillAmount;
        private NumberTextBox txtAlertLevel;
        private System.Windows.Forms.TextBox txtCassetteName;
        private System.Windows.Forms.CheckBox cbIsActive;
        private System.Windows.Forms.GroupBox gbCasetteDetails;
        private System.Windows.Forms.ComboBox cmbDemon;
        private NumberTextBox txtMaxFillAmount;
        private System.Windows.Forms.Label lbl_IsActive;
        private System.Windows.Forms.TableLayoutPanel tlp_Header;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCasetteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDenom;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAlertLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmStandardFillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMaxFillAmount;
    }
}