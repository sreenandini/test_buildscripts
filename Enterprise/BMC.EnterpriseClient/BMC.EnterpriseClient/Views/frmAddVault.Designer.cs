namespace BMC.EnterpriseClient.Views
{
    partial class frmAddVault
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_DeviceName = new System.Windows.Forms.TextBox();
            this.txt_SerialNo = new System.Windows.Forms.TextBox();
            this.txt_AlertLevel = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lbl_Manaufacturer = new System.Windows.Forms.Label();
            this.cmb_Manufacturer = new System.Windows.Forms.ComboBox();
            this.lbl_Capacity = new System.Windows.Forms.Label();
            this.txt_Capacity = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lbl_NoofCassettes = new System.Windows.Forms.Label();
            this.txt_NoofCassettes = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txt_Coinhoppers = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lbl_CoinHoppers = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnCassettes = new System.Windows.Forms.Button();
            this.btnHoppers = new System.Windows.Forms.Button();
            this.txt_Type = new System.Windows.Forms.TextBox();
            this.lbl_Type = new System.Windows.Forms.Label();
            this.lblIsWebserviceEnabled = new System.Windows.Forms.Label();
            this.chk_WebserviceEnabled = new System.Windows.Forms.CheckBox();
            this.btn_Terminate = new System.Windows.Forms.Button();
            this.btnRejection = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.Chk_Active = new System.Windows.Forms.CheckBox();
            this.chkAutoAdjust = new System.Windows.Forms.CheckBox();
            this.chkRejectionFill = new System.Windows.Forms.CheckBox();
            this.lbl_StandardFillAmount = new System.Windows.Forms.Label();
            this.txt_StandardFillAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.lbl_Sites = new System.Windows.Forms.Label();
            this.lbl_VaultDetails = new System.Windows.Forms.Label();
            this.lvVaultdetails = new System.Windows.Forms.ListView();
            this.clmVaultName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSerialNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSiteCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSiteName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_SearchVault = new System.Windows.Forms.TextBox();
            this.lbl_VaultStatus = new System.Windows.Forms.Label();
            this.cbo_Status = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tlp_header = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.mnuCopyAsset = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSubitemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_VaultName = new System.Windows.Forms.Label();
            this.lbl_SerialNUmber = new System.Windows.Forms.Label();
            this.lbl_AlertLevel = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tlp_header.SuspendLayout();
            this.mnuCopyAsset.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 5;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStatus, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlp_header, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(805, 653);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lbl_Sites, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lbl_VaultDetails, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.lvVaultdetails, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.tableLayoutPanel9, 1, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(799, 587);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoScroll = true;
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.txt_DeviceName, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.txt_SerialNo, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.txt_AlertLevel, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.lbl_Manaufacturer, 0, 4);
            this.tableLayoutPanel8.Controls.Add(this.cmb_Manufacturer, 1, 4);
            this.tableLayoutPanel8.Controls.Add(this.lbl_Capacity, 0, 6);
            this.tableLayoutPanel8.Controls.Add(this.txt_Capacity, 1, 6);
            this.tableLayoutPanel8.Controls.Add(this.lbl_NoofCassettes, 0, 7);
            this.tableLayoutPanel8.Controls.Add(this.txt_NoofCassettes, 1, 7);
            this.tableLayoutPanel8.Controls.Add(this.txt_Coinhoppers, 1, 8);
            this.tableLayoutPanel8.Controls.Add(this.lbl_CoinHoppers, 0, 8);
            this.tableLayoutPanel8.Controls.Add(this.btn_Save, 2, 14);
            this.tableLayoutPanel8.Controls.Add(this.btnNew, 1, 14);
            this.tableLayoutPanel8.Controls.Add(this.txtDescription, 0, 13);
            this.tableLayoutPanel8.Controls.Add(this.btnCassettes, 2, 7);
            this.tableLayoutPanel8.Controls.Add(this.btnHoppers, 2, 8);
            this.tableLayoutPanel8.Controls.Add(this.txt_Type, 1, 3);
            this.tableLayoutPanel8.Controls.Add(this.lbl_Type, 0, 3);
            this.tableLayoutPanel8.Controls.Add(this.lblIsWebserviceEnabled, 0, 5);
            this.tableLayoutPanel8.Controls.Add(this.chk_WebserviceEnabled, 1, 5);
            this.tableLayoutPanel8.Controls.Add(this.btn_Terminate, 0, 14);
            this.tableLayoutPanel8.Controls.Add(this.btnRejection, 2, 9);
            this.tableLayoutPanel8.Controls.Add(this.lblDescription, 0, 12);
            this.tableLayoutPanel8.Controls.Add(this.Chk_Active, 2, 11);
            this.tableLayoutPanel8.Controls.Add(this.chkAutoAdjust, 0, 11);
            this.tableLayoutPanel8.Controls.Add(this.chkRejectionFill, 1, 11);
            this.tableLayoutPanel8.Controls.Add(this.lbl_StandardFillAmount, 0, 10);
            this.tableLayoutPanel8.Controls.Add(this.txt_StandardFillAmount, 1, 10);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tableLayoutPanel8.RowCount = 15;
            this.tableLayoutPanel7.SetRowSpan(this.tableLayoutPanel8, 2);
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.015037F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.066381F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.15837F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.06335F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(353, 561);
            this.tableLayoutPanel8.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "* Name :";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "* Serial Number :";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "* Alert Level (%) :";
            // 
            // txt_DeviceName
            // 
            this.txt_DeviceName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel8.SetColumnSpan(this.txt_DeviceName, 2);
            this.txt_DeviceName.Enabled = false;
            this.txt_DeviceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DeviceName.Location = new System.Drawing.Point(150, 7);
            this.txt_DeviceName.MaxLength = 20;
            this.txt_DeviceName.Name = "txt_DeviceName";
            this.txt_DeviceName.Size = new System.Drawing.Size(143, 20);
            this.txt_DeviceName.TabIndex = 1;
            // 
            // txt_SerialNo
            // 
            this.txt_SerialNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel8.SetColumnSpan(this.txt_SerialNo, 2);
            this.txt_SerialNo.Enabled = false;
            this.txt_SerialNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SerialNo.Location = new System.Drawing.Point(150, 41);
            this.txt_SerialNo.MaxLength = 20;
            this.txt_SerialNo.Name = "txt_SerialNo";
            this.txt_SerialNo.Size = new System.Drawing.Size(143, 20);
            this.txt_SerialNo.TabIndex = 3;
            this.txt_SerialNo.Enter += new System.EventHandler(this.txt_SerialNo_Enter);
            this.txt_SerialNo.Leave += new System.EventHandler(this.txt_SerialNo_Leave);
            // 
            // txt_AlertLevel
            // 
            this.txt_AlertLevel.AllowDecimal = false;
            this.txt_AlertLevel.AllowNegative = false;
            this.txt_AlertLevel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_AlertLevel.DecimalLength = 0;
            this.txt_AlertLevel.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt_AlertLevel.Enabled = false;
            this.txt_AlertLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_AlertLevel.Location = new System.Drawing.Point(150, 75);
            this.txt_AlertLevel.MaxLength = 3;
            this.txt_AlertLevel.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txt_AlertLevel.Name = "txt_AlertLevel";
            this.txt_AlertLevel.ShortcutsEnabled = false;
            this.txt_AlertLevel.Size = new System.Drawing.Size(59, 20);
            this.txt_AlertLevel.TabIndex = 5;
            this.txt_AlertLevel.Enter += new System.EventHandler(this.txt_AlertLevel_Enter);
            this.txt_AlertLevel.Leave += new System.EventHandler(this.txt_AlertLevel_Leave);
            // 
            // lbl_Manaufacturer
            // 
            this.lbl_Manaufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Manaufacturer.AutoSize = true;
            this.lbl_Manaufacturer.Location = new System.Drawing.Point(13, 146);
            this.lbl_Manaufacturer.Name = "lbl_Manaufacturer";
            this.lbl_Manaufacturer.Size = new System.Drawing.Size(83, 13);
            this.lbl_Manaufacturer.TabIndex = 8;
            this.lbl_Manaufacturer.Text = "* Manufacturer :";
            // 
            // cmb_Manufacturer
            // 
            this.cmb_Manufacturer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutPanel8.SetColumnSpan(this.cmb_Manufacturer, 2);
            this.cmb_Manufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Manufacturer.Enabled = false;
            this.cmb_Manufacturer.FormattingEnabled = true;
            this.cmb_Manufacturer.Location = new System.Drawing.Point(150, 142);
            this.cmb_Manufacturer.Name = "cmb_Manufacturer";
            this.cmb_Manufacturer.Size = new System.Drawing.Size(143, 21);
            this.cmb_Manufacturer.TabIndex = 9;
            // 
            // lbl_Capacity
            // 
            this.lbl_Capacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Capacity.AutoSize = true;
            this.lbl_Capacity.Location = new System.Drawing.Point(13, 214);
            this.lbl_Capacity.Name = "lbl_Capacity";
            this.lbl_Capacity.Size = new System.Drawing.Size(54, 13);
            this.lbl_Capacity.TabIndex = 12;
            this.lbl_Capacity.Text = "Capacity :";
            // 
            // txt_Capacity
            // 
            this.txt_Capacity.AllowDecimal = true;
            this.txt_Capacity.AllowNegative = false;
            this.txt_Capacity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Capacity.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Capacity.DecimalLength = 2;
            this.txt_Capacity.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt_Capacity.Enabled = false;
            this.txt_Capacity.Location = new System.Drawing.Point(150, 211);
            this.txt_Capacity.MaxLength = 11;
            this.txt_Capacity.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txt_Capacity.Name = "txt_Capacity";
            this.txt_Capacity.ShortcutsEnabled = false;
            this.txt_Capacity.Size = new System.Drawing.Size(79, 20);
            this.txt_Capacity.TabIndex = 13;
            this.txt_Capacity.Enter += new System.EventHandler(this.txt_Capacity_Enter);
            this.txt_Capacity.Leave += new System.EventHandler(this.txt_Capacity_Leave);
            // 
            // lbl_NoofCassettes
            // 
            this.lbl_NoofCassettes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_NoofCassettes.AutoSize = true;
            this.lbl_NoofCassettes.Location = new System.Drawing.Point(13, 248);
            this.lbl_NoofCassettes.Name = "lbl_NoofCassettes";
            this.lbl_NoofCassettes.Size = new System.Drawing.Size(88, 13);
            this.lbl_NoofCassettes.TabIndex = 14;
            this.lbl_NoofCassettes.Text = "No of Cassettes :";
            // 
            // txt_NoofCassettes
            // 
            this.txt_NoofCassettes.AllowDecimal = false;
            this.txt_NoofCassettes.AllowNegative = false;
            this.txt_NoofCassettes.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_NoofCassettes.BackColor = System.Drawing.SystemColors.Window;
            this.txt_NoofCassettes.DecimalLength = 0;
            this.txt_NoofCassettes.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt_NoofCassettes.Enabled = false;
            this.txt_NoofCassettes.Location = new System.Drawing.Point(150, 245);
            this.txt_NoofCassettes.MaxLength = 2;
            this.txt_NoofCassettes.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txt_NoofCassettes.Name = "txt_NoofCassettes";
            this.txt_NoofCassettes.ShortcutsEnabled = false;
            this.txt_NoofCassettes.Size = new System.Drawing.Size(59, 20);
            this.txt_NoofCassettes.TabIndex = 15;
            this.txt_NoofCassettes.Enter += new System.EventHandler(this.txt_NoofCassettes_Enter);
            this.txt_NoofCassettes.Leave += new System.EventHandler(this.txt_NoofCassettes_Leave);
            // 
            // txt_Coinhoppers
            // 
            this.txt_Coinhoppers.AllowDecimal = false;
            this.txt_Coinhoppers.AllowNegative = false;
            this.txt_Coinhoppers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Coinhoppers.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Coinhoppers.DecimalLength = 0;
            this.txt_Coinhoppers.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt_Coinhoppers.Enabled = false;
            this.txt_Coinhoppers.Location = new System.Drawing.Point(150, 279);
            this.txt_Coinhoppers.MaxLength = 2;
            this.txt_Coinhoppers.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txt_Coinhoppers.Name = "txt_Coinhoppers";
            this.txt_Coinhoppers.ShortcutsEnabled = false;
            this.txt_Coinhoppers.Size = new System.Drawing.Size(58, 20);
            this.txt_Coinhoppers.TabIndex = 18;
            this.txt_Coinhoppers.Enter += new System.EventHandler(this.txt_Coinhoppers_Enter);
            this.txt_Coinhoppers.Leave += new System.EventHandler(this.txt_Coinhoppers_Leave);
            // 
            // lbl_CoinHoppers
            // 
            this.lbl_CoinHoppers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_CoinHoppers.AutoSize = true;
            this.lbl_CoinHoppers.Location = new System.Drawing.Point(13, 282);
            this.lbl_CoinHoppers.Name = "lbl_CoinHoppers";
            this.lbl_CoinHoppers.Size = new System.Drawing.Size(106, 13);
            this.lbl_CoinHoppers.TabIndex = 17;
            this.lbl_CoinHoppers.Text = "No of Coin Hoppers :";
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(269, 534);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(20, 8, 8, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(76, 24);
            this.btn_Save.TabIndex = 30;
            this.btn_Save.Tag = "";
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Location = new System.Drawing.Point(167, 534);
            this.btnNew.Margin = new System.Windows.Forms.Padding(20, 8, 8, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(74, 24);
            this.btnNew.TabIndex = 29;
            this.btnNew.Tag = "New";
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.SetColumnSpan(this.txtDescription, 3);
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(13, 444);
            this.txtDescription.MaxLength = 150;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(337, 79);
            this.txtDescription.TabIndex = 27;
            // 
            // btnCassettes
            // 
            this.btnCassettes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCassettes.Enabled = false;
            this.btnCassettes.Location = new System.Drawing.Point(249, 241);
            this.btnCassettes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnCassettes.Name = "btnCassettes";
            this.btnCassettes.Size = new System.Drawing.Size(104, 30);
            this.btnCassettes.TabIndex = 16;
            this.btnCassettes.Text = "C&assettes";
            this.btnCassettes.UseVisualStyleBackColor = true;
            this.btnCassettes.Click += new System.EventHandler(this.btnCassettes_Click);
            // 
            // btnHoppers
            // 
            this.btnHoppers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHoppers.Enabled = false;
            this.btnHoppers.Location = new System.Drawing.Point(249, 275);
            this.btnHoppers.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnHoppers.Name = "btnHoppers";
            this.btnHoppers.Size = new System.Drawing.Size(104, 30);
            this.btnHoppers.TabIndex = 19;
            this.btnHoppers.Text = "&Hoppers";
            this.btnHoppers.UseVisualStyleBackColor = true;
            this.btnHoppers.Click += new System.EventHandler(this.btnHoppers_Click);
            // 
            // txt_Type
            // 
            this.txt_Type.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_Type.Enabled = false;
            this.txt_Type.Location = new System.Drawing.Point(150, 109);
            this.txt_Type.MaxLength = 5;
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.Size = new System.Drawing.Size(58, 20);
            this.txt_Type.TabIndex = 7;
            // 
            // lbl_Type
            // 
            this.lbl_Type.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_Type.AutoSize = true;
            this.lbl_Type.Location = new System.Drawing.Point(13, 112);
            this.lbl_Type.Name = "lbl_Type";
            this.lbl_Type.Size = new System.Drawing.Size(44, 13);
            this.lbl_Type.TabIndex = 6;
            this.lbl_Type.Text = "* Type :";
            // 
            // lblIsWebserviceEnabled
            // 
            this.lblIsWebserviceEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIsWebserviceEnabled.AutoSize = true;
            this.lblIsWebserviceEnabled.Location = new System.Drawing.Point(13, 180);
            this.lblIsWebserviceEnabled.Name = "lblIsWebserviceEnabled";
            this.lblIsWebserviceEnabled.Size = new System.Drawing.Size(131, 13);
            this.lblIsWebserviceEnabled.TabIndex = 10;
            this.lblIsWebserviceEnabled.Text = "Vault Interface Enabled :";
            // 
            // chk_WebserviceEnabled
            // 
            this.chk_WebserviceEnabled.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chk_WebserviceEnabled.AutoSize = true;
            this.chk_WebserviceEnabled.BackColor = System.Drawing.SystemColors.Window;
            this.tableLayoutPanel8.SetColumnSpan(this.chk_WebserviceEnabled, 2);
            this.chk_WebserviceEnabled.Enabled = false;
            this.chk_WebserviceEnabled.Location = new System.Drawing.Point(150, 180);
            this.chk_WebserviceEnabled.Name = "chk_WebserviceEnabled";
            this.chk_WebserviceEnabled.Size = new System.Drawing.Size(15, 14);
            this.chk_WebserviceEnabled.TabIndex = 11;
            this.chk_WebserviceEnabled.UseVisualStyleBackColor = false;
            this.chk_WebserviceEnabled.CheckedChanged += new System.EventHandler(this.chk_WebserviceEnabled_CheckedChanged);
            // 
            // btn_Terminate
            // 
            this.btn_Terminate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Terminate.Location = new System.Drawing.Point(30, 534);
            this.btn_Terminate.Margin = new System.Windows.Forms.Padding(20, 8, 8, 3);
            this.btn_Terminate.Name = "btn_Terminate";
            this.btn_Terminate.Size = new System.Drawing.Size(109, 24);
            this.btn_Terminate.TabIndex = 28;
            this.btn_Terminate.Text = "&Terminate";
            this.btn_Terminate.UseVisualStyleBackColor = true;
            this.btn_Terminate.Click += new System.EventHandler(this.btn_Terminate_Click);
            // 
            // btnRejection
            // 
            this.btnRejection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRejection.Enabled = false;
            this.btnRejection.Location = new System.Drawing.Point(249, 309);
            this.btnRejection.Margin = new System.Windows.Forms.Padding(0, 3, 0, 1);
            this.btnRejection.Name = "btnRejection";
            this.btnRejection.Size = new System.Drawing.Size(104, 30);
            this.btnRejection.TabIndex = 20;
            this.btnRejection.Text = "&Rejection Cassette";
            this.btnRejection.UseVisualStyleBackColor = true;
            this.btnRejection.Click += new System.EventHandler(this.btnRejection_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(13, 428);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(73, 13);
            this.lblDescription.TabIndex = 26;
            this.lblDescription.Text = "* Description :";
            // 
            // Chk_Active
            // 
            this.Chk_Active.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Chk_Active.AutoSize = true;
            this.Chk_Active.Enabled = false;
            this.Chk_Active.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Chk_Active.Location = new System.Drawing.Point(252, 381);
            this.Chk_Active.Name = "Chk_Active";
            this.Chk_Active.Size = new System.Drawing.Size(56, 17);
            this.Chk_Active.TabIndex = 25;
            this.Chk_Active.Text = "Active";
            this.Chk_Active.UseVisualStyleBackColor = true;
            // 
            // chkAutoAdjust
            // 
            this.chkAutoAdjust.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkAutoAdjust.AutoSize = true;
            this.chkAutoAdjust.Location = new System.Drawing.Point(13, 381);
            this.chkAutoAdjust.Name = "chkAutoAdjust";
            this.chkAutoAdjust.Size = new System.Drawing.Size(118, 17);
            this.chkAutoAdjust.TabIndex = 23;
            this.chkAutoAdjust.Text = "Auto adjust on drop";
            this.chkAutoAdjust.UseVisualStyleBackColor = true;
            // 
            // chkRejectionFill
            // 
            this.chkRejectionFill.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRejectionFill.AutoSize = true;
            this.chkRejectionFill.Location = new System.Drawing.Point(150, 381);
            this.chkRejectionFill.Name = "chkRejectionFill";
            this.chkRejectionFill.Size = new System.Drawing.Size(86, 17);
            this.chkRejectionFill.TabIndex = 24;
            this.chkRejectionFill.Text = "Fill Rejection";
            this.chkRejectionFill.UseVisualStyleBackColor = true;
            // 
            // lbl_StandardFillAmount
            // 
            this.lbl_StandardFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_StandardFillAmount.AutoSize = true;
            this.lbl_StandardFillAmount.Location = new System.Drawing.Point(13, 350);
            this.lbl_StandardFillAmount.Name = "lbl_StandardFillAmount";
            this.lbl_StandardFillAmount.Size = new System.Drawing.Size(117, 13);
            this.lbl_StandardFillAmount.TabIndex = 21;
            this.lbl_StandardFillAmount.Text = "* Standard Fill Amount :";
            // 
            // txt_StandardFillAmount
            // 
            this.txt_StandardFillAmount.AllowDecimal = true;
            this.txt_StandardFillAmount.AllowNegative = false;
            this.txt_StandardFillAmount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_StandardFillAmount.BackColor = System.Drawing.SystemColors.Window;
            this.txt_StandardFillAmount.DecimalLength = 2;
            this.txt_StandardFillAmount.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txt_StandardFillAmount.Enabled = false;
            this.txt_StandardFillAmount.Location = new System.Drawing.Point(150, 346);
            this.txt_StandardFillAmount.MaxLength = 11;
            this.txt_StandardFillAmount.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txt_StandardFillAmount.Name = "txt_StandardFillAmount";
            this.txt_StandardFillAmount.ShortcutsEnabled = false;
            this.txt_StandardFillAmount.Size = new System.Drawing.Size(79, 20);
            this.txt_StandardFillAmount.TabIndex = 22;
            // 
            // lbl_Sites
            // 
            this.lbl_Sites.AutoSize = true;
            this.lbl_Sites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Sites.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Sites.Location = new System.Drawing.Point(362, 0);
            this.lbl_Sites.Name = "lbl_Sites";
            this.lbl_Sites.Size = new System.Drawing.Size(434, 20);
            this.lbl_Sites.TabIndex = 0;
            this.lbl_Sites.Text = "Vaults :";
            // 
            // lbl_VaultDetails
            // 
            this.lbl_VaultDetails.AutoSize = true;
            this.lbl_VaultDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_VaultDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VaultDetails.Location = new System.Drawing.Point(3, 0);
            this.lbl_VaultDetails.Name = "lbl_VaultDetails";
            this.lbl_VaultDetails.Padding = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.lbl_VaultDetails.Size = new System.Drawing.Size(353, 20);
            this.lbl_VaultDetails.TabIndex = 0;
            this.lbl_VaultDetails.Text = "Vault Details:";
            // 
            // lvVaultdetails
            // 
            this.lvVaultdetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmVaultName,
            this.clmSerialNo,
            this.clmSiteCode,
            this.clmSiteName,
            this.clmStatus});
            this.lvVaultdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvVaultdetails.FullRowSelect = true;
            this.lvVaultdetails.GridLines = true;
            this.lvVaultdetails.Location = new System.Drawing.Point(362, 53);
            this.lvVaultdetails.MultiSelect = false;
            this.lvVaultdetails.Name = "lvVaultdetails";
            this.lvVaultdetails.ShowItemToolTips = true;
            this.lvVaultdetails.Size = new System.Drawing.Size(434, 531);
            this.lvVaultdetails.TabIndex = 2;
            this.lvVaultdetails.UseCompatibleStateImageBehavior = false;
            this.lvVaultdetails.View = System.Windows.Forms.View.Details;
            this.lvVaultdetails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvVaultdetails_ColumnClick);
            this.lvVaultdetails.SelectedIndexChanged += new System.EventHandler(this.lvVaultdetails_SelectedIndexChanged);
            // 
            // clmVaultName
            // 
            this.clmVaultName.Text = "Vault Name";
            this.clmVaultName.Width = 150;
            // 
            // clmSerialNo
            // 
            this.clmSerialNo.Text = "Serial Number";
            this.clmSerialNo.Width = 150;
            // 
            // clmSiteCode
            // 
            this.clmSiteCode.Text = "Site Code";
            this.clmSiteCode.Width = 75;
            // 
            // clmSiteName
            // 
            this.clmSiteName.Text = "Site Name";
            this.clmSiteName.Width = 150;
            // 
            // clmStatus
            // 
            this.clmStatus.Text = "Status";
            this.clmStatus.Width = 75;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel9.Controls.Add(this.txt_SearchVault, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.lbl_VaultStatus, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.cbo_Status, 2, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(362, 23);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(434, 24);
            this.tableLayoutPanel9.TabIndex = 1;
            // 
            // txt_SearchVault
            // 
            this.txt_SearchVault.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_SearchVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SearchVault.ForeColor = System.Drawing.SystemColors.GrayText;
            this.txt_SearchVault.Location = new System.Drawing.Point(3, 3);
            this.txt_SearchVault.MaxLength = 100;
            this.txt_SearchVault.Name = "txt_SearchVault";
            this.txt_SearchVault.Size = new System.Drawing.Size(253, 20);
            this.txt_SearchVault.TabIndex = 0;
            this.txt_SearchVault.Text = "Search";
            this.txt_SearchVault.TextChanged += new System.EventHandler(this.txt_SearchSites_TextChanged);
            this.txt_SearchVault.Enter += new System.EventHandler(this.txt_SearchSites_Enter);
            this.txt_SearchVault.Leave += new System.EventHandler(this.txt_SearchSites_Leave);
            // 
            // lbl_VaultStatus
            // 
            this.lbl_VaultStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_VaultStatus.AutoSize = true;
            this.lbl_VaultStatus.Location = new System.Drawing.Point(262, 5);
            this.lbl_VaultStatus.Name = "lbl_VaultStatus";
            this.lbl_VaultStatus.Size = new System.Drawing.Size(44, 13);
            this.lbl_VaultStatus.TabIndex = 1;
            this.lbl_VaultStatus.Text = "Status :";
            // 
            // cbo_Status
            // 
            this.cbo_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbo_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_Status.FormattingEnabled = true;
            this.cbo_Status.Location = new System.Drawing.Point(312, 3);
            this.cbo_Status.Name = "cbo_Status";
            this.cbo_Status.Size = new System.Drawing.Size(119, 21);
            this.cbo_Status.TabIndex = 2;
            this.cbo_Status.SelectedIndexChanged += new System.EventHandler(this.cbo_Status_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(3, 623);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(799, 30);
            this.lblStatus.TabIndex = 0;
            // 
            // tlp_header
            // 
            this.tlp_header.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_header.ColumnCount = 1;
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_header.Controls.Add(this.lbl_Header, 0, 0);
            this.tlp_header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_header.Location = new System.Drawing.Point(3, 3);
            this.tlp_header.Name = "tlp_header";
            this.tlp_header.RowCount = 1;
            this.tlp_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_header.Size = new System.Drawing.Size(799, 24);
            this.tlp_header.TabIndex = 0;
            // 
            // lbl_Header
            // 
            this.lbl_Header.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.White;
            this.lbl_Header.Location = new System.Drawing.Point(3, 3);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(793, 17);
            this.lbl_Header.TabIndex = 0;
            this.lbl_Header.Text = "Vault Admin";
            // 
            // mnuCopyAsset
            // 
            this.mnuCopyAsset.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSubitemCopy});
            this.mnuCopyAsset.Name = "mnuCopyAsset";
            this.mnuCopyAsset.Size = new System.Drawing.Size(131, 26);
            // 
            // mnuSubitemCopy
            // 
            this.mnuSubitemCopy.Name = "mnuSubitemCopy";
            this.mnuSubitemCopy.Size = new System.Drawing.Size(130, 22);
            this.mnuSubitemCopy.Text = "CopyAsset";
            this.mnuSubitemCopy.Click += new System.EventHandler(this.mnuSubitemCopy_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_VaultName, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lbl_SerialNUmber, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.lbl_AlertLevel, 0, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel4.Location = new System.Drawing.Point(103, 30);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(94, 362);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // lbl_VaultName
            // 
            this.lbl_VaultName.AutoSize = true;
            this.lbl_VaultName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_VaultName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VaultName.Location = new System.Drawing.Point(3, 0);
            this.lbl_VaultName.Name = "lbl_VaultName";
            this.lbl_VaultName.Size = new System.Drawing.Size(41, 30);
            this.lbl_VaultName.TabIndex = 0;
            this.lbl_VaultName.Text = "Name";
            // 
            // lbl_SerialNUmber
            // 
            this.lbl_SerialNUmber.AutoSize = true;
            this.lbl_SerialNUmber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_SerialNUmber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SerialNUmber.Location = new System.Drawing.Point(3, 30);
            this.lbl_SerialNUmber.Name = "lbl_SerialNUmber";
            this.lbl_SerialNUmber.Size = new System.Drawing.Size(41, 30);
            this.lbl_SerialNUmber.TabIndex = 1;
            this.lbl_SerialNUmber.Text = "Serial Number";
            // 
            // lbl_AlertLevel
            // 
            this.lbl_AlertLevel.AutoSize = true;
            this.lbl_AlertLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_AlertLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AlertLevel.Location = new System.Drawing.Point(3, 60);
            this.lbl_AlertLevel.Name = "lbl_AlertLevel";
            this.lbl_AlertLevel.Size = new System.Drawing.Size(41, 30);
            this.lbl_AlertLevel.TabIndex = 2;
            this.lbl_AlertLevel.Text = "Alert Level";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.listBox1, 0, 2);
            this.tableLayoutPanel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textBox1.Location = new System.Drawing.Point(3, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Search";
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(94, 54);
            this.listBox1.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel6.Location = new System.Drawing.Point(103, 30);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 6;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(94, 362);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Serial Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Alert Level";
            // 
            // frmAddVault
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmAddVault";
            this.Size = new System.Drawing.Size(805, 653);
            this.Load += new System.EventHandler(this.frmFillVault_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tlp_header.ResumeLayout(false);
            this.tlp_header.PerformLayout();
            this.mnuCopyAsset.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txt_SearchVault;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        
       
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbl_VaultName;
        private System.Windows.Forms.Label lbl_SerialNUmber;
        private System.Windows.Forms.Label lbl_AlertLevel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Sites;
        private System.Windows.Forms.TextBox txt_DeviceName;
        private System.Windows.Forms.TextBox txt_SerialNo;
        private NumberTextBox txt_AlertLevel;
        private System.Windows.Forms.CheckBox Chk_Active;
        private System.Windows.Forms.Label lbl_VaultDetails;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label lbl_Manaufacturer;
        private System.Windows.Forms.ComboBox cmb_Manufacturer;
        private System.Windows.Forms.Label lbl_Type;
        private System.Windows.Forms.TextBox txt_Type;
        private System.Windows.Forms.Label lbl_Capacity;
        private NumberTextBox txt_Capacity;
        private System.Windows.Forms.Label lbl_NoofCassettes;
        private NumberTextBox txt_NoofCassettes;
        private NumberTextBox txt_Coinhoppers;
        private System.Windows.Forms.Label lbl_CoinHoppers;
        private System.Windows.Forms.CheckBox chk_WebserviceEnabled;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnHoppers;
        private System.Windows.Forms.Button btnCassettes;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.ListView lvVaultdetails;
        private System.Windows.Forms.ColumnHeader clmVaultName;
        private System.Windows.Forms.ColumnHeader clmSiteCode;
        private System.Windows.Forms.ColumnHeader clmSiteName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ContextMenuStrip mnuCopyAsset;
        private System.Windows.Forms.ToolStripMenuItem mnuSubitemCopy;
        private System.Windows.Forms.Label lblIsWebserviceEnabled;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lbl_VaultStatus;
        private System.Windows.Forms.ComboBox cbo_Status;
        private System.Windows.Forms.Button btn_Terminate;
        private System.Windows.Forms.ColumnHeader clmSerialNo;
        private System.Windows.Forms.TableLayoutPanel tlp_header;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Label lbl_StandardFillAmount;
        private NumberTextBox txt_StandardFillAmount;
        private System.Windows.Forms.ColumnHeader clmStatus;
        private System.Windows.Forms.Button btnRejection;
        private System.Windows.Forms.CheckBox chkAutoAdjust;
        private System.Windows.Forms.CheckBox chkRejectionFill;
    }
}