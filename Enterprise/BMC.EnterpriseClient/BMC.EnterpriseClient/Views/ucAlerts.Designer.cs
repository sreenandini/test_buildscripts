namespace BMC.EnterpriseClient.Views
{
    partial class ucAlerts
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
            System.Windows.Forms.ListViewGroup listViewGroup25 = new System.Windows.Forms.ListViewGroup("Subscribers", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup26 = new System.Windows.Forms.ListViewGroup("CCSubscribers", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup27 = new System.Windows.Forms.ListViewGroup("BCCSubscribers", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.userControlUI1 = new BMC.CoreLib.Win32.UserControlUI();
            this.Child = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.grpSubscribers = new System.Windows.Forms.GroupBox();
            this.lstESubscribers = new BMC.CoreLib.Win32.ListViewEx();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtbEmailContent = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.btn_LastPage = new System.Windows.Forms.Button();
            this.btn_FirstPage = new System.Windows.Forms.Button();
            this.btn_NextPage = new System.Windows.Forms.Button();
            this.btn_PreviousPage = new System.Windows.Forms.Button();
            this.grpGrid = new System.Windows.Forms.GroupBox();
            this.dgvAlerts = new System.Windows.Forms.DataGridView();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.cboAlertTypes = new BMC.Common.Utilities.BmcComboBox();
            this.lblAlert = new System.Windows.Forms.Label();
            this.cboSites = new BMC.Common.Utilities.BmcComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.chkShowProcessed = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkCancelPendingEmails = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHeaders = new System.Windows.Forms.Panel();
            this.Child.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.grpSubscribers.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlHeaders.SuspendLayout();
            this.SuspendLayout();
            // 
            // userControlUI1
            // 
            this.userControlUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUI1.Location = new System.Drawing.Point(0, 0);
            this.userControlUI1.Name = "userControlUI1";
            this.userControlUI1.Size = new System.Drawing.Size(980, 793);
            this.userControlUI1.TabIndex = 0;
            // 
            // Child
            // 
            this.Child.Controls.Add(this.groupBox1);
            this.Child.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Child.Location = new System.Drawing.Point(0, 340);
            this.Child.Name = "Child";
            this.Child.Padding = new System.Windows.Forms.Padding(3);
            this.Child.Size = new System.Drawing.Size(980, 453);
            this.Child.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 447);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.grpSubscribers);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 65);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(968, 379);
            this.panel6.TabIndex = 10;
            // 
            // grpSubscribers
            // 
            this.grpSubscribers.Controls.Add(this.lstESubscribers);
            this.grpSubscribers.Controls.Add(this.panel3);
            this.grpSubscribers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSubscribers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSubscribers.Location = new System.Drawing.Point(0, 0);
            this.grpSubscribers.Name = "grpSubscribers";
            this.grpSubscribers.Size = new System.Drawing.Size(968, 379);
            this.grpSubscribers.TabIndex = 8;
            this.grpSubscribers.TabStop = false;
            this.grpSubscribers.Text = "Email Subscribers";
            // 
            // lstESubscribers
            // 
            this.lstESubscribers.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lstESubscribers.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lstESubscribers.Dock = System.Windows.Forms.DockStyle.Fill;
            listViewGroup25.Header = "Subscribers";
            listViewGroup25.Name = "lstSubscribers";
            listViewGroup26.Header = "CCSubscribers";
            listViewGroup26.Name = "lstCCSubscribers";
            listViewGroup27.Header = "BCCSubscribers";
            listViewGroup27.Name = "lstBCCSubscribers";
            this.lstESubscribers.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup25,
            listViewGroup26,
            listViewGroup27});
            this.lstESubscribers.Location = new System.Drawing.Point(3, 17);
            this.lstESubscribers.Name = "lstESubscribers";
            this.lstESubscribers.Size = new System.Drawing.Size(962, 359);
            this.lstESubscribers.TabIndex = 0;
            this.lstESubscribers.UseCompatibleStateImageBehavior = false;
            this.lstESubscribers.View = System.Windows.Forms.View.Details;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 16);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(968, 49);
            this.panel5.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(968, 49);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtbEmailContent);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(682, 41);
            this.panel1.TabIndex = 0;
            // 
            // rtbEmailContent
            // 
            this.rtbEmailContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEmailContent.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEmailContent.Location = new System.Drawing.Point(0, 0);
            this.rtbEmailContent.Name = "rtbEmailContent";
            this.rtbEmailContent.Size = new System.Drawing.Size(682, 41);
            this.rtbEmailContent.TabIndex = 10;
            this.rtbEmailContent.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCurrentPage);
            this.panel2.Controls.Add(this.btn_LastPage);
            this.panel2.Controls.Add(this.btn_FirstPage);
            this.panel2.Controls.Add(this.btn_NextPage);
            this.panel2.Controls.Add(this.btn_PreviousPage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(691, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 43);
            this.panel2.TabIndex = 1;
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Location = new System.Drawing.Point(95, 2);
            this.txtCurrentPage.Multiline = true;
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(74, 37);
            this.txtCurrentPage.TabIndex = 8;
            // 
            // btn_LastPage
            // 
            this.btn_LastPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_LastPage.Location = new System.Drawing.Point(221, 3);
            this.btn_LastPage.Name = "btn_LastPage";
            this.btn_LastPage.Size = new System.Drawing.Size(37, 34);
            this.btn_LastPage.TabIndex = 6;
            this.btn_LastPage.Text = ">>";
            this.btn_LastPage.UseVisualStyleBackColor = true;
            this.btn_LastPage.Click += new System.EventHandler(this.btn_LastPage_Click);
            // 
            // btn_FirstPage
            // 
            this.btn_FirstPage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_FirstPage.Location = new System.Drawing.Point(10, 3);
            this.btn_FirstPage.Name = "btn_FirstPage";
            this.btn_FirstPage.Size = new System.Drawing.Size(37, 34);
            this.btn_FirstPage.TabIndex = 7;
            this.btn_FirstPage.Text = "<<";
            this.btn_FirstPage.UseVisualStyleBackColor = true;
            this.btn_FirstPage.Click += new System.EventHandler(this.btn_FirstPage_Click);
            // 
            // btn_NextPage
            // 
            this.btn_NextPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_NextPage.Location = new System.Drawing.Point(178, 3);
            this.btn_NextPage.Name = "btn_NextPage";
            this.btn_NextPage.Size = new System.Drawing.Size(37, 34);
            this.btn_NextPage.TabIndex = 4;
            this.btn_NextPage.Text = ">";
            this.btn_NextPage.UseVisualStyleBackColor = true;
            this.btn_NextPage.Click += new System.EventHandler(this.btn_NextPage_Click);
            // 
            // btn_PreviousPage
            // 
            this.btn_PreviousPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_PreviousPage.Location = new System.Drawing.Point(53, 3);
            this.btn_PreviousPage.Name = "btn_PreviousPage";
            this.btn_PreviousPage.Size = new System.Drawing.Size(37, 34);
            this.btn_PreviousPage.TabIndex = 5;
            this.btn_PreviousPage.Text = "<";
            this.btn_PreviousPage.UseVisualStyleBackColor = true;
            this.btn_PreviousPage.Click += new System.EventHandler(this.btn_PreviousPage_Click);
            // 
            // grpGrid
            // 
            this.grpGrid.Controls.Add(this.dgvAlerts);
            this.grpGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpGrid.Location = new System.Drawing.Point(0, 34);
            this.grpGrid.Name = "grpGrid";
            this.grpGrid.Size = new System.Drawing.Size(980, 306);
            this.grpGrid.TabIndex = 5;
            this.grpGrid.TabStop = false;
            // 
            // dgvAlerts
            // 
            this.dgvAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlerts.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlerts.Location = new System.Drawing.Point(3, 16);
            this.dgvAlerts.Name = "dgvAlerts";
            this.dgvAlerts.Size = new System.Drawing.Size(974, 287);
            this.dgvAlerts.TabIndex = 0;
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoRefresh.Location = new System.Drawing.Point(549, 8);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(96, 17);
            this.chkAutoRefresh.TabIndex = 5;
            this.chkAutoRefresh.Text = "Auto Update";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLoad.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(469, 3);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(74, 28);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // cboAlertTypes
            // 
            this.cboAlertTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboAlertTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAlertTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlertTypes.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAlertTypes.FormattingEnabled = true;
            this.cboAlertTypes.Location = new System.Drawing.Point(315, 6);
            this.cboAlertTypes.Name = "cboAlertTypes";
            this.cboAlertTypes.Size = new System.Drawing.Size(148, 22);
            this.cboAlertTypes.TabIndex = 3;
            // 
            // lblAlert
            // 
            this.lblAlert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAlert.AutoSize = true;
            this.lblAlert.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.Location = new System.Drawing.Point(239, 10);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(66, 13);
            this.lblAlert.TabIndex = 2;
            this.lblAlert.Text = "Alert Type";
            // 
            // cboSites
            // 
            this.cboSites.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboSites.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSites.FormattingEnabled = true;
            this.cboSites.Location = new System.Drawing.Point(98, 6);
            this.cboSites.Name = "cboSites";
            this.cboSites.Size = new System.Drawing.Size(135, 21);
            this.cboSites.TabIndex = 1;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSite.AutoSize = true;
            this.lblSite.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.Location = new System.Drawing.Point(3, 10);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(87, 13);
            this.lblSite.TabIndex = 0;
            this.lblSite.Text = "Choose a Site";
            // 
            // chkShowProcessed
            // 
            this.chkShowProcessed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkShowProcessed.AutoSize = true;
            this.chkShowProcessed.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowProcessed.Location = new System.Drawing.Point(830, 8);
            this.chkShowProcessed.Name = "chkShowProcessed";
            this.chkShowProcessed.Size = new System.Drawing.Size(147, 17);
            this.chkShowProcessed.TabIndex = 6;
            this.chkShowProcessed.Text = "Show Processed Records?";
            this.chkShowProcessed.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(473, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(135, 32);
            this.panel3.TabIndex = 6;
            // 
            // chkCancelPendingEmails
            // 
            this.chkCancelPendingEmails.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkCancelPendingEmails.AutoSize = true;
            this.chkCancelPendingEmails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCancelPendingEmails.Location = new System.Drawing.Point(652, 8);
            this.chkCancelPendingEmails.Name = "chkCancelPendingEmails";
            this.chkCancelPendingEmails.Size = new System.Drawing.Size(172, 17);
            this.chkCancelPendingEmails.TabIndex = 6;
            this.chkCancelPendingEmails.Text = "Cancel sending pending Emails ?";
            this.chkCancelPendingEmails.UseVisualStyleBackColor = true;
            this.chkCancelPendingEmails.CheckedChanged += new System.EventHandler(this.chkCancelPendingEmails_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5391F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.02302F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.189843F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.62152F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.700033F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.45466F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.52358F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.Controls.Add(this.chkShowProcessed, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSite, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboSites, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkAutoRefresh, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblAlert, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cboAlertTypes, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnLoad, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkCancelPendingEmails, 6, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(980, 34);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // pnlHeaders
            // 
            this.pnlHeaders.Controls.Add(this.tableLayoutPanel2);
            this.pnlHeaders.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaders.Location = new System.Drawing.Point(0, 0);
            this.pnlHeaders.Name = "pnlHeaders";
            this.pnlHeaders.Size = new System.Drawing.Size(980, 34);
            this.pnlHeaders.TabIndex = 4;
            // 
            // ucAlerts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Child);
            this.Controls.Add(this.grpGrid);
            this.Controls.Add(this.pnlHeaders);
            this.Controls.Add(this.userControlUI1);
            this.Name = "ucAlerts";
            this.Size = new System.Drawing.Size(980, 793);
            this.Child.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.grpSubscribers.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pnlHeaders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

     
        #endregion

        private CoreLib.Win32.UserControlUI userControlUI1;
        private System.Windows.Forms.Panel Child;
        private System.Windows.Forms.GroupBox grpGrid;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Button btnLoad;
        private Common.Utilities.BmcComboBox cboAlertTypes;
        private System.Windows.Forms.Label lblAlert;
        private Common.Utilities.BmcComboBox cboSites;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.CheckBox chkCancelPendingEmails;
        private System.Windows.Forms.DataGridView dgvAlerts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_PreviousPage;
        private System.Windows.Forms.Button btn_NextPage;
        private System.Windows.Forms.Button btn_LastPage;
        private System.Windows.Forms.Button btn_FirstPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox rtbEmailContent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkShowProcessed;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox grpSubscribers;
        private CoreLib.Win32.ListViewEx lstESubscribers;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlHeaders;



    }
}
