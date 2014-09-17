namespace BMC.EnterpriseClient.Views
{
    partial class frmCentralizedSiteSetting
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
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowerButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveProfileAs = new System.Windows.Forms.Button();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblRightControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnApplyProfile = new System.Windows.Forms.Button();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.cmbProfilesList = new BMC.Common.Utilities.BmcComboBox();
            this.pgbSiteSettings = new BMC.EnterpriseClient.Views.PropertyGridBag();
            this.uxhcSiteTreeView = new BMC.CoreLib.Win32.UxHeaderContent();
            this.tvwSiteList = new System.Windows.Forms.TreeView();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.tblMainFrame.SuspendLayout();
            this.tblLowerButtons.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblRightControls.SuspendLayout();
            this.uxhcSiteTreeView.ChildContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainFrame.Controls.Add(this.tblLowerButtons, 0, 1);
            this.tblMainFrame.Controls.Add(this.tblContainer, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(796, 614);
            this.tblMainFrame.TabIndex = 0;
            // 
            // tblLowerButtons
            // 
            this.tblLowerButtons.ColumnCount = 4;
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 169F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowerButtons.Controls.Add(this.btnSaveProfileAs, 1, 0);
            this.tblLowerButtons.Controls.Add(this.btnSaveProfile, 2, 0);
            this.tblLowerButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblLowerButtons.Controls.Add(this.lbl_Status, 0, 0);
            this.tblLowerButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowerButtons.Location = new System.Drawing.Point(3, 577);
            this.tblLowerButtons.Name = "tblLowerButtons";
            this.tblLowerButtons.RowCount = 1;
            this.tblLowerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblLowerButtons.Size = new System.Drawing.Size(790, 34);
            this.tblLowerButtons.TabIndex = 0;
            // 
            // btnSaveProfileAs
            // 
            this.btnSaveProfileAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveProfileAs.Location = new System.Drawing.Point(407, 3);
            this.btnSaveProfileAs.Name = "btnSaveProfileAs";
            this.btnSaveProfileAs.Size = new System.Drawing.Size(140, 28);
            this.btnSaveProfileAs.TabIndex = 0;
            this.btnSaveProfileAs.Text = "btnSaveProfileAs";
            this.btnSaveProfileAs.UseVisualStyleBackColor = true;
            this.btnSaveProfileAs.Click += new System.EventHandler(this.btnSaveProfileAs_Click);
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveProfile.Location = new System.Drawing.Point(567, 3);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(100, 28);
            this.btnSaveProfile.TabIndex = 1;
            this.btnSaveProfile.Text = "btnSaveProfile";
            this.btnSaveProfile.UseVisualStyleBackColor = true;
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(687, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblRightControls, 1, 0);
            this.tblContainer.Controls.Add(this.uxhcSiteTreeView, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(3, 3);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 568F));
            this.tblContainer.Size = new System.Drawing.Size(790, 568);
            this.tblContainer.TabIndex = 1;
            // 
            // tblRightControls
            // 
            this.tblRightControls.ColumnCount = 3;
            this.tblRightControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tblRightControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRightControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblRightControls.Controls.Add(this.cmbProfilesList, 1, 0);
            this.tblRightControls.Controls.Add(this.btnApplyProfile, 2, 0);
            this.tblRightControls.Controls.Add(this.lblProfileName, 0, 0);
            this.tblRightControls.Controls.Add(this.pgbSiteSettings, 0, 1);
            this.tblRightControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRightControls.Location = new System.Drawing.Point(303, 3);
            this.tblRightControls.Name = "tblRightControls";
            this.tblRightControls.RowCount = 2;
            this.tblRightControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblRightControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRightControls.Size = new System.Drawing.Size(484, 562);
            this.tblRightControls.TabIndex = 0;
            // 
            // btnApplyProfile
            // 
            this.btnApplyProfile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnApplyProfile.Location = new System.Drawing.Point(367, 3);
            this.btnApplyProfile.Name = "btnApplyProfile";
            this.btnApplyProfile.Size = new System.Drawing.Size(100, 28);
            this.btnApplyProfile.TabIndex = 1;
            this.btnApplyProfile.Text = "btnApplyProfile";
            this.btnApplyProfile.UseVisualStyleBackColor = true;
            this.btnApplyProfile.Click += new System.EventHandler(this.btnApplyProfile_Click);
            // 
            // lblProfileName
            // 
            this.lblProfileName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(3, 11);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(74, 13);
            this.lblProfileName.TabIndex = 2;
            this.lblProfileName.Text = "lblProfileName";
            // 
            // cmbProfilesList
            // 
            this.cmbProfilesList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProfilesList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbProfilesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfilesList.FormattingEnabled = true;
            this.cmbProfilesList.Location = new System.Drawing.Point(133, 7);
            this.cmbProfilesList.Name = "cmbProfilesList";
            this.cmbProfilesList.Size = new System.Drawing.Size(228, 21);
            this.cmbProfilesList.TabIndex = 0;
            this.cmbProfilesList.SelectedIndexChanged += new System.EventHandler(this.cmbProfilesList_SelectedIndexChanged);
            // 
            // pgbSiteSettings
            // 
            this.tblRightControls.SetColumnSpan(this.pgbSiteSettings, 3);
            this.pgbSiteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbSiteSettings.Location = new System.Drawing.Point(3, 38);
            this.pgbSiteSettings.Name = "pgbSiteSettings";
            this.pgbSiteSettings.Size = new System.Drawing.Size(478, 521);
            this.pgbSiteSettings.TabIndex = 3;
            // 
            // uxhcSiteTreeView
            // 
            // 
            // uxhcSiteTreeView.Child
            // 
            this.uxhcSiteTreeView.ChildContainer.Controls.Add(this.tvwSiteList);
            this.uxhcSiteTreeView.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxhcSiteTreeView.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxhcSiteTreeView.ChildContainer.Name = "Child";
            this.uxhcSiteTreeView.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxhcSiteTreeView.ChildContainer.Size = new System.Drawing.Size(294, 536);
            this.uxhcSiteTreeView.ChildContainer.TabIndex = 2;
            this.uxhcSiteTreeView.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxhcSiteTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxhcSiteTreeView.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxhcSiteTreeView.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxhcSiteTreeView.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxhcSiteTreeView.HeaderText = "uxhcSiteTreeView";
            this.uxhcSiteTreeView.IsClosable = false;
            this.uxhcSiteTreeView.Location = new System.Drawing.Point(3, 3);
            this.uxhcSiteTreeView.Name = "uxhcSiteTreeView";
            this.uxhcSiteTreeView.PinVisible = false;
            this.uxhcSiteTreeView.Size = new System.Drawing.Size(294, 562);
            this.uxhcSiteTreeView.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxhcSiteTreeView.TabIndex = 1;
            // 
            // tvwSiteList
            // 
            this.tvwSiteList.CheckBoxes = true;
            this.tvwSiteList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSiteList.Location = new System.Drawing.Point(3, 3);
            this.tvwSiteList.Name = "tvwSiteList";
            this.tvwSiteList.Size = new System.Drawing.Size(288, 530);
            this.tvwSiteList.TabIndex = 0;
            this.tvwSiteList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvwSiteList_AfterCheck);
            this.tvwSiteList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSiteList_AfterSelect);
            // 
            // lbl_Status
            // 
            this.lbl_Status.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(3, 4);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(329, 26);
            this.lbl_Status.TabIndex = 3;
            this.lbl_Status.Text = "Cannot create profile name as \"Default Profile\" or \"Select Profile\" or \"Unassigne" +
    "d\".";
            // 
            // frmCentralizedSiteSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 614);
            this.Controls.Add(this.tblMainFrame);
            this.Name = "frmCentralizedSiteSetting";
            this.Text = "frmCentralizedSiteSetting";
            this.Load += new System.EventHandler(this.frmCentralizedSiteSetting_Load);
            this.tblMainFrame.ResumeLayout(false);
            this.tblLowerButtons.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tblRightControls.ResumeLayout(false);
            this.tblRightControls.PerformLayout();
            this.uxhcSiteTreeView.ChildContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblLowerButtons;
        private System.Windows.Forms.Button btnSaveProfileAs;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblRightControls;
        private Common.Utilities.BmcComboBox cmbProfilesList;
        private CoreLib.Win32.UxHeaderContent uxhcSiteTreeView;
        private System.Windows.Forms.TreeView tvwSiteList;
        private System.Windows.Forms.Button btnApplyProfile;
        private System.Windows.Forms.Label lblProfileName;
        private PropertyGridBag pgbSiteSettings;
        private System.Windows.Forms.Label lbl_Status;

    }
}