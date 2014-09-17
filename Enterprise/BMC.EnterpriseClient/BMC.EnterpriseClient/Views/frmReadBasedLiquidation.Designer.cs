namespace BMC.EnterpriseClient.Views
{
    partial class frmReadBasedLiquidation
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
            this.tcReadBasedLiquidation = new System.Windows.Forms.TabControl();
            this.tpReadLiquidation = new System.Windows.Forms.TabPage();
            this.tblLPReadLiquidation = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.ucReadBasedLiquidation = new BMC.EnterpriseClient.Views.ucReadBasedLiquidation();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.btnPerform = new System.Windows.Forms.Button();
            this.tpReport = new System.Windows.Forms.TabPage();
            this.tblLPReport = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ucReadLiquidationReport = new BMC.EnterpriseClient.Views.ucReadLiquidationReport();
            this.tcReadBasedLiquidation.SuspendLayout();
            this.tpReadLiquidation.SuspendLayout();
            this.tblLPReadLiquidation.SuspendLayout();
            this.tpReport.SuspendLayout();
            this.tblLPReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcReadBasedLiquidation
            // 
            this.tcReadBasedLiquidation.Controls.Add(this.tpReadLiquidation);
            this.tcReadBasedLiquidation.Controls.Add(this.tpReport);
            this.tcReadBasedLiquidation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcReadBasedLiquidation.Location = new System.Drawing.Point(0, 0);
            this.tcReadBasedLiquidation.Name = "tcReadBasedLiquidation";
            this.tcReadBasedLiquidation.SelectedIndex = 0;
            this.tcReadBasedLiquidation.Size = new System.Drawing.Size(916, 483);
            this.tcReadBasedLiquidation.TabIndex = 4;
            this.tcReadBasedLiquidation.SelectedIndexChanged += new System.EventHandler(this.tcReadBasedLiquidation_SelectedIndexChanged);
            // 
            // tpReadLiquidation
            // 
            this.tpReadLiquidation.Controls.Add(this.tblLPReadLiquidation);
            this.tpReadLiquidation.Location = new System.Drawing.Point(4, 22);
            this.tpReadLiquidation.Name = "tpReadLiquidation";
            this.tpReadLiquidation.Padding = new System.Windows.Forms.Padding(3);
            this.tpReadLiquidation.Size = new System.Drawing.Size(908, 457);
            this.tpReadLiquidation.TabIndex = 0;
            this.tpReadLiquidation.Text = "Read Liquidation";
            this.tpReadLiquidation.UseVisualStyleBackColor = true;
            // 
            // tblLPReadLiquidation
            // 
            this.tblLPReadLiquidation.ColumnCount = 4;
            this.tblLPReadLiquidation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLPReadLiquidation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tblLPReadLiquidation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tblLPReadLiquidation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tblLPReadLiquidation.Controls.Add(this.btnClose, 3, 1);
            this.tblLPReadLiquidation.Controls.Add(this.ucReadBasedLiquidation, 0, 0);
            this.tblLPReadLiquidation.Controls.Add(this.btnRefresh, 0, 1);
            this.tblLPReadLiquidation.Controls.Add(this.btnDetails, 1, 1);
            this.tblLPReadLiquidation.Controls.Add(this.btnPerform, 2, 1);
            this.tblLPReadLiquidation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLPReadLiquidation.Location = new System.Drawing.Point(3, 3);
            this.tblLPReadLiquidation.Name = "tblLPReadLiquidation";
            this.tblLPReadLiquidation.RowCount = 2;
            this.tblLPReadLiquidation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLPReadLiquidation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLPReadLiquidation.Size = new System.Drawing.Size(902, 451);
            this.tblLPReadLiquidation.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(812, 418);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucReadBasedLiquidation
            // 
            this.ucReadBasedLiquidation.AutoSize = true;
            this.tblLPReadLiquidation.SetColumnSpan(this.ucReadBasedLiquidation, 4);
            this.ucReadBasedLiquidation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReadBasedLiquidation.Location = new System.Drawing.Point(3, 3);
            this.ucReadBasedLiquidation.Name = "ucReadBasedLiquidation";
            this.ucReadBasedLiquidation.Size = new System.Drawing.Size(896, 409);
            this.ucReadBasedLiquidation.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.Location = new System.Drawing.Point(503, 418);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(87, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDetails.Location = new System.Drawing.Point(607, 418);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(87, 30);
            this.btnDetails.TabIndex = 3;
            this.btnDetails.Text = "&Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // btnPerform
            // 
            this.btnPerform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPerform.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPerform.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPerform.Location = new System.Drawing.Point(709, 418);
            this.btnPerform.Name = "btnPerform";
            this.btnPerform.Size = new System.Drawing.Size(87, 30);
            this.btnPerform.TabIndex = 4;
            this.btnPerform.Text = "&Perform";
            this.btnPerform.UseVisualStyleBackColor = true;
            this.btnPerform.Click += new System.EventHandler(this.btnPerform_Click);
            // 
            // tpReport
            // 
            this.tpReport.Controls.Add(this.tblLPReport);
            this.tpReport.Location = new System.Drawing.Point(4, 22);
            this.tpReport.Name = "tpReport";
            this.tpReport.Padding = new System.Windows.Forms.Padding(3);
            this.tpReport.Size = new System.Drawing.Size(908, 457);
            this.tpReport.TabIndex = 1;
            this.tpReport.Text = "Report";
            this.tpReport.UseVisualStyleBackColor = true;
            // 
            // tblLPReport
            // 
            this.tblLPReport.ColumnCount = 1;
            this.tblLPReport.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLPReport.Controls.Add(this.btnPrint, 0, 1);
            this.tblLPReport.Controls.Add(this.ucReadLiquidationReport, 0, 0);
            this.tblLPReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLPReport.Location = new System.Drawing.Point(3, 3);
            this.tblLPReport.Name = "tblLPReport";
            this.tblLPReport.RowCount = 2;
            this.tblLPReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLPReport.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblLPReport.Size = new System.Drawing.Size(902, 451);
            this.tblLPReport.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrint.Location = new System.Drawing.Point(812, 418);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 30);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ucReadLiquidationReport
            // 
            this.ucReadLiquidationReport.AutoSize = true;
            this.ucReadLiquidationReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucReadLiquidationReport.Location = new System.Drawing.Point(3, 3);
            this.ucReadLiquidationReport.Name = "ucReadLiquidationReport";
            this.ucReadLiquidationReport.Size = new System.Drawing.Size(896, 409);
            this.ucReadLiquidationReport.TabIndex = 4;
            // 
            // frmReadBasedLiquidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 483);
            this.Controls.Add(this.tcReadBasedLiquidation);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReadBasedLiquidation";
            this.Text = "Read Based Liquidation";
            this.Load += new System.EventHandler(this.frmReadBasedLiquidation_Load);
            this.tcReadBasedLiquidation.ResumeLayout(false);
            this.tpReadLiquidation.ResumeLayout(false);
            this.tblLPReadLiquidation.ResumeLayout(false);
            this.tblLPReadLiquidation.PerformLayout();
            this.tpReport.ResumeLayout(false);
            this.tblLPReport.ResumeLayout(false);
            this.tblLPReport.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcReadBasedLiquidation;
        private System.Windows.Forms.TabPage tpReadLiquidation;
        private System.Windows.Forms.TableLayoutPanel tblLPReadLiquidation;
        private System.Windows.Forms.Button btnClose;
        private ucReadBasedLiquidation ucReadBasedLiquidation;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Button btnPerform;
        private System.Windows.Forms.TabPage tpReport;
        private System.Windows.Forms.TableLayoutPanel tblLPReport;
        private System.Windows.Forms.Button btnPrint;
        private ucReadLiquidationReport ucReadLiquidationReport;

    }
}