using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class frmProfitShare
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
            this.grpProfit = new System.Windows.Forms.GroupBox();
            this.tblpnlProfitShare = new System.Windows.Forms.TableLayoutPanel();
            this.lblProfitShareGroup = new System.Windows.Forms.Label();
            this.lblExpenseShareGroup = new System.Windows.Forms.Label();
            this.lblExpenseShareAmount = new System.Windows.Forms.Label();
            this.lblWriteOffExpense = new System.Windows.Forms.Label();
            this.lblPayPeriod = new System.Windows.Forms.Label();
            this.cboProfitShareGroup = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cboExpenseShareGroup = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.lblCarriedForwardAmount = new System.Windows.Forms.Label();
            this.lblPayPeriodValue = new System.Windows.Forms.Label();
            this.txtExpenseShareAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtWriteOffExpense = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.txtCarriedForwardAmount = new BMC.EnterpriseClient.Views.NumberTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpProfit.SuspendLayout();
            this.tblpnlProfitShare.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProfit
            // 
            this.grpProfit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProfit.Controls.Add(this.tblpnlProfitShare);
            this.grpProfit.Location = new System.Drawing.Point(6, -2);
            this.grpProfit.Name = "grpProfit";
            this.grpProfit.Size = new System.Drawing.Size(405, 196);
            this.grpProfit.TabIndex = 0;
            this.grpProfit.TabStop = false;
            // 
            // tblpnlProfitShare
            // 
            this.tblpnlProfitShare.ColumnCount = 2;
            this.tblpnlProfitShare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpnlProfitShare.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblpnlProfitShare.Controls.Add(this.lblProfitShareGroup, 0, 0);
            this.tblpnlProfitShare.Controls.Add(this.lblExpenseShareGroup, 0, 1);
            this.tblpnlProfitShare.Controls.Add(this.lblExpenseShareAmount, 0, 2);
            this.tblpnlProfitShare.Controls.Add(this.lblWriteOffExpense, 0, 3);
            this.tblpnlProfitShare.Controls.Add(this.lblPayPeriod, 0, 5);
            this.tblpnlProfitShare.Controls.Add(this.cboProfitShareGroup, 1, 0);
            this.tblpnlProfitShare.Controls.Add(this.cboExpenseShareGroup, 1, 1);
            this.tblpnlProfitShare.Controls.Add(this.lblCarriedForwardAmount, 0, 4);
            this.tblpnlProfitShare.Controls.Add(this.lblPayPeriodValue, 1, 5);
            this.tblpnlProfitShare.Controls.Add(this.txtExpenseShareAmount, 1, 2);
            this.tblpnlProfitShare.Controls.Add(this.txtWriteOffExpense, 1, 3);
            this.tblpnlProfitShare.Controls.Add(this.txtCarriedForwardAmount, 1, 4);
            this.tblpnlProfitShare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpnlProfitShare.Location = new System.Drawing.Point(3, 17);
            this.tblpnlProfitShare.Name = "tblpnlProfitShare";
            this.tblpnlProfitShare.RowCount = 6;
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblpnlProfitShare.Size = new System.Drawing.Size(399, 176);
            this.tblpnlProfitShare.TabIndex = 0;
            // 
            // lblProfitShareGroup
            // 
            this.lblProfitShareGroup.AutoSize = true;
            this.lblProfitShareGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProfitShareGroup.Location = new System.Drawing.Point(3, 0);
            this.lblProfitShareGroup.Name = "lblProfitShareGroup";
            this.lblProfitShareGroup.Size = new System.Drawing.Size(193, 30);
            this.lblProfitShareGroup.TabIndex = 0;
            this.lblProfitShareGroup.Text = "Profit Share Group:";
            // 
            // lblExpenseShareGroup
            // 
            this.lblExpenseShareGroup.AutoSize = true;
            this.lblExpenseShareGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpenseShareGroup.Location = new System.Drawing.Point(3, 30);
            this.lblExpenseShareGroup.Name = "lblExpenseShareGroup";
            this.lblExpenseShareGroup.Size = new System.Drawing.Size(193, 30);
            this.lblExpenseShareGroup.TabIndex = 2;
            this.lblExpenseShareGroup.Text = "Expense Share Group:";
            // 
            // lblExpenseShareAmount
            // 
            this.lblExpenseShareAmount.AutoSize = true;
            this.lblExpenseShareAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpenseShareAmount.Location = new System.Drawing.Point(3, 60);
            this.lblExpenseShareAmount.Name = "lblExpenseShareAmount";
            this.lblExpenseShareAmount.Size = new System.Drawing.Size(193, 30);
            this.lblExpenseShareAmount.TabIndex = 4;
            this.lblExpenseShareAmount.Text = "Expense Amount :";
            // 
            // lblWriteOffExpense
            // 
            this.lblWriteOffExpense.AutoSize = true;
            this.lblWriteOffExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWriteOffExpense.Location = new System.Drawing.Point(3, 90);
            this.lblWriteOffExpense.Name = "lblWriteOffExpense";
            this.lblWriteOffExpense.Size = new System.Drawing.Size(193, 30);
            this.lblWriteOffExpense.TabIndex = 6;
            this.lblWriteOffExpense.Text = "Write-Off Expense :";
            // 
            // lblPayPeriod
            // 
            this.lblPayPeriod.AutoSize = true;
            this.lblPayPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPayPeriod.Location = new System.Drawing.Point(3, 150);
            this.lblPayPeriod.Name = "lblPayPeriod";
            this.lblPayPeriod.Size = new System.Drawing.Size(193, 30);
            this.lblPayPeriod.TabIndex = 10;
            this.lblPayPeriod.Text = "Pay Period:";
            // 
            // cboProfitShareGroup
            // 
            this.cboProfitShareGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboProfitShareGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboProfitShareGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProfitShareGroup.FormattingEnabled = true;
            this.cboProfitShareGroup.Location = new System.Drawing.Point(202, 3);
            this.cboProfitShareGroup.Name = "cboProfitShareGroup";
            this.cboProfitShareGroup.Size = new System.Drawing.Size(194, 22);
            this.cboProfitShareGroup.TabIndex = 1;
            // 
            // cboExpenseShareGroup
            // 
            this.cboExpenseShareGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboExpenseShareGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboExpenseShareGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExpenseShareGroup.FormattingEnabled = true;
            this.cboExpenseShareGroup.Location = new System.Drawing.Point(202, 33);
            this.cboExpenseShareGroup.Name = "cboExpenseShareGroup";
            this.cboExpenseShareGroup.Size = new System.Drawing.Size(194, 22);
            this.cboExpenseShareGroup.TabIndex = 3;
            this.cboExpenseShareGroup.SelectedIndexChanged += new System.EventHandler(this.cboExpenseShareGroup_SelectedIndexChanged);
            // 
            // lblCarriedForwardAmount
            // 
            this.lblCarriedForwardAmount.AutoSize = true;
            this.lblCarriedForwardAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCarriedForwardAmount.Location = new System.Drawing.Point(3, 120);
            this.lblCarriedForwardAmount.Name = "lblCarriedForwardAmount";
            this.lblCarriedForwardAmount.Size = new System.Drawing.Size(193, 30);
            this.lblCarriedForwardAmount.TabIndex = 8;
            this.lblCarriedForwardAmount.Text = "Carry Forward Amount :";
            // 
            // lblPayPeriodValue
            // 
            this.lblPayPeriodValue.AutoSize = true;
            this.lblPayPeriodValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPayPeriodValue.Location = new System.Drawing.Point(202, 150);
            this.lblPayPeriodValue.Name = "lblPayPeriodValue";
            this.lblPayPeriodValue.Size = new System.Drawing.Size(194, 30);
            this.lblPayPeriodValue.TabIndex = 11;
            // 
            // txtExpenseShareAmount
            // 
            this.txtExpenseShareAmount.AllowDecimal = true;
            this.txtExpenseShareAmount.AllowNegative = false;
            this.txtExpenseShareAmount.DecimalLength = 2;
            this.txtExpenseShareAmount.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtExpenseShareAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExpenseShareAmount.Location = new System.Drawing.Point(202, 63);
            this.txtExpenseShareAmount.MaxLength = 13;
            this.txtExpenseShareAmount.MaxVaule = new decimal(new int[] {
            -727379969,
            232,
            0,
            131072});
            this.txtExpenseShareAmount.Name = "txtExpenseShareAmount";
            this.txtExpenseShareAmount.ReadOnly = true;
            this.txtExpenseShareAmount.ShortcutsEnabled = false;
            this.txtExpenseShareAmount.Size = new System.Drawing.Size(194, 21);
            this.txtExpenseShareAmount.TabIndex = 5;
            // 
            // txtWriteOffExpense
            // 
            this.txtWriteOffExpense.AllowDecimal = true;
            this.txtWriteOffExpense.AllowNegative = false;
            this.txtWriteOffExpense.DecimalLength = 2;
            this.txtWriteOffExpense.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtWriteOffExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWriteOffExpense.Location = new System.Drawing.Point(202, 93);
            this.txtWriteOffExpense.MaxLength = 13;
            this.txtWriteOffExpense.MaxVaule = new decimal(new int[] {
            -727379969,
            232,
            0,
            131072});
            this.txtWriteOffExpense.Name = "txtWriteOffExpense";
            this.txtWriteOffExpense.ReadOnly = true;
            this.txtWriteOffExpense.ShortcutsEnabled = false;
            this.txtWriteOffExpense.Size = new System.Drawing.Size(194, 21);
            this.txtWriteOffExpense.TabIndex = 7;
            // 
            // txtCarriedForwardAmount
            // 
            this.txtCarriedForwardAmount.AllowDecimal = true;
            this.txtCarriedForwardAmount.AllowNegative = false;
            this.txtCarriedForwardAmount.DecimalLength = 2;
            this.txtCarriedForwardAmount.Denom = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCarriedForwardAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCarriedForwardAmount.Location = new System.Drawing.Point(202, 123);
            this.txtCarriedForwardAmount.MaxLength = 8;
            this.txtCarriedForwardAmount.MaxVaule = new decimal(new int[] {
            99999999,
            0,
            0,
            131072});
            this.txtCarriedForwardAmount.Name = "txtCarriedForwardAmount";
            this.txtCarriedForwardAmount.ReadOnly = true;
            this.txtCarriedForwardAmount.ShortcutsEnabled = false;
            this.txtCarriedForwardAmount.Size = new System.Drawing.Size(194, 21);
            this.txtCarriedForwardAmount.TabIndex = 9;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(221, 200);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 29);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(325, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 29);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmProfitShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 241);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpProfit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProfitShare";
            this.Text = "Profit Share";
            this.grpProfit.ResumeLayout(false);
            this.tblpnlProfitShare.ResumeLayout(false);
            this.tblpnlProfitShare.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProfit;
        private System.Windows.Forms.TableLayoutPanel tblpnlProfitShare;
        private System.Windows.Forms.Label lblProfitShareGroup;
        private System.Windows.Forms.Label lblExpenseShareGroup;
        private System.Windows.Forms.Label lblExpenseShareAmount;
        private System.Windows.Forms.Label lblWriteOffExpense;
        private System.Windows.Forms.Label lblPayPeriod;
        private BmcComboBox cboProfitShareGroup;
        private BmcComboBox cboExpenseShareGroup;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblCarriedForwardAmount;
        private System.Windows.Forms.Label lblPayPeriodValue;
        private NumberTextBox txtExpenseShareAmount;
        private NumberTextBox txtWriteOffExpense;
        private NumberTextBox txtCarriedForwardAmount;
    }
}