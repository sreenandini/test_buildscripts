namespace BMC.EnterpriseClient.Views
{
    partial class frmVaultTransactionReason
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
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_reason = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dview_reason = new System.Windows.Forms.DataGridView();
            this.clmSno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmReasonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTransactionReason = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dview_reason)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.Location = new System.Drawing.Point(287, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(91, 29);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEdit.Location = new System.Drawing.Point(415, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(91, 29);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnNew, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txt_reason, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.066909F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.93309F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(509, 387);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // txt_reason
            // 
            this.txt_reason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txt_reason.Location = new System.Drawing.Point(8, 8);
            this.txt_reason.Margin = new System.Windows.Forms.Padding(8);
            this.txt_reason.MaxLength = 50;
            this.txt_reason.Name = "txt_reason";
            this.txt_reason.Size = new System.Drawing.Size(221, 21);
            this.txt_reason.TabIndex = 1;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 346);
            this.panel1.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dview_reason);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 30);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 316);
            this.panel3.TabIndex = 6;
            // 
            // dview_reason
            // 
            this.dview_reason.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmSno,
            this.clmType,
            this.clmReasonID,
            this.clmDescription});
            this.dview_reason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dview_reason.Location = new System.Drawing.Point(0, 0);
            this.dview_reason.MultiSelect = false;
            this.dview_reason.Name = "dview_reason";
            this.dview_reason.RowHeadersVisible = false;
            this.dview_reason.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dview_reason.Size = new System.Drawing.Size(503, 316);
            this.dview_reason.TabIndex = 1;
            this.dview_reason.SelectionChanged += new System.EventHandler(this.dview_reason_SelectionChanged);
            // 
            // clmSno
            // 
            this.clmSno.DataPropertyName = "SNo";
            this.clmSno.HeaderText = "S.No.";
            this.clmSno.Name = "clmSno";
            this.clmSno.ReadOnly = true;
            this.clmSno.Width = 50;
            // 
            // clmType
            // 
            this.clmType.DataPropertyName = "ReasonType";
            this.clmType.HeaderText = "Type";
            this.clmType.Name = "clmType";
            this.clmType.ReadOnly = true;
            this.clmType.Width = 160;
            // 
            // clmReasonID
            // 
            this.clmReasonID.DataPropertyName = "Reason_ID";
            this.clmReasonID.HeaderText = "ReasonID";
            this.clmReasonID.Name = "clmReasonID";
            this.clmReasonID.Visible = false;
            // 
            // clmDescription
            // 
            this.clmDescription.DataPropertyName = "Reason_Description";
            this.clmDescription.HeaderText = "Description";
            this.clmDescription.Name = "clmDescription";
            this.clmDescription.ReadOnly = true;
            this.clmDescription.Width = 260;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblTransactionReason);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(503, 30);
            this.panel2.TabIndex = 0;
            // 
            // lblTransactionReason
            // 
            this.lblTransactionReason.AutoSize = true;
            this.lblTransactionReason.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransactionReason.Location = new System.Drawing.Point(2, 7);
            this.lblTransactionReason.Name = "lblTransactionReason";
            this.lblTransactionReason.Size = new System.Drawing.Size(142, 13);
            this.lblTransactionReason.TabIndex = 0;
            this.lblTransactionReason.Text = "Transaction Reasons";
            // 
            // frmVaultTransactionReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 387);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVaultTransactionReason";
            this.ShowInTaskbar = false;
            this.Text = "Vault Transaction Reason";
            this.Load += new System.EventHandler(this.frmVaultTransactionReason_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dview_reason)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox txt_reason;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTransactionReason;
        private System.Windows.Forms.DataGridView dview_reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSno;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmReasonID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescription;
    }
}