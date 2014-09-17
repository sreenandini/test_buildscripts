namespace BMC.CentralisedSiteSettings.Presentation
{
    partial class frmSiteSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSiteSetting));
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbProfilesList = new System.Windows.Forms.ComboBox();
            this.btnApplyProfile = new System.Windows.Forms.Button();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.tvwSiteList = new System.Windows.Forms.TreeView();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.grpBoxSite = new System.Windows.Forms.GroupBox();
            this.tblRight = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PGBSiteSettings = new BMC.CentralisedSiteSettings.Presentation.PropertyGridBag();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblInner = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoxSite.SuspendLayout();
            this.tblRight.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblInner.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveProfile.Location = new System.Drawing.Point(447, 6);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(134, 28);
            this.btnSaveProfile.TabIndex = 0;
            this.btnSaveProfile.Text = "Save As &New Profile";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(687, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 28);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cmbProfilesList
            // 
            this.cmbProfilesList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProfilesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfilesList.FormattingEnabled = true;
            this.cmbProfilesList.Location = new System.Drawing.Point(103, 7);
            this.cmbProfilesList.Name = "cmbProfilesList";
            this.cmbProfilesList.Size = new System.Drawing.Size(278, 21);
            this.cmbProfilesList.TabIndex = 1;
            this.cmbProfilesList.SelectedIndexChanged += new System.EventHandler(this.cmbProfilesList_SelectedIndexChanged);
            // 
            // btnApplyProfile
            // 
            this.btnApplyProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyProfile.Location = new System.Drawing.Point(387, 6);
            this.btnApplyProfile.Name = "btnApplyProfile";
            this.btnApplyProfile.Size = new System.Drawing.Size(94, 23);
            this.btnApplyProfile.TabIndex = 2;
            this.btnApplyProfile.Text = "&Apply Profile";
            this.btnApplyProfile.UseVisualStyleBackColor = true;
            this.btnApplyProfile.Click += new System.EventHandler(this.btnApplyProfile_Click);
            // 
            // lblProfileName
            // 
            this.lblProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(3, 11);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(94, 13);
            this.lblProfileName.TabIndex = 0;
            this.lblProfileName.Text = "Profile Name :";
            // 
            // tvwSiteList
            // 
            this.tvwSiteList.CheckBoxes = true;
            this.tvwSiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSiteList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwSiteList.Location = new System.Drawing.Point(3, 16);
            this.tvwSiteList.Name = "tvwSiteList";
            this.tvwSiteList.Size = new System.Drawing.Size(288, 497);
            this.tvwSiteList.TabIndex = 0;
            this.tvwSiteList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeviewSiteList_AfterCheck);
            this.tvwSiteList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeviewSiteList_AfterSelect);
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateProfile.Location = new System.Drawing.Point(587, 6);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(94, 28);
            this.btnUpdateProfile.TabIndex = 1;
            this.btnUpdateProfile.Text = "&Save";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.btnUpdateProfile_Click);
            // 
            // grpBoxSite
            // 
            this.grpBoxSite.Controls.Add(this.tvwSiteList);
            this.grpBoxSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grpBoxSite.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSite.Name = "grpBoxSite";
            this.grpBoxSite.Size = new System.Drawing.Size(294, 516);
            this.grpBoxSite.TabIndex = 0;
            this.grpBoxSite.TabStop = false;
            this.grpBoxSite.Text = "Sites";
            // 
            // tblRight
            // 
            this.tblRight.ColumnCount = 1;
            this.tblRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tblRight.Controls.Add(this.groupBox1, 0, 1);
            this.tblRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRight.Location = new System.Drawing.Point(300, 0);
            this.tblRight.Margin = new System.Windows.Forms.Padding(0);
            this.tblRight.Name = "tblRight";
            this.tblRight.RowCount = 2;
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblRight.Size = new System.Drawing.Size(484, 522);
            this.tblRight.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblProfileName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbProfilesList, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnApplyProfile, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(484, 35);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PGBSiteSettings);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 481);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site Settings";
            // 
            // PGBSiteSettings
            // 
            this.PGBSiteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PGBSiteSettings.Location = new System.Drawing.Point(3, 16);
            this.PGBSiteSettings.Name = "PGBSiteSettings";
            this.PGBSiteSettings.Size = new System.Drawing.Size(472, 462);
            this.PGBSiteSettings.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 4;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFooter.Controls.Add(this.btnExit, 3, 0);
            this.tblFooter.Controls.Add(this.btnSaveProfile, 1, 0);
            this.tblFooter.Controls.Add(this.btnUpdateProfile, 2, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 522);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(784, 40);
            this.tblFooter.TabIndex = 2;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblInner, 0, 0);
            this.tblContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(784, 562);
            this.tblContainer.TabIndex = 1;
            // 
            // tblInner
            // 
            this.tblInner.ColumnCount = 2;
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblInner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Controls.Add(this.tblRight, 1, 0);
            this.tblInner.Controls.Add(this.grpBoxSite, 0, 0);
            this.tblInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInner.Location = new System.Drawing.Point(0, 0);
            this.tblInner.Margin = new System.Windows.Forms.Padding(0);
            this.tblInner.Name = "tblInner";
            this.tblInner.RowCount = 1;
            this.tblInner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblInner.Size = new System.Drawing.Size(784, 522);
            this.tblInner.TabIndex = 0;
            // 
            // frmSiteSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tblContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmSiteSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Centralised Site Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SiteSetting_Load);
            this.grpBoxSite.ResumeLayout(false);
            this.tblRight.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tblInner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbProfilesList;
        private System.Windows.Forms.Button btnApplyProfile;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.TreeView tvwSiteList;
        private System.Windows.Forms.GroupBox grpBoxSite;
        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.TableLayoutPanel tblRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private PropertyGridBag PGBSiteSettings;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblInner;
    }
}