namespace BMC.EnterpriseClient.Views
{
    partial class frmPurgeConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPurgeConfiguration));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpPurgeConfiguration = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txtLogItem = new System.Windows.Forms.TextBox();
            this.btnAddLogItem = new System.Windows.Forms.Button();
            this.tvLogPurge = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDBItem = new System.Windows.Forms.TextBox();
            this.btnAddDBItem = new System.Windows.Forms.Button();
            this.tvDBPurge = new System.Windows.Forms.TreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.LogPurgeInterval = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.chkLogPurgingNeeded = new System.Windows.Forms.CheckBox();
            this.chkDBPurgingEnabled = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkPurgeNeeded = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DBPurgeInterval = new System.Windows.Forms.NumericUpDown();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbpHistory = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uxPurgeHistory = new BMC.CoreLib.Win32.UxHeaderContent();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tvtables = new System.Windows.Forms.TreeView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpPurgeConfiguration.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogPurgeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBPurgeInterval)).BeginInit();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tbpHistory.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 667);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 667);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpPurgeConfiguration);
            this.tabControl1.Controls.Add(this.tbpHistory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(613, 647);
            this.tabControl1.TabIndex = 1;
            // 
            // tbpPurgeConfiguration
            // 
            this.tbpPurgeConfiguration.BackColor = System.Drawing.SystemColors.Control;
            this.tbpPurgeConfiguration.Controls.Add(this.panel3);
            this.tbpPurgeConfiguration.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpPurgeConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tbpPurgeConfiguration.Name = "tbpPurgeConfiguration";
            this.tbpPurgeConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPurgeConfiguration.Size = new System.Drawing.Size(605, 621);
            this.tbpPurgeConfiguration.TabIndex = 0;
            this.tbpPurgeConfiguration.Text = "Configuration";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(599, 615);
            this.panel3.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(599, 615);
            this.panel5.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 121);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(599, 436);
            this.panel7.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(599, 436);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PurgeCategory";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 416F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(593, 416);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(299, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(291, 410);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log Purge";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.44855F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.55145F));
            this.tableLayoutPanel5.Controls.Add(this.txtLogItem, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnAddLogItem, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.tvLogPurge, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.04878F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.95122F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(285, 390);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // txtLogItem
            // 
            this.txtLogItem.Location = new System.Drawing.Point(3, 3);
            this.txtLogItem.Name = "txtLogItem";
            this.txtLogItem.Size = new System.Drawing.Size(194, 21);
            this.txtLogItem.TabIndex = 0;
            // 
            // btnAddLogItem
            // 
            this.btnAddLogItem.Location = new System.Drawing.Point(203, 3);
            this.btnAddLogItem.Name = "btnAddLogItem";
            this.btnAddLogItem.Size = new System.Drawing.Size(79, 25);
            this.btnAddLogItem.TabIndex = 1;
            this.btnAddLogItem.Text = "Add";
            this.btnAddLogItem.UseVisualStyleBackColor = true;
            // 
            // tvLogPurge
            // 
            this.tvLogPurge.CheckBoxes = true;
            this.tableLayoutPanel5.SetColumnSpan(this.tvLogPurge, 2);
            this.tvLogPurge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLogPurge.Location = new System.Drawing.Point(3, 34);
            this.tvLogPurge.Name = "tvLogPurge";
            this.tvLogPurge.Size = new System.Drawing.Size(279, 353);
            this.tvLogPurge.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 410);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DB Purge";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.44855F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.55145F));
            this.tableLayoutPanel4.Controls.Add(this.txtDBItem, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnAddDBItem, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tvDBPurge, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.04878F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.95122F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(284, 390);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // txtDBItem
            // 
            this.txtDBItem.Location = new System.Drawing.Point(3, 3);
            this.txtDBItem.Name = "txtDBItem";
            this.txtDBItem.Size = new System.Drawing.Size(194, 21);
            this.txtDBItem.TabIndex = 0;
            // 
            // btnAddDBItem
            // 
            this.btnAddDBItem.Location = new System.Drawing.Point(203, 3);
            this.btnAddDBItem.Name = "btnAddDBItem";
            this.btnAddDBItem.Size = new System.Drawing.Size(78, 25);
            this.btnAddDBItem.TabIndex = 1;
            this.btnAddDBItem.Text = "Add";
            this.btnAddDBItem.UseVisualStyleBackColor = true;
            // 
            // tvDBPurge
            // 
            this.tvDBPurge.CheckBoxes = true;
            this.tableLayoutPanel4.SetColumnSpan(this.tvDBPurge, 2);
            this.tvDBPurge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDBPurge.Location = new System.Drawing.Point(3, 34);
            this.tvDBPurge.Name = "tvDBPurge";
            this.tvDBPurge.Size = new System.Drawing.Size(278, 353);
            this.tvDBPurge.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.groupBox6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(599, 121);
            this.panel4.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(599, 121);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Purge Settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.913669F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.77833F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.46305F));
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.LogPurgeInterval, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkLogPurgingNeeded, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.chkDBPurgingEnabled, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkPurgeNeeded, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.DBPurgeInterval, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.49F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(593, 100);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(197, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 26);
            this.label8.TabIndex = 8;
            this.label8.Text = "Log Purge Interval";
            // 
            // LogPurgeInterval
            // 
            this.LogPurgeInterval.Location = new System.Drawing.Point(284, 36);
            this.LogPurgeInterval.Name = "LogPurgeInterval";
            this.LogPurgeInterval.Size = new System.Drawing.Size(234, 21);
            this.LogPurgeInterval.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Is Log Purging Enabled";
            // 
            // chkLogPurgingNeeded
            // 
            this.chkLogPurgingNeeded.AutoSize = true;
            this.chkLogPurgingNeeded.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLogPurgingNeeded.Location = new System.Drawing.Point(151, 69);
            this.chkLogPurgingNeeded.Name = "chkLogPurgingNeeded";
            this.chkLogPurgingNeeded.Size = new System.Drawing.Size(30, 17);
            this.chkLogPurgingNeeded.TabIndex = 5;
            this.chkLogPurgingNeeded.Text = " ";
            this.chkLogPurgingNeeded.UseVisualStyleBackColor = true;
            // 
            // chkDBPurgingEnabled
            // 
            this.chkDBPurgingEnabled.AutoSize = true;
            this.chkDBPurgingEnabled.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDBPurgingEnabled.Location = new System.Drawing.Point(151, 36);
            this.chkDBPurgingEnabled.Name = "chkDBPurgingEnabled";
            this.chkDBPurgingEnabled.Size = new System.Drawing.Size(30, 17);
            this.chkDBPurgingEnabled.TabIndex = 3;
            this.chkDBPurgingEnabled.Text = " ";
            this.chkDBPurgingEnabled.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Purge Needed";
            // 
            // chkPurgeNeeded
            // 
            this.chkPurgeNeeded.AutoSize = true;
            this.chkPurgeNeeded.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPurgeNeeded.Location = new System.Drawing.Point(151, 3);
            this.chkPurgeNeeded.Name = "chkPurgeNeeded";
            this.chkPurgeNeeded.Size = new System.Drawing.Size(30, 17);
            this.chkPurgeNeeded.TabIndex = 1;
            this.chkPurgeNeeded.Text = " ";
            this.chkPurgeNeeded.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Is DB Purging Enabled";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(197, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "DB Purge Interval";
            // 
            // DBPurgeInterval
            // 
            this.DBPurgeInterval.Location = new System.Drawing.Point(284, 3);
            this.DBPurgeInterval.Name = "DBPurgeInterval";
            this.DBPurgeInterval.Size = new System.Drawing.Size(234, 21);
            this.DBPurgeInterval.TabIndex = 7;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tableLayoutPanel3);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 557);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(599, 58);
            this.panel6.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnSave, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(599, 58);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(508, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 39);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbpHistory
            // 
            this.tbpHistory.Controls.Add(this.panel2);
            this.tbpHistory.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpHistory.Location = new System.Drawing.Point(4, 22);
            this.tbpHistory.Name = "tbpHistory";
            this.tbpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tbpHistory.Size = new System.Drawing.Size(605, 621);
            this.tbpHistory.TabIndex = 1;
            this.tbpHistory.Text = "Purge History";
            this.tbpHistory.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uxPurgeHistory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(599, 615);
            this.panel2.TabIndex = 0;
            // 
            // uxPurgeHistory
            // 
            // 
            // uxPurgeHistory.Child
            // 
            this.uxPurgeHistory.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPurgeHistory.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxPurgeHistory.ChildContainer.Name = "Child";
            this.uxPurgeHistory.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxPurgeHistory.ChildContainer.Size = new System.Drawing.Size(599, 589);
            this.uxPurgeHistory.ChildContainer.TabIndex = 2;
            this.uxPurgeHistory.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxPurgeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxPurgeHistory.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxPurgeHistory.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxPurgeHistory.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxPurgeHistory.HeaderText = "Purge History";
            this.uxPurgeHistory.IsClosable = false;
            this.uxPurgeHistory.Location = new System.Drawing.Point(0, 0);
            this.uxPurgeHistory.Name = "uxPurgeHistory";
            this.uxPurgeHistory.PinVisible = false;
            this.uxPurgeHistory.Size = new System.Drawing.Size(599, 615);
            this.uxPurgeHistory.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxPurgeHistory.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tvtables);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox5.Location = new System.Drawing.Point(619, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(245, 667);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tables";
            // 
            // tvtables
            // 
            this.tvtables.CheckBoxes = true;
            this.tvtables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvtables.Location = new System.Drawing.Point(3, 17);
            this.tvtables.Name = "tvtables";
            this.tvtables.Size = new System.Drawing.Size(239, 647);
            this.tvtables.TabIndex = 0;
            // 
            // frmPurgeConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 667);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPurgeConfiguration";
            this.Text = "Purge Details";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpPurgeConfiguration.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogPurgeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBPurgeInterval)).EndInit();
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tbpHistory.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TreeView tvtables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpPurgeConfiguration;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox txtLogItem;
        private System.Windows.Forms.Button btnAddLogItem;
        private System.Windows.Forms.TreeView tvLogPurge;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox txtDBItem;
        private System.Windows.Forms.Button btnAddDBItem;
        private System.Windows.Forms.TreeView tvDBPurge;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown LogPurgeInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkLogPurgingNeeded;
        private System.Windows.Forms.CheckBox chkDBPurgingEnabled;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPurgeNeeded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown DBPurgeInterval;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tbpHistory;
        private System.Windows.Forms.Panel panel2;
        private CoreLib.Win32.UxHeaderContent uxPurgeHistory;
    }
}