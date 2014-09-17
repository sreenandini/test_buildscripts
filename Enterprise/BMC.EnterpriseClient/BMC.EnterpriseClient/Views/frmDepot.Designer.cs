namespace BMC.EnterpriseClient.Views
{
    partial class frmDepot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDepot));
            this.tblDepot = new System.Windows.Forms.TableLayoutPanel();
            this.tblDepotContents = new System.Windows.Forms.TableLayoutPanel();
            this.grb_SiteRep = new System.Windows.Forms.GroupBox();
            this.lvwSiteReps = new System.Windows.Forms.ListView();
            this.clmReps = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpbServiceArea = new System.Windows.Forms.GroupBox();
            this.tblServiceArea = new System.Windows.Forms.TableLayoutPanel();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblServiceDescription = new System.Windows.Forms.Label();
            this.cmbServiceArea = new System.Windows.Forms.ComboBox();
            this.lblServiceArea = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.grpDepot = new System.Windows.Forms.GroupBox();
            this.tblDepotDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblOperator = new System.Windows.Forms.Label();
            this.cmbSupplier = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmbDepot = new System.Windows.Forms.ComboBox();
            this.lblDepot = new System.Windows.Forms.Label();
            this.lblDepotName = new System.Windows.Forms.Label();
            this.lblCadastreCode = new System.Windows.Forms.Label();
            this.txtDepotName = new System.Windows.Forms.TextBox();
            this.txtCadastralCode = new System.Windows.Forms.TextBox();
            this.lblDepotAddress = new System.Windows.Forms.Label();
            this.lblDepotPostCode = new System.Windows.Forms.Label();
            this.tblDepotAddress = new System.Windows.Forms.TableLayoutPanel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.tblPostCode = new System.Windows.Forms.TableLayoutPanel();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblStreetNo = new System.Windows.Forms.Label();
            this.lblDepotPhoneNo = new System.Windows.Forms.Label();
            this.txtStreetNo = new System.Windows.Forms.TextBox();
            this.txtPhoneNo = new System.Windows.Forms.TextBox();
            this.lblProvince = new System.Windows.Forms.Label();
            this.lblContactName = new System.Windows.Forms.Label();
            this.txtProvince = new System.Windows.Forms.TextBox();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.lblMunicipality = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.txtMuncipality = new System.Windows.Forms.TextBox();
            this.txtDepotReference = new System.Windows.Forms.TextBox();
            this.chkServiceDepot = new System.Windows.Forms.CheckBox();
            this.tblControls = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tblDepot.SuspendLayout();
            this.tblDepotContents.SuspendLayout();
            this.grb_SiteRep.SuspendLayout();
            this.gpbServiceArea.SuspendLayout();
            this.tblServiceArea.SuspendLayout();
            this.grpDepot.SuspendLayout();
            this.tblDepotDetails.SuspendLayout();
            this.tblDepotAddress.SuspendLayout();
            this.tblPostCode.SuspendLayout();
            this.tblControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblDepot
            // 
            this.tblDepot.ColumnCount = 1;
            this.tblDepot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tblDepot.Controls.Add(this.tblDepotContents, 0, 0);
            this.tblDepot.Controls.Add(this.tblControls, 0, 1);
            this.tblDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDepot.Location = new System.Drawing.Point(0, 0);
            this.tblDepot.Name = "tblDepot";
            this.tblDepot.RowCount = 2;
            this.tblDepot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblDepot.Size = new System.Drawing.Size(1021, 564);
            this.tblDepot.TabIndex = 0;
            // 
            // tblDepotContents
            // 
            this.tblDepotContents.ColumnCount = 3;
            this.tblDepotContents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDepotContents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tblDepotContents.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDepotContents.Controls.Add(this.grb_SiteRep, 2, 0);
            this.tblDepotContents.Controls.Add(this.gpbServiceArea, 1, 0);
            this.tblDepotContents.Controls.Add(this.grpDepot, 0, 0);
            this.tblDepotContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDepotContents.Location = new System.Drawing.Point(3, 3);
            this.tblDepotContents.Name = "tblDepotContents";
            this.tblDepotContents.RowCount = 1;
            this.tblDepotContents.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepotContents.Size = new System.Drawing.Size(1015, 518);
            this.tblDepotContents.TabIndex = 0;
            // 
            // grb_SiteRep
            // 
            this.grb_SiteRep.AutoSize = true;
            this.grb_SiteRep.Controls.Add(this.lvwSiteReps);
            this.grb_SiteRep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_SiteRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_SiteRep.Location = new System.Drawing.Point(685, 3);
            this.grb_SiteRep.Name = "grb_SiteRep";
            this.grb_SiteRep.Size = new System.Drawing.Size(327, 512);
            this.grb_SiteRep.TabIndex = 3;
            this.grb_SiteRep.TabStop = false;
            // 
            // lvwSiteReps
            // 
            this.lvwSiteReps.CheckBoxes = true;
            this.lvwSiteReps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmReps});
            this.lvwSiteReps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSiteReps.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwSiteReps.FullRowSelect = true;
            this.lvwSiteReps.Location = new System.Drawing.Point(3, 17);
            this.lvwSiteReps.MultiSelect = false;
            this.lvwSiteReps.Name = "lvwSiteReps";
            this.lvwSiteReps.Size = new System.Drawing.Size(321, 492);
            this.lvwSiteReps.TabIndex = 0;
            this.lvwSiteReps.UseCompatibleStateImageBehavior = false;
            this.lvwSiteReps.View = System.Windows.Forms.View.Details;
            // 
            // clmReps
            // 
            this.clmReps.Text = "Site Representatives";
            this.clmReps.Width = 200;
            // 
            // gpbServiceArea
            // 
            this.gpbServiceArea.Controls.Add(this.tblServiceArea);
            this.gpbServiceArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpbServiceArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbServiceArea.Location = new System.Drawing.Point(335, 3);
            this.gpbServiceArea.Name = "gpbServiceArea";
            this.gpbServiceArea.Size = new System.Drawing.Size(344, 512);
            this.gpbServiceArea.TabIndex = 2;
            this.gpbServiceArea.TabStop = false;
            this.gpbServiceArea.Text = "ServiceArea";
            // 
            // tblServiceArea
            // 
            this.tblServiceArea.ColumnCount = 1;
            this.tblServiceArea.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblServiceArea.Controls.Add(this.txtNotes, 0, 5);
            this.tblServiceArea.Controls.Add(this.lblServiceDescription, 0, 2);
            this.tblServiceArea.Controls.Add(this.cmbServiceArea, 0, 1);
            this.tblServiceArea.Controls.Add(this.lblServiceArea, 0, 0);
            this.tblServiceArea.Controls.Add(this.txtDescription, 0, 3);
            this.tblServiceArea.Controls.Add(this.lblNotes, 0, 4);
            this.tblServiceArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblServiceArea.Location = new System.Drawing.Point(3, 17);
            this.tblServiceArea.Name = "tblServiceArea";
            this.tblServiceArea.RowCount = 6;
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblServiceArea.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tblServiceArea.Size = new System.Drawing.Size(338, 492);
            this.tblServiceArea.TabIndex = 0;
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(8, 273);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(327, 216);
            this.txtNotes.TabIndex = 5;
            // 
            // lblServiceDescription
            // 
            this.lblServiceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServiceDescription.AutoSize = true;
            this.lblServiceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceDescription.Location = new System.Drawing.Point(3, 67);
            this.lblServiceDescription.Name = "lblServiceDescription";
            this.lblServiceDescription.Size = new System.Drawing.Size(332, 15);
            this.lblServiceDescription.TabIndex = 2;
            this.lblServiceDescription.Text = "Description :";
            // 
            // cmbServiceArea
            // 
            this.cmbServiceArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbServiceArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceArea.Location = new System.Drawing.Point(3, 34);
            this.cmbServiceArea.MaxLength = 50;
            this.cmbServiceArea.Name = "cmbServiceArea";
            this.cmbServiceArea.Size = new System.Drawing.Size(332, 21);
            this.cmbServiceArea.TabIndex = 1;
            this.cmbServiceArea.SelectedIndexChanged += new System.EventHandler(this.cmbServiceArea_SelectedIndexChanged);
            this.cmbServiceArea.TextChanged += new System.EventHandler(this.cmbServiceArea_TextChanged);
            // 
            // lblServiceArea
            // 
            this.lblServiceArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServiceArea.AutoSize = true;
            this.lblServiceArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceArea.Location = new System.Drawing.Point(3, 7);
            this.lblServiceArea.Name = "lblServiceArea";
            this.lblServiceArea.Size = new System.Drawing.Size(332, 15);
            this.lblServiceArea.TabIndex = 0;
            this.lblServiceArea.Text = "Service Area :";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(3, 93);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(332, 144);
            this.txtDescription.TabIndex = 3;
            // 
            // lblNotes
            // 
            this.lblNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotes.Location = new System.Drawing.Point(3, 247);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(332, 15);
            this.lblNotes.TabIndex = 4;
            this.lblNotes.Text = "Notes :";
            // 
            // grpDepot
            // 
            this.grpDepot.Controls.Add(this.tblDepotDetails);
            this.grpDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDepot.Location = new System.Drawing.Point(3, 3);
            this.grpDepot.Name = "grpDepot";
            this.grpDepot.Padding = new System.Windows.Forms.Padding(0);
            this.grpDepot.Size = new System.Drawing.Size(326, 512);
            this.grpDepot.TabIndex = 0;
            this.grpDepot.TabStop = false;
            this.grpDepot.Text = "Depot";
            // 
            // tblDepotDetails
            // 
            this.tblDepotDetails.ColumnCount = 2;
            this.tblDepotDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDepotDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblDepotDetails.Controls.Add(this.lblOperator, 0, 0);
            this.tblDepotDetails.Controls.Add(this.cmbSupplier, 0, 1);
            this.tblDepotDetails.Controls.Add(this.cmbDepot, 1, 1);
            this.tblDepotDetails.Controls.Add(this.lblDepot, 1, 0);
            this.tblDepotDetails.Controls.Add(this.lblDepotName, 0, 2);
            this.tblDepotDetails.Controls.Add(this.lblCadastreCode, 1, 2);
            this.tblDepotDetails.Controls.Add(this.txtDepotName, 0, 3);
            this.tblDepotDetails.Controls.Add(this.txtCadastralCode, 1, 3);
            this.tblDepotDetails.Controls.Add(this.lblDepotAddress, 0, 4);
            this.tblDepotDetails.Controls.Add(this.lblDepotPostCode, 1, 4);
            this.tblDepotDetails.Controls.Add(this.tblDepotAddress, 0, 5);
            this.tblDepotDetails.Controls.Add(this.tblPostCode, 1, 5);
            this.tblDepotDetails.Controls.Add(this.lblStreetNo, 0, 6);
            this.tblDepotDetails.Controls.Add(this.lblDepotPhoneNo, 1, 6);
            this.tblDepotDetails.Controls.Add(this.txtStreetNo, 0, 7);
            this.tblDepotDetails.Controls.Add(this.txtPhoneNo, 1, 7);
            this.tblDepotDetails.Controls.Add(this.lblProvince, 0, 8);
            this.tblDepotDetails.Controls.Add(this.lblContactName, 1, 8);
            this.tblDepotDetails.Controls.Add(this.txtProvince, 0, 9);
            this.tblDepotDetails.Controls.Add(this.txtContactName, 1, 9);
            this.tblDepotDetails.Controls.Add(this.lblMunicipality, 0, 10);
            this.tblDepotDetails.Controls.Add(this.lblReference, 1, 10);
            this.tblDepotDetails.Controls.Add(this.txtMuncipality, 0, 11);
            this.tblDepotDetails.Controls.Add(this.txtDepotReference, 1, 11);
            this.tblDepotDetails.Controls.Add(this.chkServiceDepot, 1, 12);
            this.tblDepotDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDepotDetails.Location = new System.Drawing.Point(0, 13);
            this.tblDepotDetails.Name = "tblDepotDetails";
            this.tblDepotDetails.RowCount = 14;
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepotDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDepotDetails.Size = new System.Drawing.Size(326, 499);
            this.tblDepotDetails.TabIndex = 1;
            // 
            // lblOperator
            // 
            this.lblOperator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperator.AutoSize = true;
            this.lblOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperator.Location = new System.Drawing.Point(3, 7);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(157, 15);
            this.lblOperator.TabIndex = 0;
            this.lblOperator.Text = "Operator :";
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(3, 33);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(157, 21);
            this.cmbSupplier.TabIndex = 2;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // cmbDepot
            // 
            this.cmbDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(166, 33);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(157, 21);
            this.cmbDepot.TabIndex = 3;
            this.cmbDepot.SelectedIndexChanged += new System.EventHandler(this.cmbDepot_SelectedIndexChanged);
            // 
            // lblDepot
            // 
            this.lblDepot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepot.AutoSize = true;
            this.lblDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepot.Location = new System.Drawing.Point(166, 7);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(157, 15);
            this.lblDepot.TabIndex = 1;
            this.lblDepot.Text = "Depot :";
            // 
            // lblDepotName
            // 
            this.lblDepotName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepotName.AutoSize = true;
            this.lblDepotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepotName.Location = new System.Drawing.Point(3, 67);
            this.lblDepotName.Name = "lblDepotName";
            this.lblDepotName.Size = new System.Drawing.Size(157, 15);
            this.lblDepotName.TabIndex = 4;
            this.lblDepotName.Text = "* Depot Name :";
            // 
            // lblCadastreCode
            // 
            this.lblCadastreCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCadastreCode.AutoSize = true;
            this.lblCadastreCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCadastreCode.Location = new System.Drawing.Point(166, 67);
            this.lblCadastreCode.Name = "lblCadastreCode";
            this.lblCadastreCode.Size = new System.Drawing.Size(157, 15);
            this.lblCadastreCode.TabIndex = 5;
            this.lblCadastreCode.Text = "Cadastre Code :";
            // 
            // txtDepotName
            // 
            this.txtDepotName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDepotName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepotName.Location = new System.Drawing.Point(3, 93);
            this.txtDepotName.MaxLength = 30;
            this.txtDepotName.Name = "txtDepotName";
            this.txtDepotName.Size = new System.Drawing.Size(157, 20);
            this.txtDepotName.TabIndex = 6;
            // 
            // txtCadastralCode
            // 
            this.txtCadastralCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCadastralCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCadastralCode.Location = new System.Drawing.Point(166, 93);
            this.txtCadastralCode.MaxLength = 4;
            this.txtCadastralCode.Name = "txtCadastralCode";
            this.txtCadastralCode.Size = new System.Drawing.Size(157, 20);
            this.txtCadastralCode.TabIndex = 7;
            // 
            // lblDepotAddress
            // 
            this.lblDepotAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepotAddress.AutoSize = true;
            this.lblDepotAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepotAddress.Location = new System.Drawing.Point(3, 127);
            this.lblDepotAddress.Name = "lblDepotAddress";
            this.lblDepotAddress.Size = new System.Drawing.Size(157, 15);
            this.lblDepotAddress.TabIndex = 8;
            this.lblDepotAddress.Text = "* Depot Address :";
            // 
            // lblDepotPostCode
            // 
            this.lblDepotPostCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepotPostCode.AutoSize = true;
            this.lblDepotPostCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepotPostCode.Location = new System.Drawing.Point(166, 127);
            this.lblDepotPostCode.Name = "lblDepotPostCode";
            this.lblDepotPostCode.Size = new System.Drawing.Size(157, 15);
            this.lblDepotPostCode.TabIndex = 9;
            this.lblDepotPostCode.Text = "* Depot Postcode :";
            // 
            // tblDepotAddress
            // 
            this.tblDepotAddress.ColumnCount = 1;
            this.tblDepotAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepotAddress.Controls.Add(this.txtAddress, 0, 0);
            this.tblDepotAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDepotAddress.Location = new System.Drawing.Point(3, 153);
            this.tblDepotAddress.Name = "tblDepotAddress";
            this.tblDepotAddress.RowCount = 1;
            this.tblDepotAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDepotAddress.Size = new System.Drawing.Size(157, 94);
            this.tblDepotAddress.TabIndex = 13;
            // 
            // txtAddress
            // 
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(3, 3);
            this.txtAddress.MaxLength = 250;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(151, 88);
            this.txtAddress.TabIndex = 0;
            // 
            // tblPostCode
            // 
            this.tblPostCode.ColumnCount = 1;
            this.tblPostCode.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPostCode.Controls.Add(this.txtArea, 0, 2);
            this.tblPostCode.Controls.Add(this.txtPostCode, 0, 0);
            this.tblPostCode.Controls.Add(this.lblArea, 0, 1);
            this.tblPostCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPostCode.Location = new System.Drawing.Point(166, 153);
            this.tblPostCode.Name = "tblPostCode";
            this.tblPostCode.RowCount = 3;
            this.tblPostCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblPostCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblPostCode.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblPostCode.Size = new System.Drawing.Size(157, 94);
            this.tblPostCode.TabIndex = 14;
            // 
            // txtArea
            // 
            this.txtArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.Location = new System.Drawing.Point(3, 65);
            this.txtArea.MaxLength = 5;
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(151, 20);
            this.txtArea.TabIndex = 2;
            this.txtArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtArea_KeyPress);
            // 
            // txtPostCode
            // 
            this.txtPostCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPostCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostCode.Location = new System.Drawing.Point(3, 3);
            this.txtPostCode.MaxLength = 10;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(151, 20);
            this.txtPostCode.TabIndex = 0;
            // 
            // lblArea
            // 
            this.lblArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArea.Location = new System.Drawing.Point(3, 39);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(151, 15);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Area (in sqm) :";
            // 
            // lblStreetNo
            // 
            this.lblStreetNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStreetNo.AutoSize = true;
            this.lblStreetNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStreetNo.Location = new System.Drawing.Point(3, 257);
            this.lblStreetNo.Name = "lblStreetNo";
            this.lblStreetNo.Size = new System.Drawing.Size(157, 15);
            this.lblStreetNo.TabIndex = 10;
            this.lblStreetNo.Text = "Street Number :";
            // 
            // lblDepotPhoneNo
            // 
            this.lblDepotPhoneNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepotPhoneNo.AutoSize = true;
            this.lblDepotPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepotPhoneNo.Location = new System.Drawing.Point(166, 257);
            this.lblDepotPhoneNo.Name = "lblDepotPhoneNo";
            this.lblDepotPhoneNo.Size = new System.Drawing.Size(157, 15);
            this.lblDepotPhoneNo.TabIndex = 11;
            this.lblDepotPhoneNo.Text = "Depot Phone No :";
            // 
            // txtStreetNo
            // 
            this.txtStreetNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStreetNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStreetNo.Location = new System.Drawing.Point(3, 283);
            this.txtStreetNo.MaxLength = 7;
            this.txtStreetNo.Name = "txtStreetNo";
            this.txtStreetNo.Size = new System.Drawing.Size(157, 20);
            this.txtStreetNo.TabIndex = 12;
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPhoneNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneNo.Location = new System.Drawing.Point(166, 283);
            this.txtPhoneNo.MaxLength = 50;
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.Size = new System.Drawing.Size(157, 20);
            this.txtPhoneNo.TabIndex = 13;
            // 
            // lblProvince
            // 
            this.lblProvince.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProvince.AutoSize = true;
            this.lblProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvince.Location = new System.Drawing.Point(3, 317);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(157, 15);
            this.lblProvince.TabIndex = 14;
            this.lblProvince.Text = "Province :";
            // 
            // lblContactName
            // 
            this.lblContactName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContactName.AutoSize = true;
            this.lblContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactName.Location = new System.Drawing.Point(166, 317);
            this.lblContactName.Name = "lblContactName";
            this.lblContactName.Size = new System.Drawing.Size(157, 15);
            this.lblContactName.TabIndex = 15;
            this.lblContactName.Text = "Contact Name :";
            // 
            // txtProvince
            // 
            this.txtProvince.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProvince.Location = new System.Drawing.Point(3, 343);
            this.txtProvince.MaxLength = 2;
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.Size = new System.Drawing.Size(157, 20);
            this.txtProvince.TabIndex = 16;
            // 
            // txtContactName
            // 
            this.txtContactName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContactName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactName.Location = new System.Drawing.Point(166, 343);
            this.txtContactName.MaxLength = 30;
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(157, 20);
            this.txtContactName.TabIndex = 17;
            // 
            // lblMunicipality
            // 
            this.lblMunicipality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMunicipality.AutoSize = true;
            this.lblMunicipality.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMunicipality.Location = new System.Drawing.Point(3, 377);
            this.lblMunicipality.Name = "lblMunicipality";
            this.lblMunicipality.Size = new System.Drawing.Size(157, 15);
            this.lblMunicipality.TabIndex = 18;
            this.lblMunicipality.Text = "Municipality :";
            // 
            // lblReference
            // 
            this.lblReference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReference.AutoSize = true;
            this.lblReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReference.Location = new System.Drawing.Point(166, 377);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(157, 15);
            this.lblReference.TabIndex = 19;
            this.lblReference.Text = "Reference :";
            // 
            // txtMuncipality
            // 
            this.txtMuncipality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMuncipality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMuncipality.Location = new System.Drawing.Point(3, 403);
            this.txtMuncipality.MaxLength = 60;
            this.txtMuncipality.Name = "txtMuncipality";
            this.txtMuncipality.Size = new System.Drawing.Size(157, 20);
            this.txtMuncipality.TabIndex = 20;
            // 
            // txtDepotReference
            // 
            this.txtDepotReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDepotReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepotReference.Location = new System.Drawing.Point(166, 403);
            this.txtDepotReference.MaxLength = 20;
            this.txtDepotReference.Name = "txtDepotReference";
            this.txtDepotReference.Size = new System.Drawing.Size(157, 20);
            this.txtDepotReference.TabIndex = 21;
            // 
            // chkServiceDepot
            // 
            this.chkServiceDepot.AutoSize = true;
            this.chkServiceDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkServiceDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkServiceDepot.Location = new System.Drawing.Point(166, 433);
            this.chkServiceDepot.Name = "chkServiceDepot";
            this.chkServiceDepot.Size = new System.Drawing.Size(157, 24);
            this.chkServiceDepot.TabIndex = 22;
            this.chkServiceDepot.Text = "This Depot is a Service Depot";
            this.chkServiceDepot.UseVisualStyleBackColor = true;
            this.chkServiceDepot.CheckedChanged += new System.EventHandler(this.chkServiceDepot_CheckedChanged);
            // 
            // tblControls
            // 
            this.tblControls.ColumnCount = 4;
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tblControls.Controls.Add(this.btnClose, 3, 0);
            this.tblControls.Controls.Add(this.btnUpdate, 2, 0);
            this.tblControls.Controls.Add(this.btnNew, 1, 0);
            this.tblControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblControls.Location = new System.Drawing.Point(3, 527);
            this.tblControls.Name = "tblControls";
            this.tblControls.RowCount = 1;
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblControls.Size = new System.Drawing.Size(1015, 34);
            this.tblControls.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(912, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(806, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(700, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 28);
            this.btnNew.TabIndex = 0;
            this.btnNew.Tag = "New";
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmDepot
            // 
            this.AcceptButton = this.btnNew;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1021, 564);
            this.Controls.Add(this.tblDepot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(931, 600);
            this.Name = "frmDepot";
            this.ShowInTaskbar = false;
            this.Text = "Depot";
            this.Activated += new System.EventHandler(this.frmDepotNew_Activated);
            this.Load += new System.EventHandler(this.frmDepotNew_Load);
            this.tblDepot.ResumeLayout(false);
            this.tblDepotContents.ResumeLayout(false);
            this.tblDepotContents.PerformLayout();
            this.grb_SiteRep.ResumeLayout(false);
            this.gpbServiceArea.ResumeLayout(false);
            this.tblServiceArea.ResumeLayout(false);
            this.tblServiceArea.PerformLayout();
            this.grpDepot.ResumeLayout(false);
            this.tblDepotDetails.ResumeLayout(false);
            this.tblDepotDetails.PerformLayout();
            this.tblDepotAddress.ResumeLayout(false);
            this.tblDepotAddress.PerformLayout();
            this.tblPostCode.ResumeLayout(false);
            this.tblPostCode.PerformLayout();
            this.tblControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblDepot;
        private System.Windows.Forms.TableLayoutPanel tblDepotContents;
        private System.Windows.Forms.TableLayoutPanel tblControls;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.GroupBox grpDepot;
        private System.Windows.Forms.TableLayoutPanel tblDepotDetails;
        private System.Windows.Forms.Label lblOperator;
        private Helpers.BmcComboBox cmbSupplier;
        private System.Windows.Forms.ComboBox cmbDepot;
        private System.Windows.Forms.Label lblDepot;
        private System.Windows.Forms.Label lblDepotName;
        private System.Windows.Forms.Label lblCadastreCode;
        private System.Windows.Forms.TextBox txtDepotName;
        private System.Windows.Forms.TextBox txtCadastralCode;
        private System.Windows.Forms.Label lblDepotAddress;
        private System.Windows.Forms.Label lblDepotPostCode;
        private System.Windows.Forms.TableLayoutPanel tblDepotAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TableLayoutPanel tblPostCode;
        private System.Windows.Forms.TextBox txtPostCode;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label lblStreetNo;
        private System.Windows.Forms.Label lblDepotPhoneNo;
        private System.Windows.Forms.TextBox txtStreetNo;
        private System.Windows.Forms.TextBox txtPhoneNo;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.Label lblContactName;
        private System.Windows.Forms.TextBox txtProvince;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label lblMunicipality;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.TextBox txtMuncipality;
        private System.Windows.Forms.TextBox txtDepotReference;
        private System.Windows.Forms.CheckBox chkServiceDepot;
        private System.Windows.Forms.GroupBox gpbServiceArea;
        private System.Windows.Forms.TableLayoutPanel tblServiceArea;
        private System.Windows.Forms.Label lblServiceArea;
        private System.Windows.Forms.ComboBox cmbServiceArea;
        private System.Windows.Forms.Label lblServiceDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.GroupBox grb_SiteRep;
        private System.Windows.Forms.ListView lvwSiteReps;
        private System.Windows.Forms.ColumnHeader clmReps;
    }
}