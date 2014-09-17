namespace BMC.EnterpriseClient.Views
{
    partial class UcVSDrop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcVSDrop));
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_11 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_12 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_13 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_14 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_15 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_16 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_17 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_18 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_19 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_110 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_111 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_112 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_113 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_114 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_115 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_116 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_117 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lvwDetails = new BMC.CoreLib.Win32.ListViewEx();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.cPeriodCount = new BMC.EnterpriseClient.Views.CPeriodCount();
            this.cPeriodUnits = new BMC.EnterpriseClient.Views.CPeriodUnits();
            this.tabDrop = new System.Windows.Forms.TabControl();
            this.tbpPosition = new System.Windows.Forms.TabPage();
            this.tbpBatch = new System.Windows.Forms.TabPage();
            this.tbpWeek = new System.Windows.Forms.TabPage();
            this.ctxMenuColl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemCollDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemCollRemarks = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuBatch = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMenuItemBatchMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemBatchDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.deMergeBatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tblContainer.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.tabDrop.SuspendLayout();
            this.ctxMenuColl.SuspendLayout();
            this.ctxMenuBatch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.lvwDetails, 0, 1);
            this.tblContainer.Controls.Add(this.tblHeader, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(810, 612);
            this.tblContainer.TabIndex = 0;
            // 
            // lvwDetails
            // 
            this.lvwDetails.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwDetails.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwDetails.FullRowSelect = true;
            this.lvwDetails.GridLines = true;
            this.lvwDetails.HideSelection = false;
            this.lvwDetails.Location = new System.Drawing.Point(3, 38);
            this.lvwDetails.Name = "lvwDetails";
            this.lvwDetails.Size = new System.Drawing.Size(804, 571);
            this.lvwDetails.SmallImageList = this.imglstSmallIcons;
            this.lvwDetails.TabIndex = 1;
            this.lvwDetails.UseCompatibleStateImageBehavior = false;
            this.lvwDetails.View = System.Windows.Forms.View.Details;
            this.lvwDetails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwDetails_ColumnClick);
            this.lvwDetails.SelectedIndexChanged += new System.EventHandler(this.lvwDetails_SelectedIndexChanged);
            this.lvwDetails.DoubleClick += new System.EventHandler(this.lvwDetails_DoubleClick);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Black.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Blue.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "ForestGreen.ico");
            this.imglstSmallIcons.Images.SetKeyName(3, "Green.ico");
            this.imglstSmallIcons.Images.SetKeyName(4, "Red.ico");
            this.imglstSmallIcons.Images.SetKeyName(5, "White.ico");
            this.imglstSmallIcons.Images.SetKeyName(6, "Yellow.ico");
            this.imglstSmallIcons.Images.SetKeyName(7, "Pink.ico");
            this.imglstSmallIcons.Images.SetKeyName(8, "Violet.ico");
            this.imglstSmallIcons.Images.SetKeyName(9, "Orange.ico");
            this.imglstSmallIcons.Images.SetKeyName(10, "SkyBlue.ico");
            this.imglstSmallIcons.Images.SetKeyName(11, "LightRed.ico");
            this.imglstSmallIcons.Images.SetKeyName(12, "Pattern.ico");
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 4;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 306F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHeader.Controls.Add(this.cPeriodCount, 2, 0);
            this.tblHeader.Controls.Add(this.cPeriodUnits, 3, 0);
            this.tblHeader.Controls.Add(this.tabDrop, 0, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(810, 35);
            this.tblHeader.TabIndex = 0;
            // 
            // cPeriodCount
            // 
            this.cPeriodCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cPeriodCount.DisplayMember = "Text";
            this.cPeriodCount.FormattingEnabled = true;
            comboBoxItem_11.Text = "-- ALL --";
            comboBoxItem_11.Value = -1;
            comboBoxItem_12.Text = "1";
            comboBoxItem_12.Value = 1;
            comboBoxItem_13.Text = "2";
            comboBoxItem_13.Value = 2;
            comboBoxItem_14.Text = "3";
            comboBoxItem_14.Value = 3;
            comboBoxItem_15.Text = "4";
            comboBoxItem_15.Value = 4;
            comboBoxItem_16.Text = "5";
            comboBoxItem_16.Value = 5;
            comboBoxItem_17.Text = "6";
            comboBoxItem_17.Value = 6;
            comboBoxItem_18.Text = "12";
            comboBoxItem_18.Value = 12;
            comboBoxItem_19.Text = "16";
            comboBoxItem_19.Value = 16;
            comboBoxItem_110.Text = "24";
            comboBoxItem_110.Value = 24;
            comboBoxItem_111.Text = "36";
            comboBoxItem_111.Value = 36;
            comboBoxItem_112.Text = "48";
            comboBoxItem_112.Value = 48;
            comboBoxItem_113.Text = "60";
            comboBoxItem_113.Value = 60;
            comboBoxItem_114.Text = "90";
            comboBoxItem_114.Value = 90;
            this.cPeriodCount.Items.AddRange(new object[] {
            comboBoxItem_11,
            comboBoxItem_12,
            comboBoxItem_13,
            comboBoxItem_14,
            comboBoxItem_15,
            comboBoxItem_16,
            comboBoxItem_17,
            comboBoxItem_18,
            comboBoxItem_19,
            comboBoxItem_110,
            comboBoxItem_111,
            comboBoxItem_112,
            comboBoxItem_113,
            comboBoxItem_114});
            this.cPeriodCount.Location = new System.Drawing.Point(633, 7);
            this.cPeriodCount.Name = "cPeriodCount";
            this.cPeriodCount.SelectedCount = -1;
            this.cPeriodCount.Size = new System.Drawing.Size(54, 21);
            this.cPeriodCount.TabIndex = 1;
            this.cPeriodCount.ValueMember = "Value";
            // 
            // cPeriodUnits
            // 
            this.cPeriodUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cPeriodUnits.DisplayMember = "Text";
            this.cPeriodUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPeriodUnits.FormattingEnabled = true;
            comboBoxItem_115.Text = "Records";
            comboBoxItem_115.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Records;
            comboBoxItem_116.Text = "Weeks";
            comboBoxItem_116.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Weeks;
            comboBoxItem_117.Text = "Periods";
            comboBoxItem_117.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Periods;
            //this.cPeriodUnits.Items.AddRange(new object[] {
            //comboBoxItem_115,
            //comboBoxItem_116,
            //comboBoxItem_117});
            this.cPeriodUnits.Location = new System.Drawing.Point(693, 7);
            this.cPeriodUnits.Name = "cPeriodUnits";
            this.cPeriodUnits.SelectedUnit = BMC.EnterpriseClient.Views.CPeriodUnitsType.Records;
            this.cPeriodUnits.Size = new System.Drawing.Size(114, 21);
            this.cPeriodUnits.TabIndex = 2;
            this.cPeriodUnits.ValueMember = "Value";
            // 
            // tabDrop
            // 
            this.tabDrop.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabDrop.Controls.Add(this.tbpPosition);
            this.tabDrop.Controls.Add(this.tbpBatch);
            this.tabDrop.Controls.Add(this.tbpWeek);
            this.tabDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDrop.Location = new System.Drawing.Point(0, 8);
            this.tabDrop.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tabDrop.Name = "tabDrop";
            this.tabDrop.SelectedIndex = 0;
            this.tabDrop.Size = new System.Drawing.Size(306, 27);
            this.tabDrop.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabDrop.TabIndex = 0;
            this.tabDrop.SelectedIndexChanged += new System.EventHandler(this.tabDrop_SelectedIndexChanged);
            this.tabDrop.Deselected += new System.Windows.Forms.TabControlEventHandler(this.tabDrop_Deselected);
            // 
            // tbpPosition
            // 
            this.tbpPosition.Location = new System.Drawing.Point(4, 25);
            this.tbpPosition.Name = "tbpPosition";
            this.tbpPosition.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPosition.Size = new System.Drawing.Size(298, 0);
            this.tbpPosition.TabIndex = 0;
            this.tbpPosition.Text = "Position";
            this.tbpPosition.UseVisualStyleBackColor = true;
            // 
            // tbpBatch
            // 
            this.tbpBatch.Location = new System.Drawing.Point(4, 25);
            this.tbpBatch.Name = "tbpBatch";
            this.tbpBatch.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBatch.Size = new System.Drawing.Size(298, 0);
            this.tbpBatch.TabIndex = 1;
            this.tbpBatch.Text = "Batch";
            this.tbpBatch.UseVisualStyleBackColor = true;
            // 
            // tbpWeek
            // 
            this.tbpWeek.Location = new System.Drawing.Point(4, 25);
            this.tbpWeek.Name = "tbpWeek";
            this.tbpWeek.Size = new System.Drawing.Size(298, 0);
            this.tbpWeek.TabIndex = 2;
            this.tbpWeek.Text = "Week";
            this.tbpWeek.UseVisualStyleBackColor = true;
            // 
            // ctxMenuColl
            // 
            this.ctxMenuColl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemCollDetails,
            this.ctxMenuItemCollRemarks});
            this.ctxMenuColl.Name = "ctxMenuColl";
            this.ctxMenuColl.Size = new System.Drawing.Size(195, 48);
            // 
            // ctxMenuItemCollDetails
            // 
            this.ctxMenuItemCollDetails.Name = "ctxMenuItemCollDetails";
            this.ctxMenuItemCollDetails.Size = new System.Drawing.Size(194, 22);
            this.ctxMenuItemCollDetails.Text = "View Collection Details";
            this.ctxMenuItemCollDetails.Click += new System.EventHandler(this.ctxMenuItemCollDetails_Click_1);
            // 
            // ctxMenuItemCollRemarks
            // 
            this.ctxMenuItemCollRemarks.Name = "ctxMenuItemCollRemarks";
            this.ctxMenuItemCollRemarks.Size = new System.Drawing.Size(194, 22);
            this.ctxMenuItemCollRemarks.Text = "Remarks";
            this.ctxMenuItemCollRemarks.Click += new System.EventHandler(this.ctxMenuItemCollRemarks_Click);
            // 
            // ctxMenuBatch
            // 
            this.ctxMenuBatch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxMenuItemBatchMerge,
            this.ctxMenuItemBatchDelete,
            this.deMergeBatchToolStripMenuItem});
            this.ctxMenuBatch.Name = "ctxMenuPosition";
            this.ctxMenuBatch.Size = new System.Drawing.Size(198, 70);
            // 
            // ctxMenuItemBatchMerge
            // 
            this.ctxMenuItemBatchMerge.Name = "ctxMenuItemBatchMerge";
            this.ctxMenuItemBatchMerge.Size = new System.Drawing.Size(197, 22);
            this.ctxMenuItemBatchMerge.Text = "Merge Another Batch";
            this.ctxMenuItemBatchMerge.Click += new System.EventHandler(this.ctxMenuItemBatchMerge_Click);
            // 
            // ctxMenuItemBatchDelete
            // 
            this.ctxMenuItemBatchDelete.Name = "ctxMenuItemBatchDelete";
            this.ctxMenuItemBatchDelete.Size = new System.Drawing.Size(197, 22);
            this.ctxMenuItemBatchDelete.Text = "Delete Collection Batch";
            this.ctxMenuItemBatchDelete.Click += new System.EventHandler(this.ctxMenuItemBatchDelete_Click);
            // 
            // deMergeBatchToolStripMenuItem
            // 
            this.deMergeBatchToolStripMenuItem.Name = "deMergeBatchToolStripMenuItem";
            this.deMergeBatchToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.deMergeBatchToolStripMenuItem.Text = "DeMergeBatch";
            this.deMergeBatchToolStripMenuItem.Visible = false;
            this.deMergeBatchToolStripMenuItem.Click += new System.EventHandler(this.deMergeBatchToolStripMenuItem_Click);
            // 
            // UcVSDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "UcVSDrop";
            this.Size = new System.Drawing.Size(810, 612);
            this.Load += new System.EventHandler(this.UcVSDrop_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tabDrop.ResumeLayout(false);
            this.ctxMenuColl.ResumeLayout(false);
            this.ctxMenuBatch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CPeriodCount cPeriodCount;
        private CPeriodUnits cPeriodUnits;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private BMC.CoreLib.Win32.ListViewEx lvwDetails;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private System.Windows.Forms.TabControl tabDrop;
        private System.Windows.Forms.TabPage tbpPosition;
        private System.Windows.Forms.TabPage tbpBatch;
        private System.Windows.Forms.TabPage tbpWeek;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        private System.Windows.Forms.ContextMenuStrip ctxMenuColl;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemCollDetails;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemCollRemarks;
        private System.Windows.Forms.ContextMenuStrip ctxMenuBatch;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemBatchMerge;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemBatchDelete;
        private System.Windows.Forms.ToolStripMenuItem deMergeBatchToolStripMenuItem;
    }
}
