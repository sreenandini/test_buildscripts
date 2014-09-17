namespace BMC.CentralisedSiteSettings.Presentation
{
    partial class SiteSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SiteSetting));
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbProfilesList = new System.Windows.Forms.ComboBox();
            this.btnApplyProfile = new System.Windows.Forms.Button();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.treeviewSiteList = new System.Windows.Forms.TreeView();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.grpBoxSite = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGridBag1 = new BMC.CentralisedSiteSettings.Presentation.PropertyGridBag();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.grpBoxSite.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveProfile.Location = new System.Drawing.Point(72, 3);
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
            this.btnExit.Location = new System.Drawing.Point(312, 3);
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
            this.cmbProfilesList.Size = new System.Drawing.Size(272, 21);
            this.cmbProfilesList.TabIndex = 1;
            this.cmbProfilesList.SelectedIndexChanged += new System.EventHandler(this.cmbProfilesList_SelectedIndexChanged);
            // 
            // btnApplyProfile
            // 
            this.btnApplyProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApplyProfile.Location = new System.Drawing.Point(381, 6);
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
            // treeviewSiteList
            // 
            this.treeviewSiteList.CheckBoxes = true;
            this.treeviewSiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeviewSiteList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeviewSiteList.Location = new System.Drawing.Point(3, 16);
            this.treeviewSiteList.Name = "treeviewSiteList";
            this.treeviewSiteList.Size = new System.Drawing.Size(288, 537);
            this.treeviewSiteList.TabIndex = 0;
            this.treeviewSiteList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeviewSiteList_AfterCheck);
            this.treeviewSiteList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeviewSiteList_AfterSelect);
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateProfile.Location = new System.Drawing.Point(212, 3);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(94, 28);
            this.btnUpdateProfile.TabIndex = 1;
            this.btnUpdateProfile.Text = "&Save";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.btnUpdateProfile_Click);
            // 
            // grpBoxSite
            // 
            this.grpBoxSite.Controls.Add(this.treeviewSiteList);
            this.grpBoxSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grpBoxSite.Location = new System.Drawing.Point(3, 3);
            this.grpBoxSite.Name = "grpBoxSite";
            this.grpBoxSite.Size = new System.Drawing.Size(294, 556);
            this.grpBoxSite.TabIndex = 0;
            this.grpBoxSite.TabStop = false;
            this.grpBoxSite.Text = "Sites";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpBoxSite, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(303, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(478, 556);
            this.tableLayoutPanel2.TabIndex = 1;
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(478, 35);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.propertyGridBag1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 480);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site Settings";
            // 
            // propertyGridBag1
            // 
            this.propertyGridBag1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridBag1.Location = new System.Drawing.Point(3, 16);
            this.propertyGridBag1.Name = "propertyGridBag1";
            this.propertyGridBag1.Size = new System.Drawing.Size(466, 461);
            this.propertyGridBag1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnExit, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnUpdateProfile, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnSaveProfile, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 521);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(478, 35);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // SiteSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "SiteSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Centralised Site Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SiteSetting_Load);
            this.grpBoxSite.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbProfilesList;
        private System.Windows.Forms.Button btnApplyProfile;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.TreeView treeviewSiteList;
        private System.Windows.Forms.GroupBox grpBoxSite;
        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private PropertyGridBag propertyGridBag1;
    }
}