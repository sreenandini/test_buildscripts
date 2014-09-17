namespace BMC.EnterpriseClient.Views
{
    partial class frmStackerDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStackerDetails));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.dgvbtnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvbtnDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnMannualDropAlert = new System.Windows.Forms.Button();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.tblContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dgvDetails);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContent.Size = new System.Drawing.Size(778, 516);
            this.pnlContent.TabIndex = 0;
            // 
            // dgvDetails
            // 
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvbtnEdit,
            this.dgvbtnDelete});
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.Location = new System.Drawing.Point(3, 3);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.Size = new System.Drawing.Size(772, 510);
            this.dgvDetails.TabIndex = 0;
            this.dgvDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStackerDetails_CellClick);
            // 
            // dgvbtnEdit
            // 
            this.dgvbtnEdit.HeaderText = "Edit";
            this.dgvbtnEdit.Name = "dgvbtnEdit";
            this.dgvbtnEdit.ReadOnly = true;
            this.dgvbtnEdit.Text = "...";
            this.dgvbtnEdit.UseColumnTextForButtonValue = true;
            // 
            // dgvbtnDelete
            // 
            this.dgvbtnDelete.HeaderText = "Delete";
            this.dgvbtnDelete.Name = "dgvbtnDelete";
            this.dgvbtnDelete.ReadOnly = true;
            this.dgvbtnDelete.Text = "...";
            this.dgvbtnDelete.UseColumnTextForButtonValue = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.Location = new System.Drawing.Point(587, 5);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(94, 29);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "&Add";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnMannualDropAlert
            // 
            this.btnMannualDropAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMannualDropAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMannualDropAlert.Location = new System.Drawing.Point(3, 6);
            this.btnMannualDropAlert.Name = "btnMannualDropAlert";
            this.btnMannualDropAlert.Size = new System.Drawing.Size(114, 28);
            this.btnMannualDropAlert.TabIndex = 0;
            this.btnMannualDropAlert.Text = "&Manual Drop Alert";
            this.btnMannualDropAlert.UseVisualStyleBackColor = true;
            this.btnMannualDropAlert.Click += new System.EventHandler(this.btnMannualDropAlert_Click);
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.pnlContent, 0, 0);
            this.tblContainer.Controls.Add(this.tblButtons, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(784, 562);
            this.tblContainer.TabIndex = 0;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 4;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButtons.Controls.Add(this.btnMannualDropAlert, 0, 0);
            this.tblButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblButtons.Controls.Add(this.btnAddNew, 2, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(0, 522);
            this.tblButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(784, 40);
            this.tblButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(687, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmStackerDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmStackerDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stacker Details";
            this.Load += new System.EventHandler(this.StackerDetails_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmStackerDetails_KeyDown);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.tblContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridViewButtonColumn dgvbtnEdit;
        private System.Windows.Forms.DataGridViewButtonColumn dgvbtnDelete;
        private System.Windows.Forms.Button btnMannualDropAlert;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnClose;
        //private System.Windows.Forms.DataGridViewTextBoxColumn StackerName;
        //private System.Windows.Forms.DataGridViewTextBoxColumn StackerSize;
        //private System.Windows.Forms.DataGridViewTextBoxColumn StackerDescription;

    }
}

