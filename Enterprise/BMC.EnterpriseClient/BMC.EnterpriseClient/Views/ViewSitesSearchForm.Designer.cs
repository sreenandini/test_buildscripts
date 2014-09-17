namespace BMC.EnterpriseClient.Views
{
    partial class ViewSitesSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewSitesSearchForm));
            this.tblContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tblContent = new System.Windows.Forms.TableLayoutPanel();
            this.cboManufactuer = new System.Windows.Forms.ComboBox();
            this.cboMCType = new System.Windows.Forms.ComboBox();
            this.cboSiteRep = new System.Windows.Forms.ComboBox();
            this.cboArea = new System.Windows.Forms.ComboBox();
            this.cboDistrict = new System.Windows.Forms.ComboBox();
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.cboDepot = new System.Windows.Forms.ComboBox();
            this.lblSiteSearch = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.lblSubCompany = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.lblDepot = new System.Windows.Forms.Label();
            this.lblMCType = new System.Windows.Forms.Label();
            this.lblSiteRep = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblManufactuer = new System.Windows.Forms.Label();
            this.lblMachineModel = new System.Windows.Forms.Label();
            this.lblPercPayout = new System.Windows.Forms.Label();
            this.cboSiteSearch = new System.Windows.Forms.ComboBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.cboSubCompany = new System.Windows.Forms.ComboBox();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.chkExcludeVacant = new System.Windows.Forms.CheckBox();
            this.txtMachineModel = new System.Windows.Forms.TextBox();
            this.tblPercPayout = new System.Windows.Forms.TableLayoutPanel();
            this.txtPercPayoutFrom = new System.Windows.Forms.TextBox();
            this.lblPercPayoutTo = new System.Windows.Forms.Label();
            this.txtPercPayoutTo = new System.Windows.Forms.TextBox();
            this.tblContainer.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.tblContent.SuspendLayout();
            this.tblPercPayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblContainer
            // 
            this.tblContainer.ColumnCount = 1;
            this.tblContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.Controls.Add(this.tblButtons, 0, 1);
            this.tblContainer.Controls.Add(this.tblContent, 0, 0);
            this.tblContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContainer.Location = new System.Drawing.Point(0, 0);
            this.tblContainer.Name = "tblContainer";
            this.tblContainer.RowCount = 2;
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblContainer.Size = new System.Drawing.Size(336, 427);
            this.tblContainer.TabIndex = 0;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 5;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblButtons.Controls.Add(this.btnReset, 2, 0);
            this.tblButtons.Controls.Add(this.btnSearch, 3, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(0, 387);
            this.tblButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(336, 40);
            this.tblButtons.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.ImageKey = "MovePrev";
            this.btnReset.Location = new System.Drawing.Point(169, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(74, 28);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ImageKey = "MovePrev";
            this.btnSearch.Location = new System.Drawing.Point(249, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 28);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tblContent
            // 
            this.tblContent.ColumnCount = 4;
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tblContent.Controls.Add(this.cboManufactuer, 2, 12);
            this.tblContent.Controls.Add(this.cboMCType, 2, 9);
            this.tblContent.Controls.Add(this.cboSiteRep, 2, 10);
            this.tblContent.Controls.Add(this.cboArea, 2, 5);
            this.tblContent.Controls.Add(this.cboDistrict, 2, 6);
            this.tblContent.Controls.Add(this.cboOperator, 2, 7);
            this.tblContent.Controls.Add(this.cboDepot, 2, 8);
            this.tblContent.Controls.Add(this.lblSiteSearch, 1, 1);
            this.tblContent.Controls.Add(this.lblCompany, 1, 2);
            this.tblContent.Controls.Add(this.lblSubCompany, 1, 3);
            this.tblContent.Controls.Add(this.lblRegion, 1, 4);
            this.tblContent.Controls.Add(this.lblArea, 1, 5);
            this.tblContent.Controls.Add(this.lblDistrict, 1, 6);
            this.tblContent.Controls.Add(this.lblOperator, 1, 7);
            this.tblContent.Controls.Add(this.lblDepot, 1, 8);
            this.tblContent.Controls.Add(this.lblMCType, 1, 9);
            this.tblContent.Controls.Add(this.lblSiteRep, 1, 10);
            this.tblContent.Controls.Add(this.label12, 1, 11);
            this.tblContent.Controls.Add(this.lblManufactuer, 1, 12);
            this.tblContent.Controls.Add(this.lblMachineModel, 1, 13);
            this.tblContent.Controls.Add(this.lblPercPayout, 1, 14);
            this.tblContent.Controls.Add(this.cboSiteSearch, 2, 1);
            this.tblContent.Controls.Add(this.cboCompany, 2, 2);
            this.tblContent.Controls.Add(this.cboSubCompany, 2, 3);
            this.tblContent.Controls.Add(this.cboRegion, 2, 4);
            this.tblContent.Controls.Add(this.chkExcludeVacant, 2, 11);
            this.tblContent.Controls.Add(this.txtMachineModel, 2, 13);
            this.tblContent.Controls.Add(this.tblPercPayout, 2, 14);
            this.tblContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblContent.Location = new System.Drawing.Point(0, 0);
            this.tblContent.Margin = new System.Windows.Forms.Padding(0);
            this.tblContent.Name = "tblContent";
            this.tblContent.RowCount = 17;
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblContent.Size = new System.Drawing.Size(336, 387);
            this.tblContent.TabIndex = 0;
            // 
            // cboManufactuer
            // 
            this.cboManufactuer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboManufactuer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManufactuer.FormattingEnabled = true;
            this.cboManufactuer.Location = new System.Drawing.Point(133, 283);
            this.cboManufactuer.Name = "cboManufactuer";
            this.cboManufactuer.Size = new System.Drawing.Size(190, 21);
            this.cboManufactuer.TabIndex = 25;
            // 
            // cboMCType
            // 
            this.cboMCType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMCType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMCType.FormattingEnabled = true;
            this.cboMCType.Location = new System.Drawing.Point(133, 208);
            this.cboMCType.Name = "cboMCType";
            this.cboMCType.Size = new System.Drawing.Size(190, 21);
            this.cboMCType.TabIndex = 17;
            // 
            // cboSiteRep
            // 
            this.cboSiteRep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiteRep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSiteRep.FormattingEnabled = true;
            this.cboSiteRep.Location = new System.Drawing.Point(133, 233);
            this.cboSiteRep.Name = "cboSiteRep";
            this.cboSiteRep.Size = new System.Drawing.Size(190, 21);
            this.cboSiteRep.TabIndex = 19;
            // 
            // cboArea
            // 
            this.cboArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboArea.FormattingEnabled = true;
            this.cboArea.Location = new System.Drawing.Point(133, 108);
            this.cboArea.Name = "cboArea";
            this.cboArea.Size = new System.Drawing.Size(190, 21);
            this.cboArea.TabIndex = 9;
            this.cboArea.SelectedIndexChanged += new System.EventHandler(this.cboArea_SelectedIndexChanged);
            // 
            // cboDistrict
            // 
            this.cboDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDistrict.FormattingEnabled = true;
            this.cboDistrict.Location = new System.Drawing.Point(133, 133);
            this.cboDistrict.Name = "cboDistrict";
            this.cboDistrict.Size = new System.Drawing.Size(190, 21);
            this.cboDistrict.TabIndex = 11;
            // 
            // cboOperator
            // 
            this.cboOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperator.FormattingEnabled = true;
            this.cboOperator.Location = new System.Drawing.Point(133, 158);
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Size = new System.Drawing.Size(190, 21);
            this.cboOperator.TabIndex = 13;
            this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.cboOperator_SelectedIndexChanged);
            // 
            // cboDepot
            // 
            this.cboDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepot.FormattingEnabled = true;
            this.cboDepot.Location = new System.Drawing.Point(133, 183);
            this.cboDepot.Name = "cboDepot";
            this.cboDepot.Size = new System.Drawing.Size(190, 21);
            this.cboDepot.TabIndex = 15;
            // 
            // lblSiteSearch
            // 
            this.lblSiteSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSiteSearch.AutoSize = true;
            this.lblSiteSearch.Location = new System.Drawing.Point(13, 11);
            this.lblSiteSearch.Name = "lblSiteSearch";
            this.lblSiteSearch.Size = new System.Drawing.Size(68, 13);
            this.lblSiteSearch.TabIndex = 0;
            this.lblSiteSearch.Text = "Site Search :";
            // 
            // lblCompany
            // 
            this.lblCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(13, 36);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(57, 13);
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text = "Company :";
            // 
            // lblSubCompany
            // 
            this.lblSubCompany.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubCompany.AutoSize = true;
            this.lblSubCompany.Location = new System.Drawing.Point(13, 61);
            this.lblSubCompany.Name = "lblSubCompany";
            this.lblSubCompany.Size = new System.Drawing.Size(79, 13);
            this.lblSubCompany.TabIndex = 4;
            this.lblSubCompany.Text = "Sub Company :";
            // 
            // lblRegion
            // 
            this.lblRegion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(13, 86);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(47, 13);
            this.lblRegion.TabIndex = 6;
            this.lblRegion.Text = "Region :";
            // 
            // lblArea
            // 
            this.lblArea.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(13, 111);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(35, 13);
            this.lblArea.TabIndex = 8;
            this.lblArea.Text = "Area :";
            // 
            // lblDistrict
            // 
            this.lblDistrict.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.Location = new System.Drawing.Point(13, 136);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(45, 13);
            this.lblDistrict.TabIndex = 10;
            this.lblDistrict.Text = "District :";
            // 
            // lblOperator
            // 
            this.lblOperator.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(13, 161);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(54, 13);
            this.lblOperator.TabIndex = 12;
            this.lblOperator.Text = "Operator :";
            // 
            // lblDepot
            // 
            this.lblDepot.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDepot.AutoSize = true;
            this.lblDepot.Location = new System.Drawing.Point(13, 186);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(42, 13);
            this.lblDepot.TabIndex = 14;
            this.lblDepot.Text = "Depot :";
            // 
            // lblMCType
            // 
            this.lblMCType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMCType.AutoSize = true;
            this.lblMCType.Location = new System.Drawing.Point(13, 211);
            this.lblMCType.Name = "lblMCType";
            this.lblMCType.Size = new System.Drawing.Size(61, 13);
            this.lblMCType.TabIndex = 16;
            this.lblMCType.Text = "M/C Type :";
            // 
            // lblSiteRep
            // 
            this.lblSiteRep.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSiteRep.AutoSize = true;
            this.lblSiteRep.Location = new System.Drawing.Point(13, 236);
            this.lblSiteRep.Name = "lblSiteRep";
            this.lblSiteRep.Size = new System.Drawing.Size(54, 13);
            this.lblSiteRep.TabIndex = 18;
            this.lblSiteRep.Text = "Site Rep :";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 261);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Exclude Vacant Sites?";
            // 
            // lblManufactuer
            // 
            this.lblManufactuer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblManufactuer.AutoSize = true;
            this.lblManufactuer.Location = new System.Drawing.Point(13, 286);
            this.lblManufactuer.Name = "lblManufactuer";
            this.lblManufactuer.Size = new System.Drawing.Size(73, 13);
            this.lblManufactuer.TabIndex = 24;
            this.lblManufactuer.Text = "Manufactuer :";
            // 
            // lblMachineModel
            // 
            this.lblMachineModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMachineModel.AutoSize = true;
            this.lblMachineModel.Location = new System.Drawing.Point(13, 311);
            this.lblMachineModel.Name = "lblMachineModel";
            this.lblMachineModel.Size = new System.Drawing.Size(86, 13);
            this.lblMachineModel.TabIndex = 26;
            this.lblMachineModel.Text = "Machine Model :";
            // 
            // lblPercPayout
            // 
            this.lblPercPayout.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPercPayout.AutoSize = true;
            this.lblPercPayout.Location = new System.Drawing.Point(13, 336);
            this.lblPercPayout.Name = "lblPercPayout";
            this.lblPercPayout.Size = new System.Drawing.Size(57, 13);
            this.lblPercPayout.TabIndex = 28;
            this.lblPercPayout.Text = "% Payout :";
            // 
            // cboSiteSearch
            // 
            this.cboSiteSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSiteSearch.FormattingEnabled = true;
            this.cboSiteSearch.Location = new System.Drawing.Point(133, 8);
            this.cboSiteSearch.Name = "cboSiteSearch";
            this.cboSiteSearch.Size = new System.Drawing.Size(190, 21);
            this.cboSiteSearch.TabIndex = 1;
            this.cboSiteSearch.SelectedIndexChanged += new System.EventHandler(this.cboSiteSearch_SelectedIndexChanged);
            this.cboSiteSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboSiteSearch_KeyUp);
            // 
            // cboCompany
            // 
            this.cboCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(133, 33);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(190, 21);
            this.cboCompany.TabIndex = 3;
            this.cboCompany.SelectedIndexChanged += new System.EventHandler(this.cboCompany_SelectedIndexChanged);
            // 
            // cboSubCompany
            // 
            this.cboSubCompany.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSubCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubCompany.FormattingEnabled = true;
            this.cboSubCompany.Location = new System.Drawing.Point(133, 58);
            this.cboSubCompany.Name = "cboSubCompany";
            this.cboSubCompany.Size = new System.Drawing.Size(190, 21);
            this.cboSubCompany.TabIndex = 5;
            this.cboSubCompany.SelectedIndexChanged += new System.EventHandler(this.cboSubCompany_SelectedIndexChanged);
            // 
            // cboRegion
            // 
            this.cboRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(133, 83);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(190, 21);
            this.cboRegion.TabIndex = 7;
            this.cboRegion.SelectedIndexChanged += new System.EventHandler(this.cboRegion_SelectedIndexChanged);
            // 
            // chkExcludeVacant
            // 
            this.chkExcludeVacant.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkExcludeVacant.AutoSize = true;
            this.chkExcludeVacant.Location = new System.Drawing.Point(308, 260);
            this.chkExcludeVacant.Name = "chkExcludeVacant";
            this.chkExcludeVacant.Size = new System.Drawing.Size(15, 14);
            this.chkExcludeVacant.TabIndex = 23;
            this.chkExcludeVacant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkExcludeVacant.UseVisualStyleBackColor = true;
            // 
            // txtMachineModel
            // 
            this.txtMachineModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineModel.Location = new System.Drawing.Point(133, 308);
            this.txtMachineModel.Name = "txtMachineModel";
            this.txtMachineModel.Size = new System.Drawing.Size(190, 20);
            this.txtMachineModel.TabIndex = 27;
            // 
            // tblPercPayout
            // 
            this.tblPercPayout.ColumnCount = 3;
            this.tblPercPayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPercPayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblPercPayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblPercPayout.Controls.Add(this.txtPercPayoutFrom, 0, 0);
            this.tblPercPayout.Controls.Add(this.lblPercPayoutTo, 1, 0);
            this.tblPercPayout.Controls.Add(this.txtPercPayoutTo, 2, 0);
            this.tblPercPayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPercPayout.Location = new System.Drawing.Point(130, 330);
            this.tblPercPayout.Margin = new System.Windows.Forms.Padding(0);
            this.tblPercPayout.Name = "tblPercPayout";
            this.tblPercPayout.RowCount = 1;
            this.tblPercPayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPercPayout.Size = new System.Drawing.Size(196, 25);
            this.tblPercPayout.TabIndex = 29;
            // 
            // txtPercPayoutFrom
            // 
            this.txtPercPayoutFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPercPayoutFrom.Location = new System.Drawing.Point(3, 3);
            this.txtPercPayoutFrom.Name = "txtPercPayoutFrom";
            this.txtPercPayoutFrom.Size = new System.Drawing.Size(72, 20);
            this.txtPercPayoutFrom.TabIndex = 0;
            // 
            // lblPercPayoutTo
            // 
            this.lblPercPayoutTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPercPayoutTo.AutoSize = true;
            this.lblPercPayoutTo.Location = new System.Drawing.Point(81, 6);
            this.lblPercPayoutTo.Name = "lblPercPayoutTo";
            this.lblPercPayoutTo.Size = new System.Drawing.Size(26, 13);
            this.lblPercPayoutTo.TabIndex = 1;
            this.lblPercPayoutTo.Text = "To :";
            // 
            // txtPercPayoutTo
            // 
            this.txtPercPayoutTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPercPayoutTo.Location = new System.Drawing.Point(121, 3);
            this.txtPercPayoutTo.Name = "txtPercPayoutTo";
            this.txtPercPayoutTo.Size = new System.Drawing.Size(72, 20);
            this.txtPercPayoutTo.TabIndex = 2;
            // 
            // ViewSitesSearchForm
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 427);
            this.Controls.Add(this.tblContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewSitesSearchForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewSitesSearchForm_FormClosing);
            this.Load += new System.EventHandler(this.ViewSitesSearchForm_Load);
            this.tblContainer.ResumeLayout(false);
            this.tblButtons.ResumeLayout(false);
            this.tblContent.ResumeLayout(false);
            this.tblContent.PerformLayout();
            this.tblPercPayout.ResumeLayout(false);
            this.tblPercPayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblContainer;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tblContent;
        private System.Windows.Forms.ComboBox cboManufactuer;
        private System.Windows.Forms.ComboBox cboMCType;
        private System.Windows.Forms.ComboBox cboSiteRep;
        private System.Windows.Forms.ComboBox cboArea;
        private System.Windows.Forms.ComboBox cboDistrict;
        private System.Windows.Forms.ComboBox cboOperator;
        private System.Windows.Forms.ComboBox cboDepot;
        private System.Windows.Forms.Label lblSiteSearch;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblSubCompany;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.Label lblDepot;
        private System.Windows.Forms.Label lblMCType;
        private System.Windows.Forms.Label lblSiteRep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblManufactuer;
        private System.Windows.Forms.Label lblMachineModel;
        private System.Windows.Forms.Label lblPercPayout;
        private System.Windows.Forms.ComboBox cboSiteSearch;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.ComboBox cboSubCompany;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.CheckBox chkExcludeVacant;
        private System.Windows.Forms.TextBox txtMachineModel;
        private System.Windows.Forms.TableLayoutPanel tblPercPayout;
        private System.Windows.Forms.TextBox txtPercPayoutFrom;
        private System.Windows.Forms.Label lblPercPayoutTo;
        private System.Windows.Forms.TextBox txtPercPayoutTo;
    }
}