namespace BMC.EnterpriseClient.Views
{
    partial class UcAftsettings
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
            this.tblAftMainFram = new System.Windows.Forms.TableLayoutPanel();
            this.tblAftChkBoxs = new System.Windows.Forms.TableLayoutPanel();
            this.chkAllowAFTTransactions = new System.Windows.Forms.CheckBox();
            this.chkAllowOffers = new System.Windows.Forms.CheckBox();
            this.chkCashableDep = new System.Windows.Forms.CheckBox();
            this.chkAllowNoncashableDep = new System.Windows.Forms.CheckBox();
            this.chkAllowRedeemOffers = new System.Windows.Forms.CheckBox();
            this.chkAllowPointsWthdrawal = new System.Windows.Forms.CheckBox();
            this.chkAllowCashWithdrawal = new System.Windows.Forms.CheckBox();
            this.chkAllowPartialTransfer = new System.Windows.Forms.CheckBox();
            this.chkAutoDepositCashable = new System.Windows.Forms.CheckBox();
            this.chkAllowAutoDepositnon = new System.Windows.Forms.CheckBox();
            this.lblLegend = new System.Windows.Forms.Label();
            this.tblBaseDenom = new System.Windows.Forms.TableLayoutPanel();
            this.cmbBaseDenom = new System.Windows.Forms.ComboBox();
            this.lblBaseDenom = new System.Windows.Forms.Label();
            this.tblDenomValues = new System.Windows.Forms.TableLayoutPanel();
            this.lblOption2withdrawlamount = new System.Windows.Forms.Label();
            this.txtOption2WithDrawAmt = new System.Windows.Forms.TextBox();
            this.lblOption1withdrawlamount = new System.Windows.Forms.Label();
            this.txtOption1WithDrawAmt = new System.Windows.Forms.TextBox();
            this.lbloption3withdrawalamount = new System.Windows.Forms.Label();
            this.txtOption3WithDrawAmt = new System.Windows.Forms.TextBox();
            this.lbloption4withdrawalamount = new System.Windows.Forms.Label();
            this.txtOption4WithDrawAmt = new System.Windows.Forms.TextBox();
            this.lbloption5withdrawalamount = new System.Windows.Forms.Label();
            this.txtOption5WithDrawAmt = new System.Windows.Forms.TextBox();
            this.lblEFTtimeoutvalue = new System.Windows.Forms.Label();
            this.txtEFTTimeout = new System.Windows.Forms.TextBox();
            this.lblMaxdepositamount = new System.Windows.Forms.Label();
            this.lblMaxwithdrawalamount = new System.Windows.Forms.Label();
            this.txtMaxDepositAmt = new System.Windows.Forms.TextBox();
            this.txtMaxwithdrawAmt = new System.Windows.Forms.TextBox();
            this.tblButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnupdate = new System.Windows.Forms.Button();
            this.tblAftMainFram.SuspendLayout();
            this.tblAftChkBoxs.SuspendLayout();
            this.tblBaseDenom.SuspendLayout();
            this.tblDenomValues.SuspendLayout();
            this.tblButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblAftMainFram
            // 
            this.tblAftMainFram.ColumnCount = 1;
            this.tblAftMainFram.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftMainFram.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblAftMainFram.Controls.Add(this.tblAftChkBoxs, 0, 1);
            this.tblAftMainFram.Controls.Add(this.lblLegend, 0, 4);
            this.tblAftMainFram.Controls.Add(this.tblBaseDenom, 0, 0);
            this.tblAftMainFram.Controls.Add(this.tblDenomValues, 0, 2);
            this.tblAftMainFram.Controls.Add(this.tblButtons, 0, 3);
            this.tblAftMainFram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAftMainFram.Location = new System.Drawing.Point(0, 0);
            this.tblAftMainFram.Name = "tblAftMainFram";
            this.tblAftMainFram.RowCount = 5;
            this.tblAftMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblAftMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 267F));
            this.tblAftMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblAftMainFram.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblAftMainFram.Size = new System.Drawing.Size(373, 586);
            this.tblAftMainFram.TabIndex = 0;
            // 
            // tblAftChkBoxs
            // 
            this.tblAftChkBoxs.ColumnCount = 1;
            this.tblAftChkBoxs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftChkBoxs.Controls.Add(this.chkAllowAFTTransactions, 0, 0);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowOffers, 0, 1);
            this.tblAftChkBoxs.Controls.Add(this.chkCashableDep, 0, 2);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowNoncashableDep, 0, 3);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowRedeemOffers, 0, 4);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowPointsWthdrawal, 0, 5);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowCashWithdrawal, 0, 6);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowPartialTransfer, 0, 7);
            this.tblAftChkBoxs.Controls.Add(this.chkAutoDepositCashable, 0, 8);
            this.tblAftChkBoxs.Controls.Add(this.chkAllowAutoDepositnon, 0, 9);
            this.tblAftChkBoxs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAftChkBoxs.Location = new System.Drawing.Point(3, 33);
            this.tblAftChkBoxs.Name = "tblAftChkBoxs";
            this.tblAftChkBoxs.RowCount = 11;
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAftChkBoxs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblAftChkBoxs.Size = new System.Drawing.Size(367, 261);
            this.tblAftChkBoxs.TabIndex = 1;
            // 
            // chkAllowAFTTransactions
            // 
            this.chkAllowAFTTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowAFTTransactions.AutoSize = true;
            this.chkAllowAFTTransactions.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowAFTTransactions.Location = new System.Drawing.Point(3, 4);
            this.chkAllowAFTTransactions.Name = "chkAllowAFTTransactions";
            this.chkAllowAFTTransactions.Size = new System.Drawing.Size(361, 17);
            this.chkAllowAFTTransactions.TabIndex = 0;
            this.chkAllowAFTTransactions.Text = "AFT Transactions Allowed:";
            this.chkAllowAFTTransactions.UseVisualStyleBackColor = true;
            // 
            // chkAllowOffers
            // 
            this.chkAllowOffers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowOffers.AutoSize = true;
            this.chkAllowOffers.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowOffers.Location = new System.Drawing.Point(3, 29);
            this.chkAllowOffers.Name = "chkAllowOffers";
            this.chkAllowOffers.Size = new System.Drawing.Size(361, 17);
            this.chkAllowOffers.TabIndex = 1;
            this.chkAllowOffers.Text = "Allow Offers:";
            this.chkAllowOffers.UseVisualStyleBackColor = true;
            // 
            // chkCashableDep
            // 
            this.chkCashableDep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCashableDep.AutoSize = true;
            this.chkCashableDep.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkCashableDep.Location = new System.Drawing.Point(3, 54);
            this.chkCashableDep.Name = "chkCashableDep";
            this.chkCashableDep.Size = new System.Drawing.Size(361, 17);
            this.chkCashableDep.TabIndex = 2;
            this.chkCashableDep.Text = "Allow Cashable Deposits:";
            this.chkCashableDep.UseVisualStyleBackColor = true;
            // 
            // chkAllowNoncashableDep
            // 
            this.chkAllowNoncashableDep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowNoncashableDep.AutoSize = true;
            this.chkAllowNoncashableDep.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowNoncashableDep.Location = new System.Drawing.Point(3, 79);
            this.chkAllowNoncashableDep.Name = "chkAllowNoncashableDep";
            this.chkAllowNoncashableDep.Size = new System.Drawing.Size(361, 17);
            this.chkAllowNoncashableDep.TabIndex = 3;
            this.chkAllowNoncashableDep.Text = "Allow Non Cashable Deposits :";
            this.chkAllowNoncashableDep.UseVisualStyleBackColor = true;
            // 
            // chkAllowRedeemOffers
            // 
            this.chkAllowRedeemOffers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowRedeemOffers.AutoSize = true;
            this.chkAllowRedeemOffers.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowRedeemOffers.Location = new System.Drawing.Point(3, 104);
            this.chkAllowRedeemOffers.Name = "chkAllowRedeemOffers";
            this.chkAllowRedeemOffers.Size = new System.Drawing.Size(361, 17);
            this.chkAllowRedeemOffers.TabIndex = 4;
            this.chkAllowRedeemOffers.Text = "Allow Redeem Offers:";
            this.chkAllowRedeemOffers.UseVisualStyleBackColor = true;
            // 
            // chkAllowPointsWthdrawal
            // 
            this.chkAllowPointsWthdrawal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowPointsWthdrawal.AutoSize = true;
            this.chkAllowPointsWthdrawal.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowPointsWthdrawal.Location = new System.Drawing.Point(3, 129);
            this.chkAllowPointsWthdrawal.Name = "chkAllowPointsWthdrawal";
            this.chkAllowPointsWthdrawal.Size = new System.Drawing.Size(361, 17);
            this.chkAllowPointsWthdrawal.TabIndex = 5;
            this.chkAllowPointsWthdrawal.Text = "Allow Points Withdrawal:";
            this.chkAllowPointsWthdrawal.UseVisualStyleBackColor = true;
            // 
            // chkAllowCashWithdrawal
            // 
            this.chkAllowCashWithdrawal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowCashWithdrawal.AutoSize = true;
            this.chkAllowCashWithdrawal.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowCashWithdrawal.Location = new System.Drawing.Point(3, 154);
            this.chkAllowCashWithdrawal.Name = "chkAllowCashWithdrawal";
            this.chkAllowCashWithdrawal.Size = new System.Drawing.Size(361, 17);
            this.chkAllowCashWithdrawal.TabIndex = 6;
            this.chkAllowCashWithdrawal.Text = "Allow Cash Withdrawal:";
            this.chkAllowCashWithdrawal.UseVisualStyleBackColor = true;
            // 
            // chkAllowPartialTransfer
            // 
            this.chkAllowPartialTransfer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowPartialTransfer.AutoSize = true;
            this.chkAllowPartialTransfer.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowPartialTransfer.Location = new System.Drawing.Point(3, 179);
            this.chkAllowPartialTransfer.Name = "chkAllowPartialTransfer";
            this.chkAllowPartialTransfer.Size = new System.Drawing.Size(361, 17);
            this.chkAllowPartialTransfer.TabIndex = 7;
            this.chkAllowPartialTransfer.Text = "Allow Partial Transfers:";
            this.chkAllowPartialTransfer.UseVisualStyleBackColor = true;
            // 
            // chkAutoDepositCashable
            // 
            this.chkAutoDepositCashable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoDepositCashable.AutoSize = true;
            this.chkAutoDepositCashable.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAutoDepositCashable.Location = new System.Drawing.Point(3, 204);
            this.chkAutoDepositCashable.Name = "chkAutoDepositCashable";
            this.chkAutoDepositCashable.Size = new System.Drawing.Size(361, 17);
            this.chkAutoDepositCashable.TabIndex = 8;
            this.chkAutoDepositCashable.Text = "AutoDeposit Cashable Credits  On CardOut:";
            this.chkAutoDepositCashable.UseVisualStyleBackColor = true;
            // 
            // chkAllowAutoDepositnon
            // 
            this.chkAllowAutoDepositnon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllowAutoDepositnon.AutoSize = true;
            this.chkAllowAutoDepositnon.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkAllowAutoDepositnon.Location = new System.Drawing.Point(3, 229);
            this.chkAllowAutoDepositnon.Name = "chkAllowAutoDepositnon";
            this.chkAllowAutoDepositnon.Size = new System.Drawing.Size(361, 17);
            this.chkAllowAutoDepositnon.TabIndex = 9;
            this.chkAllowAutoDepositnon.Text = "AutoDeposit Non-Cashable Credits on CardOut:";
            this.chkAllowAutoDepositnon.UseVisualStyleBackColor = true;
            // 
            // lblLegend
            // 
            this.lblLegend.AutoSize = true;
            this.lblLegend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLegend.Location = new System.Drawing.Point(3, 556);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(367, 30);
            this.lblLegend.TabIndex = 4;
            this.lblLegend.Text = "Note: Max Withdrawal and Max Deposit Amount are in Dollars and other Withdrawal A" +
    "mount are in cents";
            // 
            // tblBaseDenom
            // 
            this.tblBaseDenom.ColumnCount = 2;
            this.tblBaseDenom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblBaseDenom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBaseDenom.Controls.Add(this.cmbBaseDenom, 1, 0);
            this.tblBaseDenom.Controls.Add(this.lblBaseDenom, 0, 0);
            this.tblBaseDenom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBaseDenom.Location = new System.Drawing.Point(3, 3);
            this.tblBaseDenom.Name = "tblBaseDenom";
            this.tblBaseDenom.RowCount = 1;
            this.tblBaseDenom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblBaseDenom.Size = new System.Drawing.Size(367, 24);
            this.tblBaseDenom.TabIndex = 0;
            // 
            // cmbBaseDenom
            // 
            this.cmbBaseDenom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbBaseDenom.FormattingEnabled = true;
            this.cmbBaseDenom.Location = new System.Drawing.Point(123, 3);
            this.cmbBaseDenom.MaxLength = 6;
            this.cmbBaseDenom.Name = "cmbBaseDenom";
            this.cmbBaseDenom.Size = new System.Drawing.Size(114, 21);
            this.cmbBaseDenom.TabIndex = 1;
            this.cmbBaseDenom.SelectedIndexChanged += new System.EventHandler(this.cmbBaseDenom_SelectedIndexChanged);
            this.cmbBaseDenom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AllowOnlyNumeric);
            // 
            // lblBaseDenom
            // 
            this.lblBaseDenom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBaseDenom.AutoSize = true;
            this.lblBaseDenom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaseDenom.Location = new System.Drawing.Point(3, 6);
            this.lblBaseDenom.Name = "lblBaseDenom";
            this.lblBaseDenom.Size = new System.Drawing.Size(91, 13);
            this.lblBaseDenom.TabIndex = 0;
            this.lblBaseDenom.Text = "* Base Denom:";
            // 
            // tblDenomValues
            // 
            this.tblDenomValues.ColumnCount = 2;
            this.tblDenomValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tblDenomValues.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblDenomValues.Controls.Add(this.lblOption2withdrawlamount, 0, 1);
            this.tblDenomValues.Controls.Add(this.txtOption2WithDrawAmt, 1, 1);
            this.tblDenomValues.Controls.Add(this.lblOption1withdrawlamount, 0, 0);
            this.tblDenomValues.Controls.Add(this.txtOption1WithDrawAmt, 1, 0);
            this.tblDenomValues.Controls.Add(this.lbloption3withdrawalamount, 0, 2);
            this.tblDenomValues.Controls.Add(this.txtOption3WithDrawAmt, 1, 2);
            this.tblDenomValues.Controls.Add(this.lbloption4withdrawalamount, 0, 3);
            this.tblDenomValues.Controls.Add(this.txtOption4WithDrawAmt, 1, 3);
            this.tblDenomValues.Controls.Add(this.lbloption5withdrawalamount, 0, 4);
            this.tblDenomValues.Controls.Add(this.txtOption5WithDrawAmt, 1, 4);
            this.tblDenomValues.Controls.Add(this.lblEFTtimeoutvalue, 0, 5);
            this.tblDenomValues.Controls.Add(this.txtEFTTimeout, 1, 5);
            this.tblDenomValues.Controls.Add(this.lblMaxdepositamount, 0, 6);
            this.tblDenomValues.Controls.Add(this.lblMaxwithdrawalamount, 0, 7);
            this.tblDenomValues.Controls.Add(this.txtMaxDepositAmt, 1, 6);
            this.tblDenomValues.Controls.Add(this.txtMaxwithdrawAmt, 1, 7);
            this.tblDenomValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblDenomValues.Location = new System.Drawing.Point(3, 300);
            this.tblDenomValues.Name = "tblDenomValues";
            this.tblDenomValues.RowCount = 9;
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblDenomValues.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblDenomValues.Size = new System.Drawing.Size(367, 213);
            this.tblDenomValues.TabIndex = 2;
            // 
            // lblOption2withdrawlamount
            // 
            this.lblOption2withdrawlamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOption2withdrawlamount.AutoSize = true;
            this.lblOption2withdrawlamount.Location = new System.Drawing.Point(3, 31);
            this.lblOption2withdrawlamount.Name = "lblOption2withdrawlamount";
            this.lblOption2withdrawlamount.Size = new System.Drawing.Size(159, 13);
            this.lblOption2withdrawlamount.TabIndex = 2;
            this.lblOption2withdrawlamount.Text = "* Option2 Withdrawal   Amount:";
            // 
            // txtOption2WithDrawAmt
            // 
            this.txtOption2WithDrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOption2WithDrawAmt.Location = new System.Drawing.Point(168, 28);
            this.txtOption2WithDrawAmt.MaxLength = 8;
            this.txtOption2WithDrawAmt.Name = "txtOption2WithDrawAmt";
            this.txtOption2WithDrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtOption2WithDrawAmt.TabIndex = 3;
            // 
            // lblOption1withdrawlamount
            // 
            this.lblOption1withdrawlamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOption1withdrawlamount.AutoSize = true;
            this.lblOption1withdrawlamount.Location = new System.Drawing.Point(3, 6);
            this.lblOption1withdrawlamount.Name = "lblOption1withdrawlamount";
            this.lblOption1withdrawlamount.Size = new System.Drawing.Size(159, 13);
            this.lblOption1withdrawlamount.TabIndex = 0;
            this.lblOption1withdrawlamount.Text = "* Option1 Withdrawal Amount:";
            // 
            // txtOption1WithDrawAmt
            // 
            this.txtOption1WithDrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOption1WithDrawAmt.Location = new System.Drawing.Point(168, 3);
            this.txtOption1WithDrawAmt.MaxLength = 8;
            this.txtOption1WithDrawAmt.Name = "txtOption1WithDrawAmt";
            this.txtOption1WithDrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtOption1WithDrawAmt.TabIndex = 1;
            // 
            // lbloption3withdrawalamount
            // 
            this.lbloption3withdrawalamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbloption3withdrawalamount.AutoSize = true;
            this.lbloption3withdrawalamount.Location = new System.Drawing.Point(3, 56);
            this.lbloption3withdrawalamount.Name = "lbloption3withdrawalamount";
            this.lbloption3withdrawalamount.Size = new System.Drawing.Size(159, 13);
            this.lbloption3withdrawalamount.TabIndex = 4;
            this.lbloption3withdrawalamount.Text = "* Option3 Withdrawal Amount:";
            // 
            // txtOption3WithDrawAmt
            // 
            this.txtOption3WithDrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOption3WithDrawAmt.Location = new System.Drawing.Point(168, 53);
            this.txtOption3WithDrawAmt.MaxLength = 8;
            this.txtOption3WithDrawAmt.Name = "txtOption3WithDrawAmt";
            this.txtOption3WithDrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtOption3WithDrawAmt.TabIndex = 5;
            // 
            // lbloption4withdrawalamount
            // 
            this.lbloption4withdrawalamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbloption4withdrawalamount.AutoSize = true;
            this.lbloption4withdrawalamount.Location = new System.Drawing.Point(3, 81);
            this.lbloption4withdrawalamount.Name = "lbloption4withdrawalamount";
            this.lbloption4withdrawalamount.Size = new System.Drawing.Size(159, 13);
            this.lbloption4withdrawalamount.TabIndex = 6;
            this.lbloption4withdrawalamount.Text = "* Option4 Withdrawal Amount:";
            // 
            // txtOption4WithDrawAmt
            // 
            this.txtOption4WithDrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOption4WithDrawAmt.Location = new System.Drawing.Point(168, 78);
            this.txtOption4WithDrawAmt.MaxLength = 8;
            this.txtOption4WithDrawAmt.Name = "txtOption4WithDrawAmt";
            this.txtOption4WithDrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtOption4WithDrawAmt.TabIndex = 7;
            // 
            // lbloption5withdrawalamount
            // 
            this.lbloption5withdrawalamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbloption5withdrawalamount.AutoSize = true;
            this.lbloption5withdrawalamount.Location = new System.Drawing.Point(3, 106);
            this.lbloption5withdrawalamount.Name = "lbloption5withdrawalamount";
            this.lbloption5withdrawalamount.Size = new System.Drawing.Size(159, 13);
            this.lbloption5withdrawalamount.TabIndex = 8;
            this.lbloption5withdrawalamount.Text = "* Option5 Withdrawal Amount:";
            // 
            // txtOption5WithDrawAmt
            // 
            this.txtOption5WithDrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOption5WithDrawAmt.Location = new System.Drawing.Point(168, 103);
            this.txtOption5WithDrawAmt.MaxLength = 8;
            this.txtOption5WithDrawAmt.Name = "txtOption5WithDrawAmt";
            this.txtOption5WithDrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtOption5WithDrawAmt.TabIndex = 9;
            // 
            // lblEFTtimeoutvalue
            // 
            this.lblEFTtimeoutvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEFTtimeoutvalue.AutoSize = true;
            this.lblEFTtimeoutvalue.Location = new System.Drawing.Point(3, 131);
            this.lblEFTtimeoutvalue.Name = "lblEFTtimeoutvalue";
            this.lblEFTtimeoutvalue.Size = new System.Drawing.Size(159, 13);
            this.lblEFTtimeoutvalue.TabIndex = 10;
            this.lblEFTtimeoutvalue.Text = "* EFT TimeOut Value :";
            // 
            // txtEFTTimeout
            // 
            this.txtEFTTimeout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEFTTimeout.Location = new System.Drawing.Point(168, 128);
            this.txtEFTTimeout.MaxLength = 3;
            this.txtEFTTimeout.Name = "txtEFTTimeout";
            this.txtEFTTimeout.Size = new System.Drawing.Size(196, 20);
            this.txtEFTTimeout.TabIndex = 11;
            // 
            // lblMaxdepositamount
            // 
            this.lblMaxdepositamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxdepositamount.AutoSize = true;
            this.lblMaxdepositamount.Location = new System.Drawing.Point(3, 156);
            this.lblMaxdepositamount.Name = "lblMaxdepositamount";
            this.lblMaxdepositamount.Size = new System.Drawing.Size(159, 13);
            this.lblMaxdepositamount.TabIndex = 12;
            this.lblMaxdepositamount.Text = "* MaxDeposit Amount:";
            // 
            // lblMaxwithdrawalamount
            // 
            this.lblMaxwithdrawalamount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxwithdrawalamount.AutoSize = true;
            this.lblMaxwithdrawalamount.Location = new System.Drawing.Point(3, 181);
            this.lblMaxwithdrawalamount.Name = "lblMaxwithdrawalamount";
            this.lblMaxwithdrawalamount.Size = new System.Drawing.Size(159, 13);
            this.lblMaxwithdrawalamount.TabIndex = 14;
            this.lblMaxwithdrawalamount.Text = "* Max Withdrawal Amount:";
            // 
            // txtMaxDepositAmt
            // 
            this.txtMaxDepositAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxDepositAmt.Location = new System.Drawing.Point(168, 153);
            this.txtMaxDepositAmt.MaxLength = 6;
            this.txtMaxDepositAmt.Name = "txtMaxDepositAmt";
            this.txtMaxDepositAmt.Size = new System.Drawing.Size(196, 20);
            this.txtMaxDepositAmt.TabIndex = 13;
            // 
            // txtMaxwithdrawAmt
            // 
            this.txtMaxwithdrawAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxwithdrawAmt.Location = new System.Drawing.Point(168, 178);
            this.txtMaxwithdrawAmt.MaxLength = 6;
            this.txtMaxwithdrawAmt.Name = "txtMaxwithdrawAmt";
            this.txtMaxwithdrawAmt.Size = new System.Drawing.Size(196, 20);
            this.txtMaxwithdrawAmt.TabIndex = 15;
            // 
            // tblButtons
            // 
            this.tblButtons.ColumnCount = 3;
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Controls.Add(this.btndelete, 1, 0);
            this.tblButtons.Controls.Add(this.btnupdate, 0, 0);
            this.tblButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblButtons.Location = new System.Drawing.Point(3, 519);
            this.tblButtons.Name = "tblButtons";
            this.tblButtons.RowCount = 1;
            this.tblButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblButtons.Size = new System.Drawing.Size(367, 34);
            this.tblButtons.TabIndex = 3;
            // 
            // btndelete
            // 
            this.btndelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btndelete.Location = new System.Drawing.Point(123, 3);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(100, 28);
            this.btndelete.TabIndex = 1;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click_1);
            // 
            // btnupdate
            // 
            this.btnupdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnupdate.Location = new System.Drawing.Point(3, 3);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(100, 28);
            this.btnupdate.TabIndex = 0;
            this.btnupdate.Text = "Update";
            this.btnupdate.UseVisualStyleBackColor = true;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click_1);
            // 
            // UcAftsettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblAftMainFram);
            this.Name = "UcAftsettings";
            this.Size = new System.Drawing.Size(373, 586);
            this.tblAftMainFram.ResumeLayout(false);
            this.tblAftMainFram.PerformLayout();
            this.tblAftChkBoxs.ResumeLayout(false);
            this.tblAftChkBoxs.PerformLayout();
            this.tblBaseDenom.ResumeLayout(false);
            this.tblBaseDenom.PerformLayout();
            this.tblDenomValues.ResumeLayout(false);
            this.tblDenomValues.PerformLayout();
            this.tblButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblAftMainFram;
        private System.Windows.Forms.TableLayoutPanel tblBaseDenom;
        private System.Windows.Forms.ComboBox cmbBaseDenom;
        private System.Windows.Forms.Label lblBaseDenom;
        private System.Windows.Forms.TableLayoutPanel tblAftChkBoxs;
        private System.Windows.Forms.Label lblLegend;
        private System.Windows.Forms.TableLayoutPanel tblDenomValues;
        private System.Windows.Forms.Label lblOption2withdrawlamount;
        private System.Windows.Forms.TextBox txtOption2WithDrawAmt;
        private System.Windows.Forms.Label lblOption1withdrawlamount;
        private System.Windows.Forms.TextBox txtOption1WithDrawAmt;
        private System.Windows.Forms.Label lbloption3withdrawalamount;
        private System.Windows.Forms.TextBox txtOption3WithDrawAmt;
        private System.Windows.Forms.Label lbloption4withdrawalamount;
        private System.Windows.Forms.TextBox txtOption4WithDrawAmt;
        private System.Windows.Forms.Label lbloption5withdrawalamount;
        private System.Windows.Forms.TextBox txtOption5WithDrawAmt;
        private System.Windows.Forms.Label lblEFTtimeoutvalue;
        private System.Windows.Forms.TextBox txtEFTTimeout;
        private System.Windows.Forms.Label lblMaxdepositamount;
        private System.Windows.Forms.Label lblMaxwithdrawalamount;
        private System.Windows.Forms.TextBox txtMaxDepositAmt;
        private System.Windows.Forms.TextBox txtMaxwithdrawAmt;
        private System.Windows.Forms.CheckBox chkAllowAFTTransactions;
        private System.Windows.Forms.CheckBox chkAllowOffers;
        private System.Windows.Forms.CheckBox chkCashableDep;
        private System.Windows.Forms.CheckBox chkAllowNoncashableDep;
        private System.Windows.Forms.CheckBox chkAllowRedeemOffers;
        private System.Windows.Forms.CheckBox chkAllowPointsWthdrawal;
        private System.Windows.Forms.CheckBox chkAllowCashWithdrawal;
        private System.Windows.Forms.CheckBox chkAllowPartialTransfer;
        private System.Windows.Forms.CheckBox chkAutoDepositCashable;
        private System.Windows.Forms.CheckBox chkAllowAutoDepositnon;
        private System.Windows.Forms.Button btnupdate;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.TableLayoutPanel tblButtons;
    }
}
