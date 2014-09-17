namespace BMC.MeterAdjustmentTool
{
    partial class UxFilterCriteria
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UxFilterCriteria));
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.btnProcess = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.lblInstallations = new System.Windows.Forms.Label();
            this.cboInstallations = new System.Windows.Forms.ComboBox();
            this.btnInstallationSearch = new System.Windows.Forms.Button();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.uxHeader = new BMC.MeterAdjustmentTool.Helpers.GradientHeader();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 10;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.dtpEndDate, 3, 0);
            this.tblContent.Controls.Add(this.lblStartDate, 0, 0);
            this.tblContent.Controls.Add(this.lblEndDate, 2, 0);
            this.tblContent.Controls.Add(this.dtpStartDate, 1, 0);
            this.tblContent.Controls.Add(this.btnProcess, 7, 0);
            this.tblContent.Controls.Add(this.lblInstallations, 4, 0);
            this.tblContent.Controls.Add(this.cboInstallations, 5, 0);
            this.tblContent.Controls.Add(this.btnInstallationSearch, 6, 0);
            this.tblContent.Controls.Add(this.btnViewReport, 8, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 23);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 2;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(1151, 42);
            this.tblContent.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(328, 7);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(119, 21);
            this.dtpEndDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(3, 11);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(75, 13);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date :";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(228, 11);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(68, 13);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date :";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(103, 7);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(119, 21);
            this.dtpStartDate.TabIndex = 1;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.ImageKey = "Component.ico";
            this.btnProcess.ImageList = this.imglstSmallIcons;
            this.btnProcess.Location = new System.Drawing.Point(793, 3);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(119, 28);
            this.btnProcess.TabIndex = 7;
            this.btnProcess.Text = "Process";
            this.btnProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcess.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Database.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Cancel.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "ConnectServer.ico");
            this.imglstSmallIcons.Images.SetKeyName(3, "Component.ico");
            this.imglstSmallIcons.Images.SetKeyName(4, "Search.ico");
            // 
            // lblInstallations
            // 
            this.lblInstallations.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInstallations.AutoSize = true;
            this.lblInstallations.Location = new System.Drawing.Point(453, 11);
            this.lblInstallations.Name = "lblInstallations";
            this.lblInstallations.Size = new System.Drawing.Size(85, 13);
            this.lblInstallations.TabIndex = 4;
            this.lblInstallations.Text = "Installations :";
            // 
            // cboInstallations
            // 
            this.cboInstallations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboInstallations.DropDownHeight = 250;
            this.cboInstallations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstallations.FormattingEnabled = true;
            this.cboInstallations.IntegralHeight = false;
            this.cboInstallations.Location = new System.Drawing.Point(553, 7);
            this.cboInstallations.MaxDropDownItems = 16;
            this.cboInstallations.Name = "cboInstallations";
            this.cboInstallations.Size = new System.Drawing.Size(174, 21);
            this.cboInstallations.TabIndex = 5;
            this.cboInstallations.SelectedIndexChanged += new System.EventHandler(this.cboInstallations_SelectedIndexChanged);
            // 
            // btnInstallationSearch
            // 
            this.btnInstallationSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstallationSearch.ImageKey = "Search.ico";
            this.btnInstallationSearch.ImageList = this.imglstSmallIcons;
            this.btnInstallationSearch.Location = new System.Drawing.Point(733, 3);
            this.btnInstallationSearch.Name = "btnInstallationSearch";
            this.btnInstallationSearch.Size = new System.Drawing.Size(54, 28);
            this.btnInstallationSearch.TabIndex = 6;
            this.btnInstallationSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInstallationSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInstallationSearch.UseVisualStyleBackColor = true;
            this.btnInstallationSearch.Click += new System.EventHandler(this.btnInstallationSearch_Click);
            // 
            // btnViewReport
            // 
            this.btnViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewReport.ImageKey = "Component.ico";
            this.btnViewReport.ImageList = this.imglstSmallIcons;
            this.btnViewReport.Location = new System.Drawing.Point(918, 3);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(174, 28);
            this.btnViewReport.TabIndex = 8;
            this.btnViewReport.Text = "View Audit Report";
            this.btnViewReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnViewReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewReport.UseVisualStyleBackColor = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // uxHeader
            // 
            this.uxHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.uxHeader.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHeader.ForeColor = System.Drawing.Color.White;
            this.uxHeader.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxHeader.Location = new System.Drawing.Point(0, 0);
            this.uxHeader.Name = "uxHeader";
            this.uxHeader.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.uxHeader.RepeatGradient = false;
            this.uxHeader.Size = new System.Drawing.Size(1151, 23);
            this.uxHeader.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxHeader.TabIndex = 0;
            this.uxHeader.Text = "Filter Criteria";
            // 
            // UxFilterCriteria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContent);
            this.Controls.Add(this.uxHeader);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UxFilterCriteria";
            this.Size = new System.Drawing.Size(1151, 65);
            this.Load += new System.EventHandler(this.UxFilterCriteria_Load);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BMC.MeterAdjustmentTool.Helpers.GradientHeader uxHeader;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.Label lblInstallations;
        private System.Windows.Forms.ComboBox cboInstallations;
        private System.Windows.Forms.Button btnInstallationSearch;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnViewReport;
    }
}
