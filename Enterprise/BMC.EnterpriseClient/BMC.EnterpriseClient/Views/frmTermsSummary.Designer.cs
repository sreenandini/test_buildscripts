namespace BMC.EnterpriseClient.Views
{
    partial class frmTermsSummary
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lvTermsSummary = new BMC.CoreLib.Win32.ListViewEx();
            this.clmSiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMachine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSupPos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSiteCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSupSite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTerms = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmRentShares = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmShortfall = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmRent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLicence = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSupplierPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmLocationPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCompanyPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmImport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmPosType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmPop = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmJackpot = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDisplayVacantPositions = new System.Windows.Forms.CheckBox();
            this.lblSubCompany = new System.Windows.Forms.Label();
            this.cmbSubCompanies = new System.Windows.Forms.ComboBox();
            this.lblOperator = new System.Windows.Forms.Label();
            this.cmbOperators = new System.Windows.Forms.ComboBox();
            this.lblMachineType = new System.Windows.Forms.Label();
            this.cmbMachineTypes = new System.Windows.Forms.ComboBox();
            this.lblDepot = new System.Windows.Forms.Label();
            this.cmbDepot = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1168, 684);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExport, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnView, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 644);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1168, 40);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1051, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(114, 28);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(931, 6);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(114, 28);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(811, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(114, 28);
            this.btnView.TabIndex = 12;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lvTermsSummary, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.grpOptions, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.96639F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.03362F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1162, 638);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lvTermsSummary
            // 
            this.lvTermsSummary.ClipboardCopyFormat = BMC.CoreLib.Win32.ListViewClipboardCopyFormat.Default;
            this.lvTermsSummary.ClipboardCopyMode = BMC.CoreLib.Win32.ListViewClipboardCopyMode.Disable;
            this.lvTermsSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmSiteName,
            this.clmPosition,
            this.clmMachine,
            this.clmSupPos,
            this.clmSiteCode,
            this.clmSupSite,
            this.clmTerms,
            this.clmRentShares,
            this.clmShortfall,
            this.clmRent,
            this.clmLicence,
            this.clmSupplierPercent,
            this.clmLocationPercent,
            this.clmCompanyPercent,
            this.clmImport,
            this.clmPosType,
            this.clmPop,
            this.clmJackpot});
            this.lvTermsSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTermsSummary.FullRowSelect = true;
            this.lvTermsSummary.GridLines = true;
            this.lvTermsSummary.HideSelection = false;
            this.lvTermsSummary.Location = new System.Drawing.Point(3, 104);
            this.lvTermsSummary.Name = "lvTermsSummary";
            this.lvTermsSummary.Size = new System.Drawing.Size(1156, 531);
            this.lvTermsSummary.TabIndex = 2;
            this.lvTermsSummary.UseCompatibleStateImageBehavior = false;
            this.lvTermsSummary.View = System.Windows.Forms.View.Details;
            this.lvTermsSummary.DoubleClick += new System.EventHandler(this.lvTermsSummary_DoubleClick);
            // 
            // clmSiteName
            // 
            this.clmSiteName.Text = "Site";
            // 
            // clmPosition
            // 
            this.clmPosition.Text = "Pos";
            // 
            // clmMachine
            // 
            this.clmMachine.Text = "Machine";
            // 
            // clmSupPos
            // 
            this.clmSupPos.Text = "Sup Pos";
            // 
            // clmSiteCode
            // 
            this.clmSiteCode.Text = "Site Code";
            // 
            // clmSupSite
            // 
            this.clmSupSite.Text = "Sup Site";
            // 
            // clmTerms
            // 
            this.clmTerms.Text = "Terms";
            // 
            // clmRentShares
            // 
            this.clmRentShares.Text = "Rent/Shares";
            // 
            // clmShortfall
            // 
            this.clmShortfall.Text = "Shortfall";
            // 
            // clmRent
            // 
            this.clmRent.Text = "Rent";
            // 
            // clmLicence
            // 
            this.clmLicence.Text = "Licence";
            // 
            // clmSupplierPercent
            // 
            this.clmSupplierPercent.Text = "Supplier %";
            // 
            // clmLocationPercent
            // 
            this.clmLocationPercent.Text = "Location %";
            // 
            // clmCompanyPercent
            // 
            this.clmCompanyPercent.Text = "Company %";
            // 
            // clmImport
            // 
            this.clmImport.Text = "Import";
            // 
            // clmPosType
            // 
            this.clmPosType.Text = "Pos Type";
            // 
            // clmPop
            // 
            this.clmPop.Text = "Pop";
            // 
            // clmJackpot
            // 
            this.clmJackpot.Text = "Jackpot";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.tableLayoutPanel4);
            this.grpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOptions.Location = new System.Drawing.Point(3, 3);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(1156, 95);
            this.grpOptions.TabIndex = 0;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 8;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.chkDisplayVacantPositions, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lblSubCompany, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbSubCompanies, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblOperator, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbOperators, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMachineType, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmbMachineTypes, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblDepot, 2, 1);
            this.tableLayoutPanel4.Controls.Add(this.cmbDepot, 3, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.74627F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.25373F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1150, 76);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // chkDisplayVacantPositions
            // 
            this.chkDisplayVacantPositions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDisplayVacantPositions.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.chkDisplayVacantPositions, 2);
            this.chkDisplayVacantPositions.Location = new System.Drawing.Point(3, 48);
            this.chkDisplayVacantPositions.Name = "chkDisplayVacantPositions";
            this.chkDisplayVacantPositions.Size = new System.Drawing.Size(294, 17);
            this.chkDisplayVacantPositions.TabIndex = 10;
            this.chkDisplayVacantPositions.Text = "Display Vacant Positions";
            this.chkDisplayVacantPositions.UseVisualStyleBackColor = true;
            // 
            // lblSubCompany
            // 
            this.lblSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubCompany.AutoSize = true;
            this.lblSubCompany.Location = new System.Drawing.Point(3, 12);
            this.lblSubCompany.Name = "lblSubCompany";
            this.lblSubCompany.Size = new System.Drawing.Size(144, 13);
            this.lblSubCompany.TabIndex = 0;
            this.lblSubCompany.Text = "Sub Company";
            // 
            // cmbSubCompanies
            // 
            this.cmbSubCompanies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSubCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubCompanies.FormattingEnabled = true;
            this.cmbSubCompanies.Location = new System.Drawing.Point(153, 8);
            this.cmbSubCompanies.Name = "cmbSubCompanies";
            this.cmbSubCompanies.Size = new System.Drawing.Size(144, 21);
            this.cmbSubCompanies.TabIndex = 1;
            // 
            // lblOperator
            // 
            this.lblOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(303, 12);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(144, 13);
            this.lblOperator.TabIndex = 2;
            this.lblOperator.Text = "Operator";
            // 
            // cmbOperators
            // 
            this.cmbOperators.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbOperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperators.FormattingEnabled = true;
            this.cmbOperators.Location = new System.Drawing.Point(453, 8);
            this.cmbOperators.Name = "cmbOperators";
            this.cmbOperators.Size = new System.Drawing.Size(144, 21);
            this.cmbOperators.TabIndex = 3;
            this.cmbOperators.SelectedIndexChanged += new System.EventHandler(this.cmbOperators_SelectedIndexChanged);
            // 
            // lblMachineType
            // 
            this.lblMachineType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMachineType.AutoSize = true;
            this.lblMachineType.Location = new System.Drawing.Point(603, 12);
            this.lblMachineType.Name = "lblMachineType";
            this.lblMachineType.Size = new System.Drawing.Size(144, 13);
            this.lblMachineType.TabIndex = 4;
            this.lblMachineType.Text = "Machine Type";
            // 
            // cmbMachineTypes
            // 
            this.cmbMachineTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMachineTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineTypes.FormattingEnabled = true;
            this.cmbMachineTypes.Location = new System.Drawing.Point(753, 8);
            this.cmbMachineTypes.Name = "cmbMachineTypes";
            this.cmbMachineTypes.Size = new System.Drawing.Size(144, 21);
            this.cmbMachineTypes.TabIndex = 5;
            // 
            // lblDepot
            // 
            this.lblDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepot.AutoSize = true;
            this.lblDepot.Location = new System.Drawing.Point(303, 50);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(144, 13);
            this.lblDepot.TabIndex = 6;
            this.lblDepot.Text = "Depot";
            // 
            // cmbDepot
            // 
            this.cmbDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(453, 46);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(144, 21);
            this.cmbDepot.TabIndex = 7;
            // 
            // frmTermsSummary
            // 
            this.ClientSize = new System.Drawing.Size(1168, 684);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTermsSummary";
            this.Text = "Terms Summary";
            this.Load += new System.EventHandler(this.frmTermsSummary_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.grpOptions.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblSubCompany;
        private System.Windows.Forms.ComboBox cmbSubCompanies;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cmbOperators;
        private System.Windows.Forms.Label lblMachineType;
        private System.Windows.Forms.ComboBox cmbMachineTypes;
        private System.Windows.Forms.Label lblDepot;
        private System.Windows.Forms.ComboBox cmbDepot;
        private System.Windows.Forms.CheckBox chkDisplayVacantPositions;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnView;
        private CoreLib.Win32.ListViewEx lvTermsSummary;
        private System.Windows.Forms.ColumnHeader clmSiteName;
        private System.Windows.Forms.ColumnHeader clmPosition;
        private System.Windows.Forms.ColumnHeader clmMachine;
        private System.Windows.Forms.ColumnHeader clmSupPos;
        private System.Windows.Forms.ColumnHeader clmSiteCode;
        private System.Windows.Forms.ColumnHeader clmSupSite;
        private System.Windows.Forms.ColumnHeader clmTerms;
        private System.Windows.Forms.ColumnHeader clmRentShares;
        private System.Windows.Forms.ColumnHeader clmShortfall;
        private System.Windows.Forms.ColumnHeader clmRent;
        private System.Windows.Forms.ColumnHeader clmLicence;
        private System.Windows.Forms.ColumnHeader clmSupplierPercent;
        private System.Windows.Forms.ColumnHeader clmLocationPercent;
        private System.Windows.Forms.ColumnHeader clmCompanyPercent;
        private System.Windows.Forms.ColumnHeader clmImport;
        private System.Windows.Forms.ColumnHeader clmPosType;
        private System.Windows.Forms.ColumnHeader clmPop;
        private System.Windows.Forms.ColumnHeader clmJackpot;

    }
}