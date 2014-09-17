using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.LogManagement;
using BMC.EnterpriseClient.Helpers;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmAddEmployeeCard : Form
    {

        #region Declarations
        bool bWasMasterCard = false;
        private EmployeeCardBiz objEmployeeCardBiz = null;
        frmAdminUtilities frmUtobj = new frmAdminUtilities();
        List<EmployeeCardEntity> lstEmpCards = null;
        List<string> _LoadValues = new List<string>();
        List<string> _SaveValues = new List<string>();
        bool bRevokeAll = false;
        private BMC.EnterpriseClient.Helpers.Datawatcher ObjDataWatcher = null;
        #endregion

        public frmAddEmployeeCard()
        {
            InitializeComponent();
            setTagProperty();
            ObjDataWatcher = new Datawatcher(this);
        }

        private void setTagProperty()
        {
            this.lblEmpCardnumber.Tag = "Key_CardNumberMandatory";
            this.optActiveCard.Tag = "Key_ActiveCard";
            this.Tag = "Key_AddEmployeeCard";
            this.btnClose.Tag = "Key_Close";
            this.optInactiveCard.Tag = "Key_InActiveCard";
            this.btnSave.Tag = "Key_SaveCaption";
        }
        //Rajkumar For Unsaved Message
        private void OnLoadData()
        {
            try
            {
                _LoadValues.Clear();
                _LoadValues.Add(optActiveCard.Checked.ToString());
                _LoadValues.Add(optInactiveCard.Checked.ToString());
                _LoadValues.Add(cmbEmpCardNumber.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void OnSaveData()
        {
            try
            {
                _SaveValues.Clear();
                _SaveValues.Add(optActiveCard.Checked.ToString());
                _SaveValues.Add(optInactiveCard.Checked.ToString());
                _SaveValues.Add(cmbEmpCardNumber.Text);
            }
            catch (Exception ex)
            {
               ExceptionManager.Publish(ex);                
            }
        }    
        // End Rajkumar Unsaved Message
        private void frmAddEmployeeCard_Load(object sender, EventArgs e)
        {
            this.ResolveResources();
            objEmployeeCardBiz = EmployeeCardBiz.CreateInstance();
            LoadData();
            OnLoadData();
        }

        private void LoadData()
        {
            ClearText();
            FillEmpCardNumber(string.Empty);
        }


        /// <summary>
        /// reset the values .
        /// </summary>
        private void ClearText()
        {
            cmbEmpCardNumber.SelectedIndex = -1;
            optActiveCard.Checked = true;
            optInactiveCard.Checked = false;
        }

        /// <summary>
        /// Get the List of Employee card details.
        /// <paramref name=""/>
        /// <returns></returns>
        /// </summary>
        private void FillEmpCardNumber(string EmpCardNo)
        {
            try
            {

                lstEmpCards = objEmployeeCardBiz.GetEmployeeCardInfo(string.IsNullOrEmpty(EmpCardNo) ? null : EmpCardNo);
                cmbEmpCardNumber.DataSource = lstEmpCards == null ? new List<EmployeeCardEntity>() : lstEmpCards.GroupBy(emp => emp.EmpID).Select(emp => emp.First()).ToList();
                cmbEmpCardNumber.DisplayMember = "EmployeeCardNumber";
                cmbEmpCardNumber.SelectedIndex = -1;
                cmbEmpCardNumber.AutoCompleteMode = AutoCompleteMode.Suggest;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// Validate all the controls in the Screen
        /// </summary>
        /// <param name="ErrorMsg">Returs the Error message</param>
        /// <returns></returns>
        bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (!(cmbEmpCardNumber.Text.IsNumeric()))
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_VALID_ENTER");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cmbEmpCardNumber.Focus();
                    retVal = false;
                }
                else if (!cmbEmpCardNumber.Text.IsLengthGreaterThanZero())
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_VALID_SELECT");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cmbEmpCardNumber.Focus();
                    retVal = false;
                }

                else if (cmbEmpCardNumber.Text.Trim().Length > 10)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_INVALID_CHARACTERS");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cmbEmpCardNumber.Focus();
                    retVal = false;
                }
                else if (cmbEmpCardNumber.Text.TrimStart('0') == string.Empty)
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_ZERO");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cmbEmpCardNumber.Focus();
                    retVal = false;
                }
                else if (isInValidCard())
                {
                    ErrorMsg = this.GetResourceTextByKey(1, "MSG_ENT_EMP_CARD_EXITS");
                    Win32Extensions.ShowErrorMessageBox(this, ErrorMsg, this.Text);
                    cmbEmpCardNumber.Focus();
                    retVal = false;
                }
                else
                    retVal = true;

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = ex.Message;
                retVal = false;
            }
            return retVal;

        }

        private bool isInValidCard()
        {
            int iEmpCardNumber = 0;
            bool bResult = false;

            try
            {
                iEmpCardNumber = Convert.ToInt32(cmbEmpCardNumber.Text);
                List<EmployeeCardEntity> lstCardDetails = cmbEmpCardNumber.Items.Cast<EmployeeCardEntity>().ToList();

                bResult = lstCardDetails.Where(empcard =>
                    iEmpCardNumber == Convert.ToInt32(empcard.EmployeeCardNumber) &&
                                                      cmbEmpCardNumber.Text.Trim().CompareTo(empcard.EmployeeCardNumber) != 0).Count() > 0 ? true : false;
            }
            catch
            {
                bResult = true;
            }

            return bResult;
        }

        /// <summary>
        /// Save the Employee card details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string ErrorMessage = string.Empty;

            //validations
            try
            {
                if (ValidateControls(ref ErrorMessage))
                {
                    string TempcmbCardNumber;
                    TempcmbCardNumber = cmbEmpCardNumber.Text.Trim();
                    EmployeeCardEntity oEnt = lstEmpCards.Find(o => o.EmployeeCardNumber == cmbEmpCardNumber.Text);
                    if (oEnt == null)
                    {
                        bool Result = objEmployeeCardBiz.InsertEmployeeCardDetails(cmbEmpCardNumber.Text.Trim(), null, null,
                                    optActiveCard.Checked ? true : false,
                                    AppEntryPoint.Current.UserName);
                        if (Result)
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_EMP_SUCCESS"), this.Text);
                        else
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ENT_EMP_ALREADY_EXISTS"), this.Text);

                        lstEmpCards = objEmployeeCardBiz.GetEmployeeCardDetails(cmbEmpCardNumber.Text);

                        int? iExportUser = 0;

                        foreach (EmployeeCardEntity eCard in lstEmpCards.GroupBy(emp => emp.EmpID).Select(emp => emp.First()).ToList())
                        {
                            //Gets login user id in absence of card user id
                            iExportUser = eCard.UserID == 0 ? -1 : eCard.UserID;                          

                            if (eCard.UserID != 0)
                                objEmployeeCardBiz.InsertExportHistory(iExportUser.ToString(), iExportUser, "UserDetails", "ALL");
                            //Calling Audit Method
                            AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.EmployeeCard,
                                Audit_Slot = "",
                                Audit_Screen_Name = "Employee card Details",
                                Audit_Field = cmbEmpCardNumber.Text,
                                Audit_Old_Vl = optActiveCard.Checked ? "Assigned" : "Unassigned",
                                Audit_New_Vl = optActiveCard.Checked ? "Active" : "InActive",
                                Audit_Desc = "Employee card Details" +
                                    cmbEmpCardNumber.Text + ((Result) ? "' inserted   .. '" : "' updated'"),
                                AuditOperationType = OperationType.ADD,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);

                            FillEmpCardNumber(string.Empty);
                            frmUtobj.setListBox(cmbEmpCardNumber, TempcmbCardNumber, 0);
                            this.Close();
                        }
                    }
                    else
                        this.ShowInfoMessageBox("Employee Card: " + oEnt.EmployeeCardNumber + " already exists");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }
        public bool CompareItems(List<string>LoadValues,List<string>SaveValues)
        {
            try
            {
                string _LoadString = "";
                string _SaveString = "";
                foreach (var item in LoadValues)
                {
                    StringBuilder obj = new StringBuilder(_LoadString);
                    _LoadString = obj.Append(item.ToString()).ToString();
                }
                foreach (var item in SaveValues)
                {
                    StringBuilder obj = new StringBuilder(_SaveString);
                    _SaveString = obj.Append(item.ToString()).ToString();
                }

                if (_LoadString == _SaveString)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;    
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                OnSaveData();
                if (CompareItems(_LoadValues, _SaveValues))
                {
                    ObjDataWatcher.DataModify = true;
                }
                else
                {
                    ObjDataWatcher.DataModify = false;
                }

               this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.Close();
            }
        }

        /// <summary>
        /// Fill the Employee details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmpCardNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EmployeeCardEntity entity = (cmbEmpCardNumber.SelectedItem as EmployeeCardEntity);                
                optActiveCard.Checked = ((bool)entity.IsActive) ? true : false;
                optInactiveCard.Checked = ((bool)entity.IsActive) ? false : true;               
                OnLoadData();
              }
            catch (Exception ex)
            {  
                OnLoadData();
                ExceptionManager.Publish(ex);
            }

        }

        /// <summary>
        /// Special characters restricted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbEmpCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar));
        }
    }
}
