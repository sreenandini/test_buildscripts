using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseDataAccess;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Business;
using BMC.Common;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmCommonShareAddEdit : Form
    {
        private CommonProfitShareType _functionality = CommonProfitShareType.ProfitShare;
        private CommonProfitShareGroupEntity _parentEntity = null;
        private CommonProfitShareEntity _entity = null;
        private CommonProfitShareBusiness _business = new CommonProfitShareBusiness();
        BMC.EnterpriseClient.Helpers.Datawatcher objDatawatcher = null;

        private double _remainingPercentage = 0;
        public bool MyProperty { get; set; }

        public frmCommonShareAddEdit(CommonProfitShareType functionality, FormEditTypes editType,
            CommonProfitShareGroupEntity parentEntity, CommonProfitShareEntity entity,
            double remainingPercentage)
        {
            this.InitializeComponent();
            _functionality = functionality;
            _parentEntity = parentEntity;
            _entity = entity;
            _remainingPercentage = Math.Round(remainingPercentage, 2);
            updPercentage.Maximum = Math.Round((decimal)_remainingPercentage, 2);
            this.Text = GetCaption(editType) + ProfitShareFunctional.GetCaptionForHeader(editType, FormGroupTypes.Item, functionality);
            //this.Text = ProfitShareFunctional.GetCaption(editType, FormGroupTypes.Item, functionality);
            this.cboShareHolders.Enabled = editType == FormEditTypes.Add ? true : false;
            objDatawatcher = new Helpers.Datawatcher(this);
            SetPropertyTag();
        }

        private string GetCaption(FormEditTypes editType)
        {
            string sResult = "";
            if (editType == FormEditTypes.Add)
            {
                sResult = this.GetResourceTextByKey("Key_Add");
            }
            else if (editType == FormEditTypes.Delete)
            {
                sResult = this.GetResourceTextByKey("Key_Delete");
            }
            else if (editType == FormEditTypes.Edit)
            {
                sResult = this.GetResourceTextByKey("Key_Edit");
            }
            else if (editType == FormEditTypes.List)
            {
                sResult = this.GetResourceTextByKey("Key_List");
            }

            return sResult + " " ;
        }

        private void SetPropertyTag()
        {
            try
            {
                this.btnCancel.Tag = "Key_CancelCaption";
                this.lblDescription.Tag = "Key_DescriptionCaptionColon";
                this.btnSave.Tag = "Key_OKCaption";
                this.lblSharePercentage.Tag = "Key_PercentageMandatoryColon";
                this.lblShareHolderName.Tag = "Key_ShareHolderMandatory";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (cboShareHolders.SelectedIndex == -1)
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_PS_SELECT_SH"), this.Text);
                    cboShareHolders.Focus();
                    return;
                }
                else if (updPercentage.Value < 0 ||
                    updPercentage.Value > (decimal)_remainingPercentage)
                {
                    this.ShowInfoMessageBox(string.Format(this.GetResourceTextByKey(1, "MSG_PS_VALIDPERC"), _remainingPercentage), this.Text);
                    updPercentage.Focus();
                    return;
                }
                _entity.ShareHolder = ((ShareHolderEntity)cboShareHolders.SelectedItem);
                _entity.Percentage = Math.Round((Double)updPercentage.Value, 2);
                _entity.Description = txtDescription.Text.Trim();
                if (_business.ModifyShare(_functionality, _entity))
                {

                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_SUCCESS"), this.Text);
                    objDatawatcher.DataModify = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.ShowInfoMessageBox(this.GetResourceTextByKey(1, "MSG_SHAREGROUP_SH_FAILURE"), this.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void frmCommonShareAddEdit_Load(object sender, EventArgs e)
        {
            this.ReloadData();
            this.ResolveResources();
        }

        private void ReloadData()
        {
            try
            {
                updPercentage.Value = Math.Round((decimal)_entity.Percentage, 2);
                txtDescription.Text = _entity.Description;
                //_remainingPercentage = (int)(100.00 - _business.GetProfitSharePercentage(_functionality, _parentEntity.Id, _entity.Id));

                BindingList<ShareHolderEntity> list = new BindingList<ShareHolderEntity>(_business.GetShareHolders(_functionality, _parentEntity.Id, _entity.Id));
                cboShareHolders.DisplayMember = "Name";
                cboShareHolders.ValueMember = "Id";
                cboShareHolders.DataSource = list;
                int i = -1;
                if (_entity.ShareHolder != null)
                {
                    ShareHolderEntity entity = (from l in list
                                                let j = ++i
                                                where l.Id == _entity.ShareHolder.Id
                                                select l).FirstOrDefault();
                }

                if (i > -1 &&
                    i < cboShareHolders.Items.Count)
                {
                    cboShareHolders.SelectedIndex = i;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

    }
}