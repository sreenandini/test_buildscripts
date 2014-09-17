namespace BMC.EnterpriseClient.Views
{
    partial class frmEmailSubscriberDetails
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmailSubscriberDetails));
            this.uxEmailSubscribers = new BMC.CoreLib.Win32.UxHeaderContent();
            this.tbcSubscribers = new System.Windows.Forms.TabControl();
            this.tbpSubscribers = new System.Windows.Forms.TabPage();
            this.uxSubscribers = new BMC.CoreLib.Win32.UxHeaderContent();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.dgvBccSubscribers = new System.Windows.Forms.DataGridView();
            this.dgEmailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dgvCCSubscribers = new System.Windows.Forms.DataGridView();
            this.dgccEmailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAlertType = new System.Windows.Forms.Label();
            this.cboAlertTypes = new BMC.Common.Utilities.BmcComboBox();
            this.lblSubscribers = new System.Windows.Forms.Label();
            this.dgvSubscribers = new System.Windows.Forms.DataGridView();
            this.dgcEmailID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCCSubscribers = new System.Windows.Forms.Label();
            this.lblBCCSubscribers = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblSender = new System.Windows.Forms.Label();
            this.tbpFromDetails = new System.Windows.Forms.TabPage();
            this.uxHeaderContent1 = new BMC.CoreLib.Win32.UxHeaderContent();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPWd = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUID = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.lblport = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblEnableSSL = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveMailInfo = new System.Windows.Forms.Button();
            this.chkEnableSSL = new System.Windows.Forms.CheckBox();
            this.tbcSubscribers.SuspendLayout();
            this.tbpSubscribers.SuspendLayout();
            this.uxSubscribers.ChildContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBccSubscribers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCCSubscribers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscribers)).BeginInit();
            this.panel1.SuspendLayout();
            this.tbpFromDetails.SuspendLayout();
            this.uxHeaderContent1.ChildContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxEmailSubscribers
            // 
            // 
            // uxEmailSubscribers.Child
            // 
            this.uxEmailSubscribers.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxEmailSubscribers.ChildContainer.Name = "Child";
            this.uxEmailSubscribers.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxEmailSubscribers.ChildContainer.Size = new System.Drawing.Size(786, 620);
            this.uxEmailSubscribers.ChildContainer.TabIndex = 2;
            this.uxEmailSubscribers.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxEmailSubscribers.EndColor = System.Drawing.SystemColors.Control;
            this.uxEmailSubscribers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxEmailSubscribers.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxEmailSubscribers.HeaderText = "";
            this.uxEmailSubscribers.IsClosable = false;
            this.uxEmailSubscribers.Location = new System.Drawing.Point(0, 0);
            this.uxEmailSubscribers.Name = "uxEmailSubscribers";
            this.uxEmailSubscribers.PinVisible = false;
            this.uxEmailSubscribers.Size = new System.Drawing.Size(775, 612);
            this.uxEmailSubscribers.StartColor = System.Drawing.SystemColors.Control;
            this.uxEmailSubscribers.TabIndex = 1;
            // 
            // tbcSubscribers
            // 
            this.tbcSubscribers.Controls.Add(this.tbpSubscribers);
            this.tbcSubscribers.Controls.Add(this.tbpFromDetails);
            this.tbcSubscribers.Location = new System.Drawing.Point(0, 0);
            this.tbcSubscribers.Name = "tbcSubscribers";
            this.tbcSubscribers.SelectedIndex = 0;
            this.tbcSubscribers.Size = new System.Drawing.Size(634, 526);
            this.tbcSubscribers.TabIndex = 1;
            // 
            // tbpSubscribers
            // 
            this.tbpSubscribers.Controls.Add(this.uxSubscribers);
            this.tbpSubscribers.Location = new System.Drawing.Point(4, 22);
            this.tbpSubscribers.Name = "tbpSubscribers";
            this.tbpSubscribers.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSubscribers.Size = new System.Drawing.Size(626, 500);
            this.tbpSubscribers.TabIndex = 0;
            this.tbpSubscribers.Text = "Mail Subscribers";
            this.tbpSubscribers.UseVisualStyleBackColor = true;
            // 
            // uxSubscribers
            // 
            this.uxSubscribers.BackColor = System.Drawing.SystemColors.Control;
            // 
            // uxSubscribers.Child
            // 
            this.uxSubscribers.ChildContainer.Controls.Add(this.tableLayoutPanel1);
            this.uxSubscribers.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSubscribers.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxSubscribers.ChildContainer.Name = "Child";
            this.uxSubscribers.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxSubscribers.ChildContainer.Size = new System.Drawing.Size(620, 468);
            this.uxSubscribers.ChildContainer.TabIndex = 2;
            this.uxSubscribers.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxSubscribers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxSubscribers.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxSubscribers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxSubscribers.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxSubscribers.HeaderText = "Email Subscribers";
            this.uxSubscribers.IsClosable = false;
            this.uxSubscribers.Location = new System.Drawing.Point(3, 3);
            this.uxSubscribers.Name = "uxSubscribers";
            this.uxSubscribers.PinVisible = false;
            this.uxSubscribers.Size = new System.Drawing.Size(620, 494);
            this.uxSubscribers.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxSubscribers.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.20419F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.79581F));
            this.tableLayoutPanel1.Controls.Add(this.txtSender, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSubject, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvBccSubscribers, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.dgvCCSubscribers, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblAlertType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboAlertTypes, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSubscribers, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.dgvSubscribers, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCCSubscribers, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblBCCSubscribers, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtSubject, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblSender, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 468);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtSender
            // 
            this.txtSender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSender.Location = new System.Drawing.Point(133, 90);
            this.txtSender.MaxLength = 100;
            this.txtSender.Multiline = true;
            this.txtSender.Name = "txtSender";
            this.txtSender.ReadOnly = true;
            this.txtSender.Size = new System.Drawing.Size(399, 29);
            this.txtSender.TabIndex = 5;
            // 
            // lblSubject
            // 
            this.lblSubject.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(3, 51);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(50, 13);
            this.lblSubject.TabIndex = 2;
            this.lblSubject.Text = "Subject";
            // 
            // dgvBccSubscribers
            // 
            this.dgvBccSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dgvBccSubscribers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBccSubscribers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgEmailID});
            this.dgvBccSubscribers.ContextMenuStrip = this.Strip;
            this.dgvBccSubscribers.Location = new System.Drawing.Point(133, 305);
            this.dgvBccSubscribers.Name = "dgvBccSubscribers";
            this.dgvBccSubscribers.Size = new System.Drawing.Size(404, 77);
            this.dgvBccSubscribers.TabIndex = 11;
            // 
            // dgEmailID
            // 
            this.dgEmailID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgEmailID.HeaderText = "EmailID";
            this.dgEmailID.Name = "dgEmailID";
            // 
            // Strip
            // 
            this.Strip.Name = "Strip";
            this.Strip.Size = new System.Drawing.Size(61, 4);
            // 
            // dgvCCSubscribers
            // 
            this.dgvCCSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dgvCCSubscribers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCCSubscribers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgccEmailID});
            this.dgvCCSubscribers.ContextMenuStrip = this.Strip;
            this.dgvCCSubscribers.Location = new System.Drawing.Point(133, 222);
            this.dgvCCSubscribers.Name = "dgvCCSubscribers";
            this.dgvCCSubscribers.Size = new System.Drawing.Size(404, 77);
            this.dgvCCSubscribers.TabIndex = 9;
            // 
            // dgccEmailID
            // 
            this.dgccEmailID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgccEmailID.HeaderText = "EmailID";
            this.dgccEmailID.Name = "dgccEmailID";
            // 
            // lblAlertType
            // 
            this.lblAlertType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAlertType.AutoSize = true;
            this.lblAlertType.Location = new System.Drawing.Point(3, 7);
            this.lblAlertType.Name = "lblAlertType";
            this.lblAlertType.Size = new System.Drawing.Size(66, 13);
            this.lblAlertType.TabIndex = 0;
            this.lblAlertType.Text = "Alert Type";
            // 
            // cboAlertTypes
            // 
            this.cboAlertTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboAlertTypes.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboAlertTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlertTypes.FormattingEnabled = true;
            this.cboAlertTypes.Location = new System.Drawing.Point(133, 3);
            this.cboAlertTypes.Name = "cboAlertTypes";
            this.cboAlertTypes.Size = new System.Drawing.Size(329, 22);
            this.cboAlertTypes.TabIndex = 1;
            this.cboAlertTypes.SelectedIndexChanged += new System.EventHandler(this.cboAlertTypes_SelectedIndexChanged);
            // 
            // lblSubscribers
            // 
            this.lblSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubscribers.AutoSize = true;
            this.lblSubscribers.Location = new System.Drawing.Point(3, 164);
            this.lblSubscribers.Name = "lblSubscribers";
            this.lblSubscribers.Size = new System.Drawing.Size(74, 13);
            this.lblSubscribers.TabIndex = 6;
            this.lblSubscribers.Text = "Subscribers";
            // 
            // dgvSubscribers
            // 
            this.dgvSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dgvSubscribers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubscribers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcEmailID});
            this.dgvSubscribers.ContextMenuStrip = this.Strip;
            this.dgvSubscribers.GridColor = System.Drawing.SystemColors.Control;
            this.dgvSubscribers.Location = new System.Drawing.Point(133, 125);
            this.dgvSubscribers.Name = "dgvSubscribers";
            this.dgvSubscribers.Size = new System.Drawing.Size(399, 91);
            this.dgvSubscribers.TabIndex = 7;
            // 
            // dgcEmailID
            // 
            this.dgcEmailID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgcEmailID.HeaderText = "EmailID";
            this.dgcEmailID.Name = "dgcEmailID";
            // 
            // lblCCSubscribers
            // 
            this.lblCCSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCCSubscribers.AutoSize = true;
            this.lblCCSubscribers.Location = new System.Drawing.Point(3, 254);
            this.lblCCSubscribers.Name = "lblCCSubscribers";
            this.lblCCSubscribers.Size = new System.Drawing.Size(96, 13);
            this.lblCCSubscribers.TabIndex = 8;
            this.lblCCSubscribers.Text = "CC Subscribers";
            // 
            // lblBCCSubscribers
            // 
            this.lblBCCSubscribers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBCCSubscribers.AutoSize = true;
            this.lblBCCSubscribers.Location = new System.Drawing.Point(3, 337);
            this.lblBCCSubscribers.Name = "lblBCCSubscribers";
            this.lblBCCSubscribers.Size = new System.Drawing.Size(104, 13);
            this.lblBCCSubscribers.TabIndex = 10;
            this.lblBCCSubscribers.Text = "BCC Subscribers";
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSubject.Location = new System.Drawing.Point(133, 31);
            this.txtSubject.MaxLength = 500;
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(399, 53);
            this.txtSubject.TabIndex = 3;
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Location = new System.Drawing.Point(3, 388);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 61);
            this.panel1.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(410, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 50);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblSender
            // 
            this.lblSender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(3, 98);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(48, 13);
            this.lblSender.TabIndex = 4;
            this.lblSender.Text = "Sender";
            // 
            // tbpFromDetails
            // 
            this.tbpFromDetails.Controls.Add(this.uxHeaderContent1);
            this.tbpFromDetails.Location = new System.Drawing.Point(4, 22);
            this.tbpFromDetails.Name = "tbpFromDetails";
            this.tbpFromDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFromDetails.Size = new System.Drawing.Size(626, 500);
            this.tbpFromDetails.TabIndex = 1;
            this.tbpFromDetails.Text = "Mail Server Configuration";
            this.tbpFromDetails.UseVisualStyleBackColor = true;
            // 
            // uxHeaderContent1
            // 
            this.uxHeaderContent1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // uxHeaderContent1.Child
            // 
            this.uxHeaderContent1.ChildContainer.Controls.Add(this.tableLayoutPanel2);
            this.uxHeaderContent1.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxHeaderContent1.ChildContainer.Name = "Child";
            this.uxHeaderContent1.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxHeaderContent1.ChildContainer.Size = new System.Drawing.Size(566, 312);
            this.uxHeaderContent1.ChildContainer.TabIndex = 2;
            this.uxHeaderContent1.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxHeaderContent1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxHeaderContent1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxHeaderContent1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHeaderContent1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxHeaderContent1.HeaderText = "Mail Server Configuration";
            this.uxHeaderContent1.IsClosable = false;
            this.uxHeaderContent1.Location = new System.Drawing.Point(3, 3);
            this.uxHeaderContent1.Name = "uxHeaderContent1";
            this.uxHeaderContent1.PinVisible = false;
            this.uxHeaderContent1.Size = new System.Drawing.Size(620, 494);
            this.uxHeaderContent1.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxHeaderContent1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.64675F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.35326F));
            this.tableLayoutPanel2.Controls.Add(this.lblPWd, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.txtPassword, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblUID, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtUserID, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblport, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtPort, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblServer, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtServerName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblEnableSSL, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.chkEnableSSL, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.16667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.83333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(562, 306);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // lblPWd
            // 
            this.lblPWd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPWd.AutoSize = true;
            this.lblPWd.Location = new System.Drawing.Point(3, 182);
            this.lblPWd.Name = "lblPWd";
            this.lblPWd.Size = new System.Drawing.Size(61, 13);
            this.lblPWd.TabIndex = 8;
            this.lblPWd.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPassword.Location = new System.Drawing.Point(124, 178);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(323, 21);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblUID
            // 
            this.lblUID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUID.AutoSize = true;
            this.lblUID.Location = new System.Drawing.Point(3, 139);
            this.lblUID.Name = "lblUID";
            this.lblUID.Size = new System.Drawing.Size(70, 13);
            this.lblUID.TabIndex = 6;
            this.lblUID.Text = "User Name";
            // 
            // txtUserID
            // 
            this.txtUserID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtUserID.Location = new System.Drawing.Point(124, 135);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(323, 21);
            this.txtUserID.TabIndex = 7;
            // 
            // lblport
            // 
            this.lblport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblport.AutoSize = true;
            this.lblport.Location = new System.Drawing.Point(3, 56);
            this.lblport.Name = "lblport";
            this.lblport.Size = new System.Drawing.Size(30, 13);
            this.lblport.TabIndex = 2;
            this.lblport.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPort.Location = new System.Drawing.Point(124, 52);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(323, 21);
            this.txtPort.TabIndex = 3;
            // 
            // lblServer
            // 
            this.lblServer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(3, 15);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(83, 13);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Name";
            // 
            // txtServerName
            // 
            this.txtServerName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServerName.Location = new System.Drawing.Point(124, 11);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(323, 21);
            this.txtServerName.TabIndex = 1;
            // 
            // lblEnableSSL
            // 
            this.lblEnableSSL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEnableSSL.AutoSize = true;
            this.lblEnableSSL.Location = new System.Drawing.Point(3, 98);
            this.lblEnableSSL.Name = "lblEnableSSL";
            this.lblEnableSSL.Size = new System.Drawing.Size(67, 13);
            this.lblEnableSSL.TabIndex = 4;
            this.lblEnableSSL.Text = "EnableSSL";
            // 
            // panel2
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.btnSaveMailInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 216);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(556, 87);
            this.panel2.TabIndex = 10;
            // 
            // btnSaveMailInfo
            // 
            this.btnSaveMailInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSaveMailInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveMailInfo.Location = new System.Drawing.Point(320, 8);
            this.btnSaveMailInfo.Name = "btnSaveMailInfo";
            this.btnSaveMailInfo.Size = new System.Drawing.Size(124, 50);
            this.btnSaveMailInfo.TabIndex = 10;
            this.btnSaveMailInfo.Text = "Save";
            this.btnSaveMailInfo.UseVisualStyleBackColor = true;
            this.btnSaveMailInfo.Click += new System.EventHandler(this.btnSaveMailInfo_Click);
            // 
            // chkEnableSSL
            // 
            this.chkEnableSSL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEnableSSL.AutoSize = true;
            this.chkEnableSSL.Location = new System.Drawing.Point(124, 96);
            this.chkEnableSSL.Name = "chkEnableSSL";
            this.chkEnableSSL.Size = new System.Drawing.Size(30, 17);
            this.chkEnableSSL.TabIndex = 5;
            this.chkEnableSSL.Text = " ";
            this.chkEnableSSL.UseVisualStyleBackColor = true;
            this.chkEnableSSL.CheckedChanged += new System.EventHandler(this.chkEnableSSL_CheckedChanged);
            // 
            // frmEmailSubscriberDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 538);
            this.Controls.Add(this.tbcSubscribers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmailSubscriberDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email Subscriber Details";
            this.Load += new System.EventHandler(this.frmEmailSubscriberDetails_Load);
            this.tbcSubscribers.ResumeLayout(false);
            this.tbpSubscribers.ResumeLayout(false);
            this.uxSubscribers.ChildContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBccSubscribers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCCSubscribers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscribers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tbpFromDetails.ResumeLayout(false);
            this.uxHeaderContent1.ChildContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

      

        #endregion

      
     
        private CoreLib.Win32.UxHeaderContent uxEmailSubscribers;
        private System.Windows.Forms.TabControl tbcSubscribers;
        private System.Windows.Forms.TabPage tbpSubscribers;
        private System.Windows.Forms.TabPage tbpFromDetails;
        private CoreLib.Win32.UxHeaderContent uxSubscribers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblAlertType;
        private Common.Utilities.BmcComboBox cboAlertTypes;
        private System.Windows.Forms.Label lblSubscribers;
        private System.Windows.Forms.DataGridView dgvSubscribers;
        private System.Windows.Forms.Label lblCCSubscribers;
        private System.Windows.Forms.DataGridView dgvCCSubscribers;
        private System.Windows.Forms.Label lblBCCSubscribers;
        private System.Windows.Forms.DataGridView dgvBccSubscribers;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcEmailID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgEmailID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgccEmailID;
        private CoreLib.Win32.UxHeaderContent uxHeaderContent1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lblport;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblEnableSSL;
        private System.Windows.Forms.Label lblUID;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label lblPWd;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSaveMailInfo;
        private System.Windows.Forms.CheckBox chkEnableSSL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.ContextMenuStrip Strip;
 


    }
}