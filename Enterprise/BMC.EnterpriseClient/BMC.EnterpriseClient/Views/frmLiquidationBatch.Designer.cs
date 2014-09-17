namespace BMC.EnterpriseClient.Views
{
    partial class frmLiquidationBatch
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
            this.btnPerformLiquidation = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSiteCode = new System.Windows.Forms.Label();
            this.cboSiteCode = new System.Windows.Forms.ComboBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.cboBatchNo = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPerformLiquidation
            // 
            this.btnPerformLiquidation.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPerformLiquidation.Location = new System.Drawing.Point(979, 0);
            this.btnPerformLiquidation.Name = "btnPerformLiquidation";
            this.btnPerformLiquidation.Size = new System.Drawing.Size(134, 45);
            this.btnPerformLiquidation.TabIndex = 0;
            this.btnPerformLiquidation.Text = "Perform Liquidation";
            this.btnPerformLiquidation.UseVisualStyleBackColor = true;
            this.btnPerformLiquidation.Click += new System.EventHandler(this.btnPerformLiquidation_Click);
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.Location = new System.Drawing.Point(1113, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 45);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPerformLiquidation);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 711);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1188, 45);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.17391F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.82609F));
            this.tableLayoutPanel2.Controls.Add(this.lblSiteCode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboSiteCode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblBatchNo, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cboBatchNo, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(445, 73);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblSiteCode
            // 
            this.lblSiteCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSiteCode.AutoSize = true;
            this.lblSiteCode.Location = new System.Drawing.Point(3, 11);
            this.lblSiteCode.Name = "lblSiteCode";
            this.lblSiteCode.Size = new System.Drawing.Size(114, 13);
            this.lblSiteCode.TabIndex = 0;
            this.lblSiteCode.Text = "* Site Code:";
            // 
            // cboSiteCode
            // 
            this.cboSiteCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiteCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSiteCode.FormattingEnabled = true;
            this.cboSiteCode.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cboSiteCode.Location = new System.Drawing.Point(123, 7);
            this.cboSiteCode.Name = "cboSiteCode";
            this.cboSiteCode.Size = new System.Drawing.Size(319, 21);
            this.cboSiteCode.TabIndex = 1;
            this.cboSiteCode.SelectedIndexChanged += new System.EventHandler(this.cboSiteCode_SelectedIndexChanged);
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(3, 48);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(114, 13);
            this.lblBatchNo.TabIndex = 2;
            this.lblBatchNo.Text = "* Batch No:";
            // 
            // cboBatchNo
            // 
            this.cboBatchNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBatchNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBatchNo.FormattingEnabled = true;
            this.cboBatchNo.Location = new System.Drawing.Point(123, 44);
            this.cboBatchNo.Name = "cboBatchNo";
            this.cboBatchNo.Size = new System.Drawing.Size(319, 21);
            this.cboBatchNo.TabIndex = 3;
            // 
            // frmLiquidationBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 756);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmLiquidationBatch";
            this.Text = "Collection Based Liquidation";
            this.Load += new System.EventHandler(this.frmLiquidationBatch_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPerformLiquidation;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblSiteCode;
        private System.Windows.Forms.ComboBox cboSiteCode;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.ComboBox cboBatchNo;

    }
}