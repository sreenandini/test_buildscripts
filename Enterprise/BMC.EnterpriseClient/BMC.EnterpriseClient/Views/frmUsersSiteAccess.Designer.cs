namespace BMC.EnterpriseClient.Views
{
    partial class frmUsersSiteAccess
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lstCustomerAccess = new System.Windows.Forms.ComboBox();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tvCompany = new System.Windows.Forms.TreeView();
            this.tvDepots = new System.Windows.Forms.TreeView();
            this.tvSite = new System.Windows.Forms.TreeView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkViewAllCompanies = new System.Windows.Forms.CheckBox();
            this.lblSubCmpAccess = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ChkViewDepots = new System.Windows.Forms.CheckBox();
            this.lblOperatorAccess = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chkSelectAllSites = new System.Windows.Forms.CheckBox();
            this.lblSiteAccess = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lstCustomerAccess);
            this.panel1.Controls.Add(this.btnNewGroup);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(697, 39);
            this.panel1.TabIndex = 1;
            // 
            // lstCustomerAccess
            // 
            this.lstCustomerAccess.FormattingEnabled = true;
            this.lstCustomerAccess.Location = new System.Drawing.Point(8, 3);
            this.lstCustomerAccess.Name = "lstCustomerAccess";
            this.lstCustomerAccess.Size = new System.Drawing.Size(307, 21);
            this.lstCustomerAccess.TabIndex = 9;
            this.lstCustomerAccess.SelectedIndexChanged += new System.EventHandler(lstCustomerAccess_SelectedIndexChanged);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(321, 3);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(81, 25);
            this.btnNewGroup.TabIndex = 10;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(btnNewGroup_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tvSite, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tvDepots, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.19261F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.80739F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(964, 420);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tvCompany);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(324, 359);
            this.panel2.TabIndex = 0;
            // 
            // tvCompany
            // 
            this.tvCompany.CheckBoxes = true;
            this.tvCompany.Location = new System.Drawing.Point(8, 0);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(319, 356);
            this.tvCompany.TabIndex = 17;
            this.tvCompany.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvCompany_NodeMouseClick);
            // 
            // tvDepots
            // 
            this.tvDepots.CheckBoxes = true;
            this.tvDepots.Location = new System.Drawing.Point(333, 58);
            this.tvDepots.Name = "tvDepots";
            this.tvDepots.Size = new System.Drawing.Size(314, 359);
            this.tvDepots.TabIndex = 17;
            this.tvDepots.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDepots_NodeMouseClick);
            // 
            // tvSite
            // 
            this.tvSite.CheckBoxes = true;
            this.tvSite.Location = new System.Drawing.Point(653, 58);
            this.tvSite.Name = "tvSite";
            this.tvSite.Size = new System.Drawing.Size(308, 356);
            this.tvSite.TabIndex = 18;
            this.tvSite.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSite_NodeMouseClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(890, 471);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 26);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkViewAllCompanies);
            this.panel3.Controls.Add(this.lblSubCmpAccess);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(324, 49);
            this.panel3.TabIndex = 19;
            // 
            // chkViewAllCompanies
            // 
            this.chkViewAllCompanies.AutoSize = true;
            this.chkViewAllCompanies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewAllCompanies.Location = new System.Drawing.Point(185, 29);
            this.chkViewAllCompanies.Name = "chkViewAllCompanies";
            this.chkViewAllCompanies.Size = new System.Drawing.Size(127, 17);
            this.chkViewAllCompanies.TabIndex = 16;
            this.chkViewAllCompanies.Text = "View All Companies ?";
            this.chkViewAllCompanies.UseVisualStyleBackColor = true;
            this.chkViewAllCompanies.CheckedChanged += new System.EventHandler(this.chkViewAllCompanies_CheckedChanged);
            // 
            // lblSubCmpAccess
            // 
            this.lblSubCmpAccess.AutoSize = true;
            this.lblSubCmpAccess.Location = new System.Drawing.Point(31, 0);
            this.lblSubCmpAccess.Name = "lblSubCmpAccess";
            this.lblSubCmpAccess.Size = new System.Drawing.Size(271, 13);
            this.lblSubCmpAccess.TabIndex = 15;
            this.lblSubCmpAccess.Text = "Select the Sub Companies that this group has access to";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ChkViewDepots);
            this.panel4.Controls.Add(this.lblOperatorAccess);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(333, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(314, 49);
            this.panel4.TabIndex = 20;
            // 
            // ChkViewDepots
            // 
            this.ChkViewDepots.AutoSize = true;
            this.ChkViewDepots.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkViewDepots.Location = new System.Drawing.Point(167, 29);
            this.ChkViewDepots.Name = "ChkViewDepots";
            this.ChkViewDepots.Size = new System.Drawing.Size(109, 17);
            this.ChkViewDepots.TabIndex = 15;
            this.ChkViewDepots.Text = "View All Depots ?";
            this.ChkViewDepots.UseVisualStyleBackColor = true;
            this.ChkViewDepots.CheckedChanged += new System.EventHandler(this.ChkViewDepots_CheckedChanged);
            // 
            // lblOperatorAccess
            // 
            this.lblOperatorAccess.AutoSize = true;
            this.lblOperatorAccess.Location = new System.Drawing.Point(17, 0);
            this.lblOperatorAccess.Name = "lblOperatorAccess";
            this.lblOperatorAccess.Size = new System.Drawing.Size(231, 13);
            this.lblOperatorAccess.TabIndex = 14;
            this.lblOperatorAccess.Text = "Select the Depots that this group has access to";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.chkSelectAllSites);
            this.panel5.Controls.Add(this.lblSiteAccess);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(653, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(308, 49);
            this.panel5.TabIndex = 21;
            // 
            // chkSelectAllSites
            // 
            this.chkSelectAllSites.AutoSize = true;
            this.chkSelectAllSites.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelectAllSites.Location = new System.Drawing.Point(190, 29);
            this.chkSelectAllSites.Name = "chkSelectAllSites";
            this.chkSelectAllSites.Size = new System.Drawing.Size(76, 17);
            this.chkSelectAllSites.TabIndex = 16;
            this.chkSelectAllSites.Text = "Select All?";
            this.chkSelectAllSites.UseVisualStyleBackColor = true;
            this.chkSelectAllSites.CheckedChanged += new System.EventHandler(this.chkSelectAllSites_CheckedChanged);
            // 
            // lblSiteAccess
            // 
            this.lblSiteAccess.AutoSize = true;
            this.lblSiteAccess.Location = new System.Drawing.Point(16, 0);
            this.lblSiteAccess.Name = "lblSiteAccess";
            this.lblSiteAccess.Size = new System.Drawing.Size(220, 13);
            this.lblSiteAccess.TabIndex = 15;
            this.lblSiteAccess.Text = "Select the Sites that this group has access to";
            // 
            // frmUsersSiteAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 526);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "frmUsersSiteAccess";
            this.Text = "frmUsersSiteAccess";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox lstCustomerAccess;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tvCompany;
        private System.Windows.Forms.TreeView tvDepots;
        private System.Windows.Forms.TreeView tvSite;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkViewAllCompanies;
        private System.Windows.Forms.Label lblSubCmpAccess;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox ChkViewDepots;
        private System.Windows.Forms.Label lblOperatorAccess;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox chkSelectAllSites;
        private System.Windows.Forms.Label lblSiteAccess;
    }
}