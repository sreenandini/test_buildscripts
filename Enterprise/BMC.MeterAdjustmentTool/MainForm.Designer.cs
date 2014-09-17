namespace BMC.MeterAdjustmentTool
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sbarMain = new System.Windows.Forms.StatusStrip();
            this.sbrItemConnectStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemSpring1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemEntUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemSite = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemDBServer = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbrItemDBUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.tbrItemConnect = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbrItemDisconnect = new System.Windows.Forms.ToolStripButton();
            this.tbrItemSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tbpRead = new System.Windows.Forms.TabPage();
            this.tblRead = new System.Windows.Forms.TableLayoutPanel();
            this.uxHourlyVTP = new BMC.MeterAdjustmentTool.UxDeltaGrid();
            this.uxFilterRead = new BMC.MeterAdjustmentTool.UxFilterCriteria();
            this.uxDailyRead = new BMC.MeterAdjustmentTool.UxDeltaGrid();
            this.tbpCollections = new System.Windows.Forms.TabPage();
            this.tblCollections = new System.Windows.Forms.TableLayoutPanel();
            this.uxCollectionDetails = new BMC.MeterAdjustmentTool.UxDeltaGrid();
            this.uxFilterCollections = new BMC.MeterAdjustmentTool.UxFilterCriteria();
            this.uxBatchDetails = new BMC.MeterAdjustmentTool.UxDeltaGrid();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.tblInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblSiteStatus = new System.Windows.Forms.Label();
            this.tbpMGMDDelta = new System.Windows.Forms.TabPage();
            this.tblMGMDDelta = new System.Windows.Forms.TableLayoutPanel();
            this.uxFilterMGMDDelta = new BMC.MeterAdjustmentTool.UxFilterCriteria();
            this.uxMGMDDelta = new BMC.MeterAdjustmentTool.UxDeltaGrid();
            this.sbarMain.SuspendLayout();
            this.tbrMain.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.tabContainer.SuspendLayout();
            this.tbpRead.SuspendLayout();
            this.tblRead.SuspendLayout();
            this.tbpCollections.SuspendLayout();
            this.tblCollections.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.tblInfo.SuspendLayout();
            this.tbpMGMDDelta.SuspendLayout();
            this.tblMGMDDelta.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbarMain
            // 
            this.sbarMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbarMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.sbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbrItemConnectStatus,
            this.sbrItemSpring1,
            this.sbrItemEntUser,
            this.sbrItemSite,
            this.sbrItemDBServer,
            this.sbrItemDBUserName});
            this.sbarMain.Location = new System.Drawing.Point(0, 541);
            this.sbarMain.Name = "sbarMain";
            this.sbarMain.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.sbarMain.Size = new System.Drawing.Size(990, 25);
            this.sbarMain.SizingGrip = false;
            this.sbarMain.TabIndex = 0;
            // 
            // sbrItemConnectStatus
            // 
            this.sbrItemConnectStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemConnectStatus.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemConnectStatus.Image = global::BMC.MeterAdjustmentTool.Properties.Resources.ViewSites1;
            this.sbrItemConnectStatus.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemConnectStatus.Name = "sbrItemConnectStatus";
            this.sbrItemConnectStatus.Size = new System.Drawing.Size(59, 20);
            this.sbrItemConnectStatus.Text = "[Site]";
            // 
            // sbrItemSpring1
            // 
            this.sbrItemSpring1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemSpring1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemSpring1.Name = "sbrItemSpring1";
            this.sbrItemSpring1.Size = new System.Drawing.Size(666, 20);
            this.sbrItemSpring1.Spring = true;
            // 
            // sbrItemEntUser
            // 
            this.sbrItemEntUser.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemEntUser.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemEntUser.Image = global::BMC.MeterAdjustmentTool.Properties.Resources.User;
            this.sbrItemEntUser.ImageTransparentColor = System.Drawing.Color.Black;
            this.sbrItemEntUser.Name = "sbrItemEntUser";
            this.sbrItemEntUser.Size = new System.Drawing.Size(63, 20);
            this.sbrItemEntUser.Text = "[User]";
            // 
            // sbrItemSite
            // 
            this.sbrItemSite.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemSite.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemSite.Name = "sbrItemSite";
            this.sbrItemSite.Size = new System.Drawing.Size(33, 20);
            this.sbrItemSite.Text = "Site";
            // 
            // sbrItemDBServer
            // 
            this.sbrItemDBServer.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemDBServer.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemDBServer.Name = "sbrItemDBServer";
            this.sbrItemDBServer.Size = new System.Drawing.Size(78, 20);
            this.sbrItemDBServer.Text = "DB SERVER";
            // 
            // sbrItemDBUserName
            // 
            this.sbrItemDBUserName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbrItemDBUserName.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.sbrItemDBUserName.Name = "sbrItemDBUserName";
            this.sbrItemDBUserName.Size = new System.Drawing.Size(74, 20);
            this.sbrItemDBUserName.Text = "User Name";
            // 
            // tbrMain
            // 
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbrItemConnect,
            this.tbrItemSep1,
            this.tbrItemDisconnect,
            this.tbrItemSep2});
            this.tbrMain.Location = new System.Drawing.Point(0, 0);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tbrMain.Size = new System.Drawing.Size(990, 25);
            this.tbrMain.TabIndex = 2;
            this.tbrMain.Text = "toolStrip1";
            // 
            // tbrItemConnect
            // 
            this.tbrItemConnect.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbrItemConnect.Image = global::BMC.MeterAdjustmentTool.Properties.Resources.ConnectServer;
            this.tbrItemConnect.ImageTransparentColor = System.Drawing.Color.Black;
            this.tbrItemConnect.Name = "tbrItemConnect";
            this.tbrItemConnect.Size = new System.Drawing.Size(110, 22);
            this.tbrItemConnect.Text = "Connect site...";
            this.tbrItemConnect.Click += new System.EventHandler(this.tbrItemConnect_Click);
            // 
            // tbrItemSep1
            // 
            this.tbrItemSep1.Name = "tbrItemSep1";
            this.tbrItemSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // tbrItemDisconnect
            // 
            this.tbrItemDisconnect.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbrItemDisconnect.Image = global::BMC.MeterAdjustmentTool.Properties.Resources.DisconnectServer;
            this.tbrItemDisconnect.ImageTransparentColor = System.Drawing.Color.Black;
            this.tbrItemDisconnect.Name = "tbrItemDisconnect";
            this.tbrItemDisconnect.Size = new System.Drawing.Size(125, 22);
            this.tbrItemDisconnect.Text = "Disconnect site...";
            this.tbrItemDisconnect.Click += new System.EventHandler(this.tbrItemDisconnect_Click);
            // 
            // tbrItemSep2
            // 
            this.tbrItemSep2.Name = "tbrItemSep2";
            this.tbrItemSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.tblContainer);
            this.pnlContainer.Location = new System.Drawing.Point(0, 25);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(6);
            this.pnlContainer.Size = new System.Drawing.Size(743, 372);
            this.pnlContainer.TabIndex = 3;
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tabContainer, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(6, 6);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 1;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 360F));
            this.tblContainer.Size = new System.Drawing.Size(731, 360);
            this.tblContainer.TabIndex = 1;
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tbpRead);
            this.tabContainer.Controls.Add(this.tbpCollections);
            this.tabContainer.Controls.Add(this.tbpMGMDDelta);
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Location = new System.Drawing.Point(3, 3);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(725, 354);
            this.tabContainer.TabIndex = 1;
            // 
            // tbpRead
            // 
            this.tbpRead.Controls.Add(this.tblRead);
            this.tbpRead.Location = new System.Drawing.Point(4, 22);
            this.tbpRead.Margin = new System.Windows.Forms.Padding(0);
            this.tbpRead.Name = "tbpRead";
            this.tbpRead.Size = new System.Drawing.Size(717, 328);
            this.tbpRead.TabIndex = 0;
            this.tbpRead.Text = "Read && VTP";
            this.tbpRead.UseVisualStyleBackColor = true;
            // 
            // tblRead
            // 
            this.tblRead.BackColor = System.Drawing.SystemColors.Control;
            this.tblRead.ColumnCount = 1;
            this.tblRead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRead.Controls.Add(this.uxHourlyVTP, 0, 2);
            this.tblRead.Controls.Add(this.uxFilterRead, 0, 0);
            this.tblRead.Controls.Add(this.uxDailyRead, 0, 1);
            this.tblRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRead.Location = new System.Drawing.Point(0, 0);
            this.tblRead.Name = "tblRead";
            this.tblRead.RowCount = 3;
            this.tblRead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblRead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRead.Size = new System.Drawing.Size(717, 328);
            this.tblRead.TabIndex = 0;
            // 
            // uxHourlyVTP
            // 
            this.uxHourlyVTP.CreateDataInterface = null;
            this.uxHourlyVTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxHourlyVTP.EditItem = null;
            this.uxHourlyVTP.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxHourlyVTP.HeaderText = "Suspected Hourly Data";
            this.uxHourlyVTP.IsEditable = false;
            this.uxHourlyVTP.LoadGridItems = null;
            this.uxHourlyVTP.Location = new System.Drawing.Point(3, 197);
            this.uxHourlyVTP.Name = "uxHourlyVTP";
            this.uxHourlyVTP.ProcessedArgs = null;
            this.uxHourlyVTP.Size = new System.Drawing.Size(711, 128);
            this.uxHourlyVTP.TabIndex = 2;
            this.uxHourlyVTP.UpdateItem = null;
            // 
            // uxFilterRead
            // 
            this.uxFilterRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFilterRead.EndDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 21);
            this.uxFilterRead.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxFilterRead.Location = new System.Drawing.Point(3, 3);
            this.uxFilterRead.Name = "uxFilterRead";
            this.uxFilterRead.OwnerForm = null;
            this.uxFilterRead.Size = new System.Drawing.Size(711, 54);
            this.uxFilterRead.StartDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 21);
            this.uxFilterRead.TabIndex = 0;
            this.uxFilterRead.ProcessClicked += new BMC.MeterAdjustmentTool.ProcessClickedEventHandler(this.uxFilterRead_ProcessClicked);
            // 
            // uxDailyRead
            // 
            this.uxDailyRead.CreateDataInterface = null;
            this.uxDailyRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxDailyRead.EditItem = null;
            this.uxDailyRead.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxDailyRead.HeaderText = "Suspected Daily Reads";
            this.uxDailyRead.IsEditable = false;
            this.uxDailyRead.LoadGridItems = null;
            this.uxDailyRead.Location = new System.Drawing.Point(3, 63);
            this.uxDailyRead.Name = "uxDailyRead";
            this.uxDailyRead.ProcessedArgs = null;
            this.uxDailyRead.Size = new System.Drawing.Size(711, 128);
            this.uxDailyRead.TabIndex = 1;
            this.uxDailyRead.UpdateItem = null;
            // 
            // tbpCollections
            // 
            this.tbpCollections.Controls.Add(this.tblCollections);
            this.tbpCollections.Location = new System.Drawing.Point(4, 22);
            this.tbpCollections.Margin = new System.Windows.Forms.Padding(0);
            this.tbpCollections.Name = "tbpCollections";
            this.tbpCollections.Size = new System.Drawing.Size(717, 328);
            this.tbpCollections.TabIndex = 1;
            this.tbpCollections.Text = "Collections";
            this.tbpCollections.UseVisualStyleBackColor = true;
            // 
            // tblCollections
            // 
            this.tblCollections.BackColor = System.Drawing.SystemColors.Control;
            this.tblCollections.ColumnCount = 1;
            this.tblCollections.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblCollections.Controls.Add(this.uxCollectionDetails, 0, 2);
            this.tblCollections.Controls.Add(this.uxFilterCollections, 0, 0);
            this.tblCollections.Controls.Add(this.uxBatchDetails, 0, 1);
            this.tblCollections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblCollections.Location = new System.Drawing.Point(0, 0);
            this.tblCollections.Name = "tblCollections";
            this.tblCollections.RowCount = 3;
            this.tblCollections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblCollections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCollections.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblCollections.Size = new System.Drawing.Size(717, 328);
            this.tblCollections.TabIndex = 1;
            // 
            // uxCollectionDetails
            // 
            this.uxCollectionDetails.CreateDataInterface = null;
            this.uxCollectionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxCollectionDetails.EditItem = null;
            this.uxCollectionDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxCollectionDetails.HeaderText = "Suspected Collection Details";
            this.uxCollectionDetails.IsEditable = false;
            this.uxCollectionDetails.LoadGridItems = null;
            this.uxCollectionDetails.Location = new System.Drawing.Point(3, 197);
            this.uxCollectionDetails.Name = "uxCollectionDetails";
            this.uxCollectionDetails.ProcessedArgs = null;
            this.uxCollectionDetails.Size = new System.Drawing.Size(711, 128);
            this.uxCollectionDetails.TabIndex = 2;
            this.uxCollectionDetails.UpdateItem = null;
            // 
            // uxFilterCollections
            // 
            this.uxFilterCollections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFilterCollections.EndDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 52);
            this.uxFilterCollections.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxFilterCollections.Location = new System.Drawing.Point(3, 3);
            this.uxFilterCollections.Name = "uxFilterCollections";
            this.uxFilterCollections.OwnerForm = null;
            this.uxFilterCollections.Size = new System.Drawing.Size(711, 54);
            this.uxFilterCollections.StartDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 52);
            this.uxFilterCollections.TabIndex = 0;
            this.uxFilterCollections.ProcessClicked += new BMC.MeterAdjustmentTool.ProcessClickedEventHandler(this.uxFilterCollections_ProcessClicked);
            // 
            // uxBatchDetails
            // 
            this.uxBatchDetails.CreateDataInterface = null;
            this.uxBatchDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxBatchDetails.EditItem = null;
            this.uxBatchDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxBatchDetails.HeaderText = "Suspected Batch Details";
            this.uxBatchDetails.IsEditable = false;
            this.uxBatchDetails.LoadGridItems = null;
            this.uxBatchDetails.Location = new System.Drawing.Point(3, 63);
            this.uxBatchDetails.Name = "uxBatchDetails";
            this.uxBatchDetails.ProcessedArgs = null;
            this.uxBatchDetails.Size = new System.Drawing.Size(711, 128);
            this.uxBatchDetails.TabIndex = 1;
            this.uxBatchDetails.UpdateItem = null;
            this.uxBatchDetails.RowSelected += new BMC.MeterAdjustmentTool.GridRowSelectedEventHandler(this.uxBatchDetails_RowSelected);
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlInfo.Controls.Add(this.tblInfo);
            this.pnlInfo.Location = new System.Drawing.Point(7, 377);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(430, 127);
            this.pnlInfo.TabIndex = 4;
            // 
            // tblInfo
            // 
            this.tblInfo.ColumnCount = 1;
            this.tblInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInfo.Controls.Add(this.lblSiteStatus, 0, 0);
            this.tblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblInfo.Location = new System.Drawing.Point(0, 0);
            this.tblInfo.Name = "tblInfo";
            this.tblInfo.RowCount = 1;
            this.tblInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblInfo.Size = new System.Drawing.Size(430, 127);
            this.tblInfo.TabIndex = 0;
            // 
            // lblSiteStatus
            // 
            this.lblSiteStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSiteStatus.AutoSize = true;
            this.lblSiteStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSiteStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSiteStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSiteStatus.Location = new System.Drawing.Point(151, 56);
            this.lblSiteStatus.Name = "lblSiteStatus";
            this.lblSiteStatus.Size = new System.Drawing.Size(127, 14);
            this.lblSiteStatus.TabIndex = 0;
            this.lblSiteStatus.Text = "Site disconnected.";
            this.lblSiteStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbpMGMDDelta
            // 
            this.tbpMGMDDelta.Controls.Add(this.tblMGMDDelta);
            this.tbpMGMDDelta.Location = new System.Drawing.Point(4, 22);
            this.tbpMGMDDelta.Name = "tbpMGMDDelta";
            this.tbpMGMDDelta.Size = new System.Drawing.Size(717, 328);
            this.tbpMGMDDelta.TabIndex = 2;
            this.tbpMGMDDelta.Text = "MGMD Session";
            this.tbpMGMDDelta.UseVisualStyleBackColor = true;
            // 
            // tblMGMDDelta
            // 
            this.tblMGMDDelta.BackColor = System.Drawing.SystemColors.Control;
            this.tblMGMDDelta.ColumnCount = 1;
            this.tblMGMDDelta.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMGMDDelta.Controls.Add(this.uxFilterMGMDDelta, 0, 0);
            this.tblMGMDDelta.Controls.Add(this.uxMGMDDelta, 0, 1);
            this.tblMGMDDelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMGMDDelta.Location = new System.Drawing.Point(0, 0);
            this.tblMGMDDelta.Name = "tblMGMDDelta";
            this.tblMGMDDelta.RowCount = 2;
            this.tblMGMDDelta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblMGMDDelta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMGMDDelta.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMGMDDelta.Size = new System.Drawing.Size(717, 328);
            this.tblMGMDDelta.TabIndex = 1;
            // 
            // uxFilterMGMDDelta
            // 
            this.uxFilterMGMDDelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxFilterMGMDDelta.EndDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 21);
            this.uxFilterMGMDDelta.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxFilterMGMDDelta.Location = new System.Drawing.Point(3, 3);
            this.uxFilterMGMDDelta.Name = "uxFilterMGMDDelta";
            this.uxFilterMGMDDelta.OwnerForm = null;
            this.uxFilterMGMDDelta.Size = new System.Drawing.Size(711, 54);
            this.uxFilterMGMDDelta.StartDate = new System.DateTime(2012, 3, 21, 16, 33, 7, 21);
            this.uxFilterMGMDDelta.TabIndex = 0;
            this.uxFilterMGMDDelta.ProcessClicked += new BMC.MeterAdjustmentTool.ProcessClickedEventHandler(this.uxFilterMGMDDelta_ProcessClicked);
            // 
            // uxMGMDDelta
            // 
            this.uxMGMDDelta.CreateDataInterface = null;
            this.uxMGMDDelta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uxMGMDDelta.EditItem = null;
            this.uxMGMDDelta.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxMGMDDelta.HeaderText = "Suspected MGMD Session Deltas";
            this.uxMGMDDelta.IsEditable = false;
            this.uxMGMDDelta.LoadGridItems = null;
            this.uxMGMDDelta.Location = new System.Drawing.Point(3, 63);
            this.uxMGMDDelta.Name = "uxMGMDDelta";
            this.uxMGMDDelta.ProcessedArgs = null;
            this.uxMGMDDelta.Size = new System.Drawing.Size(711, 262);
            this.uxMGMDDelta.TabIndex = 1;
            this.uxMGMDDelta.UpdateItem = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 566);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.tbrMain);
            this.Controls.Add(this.sbarMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(932, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMC Meter Adjustment Tool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.sbarMain.ResumeLayout(false);
            this.sbarMain.PerformLayout();
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            this.tblContainer.ResumeLayout(false);
            this.tabContainer.ResumeLayout(false);
            this.tbpRead.ResumeLayout(false);
            this.tblRead.ResumeLayout(false);
            this.tbpCollections.ResumeLayout(false);
            this.tblCollections.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.tblInfo.ResumeLayout(false);
            this.tblInfo.PerformLayout();
            this.tbpMGMDDelta.ResumeLayout(false);
            this.tblMGMDDelta.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sbarMain;
        private System.Windows.Forms.ToolStrip tbrMain;
        private System.Windows.Forms.ToolStripButton tbrItemConnect;
        private System.Windows.Forms.ToolStripButton tbrItemDisconnect;
        private System.Windows.Forms.ToolStripSeparator tbrItemSep1;
        private System.Windows.Forms.ToolStripSeparator tbrItemSep2;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.TableLayoutPanel tblInfo;
        private System.Windows.Forms.Label lblSiteStatus;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemConnectStatus;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemSpring1;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemSite;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemDBServer;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemDBUserName;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tbpRead;
        private System.Windows.Forms.TableLayoutPanel tblRead;
        private UxDeltaGrid uxHourlyVTP;
        private UxFilterCriteria uxFilterRead;
        private UxDeltaGrid uxDailyRead;
        private System.Windows.Forms.TabPage tbpCollections;
        private System.Windows.Forms.TableLayoutPanel tblCollections;
        private UxDeltaGrid uxCollectionDetails;
        private UxFilterCriteria uxFilterCollections;
        private UxDeltaGrid uxBatchDetails;
        private System.Windows.Forms.ToolStripStatusLabel sbrItemEntUser;
        private System.Windows.Forms.TabPage tbpMGMDDelta;
        private System.Windows.Forms.TableLayoutPanel tblMGMDDelta;
        private UxFilterCriteria uxFilterMGMDDelta;
        private UxDeltaGrid uxMGMDDelta;
    }
}

