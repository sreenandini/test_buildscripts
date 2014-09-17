namespace BMCMeterAnalysis
{
    partial class ucBMCMeterAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBMCMeterAnalysis));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.axMSChartMeterGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdnPeriod = new System.Windows.Forms.RadioButton();
            this.rdnWeek = new System.Windows.Forms.RadioButton();
            this.rdnDay = new System.Windows.Forms.RadioButton();
            this.cmbDatatype = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cmbCriteria = new System.Windows.Forms.ComboBox();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.lblDataType = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.rdnPayout = new System.Windows.Forms.RadioButton();
            this.rdnHold = new System.Windows.Forms.RadioButton();
            this.cmbRecordCount = new System.Windows.Forms.ComboBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cmbGroupBy = new System.Windows.Forms.ComboBox();
            this.lblGroupBy = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cmbDepot = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbOperator = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmbManufacturer = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGame = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowTree = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubCompany = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkActiveSites = new System.Windows.Forms.CheckBox();
            this.tvSiteDetails = new System.Windows.Forms.TreeView();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.statusBar2 = new System.Windows.Forms.StatusBar();
            this.dgMeterDetails = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraph)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMeterDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.axMSChartMeterGraph);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnProcess);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(252, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 444);
            this.panel1.TabIndex = 2;
            // 
            // axMSChartMeterGraph
            // 
            this.axMSChartMeterGraph.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
            this.axMSChartMeterGraph.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.axMSChartMeterGraph.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.axMSChartMeterGraph.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.axMSChartMeterGraph.BorderlineWidth = 2;
            this.axMSChartMeterGraph.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea2.Area3DStyle.Inclination = 40;
            chartArea2.Area3DStyle.IsClustered = true;
            chartArea2.Area3DStyle.IsRightAngleAxes = false;
            chartArea2.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea2.Area3DStyle.Perspective = 9;
            chartArea2.Area3DStyle.Rotation = 25;
            chartArea2.Area3DStyle.WallWidth = 3;
            chartArea2.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea2.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea2.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.BackColor = System.Drawing.Color.OldLace;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.BackSecondaryColor = System.Drawing.Color.White;
            chartArea2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "Default";
            chartArea2.ShadowColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraph.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend2.IsDockedInsideChartArea = false;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Default";
            this.axMSChartMeterGraph.Legends.Add(legend2);
            this.axMSChartMeterGraph.Location = new System.Drawing.Point(271, 103);
            this.axMSChartMeterGraph.Name = "axMSChartMeterGraph";
            series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series3.BorderWidth = 3;
            series3.ChartArea = "Default";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
            series3.Legend = "Default";
            series3.MarkerSize = 8;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "Series1";
            series3.ShadowColor = System.Drawing.Color.Black;
            series3.ShadowOffset = 2;
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series4.BorderWidth = 3;
            series4.ChartArea = "Default";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            series4.Legend = "Default";
            series4.MarkerSize = 9;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Diamond;
            series4.Name = "Series2";
            series4.ShadowColor = System.Drawing.Color.Black;
            series4.ShadowOffset = 2;
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.axMSChartMeterGraph.Series.Add(series3);
            this.axMSChartMeterGraph.Series.Add(series4);
            this.axMSChartMeterGraph.Size = new System.Drawing.Size(484, 336);
            this.axMSChartMeterGraph.TabIndex = 10;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(187, 400);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 36);
            this.btnPrint.TabIndex = 8;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(146, 400);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(116, 36);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnProcess.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.ForeColor = System.Drawing.Color.White;
            this.btnProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnProcess.Image")));
            this.btnProcess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProcess.Location = new System.Drawing.Point(4, 400);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(118, 36);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Process";
            this.btnProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.rdnPeriod);
            this.panel4.Controls.Add(this.rdnWeek);
            this.panel4.Controls.Add(this.rdnDay);
            this.panel4.Controls.Add(this.cmbDatatype);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(271, 70);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(484, 27);
            this.panel4.TabIndex = 5;
            // 
            // rdnPeriod
            // 
            this.rdnPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdnPeriod.AutoSize = true;
            this.rdnPeriod.ForeColor = System.Drawing.Color.White;
            this.rdnPeriod.Location = new System.Drawing.Point(334, 4);
            this.rdnPeriod.Name = "rdnPeriod";
            this.rdnPeriod.Size = new System.Drawing.Size(70, 17);
            this.rdnPeriod.TabIndex = 5;
            this.rdnPeriod.Text = "By Period";
            this.rdnPeriod.UseVisualStyleBackColor = true;
            this.rdnPeriod.CheckedChanged += new System.EventHandler(this.rdnPeriod_CheckedChanged);
            // 
            // rdnWeek
            // 
            this.rdnWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdnWeek.AutoSize = true;
            this.rdnWeek.Checked = true;
            this.rdnWeek.ForeColor = System.Drawing.Color.White;
            this.rdnWeek.Location = new System.Drawing.Point(241, 4);
            this.rdnWeek.Name = "rdnWeek";
            this.rdnWeek.Size = new System.Drawing.Size(69, 17);
            this.rdnWeek.TabIndex = 4;
            this.rdnWeek.TabStop = true;
            this.rdnWeek.Text = "By Week";
            this.rdnWeek.UseVisualStyleBackColor = true;
            this.rdnWeek.CheckedChanged += new System.EventHandler(this.rdnWeek_CheckedChanged);
            // 
            // rdnDay
            // 
            this.rdnDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.rdnDay.AutoSize = true;
            this.rdnDay.ForeColor = System.Drawing.Color.White;
            this.rdnDay.Location = new System.Drawing.Point(161, 4);
            this.rdnDay.Name = "rdnDay";
            this.rdnDay.Size = new System.Drawing.Size(59, 17);
            this.rdnDay.TabIndex = 3;
            this.rdnDay.Text = "By Day";
            this.rdnDay.UseVisualStyleBackColor = true;
            this.rdnDay.CheckedChanged += new System.EventHandler(this.rdnDay_CheckedChanged);
            // 
            // cmbDatatype
            // 
            this.cmbDatatype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cmbDatatype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatatype.FormattingEnabled = true;
            this.cmbDatatype.Location = new System.Drawing.Point(4, 2);
            this.cmbDatatype.Name = "cmbDatatype";
            this.cmbDatatype.Size = new System.Drawing.Size(142, 21);
            this.cmbDatatype.TabIndex = 1;
            this.cmbDatatype.SelectedIndexChanged += new System.EventHandler(this.cmbDatatype_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(-67, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Data Type";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.cmbCriteria);
            this.panel6.Controls.Add(this.lblCriteria);
            this.panel6.Controls.Add(this.lblDataType);
            this.panel6.Controls.Add(this.chkActive);
            this.panel6.Controls.Add(this.rdnPayout);
            this.panel6.Controls.Add(this.rdnHold);
            this.panel6.Controls.Add(this.cmbRecordCount);
            this.panel6.Controls.Add(this.dtpTo);
            this.panel6.Controls.Add(this.dtpFrom);
            this.panel6.Controls.Add(this.cmbGroupBy);
            this.panel6.Controls.Add(this.lblGroupBy);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Location = new System.Drawing.Point(4, 276);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(258, 118);
            this.panel6.TabIndex = 4;
            // 
            // cmbCriteria
            // 
            this.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteria.FormattingEnabled = true;
            this.cmbCriteria.Location = new System.Drawing.Point(53, 61);
            this.cmbCriteria.Name = "cmbCriteria";
            this.cmbCriteria.Size = new System.Drawing.Size(106, 21);
            this.cmbCriteria.TabIndex = 27;
            // 
            // lblCriteria
            // 
            this.lblCriteria.AutoSize = true;
            this.lblCriteria.ForeColor = System.Drawing.Color.White;
            this.lblCriteria.Location = new System.Drawing.Point(1, 66);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(42, 13);
            this.lblCriteria.TabIndex = 26;
            this.lblCriteria.Text = "Criteria:";
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.ForeColor = System.Drawing.Color.White;
            this.lblDataType.Location = new System.Drawing.Point(1, 40);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(50, 13);
            this.lblDataType.TabIndex = 25;
            this.lblDataType.Text = "Records:";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.ForeColor = System.Drawing.Color.White;
            this.chkActive.Location = new System.Drawing.Point(170, 77);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(90, 17);
            this.chkActive.TabIndex = 7;
            this.chkActive.Text = "Active Assets";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // rdnPayout
            // 
            this.rdnPayout.AutoSize = true;
            this.rdnPayout.ForeColor = System.Drawing.Color.White;
            this.rdnPayout.Location = new System.Drawing.Point(170, 52);
            this.rdnPayout.Name = "rdnPayout";
            this.rdnPayout.Size = new System.Drawing.Size(69, 17);
            this.rdnPayout.TabIndex = 6;
            this.rdnPayout.Text = "Payout %";
            this.rdnPayout.UseVisualStyleBackColor = true;
            // 
            // rdnHold
            // 
            this.rdnHold.AutoSize = true;
            this.rdnHold.Checked = true;
            this.rdnHold.ForeColor = System.Drawing.Color.White;
            this.rdnHold.Location = new System.Drawing.Point(170, 32);
            this.rdnHold.Name = "rdnHold";
            this.rdnHold.Size = new System.Drawing.Size(58, 17);
            this.rdnHold.TabIndex = 5;
            this.rdnHold.TabStop = true;
            this.rdnHold.Text = "Hold %";
            this.rdnHold.UseVisualStyleBackColor = true;
            // 
            // cmbRecordCount
            // 
            this.cmbRecordCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordCount.FormattingEnabled = true;
            this.cmbRecordCount.Location = new System.Drawing.Point(53, 36);
            this.cmbRecordCount.Name = "cmbRecordCount";
            this.cmbRecordCount.Size = new System.Drawing.Size(106, 21);
            this.cmbRecordCount.TabIndex = 3;
            this.cmbRecordCount.SelectedIndexChanged += new System.EventHandler(this.cmbRecordCount_SelectedIndexChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(162, 6);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(84, 20);
            this.dtpTo.TabIndex = 2;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(53, 7);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(84, 20);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupBy.FormattingEnabled = true;
            this.cmbGroupBy.Location = new System.Drawing.Point(53, 88);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Size = new System.Drawing.Size(106, 21);
            this.cmbGroupBy.TabIndex = 4;
            // 
            // lblGroupBy
            // 
            this.lblGroupBy.AutoSize = true;
            this.lblGroupBy.ForeColor = System.Drawing.Color.White;
            this.lblGroupBy.Location = new System.Drawing.Point(1, 91);
            this.lblGroupBy.Name = "lblGroupBy";
            this.lblGroupBy.Size = new System.Drawing.Size(54, 13);
            this.lblGroupBy.TabIndex = 18;
            this.lblGroupBy.Text = "Group By:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(138, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "To:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(1, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "From:";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.cmbDepot);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.cmbOperator);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Location = new System.Drawing.Point(4, 196);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(258, 74);
            this.panel5.TabIndex = 3;
            // 
            // cmbDepot
            // 
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(77, 39);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(169, 21);
            this.cmbDepot.TabIndex = 13;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Depot:";
            // 
            // cmbOperator
            // 
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(77, 12);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(169, 21);
            this.cmbOperator.TabIndex = 11;
            this.cmbOperator.SelectedValueChanged += new System.EventHandler(this.cmbOperator_SelectedValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(1, 15);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(51, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Operator:";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.cmbManufacturer);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.cmbGame);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.cmbCategory);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.cmbType);
            this.panel7.Controls.Add(this.lblType);
            this.panel7.Location = new System.Drawing.Point(4, 69);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(258, 123);
            this.panel7.TabIndex = 2;
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(77, 92);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(168, 21);
            this.cmbManufacturer.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(1, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Manufacturer:";
            // 
            // cmbGame
            // 
            this.cmbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGame.FormattingEnabled = true;
            this.cmbGame.Location = new System.Drawing.Point(77, 65);
            this.cmbGame.Name = "cmbGame";
            this.cmbGame.Size = new System.Drawing.Size(168, 21);
            this.cmbGame.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Game:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(77, 38);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(168, 21);
            this.cmbCategory.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Category:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(77, 11);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(168, 21);
            this.cmbType.TabIndex = 9;
            this.cmbType.SelectedValueChanged += new System.EventHandler(this.cmbType_SelectedValueChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.ForeColor = System.Drawing.Color.White;
            this.lblType.Location = new System.Drawing.Point(1, 14);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Type:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnShowTree);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSite);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtDistrict);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtArea);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtRegion);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSubCompany);
            this.groupBox2.Controls.Add(this.lblCompany);
            this.groupBox2.Controls.Add(this.txtCompany);
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(751, 60);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnShowTree
            // 
            this.btnShowTree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnShowTree.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnShowTree.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnShowTree.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnShowTree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnShowTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold);
            this.btnShowTree.ForeColor = System.Drawing.Color.White;
            this.btnShowTree.Location = new System.Drawing.Point(5, 28);
            this.btnShowTree.Name = "btnShowTree";
            this.btnShowTree.Size = new System.Drawing.Size(71, 20);
            this.btnShowTree.TabIndex = 9;
            this.btnShowTree.Text = "<< TREE";
            this.btnShowTree.UseVisualStyleBackColor = false;
            this.btnShowTree.Click += new System.EventHandler(this.btnShowTree_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(591, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Site";
            // 
            // txtSite
            // 
            this.txtSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSite.BackColor = System.Drawing.Color.White;
            this.txtSite.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSite.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtSite.Location = new System.Drawing.Point(591, 28);
            this.txtSite.Name = "txtSite";
            this.txtSite.ReadOnly = true;
            this.txtSite.Size = new System.Drawing.Size(150, 20);
            this.txtSite.TabIndex = 10;
            this.txtSite.Text = "ALL";
            this.txtSite.MouseHover += new System.EventHandler(this.txtSite_MouseHover);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(644, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "District";
            this.label4.Visible = false;
            // 
            // txtDistrict
            // 
            this.txtDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtDistrict.BackColor = System.Drawing.Color.White;
            this.txtDistrict.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtDistrict.Location = new System.Drawing.Point(646, 47);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.ReadOnly = true;
            this.txtDistrict.Size = new System.Drawing.Size(115, 20);
            this.txtDistrict.TabIndex = 8;
            this.txtDistrict.Text = "ALL";
            this.txtDistrict.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(530, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Area";
            this.label3.Visible = false;
            // 
            // txtArea
            // 
            this.txtArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtArea.BackColor = System.Drawing.Color.White;
            this.txtArea.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtArea.Location = new System.Drawing.Point(533, 47);
            this.txtArea.Name = "txtArea";
            this.txtArea.ReadOnly = true;
            this.txtArea.Size = new System.Drawing.Size(115, 20);
            this.txtArea.TabIndex = 6;
            this.txtArea.Text = "ALL";
            this.txtArea.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(409, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Region";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRegion
            // 
            this.txtRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtRegion.BackColor = System.Drawing.Color.White;
            this.txtRegion.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegion.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtRegion.Location = new System.Drawing.Point(413, 28);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.ReadOnly = true;
            this.txtRegion.Size = new System.Drawing.Size(140, 20);
            this.txtRegion.TabIndex = 4;
            this.txtRegion.Text = "ALL";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(247, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sub Company";
            // 
            // txtSubCompany
            // 
            this.txtSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtSubCompany.BackColor = System.Drawing.Color.White;
            this.txtSubCompany.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubCompany.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtSubCompany.Location = new System.Drawing.Point(248, 28);
            this.txtSubCompany.Name = "txtSubCompany";
            this.txtSubCompany.ReadOnly = true;
            this.txtSubCompany.Size = new System.Drawing.Size(140, 20);
            this.txtSubCompany.TabIndex = 2;
            this.txtSubCompany.Text = "ALL";
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.White;
            this.lblCompany.Location = new System.Drawing.Point(80, 11);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(51, 13);
            this.lblCompany.TabIndex = 1;
            this.lblCompany.Text = "Company";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtCompany.BackColor = System.Drawing.Color.White;
            this.txtCompany.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.ForeColor = System.Drawing.Color.DarkBlue;
            this.txtCompany.Location = new System.Drawing.Point(82, 28);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(140, 20);
            this.txtCompany.TabIndex = 0;
            this.txtCompany.Text = "ALL";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkActiveSites);
            this.panel2.Controls.Add(this.tvSiteDetails);
            this.panel2.Location = new System.Drawing.Point(11, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 445);
            this.panel2.TabIndex = 1;
            // 
            // chkActiveSites
            // 
            this.chkActiveSites.AutoSize = true;
            this.chkActiveSites.Checked = true;
            this.chkActiveSites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActiveSites.ForeColor = System.Drawing.Color.White;
            this.chkActiveSites.Location = new System.Drawing.Point(149, 10);
            this.chkActiveSites.Name = "chkActiveSites";
            this.chkActiveSites.Size = new System.Drawing.Size(85, 17);
            this.chkActiveSites.TabIndex = 3;
            this.chkActiveSites.Text = "Active Sites ";
            this.chkActiveSites.UseVisualStyleBackColor = true;
            this.chkActiveSites.CheckedChanged += new System.EventHandler(this.chkActiveSites_CheckedChanged);
            // 
            // tvSiteDetails
            // 
            this.tvSiteDetails.BackColor = System.Drawing.Color.White;
            this.tvSiteDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvSiteDetails.ForeColor = System.Drawing.Color.DarkBlue;
            this.tvSiteDetails.LineColor = System.Drawing.Color.Blue;
            this.tvSiteDetails.Location = new System.Drawing.Point(3, 30);
            this.tvSiteDetails.Name = "tvSiteDetails";
            this.tvSiteDetails.Size = new System.Drawing.Size(225, 406);
            this.tvSiteDetails.TabIndex = 2;
            this.tvSiteDetails.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSiteDetails_NodeMouseClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(17, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Org Hierarchical View";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.statusBar2);
            this.panel3.Controls.Add(this.dgMeterDetails);
            this.panel3.Location = new System.Drawing.Point(10, 455);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1007, 313);
            this.panel3.TabIndex = 3;
            // 
            // statusBar2
            // 
            this.statusBar2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusBar2.Dock = System.Windows.Forms.DockStyle.None;
            this.statusBar2.Location = new System.Drawing.Point(4, 288);
            this.statusBar2.Name = "statusBar2";
            this.statusBar2.Size = new System.Drawing.Size(1003, 22);
            this.statusBar2.TabIndex = 3;
            // 
            // dgMeterDetails
            // 
            this.dgMeterDetails.AllowUserToAddRows = false;
            this.dgMeterDetails.AllowUserToDeleteRows = false;
            this.dgMeterDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMeterDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgMeterDetails.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgMeterDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMeterDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMeterDetails.ColumnHeadersHeight = 20;
            this.dgMeterDetails.Location = new System.Drawing.Point(3, 3);
            this.dgMeterDetails.Name = "dgMeterDetails";
            this.dgMeterDetails.RowTemplate.Height = 18;
            this.dgMeterDetails.RowTemplate.ReadOnly = true;
            this.dgMeterDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMeterDetails.Size = new System.Drawing.Size(1003, 285);
            this.dgMeterDetails.TabIndex = 2;
            this.dgMeterDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMeterDetails_CellClick);
            this.dgMeterDetails.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeterDetails_CellMouseDoubleClick);
            this.dgMeterDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeterDetails_CellMouseDown);
            this.dgMeterDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMeterDetails_RowEnter);
            this.dgMeterDetails.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeterDetails_RowHeaderMouseClick);
            this.dgMeterDetails.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeterDetails_RowHeaderMouseDoubleClick);
            // 
            // ucBMCMeterAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ucBMCMeterAnalysis";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.ucBMCMeterAnalysis_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraph)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgMeterDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;       
       
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tvSiteDetails;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cmbGroupBy;
        private System.Windows.Forms.Label lblGroupBy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cmbDepot;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbOperator;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox cmbManufacturer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbGame;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdnDay;
        private System.Windows.Forms.ComboBox cmbRecordCount;
        private System.Windows.Forms.ComboBox cmbDatatype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdnWeek;
        private System.Windows.Forms.RadioButton rdnPeriod;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.RadioButton rdnPayout;
        private System.Windows.Forms.RadioButton rdnHold;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnShowTree;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusBar statusBar2;
        private System.Windows.Forms.DataGridView dgMeterDetails;
  
        private System.Windows.Forms.CheckBox chkActiveSites;
        private System.Windows.Forms.DataVisualization.Charting.Chart axMSChartMeterGraph;
        private System.Windows.Forms.Label lblCriteria;
        private System.Windows.Forms.ComboBox cmbCriteria;

    }
}
