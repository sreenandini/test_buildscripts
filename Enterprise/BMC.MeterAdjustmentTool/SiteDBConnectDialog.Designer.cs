namespace BMC.MeterAdjustmentTool
{
    partial class SiteDBConnectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteDBConnectDialog));
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnConnect = new System.Windows.Forms.Button();
            this.tblDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblDatabaseServer = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConnectionTimeout = new System.Windows.Forms.Label();
            this.updConenctionTimeout = new System.Windows.Forms.NumericUpDown();
            this.cboSites = new System.Windows.Forms.ComboBox();
            this.cboDatabaseServer = new System.Windows.Forms.ComboBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkSaveDetails = new System.Windows.Forms.CheckBox();
            this.pnlBottom.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updConenctionTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.chkSaveDetails);
            this.pnlBottom.Controls.Add(this.tblButtons);
            this.pnlBottom.Location = new System.Drawing.Point(0, 178);
            this.pnlBottom.Size = new System.Drawing.Size(406, 38);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblDetails);
            this.pnlContainer.Size = new System.Drawing.Size(406, 178);
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 2;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblButtons.Controls.Add(this.btnCancel, 1, 0);
            this.tblButtons.Controls.Add(this.btnConnect, 0, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.tblButtons.Location = new System.Drawing.Point(146, 0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(260, 38);
            this.tblButtons.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageList = this.imglstSmallIcons;
            this.btnCancel.Location = new System.Drawing.Point(133, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Database.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Cancel.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "ConnectServer.ico");
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.ImageKey = "ConnectServer.ico";
            this.btnConnect.Location = new System.Drawing.Point(3, 5);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(124, 28);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tblDetails
            // 
            this.tblDetails.ColumnCount = 3;
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblDetails.Controls.Add(this.lblSite, 0, 0);
            this.tblDetails.Controls.Add(this.lblDatabaseServer, 0, 1);
            this.tblDetails.Controls.Add(this.lblUserName, 0, 2);
            this.tblDetails.Controls.Add(this.lblPassword, 0, 3);
            this.tblDetails.Controls.Add(this.lblConnectionTimeout, 0, 4);
            this.tblDetails.Controls.Add(this.updConenctionTimeout, 1, 4);
            this.tblDetails.Controls.Add(this.cboSites, 1, 0);
            this.tblDetails.Controls.Add(this.cboDatabaseServer, 1, 1);
            this.tblDetails.Controls.Add(this.txtUserName, 1, 2);
            this.tblDetails.Controls.Add(this.txtPassword, 1, 3);
            this.tblDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDetails.Location = new System.Drawing.Point(0, 0);
            this.tblDetails.Name = "tblDetails";
            this.tblDetails.RowCount = 6;
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDetails.Size = new System.Drawing.Size(406, 178);
            this.tblDetails.TabIndex = 0;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(3, 11);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(38, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "&Site :";
            // 
            // lblDatabaseServer
            // 
            this.lblDatabaseServer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDatabaseServer.AutoSize = true;
            this.lblDatabaseServer.Location = new System.Drawing.Point(3, 46);
            this.lblDatabaseServer.Name = "lblDatabaseServer";
            this.lblDatabaseServer.Size = new System.Drawing.Size(113, 13);
            this.lblDatabaseServer.TabIndex = 2;
            this.lblDatabaseServer.Text = "&Database Server :";
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(3, 81);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(79, 13);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "&User Name :";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 116);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(70, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "&Password :";
            // 
            // lblConnectionTimeout
            // 
            this.lblConnectionTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConnectionTimeout.AutoSize = true;
            this.lblConnectionTimeout.Location = new System.Drawing.Point(3, 151);
            this.lblConnectionTimeout.Name = "lblConnectionTimeout";
            this.lblConnectionTimeout.Size = new System.Drawing.Size(62, 13);
            this.lblConnectionTimeout.TabIndex = 8;
            this.lblConnectionTimeout.Text = "&Timeout :";
            // 
            // updConenctionTimeout
            // 
            this.updConenctionTimeout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.updConenctionTimeout.Location = new System.Drawing.Point(143, 147);
            this.updConenctionTimeout.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.updConenctionTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updConenctionTimeout.Name = "updConenctionTimeout";
            this.updConenctionTimeout.Size = new System.Drawing.Size(85, 21);
            this.updConenctionTimeout.TabIndex = 9;
            this.updConenctionTimeout.Value = new decimal(new int[] {
            32000,
            0,
            0,
            0});
            // 
            // cboSites
            // 
            this.cboSites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSites.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSites.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSites.Enabled = false;
            this.cboSites.FormattingEnabled = true;
            this.cboSites.Location = new System.Drawing.Point(143, 7);
            this.cboSites.Name = "cboSites";
            this.cboSites.Size = new System.Drawing.Size(250, 21);
            this.cboSites.TabIndex = 1;
            this.cboSites.SelectedIndexChanged += new System.EventHandler(this.cboSites_SelectedIndexChanged);
            this.cboSites.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboSites_KeyDown);
            // 
            // cboDatabaseServer
            // 
            this.cboDatabaseServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDatabaseServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboDatabaseServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDatabaseServer.FormattingEnabled = true;
            this.cboDatabaseServer.Location = new System.Drawing.Point(143, 42);
            this.cboDatabaseServer.Name = "cboDatabaseServer";
            this.cboDatabaseServer.Size = new System.Drawing.Size(250, 21);
            this.cboDatabaseServer.TabIndex = 3;
            this.cboDatabaseServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboDatabaseServer_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Location = new System.Drawing.Point(143, 77);
            this.txtUserName.MaxLength = 255;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(250, 21);
            this.txtUserName.TabIndex = 5;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(143, 112);
            this.txtPassword.MaxLength = 255;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 21);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // chkSaveDetails
            // 
            this.chkSaveDetails.AutoSize = true;
            this.chkSaveDetails.Checked = true;
            this.chkSaveDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveDetails.Location = new System.Drawing.Point(6, 12);
            this.chkSaveDetails.Name = "chkSaveDetails";
            this.chkSaveDetails.Size = new System.Drawing.Size(98, 17);
            this.chkSaveDetails.TabIndex = 1;
            this.chkSaveDetails.Text = "Save Details";
            this.chkSaveDetails.UseVisualStyleBackColor = true;
            // 
            // SiteDBConnectDialog
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(406, 216);
            this.Name = "SiteDBConnectDialog";
            this.Text = "Connect to Site...";
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblDetails.ResumeLayout(false);
            this.tblDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updConenctionTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tblDetails;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblDatabaseServer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConnectionTimeout;
        private System.Windows.Forms.NumericUpDown updConenctionTimeout;
        private System.Windows.Forms.ComboBox cboSites;
        private System.Windows.Forms.ComboBox cboDatabaseServer;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkSaveDetails;
    }
}