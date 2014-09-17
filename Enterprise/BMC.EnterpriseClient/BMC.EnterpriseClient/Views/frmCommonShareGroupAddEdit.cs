using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseClient.Helpers;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using System.Windows.Forms.VisualStyles;
//using System.Transactions;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.Common.ExceptionManagement;
using System.Transactions;
using BMC.Common;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmCommonShareGroupAddEdit : GenericFormBase
    {
        public CommonProfitShareType _functionality = CommonProfitShareType.ProfitShare;
        public CommonProfitShareGroupEntity _entity = null;
        private CommonProfitShareBusiness _business = new CommonProfitShareBusiness();
        private List<CommonProfitShareEntity> _shares = null;
        private bool _isNewlyAdded = false;
        private TransactionHelper _transaction = null;
        public FormEditTypes _editType;
        string share_name;
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;


        public frmCommonShareGroupAddEdit(CommonProfitShareType functionality, FormEditTypes editType, CommonProfitShareGroupEntity entity)
        {
            InitializeComponent();
            share_name = entity.Name;
            _functionality = functionality;
            _entity = entity;
            _editType = editType;
            objDatawatcher = new Helpers.Datawatcher(this, null, true);
            SetPropertyTag();
            this.Text = ProfitShareFunctional.GetCaption(_editType, FormGroupTypes.Group, _functionality);
            this.grpContainer.Text = ProfitShareFunctional.GetCaption(FormEditTypes.List, FormGroupTypes.Group, _functionality);
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnAdd.Tag = "Key_AddCaption";
                this.btnCancel.Tag = "Key_CancelCaption";
                this.lblDescription.Tag = "Key_DescriptionCaptionColon";
                this.btnSave.Tag = "Key_SaveCaption";
                this.lblName.Tag = "Key_NameMandatory";
                this.chdrDelete.Tag = "Key_Delete";
                this.chdrEdit.Tag = "Key_Edit_WOShortCut";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void Audit_Viewer(CommonProfitShareType functionality, FormEditTypes editType, CommonProfitShareGroupEntity entity, string ShareName)
        {

            if (functionality == CommonProfitShareType.ProfitShare)
            {
                if (_editType == FormEditTypes.Edit)
                {
                    try
                    {
                        AuditBusiness business = new AuditBusiness(DatabaseHelper.GetConnectionString());
                        {

                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.ProfitShare,
                                Audit_Screen_Name = "ProfitShare | ProfitShareAddEdit",
                                Audit_Desc = "ProfitShare Updated-" + "ProfitShare ID: " + _entity.Id + "; ProfitShare: " + ShareName + "",
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Old_Vl = share_name,
                                Audit_New_Vl = ShareName,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for ProfitShare Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
                else
                {
                    if (_editType == FormEditTypes.Add)
                    {
                        try
                        {

                            AuditBusiness business = new AuditBusiness(DatabaseHelper.GetConnectionString());
                            {
                                business.InsertAuditData(new Audit.Transport.Audit_History
                                {
                                    EnterpriseModuleName = ModuleNameEnterprise.ProfitShare,
                                    Audit_Screen_Name = "ProfitShare | profitShareAddEdit",
                                    Audit_Desc = "ProfitShare Added-" + "ProfitShare:" + txtName.Text.Trim() + "",
                                    AuditOperationType = OperationType.ADD,
                                    Audit_User_ID = AppEntryPoint.Current.UserId,
                                    Audit_User_Name = AppEntryPoint.Current.UserName
                                }, false);
                            }
                        }

                        catch (Exception ex)
                        {
                            LogManager.WriteLog("Error While Adding Audit Log for ProfitShare Insert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                        }
                    }

                }
            }


            else
            {
                if (editType == FormEditTypes.Edit)
                {
                    try
                    {
                        AuditBusiness business = new AuditBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.ExpenseShare,
                                Audit_Screen_Name = "ExpenseShare | ExpenseShareAddEdit",
                                Audit_Desc = "ExpenseShare Updated-" + "ExpenseShare ID: " + _entity.Id + "; ExpenseShare: " + ShareName + "",
                                AuditOperationType = OperationType.MODIFY,
                                Audit_Old_Vl = share_name,
                                Audit_New_Vl = ShareName,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }

                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for ExpenseShare Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }

                else
                {
                    try
                    {
                        AuditBusiness business = new AuditBusiness(DatabaseHelper.GetConnectionString());
                        {
                            business.InsertAuditData(new Audit.Transport.Audit_History
                            {
                                EnterpriseModuleName = ModuleNameEnterprise.ExpenseShare,
                                Audit_Screen_Name = "ExpenseShare | ExpenseShareAddEdit",
                                Audit_Desc = "ExpenseShare Added-" + "ExpenseShare ID: " + _entity.Id + "; ExpenseShare: " + ShareName + "",
                                AuditOperationType = OperationType.ADD,
                                Audit_User_ID = AppEntryPoint.Current.UserId,
                                Audit_User_Name = AppEntryPoint.Current.UserName
                            }, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("Error While Adding Audit Log for ExpenseShare Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                    }
                }
            }

        }


        private void frmCommonShareGroupAddEdit_Load(object sender, EventArgs e)
        {
            _transaction = new TransactionHelper(TransactionScopeOption.RequiresNew, System.Transactions.IsolationLevel.Serializable);
            txtName.Text = _entity.Name;
            txtDescription.Text = _entity.Description;

            this.ReloadGrid();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReloadGrid()
        {
            try
            {
                _shares = _business.GetShares(_functionality, _entity.Id);
                grdItems.DataSource = _shares;

                float sum = 0;
                if (_shares != null)
                {
                    sum = ((float)_shares.Sum(s => s.Percentage));
                }
                btnAdd.Enabled = (sum == 0 || sum < 100);

                SetTagofgrdItemsColumns();
                this.ResolveResources();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagofgrdItemsColumns()
        {
            grdItems.Columns["ShareHolderName"].Tag = "Key_ShareHolders";
            grdItems.Columns["Percentage"].Tag = "Key_Percentage";
            grdItems.Columns["Description"].Tag = "Key_Description";

        }

        private void ShowGroupItem(FormEditTypes editType, CommonProfitShareEntity entity)
        {
            List<CommonProfitShareEntity> source = grdItems.DataSource as List<CommonProfitShareEntity>;
            float sum = 0;
            if (source != null)
            {
                sum = (float)source.Sum(s => s.Percentage);
            }
            double remainingPercentage = (100.00 - sum + entity.Percentage);
            if (entity.Percentage == 0) entity.Percentage = remainingPercentage;

            entity.Parent = _entity;
            new frmCommonShareAddEdit(_functionality, editType, _entity, entity, remainingPercentage)
                .ShowDialogExResultAndDestroy<frmCommonShareAddEdit>(
                this, (f) =>
                {
                },
                (f) =>
                {
                    this.ReloadGrid();
                });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.SaveShareGroup(false))
            {
                this.ShowGroupItem(FormEditTypes.Add, new CommonProfitShareEntity());
            }
        }

        private void grdItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                List<CommonProfitShareEntity> dataSource = grdItems.DataSource as List<CommonProfitShareEntity>;
                if (e.RowIndex == -1)
                {
                    return;
                }
                CommonProfitShareEntity entity = dataSource[e.RowIndex];

                if (e.ColumnIndex == chdrEdit.Index)
                {
                    this.ShowGroupItem(FormEditTypes.Edit, entity);
                }
                else if (e.ColumnIndex == chdrDelete.Index)
                {
                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_DEL")) == DialogResult.Yes)
                    {
                        if (_business.DeleteShare(_functionality, _entity.Id, entity.Id))
                        {

                            this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_DEL"), entity.ShareHolderName));
                            this.ReloadGrid();
                        }
                        else
                        {
                            this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_ERRORDEL"), entity.ShareHolderName));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowErrorMessageBox(ex.Message);
            }
        }

        private bool SaveShareGroup(bool force)
        {
            try
            {
                string text = txtName.Text.Trim();
                if (text.Length <= 0)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_VALIDGROUP"));
                    txtName.Focus();
                    return false;
                }

                if (_business.IsShareGroupAlreadyExists(_functionality, text, _entity.Id))
                {
                    this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_NAMEEXISTS"), text));
                    txtName.Focus();
                    return false;
                }

                _entity.Name = text;
                _entity.Description = txtDescription.Text.Trim();
                txtDescription.WordWrap = true;
                txtDescription.Multiline = true;
                if (_entity.Id <= 0 || force)
                {
                    if (force)
                    {
                        if (_shares == null || _shares.Count == 0)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_RETAI_MAND"));
                            grdItems.Focus();
                            return false;
                        }
                        List<Int32> _list = _shares.Select(X => X.ShareHolder.Id).ToList<Int32>(); ;
                        if (!_shares.Exists((c) => c.ShareHolder.Id == 3))
                        //if (! _shares.Exists((c) => c.ShareHolderName.IgnoreCaseCompare("Retailer")))
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_RETAI_MAND"));
                            grdItems.Focus();
                            btnAdd.Enabled = true;
                            return false;
                        }

                        double sum = _shares.Sum(s => s.Percentage);
                        if (sum != 100)
                        {
                            this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_VALIDPERC"), sum));
                            grdItems.Focus();
                            return false;
                        }
                        _entity.Percentage = sum;
                    }

                    bool isNewItem = (_entity.Id <= 0);
                    if (isNewItem)
                    {
                    }
                    if (_business.ModifyShareGroup(_functionality, _entity))
                    {
                        _isNewlyAdded = (isNewItem && (_entity.Id > 0));
                        if (force)
                        {
                            this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_SUCCESS"));
                            objDatawatcher.DataModify = false;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    else
                    {
                        this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_FAILURE"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ShareName = txtName.Text;
                if (!this.SaveShareGroup(true))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    return;
                }

                Audit_Viewer(_functionality, _editType, _entity, ShareName);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmCommonShareGroupAddEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isExited = false;

            try
            {
                objDatawatcher.OwnerForm_FormClosing(sender, e);
                if (e.Cancel)
                {
                    isExited = true;
                    return;
                }
                _transaction.SaveAndDispose(this.DialogResult != DialogResult.OK);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                if (!isExited && _isNewlyAdded && this.DialogResult != DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
