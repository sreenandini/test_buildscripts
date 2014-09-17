using BMC.EnterpriseClient.Helpers;
namespace BMC.EnterpriseClient.Views
{
    partial class ucUserAdmin
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
            this.txtStaffUserName = new System.Windows.Forms.TextBox();
            this.chkStockController = new System.Windows.Forms.CheckBox();
            this.gpCustomerAccessSecurityGroups = new System.Windows.Forms.GroupBox();
            this.tbluseraccess = new System.Windows.Forms.TableLayoutPanel();
            this.lstCustomerAccessIncluded = new System.Windows.Forms.ListBox();
            this.lblNotIncluded = new System.Windows.Forms.Label();
            this.lblIncluded = new System.Windows.Forms.Label();
            this.lstCustomerAccessNotIncluded = new System.Windows.Forms.ListBox();
            this.chkRepresentative = new System.Windows.Forms.CheckBox();
            this.chkEngineer = new System.Windows.Forms.CheckBox();
            this.txtJobDescription = new System.Windows.Forms.TextBox();
            this.txtDepart = new System.Windows.Forms.TextBox();
            this.rtfNotes = new System.Windows.Forms.RichTextBox();
            this.txtExtensionNumber = new System.Windows.Forms.TextBox();
            this.txtPersonnelNo = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobilePhone = new System.Windows.Forms.TextBox();
            this.txtHomePhone = new System.Windows.Forms.TextBox();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.cmbUserLevel = new System.Windows.Forms.ComboBox();
            this.txtPasswordCheck = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtWindowsUsername = new System.Windows.Forms.TextBox();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.tblMainucadmin = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSurname = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.cmbSurname = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.lblJobTitleDescription = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblExt = new System.Windows.Forms.Label();
            this.lblPersonnelNo = new System.Windows.Forms.Label();
            this.lblEMail = new System.Windows.Forms.Label();
            this.lblMobile = new System.Windows.Forms.Label();
            this.lblTelephone = new System.Windows.Forms.Label();
            this.lblPostCode = new System.Windows.Forms.Label();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.lblPasswordCheck = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblWusername = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserLevel = new System.Windows.Forms.Label();
            this.lblDepot = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblDateFormat = new System.Windows.Forms.Label();
            this.lblServiceArea = new System.Windows.Forms.Label();
            this.lblUserLanguage = new System.Windows.Forms.Label();
            this.lblCurrencyFormat = new System.Windows.Forms.Label();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.treeOps = new System.Windows.Forms.TreeView();
            this.tblusercombobox = new System.Windows.Forms.TableLayoutPanel();
            this.cmbDate = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmbSupplier = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmbDepot = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmdServiceArea = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmbLanguage = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.cmbCurrency = new BMC.EnterpriseClient.Helpers.BmcComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tblbuttons = new System.Windows.Forms.TableLayoutPanel();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnAssignSite = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnUnLockUser = new System.Windows.Forms.Button();
            this.btnLockUser = new System.Windows.Forms.Button();
            this.chkTerminate = new System.Windows.Forms.CheckBox();
            this.tblAdminusers = new System.Windows.Forms.TableLayoutPanel();
            this.gpCustomerAccessSecurityGroups.SuspendLayout();
            this.tbluseraccess.SuspendLayout();
            this.tblMainucadmin.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.tblusercombobox.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tblbuttons.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tblAdminusers.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStaffUserName
            // 
            this.txtStaffUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStaffUserName.Location = new System.Drawing.Point(399, 16);
            this.txtStaffUserName.MaxLength = 30;
            this.txtStaffUserName.Name = "txtStaffUserName";
            this.txtStaffUserName.Size = new System.Drawing.Size(161, 20);
            this.txtStaffUserName.TabIndex = 1;
            this.txtStaffUserName.Leave += new System.EventHandler(this.txtStaffUserName_Leave);
            // 
            // chkStockController
            // 
            this.chkStockController.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStockController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStockController.Location = new System.Drawing.Point(236, 257);
            this.chkStockController.Name = "chkStockController";
            this.chkStockController.Size = new System.Drawing.Size(157, 20);
            this.chkStockController.TabIndex = 17;
            this.chkStockController.Text = "IsStock Controller";
            this.chkStockController.UseVisualStyleBackColor = true;
            // 
            // gpCustomerAccessSecurityGroups
            // 
            this.tblMain.SetColumnSpan(this.gpCustomerAccessSecurityGroups, 2);
            this.gpCustomerAccessSecurityGroups.Controls.Add(this.tbluseraccess);
            this.gpCustomerAccessSecurityGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpCustomerAccessSecurityGroups.Location = new System.Drawing.Point(3, 392);
            this.gpCustomerAccessSecurityGroups.Name = "gpCustomerAccessSecurityGroups";
            this.gpCustomerAccessSecurityGroups.Size = new System.Drawing.Size(740, 134);
            this.gpCustomerAccessSecurityGroups.TabIndex = 0;
            this.gpCustomerAccessSecurityGroups.TabStop = false;
            this.gpCustomerAccessSecurityGroups.Text = "Customer Access Security Groups";
            // 
            // tbluseraccess
            // 
            this.tbluseraccess.ColumnCount = 2;
            this.tbluseraccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbluseraccess.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbluseraccess.Controls.Add(this.lstCustomerAccessIncluded, 1, 1);
            this.tbluseraccess.Controls.Add(this.lblNotIncluded, 0, 0);
            this.tbluseraccess.Controls.Add(this.lblIncluded, 1, 0);
            this.tbluseraccess.Controls.Add(this.lstCustomerAccessNotIncluded, 0, 1);
            this.tbluseraccess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbluseraccess.Location = new System.Drawing.Point(3, 16);
            this.tbluseraccess.Name = "tbluseraccess";
            this.tbluseraccess.RowCount = 2;
            this.tbluseraccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tbluseraccess.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.71429F));
            this.tbluseraccess.Size = new System.Drawing.Size(734, 115);
            this.tbluseraccess.TabIndex = 2;
            // 
            // lstCustomerAccessIncluded
            // 
            this.lstCustomerAccessIncluded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCustomerAccessIncluded.FormattingEnabled = true;
            this.lstCustomerAccessIncluded.Location = new System.Drawing.Point(370, 19);
            this.lstCustomerAccessIncluded.Name = "lstCustomerAccessIncluded";
            this.lstCustomerAccessIncluded.Size = new System.Drawing.Size(361, 93);
            this.lstCustomerAccessIncluded.TabIndex = 1;
            this.lstCustomerAccessIncluded.DoubleClick += new System.EventHandler(this.lstCustomerAccessIncluded_DoubleClick);
            // 
            // lblNotIncluded
            // 
            this.lblNotIncluded.AutoSize = true;
            this.lblNotIncluded.Location = new System.Drawing.Point(3, 0);
            this.lblNotIncluded.Name = "lblNotIncluded";
            this.lblNotIncluded.Size = new System.Drawing.Size(68, 13);
            this.lblNotIncluded.TabIndex = 0;
            this.lblNotIncluded.Text = "Not Included";
            // 
            // lblIncluded
            // 
            this.lblIncluded.AutoSize = true;
            this.lblIncluded.Location = new System.Drawing.Point(370, 0);
            this.lblIncluded.Name = "lblIncluded";
            this.lblIncluded.Size = new System.Drawing.Size(48, 13);
            this.lblIncluded.TabIndex = 1;
            this.lblIncluded.Text = "Included";
            // 
            // lstCustomerAccessNotIncluded
            // 
            this.lstCustomerAccessNotIncluded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCustomerAccessNotIncluded.FormattingEnabled = true;
            this.lstCustomerAccessNotIncluded.Location = new System.Drawing.Point(3, 19);
            this.lstCustomerAccessNotIncluded.Name = "lstCustomerAccessNotIncluded";
            this.lstCustomerAccessNotIncluded.Size = new System.Drawing.Size(361, 93);
            this.lstCustomerAccessNotIncluded.TabIndex = 0;
            this.lstCustomerAccessNotIncluded.DoubleClick += new System.EventHandler(this.lstCustomerAccessNotIncluded_DoubleClick);
            // 
            // chkRepresentative
            // 
            this.chkRepresentative.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRepresentative.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkRepresentative.Location = new System.Drawing.Point(3, 283);
            this.chkRepresentative.Name = "chkRepresentative";
            this.chkRepresentative.Size = new System.Drawing.Size(227, 21);
            this.chkRepresentative.TabIndex = 18;
            this.chkRepresentative.Text = "Is Representative                 ";
            this.chkRepresentative.UseVisualStyleBackColor = true;
            this.chkRepresentative.CheckedChanged += new System.EventHandler(this.ChkRepresentative_CheckedChanged);
            // 
            // chkEngineer
            // 
            this.chkEngineer.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEngineer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEngineer.Location = new System.Drawing.Point(3, 257);
            this.chkEngineer.Name = "chkEngineer";
            this.chkEngineer.Size = new System.Drawing.Size(227, 20);
            this.chkEngineer.TabIndex = 16;
            this.chkEngineer.Text = "Is Engineer                           ";
            this.chkEngineer.UseVisualStyleBackColor = true;
            // 
            // txtJobDescription
            // 
            this.txtJobDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJobDescription.Location = new System.Drawing.Point(236, 231);
            this.txtJobDescription.MaxLength = 30;
            this.txtJobDescription.Name = "txtJobDescription";
            this.txtJobDescription.Size = new System.Drawing.Size(157, 20);
            this.txtJobDescription.TabIndex = 15;
            // 
            // txtDepart
            // 
            this.txtDepart.Location = new System.Drawing.Point(3, 231);
            this.txtDepart.MaxLength = 30;
            this.txtDepart.Name = "txtDepart";
            this.txtDepart.Size = new System.Drawing.Size(173, 20);
            this.txtDepart.TabIndex = 14;
            // 
            // rtfNotes
            // 
            this.tblMainucadmin.SetColumnSpan(this.rtfNotes, 2);
            this.rtfNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfNotes.Location = new System.Drawing.Point(399, 192);
            this.rtfNotes.MaxLength = 255;
            this.rtfNotes.Name = "rtfNotes";
            this.tblMainucadmin.SetRowSpan(this.rtfNotes, 5);
            this.rtfNotes.Size = new System.Drawing.Size(338, 124);
            this.rtfNotes.TabIndex = 13;
            this.rtfNotes.Text = "";
            // 
            // txtExtensionNumber
            // 
            this.txtExtensionNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExtensionNumber.Location = new System.Drawing.Point(236, 192);
            this.txtExtensionNumber.MaxLength = 15;
            this.txtExtensionNumber.Name = "txtExtensionNumber";
            this.txtExtensionNumber.Size = new System.Drawing.Size(157, 20);
            this.txtExtensionNumber.TabIndex = 12;
            this.txtExtensionNumber.Text = " ";
            this.txtExtensionNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtExtensionNumber_KeyPress);
            // 
            // txtPersonnelNo
            // 
            this.txtPersonnelNo.Location = new System.Drawing.Point(3, 192);
            this.txtPersonnelNo.MaxLength = 10;
            this.txtPersonnelNo.Name = "txtPersonnelNo";
            this.txtPersonnelNo.Size = new System.Drawing.Size(173, 20);
            this.txtPersonnelNo.TabIndex = 11;
            this.txtPersonnelNo.Tag = "";
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Location = new System.Drawing.Point(566, 153);
            this.txtEmail.MaxLength = 35;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(171, 20);
            this.txtEmail.TabIndex = 10;
            // 
            // txtMobilePhone
            // 
            this.txtMobilePhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMobilePhone.Location = new System.Drawing.Point(399, 153);
            this.txtMobilePhone.MaxLength = 15;
            this.txtMobilePhone.Name = "txtMobilePhone";
            this.txtMobilePhone.Size = new System.Drawing.Size(161, 20);
            this.txtMobilePhone.TabIndex = 9;
            // 
            // txtHomePhone
            // 
            this.txtHomePhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHomePhone.Location = new System.Drawing.Point(236, 153);
            this.txtHomePhone.MaxLength = 15;
            this.txtHomePhone.Name = "txtHomePhone";
            this.txtHomePhone.Size = new System.Drawing.Size(157, 20);
            this.txtHomePhone.TabIndex = 8;
            this.txtHomePhone.Text = " ";
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(3, 153);
            this.txtPostCode.MaxLength = 10;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(173, 20);
            this.txtPostCode.TabIndex = 7;
            // 
            // cmbUserLevel
            // 
            this.tblMainucadmin.SetColumnSpan(this.cmbUserLevel, 2);
            this.cmbUserLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserLevel.FormattingEnabled = true;
            this.cmbUserLevel.Location = new System.Drawing.Point(399, 96);
            this.cmbUserLevel.Name = "cmbUserLevel";
            this.cmbUserLevel.Size = new System.Drawing.Size(338, 21);
            this.cmbUserLevel.TabIndex = 6;
            // 
            // txtPasswordCheck
            // 
            this.txtPasswordCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPasswordCheck.Location = new System.Drawing.Point(566, 57);
            this.txtPasswordCheck.MaxLength = 50;
            this.txtPasswordCheck.Name = "txtPasswordCheck";
            this.txtPasswordCheck.Size = new System.Drawing.Size(171, 20);
            this.txtPasswordCheck.TabIndex = 5;
            this.txtPasswordCheck.UseSystemPasswordChar = true;
            this.txtPasswordCheck.Leave += new System.EventHandler(this.TxtPasswordCheck_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(399, 57);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(161, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtAddress
            // 
            this.tblMainucadmin.SetColumnSpan(this.txtAddress, 2);
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Location = new System.Drawing.Point(3, 57);
            this.txtAddress.MaxLength = 250;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.tblMainucadmin.SetRowSpan(this.txtAddress, 3);
            this.txtAddress.Size = new System.Drawing.Size(390, 77);
            this.txtAddress.TabIndex = 3;
            // 
            // txtWindowsUsername
            // 
            this.txtWindowsUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWindowsUsername.Location = new System.Drawing.Point(566, 16);
            this.txtWindowsUsername.MaxLength = 30;
            this.txtWindowsUsername.Name = "txtWindowsUsername";
            this.txtWindowsUsername.Size = new System.Drawing.Size(171, 20);
            this.txtWindowsUsername.TabIndex = 2;
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(236, 16);
            this.txtTitle.MaxLength = 5;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(157, 20);
            this.txtTitle.TabIndex = 0;
            // 
            // tblMainucadmin
            // 
            this.tblMainucadmin.ColumnCount = 4;
            this.tblMainucadmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.54362F));
            this.tblMainucadmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.01342F));
            this.tblMainucadmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.56604F));
            this.tblMainucadmin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.79692F));
            this.tblMainucadmin.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tblMainucadmin.Controls.Add(this.txtJobDescription, 1, 11);
            this.tblMainucadmin.Controls.Add(this.txtDepart, 0, 11);
            this.tblMainucadmin.Controls.Add(this.lblJobTitleDescription, 1, 10);
            this.tblMainucadmin.Controls.Add(this.lblDepartment, 0, 10);
            this.tblMainucadmin.Controls.Add(this.txtExtensionNumber, 1, 9);
            this.tblMainucadmin.Controls.Add(this.txtPersonnelNo, 0, 9);
            this.tblMainucadmin.Controls.Add(this.lblNotes, 2, 8);
            this.tblMainucadmin.Controls.Add(this.lblExt, 1, 8);
            this.tblMainucadmin.Controls.Add(this.lblPersonnelNo, 0, 8);
            this.tblMainucadmin.Controls.Add(this.txtEmail, 3, 7);
            this.tblMainucadmin.Controls.Add(this.txtMobilePhone, 2, 7);
            this.tblMainucadmin.Controls.Add(this.txtHomePhone, 1, 7);
            this.tblMainucadmin.Controls.Add(this.txtPostCode, 0, 7);
            this.tblMainucadmin.Controls.Add(this.lblEMail, 3, 6);
            this.tblMainucadmin.Controls.Add(this.lblMobile, 2, 6);
            this.tblMainucadmin.Controls.Add(this.lblTelephone, 1, 6);
            this.tblMainucadmin.Controls.Add(this.lblPostCode, 0, 6);
            this.tblMainucadmin.Controls.Add(this.cmbUserLevel, 2, 5);
            this.tblMainucadmin.Controls.Add(this.txtAddress, 0, 3);
            this.tblMainucadmin.Controls.Add(this.txtPassword, 2, 3);
            this.tblMainucadmin.Controls.Add(this.lblCurrentPassword, 2, 2);
            this.tblMainucadmin.Controls.Add(this.lblPasswordCheck, 3, 2);
            this.tblMainucadmin.Controls.Add(this.lblAddress, 0, 2);
            this.tblMainucadmin.Controls.Add(this.txtWindowsUsername, 3, 1);
            this.tblMainucadmin.Controls.Add(this.txtStaffUserName, 2, 1);
            this.tblMainucadmin.Controls.Add(this.txtTitle, 1, 1);
            this.tblMainucadmin.Controls.Add(this.lblWusername, 3, 0);
            this.tblMainucadmin.Controls.Add(this.lblUserName, 2, 0);
            this.tblMainucadmin.Controls.Add(this.label1, 1, 0);
            this.tblMainucadmin.Controls.Add(this.lblUserLevel, 2, 4);
            this.tblMainucadmin.Controls.Add(this.rtfNotes, 2, 9);
            this.tblMainucadmin.Controls.Add(this.chkEngineer, 0, 12);
            this.tblMainucadmin.Controls.Add(this.chkStockController, 1, 12);
            this.tblMainucadmin.Controls.Add(this.chkRepresentative, 0, 13);
            this.tblMainucadmin.Controls.Add(this.txtPasswordCheck, 3, 3);
            this.tblMainucadmin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainucadmin.Location = new System.Drawing.Point(0, 0);
            this.tblMainucadmin.Name = "tblMainucadmin";
            this.tblMainucadmin.RowCount = 15;
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblMainucadmin.Size = new System.Drawing.Size(740, 325);
            this.tblMainucadmin.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.lblSurname, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtFirstName, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblFirstName, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtSurname, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.cmbSurname, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tblMainucadmin.SetRowSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(227, 38);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSurname.Location = new System.Drawing.Point(3, 0);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(107, 13);
            this.lblSurname.TabIndex = 0;
            this.lblSurname.Text = "* Surname:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(116, 16);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(108, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFirstName.Location = new System.Drawing.Point(116, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(108, 13);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "* FirstName:";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(3, 16);
            this.txtSurname.MaxLength = 50;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(107, 20);
            this.txtSurname.TabIndex = 0;
            // 
            // cmbSurname
            // 
            this.cmbSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.SetColumnSpan(this.cmbSurname, 2);
            this.cmbSurname.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSurname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSurname.Location = new System.Drawing.Point(3, 42);
            this.cmbSurname.Name = "cmbSurname";
            this.cmbSurname.Size = new System.Drawing.Size(221, 21);
            this.cmbSurname.TabIndex = 4;
            this.cmbSurname.SelectedIndexChanged += new System.EventHandler(this.LstSurname_SelectedIndexChanged);
            // 
            // lblJobTitleDescription
            // 
            this.lblJobTitleDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblJobTitleDescription.Location = new System.Drawing.Point(236, 215);
            this.lblJobTitleDescription.Name = "lblJobTitleDescription";
            this.lblJobTitleDescription.Size = new System.Drawing.Size(157, 13);
            this.lblJobTitleDescription.TabIndex = 27;
            this.lblJobTitleDescription.Text = "Job Title/Description:";
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDepartment.Location = new System.Drawing.Point(3, 215);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(227, 13);
            this.lblDepartment.TabIndex = 26;
            this.lblDepartment.Text = "Department:";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNotes.Location = new System.Drawing.Point(399, 176);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(161, 13);
            this.lblNotes.TabIndex = 23;
            this.lblNotes.Text = "Notes:";
            // 
            // lblExt
            // 
            this.lblExt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblExt.Location = new System.Drawing.Point(236, 176);
            this.lblExt.Name = "lblExt";
            this.lblExt.Size = new System.Drawing.Size(157, 13);
            this.lblExt.TabIndex = 22;
            this.lblExt.Text = "Ext:";
            // 
            // lblPersonnelNo
            // 
            this.lblPersonnelNo.AutoSize = true;
            this.lblPersonnelNo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPersonnelNo.Location = new System.Drawing.Point(3, 176);
            this.lblPersonnelNo.Name = "lblPersonnelNo";
            this.lblPersonnelNo.Size = new System.Drawing.Size(227, 13);
            this.lblPersonnelNo.TabIndex = 21;
            this.lblPersonnelNo.Text = "Personnel No:";
            // 
            // lblEMail
            // 
            this.lblEMail.AutoSize = true;
            this.lblEMail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEMail.Location = new System.Drawing.Point(566, 137);
            this.lblEMail.Name = "lblEMail";
            this.lblEMail.Size = new System.Drawing.Size(171, 13);
            this.lblEMail.TabIndex = 16;
            this.lblEMail.Text = "E-Mail:";
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMobile.Location = new System.Drawing.Point(399, 137);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(161, 13);
            this.lblMobile.TabIndex = 15;
            this.lblMobile.Text = "Mobile:";
            // 
            // lblTelephone
            // 
            this.lblTelephone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTelephone.Location = new System.Drawing.Point(236, 137);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(157, 13);
            this.lblTelephone.TabIndex = 14;
            this.lblTelephone.Text = "Telephone:";
            // 
            // lblPostCode
            // 
            this.lblPostCode.AutoSize = true;
            this.lblPostCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPostCode.Location = new System.Drawing.Point(3, 137);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(227, 13);
            this.lblPostCode.TabIndex = 13;
            this.lblPostCode.Text = "Post Code:";
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCurrentPassword.Location = new System.Drawing.Point(399, 41);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(161, 13);
            this.lblCurrentPassword.TabIndex = 6;
            this.lblCurrentPassword.Text = "* Current Password:";
            // 
            // lblPasswordCheck
            // 
            this.lblPasswordCheck.AutoSize = true;
            this.lblPasswordCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblPasswordCheck.Location = new System.Drawing.Point(566, 41);
            this.lblPasswordCheck.Name = "lblPasswordCheck";
            this.lblPasswordCheck.Size = new System.Drawing.Size(171, 13);
            this.lblPasswordCheck.TabIndex = 7;
            this.lblPasswordCheck.Text = "* Password Check:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(3, 41);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 54;
            this.lblAddress.Text = "Address:";
            // 
            // lblWusername
            // 
            this.lblWusername.AutoSize = true;
            this.lblWusername.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblWusername.Location = new System.Drawing.Point(566, 0);
            this.lblWusername.Name = "lblWusername";
            this.lblWusername.Size = new System.Drawing.Size(171, 13);
            this.lblWusername.TabIndex = 2;
            this.lblWusername.Text = "* WindowsUserName:";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUserName.Location = new System.Drawing.Point(399, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(161, 13);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "* UserName:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(236, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // lblUserLevel
            // 
            this.lblUserLevel.AutoSize = true;
            this.lblUserLevel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUserLevel.Location = new System.Drawing.Point(399, 80);
            this.lblUserLevel.Name = "lblUserLevel";
            this.lblUserLevel.Size = new System.Drawing.Size(161, 13);
            this.lblUserLevel.TabIndex = 11;
            this.lblUserLevel.Text = "User Level:";
            // 
            // lblDepot
            // 
            this.lblDepot.AutoSize = true;
            this.lblDepot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDepot.Location = new System.Drawing.Point(126, 1);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(117, 13);
            this.lblDepot.TabIndex = 1;
            this.lblDepot.Text = "Depot:";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSupplier.Location = new System.Drawing.Point(3, 1);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(117, 13);
            this.lblSupplier.TabIndex = 0;
            this.lblSupplier.Text = "Supplier:";
            // 
            // lblDateFormat
            // 
            this.lblDateFormat.AutoSize = true;
            this.lblDateFormat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDateFormat.Location = new System.Drawing.Point(618, 1);
            this.lblDateFormat.Name = "lblDateFormat";
            this.lblDateFormat.Size = new System.Drawing.Size(119, 13);
            this.lblDateFormat.TabIndex = 5;
            this.lblDateFormat.Text = "Date Format:";
            this.lblDateFormat.Visible = false;
            // 
            // lblServiceArea
            // 
            this.lblServiceArea.AutoSize = true;
            this.lblServiceArea.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblServiceArea.Location = new System.Drawing.Point(249, 1);
            this.lblServiceArea.Name = "lblServiceArea";
            this.lblServiceArea.Size = new System.Drawing.Size(117, 13);
            this.lblServiceArea.TabIndex = 2;
            this.lblServiceArea.Text = "Service Area:";
            // 
            // lblUserLanguage
            // 
            this.lblUserLanguage.AutoSize = true;
            this.lblUserLanguage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblUserLanguage.Location = new System.Drawing.Point(372, 1);
            this.lblUserLanguage.Name = "lblUserLanguage";
            this.lblUserLanguage.Size = new System.Drawing.Size(117, 13);
            this.lblUserLanguage.TabIndex = 3;
            this.lblUserLanguage.Text = "User Language:";
            // 
            // lblCurrencyFormat
            // 
            this.lblCurrencyFormat.AutoSize = true;
            this.lblCurrencyFormat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCurrencyFormat.Location = new System.Drawing.Point(495, 1);
            this.lblCurrencyFormat.Name = "lblCurrencyFormat";
            this.lblCurrencyFormat.Size = new System.Drawing.Size(117, 13);
            this.lblCurrencyFormat.TabIndex = 4;
            this.lblCurrencyFormat.Text = "Currency Format:";
            this.lblCurrencyFormat.Visible = false;
            // 
            // tblMain
            // 
            this.tblMain.AutoScroll = true;
            this.tblMain.ColumnCount = 3;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.8356F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.12606F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.03835F));
            this.tblMain.Controls.Add(this.treeOps, 2, 0);
            this.tblMain.Controls.Add(this.gpCustomerAccessSecurityGroups, 0, 2);
            this.tblMain.Controls.Add(this.tblusercombobox, 0, 1);
            this.tblMain.Controls.Add(this.panel6, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(3, 3);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 3;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.74916F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11421F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.13663F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMain.Size = new System.Drawing.Size(934, 529);
            this.tblMain.TabIndex = 0;
            // 
            // treeOps
            // 
            this.treeOps.CheckBoxes = true;
            this.treeOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOps.Location = new System.Drawing.Point(749, 3);
            this.treeOps.Name = "treeOps";
            this.tblMain.SetRowSpan(this.treeOps, 3);
            this.treeOps.Size = new System.Drawing.Size(182, 523);
            this.treeOps.TabIndex = 0;
            // 
            // tblusercombobox
            // 
            this.tblusercombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tblusercombobox.ColumnCount = 6;
            this.tblMain.SetColumnSpan(this.tblusercombobox, 2);
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblusercombobox.Controls.Add(this.lblDateFormat, 5, 0);
            this.tblusercombobox.Controls.Add(this.lblServiceArea, 2, 0);
            this.tblusercombobox.Controls.Add(this.cmbDate, 5, 1);
            this.tblusercombobox.Controls.Add(this.lblDepot, 1, 0);
            this.tblusercombobox.Controls.Add(this.lblSupplier, 0, 0);
            this.tblusercombobox.Controls.Add(this.cmbSupplier, 0, 1);
            this.tblusercombobox.Controls.Add(this.cmbDepot, 1, 1);
            this.tblusercombobox.Controls.Add(this.cmdServiceArea, 2, 1);
            this.tblusercombobox.Controls.Add(this.lblUserLanguage, 3, 0);
            this.tblusercombobox.Controls.Add(this.cmbLanguage, 3, 1);
            this.tblusercombobox.Controls.Add(this.lblCurrencyFormat, 4, 0);
            this.tblusercombobox.Controls.Add(this.cmbCurrency, 4, 1);
            this.tblusercombobox.Location = new System.Drawing.Point(3, 334);
            this.tblusercombobox.Name = "tblusercombobox";
            this.tblusercombobox.RowCount = 2;
            this.tblusercombobox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.94118F));
            this.tblusercombobox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.05882F));
            this.tblusercombobox.Size = new System.Drawing.Size(740, 52);
            this.tblusercombobox.TabIndex = 1;
            // 
            // cmbDate
            // 
            this.cmbDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDate.FormattingEnabled = true;
            this.cmbDate.Location = new System.Drawing.Point(618, 17);
            this.cmbDate.Name = "cmbDate";
            this.cmbDate.Size = new System.Drawing.Size(119, 21);
            this.cmbDate.TabIndex = 5;
            this.cmbDate.Visible = false;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(3, 17);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(117, 21);
            this.cmbSupplier.Sorted = true;
            this.cmbSupplier.TabIndex = 0;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.CmbSupplier_SelectedIndexChanged);
            // 
            // cmbDepot
            // 
            this.cmbDepot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDepot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(126, 17);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(117, 21);
            this.cmbDepot.Sorted = true;
            this.cmbDepot.TabIndex = 1;
            this.cmbDepot.SelectedIndexChanged += new System.EventHandler(this.CmbDepot_SelectedIndexChanged);
            // 
            // cmdServiceArea
            // 
            this.cmdServiceArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdServiceArea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmdServiceArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmdServiceArea.FormattingEnabled = true;
            this.cmdServiceArea.Location = new System.Drawing.Point(249, 17);
            this.cmdServiceArea.Name = "cmdServiceArea";
            this.cmdServiceArea.Size = new System.Drawing.Size(117, 21);
            this.cmdServiceArea.Sorted = true;
            this.cmdServiceArea.TabIndex = 2;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(372, 17);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(117, 21);
            this.cmbLanguage.TabIndex = 3;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCurrency.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(495, 17);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(117, 21);
            this.cmbCurrency.TabIndex = 4;
            this.cmbCurrency.Visible = false;
            // 
            // panel6
            // 
            this.panel6.AutoScroll = true;
            this.tblMain.SetColumnSpan(this.panel6, 2);
            this.panel6.Controls.Add(this.tblMainucadmin);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(740, 325);
            this.panel6.TabIndex = 0;
            // 
            // tblbuttons
            // 
            this.tblbuttons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tblbuttons.ColumnCount = 10;
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.tblbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tblbuttons.Controls.Add(this.btnResetPassword, 7, 0);
            this.tblbuttons.Controls.Add(this.flowLayoutPanel2, 9, 0);
            this.tblbuttons.Controls.Add(this.flowLayoutPanel3, 8, 0);
            this.tblbuttons.Controls.Add(this.btnAssignSite, 6, 0);
            this.tblbuttons.Controls.Add(this.flowLayoutPanel1, 5, 0);
            this.tblbuttons.Controls.Add(this.chkTerminate, 4, 0);
            this.tblbuttons.Location = new System.Drawing.Point(192, 538);
            this.tblbuttons.Name = "tblbuttons";
            this.tblbuttons.RowCount = 1;
            this.tblbuttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblbuttons.Size = new System.Drawing.Size(745, 34);
            this.tblbuttons.TabIndex = 0;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetPassword.Location = new System.Drawing.Point(430, 3);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(100, 28);
            this.btnResetPassword.TabIndex = 2;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.BtnEdit);
            this.flowLayoutPanel2.Controls.Add(this.btnCancel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(640, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(105, 34);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // BtnEdit
            // 
            this.BtnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnEdit.Location = new System.Drawing.Point(3, 3);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(100, 28);
            this.BtnEdit.TabIndex = 0;
            this.BtnEdit.Text = "Edit";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(3, 37);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.btnUpdate);
            this.flowLayoutPanel3.Controls.Add(this.btnNew);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(533, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(107, 34);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(3, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 28);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(3, 37);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 28);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New User";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAssignSite
            // 
            this.btnAssignSite.Location = new System.Drawing.Point(320, 3);
            this.btnAssignSite.Name = "btnAssignSite";
            this.btnAssignSite.Size = new System.Drawing.Size(100, 28);
            this.btnAssignSite.TabIndex = 1;
            this.btnAssignSite.Text = "Assign Sites";
            this.btnAssignSite.UseVisualStyleBackColor = true;
            this.btnAssignSite.Click += new System.EventHandler(this.btnAssignSite_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnUnLockUser);
            this.flowLayoutPanel1.Controls.Add(this.btnLockUser);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(209, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(105, 34);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // btnUnLockUser
            // 
            this.btnUnLockUser.Location = new System.Drawing.Point(3, 3);
            this.btnUnLockUser.Name = "btnUnLockUser";
            this.btnUnLockUser.Size = new System.Drawing.Size(100, 28);
            this.btnUnLockUser.TabIndex = 0;
            this.btnUnLockUser.Text = "UnLock User";
            this.btnUnLockUser.UseVisualStyleBackColor = true;
            this.btnUnLockUser.Click += new System.EventHandler(this.btnUnLockUser_Click);
            // 
            // btnLockUser
            // 
            this.btnLockUser.Location = new System.Drawing.Point(3, 37);
            this.btnLockUser.Name = "btnLockUser";
            this.btnLockUser.Size = new System.Drawing.Size(100, 28);
            this.btnLockUser.TabIndex = 0;
            this.btnLockUser.Text = "Lock User";
            this.btnLockUser.UseVisualStyleBackColor = true;
            this.btnLockUser.Click += new System.EventHandler(this.btnLockUser_Click);
            // 
            // chkTerminate
            // 
            this.chkTerminate.AutoSize = true;
            this.chkTerminate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkTerminate.Location = new System.Drawing.Point(107, 3);
            this.chkTerminate.Name = "chkTerminate";
            this.chkTerminate.Size = new System.Drawing.Size(98, 17);
            this.chkTerminate.TabIndex = 0;
            this.chkTerminate.Text = "Terminate User";
            this.chkTerminate.UseVisualStyleBackColor = true;
            this.chkTerminate.CheckedChanged += new System.EventHandler(this.chkTerminate_CheckedChanged);
            // 
            // tblAdminusers
            // 
            this.tblAdminusers.ColumnCount = 1;
            this.tblAdminusers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAdminusers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblAdminusers.Controls.Add(this.tblMain, 0, 0);
            this.tblAdminusers.Controls.Add(this.tblbuttons, 0, 1);
            this.tblAdminusers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAdminusers.Location = new System.Drawing.Point(0, 0);
            this.tblAdminusers.Name = "tblAdminusers";
            this.tblAdminusers.RowCount = 2;
            this.tblAdminusers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAdminusers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tblAdminusers.Size = new System.Drawing.Size(940, 575);
            this.tblAdminusers.TabIndex = 1;
            // 
            // ucUserAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tblAdminusers);
            this.Name = "ucUserAdmin";
            this.Size = new System.Drawing.Size(940, 575);
            this.Load += new System.EventHandler(this.ucUserAdmin_Load);
            this.gpCustomerAccessSecurityGroups.ResumeLayout(false);
            this.tbluseraccess.ResumeLayout(false);
            this.tbluseraccess.PerformLayout();
            this.tblMainucadmin.ResumeLayout(false);
            this.tblMainucadmin.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tblMain.ResumeLayout(false);
            this.tblusercombobox.ResumeLayout(false);
            this.tblusercombobox.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.tblbuttons.ResumeLayout(false);
            this.tblbuttons.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tblAdminusers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

     


        #endregion

        private System.Windows.Forms.TextBox txtStaffUserName;
        private System.Windows.Forms.CheckBox chkStockController;
        private System.Windows.Forms.GroupBox gpCustomerAccessSecurityGroups;
        private System.Windows.Forms.TableLayoutPanel tblMainucadmin;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtWindowsUsername;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPasswordCheck;
        private System.Windows.Forms.ComboBox cmbUserLevel;
        private System.Windows.Forms.TextBox txtPostCode;
        private System.Windows.Forms.TextBox txtHomePhone;
        private System.Windows.Forms.TextBox txtMobilePhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPersonnelNo;
        private System.Windows.Forms.TextBox txtExtensionNumber;
        private System.Windows.Forms.RichTextBox rtfNotes;
        private System.Windows.Forms.TextBox txtDepart;
        private System.Windows.Forms.TextBox txtJobDescription;
        private System.Windows.Forms.CheckBox chkEngineer;
        private System.Windows.Forms.CheckBox chkRepresentative;
        private System.Windows.Forms.ListBox lstCustomerAccessIncluded;
        private System.Windows.Forms.ListBox lstCustomerAccessNotIncluded;
        private System.Windows.Forms.Label lblIncluded;
        private System.Windows.Forms.Label lblNotIncluded;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.Label lblTelephone;
        private System.Windows.Forms.Label lblExt;
        private System.Windows.Forms.Label lblJobTitleDescription;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPostCode;
        private System.Windows.Forms.Label lblPersonnelNo;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPasswordCheck;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblWusername;
        private System.Windows.Forms.Label lblUserLevel;
        private System.Windows.Forms.Label lblEMail;
        private System.Windows.Forms.Label lblDepot;
        private BmcComboBox cmbDepot;
        private System.Windows.Forms.Label lblSupplier;
        private BmcComboBox cmbSupplier;
        private System.Windows.Forms.Label lblDateFormat;
        private BmcComboBox cmbDate;
        private System.Windows.Forms.Label lblServiceArea;
        private BmcComboBox cmdServiceArea;
        private System.Windows.Forms.Label lblUserLanguage;
        private BmcComboBox cmbLanguage;
        private System.Windows.Forms.Label lblCurrencyFormat;
        private BmcComboBox cmbCurrency;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtFirstName;
        private BmcComboBox cmbSurname;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TableLayoutPanel tblusercombobox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tbluseraccess;
        private System.Windows.Forms.TableLayoutPanel tblbuttons;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnAssignSite;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnUnLockUser;
        private System.Windows.Forms.Button btnLockUser;
        private System.Windows.Forms.CheckBox chkTerminate;
        private System.Windows.Forms.TableLayoutPanel tblAdminusers;
        private System.Windows.Forms.TreeView treeOps;


    }
}
