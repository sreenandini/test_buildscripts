namespace BMC.EnterpriseClient.Views
{
    partial class frmShareHolderAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShareHolderAddEdit));
            this.txtShareHolderName = new System.Windows.Forms.TextBox();
            this.txtShareHolderDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblShareHolderName = new System.Windows.Forms.Label();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtShareHolderName
            // 
            this.txtShareHolderName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtShareHolderName.Location = new System.Drawing.Point(72, 10);
            this.txtShareHolderName.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.txtShareHolderName.MaxLength = 50;
            this.txtShareHolderName.Name = "txtShareHolderName";
            this.txtShareHolderName.Size = new System.Drawing.Size(236, 20);
            this.txtShareHolderName.TabIndex = 1;
            // 
            // txtShareHolderDescription
            // 
            this.txtShareHolderDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShareHolderDescription.Location = new System.Drawing.Point(72, 43);
            this.txtShareHolderDescription.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.txtShareHolderDescription.MaxLength = 255;
            this.txtShareHolderDescription.Multiline = true;
            this.txtShareHolderDescription.Name = "txtShareHolderDescription";
            this.txtShareHolderDescription.Size = new System.Drawing.Size(407, 72);
            this.txtShareHolderDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(3, 46);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // lblShareHolderName
            // 
            this.lblShareHolderName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblShareHolderName.AutoSize = true;
            this.lblShareHolderName.Location = new System.Drawing.Point(3, 13);
            this.lblShareHolderName.Name = "lblShareHolderName";
            this.lblShareHolderName.Size = new System.Drawing.Size(45, 13);
            this.lblShareHolderName.TabIndex = 0;
            this.lblShareHolderName.Text = "* Name:";
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 4;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 3F));
            this.tblButtons.Controls.Add(this.btnCancel, 2, 0);
            this.tblButtons.Controls.Add(this.btnSave, 1, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblButtons.Location = new System.Drawing.Point(0, 132);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(485, 43);
            this.tblButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(380, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(276, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblShareHolderName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDescription, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtShareHolderName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtShareHolderDescription, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 132);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmShareHolderAddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(485, 175);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tblButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShareHolderAddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Share Holder";
            this.Load += new System.EventHandler(this.frmAddShareHolder_Load);
            this.tblButtons.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtShareHolderName;
        private System.Windows.Forms.TextBox txtShareHolderDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblShareHolderName;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}