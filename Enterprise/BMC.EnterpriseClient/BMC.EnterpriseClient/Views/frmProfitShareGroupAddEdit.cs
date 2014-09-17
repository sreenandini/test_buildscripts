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
using BMC.EnterpriseBusiness;

//using System.Transactions;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;


namespace BMC.EnterpriseClient.Views
{
    public partial class frmProfitShareGroupAddEdit : GenericFormBase
    {
        //frmProfitShareAddEdit ProfitShareAdd;
        //private TransactionScope _scope = null;
        double TotalPercentage = 0;
        private int _PFShareGroupID;
        public ProfitShareGroupEntity _PSGroup;
       // private ProfitShareBusiness _PSBusiness = new ProfitShareBusiness();
        private ShareHolderPercentageDetails _SHPercentage = new ShareHolderPercentageDetails();
        public ShareHolderPercentageDetails sh_Percent = new ShareHolderPercentageDetails();

        // public GetProfitShareGroupDetailsResult pf_ent = new GetProfitShareGroupDetailsResult();

        public frmProfitShareGroupAddEdit(int PFShareGroupID)
        {
            InitializeComponent();
            _PSGroup = new ProfitShareGroupEntity();
            _PFShareGroupID = PFShareGroupID;
            Edit.Width = 50;
            Delete.Width = 50;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool GridToText()
        {
            if (_PSGroup == null)
            {
                _PSGroup = new ProfitShareGroupEntity();
            }
            txtProfitShareGroupName.Text = _PSGroup.ProfitShareGroupName;
            txtProfitShareGroupDescription.Text = _PSGroup.ProfitShareGroupDescription;
            return true;
        }

        private bool AddEditGroupToDB()
        {
            if (_PSGroup == null)
            {
                _PSGroup = new ProfitShareGroupEntity();
            }

            _PSGroup.ProfitShareGroupName = txtProfitShareGroupName.Text;
            _PSGroup.ProfitShareGroupDescription = txtProfitShareGroupDescription.Text;

            //Save this info to DB. If exists Update else Add New

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditGroupToDB();

                TotalPercentage = GetRemainingPercentage();
                int[] SHLst = GetShList();
                frmProfitShareAddEdit frmPS = new frmProfitShareAddEdit(null, _PSGroup.ProfitShareGroupId, FormEditTypes.Add, TotalPercentage, sh_Percent, SHLst);
                if (frmPS.ShowDialog() == DialogResult.OK)
                {
                    BindingList<GetProfitShareGroupDetailsResult> lst_profit = (BindingList<GetProfitShareGroupDetailsResult>)grdvwProfitShareGroup.DataSource;
                    if (lst_profit != null)
                    {
                        AddtoGrid(sh_Percent, lst_profit);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error while Saving ProfitShareGroup :" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void AddtoGrid(ShareHolderPercentageDetails sh_Percent, BindingList<GetProfitShareGroupDetailsResult> lst_profit)
        {
            try
            {
                GetProfitShareGroupDetailsResult pf_ent = new GetProfitShareGroupDetailsResult();
                // pf_ent.ProfitShareId = 0;
                pf_ent.ShareHolderId = sh_Percent.ShareHolderId;
                pf_ent.ShareHolderName = sh_Percent.ShareHolderName;
                pf_ent.ProfitSharePercentage = sh_Percent.ProfitSharePercentage;
                pf_ent.ProfitShareDescription = sh_Percent.Description;
                lst_profit.Add(pf_ent);
                // EnableGridColumn(lst_profit);
                grdvwProfitShareGroup.DataSource = null;
                grdvwProfitShareGroup.DataSource = lst_profit;
                grdvwProfitShareGroup.Columns[2].Visible = false;
                grdvwProfitShareGroup.Columns[3].Visible = false;
                TotalPercentage = GetRemainingPercentage();

                if (TotalPercentage != 0)
                {
                    btnAdd.Enabled = true;
                    if (DialogResult.OK == MessageBox.Show("Do you want to continue:", "ShareHolderName", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    {
                        AddEditGroupToDB();
                        int[] SHLst = GetShList();
                        frmProfitShareAddEdit frmPS = new frmProfitShareAddEdit(null, _PSGroup.ProfitShareGroupId, FormEditTypes.Add, TotalPercentage, sh_Percent, SHLst);
                        if (frmPS.ShowDialog() == DialogResult.OK)
                        {
                            if (lst_profit != null)
                            {
                                AddtoGrid(sh_Percent, lst_profit);
                            }
                        }
                    }
                }

                else
                {
                    btnAdd.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error: Inserting Data to Grid " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        public int[] GetShList()
        {
            int size = grdvwProfitShareGroup.RowCount;
            int[] ShLst = new int[size];
            try
            {
                for (int i = 0; i < grdvwProfitShareGroup.Rows.Count; ++i)
                {
                    ShLst[i] = Convert.ToInt32(grdvwProfitShareGroup.Rows[i].Cells[3].Value);
                }
                return ShLst;
            }
            catch (Exception ex)
            {

                LogManager.WriteLog("GetShList : " + ex.Message, LogManager.enumLogLevel.Error);
                return null;
            }

        }
        public double GetRemainingPercentage()
        {
            try
            {
                Double UsedPercenatge = 0;
                Double RemainingPercentage = 0;
                for (int i = 0; i < grdvwProfitShareGroup.Rows.Count; ++i)
                {
                    UsedPercenatge += Convert.ToDouble(grdvwProfitShareGroup.Rows[i].Cells[5].Value);
                }
                RemainingPercentage = 100 - UsedPercenatge;
                return RemainingPercentage;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetRemainingPercentage : " + ex.Message, LogManager.enumLogLevel.Error);
                return -1;
            }
        }


        private void LoadProfitShareGrid()
        {
            try
            {

                //Load Share holder Per Details directly from DB
                int? PFShareGroupID = _PFShareGroupID;
                BindingList<GetProfitShareGroupDetailsResult> lst_profit = ProfitShareBusiness.GetProfitShare(PFShareGroupID);
                //if (lst_profit != null && lst_profit.Count > 0)
                //{
                lst_profit.AllowNew = false;
                grdvwProfitShareGroup.DataSource = lst_profit;
                //}
                EnableGridColumn(lst_profit);
                IsShareExceed(lst_profit);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Loading Profit Share Group grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        void EnableGridColumn(BindingList<GetProfitShareGroupDetailsResult> lst_profit)
        {
            if (lst_profit == null || lst_profit.Count == 0)
            {

                ShareHolderName.Visible = false;
                ProfitShareGroupDescription.Visible = false;
                ProfitSharePercentage.Visible = false;
            }
            else
            {
                ShareHolderName.Visible = true;
                ProfitShareGroupDescription.Visible = true;
                ProfitSharePercentage.Visible = true;
            }

        }

        bool IsShareExceed(BindingList<GetProfitShareGroupDetailsResult> lst_profit)
        {
            TotalPercentage = lst_profit.Sum(obj => obj.ProfitSharePercentage);
            return btnAdd.Enabled = TotalPercentage < 100;
        }

        private bool EditGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                result = EditProfitShareGroupGridRow(rowIndex);
                if (result)
                {
                    int[] SHLst = GetShList();
                    BindingList<GetProfitShareGroupDetailsResult> lst_profit = (BindingList<GetProfitShareGroupDetailsResult>)grdvwProfitShareGroup.DataSource;
                    IsShareExceed(lst_profit);
                    double MaxPercentToEdit = 100 - TotalPercentage + _SHPercentage.ProfitSharePercentage;
                    frmProfitShareAddEdit frmPS = new frmProfitShareAddEdit(_SHPercentage.ShareHolderId, _PFShareGroupID, FormEditTypes.Edit, MaxPercentToEdit, sh_Percent, SHLst);
                    if (DialogResult.OK == frmPS.ShowDialog())
                    {

                        GetProfitShareGroupDetailsResult gp_Share = lst_profit.ToList().Find(obj => obj.ShareHolderId == _SHPercentage.ShareHolderId);
                        gp_Share.ProfitSharePercentage = _SHPercentage.ProfitSharePercentage;
                        gp_Share.ProfitShareDescription = _SHPercentage.Description;
                        grdvwProfitShareGroup.DataSource = lst_profit;
                        IsShareExceed(lst_profit);
                    }



                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Editing Grid Row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool EditProfitShareGroupGridRow(int rowIndex)
        {
            try
            {

                //float MaxAllowedPer = GetTotalUsedPercentage();
                _SHPercentage.ShareHolderId = Convert.ToInt32("0" + grdvwProfitShareGroup["ShareHolderId", rowIndex].Value);
                _SHPercentage.ShareHolderName = grdvwProfitShareGroup["ShareHolderName", rowIndex].Value.ToString();
                _SHPercentage.ProfitSharePercentage = Convert.ToInt32(grdvwProfitShareGroup["ProfitSharePercentage", rowIndex].Value.ToString());
                _SHPercentage.Description = grdvwProfitShareGroup["ProfitShareDescription", rowIndex].Value.ToString();

                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error:Editing ProfitShareGroup", ex.Message, LogManager.enumLogLevel.Error);
                return false;

            }
        }


        private bool DeleteGridRow(int rowIndex)
        {

            try
            {
                bool result = false;
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Deleting Grid Row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool DeleteProfitShareGroupGridRow(int rowIndex)
        {
            try
            {

                bool result = false;
                string strProfitShareGroupId = grdvwProfitShareGroup["ProfitShareGroupId", rowIndex].Value.ToString();
                if (MessageBox.Show("Do you want to delete the ProfitShareGroup " + "'" + strProfitShareGroupId + "'" + " ?", "ProfitShareGroup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    ProfitShareGroupBusiness bProfitShareGroup = new ProfitShareGroupBusiness();
                    int ProfitShareGroupId = -1;
                    int.TryParse(grdvwProfitShareGroup["ProfitShareGroupId", rowIndex].Value.ToString(), out ProfitShareGroupId);
                    if (bProfitShareGroup.DeleteProfitShareGroup(ProfitShareGroupId))
                    {
                        MessageBox.Show("ProfitShareHolderGroup  " + strProfitShareGroupId + " deleted successfully", "ProfitShareGroup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            //
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("Error While Adding Audit Log for ProfitSahreGroup Delete: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                        }

                    }

                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in deleting ProfitShareGroup : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool _IsUpdate = false;

        public bool IsUpdate
        {
            get
            {
                return _IsUpdate;
            }
            set
            {
                _IsUpdate = value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // AddEditGroupToDB();

                string ProfitShareGroupName = txtProfitShareGroupName.Text.Trim();
                ProfitShareBusiness ProfitShareGroup = new ProfitShareBusiness();

                // Profit Share group name validation

                if ((String.IsNullOrEmpty(ProfitShareGroupName)) || (ProfitShareGroupName.Length > 50))
                {
                    MessageBox.Show("Please enter a valid Profit Share Group Name  within the length 1 to 50.", "Profit Share Group Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProfitShareGroupName.Focus();
                    return;
                }


                //Profit Share group Name already Exists

                int? iNameExists = 0;
                int IsProfitShareGroupNameExists = ProfitShareGroup.IsNameExists(txtProfitShareGroupName.Text, ref iNameExists);
                if (iNameExists > 0)
                {
                    MessageBox.Show("The Profit Share Group Name  entered already exists.", "Profit Share Group", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProfitShareGroupName.Focus();
                    return;
                }


                //Profit Share group - Share holder percentage


                if (TotalPercentage != 0)
                {
                    btnAdd.Enabled = true;
                    MessageBox.Show("The total share percentage should be equal to 100", "Profit Share Group");
                    if (MessageBox.Show("Do you want to Continue?", "Profit Share Group", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        BindingList<GetProfitShareGroupDetailsResult> lst_profit = (BindingList<GetProfitShareGroupDetailsResult>)grdvwProfitShareGroup.DataSource;
                        AddEditGroupToDB();
                        int[] SHLst = GetShList();
                        frmProfitShareAddEdit frmPS = new frmProfitShareAddEdit(null, _PSGroup.ProfitShareGroupId, FormEditTypes.Add, TotalPercentage, sh_Percent, SHLst);
                        if (frmPS.ShowDialog() == DialogResult.OK)
                        {
                            if (lst_profit != null)
                            {
                                AddtoGrid(sh_Percent, lst_profit);
                            }
                        }
                        //grdvwProfitShareGroup.DataSource = null;
                    }
                    else
                    {
                        //return;
                        //try
                        //{
                        //int InsertPSGroupList=_PSBusiness.
                        //Save To PSG
                        //PSG Name,%,Desc

                    }
                }
                else
                {
                    btnAdd.Enabled = false;
                }
                #region DeadCode After use

                //if (IsUpdate)
                //{
                //    try
                //    {
                //        LogManager.WriteLog("Profit Share Group Edit Starts", LogManager.enumLogLevel.Info);
                //        //
                //        LogManager.WriteLog("Profit Share Group Edit Ends", LogManager.enumLogLevel.Info);
                //    }
                //    catch (Exception ex)
                //    {
                //        LogManager.WriteLog("Error While Adding Audit Log for Profit Share Group Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                //    }
                //}
                //else
                //{
                //    try
                //    {
                //        //
                //    }

                //    catch (Exception ex)
                //    {
                //        LogManager.WriteLog("Error While Adding Audit Log for Profit Share Group Insert: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                //    }
                //}
                //MessageBox.Show("Profit Share Group details saved successfully.", "Profit Share Group", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //LogManager.WriteLog("ProfitShareGroup Added : " + ProfitShareGroupName + " ", LogManager.enumLogLevel.Info);

                //if (!IsUpdate)
                //{
                //    txtProfitShareGroupName.Text = string.Empty;
                //    txtProfitShareGroupDescription.Text = string.Empty;
                //    if (MessageBox.Show("Do you want to add another Profit Share Group details?", "Profit Share Group", MessageBoxButtons.YesNo) == DialogResult.No)
                //    {
                //        this.Close();
                //    }
                //    else
                //    {
                //        txtProfitShareGroupName.Focus();
                //    }
                //}
                //else
                //{
                //} 
                #endregion
                this.Close();

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Profit Share Group Save" + ex.Message, LogManager.enumLogLevel.Error);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
            }
        }


        private void frmProfitShareGroup_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            LoadProfitShareGrid();

        }

        private void frmProfitShareGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("frmProfitShareGroup", "frmProfitShareGroup_FormClosing");

            try
            {
                //if (this.DialogResult == DialogResult.OK)
                //{
                //    _scope.Complete();
                //}
                //else
                //{
                //    if (Transaction.Current != null)
                //    {
                //        Transaction.Current.Rollback();
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                this.ShowErrorMessageBox(ex.Message);
            }
        }


        private void grdvwProfitShareGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Edit PSG Started", LogManager.enumLogLevel.Info);
                int RowIndex = e.RowIndex;
                if (RowIndex < 0)
                    return;

                bool EditRow = (e.ColumnIndex == 0);
                bool DeleteRow = (e.ColumnIndex == 1);

                if (!((DeleteRow) || (EditRow)))
                    return;

                if ((DeleteRow) && (e.RowIndex <= 2))
                {
                    MessageBox.Show("First 3 ShareHolders cannot be deleted.");
                    return;
                }

                if (EditRow)
                {

                    EditGridRow(RowIndex);

                }
                else if (DeleteRow)
                    DeleteGridRow(RowIndex);
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Clicking Edit/Delete button in Grid View : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }




    }
}
