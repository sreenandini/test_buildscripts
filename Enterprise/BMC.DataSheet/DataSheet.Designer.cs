namespace CustomReports
{
    partial class frmDataSheet
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpCriteria = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpPUPDEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.lblSubCompany = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblBasedOn = new System.Windows.Forms.Label();
            this.cmbReportName = new System.Windows.Forms.ComboBox();
            this.cmbBasedOn = new System.Windows.Forms.ComboBox();
            this.cmbCompany = new System.Windows.Forms.ComboBox();
            this.cmbSubCompany = new System.Windows.Forms.ComboBox();
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.cmbZone = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkLstBoxCustom = new System.Windows.Forms.CheckedListBox();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lblReportName = new System.Windows.Forms.Label();
            this.dtpPUPDStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblMessage = new System.Windows.Forms.Label();
            this.dgvPreview = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpCriteria.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grpCriteria, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvPreview, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grpCriteria
            // 
            this.grpCriteria.Controls.Add(this.tableLayoutPanel2);
            this.grpCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCriteria.Location = new System.Drawing.Point(3, 0);
            this.grpCriteria.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.grpCriteria.Name = "grpCriteria";
            this.grpCriteria.Size = new System.Drawing.Size(778, 278);
            this.grpCriteria.TabIndex = 0;
            this.grpCriteria.TabStop = false;
            this.grpCriteria.Text = "Criteria";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 397F));
            this.tableLayoutPanel2.Controls.Add(this.dtpPUPDEndDate, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblEndDate, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.lblStartDate, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblZone, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblSite, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblSubCompany, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblCompany, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblBasedOn, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbReportName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbBasedOn, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbCompany, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmbSubCompany, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cmbSite, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.cmbZone, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 2, 8);
            this.tableLayoutPanel2.Controls.Add(this.chkLstBoxCustom, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkSelectAll, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblReportName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpPUPDStartDate, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.lblMessage, 0, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 9;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.55966F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.35521F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.67181F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 259);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dtpPUPDEndDate
            // 
            this.dtpPUPDEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPUPDEndDate.Location = new System.Drawing.Point(128, 194);
            this.dtpPUPDEndDate.Name = "dtpPUPDEndDate";
            this.dtpPUPDEndDate.Size = new System.Drawing.Size(244, 20);
            this.dtpPUPDEndDate.TabIndex = 20;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(3, 198);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(55, 13);
            this.lblEndDate.TabIndex = 18;
            this.lblEndDate.Text = "End Date:";
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(3, 169);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(58, 13);
            this.lblStartDate.TabIndex = 17;
            this.lblStartDate.Text = "Start Date:";
            // 
            // lblZone
            // 
            this.lblZone.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(3, 142);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(35, 13);
            this.lblZone.TabIndex = 16;
            this.lblZone.Text = "Zone:";
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(3, 115);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(28, 13);
            this.lblSite.TabIndex = 15;
            this.lblSite.Text = "Site:";
            // 
            // lblSubCompany
            // 
            this.lblSubCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubCompany.AutoSize = true;
            this.lblSubCompany.Location = new System.Drawing.Point(3, 88);
            this.lblSubCompany.Name = "lblSubCompany";
            this.lblSubCompany.Size = new System.Drawing.Size(76, 13);
            this.lblSubCompany.TabIndex = 14;
            this.lblSubCompany.Text = "Sub Company:";
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(3, 61);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(51, 13);
            this.lblCompany.TabIndex = 13;
            this.lblCompany.Text = "Company";
            // 
            // lblBasedOn
            // 
            this.lblBasedOn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBasedOn.AutoSize = true;
            this.lblBasedOn.Location = new System.Drawing.Point(3, 34);
            this.lblBasedOn.Name = "lblBasedOn";
            this.lblBasedOn.Size = new System.Drawing.Size(57, 13);
            this.lblBasedOn.TabIndex = 12;
            this.lblBasedOn.Text = "Based On:";
            // 
            // cmbReportName
            // 
            this.cmbReportName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbReportName.FormattingEnabled = true;
            this.cmbReportName.Location = new System.Drawing.Point(128, 3);
            this.cmbReportName.Name = "cmbReportName";
            this.cmbReportName.Size = new System.Drawing.Size(244, 21);
            this.cmbReportName.TabIndex = 0;
            this.cmbReportName.SelectedIndexChanged += new System.EventHandler(this.cmbReportName_SelectedIndexChanged);
            // 
            // cmbBasedOn
            // 
            this.cmbBasedOn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBasedOn.FormattingEnabled = true;
            this.cmbBasedOn.Location = new System.Drawing.Point(128, 30);
            this.cmbBasedOn.Name = "cmbBasedOn";
            this.cmbBasedOn.Size = new System.Drawing.Size(244, 21);
            this.cmbBasedOn.TabIndex = 1;
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(128, 57);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Size = new System.Drawing.Size(244, 21);
            this.cmbCompany.TabIndex = 2;
            // 
            // cmbSubCompany
            // 
            this.cmbSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSubCompany.FormattingEnabled = true;
            this.cmbSubCompany.Location = new System.Drawing.Point(128, 84);
            this.cmbSubCompany.Name = "cmbSubCompany";
            this.cmbSubCompany.Size = new System.Drawing.Size(244, 21);
            this.cmbSubCompany.TabIndex = 3;
            // 
            // cmbSite
            // 
            this.cmbSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(128, 111);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(244, 21);
            this.cmbSite.TabIndex = 4;
            // 
            // cmbZone
            // 
            this.cmbZone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbZone.FormattingEnabled = true;
            this.cmbZone.Location = new System.Drawing.Point(128, 138);
            this.cmbZone.Name = "cmbZone";
            this.cmbZone.Size = new System.Drawing.Size(244, 21);
            this.cmbZone.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.btnReport, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnPreview, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnExport, 3, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(378, 220);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(391, 39);
            this.tableLayoutPanel3.TabIndex = 8;
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.Location = new System.Drawing.Point(182, 5);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(100, 28);
            this.btnReport.TabIndex = 2;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            // 
            // btnPreview
            // 
            this.btnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreview.Location = new System.Drawing.Point(76, 5);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(100, 28);
            this.btnPreview.TabIndex = 1;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(288, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // chkLstBoxCustom
            // 
            this.chkLstBoxCustom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkLstBoxCustom.FormattingEnabled = true;
            this.chkLstBoxCustom.Location = new System.Drawing.Point(378, 30);
            this.chkLstBoxCustom.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.chkLstBoxCustom.MultiColumn = true;
            this.chkLstBoxCustom.Name = "chkLstBoxCustom";
            this.tableLayoutPanel2.SetRowSpan(this.chkLstBoxCustom, 7);
            this.chkLstBoxCustom.Size = new System.Drawing.Size(391, 190);
            this.chkLstBoxCustom.TabIndex = 9;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Checked = true;
            this.chkSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAll.Location = new System.Drawing.Point(378, 5);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 10;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lblReportName
            // 
            this.lblReportName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblReportName.AutoSize = true;
            this.lblReportName.Location = new System.Drawing.Point(3, 7);
            this.lblReportName.Name = "lblReportName";
            this.lblReportName.Size = new System.Drawing.Size(73, 13);
            this.lblReportName.TabIndex = 11;
            this.lblReportName.Text = "Report Name:";
            // 
            // dtpPUPDStartDate
            // 
            this.dtpPUPDStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpPUPDStartDate.Location = new System.Drawing.Point(128, 165);
            this.dtpPUPDStartDate.Name = "dtpPUPDStartDate";
            this.dtpPUPDStartDate.Size = new System.Drawing.Size(244, 20);
            this.dtpPUPDStartDate.TabIndex = 19;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.lblMessage, 2);
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(3, 229);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(51, 20);
            this.lblMessage.TabIndex = 21;
            this.lblMessage.Text = "label1";
            // 
            // dgvPreview
            // 
            this.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreview.Location = new System.Drawing.Point(3, 284);
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            this.dgvPreview.Size = new System.Drawing.Size(778, 275);
            this.dgvPreview.TabIndex = 1;
            // 
            // frmDataSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmDataSheet";
            this.Text = "frmDataSheet";
            this.Load += new System.EventHandler(this.frmDataSheet_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpCriteria.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpCriteria;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cmbReportName;
        private System.Windows.Forms.ComboBox cmbBasedOn;
        private System.Windows.Forms.ComboBox cmbCompany;
        private System.Windows.Forms.ComboBox cmbSubCompany;
        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.ComboBox cmbZone;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckedListBox chkLstBoxCustom;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblSubCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblBasedOn;
        private System.Windows.Forms.Label lblReportName;
        private System.Windows.Forms.DateTimePicker dtpPUPDEndDate;
        private System.Windows.Forms.DateTimePicker dtpPUPDStartDate;
        private System.Windows.Forms.Label lblMessage;
    }
}