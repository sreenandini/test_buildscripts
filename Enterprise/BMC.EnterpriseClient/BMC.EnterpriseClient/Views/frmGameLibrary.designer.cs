namespace BMC.EnterpriseClient.Views
{
    partial class frmGameLibrary
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
            this.trv_GameCategory = new System.Windows.Forms.TreeView();
            this.grp_GameTitleDetails = new System.Windows.Forms.GroupBox();
            this.gpFilterBy = new System.Windows.Forms.GroupBox();
            this.tblFilterLayout = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_ManufacturerFilter = new System.Windows.Forms.Label();
            this.cboMachineManufaturer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_ManufacturerFilter = new System.Windows.Forms.ComboBox();
            this.btn_ManufacturerFilter = new System.Windows.Forms.Button();
            this.grf_FilterBy = new System.Windows.Forms.GroupBox();
            this.cmbGameName = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.lblGameName = new System.Windows.Forms.Label();
            this.cmb_GameCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lbl_GameCategoryFilter = new System.Windows.Forms.Label();
            this.btn_GameCategoryFilter = new System.Windows.Forms.Button();
            this.grp_GroupBy = new System.Windows.Forms.GroupBox();
            this.lbl_GroupBy = new System.Windows.Forms.Label();
            this.cmb_GroupBy = new System.Windows.Forms.ComboBox();
            this.grp_gameDetails = new System.Windows.Forms.GroupBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btn_MapGame = new System.Windows.Forms.Button();
            this.lvGameDetails = new System.Windows.Forms.ListView();
            this.clmGameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmManId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMachineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grp_PayTableDetails = new System.Windows.Forms.GroupBox();
            this.btn_EditPayTable = new System.Windows.Forms.Button();
            this.lvPayTable = new System.Windows.Forms.ListView();
            this.clmPayTabDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDenom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmPayoutPercentage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmPayTableBet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTheoPayoutPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnu_CategoryEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_EditGameTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editGameTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_EditManufacturer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editManufacturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameTitleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_AllCategory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_AllManufacturer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newManufacturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grp_GameTitleDetails.SuspendLayout();
            this.gpFilterBy.SuspendLayout();
            this.tblFilterLayout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grf_FilterBy.SuspendLayout();
            this.grp_GroupBy.SuspendLayout();
            this.grp_gameDetails.SuspendLayout();
            this.grp_PayTableDetails.SuspendLayout();
            this.mnu_CategoryEdit.SuspendLayout();
            this.mnu_EditGameTitle.SuspendLayout();
            this.mnu_EditManufacturer.SuspendLayout();
            this.mnu_AllCategory.SuspendLayout();
            this.mnu_AllManufacturer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // trv_GameCategory
            // 
            this.trv_GameCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trv_GameCategory.HideSelection = false;
            this.trv_GameCategory.Location = new System.Drawing.Point(3, 17);
            this.trv_GameCategory.Name = "trv_GameCategory";
            this.trv_GameCategory.Size = new System.Drawing.Size(299, 439);
            this.trv_GameCategory.TabIndex = 1;
            this.trv_GameCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_GameCategory_AfterSelect);
            this.trv_GameCategory.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trv_GameCategory_NodeMouseClick);
            // 
            // grp_GameTitleDetails
            // 
            this.grp_GameTitleDetails.AutoSize = true;
            this.grp_GameTitleDetails.Controls.Add(this.gpFilterBy);
            this.grp_GameTitleDetails.Controls.Add(this.grp_GroupBy);
            this.grp_GameTitleDetails.Controls.Add(this.trv_GameCategory);
            this.grp_GameTitleDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_GameTitleDetails.Location = new System.Drawing.Point(3, 3);
            this.grp_GameTitleDetails.Name = "grp_GameTitleDetails";
            this.grp_GameTitleDetails.Size = new System.Drawing.Size(305, 701);
            this.grp_GameTitleDetails.TabIndex = 1;
            this.grp_GameTitleDetails.TabStop = false;
            this.grp_GameTitleDetails.Text = "Game Title Details";
            // 
            // gpFilterBy
            // 
            this.gpFilterBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpFilterBy.Controls.Add(this.tblFilterLayout);
            this.gpFilterBy.Location = new System.Drawing.Point(3, 462);
            this.gpFilterBy.Name = "gpFilterBy";
            this.gpFilterBy.Size = new System.Drawing.Size(299, 178);
            this.gpFilterBy.TabIndex = 0;
            this.gpFilterBy.TabStop = false;
            this.gpFilterBy.Text = "Filter By";
            // 
            // tblFilterLayout
            // 
            this.tblFilterLayout.ColumnCount = 1;
            this.tblFilterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilterLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFilterLayout.Controls.Add(this.groupBox1, 0, 1);
            this.tblFilterLayout.Controls.Add(this.grf_FilterBy, 0, 0);
            this.tblFilterLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilterLayout.Location = new System.Drawing.Point(3, 17);
            this.tblFilterLayout.Name = "tblFilterLayout";
            this.tblFilterLayout.RowCount = 2;
            this.tblFilterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterLayout.Size = new System.Drawing.Size(293, 158);
            this.tblFilterLayout.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_ManufacturerFilter);
            this.groupBox1.Controls.Add(this.cboMachineManufaturer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmb_ManufacturerFilter);
            this.groupBox1.Controls.Add(this.btn_ManufacturerFilter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 73);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lbl_ManufacturerFilter
            // 
            this.lbl_ManufacturerFilter.Location = new System.Drawing.Point(3, 10);
            this.lbl_ManufacturerFilter.Name = "lbl_ManufacturerFilter";
            this.lbl_ManufacturerFilter.Size = new System.Drawing.Size(87, 32);
            this.lbl_ManufacturerFilter.TabIndex = 1;
            this.lbl_ManufacturerFilter.Text = "Game Manufacturer:";
            // 
            // cboMachineManufaturer
            // 
            this.cboMachineManufaturer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMachineManufaturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachineManufaturer.FormattingEnabled = true;
            this.cboMachineManufaturer.Location = new System.Drawing.Point(104, 48);
            this.cboMachineManufaturer.Name = "cboMachineManufaturer";
            this.cboMachineManufaturer.Size = new System.Drawing.Size(139, 21);
            this.cboMachineManufaturer.TabIndex = 3;
            this.cboMachineManufaturer.SelectedIndexChanged += new System.EventHandler(this.cmb_ManufacturerFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "M/C Manufacturer:";
            // 
            // cmb_ManufacturerFilter
            // 
            this.cmb_ManufacturerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_ManufacturerFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ManufacturerFilter.FormattingEnabled = true;
            this.cmb_ManufacturerFilter.Location = new System.Drawing.Point(104, 16);
            this.cmb_ManufacturerFilter.Name = "cmb_ManufacturerFilter";
            this.cmb_ManufacturerFilter.Size = new System.Drawing.Size(137, 21);
            this.cmb_ManufacturerFilter.TabIndex = 1;
            this.cmb_ManufacturerFilter.SelectedIndexChanged += new System.EventHandler(this.cmb_ManufacturerFilter_SelectedIndexChanged_1);
            this.cmb_ManufacturerFilter.MouseHover += new System.EventHandler(this.cmb_ManufacturerFilter_MouseHover);
            // 
            // btn_ManufacturerFilter
            // 
            this.btn_ManufacturerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ManufacturerFilter.Location = new System.Drawing.Point(249, 16);
            this.btn_ManufacturerFilter.Name = "btn_ManufacturerFilter";
            this.btn_ManufacturerFilter.Size = new System.Drawing.Size(34, 23);
            this.btn_ManufacturerFilter.TabIndex = 0;
            this.btn_ManufacturerFilter.Text = "...";
            this.btn_ManufacturerFilter.UseVisualStyleBackColor = true;
            this.btn_ManufacturerFilter.Click += new System.EventHandler(this.btn_ManufacturerFilter_Click);
            // 
            // grf_FilterBy
            // 
            this.grf_FilterBy.Controls.Add(this.cmbGameName);
            this.grf_FilterBy.Controls.Add(this.lblGameName);
            this.grf_FilterBy.Controls.Add(this.cmb_GameCategoryFilter);
            this.grf_FilterBy.Controls.Add(this.lbl_GameCategoryFilter);
            this.grf_FilterBy.Controls.Add(this.btn_GameCategoryFilter);
            this.grf_FilterBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grf_FilterBy.Location = new System.Drawing.Point(3, 3);
            this.grf_FilterBy.Name = "grf_FilterBy";
            this.grf_FilterBy.Size = new System.Drawing.Size(287, 73);
            this.grf_FilterBy.TabIndex = 1;
            this.grf_FilterBy.TabStop = false;
            // 
            // cmbGameName
            // 
            this.cmbGameName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGameName.BackColor = System.Drawing.SystemColors.Window;
            this.cmbGameName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGameName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGameName.FormattingEnabled = true;
            this.cmbGameName.ItemHeight = 17;
            this.cmbGameName.Location = new System.Drawing.Point(108, 20);
            this.cmbGameName.Name = "cmbGameName";
            this.cmbGameName.Size = new System.Drawing.Size(135, 23);
            this.cmbGameName.TabIndex = 1;
            this.cmbGameName.SelectedIndexChanged += new System.EventHandler(this.cmbGameName_SelectedIndexChanged);
            this.cmbGameName.MouseHover += new System.EventHandler(this.cmbGameName_MouseHover);
            // 
            // lblGameName
            // 
            this.lblGameName.AutoSize = true;
            this.lblGameName.Location = new System.Drawing.Point(8, 23);
            this.lblGameName.Name = "lblGameName";
            this.lblGameName.Size = new System.Drawing.Size(87, 13);
            this.lblGameName.TabIndex = 1;
            this.lblGameName.Text = "Game Name :";
            // 
            // cmb_GameCategoryFilter
            // 
            this.cmb_GameCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_GameCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GameCategoryFilter.FormattingEnabled = true;
            this.cmb_GameCategoryFilter.Location = new System.Drawing.Point(108, 52);
            this.cmb_GameCategoryFilter.Name = "cmb_GameCategoryFilter";
            this.cmb_GameCategoryFilter.Size = new System.Drawing.Size(135, 21);
            this.cmb_GameCategoryFilter.TabIndex = 3;
            this.cmb_GameCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmb_GameCategoryFilter_SelectedIndexChanged);
            this.cmb_GameCategoryFilter.MouseHover += new System.EventHandler(this.cmb_GameCategoryFilter_MouseHover);
            // 
            // lbl_GameCategoryFilter
            // 
            this.lbl_GameCategoryFilter.AutoSize = true;
            this.lbl_GameCategoryFilter.Location = new System.Drawing.Point(8, 55);
            this.lbl_GameCategoryFilter.Name = "lbl_GameCategoryFilter";
            this.lbl_GameCategoryFilter.Size = new System.Drawing.Size(103, 13);
            this.lbl_GameCategoryFilter.TabIndex = 0;
            this.lbl_GameCategoryFilter.Text = "Game Category:";
            // 
            // btn_GameCategoryFilter
            // 
            this.btn_GameCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GameCategoryFilter.Location = new System.Drawing.Point(249, 50);
            this.btn_GameCategoryFilter.Name = "btn_GameCategoryFilter";
            this.btn_GameCategoryFilter.Size = new System.Drawing.Size(34, 23);
            this.btn_GameCategoryFilter.TabIndex = 4;
            this.btn_GameCategoryFilter.Text = "...";
            this.btn_GameCategoryFilter.UseVisualStyleBackColor = true;
            this.btn_GameCategoryFilter.Click += new System.EventHandler(this.btn_GameCategoryFilter_Click);
            // 
            // grp_GroupBy
            // 
            this.grp_GroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_GroupBy.Controls.Add(this.lbl_GroupBy);
            this.grp_GroupBy.Controls.Add(this.cmb_GroupBy);
            this.grp_GroupBy.Location = new System.Drawing.Point(9, 646);
            this.grp_GroupBy.Name = "grp_GroupBy";
            this.grp_GroupBy.Size = new System.Drawing.Size(283, 55);
            this.grp_GroupBy.TabIndex = 2;
            this.grp_GroupBy.TabStop = false;
            this.grp_GroupBy.Text = "Group By";
            // 
            // lbl_GroupBy
            // 
            this.lbl_GroupBy.AutoSize = true;
            this.lbl_GroupBy.Location = new System.Drawing.Point(8, 28);
            this.lbl_GroupBy.Name = "lbl_GroupBy";
            this.lbl_GroupBy.Size = new System.Drawing.Size(55, 13);
            this.lbl_GroupBy.TabIndex = 1;
            this.lbl_GroupBy.Text = "Options:";
            // 
            // cmb_GroupBy
            // 
            this.cmb_GroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_GroupBy.FormattingEnabled = true;
            this.cmb_GroupBy.Location = new System.Drawing.Point(101, 25);
            this.cmb_GroupBy.Name = "cmb_GroupBy";
            this.cmb_GroupBy.Size = new System.Drawing.Size(173, 21);
            this.cmb_GroupBy.TabIndex = 1;
            this.cmb_GroupBy.SelectedIndexChanged += new System.EventHandler(this.cmb_GroupBy_SelectedIndexChanged);
            this.cmb_GroupBy.MouseHover += new System.EventHandler(this.cmb_GroupBy_MouseHover);
            // 
            // grp_gameDetails
            // 
            this.grp_gameDetails.AutoSize = true;
            this.grp_gameDetails.Controls.Add(this.btnLoad);
            this.grp_gameDetails.Controls.Add(this.btn_MapGame);
            this.grp_gameDetails.Controls.Add(this.lvGameDetails);
            this.grp_gameDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_gameDetails.Location = new System.Drawing.Point(314, 3);
            this.grp_gameDetails.Name = "grp_gameDetails";
            this.grp_gameDetails.Size = new System.Drawing.Size(381, 701);
            this.grp_gameDetails.TabIndex = 0;
            this.grp_gameDetails.TabStop = false;
            this.grp_gameDetails.Text = "M/C Protocol- Game Details";
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnLoad.Location = new System.Drawing.Point(50, 647);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(107, 30);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btn_MapGame
            // 
            this.btn_MapGame.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_MapGame.Location = new System.Drawing.Point(223, 647);
            this.btn_MapGame.Name = "btn_MapGame";
            this.btn_MapGame.Size = new System.Drawing.Size(107, 30);
            this.btn_MapGame.TabIndex = 2;
            this.btn_MapGame.Text = "Map Game";
            this.btn_MapGame.UseVisualStyleBackColor = true;
            this.btn_MapGame.Click += new System.EventHandler(this.btn_MapGame_Click);
            // 
            // lvGameDetails
            // 
            this.lvGameDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvGameDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmGameName,
            this.clmManId,
            this.clmMachineName});
            this.lvGameDetails.FullRowSelect = true;
            this.lvGameDetails.GridLines = true;
            this.lvGameDetails.HideSelection = false;
            this.lvGameDetails.Location = new System.Drawing.Point(0, 17);
            this.lvGameDetails.MultiSelect = false;
            this.lvGameDetails.Name = "lvGameDetails";
            this.lvGameDetails.Size = new System.Drawing.Size(381, 607);
            this.lvGameDetails.TabIndex = 1;
            this.lvGameDetails.UseCompatibleStateImageBehavior = false;
            this.lvGameDetails.View = System.Windows.Forms.View.Details;
            this.lvGameDetails.SelectedIndexChanged += new System.EventHandler(this.lvGameDetails_SelectedIndexChanged);
            // 
            // clmGameName
            // 
            this.clmGameName.Tag = "clmGameName";
            this.clmGameName.Text = "Game Name";
            this.clmGameName.Width = 142;
            // 
            // clmManId
            // 
            this.clmManId.Tag = "clmManId";
            this.clmManId.Text = "Game Manufacturer";
            this.clmManId.Width = 120;
            // 
            // clmMachineName
            // 
            this.clmMachineName.Text = "Machine Name";
            this.clmMachineName.Width = 160;
            // 
            // grp_PayTableDetails
            // 
            this.grp_PayTableDetails.AutoSize = true;
            this.grp_PayTableDetails.Controls.Add(this.btn_EditPayTable);
            this.grp_PayTableDetails.Controls.Add(this.lvPayTable);
            this.grp_PayTableDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_PayTableDetails.Location = new System.Drawing.Point(701, 3);
            this.grp_PayTableDetails.Name = "grp_PayTableDetails";
            this.grp_PayTableDetails.Size = new System.Drawing.Size(371, 701);
            this.grp_PayTableDetails.TabIndex = 2;
            this.grp_PayTableDetails.TabStop = false;
            this.grp_PayTableDetails.Text = "M/C Protocol- Pay Table Details";
            // 
            // btn_EditPayTable
            // 
            this.btn_EditPayTable.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_EditPayTable.Location = new System.Drawing.Point(134, 646);
            this.btn_EditPayTable.Name = "btn_EditPayTable";
            this.btn_EditPayTable.Size = new System.Drawing.Size(107, 30);
            this.btn_EditPayTable.TabIndex = 1;
            this.btn_EditPayTable.Text = "Edit Pay Table";
            this.btn_EditPayTable.UseVisualStyleBackColor = true;
            this.btn_EditPayTable.Click += new System.EventHandler(this.btn_EditPayTable_Click);
            // 
            // lvPayTable
            // 
            this.lvPayTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPayTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmPayTabDesc,
            this.clmDenom,
            this.clmPayoutPercentage,
            this.clmPayTableBet,
            this.clmTheoPayoutPercent});
            this.lvPayTable.FullRowSelect = true;
            this.lvPayTable.GridLines = true;
            this.lvPayTable.HideSelection = false;
            this.lvPayTable.Location = new System.Drawing.Point(3, 17);
            this.lvPayTable.Name = "lvPayTable";
            this.lvPayTable.Size = new System.Drawing.Size(365, 607);
            this.lvPayTable.TabIndex = 1;
            this.lvPayTable.UseCompatibleStateImageBehavior = false;
            this.lvPayTable.View = System.Windows.Forms.View.Details;
            // 
            // clmPayTabDesc
            // 
            this.clmPayTabDesc.Tag = "clmPayTabDesc";
            this.clmPayTabDesc.Text = "Pay Table Description";
            this.clmPayTabDesc.Width = 165;
            // 
            // clmDenom
            // 
            this.clmDenom.Tag = "clmDenom";
            this.clmDenom.Text = "Denom";
            // 
            // clmPayoutPercentage
            // 
            this.clmPayoutPercentage.Tag = "clmPayoutPercentage";
            this.clmPayoutPercentage.Text = "Payout %";
            this.clmPayoutPercentage.Width = 65;
            // 
            // clmPayTableBet
            // 
            this.clmPayTableBet.Tag = "clmPayTableBet";
            this.clmPayTableBet.Text = "Pay Table Bet";
            this.clmPayTableBet.Width = 85;
            // 
            // clmTheoPayoutPercent
            // 
            this.clmTheoPayoutPercent.Tag = "clmTheoPayoutPercent";
            this.clmTheoPayoutPercent.Text = "Theo Payout %";
            this.clmTheoPayoutPercent.Width = 90;
            // 
            // mnu_CategoryEdit
            // 
            this.mnu_CategoryEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editCategoryToolStripMenuItem,
            this.newGameTitleToolStripMenuItem});
            this.mnu_CategoryEdit.Name = "mnu_CategoryEdit";
            this.mnu_CategoryEdit.Size = new System.Drawing.Size(159, 48);
            // 
            // editCategoryToolStripMenuItem
            // 
            this.editCategoryToolStripMenuItem.Name = "editCategoryToolStripMenuItem";
            this.editCategoryToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editCategoryToolStripMenuItem.Text = "Edit Category";
            this.editCategoryToolStripMenuItem.Click += new System.EventHandler(this.editCategoryToolStripMenuItem_Click);
            // 
            // newGameTitleToolStripMenuItem
            // 
            this.newGameTitleToolStripMenuItem.Name = "newGameTitleToolStripMenuItem";
            this.newGameTitleToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.newGameTitleToolStripMenuItem.Text = "New Game Title";
            this.newGameTitleToolStripMenuItem.Click += new System.EventHandler(this.newGameTitleToolStripMenuItem_Click);
            // 
            // mnu_EditGameTitle
            // 
            this.mnu_EditGameTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editGameTitleToolStripMenuItem,
            this.mapGameToolStripMenuItem});
            this.mnu_EditGameTitle.Name = "mnu_EditGameTitle";
            this.mnu_EditGameTitle.Size = new System.Drawing.Size(155, 48);
            // 
            // editGameTitleToolStripMenuItem
            // 
            this.editGameTitleToolStripMenuItem.Name = "editGameTitleToolStripMenuItem";
            this.editGameTitleToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.editGameTitleToolStripMenuItem.Text = "Edit Game Title";
            this.editGameTitleToolStripMenuItem.Click += new System.EventHandler(this.editGameTitleToolStripMenuItem_Click);
            // 
            // mapGameToolStripMenuItem
            // 
            this.mapGameToolStripMenuItem.Name = "mapGameToolStripMenuItem";
            this.mapGameToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.mapGameToolStripMenuItem.Text = "Map Game";
            this.mapGameToolStripMenuItem.Click += new System.EventHandler(this.mapGameToolStripMenuItem_Click);
            // 
            // mnu_EditManufacturer
            // 
            this.mnu_EditManufacturer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editManufacturerToolStripMenuItem,
            this.newGameTitleToolStripMenuItem1});
            this.mnu_EditManufacturer.Name = "mnu_EditManufacturer";
            this.mnu_EditManufacturer.Size = new System.Drawing.Size(170, 48);
            // 
            // editManufacturerToolStripMenuItem
            // 
            this.editManufacturerToolStripMenuItem.Name = "editManufacturerToolStripMenuItem";
            this.editManufacturerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.editManufacturerToolStripMenuItem.Text = "Edit Manufacturer";
            this.editManufacturerToolStripMenuItem.Click += new System.EventHandler(this.editManufacturerToolStripMenuItem_Click);
            // 
            // newGameTitleToolStripMenuItem1
            // 
            this.newGameTitleToolStripMenuItem1.Name = "newGameTitleToolStripMenuItem1";
            this.newGameTitleToolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.newGameTitleToolStripMenuItem1.Text = "New Game Title";
            this.newGameTitleToolStripMenuItem1.Click += new System.EventHandler(this.newGameTitleToolStripMenuItem_Click);
            // 
            // mnu_AllCategory
            // 
            this.mnu_AllCategory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCategoryToolStripMenuItem});
            this.mnu_AllCategory.Name = "mnu_AllCategory";
            this.mnu_AllCategory.Size = new System.Drawing.Size(150, 26);
            // 
            // newCategoryToolStripMenuItem
            // 
            this.newCategoryToolStripMenuItem.Name = "newCategoryToolStripMenuItem";
            this.newCategoryToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.newCategoryToolStripMenuItem.Text = "New Category";
            this.newCategoryToolStripMenuItem.Click += new System.EventHandler(this.newCategoryToolStripMenuItem_Click);
            // 
            // mnu_AllManufacturer
            // 
            this.mnu_AllManufacturer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newManufacturerToolStripMenuItem});
            this.mnu_AllManufacturer.Name = "mnu_AllManufacturer";
            this.mnu_AllManufacturer.Size = new System.Drawing.Size(174, 26);
            // 
            // newManufacturerToolStripMenuItem
            // 
            this.newManufacturerToolStripMenuItem.Name = "newManufacturerToolStripMenuItem";
            this.newManufacturerToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.newManufacturerToolStripMenuItem.Text = "New Manufacturer";
            this.newManufacturerToolStripMenuItem.Click += new System.EventHandler(this.newManufacturerToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.grp_GameTitleDetails, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grp_gameDetails, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grp_PayTableDetails, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1075, 707);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmGameLibrary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 707);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmGameLibrary";
            this.Text = "Game Library";
            this.Load += new System.EventHandler(this.GameLibraryForm_Load);
            this.grp_GameTitleDetails.ResumeLayout(false);
            this.gpFilterBy.ResumeLayout(false);
            this.tblFilterLayout.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grf_FilterBy.ResumeLayout(false);
            this.grf_FilterBy.PerformLayout();
            this.grp_GroupBy.ResumeLayout(false);
            this.grp_GroupBy.PerformLayout();
            this.grp_gameDetails.ResumeLayout(false);
            this.grp_PayTableDetails.ResumeLayout(false);
            this.mnu_CategoryEdit.ResumeLayout(false);
            this.mnu_EditGameTitle.ResumeLayout(false);
            this.mnu_EditManufacturer.ResumeLayout(false);
            this.mnu_AllCategory.ResumeLayout(false);
            this.mnu_AllManufacturer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trv_GameCategory;
        private System.Windows.Forms.GroupBox grp_GameTitleDetails;
        private System.Windows.Forms.GroupBox grp_gameDetails;
        private System.Windows.Forms.GroupBox grp_PayTableDetails;
        private System.Windows.Forms.ListView lvGameDetails;
        private System.Windows.Forms.ListView lvPayTable;
        private System.Windows.Forms.Button btn_MapGame;
        private System.Windows.Forms.Button btn_EditPayTable;
        private System.Windows.Forms.ContextMenuStrip mnu_CategoryEdit;
        private System.Windows.Forms.ToolStripMenuItem editCategoryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnu_EditGameTitle;
        private System.Windows.Forms.ContextMenuStrip mnu_EditManufacturer;
        private System.Windows.Forms.ToolStripMenuItem newGameTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editGameTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editManufacturerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameTitleToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip mnu_AllCategory;
        private System.Windows.Forms.ToolStripMenuItem newCategoryToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnu_AllManufacturer;
        private System.Windows.Forms.ToolStripMenuItem newManufacturerToolStripMenuItem;
        private System.Windows.Forms.GroupBox grp_GroupBy;
        private System.Windows.Forms.Label lbl_GroupBy;
        private System.Windows.Forms.ComboBox cmb_GroupBy;
        private System.Windows.Forms.GroupBox grf_FilterBy;
        private System.Windows.Forms.Label lbl_ManufacturerFilter;
        private System.Windows.Forms.Label lbl_GameCategoryFilter;
        private System.Windows.Forms.Button btn_ManufacturerFilter;
        private System.Windows.Forms.Button btn_GameCategoryFilter;
        private System.Windows.Forms.ComboBox cmb_ManufacturerFilter;
        private System.Windows.Forms.ComboBox cmb_GameCategoryFilter;
        private System.Windows.Forms.ColumnHeader clmGameName;
        private System.Windows.Forms.ColumnHeader clmManId;
        private System.Windows.Forms.ColumnHeader clmPayTabDesc;
        private System.Windows.Forms.ColumnHeader clmPayoutPercentage;
        private System.Windows.Forms.ColumnHeader clmPayTableBet;
        private System.Windows.Forms.ColumnHeader clmTheoPayoutPercent;
        private System.Windows.Forms.ColumnHeader clmMachineName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblGameName;
        private System.Windows.Forms.ComboBox cboMachineManufaturer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gpFilterBy;
        private System.Windows.Forms.TableLayoutPanel tblFilterLayout;
        private Helpers.BmcComboBox cmbGameName;
        private System.Windows.Forms.ColumnHeader clmDenom;
        private System.Windows.Forms.Button btnLoad;
    }
}