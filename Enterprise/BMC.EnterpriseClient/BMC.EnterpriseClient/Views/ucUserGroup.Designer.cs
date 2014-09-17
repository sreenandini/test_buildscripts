namespace BMC.EnterpriseClient
{
    partial class ucUserGroup
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnExchgAdmin = new System.Windows.Forms.Button();
            this.tvwUserGroupRoles = new System.Windows.Forms.TreeView();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.cboUserGroupList = new System.Windows.Forms.ComboBox();
            this.tblUserGroupContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblNewGroup = new System.Windows.Forms.TableLayoutPanel();
            this.tblNewGroupChild = new System.Windows.Forms.TableLayoutPanel();
            this.fpnlAddNewGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.txtNewGroup = new System.Windows.Forms.TextBox();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.fpnlFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAssociateMode = new System.Windows.Forms.Button();
            this.btnRptAdmin = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.tblUserGroupContainer.SuspendLayout();
            this.tblNewGroup.SuspendLayout();
            this.tblNewGroupChild.SuspendLayout();
            this.fpnlAddNewGroup.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.fpnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1082, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(575, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "&Edit Group";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnExchgAdmin
            // 
            this.btnExchgAdmin.Enabled = false;
            this.btnExchgAdmin.Location = new System.Drawing.Point(681, 3);
            this.btnExchgAdmin.Name = "btnExchgAdmin";
            this.btnExchgAdmin.Size = new System.Drawing.Size(100, 28);
            this.btnExchgAdmin.TabIndex = 3;
            this.btnExchgAdmin.Text = "E&xchange Admin";
            this.btnExchgAdmin.UseVisualStyleBackColor = true;
            this.btnExchgAdmin.Click += new System.EventHandler(this.btnExchgAdmin_Click);
            // 
            // tvwUserGroupRoles
            // 
            this.tvwUserGroupRoles.CheckBoxes = true;
            this.tvwUserGroupRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwUserGroupRoles.Enabled = false;
            this.tvwUserGroupRoles.Location = new System.Drawing.Point(3, 43);
            this.tvwUserGroupRoles.Name = "tvwUserGroupRoles";
            this.tvwUserGroupRoles.Size = new System.Drawing.Size(1173, 708);
            this.tvwUserGroupRoles.TabIndex = 0;
            this.tvwUserGroupRoles.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvUserGroupRoles_BeforeCheck);
            this.tvwUserGroupRoles.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvUserGroupRoles_AfterCheck);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(363, 3);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(100, 28);
            this.btnNewGroup.TabIndex = 0;
            this.btnNewGroup.Text = "&New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // cboUserGroupList
            // 
            this.cboUserGroupList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUserGroupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUserGroupList.FormattingEnabled = true;
            this.cboUserGroupList.Location = new System.Drawing.Point(558, 3);
            this.cboUserGroupList.Name = "cboUserGroupList";
            this.cboUserGroupList.Size = new System.Drawing.Size(549, 21);
            this.cboUserGroupList.TabIndex = 1;
            this.cboUserGroupList.SelectedIndexChanged += new System.EventHandler(this.cmbUserGroupList_SelectedIndexChanged);
            // 
            // tblUserGroupContainer
            // 
            this.tblUserGroupContainer.ColumnCount = 1;
            this.tblUserGroupContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblUserGroupContainer.Controls.Add(this.tblNewGroup, 0, 0);
            this.tblUserGroupContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblUserGroupContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblUserGroupContainer.Location = new System.Drawing.Point(0, 0);
            this.tblUserGroupContainer.Name = "tblUserGroupContainer";
            this.tblUserGroupContainer.RowCount = 2;
            this.tblUserGroupContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblUserGroupContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblUserGroupContainer.Size = new System.Drawing.Size(1185, 800);
            this.tblUserGroupContainer.TabIndex = 0;
            // 
            // tblNewGroup
            // 
            this.tblNewGroup.ColumnCount = 1;
            this.tblNewGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblNewGroup.Controls.Add(this.tvwUserGroupRoles, 0, 1);
            this.tblNewGroup.Controls.Add(this.tblNewGroupChild, 0, 0);
            this.tblNewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblNewGroup.Location = new System.Drawing.Point(3, 3);
            this.tblNewGroup.Name = "tblNewGroup";
            this.tblNewGroup.RowCount = 2;
            this.tblNewGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblNewGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblNewGroup.Size = new System.Drawing.Size(1179, 754);
            this.tblNewGroup.TabIndex = 0;
            // 
            // tblNewGroupChild
            // 
            this.tblNewGroupChild.ColumnCount = 2;
            this.tblNewGroupChild.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblNewGroupChild.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblNewGroupChild.Controls.Add(this.fpnlAddNewGroup, 0, 0);
            this.tblNewGroupChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblNewGroupChild.Location = new System.Drawing.Point(3, 3);
            this.tblNewGroupChild.Name = "tblNewGroupChild";
            this.tblNewGroupChild.RowCount = 1;
            this.tblNewGroupChild.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblNewGroupChild.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tblNewGroupChild.Size = new System.Drawing.Size(1173, 34);
            this.tblNewGroupChild.TabIndex = 1;
            // 
            // fpnlAddNewGroup
            // 
            this.tblNewGroupChild.SetColumnSpan(this.fpnlAddNewGroup, 2);
            this.fpnlAddNewGroup.Controls.Add(this.txtNewGroup);
            this.fpnlAddNewGroup.Controls.Add(this.cboUserGroupList);
            this.fpnlAddNewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnlAddNewGroup.Location = new System.Drawing.Point(3, 3);
            this.fpnlAddNewGroup.Name = "fpnlAddNewGroup";
            this.fpnlAddNewGroup.Size = new System.Drawing.Size(1167, 28);
            this.fpnlAddNewGroup.TabIndex = 0;
            // 
            // txtNewGroup
            // 
            this.txtNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewGroup.Location = new System.Drawing.Point(3, 3);
            this.txtNewGroup.Name = "txtNewGroup";
            this.txtNewGroup.Size = new System.Drawing.Size(549, 20);
            this.txtNewGroup.TabIndex = 0;
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 7;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 217F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.Controls.Add(this.fpnlFooter, 0, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(0, 760);
            this.tblFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(1185, 40);
            this.tblFooter.TabIndex = 4;
            // 
            // fpnlFooter
            // 
            this.tblFooter.SetColumnSpan(this.fpnlFooter, 7);
            this.fpnlFooter.Controls.Add(this.btnCancel);
            this.fpnlFooter.Controls.Add(this.btnAssociateMode);
            this.fpnlFooter.Controls.Add(this.btnRptAdmin);
            this.fpnlFooter.Controls.Add(this.btnExchgAdmin);
            this.fpnlFooter.Controls.Add(this.btnEdit);
            this.fpnlFooter.Controls.Add(this.btnAddGroup);
            this.fpnlFooter.Controls.Add(this.btnNewGroup);
            this.fpnlFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpnlFooter.Location = new System.Drawing.Point(0, 0);
            this.fpnlFooter.Margin = new System.Windows.Forms.Padding(0);
            this.fpnlFooter.Name = "fpnlFooter";
            this.fpnlFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fpnlFooter.Size = new System.Drawing.Size(1185, 40);
            this.fpnlFooter.TabIndex = 1;
            // 
            // btnAssociateMode
            // 
            this.btnAssociateMode.Enabled = false;
            this.btnAssociateMode.Location = new System.Drawing.Point(893, 3);
            this.btnAssociateMode.Name = "btnAssociateMode";
            this.btnAssociateMode.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnAssociateMode.Size = new System.Drawing.Size(183, 28);
            this.btnAssociateMode.TabIndex = 5;
            this.btnAssociateMode.Text = "New E&mployee Card";
            this.btnAssociateMode.UseVisualStyleBackColor = true;
            this.btnAssociateMode.Click += new System.EventHandler(this.btnAddEmpCard_Click);
            // 
            // btnRptAdmin
            // 
            this.btnRptAdmin.Enabled = false;
            this.btnRptAdmin.Location = new System.Drawing.Point(787, 3);
            this.btnRptAdmin.Name = "btnRptAdmin";
            this.btnRptAdmin.Size = new System.Drawing.Size(100, 28);
            this.btnRptAdmin.TabIndex = 4;
            this.btnRptAdmin.Text = "&Report Admin";
            this.btnRptAdmin.UseVisualStyleBackColor = true;
            this.btnRptAdmin.Click += new System.EventHandler(this.btnRptAdmin_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(469, 3);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(100, 28);
            this.btnAddGroup.TabIndex = 1;
            this.btnAddGroup.Text = "&Add Group";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // ucUserGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tblUserGroupContainer);
            this.Name = "ucUserGroup";
            this.Size = new System.Drawing.Size(1185, 800);
            this.Load += new System.EventHandler(this.ucUserGroup_Load);
            this.tblUserGroupContainer.ResumeLayout(false);
            this.tblNewGroup.ResumeLayout(false);
            this.tblNewGroupChild.ResumeLayout(false);
            this.fpnlAddNewGroup.ResumeLayout(false);
            this.fpnlAddNewGroup.PerformLayout();
            this.tblFooter.ResumeLayout(false);
            this.fpnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnExchgAdmin;
        private System.Windows.Forms.TreeView tvwUserGroupRoles;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.ComboBox cboUserGroupList;
        private System.Windows.Forms.TableLayoutPanel tblUserGroupContainer;
        private System.Windows.Forms.TableLayoutPanel tblNewGroup;
        private System.Windows.Forms.FlowLayoutPanel fpnlAddNewGroup;
        private System.Windows.Forms.TextBox txtNewGroup;
        private System.Windows.Forms.TableLayoutPanel tblNewGroupChild;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Button btnRptAdmin;
        private System.Windows.Forms.Button btnAssociateMode;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.FlowLayoutPanel fpnlFooter;
    }
}
