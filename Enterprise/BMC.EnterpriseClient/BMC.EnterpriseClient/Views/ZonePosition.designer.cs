namespace BMC.EnterpriseClient.Views
{
    partial class ZonePosition
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
            this.btnRoutePositions = new System.Windows.Forms.Button();
            this.btnBulkCopyPositions = new System.Windows.Forms.Button();
            this.btnNewPosition = new System.Windows.Forms.Button();
            this.btnEditPosition = new System.Windows.Forms.Button();
            this.grpButtons = new System.Windows.Forms.GroupBox();
            this.tblPositionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.grpZones = new System.Windows.Forms.GroupBox();
            this.tblZone = new System.Windows.Forms.TableLayoutPanel();
            this.lblZones = new System.Windows.Forms.Label();
            this.lstZones = new System.Windows.Forms.ListBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtZoneName = new System.Windows.Forms.TextBox();
            this.btnZoneTimes = new System.Windows.Forms.Button();
            this.lblOpeningHours = new System.Windows.Forms.Label();
            this.lstZoneOpeningHours = new System.Windows.Forms.ComboBox();
            this.chkPromotionEnabled = new System.Windows.Forms.CheckBox();
            this.grpInstallations = new System.Windows.Forms.GroupBox();
            this.lvBarPos = new System.Windows.Forms.ListView();
            this.colPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colZone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGameTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRouteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblInstallations = new System.Windows.Forms.Label();
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.grpZoneButtons = new System.Windows.Forms.GroupBox();
            this.tblZoneButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewZone = new System.Windows.Forms.Button();
            this.btnZoneDelete = new System.Windows.Forms.Button();
            this.btnZoneApply = new System.Windows.Forms.Button();
            this.grpButtons.SuspendLayout();
            this.tblPositionButtons.SuspendLayout();
            this.grpZones.SuspendLayout();
            this.tblZone.SuspendLayout();
            this.grpInstallations.SuspendLayout();
            this.tblContainer.SuspendLayout();
            this.grpZoneButtons.SuspendLayout();
            this.tblZoneButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRoutePositions
            // 
            this.btnRoutePositions.Location = new System.Drawing.Point(3, 127);
            this.btnRoutePositions.Name = "btnRoutePositions";
            this.btnRoutePositions.Size = new System.Drawing.Size(100, 28);
            this.btnRoutePositions.TabIndex = 3;
            this.btnRoutePositions.Text = "Route Positions";
            this.btnRoutePositions.UseVisualStyleBackColor = true;
            this.btnRoutePositions.Click += new System.EventHandler(this.btnRoutePositions_Click);
            // 
            // btnBulkCopyPositions
            // 
            this.btnBulkCopyPositions.Location = new System.Drawing.Point(3, 83);
            this.btnBulkCopyPositions.Name = "btnBulkCopyPositions";
            this.btnBulkCopyPositions.Size = new System.Drawing.Size(100, 36);
            this.btnBulkCopyPositions.TabIndex = 2;
            this.btnBulkCopyPositions.Text = "Bulk Copy Positions";
            this.btnBulkCopyPositions.UseVisualStyleBackColor = true;
            this.btnBulkCopyPositions.Click += new System.EventHandler(this.btnBulkCopyPositions_Click);
            // 
            // btnNewPosition
            // 
            this.btnNewPosition.Location = new System.Drawing.Point(3, 43);
            this.btnNewPosition.Name = "btnNewPosition";
            this.btnNewPosition.Size = new System.Drawing.Size(100, 28);
            this.btnNewPosition.TabIndex = 1;
            this.btnNewPosition.Text = "New Position";
            this.btnNewPosition.UseVisualStyleBackColor = true;
            this.btnNewPosition.Click += new System.EventHandler(this.btnNewPosition_Click);
            // 
            // btnEditPosition
            // 
            this.btnEditPosition.Enabled = false;
            this.btnEditPosition.Location = new System.Drawing.Point(3, 3);
            this.btnEditPosition.Name = "btnEditPosition";
            this.btnEditPosition.Size = new System.Drawing.Size(100, 28);
            this.btnEditPosition.TabIndex = 0;
            this.btnEditPosition.Text = "Edit Position";
            this.btnEditPosition.UseVisualStyleBackColor = true;
            this.btnEditPosition.Click += new System.EventHandler(this.btnEditPosition_Click);
            // 
            // grpButtons
            // 
            this.grpButtons.Controls.Add(this.tblPositionButtons);
            this.grpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpButtons.Location = new System.Drawing.Point(612, 3);
            this.grpButtons.Name = "grpButtons";
            this.grpButtons.Size = new System.Drawing.Size(114, 408);
            this.grpButtons.TabIndex = 0;
            this.grpButtons.TabStop = false;
            // 
            // tblPositionButtons
            // 
            this.tblPositionButtons.ColumnCount = 1;
            this.tblPositionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPositionButtons.Controls.Add(this.btnEditPosition, 0, 0);
            this.tblPositionButtons.Controls.Add(this.btnRoutePositions, 0, 3);
            this.tblPositionButtons.Controls.Add(this.btnBulkCopyPositions, 0, 2);
            this.tblPositionButtons.Controls.Add(this.btnNewPosition, 0, 1);
            this.tblPositionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPositionButtons.Location = new System.Drawing.Point(3, 16);
            this.tblPositionButtons.Name = "tblPositionButtons";
            this.tblPositionButtons.RowCount = 4;
            this.tblPositionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblPositionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblPositionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tblPositionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tblPositionButtons.Size = new System.Drawing.Size(108, 389);
            this.tblPositionButtons.TabIndex = 0;
            // 
            // grpZones
            // 
            this.grpZones.Controls.Add(this.tblZone);
            this.grpZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpZones.Location = new System.Drawing.Point(3, 417);
            this.grpZones.Name = "grpZones";
            this.grpZones.Size = new System.Drawing.Size(603, 172);
            this.grpZones.TabIndex = 1;
            this.grpZones.TabStop = false;
            // 
            // tblZone
            // 
            this.tblZone.ColumnCount = 3;
            this.tblZone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblZone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblZone.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblZone.Controls.Add(this.lblZones, 0, 0);
            this.tblZone.Controls.Add(this.lstZones, 0, 1);
            this.tblZone.Controls.Add(this.lblName, 1, 0);
            this.tblZone.Controls.Add(this.txtZoneName, 2, 0);
            this.tblZone.Controls.Add(this.btnZoneTimes, 2, 3);
            this.tblZone.Controls.Add(this.lblOpeningHours, 1, 2);
            this.tblZone.Controls.Add(this.lstZoneOpeningHours, 2, 2);
            this.tblZone.Controls.Add(this.chkPromotionEnabled, 2, 1);
            this.tblZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblZone.Location = new System.Drawing.Point(3, 16);
            this.tblZone.Name = "tblZone";
            this.tblZone.RowCount = 4;
            this.tblZone.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblZone.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblZone.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblZone.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblZone.Size = new System.Drawing.Size(597, 153);
            this.tblZone.TabIndex = 0;
            // 
            // lblZones
            // 
            this.lblZones.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblZones.AutoSize = true;
            this.lblZones.Location = new System.Drawing.Point(3, 6);
            this.lblZones.Name = "lblZones";
            this.lblZones.Size = new System.Drawing.Size(43, 13);
            this.lblZones.TabIndex = 0;
            this.lblZones.Text = "Zones :";
            // 
            // lstZones
            // 
            this.lstZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstZones.FormattingEnabled = true;
            this.lstZones.Location = new System.Drawing.Point(3, 28);
            this.lstZones.Name = "lstZones";
            this.tblZone.SetRowSpan(this.lstZones, 3);
            this.lstZones.Size = new System.Drawing.Size(194, 122);
            this.lstZones.TabIndex = 1;
            this.lstZones.SelectedIndexChanged += new System.EventHandler(this.lstZones_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(203, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(48, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "* Name :";
            // 
            // txtZoneName
            // 
            this.txtZoneName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtZoneName.Location = new System.Drawing.Point(303, 3);
            this.txtZoneName.MaxLength = 50;
            this.txtZoneName.Name = "txtZoneName";
            this.txtZoneName.Size = new System.Drawing.Size(241, 20);
            this.txtZoneName.TabIndex = 3;
            // 
            // btnZoneTimes
            // 
            this.btnZoneTimes.Location = new System.Drawing.Point(303, 78);
            this.btnZoneTimes.Name = "btnZoneTimes";
            this.btnZoneTimes.Size = new System.Drawing.Size(100, 28);
            this.btnZoneTimes.TabIndex = 7;
            this.btnZoneTimes.Text = "Zone Times";
            this.btnZoneTimes.UseVisualStyleBackColor = true;
            this.btnZoneTimes.Click += new System.EventHandler(this.btnZoneTimes_Click);
            // 
            // lblOpeningHours
            // 
            this.lblOpeningHours.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOpeningHours.AutoSize = true;
            this.lblOpeningHours.Location = new System.Drawing.Point(203, 56);
            this.lblOpeningHours.Name = "lblOpeningHours";
            this.lblOpeningHours.Size = new System.Drawing.Size(84, 13);
            this.lblOpeningHours.TabIndex = 5;
            this.lblOpeningHours.Text = "Opening Hours :";
            // 
            // lstZoneOpeningHours
            // 
            this.lstZoneOpeningHours.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lstZoneOpeningHours.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstZoneOpeningHours.FormattingEnabled = true;
            this.lstZoneOpeningHours.Location = new System.Drawing.Point(303, 53);
            this.lstZoneOpeningHours.Name = "lstZoneOpeningHours";
            this.lstZoneOpeningHours.Size = new System.Drawing.Size(241, 21);
            this.lstZoneOpeningHours.TabIndex = 6;
            // 
            // chkPromotionEnabled
            // 
            this.chkPromotionEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPromotionEnabled.AutoSize = true;
            this.chkPromotionEnabled.Location = new System.Drawing.Point(303, 29);
            this.chkPromotionEnabled.Name = "chkPromotionEnabled";
            this.chkPromotionEnabled.Size = new System.Drawing.Size(126, 17);
            this.chkPromotionEnabled.TabIndex = 4;
            this.chkPromotionEnabled.Text = "Is Promotion Enabled";
            this.chkPromotionEnabled.UseVisualStyleBackColor = true;
            this.chkPromotionEnabled.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // grpInstallations
            // 
            this.grpInstallations.Controls.Add(this.lvBarPos);
            this.grpInstallations.Controls.Add(this.lblInstallations);
            this.grpInstallations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInstallations.Location = new System.Drawing.Point(3, 3);
            this.grpInstallations.Name = "grpInstallations";
            this.grpInstallations.Size = new System.Drawing.Size(603, 408);
            this.grpInstallations.TabIndex = 0;
            this.grpInstallations.TabStop = false;
            // 
            // lvBarPos
            // 
            this.lvBarPos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPos,
            this.colLocation,
            this.colZone,
            this.colType,
            this.colGameTitle,
            this.colRouteName});
            this.lvBarPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvBarPos.FullRowSelect = true;
            this.lvBarPos.GridLines = true;
            this.lvBarPos.HideSelection = false;
            this.lvBarPos.Location = new System.Drawing.Point(3, 16);
            this.lvBarPos.MultiSelect = false;
            this.lvBarPos.Name = "lvBarPos";
            this.lvBarPos.Size = new System.Drawing.Size(597, 389);
            this.lvBarPos.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvBarPos.TabIndex = 1;
            this.lvBarPos.UseCompatibleStateImageBehavior = false;
            this.lvBarPos.View = System.Windows.Forms.View.Details;
            this.lvBarPos.SelectedIndexChanged += new System.EventHandler(this.lvBarPos_SelectedIndexChanged);
            this.lvBarPos.DoubleClick += new System.EventHandler(this.lvBarPos_DoubleClick);
            // 
            // colPos
            // 
            this.colPos.Text = "Pos";
            // 
            // colLocation
            // 
            this.colLocation.Text = "Location";
            this.colLocation.Width = 120;
            // 
            // colZone
            // 
            this.colZone.Text = "Zone";
            this.colZone.Width = 100;
            // 
            // colType
            // 
            this.colType.Text = "Type";
            this.colType.Width = 80;
            // 
            // colGameTitle
            // 
            this.colGameTitle.Text = "Game Title";
            this.colGameTitle.Width = 140;
            // 
            // colRouteName
            // 
            this.colRouteName.Text = "Route Name";
            this.colRouteName.Width = 100;
            // 
            // lblInstallations
            // 
            this.lblInstallations.AutoSize = true;
            this.lblInstallations.Location = new System.Drawing.Point(1, 0);
            this.lblInstallations.Name = "lblInstallations";
            this.lblInstallations.Size = new System.Drawing.Size(68, 13);
            this.lblInstallations.TabIndex = 0;
            this.lblInstallations.Text = "Installations :";
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 2;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblContainer.Controls.Add(this.grpInstallations, 0, 0);
            this.tblContainer.Controls.Add(this.grpZones, 0, 1);
            this.tblContainer.Controls.Add(this.grpButtons, 1, 0);
            this.tblContainer.Controls.Add(this.grpZoneButtons, 1, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblContainer.Size = new System.Drawing.Size(729, 592);
            this.tblContainer.TabIndex = 0;
            // 
            // grpZoneButtons
            // 
            this.grpZoneButtons.Controls.Add(this.tblZoneButton);
            this.grpZoneButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpZoneButtons.Location = new System.Drawing.Point(612, 417);
            this.grpZoneButtons.Name = "grpZoneButtons";
            this.grpZoneButtons.Size = new System.Drawing.Size(114, 172);
            this.grpZoneButtons.TabIndex = 2;
            this.grpZoneButtons.TabStop = false;
            // 
            // tblZoneButton
            // 
            this.tblZoneButton.ColumnCount = 1;
            this.tblZoneButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblZoneButton.Controls.Add(this.btnNewZone, 0, 0);
            this.tblZoneButton.Controls.Add(this.btnZoneDelete, 0, 2);
            this.tblZoneButton.Controls.Add(this.btnZoneApply, 0, 1);
            this.tblZoneButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblZoneButton.Location = new System.Drawing.Point(3, 16);
            this.tblZoneButton.Name = "tblZoneButton";
            this.tblZoneButton.RowCount = 3;
            this.tblZoneButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblZoneButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblZoneButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblZoneButton.Size = new System.Drawing.Size(108, 153);
            this.tblZoneButton.TabIndex = 0;
            // 
            // btnNewZone
            // 
            this.btnNewZone.Location = new System.Drawing.Point(3, 3);
            this.btnNewZone.Name = "btnNewZone";
            this.btnNewZone.Size = new System.Drawing.Size(100, 28);
            this.btnNewZone.TabIndex = 0;
            this.btnNewZone.Text = "New Zone";
            this.btnNewZone.UseVisualStyleBackColor = true;
            this.btnNewZone.Click += new System.EventHandler(this.btnNewZone_Click);
            // 
            // btnZoneDelete
            // 
            this.btnZoneDelete.Location = new System.Drawing.Point(3, 83);
            this.btnZoneDelete.Name = "btnZoneDelete";
            this.btnZoneDelete.Size = new System.Drawing.Size(100, 27);
            this.btnZoneDelete.TabIndex = 2;
            this.btnZoneDelete.Text = "Delete";
            this.btnZoneDelete.UseVisualStyleBackColor = true;
            this.btnZoneDelete.Click += new System.EventHandler(this.btnZoneDelete_Click);
            // 
            // btnZoneApply
            // 
            this.btnZoneApply.Location = new System.Drawing.Point(3, 43);
            this.btnZoneApply.Name = "btnZoneApply";
            this.btnZoneApply.Size = new System.Drawing.Size(100, 29);
            this.btnZoneApply.TabIndex = 1;
            this.btnZoneApply.Text = "Apply";
            this.btnZoneApply.UseVisualStyleBackColor = true;
            this.btnZoneApply.Click += new System.EventHandler(this.btnZoneApply_Click);
            // 
            // ZonePosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tblContainer);
            this.Name = "ZonePosition";
            this.Size = new System.Drawing.Size(729, 592);
            this.grpButtons.ResumeLayout(false);
            this.tblPositionButtons.ResumeLayout(false);
            this.grpZones.ResumeLayout(false);
            this.tblZone.ResumeLayout(false);
            this.tblZone.PerformLayout();
            this.grpInstallations.ResumeLayout(false);
            this.grpInstallations.PerformLayout();
            this.tblContainer.ResumeLayout(false);
            this.grpZoneButtons.ResumeLayout(false);
            this.tblZoneButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRoutePositions;
        private System.Windows.Forms.Button btnBulkCopyPositions;
        private System.Windows.Forms.Button btnNewPosition;
        private System.Windows.Forms.Button btnEditPosition;
        private System.Windows.Forms.GroupBox grpButtons;
        private System.Windows.Forms.GroupBox grpZones;
        private System.Windows.Forms.Label lblZones;
        private System.Windows.Forms.ListBox lstZones;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnZoneTimes;
        private System.Windows.Forms.TextBox txtZoneName;
        private System.Windows.Forms.ComboBox lstZoneOpeningHours;
        private System.Windows.Forms.Label lblOpeningHours;
        private System.Windows.Forms.GroupBox grpInstallations;
        private System.Windows.Forms.ListView lvBarPos;
        private System.Windows.Forms.ColumnHeader colPos;
        private System.Windows.Forms.ColumnHeader colLocation;
        private System.Windows.Forms.ColumnHeader colZone;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colGameTitle;
        private System.Windows.Forms.Label lblInstallations;
        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.Button btnZoneDelete;
        private System.Windows.Forms.Button btnZoneApply;
        private System.Windows.Forms.Button btnNewZone;
        private System.Windows.Forms.GroupBox grpZoneButtons;
        private System.Windows.Forms.CheckBox chkPromotionEnabled;
        private System.Windows.Forms.ColumnHeader colRouteName;
        private System.Windows.Forms.TableLayoutPanel tblZone;
        private System.Windows.Forms.TableLayoutPanel tblZoneButton;
        private System.Windows.Forms.TableLayoutPanel tblPositionButtons;
    }
}
