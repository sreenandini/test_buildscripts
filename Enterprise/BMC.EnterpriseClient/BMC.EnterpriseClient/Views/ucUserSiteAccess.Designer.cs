using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class ucUserSiteAccess
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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.chkViewAllCompanies = new System.Windows.Forms.CheckBox();
            this.tvCompany = new System.Windows.Forms.TreeView();
            this.lblSubCmpAccess = new System.Windows.Forms.Label();
            this.lblOperatorAccess = new System.Windows.Forms.Label();
            this.ChkViewDepots = new System.Windows.Forms.CheckBox();
            this.lblSiteAccess = new System.Windows.Forms.Label();
            this.chkSelectAllSites = new System.Windows.Forms.CheckBox();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.tvSite = new System.Windows.Forms.TreeView();
            this.tvDepots = new System.Windows.Forms.TreeView();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveGroup = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblMainContent = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNewGroup = new System.Windows.Forms.TextBox();
            this.cboCustomerAccess = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.tblContainer.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tblMainContent.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(3, 37);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // chkViewAllCompanies
            // 
            this.chkViewAllCompanies.AutoSize = true;
            this.chkViewAllCompanies.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkViewAllCompanies.Location = new System.Drawing.Point(3, 63);
            this.chkViewAllCompanies.Name = "chkViewAllCompanies";
            this.chkViewAllCompanies.Size = new System.Drawing.Size(127, 17);
            this.chkViewAllCompanies.TabIndex = 3;
            this.chkViewAllCompanies.Text = "View All Companies ?";
            this.chkViewAllCompanies.UseVisualStyleBackColor = true;
            this.chkViewAllCompanies.CheckedChanged += new System.EventHandler(this.chkViewAllCompanies_CheckedChanged);
            // 
            // tvCompany
            // 
            this.tvCompany.CheckBoxes = true;
            this.tvCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCompany.Location = new System.Drawing.Point(3, 93);
            this.tvCompany.Name = "tvCompany";
            this.tvCompany.Size = new System.Drawing.Size(286, 282);
            this.tvCompany.TabIndex = 8;
            this.tvCompany.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvCompany_AfterCheck);
            // 
            // lblSubCmpAccess
            // 
            this.lblSubCmpAccess.AutoSize = true;
            this.lblSubCmpAccess.Location = new System.Drawing.Point(3, 40);
            this.lblSubCmpAccess.Name = "lblSubCmpAccess";
            this.lblSubCmpAccess.Size = new System.Drawing.Size(271, 13);
            this.lblSubCmpAccess.TabIndex = 2;
            this.lblSubCmpAccess.Text = "Select the Sub Companies that this group has access to";
            // 
            // lblOperatorAccess
            // 
            this.lblOperatorAccess.AutoSize = true;
            this.lblOperatorAccess.Location = new System.Drawing.Point(587, 40);
            this.lblOperatorAccess.Name = "lblOperatorAccess";
            this.lblOperatorAccess.Size = new System.Drawing.Size(231, 13);
            this.lblOperatorAccess.TabIndex = 6;
            this.lblOperatorAccess.Text = "Select the Depots that this group has access to";
            // 
            // ChkViewDepots
            // 
            this.ChkViewDepots.AutoSize = true;
            this.ChkViewDepots.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkViewDepots.Location = new System.Drawing.Point(587, 63);
            this.ChkViewDepots.Name = "ChkViewDepots";
            this.ChkViewDepots.Size = new System.Drawing.Size(109, 17);
            this.ChkViewDepots.TabIndex = 7;
            this.ChkViewDepots.Text = "View All Depots ?";
            this.ChkViewDepots.UseVisualStyleBackColor = true;
            this.ChkViewDepots.CheckedChanged += new System.EventHandler(this.ChkViewDepots_CheckedChanged);
            // 
            // lblSiteAccess
            // 
            this.lblSiteAccess.AutoSize = true;
            this.lblSiteAccess.Location = new System.Drawing.Point(295, 40);
            this.lblSiteAccess.Name = "lblSiteAccess";
            this.lblSiteAccess.Size = new System.Drawing.Size(220, 13);
            this.lblSiteAccess.TabIndex = 4;
            this.lblSiteAccess.Text = "Select the Sites that this group has access to";
            // 
            // chkSelectAllSites
            // 
            this.chkSelectAllSites.AutoSize = true;
            this.chkSelectAllSites.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelectAllSites.Location = new System.Drawing.Point(295, 63);
            this.chkSelectAllSites.Name = "chkSelectAllSites";
            this.chkSelectAllSites.Size = new System.Drawing.Size(76, 17);
            this.chkSelectAllSites.TabIndex = 5;
            this.chkSelectAllSites.Text = "Select All?";
            this.chkSelectAllSites.UseVisualStyleBackColor = true;
            this.chkSelectAllSites.Click += new System.EventHandler(this.chkSelectAllSites_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(3, 3);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(100, 28);
            this.btnNewGroup.TabIndex = 0;
            this.btnNewGroup.Text = "&New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // tvSite
            // 
            this.tvSite.CheckBoxes = true;
            this.tvSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSite.Location = new System.Drawing.Point(295, 93);
            this.tvSite.Name = "tvSite";
            this.tvSite.Size = new System.Drawing.Size(286, 282);
            this.tvSite.TabIndex = 9;
            this.tvSite.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSite_AfterCheck);
            // 
            // tvDepots
            // 
            this.tvDepots.CheckBoxes = true;
            this.tvDepots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDepots.Location = new System.Drawing.Point(587, 93);
            this.tvDepots.Name = "tvDepots";
            this.tvDepots.Size = new System.Drawing.Size(287, 282);
            this.tvDepots.TabIndex = 0;
            this.tvDepots.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvDepots_AfterCheck);
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblContainer.Controls.Add(this.tblMainContent, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(883, 424);
            this.tblContainer.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 4;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFooter.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tblFooter.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tblFooter.Controls.Add(this.btnCancel, 3, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(3, 387);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(877, 34);
            this.tblFooter.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnNewGroup);
            this.flowLayoutPanel1.Controls.Add(this.btnSaveGroup);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(559, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(106, 34);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSaveGroup
            // 
            this.btnSaveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveGroup.Location = new System.Drawing.Point(3, 37);
            this.btnSaveGroup.Name = "btnSaveGroup";
            this.btnSaveGroup.Size = new System.Drawing.Size(100, 28);
            this.btnSaveGroup.TabIndex = 14;
            this.btnSaveGroup.Text = "&Add Group";
            this.btnSaveGroup.UseVisualStyleBackColor = true;
            this.btnSaveGroup.Click += new System.EventHandler(this.btnSaveGroup_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnEdit);
            this.flowLayoutPanel2.Controls.Add(this.btnUpdate);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(665, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(106, 34);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(3, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(774, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tblMainContent
            // 
            this.tblMainContent.ColumnCount = 3;
            this.tblMainContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblMainContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblMainContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblMainContent.Controls.Add(this.chkViewAllCompanies, 0, 2);
            this.tblMainContent.Controls.Add(this.tvCompany, 0, 3);
            this.tblMainContent.Controls.Add(this.tvSite, 1, 3);
            this.tblMainContent.Controls.Add(this.tvDepots, 2, 3);
            this.tblMainContent.Controls.Add(this.lblSubCmpAccess, 0, 1);
            this.tblMainContent.Controls.Add(this.ChkViewDepots, 2, 2);
            this.tblMainContent.Controls.Add(this.lblOperatorAccess, 2, 1);
            this.tblMainContent.Controls.Add(this.chkSelectAllSites, 1, 2);
            this.tblMainContent.Controls.Add(this.lblSiteAccess, 1, 1);
            this.tblMainContent.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tblMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainContent.Location = new System.Drawing.Point(3, 3);
            this.tblMainContent.Name = "tblMainContent";
            this.tblMainContent.RowCount = 4;
            this.tblMainContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMainContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblMainContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblMainContent.Size = new System.Drawing.Size(877, 378);
            this.tblMainContent.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.tblMainContent.SetColumnSpan(this.flowLayoutPanel3, 2);
            this.flowLayoutPanel3.Controls.Add(this.cboCustomerAccess);
            this.flowLayoutPanel3.Controls.Add(this.txtNewGroup);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(578, 34);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // txtNewGroup
            // 
            this.txtNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewGroup.Location = new System.Drawing.Point(3, 30);
            this.txtNewGroup.Name = "txtNewGroup";
            this.txtNewGroup.Size = new System.Drawing.Size(575, 20);
            this.txtNewGroup.TabIndex = 10;
            // 
            // cboCustomerAccess
            // 
            this.cboCustomerAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCustomerAccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomerAccess.FormattingEnabled = true;
            this.cboCustomerAccess.Location = new System.Drawing.Point(3, 3);
            this.cboCustomerAccess.Name = "cboCustomerAccess";
            this.cboCustomerAccess.Size = new System.Drawing.Size(575, 21);
            this.cboCustomerAccess.TabIndex = 0;
            this.cboCustomerAccess.SelectedIndexChanged += new System.EventHandler(this.cboCustomerAccess_SelectedIndexChanged);
            // 
            // ucUserSiteAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "ucUserSiteAccess";
            this.Size = new System.Drawing.Size(883, 424);
            this.Load += new System.EventHandler(this.ucUserSiteAccess_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblFooter.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tblMainContent.ResumeLayout(false);
            this.tblMainContent.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

     

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.CheckBox chkViewAllCompanies;
        private System.Windows.Forms.TreeView tvCompany;
        private System.Windows.Forms.Label lblSubCmpAccess;
        private BmcComboBox cboCustomerAccess;//
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.CheckBox ChkViewDepots;
        private System.Windows.Forms.Label lblSiteAccess;
        private System.Windows.Forms.Label lblOperatorAccess;
        private System.Windows.Forms.CheckBox chkSelectAllSites;
        private System.Windows.Forms.TreeView tvSite;
        private System.Windows.Forms.TreeView tvDepots;
        private System.Windows.Forms.TableLayoutPanel tblMainContent;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnSaveGroup;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TextBox txtNewGroup;


    }
}
