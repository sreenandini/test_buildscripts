namespace BMC.EnterpriseClient.Views
{
    partial class frmUserSiteAccess
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
            this.lstCustomerAccess = new System.Windows.Forms.ComboBox();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.grpGroup = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvCompany = new System.Windows.Forms.TreeView();
            this.chkViewAllCompanies = new System.Windows.Forms.CheckBox();
            this.lblSubCmpAccess = new System.Windows.Forms.Label();
            this.tvSite = new System.Windows.Forms.TreeView();
            this.tvDepots = new System.Windows.Forms.TreeView();
            this.chkSelectAllSites = new System.Windows.Forms.CheckBox();
            this.ChkViewDepots = new System.Windows.Forms.CheckBox();
            this.lblSiteAccess = new System.Windows.Forms.Label();
            this.lblOperatorAccess = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpGroup.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstCustomerAccess
            // 
            this.lstCustomerAccess.FormattingEnabled = true;
            this.lstCustomerAccess.Location = new System.Drawing.Point(10, 12);
            this.lstCustomerAccess.Name = "lstCustomerAccess";
            this.lstCustomerAccess.Size = new System.Drawing.Size(357, 21);
            this.lstCustomerAccess.TabIndex = 0;
            this.lstCustomerAccess.SelectedIndexChanged += new System.EventHandler(this.lstCustomerAccess_SelectedIndexChanged);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(380, 10);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(94, 25);
            this.btnNewGroup.TabIndex = 1;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // grpGroup
            // 
            this.grpGroup.Controls.Add(this.splitContainer1);
            this.grpGroup.Location = new System.Drawing.Point(7, 39);
            this.grpGroup.Name = "grpGroup";
            this.grpGroup.Size = new System.Drawing.Size(1143, 428);
            this.grpGroup.TabIndex = 3;
            this.grpGroup.TabStop = false;
            this.grpGroup.Text = " ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvCompany);
            this.splitContainer1.Panel1.Controls.Add(this.chkViewAllCompanies);
            this.splitContainer1.Panel1.Controls.Add(this.lblSubCmpAccess);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tvSite);
            this.splitContainer1.Panel2.Controls.Add(this.tvDepots);
            this.splitContainer1.Panel2.Controls.Add(this.chkSelectAllSites);
            this.splitContainer1.Panel2.Controls.Add(this.ChkViewDepots);
            this.splitContainer1.Panel2.Controls.Add(this.lblSiteAccess);
            this.splitContainer1.Panel2.Controls.Add(this.lblOperatorAccess);
            this.splitContainer1.Size = new System.Drawing.Size(1137, 408);
            this.splitContainer1.SplitterDistance = 378;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // tvCompany
            // 
            this.tvCompany.CheckBoxes = true;
            this.tvCompany.Location = new System.Drawing.Point(7, 61);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(367, 345);
            this.tvCompany.TabIndex = 15;
            this.tvCompany.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCompany_NodeMouseClick);
            
            // 
            // chkViewAllCompanies
            // 
            this.chkViewAllCompanies.AutoSize = true;
            this.chkViewAllCompanies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewAllCompanies.Location = new System.Drawing.Point(199, 38);
            this.chkViewAllCompanies.Name = "chkViewAllCompanies";
            this.chkViewAllCompanies.Size = new System.Drawing.Size(149, 17);
            this.chkViewAllCompanies.TabIndex = 14;
            this.chkViewAllCompanies.Text = "View All Companies ?";
            this.chkViewAllCompanies.UseVisualStyleBackColor = true;
            this.chkViewAllCompanies.CheckedChanged += new System.EventHandler(this.chkViewAllCompanies_CheckedChanged);
            // 
            // lblSubCmpAccess
            // 
            this.lblSubCmpAccess.AutoSize = true;
            this.lblSubCmpAccess.Location = new System.Drawing.Point(3, 11);
            this.lblSubCmpAccess.Name = "lblSubCmpAccess";
            this.lblSubCmpAccess.Size = new System.Drawing.Size(326, 13);
            this.lblSubCmpAccess.TabIndex = 13;
            this.lblSubCmpAccess.Text = "Select the Sub Companies that this group has access to";
            // 
            // tvSite
            // 
            this.tvSite.CheckBoxes = true;
            this.tvSite.Location = new System.Drawing.Point(378, 61);
            this.tvSite.Name = "tvSite";
            this.tvSite.Size = new System.Drawing.Size(367, 345);
            this.tvSite.TabIndex = 17;
            this.tvSite.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSite_NodeMouseClick);
            // 
            // tvDepots
            // 
            this.tvDepots.CheckBoxes = true;
            this.tvDepots.Location = new System.Drawing.Point(3, 61);
            this.tvDepots.Name = "tvDepots";
            this.tvDepots.Size = new System.Drawing.Size(367, 345);
            this.tvDepots.TabIndex = 16;
            this.tvDepots.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDepots_NodeMouseClick);
            // 
            // chkSelectAllSites
            // 
            this.chkSelectAllSites.AutoSize = true;
            this.chkSelectAllSites.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelectAllSites.Location = new System.Drawing.Point(580, 38);
            this.chkSelectAllSites.Name = "chkSelectAllSites";
            this.chkSelectAllSites.Size = new System.Drawing.Size(85, 17);
            this.chkSelectAllSites.TabIndex = 14;
            this.chkSelectAllSites.Text = "Select All?";
            this.chkSelectAllSites.UseVisualStyleBackColor = true;
            this.chkSelectAllSites.CheckedChanged += new System.EventHandler(this.chkSelectAllSites_CheckedChanged);
            // 
            // ChkViewDepots
            // 
            this.ChkViewDepots.AutoSize = true;
            this.ChkViewDepots.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkViewDepots.Location = new System.Drawing.Point(180, 38);
            this.ChkViewDepots.Name = "ChkViewDepots";
            this.ChkViewDepots.Size = new System.Drawing.Size(125, 17);
            this.ChkViewDepots.TabIndex = 13;
            this.ChkViewDepots.Text = "View All Depots ?";
            this.ChkViewDepots.UseVisualStyleBackColor = true;
            this.ChkViewDepots.CheckedChanged += new System.EventHandler(this.ChkViewDepots_CheckedChanged);
            // 
            // lblSiteAccess
            // 
            this.lblSiteAccess.AutoSize = true;
            this.lblSiteAccess.Location = new System.Drawing.Point(415, 11);
            this.lblSiteAccess.Name = "lblSiteAccess";
            this.lblSiteAccess.Size = new System.Drawing.Size(264, 13);
            this.lblSiteAccess.TabIndex = 11;
            this.lblSiteAccess.Text = "Select the Sites that this group has access to";
            // 
            // lblOperatorAccess
            // 
            this.lblOperatorAccess.AutoSize = true;
            this.lblOperatorAccess.Location = new System.Drawing.Point(15, 11);
            this.lblOperatorAccess.Name = "lblOperatorAccess";
            this.lblOperatorAccess.Size = new System.Drawing.Size(276, 13);
            this.lblOperatorAccess.TabIndex = 10;
            this.lblOperatorAccess.Text = "Select the Depots that this group has access to";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(971, 473);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 26);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1065, 473);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUserSiteAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 501);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.grpGroup);
            this.Controls.Add(this.btnNewGroup);
            this.Controls.Add(this.lstCustomerAccess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmUserSiteAccess";
            this.Text = "User Site Access Administration";
            this.Load += new System.EventHandler(this.frmUserSiteAccess_Load);
            this.grpGroup.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
  
        #endregion

        private System.Windows.Forms.ComboBox lstCustomerAccess;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.GroupBox grpGroup;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkViewAllCompanies;
        private System.Windows.Forms.Label lblSubCmpAccess;
        private System.Windows.Forms.CheckBox chkSelectAllSites;
        private System.Windows.Forms.CheckBox ChkViewDepots;
        private System.Windows.Forms.Label lblSiteAccess;
        private System.Windows.Forms.Label lblOperatorAccess;
        private System.Windows.Forms.TreeView tvCompany;
        private System.Windows.Forms.TreeView tvSite;
        private System.Windows.Forms.TreeView tvDepots;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}

