namespace BMC.EnterpriseClient.Views
{
    partial class frmOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOperator));
            this.tb1Operator = new System.Windows.Forms.TableLayoutPanel();
            this.tblControls = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_AddNew = new System.Windows.Forms.Button();
            this.tblOperatorDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblOperatorName = new System.Windows.Forms.Label();
            this.lblOperatorInvoiceName = new System.Windows.Forms.Label();
            this.txt_OperatorName = new System.Windows.Forms.TextBox();
            this.txt_OperatorInvoiceName = new System.Windows.Forms.TextBox();
            this.lblOperatorAddress = new System.Windows.Forms.Label();
            this.lblOperatorInvoiceAddress = new System.Windows.Forms.Label();
            this.txt_OperatorAddress = new System.Windows.Forms.TextBox();
            this.txt_OperatorInvoiceAddress = new System.Windows.Forms.TextBox();
            this.lblOperatorPostCode = new System.Windows.Forms.Label();
            this.lblOperatorInvoicePostCode = new System.Windows.Forms.Label();
            this.txt_OperatorPostcode = new System.Windows.Forms.TextBox();
            this.txt_OperatorInvoicePostcode = new System.Windows.Forms.TextBox();
            this.lblOperatorPhone = new System.Windows.Forms.Label();
            this.lblOperatorEmail = new System.Windows.Forms.Label();
            this.txt_OperatorPhone = new System.Windows.Forms.TextBox();
            this.txt_OperatorEmail = new System.Windows.Forms.TextBox();
            this.lblOperatorFax = new System.Windows.Forms.Label();
            this.lblOperatorContactName = new System.Windows.Forms.Label();
            this.txt_OperatorFax = new System.Windows.Forms.TextBox();
            this.txt_OperatorContactName = new System.Windows.Forms.TextBox();
            this.lstOperators = new System.Windows.Forms.ListBox();
            this.tb1Operator.SuspendLayout();
            this.tblControls.SuspendLayout();
            this.tblOperatorDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb1Operator
            // 
            this.tb1Operator.ColumnCount = 2;
            this.tb1Operator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tb1Operator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tb1Operator.Controls.Add(this.tblControls, 0, 1);
            this.tb1Operator.Controls.Add(this.tblOperatorDetails, 1, 0);
            this.tb1Operator.Controls.Add(this.lstOperators, 0, 0);
            this.tb1Operator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb1Operator.Location = new System.Drawing.Point(0, 0);
            this.tb1Operator.Name = "tb1Operator";
            this.tb1Operator.RowCount = 2;
            this.tb1Operator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tb1Operator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tb1Operator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tb1Operator.Size = new System.Drawing.Size(846, 628);
            this.tb1Operator.TabIndex = 0;
            // 
            // tblControls
            // 
            this.tblControls.ColumnCount = 5;
            this.tb1Operator.SetColumnSpan(this.tblControls, 2);
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tblControls.Controls.Add(this.btn_Update, 2, 0);
            this.tblControls.Controls.Add(this.btn_Close, 4, 0);
            this.tblControls.Controls.Add(this.btn_Delete, 3, 0);
            this.tblControls.Controls.Add(this.btn_AddNew, 1, 0);
            this.tblControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblControls.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tblControls.Location = new System.Drawing.Point(3, 591);
            this.tblControls.Name = "tblControls";
            this.tblControls.RowCount = 1;
            this.tblControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblControls.Size = new System.Drawing.Size(840, 34);
            this.tblControls.TabIndex = 2;
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Update.AutoSize = true;
            this.btn_Update.Location = new System.Drawing.Point(543, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(94, 28);
            this.btn_Update.TabIndex = 1;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.AutoSize = true;
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(743, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(94, 28);
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Tag = "";
            this.btn_Close.Text = "&Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.AutoSize = true;
            this.btn_Delete.Location = new System.Drawing.Point(643, 3);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(94, 28);
            this.btn_Delete.TabIndex = 2;
            this.btn_Delete.Text = "&Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btn_AddNew
            // 
            this.btn_AddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_AddNew.AutoSize = true;
            this.btn_AddNew.Location = new System.Drawing.Point(443, 3);
            this.btn_AddNew.Name = "btn_AddNew";
            this.btn_AddNew.Size = new System.Drawing.Size(94, 28);
            this.btn_AddNew.TabIndex = 0;
            this.btn_AddNew.Tag = "Add New";
            this.btn_AddNew.Text = "&Add ";
            this.btn_AddNew.UseVisualStyleBackColor = true;
            this.btn_AddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // tblOperatorDetails
            // 
            this.tblOperatorDetails.ColumnCount = 2;
            this.tblOperatorDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOperatorDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblOperatorDetails.Controls.Add(this.lblOperatorName, 0, 0);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorInvoiceName, 1, 0);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorName, 0, 1);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorInvoiceName, 1, 1);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorAddress, 0, 2);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorInvoiceAddress, 1, 2);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorAddress, 0, 3);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorInvoiceAddress, 1, 3);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorPostCode, 0, 4);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorInvoicePostCode, 1, 4);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorPostcode, 0, 5);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorInvoicePostcode, 1, 5);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorPhone, 0, 6);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorEmail, 1, 6);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorPhone, 0, 7);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorEmail, 1, 7);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorFax, 0, 8);
            this.tblOperatorDetails.Controls.Add(this.lblOperatorContactName, 1, 8);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorFax, 0, 9);
            this.tblOperatorDetails.Controls.Add(this.txt_OperatorContactName, 1, 9);
            this.tblOperatorDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblOperatorDetails.Location = new System.Drawing.Point(303, 3);
            this.tblOperatorDetails.Name = "tblOperatorDetails";
            this.tblOperatorDetails.RowCount = 10;
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblOperatorDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblOperatorDetails.Size = new System.Drawing.Size(540, 582);
            this.tblOperatorDetails.TabIndex = 1;
            // 
            // lblOperatorName
            // 
            this.lblOperatorName.AutoSize = true;
            this.lblOperatorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorName.Location = new System.Drawing.Point(3, 0);
            this.lblOperatorName.Name = "lblOperatorName";
            this.lblOperatorName.Size = new System.Drawing.Size(264, 19);
            this.lblOperatorName.TabIndex = 0;
            this.lblOperatorName.Text = "* Operator Name :";
            // 
            // lblOperatorInvoiceName
            // 
            this.lblOperatorInvoiceName.AutoSize = true;
            this.lblOperatorInvoiceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorInvoiceName.Location = new System.Drawing.Point(273, 0);
            this.lblOperatorInvoiceName.Name = "lblOperatorInvoiceName";
            this.lblOperatorInvoiceName.Size = new System.Drawing.Size(264, 19);
            this.lblOperatorInvoiceName.TabIndex = 2;
            this.lblOperatorInvoiceName.Text = "Operator Invoice Name :";
            // 
            // txt_OperatorName
            // 
            this.txt_OperatorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorName.Location = new System.Drawing.Point(3, 22);
            this.txt_OperatorName.MaxLength = 50;
            this.txt_OperatorName.Name = "txt_OperatorName";
            this.txt_OperatorName.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorName.TabIndex = 1;
            // 
            // txt_OperatorInvoiceName
            // 
            this.txt_OperatorInvoiceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorInvoiceName.Location = new System.Drawing.Point(273, 22);
            this.txt_OperatorInvoiceName.MaxLength = 50;
            this.txt_OperatorInvoiceName.Name = "txt_OperatorInvoiceName";
            this.txt_OperatorInvoiceName.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorInvoiceName.TabIndex = 3;
            // 
            // lblOperatorAddress
            // 
            this.lblOperatorAddress.AutoSize = true;
            this.lblOperatorAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorAddress.Location = new System.Drawing.Point(3, 44);
            this.lblOperatorAddress.Name = "lblOperatorAddress";
            this.lblOperatorAddress.Size = new System.Drawing.Size(264, 20);
            this.lblOperatorAddress.TabIndex = 4;
            this.lblOperatorAddress.Text = "Operator Address :";
            // 
            // lblOperatorInvoiceAddress
            // 
            this.lblOperatorInvoiceAddress.AutoSize = true;
            this.lblOperatorInvoiceAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorInvoiceAddress.Location = new System.Drawing.Point(273, 44);
            this.lblOperatorInvoiceAddress.Name = "lblOperatorInvoiceAddress";
            this.lblOperatorInvoiceAddress.Size = new System.Drawing.Size(264, 20);
            this.lblOperatorInvoiceAddress.TabIndex = 6;
            this.lblOperatorInvoiceAddress.Text = "Operator Invoice Address :";
            // 
            // txt_OperatorAddress
            // 
            this.txt_OperatorAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorAddress.Location = new System.Drawing.Point(3, 67);
            this.txt_OperatorAddress.Multiline = true;
            this.txt_OperatorAddress.Name = "txt_OperatorAddress";
            this.txt_OperatorAddress.Size = new System.Drawing.Size(264, 102);
            this.txt_OperatorAddress.TabIndex = 5;
            // 
            // txt_OperatorInvoiceAddress
            // 
            this.txt_OperatorInvoiceAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorInvoiceAddress.Location = new System.Drawing.Point(273, 67);
            this.txt_OperatorInvoiceAddress.Multiline = true;
            this.txt_OperatorInvoiceAddress.Name = "txt_OperatorInvoiceAddress";
            this.txt_OperatorInvoiceAddress.Size = new System.Drawing.Size(264, 102);
            this.txt_OperatorInvoiceAddress.TabIndex = 7;
            // 
            // lblOperatorPostCode
            // 
            this.lblOperatorPostCode.AutoSize = true;
            this.lblOperatorPostCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorPostCode.Location = new System.Drawing.Point(3, 172);
            this.lblOperatorPostCode.Name = "lblOperatorPostCode";
            this.lblOperatorPostCode.Size = new System.Drawing.Size(264, 18);
            this.lblOperatorPostCode.TabIndex = 8;
            this.lblOperatorPostCode.Text = "Operator Postcode :";
            // 
            // lblOperatorInvoicePostCode
            // 
            this.lblOperatorInvoicePostCode.AutoSize = true;
            this.lblOperatorInvoicePostCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorInvoicePostCode.Location = new System.Drawing.Point(273, 172);
            this.lblOperatorInvoicePostCode.Name = "lblOperatorInvoicePostCode";
            this.lblOperatorInvoicePostCode.Size = new System.Drawing.Size(264, 18);
            this.lblOperatorInvoicePostCode.TabIndex = 10;
            this.lblOperatorInvoicePostCode.Text = "Operator Invoice Postcode :";
            // 
            // txt_OperatorPostcode
            // 
            this.txt_OperatorPostcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorPostcode.Location = new System.Drawing.Point(3, 193);
            this.txt_OperatorPostcode.MaxLength = 15;
            this.txt_OperatorPostcode.Name = "txt_OperatorPostcode";
            this.txt_OperatorPostcode.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorPostcode.TabIndex = 9;
            // 
            // txt_OperatorInvoicePostcode
            // 
            this.txt_OperatorInvoicePostcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorInvoicePostcode.Location = new System.Drawing.Point(273, 193);
            this.txt_OperatorInvoicePostcode.MaxLength = 50;
            this.txt_OperatorInvoicePostcode.Name = "txt_OperatorInvoicePostcode";
            this.txt_OperatorInvoicePostcode.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorInvoicePostcode.TabIndex = 11;
            // 
            // lblOperatorPhone
            // 
            this.lblOperatorPhone.AutoSize = true;
            this.lblOperatorPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorPhone.Location = new System.Drawing.Point(3, 218);
            this.lblOperatorPhone.Name = "lblOperatorPhone";
            this.lblOperatorPhone.Size = new System.Drawing.Size(264, 19);
            this.lblOperatorPhone.TabIndex = 12;
            this.lblOperatorPhone.Text = "Operator Phone :";
            // 
            // lblOperatorEmail
            // 
            this.lblOperatorEmail.AutoSize = true;
            this.lblOperatorEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorEmail.Location = new System.Drawing.Point(273, 218);
            this.lblOperatorEmail.Name = "lblOperatorEmail";
            this.lblOperatorEmail.Size = new System.Drawing.Size(264, 19);
            this.lblOperatorEmail.TabIndex = 14;
            this.lblOperatorEmail.Text = "Operator Email :";
            // 
            // txt_OperatorPhone
            // 
            this.txt_OperatorPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorPhone.Location = new System.Drawing.Point(3, 240);
            this.txt_OperatorPhone.MaxLength = 15;
            this.txt_OperatorPhone.Name = "txt_OperatorPhone";
            this.txt_OperatorPhone.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorPhone.TabIndex = 13;
            // 
            // txt_OperatorEmail
            // 
            this.txt_OperatorEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorEmail.Location = new System.Drawing.Point(273, 240);
            this.txt_OperatorEmail.MaxLength = 100;
            this.txt_OperatorEmail.Name = "txt_OperatorEmail";
            this.txt_OperatorEmail.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorEmail.TabIndex = 15;
            // 
            // lblOperatorFax
            // 
            this.lblOperatorFax.AutoSize = true;
            this.lblOperatorFax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorFax.Location = new System.Drawing.Point(3, 264);
            this.lblOperatorFax.Name = "lblOperatorFax";
            this.lblOperatorFax.Size = new System.Drawing.Size(264, 16);
            this.lblOperatorFax.TabIndex = 16;
            this.lblOperatorFax.Text = "Operator Fax :";
            // 
            // lblOperatorContactName
            // 
            this.lblOperatorContactName.AutoSize = true;
            this.lblOperatorContactName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperatorContactName.Location = new System.Drawing.Point(273, 264);
            this.lblOperatorContactName.Name = "lblOperatorContactName";
            this.lblOperatorContactName.Size = new System.Drawing.Size(264, 16);
            this.lblOperatorContactName.TabIndex = 18;
            this.lblOperatorContactName.Text = "Operator Contact Name :";
            // 
            // txt_OperatorFax
            // 
            this.txt_OperatorFax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorFax.Location = new System.Drawing.Point(3, 283);
            this.txt_OperatorFax.MaxLength = 15;
            this.txt_OperatorFax.Name = "txt_OperatorFax";
            this.txt_OperatorFax.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorFax.TabIndex = 17;
            // 
            // txt_OperatorContactName
            // 
            this.txt_OperatorContactName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_OperatorContactName.Location = new System.Drawing.Point(273, 283);
            this.txt_OperatorContactName.MaxLength = 50;
            this.txt_OperatorContactName.Name = "txt_OperatorContactName";
            this.txt_OperatorContactName.Size = new System.Drawing.Size(264, 21);
            this.txt_OperatorContactName.TabIndex = 19;
            // 
            // lstOperators
            // 
            this.lstOperators.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstOperators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOperators.FormattingEnabled = true;
            this.lstOperators.Location = new System.Drawing.Point(3, 3);
            this.lstOperators.Name = "lstOperators";
            this.lstOperators.Size = new System.Drawing.Size(294, 582);
            this.lstOperators.TabIndex = 0;
            this.lstOperators.SelectedIndexChanged += new System.EventHandler(this.lbOperator_SelectedIndexChanged);
            // 
            // frmOperator
            // 
            this.AcceptButton = this.btn_AddNew;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(846, 628);
            this.Controls.Add(this.tb1Operator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmOperator";
            this.ShowInTaskbar = false;
            this.Text = "Operators";
            this.Load += new System.EventHandler(this.Operator_Load);
            this.tb1Operator.ResumeLayout(false);
            this.tblControls.ResumeLayout(false);
            this.tblControls.PerformLayout();
            this.tblOperatorDetails.ResumeLayout(false);
            this.tblOperatorDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tb1Operator;
        private System.Windows.Forms.ListBox lstOperators;
        private System.Windows.Forms.TableLayoutPanel tblOperatorDetails;
        private System.Windows.Forms.Label lblOperatorName;
        private System.Windows.Forms.Label lblOperatorInvoiceName;
        private System.Windows.Forms.TextBox txt_OperatorName;
        private System.Windows.Forms.TextBox txt_OperatorInvoiceName;
        private System.Windows.Forms.Label lblOperatorAddress;
        private System.Windows.Forms.Label lblOperatorInvoiceAddress;
        private System.Windows.Forms.TextBox txt_OperatorAddress;
        private System.Windows.Forms.TextBox txt_OperatorInvoiceAddress;
        private System.Windows.Forms.Label lblOperatorPostCode;
        private System.Windows.Forms.Label lblOperatorInvoicePostCode;
        private System.Windows.Forms.TextBox txt_OperatorPostcode;
        private System.Windows.Forms.TextBox txt_OperatorInvoicePostcode;
        private System.Windows.Forms.Label lblOperatorPhone;
        private System.Windows.Forms.Label lblOperatorEmail;
        private System.Windows.Forms.TextBox txt_OperatorPhone;
        private System.Windows.Forms.TextBox txt_OperatorEmail;
        private System.Windows.Forms.Label lblOperatorFax;
        private System.Windows.Forms.Label lblOperatorContactName;
        private System.Windows.Forms.TextBox txt_OperatorFax;
        private System.Windows.Forms.TextBox txt_OperatorContactName;
        private System.Windows.Forms.TableLayoutPanel tblControls;
        private System.Windows.Forms.Button btn_AddNew;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Close;
    }
}