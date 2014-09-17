using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseClient.Views
{
    public partial class frmProfitShareAddEdit : Form
    {
        private ShareHolderPercentageDetails _objSHPer = new ShareHolderPercentageDetails();
        private FormEditTypes _EditType;
        private double _MaxAllowedPercentage;
        private int _ProfitShareGroupId = -1;
        public int[] _SHLst;
        ShareHolderPercentageDetails _SHPDetails = null;



        public FormEditTypes EditType
        {
            get
            {
                return _EditType;
            }

            set
            {
                _EditType = value;
                this.Text = (_EditType == FormEditTypes.Add ? "Add" : "Edit") + " Profit Share";
            }
        }


        public frmProfitShareAddEdit(int? ShareHolderId, int ProfitShareGroupId, FormEditTypes formEditType, Double MaxAllowedPercentage, ShareHolderPercentageDetails SHPDetails, int[] SHLst)
        {


            InitializeComponent();
            try
            {
                LogManager.WriteLog("Loading Add Edit PSG", LogManager.enumLogLevel.Warning);

                _SHLst = SHLst;
                ShareHolderDetailsResult sh_res = (ShareHolderDetailsResult)cmbShareHolderName.SelectedItem;

                LoadShareHolderDropDown(ShareHolderId);
                _EditType = formEditType;
                _MaxAllowedPercentage = MaxAllowedPercentage;
                _objSHPer = SHPDetails;

                _ProfitShareGroupId = ProfitShareGroupId;
                _SHPDetails = SHPDetails;
                if (EditType.Equals(FormEditTypes.Edit))
                {
                    //cmbShareHolderName.SelectedItem=
                    cmbShareHolderName.Enabled = false;
                }

                txtSharePercentage.Value = Convert.ToDecimal(_MaxAllowedPercentage);
                txtSharePercentage.Maximum = Convert.ToDecimal(_MaxAllowedPercentage);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("frmProfitShareAddEdit method : " + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        public void LoadShareHolderDropDown(int? _ShareHolderId)
        {
            try
            {
                List<ShareHolderDetailsResult> lstShareHolderList = ProfitShareBusiness.CreateInstance().GetShareHolderDetails(_ShareHolderId);
                if (FormEditTypes.Add.Equals("Add"))
                {
                    if (_SHLst != null)
                    {
                        int count = _SHLst.Count();

                        foreach (int ShareHolderID in _SHLst)
                        {
                            lstShareHolderList.RemoveAll(j => j.ShareHolderId.Equals(ShareHolderID));
                        }
                    }
                }
                cmbShareHolderName.DataSource = lstShareHolderList;
                cmbShareHolderName.DisplayMember = "ShareHolderName";
                cmbShareHolderName.ValueMember = "ShareHolderId";
                if (_ShareHolderId != null)
                {
                    cmbShareHolderName.SelectedItem = lstShareHolderList.Find(obj => obj.ShareHolderId == _ShareHolderId);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error: LoadShareHolder :" + ex.Message, LogManager.enumLogLevel.Error);
            }

        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    _ProfitShareGroupId
        //    //Validation - Name, Percentage
        //    _objSHPer.ShareHolderId = cmbShareHolderName.SelectedIndex; //?????????????????????????
        //    _objSHPer.SharePercentage = Convert.ToSingle(txtSharePercentage.Value);
        //    _objSHPer.Description = txtDescription.Text;
        //    //Save the Object To Database.


        //}



        private void AddEditProfitShareHolder_Load(object sender, EventArgs e)
        {
            //
        }

        private void frmAddEditProfitShareHolder_Load(object sender, EventArgs e)
        {
            //



        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (FormEditTypes.Add.Equals("Add"))
            {
                if (_SHPDetails != null)
                {
                    // _SHPDetails.
                    ShareHolderDetailsResult sh_res = (ShareHolderDetailsResult)cmbShareHolderName.SelectedItem;
                    _SHPDetails.ShareHolderId = sh_res.ShareHolderId;
                    _SHPDetails.ShareHolderName = sh_res.ShareHolderName;
                    _SHPDetails.ProfitSharePercentage = Convert.ToDouble(txtSharePercentage.Value.ToString());
                    _SHPDetails.Description = txtDescription.Text;
                }
            }
            else if (FormEditTypes.Add.Equals("Edit"))
            {
                //Edit row
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}