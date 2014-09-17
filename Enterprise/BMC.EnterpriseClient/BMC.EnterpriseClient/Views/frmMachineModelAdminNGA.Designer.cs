namespace BMC.EnterpriseClient.Views
{
    partial class frmMachineModelAdminNGA
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
            this.gpNewMachineModel = new System.Windows.Forms.GroupBox();
            this.lblReleaseDate = new System.Windows.Forms.Label();
            this.dtReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.lblDepreciationPolicy = new System.Windows.Forms.Label();
            this.cmbDepreciationPolicy = new System.Windows.Forms.ComboBox();
            this.txtMachineCategory = new System.Windows.Forms.TextBox();
            this.lblSubCategory = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbMachineTypeID = new System.Windows.Forms.ComboBox();
            this.txtModelCode = new System.Windows.Forms.TextBox();
            this.lblModelCode = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.chkTestMachine = new System.Windows.Forms.CheckBox();
            this.cmbManufacturerID = new System.Windows.Forms.ComboBox();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.chkDeListedModel = new System.Windows.Forms.CheckBox();
            this.chkUseDepreciationDefault = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gpNewMachineModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpNewMachineModel
            // 
            this.gpNewMachineModel.Controls.Add(this.lblReleaseDate);
            this.gpNewMachineModel.Controls.Add(this.dtReleaseDate);
            this.gpNewMachineModel.Controls.Add(this.lblDepreciationPolicy);
            this.gpNewMachineModel.Controls.Add(this.cmbDepreciationPolicy);
            this.gpNewMachineModel.Controls.Add(this.txtMachineCategory);
            this.gpNewMachineModel.Controls.Add(this.lblSubCategory);
            this.gpNewMachineModel.Controls.Add(this.lblCategory);
            this.gpNewMachineModel.Controls.Add(this.cmbCategory);
            this.gpNewMachineModel.Controls.Add(this.lblType);
            this.gpNewMachineModel.Controls.Add(this.cmbMachineTypeID);
            this.gpNewMachineModel.Controls.Add(this.txtModelCode);
            this.gpNewMachineModel.Controls.Add(this.lblModelCode);
            this.gpNewMachineModel.Controls.Add(this.lblManufacturer);
            this.gpNewMachineModel.Controls.Add(this.chkTestMachine);
            this.gpNewMachineModel.Controls.Add(this.cmbManufacturerID);
            this.gpNewMachineModel.Controls.Add(this.txtMachineName);
            this.gpNewMachineModel.Controls.Add(this.chkDeListedModel);
            this.gpNewMachineModel.Controls.Add(this.chkUseDepreciationDefault);
            this.gpNewMachineModel.Controls.Add(this.lblName);
            this.gpNewMachineModel.Location = new System.Drawing.Point(6, 4);
            this.gpNewMachineModel.Name = "gpNewMachineModel";
            this.gpNewMachineModel.Size = new System.Drawing.Size(733, 205);
            this.gpNewMachineModel.TabIndex = 0;
            this.gpNewMachineModel.TabStop = false;
            this.gpNewMachineModel.Text = "New Machine Model Details";
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AutoSize = true;
            this.lblReleaseDate.Location = new System.Drawing.Point(9, 168);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(88, 13);
            this.lblReleaseDate.TabIndex = 17;
            this.lblReleaseDate.Text = "Release Date:";
            // 
            // dtReleaseDate
            // 
            this.dtReleaseDate.CustomFormat = "dd/MM/yy";
            this.dtReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtReleaseDate.Location = new System.Drawing.Point(129, 164);
            this.dtReleaseDate.Name = "dtReleaseDate";
            this.dtReleaseDate.Size = new System.Drawing.Size(251, 21);
            this.dtReleaseDate.TabIndex = 18;
            // 
            // lblDepreciationPolicy
            // 
            this.lblDepreciationPolicy.AutoSize = true;
            this.lblDepreciationPolicy.Location = new System.Drawing.Point(9, 134);
            this.lblDepreciationPolicy.Name = "lblDepreciationPolicy";
            this.lblDepreciationPolicy.Size = new System.Drawing.Size(121, 13);
            this.lblDepreciationPolicy.TabIndex = 13;
            this.lblDepreciationPolicy.Text = "Depreciation Policy:";
            // 
            // cmbDepreciationPolicy
            // 
            this.cmbDepreciationPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepreciationPolicy.FormattingEnabled = true;
            this.cmbDepreciationPolicy.Location = new System.Drawing.Point(129, 130);
            this.cmbDepreciationPolicy.Name = "cmbDepreciationPolicy";
            this.cmbDepreciationPolicy.Size = new System.Drawing.Size(251, 21);
            this.cmbDepreciationPolicy.TabIndex = 14;
            // 
            // txtMachineCategory
            // 
            this.txtMachineCategory.Location = new System.Drawing.Point(499, 130);
            this.txtMachineCategory.MaxLength = 50;
            this.txtMachineCategory.Name = "txtMachineCategory";
            this.txtMachineCategory.Size = new System.Drawing.Size(226, 21);
            this.txtMachineCategory.TabIndex = 16;
            // 
            // lblSubCategory
            // 
            this.lblSubCategory.AutoSize = true;
            this.lblSubCategory.Location = new System.Drawing.Point(400, 134);
            this.lblSubCategory.Name = "lblSubCategory";
            this.lblSubCategory.Size = new System.Drawing.Size(91, 13);
            this.lblSubCategory.TabIndex = 15;
            this.lblSubCategory.Text = "Sub Category:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(416, 99);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(65, 13);
            this.lblCategory.TabIndex = 11;
            this.lblCategory.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(499, 95);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(226, 21);
            this.cmbCategory.TabIndex = 12;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 62);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(40, 13);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type:";
            // 
            // cmbMachineTypeID
            // 
            this.cmbMachineTypeID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachineTypeID.FormattingEnabled = true;
            this.cmbMachineTypeID.Location = new System.Drawing.Point(129, 58);
            this.cmbMachineTypeID.Name = "cmbMachineTypeID";
            this.cmbMachineTypeID.Size = new System.Drawing.Size(251, 21);
            this.cmbMachineTypeID.TabIndex = 5;
            // 
            // txtModelCode
            // 
            this.txtModelCode.Location = new System.Drawing.Point(499, 58);
            this.txtModelCode.MaxLength = 50;
            this.txtModelCode.Name = "txtModelCode";
            this.txtModelCode.Size = new System.Drawing.Size(116, 21);
            this.txtModelCode.TabIndex = 7;
            this.txtModelCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtModelCode_KeyPress);
            // 
            // lblModelCode
            // 
            this.lblModelCode.AutoSize = true;
            this.lblModelCode.Location = new System.Drawing.Point(400, 62);
            this.lblModelCode.Name = "lblModelCode";
            this.lblModelCode.Size = new System.Drawing.Size(79, 13);
            this.lblModelCode.TabIndex = 6;
            this.lblModelCode.Text = "Model Code:";
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Location = new System.Drawing.Point(400, 28);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(87, 13);
            this.lblManufacturer.TabIndex = 2;
            this.lblManufacturer.Text = "Manufacturer:";
            // 
            // chkTestMachine
            // 
            this.chkTestMachine.AutoSize = true;
            this.chkTestMachine.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTestMachine.Location = new System.Drawing.Point(269, 97);
            this.chkTestMachine.Name = "chkTestMachine";
            this.chkTestMachine.Size = new System.Drawing.Size(105, 17);
            this.chkTestMachine.TabIndex = 10;
            this.chkTestMachine.Text = "Test Machine:";
            this.chkTestMachine.UseVisualStyleBackColor = true;
            // 
            // cmbManufacturerID
            // 
            this.cmbManufacturerID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbManufacturerID.FormattingEnabled = true;
            this.cmbManufacturerID.Location = new System.Drawing.Point(499, 25);
            this.cmbManufacturerID.Name = "cmbManufacturerID";
            this.cmbManufacturerID.Size = new System.Drawing.Size(226, 21);
            this.cmbManufacturerID.TabIndex = 3;
            // 
            // txtMachineName
            // 
            this.txtMachineName.Location = new System.Drawing.Point(129, 25);
            this.txtMachineName.MaxLength = 50;
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Size = new System.Drawing.Size(251, 21);
            this.txtMachineName.TabIndex = 1;
            // 
            // chkDeListedModel
            // 
            this.chkDeListedModel.AutoSize = true;
            this.chkDeListedModel.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDeListedModel.Location = new System.Drawing.Point(145, 97);
            this.chkDeListedModel.Name = "chkDeListedModel";
            this.chkDeListedModel.Size = new System.Drawing.Size(122, 17);
            this.chkDeListedModel.TabIndex = 9;
            this.chkDeListedModel.Text = "De-Listed Model:";
            this.chkDeListedModel.UseVisualStyleBackColor = true;
            // 
            // chkUseDepreciationDefault
            // 
            this.chkUseDepreciationDefault.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUseDepreciationDefault.Checked = true;
            this.chkUseDepreciationDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDepreciationDefault.Location = new System.Drawing.Point(12, 90);
            this.chkUseDepreciationDefault.Name = "chkUseDepreciationDefault";
            this.chkUseDepreciationDefault.Size = new System.Drawing.Size(127, 30);
            this.chkUseDepreciationDefault.TabIndex = 8;
            this.chkUseDepreciationDefault.Text = "Use Depreciation \r\nDefault:";
            this.chkUseDepreciationDefault.UseVisualStyleBackColor = true;
            this.chkUseDepreciationDefault.CheckedChanged += new System.EventHandler(this.chkUseDepreciationDefault_CheckedChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(206, 215);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(98, 36);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(426, 215);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMachineModelAdminNGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(743, 258);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gpNewMachineModel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMachineModelAdminNGA";
            this.Text = "Machine Model Administration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMachineModelAdminNGA_FormClosing);
            this.Load += new System.EventHandler(this.frmMachineModelAdminNGA_Load);
            this.gpNewMachineModel.ResumeLayout(false);
            this.gpNewMachineModel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gpNewMachineModel;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.CheckBox chkDeListedModel;
        private System.Windows.Forms.CheckBox chkUseDepreciationDefault;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkTestMachine;
        private System.Windows.Forms.ComboBox cmbManufacturerID;
        private System.Windows.Forms.Label lblReleaseDate;
        private System.Windows.Forms.DateTimePicker dtReleaseDate;
        private System.Windows.Forms.Label lblDepreciationPolicy;
        private System.Windows.Forms.ComboBox cmbDepreciationPolicy;
        private System.Windows.Forms.TextBox txtMachineCategory;
        private System.Windows.Forms.Label lblSubCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbMachineTypeID;
        private System.Windows.Forms.TextBox txtModelCode;
        private System.Windows.Forms.Label lblModelCode;
        private System.Windows.Forms.Label lblManufacturer;
    }
}