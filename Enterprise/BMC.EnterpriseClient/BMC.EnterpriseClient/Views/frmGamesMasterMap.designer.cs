namespace BMC.EnterpriseClient.Views
{
    partial class frmGamesMasterMap
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
            this.grpMapUnMapGames = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSrcUnMapGame = new System.Windows.Forms.TextBox();
            this.lblGameManufacturer = new System.Windows.Forms.Label();
            this.lblGameCategory = new System.Windows.Forms.Label();
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.txtGameCategory = new System.Windows.Forms.TextBox();
            this.txtGameManufacturer = new System.Windows.Forms.TextBox();
            this.cmbGameTitle = new System.Windows.Forms.ComboBox();
            this.grpMappedGameInfo = new System.Windows.Forms.GroupBox();
            this.lvMappedGameInfo = new System.Windows.Forms.ListView();
            this.clmUnMapInfoSiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUnMapInfoPositionNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUnMapInfoAssetNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUMapInfoMacSerialNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUnMapInfoGamePartNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmUnMapInfoIsGameActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpUnmappedInfo = new System.Windows.Forms.GroupBox();
            this.lvUnmappedInfo = new System.Windows.Forms.ListView();
            this.clmMapInfoSiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMapInfoPositionNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMapInfoAssetNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMapInfoMacSerialNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMapInfoGamePartNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMapInfoIsGameActive = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblGameName = new System.Windows.Forms.Label();
            this.lblUnmapped = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.lvMapped = new System.Windows.Forms.ListView();
            this.clmUnMapGameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmManufactur = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMachine_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvUnmapped = new System.Windows.Forms.ListView();
            this.clmGameName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmManufacture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMachineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpMapUnMapGames.SuspendLayout();
            this.grpMappedGameInfo.SuspendLayout();
            this.grpUnmappedInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMapUnMapGames
            // 
            this.grpMapUnMapGames.Controls.Add(this.btnClear);
            this.grpMapUnMapGames.Controls.Add(this.btnSearch);
            this.grpMapUnMapGames.Controls.Add(this.txtSrcUnMapGame);
            this.grpMapUnMapGames.Controls.Add(this.lblGameManufacturer);
            this.grpMapUnMapGames.Controls.Add(this.lblGameCategory);
            this.grpMapUnMapGames.Controls.Add(this.lblGameTitle);
            this.grpMapUnMapGames.Controls.Add(this.txtGameCategory);
            this.grpMapUnMapGames.Controls.Add(this.txtGameManufacturer);
            this.grpMapUnMapGames.Controls.Add(this.cmbGameTitle);
            this.grpMapUnMapGames.Controls.Add(this.grpMappedGameInfo);
            this.grpMapUnMapGames.Controls.Add(this.grpUnmappedInfo);
            this.grpMapUnMapGames.Controls.Add(this.lblGameName);
            this.grpMapUnMapGames.Controls.Add(this.lblUnmapped);
            this.grpMapUnMapGames.Controls.Add(this.btnRemove);
            this.grpMapUnMapGames.Controls.Add(this.btnAssign);
            this.grpMapUnMapGames.Controls.Add(this.lvMapped);
            this.grpMapUnMapGames.Controls.Add(this.lvUnmapped);
            this.grpMapUnMapGames.Location = new System.Drawing.Point(7, -1);
            this.grpMapUnMapGames.Name = "grpMapUnMapGames";
            this.grpMapUnMapGames.Size = new System.Drawing.Size(890, 802);
            this.grpMapUnMapGames.TabIndex = 0;
            this.grpMapUnMapGames.TabStop = false;
            this.grpMapUnMapGames.Text = "Mapped Unmapped Games";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(359, 85);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(57, 21);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(293, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 21);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSrcUnMapGame
            // 
            this.txtSrcUnMapGame.ForeColor = System.Drawing.Color.DimGray;
            this.txtSrcUnMapGame.Location = new System.Drawing.Point(6, 85);
            this.txtSrcUnMapGame.Name = "txtSrcUnMapGame";
            this.txtSrcUnMapGame.Size = new System.Drawing.Size(278, 21);
            this.txtSrcUnMapGame.TabIndex = 7;
            this.txtSrcUnMapGame.Text = "Search Unmapped M/c Protocol Games";
            this.txtSrcUnMapGame.Enter += new System.EventHandler(this.txtSrcUnMapGame_Enter);
            this.txtSrcUnMapGame.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSrcUnMapGame_KeyPress);
            this.txtSrcUnMapGame.Leave += new System.EventHandler(this.txtSrcUnMapGame_Leave);
            // 
            // lblGameManufacturer
            // 
            this.lblGameManufacturer.AutoSize = true;
            this.lblGameManufacturer.Location = new System.Drawing.Point(606, 16);
            this.lblGameManufacturer.Name = "lblGameManufacturer";
            this.lblGameManufacturer.Size = new System.Drawing.Size(120, 13);
            this.lblGameManufacturer.TabIndex = 4;
            this.lblGameManufacturer.Text = "Game Manufacturer";
            // 
            // lblGameCategory
            // 
            this.lblGameCategory.AutoSize = true;
            this.lblGameCategory.Location = new System.Drawing.Point(353, 17);
            this.lblGameCategory.Name = "lblGameCategory";
            this.lblGameCategory.Size = new System.Drawing.Size(98, 13);
            this.lblGameCategory.TabIndex = 2;
            this.lblGameCategory.Text = "Game Category";
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.AutoSize = true;
            this.lblGameTitle.Location = new System.Drawing.Point(3, 17);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(69, 13);
            this.lblGameTitle.TabIndex = 0;
            this.lblGameTitle.Text = "Game Title";
            // 
            // txtGameCategory
            // 
            this.txtGameCategory.Location = new System.Drawing.Point(356, 32);
            this.txtGameCategory.Name = "txtGameCategory";
            this.txtGameCategory.ReadOnly = true;
            this.txtGameCategory.Size = new System.Drawing.Size(247, 21);
            this.txtGameCategory.TabIndex = 3;
            // 
            // txtGameManufacturer
            // 
            this.txtGameManufacturer.Location = new System.Drawing.Point(609, 32);
            this.txtGameManufacturer.Name = "txtGameManufacturer";
            this.txtGameManufacturer.ReadOnly = true;
            this.txtGameManufacturer.Size = new System.Drawing.Size(275, 21);
            this.txtGameManufacturer.TabIndex = 5;
            // 
            // cmbGameTitle
            // 
            this.cmbGameTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGameTitle.FormattingEnabled = true;
            this.cmbGameTitle.Location = new System.Drawing.Point(6, 32);
            this.cmbGameTitle.Name = "cmbGameTitle";
            this.cmbGameTitle.Size = new System.Drawing.Size(341, 21);
            this.cmbGameTitle.TabIndex = 1;
            this.cmbGameTitle.SelectedIndexChanged += new System.EventHandler(this.cmbGameTitle_SelectedIndexChanged);
            // 
            // grpMappedGameInfo
            // 
            this.grpMappedGameInfo.Controls.Add(this.lvMappedGameInfo);
            this.grpMappedGameInfo.Location = new System.Drawing.Point(468, 493);
            this.grpMappedGameInfo.Name = "grpMappedGameInfo";
            this.grpMappedGameInfo.Size = new System.Drawing.Size(413, 301);
            this.grpMappedGameInfo.TabIndex = 16;
            this.grpMappedGameInfo.TabStop = false;
            this.grpMappedGameInfo.Text = "Mapped M/c Protocol Game Information";
            // 
            // lvMappedGameInfo
            // 
            this.lvMappedGameInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmUnMapInfoSiteName,
            this.clmUnMapInfoPositionNo,
            this.clmUnMapInfoAssetNo,
            this.clmUMapInfoMacSerialNo,
            this.clmUnMapInfoGamePartNo,
            this.clmUnMapInfoIsGameActive});
            this.lvMappedGameInfo.FullRowSelect = true;
            this.lvMappedGameInfo.GridLines = true;
            this.lvMappedGameInfo.Location = new System.Drawing.Point(7, 15);
            this.lvMappedGameInfo.Name = "lvMappedGameInfo";
            this.lvMappedGameInfo.Size = new System.Drawing.Size(400, 286);
            this.lvMappedGameInfo.TabIndex = 0;
            this.lvMappedGameInfo.UseCompatibleStateImageBehavior = false;
            this.lvMappedGameInfo.View = System.Windows.Forms.View.Details;
            // 
            // clmUnMapInfoSiteName
            // 
            this.clmUnMapInfoSiteName.Tag = "clmUnMapInfoSiteName";
            this.clmUnMapInfoSiteName.Text = "Site Name";
            this.clmUnMapInfoSiteName.Width = 95;
            // 
            // clmUnMapInfoPositionNo
            // 
            this.clmUnMapInfoPositionNo.Tag = "clmUnMapInfoPositionNo";
            this.clmUnMapInfoPositionNo.Text = "Position No";
            this.clmUnMapInfoPositionNo.Width = 80;
            // 
            // clmUnMapInfoAssetNo
            // 
            this.clmUnMapInfoAssetNo.Tag = "clmUnMapInfoAssetNo";
            this.clmUnMapInfoAssetNo.Text = "Asset No";
            this.clmUnMapInfoAssetNo.Width = 80;
            // 
            // clmUMapInfoMacSerialNo
            // 
            this.clmUMapInfoMacSerialNo.Tag = "clmUMapInfoMacSerialNo";
            this.clmUMapInfoMacSerialNo.Text = "M/C SerialNo";
            this.clmUMapInfoMacSerialNo.Width = 100;
            // 
            // clmUnMapInfoGamePartNo
            // 
            this.clmUnMapInfoGamePartNo.Tag = "clmUnMapInfoGamePartNo";
            this.clmUnMapInfoGamePartNo.Text = "Game Part Number";
            this.clmUnMapInfoGamePartNo.Width = 85;
            // 
            // clmUnMapInfoIsGameActive
            // 
            this.clmUnMapInfoIsGameActive.Tag = "clmUnMapInfoIsGameActive";
            this.clmUnMapInfoIsGameActive.Text = "IsGameActive";
            this.clmUnMapInfoIsGameActive.Width = 85;
            // 
            // grpUnmappedInfo
            // 
            this.grpUnmappedInfo.Controls.Add(this.lvUnmappedInfo);
            this.grpUnmappedInfo.Location = new System.Drawing.Point(9, 493);
            this.grpUnmappedInfo.Name = "grpUnmappedInfo";
            this.grpUnmappedInfo.Size = new System.Drawing.Size(413, 301);
            this.grpUnmappedInfo.TabIndex = 15;
            this.grpUnmappedInfo.TabStop = false;
            this.grpUnmappedInfo.Text = "UnMapped M/c Protocol Game Information";
            // 
            // lvUnmappedInfo
            // 
            this.lvUnmappedInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmMapInfoSiteName,
            this.clmMapInfoPositionNo,
            this.clmMapInfoAssetNo,
            this.clmMapInfoMacSerialNo,
            this.clmMapInfoGamePartNo,
            this.clmMapInfoIsGameActive});
            this.lvUnmappedInfo.FullRowSelect = true;
            this.lvUnmappedInfo.GridLines = true;
            this.lvUnmappedInfo.Location = new System.Drawing.Point(7, 20);
            this.lvUnmappedInfo.Name = "lvUnmappedInfo";
            this.lvUnmappedInfo.Size = new System.Drawing.Size(400, 281);
            this.lvUnmappedInfo.TabIndex = 0;
            this.lvUnmappedInfo.UseCompatibleStateImageBehavior = false;
            this.lvUnmappedInfo.View = System.Windows.Forms.View.Details;
            // 
            // clmMapInfoSiteName
            // 
            this.clmMapInfoSiteName.Tag = "clmMapInfoSiteName";
            this.clmMapInfoSiteName.Text = "Site Name";
            this.clmMapInfoSiteName.Width = 95;
            // 
            // clmMapInfoPositionNo
            // 
            this.clmMapInfoPositionNo.Tag = "clmMapInfoPositionNo";
            this.clmMapInfoPositionNo.Text = "Position No";
            this.clmMapInfoPositionNo.Width = 80;
            // 
            // clmMapInfoAssetNo
            // 
            this.clmMapInfoAssetNo.Tag = "clmMapInfoAssetNo";
            this.clmMapInfoAssetNo.Text = "Asset No";
            this.clmMapInfoAssetNo.Width = 80;
            // 
            // clmMapInfoMacSerialNo
            // 
            this.clmMapInfoMacSerialNo.Tag = "clmMapInfoMacSerialNo";
            this.clmMapInfoMacSerialNo.Text = "M/C Serial No";
            this.clmMapInfoMacSerialNo.Width = 100;
            // 
            // clmMapInfoGamePartNo
            // 
            this.clmMapInfoGamePartNo.Tag = "clmMapInfoGamePartNo";
            this.clmMapInfoGamePartNo.Text = "Game Part Number";
            this.clmMapInfoGamePartNo.Width = 85;
            // 
            // clmMapInfoIsGameActive
            // 
            this.clmMapInfoIsGameActive.Tag = "clmMapInfoIsGameActive";
            this.clmMapInfoIsGameActive.Text = "IsGameActive";
            this.clmMapInfoIsGameActive.Width = 85;
            // 
            // lblGameName
            // 
            this.lblGameName.AutoSize = true;
            this.lblGameName.Location = new System.Drawing.Point(472, 93);
            this.lblGameName.Name = "lblGameName";
            this.lblGameName.Size = new System.Drawing.Size(169, 13);
            this.lblGameName.TabIndex = 11;
            this.lblGameName.Text = "Mapped M/c Protocol Games";
            // 
            // lblUnmapped
            // 
            this.lblUnmapped.AutoSize = true;
            this.lblUnmapped.Location = new System.Drawing.Point(3, 69);
            this.lblUnmapped.Name = "lblUnmapped";
            this.lblUnmapped.Size = new System.Drawing.Size(186, 13);
            this.lblUnmapped.TabIndex = 6;
            this.lblUnmapped.Text = "Unmapped M/c Protocol Games";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(422, 308);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(47, 20);
            this.btnRemove.TabIndex = 14;
            this.btnRemove.Text = "<<<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAssign
            // 
            this.btnAssign.Location = new System.Drawing.Point(422, 246);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(46, 20);
            this.btnAssign.TabIndex = 13;
            this.btnAssign.Text = ">>>";
            this.btnAssign.UseVisualStyleBackColor = true;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // lvMapped
            // 
            this.lvMapped.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmUnMapGameName,
            this.clmManufactur,
            this.clmMachine_Name});
            this.lvMapped.FullRowSelect = true;
            this.lvMapped.GridLines = true;
            this.lvMapped.Location = new System.Drawing.Point(475, 112);
            this.lvMapped.Name = "lvMapped";
            this.lvMapped.Size = new System.Drawing.Size(406, 375);
            this.lvMapped.TabIndex = 12;
            this.lvMapped.UseCompatibleStateImageBehavior = false;
            this.lvMapped.View = System.Windows.Forms.View.Details;
            this.lvMapped.SelectedIndexChanged += new System.EventHandler(this.lvMapped_SelectedIndexChanged);
            // 
            // clmUnMapGameName
            // 
            this.clmUnMapGameName.Tag = "clmUnMapGameName";
            this.clmUnMapGameName.Text = "Game Name";
            this.clmUnMapGameName.Width = 142;
            // 
            // clmManufactur
            // 
            this.clmManufactur.Text = "Game Manufacturer";
            this.clmManufactur.Width = 151;
            // 
            // clmMachine_Name
            // 
            this.clmMachine_Name.Text = "Machine Name";
            this.clmMachine_Name.Width = 107;
            // 
            // lvUnmapped
            // 
            this.lvUnmapped.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmGameName,
            this.clmManufacture,
            this.clmMachineName});
            this.lvUnmapped.FullRowSelect = true;
            this.lvUnmapped.GridLines = true;
            this.lvUnmapped.Location = new System.Drawing.Point(6, 112);
            this.lvUnmapped.Name = "lvUnmapped";
            this.lvUnmapped.Size = new System.Drawing.Size(410, 375);
            this.lvUnmapped.TabIndex = 10;
            this.lvUnmapped.UseCompatibleStateImageBehavior = false;
            this.lvUnmapped.View = System.Windows.Forms.View.Details;
            this.lvUnmapped.SelectedIndexChanged += new System.EventHandler(this.lvUnmapped_SelectedIndexChanged);
            // 
            // clmGameName
            // 
            this.clmGameName.Tag = "clmMapGameName";
            this.clmGameName.Text = "Game Name";
            this.clmGameName.Width = 142;
            // 
            // clmManufacture
            // 
            this.clmManufacture.Text = "Game Manufacturer";
            this.clmManufacture.Width = 140;
            // 
            // clmMachineName
            // 
            this.clmMachineName.Text = "Machine Name";
            this.clmMachineName.Width = 122;
            // 
            // frmGamesMasterMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(904, 809);
            this.Controls.Add(this.grpMapUnMapGames);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGamesMasterMap";
            this.Text = "Game Master Mapping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGamesMasterMap_FormClosing);
            this.Load += new System.EventHandler(this.frmGamesMasterMap_Load);
            this.grpMapUnMapGames.ResumeLayout(false);
            this.grpMapUnMapGames.PerformLayout();
            this.grpMappedGameInfo.ResumeLayout(false);
            this.grpUnmappedInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpMapUnMapGames;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.ListView lvMapped;
        private System.Windows.Forms.ListView lvUnmapped;
        private System.Windows.Forms.Label lblUnmapped;
        private System.Windows.Forms.GroupBox grpUnmappedInfo;
        private System.Windows.Forms.ListView lvUnmappedInfo;
        private System.Windows.Forms.Label lblGameName;
        private System.Windows.Forms.GroupBox grpMappedGameInfo;
        private System.Windows.Forms.ListView lvMappedGameInfo;
        private System.Windows.Forms.ColumnHeader clmGameName;
        private System.Windows.Forms.ColumnHeader clmUnMapGameName;
        private System.Windows.Forms.ColumnHeader clmMapInfoSiteName;
        private System.Windows.Forms.ColumnHeader clmMapInfoPositionNo;
        private System.Windows.Forms.ColumnHeader clmMapInfoAssetNo;
        private System.Windows.Forms.ColumnHeader clmMapInfoMacSerialNo;
        private System.Windows.Forms.ColumnHeader clmMapInfoGamePartNo;
        private System.Windows.Forms.ColumnHeader clmMapInfoIsGameActive;
        private System.Windows.Forms.ColumnHeader clmUnMapInfoSiteName;
        private System.Windows.Forms.ColumnHeader clmUnMapInfoPositionNo;
        private System.Windows.Forms.ColumnHeader clmUnMapInfoAssetNo;
        private System.Windows.Forms.ColumnHeader clmUMapInfoMacSerialNo;
        private System.Windows.Forms.ColumnHeader clmUnMapInfoGamePartNo;
        private System.Windows.Forms.ColumnHeader clmUnMapInfoIsGameActive;
        private System.Windows.Forms.ColumnHeader clmManufacture;
        private System.Windows.Forms.ColumnHeader clmMachineName;
        private System.Windows.Forms.ColumnHeader clmManufactur;
        private System.Windows.Forms.ColumnHeader clmMachine_Name;
        private System.Windows.Forms.TextBox txtGameCategory;
        private System.Windows.Forms.TextBox txtGameManufacturer;
        private System.Windows.Forms.ComboBox cmbGameTitle;
        private System.Windows.Forms.Label lblGameManufacturer;
        private System.Windows.Forms.Label lblGameCategory;
        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.TextBox txtSrcUnMapGame;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
    }
}