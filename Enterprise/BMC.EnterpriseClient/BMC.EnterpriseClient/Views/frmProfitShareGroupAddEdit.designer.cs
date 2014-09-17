namespace BMC.EnterpriseClient.Views
{
    partial class frmProfitShareGroupAddEdit
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
            this.components = new System.ComponentModel.Container();
            this.txtProfitShareGroupName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grdvwProfitShareGroup = new System.Windows.Forms.DataGridView();
            this.cmsProfitShareGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsprofitsharegroupAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsprofitsharegroupEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsprofitsharegroupDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.txtProfitShareGroupDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpContainer = new System.Windows.Forms.GroupBox();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ShareHolderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitSharePercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitShareGroupDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitShareId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShareHolderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdvwProfitShareGroup)).BeginInit();
            this.cmsProfitShareGroup.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.grpContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProfitShareGroupName
            // 
            this.txtProfitShareGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfitShareGroupName.Location = new System.Drawing.Point(106, 10);
            this.txtProfitShareGroupName.Name = "txtProfitShareGroupName";
            this.txtProfitShareGroupName.Size = new System.Drawing.Size(629, 20);
            this.txtProfitShareGroupName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Name :";
            // 
            // grdvwProfitShareGroup
            // 
            this.grdvwProfitShareGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdvwProfitShareGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdvwProfitShareGroup.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdvwProfitShareGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdvwProfitShareGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit,
            this.Delete,
            this.ShareHolderName,
            this.ProfitSharePercentage,
            this.ProfitShareGroupDescription,
            this.ProfitShareId,
            this.ShareHolderId});
            this.tblContent.SetColumnSpan(this.grdvwProfitShareGroup, 2);
            this.grdvwProfitShareGroup.ContextMenuStrip = this.cmsProfitShareGroup;
            this.grdvwProfitShareGroup.Location = new System.Drawing.Point(3, 83);
            this.grdvwProfitShareGroup.Name = "grdvwProfitShareGroup";
            this.grdvwProfitShareGroup.ReadOnly = true;
            this.grdvwProfitShareGroup.RowHeadersVisible = false;
            this.grdvwProfitShareGroup.Size = new System.Drawing.Size(732, 384);
            this.grdvwProfitShareGroup.TabIndex = 11;
            this.grdvwProfitShareGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvwProfitShareGroup_CellClick);
            // 
            // cmsProfitShareGroup
            // 
            this.cmsProfitShareGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsprofitsharegroupAdd,
            this.cmsprofitsharegroupEdit,
            this.cmsprofitsharegroupDelete});
            this.cmsProfitShareGroup.Name = "cmsprofitsharegroup";
            this.cmsProfitShareGroup.Size = new System.Drawing.Size(106, 70);
            // 
            // cmsprofitsharegroupAdd
            // 
            this.cmsprofitsharegroupAdd.Name = "cmsprofitsharegroupAdd";
            this.cmsprofitsharegroupAdd.Size = new System.Drawing.Size(105, 22);
            this.cmsprofitsharegroupAdd.Text = "Add";
            // 
            // cmsprofitsharegroupEdit
            // 
            this.cmsprofitsharegroupEdit.Name = "cmsprofitsharegroupEdit";
            this.cmsprofitsharegroupEdit.Size = new System.Drawing.Size(105, 22);
            this.cmsprofitsharegroupEdit.Text = "Edit";
            // 
            // cmsprofitsharegroupDelete
            // 
            this.cmsprofitsharegroupDelete.Name = "cmsprofitsharegroupDelete";
            this.cmsprofitsharegroupDelete.Size = new System.Drawing.Size(105, 22);
            this.cmsprofitsharegroupDelete.Text = "Delete";
            // 
            // txtProfitShareGroupDescription
            // 
            this.txtProfitShareGroupDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfitShareGroupDescription.Location = new System.Drawing.Point(106, 50);
            this.txtProfitShareGroupDescription.Name = "txtProfitShareGroupDescription";
            this.txtProfitShareGroupDescription.Size = new System.Drawing.Size(629, 20);
            this.txtProfitShareGroupDescription.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 53);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 15;
            this.lblDescription.Text = "Description :";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(557, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 5;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.Controls.Add(this.btnSave, 3, 0);
            this.tblButtons.Controls.Add(this.btnAdd, 1, 0);
            this.tblButtons.Controls.Add(this.btnCancel, 4, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblButtons.Location = new System.Drawing.Point(0, 501);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(760, 40);
            this.tblButtons.TabIndex = 13;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(15, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(97, 28);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(660, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpContainer);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(5, 6, 5, 0);
            this.pnlContainer.Size = new System.Drawing.Size(760, 501);
            this.pnlContainer.TabIndex = 14;
            // 
            // grpContainer
            // 
            this.grpContainer.Controls.Add(this.tblContent);
            this.grpContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContainer.Location = new System.Drawing.Point(5, 6);
            this.grpContainer.Name = "grpContainer";
            this.grpContainer.Padding = new System.Windows.Forms.Padding(6);
            this.grpContainer.Size = new System.Drawing.Size(750, 495);
            this.grpContainer.TabIndex = 0;
            this.grpContainer.TabStop = false;
            this.grpContainer.Text = "Profit Share Group";
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.txtProfitShareGroupName, 1, 0);
            this.tblContent.Controls.Add(this.txtProfitShareGroupDescription, 1, 1);
            this.tblContent.Controls.Add(this.lblName, 0, 0);
            this.tblContent.Controls.Add(this.lblDescription, 0, 1);
            this.tblContent.Controls.Add(this.grdvwProfitShareGroup, 0, 2);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(6, 19);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 3;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContent.Size = new System.Drawing.Size(738, 470);
            this.tblContent.TabIndex = 0;
            // 
            // Edit
            // 
            this.Edit.ContextMenuStrip = this.cmsProfitShareGroup;
            this.Edit.HeaderText = "Edit";
            this.Edit.Name = "Edit";
            this.Edit.ReadOnly = true;
            this.Edit.Text = "...";
            this.Edit.UseColumnTextForButtonValue = true;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "Delete";
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Text = "...";
            this.Delete.UseColumnTextForButtonValue = true;
            // 
            // ShareHolderName
            // 
            this.ShareHolderName.DataPropertyName = "ShareHolderName";
            this.ShareHolderName.HeaderText = "Share Holder";
            this.ShareHolderName.Name = "ShareHolderName";
            this.ShareHolderName.ReadOnly = true;
            // 
            // ProfitSharePercentage
            // 
            this.ProfitSharePercentage.DataPropertyName = "ProfitSharePercentage";
            this.ProfitSharePercentage.HeaderText = "Percentage";
            this.ProfitSharePercentage.Name = "ProfitSharePercentage";
            this.ProfitSharePercentage.ReadOnly = true;
            // 
            // ProfitShareGroupDescription
            // 
            this.ProfitShareGroupDescription.DataPropertyName = "ProfitShareDescription";
            this.ProfitShareGroupDescription.HeaderText = "Description";
            this.ProfitShareGroupDescription.Name = "ProfitShareGroupDescription";
            this.ProfitShareGroupDescription.ReadOnly = true;
            // 
            // ProfitShareId
            // 
            this.ProfitShareId.DataPropertyName = "ProfitShareId";
            this.ProfitShareId.HeaderText = "ProfitShareId";
            this.ProfitShareId.Name = "ProfitShareId";
            this.ProfitShareId.ReadOnly = true;
            this.ProfitShareId.Visible = false;
            // 
            // ShareHolderId
            // 
            this.ShareHolderId.DataPropertyName = "ShareHolderId";
            this.ShareHolderId.HeaderText = "ShareHolderId";
            this.ShareHolderId.Name = "ShareHolderId";
            this.ShareHolderId.ReadOnly = true;
            this.ShareHolderId.Visible = false;
            // 
            // frmProfitShareGroupAddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 541);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.tblButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmProfitShareGroupAddEdit";
            this.Text = "Profit Share Group";
            this.Load += new System.EventHandler(this.frmProfitShareGroup_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProfitShareGroup_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdvwProfitShareGroup)).EndInit();
            this.cmsProfitShareGroup.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.grpContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtProfitShareGroupName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DataGridView grdvwProfitShareGroup;
        private System.Windows.Forms.ContextMenuStrip cmsProfitShareGroup;
        public System.Windows.Forms.ToolStripMenuItem cmsprofitsharegroupAdd;
        private System.Windows.Forms.ToolStripMenuItem cmsprofitsharegroupEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsprofitsharegroupDelete;
        private System.Windows.Forms.TextBox txtProfitShareGroupDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.GroupBox grpContainer;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShareHolderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitSharePercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitShareGroupDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitShareId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShareHolderId;


    }
}