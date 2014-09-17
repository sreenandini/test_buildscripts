namespace BMC.EnterpriseClient.Views
{
    partial class frmManufacturer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManufacturer));
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkSCB = new System.Windows.Forms.CheckBox();
            this.btnViewMeterSet = new System.Windows.Forms.Button();
            this.grpAll = new System.Windows.Forms.GroupBox();
            this.lblServiceEmail = new System.Windows.Forms.Label();
            this.txtServiceTel = new System.Windows.Forms.TextBox();
            this.lblServiceTel = new System.Windows.Forms.Label();
            this.txtServiceContact = new System.Windows.Forms.TextBox();
            this.lblSales = new System.Windows.Forms.Label();
            this.lblServiceContact = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.txtServicePostcode = new System.Windows.Forms.TextBox();
            this.lblSalesAddress = new System.Windows.Forms.Label();
            this.lblServicePostcode = new System.Windows.Forms.Label();
            this.txtSalesAddress = new System.Windows.Forms.TextBox();
            this.lblSalesPostCode = new System.Windows.Forms.Label();
            this.lblServiceAddress = new System.Windows.Forms.Label();
            this.txtSalesPostcode = new System.Windows.Forms.TextBox();
            this.txtSalesEmail = new System.Windows.Forms.TextBox();
            this.lblSalesContact = new System.Windows.Forms.Label();
            this.lblSalesEmail = new System.Windows.Forms.Label();
            this.txtSalesContact = new System.Windows.Forms.TextBox();
            this.txtSalesTel = new System.Windows.Forms.TextBox();
            this.lblSalesTel = new System.Windows.Forms.Label();
            this.txtServiceEmail = new System.Windows.Forms.TextBox();
            this.txtServiceAddress = new System.Windows.Forms.TextBox();
            this.lblManufacturerCode = new System.Windows.Forms.Label();
            this.lbltManufacturerName = new System.Windows.Forms.Label();
            this.txtManufacturerName = new System.Windows.Forms.TextBox();
            this.txtManufacturerCode = new System.Windows.Forms.TextBox();
            this.lstManufacturers = new System.Windows.Forms.ListBox();
            this.grpSingleCoin = new System.Windows.Forms.GroupBox();
            this.chkCoinsIn = new System.Windows.Forms.CheckBox();
            this.chkHandpayAdded2CoinOut = new System.Windows.Forms.CheckBox();
            this.chkGamesWon = new System.Windows.Forms.CheckBox();
            this.chkCoinsOut = new System.Windows.Forms.CheckBox();
            this.chkGamesBet = new System.Windows.Forms.CheckBox();
            this.chkCoinsDrop = new System.Windows.Forms.CheckBox();
            this.chkNotes = new System.Windows.Forms.CheckBox();
            this.chkHandpay = new System.Windows.Forms.CheckBox();
            this.chkExternalCredits = new System.Windows.Forms.CheckBox();
            this.grpAll.SuspendLayout();
            this.grpSingleCoin.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(422, 374);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(75, 33);
            this.btnAddNew.TabIndex = 9;
            this.btnAddNew.Text = "&Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(508, 374);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 33);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(589, 374);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 33);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // chkSCB
            // 
            this.chkSCB.AutoSize = true;
            this.chkSCB.Location = new System.Drawing.Point(8, 378);
            this.chkSCB.Name = "chkSCB";
            this.chkSCB.Size = new System.Drawing.Size(105, 17);
            this.chkSCB.TabIndex = 7;
            this.chkSCB.Text = "Single Coin Build";
            this.chkSCB.UseVisualStyleBackColor = true;
            this.chkSCB.CheckedChanged += new System.EventHandler(this.ChkSCB_CheckedChanged);
            // 
            // btnViewMeterSet
            // 
            this.btnViewMeterSet.Location = new System.Drawing.Point(132, 374);
            this.btnViewMeterSet.Name = "btnViewMeterSet";
            this.btnViewMeterSet.Size = new System.Drawing.Size(104, 33);
            this.btnViewMeterSet.TabIndex = 8;
            this.btnViewMeterSet.Text = "&Open Meter Set";
            this.btnViewMeterSet.UseVisualStyleBackColor = true;
            this.btnViewMeterSet.Click += new System.EventHandler(this.BtnViewMeterSet_Click);
            // 
            // grpAll
            // 
            this.grpAll.Controls.Add(this.lblServiceEmail);
            this.grpAll.Controls.Add(this.txtServiceTel);
            this.grpAll.Controls.Add(this.lblServiceTel);
            this.grpAll.Controls.Add(this.txtServiceContact);
            this.grpAll.Controls.Add(this.lblSales);
            this.grpAll.Controls.Add(this.lblServiceContact);
            this.grpAll.Controls.Add(this.lblService);
            this.grpAll.Controls.Add(this.txtServicePostcode);
            this.grpAll.Controls.Add(this.lblSalesAddress);
            this.grpAll.Controls.Add(this.lblServicePostcode);
            this.grpAll.Controls.Add(this.txtSalesAddress);
            this.grpAll.Controls.Add(this.lblSalesPostCode);
            this.grpAll.Controls.Add(this.lblServiceAddress);
            this.grpAll.Controls.Add(this.txtSalesPostcode);
            this.grpAll.Controls.Add(this.txtSalesEmail);
            this.grpAll.Controls.Add(this.lblSalesContact);
            this.grpAll.Controls.Add(this.lblSalesEmail);
            this.grpAll.Controls.Add(this.txtSalesContact);
            this.grpAll.Controls.Add(this.txtSalesTel);
            this.grpAll.Controls.Add(this.lblSalesTel);
            this.grpAll.Controls.Add(this.txtServiceEmail);
            this.grpAll.Controls.Add(this.txtServiceAddress);
            this.grpAll.Location = new System.Drawing.Point(243, 2);
            this.grpAll.Name = "grpAll";
            this.grpAll.Size = new System.Drawing.Size(421, 364);
            this.grpAll.TabIndex = 1;
            this.grpAll.TabStop = false;
            // 
            // lblServiceEmail
            // 
            this.lblServiceEmail.AutoSize = true;
            this.lblServiceEmail.Location = new System.Drawing.Point(212, 309);
            this.lblServiceEmail.Name = "lblServiceEmail";
            this.lblServiceEmail.Size = new System.Drawing.Size(39, 13);
            this.lblServiceEmail.TabIndex = 19;
            this.lblServiceEmail.Text = "E-Mail:";
            // 
            // txtServiceTel
            // 
            this.txtServiceTel.Location = new System.Drawing.Point(215, 282);
            this.txtServiceTel.MaxLength = 50;
            this.txtServiceTel.Name = "txtServiceTel";
            this.txtServiceTel.Size = new System.Drawing.Size(198, 20);
            this.txtServiceTel.TabIndex = 17;
            this.txtServiceTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblServiceTel
            // 
            this.lblServiceTel.AutoSize = true;
            this.lblServiceTel.Location = new System.Drawing.Point(212, 266);
            this.lblServiceTel.Name = "lblServiceTel";
            this.lblServiceTel.Size = new System.Drawing.Size(25, 13);
            this.lblServiceTel.TabIndex = 15;
            this.lblServiceTel.Text = "Tel:";
            // 
            // txtServiceContact
            // 
            this.txtServiceContact.AcceptsReturn = true;
            this.txtServiceContact.Location = new System.Drawing.Point(215, 239);
            this.txtServiceContact.MaxLength = 50;
            this.txtServiceContact.Name = "txtServiceContact";
            this.txtServiceContact.Size = new System.Drawing.Size(198, 20);
            this.txtServiceContact.TabIndex = 13;
            this.txtServiceContact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblSales
            // 
            this.lblSales.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSales.Location = new System.Drawing.Point(8, 65);
            this.lblSales.Name = "lblSales";
            this.lblSales.Size = new System.Drawing.Size(198, 15);
            this.lblSales.TabIndex = 0;
            this.lblSales.Text = "Sales";
            this.lblSales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServiceContact
            // 
            this.lblServiceContact.AutoSize = true;
            this.lblServiceContact.Location = new System.Drawing.Point(212, 223);
            this.lblServiceContact.Name = "lblServiceContact";
            this.lblServiceContact.Size = new System.Drawing.Size(47, 13);
            this.lblServiceContact.TabIndex = 12;
            this.lblServiceContact.Text = "Contact:";
            // 
            // lblService
            // 
            this.lblService.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblService.Location = new System.Drawing.Point(215, 65);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(198, 15);
            this.lblService.TabIndex = 1;
            this.lblService.Text = "Service";
            this.lblService.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtServicePostcode
            // 
            this.txtServicePostcode.Location = new System.Drawing.Point(215, 196);
            this.txtServicePostcode.MaxLength = 10;
            this.txtServicePostcode.Name = "txtServicePostcode";
            this.txtServicePostcode.Size = new System.Drawing.Size(198, 20);
            this.txtServicePostcode.TabIndex = 9;
            this.txtServicePostcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblSalesAddress
            // 
            this.lblSalesAddress.AutoSize = true;
            this.lblSalesAddress.Location = new System.Drawing.Point(5, 89);
            this.lblSalesAddress.Name = "lblSalesAddress";
            this.lblSalesAddress.Size = new System.Drawing.Size(48, 13);
            this.lblSalesAddress.TabIndex = 2;
            this.lblSalesAddress.Text = "Address:";
            // 
            // lblServicePostcode
            // 
            this.lblServicePostcode.AutoSize = true;
            this.lblServicePostcode.Location = new System.Drawing.Point(212, 180);
            this.lblServicePostcode.Name = "lblServicePostcode";
            this.lblServicePostcode.Size = new System.Drawing.Size(55, 13);
            this.lblServicePostcode.TabIndex = 8;
            this.lblServicePostcode.Text = "Postcode:";
            // 
            // txtSalesAddress
            // 
            this.txtSalesAddress.Location = new System.Drawing.Point(8, 105);
            this.txtSalesAddress.MaxLength = 250;
            this.txtSalesAddress.Multiline = true;
            this.txtSalesAddress.Name = "txtSalesAddress";
            this.txtSalesAddress.Size = new System.Drawing.Size(198, 69);
            this.txtSalesAddress.TabIndex = 3;
            this.txtSalesAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblSalesPostCode
            // 
            this.lblSalesPostCode.AutoSize = true;
            this.lblSalesPostCode.Location = new System.Drawing.Point(5, 180);
            this.lblSalesPostCode.Name = "lblSalesPostCode";
            this.lblSalesPostCode.Size = new System.Drawing.Size(55, 13);
            this.lblSalesPostCode.TabIndex = 6;
            this.lblSalesPostCode.Text = "Postcode:";
            // 
            // lblServiceAddress
            // 
            this.lblServiceAddress.AutoSize = true;
            this.lblServiceAddress.Location = new System.Drawing.Point(212, 89);
            this.lblServiceAddress.Name = "lblServiceAddress";
            this.lblServiceAddress.Size = new System.Drawing.Size(48, 13);
            this.lblServiceAddress.TabIndex = 4;
            this.lblServiceAddress.Text = "Address:";
            // 
            // txtSalesPostcode
            // 
            this.txtSalesPostcode.Location = new System.Drawing.Point(8, 196);
            this.txtSalesPostcode.MaxLength = 10;
            this.txtSalesPostcode.Name = "txtSalesPostcode";
            this.txtSalesPostcode.Size = new System.Drawing.Size(198, 20);
            this.txtSalesPostcode.TabIndex = 7;
            this.txtSalesPostcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // txtSalesEmail
            // 
            this.txtSalesEmail.Location = new System.Drawing.Point(8, 325);
            this.txtSalesEmail.MaxLength = 50;
            this.txtSalesEmail.Name = "txtSalesEmail";
            this.txtSalesEmail.Size = new System.Drawing.Size(198, 20);
            this.txtSalesEmail.TabIndex = 20;
            this.txtSalesEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblSalesContact
            // 
            this.lblSalesContact.AutoSize = true;
            this.lblSalesContact.Location = new System.Drawing.Point(5, 223);
            this.lblSalesContact.Name = "lblSalesContact";
            this.lblSalesContact.Size = new System.Drawing.Size(47, 13);
            this.lblSalesContact.TabIndex = 10;
            this.lblSalesContact.Text = "Contact:";
            // 
            // lblSalesEmail
            // 
            this.lblSalesEmail.AutoSize = true;
            this.lblSalesEmail.Location = new System.Drawing.Point(5, 309);
            this.lblSalesEmail.Name = "lblSalesEmail";
            this.lblSalesEmail.Size = new System.Drawing.Size(39, 13);
            this.lblSalesEmail.TabIndex = 18;
            this.lblSalesEmail.Text = "E-Mail:";
            // 
            // txtSalesContact
            // 
            this.txtSalesContact.Location = new System.Drawing.Point(8, 239);
            this.txtSalesContact.MaxLength = 50;
            this.txtSalesContact.Name = "txtSalesContact";
            this.txtSalesContact.Size = new System.Drawing.Size(198, 20);
            this.txtSalesContact.TabIndex = 11;
            this.txtSalesContact.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // txtSalesTel
            // 
            this.txtSalesTel.Location = new System.Drawing.Point(8, 282);
            this.txtSalesTel.MaxLength = 50;
            this.txtSalesTel.Name = "txtSalesTel";
            this.txtSalesTel.Size = new System.Drawing.Size(198, 20);
            this.txtSalesTel.TabIndex = 16;
            this.txtSalesTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblSalesTel
            // 
            this.lblSalesTel.AutoSize = true;
            this.lblSalesTel.Location = new System.Drawing.Point(5, 266);
            this.lblSalesTel.Name = "lblSalesTel";
            this.lblSalesTel.Size = new System.Drawing.Size(25, 13);
            this.lblSalesTel.TabIndex = 14;
            this.lblSalesTel.Text = "Tel:";
            // 
            // txtServiceEmail
            // 
            this.txtServiceEmail.Location = new System.Drawing.Point(215, 325);
            this.txtServiceEmail.MaxLength = 50;
            this.txtServiceEmail.Name = "txtServiceEmail";
            this.txtServiceEmail.Size = new System.Drawing.Size(198, 20);
            this.txtServiceEmail.TabIndex = 21;
            this.txtServiceEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // txtServiceAddress
            // 
            this.txtServiceAddress.Location = new System.Drawing.Point(215, 105);
            this.txtServiceAddress.MaxLength = 250;
            this.txtServiceAddress.Multiline = true;
            this.txtServiceAddress.Name = "txtServiceAddress";
            this.txtServiceAddress.Size = new System.Drawing.Size(198, 69);
            this.txtServiceAddress.TabIndex = 5;
            this.txtServiceAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lblManufacturerCode
            // 
            this.lblManufacturerCode.AutoSize = true;
            this.lblManufacturerCode.Location = new System.Drawing.Point(459, 11);
            this.lblManufacturerCode.Name = "lblManufacturerCode";
            this.lblManufacturerCode.Size = new System.Drawing.Size(35, 13);
            this.lblManufacturerCode.TabIndex = 4;
            this.lblManufacturerCode.Text = "Code:";
            // 
            // lbltManufacturerName
            // 
            this.lbltManufacturerName.AutoSize = true;
            this.lbltManufacturerName.Location = new System.Drawing.Point(248, 11);
            this.lbltManufacturerName.Name = "lbltManufacturerName";
            this.lbltManufacturerName.Size = new System.Drawing.Size(80, 13);
            this.lbltManufacturerName.TabIndex = 2;
            this.lbltManufacturerName.Text = "* Manufacturer:";
            // 
            // txtManufacturerName
            // 
            this.txtManufacturerName.Location = new System.Drawing.Point(249, 31);
            this.txtManufacturerName.MaxLength = 50;
            this.txtManufacturerName.Name = "txtManufacturerName";
            this.txtManufacturerName.Size = new System.Drawing.Size(198, 20);
            this.txtManufacturerName.TabIndex = 3;
            this.txtManufacturerName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // txtManufacturerCode
            // 
            this.txtManufacturerCode.Location = new System.Drawing.Point(460, 31);
            this.txtManufacturerCode.MaxLength = 10;
            this.txtManufacturerCode.Name = "txtManufacturerCode";
            this.txtManufacturerCode.Size = new System.Drawing.Size(198, 20);
            this.txtManufacturerCode.TabIndex = 5;
            this.txtManufacturerCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChkValidText_KeyPress);
            // 
            // lstManufacturers
            // 
            this.lstManufacturers.FormattingEnabled = true;
            this.lstManufacturers.Location = new System.Drawing.Point(8, 11);
            this.lstManufacturers.Name = "lstManufacturers";
            this.lstManufacturers.Size = new System.Drawing.Size(228, 355);
            this.lstManufacturers.Sorted = true;
            this.lstManufacturers.TabIndex = 0;
            this.lstManufacturers.SelectedIndexChanged += new System.EventHandler(this.lstManufacturers_SelectedIndexChanged);
            // 
            // grpSingleCoin
            // 
            this.grpSingleCoin.Controls.Add(this.chkCoinsIn);
            this.grpSingleCoin.Controls.Add(this.chkHandpayAdded2CoinOut);
            this.grpSingleCoin.Controls.Add(this.chkGamesWon);
            this.grpSingleCoin.Controls.Add(this.chkCoinsOut);
            this.grpSingleCoin.Controls.Add(this.chkGamesBet);
            this.grpSingleCoin.Controls.Add(this.chkCoinsDrop);
            this.grpSingleCoin.Controls.Add(this.chkNotes);
            this.grpSingleCoin.Controls.Add(this.chkHandpay);
            this.grpSingleCoin.Controls.Add(this.chkExternalCredits);
            this.grpSingleCoin.Location = new System.Drawing.Point(250, 57);
            this.grpSingleCoin.Name = "grpSingleCoin";
            this.grpSingleCoin.Size = new System.Drawing.Size(408, 301);
            this.grpSingleCoin.TabIndex = 6;
            this.grpSingleCoin.TabStop = false;
            // 
            // chkCoinsIn
            // 
            this.chkCoinsIn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCoinsIn.Location = new System.Drawing.Point(55, 68);
            this.chkCoinsIn.Name = "chkCoinsIn";
            this.chkCoinsIn.Size = new System.Drawing.Size(110, 17);
            this.chkCoinsIn.TabIndex = 0;
            this.chkCoinsIn.Text = "Coins In";
            this.chkCoinsIn.UseVisualStyleBackColor = true;
            // 
            // chkHandpayAdded2CoinOut
            // 
            this.chkHandpayAdded2CoinOut.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHandpayAdded2CoinOut.Location = new System.Drawing.Point(93, 254);
            this.chkHandpayAdded2CoinOut.Name = "chkHandpayAdded2CoinOut";
            this.chkHandpayAdded2CoinOut.Size = new System.Drawing.Size(236, 17);
            this.chkHandpayAdded2CoinOut.TabIndex = 8;
            this.chkHandpayAdded2CoinOut.Text = "Handpay added to Coins Out Meter";
            this.chkHandpayAdded2CoinOut.UseVisualStyleBackColor = true;
            // 
            // chkGamesWon
            // 
            this.chkGamesWon.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGamesWon.Location = new System.Drawing.Point(263, 199);
            this.chkGamesWon.Name = "chkGamesWon";
            this.chkGamesWon.Size = new System.Drawing.Size(110, 17);
            this.chkGamesWon.TabIndex = 7;
            this.chkGamesWon.Text = "Games Won";
            this.chkGamesWon.UseVisualStyleBackColor = true;
            // 
            // chkCoinsOut
            // 
            this.chkCoinsOut.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCoinsOut.Location = new System.Drawing.Point(263, 71);
            this.chkCoinsOut.Name = "chkCoinsOut";
            this.chkCoinsOut.Size = new System.Drawing.Size(110, 17);
            this.chkCoinsOut.TabIndex = 2;
            this.chkCoinsOut.Text = "Coins Out";
            this.chkCoinsOut.UseVisualStyleBackColor = true;
            // 
            // chkGamesBet
            // 
            this.chkGamesBet.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGamesBet.Location = new System.Drawing.Point(55, 199);
            this.chkGamesBet.Name = "chkGamesBet";
            this.chkGamesBet.Size = new System.Drawing.Size(110, 17);
            this.chkGamesBet.TabIndex = 5;
            this.chkGamesBet.Text = "Games Bet";
            this.chkGamesBet.UseVisualStyleBackColor = true;
            // 
            // chkCoinsDrop
            // 
            this.chkCoinsDrop.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCoinsDrop.Location = new System.Drawing.Point(55, 111);
            this.chkCoinsDrop.Name = "chkCoinsDrop";
            this.chkCoinsDrop.Size = new System.Drawing.Size(110, 17);
            this.chkCoinsDrop.TabIndex = 1;
            this.chkCoinsDrop.Text = "Coins Drop";
            this.chkCoinsDrop.UseVisualStyleBackColor = true;
            // 
            // chkNotes
            // 
            this.chkNotes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNotes.Location = new System.Drawing.Point(263, 156);
            this.chkNotes.Name = "chkNotes";
            this.chkNotes.Size = new System.Drawing.Size(110, 17);
            this.chkNotes.TabIndex = 6;
            this.chkNotes.Text = "Notes";
            this.chkNotes.UseVisualStyleBackColor = true;
            // 
            // chkHandpay
            // 
            this.chkHandpay.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkHandpay.Location = new System.Drawing.Point(263, 111);
            this.chkHandpay.Name = "chkHandpay";
            this.chkHandpay.Size = new System.Drawing.Size(110, 17);
            this.chkHandpay.TabIndex = 3;
            this.chkHandpay.Text = "Handpay";
            this.chkHandpay.UseVisualStyleBackColor = true;
            // 
            // chkExternalCredits
            // 
            this.chkExternalCredits.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkExternalCredits.Location = new System.Drawing.Point(55, 156);
            this.chkExternalCredits.Name = "chkExternalCredits";
            this.chkExternalCredits.Size = new System.Drawing.Size(110, 17);
            this.chkExternalCredits.TabIndex = 4;
            this.chkExternalCredits.Text = "External Credits";
            this.chkExternalCredits.UseVisualStyleBackColor = true;
            // 
            // frmManufacturer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 415);
            this.Controls.Add(this.lbltManufacturerName);
            this.Controls.Add(this.lstManufacturers);
            this.Controls.Add(this.lblManufacturerCode);
            this.Controls.Add(this.btnViewMeterSet);
            this.Controls.Add(this.txtManufacturerName);
            this.Controls.Add(this.chkSCB);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtManufacturerCode);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.grpAll);
            this.Controls.Add(this.grpSingleCoin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManufacturer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManufacturerForm";
            this.Load += new System.EventHandler(this.ManufacturerForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManufacturer_FormClosing);
            this.grpAll.ResumeLayout(false);
            this.grpAll.PerformLayout();
            this.grpSingleCoin.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkSCB;
        private System.Windows.Forms.Button btnViewMeterSet;
        private System.Windows.Forms.GroupBox grpAll;
        private System.Windows.Forms.TextBox txtServiceEmail;
        private System.Windows.Forms.Label lbltManufacturerName;
        private System.Windows.Forms.Label lblServiceEmail;
        private System.Windows.Forms.TextBox txtManufacturerName;
        private System.Windows.Forms.TextBox txtServiceTel;
        private System.Windows.Forms.Label lblManufacturerCode;
        private System.Windows.Forms.Label lblServiceTel;
        private System.Windows.Forms.TextBox txtManufacturerCode;
        private System.Windows.Forms.TextBox txtServiceContact;
        private System.Windows.Forms.Label lblSales;
        private System.Windows.Forms.Label lblServiceContact;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.TextBox txtServicePostcode;
        private System.Windows.Forms.Label lblSalesAddress;
        private System.Windows.Forms.Label lblServicePostcode;
        private System.Windows.Forms.TextBox txtSalesAddress;
        private System.Windows.Forms.TextBox txtServiceAddress;
        private System.Windows.Forms.Label lblSalesPostCode;
        private System.Windows.Forms.Label lblServiceAddress;
        private System.Windows.Forms.TextBox txtSalesPostcode;
        private System.Windows.Forms.TextBox txtSalesEmail;
        private System.Windows.Forms.Label lblSalesContact;
        private System.Windows.Forms.Label lblSalesEmail;
        private System.Windows.Forms.TextBox txtSalesContact;
        private System.Windows.Forms.TextBox txtSalesTel;
        private System.Windows.Forms.Label lblSalesTel;
        private System.Windows.Forms.ListBox lstManufacturers;
        private System.Windows.Forms.GroupBox grpSingleCoin;
        private System.Windows.Forms.CheckBox chkHandpayAdded2CoinOut;
        private System.Windows.Forms.CheckBox chkCoinsIn;
        private System.Windows.Forms.CheckBox chkGamesWon;
        private System.Windows.Forms.CheckBox chkCoinsOut;
        private System.Windows.Forms.CheckBox chkGamesBet;
        private System.Windows.Forms.CheckBox chkCoinsDrop;
        private System.Windows.Forms.CheckBox chkNotes;
        private System.Windows.Forms.CheckBox chkHandpay;
        private System.Windows.Forms.CheckBox chkExternalCredits;
    }
}