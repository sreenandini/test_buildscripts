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

namespace BMC.EnterpriseClient.Views
{
    public partial class ProfitShareGroupList : Form
    {

        public enum GridFormTypes
        {
            gftProfitShareGroup = 0,
        }
        public GridFormTypes GridFormType = GridFormTypes.gftProfitShareGroup;


        public ProfitShareGroupList()
        {

            InitializeComponent();
            LoadProfitShareGrid();

        }
        private void LoadProfitShareGrid()
        {
            try
            {
                ProfitShareGroupBusiness bProfitShareGroup = new ProfitShareGroupBusiness();
                grdvwProfitShareGroupMaster.DataSource = bProfitShareGroup.GetProfitShareGroup();
                grdvwProfitShareGroupMaster.Columns[0].Width = 50;
                grdvwProfitShareGroupMaster.Columns[1].Width = 50;
                grdvwProfitShareGroupMaster.Columns[2].Width = 100;
                grdvwProfitShareGroupMaster.Columns[3].Width = 120;
                grdvwProfitShareGroupMaster.Columns[4].Width = 120;
                grdvwProfitShareGroupMaster.Columns[5].Width = 200;

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error : Loading Profit Share Group grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void grdvwProfitShareGroupGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int RowIndex = e.RowIndex;
                if (RowIndex < 0)
                    return;

                bool EditRow = (e.ColumnIndex == 0);
                bool DeleteRow = (e.ColumnIndex == 1);

                if (!((DeleteRow) || (EditRow)))
                    return;

                if ((DeleteRow) && (e.RowIndex <= 2))
                {
                    MessageBox.Show("First 3 share holders can not be deleted.");
                    return;
                }

                if (EditRow)
                    EditGridRow(RowIndex);
                else if (DeleteRow)
                    DeleteGridRow(RowIndex);
            }

            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Clicking Edit/Delete button in grid view : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        private bool EditGridRow(int rowIndex)
        {
            try
            {
                bool result = false;
                result = EditProfitShareGridRow(rowIndex);
                if (result)
                    LoadProfitShareGrid();
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Editing grid row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool EditProfitShareGridRow(int rowIndex)
        {
            try
            {
                frmProfitShareGroupAddEdit ProfitShareGroup = new frmProfitShareGroupAddEdit(3);
                ProfitShareGroup.IsUpdate = true;
                int ShrID = -1;
                int.TryParse(grdvwProfitShareGroupMaster["ProfitShareGroupId", rowIndex].Value.ToString(), out ShrID);
                ProfitShareGroup._PSGroup.ProfitShareGroupId = ShrID;


                ProfitShareGroupBusiness objProfitShareGroup = new ProfitShareGroupBusiness();
                ProfitShareGroup._PSGroup.ProfitShareGroupName = grdvwProfitShareGroupMaster["ProfitShareGroupName", rowIndex].Value.ToString();
                ProfitShareGroup._PSGroup.ProfitShareGroupDescription = grdvwProfitShareGroupMaster["ProfitShareGroupDescription", rowIndex].Value.ToString();
                ProfitShareGroup.GridToText();

                ProfitShareGroup.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in editing ShareHolder : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool DeleteGridRow(int rowIndex)
        {

            try
            {
                bool result = false;
                if (GridFormType == GridFormTypes.gftProfitShareGroup)
                {
                    result = DeleteProfitShareGroupGridRow(rowIndex);
                    if (result)
                        LoadProfitShareGrid();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error - Deleting grid row : ", ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private bool DeleteProfitShareGroupGridRow(int rowIndex)
        {
            try
            {

                bool result = false;
                string strProfitShareGroupId = grdvwProfitShareGroupMaster["ProfitShareGroupId", rowIndex].Value.ToString();

                //If the selected Share holder name is existign in the DEONTDELETE list then
                //tell the user that - This shareholder can not be deleted.

                if (MessageBox.Show("Do you want to delete the ProfitShareGroup " + "'" + strProfitShareGroupId + "'" + " ?", "ProfitShareGroup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (DialogResult.Yes == DialogResult.Yes)
                    {
                        if (grdvwProfitShareGroupMaster.Rows[0].Cells[1].Selected || grdvwProfitShareGroupMaster.Rows[1].Cells[1].Selected || grdvwProfitShareGroupMaster.Rows[2].Cells[1].Selected)
                        {
                            MessageBox.Show("ProfitShareGroup" + strProfitShareGroupId + " cannot be deleted. Please make sure the ShareHolder is not associated ", "ShareHolder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return result;
                        }
                    }

                    ProfitShareGroupBusiness bProfitShareGroup = new ProfitShareGroupBusiness();
                    int ProfitShareGroupId = -1;
                    int.TryParse(grdvwProfitShareGroupMaster["ProfitShareGroupId", rowIndex].Value.ToString(), out ProfitShareGroupId);
                    if (bProfitShareGroup.DeleteProfitShareGroup(ProfitShareGroupId))
                    {
                        MessageBox.Show("ProfitShareHolderGroup  " + strProfitShareGroupId + " deleted successfully", "ProfitShareGroup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        try
                        {
                            //
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteLog("Error While Adding Audit Log for ShareHolder Delete: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
                        }
                        //del-ProfitShareGroupId,ProfitShareGroupName,ProfitShareGroupDescribtion,ProfitshareGroupPercentage
                    }

                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Deleting ProfitShareGroup : " + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProfitShareGroupAddEdit objProfitShare = new frmProfitShareGroupAddEdit(3);
            objProfitShare.ShowDialog();


        }
    }
}

