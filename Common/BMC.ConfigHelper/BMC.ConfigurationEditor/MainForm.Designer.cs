namespace BMC.ConfigurationEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.imglstSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlOthers = new System.Windows.Forms.Panel();
            this.tblTop = new System.Windows.Forms.TableLayoutPanel();
            this.lblInstallationTypes = new System.Windows.Forms.Label();
            this.cboInstallationTypes = new System.Windows.Forms.ComboBox();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.tvwSections = new System.Windows.Forms.TreeView();
            this.pgrdKeyValues = new System.Windows.Forms.PropertyGrid();
            this.tblContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblTop.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblContainer.Controls.Add(this.tblButtons, 0, 2);
            this.tblContainer.Controls.Add(this.tblTop, 0, 0);
            this.tblContainer.Controls.Add(this.tblContent, 0, 1);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 3;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(915, 562);
            this.tblContainer.TabIndex = 0;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 4;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tblButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblButtons.Controls.Add(this.btnSave, 1, 0);
            this.tblButtons.Controls.Add(this.pnlOthers, 0, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(3, 525);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 2;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tblButtons.Size = new System.Drawing.Size(909, 34);
            this.tblButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClose.ImageKey = "Cancel.ico";
            this.btnClose.ImageList = this.imglstSmallIcons;
            this.btnClose.Location = new System.Drawing.Point(792, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(111, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "C&lose";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // imglstSmallIcons
            // 
            this.imglstSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstSmallIcons.ImageStream")));
            this.imglstSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstSmallIcons.Images.SetKeyName(0, "Save.ico");
            this.imglstSmallIcons.Images.SetKeyName(1, "Cancel.ico");
            this.imglstSmallIcons.Images.SetKeyName(2, "Folder2.ico");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSave.ImageKey = "Save.ico";
            this.btnSave.ImageList = this.imglstSmallIcons;
            this.btnSave.Location = new System.Drawing.Point(672, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 28);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Visible = false;
            // 
            // pnlOthers
            // 
            this.pnlOthers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOthers.Location = new System.Drawing.Point(0, 0);
            this.pnlOthers.Margin = new System.Windows.Forms.Padding(0);
            this.pnlOthers.Name = "pnlOthers";
            this.pnlOthers.Size = new System.Drawing.Size(669, 34);
            this.pnlOthers.TabIndex = 0;
            // 
            // tblTop
            // 
            this.tblTop.ColumnCount = 2;
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tblTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTop.Controls.Add(this.lblInstallationTypes, 0, 0);
            this.tblTop.Controls.Add(this.cboInstallationTypes, 1, 0);
            this.tblTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTop.Location = new System.Drawing.Point(0, 0);
            this.tblTop.Margin = new System.Windows.Forms.Padding(0);
            this.tblTop.Name = "tblTop";
            this.tblTop.RowCount = 1;
            this.tblTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblTop.Size = new System.Drawing.Size(915, 40);
            this.tblTop.TabIndex = 0;
            // 
            // lblInstallationTypes
            // 
            this.lblInstallationTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInstallationTypes.AutoSize = true;
            this.lblInstallationTypes.Location = new System.Drawing.Point(3, 13);
            this.lblInstallationTypes.Name = "lblInstallationTypes";
            this.lblInstallationTypes.Size = new System.Drawing.Size(117, 13);
            this.lblInstallationTypes.TabIndex = 0;
            this.lblInstallationTypes.Text = "&Installation Types :";
            // 
            // cboInstallationTypes
            // 
            this.cboInstallationTypes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboInstallationTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstallationTypes.FormattingEnabled = true;
            this.cboInstallationTypes.Location = new System.Drawing.Point(203, 9);
            this.cboInstallationTypes.Name = "cboInstallationTypes";
            this.cboInstallationTypes.Size = new System.Drawing.Size(349, 21);
            this.cboInstallationTypes.TabIndex = 1;
            this.cboInstallationTypes.SelectedIndexChanged += new System.EventHandler(this.cboInstallationTypes_SelectedIndexChanged);
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 2;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblContent.Controls.Add(this.tvwSections, 0, 0);
            this.tblContent.Controls.Add(this.pgrdKeyValues, 1, 0);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(3, 43);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 1;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.Size = new System.Drawing.Size(909, 476);
            this.tblContent.TabIndex = 1;
            // 
            // tvwSections
            // 
            this.tvwSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwSections.HideSelection = false;
            this.tvwSections.ImageIndex = 2;
            this.tvwSections.ImageList = this.imglstSmallIcons;
            this.tvwSections.Location = new System.Drawing.Point(3, 3);
            this.tvwSections.Name = "tvwSections";
            this.tvwSections.SelectedImageIndex = 0;
            this.tvwSections.Size = new System.Drawing.Size(266, 470);
            this.tvwSections.TabIndex = 0;
            this.tvwSections.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwSections_AfterSelect);
            // 
            // pgrdKeyValues
            // 
            this.pgrdKeyValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgrdKeyValues.HelpVisible = false;
            this.pgrdKeyValues.LineColor = System.Drawing.Color.LightGray;
            this.pgrdKeyValues.Location = new System.Drawing.Point(275, 3);
            this.pgrdKeyValues.Name = "pgrdKeyValues";
            this.pgrdKeyValues.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgrdKeyValues.Size = new System.Drawing.Size(631, 470);
            this.pgrdKeyValues.TabIndex = 1;
            this.pgrdKeyValues.ToolbarVisible = false;
            this.pgrdKeyValues.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgrdKeyValues_PropertyValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 562);
            this.Controls.Add(this.tblContainer);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(702, 560);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BMC Configuration Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblTop.ResumeLayout(false);
            this.tblTop.PerformLayout();
            this.tblContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblTop;
        private System.Windows.Forms.Label lblInstallationTypes;
        private System.Windows.Forms.ComboBox cboInstallationTypes;
        public System.Windows.Forms.TableLayoutPanel tblButtons;
        public System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imglstSmallIcons;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Panel pnlOthers;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.TreeView tvwSections;
        private System.Windows.Forms.PropertyGrid pgrdKeyValues;
    }
}

