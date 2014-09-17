namespace BMC.EnterpriseClient.Views
{
    partial class frmUserGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserGroup));
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddEmpCard = new System.Windows.Forms.Button();
            this.btnCardTypes = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnExchgAdmin = new System.Windows.Forms.Button();
            this.btnRptAdmin = new System.Windows.Forms.Button();
            this.tvUserGroupRoles = new System.Windows.Forms.TreeView();
            this.cmbUserGroupList = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(463, 11);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(81, 21);
            this.btnNewGroup.TabIndex = 10;
            this.btnNewGroup.Text = "New &Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(466, 650);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 40);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            // 
            // btnAddEmpCard
            // 
            this.btnAddEmpCard.Enabled = false;
            this.btnAddEmpCard.Location = new System.Drawing.Point(277, 650);
            this.btnAddEmpCard.Name = "btnAddEmpCard";
            this.btnAddEmpCard.Size = new System.Drawing.Size(86, 40);
            this.btnAddEmpCard.TabIndex = 16;
            this.btnAddEmpCard.Text = "&New Employee Card";
            this.btnAddEmpCard.UseVisualStyleBackColor = true;
            // 
            // btnCardTypes
            // 
            this.btnCardTypes.Enabled = false;
            this.btnCardTypes.Location = new System.Drawing.Point(199, 650);
            this.btnCardTypes.Name = "btnCardTypes";
            this.btnCardTypes.Size = new System.Drawing.Size(73, 40);
            this.btnCardTypes.TabIndex = 17;
            this.btnCardTypes.Text = "Car&d Types";
            this.btnCardTypes.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCardTypes);
            this.panel1.Controls.Add(this.btnAddEmpCard);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.btnExchgAdmin);
            this.panel1.Controls.Add(this.btnRptAdmin);
            this.panel1.Controls.Add(this.tvUserGroupRoles);
            this.panel1.Controls.Add(this.btnNewGroup);
            this.panel1.Controls.Add(this.cmbUserGroupList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 700);
            this.panel1.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(379, 650);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(81, 40);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Ed&it";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click_1);
            // 
            // btnExchgAdmin
            // 
            this.btnExchgAdmin.Enabled = false;
            this.btnExchgAdmin.Location = new System.Drawing.Point(98, 650);
            this.btnExchgAdmin.Name = "btnExchgAdmin";
            this.btnExchgAdmin.Size = new System.Drawing.Size(81, 40);
            this.btnExchgAdmin.TabIndex = 13;
            this.btnExchgAdmin.Text = "Exc&hange Admin";
            this.btnExchgAdmin.UseVisualStyleBackColor = true;
            // 
            // btnRptAdmin
            // 
            this.btnRptAdmin.Enabled = false;
            this.btnRptAdmin.Location = new System.Drawing.Point(11, 650);
            this.btnRptAdmin.Name = "btnRptAdmin";
            this.btnRptAdmin.Size = new System.Drawing.Size(81, 40);
            this.btnRptAdmin.TabIndex = 12;
            this.btnRptAdmin.Text = "&Report Admin";
            this.btnRptAdmin.UseVisualStyleBackColor = true;
            // 
            // tvUserGroupRoles
            // 
            this.tvUserGroupRoles.CheckBoxes = true;
            this.tvUserGroupRoles.Enabled = false;
            this.tvUserGroupRoles.Location = new System.Drawing.Point(7, 35);
            this.tvUserGroupRoles.Name = "tvUserGroupRoles";
            this.tvUserGroupRoles.Size = new System.Drawing.Size(537, 609);
            this.tvUserGroupRoles.TabIndex = 11;
            // 
            // cmbUserGroupList
            // 
            this.cmbUserGroupList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbUserGroupList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserGroupList.FormattingEnabled = true;
            this.cmbUserGroupList.Location = new System.Drawing.Point(7, 11);
            this.cmbUserGroupList.Name = "cmbUserGroupList";
            this.cmbUserGroupList.Size = new System.Drawing.Size(289, 21);
            this.cmbUserGroupList.TabIndex = 9;
            this.cmbUserGroupList.SelectedIndexChanged += new System.EventHandler(this.cmbUserGroupList_SelectedIndexChanged);
            // 
            // frmUserGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 700);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User Group Administration";
            this.Load += new System.EventHandler(this.frmUserGroup_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BMC.EnterpriseClient.Helpers.BmcComboBox cmbUserGroupList;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddEmpCard;
        private System.Windows.Forms.Button btnCardTypes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnExchgAdmin;
        private System.Windows.Forms.Button btnRptAdmin;
        private System.Windows.Forms.TreeView tvUserGroupRoles;



    }
}