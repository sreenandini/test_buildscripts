using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using Audit.BusinessClasses;
using Audit.Transport;
using System.Text.RegularExpressions;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using BMC.CoreLib.Win32;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{

    public delegate void OnOpenSubcompany();
    public delegate void OnOpenExisitingSubCompanyHandler(OrganisationDetailsEntity _OrganisationDetailsEntity);
    public delegate void OnOpenSiteHandler(OrganisationDetailsEntity _OrganisationSiteEntity);//
    public delegate void RefreshOnSaveCompany(int CompanyID);

    public partial class ucAdminCompany : UserControl
    {

        AdminCompany objAdminCompanyBiz = new AdminCompany();
        public RefreshOnSaveCompany NotifyChanges;
        public OnOpenExisitingSubCompanyHandler OpenSubCompanyForm;
        public OnOpenSite OpenSubsiteForm;


        AdminCompanyResult objAdminCompanyResult = null;
        private int _iCompanyID = 0;
        private string _NewTabcompany;

        public ucAdminCompany()
        {
            try
            {
                InitializeComponent();
                SetTagProperty();
                this.ResolveResources();
                this.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        //Load existing company details when CompanyID>0
        public ucAdminCompany(int CompanyID)
            : this()
        {
            try
            {
                _iCompanyID = CompanyID;

                LoadCompanyDetails();

                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company_New"))
                {
                    btn_AddnewCompany.Visible = false;
                }
                if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Sub_New"))
                    btnNewSubCompany.Visible = false;

                if (_iCompanyID > 0)
                {
                    if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company_Edit"))
                    {
                        btnDetailsSave.Visible = false;

                        foreach (Control ct in this.tblDetails.Controls)
                        {
                            if (ct.GetType() == typeof(TextBox))
                            {
                                ((TextBox)ct).ReadOnly = true;
                                ((TextBox)ct).BackColor = Color.White;
                            }
                        }

                        foreach (Control ct in this.tblInvoice.Controls)
                        {
                            if (ct.GetType() == typeof(TextBox))
                            {
                                ((TextBox)ct).ReadOnly = true;
                                ((TextBox)ct).BackColor = Color.White;
                            }
                        }

                        if (!AppGlobals.Current.HasUserAccess("HQ_Admin_Customers_Company_New"))
                        {
                            this.tblMain.RowStyles[0].Height = 30;
                            lblErrorMessage.Text = this.GetResourceTextByKey(1, "MSG_USER") + AppGlobals.Current.UserName + " " + this.GetResourceTextByKey(1, "MSG_NOPERMISSIONTOEDIT");
                        }
                    }
                    else
                    {
                        this.tblMain.RowStyles[0].Height = 0;
                    }
                }
                else
                {
                    this.tblMain.RowStyles[0].Height = 0;
                    if (this.FindForm()==null || ((TreeView)this.FindForm().Controls.Find("trvOrganisation", true)[0]).Nodes.Count == 0)
                    {
                        btn_AddnewCompany.Enabled = true;
                        btnNewSubCompany.Enabled = false;
                        btnDetailsClose.Enabled = false;
                        btnDetailsSave.Enabled = false;
                        grpDetails.Visible = false;
                        grpInvoice.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public OrganisationDetailsEntity Entity { get; set; }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.lblName.Tag = "Key_NameMandatory";
            this.lblAddress.Tag = "Key_AddressColon";
            this.btnDetailsClose.Tag = "Key_Close";
            this.lblContactEMail.Tag = "Key_ContactEMailColon";
            this.lblContactName.Tag = "Key_ContactNameColon";
            this.lblContactPhoneNo.Tag = "Key_ContactPhoneNumber";
            this.lblInvoiceAddress.Tag = "Key_InvoiceAddressColon";
            this.lblInvoiceName.Tag = "Key_InvoiceNameColon";
            this.lblInvoicePostCode.Tag = "Key_InvoicePostCode";
            this.btn_AddnewCompany.Tag = "Key_NewCompany";
            this.btnNewSubCompany.Tag = "Key_NewSubCompany";
            this.lblPostcode.Tag = "Key_PostcodeColon";
            this.btnDetailsSave.Tag = "Key_Apply";
            this.lblSPhoneNumber.Tag = "Key_SwitchboardPhoneNumber";
            this.grpDetails.Tag = "Key_Details";
            this.grpInvoice.Tag = "Key_Invoice";
        }

        public void LoadCompanyDetails()
        {

            try
            {
                //COMPANY DETAILS
                objAdminCompanyResult = new AdminCompany().GetCompanyDetails(_iCompanyID);
                if (_iCompanyID != 0)
                {
                    _NewTabcompany = objAdminCompanyResult.CompanyEntity.Company_Name;
                    txtName.Text = objAdminCompanyResult.CompanyEntity.Company_Name;
                    txtAddress1.Text = objAdminCompanyResult.CompanyEntity.Company_Address_1;
                    txtAddress2.Text = objAdminCompanyResult.CompanyEntity.Company_Address_2;
                    txtAddress3.Text = objAdminCompanyResult.CompanyEntity.Company_Address_3;
                    txtAddress4.Text = objAdminCompanyResult.CompanyEntity.Company_Address_4;
                    txtAddress5.Text = objAdminCompanyResult.CompanyEntity.Company_Address_5;
                    txtSPhoneNumber.Text = objAdminCompanyResult.CompanyEntity.Company_Switchboard_Phone_No;
                    txtContactEMail.Text = objAdminCompanyResult.CompanyEntity.Company_Contact_Email_Address;
                    txtContactName.Text = objAdminCompanyResult.CompanyEntity.Company_Contact_Name;
                    txtContactPhoneNo.Text = objAdminCompanyResult.CompanyEntity.Company_Contact_Phone_No;
                    txtPostcode.Text = objAdminCompanyResult.CompanyEntity.Company_Postcode;
                    txtInvoiceName.Text = objAdminCompanyResult.CompanyEntity.Company_Invoice_Name;
                    txtInvoiceAddress.Text = objAdminCompanyResult.CompanyEntity.Company_Invoice_Address;
                    txtInvoicePostCode.Text = objAdminCompanyResult.CompanyEntity.Company_Invoice_Postcode;
                }
              

                //Disable default controls for new company 
                //txtInvoiceAddress.Enabled = txtInvoiceName.Enabled = txtInvoicePostCode.Enabled = _iCompanyID > 0 ? true : false;


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }

        private void btnDetailsSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsNewCompany = true;
                int iReturnValue = 0;
                CompanyEntity objCompanyEntity = new CompanyEntity();
                try
                {
                    if (_iCompanyID > 0)
                        IsNewCompany = false;
                    if (txtName.CheckEmptyAndReturnOnError(this.GetResourceTextByKey(1, "MSG_COMPANY_NAME_EMPTY"), this.GetResourceTextByKey(1, "MSG_COMPANY_ADMIN"), MessageBoxIcon.Warning))
                        objCompanyEntity.Company_Name = txtName.Text.Trim();
                    else
                        return;
                    objCompanyEntity.Company_Address_1 = txtAddress1.Text.Trim();
                    objCompanyEntity.Company_Address_2 = txtAddress2.Text.Trim();
                    objCompanyEntity.Company_Address_3 = txtAddress3.Text.Trim();
                    objCompanyEntity.Company_Address_4 = txtAddress4.Text.Trim();
                    objCompanyEntity.Company_Address_5 = txtAddress5.Text.Trim();
                    objCompanyEntity.Company_Switchboard_Phone_No = txtSPhoneNumber.Text.Trim();
                    if (txtContactEMail.ValidateEmailAndReturnOnError(this.GetResourceTextByKey(1, "MSG_INVALID_EMAIL_ID"), this.GetResourceTextByKey(1, "MSG_COMPANY_ADMIN"), MessageBoxIcon.Warning))
                        objCompanyEntity.Company_Contact_Email_Address = txtContactEMail.Text.Trim();
                    else
                        return;
                    objCompanyEntity.Company_Contact_Name = txtContactName.Text.Trim();
                    objCompanyEntity.Company_Contact_Phone_No = txtContactPhoneNo.Text.Trim();

                    //Refer CR#164196 for commenting this code
                    //if (txtPostcode.CheckNumericAndReturnOnError("Postcode should be numeric", "Company Admin", MessageBoxIcon.Warning))
                    objCompanyEntity.Company_Postcode = txtPostcode.Text.Trim();
                    /*else
                        return;
                     * Comment ends
                     */

                    objCompanyEntity.Company_Invoice_Name = txtInvoiceName.Text.Trim();
                    objCompanyEntity.Company_Invoice_Address = txtInvoiceAddress.Text.Trim();
                    objCompanyEntity.Company_Invoice_Postcode = txtInvoicePostCode.Text.Trim();

                    iReturnValue = objAdminCompanyBiz.UpdateCompanyDetails(_iCompanyID, objCompanyEntity);
                    if (iReturnValue == -1)
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_COMPANY_NAME_EXISTS"), this.ParentForm.Text);
                        txtName.Focus();
                        return;
                    }
                    _iCompanyID = iReturnValue;

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SAVE_SUCCESS"), this.ParentForm.Text);
                    
                    btnNewSubCompany.Enabled = true;
                    btn_AddnewCompany.Enabled = true;
                    //Notify Change to main screen 
                    if (NotifyChanges != null)
                    {
                        NotifyChanges(_iCompanyID);
                    }
                    //CHECK FOR AUDITING CHANGES 
                }
                catch (Exception Ex)
                {

                    Win32Extensions.ShowErrorMessageBox(this, this.GetResourceTextByKey(1, "MSG_ERROR_UPDATING_COMPANY"), this.ParentForm.Text);
                    ExceptionManager.Publish(Ex);
                }

                bool AuditOperationType = false;
                try
                {
                    int iCount = 0;

                    StringBuilder oStringBuild = new StringBuilder();
                    if (!IsNewCompany)
                    {

                        string sOldValue = string.Empty;
                        string sNewValue = string.Empty;
                        foreach (ChangeHistory ch in objCompanyEntity.GetUpdatedFields(objAdminCompanyResult.CompanyEntity))
                        {
                            oStringBuild.Append(ch.ProperyName).Append(",");
                            iCount = oStringBuild.ToString().Split(',').Count();
                            if (iCount == 2)
                            {
                                sOldValue = ch.OldValue;
                                sNewValue = ch.NewValue;
                            }
                        }

                        if (iCount > 2)
                        {
                            AuditOperationType = true;
                            Audit(oStringBuild.ToString(), "", "", "Company [" + oStringBuild.ToString() + "] Modified", AuditOperationType);
                        }
                        else if (iCount != 0)
                        {
                            AuditOperationType = true;
                            Audit(oStringBuild.ToString(), sOldValue, sNewValue, string.Format("Company {0} Modified ..[{1}]:{2}-->{3}", objAdminCompanyResult.CompanyEntity.Company_Name, oStringBuild.ToString(), sOldValue, sNewValue), AuditOperationType);
                        }
                    }
                    else
                    {
                        foreach (ChangeHistory ch in objCompanyEntity.GetInitialFields())
                        {
                            oStringBuild.Append(ch.ProperyName).Append(",");
                        }
                        Audit(oStringBuild.ToString(), "", txtName.Text, "Company " + txtName.Text + " Added.", AuditOperationType);


                    }
                    AuditOperationType = false;
                }
                catch (Exception Ex)
                {
                    ExceptionManager.Publish(Ex);
                    AuditOperationType = false;
                }

                LoadCompanyDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


        public void Audit(string Audit_Field, string sOldValue, string sNewValue, string Desc, bool AuditOperationType)
        {
            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
            business.InsertAuditData(new Audit.Transport.Audit_History
            {
                EnterpriseModuleName = ModuleNameEnterprise.Audit_Company,
                Audit_Slot = "",
                Audit_Screen_Name = "Company Administration",
                Audit_Field = Audit_Field,
                Audit_Old_Vl = sOldValue,
                Audit_New_Vl = sNewValue,
                Audit_Desc = Desc,
                AuditOperationType = (AuditOperationType == true) ? OperationType.MODIFY : OperationType.ADD,
                Audit_User_ID = AppEntryPoint.Current.UserId,
                Audit_User_Name = AppEntryPoint.Current.UserName
            }, false);
        }

        private void btnDetailsClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (((TreeView)this.FindForm().Controls.Find("trvOrganisation", true)[0]).Nodes.Count != 0)
                    ((Panel)this.Parent).Controls.Remove(this);
                else
                {
                    btn_AddnewCompany.Enabled = true;
                    btnNewSubCompany.Enabled = false;
                    btnDetailsClose.Enabled = false;
                    btnDetailsSave.Enabled = false;
                    grpDetails.Visible = false;
                    grpInvoice.Visible = false;

                }
                 //((Panel)this.Parent).FindForm().
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_AddnewCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _iCompanyID = 0;
                this.ResetControls();
                btnNewSubCompany.Enabled = false;
                btn_AddnewCompany.Enabled = false;

                //For Fresh company creation
                btnDetailsClose.Enabled = true;
                btnDetailsSave.Enabled = true;
                grpDetails.Visible = true;
                grpInvoice.Visible = true;

                foreach (Control ct in this.tblDetails.Controls)
                {
                    if (ct.GetType() == typeof(TextBox))
                    {
                        ((TextBox)ct).ReadOnly = false;
                        ((TextBox)ct).BackColor = Color.White;
                    }
                }

                foreach (Control ct in this.tblInvoice.Controls)
                {
                    if (ct.GetType() == typeof(TextBox))
                    {
                        ((TextBox)ct).ReadOnly = false;
                        ((TextBox)ct).BackColor = Color.White;
                    }
                }

                this.LoadCompanyDetails();
                //For Fresh company creation
                btnDetailsClose.Enabled = true;
                btnDetailsSave.Enabled = true;
                grpDetails.Visible = true;
                grpInvoice.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void ResetControls()
        {
            try
            {
                txtName.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtAddress3.Text = string.Empty;
                txtAddress4.Text = string.Empty;
                txtAddress5.Text = string.Empty;
                txtSPhoneNumber.Text = string.Empty;
                txtContactEMail.Text = string.Empty;
                txtContactName.Text = string.Empty;
                txtContactPhoneNo.Text = string.Empty;
                txtPostcode.Text = string.Empty;
                txtInvoiceName.Text = string.Empty;
                txtInvoiceAddress.Text = string.Empty;
                txtInvoicePostCode.Text = string.Empty;

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

        }

        private void btnNewSubCompany_Click(object sender, EventArgs e)
        {
            try
            {
                if (_iCompanyID != 0)
                {
                    objAdminCompanyResult = new AdminCompany().GetCompanyDetails(_iCompanyID);
                    if (OpenSubCompanyForm != null)
                    {
                        OpenSubCompanyForm(this.Entity);
                    }
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_CREATE_COMPANY"), this.ParentForm.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }


    }

    public static class Extension
    {
        public static bool CheckEmptyAndReturnOnError(this TextBox control, String ErrorMessage, String Caption, MessageBoxIcon Icon)
        {
            try
            {
                if (control.Text.Trim() == string.Empty)
                {
                    control.Focus();
                    MessageBox.Show(ErrorMessage, Caption, MessageBoxButtons.OK, Icon);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            return true;
        }

        public static bool ValidateEmailAndReturnOnError(this TextBox control, String ErrorMessage, String Caption, MessageBoxIcon Icon)
        {
            try
            {
                if (control.Text.Trim() != string.Empty && !control.Text.Trim().ValidateEmail())
                {
                    control.Focus();
                    MessageBox.Show(ErrorMessage, Caption, MessageBoxButtons.OK, Icon);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;

            }
            return true;
        }
        
        public static bool ValidateEmail(this string EMailID)
        {
            string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

            return Regex.IsMatch(EMailID, MatchEmailPattern);
        }

        public static bool IsNumeric(this string Str)
        {
            try
            {
                long.Parse(Str);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public static string NullToString(this string str)
        {
            if (str == null)
                return string.Empty;
            else
                return str;
        }
    }
}

