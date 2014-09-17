namespace BMC.EnterpriseClient.Views
{
    partial class LiquidationDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiquidationDetails));
            this.lblRetailer = new System.Windows.Forms.Label();
            this.lblBalanceDue = new System.Windows.Forms.Label();
            this.txtRetailer = new System.Windows.Forms.TextBox();
            this.txtBalanceDue = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnProfitShare = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateCollected = new System.Windows.Forms.Label();
            this.txtDateCollected = new System.Windows.Forms.TextBox();
            this.lblRetailerName = new System.Windows.Forms.Label();
            this.txtRetailerName = new System.Windows.Forms.TextBox();
            this.lblGross = new System.Windows.Forms.Label();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.lblTicketsExpected = new System.Windows.Forms.Label();
            this.txtTicketsPaid = new System.Windows.Forms.TextBox();
            this.txtTicketsExpected = new System.Windows.Forms.TextBox();
            this.lblAdvancetoRetailer = new System.Windows.Forms.Label();
            this.txtRetailerShare = new System.Windows.Forms.TextBox();
            this.lblNet = new System.Windows.Forms.Label();
            this.txtRetailerNegativeNet = new System.Windows.Forms.TextBox();
            this.lblTicketsPaid = new System.Windows.Forms.Label();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.txtNetValue = new System.Windows.Forms.TextBox();
            this.lblNet22 = new System.Windows.Forms.Label();
            this.lblRetailerShare = new System.Windows.Forms.Label();
            this.lblRetailersNegativeNet = new System.Windows.Forms.Label();
            this.lblFixedExpense = new System.Windows.Forms.Label();
            this.txtFixedExpense = new System.Windows.Forms.TextBox();
            this.lblRetailerSharebeforeFixedExpense = new System.Windows.Forms.Label();
            this.txtRetailerSharebeforeFixedExpense = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtAdvanceToRetailer = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRetailer
            // 
            this.lblRetailer.AutoSize = true;
            this.lblRetailer.Location = new System.Drawing.Point(6, 377);
            this.lblRetailer.Name = "lblRetailer";
            this.lblRetailer.Size = new System.Drawing.Size(49, 13);
            this.lblRetailer.TabIndex = 2;
            this.lblRetailer.Text = "Retailer :";
            // 
            // lblBalanceDue
            // 
            this.lblBalanceDue.AutoSize = true;
            this.lblBalanceDue.Location = new System.Drawing.Point(275, 378);
            this.lblBalanceDue.Name = "lblBalanceDue";
            this.lblBalanceDue.Size = new System.Drawing.Size(72, 13);
            this.lblBalanceDue.TabIndex = 4;
            this.lblBalanceDue.Text = "Balance Due:";
            // 
            // txtRetailer
            // 
            this.txtRetailer.Location = new System.Drawing.Point(61, 374);
            this.txtRetailer.Name = "txtRetailer";
            this.txtRetailer.ReadOnly = true;
            this.txtRetailer.Size = new System.Drawing.Size(193, 20);
            this.txtRetailer.TabIndex = 3;
            // 
            // txtBalanceDue
            // 
            this.txtBalanceDue.Location = new System.Drawing.Point(356, 374);
            this.txtBalanceDue.Name = "txtBalanceDue";
            this.txtBalanceDue.ReadOnly = true;
            this.txtBalanceDue.Size = new System.Drawing.Size(173, 20);
            this.txtBalanceDue.TabIndex = 5;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfirm.Location = new System.Drawing.Point(118, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(94, 37);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnProfitShare
            // 
            this.btnProfitShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfitShare.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnProfitShare.Location = new System.Drawing.Point(18, 0);
            this.btnProfitShare.Name = "btnProfitShare";
            this.btnProfitShare.Size = new System.Drawing.Size(94, 37);
            this.btnProfitShare.TabIndex = 0;
            this.btnProfitShare.Text = "Profit Share";
            this.btnProfitShare.UseVisualStyleBackColor = true;
            this.btnProfitShare.Click += new System.EventHandler(this.btnProfitShare_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblDateCollected, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDateCollected, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblGross, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtGross, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTicketsExpected, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtTicketsPaid, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtTicketsExpected, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblAdvancetoRetailer, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerShare, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblNet, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerNegativeNet, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblTicketsPaid, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtNet, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtNetValue, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblNet22, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerShare, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailersNegativeNet, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblFixedExpense, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtFixedExpense, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.txtAdvanceToRetailer, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.lblRetailerSharebeforeFixedExpense, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtRetailerSharebeforeFixedExpense, 1, 10);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(532, 366);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblDateCollected
            // 
            this.lblDateCollected.AutoSize = true;
            this.lblDateCollected.Location = new System.Drawing.Point(3, 0);
            this.lblDateCollected.Name = "lblDateCollected";
            this.lblDateCollected.Size = new System.Drawing.Size(80, 13);
            this.lblDateCollected.TabIndex = 0;
            this.lblDateCollected.Text = "Date Collected:";
            // 
            // txtDateCollected
            // 
            this.txtDateCollected.Location = new System.Drawing.Point(269, 3);
            this.txtDateCollected.Name = "txtDateCollected";
            this.txtDateCollected.ReadOnly = true;
            this.txtDateCollected.Size = new System.Drawing.Size(257, 20);
            this.txtDateCollected.TabIndex = 1;
            // 
            // lblRetailerName
            // 
            this.lblRetailerName.AutoSize = true;
            this.lblRetailerName.Location = new System.Drawing.Point(3, 30);
            this.lblRetailerName.Name = "lblRetailerName";
            this.lblRetailerName.Size = new System.Drawing.Size(77, 13);
            this.lblRetailerName.TabIndex = 2;
            this.lblRetailerName.Text = "Retailer Name:";
            // 
            // txtRetailerName
            // 
            this.txtRetailerName.Location = new System.Drawing.Point(269, 33);
            this.txtRetailerName.Name = "txtRetailerName";
            this.txtRetailerName.ReadOnly = true;
            this.txtRetailerName.Size = new System.Drawing.Size(257, 20);
            this.txtRetailerName.TabIndex = 3;
            // 
            // lblGross
            // 
            this.lblGross.AutoSize = true;
            this.lblGross.Location = new System.Drawing.Point(3, 60);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(92, 13);
            this.lblGross.TabIndex = 4;
            this.lblGross.Text = "Total Declared In:";
            // 
            // txtGross
            // 
            this.txtGross.Location = new System.Drawing.Point(269, 63);
            this.txtGross.Name = "txtGross";
            this.txtGross.ReadOnly = true;
            this.txtGross.Size = new System.Drawing.Size(257, 20);
            this.txtGross.TabIndex = 5;
            // 
            // lblTicketsExpected
            // 
            this.lblTicketsExpected.AutoSize = true;
            this.lblTicketsExpected.Location = new System.Drawing.Point(3, 90);
            this.lblTicketsExpected.Name = "lblTicketsExpected";
            this.lblTicketsExpected.Size = new System.Drawing.Size(100, 13);
            this.lblTicketsExpected.TabIndex = 6;
            this.lblTicketsExpected.Text = "Total Declared Out:";
            // 
            // txtTicketsPaid
            // 
            this.txtTicketsPaid.Location = new System.Drawing.Point(269, 243);
            this.txtTicketsPaid.Name = "txtTicketsPaid";
            this.txtTicketsPaid.ReadOnly = true;
            this.txtTicketsPaid.Size = new System.Drawing.Size(257, 20);
            this.txtTicketsPaid.TabIndex = 17;
            // 
            // txtTicketsExpected
            // 
            this.txtTicketsExpected.Location = new System.Drawing.Point(269, 93);
            this.txtTicketsExpected.Name = "txtTicketsExpected";
            this.txtTicketsExpected.ReadOnly = true;
            this.txtTicketsExpected.Size = new System.Drawing.Size(257, 20);
            this.txtTicketsExpected.TabIndex = 7;
            // 
            // lblAdvancetoRetailer
            // 
            this.lblAdvancetoRetailer.AutoSize = true;
            this.lblAdvancetoRetailer.Location = new System.Drawing.Point(3, 270);
            this.lblAdvancetoRetailer.Name = "lblAdvancetoRetailer";
            this.lblAdvancetoRetailer.Size = new System.Drawing.Size(107, 13);
            this.lblAdvancetoRetailer.TabIndex = 18;
            this.lblAdvancetoRetailer.Text = "Advance to Retailer :";
            // 
            // txtRetailerShare
            // 
            this.txtRetailerShare.Location = new System.Drawing.Point(269, 213);
            this.txtRetailerShare.Name = "txtRetailerShare";
            this.txtRetailerShare.ReadOnly = true;
            this.txtRetailerShare.Size = new System.Drawing.Size(257, 20);
            this.txtRetailerShare.TabIndex = 15;
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.lblNet.Location = new System.Drawing.Point(3, 120);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(27, 13);
            this.lblNet.TabIndex = 8;
            this.lblNet.Text = "Net:";
            // 
            // txtRetailerNegativeNet
            // 
            this.txtRetailerNegativeNet.Location = new System.Drawing.Point(269, 183);
            this.txtRetailerNegativeNet.Name = "txtRetailerNegativeNet";
            this.txtRetailerNegativeNet.ReadOnly = true;
            this.txtRetailerNegativeNet.Size = new System.Drawing.Size(257, 20);
            this.txtRetailerNegativeNet.TabIndex = 13;
            // 
            // lblTicketsPaid
            // 
            this.lblTicketsPaid.AutoSize = true;
            this.lblTicketsPaid.Location = new System.Drawing.Point(3, 240);
            this.lblTicketsPaid.Name = "lblTicketsPaid";
            this.lblTicketsPaid.Size = new System.Drawing.Size(79, 13);
            this.lblTicketsPaid.TabIndex = 16;
            this.lblTicketsPaid.Text = "Vouchers Paid:";
            // 
            // txtNet
            // 
            this.txtNet.Location = new System.Drawing.Point(269, 123);
            this.txtNet.Name = "txtNet";
            this.txtNet.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(257, 20);
            this.txtNet.TabIndex = 9;
            // 
            // txtNetValue
            // 
            this.txtNetValue.Location = new System.Drawing.Point(269, 153);
            this.txtNetValue.Name = "txtNetValue";
            this.txtNetValue.ReadOnly = true;
            this.txtNetValue.Size = new System.Drawing.Size(257, 20);
            this.txtNetValue.TabIndex = 11;
            // 
            // lblNet22
            // 
            this.lblNet22.AutoSize = true;
            this.lblNet22.Location = new System.Drawing.Point(3, 150);
            this.lblNet22.Name = "lblNet22";
            this.lblNet22.Size = new System.Drawing.Size(31, 13);
            this.lblNet22.TabIndex = 10;
            this.lblNet22.Text = "Net *";
            // 
            // lblRetailerShare
            // 
            this.lblRetailerShare.AutoSize = true;
            this.lblRetailerShare.Location = new System.Drawing.Point(3, 210);
            this.lblRetailerShare.Name = "lblRetailerShare";
            this.lblRetailerShare.Size = new System.Drawing.Size(77, 13);
            this.lblRetailerShare.TabIndex = 14;
            this.lblRetailerShare.Text = "Retailer Share:";
            // 
            // lblRetailersNegativeNet
            // 
            this.lblRetailersNegativeNet.AutoSize = true;
            this.lblRetailersNegativeNet.Location = new System.Drawing.Point(3, 180);
            this.lblRetailersNegativeNet.Name = "lblRetailersNegativeNet";
            this.lblRetailersNegativeNet.Size = new System.Drawing.Size(204, 13);
            this.lblRetailersNegativeNet.TabIndex = 12;
            this.lblRetailersNegativeNet.Text = "Retailer\'s Negative Net : (Prior  Collection)";
            // 
            // lblFixedExpense
            // 
            this.lblFixedExpense.AutoSize = true;
            this.lblFixedExpense.Location = new System.Drawing.Point(3, 330);
            this.lblFixedExpense.Name = "lblFixedExpense";
            this.lblFixedExpense.Size = new System.Drawing.Size(82, 13);
            this.lblFixedExpense.TabIndex = 22;
            this.lblFixedExpense.Text = "Expense Share:";
            // 
            // txtFixedExpense
            // 
            this.txtFixedExpense.Location = new System.Drawing.Point(269, 333);
            this.txtFixedExpense.Name = "txtFixedExpense";
            this.txtFixedExpense.ReadOnly = true;
            this.txtFixedExpense.Size = new System.Drawing.Size(257, 20);
            this.txtFixedExpense.TabIndex = 23;
            // 
            // lblRetailerSharebeforeFixedExpense
            // 
            this.lblRetailerSharebeforeFixedExpense.AutoSize = true;
            this.lblRetailerSharebeforeFixedExpense.Location = new System.Drawing.Point(3, 300);
            this.lblRetailerSharebeforeFixedExpense.Name = "lblRetailerSharebeforeFixedExpense";
            this.lblRetailerSharebeforeFixedExpense.Size = new System.Drawing.Size(186, 13);
            this.lblRetailerSharebeforeFixedExpense.TabIndex = 20;
            this.lblRetailerSharebeforeFixedExpense.Text = "Retailer Share Before Fixed Expense :";
            // 
            // txtRetailerSharebeforeFixedExpense
            // 
            this.txtRetailerSharebeforeFixedExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRetailerSharebeforeFixedExpense.Location = new System.Drawing.Point(269, 303);
            this.txtRetailerSharebeforeFixedExpense.Name = "txtRetailerSharebeforeFixedExpense";
            this.txtRetailerSharebeforeFixedExpense.ReadOnly = true;
            this.txtRetailerSharebeforeFixedExpense.Size = new System.Drawing.Size(260, 20);
            this.txtRetailerSharebeforeFixedExpense.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lblBalanceDue);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.lblRetailer);
            this.panel1.Controls.Add(this.txtRetailer);
            this.panel1.Controls.Add(this.txtBalanceDue);
            this.panel1.Location = new System.Drawing.Point(13, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 477);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnProfitShare);
            this.panel2.Controls.Add(this.btnConfirm);
            this.panel2.Location = new System.Drawing.Point(220, 423);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(315, 54);
            this.panel2.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Location = new System.Drawing.Point(218, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 37);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAdvanceToRetailer
            // 
            this.txtAdvanceToRetailer.AllowDecimal = true;
            this.txtAdvanceToRetailer.AllowNegative = false;
            this.txtAdvanceToRetailer.DecimalLength = 2;
            this.txtAdvanceToRetailer.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAdvanceToRetailer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAdvanceToRetailer.Location = new System.Drawing.Point(269, 273);
            this.txtAdvanceToRetailer.MaxLength = 12;
            this.txtAdvanceToRetailer.MaxVaule = new decimal(new int[] {
            1215752191,
            23,
            0,
            131072});
            this.txtAdvanceToRetailer.Name = "txtAdvanceToRetailer";
            this.txtAdvanceToRetailer.ShortcutsEnabled = false;
            this.txtAdvanceToRetailer.Size = new System.Drawing.Size(260, 20);
            this.txtAdvanceToRetailer.TabIndex = 19;
            this.txtAdvanceToRetailer.TextChanged += new System.EventHandler(this.txtAdvanceToRetailer_TextChanged);
            // 
            // LiquidationDetails
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 496);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LiquidationDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retailer\'s Liquidation Details:";
            this.Load += new System.EventHandler(this.LiquidationDetails_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRetailer;
        private System.Windows.Forms.Label lblBalanceDue;
        private System.Windows.Forms.TextBox txtRetailer;
        private System.Windows.Forms.TextBox txtBalanceDue;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnProfitShare;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblDateCollected;
        private System.Windows.Forms.TextBox txtDateCollected;
        private System.Windows.Forms.Label lblRetailerName;
        private System.Windows.Forms.TextBox txtRetailerName;
        private System.Windows.Forms.Label lblGross;
        private System.Windows.Forms.TextBox txtGross;
        private System.Windows.Forms.Label lblTicketsExpected;
        private System.Windows.Forms.TextBox txtTicketsPaid;
        private System.Windows.Forms.TextBox txtTicketsExpected;
        private System.Windows.Forms.Label lblAdvancetoRetailer;
        private System.Windows.Forms.TextBox txtRetailerShare;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.TextBox txtRetailerNegativeNet;
        private System.Windows.Forms.Label lblTicketsPaid;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.TextBox txtNetValue;
        private System.Windows.Forms.Label lblNet22;
        private System.Windows.Forms.Label lblRetailerShare;
        private System.Windows.Forms.Label lblRetailersNegativeNet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblFixedExpense;
        private System.Windows.Forms.TextBox txtFixedExpense;
        private NumberTextBox txtAdvanceToRetailer;
        private System.Windows.Forms.Label lblRetailerSharebeforeFixedExpense;
        private System.Windows.Forms.TextBox txtRetailerSharebeforeFixedExpense;
    }
}