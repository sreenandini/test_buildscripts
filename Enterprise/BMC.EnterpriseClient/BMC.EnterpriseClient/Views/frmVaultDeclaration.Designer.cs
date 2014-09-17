namespace BMC.EnterpriseClient.Views
{
    partial class frmVaultDeclaration
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVaultDeclaration));
            this.tlpDetailsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gbDeclarationDetails = new System.Windows.Forms.GroupBox();
            this.tlpDeclaration = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblDeclarationDetails = new System.Windows.Forms.Label();
            this.flpDeclaration = new System.Windows.Forms.FlowLayoutPanel();
            this.gbVaultDetails = new System.Windows.Forms.GroupBox();
            this.tlpVaultDetails = new System.Windows.Forms.TableLayoutPanel();
            this.txtVaultBalance = new System.Windows.Forms.Label();
            this.txtMeterBalance = new System.Windows.Forms.Label();
            this.txtAdjustmentAmount = new System.Windows.Forms.Label();
            this.txtBleedAmount = new System.Windows.Forms.Label();
            this.lblFillAmount = new System.Windows.Forms.Label();
            this.lblBleedAmount = new System.Windows.Forms.Label();
            this.lblAdjustmentAmount = new System.Windows.Forms.Label();
            this.lblMeterBalance = new System.Windows.Forms.Label();
            this.lblVaultBalance = new System.Windows.Forms.Label();
            this.lblDeclaredBalance = new System.Windows.Forms.Label();
            this.txtFillAmount = new System.Windows.Forms.Label();
            this.lblVaultDetails = new System.Windows.Forms.Label();
            this.txtDeclaredBalance = new System.Windows.Forms.Label();
            this.gbDropDetails = new System.Windows.Forms.GroupBox();
            this.tlpDropDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lvDropDetails = new System.Windows.Forms.ListView();
            this.clm_DropID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clm_Drop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDropSiteRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDropDetails = new System.Windows.Forms.Label();
            this.txtTyprPrefix = new System.Windows.Forms.Label();
            this.txtManufacturer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tlpMainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.gbSiteAndVaultDetails = new System.Windows.Forms.GroupBox();
            this.tlpSiteVaultSelectionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblVault = new System.Windows.Forms.Label();
            this.txtVaultName = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tlpDetailsPanel.SuspendLayout();
            this.gbDeclarationDetails.SuspendLayout();
            this.tlpDeclaration.SuspendLayout();
            this.gbVaultDetails.SuspendLayout();
            this.tlpVaultDetails.SuspendLayout();
            this.gbDropDetails.SuspendLayout();
            this.tlpDropDetails.SuspendLayout();
            this.tlpMainPanel.SuspendLayout();
            this.gbSiteAndVaultDetails.SuspendLayout();
            this.tlpSiteVaultSelectionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpDetailsPanel
            // 
            this.tlpDetailsPanel.ColumnCount = 2;
            this.tlpDetailsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpDetailsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpDetailsPanel.Controls.Add(this.gbDeclarationDetails, 1, 0);
            this.tlpDetailsPanel.Controls.Add(this.gbVaultDetails, 0, 1);
            this.tlpDetailsPanel.Controls.Add(this.gbDropDetails, 0, 0);
            this.tlpDetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetailsPanel.Location = new System.Drawing.Point(3, 53);
            this.tlpDetailsPanel.Name = "tlpDetailsPanel";
            this.tlpDetailsPanel.RowCount = 2;
            this.tlpDetailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tlpDetailsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpDetailsPanel.Size = new System.Drawing.Size(1170, 556);
            this.tlpDetailsPanel.TabIndex = 2;
            // 
            // gbDeclarationDetails
            // 
            this.gbDeclarationDetails.Controls.Add(this.tlpDeclaration);
            this.gbDeclarationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDeclarationDetails.Location = new System.Drawing.Point(354, 3);
            this.gbDeclarationDetails.Name = "gbDeclarationDetails";
            this.tlpDetailsPanel.SetRowSpan(this.gbDeclarationDetails, 2);
            this.gbDeclarationDetails.Size = new System.Drawing.Size(813, 550);
            this.gbDeclarationDetails.TabIndex = 2;
            this.gbDeclarationDetails.TabStop = false;
            // 
            // tlpDeclaration
            // 
            this.tlpDeclaration.ColumnCount = 3;
            this.tlpDeclaration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.04348F));
            this.tlpDeclaration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.04348F));
            this.tlpDeclaration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.91304F));
            this.tlpDeclaration.Controls.Add(this.btnSave, 2, 2);
            this.tlpDeclaration.Controls.Add(this.btnStartStop, 0, 2);
            this.tlpDeclaration.Controls.Add(this.btnClear, 1, 2);
            this.tlpDeclaration.Controls.Add(this.lblDeclarationDetails, 0, 0);
            this.tlpDeclaration.Controls.Add(this.flpDeclaration, 0, 1);
            this.tlpDeclaration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDeclaration.Location = new System.Drawing.Point(3, 16);
            this.tlpDeclaration.Name = "tlpDeclaration";
            this.tlpDeclaration.RowCount = 3;
            this.tlpDeclaration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDeclaration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDeclaration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDeclaration.Size = new System.Drawing.Size(807, 531);
            this.tlpDeclaration.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(712, 504);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 24);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStop.Location = new System.Drawing.Point(3, 504);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(92, 24);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Tag = "Start";
            this.btnStartStop.Text = "S&tart Counter";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(108, 504);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(92, 24);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "&Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblDeclarationDetails
            // 
            this.lblDeclarationDetails.AutoSize = true;
            this.lblDeclarationDetails.BackColor = System.Drawing.Color.SteelBlue;
            this.tlpDeclaration.SetColumnSpan(this.lblDeclarationDetails, 3);
            this.lblDeclarationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeclarationDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeclarationDetails.ForeColor = System.Drawing.Color.White;
            this.lblDeclarationDetails.Location = new System.Drawing.Point(3, 0);
            this.lblDeclarationDetails.Name = "lblDeclarationDetails";
            this.lblDeclarationDetails.Size = new System.Drawing.Size(801, 20);
            this.lblDeclarationDetails.TabIndex = 3;
            this.lblDeclarationDetails.Text = "Declaration Details";
            this.lblDeclarationDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpDeclaration
            // 
            this.flpDeclaration.AutoScroll = true;
            this.tlpDeclaration.SetColumnSpan(this.flpDeclaration, 3);
            this.flpDeclaration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpDeclaration.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpDeclaration.Location = new System.Drawing.Point(3, 23);
            this.flpDeclaration.Name = "flpDeclaration";
            this.flpDeclaration.Size = new System.Drawing.Size(801, 475);
            this.flpDeclaration.TabIndex = 4;
            this.flpDeclaration.WrapContents = false;
            // 
            // gbVaultDetails
            // 
            this.gbVaultDetails.Controls.Add(this.tlpVaultDetails);
            this.gbVaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbVaultDetails.Location = new System.Drawing.Point(3, 364);
            this.gbVaultDetails.Name = "gbVaultDetails";
            this.gbVaultDetails.Size = new System.Drawing.Size(345, 189);
            this.gbVaultDetails.TabIndex = 1;
            this.gbVaultDetails.TabStop = false;
            // 
            // tlpVaultDetails
            // 
            this.tlpVaultDetails.ColumnCount = 2;
            this.tlpVaultDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpVaultDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpVaultDetails.Controls.Add(this.txtVaultBalance, 1, 5);
            this.tlpVaultDetails.Controls.Add(this.txtMeterBalance, 1, 4);
            this.tlpVaultDetails.Controls.Add(this.txtAdjustmentAmount, 1, 3);
            this.tlpVaultDetails.Controls.Add(this.txtBleedAmount, 1, 2);
            this.tlpVaultDetails.Controls.Add(this.lblFillAmount, 0, 1);
            this.tlpVaultDetails.Controls.Add(this.lblBleedAmount, 0, 2);
            this.tlpVaultDetails.Controls.Add(this.lblAdjustmentAmount, 0, 3);
            this.tlpVaultDetails.Controls.Add(this.lblMeterBalance, 0, 4);
            this.tlpVaultDetails.Controls.Add(this.lblVaultBalance, 0, 5);
            this.tlpVaultDetails.Controls.Add(this.lblDeclaredBalance, 0, 6);
            this.tlpVaultDetails.Controls.Add(this.txtFillAmount, 1, 1);
            this.tlpVaultDetails.Controls.Add(this.lblVaultDetails, 0, 0);
            this.tlpVaultDetails.Controls.Add(this.txtDeclaredBalance, 1, 6);
            this.tlpVaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpVaultDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpVaultDetails.Name = "tlpVaultDetails";
            this.tlpVaultDetails.RowCount = 7;
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlpVaultDetails.Size = new System.Drawing.Size(339, 170);
            this.tlpVaultDetails.TabIndex = 0;
            // 
            // txtVaultBalance
            // 
            this.txtVaultBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtVaultBalance.BackColor = System.Drawing.Color.Gainsboro;
            this.txtVaultBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVaultBalance.Location = new System.Drawing.Point(149, 118);
            this.txtVaultBalance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtVaultBalance.Name = "txtVaultBalance";
            this.txtVaultBalance.Size = new System.Drawing.Size(175, 20);
            this.txtVaultBalance.TabIndex = 10;
            this.txtVaultBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMeterBalance
            // 
            this.txtMeterBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtMeterBalance.BackColor = System.Drawing.Color.Gainsboro;
            this.txtMeterBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMeterBalance.Location = new System.Drawing.Point(149, 94);
            this.txtMeterBalance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMeterBalance.Name = "txtMeterBalance";
            this.txtMeterBalance.Size = new System.Drawing.Size(175, 20);
            this.txtMeterBalance.TabIndex = 8;
            this.txtMeterBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAdjustmentAmount
            // 
            this.txtAdjustmentAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtAdjustmentAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtAdjustmentAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdjustmentAmount.Location = new System.Drawing.Point(149, 70);
            this.txtAdjustmentAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAdjustmentAmount.Name = "txtAdjustmentAmount";
            this.txtAdjustmentAmount.Size = new System.Drawing.Size(175, 20);
            this.txtAdjustmentAmount.TabIndex = 6;
            this.txtAdjustmentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBleedAmount
            // 
            this.txtBleedAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtBleedAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBleedAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBleedAmount.Location = new System.Drawing.Point(149, 46);
            this.txtBleedAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBleedAmount.Name = "txtBleedAmount";
            this.txtBleedAmount.Size = new System.Drawing.Size(175, 20);
            this.txtBleedAmount.TabIndex = 4;
            this.txtBleedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFillAmount
            // 
            this.lblFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFillAmount.AutoSize = true;
            this.lblFillAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFillAmount.Location = new System.Drawing.Point(3, 25);
            this.lblFillAmount.Name = "lblFillAmount";
            this.lblFillAmount.Size = new System.Drawing.Size(64, 13);
            this.lblFillAmount.TabIndex = 1;
            this.lblFillAmount.Text = "Fill Amount :";
            this.lblFillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBleedAmount
            // 
            this.lblBleedAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBleedAmount.AutoSize = true;
            this.lblBleedAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBleedAmount.Location = new System.Drawing.Point(3, 49);
            this.lblBleedAmount.Name = "lblBleedAmount";
            this.lblBleedAmount.Size = new System.Drawing.Size(79, 13);
            this.lblBleedAmount.TabIndex = 3;
            this.lblBleedAmount.Text = "Bleed Amount :";
            this.lblBleedAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAdjustmentAmount
            // 
            this.lblAdjustmentAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAdjustmentAmount.AutoSize = true;
            this.lblAdjustmentAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdjustmentAmount.Location = new System.Drawing.Point(3, 73);
            this.lblAdjustmentAmount.Name = "lblAdjustmentAmount";
            this.lblAdjustmentAmount.Size = new System.Drawing.Size(104, 13);
            this.lblAdjustmentAmount.TabIndex = 5;
            this.lblAdjustmentAmount.Text = "Adjustment Amount :";
            this.lblAdjustmentAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMeterBalance
            // 
            this.lblMeterBalance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMeterBalance.AutoSize = true;
            this.lblMeterBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeterBalance.Location = new System.Drawing.Point(3, 97);
            this.lblMeterBalance.Name = "lblMeterBalance";
            this.lblMeterBalance.Size = new System.Drawing.Size(82, 13);
            this.lblMeterBalance.TabIndex = 7;
            this.lblMeterBalance.Text = "Meter Balance :";
            this.lblMeterBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVaultBalance
            // 
            this.lblVaultBalance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblVaultBalance.AutoSize = true;
            this.lblVaultBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaultBalance.Location = new System.Drawing.Point(3, 121);
            this.lblVaultBalance.Name = "lblVaultBalance";
            this.lblVaultBalance.Size = new System.Drawing.Size(79, 13);
            this.lblVaultBalance.TabIndex = 9;
            this.lblVaultBalance.Text = "Vault Balance :";
            this.lblVaultBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDeclaredBalance
            // 
            this.lblDeclaredBalance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDeclaredBalance.AutoSize = true;
            this.lblDeclaredBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeclaredBalance.Location = new System.Drawing.Point(3, 148);
            this.lblDeclaredBalance.Name = "lblDeclaredBalance";
            this.lblDeclaredBalance.Size = new System.Drawing.Size(98, 13);
            this.lblDeclaredBalance.TabIndex = 11;
            this.lblDeclaredBalance.Text = "Declared Balance :";
            this.lblDeclaredBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFillAmount
            // 
            this.txtFillAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtFillAmount.BackColor = System.Drawing.Color.Gainsboro;
            this.txtFillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFillAmount.Location = new System.Drawing.Point(149, 22);
            this.txtFillAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFillAmount.Name = "txtFillAmount";
            this.txtFillAmount.Size = new System.Drawing.Size(175, 20);
            this.txtFillAmount.TabIndex = 2;
            this.txtFillAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblVaultDetails
            // 
            this.lblVaultDetails.AutoSize = true;
            this.lblVaultDetails.BackColor = System.Drawing.Color.SteelBlue;
            this.tlpVaultDetails.SetColumnSpan(this.lblVaultDetails, 2);
            this.lblVaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVaultDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVaultDetails.ForeColor = System.Drawing.Color.White;
            this.lblVaultDetails.Location = new System.Drawing.Point(3, 0);
            this.lblVaultDetails.Name = "lblVaultDetails";
            this.lblVaultDetails.Size = new System.Drawing.Size(333, 20);
            this.lblVaultDetails.TabIndex = 0;
            this.lblVaultDetails.Text = "Vault Details";
            this.lblVaultDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDeclaredBalance
            // 
            this.txtDeclaredBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtDeclaredBalance.BackColor = System.Drawing.Color.Gainsboro;
            this.txtDeclaredBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDeclaredBalance.Location = new System.Drawing.Point(149, 142);
            this.txtDeclaredBalance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDeclaredBalance.Name = "txtDeclaredBalance";
            this.txtDeclaredBalance.Size = new System.Drawing.Size(175, 26);
            this.txtDeclaredBalance.TabIndex = 12;
            this.txtDeclaredBalance.Text = "0";
            this.txtDeclaredBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbDropDetails
            // 
            this.gbDropDetails.Controls.Add(this.tlpDropDetails);
            this.gbDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDropDetails.Location = new System.Drawing.Point(3, 3);
            this.gbDropDetails.Name = "gbDropDetails";
            this.gbDropDetails.Size = new System.Drawing.Size(345, 355);
            this.gbDropDetails.TabIndex = 0;
            this.gbDropDetails.TabStop = false;
            // 
            // tlpDropDetails
            // 
            this.tlpDropDetails.ColumnCount = 1;
            this.tlpDropDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDropDetails.Controls.Add(this.lvDropDetails, 0, 1);
            this.tlpDropDetails.Controls.Add(this.lblDropDetails, 0, 0);
            this.tlpDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDropDetails.Location = new System.Drawing.Point(3, 16);
            this.tlpDropDetails.Name = "tlpDropDetails";
            this.tlpDropDetails.RowCount = 2;
            this.tlpDropDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpDropDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpDropDetails.Size = new System.Drawing.Size(339, 336);
            this.tlpDropDetails.TabIndex = 1;
            // 
            // lvDropDetails
            // 
            this.lvDropDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clm_DropID,
            this.clm_Drop,
            this.clmDropSiteRef});
            this.lvDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDropDetails.FullRowSelect = true;
            this.lvDropDetails.GridLines = true;
            this.lvDropDetails.HideSelection = false;
            this.lvDropDetails.Location = new System.Drawing.Point(3, 23);
            this.lvDropDetails.MultiSelect = false;
            this.lvDropDetails.Name = "lvDropDetails";
            this.lvDropDetails.Size = new System.Drawing.Size(333, 310);
            this.lvDropDetails.TabIndex = 1;
            this.lvDropDetails.UseCompatibleStateImageBehavior = false;
            this.lvDropDetails.View = System.Windows.Forms.View.Details;
            this.lvDropDetails.SelectedIndexChanged += new System.EventHandler(this.lvDropDetails_SelectedIndexChanged);
            // 
            // clm_DropID
            // 
            this.clm_DropID.Text = "Drop ID";
            this.clm_DropID.Width = 70;
            // 
            // clm_Drop
            // 
            this.clm_Drop.Text = "Drop Created";
            this.clm_Drop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clm_Drop.Width = 125;
            // 
            // clmDropSiteRef
            // 
            this.clmDropSiteRef.Text = "Site Drop Ref";
            this.clmDropSiteRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmDropSiteRef.Width = 93;
            // 
            // lblDropDetails
            // 
            this.lblDropDetails.AutoSize = true;
            this.lblDropDetails.BackColor = System.Drawing.Color.SteelBlue;
            this.lblDropDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDropDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDropDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDropDetails.ForeColor = System.Drawing.Color.White;
            this.lblDropDetails.Location = new System.Drawing.Point(3, 0);
            this.lblDropDetails.Name = "lblDropDetails";
            this.lblDropDetails.Size = new System.Drawing.Size(333, 20);
            this.lblDropDetails.TabIndex = 0;
            this.lblDropDetails.Text = "Drop Details";
            this.lblDropDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTyprPrefix
            // 
            this.txtTyprPrefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTyprPrefix.BackColor = System.Drawing.Color.Gainsboro;
            this.txtTyprPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTyprPrefix.Location = new System.Drawing.Point(956, 2);
            this.txtTyprPrefix.Name = "txtTyprPrefix";
            this.txtTyprPrefix.Size = new System.Drawing.Size(171, 20);
            this.txtTyprPrefix.TabIndex = 7;
            this.txtTyprPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtManufacturer.BackColor = System.Drawing.Color.Gainsboro;
            this.txtManufacturer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtManufacturer.Location = new System.Drawing.Point(676, 2);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.Size = new System.Drawing.Size(180, 20);
            this.txtManufacturer.TabIndex = 5;
            this.txtManufacturer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(913, 3);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlpMainPanel
            // 
            this.tlpMainPanel.ColumnCount = 1;
            this.tlpMainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.Controls.Add(this.tlpDetailsPanel, 0, 1);
            this.tlpMainPanel.Controls.Add(this.gbSiteAndVaultDetails, 0, 0);
            this.tlpMainPanel.Controls.Add(this.lblStatus, 0, 2);
            this.tlpMainPanel.Controls.Add(this.ssStatus, 0, 3);
            this.tlpMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpMainPanel.Name = "tlpMainPanel";
            this.tlpMainPanel.RowCount = 4;
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainPanel.Size = new System.Drawing.Size(1176, 656);
            this.tlpMainPanel.TabIndex = 0;
            // 
            // gbSiteAndVaultDetails
            // 
            this.gbSiteAndVaultDetails.Controls.Add(this.tlpSiteVaultSelectionPanel);
            this.gbSiteAndVaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSiteAndVaultDetails.Location = new System.Drawing.Point(3, 3);
            this.gbSiteAndVaultDetails.Name = "gbSiteAndVaultDetails";
            this.gbSiteAndVaultDetails.Size = new System.Drawing.Size(1170, 44);
            this.gbSiteAndVaultDetails.TabIndex = 1;
            this.gbSiteAndVaultDetails.TabStop = false;
            // 
            // tlpSiteVaultSelectionPanel
            // 
            this.tlpSiteVaultSelectionPanel.ColumnCount = 8;
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.283069F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84962F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.283068F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84962F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.752305F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84962F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.283068F));
            this.tlpSiteVaultSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.84962F));
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.lblSite, 0, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.txtTyprPrefix, 7, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.lblVault, 2, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.txtManufacturer, 5, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.label3, 6, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.txtVaultName, 3, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.lblManufacturer, 4, 0);
            this.tlpSiteVaultSelectionPanel.Controls.Add(this.txtSiteName, 1, 0);
            this.tlpSiteVaultSelectionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSiteVaultSelectionPanel.Location = new System.Drawing.Point(3, 16);
            this.tlpSiteVaultSelectionPanel.Name = "tlpSiteVaultSelectionPanel";
            this.tlpSiteVaultSelectionPanel.RowCount = 1;
            this.tlpSiteVaultSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSiteVaultSelectionPanel.Size = new System.Drawing.Size(1164, 25);
            this.tlpSiteVaultSelectionPanel.TabIndex = 0;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSite.AutoSize = true;
            this.lblSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(39, 3);
            this.lblSite.Margin = new System.Windows.Forms.Padding(3);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(31, 19);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "Site :";
            this.lblSite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVault
            // 
            this.lblVault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVault.AutoSize = true;
            this.lblVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVault.Location = new System.Drawing.Point(313, 3);
            this.lblVault.Margin = new System.Windows.Forms.Padding(3);
            this.lblVault.Name = "lblVault";
            this.lblVault.Size = new System.Drawing.Size(37, 19);
            this.lblVault.TabIndex = 2;
            this.lblVault.Text = "Vault :";
            this.lblVault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVaultName
            // 
            this.txtVaultName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtVaultName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtVaultName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVaultName.Location = new System.Drawing.Point(356, 2);
            this.txtVaultName.Name = "txtVaultName";
            this.txtVaultName.Size = new System.Drawing.Size(180, 20);
            this.txtVaultName.TabIndex = 3;
            this.txtVaultName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(594, 6);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(76, 13);
            this.lblManufacturer.TabIndex = 4;
            this.lblManufacturer.Text = "Manufacturer :";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSiteName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtSiteName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSiteName.Location = new System.Drawing.Point(76, 2);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(180, 20);
            this.txtSiteName.TabIndex = 1;
            this.txtSiteName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtSiteName.MouseHover += new System.EventHandler(this.txtSiteName_MouseHover);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(588, 617);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 7;
            // 
            // ssStatus
            // 
            this.ssStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ssStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.ssStatus.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.ssStatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.ssStatus.Location = new System.Drawing.Point(0, 636);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(1176, 20);
            this.ssStatus.TabIndex = 10;
            // 
            // frmVaultDeclaration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 656);
            this.Controls.Add(this.tlpMainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVaultDeclaration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VaultDeclaration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVaultDeclaration_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmVaultDeclaration_FormClosed);
            this.Load += new System.EventHandler(this.frmVaultDeclaration_Load);
            this.tlpDetailsPanel.ResumeLayout(false);
            this.gbDeclarationDetails.ResumeLayout(false);
            this.tlpDeclaration.ResumeLayout(false);
            this.tlpDeclaration.PerformLayout();
            this.gbVaultDetails.ResumeLayout(false);
            this.tlpVaultDetails.ResumeLayout(false);
            this.tlpVaultDetails.PerformLayout();
            this.gbDropDetails.ResumeLayout(false);
            this.tlpDropDetails.ResumeLayout(false);
            this.tlpDropDetails.PerformLayout();
            this.tlpMainPanel.ResumeLayout(false);
            this.tlpMainPanel.PerformLayout();
            this.gbSiteAndVaultDetails.ResumeLayout(false);
            this.tlpSiteVaultSelectionPanel.ResumeLayout(false);
            this.tlpSiteVaultSelectionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainPanel;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsPanel;
        private System.Windows.Forms.GroupBox gbDeclarationDetails;
        private System.Windows.Forms.GroupBox gbVaultDetails;
        private System.Windows.Forms.TableLayoutPanel tlpVaultDetails;
        private System.Windows.Forms.Label lblFillAmount;
        private System.Windows.Forms.Label lblBleedAmount;
        private System.Windows.Forms.Label lblAdjustmentAmount;
        private System.Windows.Forms.Label lblMeterBalance;
        private System.Windows.Forms.Label lblVaultBalance;
        private System.Windows.Forms.Label lblDeclaredBalance;
        private System.Windows.Forms.Label txtDeclaredBalance;
        private System.Windows.Forms.Label txtVaultBalance;
        private System.Windows.Forms.Label txtMeterBalance;
        private System.Windows.Forms.Label txtAdjustmentAmount;
        private System.Windows.Forms.Label txtBleedAmount;
        private System.Windows.Forms.Label txtFillAmount;
        private System.Windows.Forms.GroupBox gbSiteAndVaultDetails;
        private System.Windows.Forms.TableLayoutPanel tlpSiteVaultSelectionPanel;
        private System.Windows.Forms.Label lblVault;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.TableLayoutPanel tlpDeclaration;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox gbDropDetails;
        private System.Windows.Forms.ListView lvDropDetails;
        private System.Windows.Forms.Label txtTyprPrefix;
        private System.Windows.Forms.Label txtManufacturer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtVaultName;
        private System.Windows.Forms.ColumnHeader clm_Drop;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ColumnHeader clm_DropID;
        private System.Windows.Forms.ColumnHeader clmDropSiteRef;
        private System.Windows.Forms.TableLayoutPanel tlpDropDetails;
        private System.Windows.Forms.Label lblDropDetails;
        private System.Windows.Forms.Label lblVaultDetails;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.FlowLayoutPanel flpDeclaration;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblDeclarationDetails;
        private System.Windows.Forms.Label txtSiteName;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}