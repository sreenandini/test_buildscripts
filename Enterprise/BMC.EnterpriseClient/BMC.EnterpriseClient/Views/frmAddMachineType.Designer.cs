using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class frmAddMachineType
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbName = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.chkNGA = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDepreciationPolicy = new System.Windows.Forms.Label();
            this.lblLedger = new System.Windows.Forms.Label();
            this.cmbDepreciation = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.txtIncomeLedgerCode = new System.Windows.Forms.TextBox();
            this.lblCMPGatewayCode = new System.Windows.Forms.Label();
            this.lblSiteIcon = new System.Windows.Forms.Label();
            this.txtCMPGatewayCode = new System.Windows.Forms.TextBox();
            this.cmbSiteIcon = new System.Windows.Forms.ComboBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.picSlotIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSlotIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(14, 23);
            this.txtName.MaxLength = 40;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(293, 21);
            this.txtName.TabIndex = 2;
            // 
            // cmbName
            // 
            this.cmbName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(14, 23);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(293, 22);
            this.cmbName.TabIndex = 1;
            this.cmbName.SelectedIndexChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
            // 
            // chkNGA
            // 
            this.chkNGA.AutoSize = true;
            this.chkNGA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNGA.Location = new System.Drawing.Point(10, 207);
            this.chkNGA.Name = "chkNGA";
            this.chkNGA.Size = new System.Drawing.Size(146, 17);
            this.chkNGA.TabIndex = 8;
            this.chkNGA.Text = "Is Non Gaming Asset";
            this.chkNGA.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(16, 298);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(87, 28);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "* Name:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 49);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(76, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(14, 64);
            this.txtDescription.MaxLength = 30;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(293, 21);
            this.txtDescription.TabIndex = 4;
            // 
            // lblDepreciationPolicy
            // 
            this.lblDepreciationPolicy.AutoSize = true;
            this.lblDepreciationPolicy.Location = new System.Drawing.Point(10, 89);
            this.lblDepreciationPolicy.Name = "lblDepreciationPolicy";
            this.lblDepreciationPolicy.Size = new System.Drawing.Size(121, 13);
            this.lblDepreciationPolicy.TabIndex = 4;
            this.lblDepreciationPolicy.Text = "Depreciation Policy:";
            // 
            // lblLedger
            // 
            this.lblLedger.AutoSize = true;
            this.lblLedger.Location = new System.Drawing.Point(10, 131);
            this.lblLedger.Name = "lblLedger";
            this.lblLedger.Size = new System.Drawing.Size(85, 13);
            this.lblLedger.TabIndex = 6;
            this.lblLedger.Text = "Ledger Code:";
            // 
            // cmbDepreciation
            // 
            this.cmbDepreciation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDepreciation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepreciation.FormattingEnabled = true;
            this.cmbDepreciation.Location = new System.Drawing.Point(14, 104);
            this.cmbDepreciation.Name = "cmbDepreciation";
            this.cmbDepreciation.Size = new System.Drawing.Size(293, 22);
            this.cmbDepreciation.TabIndex = 5;
            // 
            // txtIncomeLedgerCode
            // 
            this.txtIncomeLedgerCode.Location = new System.Drawing.Point(14, 145);
            this.txtIncomeLedgerCode.MaxLength = 20;
            this.txtIncomeLedgerCode.Name = "txtIncomeLedgerCode";
            this.txtIncomeLedgerCode.Size = new System.Drawing.Size(293, 21);
            this.txtIncomeLedgerCode.TabIndex = 6;
            // 
            // lblCMPGatewayCode
            // 
            this.lblCMPGatewayCode.AutoSize = true;
            this.lblCMPGatewayCode.Location = new System.Drawing.Point(10, 180);
            this.lblCMPGatewayCode.Name = "lblCMPGatewayCode";
            this.lblCMPGatewayCode.Size = new System.Drawing.Size(125, 13);
            this.lblCMPGatewayCode.TabIndex = 8;
            this.lblCMPGatewayCode.Text = "CMP Gateway Code:";
            // 
            // lblSiteIcon
            // 
            this.lblSiteIcon.AutoSize = true;
            this.lblSiteIcon.Location = new System.Drawing.Point(10, 238);
            this.lblSiteIcon.Name = "lblSiteIcon";
            this.lblSiteIcon.Size = new System.Drawing.Size(63, 13);
            this.lblSiteIcon.TabIndex = 11;
            this.lblSiteIcon.Text = "Site Icon:";
            // 
            // txtCMPGatewayCode
            // 
            this.txtCMPGatewayCode.Location = new System.Drawing.Point(135, 177);
            this.txtCMPGatewayCode.MaxLength = 8;
            this.txtCMPGatewayCode.Name = "txtCMPGatewayCode";
            this.txtCMPGatewayCode.Size = new System.Drawing.Size(119, 21);
            this.txtCMPGatewayCode.TabIndex = 7;
            this.txtCMPGatewayCode.TextChanged += new System.EventHandler(this.txtCMPGatewayCode_TextChanged);
            // 
            // cmbSiteIcon
            // 
            this.cmbSiteIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSiteIcon.FormattingEnabled = true;
            this.cmbSiteIcon.Location = new System.Drawing.Point(14, 255);
            this.cmbSiteIcon.Name = "cmbSiteIcon";
            this.cmbSiteIcon.Size = new System.Drawing.Size(227, 21);
            this.cmbSiteIcon.TabIndex = 9;
            this.cmbSiteIcon.SelectedIndexChanged += new System.EventHandler(this.cmbSiteIcon_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(119, 298);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 28);
            this.btnNew.TabIndex = 12;
            this.btnNew.Text = "&New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(220, 298);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 28);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(17, 298);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 28);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // picSlotIcon
            // 
            this.picSlotIcon.Location = new System.Drawing.Point(250, 207);
            this.picSlotIcon.Name = "picSlotIcon";
            this.picSlotIcon.Size = new System.Drawing.Size(54, 44);
            this.picSlotIcon.TabIndex = 4;
            this.picSlotIcon.TabStop = false;
            // 
            // frmAddMachineType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(315, 336);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cmbSiteIcon);
            this.Controls.Add(this.txtCMPGatewayCode);
            this.Controls.Add(this.lblSiteIcon);
            this.Controls.Add(this.lblCMPGatewayCode);
            this.Controls.Add(this.txtIncomeLedgerCode);
            this.Controls.Add(this.cmbDepreciation);
            this.Controls.Add(this.lblLedger);
            this.Controls.Add(this.lblDepreciationPolicy);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picSlotIcon);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.chkNGA);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddMachineType";
            this.Text = "Machine type admin";
            this.Load += new System.EventHandler(this.frmAddMachineType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSlotIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private BmcComboBox cmbName;
        private System.Windows.Forms.CheckBox chkNGA;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDepreciationPolicy;
        private System.Windows.Forms.Label lblLedger;
        private BmcComboBox cmbDepreciation;
        private System.Windows.Forms.TextBox txtIncomeLedgerCode;
        private System.Windows.Forms.Label lblCMPGatewayCode;
        private System.Windows.Forms.Label lblSiteIcon;
        private System.Windows.Forms.TextBox txtCMPGatewayCode;
        private System.Windows.Forms.ComboBox cmbSiteIcon;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.PictureBox picSlotIcon;
    }
}