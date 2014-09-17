namespace BMC.EnterpriseClient.Views
{
    partial class frmUserAdministration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserAdministration));
            this.grpControls = new System.Windows.Forms.GroupBox();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.grbUserAdministration = new System.Windows.Forms.GroupBox();
            this.tsUserAdministration = new System.Windows.Forms.ToolStrip();
            this.tsbtnUserRoles = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUserSiteAccess = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUsers = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAssignSites = new System.Windows.Forms.ToolStripButton();
            this.tblFooter = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblContainer.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.grbUserAdministration.SuspendLayout();
            this.tsUserAdministration.SuspendLayout();
            this.tblFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpControls
            // 
            this.grpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControls.Location = new System.Drawing.Point(3, 3);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(646, 504);
            this.grpControls.TabIndex = 0;
            this.grpControls.TabStop = false;
            this.grpControls.Text = " ";
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Controls.Add(this.tblHeader, 0, 0);
            this.tblContainer.Controls.Add(this.tblFooter, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(784, 562);
            this.tblContainer.TabIndex = 0;
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 2;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tblHeader.Controls.Add(this.grbUserAdministration, 0, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(3, 3);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(778, 516);
            this.tblHeader.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.grpControls, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(123, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 510F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(652, 510);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // grbUserAdministration
            // 
            this.grbUserAdministration.Controls.Add(this.tsUserAdministration);
            this.grbUserAdministration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbUserAdministration.Location = new System.Drawing.Point(3, 3);
            this.grbUserAdministration.Name = "grbUserAdministration";
            this.grbUserAdministration.Size = new System.Drawing.Size(114, 510);
            this.grbUserAdministration.TabIndex = 1;
            this.grbUserAdministration.TabStop = false;
            // 
            // tsUserAdministration
            // 
            this.tsUserAdministration.BackColor = System.Drawing.SystemColors.Control;
            this.tsUserAdministration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsUserAdministration.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsUserAdministration.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tsUserAdministration.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnUserRoles,
            this.tsbtnUserSiteAccess,
            this.tsbtnUsers,
            this.tsbtnAssignSites});
            this.tsUserAdministration.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsUserAdministration.Location = new System.Drawing.Point(3, 16);
            this.tsUserAdministration.Name = "tsUserAdministration";
            this.tsUserAdministration.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsUserAdministration.Size = new System.Drawing.Size(108, 491);
            this.tsUserAdministration.TabIndex = 0;
            this.tsUserAdministration.Text = "toolStrip1";
            // 
            // tsbtnUserRoles
            // 
            this.tsbtnUserRoles.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbtnUserRoles.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUserRoles.Image")));
            this.tsbtnUserRoles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnUserRoles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUserRoles.Name = "tsbtnUserRoles";
            this.tsbtnUserRoles.Size = new System.Drawing.Size(106, 89);
            this.tsbtnUserRoles.Text = "User Roles";
            this.tsbtnUserRoles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnUserRoles.Click += new System.EventHandler(this.tsbtnUserRoles_Click);
            // 
            // tsbtnUserSiteAccess
            // 
            this.tsbtnUserSiteAccess.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUserSiteAccess.Image")));
            this.tsbtnUserSiteAccess.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnUserSiteAccess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUserSiteAccess.Name = "tsbtnUserSiteAccess";
            this.tsbtnUserSiteAccess.Size = new System.Drawing.Size(106, 91);
            this.tsbtnUserSiteAccess.Text = "User Site Access";
            this.tsbtnUserSiteAccess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnUserSiteAccess.Click += new System.EventHandler(this.tsbtnUserSiteAccess_Click);
            // 
            // tsbtnUsers
            // 
            this.tsbtnUsers.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUsers.Image")));
            this.tsbtnUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUsers.Name = "tsbtnUsers";
            this.tsbtnUsers.Size = new System.Drawing.Size(106, 91);
            this.tsbtnUsers.Text = "Users";
            this.tsbtnUsers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnUsers.Click += new System.EventHandler(this.tsbtnUsers_Click);
            // 
            // tsbtnAssignSites
            // 
            this.tsbtnAssignSites.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAssignSites.Image")));
            this.tsbtnAssignSites.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAssignSites.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAssignSites.Name = "tsbtnAssignSites";
            this.tsbtnAssignSites.Size = new System.Drawing.Size(106, 91);
            this.tsbtnAssignSites.Text = "Assign Sites";
            this.tsbtnAssignSites.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAssignSites.Click += new System.EventHandler(this.tsbtnAssignSites_Click);
            // 
            // tblFooter
            // 
            this.tblFooter.ColumnCount = 2;
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblFooter.Controls.Add(this.btnClose, 1, 0);
            this.tblFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFooter.Location = new System.Drawing.Point(3, 525);
            this.tblFooter.Name = "tblFooter";
            this.tblFooter.RowCount = 1;
            this.tblFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFooter.Size = new System.Drawing.Size(778, 34);
            this.tblFooter.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(675, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmUserAdministration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmUserAdministration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserAdministration";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmUserAdministration_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.grbUserAdministration.ResumeLayout(false);
            this.grbUserAdministration.PerformLayout();
            this.tsUserAdministration.ResumeLayout(false);
            this.tsUserAdministration.PerformLayout();
            this.tblFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      

        #endregion

        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.ToolStrip tsUserAdministration;
        private System.Windows.Forms.ToolStripButton tsbtnUserRoles;
        private System.Windows.Forms.ToolStripButton tsbtnUserSiteAccess;
        private System.Windows.Forms.ToolStripButton tsbtnUsers;
        private System.Windows.Forms.ToolStripButton tsbtnAssignSites;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tblFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grbUserAdministration;




    }
}