namespace BMC.EnterpriseClient.Views
{
    partial class ucSiteMaintenance
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
            this.components = new System.ComponentModel.Container();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lvTransactionKey = new System.Windows.Forms.ListView();
            this.clmTransactionKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTransactionFlagName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCreatedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmExpiryDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStaff_First_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStaff_Last_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctxmenuVoid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxmenuItemVoid = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateAuthorizationKey = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtAuthorizationKey = new System.Windows.Forms.TextBox();
            this.rdbtnSiteRecovery = new System.Windows.Forms.RadioButton();
            this.rdbtnNewSite = new System.Windows.Forms.RadioButton();
            this.rdbtnFactoryReset = new System.Windows.Forms.RadioButton();
            this.tblContainer.SuspendLayout();
            this.ctxmenuVoid.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.AutoSize = true;
            this.tblContainer.ColumnCount = 4;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.lvTransactionKey, 1, 3);
            this.tblContainer.Controls.Add(this.btnCreateAuthorizationKey, 1, 2);
            this.tblContainer.Controls.Add(this.btnRefresh, 3, 2);
            this.tblContainer.Controls.Add(this.txtAuthorizationKey, 2, 2);
            this.tblContainer.Controls.Add(this.rdbtnSiteRecovery, 3, 1);
            this.tblContainer.Controls.Add(this.rdbtnNewSite, 1, 1);
            this.tblContainer.Controls.Add(this.rdbtnFactoryReset, 2, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 4;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(890, 339);
            this.tblContainer.TabIndex = 0;
            // 
            // lvTransactionKey
            // 
            this.lvTransactionKey.AutoArrange = false;
            this.lvTransactionKey.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTransactionKey,
            this.clmTransactionFlagName,
            this.clmCreatedDate,
            this.clmExpiryDate,
            this.clmStaff_First_Name,
            this.clmStaff_Last_Name,
            this.clmStatus});
            this.tblContainer.SetColumnSpan(this.lvTransactionKey, 3);
            this.lvTransactionKey.ContextMenuStrip = this.ctxmenuVoid;
            this.lvTransactionKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTransactionKey.FullRowSelect = true;
            this.lvTransactionKey.GridLines = true;
            this.lvTransactionKey.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTransactionKey.HideSelection = false;
            this.lvTransactionKey.Location = new System.Drawing.Point(13, 73);
            this.lvTransactionKey.MultiSelect = false;
            this.lvTransactionKey.Name = "lvTransactionKey";
            this.lvTransactionKey.ShowGroups = false;
            this.lvTransactionKey.Size = new System.Drawing.Size(874, 263);
            this.lvTransactionKey.TabIndex = 6;
            this.lvTransactionKey.UseCompatibleStateImageBehavior = false;
            this.lvTransactionKey.View = System.Windows.Forms.View.Details;
            // 
            // clmTransactionKey
            // 
            this.clmTransactionKey.Text = "Transaction Key";
            this.clmTransactionKey.Width = 250;
            // 
            // clmTransactionFlagName
            // 
            this.clmTransactionFlagName.Text = "Type";
            // 
            // clmCreatedDate
            // 
            this.clmCreatedDate.Text = "Created Date";
            this.clmCreatedDate.Width = 140;
            // 
            // clmExpiryDate
            // 
            this.clmExpiryDate.Text = "Expiry Date";
            this.clmExpiryDate.Width = 140;
            // 
            // clmStaff_First_Name
            // 
            this.clmStaff_First_Name.Text = "First Name";
            this.clmStaff_First_Name.Width = 160;
            // 
            // clmStaff_Last_Name
            // 
            this.clmStaff_Last_Name.Text = "Last Name";
            this.clmStaff_Last_Name.Width = 160;
            // 
            // clmStatus
            // 
            this.clmStatus.Text = "Status";
            // 
            // ctxmenuVoid
            // 
            this.ctxmenuVoid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxmenuItemVoid});
            this.ctxmenuVoid.Name = "contextMenuStrip1";
            this.ctxmenuVoid.Size = new System.Drawing.Size(99, 26);
            // 
            // ctxmenuItemVoid
            // 
            this.ctxmenuItemVoid.Name = "ctxmenuItemVoid";
            this.ctxmenuItemVoid.Size = new System.Drawing.Size(98, 22);
            this.ctxmenuItemVoid.Text = "&Void";
            this.ctxmenuItemVoid.Click += new System.EventHandler(this.ctxmenuItemVoid_Click);
            // 
            // btnCreateAuthorizationKey
            // 
            this.btnCreateAuthorizationKey.Location = new System.Drawing.Point(13, 39);
            this.btnCreateAuthorizationKey.Name = "btnCreateAuthorizationKey";
            this.btnCreateAuthorizationKey.Size = new System.Drawing.Size(147, 28);
            this.btnCreateAuthorizationKey.TabIndex = 3;
            this.btnCreateAuthorizationKey.Text = "Generate Authorization Key";
            this.btnCreateAuthorizationKey.UseVisualStyleBackColor = true;
            this.btnCreateAuthorizationKey.Click += new System.EventHandler(this.btnCreateAuthorizationKey_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(353, 39);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 28);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtAuthorizationKey
            // 
            this.txtAuthorizationKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAuthorizationKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAuthorizationKey.Location = new System.Drawing.Point(178, 39);
            this.txtAuthorizationKey.Name = "txtAuthorizationKey";
            this.txtAuthorizationKey.ReadOnly = true;
            this.txtAuthorizationKey.Size = new System.Drawing.Size(169, 20);
            this.txtAuthorizationKey.TabIndex = 4;
            this.txtAuthorizationKey.WordWrap = false;
            // 
            // rdbtnSiteRecovery
            // 
            this.rdbtnSiteRecovery.AutoSize = true;
            this.rdbtnSiteRecovery.Location = new System.Drawing.Point(353, 16);
            this.rdbtnSiteRecovery.Name = "rdbtnSiteRecovery";
            this.rdbtnSiteRecovery.Size = new System.Drawing.Size(92, 17);
            this.rdbtnSiteRecovery.TabIndex = 2;
            this.rdbtnSiteRecovery.Text = "Site Recovery";
            this.rdbtnSiteRecovery.UseVisualStyleBackColor = true;
            // 
            // rdbtnNewSite
            // 
            this.rdbtnNewSite.AutoSize = true;
            this.rdbtnNewSite.Location = new System.Drawing.Point(13, 16);
            this.rdbtnNewSite.Name = "rdbtnNewSite";
            this.rdbtnNewSite.Size = new System.Drawing.Size(68, 17);
            this.rdbtnNewSite.TabIndex = 0;
            this.rdbtnNewSite.TabStop = true;
            this.rdbtnNewSite.Text = "New Site";
            this.rdbtnNewSite.UseVisualStyleBackColor = true;
            // 
            // rdbtnFactoryReset
            // 
            this.rdbtnFactoryReset.AutoSize = true;
            this.rdbtnFactoryReset.Location = new System.Drawing.Point(178, 16);
            this.rdbtnFactoryReset.Name = "rdbtnFactoryReset";
            this.rdbtnFactoryReset.Size = new System.Drawing.Size(91, 17);
            this.rdbtnFactoryReset.TabIndex = 1;
            this.rdbtnFactoryReset.TabStop = true;
            this.rdbtnFactoryReset.Text = "Factory Reset";
            this.rdbtnFactoryReset.UseVisualStyleBackColor = true;
            // 
            // ucSiteMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "ucSiteMaintenance";
            this.Size = new System.Drawing.Size(890, 339);
            this.Load += new System.EventHandler(this.ucSiteMaintenance_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblContainer.PerformLayout();
            this.ctxmenuVoid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.RadioButton rdbtnFactoryReset;
        private System.Windows.Forms.RadioButton rdbtnSiteRecovery;
        private System.Windows.Forms.Button btnCreateAuthorizationKey;
        private System.Windows.Forms.TextBox txtAuthorizationKey;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lvTransactionKey;
        private System.Windows.Forms.ColumnHeader clmTransactionKey;
        private System.Windows.Forms.ColumnHeader clmTransactionFlagName;
        private System.Windows.Forms.ColumnHeader clmCreatedDate;
        private System.Windows.Forms.ColumnHeader clmExpiryDate;
        private System.Windows.Forms.ColumnHeader clmStaff_First_Name;
        private System.Windows.Forms.ColumnHeader clmStaff_Last_Name;
        private System.Windows.Forms.ColumnHeader clmStatus;
        private System.Windows.Forms.RadioButton rdbtnNewSite;
        private System.Windows.Forms.ContextMenuStrip ctxmenuVoid;
        private System.Windows.Forms.ToolStripMenuItem ctxmenuItemVoid;
    }
}
