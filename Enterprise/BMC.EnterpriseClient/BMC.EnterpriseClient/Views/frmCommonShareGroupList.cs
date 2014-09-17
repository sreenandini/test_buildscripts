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
using BMC.Common.ExceptionManagement;
using BMC.CoreLib;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmCommonShareGroupList : Form
    {
        private CommonProfitShareType _functionality = CommonProfitShareType.ProfitShare;
        private CommonProfitShareBusiness _business = new CommonProfitShareBusiness();
        //private FormEditTypes editType;
        //private CommonProfitShareGroupEntity entity;
        public frmCommonShareGroupList(CommonProfitShareType functionality)
        {
            this.InitializeComponent();
            SetTagProperty();
            _functionality = functionality;
            this.Text = ProfitShareFunctional.GetCaption(FormEditTypes.List, FormGroupTypes.Group, functionality);

        }

        /// <summary>
        /// Assigns the Resource Key names to the controls--Created by kishore sivagnanam
        /// </summary>
        public void SetTagProperty()
        {
            this.btnAdd.Tag = "Key_AddCaption";
            this.btnCancel.Tag = "Key_CloseCaption";
            this.chdrDelete.Tag = "Key_Delete";
            this.chdrEdit.Tag = "Key_Edit_WOShortCut";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.ShowGroupItem(FormEditTypes.Add, new CommonProfitShareGroupEntity());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReloadGrid()
        {
            try
            {
                grdItems.DataSource = _business.GetShareGroups(_functionality);
                SetTagofgrdItemsColumns();
                this.ResolveResources();
                grdItems.Columns[3].Width = 100;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void SetTagofgrdItemsColumns()
        {
            grdItems.Columns["Name"].Tag = "Key_Name";
            grdItems.Columns["Percentage"].Tag = "Key_Percentage";
            grdItems.Columns["Description"].Tag = "Key_Description";

        }

        private void ShowGroupItem(FormEditTypes editType, CommonProfitShareGroupEntity entity)
        {

            if (new frmCommonShareGroupAddEdit(_functionality, editType, entity)

                .ShowDialogExResultAndDestroy<frmCommonShareGroupAddEdit>(
                this, (f) =>
                {
                },
                (f) =>
                {
                }) == DialogResult.OK)
            {
                this.ReloadGrid();
            }
        }

        private void frmCommonShareGroupList_Load(object sender, EventArgs e)
        {
            this.ReloadGrid();
        }

        private void grdItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                List<CommonProfitShareGroupEntity> dataSource = grdItems.DataSource as List<CommonProfitShareGroupEntity>;
                if (e.RowIndex == -1)
                {
                    return;
                }
                CommonProfitShareGroupEntity entity = dataSource[e.RowIndex];

                if (e.ColumnIndex == chdrEdit.Index)
                {
                    this.ShowGroupItem(FormEditTypes.Edit, dataSource[e.RowIndex]);
                }
                else if (e.ColumnIndex == chdrDelete.Index)
                {

                    if (this.ShowQuestionMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_DEL")) == DialogResult.Yes)
                    {
                        if (_business.DeleteShareGroup(_functionality, entity.Id))
                        {
                            this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SHAREHOLDER_DEL"), entity.Name));
                            this.ReloadGrid();
                            if (_functionality == CommonProfitShareType.ProfitShare)
                            {
                                try
                                {
                                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                                    {

                                        business.InsertAuditData(new Audit.Transport.Audit_History
                                        {
                                            EnterpriseModuleName = ModuleNameEnterprise.ProfitShare,
                                            Audit_Screen_Name = "ProfitShare|ProfitShareAddEdit",
                                            Audit_Desc = "ProfitShare Deleted " + "ProfitShare ID: " + entity.Id + "; ProfitShare: " + entity.Name,
                                            AuditOperationType = OperationType.DELETE,
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
                                try
                                {
                                    AuditViewerBusiness business = new AuditViewerBusiness(DatabaseHelper.GetConnectionString());
                                    {

                                        business.InsertAuditData(new Audit.Transport.Audit_History
                                        {
                                            EnterpriseModuleName = ModuleNameEnterprise.ExpenseShare,
                                            Audit_Screen_Name = "ExpenseShare|ExpenseShareAddEdit",
                                            Audit_Desc = "ExpenseShare Deleted " + "ExpenseShare ID: " + entity.Id + "; ExpenseShare: " + entity.Name,
                                            AuditOperationType = OperationType.DELETE,
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
                }
                else
                {
                    //this.ShowInfoMessageBox(string.Format("Unable to delete [{0}].", entity.Name), this.Text);
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                this.ShowErrorMessageBox(ex.Message);
            }
        }

    }
}

