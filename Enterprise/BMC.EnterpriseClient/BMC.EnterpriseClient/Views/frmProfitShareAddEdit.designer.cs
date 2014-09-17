namespace BMC.EnterpriseClient.Views
{
    partial class frmProfitShareAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfitShareAddEdit));
            this.lblMaxPerAllowed = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSharePercentage = new System.Windows.Forms.NumericUpDown();
            this.lblSharePercentage = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblShareHolderName = new System.Windows.Forms.Label();
            this.cmbShareHolderName = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tblButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSharePercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMaxPerAllowed
            // 
            this.lblMaxPerAllowed.AutoSize = true;
            this.lblMaxPerAllowed.Location = new System.Drawing.Point(491, 226);
            this.lblMaxPerAllowed.Name = "lblMaxPerAllowed";
            this.lblMaxPerAllowed.Size = new System.Drawing.Size(35, 13);
            this.lblMaxPerAllowed.TabIndex = 11;
            this.lblMaxPerAllowed.Text = "label1";
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 3;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblButtons.Controls.Add(this.btnCancel, 2, 0);
            this.tblButtons.Controls.Add(this.btnSave, 1, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblButtons.Location = new System.Drawing.Point(0, 158);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(468, 43);
            this.tblButtons.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(371, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 28);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(271, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 28);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtSharePercentage, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSharePercentage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblDescription, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblShareHolderName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbShareHolderName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDescription, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 158);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // txtSharePercentage
            // 
            this.txtSharePercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSharePercentage.Location = new System.Drawing.Point(143, 50);
            this.txtSharePercentage.Name = "txtSharePercentage";
            this.txtSharePercentage.Size = new System.Drawing.Size(322, 20);
            this.txtSharePercentage.TabIndex = 1;
            // 
            // lblSharePercentage
            // 
            this.lblSharePercentage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSharePercentage.AutoSize = true;
            this.lblSharePercentage.Location = new System.Drawing.Point(3, 53);
            this.lblSharePercentage.Name = "lblSharePercentage";
            this.lblSharePercentage.Size = new System.Drawing.Size(68, 13);
            this.lblSharePercentage.TabIndex = 7;
            this.lblSharePercentage.Text = "&Percentage :";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 86);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(66, 13);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "&Description :";
            // 
            // lblShareHolderName
            // 
            this.lblShareHolderName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShareHolderName.AutoSize = true;
            this.lblShareHolderName.Location = new System.Drawing.Point(3, 13);
            this.lblShareHolderName.Name = "lblShareHolderName";
            this.lblShareHolderName.Size = new System.Drawing.Size(75, 13);
            this.lblShareHolderName.TabIndex = 5;
            this.lblShareHolderName.Text = "Share &Holder :";
            // 
            // cmbShareHolderName
            // 
            this.cmbShareHolderName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbShareHolderName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShareHolderName.FormattingEnabled = true;
            this.cmbShareHolderName.Location = new System.Drawing.Point(143, 9);
            this.cmbShareHolderName.Name = "cmbShareHolderName";
            this.cmbShareHolderName.Size = new System.Drawing.Size(322, 21);
            this.cmbShareHolderName.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(143, 83);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(319, 72);
            this.txtDescription.TabIndex = 11;
            // 
            // frmProfitShareAddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(468, 201);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tblButtons);
            this.Controls.Add(this.lblMaxPerAllowed);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProfitShareAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Profit Share Holder";
            this.Load += new System.EventHandler(this.frmAddEditProfitShareHolder_Load);
            this.tblButtons.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSharePercentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaxPerAllowed;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbShareHolderName;
        private System.Windows.Forms.NumericUpDown txtSharePercentage;
        private System.Windows.Forms.Label lblSharePercentage;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblShareHolderName;
        private System.Windows.Forms.TextBox txtDescription;
    }
}