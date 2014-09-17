namespace BMC.CentralisedSiteSettings.Presentation
{
    partial class ProfileConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileConfiguration));
            this.grpBoxMain = new System.Windows.Forms.GroupBox();
            this.grpBoxProfile = new System.Windows.Forms.GroupBox();
            this.btnUpdateProfile = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.propertyGridBag1 = new BMC.CentralisedSiteSettings.Presentation.PropertyGridBag();
            this.cmbProfilesList = new System.Windows.Forms.ComboBox();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpBoxMain.SuspendLayout();
            this.grpBoxProfile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBoxMain
            // 
            this.grpBoxMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxMain.Controls.Add(this.grpBoxProfile);
            this.grpBoxMain.Location = new System.Drawing.Point(4, 3);
            this.grpBoxMain.Name = "grpBoxMain";
            this.grpBoxMain.Size = new System.Drawing.Size(698, 653);
            this.grpBoxMain.TabIndex = 9;
            this.grpBoxMain.TabStop = false;
            // 
            // grpBoxProfile
            // 
            this.grpBoxProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBoxProfile.Controls.Add(this.btnUpdateProfile);
            this.grpBoxProfile.Controls.Add(this.groupBox1);
            this.grpBoxProfile.Controls.Add(this.cmbProfilesList);
            this.grpBoxProfile.Controls.Add(this.lblProfileName);
            this.grpBoxProfile.Controls.Add(this.btnSaveProfile);
            this.grpBoxProfile.Controls.Add(this.btnExit);
            this.grpBoxProfile.Location = new System.Drawing.Point(0, -5);
            this.grpBoxProfile.Name = "grpBoxProfile";
            this.grpBoxProfile.Size = new System.Drawing.Size(693, 662);
            this.grpBoxProfile.TabIndex = 1;
            this.grpBoxProfile.TabStop = false;
            // 
            // btnUpdateProfile
            // 
            this.btnUpdateProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUpdateProfile.Location = new System.Drawing.Point(290, 629);
            this.btnUpdateProfile.Name = "btnUpdateProfile";
            this.btnUpdateProfile.Size = new System.Drawing.Size(113, 23);
            this.btnUpdateProfile.TabIndex = 8;
            this.btnUpdateProfile.Text = "&Edit";
            this.btnUpdateProfile.UseVisualStyleBackColor = true;
            this.btnUpdateProfile.Click += new System.EventHandler(this.btnUpdateProfile_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.propertyGridBag1);
            this.groupBox1.Location = new System.Drawing.Point(6, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 578);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Site Settings";
            // 
            // propertyGridBag1
            // 
            this.propertyGridBag1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGridBag1.Location = new System.Drawing.Point(10, 14);
            this.propertyGridBag1.Name = "propertyGridBag1";
            this.propertyGridBag1.Size = new System.Drawing.Size(661, 558);
            this.propertyGridBag1.TabIndex = 0;
            this.propertyGridBag1.Load += new System.EventHandler(this.propertyGridBag1_Load);
            // 
            // cmbProfilesList
            // 
            this.cmbProfilesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProfilesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfilesList.FormattingEnabled = true;
            this.cmbProfilesList.Location = new System.Drawing.Point(92, 18);
            this.cmbProfilesList.Name = "cmbProfilesList";
            this.cmbProfilesList.Size = new System.Drawing.Size(585, 21);
            this.cmbProfilesList.TabIndex = 4;
            this.cmbProfilesList.SelectedIndexChanged += new System.EventHandler(this.cmbProfilesList_SelectedIndexChanged);
            // 
            // lblProfileName
            // 
            this.lblProfileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(13, 22);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(73, 13);
            this.lblProfileName.TabIndex = 6;
            this.lblProfileName.Text = "Profile Name :";
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSaveProfile.Location = new System.Drawing.Point(127, 628);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(113, 23);
            this.btnSaveProfile.TabIndex = 1;
            this.btnSaveProfile.Text = "&New";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnExit.Location = new System.Drawing.Point(448, 628);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ProfileConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 668);
            this.Controls.Add(this.grpBoxMain);
            this.Name = "ProfileConfiguration";
            this.Text = "Site Controller Settings Profile";
            this.Load += new System.EventHandler(this.SiteSetting_Load);
            this.grpBoxMain.ResumeLayout(false);
            this.grpBoxProfile.ResumeLayout(false);
            this.grpBoxProfile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxMain;
        private System.Windows.Forms.GroupBox grpBoxProfile;
        private System.Windows.Forms.Button btnUpdateProfile;
        private System.Windows.Forms.GroupBox groupBox1;
        private PropertyGridBag propertyGridBag1;
        private System.Windows.Forms.ComboBox cmbProfilesList;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnExit;
    }
}