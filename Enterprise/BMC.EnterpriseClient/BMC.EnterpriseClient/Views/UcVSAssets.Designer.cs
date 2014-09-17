namespace BMC.EnterpriseClient.Views
{
    partial class UcVSAssets
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
            this.tbpReport = new System.Windows.Forms.TabPage();
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tabAsset = new System.Windows.Forms.TabControl();
            this.tbpHistory = new System.Windows.Forms.TabPage();
            this.tbpControl = new System.Windows.Forms.TabPage();
            this.tbpGameReports = new System.Windows.Forms.TabPage();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lvwDetails = new BMC.CoreLib.Win32.ListViewEx();
            this.tblHeader.SuspendLayout();
            this.tabAsset.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpReport
            // 
            this.tbpReport.Location = new System.Drawing.Point(4, 25);
            this.tbpReport.Name = "tbpReport";
            this.tbpReport.Padding = new System.Windows.Forms.Padding(3);
            this.tbpReport.Size = new System.Drawing.Size(394, 0);
            this.tbpReport.TabIndex = 0;
            this.tbpReport.Text = "Asset Report";
            this.tbpReport.UseVisualStyleBackColor = true;
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 2;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 402F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Controls.Add(this.tabAsset, 0, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(833, 35);
            this.tblHeader.TabIndex = 0;
            // 
            // tabAsset
            // 
            this.tabAsset.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabAsset.Controls.Add(this.tbpReport);
            this.tabAsset.Controls.Add(this.tbpHistory);
            this.tabAsset.Controls.Add(this.tbpControl);
            this.tabAsset.Controls.Add(this.tbpGameReports);
            this.tabAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAsset.Location = new System.Drawing.Point(0, 8);
            this.tabAsset.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tabAsset.Name = "tabAsset";
            this.tabAsset.SelectedIndex = 0;
            this.tabAsset.Size = new System.Drawing.Size(402, 27);
            this.tabAsset.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabAsset.TabIndex = 0;
            this.tabAsset.SelectedIndexChanged += new System.EventHandler(this.tabAsset_SelectedIndexChanged);
            // 
            // tbpHistory
            // 
            this.tbpHistory.Location = new System.Drawing.Point(4, 25);
            this.tbpHistory.Name = "tbpHistory";
            this.tbpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tbpHistory.Size = new System.Drawing.Size(394, 0);
            this.tbpHistory.TabIndex = 1;
            this.tbpHistory.Text = "Asset History";
            this.tbpHistory.UseVisualStyleBackColor = true;
            // 
            // tbpControl
            // 
            this.tbpControl.Location = new System.Drawing.Point(4, 25);
            this.tbpControl.Name = "tbpControl";
            this.tbpControl.Size = new System.Drawing.Size(394, 0);
            this.tbpControl.TabIndex = 2;
            this.tbpControl.Text = "Control";
            this.tbpControl.UseVisualStyleBackColor = true;
            // 
            // tbpGameReports
            // 
            this.tbpGameReports.Location = new System.Drawing.Point(4, 25);
            this.tbpGameReports.Name = "tbpGameReports";
            this.tbpGameReports.Size = new System.Drawing.Size(394, 0);
            this.tbpGameReports.TabIndex = 3;
            this.tbpGameReports.Text = "Game Reports";
            this.tbpGameReports.UseVisualStyleBackColor = true;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblContainer.Controls.Add(this.lvwDetails, 0, 2);
            this.tblContainer.Controls.Add(this.tblHeader, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 3;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContainer.Size = new System.Drawing.Size(833, 611);
            this.tblContainer.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblCategory, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboCategory, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpFromDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpToDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblFrom, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(833, 35);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(386, 11);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 13);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category :";
            // 
            // lblTo
            // 
            this.lblTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(202, 11);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(26, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To :";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(453, 7);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(150, 21);
            this.cboCategory.TabIndex = 5;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(63, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(133, 20);
            this.dtpFromDate.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(247, 7);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(133, 20);
            this.dtpToDate.TabIndex = 3;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(3, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(36, 13);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From :";
            // 
            // lvwDetails
            // 
            this.lvwDetails.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwDetails.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwDetails.FullRowSelect = true;
            this.lvwDetails.GridLines = true;
            this.lvwDetails.HideSelection = false;
            this.lvwDetails.Location = new System.Drawing.Point(3, 73);
            this.lvwDetails.Name = "lvwDetails";
            this.lvwDetails.Size = new System.Drawing.Size(827, 535);
            this.lvwDetails.TabIndex = 2;
            this.lvwDetails.UseCompatibleStateImageBehavior = false;
            this.lvwDetails.View = System.Windows.Forms.View.Details;
            // 
            // UcVSAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "UcVSAssets";
            this.Size = new System.Drawing.Size(833, 611);
            this.Load += new System.EventHandler(this.UcVSAssets_Load);
            this.tblHeader.ResumeLayout(false);
            this.tabAsset.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tbpReport;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TabControl tabAsset;
        private System.Windows.Forms.TabPage tbpHistory;
        private System.Windows.Forms.TabPage tbpControl;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private BMC.CoreLib.Win32.ListViewEx lvwDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.TabPage tbpGameReports;
    }
}
