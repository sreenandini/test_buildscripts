namespace BMC.EnterpriseClient.Views
{
    partial class frmAssetManagement
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
            this.ctMenuAssetTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.IDM_ADD_TYPE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_EDIT_TYPE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_MODEL_ADMIN = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_BUY_MACHINE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_EDIT_MACHINE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_ADD_MODEL_NGA = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_EDIT_MODEL_NGA = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_TERMINATE_MACHINE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_VIEW_TERMINATE_MACHINE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SAVE_TEMPLATE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_EDIT_TEMPLATE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DELETE_TEMPLATE = new System.Windows.Forms.ToolStripMenuItem();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.cmbFilterbyGame = new System.Windows.Forms.ComboBox();
            this.btnAvailabilitySelectNone = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGame = new System.Windows.Forms.Label();
            this.lstMachineAvailability = new System.Windows.Forms.CheckedListBox();
            this.btnAvailabilitySelectAll = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbRepresentative = new System.Windows.Forms.ComboBox();
            this.BtnEditUser = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbDepot = new System.Windows.Forms.ComboBox();
            this.btnEditDepot = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.btnEditSupplier = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbGameCategory = new BMC.Common.Utilities.BmcComboBox();
            this.btnGameCat = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbModelType = new System.Windows.Forms.ComboBox();
            this.btnModelType = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.btnEditManufacturer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMachineTypes = new System.Windows.Forms.ComboBox();
            this.btnEditTypes = new System.Windows.Forms.Button();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpView = new System.Windows.Forms.GroupBox();
            this.chkDisplayInstallationDetails = new System.Windows.Forms.CheckBox();
            this.cmbGroupStockBy = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvStock = new System.Windows.Forms.TreeView();
            this.pnlbuttons = new System.Windows.Forms.Panel();
            this.tbButtonGroup = new System.Windows.Forms.TableLayoutPanel();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnDepreciation = new System.Windows.Forms.Button();
            this.btnExAsset = new System.Windows.Forms.Button();
            this.btnImpAsset = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctMenuAssetTree.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.gpView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlbuttons.SuspendLayout();
            this.tbButtonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctMenuAssetTree
            // 
            this.ctMenuAssetTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_ADD_TYPE,
            this.IDM_EDIT_TYPE,
            this.IDM_MODEL_ADMIN,
            this.IDM_BUY_MACHINE,
            this.IDM_EDIT_MACHINE,
            this.IDM_ADD_MODEL_NGA,
            this.IDM_EDIT_MODEL_NGA,
            this.IDM_TERMINATE_MACHINE,
            this.IDM_VIEW_TERMINATE_MACHINE,
            this.IDM_SAVE_TEMPLATE,
            this.IDM_EDIT_TEMPLATE,
            this.IDM_DELETE_TEMPLATE});
            this.ctMenuAssetTree.Name = "contextMenuStrip1";
            this.ctMenuAssetTree.ShowImageMargin = false;
            this.ctMenuAssetTree.Size = new System.Drawing.Size(181, 268);
            this.ctMenuAssetTree.Text = "IDM_DELETE_TEMPLATE";
            // 
            // IDM_ADD_TYPE
            // 
            this.IDM_ADD_TYPE.Name = "IDM_ADD_TYPE";
            this.IDM_ADD_TYPE.Size = new System.Drawing.Size(180, 22);
            this.IDM_ADD_TYPE.Text = "Add Type";
            this.IDM_ADD_TYPE.Click += new System.EventHandler(this.IDM_ADD_TYPE_Click);
            // 
            // IDM_EDIT_TYPE
            // 
            this.IDM_EDIT_TYPE.Name = "IDM_EDIT_TYPE";
            this.IDM_EDIT_TYPE.Size = new System.Drawing.Size(180, 22);
            this.IDM_EDIT_TYPE.Text = "Edit Type";
            this.IDM_EDIT_TYPE.Click += new System.EventHandler(this.IDM_EDIT_TYPE_Click);
            // 
            // IDM_MODEL_ADMIN
            // 
            this.IDM_MODEL_ADMIN.Name = "IDM_MODEL_ADMIN";
            this.IDM_MODEL_ADMIN.Size = new System.Drawing.Size(180, 22);
            this.IDM_MODEL_ADMIN.Text = "Model Administrator";
            this.IDM_MODEL_ADMIN.Click += new System.EventHandler(this.IDM_MODEL_ADMIN_Click);
            // 
            // IDM_BUY_MACHINE
            // 
            this.IDM_BUY_MACHINE.Name = "IDM_BUY_MACHINE";
            this.IDM_BUY_MACHINE.Size = new System.Drawing.Size(180, 22);
            this.IDM_BUY_MACHINE.Text = "Buy Machine";
            this.IDM_BUY_MACHINE.Click += new System.EventHandler(this.toolStrip_buymachine_Click);
            // 
            // IDM_EDIT_MACHINE
            // 
            this.IDM_EDIT_MACHINE.Name = "IDM_EDIT_MACHINE";
            this.IDM_EDIT_MACHINE.Size = new System.Drawing.Size(180, 22);
            this.IDM_EDIT_MACHINE.Text = "Edit Machine";
            this.IDM_EDIT_MACHINE.Click += new System.EventHandler(this.IDM_EDIT_MACHINE_Click);
            // 
            // IDM_ADD_MODEL_NGA
            // 
            this.IDM_ADD_MODEL_NGA.Name = "IDM_ADD_MODEL_NGA";
            this.IDM_ADD_MODEL_NGA.Size = new System.Drawing.Size(180, 22);
            this.IDM_ADD_MODEL_NGA.Text = "Add Category";
            this.IDM_ADD_MODEL_NGA.Visible = false;
            this.IDM_ADD_MODEL_NGA.Click += new System.EventHandler(this.IDM_ADD_MODEL_NGA_Click);
            // 
            // IDM_EDIT_MODEL_NGA
            // 
            this.IDM_EDIT_MODEL_NGA.Name = "IDM_EDIT_MODEL_NGA";
            this.IDM_EDIT_MODEL_NGA.Size = new System.Drawing.Size(180, 22);
            this.IDM_EDIT_MODEL_NGA.Text = "Edit Category";
            this.IDM_EDIT_MODEL_NGA.Visible = false;
            this.IDM_EDIT_MODEL_NGA.Click += new System.EventHandler(this.IDM_EDIT_MODEL_NGA_Click);
            // 
            // IDM_TERMINATE_MACHINE
            // 
            this.IDM_TERMINATE_MACHINE.Name = "IDM_TERMINATE_MACHINE";
            this.IDM_TERMINATE_MACHINE.Size = new System.Drawing.Size(180, 22);
            this.IDM_TERMINATE_MACHINE.Text = "Terminate Machine";
            this.IDM_TERMINATE_MACHINE.Click += new System.EventHandler(this.IDM_TERMINATE_MACHINE_Click);
            // 
            // IDM_VIEW_TERMINATE_MACHINE
            // 
            this.IDM_VIEW_TERMINATE_MACHINE.Name = "IDM_VIEW_TERMINATE_MACHINE";
            this.IDM_VIEW_TERMINATE_MACHINE.Size = new System.Drawing.Size(180, 22);
            this.IDM_VIEW_TERMINATE_MACHINE.Text = "View Termination Details";
            this.IDM_VIEW_TERMINATE_MACHINE.Click += new System.EventHandler(this.IDM_VIEW_TERMINATE_MACHINE_Click);
            // 
            // IDM_SAVE_TEMPLATE
            // 
            this.IDM_SAVE_TEMPLATE.Enabled = false;
            this.IDM_SAVE_TEMPLATE.Name = "IDM_SAVE_TEMPLATE";
            this.IDM_SAVE_TEMPLATE.Size = new System.Drawing.Size(180, 22);
            this.IDM_SAVE_TEMPLATE.Text = "Save As Template";
            this.IDM_SAVE_TEMPLATE.Click += new System.EventHandler(this.IDM_SAVE_TEMPLATE_Click);
            // 
            // IDM_EDIT_TEMPLATE
            // 
            this.IDM_EDIT_TEMPLATE.Enabled = false;
            this.IDM_EDIT_TEMPLATE.Name = "IDM_EDIT_TEMPLATE";
            this.IDM_EDIT_TEMPLATE.Size = new System.Drawing.Size(180, 22);
            this.IDM_EDIT_TEMPLATE.Text = "Edit Template";
            this.IDM_EDIT_TEMPLATE.Click += new System.EventHandler(this.IDM_EDIT_TEMPLATE_Click);
            // 
            // IDM_DELETE_TEMPLATE
            // 
            this.IDM_DELETE_TEMPLATE.Enabled = false;
            this.IDM_DELETE_TEMPLATE.Name = "IDM_DELETE_TEMPLATE";
            this.IDM_DELETE_TEMPLATE.Size = new System.Drawing.Size(180, 22);
            this.IDM_DELETE_TEMPLATE.Text = "Delete Template";
            this.IDM_DELETE_TEMPLATE.Click += new System.EventHandler(this.IDM_DELETE_TEMPLATE_Click);
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.cmbFilterbyGame);
            this.grpFilter.Controls.Add(this.btnAvailabilitySelectNone);
            this.grpFilter.Controls.Add(this.label2);
            this.grpFilter.Controls.Add(this.lblGame);
            this.grpFilter.Controls.Add(this.lstMachineAvailability);
            this.grpFilter.Controls.Add(this.btnAvailabilitySelectAll);
            this.grpFilter.Controls.Add(this.label9);
            this.grpFilter.Controls.Add(this.cmbRepresentative);
            this.grpFilter.Controls.Add(this.BtnEditUser);
            this.grpFilter.Controls.Add(this.label8);
            this.grpFilter.Controls.Add(this.cmbDepot);
            this.grpFilter.Controls.Add(this.btnEditDepot);
            this.grpFilter.Controls.Add(this.label7);
            this.grpFilter.Controls.Add(this.cmbSupplier);
            this.grpFilter.Controls.Add(this.btnEditSupplier);
            this.grpFilter.Controls.Add(this.label6);
            this.grpFilter.Controls.Add(this.cmbGameCategory);
            this.grpFilter.Controls.Add(this.btnGameCat);
            this.grpFilter.Controls.Add(this.label5);
            this.grpFilter.Controls.Add(this.cmbModelType);
            this.grpFilter.Controls.Add(this.btnModelType);
            this.grpFilter.Controls.Add(this.label4);
            this.grpFilter.Controls.Add(this.cmbManufacturer);
            this.grpFilter.Controls.Add(this.btnEditManufacturer);
            this.grpFilter.Controls.Add(this.label3);
            this.grpFilter.Controls.Add(this.cmbMachineTypes);
            this.grpFilter.Controls.Add(this.btnEditTypes);
            this.grpFilter.Controls.Add(this.txtMachineName);
            this.grpFilter.Controls.Add(this.label1);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilter.Location = new System.Drawing.Point(6, 0);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(312, 589);
            this.grpFilter.TabIndex = 0;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Filter";
            this.grpFilter.Enter += new System.EventHandler(this.grpFilter_Enter);
            // 
            // cmbFilterbyGame
            // 
            this.cmbFilterbyGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFilterbyGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterbyGame.FormattingEnabled = true;
            this.cmbFilterbyGame.Location = new System.Drawing.Point(2, 93);
            this.cmbFilterbyGame.Name = "cmbFilterbyGame";
            this.cmbFilterbyGame.Size = new System.Drawing.Size(310, 21);
            this.cmbFilterbyGame.TabIndex = 3;
            // 
            // btnAvailabilitySelectNone
            // 
            this.btnAvailabilitySelectNone.Location = new System.Drawing.Point(5, 538);
            this.btnAvailabilitySelectNone.Name = "btnAvailabilitySelectNone";
            this.btnAvailabilitySelectNone.Size = new System.Drawing.Size(97, 23);
            this.btnAvailabilitySelectNone.TabIndex = 28;
            this.btnAvailabilitySelectNone.Text = "&Select None";
            this.btnAvailabilitySelectNone.UseVisualStyleBackColor = true;
            this.btnAvailabilitySelectNone.Click += new System.EventHandler(this.btnAvailabilitySelectNone_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Machine Types:";
            // 
            // lblGame
            // 
            this.lblGame.AutoSize = true;
            this.lblGame.Location = new System.Drawing.Point(2, 72);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(69, 13);
            this.lblGame.TabIndex = 2;
            this.lblGame.Text = "Game Name:";
            // 
            // lstMachineAvailability
            // 
            this.lstMachineAvailability.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMachineAvailability.CheckOnClick = true;
            this.lstMachineAvailability.FormattingEnabled = true;
            this.lstMachineAvailability.Location = new System.Drawing.Point(2, 444);
            this.lstMachineAvailability.Name = "lstMachineAvailability";
            this.lstMachineAvailability.Size = new System.Drawing.Size(310, 94);
            this.lstMachineAvailability.TabIndex = 27;
            // 
            // btnAvailabilitySelectAll
            // 
            this.btnAvailabilitySelectAll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAvailabilitySelectAll.Location = new System.Drawing.Point(231, 537);
            this.btnAvailabilitySelectAll.Name = "btnAvailabilitySelectAll";
            this.btnAvailabilitySelectAll.Size = new System.Drawing.Size(81, 23);
            this.btnAvailabilitySelectAll.TabIndex = 29;
            this.btnAvailabilitySelectAll.Text = "Selec&t All";
            this.btnAvailabilitySelectAll.UseVisualStyleBackColor = true;
            this.btnAvailabilitySelectAll.Click += new System.EventHandler(this.btnAvailabilitySelectAll_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 426);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Machine Availability:";
            // 
            // cmbRepresentative
            // 
            this.cmbRepresentative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRepresentative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRepresentative.FormattingEnabled = true;
            this.cmbRepresentative.Location = new System.Drawing.Point(1, 402);
            this.cmbRepresentative.Name = "cmbRepresentative";
            this.cmbRepresentative.Size = new System.Drawing.Size(311, 21);
            this.cmbRepresentative.TabIndex = 24;
            // 
            // BtnEditUser
            // 
            this.BtnEditUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnEditUser.Location = new System.Drawing.Point(223, 380);
            this.BtnEditUser.Name = "BtnEditUser";
            this.BtnEditUser.Size = new System.Drawing.Size(90, 21);
            this.BtnEditUser.TabIndex = 22;
            this.BtnEditUser.Text = "Admin";
            this.BtnEditUser.UseVisualStyleBackColor = true;
            this.BtnEditUser.Click += new System.EventHandler(this.BtnEditUser_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 386);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Representative:";
            // 
            // cmbDepot
            // 
            this.cmbDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(0, 359);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(312, 21);
            this.cmbDepot.TabIndex = 21;
            // 
            // btnEditDepot
            // 
            this.btnEditDepot.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditDepot.Location = new System.Drawing.Point(223, 338);
            this.btnEditDepot.Name = "btnEditDepot";
            this.btnEditDepot.Size = new System.Drawing.Size(90, 21);
            this.btnEditDepot.TabIndex = 19;
            this.btnEditDepot.Text = "Admin";
            this.btnEditDepot.UseVisualStyleBackColor = true;
            this.btnEditDepot.Click += new System.EventHandler(this.btnEditDepot_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 342);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Depot :";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(0, 316);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(312, 21);
            this.cmbSupplier.TabIndex = 18;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // btnEditSupplier
            // 
            this.btnEditSupplier.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditSupplier.Location = new System.Drawing.Point(223, 295);
            this.btnEditSupplier.Name = "btnEditSupplier";
            this.btnEditSupplier.Size = new System.Drawing.Size(90, 21);
            this.btnEditSupplier.TabIndex = 16;
            this.btnEditSupplier.Text = "Admin";
            this.btnEditSupplier.UseVisualStyleBackColor = true;
            this.btnEditSupplier.Click += new System.EventHandler(this.btnEditSupplier_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Operator:";
            // 
            // cmbGameCategory
            // 
            this.cmbGameCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGameCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGameCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGameCategory.FormattingEnabled = true;
            this.cmbGameCategory.Location = new System.Drawing.Point(0, 274);
            this.cmbGameCategory.Name = "cmbGameCategory";
            this.cmbGameCategory.Size = new System.Drawing.Size(313, 21);
            this.cmbGameCategory.TabIndex = 15;
            // 
            // btnGameCat
            // 
            this.btnGameCat.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnGameCat.Location = new System.Drawing.Point(223, 252);
            this.btnGameCat.Name = "btnGameCat";
            this.btnGameCat.Size = new System.Drawing.Size(90, 21);
            this.btnGameCat.TabIndex = 13;
            this.btnGameCat.Text = "Admin";
            this.btnGameCat.UseVisualStyleBackColor = true;
            this.btnGameCat.Click += new System.EventHandler(this.btnGameCat_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Game Category:";
            // 
            // cmbModelType
            // 
            this.cmbModelType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbModelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModelType.FormattingEnabled = true;
            this.cmbModelType.Location = new System.Drawing.Point(0, 230);
            this.cmbModelType.Name = "cmbModelType";
            this.cmbModelType.Size = new System.Drawing.Size(313, 21);
            this.cmbModelType.TabIndex = 12;
            // 
            // btnModelType
            // 
            this.btnModelType.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnModelType.Location = new System.Drawing.Point(223, 208);
            this.btnModelType.Name = "btnModelType";
            this.btnModelType.Size = new System.Drawing.Size(90, 21);
            this.btnModelType.TabIndex = 10;
            this.btnModelType.Text = "Admin";
            this.btnModelType.UseVisualStyleBackColor = true;
            this.btnModelType.Click += new System.EventHandler(this.btnModelType_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Cabinet/Model Type:";
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(0, 186);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(313, 21);
            this.cmbManufacturer.TabIndex = 9;
            // 
            // btnEditManufacturer
            // 
            this.btnEditManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditManufacturer.Location = new System.Drawing.Point(223, 164);
            this.btnEditManufacturer.Name = "btnEditManufacturer";
            this.btnEditManufacturer.Size = new System.Drawing.Size(90, 21);
            this.btnEditManufacturer.TabIndex = 7;
            this.btnEditManufacturer.Text = "Admin";
            this.btnEditManufacturer.UseVisualStyleBackColor = true;
            this.btnEditManufacturer.Click += new System.EventHandler(this.btnEditManufacturer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Manufacturer: ";
            // 
            // cmbMachineTypes
            // 
            this.cmbMachineTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineTypes.FormattingEnabled = true;
            this.cmbMachineTypes.Location = new System.Drawing.Point(0, 142);
            this.cmbMachineTypes.Name = "cmbMachineTypes";
            this.cmbMachineTypes.Size = new System.Drawing.Size(313, 21);
            this.cmbMachineTypes.TabIndex = 6;
            // 
            // btnEditTypes
            // 
            this.btnEditTypes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEditTypes.Location = new System.Drawing.Point(223, 120);
            this.btnEditTypes.Name = "btnEditTypes";
            this.btnEditTypes.Size = new System.Drawing.Size(90, 21);
            this.btnEditTypes.TabIndex = 4;
            this.btnEditTypes.Text = "Admin";
            this.btnEditTypes.UseVisualStyleBackColor = true;
            this.btnEditTypes.Click += new System.EventHandler(this.btnEditTypes_Click);
            // 
            // txtMachineName
            // 
            this.txtMachineName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineName.Location = new System.Drawing.Point(2, 47);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Size = new System.Drawing.Size(310, 20);
            this.txtMachineName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Title/Asset No/Serial No:";
            // 
            // gpView
            // 
            this.gpView.Controls.Add(this.chkDisplayInstallationDetails);
            this.gpView.Controls.Add(this.cmbGroupStockBy);
            this.gpView.Controls.Add(this.label10);
            this.gpView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gpView.Location = new System.Drawing.Point(6, 595);
            this.gpView.Name = "gpView";
            this.gpView.Size = new System.Drawing.Size(312, 83);
            this.gpView.TabIndex = 1;
            this.gpView.TabStop = false;
            this.gpView.Text = "Viewing Options";
            // 
            // chkDisplayInstallationDetails
            // 
            this.chkDisplayInstallationDetails.AutoSize = true;
            this.chkDisplayInstallationDetails.Location = new System.Drawing.Point(3, 64);
            this.chkDisplayInstallationDetails.Name = "chkDisplayInstallationDetails";
            this.chkDisplayInstallationDetails.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDisplayInstallationDetails.Size = new System.Drawing.Size(148, 17);
            this.chkDisplayInstallationDetails.TabIndex = 2;
            this.chkDisplayInstallationDetails.Text = "Display Installation Details";
            this.chkDisplayInstallationDetails.UseVisualStyleBackColor = true;
            this.chkDisplayInstallationDetails.CheckedChanged += new System.EventHandler(this.chkDisplayInstallationDetails_CheckedChanged);
            // 
            // cmbGroupStockBy
            // 
            this.cmbGroupStockBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroupStockBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupStockBy.FormattingEnabled = true;
            this.cmbGroupStockBy.Items.AddRange(new object[] {
            "Avaliability",
            "Depot",
            "Machine Type",
            "Representative"});
            this.cmbGroupStockBy.Location = new System.Drawing.Point(2, 37);
            this.cmbGroupStockBy.Name = "cmbGroupStockBy";
            this.cmbGroupStockBy.Size = new System.Drawing.Size(310, 21);
            this.cmbGroupStockBy.TabIndex = 1;
            this.cmbGroupStockBy.SelectedIndexChanged += new System.EventHandler(this.cmbGroupStockBy_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Group Stock By:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.grpFilter);
            this.splitContainer1.Panel1.Controls.Add(this.gpView);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1MinSize = 225;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tvStock);
            this.splitContainer1.Panel2.Controls.Add(this.pnlbuttons);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1117, 678);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvStock
            // 
            this.tvStock.ContextMenuStrip = this.ctMenuAssetTree;
            this.tvStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStock.Location = new System.Drawing.Point(0, 6);
            this.tvStock.Name = "tvStock";
            this.tvStock.Size = new System.Drawing.Size(789, 625);
            this.tvStock.TabIndex = 0;
            this.tvStock.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvStock_AfterExpand);
            this.tvStock.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvStock_MouseClick);
            // 
            // pnlbuttons
            // 
            this.pnlbuttons.Controls.Add(this.tbButtonGroup);
            this.pnlbuttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbuttons.Location = new System.Drawing.Point(0, 631);
            this.pnlbuttons.Name = "pnlbuttons";
            this.pnlbuttons.Size = new System.Drawing.Size(789, 47);
            this.pnlbuttons.TabIndex = 6;
            // 
            // tbButtonGroup
            // 
            this.tbButtonGroup.ColumnCount = 6;
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.29298F));
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.01073F));
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.71033F));
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.13032F));
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.09028F));
            this.tbButtonGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.76536F));
            this.tbButtonGroup.Controls.Add(this.btnDisplay, 0, 0);
            this.tbButtonGroup.Controls.Add(this.btnDepreciation, 1, 0);
            this.tbButtonGroup.Controls.Add(this.btnExAsset, 2, 0);
            this.tbButtonGroup.Controls.Add(this.btnImpAsset, 3, 0);
            this.tbButtonGroup.Controls.Add(this.btnClose, 5, 0);
            this.tbButtonGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbButtonGroup.Location = new System.Drawing.Point(0, 0);
            this.tbButtonGroup.Name = "tbButtonGroup";
            this.tbButtonGroup.RowCount = 1;
            this.tbButtonGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbButtonGroup.Size = new System.Drawing.Size(789, 47);
            this.tbButtonGroup.TabIndex = 0;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisplay.Location = new System.Drawing.Point(10, 5);
            this.btnDisplay.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(115, 35);
            this.btnDisplay.TabIndex = 0;
            this.btnDisplay.Text = "&Display";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnDepreciation
            // 
            this.btnDepreciation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDepreciation.Location = new System.Drawing.Point(138, 5);
            this.btnDepreciation.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.btnDepreciation.Name = "btnDepreciation";
            this.btnDepreciation.Size = new System.Drawing.Size(136, 35);
            this.btnDepreciation.TabIndex = 1;
            this.btnDepreciation.Text = "D&epreciation";
            this.btnDepreciation.UseVisualStyleBackColor = true;
            this.btnDepreciation.Click += new System.EventHandler(this.btnDepreciation_Click);
            // 
            // btnExAsset
            // 
            this.btnExAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExAsset.Location = new System.Drawing.Point(287, 5);
            this.btnExAsset.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.btnExAsset.Name = "btnExAsset";
            this.btnExAsset.Size = new System.Drawing.Size(134, 35);
            this.btnExAsset.TabIndex = 2;
            this.btnExAsset.Text = "Export &Asset details";
            this.btnExAsset.UseVisualStyleBackColor = true;
            this.btnExAsset.Visible = false;
            this.btnExAsset.Click += new System.EventHandler(this.btnExImpAsset_Click);
            // 
            // btnImpAsset
            // 
            this.btnImpAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImpAsset.Location = new System.Drawing.Point(434, 5);
            this.btnImpAsset.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.btnImpAsset.Name = "btnImpAsset";
            this.btnImpAsset.Size = new System.Drawing.Size(130, 35);
            this.btnImpAsset.TabIndex = 3;
            this.btnImpAsset.Text = "&Import Asset details";
            this.btnImpAsset.UseVisualStyleBackColor = true;
            this.btnImpAsset.Visible = false;
            this.btnImpAsset.Click += new System.EventHandler(this.btnImpAsset_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(703, 5);
            this.btnClose.Margin = new System.Windows.Forms.Padding(10, 5, 3, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmAssetManagement
            // 
            this.AcceptButton = this.btnDisplay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 678);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmAssetManagement";
            this.Text = "Asset Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAssetManagement_FormClosing);
            this.Load += new System.EventHandler(this.Asset_Management_Load);
            this.ctMenuAssetTree.ResumeLayout(false);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.gpView.ResumeLayout(false);
            this.gpView.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlbuttons.ResumeLayout(false);
            this.tbButtonGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctMenuAssetTree;
        private System.Windows.Forms.ToolStripMenuItem IDM_ADD_TYPE;
        private System.Windows.Forms.ToolStripMenuItem IDM_EDIT_TYPE;
        private System.Windows.Forms.ToolStripMenuItem IDM_BUY_MACHINE;
        private System.Windows.Forms.GroupBox gpView;
        private System.Windows.Forms.CheckBox chkDisplayInstallationDetails;
        private System.Windows.Forms.ComboBox cmbGroupStockBy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.CheckedListBox lstMachineAvailability;
        private System.Windows.Forms.Button btnAvailabilitySelectAll;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbRepresentative;
        private System.Windows.Forms.Button BtnEditUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbDepot;
        private System.Windows.Forms.Button btnEditDepot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Button btnEditSupplier;
        private System.Windows.Forms.Label label6;
        private BMC.Common.Utilities.BmcComboBox  cmbGameCategory;
        private System.Windows.Forms.Button btnGameCat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbModelType;
        private System.Windows.Forms.Button btnModelType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.Button btnEditManufacturer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMachineTypes;
        private System.Windows.Forms.Button btnEditTypes;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlbuttons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDepreciation;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.TreeView tvStock;
        private System.Windows.Forms.TableLayoutPanel tbButtonGroup;
        private System.Windows.Forms.ToolStripMenuItem IDM_EDIT_MACHINE;
        private System.Windows.Forms.ToolStripMenuItem IDM_TERMINATE_MACHINE;
        private System.Windows.Forms.ToolStripMenuItem IDM_ADD_MODEL_NGA;
        private System.Windows.Forms.ToolStripMenuItem IDM_EDIT_MODEL_NGA;
        private System.Windows.Forms.ToolStripMenuItem IDM_MODEL_ADMIN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAvailabilitySelectNone;
        private System.Windows.Forms.ToolStripMenuItem IDM_VIEW_TERMINATE_MACHINE;
        private System.Windows.Forms.ToolStripMenuItem IDM_SAVE_TEMPLATE;
        private System.Windows.Forms.ToolStripMenuItem IDM_EDIT_TEMPLATE;
        private System.Windows.Forms.ToolStripMenuItem IDM_DELETE_TEMPLATE;
        private System.Windows.Forms.Button btnExAsset;
        private System.Windows.Forms.Button btnImpAsset;
        private System.Windows.Forms.Label lblGame;
        private System.Windows.Forms.ComboBox cmbFilterbyGame;
    }
}