namespace BMC.EnterpriseClient
{
    partial class frmMeterAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeterAnalysis));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblMainFrame = new System.Windows.Forms.TableLayoutPanel();
            this.axMSChartMeterGraphFull = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tblButtonLowerPannel = new System.Windows.Forms.TableLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.dgExportMADetails = new System.Windows.Forms.DataGridView();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.tblFilterCriteria = new System.Windows.Forms.TableLayoutPanel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblGraphFilter = new System.Windows.Forms.TableLayoutPanel();
            this.btnZoom = new System.Windows.Forms.Button();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cmbDatatype = new BMC.Common.Utilities.BmcComboBox();
            this.rdnDay = new System.Windows.Forms.RadioButton();
            this.rdnWeek = new System.Windows.Forms.RadioButton();
            this.rdnPeriod = new System.Windows.Forms.RadioButton();
            this.tblOrganisationSelection = new System.Windows.Forms.TableLayoutPanel();
            this.lblCompany = new System.Windows.Forms.Label();
            this.txtSubCompany = new System.Windows.Forms.TextBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.txtSite = new System.Windows.Forms.TextBox();
            this.lblSubCompany = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.txtRegion = new System.Windows.Forms.TextBox();
            this.tblMainOptions = new System.Windows.Forms.TableLayoutPanel();
            this.btnHideTree = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.rdnGame = new System.Windows.Forms.RadioButton();
            this.rdnSlot = new System.Windows.Forms.RadioButton();
            this.tblMeterAnalysisGraph = new System.Windows.Forms.TableLayoutPanel();
            this.axMSChartMeterGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tblChartOptions = new System.Windows.Forms.TableLayoutPanel();
            this.lblAvgColor = new System.Windows.Forms.Label();
            this.rdnLineChart = new System.Windows.Forms.RadioButton();
            this.rdnColumnChart = new System.Windows.Forms.RadioButton();
            this.chkShowDataValue = new System.Windows.Forms.CheckBox();
            this.cmbAvgColor = new System.Windows.Forms.ComboBox();
            this.lblSelectedColor = new System.Windows.Forms.Label();
            this.cmbSelectColor = new System.Windows.Forms.ComboBox();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.tblComboBoxPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lblDepot = new System.Windows.Forms.Label();
            this.cmbType = new BMC.Common.Utilities.BmcComboBox();
            this.cmbDepot = new BMC.Common.Utilities.BmcComboBox();
            this.cmbOperator = new BMC.Common.Utilities.BmcComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.pnlHoldPercentage = new System.Windows.Forms.Panel();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.rdnPayout = new System.Windows.Forms.RadioButton();
            this.rdnHold = new System.Windows.Forms.RadioButton();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.cmbManufacturer = new BMC.Common.Utilities.BmcComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblRecords = new System.Windows.Forms.Label();
            this.cmbRecordCount = new BMC.Common.Utilities.BmcComboBox();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.cmbCriteria = new BMC.Common.Utilities.BmcComboBox();
            this.lblGroupBy = new System.Windows.Forms.Label();
            this.cmbGroupBy = new BMC.Common.Utilities.BmcComboBox();
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbGame = new BMC.Common.Utilities.BmcComboBox();
            this.cmbCategory = new BMC.Common.Utilities.BmcComboBox();
            this.lblDenom = new System.Windows.Forms.Label();
            this.cmbDenom = new BMC.Common.Utilities.BmcComboBox();
            this.lblPayout = new System.Windows.Forms.Label();
            this.cmbPayout = new BMC.Common.Utilities.BmcComboBox();
            this.dgMeterDetails = new System.Windows.Forms.DataGridView();
            this.tblOrganisationView = new System.Windows.Forms.TableLayoutPanel();
            this.chkActiveSites = new System.Windows.Forms.CheckBox();
            this.uxOrganisationDetails = new BMC.CoreLib.Win32.UxHeaderContent();
            this.tvSiteDetails = new System.Windows.Forms.TreeView();
            this.tblMainFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraphFull)).BeginInit();
            this.tblButtonLowerPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExportMADetails)).BeginInit();
            this.tblContent.SuspendLayout();
            this.tblFilterCriteria.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tblGraphFilter.SuspendLayout();
            this.tblOrganisationSelection.SuspendLayout();
            this.tblMainOptions.SuspendLayout();
            this.tblMeterAnalysisGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraph)).BeginInit();
            this.tblChartOptions.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tblComboBoxPanel.SuspendLayout();
            this.pnlHoldPercentage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMeterDetails)).BeginInit();
            this.tblOrganisationView.SuspendLayout();
            this.uxOrganisationDetails.ChildContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMainFrame
            // 
            this.tblMainFrame.ColumnCount = 1;
            this.tblMainFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainFrame.Controls.Add(this.axMSChartMeterGraphFull, 0, 0);
            this.tblMainFrame.Controls.Add(this.tblButtonLowerPannel, 0, 2);
            this.tblMainFrame.Controls.Add(this.tblContent, 0, 1);
            this.tblMainFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainFrame.Location = new System.Drawing.Point(0, 0);
            this.tblMainFrame.Margin = new System.Windows.Forms.Padding(0);
            this.tblMainFrame.Name = "tblMainFrame";
            this.tblMainFrame.RowCount = 3;
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.17061F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.82938F));
            this.tblMainFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblMainFrame.Size = new System.Drawing.Size(1276, 998);
            this.tblMainFrame.TabIndex = 0;
            // 
            // axMSChartMeterGraphFull
            // 
            this.axMSChartMeterGraphFull.BackColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraphFull.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.axMSChartMeterGraphFull.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraphFull.BorderlineColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraphFull.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraphFull.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.LineWidth = 2;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.ScaleView.Position = 0D;
            chartArea1.AxisX.ScaleView.Size = 10D;
            chartArea1.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea1.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea1.AxisX.ScrollBar.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.ScrollBar.Size = 20D;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX2.MinorGrid.Enabled = true;
            chartArea1.AxisX2.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX2.MinorTickMark.Enabled = true;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.LineWidth = 2;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "caMeterAnalysis";
            chartArea1.ShadowColor = System.Drawing.Color.Gray;
            chartArea1.ShadowOffset = 5;
            this.axMSChartMeterGraphFull.ChartAreas.Add(chartArea1);
            this.axMSChartMeterGraphFull.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "legdMeterAnalysis";
            legend1.Title = "Meter Analysis";
            legend1.TitleAlignment = System.Drawing.StringAlignment.Near;
            this.axMSChartMeterGraphFull.Legends.Add(legend1);
            this.axMSChartMeterGraphFull.Location = new System.Drawing.Point(3, 3);
            this.axMSChartMeterGraphFull.Name = "axMSChartMeterGraphFull";
            this.axMSChartMeterGraphFull.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BorderColor = System.Drawing.Color.Black;
            series1.BorderWidth = 2;
            series1.ChartArea = "caMeterAnalysis";
            series1.Color = System.Drawing.Color.SteelBlue;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.LabelForeColor = System.Drawing.Color.Blue;
            series1.Legend = "legdMeterAnalysis";
            series1.MarkerSize = 10;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series1.Name = "Selected";
            series1.ShadowOffset = 1;
            series1.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series1.SmartLabelStyle.IsOverlappedHidden = false;
            series1.SmartLabelStyle.MaxMovingDistance = 50D;
            series1.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center)));
            series2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            series2.BorderColor = System.Drawing.Color.DarkRed;
            series2.BorderWidth = 2;
            series2.ChartArea = "caMeterAnalysis";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.DarkRed;
            series2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsValueShownAsLabel = true;
            series2.LabelForeColor = System.Drawing.Color.DarkRed;
            series2.Legend = "legdMeterAnalysis";
            series2.MarkerSize = 10;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series2.Name = "Average";
            series2.ShadowOffset = 1;
            series2.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            series2.SmartLabelStyle.IsOverlappedHidden = false;
            series2.SmartLabelStyle.MaxMovingDistance = 50D;
            series2.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)(((((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Bottom | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Right) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Left) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight)));
            this.axMSChartMeterGraphFull.Series.Add(series1);
            this.axMSChartMeterGraphFull.Series.Add(series2);
            this.axMSChartMeterGraphFull.Size = new System.Drawing.Size(1270, 225);
            this.axMSChartMeterGraphFull.TabIndex = 1;
            this.axMSChartMeterGraphFull.TabStop = false;
            // 
            // tblButtonLowerPannel
            // 
            this.tblButtonLowerPannel.ColumnCount = 6;
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtonLowerPannel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtonLowerPannel.Controls.Add(this.btnExport, 2, 0);
            this.tblButtonLowerPannel.Controls.Add(this.btnClose, 5, 0);
            this.tblButtonLowerPannel.Controls.Add(this.btnProcess, 1, 0);
            this.tblButtonLowerPannel.Controls.Add(this.dgExportMADetails, 4, 0);
            this.tblButtonLowerPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtonLowerPannel.Location = new System.Drawing.Point(0, 957);
            this.tblButtonLowerPannel.Margin = new System.Windows.Forms.Padding(0);
            this.tblButtonLowerPannel.Name = "tblButtonLowerPannel";
            this.tblButtonLowerPannel.RowCount = 1;
            this.tblButtonLowerPannel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtonLowerPannel.Size = new System.Drawing.Size(1276, 41);
            this.tblButtonLowerPannel.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(423, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(114, 28);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1159, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(303, 6);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(114, 28);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "btnProcess";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // dgExportMADetails
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgExportMADetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgExportMADetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgExportMADetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgExportMADetails.Location = new System.Drawing.Point(663, 3);
            this.dgExportMADetails.Name = "dgExportMADetails";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgExportMADetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgExportMADetails.Size = new System.Drawing.Size(490, 31);
            this.dgExportMADetails.TabIndex = 3;
            this.dgExportMADetails.TabStop = false;
            this.dgExportMADetails.Visible = false;
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Controls.Add(this.tblFilterCriteria, 1, 0);
            this.tblContent.Controls.Add(this.tblOrganisationView, 0, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 231);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 622F));
            this.tblContent.Size = new System.Drawing.Size(1276, 726);
            this.tblContent.TabIndex = 0;
            // 
            // tblFilterCriteria
            // 
            this.tblFilterCriteria.ColumnCount = 1;
            this.tblFilterCriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilterCriteria.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblFilterCriteria.Controls.Add(this.tblContainer, 0, 0);
            this.tblFilterCriteria.Controls.Add(this.dgMeterDetails, 0, 1);
            this.tblFilterCriteria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilterCriteria.Location = new System.Drawing.Point(300, 0);
            this.tblFilterCriteria.Margin = new System.Windows.Forms.Padding(0);
            this.tblFilterCriteria.Name = "tblFilterCriteria";
            this.tblFilterCriteria.RowCount = 2;
            this.tblFilterCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.3142F));
            this.tblFilterCriteria.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.6858F));
            this.tblFilterCriteria.Size = new System.Drawing.Size(976, 726);
            this.tblFilterCriteria.TabIndex = 1;
            // 
            // tblContainer
            // 
            this.tblContainer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblGraphFilter, 1, 2);
            this.tblContainer.Controls.Add(this.tblOrganisationSelection, 1, 0);
            this.tblContainer.Controls.Add(this.tblMainOptions, 0, 0);
            this.tblContainer.Controls.Add(this.tblMeterAnalysisGraph, 1, 3);
            this.tblContainer.Controls.Add(this.pnlFilter, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 4;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(976, 481);
            this.tblContainer.TabIndex = 0;
            // 
            // tblGraphFilter
            // 
            this.tblGraphFilter.ColumnCount = 7;
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.40144F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.23043F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.38805F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.38805F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.38805F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.09847F));
            this.tblGraphFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.10551F));
            this.tblGraphFilter.Controls.Add(this.btnZoom, 6, 0);
            this.tblGraphFilter.Controls.Add(this.lblDataType, 0, 0);
            this.tblGraphFilter.Controls.Add(this.cmbDatatype, 1, 0);
            this.tblGraphFilter.Controls.Add(this.rdnDay, 2, 0);
            this.tblGraphFilter.Controls.Add(this.rdnWeek, 3, 0);
            this.tblGraphFilter.Controls.Add(this.rdnPeriod, 4, 0);
            this.tblGraphFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGraphFilter.Location = new System.Drawing.Point(282, 73);
            this.tblGraphFilter.Margin = new System.Windows.Forms.Padding(0);
            this.tblGraphFilter.Name = "tblGraphFilter";
            this.tblGraphFilter.RowCount = 1;
            this.tblGraphFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblGraphFilter.Size = new System.Drawing.Size(693, 35);
            this.tblGraphFilter.TabIndex = 1;
            // 
            // btnZoom
            // 
            this.btnZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnZoom.Location = new System.Drawing.Point(590, 3);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(100, 28);
            this.btnZoom.TabIndex = 5;
            this.btnZoom.Text = "btnZoom";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoomGraph_Click);
            // 
            // lblDataType
            // 
            this.lblDataType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(3, 11);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(64, 13);
            this.lblDataType.TabIndex = 0;
            this.lblDataType.Text = "lblDataType";
            this.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDatatype
            // 
            this.cmbDatatype.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDatatype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDatatype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatatype.FormattingEnabled = true;
            this.cmbDatatype.Location = new System.Drawing.Point(95, 7);
            this.cmbDatatype.Name = "cmbDatatype";
            this.cmbDatatype.Size = new System.Drawing.Size(113, 21);
            this.cmbDatatype.TabIndex = 1;
            this.cmbDatatype.SelectedIndexChanged += new System.EventHandler(this.cmbDatatype_SelectedIndexChanged);
            // 
            // rdnDay
            // 
            this.rdnDay.AutoSize = true;
            this.rdnDay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdnDay.Location = new System.Drawing.Point(214, 3);
            this.rdnDay.Name = "rdnDay";
            this.rdnDay.Size = new System.Drawing.Size(86, 29);
            this.rdnDay.TabIndex = 2;
            this.rdnDay.Text = "rdnDay";
            this.rdnDay.UseVisualStyleBackColor = true;
            this.rdnDay.CheckedChanged += new System.EventHandler(this.rdnGraphPeriod_CheckedChanged);
            // 
            // rdnWeek
            // 
            this.rdnWeek.AutoSize = true;
            this.rdnWeek.Checked = true;
            this.rdnWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdnWeek.Location = new System.Drawing.Point(306, 3);
            this.rdnWeek.Name = "rdnWeek";
            this.rdnWeek.Size = new System.Drawing.Size(86, 29);
            this.rdnWeek.TabIndex = 3;
            this.rdnWeek.TabStop = true;
            this.rdnWeek.Text = "rdnWeek";
            this.rdnWeek.UseVisualStyleBackColor = true;
            this.rdnWeek.CheckedChanged += new System.EventHandler(this.rdnGraphPeriod_CheckedChanged);
            // 
            // rdnPeriod
            // 
            this.rdnPeriod.AutoSize = true;
            this.rdnPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdnPeriod.Location = new System.Drawing.Point(398, 3);
            this.rdnPeriod.Name = "rdnPeriod";
            this.rdnPeriod.Size = new System.Drawing.Size(86, 29);
            this.rdnPeriod.TabIndex = 4;
            this.rdnPeriod.Text = "rdnPeriod";
            this.rdnPeriod.UseVisualStyleBackColor = true;
            this.rdnPeriod.CheckedChanged += new System.EventHandler(this.rdnGraphPeriod_CheckedChanged);
            // 
            // tblOrganisationSelection
            // 
            this.tblOrganisationSelection.ColumnCount = 6;
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.78947F));
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tblOrganisationSelection.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tblOrganisationSelection.Controls.Add(this.lblCompany, 1, 0);
            this.tblOrganisationSelection.Controls.Add(this.txtSubCompany, 2, 1);
            this.tblOrganisationSelection.Controls.Add(this.lblRegion, 3, 0);
            this.tblOrganisationSelection.Controls.Add(this.txtSite, 4, 1);
            this.tblOrganisationSelection.Controls.Add(this.lblSubCompany, 1, 1);
            this.tblOrganisationSelection.Controls.Add(this.txtCompany, 2, 0);
            this.tblOrganisationSelection.Controls.Add(this.lblSite, 3, 1);
            this.tblOrganisationSelection.Controls.Add(this.txtRegion, 4, 0);
            this.tblOrganisationSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOrganisationSelection.Location = new System.Drawing.Point(282, 1);
            this.tblOrganisationSelection.Margin = new System.Windows.Forms.Padding(0);
            this.tblOrganisationSelection.Name = "tblOrganisationSelection";
            this.tblOrganisationSelection.RowCount = 2;
            this.tblContainer.SetRowSpan(this.tblOrganisationSelection, 2);
            this.tblOrganisationSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOrganisationSelection.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOrganisationSelection.Size = new System.Drawing.Size(693, 71);
            this.tblOrganisationSelection.TabIndex = 3;
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(3, 11);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(139, 13);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "lblCompany";
            // 
            // txtSubCompany
            // 
            this.txtSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubCompany.Location = new System.Drawing.Point(148, 43);
            this.txtSubCompany.Name = "txtSubCompany";
            this.txtSubCompany.ReadOnly = true;
            this.txtSubCompany.Size = new System.Drawing.Size(212, 20);
            this.txtSubCompany.TabIndex = 3;
            this.txtSubCompany.TabStop = false;
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(366, 11);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(103, 13);
            this.lblRegion.TabIndex = 4;
            this.lblRegion.Text = "lblRegion";
            // 
            // txtSite
            // 
            this.txtSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSite.Location = new System.Drawing.Point(475, 43);
            this.txtSite.Name = "txtSite";
            this.txtSite.ReadOnly = true;
            this.txtSite.Size = new System.Drawing.Size(212, 20);
            this.txtSite.TabIndex = 7;
            this.txtSite.TabStop = false;
            // 
            // lblSubCompany
            // 
            this.lblSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCompany.AutoSize = true;
            this.lblSubCompany.Location = new System.Drawing.Point(3, 46);
            this.lblSubCompany.Name = "lblSubCompany";
            this.lblSubCompany.Size = new System.Drawing.Size(139, 13);
            this.lblSubCompany.TabIndex = 2;
            this.lblSubCompany.Text = "lblSubCompany";
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(148, 7);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(212, 20);
            this.txtCompany.TabIndex = 1;
            this.txtCompany.TabStop = false;
            // 
            // lblSite
            // 
            this.lblSite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(366, 46);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(103, 13);
            this.lblSite.TabIndex = 6;
            this.lblSite.Text = "lblSite";
            // 
            // txtRegion
            // 
            this.txtRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRegion.Location = new System.Drawing.Point(475, 7);
            this.txtRegion.Name = "txtRegion";
            this.txtRegion.ReadOnly = true;
            this.txtRegion.Size = new System.Drawing.Size(212, 20);
            this.txtRegion.TabIndex = 5;
            this.txtRegion.TabStop = false;
            // 
            // tblMainOptions
            // 
            this.tblMainOptions.ColumnCount = 3;
            this.tblMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tblMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainOptions.Controls.Add(this.btnHideTree, 0, 0);
            this.tblMainOptions.Controls.Add(this.rdnGame, 2, 0);
            this.tblMainOptions.Controls.Add(this.rdnSlot, 1, 0);
            this.tblMainOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainOptions.Location = new System.Drawing.Point(1, 1);
            this.tblMainOptions.Margin = new System.Windows.Forms.Padding(0);
            this.tblMainOptions.Name = "tblMainOptions";
            this.tblMainOptions.RowCount = 1;
            this.tblMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblMainOptions.Size = new System.Drawing.Size(280, 35);
            this.tblMainOptions.TabIndex = 0;
            // 
            // btnHideTree
            // 
            this.btnHideTree.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnHideTree.ImageKey = "MovePrev";
            this.btnHideTree.ImageList = this.imglstSmallIcons;
            this.btnHideTree.Location = new System.Drawing.Point(3, 6);
            this.btnHideTree.Name = "btnHideTree";
            this.btnHideTree.Size = new System.Drawing.Size(69, 23);
            this.btnHideTree.TabIndex = 0;
            this.btnHideTree.Text = "btnHideTree";
            this.btnHideTree.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHideTree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHideTree.UseVisualStyleBackColor = true;
            this.btnHideTree.Click += new System.EventHandler(this.btnHideTree_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Company");
            this.imglstSmallIcons.Images.SetKeyName(1, "SubCompany");
            this.imglstSmallIcons.Images.SetKeyName(2, "Region");
            this.imglstSmallIcons.Images.SetKeyName(3, "Area");
            this.imglstSmallIcons.Images.SetKeyName(4, "District");
            this.imglstSmallIcons.Images.SetKeyName(5, "Site");
            this.imglstSmallIcons.Images.SetKeyName(6, "MovePrev");
            this.imglstSmallIcons.Images.SetKeyName(7, "MoveNext");
            this.imglstSmallIcons.Images.SetKeyName(8, "MoveFirst.ico");
            this.imglstSmallIcons.Images.SetKeyName(9, "MovePrev.ico");
            this.imglstSmallIcons.Images.SetKeyName(10, "MoveNext.ico");
            this.imglstSmallIcons.Images.SetKeyName(11, "MoveLast.ico");
            // 
            // rdnGame
            // 
            this.rdnGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdnGame.AutoSize = true;
            this.rdnGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnGame.Location = new System.Drawing.Point(180, 6);
            this.rdnGame.Name = "rdnGame";
            this.rdnGame.Size = new System.Drawing.Size(97, 22);
            this.rdnGame.TabIndex = 2;
            this.rdnGame.Text = "rdnGame";
            this.rdnGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdnGame.UseVisualStyleBackColor = true;
            this.rdnGame.CheckedChanged += new System.EventHandler(this.rdnGame_CheckedChanged);
            // 
            // rdnSlot
            // 
            this.rdnSlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.rdnSlot.AutoSize = true;
            this.rdnSlot.Checked = true;
            this.rdnSlot.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdnSlot.Location = new System.Drawing.Point(78, 6);
            this.rdnSlot.Name = "rdnSlot";
            this.rdnSlot.Size = new System.Drawing.Size(96, 22);
            this.rdnSlot.TabIndex = 1;
            this.rdnSlot.TabStop = true;
            this.rdnSlot.Text = "rdnSlot";
            this.rdnSlot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdnSlot.UseVisualStyleBackColor = true;
            this.rdnSlot.CheckedChanged += new System.EventHandler(this.rdnGame_CheckedChanged);
            // 
            // tblMeterAnalysisGraph
            // 
            this.tblMeterAnalysisGraph.ColumnCount = 2;
            this.tblMeterAnalysisGraph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMeterAnalysisGraph.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMeterAnalysisGraph.Controls.Add(this.axMSChartMeterGraph, 0, 1);
            this.tblMeterAnalysisGraph.Controls.Add(this.tblChartOptions, 0, 0);
            this.tblMeterAnalysisGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMeterAnalysisGraph.Location = new System.Drawing.Point(282, 109);
            this.tblMeterAnalysisGraph.Margin = new System.Windows.Forms.Padding(0);
            this.tblMeterAnalysisGraph.Name = "tblMeterAnalysisGraph";
            this.tblMeterAnalysisGraph.RowCount = 2;
            this.tblMeterAnalysisGraph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblMeterAnalysisGraph.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMeterAnalysisGraph.Size = new System.Drawing.Size(693, 371);
            this.tblMeterAnalysisGraph.TabIndex = 2;
            // 
            // axMSChartMeterGraph
            // 
            this.axMSChartMeterGraph.BackColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.axMSChartMeterGraph.BackSecondaryColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraph.BorderlineColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraph.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            this.axMSChartMeterGraph.BorderSkin.BorderColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.LineWidth = 2;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisX.MinorGrid.Enabled = true;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisX.MinorTickMark.Enabled = true;
            chartArea2.AxisX.ScaleView.Position = 0D;
            chartArea2.AxisX.ScaleView.Size = 10D;
            chartArea2.AxisX.ScaleView.SmallScrollSize = 10D;
            chartArea2.AxisX.ScrollBar.BackColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.ScrollBar.ButtonColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;
            chartArea2.AxisX.ScrollBar.IsPositionedInside = false;
            chartArea2.AxisX.ScrollBar.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisX.ScrollBar.Size = 20D;
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.LineWidth = 2;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea2.AxisY2.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.IsSameFontSizeForAllAxes = true;
            chartArea2.Name = "caMeterAnalysis";
            chartArea2.ShadowColor = System.Drawing.Color.Gray;
            chartArea2.ShadowOffset = 5;
            this.axMSChartMeterGraph.ChartAreas.Add(chartArea2);
            this.tblMeterAnalysisGraph.SetColumnSpan(this.axMSChartMeterGraph, 2);
            this.axMSChartMeterGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "legdMeterAnalysis";
            legend2.Title = "Meter Analysis";
            legend2.TitleAlignment = System.Drawing.StringAlignment.Near;
            this.axMSChartMeterGraph.Legends.Add(legend2);
            this.axMSChartMeterGraph.Location = new System.Drawing.Point(3, 28);
            this.axMSChartMeterGraph.Name = "axMSChartMeterGraph";
            this.axMSChartMeterGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series3.BorderColor = System.Drawing.Color.Black;
            series3.BorderWidth = 2;
            series3.ChartArea = "caMeterAnalysis";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Gold;
            series3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.IsValueShownAsLabel = true;
            series3.LabelForeColor = System.Drawing.Color.Blue;
            series3.Legend = "legdMeterAnalysis";
            series3.MarkerSize = 10;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series3.Name = "Selected";
            series3.ShadowOffset = 1;
            series3.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series3.SmartLabelStyle.IsOverlappedHidden = false;
            series3.SmartLabelStyle.MaxMovingDistance = 50D;
            series3.SmartLabelStyle.MinMovingDistance = 30D;
            series3.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)(((((((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Bottom) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Right) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Left) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center)));
            series4.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            series4.BorderColor = System.Drawing.Color.DarkRed;
            series4.BorderWidth = 2;
            series4.ChartArea = "caMeterAnalysis";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.DarkRed;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series4.IsValueShownAsLabel = true;
            series4.LabelForeColor = System.Drawing.Color.DarkRed;
            series4.Legend = "legdMeterAnalysis";
            series4.MarkerSize = 10;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Cross;
            series4.Name = "Average";
            series4.ShadowOffset = 1;
            series4.SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.No;
            series4.SmartLabelStyle.IsOverlappedHidden = false;
            series4.SmartLabelStyle.MaxMovingDistance = 50D;
            series4.SmartLabelStyle.MinMovingDistance = 20D;
            series4.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)(((((((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Bottom) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Right) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Left) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Center)));
            this.axMSChartMeterGraph.Series.Add(series3);
            this.axMSChartMeterGraph.Series.Add(series4);
            this.axMSChartMeterGraph.Size = new System.Drawing.Size(687, 340);
            this.axMSChartMeterGraph.TabIndex = 1;
            // 
            // tblChartOptions
            // 
            this.tblChartOptions.ColumnCount = 7;
            this.tblMeterAnalysisGraph.SetColumnSpan(this.tblChartOptions, 2);
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.14314F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.73143F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5397F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.820401F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.70978F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.8204F));
            this.tblChartOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.23515F));
            this.tblChartOptions.Controls.Add(this.lblAvgColor, 4, 0);
            this.tblChartOptions.Controls.Add(this.rdnLineChart, 0, 0);
            this.tblChartOptions.Controls.Add(this.rdnColumnChart, 1, 0);
            this.tblChartOptions.Controls.Add(this.chkShowDataValue, 6, 0);
            this.tblChartOptions.Controls.Add(this.cmbAvgColor, 5, 0);
            this.tblChartOptions.Controls.Add(this.lblSelectedColor, 2, 0);
            this.tblChartOptions.Controls.Add(this.cmbSelectColor, 3, 0);
            this.tblChartOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblChartOptions.Location = new System.Drawing.Point(1, 1);
            this.tblChartOptions.Margin = new System.Windows.Forms.Padding(1);
            this.tblChartOptions.Name = "tblChartOptions";
            this.tblChartOptions.RowCount = 1;
            this.tblChartOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblChartOptions.Size = new System.Drawing.Size(691, 23);
            this.tblChartOptions.TabIndex = 0;
            // 
            // lblAvgColor
            // 
            this.lblAvgColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblAvgColor.AutoSize = true;
            this.lblAvgColor.Location = new System.Drawing.Point(411, 5);
            this.lblAvgColor.Name = "lblAvgColor";
            this.lblAvgColor.Size = new System.Drawing.Size(60, 13);
            this.lblAvgColor.TabIndex = 4;
            this.lblAvgColor.Text = "lblAvgColor";
            // 
            // rdnLineChart
            // 
            this.rdnLineChart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdnLineChart.AutoSize = true;
            this.rdnLineChart.Checked = true;
            this.rdnLineChart.Location = new System.Drawing.Point(2, 3);
            this.rdnLineChart.Margin = new System.Windows.Forms.Padding(2);
            this.rdnLineChart.Name = "rdnLineChart";
            this.rdnLineChart.Size = new System.Drawing.Size(85, 17);
            this.rdnLineChart.TabIndex = 0;
            this.rdnLineChart.TabStop = true;
            this.rdnLineChart.Text = "rdnLineChart";
            this.rdnLineChart.UseVisualStyleBackColor = true;
            this.rdnLineChart.CheckedChanged += new System.EventHandler(this.rdnChartType_CheckedChanged);
            // 
            // rdnColumnChart
            // 
            this.rdnColumnChart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rdnColumnChart.AutoSize = true;
            this.rdnColumnChart.Location = new System.Drawing.Point(99, 3);
            this.rdnColumnChart.Margin = new System.Windows.Forms.Padding(2);
            this.rdnColumnChart.Name = "rdnColumnChart";
            this.rdnColumnChart.Size = new System.Drawing.Size(80, 17);
            this.rdnColumnChart.TabIndex = 1;
            this.rdnColumnChart.TabStop = true;
            this.rdnColumnChart.Text = "rdnbarChart";
            this.rdnColumnChart.UseVisualStyleBackColor = true;
            this.rdnColumnChart.CheckedChanged += new System.EventHandler(this.rdnChartType_CheckedChanged);
            // 
            // chkShowDataValue
            // 
            this.chkShowDataValue.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkShowDataValue.AutoSize = true;
            this.chkShowDataValue.Checked = true;
            this.chkShowDataValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDataValue.Location = new System.Drawing.Point(567, 3);
            this.chkShowDataValue.Name = "chkShowDataValue";
            this.chkShowDataValue.Size = new System.Drawing.Size(121, 17);
            this.chkShowDataValue.TabIndex = 6;
            this.chkShowDataValue.Text = "chkShowDataValue";
            this.chkShowDataValue.UseVisualStyleBackColor = true;
            this.chkShowDataValue.CheckedChanged += new System.EventHandler(this.chkShowDataValue_CheckedChanged);
            // 
            // cmbAvgColor
            // 
            this.cmbAvgColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAvgColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAvgColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAvgColor.FormattingEnabled = true;
            this.cmbAvgColor.Location = new System.Drawing.Point(474, 1);
            this.cmbAvgColor.Margin = new System.Windows.Forms.Padding(0);
            this.cmbAvgColor.Name = "cmbAvgColor";
            this.cmbAvgColor.Size = new System.Drawing.Size(54, 21);
            this.cmbAvgColor.TabIndex = 5;
            this.cmbAvgColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbColor_DrawItem);
            this.cmbAvgColor.SelectedIndexChanged += new System.EventHandler(this.cmbAvgColor_SelectedIndexChanged);
            // 
            // lblSelectedColor
            // 
            this.lblSelectedColor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSelectedColor.AutoSize = true;
            this.lblSelectedColor.Location = new System.Drawing.Point(247, 5);
            this.lblSelectedColor.Name = "lblSelectedColor";
            this.lblSelectedColor.Size = new System.Drawing.Size(83, 13);
            this.lblSelectedColor.TabIndex = 2;
            this.lblSelectedColor.Text = "lblSelectedColor";
            // 
            // cmbSelectColor
            // 
            this.cmbSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSelectColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectColor.FormattingEnabled = true;
            this.cmbSelectColor.Location = new System.Drawing.Point(333, 1);
            this.cmbSelectColor.Margin = new System.Windows.Forms.Padding(0);
            this.cmbSelectColor.Name = "cmbSelectColor";
            this.cmbSelectColor.Size = new System.Drawing.Size(54, 21);
            this.cmbSelectColor.TabIndex = 3;
            this.cmbSelectColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbColor_DrawItem);
            this.cmbSelectColor.SelectedIndexChanged += new System.EventHandler(this.cmbSelectColor_SelectedIndexChanged);
            // 
            // pnlFilter
            // 
            this.pnlFilter.AutoScroll = true;
            this.pnlFilter.AutoScrollMinSize = new System.Drawing.Size(260, 420);
            this.pnlFilter.Controls.Add(this.tblComboBoxPanel);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(1, 37);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFilter.Name = "pnlFilter";
            this.tblContainer.SetRowSpan(this.pnlFilter, 3);
            this.pnlFilter.Size = new System.Drawing.Size(280, 443);
            this.pnlFilter.TabIndex = 1;
            // 
            // tblComboBoxPanel
            // 
            this.tblComboBoxPanel.ColumnCount = 2;
            this.tblComboBoxPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblComboBoxPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblComboBoxPanel.Controls.Add(this.lblDepot, 0, 1);
            this.tblComboBoxPanel.Controls.Add(this.cmbType, 1, 2);
            this.tblComboBoxPanel.Controls.Add(this.cmbDepot, 1, 1);
            this.tblComboBoxPanel.Controls.Add(this.cmbOperator, 1, 0);
            this.tblComboBoxPanel.Controls.Add(this.lblType, 0, 2);
            this.tblComboBoxPanel.Controls.Add(this.lblOperator, 0, 0);
            this.tblComboBoxPanel.Controls.Add(this.pnlHoldPercentage, 0, 9);
            this.tblComboBoxPanel.Controls.Add(this.lblManufacturer, 0, 3);
            this.tblComboBoxPanel.Controls.Add(this.cmbManufacturer, 1, 3);
            this.tblComboBoxPanel.Controls.Add(this.lblFrom, 0, 4);
            this.tblComboBoxPanel.Controls.Add(this.dtpFrom, 1, 4);
            this.tblComboBoxPanel.Controls.Add(this.lblTo, 0, 5);
            this.tblComboBoxPanel.Controls.Add(this.dtpTo, 1, 5);
            this.tblComboBoxPanel.Controls.Add(this.lblRecords, 0, 6);
            this.tblComboBoxPanel.Controls.Add(this.cmbRecordCount, 1, 6);
            this.tblComboBoxPanel.Controls.Add(this.lblCriteria, 0, 7);
            this.tblComboBoxPanel.Controls.Add(this.cmbCriteria, 1, 7);
            this.tblComboBoxPanel.Controls.Add(this.lblGroupBy, 0, 8);
            this.tblComboBoxPanel.Controls.Add(this.cmbGroupBy, 1, 8);
            this.tblComboBoxPanel.Controls.Add(this.lblGameTitle, 0, 11);
            this.tblComboBoxPanel.Controls.Add(this.lblCategory, 0, 10);
            this.tblComboBoxPanel.Controls.Add(this.cmbGame, 1, 11);
            this.tblComboBoxPanel.Controls.Add(this.cmbCategory, 1, 10);
            this.tblComboBoxPanel.Controls.Add(this.lblDenom, 0, 12);
            this.tblComboBoxPanel.Controls.Add(this.cmbDenom, 1, 12);
            this.tblComboBoxPanel.Controls.Add(this.lblPayout, 0, 13);
            this.tblComboBoxPanel.Controls.Add(this.cmbPayout, 1, 13);
            this.tblComboBoxPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblComboBoxPanel.Location = new System.Drawing.Point(0, 0);
            this.tblComboBoxPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tblComboBoxPanel.Name = "tblComboBoxPanel";
            this.tblComboBoxPanel.RowCount = 16;
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblComboBoxPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblComboBoxPanel.Size = new System.Drawing.Size(280, 443);
            this.tblComboBoxPanel.TabIndex = 0;
            // 
            // lblDepot
            // 
            this.lblDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepot.AutoSize = true;
            this.lblDepot.ForeColor = System.Drawing.Color.Black;
            this.lblDepot.Location = new System.Drawing.Point(3, 38);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(134, 13);
            this.lblDepot.TabIndex = 2;
            this.lblDepot.Text = "lblDepot";
            // 
            // cmbType
            // 
            this.cmbType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(141, 64);
            this.cmbType.Margin = new System.Windows.Forms.Padding(1);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 5;
            // 
            // cmbDepot
            // 
            this.cmbDepot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDepot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(141, 34);
            this.cmbDepot.Margin = new System.Windows.Forms.Padding(1);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(121, 21);
            this.cmbDepot.TabIndex = 3;
            // 
            // cmbOperator
            // 
            this.cmbOperator.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbOperator.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperator.FormattingEnabled = true;
            this.cmbOperator.Location = new System.Drawing.Point(141, 4);
            this.cmbOperator.Margin = new System.Windows.Forms.Padding(1);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(121, 21);
            this.cmbOperator.TabIndex = 1;
            this.cmbOperator.SelectedIndexChanged += new System.EventHandler(this.cmbOperator_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblType.AutoSize = true;
            this.lblType.ForeColor = System.Drawing.Color.Black;
            this.lblType.Location = new System.Drawing.Point(3, 68);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(134, 13);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "lblType";
            // 
            // lblOperator
            // 
            this.lblOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperator.AutoSize = true;
            this.lblOperator.ForeColor = System.Drawing.Color.Black;
            this.lblOperator.Location = new System.Drawing.Point(3, 8);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(134, 13);
            this.lblOperator.TabIndex = 0;
            this.lblOperator.Text = "lblOperator";
            // 
            // pnlHoldPercentage
            // 
            this.tblComboBoxPanel.SetColumnSpan(this.pnlHoldPercentage, 2);
            this.pnlHoldPercentage.Controls.Add(this.chkActive);
            this.pnlHoldPercentage.Controls.Add(this.rdnPayout);
            this.pnlHoldPercentage.Controls.Add(this.rdnHold);
            this.pnlHoldPercentage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHoldPercentage.Location = new System.Drawing.Point(3, 273);
            this.pnlHoldPercentage.Name = "pnlHoldPercentage";
            this.pnlHoldPercentage.Size = new System.Drawing.Size(274, 24);
            this.pnlHoldPercentage.TabIndex = 18;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.ForeColor = System.Drawing.Color.Black;
            this.chkActive.Location = new System.Drawing.Point(155, 4);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(74, 17);
            this.chkActive.TabIndex = 2;
            this.chkActive.Text = "chkActive";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // rdnPayout
            // 
            this.rdnPayout.AutoSize = true;
            this.rdnPayout.ForeColor = System.Drawing.Color.Black;
            this.rdnPayout.Location = new System.Drawing.Point(67, 3);
            this.rdnPayout.Name = "rdnPayout";
            this.rdnPayout.Size = new System.Drawing.Size(73, 17);
            this.rdnPayout.TabIndex = 1;
            this.rdnPayout.Text = "rdnPayout";
            this.rdnPayout.UseVisualStyleBackColor = true;
            // 
            // rdnHold
            // 
            this.rdnHold.AutoSize = true;
            this.rdnHold.Checked = true;
            this.rdnHold.ForeColor = System.Drawing.Color.Black;
            this.rdnHold.Location = new System.Drawing.Point(3, 4);
            this.rdnHold.Name = "rdnHold";
            this.rdnHold.Size = new System.Drawing.Size(62, 17);
            this.rdnHold.TabIndex = 0;
            this.rdnHold.TabStop = true;
            this.rdnHold.Text = "rdnHold";
            this.rdnHold.UseVisualStyleBackColor = true;
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.ForeColor = System.Drawing.Color.Black;
            this.lblManufacturer.Location = new System.Drawing.Point(3, 98);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(134, 13);
            this.lblManufacturer.TabIndex = 6;
            this.lblManufacturer.Text = "lblManufacturer";
            // 
            // cmbManufacturer
            // 
            this.cmbManufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbManufacturer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturer.FormattingEnabled = true;
            this.cmbManufacturer.Location = new System.Drawing.Point(141, 94);
            this.cmbManufacturer.Margin = new System.Windows.Forms.Padding(1);
            this.cmbManufacturer.Name = "cmbManufacturer";
            this.cmbManufacturer.Size = new System.Drawing.Size(121, 21);
            this.cmbManufacturer.TabIndex = 7;
            // 
            // lblFrom
            // 
            this.lblFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrom.AutoSize = true;
            this.lblFrom.ForeColor = System.Drawing.Color.Black;
            this.lblFrom.Location = new System.Drawing.Point(3, 128);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(134, 13);
            this.lblFrom.TabIndex = 8;
            this.lblFrom.Text = "lblFrom";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(141, 125);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(1);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 20);
            this.dtpFrom.TabIndex = 9;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTo.AutoSize = true;
            this.lblTo.ForeColor = System.Drawing.Color.Black;
            this.lblTo.Location = new System.Drawing.Point(3, 158);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(134, 13);
            this.lblTo.TabIndex = 10;
            this.lblTo.Text = "lblTo";
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(141, 155);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(1);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 20);
            this.dtpTo.TabIndex = 11;
            // 
            // lblRecords
            // 
            this.lblRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecords.AutoSize = true;
            this.lblRecords.ForeColor = System.Drawing.Color.Black;
            this.lblRecords.Location = new System.Drawing.Point(3, 188);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(134, 13);
            this.lblRecords.TabIndex = 12;
            this.lblRecords.Text = "lblRecords";
            // 
            // cmbRecordCount
            // 
            this.cmbRecordCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbRecordCount.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbRecordCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordCount.FormattingEnabled = true;
            this.cmbRecordCount.Location = new System.Drawing.Point(141, 184);
            this.cmbRecordCount.Margin = new System.Windows.Forms.Padding(1);
            this.cmbRecordCount.Name = "cmbRecordCount";
            this.cmbRecordCount.Size = new System.Drawing.Size(121, 21);
            this.cmbRecordCount.TabIndex = 13;
            this.cmbRecordCount.SelectedIndexChanged += new System.EventHandler(this.cmbRecordCount_SelectedIndexChanged);
            // 
            // lblCriteria
            // 
            this.lblCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCriteria.AutoSize = true;
            this.lblCriteria.ForeColor = System.Drawing.Color.Black;
            this.lblCriteria.Location = new System.Drawing.Point(3, 218);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(134, 13);
            this.lblCriteria.TabIndex = 14;
            this.lblCriteria.Text = "lblCriteria";
            // 
            // cmbCriteria
            // 
            this.cmbCriteria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbCriteria.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteria.FormattingEnabled = true;
            this.cmbCriteria.Location = new System.Drawing.Point(141, 214);
            this.cmbCriteria.Margin = new System.Windows.Forms.Padding(1);
            this.cmbCriteria.Name = "cmbCriteria";
            this.cmbCriteria.Size = new System.Drawing.Size(121, 21);
            this.cmbCriteria.TabIndex = 15;
            // 
            // lblGroupBy
            // 
            this.lblGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGroupBy.AutoSize = true;
            this.lblGroupBy.ForeColor = System.Drawing.Color.Black;
            this.lblGroupBy.Location = new System.Drawing.Point(3, 248);
            this.lblGroupBy.Name = "lblGroupBy";
            this.lblGroupBy.Size = new System.Drawing.Size(134, 13);
            this.lblGroupBy.TabIndex = 16;
            this.lblGroupBy.Text = "lblGroupBy";
            // 
            // cmbGroupBy
            // 
            this.cmbGroupBy.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbGroupBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroupBy.FormattingEnabled = true;
            this.cmbGroupBy.Location = new System.Drawing.Point(141, 244);
            this.cmbGroupBy.Margin = new System.Windows.Forms.Padding(1);
            this.cmbGroupBy.Name = "cmbGroupBy";
            this.cmbGroupBy.Size = new System.Drawing.Size(121, 21);
            this.cmbGroupBy.Sorted = true;
            this.cmbGroupBy.TabIndex = 17;
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGameTitle.AutoSize = true;
            this.lblGameTitle.ForeColor = System.Drawing.Color.Black;
            this.lblGameTitle.Location = new System.Drawing.Point(3, 338);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(134, 13);
            this.lblGameTitle.TabIndex = 21;
            this.lblGameTitle.Text = "lblGameTitle";
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCategory.AutoSize = true;
            this.lblCategory.ForeColor = System.Drawing.Color.Black;
            this.lblCategory.Location = new System.Drawing.Point(3, 308);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(134, 13);
            this.lblCategory.TabIndex = 19;
            this.lblCategory.Text = "lblGameCategory";
            // 
            // cmbGame
            // 
            this.cmbGame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbGame.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGame.FormattingEnabled = true;
            this.cmbGame.Location = new System.Drawing.Point(141, 334);
            this.cmbGame.Margin = new System.Windows.Forms.Padding(1);
            this.cmbGame.Name = "cmbGame";
            this.cmbGame.Size = new System.Drawing.Size(121, 21);
            this.cmbGame.TabIndex = 22;
            this.cmbGame.SelectedIndexChanged += new System.EventHandler(this.cmbGame_SelectedIndexChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(141, 304);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(1);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 20;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // lblDenom
            // 
            this.lblDenom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDenom.AutoSize = true;
            this.lblDenom.ForeColor = System.Drawing.Color.Black;
            this.lblDenom.Location = new System.Drawing.Point(3, 368);
            this.lblDenom.Name = "lblDenom";
            this.lblDenom.Size = new System.Drawing.Size(134, 13);
            this.lblDenom.TabIndex = 23;
            this.lblDenom.Text = "lblDenom";
            // 
            // cmbDenom
            // 
            this.cmbDenom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbDenom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDenom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDenom.FormattingEnabled = true;
            this.cmbDenom.Location = new System.Drawing.Point(141, 364);
            this.cmbDenom.Margin = new System.Windows.Forms.Padding(1);
            this.cmbDenom.Name = "cmbDenom";
            this.cmbDenom.Size = new System.Drawing.Size(121, 21);
            this.cmbDenom.TabIndex = 24;
            this.cmbDenom.SelectedIndexChanged += new System.EventHandler(this.cmbDenom_SelectedIndexChanged);
            // 
            // lblPayout
            // 
            this.lblPayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPayout.AutoSize = true;
            this.lblPayout.ForeColor = System.Drawing.Color.Black;
            this.lblPayout.Location = new System.Drawing.Point(3, 398);
            this.lblPayout.Name = "lblPayout";
            this.lblPayout.Size = new System.Drawing.Size(134, 13);
            this.lblPayout.TabIndex = 25;
            this.lblPayout.Text = "lblPayout";
            // 
            // cmbPayout
            // 
            this.cmbPayout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbPayout.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayout.FormattingEnabled = true;
            this.cmbPayout.Location = new System.Drawing.Point(141, 394);
            this.cmbPayout.Margin = new System.Windows.Forms.Padding(1);
            this.cmbPayout.Name = "cmbPayout";
            this.cmbPayout.Size = new System.Drawing.Size(121, 21);
            this.cmbPayout.TabIndex = 26;
            // 
            // dgMeterDetails
            // 
            this.dgMeterDetails.AllowUserToAddRows = false;
            this.dgMeterDetails.AllowUserToDeleteRows = false;
            this.dgMeterDetails.AllowUserToResizeRows = false;
            this.dgMeterDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMeterDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgMeterDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgMeterDetails.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMeterDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMeterDetails.ColumnHeadersHeight = 20;
            this.dgMeterDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMeterDetails.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgMeterDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgMeterDetails.Location = new System.Drawing.Point(3, 484);
            this.dgMeterDetails.Name = "dgMeterDetails";
            this.dgMeterDetails.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMeterDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgMeterDetails.RowHeadersVisible = false;
            this.dgMeterDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgMeterDetails.Size = new System.Drawing.Size(970, 239);
            this.dgMeterDetails.TabIndex = 1;
            this.dgMeterDetails.TabStop = false;
            this.dgMeterDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMeterDetails_CellClick);
            this.dgMeterDetails.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgMeterDetails_CellMouseDown);
            this.dgMeterDetails.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMeterDetails_RowEnter);
            // 
            // tblOrganisationView
            // 
            this.tblOrganisationView.ColumnCount = 1;
            this.tblOrganisationView.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOrganisationView.Controls.Add(this.chkActiveSites, 0, 0);
            this.tblOrganisationView.Controls.Add(this.uxOrganisationDetails, 0, 1);
            this.tblOrganisationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOrganisationView.Location = new System.Drawing.Point(0, 0);
            this.tblOrganisationView.Margin = new System.Windows.Forms.Padding(0);
            this.tblOrganisationView.Name = "tblOrganisationView";
            this.tblOrganisationView.RowCount = 2;
            this.tblOrganisationView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblOrganisationView.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblOrganisationView.Size = new System.Drawing.Size(300, 726);
            this.tblOrganisationView.TabIndex = 0;
            // 
            // chkActiveSites
            // 
            this.chkActiveSites.AutoSize = true;
            this.chkActiveSites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkActiveSites.Location = new System.Drawing.Point(3, 3);
            this.chkActiveSites.Name = "chkActiveSites";
            this.chkActiveSites.Size = new System.Drawing.Size(294, 24);
            this.chkActiveSites.TabIndex = 0;
            this.chkActiveSites.Text = "chkActiveSites";
            this.chkActiveSites.UseVisualStyleBackColor = true;
            this.chkActiveSites.CheckedChanged += new System.EventHandler(this.chkActiveSites_CheckedChanged);
            // 
            // uxOrganisationDetails
            // 
            // 
            // uxOrganisationDetails.Child
            // 
            this.uxOrganisationDetails.ChildContainer.Controls.Add(this.tvSiteDetails);
            this.uxOrganisationDetails.ChildContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxOrganisationDetails.ChildContainer.Location = new System.Drawing.Point(0, 26);
            this.uxOrganisationDetails.ChildContainer.Name = "Child";
            this.uxOrganisationDetails.ChildContainer.Padding = new System.Windows.Forms.Padding(3);
            this.uxOrganisationDetails.ChildContainer.Size = new System.Drawing.Size(294, 664);
            this.uxOrganisationDetails.ChildContainer.TabIndex = 2;
            this.uxOrganisationDetails.ContentPadding = new System.Windows.Forms.Padding(3);
            this.uxOrganisationDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxOrganisationDetails.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(149)))), ((int)(((byte)(192)))));
            this.uxOrganisationDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxOrganisationDetails.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.uxOrganisationDetails.HeaderText = "uxOrgHierarchicalView";
            this.uxOrganisationDetails.IsClosable = false;
            this.uxOrganisationDetails.Location = new System.Drawing.Point(3, 33);
            this.uxOrganisationDetails.Name = "uxOrganisationDetails";
            this.uxOrganisationDetails.PinVisible = false;
            this.uxOrganisationDetails.Size = new System.Drawing.Size(294, 690);
            this.uxOrganisationDetails.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(64)))), ((int)(((byte)(114)))));
            this.uxOrganisationDetails.TabIndex = 0;
            this.uxOrganisationDetails.TabStop = false;
            // 
            // tvSiteDetails
            // 
            this.tvSiteDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSiteDetails.HideSelection = false;
            this.tvSiteDetails.ImageIndex = 10;
            this.tvSiteDetails.ImageList = this.imglstSmallIcons;
            this.tvSiteDetails.Location = new System.Drawing.Point(3, 3);
            this.tvSiteDetails.Name = "tvSiteDetails";
            this.tvSiteDetails.SelectedImageIndex = 0;
            this.tvSiteDetails.Size = new System.Drawing.Size(288, 658);
            this.tvSiteDetails.TabIndex = 0;
            this.tvSiteDetails.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSiteDetails_NodeMouseClick);
            // 
            // frmMeterAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 998);
            this.Controls.Add(this.tblMainFrame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeterAnalysis";
            this.Text = "Meter Analysis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMeterAnalysis_FormClosing);
            this.Load += new System.EventHandler(this.frmMeterAnalysis_Load);
            this.tblMainFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraphFull)).EndInit();
            this.tblButtonLowerPannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExportMADetails)).EndInit();
            this.tblContent.ResumeLayout(false);
            this.tblFilterCriteria.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tblGraphFilter.ResumeLayout(false);
            this.tblGraphFilter.PerformLayout();
            this.tblOrganisationSelection.ResumeLayout(false);
            this.tblOrganisationSelection.PerformLayout();
            this.tblMainOptions.ResumeLayout(false);
            this.tblMainOptions.PerformLayout();
            this.tblMeterAnalysisGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMSChartMeterGraph)).EndInit();
            this.tblChartOptions.ResumeLayout(false);
            this.tblChartOptions.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.tblComboBoxPanel.ResumeLayout(false);
            this.tblComboBoxPanel.PerformLayout();
            this.pnlHoldPercentage.ResumeLayout(false);
            this.pnlHoldPercentage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMeterDetails)).EndInit();
            this.tblOrganisationView.ResumeLayout(false);
            this.tblOrganisationView.PerformLayout();
            this.uxOrganisationDetails.ChildContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMainFrame;
        private System.Windows.Forms.TableLayoutPanel tblButtonLowerPannel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.TableLayoutPanel tblFilterCriteria;
        private System.Windows.Forms.TableLayoutPanel tblOrganisationView;
        private System.Windows.Forms.CheckBox chkActiveSites;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblGraphFilter;
        private System.Windows.Forms.Label lblDataType;
        private Common.Utilities.BmcComboBox cmbDatatype;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.RadioButton rdnDay;
        private System.Windows.Forms.RadioButton rdnWeek;
        private System.Windows.Forms.RadioButton rdnPeriod;
        private System.Windows.Forms.DataVisualization.Charting.Chart axMSChartMeterGraph;
        private System.Windows.Forms.DataGridView dgMeterDetails;
        private System.Windows.Forms.RadioButton rdnSlot;
        private System.Windows.Forms.RadioButton rdnGame;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnHideTree;
        private System.Windows.Forms.TableLayoutPanel tblOrganisationSelection;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblSubCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.TextBox txtSite;
        private System.Windows.Forms.TextBox txtRegion;
        private System.Windows.Forms.TextBox txtSubCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private CoreLib.Win32.UxHeaderContent uxOrganisationDetails;
        private System.Windows.Forms.TreeView tvSiteDetails;
        private System.Windows.Forms.DataGridView dgExportMADetails;
        private System.Windows.Forms.DataVisualization.Charting.Chart axMSChartMeterGraphFull;
        private System.Windows.Forms.TableLayoutPanel tblMainOptions;
        private System.Windows.Forms.TableLayoutPanel tblMeterAnalysisGraph;
        private System.Windows.Forms.CheckBox chkShowDataValue;
        private System.Windows.Forms.RadioButton rdnLineChart;
        private System.Windows.Forms.RadioButton rdnColumnChart;
        private System.Windows.Forms.TableLayoutPanel tblChartOptions;
        private System.Windows.Forms.Label lblAvgColor;
        private System.Windows.Forms.Label lblSelectedColor;
        private System.Windows.Forms.ComboBox cmbAvgColor;
        private System.Windows.Forms.ComboBox cmbSelectColor;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.TableLayoutPanel tblComboBoxPanel;
        private System.Windows.Forms.Label lblDepot;
        private Common.Utilities.BmcComboBox cmbType;
        private Common.Utilities.BmcComboBox cmbDepot;
        private Common.Utilities.BmcComboBox cmbOperator;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Panel pnlHoldPercentage;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.RadioButton rdnPayout;
        private System.Windows.Forms.RadioButton rdnHold;
        private System.Windows.Forms.Label lblManufacturer;
        private Common.Utilities.BmcComboBox cmbManufacturer;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblRecords;
        private Common.Utilities.BmcComboBox cmbRecordCount;
        private System.Windows.Forms.Label lblCriteria;
        private Common.Utilities.BmcComboBox cmbCriteria;
        private System.Windows.Forms.Label lblGroupBy;
        private Common.Utilities.BmcComboBox cmbGroupBy;
        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.Label lblCategory;
        private Common.Utilities.BmcComboBox cmbGame;
        private Common.Utilities.BmcComboBox cmbCategory;
        private System.Windows.Forms.Label lblDenom;
        private Common.Utilities.BmcComboBox cmbDenom;
        private System.Windows.Forms.Label lblPayout;
        private Common.Utilities.BmcComboBox cmbPayout;

    }
}