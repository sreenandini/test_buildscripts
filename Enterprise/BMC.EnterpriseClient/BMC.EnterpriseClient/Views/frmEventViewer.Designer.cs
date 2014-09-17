namespace BMC.EnterpriseClient.Views
{
    partial class frmEventViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.tblLowwerButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnHideFilter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblTopControls = new System.Windows.Forms.TableLayoutPanel();
            this.tblFilterOptions = new System.Windows.Forms.TableLayoutPanel();
            this.rdnLocal = new System.Windows.Forms.RadioButton();
            this.rdnSiteLevel = new System.Windows.Forms.RadioButton();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.lblSite = new System.Windows.Forms.Label();
            this.cmbSite = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.lblTypeOfEvent = new System.Windows.Forms.Label();
            this.cmbEventType = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.chkEndDateTime = new System.Windows.Forms.CheckBox();
            this.chkShowAutoClosedEvents = new System.Windows.Forms.CheckBox();
            this.dgEventViewer = new System.Windows.Forms.DataGridView();
            this.tblMainFrame.SuspendLayout();
            this.tblLowwerButtons.SuspendLayout();
            this.tblTopControls.SuspendLayout();
            this.tblFilterOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEventViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.tblLowwerButtons, 0, 1);
            this.tblMainFrame.Controls.Add(this.tblTopControls, 0, 0);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 2;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(937, 695);
            this.tblMainFrame.TabIndex = 0;
            // 
            // tblLowwerButtons
            // 
            this.tblLowwerButtons.ColumnCount = 4;
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLowwerButtons.Controls.Add(this.btnHideFilter, 0, 0);
            this.tblLowwerButtons.Controls.Add(this.btnExport, 2, 0);
            this.tblLowwerButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblLowwerButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLowwerButtons.Location = new System.Drawing.Point(3, 658);
            this.tblLowwerButtons.Name = "tblLowwerButtons";
            this.tblLowwerButtons.RowCount = 1;
            this.tblLowwerButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLowwerButtons.Size = new System.Drawing.Size(931, 34);
            this.tblLowwerButtons.TabIndex = 1;
            // 
            // btnHideFilter
            // 
            this.btnHideFilter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnHideFilter.Location = new System.Drawing.Point(3, 3);
            this.btnHideFilter.Name = "btnHideFilter";
            this.btnHideFilter.Size = new System.Drawing.Size(100, 28);
            this.btnHideFilter.TabIndex = 0;
            this.btnHideFilter.Text = "btnHideFilter";
            this.btnHideFilter.UseVisualStyleBackColor = true;
            this.btnHideFilter.Click += new System.EventHandler(this.btnHideFilter_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExport.Location = new System.Drawing.Point(708, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 28);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(828, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblTopControls
            // 
            this.tblTopControls.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblTopControls.ColumnCount = 2;
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblTopControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTopControls.Controls.Add(this.tblFilterOptions, 0, 0);
            this.tblTopControls.Controls.Add(this.dgEventViewer, 1, 0);
            this.tblTopControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTopControls.Location = new System.Drawing.Point(3, 3);
            this.tblTopControls.Name = "tblTopControls";
            this.tblTopControls.RowCount = 1;
            this.tblTopControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTopControls.Size = new System.Drawing.Size(931, 649);
            this.tblTopControls.TabIndex = 0;
            // 
            // tblFilterOptions
            // 
            this.tblFilterOptions.ColumnCount = 2;
            this.tblFilterOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFilterOptions.Controls.Add(this.rdnLocal, 1, 0);
            this.tblFilterOptions.Controls.Add(this.rdnSiteLevel, 0, 0);
            this.tblFilterOptions.Controls.Add(this.lblStartDate, 0, 1);
            this.tblFilterOptions.Controls.Add(this.dtFromDate, 1, 1);
            this.tblFilterOptions.Controls.Add(this.lblEndDate, 0, 3);
            this.tblFilterOptions.Controls.Add(this.dtToDate, 1, 3);
            this.tblFilterOptions.Controls.Add(this.lblSite, 0, 4);
            this.tblFilterOptions.Controls.Add(this.cmbSite, 1, 4);
            this.tblFilterOptions.Controls.Add(this.lblTypeOfEvent, 0, 5);
            this.tblFilterOptions.Controls.Add(this.cmbEventType, 1, 5);
            this.tblFilterOptions.Controls.Add(this.btnSearch, 1, 7);
            this.tblFilterOptions.Controls.Add(this.btnClearFilters, 0, 7);
            this.tblFilterOptions.Controls.Add(this.chkEndDateTime, 0, 2);
            this.tblFilterOptions.Controls.Add(this.chkShowAutoClosedEvents, 0, 6);
            this.tblFilterOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilterOptions.Location = new System.Drawing.Point(4, 4);
            this.tblFilterOptions.Name = "tblFilterOptions";
            this.tblFilterOptions.RowCount = 8;
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblFilterOptions.Size = new System.Drawing.Size(294, 641);
            this.tblFilterOptions.TabIndex = 0;
            // 
            // rdnLocal
            // 
            this.rdnLocal.AutoSize = true;
            this.rdnLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdnLocal.Location = new System.Drawing.Point(150, 3);
            this.rdnLocal.Name = "rdnLocal";
            this.rdnLocal.Size = new System.Drawing.Size(141, 19);
            this.rdnLocal.TabIndex = 1;
            this.rdnLocal.Text = "rdnLocal";
            this.rdnLocal.UseVisualStyleBackColor = true;
            // 
            // rdnSiteLevel
            // 
            this.rdnSiteLevel.AutoSize = true;
            this.rdnSiteLevel.Checked = true;
            this.rdnSiteLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdnSiteLevel.Location = new System.Drawing.Point(3, 3);
            this.rdnSiteLevel.Name = "rdnSiteLevel";
            this.rdnSiteLevel.Size = new System.Drawing.Size(141, 19);
            this.rdnSiteLevel.TabIndex = 0;
            this.rdnSiteLevel.TabStop = true;
            this.rdnSiteLevel.Text = "rdnSiteLevel";
            this.rdnSiteLevel.UseVisualStyleBackColor = true;
            this.rdnSiteLevel.CheckedChanged += new System.EventHandler(this.rdnSiteLevel_CheckedChanged);
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(3, 31);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(62, 13);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "lblStartDate";
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFromDate.Location = new System.Drawing.Point(150, 28);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(141, 20);
            this.dtFromDate.TabIndex = 3;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(3, 81);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(59, 13);
            this.lblEndDate.TabIndex = 5;
            this.lblEndDate.Text = "lblEndDate";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dtToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtToDate.Location = new System.Drawing.Point(150, 78);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(141, 20);
            this.dtToDate.TabIndex = 6;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(3, 106);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(35, 13);
            this.lblSite.TabIndex = 7;
            this.lblSite.Text = "lblSite";
            // 
            // cmbSite
            // 
            this.cmbSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSite.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Location = new System.Drawing.Point(150, 103);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(141, 21);
            this.cmbSite.TabIndex = 8;
            // 
            // lblTypeOfEvent
            // 
            this.lblTypeOfEvent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTypeOfEvent.AutoSize = true;
            this.lblTypeOfEvent.Location = new System.Drawing.Point(3, 131);
            this.lblTypeOfEvent.Name = "lblTypeOfEvent";
            this.lblTypeOfEvent.Size = new System.Drawing.Size(80, 13);
            this.lblTypeOfEvent.TabIndex = 9;
            this.lblTypeOfEvent.Text = "lblTypeOfEvent";
            // 
            // cmbEventType
            // 
            this.cmbEventType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEventType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventType.FormattingEnabled = true;
            this.cmbEventType.Location = new System.Drawing.Point(150, 128);
            this.cmbEventType.Name = "cmbEventType";
            this.cmbEventType.Size = new System.Drawing.Size(141, 21);
            this.cmbEventType.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(150, 178);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(3, 178);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(100, 28);
            this.btnClearFilters.TabIndex = 12;
            this.btnClearFilters.Text = "btnClearFilters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // chkEndDateTime
            // 
            this.chkEndDateTime.AutoSize = true;
            this.chkEndDateTime.Checked = true;
            this.chkEndDateTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tblFilterOptions.SetColumnSpan(this.chkEndDateTime, 2);
            this.chkEndDateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEndDateTime.Location = new System.Drawing.Point(3, 53);
            this.chkEndDateTime.Name = "chkEndDateTime";
            this.chkEndDateTime.Size = new System.Drawing.Size(288, 19);
            this.chkEndDateTime.TabIndex = 4;
            this.chkEndDateTime.Text = "chkEndDateTime";
            this.chkEndDateTime.UseVisualStyleBackColor = true;
            this.chkEndDateTime.CheckedChanged += new System.EventHandler(this.chkEndDateTime_CheckedChanged);
            // 
            // chkShowAutoClosedEvents
            // 
            this.chkShowAutoClosedEvents.AutoSize = true;
            this.tblFilterOptions.SetColumnSpan(this.chkShowAutoClosedEvents, 2);
            this.chkShowAutoClosedEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowAutoClosedEvents.Location = new System.Drawing.Point(3, 153);
            this.chkShowAutoClosedEvents.Name = "chkShowAutoClosedEvents";
            this.chkShowAutoClosedEvents.Size = new System.Drawing.Size(288, 19);
            this.chkShowAutoClosedEvents.TabIndex = 11;
            this.chkShowAutoClosedEvents.Text = "chkShowAutoClosedEvents";
            this.chkShowAutoClosedEvents.UseVisualStyleBackColor = true;
            // 
            // dgEventViewer
            // 
            this.dgEventViewer.AllowUserToAddRows = false;
            this.dgEventViewer.AllowUserToDeleteRows = false;
            this.dgEventViewer.AllowUserToResizeRows = false;
            this.dgEventViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgEventViewer.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgEventViewer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEventViewer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgEventViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgEventViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEventViewer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgEventViewer.Location = new System.Drawing.Point(305, 4);
            this.dgEventViewer.Name = "dgEventViewer";
            this.dgEventViewer.ReadOnly = true;
            this.dgEventViewer.RowHeadersVisible = false;
            this.dgEventViewer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgEventViewer.Size = new System.Drawing.Size(622, 641);
            this.dgEventViewer.TabIndex = 1;
            this.dgEventViewer.TabStop = false;
            // 
            // frmEventViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 695);
            this.Controls.Add(this.tblMainFrame);
            this.Name = "frmEventViewer";
            this.Text = "Events Viewer";
            this.Load += new System.EventHandler(this.frmEventViewer_Load);
            this.tblMainFrame.ResumeLayout(false);
            this.tblLowwerButtons.ResumeLayout(false);
            this.tblTopControls.ResumeLayout(false);
            this.tblFilterOptions.ResumeLayout(false);
            this.tblFilterOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEventViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblLowwerButtons;
        private System.Windows.Forms.TableLayoutPanel tblTopControls;
        private System.Windows.Forms.TableLayoutPanel tblFilterOptions;
        private System.Windows.Forms.RadioButton rdnSiteLevel;
        private System.Windows.Forms.RadioButton rdnLocal;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.CheckBox chkEndDateTime;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblTypeOfEvent;
        private System.Windows.Forms.CheckBox chkShowAutoClosedEvents;
        private Helpers.BmcComboBox cmbSite;
        private Helpers.BmcComboBox cmbEventType;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnHideFilter;
        private System.Windows.Forms.DataGridView dgEventViewer;
    }
}