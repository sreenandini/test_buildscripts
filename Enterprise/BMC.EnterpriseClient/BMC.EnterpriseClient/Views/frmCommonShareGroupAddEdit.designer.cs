namespace BMC.EnterpriseClient.Views
{
    partial class frmCommonShareGroupAddEdit
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grdItems = new System.Windows.Forms.DataGridView();
            this.chdrEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chdrDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.grpContainer = new System.Windows.Forms.GroupBox();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.tblButtons.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.grpContainer.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtName.Location = new System.Drawing.Point(73, 10);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(311, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(3, 13);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "* Name:";
            // 
            // grdItems
            // 
            this.grdItems.AllowUserToAddRows = false;
            this.grdItems.AllowUserToDeleteRows = false;
            this.grdItems.AllowUserToResizeRows = false;
            this.grdItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdItems.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chdrEdit,
            this.chdrDelete});
            this.tblContent.SetColumnSpan(this.grdItems, 2);
            this.grdItems.Location = new System.Drawing.Point(3, 79);
            this.grdItems.Name = "grdItems";
            this.grdItems.ReadOnly = true;
            this.grdItems.RowHeadersVisible = false;
            this.grdItems.Size = new System.Drawing.Size(732, 388);
            this.grdItems.TabIndex = 4;
            this.grdItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdItems_CellClick);
            // 
            // chdrEdit
            // 
            this.chdrEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chdrEdit.HeaderText = "Edit";
            this.chdrEdit.Name = "chdrEdit";
            this.chdrEdit.ReadOnly = true;
            this.chdrEdit.Text = "...";
            this.chdrEdit.UseColumnTextForButtonValue = true;
            this.chdrEdit.Width = 60;
            // 
            // chdrDelete
            // 
            this.chdrDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chdrDelete.HeaderText = "Delete";
            this.chdrDelete.Name = "chdrDelete";
            this.chdrDelete.ReadOnly = true;
            this.chdrDelete.Text = "...";
            this.chdrDelete.UseColumnTextForButtonValue = true;
            this.chdrDelete.Width = 60;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(73, 43);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(662, 30);
            this.txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 51);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(537, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 6;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblButtons.Controls.Add(this.btnSave, 3, 0);
            this.tblButtons.Controls.Add(this.btnAdd, 1, 0);
            this.tblButtons.Controls.Add(this.btnCancel, 4, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblButtons.Location = new System.Drawing.Point(0, 501);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(760, 40);
            this.tblButtons.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(15, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(97, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(640, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 2;
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
            this.pnlContainer.TabIndex = 0;
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
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.txtName, 1, 0);
            this.tblContent.Controls.Add(this.lblName, 0, 0);
            this.tblContent.Controls.Add(this.grdItems, 0, 2);
            this.tblContent.Controls.Add(this.lblDescription, 0, 1);
            this.tblContent.Controls.Add(this.txtDescription, 1, 1);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(6, 19);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 3;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContent.Size = new System.Drawing.Size(738, 470);
            this.tblContent.TabIndex = 0;
            // 
            // frmCommonShareGroupAddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(760, 541);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.tblButtons);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "frmCommonShareGroupAddEdit";
            this.ShowInTaskbar = false;
            this.Text = "Profit Share Group";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCommonShareGroupAddEdit_FormClosing);
            this.Load += new System.EventHandler(this.frmCommonShareGroupAddEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.tblButtons.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            this.grpContainer.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.DataGridView grdItems;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.GroupBox grpContainer;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.DataGridViewButtonColumn chdrEdit;
        private System.Windows.Forms.DataGridViewButtonColumn chdrDelete;


    }
}