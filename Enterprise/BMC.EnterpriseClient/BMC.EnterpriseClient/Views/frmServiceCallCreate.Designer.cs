namespace BMC.EnterpriseClient.Views
{
    partial class frmServiceCallCreate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceCallCreate));
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblBottomPane = new System.Windows.Forms.TableLayoutPanel();
            this.grpNotes = new System.Windows.Forms.GroupBox();
            this.lvNotes = new System.Windows.Forms.ListView();
            this.colUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateSent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.btnAddCall = new System.Windows.Forms.Button();
            this.grpBoxCall = new System.Windows.Forms.GroupBox();
            this.tblOpenCall = new System.Windows.Forms.TableLayoutPanel();
            this.tblLoggedTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpReceivedTime = new System.Windows.Forms.DateTimePicker();
            this.chkReceivedTime = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRemedy = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRemedyNotes = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblLogged = new System.Windows.Forms.Label();
            this.lblDespatched = new System.Windows.Forms.Label();
            this.lblAcknowledged = new System.Windows.Forms.Label();
            this.lblArrived = new System.Windows.Forms.Label();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.cmbCallStatus = new System.Windows.Forms.ComboBox();
            this.cmbCallSource = new System.Windows.Forms.ComboBox();
            this.cmbMachineType = new System.Windows.Forms.ComboBox();
            this.cmbMachineName = new System.Windows.Forms.ComboBox();
            this.cmbFaultGroup = new System.Windows.Forms.ComboBox();
            this.cmbFault = new System.Windows.Forms.ComboBox();
            this.cmbEngineer = new System.Windows.Forms.ComboBox();
            this.cmbRemedy = new System.Windows.Forms.ComboBox();
            this.cmbSites = new System.Windows.Forms.ComboBox();
            this.txtFaultNotes = new System.Windows.Forms.RichTextBox();
            this.txtRemedyNotes = new System.Windows.Forms.RichTextBox();
            this.chkShowAllEng = new System.Windows.Forms.CheckBox();
            this.txtSubCompany = new System.Windows.Forms.TextBox();
            this.txtPostcode = new System.Windows.Forms.TextBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtOpenHours = new System.Windows.Forms.TextBox();
            this.txtDepot = new System.Windows.Forms.TextBox();
            this.txtSiteDetails = new System.Windows.Forms.RichTextBox();
            this.chkShowMachineHistory = new System.Windows.Forms.CheckBox();
            this.tblCompletedTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpCompletedTime = new System.Windows.Forms.DateTimePicker();
            this.chkCompletedTime = new System.Windows.Forms.CheckBox();
            this.tblArrivedTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpArrivalTime = new System.Windows.Forms.DateTimePicker();
            this.chkArrivalTime = new System.Windows.Forms.CheckBox();
            this.tblEngrAckTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpEngrAckTime = new System.Windows.Forms.DateTimePicker();
            this.chkEngrAckTime = new System.Windows.Forms.CheckBox();
            this.tblPassedToEngTime = new System.Windows.Forms.TableLayoutPanel();
            this.dtpPassedToEngrTime = new System.Windows.Forms.DateTimePicker();
            this.chkPassedToEngrTime = new System.Windows.Forms.CheckBox();
            this.tblMain.SuspendLayout();
            this.tblBottomPane.SuspendLayout();
            this.grpNotes.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.grpBoxCall.SuspendLayout();
            this.tblOpenCall.SuspendLayout();
            this.tblLoggedTime.SuspendLayout();
            this.tblCompletedTime.SuspendLayout();
            this.tblArrivedTime.SuspendLayout();
            this.tblEngrAckTime.SuspendLayout();
            this.tblPassedToEngTime.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tblBottomPane, 0, 1);
            this.tblMain.Controls.Add(this.grpBoxCall, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblMain.Size = new System.Drawing.Size(1002, 625);
            this.tblMain.TabIndex = 0;
            // 
            // tblBottomPane
            // 
            this.tblBottomPane.ColumnCount = 3;
            this.tblBottomPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.82353F));
            this.tblBottomPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.58824F));
            this.tblBottomPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.58824F));
            this.tblBottomPane.Controls.Add(this.grpNotes, 0, 0);
            this.tblBottomPane.Controls.Add(this.pnlButtons, 1, 3);
            this.tblBottomPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBottomPane.Location = new System.Drawing.Point(3, 471);
            this.tblBottomPane.Name = "tblBottomPane";
            this.tblBottomPane.RowCount = 4;
            this.tblBottomPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.3578F));
            this.tblBottomPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.86238F));
            this.tblBottomPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.89743F));
            this.tblBottomPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblBottomPane.Size = new System.Drawing.Size(996, 151);
            this.tblBottomPane.TabIndex = 1;
            // 
            // grpNotes
            // 
            this.tblBottomPane.SetColumnSpan(this.grpNotes, 3);
            this.grpNotes.Controls.Add(this.lvNotes);
            this.grpNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNotes.Location = new System.Drawing.Point(3, 3);
            this.grpNotes.Name = "grpNotes";
            this.tblBottomPane.SetRowSpan(this.grpNotes, 3);
            this.grpNotes.Size = new System.Drawing.Size(990, 103);
            this.grpNotes.TabIndex = 5;
            this.grpNotes.TabStop = false;
            this.grpNotes.Text = "Notes";
            // 
            // lvNotes
            // 
            this.lvNotes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUser,
            this.colNote,
            this.colDateSent});
            this.lvNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvNotes.FullRowSelect = true;
            this.lvNotes.GridLines = true;
            this.lvNotes.Location = new System.Drawing.Point(3, 16);
            this.lvNotes.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.lvNotes.Name = "lvNotes";
            this.lvNotes.Size = new System.Drawing.Size(984, 84);
            this.lvNotes.TabIndex = 30;
            this.lvNotes.UseCompatibleStateImageBehavior = false;
            this.lvNotes.View = System.Windows.Forms.View.Details;
            // 
            // colUser
            // 
            this.colUser.Text = "User";
            this.colUser.Width = 123;
            // 
            // colNote
            // 
            this.colNote.Text = "Note";
            this.colNote.Width = 746;
            // 
            // colDateSent
            // 
            this.colDateSent.Text = "DateSent";
            this.colDateSent.Width = 107;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tblBottomPane.SetColumnSpan(this.pnlButtons, 2);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.btnAddNote);
            this.pnlButtons.Controls.Add(this.btnAddCall);
            this.pnlButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.pnlButtons.Location = new System.Drawing.Point(588, 112);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(405, 36);
            this.pnlButtons.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(302, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddNote
            // 
            this.btnAddNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNote.Location = new System.Drawing.Point(196, 3);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(100, 28);
            this.btnAddNote.TabIndex = 32;
            this.btnAddNote.Text = "Add &Note";
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // btnAddCall
            // 
            this.btnAddCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCall.Location = new System.Drawing.Point(90, 3);
            this.btnAddCall.Name = "btnAddCall";
            this.btnAddCall.Size = new System.Drawing.Size(100, 28);
            this.btnAddCall.TabIndex = 31;
            this.btnAddCall.Text = "Add &Call";
            this.btnAddCall.UseVisualStyleBackColor = true;
            this.btnAddCall.Click += new System.EventHandler(this.btnAddCall_Click);
            // 
            // grpBoxCall
            // 
            this.grpBoxCall.Controls.Add(this.tblOpenCall);
            this.grpBoxCall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBoxCall.Location = new System.Drawing.Point(3, 3);
            this.grpBoxCall.Name = "grpBoxCall";
            this.grpBoxCall.Size = new System.Drawing.Size(996, 462);
            this.grpBoxCall.TabIndex = 0;
            this.grpBoxCall.TabStop = false;
            this.grpBoxCall.Text = "Open Call";
            // 
            // tblOpenCall
            // 
            this.tblOpenCall.ColumnCount = 5;
            this.tblOpenCall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblOpenCall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblOpenCall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOpenCall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblOpenCall.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOpenCall.Controls.Add(this.tblLoggedTime, 4, 8);
            this.tblOpenCall.Controls.Add(this.label1, 1, 0);
            this.tblOpenCall.Controls.Add(this.label2, 1, 2);
            this.tblOpenCall.Controls.Add(this.label3, 1, 7);
            this.tblOpenCall.Controls.Add(this.label4, 1, 9);
            this.tblOpenCall.Controls.Add(this.label5, 1, 8);
            this.tblOpenCall.Controls.Add(this.label6, 1, 3);
            this.tblOpenCall.Controls.Add(this.label7, 1, 4);
            this.tblOpenCall.Controls.Add(this.label8, 1, 5);
            this.tblOpenCall.Controls.Add(this.lblRemedy, 1, 12);
            this.tblOpenCall.Controls.Add(this.label10, 1, 11);
            this.tblOpenCall.Controls.Add(this.label9, 1, 10);
            this.tblOpenCall.Controls.Add(this.lblRemedyNotes, 1, 13);
            this.tblOpenCall.Controls.Add(this.label13, 3, 0);
            this.tblOpenCall.Controls.Add(this.label14, 1, 1);
            this.tblOpenCall.Controls.Add(this.label15, 3, 1);
            this.tblOpenCall.Controls.Add(this.label16, 3, 3);
            this.tblOpenCall.Controls.Add(this.label17, 3, 4);
            this.tblOpenCall.Controls.Add(this.label18, 3, 5);
            this.tblOpenCall.Controls.Add(this.label19, 3, 6);
            this.tblOpenCall.Controls.Add(this.label20, 3, 7);
            this.tblOpenCall.Controls.Add(this.lblLogged, 3, 8);
            this.tblOpenCall.Controls.Add(this.lblDespatched, 3, 9);
            this.tblOpenCall.Controls.Add(this.lblAcknowledged, 3, 10);
            this.tblOpenCall.Controls.Add(this.lblArrived, 3, 11);
            this.tblOpenCall.Controls.Add(this.lblCompleted, 3, 12);
            this.tblOpenCall.Controls.Add(this.cmbCallStatus, 2, 0);
            this.tblOpenCall.Controls.Add(this.cmbCallSource, 2, 2);
            this.tblOpenCall.Controls.Add(this.cmbMachineType, 2, 7);
            this.tblOpenCall.Controls.Add(this.cmbMachineName, 2, 9);
            this.tblOpenCall.Controls.Add(this.cmbFaultGroup, 2, 3);
            this.tblOpenCall.Controls.Add(this.cmbFault, 2, 4);
            this.tblOpenCall.Controls.Add(this.cmbEngineer, 2, 11);
            this.tblOpenCall.Controls.Add(this.cmbRemedy, 2, 12);
            this.tblOpenCall.Controls.Add(this.cmbSites, 2, 1);
            this.tblOpenCall.Controls.Add(this.txtFaultNotes, 2, 5);
            this.tblOpenCall.Controls.Add(this.txtRemedyNotes, 2, 13);
            this.tblOpenCall.Controls.Add(this.chkShowAllEng, 2, 10);
            this.tblOpenCall.Controls.Add(this.txtSubCompany, 4, 0);
            this.tblOpenCall.Controls.Add(this.txtPostcode, 4, 3);
            this.tblOpenCall.Controls.Add(this.txtContact, 4, 4);
            this.tblOpenCall.Controls.Add(this.txtContactPhone, 4, 5);
            this.tblOpenCall.Controls.Add(this.txtOpenHours, 4, 6);
            this.tblOpenCall.Controls.Add(this.txtDepot, 4, 7);
            this.tblOpenCall.Controls.Add(this.txtSiteDetails, 4, 1);
            this.tblOpenCall.Controls.Add(this.chkShowMachineHistory, 2, 8);
            this.tblOpenCall.Controls.Add(this.tblCompletedTime, 4, 12);
            this.tblOpenCall.Controls.Add(this.tblArrivedTime, 4, 11);
            this.tblOpenCall.Controls.Add(this.tblEngrAckTime, 4, 10);
            this.tblOpenCall.Controls.Add(this.tblPassedToEngTime, 4, 9);
            this.tblOpenCall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOpenCall.Location = new System.Drawing.Point(3, 16);
            this.tblOpenCall.Name = "tblOpenCall";
            this.tblOpenCall.RowCount = 15;
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666667F));
            this.tblOpenCall.Size = new System.Drawing.Size(990, 443);
            this.tblOpenCall.TabIndex = 0;
            // 
            // tblLoggedTime
            // 
            this.tblLoggedTime.ColumnCount = 3;
            this.tblLoggedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblLoggedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoggedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoggedTime.Controls.Add(this.dtpReceivedTime, 0, 0);
            this.tblLoggedTime.Controls.Add(this.chkReceivedTime, 2, 0);
            this.tblLoggedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoggedTime.Location = new System.Drawing.Point(648, 232);
            this.tblLoggedTime.Margin = new System.Windows.Forms.Padding(3, 0, 25, 3);
            this.tblLoggedTime.Name = "tblLoggedTime";
            this.tblLoggedTime.RowCount = 1;
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblLoggedTime.Size = new System.Drawing.Size(317, 26);
            this.tblLoggedTime.TabIndex = 54;
            // 
            // dtpReceivedTime
            // 
            this.dtpReceivedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpReceivedTime.Enabled = false;
            this.dtpReceivedTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceivedTime.Location = new System.Drawing.Point(3, 1);
            this.dtpReceivedTime.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dtpReceivedTime.Name = "dtpReceivedTime";
            this.dtpReceivedTime.Size = new System.Drawing.Size(197, 20);
            this.dtpReceivedTime.TabIndex = 21;
            // 
            // chkReceivedTime
            // 
            this.chkReceivedTime.AutoSize = true;
            this.chkReceivedTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkReceivedTime.Location = new System.Drawing.Point(226, 3);
            this.chkReceivedTime.Name = "chkReceivedTime";
            this.chkReceivedTime.Size = new System.Drawing.Size(88, 14);
            this.chkReceivedTime.TabIndex = 20;
            this.chkReceivedTime.UseVisualStyleBackColor = true;
            this.chkReceivedTime.CheckedChanged += new System.EventHandler(this.chkReceivedTime_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(23, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "* Call Status:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(23, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "* Call Source: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(23, 206);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Machine Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(23, 264);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Machine Name:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(23, 235);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Show Machine History:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(23, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 23);
            this.label6.TabIndex = 3;
            this.label6.Text = "* Fault Group:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(23, 119);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "* Fault:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(23, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Fault Notes:";
            // 
            // lblRemedy
            // 
            this.lblRemedy.AutoSize = true;
            this.lblRemedy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemedy.Location = new System.Drawing.Point(23, 351);
            this.lblRemedy.Margin = new System.Windows.Forms.Padding(3);
            this.lblRemedy.Name = "lblRemedy";
            this.lblRemedy.Size = new System.Drawing.Size(116, 23);
            this.lblRemedy.TabIndex = 10;
            this.lblRemedy.Text = "Remedy:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(23, 322);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 23);
            this.label10.TabIndex = 9;
            this.label10.Text = "Engineer:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 296);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Show all Eng.:";
            // 
            // lblRemedyNotes
            // 
            this.lblRemedyNotes.AutoSize = true;
            this.lblRemedyNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemedyNotes.Location = new System.Drawing.Point(23, 380);
            this.lblRemedyNotes.Margin = new System.Windows.Forms.Padding(3);
            this.lblRemedyNotes.Name = "lblRemedyNotes";
            this.lblRemedyNotes.Size = new System.Drawing.Size(116, 23);
            this.lblRemedyNotes.TabIndex = 11;
            this.lblRemedyNotes.Text = "Remedy Notes:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(490, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 23);
            this.label13.TabIndex = 12;
            this.label13.Text = "Sub Company:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(23, 32);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 23);
            this.label14.TabIndex = 13;
            this.label14.Text = "* Site Name:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(490, 32);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(152, 23);
            this.label15.TabIndex = 14;
            this.label15.Text = "Site Details:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(490, 90);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 23);
            this.label16.TabIndex = 15;
            this.label16.Text = "Postcode:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(490, 119);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(152, 23);
            this.label17.TabIndex = 16;
            this.label17.Text = "Contact:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(490, 148);
            this.label18.Margin = new System.Windows.Forms.Padding(3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(152, 23);
            this.label18.TabIndex = 17;
            this.label18.Text = "Contact Phone:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(490, 177);
            this.label19.Margin = new System.Windows.Forms.Padding(3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(152, 23);
            this.label19.TabIndex = 18;
            this.label19.Text = "Opening Hours:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Location = new System.Drawing.Point(490, 206);
            this.label20.Margin = new System.Windows.Forms.Padding(3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(152, 23);
            this.label20.TabIndex = 19;
            this.label20.Text = "Depot:";
            // 
            // lblLogged
            // 
            this.lblLogged.AutoSize = true;
            this.lblLogged.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogged.Location = new System.Drawing.Point(490, 235);
            this.lblLogged.Margin = new System.Windows.Forms.Padding(3);
            this.lblLogged.Name = "lblLogged";
            this.lblLogged.Size = new System.Drawing.Size(152, 23);
            this.lblLogged.TabIndex = 20;
            this.lblLogged.Text = "Time Logged:";
            // 
            // lblDespatched
            // 
            this.lblDespatched.AutoSize = true;
            this.lblDespatched.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDespatched.Location = new System.Drawing.Point(490, 264);
            this.lblDespatched.Margin = new System.Windows.Forms.Padding(3);
            this.lblDespatched.Name = "lblDespatched";
            this.lblDespatched.Size = new System.Drawing.Size(152, 23);
            this.lblDespatched.TabIndex = 21;
            this.lblDespatched.Text = "Time Passed to Engineer:";
            // 
            // lblAcknowledged
            // 
            this.lblAcknowledged.AutoSize = true;
            this.lblAcknowledged.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAcknowledged.Location = new System.Drawing.Point(490, 293);
            this.lblAcknowledged.Margin = new System.Windows.Forms.Padding(3);
            this.lblAcknowledged.Name = "lblAcknowledged";
            this.lblAcknowledged.Size = new System.Drawing.Size(152, 23);
            this.lblAcknowledged.TabIndex = 22;
            this.lblAcknowledged.Text = "Time Engineer Acknowledged:";
            // 
            // lblArrived
            // 
            this.lblArrived.AutoSize = true;
            this.lblArrived.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblArrived.Location = new System.Drawing.Point(490, 322);
            this.lblArrived.Margin = new System.Windows.Forms.Padding(3);
            this.lblArrived.Name = "lblArrived";
            this.lblArrived.Size = new System.Drawing.Size(152, 23);
            this.lblArrived.TabIndex = 23;
            this.lblArrived.Text = "Time Arrived at Site:";
            // 
            // lblCompleted
            // 
            this.lblCompleted.AutoSize = true;
            this.lblCompleted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCompleted.Location = new System.Drawing.Point(490, 351);
            this.lblCompleted.Margin = new System.Windows.Forms.Padding(3);
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.Size = new System.Drawing.Size(152, 23);
            this.lblCompleted.TabIndex = 24;
            this.lblCompleted.Text = "Time Completed:";
            // 
            // cmbCallStatus
            // 
            this.cmbCallStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCallStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCallStatus.FormattingEnabled = true;
            this.cmbCallStatus.Location = new System.Drawing.Point(145, 3);
            this.cmbCallStatus.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbCallStatus.Name = "cmbCallStatus";
            this.cmbCallStatus.Size = new System.Drawing.Size(317, 21);
            this.cmbCallStatus.TabIndex = 0;
            this.cmbCallStatus.SelectedIndexChanged += new System.EventHandler(this.cmbCallStatus_SelectedIndexChanged);
            // 
            // cmbCallSource
            // 
            this.cmbCallSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCallSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCallSource.FormattingEnabled = true;
            this.cmbCallSource.Location = new System.Drawing.Point(145, 61);
            this.cmbCallSource.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbCallSource.Name = "cmbCallSource";
            this.cmbCallSource.Size = new System.Drawing.Size(317, 21);
            this.cmbCallSource.TabIndex = 2;
            // 
            // cmbMachineType
            // 
            this.cmbMachineType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineType.FormattingEnabled = true;
            this.cmbMachineType.Location = new System.Drawing.Point(145, 206);
            this.cmbMachineType.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbMachineType.Name = "cmbMachineType";
            this.cmbMachineType.Size = new System.Drawing.Size(317, 21);
            this.cmbMachineType.TabIndex = 6;
            this.cmbMachineType.SelectedIndexChanged += new System.EventHandler(this.cmbMachineType_SelectedIndexChanged);
            // 
            // cmbMachineName
            // 
            this.cmbMachineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMachineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineName.FormattingEnabled = true;
            this.cmbMachineName.Location = new System.Drawing.Point(145, 264);
            this.cmbMachineName.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbMachineName.Name = "cmbMachineName";
            this.cmbMachineName.Size = new System.Drawing.Size(317, 21);
            this.cmbMachineName.TabIndex = 8;
            // 
            // cmbFaultGroup
            // 
            this.cmbFaultGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFaultGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFaultGroup.FormattingEnabled = true;
            this.cmbFaultGroup.Location = new System.Drawing.Point(145, 90);
            this.cmbFaultGroup.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbFaultGroup.Name = "cmbFaultGroup";
            this.cmbFaultGroup.Size = new System.Drawing.Size(317, 21);
            this.cmbFaultGroup.TabIndex = 3;
            this.cmbFaultGroup.SelectedIndexChanged += new System.EventHandler(this.cmbFaultGroup_SelectedIndexChanged);
            // 
            // cmbFault
            // 
            this.cmbFault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFault.FormattingEnabled = true;
            this.cmbFault.Location = new System.Drawing.Point(145, 119);
            this.cmbFault.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbFault.Name = "cmbFault";
            this.cmbFault.Size = new System.Drawing.Size(317, 21);
            this.cmbFault.TabIndex = 4;
            // 
            // cmbEngineer
            // 
            this.cmbEngineer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEngineer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEngineer.FormattingEnabled = true;
            this.cmbEngineer.Location = new System.Drawing.Point(145, 322);
            this.cmbEngineer.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbEngineer.Name = "cmbEngineer";
            this.cmbEngineer.Size = new System.Drawing.Size(317, 21);
            this.cmbEngineer.TabIndex = 10;
            this.cmbEngineer.SelectedIndexChanged += new System.EventHandler(this.cmbEngineer_SelectedIndexChanged);
            // 
            // cmbRemedy
            // 
            this.cmbRemedy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRemedy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemedy.FormattingEnabled = true;
            this.cmbRemedy.Location = new System.Drawing.Point(145, 351);
            this.cmbRemedy.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbRemedy.Name = "cmbRemedy";
            this.cmbRemedy.Size = new System.Drawing.Size(317, 21);
            this.cmbRemedy.TabIndex = 11;
            // 
            // cmbSites
            // 
            this.cmbSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSites.FormattingEnabled = true;
            this.cmbSites.Location = new System.Drawing.Point(145, 32);
            this.cmbSites.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.cmbSites.Name = "cmbSites";
            this.cmbSites.Size = new System.Drawing.Size(317, 21);
            this.cmbSites.TabIndex = 1;
            this.cmbSites.SelectedIndexChanged += new System.EventHandler(this.cmbSites_SelectedIndexChanged);
            // 
            // txtFaultNotes
            // 
            this.txtFaultNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFaultNotes.Location = new System.Drawing.Point(145, 148);
            this.txtFaultNotes.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtFaultNotes.Name = "txtFaultNotes";
            this.tblOpenCall.SetRowSpan(this.txtFaultNotes, 2);
            this.txtFaultNotes.Size = new System.Drawing.Size(317, 52);
            this.txtFaultNotes.TabIndex = 5;
            this.txtFaultNotes.Text = "";
            // 
            // txtRemedyNotes
            // 
            this.txtRemedyNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemedyNotes.Location = new System.Drawing.Point(145, 380);
            this.txtRemedyNotes.Margin = new System.Windows.Forms.Padding(3, 3, 25, 10);
            this.txtRemedyNotes.Name = "txtRemedyNotes";
            this.tblOpenCall.SetRowSpan(this.txtRemedyNotes, 2);
            this.txtRemedyNotes.Size = new System.Drawing.Size(317, 53);
            this.txtRemedyNotes.TabIndex = 12;
            this.txtRemedyNotes.Text = "";
            // 
            // chkShowAllEng
            // 
            this.chkShowAllEng.AutoSize = true;
            this.chkShowAllEng.Location = new System.Drawing.Point(145, 296);
            this.chkShowAllEng.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkShowAllEng.Name = "chkShowAllEng";
            this.chkShowAllEng.Size = new System.Drawing.Size(15, 14);
            this.chkShowAllEng.TabIndex = 9;
            this.chkShowAllEng.UseVisualStyleBackColor = true;
            this.chkShowAllEng.CheckedChanged += new System.EventHandler(this.chkShowAllEng_CheckedChanged);
            // 
            // txtSubCompany
            // 
            this.txtSubCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubCompany.Enabled = false;
            this.txtSubCompany.Location = new System.Drawing.Point(648, 3);
            this.txtSubCompany.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtSubCompany.Name = "txtSubCompany";
            this.txtSubCompany.Size = new System.Drawing.Size(317, 20);
            this.txtSubCompany.TabIndex = 13;
            // 
            // txtPostcode
            // 
            this.txtPostcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPostcode.Enabled = false;
            this.txtPostcode.Location = new System.Drawing.Point(648, 90);
            this.txtPostcode.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtPostcode.Name = "txtPostcode";
            this.txtPostcode.Size = new System.Drawing.Size(317, 20);
            this.txtPostcode.TabIndex = 15;
            // 
            // txtContact
            // 
            this.txtContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContact.Enabled = false;
            this.txtContact.Location = new System.Drawing.Point(648, 119);
            this.txtContact.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(317, 20);
            this.txtContact.TabIndex = 6;
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContactPhone.Enabled = false;
            this.txtContactPhone.Location = new System.Drawing.Point(648, 148);
            this.txtContactPhone.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(317, 20);
            this.txtContactPhone.TabIndex = 17;
            // 
            // txtOpenHours
            // 
            this.txtOpenHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOpenHours.Enabled = false;
            this.txtOpenHours.Location = new System.Drawing.Point(648, 177);
            this.txtOpenHours.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtOpenHours.Name = "txtOpenHours";
            this.txtOpenHours.Size = new System.Drawing.Size(317, 20);
            this.txtOpenHours.TabIndex = 18;
            // 
            // txtDepot
            // 
            this.txtDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDepot.Enabled = false;
            this.txtDepot.Location = new System.Drawing.Point(648, 206);
            this.txtDepot.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtDepot.Name = "txtDepot";
            this.txtDepot.Size = new System.Drawing.Size(317, 20);
            this.txtDepot.TabIndex = 19;
            // 
            // txtSiteDetails
            // 
            this.txtSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSiteDetails.Enabled = false;
            this.txtSiteDetails.Location = new System.Drawing.Point(648, 32);
            this.txtSiteDetails.Margin = new System.Windows.Forms.Padding(3, 3, 25, 3);
            this.txtSiteDetails.Name = "txtSiteDetails";
            this.tblOpenCall.SetRowSpan(this.txtSiteDetails, 2);
            this.txtSiteDetails.Size = new System.Drawing.Size(317, 52);
            this.txtSiteDetails.TabIndex = 14;
            this.txtSiteDetails.Text = "";
            // 
            // chkShowMachineHistory
            // 
            this.chkShowMachineHistory.AutoSize = true;
            this.chkShowMachineHistory.Location = new System.Drawing.Point(145, 238);
            this.chkShowMachineHistory.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.chkShowMachineHistory.Name = "chkShowMachineHistory";
            this.chkShowMachineHistory.Size = new System.Drawing.Size(15, 14);
            this.chkShowMachineHistory.TabIndex = 7;
            this.chkShowMachineHistory.UseVisualStyleBackColor = true;
            this.chkShowMachineHistory.CheckedChanged += new System.EventHandler(this.chkShowMachineHistory_CheckedChanged);
            // 
            // tblCompletedTime
            // 
            this.tblCompletedTime.ColumnCount = 3;
            this.tblCompletedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblCompletedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblCompletedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCompletedTime.Controls.Add(this.dtpCompletedTime, 0, 0);
            this.tblCompletedTime.Controls.Add(this.chkCompletedTime, 2, 0);
            this.tblCompletedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCompletedTime.Location = new System.Drawing.Point(648, 348);
            this.tblCompletedTime.Margin = new System.Windows.Forms.Padding(3, 0, 25, 3);
            this.tblCompletedTime.Name = "tblCompletedTime";
            this.tblCompletedTime.RowCount = 1;
            this.tblCompletedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCompletedTime.Size = new System.Drawing.Size(317, 26);
            this.tblCompletedTime.TabIndex = 50;
            // 
            // dtpCompletedTime
            // 
            this.dtpCompletedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCompletedTime.Enabled = false;
            this.dtpCompletedTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCompletedTime.Location = new System.Drawing.Point(3, 1);
            this.dtpCompletedTime.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dtpCompletedTime.Name = "dtpCompletedTime";
            this.dtpCompletedTime.Size = new System.Drawing.Size(197, 20);
            this.dtpCompletedTime.TabIndex = 29;
            // 
            // chkCompletedTime
            // 
            this.chkCompletedTime.AutoSize = true;
            this.chkCompletedTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkCompletedTime.Enabled = false;
            this.chkCompletedTime.Location = new System.Drawing.Point(226, 3);
            this.chkCompletedTime.Name = "chkCompletedTime";
            this.chkCompletedTime.Size = new System.Drawing.Size(88, 14);
            this.chkCompletedTime.TabIndex = 28;
            this.chkCompletedTime.UseVisualStyleBackColor = true;
            this.chkCompletedTime.CheckedChanged += new System.EventHandler(this.chkCompletedTime_CheckedChanged);
            // 
            // tblArrivedTime
            // 
            this.tblArrivedTime.ColumnCount = 3;
            this.tblArrivedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblArrivedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblArrivedTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblArrivedTime.Controls.Add(this.dtpArrivalTime, 0, 0);
            this.tblArrivedTime.Controls.Add(this.chkArrivalTime, 2, 0);
            this.tblArrivedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblArrivedTime.Location = new System.Drawing.Point(648, 319);
            this.tblArrivedTime.Margin = new System.Windows.Forms.Padding(3, 0, 25, 3);
            this.tblArrivedTime.Name = "tblArrivedTime";
            this.tblArrivedTime.RowCount = 1;
            this.tblArrivedTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblArrivedTime.Size = new System.Drawing.Size(317, 26);
            this.tblArrivedTime.TabIndex = 51;
            // 
            // dtpArrivalTime
            // 
            this.dtpArrivalTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpArrivalTime.Enabled = false;
            this.dtpArrivalTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpArrivalTime.Location = new System.Drawing.Point(3, 1);
            this.dtpArrivalTime.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dtpArrivalTime.Name = "dtpArrivalTime";
            this.dtpArrivalTime.Size = new System.Drawing.Size(197, 20);
            this.dtpArrivalTime.TabIndex = 27;
            // 
            // chkArrivalTime
            // 
            this.chkArrivalTime.AutoSize = true;
            this.chkArrivalTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkArrivalTime.Enabled = false;
            this.chkArrivalTime.Location = new System.Drawing.Point(226, 3);
            this.chkArrivalTime.Name = "chkArrivalTime";
            this.chkArrivalTime.Size = new System.Drawing.Size(88, 14);
            this.chkArrivalTime.TabIndex = 26;
            this.chkArrivalTime.UseVisualStyleBackColor = true;
            this.chkArrivalTime.CheckedChanged += new System.EventHandler(this.chkArrivalTime_CheckedChanged);
            // 
            // tblEngrAckTime
            // 
            this.tblEngrAckTime.ColumnCount = 3;
            this.tblEngrAckTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEngrAckTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblEngrAckTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEngrAckTime.Controls.Add(this.dtpEngrAckTime, 0, 0);
            this.tblEngrAckTime.Controls.Add(this.chkEngrAckTime, 2, 0);
            this.tblEngrAckTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEngrAckTime.Location = new System.Drawing.Point(648, 290);
            this.tblEngrAckTime.Margin = new System.Windows.Forms.Padding(3, 0, 25, 3);
            this.tblEngrAckTime.Name = "tblEngrAckTime";
            this.tblEngrAckTime.RowCount = 1;
            this.tblEngrAckTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblEngrAckTime.Size = new System.Drawing.Size(317, 26);
            this.tblEngrAckTime.TabIndex = 52;
            // 
            // dtpEngrAckTime
            // 
            this.dtpEngrAckTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpEngrAckTime.Enabled = false;
            this.dtpEngrAckTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEngrAckTime.Location = new System.Drawing.Point(3, 1);
            this.dtpEngrAckTime.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dtpEngrAckTime.Name = "dtpEngrAckTime";
            this.dtpEngrAckTime.Size = new System.Drawing.Size(197, 20);
            this.dtpEngrAckTime.TabIndex = 25;
            // 
            // chkEngrAckTime
            // 
            this.chkEngrAckTime.AutoSize = true;
            this.chkEngrAckTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEngrAckTime.Enabled = false;
            this.chkEngrAckTime.Location = new System.Drawing.Point(226, 3);
            this.chkEngrAckTime.Name = "chkEngrAckTime";
            this.chkEngrAckTime.Size = new System.Drawing.Size(88, 14);
            this.chkEngrAckTime.TabIndex = 24;
            this.chkEngrAckTime.UseVisualStyleBackColor = true;
            this.chkEngrAckTime.CheckedChanged += new System.EventHandler(this.chkEngrAckTime_CheckedChanged);
            // 
            // tblPassedToEngTime
            // 
            this.tblPassedToEngTime.ColumnCount = 3;
            this.tblPassedToEngTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPassedToEngTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblPassedToEngTime.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPassedToEngTime.Controls.Add(this.dtpPassedToEngrTime, 0, 0);
            this.tblPassedToEngTime.Controls.Add(this.chkPassedToEngrTime, 2, 0);
            this.tblPassedToEngTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPassedToEngTime.Location = new System.Drawing.Point(648, 261);
            this.tblPassedToEngTime.Margin = new System.Windows.Forms.Padding(3, 0, 25, 3);
            this.tblPassedToEngTime.Name = "tblPassedToEngTime";
            this.tblPassedToEngTime.RowCount = 1;
            this.tblPassedToEngTime.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPassedToEngTime.Size = new System.Drawing.Size(317, 26);
            this.tblPassedToEngTime.TabIndex = 53;
            // 
            // dtpPassedToEngrTime
            // 
            this.dtpPassedToEngrTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpPassedToEngrTime.Enabled = false;
            this.dtpPassedToEngrTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPassedToEngrTime.Location = new System.Drawing.Point(3, 1);
            this.dtpPassedToEngrTime.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.dtpPassedToEngrTime.Name = "dtpPassedToEngrTime";
            this.dtpPassedToEngrTime.Size = new System.Drawing.Size(197, 20);
            this.dtpPassedToEngrTime.TabIndex = 23;
            // 
            // chkPassedToEngrTime
            // 
            this.chkPassedToEngrTime.AutoSize = true;
            this.chkPassedToEngrTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkPassedToEngrTime.Enabled = false;
            this.chkPassedToEngrTime.Location = new System.Drawing.Point(226, 3);
            this.chkPassedToEngrTime.Name = "chkPassedToEngrTime";
            this.chkPassedToEngrTime.Size = new System.Drawing.Size(88, 14);
            this.chkPassedToEngrTime.TabIndex = 22;
            this.chkPassedToEngrTime.UseVisualStyleBackColor = true;
            this.chkPassedToEngrTime.CheckedChanged += new System.EventHandler(this.chkPassedToEngrTime_CheckedChanged);
            // 
            // frmServiceCallCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 625);
            this.Controls.Add(this.tblMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServiceCallCreate";
            this.ShowInTaskbar = false;
            this.Text = "Logging Call - (Call ID)";
            this.Load += new System.EventHandler(this.frmServiceCallCreate_Load);
            this.tblMain.ResumeLayout(false);
            this.tblBottomPane.ResumeLayout(false);
            this.grpNotes.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.grpBoxCall.ResumeLayout(false);
            this.tblOpenCall.ResumeLayout(false);
            this.tblOpenCall.PerformLayout();
            this.tblLoggedTime.ResumeLayout(false);
            this.tblLoggedTime.PerformLayout();
            this.tblCompletedTime.ResumeLayout(false);
            this.tblCompletedTime.PerformLayout();
            this.tblArrivedTime.ResumeLayout(false);
            this.tblArrivedTime.PerformLayout();
            this.tblEngrAckTime.ResumeLayout(false);
            this.tblEngrAckTime.PerformLayout();
            this.tblPassedToEngTime.ResumeLayout(false);
            this.tblPassedToEngTime.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.GroupBox grpBoxCall;
        private System.Windows.Forms.TableLayoutPanel tblOpenCall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRemedy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblLogged;
        private System.Windows.Forms.Label lblDespatched;
        private System.Windows.Forms.Label lblAcknowledged;
        private System.Windows.Forms.Label lblArrived;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.ComboBox cmbCallStatus;
        private System.Windows.Forms.ComboBox cmbCallSource;
        private System.Windows.Forms.ComboBox cmbMachineType;
        private System.Windows.Forms.ComboBox cmbMachineName;
        private System.Windows.Forms.ComboBox cmbFaultGroup;
        private System.Windows.Forms.ComboBox cmbFault;
        private System.Windows.Forms.ComboBox cmbEngineer;
        private System.Windows.Forms.ComboBox cmbRemedy;
        private System.Windows.Forms.ComboBox cmbSites;
        private System.Windows.Forms.RichTextBox txtFaultNotes;
        private System.Windows.Forms.RichTextBox txtRemedyNotes;
        private System.Windows.Forms.CheckBox chkShowAllEng;
        private System.Windows.Forms.TextBox txtSubCompany;
        private System.Windows.Forms.TextBox txtPostcode;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.TextBox txtOpenHours;
        private System.Windows.Forms.TextBox txtDepot;
        private System.Windows.Forms.RichTextBox txtSiteDetails;
        private System.Windows.Forms.CheckBox chkShowMachineHistory;
        private System.Windows.Forms.TableLayoutPanel tblCompletedTime;
        private System.Windows.Forms.CheckBox chkCompletedTime;
        private System.Windows.Forms.TableLayoutPanel tblLoggedTime;
        private System.Windows.Forms.DateTimePicker dtpReceivedTime;
        private System.Windows.Forms.CheckBox chkReceivedTime;
        private System.Windows.Forms.DateTimePicker dtpCompletedTime;
        private System.Windows.Forms.TableLayoutPanel tblArrivedTime;
        private System.Windows.Forms.DateTimePicker dtpArrivalTime;
        private System.Windows.Forms.CheckBox chkArrivalTime;
        private System.Windows.Forms.TableLayoutPanel tblEngrAckTime;
        private System.Windows.Forms.DateTimePicker dtpEngrAckTime;
        private System.Windows.Forms.CheckBox chkEngrAckTime;
        private System.Windows.Forms.TableLayoutPanel tblPassedToEngTime;
        private System.Windows.Forms.DateTimePicker dtpPassedToEngrTime;
        private System.Windows.Forms.CheckBox chkPassedToEngrTime;
        private System.Windows.Forms.TableLayoutPanel tblBottomPane;
        private System.Windows.Forms.GroupBox grpNotes;
        private System.Windows.Forms.ListView lvNotes;
        private System.Windows.Forms.ColumnHeader colUser;
        private System.Windows.Forms.ColumnHeader colNote;
        private System.Windows.Forms.ColumnHeader colDateSent;
        private System.Windows.Forms.Label lblRemedyNotes;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Button btnAddCall;
    }
}