namespace BMC.EnterpriseClient.Views
{
    partial class UcVSHourlyDetails
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
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_118 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_119 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_120 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_121 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_122 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_123 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_124 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_125 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_126 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_127 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_128 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_129 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_130 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<int> comboBoxItem_131 = new BMC.CoreLib.Win32.ComboBoxItem<int>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_132 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_133 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType> comboBoxItem_134 = new BMC.CoreLib.Win32.ComboBoxItem<BMC.EnterpriseClient.Views.CPeriodUnitsType>();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lvwHourly = new BMC.CoreLib.Win32.ListViewEx();
            this.chdrDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrDay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChdrAsset = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chdrTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tblHeader = new System.Windows.Forms.TableLayoutPanel();
            this.cboHourlyTypes = new System.Windows.Forms.ComboBox();
            this.cPeriodCount1 = new BMC.EnterpriseClient.Views.CPeriodCount();
            this.cPeriodUnits1 = new BMC.EnterpriseClient.Views.CPeriodUnits();
            this.cboFilterBy = new System.Windows.Forms.ComboBox();
            this.cboFilterByValue = new System.Windows.Forms.ComboBox();
            this.btnDetails = new System.Windows.Forms.Button();
            this.chkCalendarDay = new System.Windows.Forms.CheckBox();
            this.tblContainer.SuspendLayout();
            this.tblHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.lvwHourly, 0, 1);
            this.tblContainer.Controls.Add(this.tblHeader, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Size = new System.Drawing.Size(1041, 611);
            this.tblContainer.TabIndex = 0;
            // 
            // lvwHourly
            // 
            this.lvwHourly.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvwHourly.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvwHourly.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chdrDate,
            this.chdrDay,
            this.ChdrAsset,
            this.chdrTotal});
            this.lvwHourly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwHourly.FullRowSelect = true;
            this.lvwHourly.GridLines = true;
            this.lvwHourly.HideSelection = false;
            this.lvwHourly.Location = new System.Drawing.Point(3, 38);
            this.lvwHourly.Name = "lvwHourly";
            this.lvwHourly.Size = new System.Drawing.Size(1035, 570);
            this.lvwHourly.TabIndex = 1;
            this.lvwHourly.UseCompatibleStateImageBehavior = false;
            this.lvwHourly.View = System.Windows.Forms.View.Details;
            this.lvwHourly.SelectedIndexChanged += new System.EventHandler(this.lvwHourly_SelectedIndexChanged);
            this.lvwHourly.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwHourly_KeyDown);
            this.lvwHourly.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwHourly_MouseDoubleClick);
            // 
            // chdrDate
            // 
            this.chdrDate.Text = "Date";
            // 
            // chdrDay
            // 
            this.chdrDay.Text = "Day";
            // 
            // ChdrAsset
            // 
            this.ChdrAsset.Text = "Asset";
            this.ChdrAsset.Width = 0;
            // 
            // chdrTotal
            // 
            this.chdrTotal.Text = "Total";
            this.chdrTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tblHeader
            // 
            this.tblHeader.ColumnCount = 7;
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblHeader.Controls.Add(this.cboHourlyTypes, 0, 0);
            this.tblHeader.Controls.Add(this.cPeriodCount1, 3, 0);
            this.tblHeader.Controls.Add(this.cPeriodUnits1, 4, 0);
            this.tblHeader.Controls.Add(this.cboFilterBy, 5, 0);
            this.tblHeader.Controls.Add(this.cboFilterByValue, 6, 0);
            this.tblHeader.Controls.Add(this.btnDetails, 2, 0);
            this.tblHeader.Controls.Add(this.chkCalendarDay, 1, 0);
            this.tblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblHeader.Location = new System.Drawing.Point(0, 0);
            this.tblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tblHeader.Name = "tblHeader";
            this.tblHeader.RowCount = 1;
            this.tblHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblHeader.Size = new System.Drawing.Size(1041, 35);
            this.tblHeader.TabIndex = 0;
            // 
            // cboHourlyTypes
            // 
            this.cboHourlyTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHourlyTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHourlyTypes.FormattingEnabled = true;
            this.cboHourlyTypes.Location = new System.Drawing.Point(3, 7);
            this.cboHourlyTypes.MinimumSize = new System.Drawing.Size(130, 0);
            this.cboHourlyTypes.Name = "cboHourlyTypes";
            this.cboHourlyTypes.Size = new System.Drawing.Size(294, 21);
            this.cboHourlyTypes.TabIndex = 0;
            this.cboHourlyTypes.SelectedIndexChanged += new System.EventHandler(this.cboHourlyTypes_SelectedIndexChanged);
            // 
            // cPeriodCount1
            // 
            this.cPeriodCount1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cPeriodCount1.DisplayMember = "Text";
            this.cPeriodCount1.FormattingEnabled = true;
            comboBoxItem_118.Text = null;
            comboBoxItem_118.Value = -1;
            comboBoxItem_119.Text = "1";
            comboBoxItem_119.Value = 1;
            comboBoxItem_120.Text = "2";
            comboBoxItem_120.Value = 2;
            comboBoxItem_121.Text = "3";
            comboBoxItem_121.Value = 3;
            comboBoxItem_122.Text = "4";
            comboBoxItem_122.Value = 4;
            comboBoxItem_123.Text = "5";
            comboBoxItem_123.Value = 5;
            comboBoxItem_124.Text = "6";
            comboBoxItem_124.Value = 6;
            comboBoxItem_125.Text = "12";
            comboBoxItem_125.Value = 12;
            comboBoxItem_126.Text = "16";
            comboBoxItem_126.Value = 16;
            comboBoxItem_127.Text = "24";
            comboBoxItem_127.Value = 24;
            comboBoxItem_128.Text = "36";
            comboBoxItem_128.Value = 36;
            comboBoxItem_129.Text = "48";
            comboBoxItem_129.Value = 48;
            comboBoxItem_130.Text = "60";
            comboBoxItem_130.Value = 60;
            comboBoxItem_131.Text = "90";
            comboBoxItem_131.Value = 90;
            this.cPeriodCount1.Items.AddRange(new object[] {
            comboBoxItem_118,
            comboBoxItem_119,
            comboBoxItem_120,
            comboBoxItem_121,
            comboBoxItem_122,
            comboBoxItem_123,
            comboBoxItem_124,
            comboBoxItem_125,
            comboBoxItem_126,
            comboBoxItem_127,
            comboBoxItem_128,
            comboBoxItem_129,
            comboBoxItem_130,
            comboBoxItem_131});
            this.cPeriodCount1.Location = new System.Drawing.Point(624, 7);
            this.cPeriodCount1.Name = "cPeriodCount1";
            this.cPeriodCount1.SelectedCount = -1;
            this.cPeriodCount1.Size = new System.Drawing.Size(54, 21);
            this.cPeriodCount1.TabIndex = 3;
            this.cPeriodCount1.ValueMember = "Value";
            this.cPeriodCount1.SelectedIndexChanged += new System.EventHandler(this.cPeriodCount1_SelectedIndexChanged);
            // 
            // cPeriodUnits1
            // 
            this.cPeriodUnits1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cPeriodUnits1.DisplayMember = "Text";
            this.cPeriodUnits1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cPeriodUnits1.FormattingEnabled = true;
            comboBoxItem_132.Text = "Records";
            comboBoxItem_132.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Records;
            comboBoxItem_133.Text = "Weeks";
            comboBoxItem_133.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Weeks;
            comboBoxItem_134.Text = "Periods";
            comboBoxItem_134.Value = BMC.EnterpriseClient.Views.CPeriodUnitsType.Periods;
            this.cPeriodUnits1.Items.AddRange(new object[] {
            comboBoxItem_132,
            comboBoxItem_133,
            comboBoxItem_134});
            this.cPeriodUnits1.Location = new System.Drawing.Point(684, 7);
            this.cPeriodUnits1.Name = "cPeriodUnits1";
            this.cPeriodUnits1.SelectedUnit = BMC.EnterpriseClient.Views.CPeriodUnitsType.Records;
            this.cPeriodUnits1.Size = new System.Drawing.Size(114, 21);
            this.cPeriodUnits1.TabIndex = 4;
            this.cPeriodUnits1.ValueMember = "Value";
            // 
            // cboFilterBy
            // 
            this.cboFilterBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterBy.FormattingEnabled = true;
            this.cboFilterBy.Location = new System.Drawing.Point(804, 7);
            this.cboFilterBy.Name = "cboFilterBy";
            this.cboFilterBy.Size = new System.Drawing.Size(114, 21);
            this.cboFilterBy.TabIndex = 5;
            this.cboFilterBy.SelectedIndexChanged += new System.EventHandler(this.cboFilterBy_SelectedIndexChanged);
            // 
            // cboFilterByValue
            // 
            this.cboFilterByValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFilterByValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterByValue.FormattingEnabled = true;
            this.cboFilterByValue.Location = new System.Drawing.Point(924, 7);
            this.cboFilterByValue.Name = "cboFilterByValue";
            this.cboFilterByValue.Size = new System.Drawing.Size(114, 21);
            this.cboFilterByValue.TabIndex = 6;
            this.cboFilterByValue.SelectedIndexChanged += new System.EventHandler(this.cboFilterByValue_SelectedIndexChanged);
            this.cboFilterByValue.MouseLeave += new System.EventHandler(this.cboFilterByValue_MouseLeave_1);
            this.cboFilterByValue.MouseHover += new System.EventHandler(this.cboFilterByValue_MouseHover_1);
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetails.Location = new System.Drawing.Point(544, 6);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(74, 23);
            this.btnDetails.TabIndex = 2;
            this.btnDetails.Text = "Details";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // chkCalendarDay
            // 
            this.chkCalendarDay.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkCalendarDay.AutoSize = true;
            this.chkCalendarDay.BackColor = System.Drawing.SystemColors.Control;
            this.chkCalendarDay.Location = new System.Drawing.Point(442, 9);
            this.chkCalendarDay.Name = "chkCalendarDay";
            this.chkCalendarDay.Size = new System.Drawing.Size(96, 17);
            this.chkCalendarDay.TabIndex = 1;
            this.chkCalendarDay.Text = "Calendar Day?";
            this.chkCalendarDay.UseVisualStyleBackColor = false;
            // 
            // UcVSHourlyDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblContainer);
            this.Name = "UcVSHourlyDetails";
            this.Size = new System.Drawing.Size(1041, 611);
            this.Load += new System.EventHandler(this.UcVSHourlyDetails_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblHeader.ResumeLayout(false);
            this.tblHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private BMC.CoreLib.Win32.ListViewEx lvwHourly;
        private System.Windows.Forms.ColumnHeader chdrDate;
        private System.Windows.Forms.ColumnHeader chdrDay;
        private System.Windows.Forms.ColumnHeader chdrTotal;
        private System.Windows.Forms.TableLayoutPanel tblHeader;
        private CPeriodCount cPeriodCount1;
        private CPeriodUnits cPeriodUnits1;
        private System.Windows.Forms.ComboBox cboFilterBy;
        private System.Windows.Forms.ComboBox cboFilterByValue;
        private System.Windows.Forms.ComboBox cboHourlyTypes;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.CheckBox chkCalendarDay;
        private System.Windows.Forms.ColumnHeader ChdrAsset;
    }
}
