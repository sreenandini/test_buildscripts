using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.CoreLib.Win32;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseDataAccess;
using BMC.CoreLib.Concurrent;
using System.Threading;
using BMC.Common;
using BMC.Common.ExceptionManagement;
namespace BMC.EnterpriseClient.Views
{
    public partial class frmAssociateUsertoEmployeeCard : Form
    {
        #region Declarations
        private EmployeeCardBiz objEmpcardbiz = null;
        private int _UserID = 0;
        private int _AsignCnt = 0;
        private bool IsSingleCard = true;
        private bool bRevokeAll = false;
        private bool bAssigned = false;
        private string MaxNoOfCardsForEmployee = "";
        private List<EmployeeCardEntity> eOtherCardFilter = null;
        private List<EmployeeCardEntity> empCardDetails = null;
        private List<EmployeeCardEntity> eCardDetails = null;
        private List<EmployeeCardEntity> eOtherCardDetails = null;
        private List<EmployeeCardEntity> eAsginCardDetails = null;
        private IExecutorService _exec = ExecutorServiceFactory.CreateExecutorService();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        #endregion

        public frmAssociateUsertoEmployeeCard()
        {
            InitializeComponent();
            objEmpcardbiz = EmployeeCardBiz.CreateInstance();
            SetPropertyTag();//Externalization changes
        }
        //Externalization changes
        private void SetPropertyTag()
        {
            try
            {
                this.btnClose.Tag = "Key_CloseCaption";
                this.btnRevoke.Tag = "Key_Revoke";
                this.btnSave.Tag = "Key_Assign";
                this.lblAssignedCards.Tag = "Key_AssignedCards";
                this.grpUsertoEmpCard.Tag = "Key_EmployeeCardTracking";
                this.lblOtherCards.Tag = "Key_OtherCards";
                this.btnRevokeAll.Tag = "Key_RevokeAll";
                this.txtSrcOtherCards.Tag = "Key_SearchOtherCards";
                this.Tag = "Key_AssociateUsertoEmployeeCard";
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        /// <summary>
        /// Overloaded constructor
        /// </summary>
        /// <param name="UserID"></param>
        public frmAssociateUsertoEmployeeCard(int UserID)
        {
            InitializeComponent();
            objEmpcardbiz = EmployeeCardBiz.CreateInstance();
            this._UserID = UserID;
            objDatawatcher = new Helpers.Datawatcher(this);
            SetPropertyTag();
        }

        /// <summary>
        /// exit the current form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Save teh Employee card details for the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string EmpCards = string.Empty;
            int cntCards = _AsignCnt;
            string sMsg = string.Empty;

            LogManager.WriteLog("Save Employee Details for User", LogManager.enumLogLevel.Info);

            try
            {
                {
                    //Get teh list of selected employee cards
                    for (int i = 0; i < chklstEmpcards.Items.Count; i++)
                    {
                        if (chklstEmpcards.GetItemChecked(i)
                            && !((chklstEmpcards.Items[i] as EmployeeCardEntity).Mapped.ToUpper().Equals("ASSIGNED")))
                        {
                            cntCards++;
                            if ((cntCards > Convert.ToInt32(MaxNoOfCardsForEmployee)) || (cntCards > 1 && IsSingleCard))
                            {
                                sMsg = IsSingleCard ? this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_ERROR") : string.Format(this.GetResourceTextByKey(1,"MSG_ASSOCIATEEMP_MAX"), MaxNoOfCardsForEmployee);
                                this.ShowInfoMessageBox(sMsg, this.Text); return;
                            }
                            EmpCards += (chklstEmpcards.Items[i] as EmployeeCardEntity).EmployeeCardNumber + ",";
                        }
                    }
                }

                if (string.IsNullOrEmpty(EmpCards))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_SELECTEMP"), this.Text);
                    return;
                }

                int? ReturnValue = objEmpcardbiz.UpdateUseronEmpCard(this._UserID, IsSingleCard ? true : false, EmpCards);

                if (ReturnValue == 1)
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_SUCCESS"), this.Text);
                else if (ReturnValue == 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_FAILED"), this.Text);
                    return;
                }

                bAssigned = true;   //Based on this flag no card alert msg will be showed

                //Calling Audit Method
                AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.EmployeeCard,
                    Audit_Slot = "",
                    Audit_Screen_Name = "Associate User to Employee",
                    Audit_Field = this._UserID.ToString(),
                    Audit_Old_Vl = EmpCards,
                    Audit_New_Vl = EmpCards,
                    Audit_Desc = "Employee cards " + EmpCards + " have been added for the user",
                    AuditOperationType = OperationType.ADD,
                    Audit_User_ID = AppEntryPoint.Current.UserId,
                    Audit_User_Name = AppEntryPoint.Current.UserName
                }, false);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_FAILASSC") + ex.Message, this.Text);
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
            LoadEmployeeDetails(false);
        }

        /// <summary>
        /// Revoke the Employee card associated with the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevoke_Click(object sender, EventArgs e)
        {
            string EmpCards = string.Empty;

            try
            {
                {
                    //Get the list of selected employee cards to be revoked
                    for (int i = 0; i < chklstAsignEmpCards.Items.Count; i++)
                    {
                        EmpCards += (chklstAsignEmpCards.GetItemChecked(i) || bRevokeAll) ?
                            (chklstAsignEmpCards.Items[i] as EmployeeCardEntity).EmployeeCardNumber + "," :
                            string.Empty;
                    }
                }

                if (string.IsNullOrEmpty(EmpCards))
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_REVOKE"), this.Text);
                    return;
                }
                else
                {
                    //Confirm whether to revoke
                    DialogResult result = this.ShowQuestionMessageBox(this.GetResourceTextByKey(1,"MSG_WANT_TO_REVOKE") + 
                        (bRevokeAll ? this.GetResourceTextByKey(1,"MSG_ALLTHE") : this.GetResourceTextByKey(1,"MSG_SELECTED")) + this.GetResourceTextByKey(1,"MSG_EMPLOYEE_CARD_ASSIGNED"),this.Text);

                    if (result.ToString() == "Yes")
                    {
                        //objEmpcardbiz.RevokeEmployeeCard(this._UserID, EmpCards);
                        this.ShowInfoMessageBox((bRevokeAll ? this.GetResourceTextByKey(1, "MSG_ALLTHE") : this.GetResourceTextByKey(1, "MSG_SELECTED")) + 
                            this.GetResourceTextByKey(1,"MSG_EMPLOYEE_CARD_REVOKED"),this.Text);


                        //Calling Audit Method
                        AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());

                        business.InsertAuditData(new Audit.Transport.Audit_History
                        {
                            EnterpriseModuleName = ModuleNameEnterprise.EmployeeCard,
                            Audit_Slot = "",
                            Audit_Screen_Name = "Employee Card Number - Revoke Employee",
                            Audit_Field = this._UserID.ToString(),
                            Audit_Old_Vl = EmpCards,
                            Audit_New_Vl = EmpCards,
                            Audit_Desc = "Employee card(s) " + EmpCards + " have been revoked from the user",
                            AuditOperationType = OperationType.MODIFY,
                            Audit_User_ID = AppEntryPoint.Current.UserId,
                            Audit_User_Name = AppEntryPoint.Current.UserName
                        }, false);
                    }
                }
                LoadEmployeeDetails(false);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_FAILREVOKE") + ex.Message,this.Text);
                LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        /// <summary>
        /// Set Initial Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssociateUsertoEmployeeCard_Load(object sender, EventArgs e)
        {
            chklstEmpcards.CheckOnClick = true;
            chklstAsignEmpCards.CheckOnClick = true;

            string SingleCardSetting = string.Empty;
            try
            {
                objEmpcardbiz.GetSetting(0, "IsSingleCardEmployee", "false", ref SingleCardSetting);
                IsSingleCard = Convert.ToBoolean(SingleCardSetting);
            }
            catch
            {
                IsSingleCard = false;
            }

            try
            {
                objEmpcardbiz.GetSetting(0, "MaxNoOfCardsForEmployee", "5", ref MaxNoOfCardsForEmployee);
            }
            catch
            {
                MaxNoOfCardsForEmployee = "5";
            }

            grpUsertoEmpCard.Text = IsSingleCard ? "Single Employee card" : "Multiple Employee cards";

            LoadEmployeeDetails(false);

            this.ResolveResources();//Externalization changes
        }

        /// <summary>
        /// Load all the employee card numbers available.
        /// </summary>
        private void LoadEmployeeDetails(bool bFilter)
        {
            BMC.CoreLib.Win32.Win32Extensions.ShowAsyncDialog(this, "Employee Card  Details...", _exec,
            (o) =>
            {
                try
                {
                    //EmployeeCardEntity entity;
                    LogManager.WriteLog("LoadEmployeeDetails", LogManager.enumLogLevel.Info);

                    o.CrossThreadInvoke(() =>
                        {
                            chklstEmpcards.BeginUpdate();
                            chklstAsignEmpCards.BeginUpdate();
                            chklstAsignEmpCards.ClearSelected();
                            chklstEmpcards.ClearSelected();

                            if (!bFilter)
                            {
                                chklstAsignEmpCards.DisplayMember = null;
                                chklstAsignEmpCards.BeginUpdate();
                                empCardDetails = objEmpcardbiz.GetEmployeeCardDetails(null);
                                eCardDetails = empCardDetails.GroupBy(emp => emp.EmpID).Select(emp => emp.First()).ToList();
                                eOtherCardDetails = eCardDetails.Where(emp => emp.UserID != this._UserID && emp.UserID < 1).ToList();
                                eAsginCardDetails = eCardDetails.Where(emp => emp.UserID == this._UserID).ToList();

                                chklstAsignEmpCards.DataSource = eAsginCardDetails;
                                chklstAsignEmpCards.DisplayMember = "EmployeeCardNumber";
                                chklstAsignEmpCards.EndUpdate();

                                _AsignCnt = chklstAsignEmpCards.Items.Count;
                            }
                            else
                            {
                                eOtherCardFilter = eOtherCardDetails.Where(emp => emp.EmployeeCardNumber.Contains(txtSrcOtherCards.Text) || txtSrcOtherCards.Text.Trim() == "Search Other Cards").ToList();
                            }

                            chklstEmpcards.DisplayMember = null;
                            chklstEmpcards.DataSource = bFilter ? eOtherCardFilter : eOtherCardDetails;
                            chklstEmpcards.DisplayMember = "EmployeeCardNumber";

                            chklstEmpcards.EndUpdate();
                            chklstAsignEmpCards.EndUpdate();
                            chklstEmpcards.Enabled = true;
                            btnSave.Enabled = true;

                            if (_AsignCnt == 0)
                            {
                                btnRevoke.Enabled = false;
                                btnRevokeAll.Enabled = false;
                            }
                            else
                            {
                                if (IsSingleCard || (_AsignCnt >= Convert.ToInt32(MaxNoOfCardsForEmployee)))
                                {
                                    chklstEmpcards.Enabled = false;
                                    btnSave.Enabled = false;
                                }

                                btnRevokeAll.Enabled = true;
                                btnRevoke.Enabled = true;
                            }

                            if (eOtherCardDetails.Count < 1 && !bFilter)
                            {
                                //Show alert only during load time
                                if (!bAssigned)
                                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_ASSOCIATEEMP_NOCARD"),this.Text);

                                chklstEmpcards.Enabled = false;
                                btnSave.Enabled = false;
                            }
                        });
                }
                catch (Exception ex)
                {
                    this.ShowErrorMessageBox(ex.Message,this.Text);
                    LogManager.WriteLog(ex.Message, LogManager.enumLogLevel.Error);
                }
            });
        }

        private void chklstEmpcards_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Indeterminate)
                e.NewValue = CheckState.Indeterminate;
        }

        private void btnRevokeAll_Click(object sender, EventArgs e)
        {
            try
            {
                bRevokeAll = true;
                btnRevoke_Click(sender, e);
            }
            finally
            {
                bRevokeAll = false;
            }
        }

        private void txtSrcOtherCards_Leave(object sender, EventArgs e)
        {
            if (txtSrcOtherCards.Text.Length == 0)
            {
                txtSrcOtherCards.Text = "Search Other Cards";
                txtSrcOtherCards.ForeColor = Color.DimGray;
            }
        }

        private void txtSrcOtherCards_Enter(object sender, EventArgs e)
        {
            if (txtSrcOtherCards.Text.Length == 0 || txtSrcOtherCards.Text == "Search Other Cards")
            {
                txtSrcOtherCards.Text = "";
            }
            txtSrcOtherCards.ForeColor = Color.Black;
        }

        private void txtSrcOtherCards_TextChanged(object sender, EventArgs e)
        {
            if (txtSrcOtherCards.Text != "Search Other Cards" && eOtherCardDetails.Count > 0)
            {
                LoadEmployeeDetails(true);
            }
        }

    }
}
