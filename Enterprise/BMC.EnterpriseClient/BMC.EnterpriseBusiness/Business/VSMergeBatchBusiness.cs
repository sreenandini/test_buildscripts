using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.EnterpriseBusiness.Business;
using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;
using BMC.CoreLib.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    // RAJKUMAR R
    public class VSMergeBatchBusiness
    {
        AuditViewerBusiness business = new AuditViewerBusiness();
        AssetManagementBiz objAssetBiz = null;
        
        public bool IsBatchAvailableCheck(int batchNo, int SiteId)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                int item = Convert.ToInt32(obj.FnGetBatch(batchNo, SiteId));
                if (item > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        public bool DeleteBatchCheck(int DeleteBatchNo)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();

                if (Convert.ToBoolean(obj.usp_Delete_Batch(DeleteBatchNo)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public string GetDeletedBatchList(int batchID)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                string newsBatchList = obj.FnGetDeletedBatch(batchID);
                return newsBatchList;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "";
            }
        }
        public int CreateMergeBatchCheck(int DeleteBatchNo, int MergeBatchNo)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                obj.usp_Create_MergeBatch(DeleteBatchNo, MergeBatchNo);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int MergeBatchCheck(int DeleteBatchNo, int MergeBatchNo)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                obj.usp_Merge_Batch(DeleteBatchNo,MergeBatchNo);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public int DeMergeBatchcheck(int DeleteBatchNo, int MergeBatchNo)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                obj.usp_DeMerge_Batch(DeleteBatchNo, MergeBatchNo);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }
        public string ProcessNegativeNetcheck(int MinId, int SiteID, bool Exclude)
        {
            try
            {

                double dBatchNegativeNet = 0.22;
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                string newsBatchList = (obj.FnGetMergeBatchList(MinId, SiteID, Exclude));
                string[] sBatchList = newsBatchList.Split(',');
                for (int i = 1; i < sBatchList.Length; i++)
                {
                    CalculateNegativeNet(Convert.ToInt32(sBatchList[i - 1]), Convert.ToInt32(sBatchList[i]), dBatchNegativeNet);
                }
                return "True";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return "False";
            }
        }
        public double CalculateNegativeNet(int iPreviousBatchId, int iCurrentBatchId, double SGVI_Batch_Net_Value)
        {
            try
            {
                EnterpriseDataContext obj = EnterpriseDataContextHelper.GetDataContext();
                double item = obj.esp_Calculate_Merge_Batch_Negative_Net(iPreviousBatchId, iCurrentBatchId, SGVI_Batch_Net_Value);
                return item;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0.00;
            }
        }
        public void IDM_DEMERGE_COL_BATCH(int DeleteBatchNo, int MergedBatchNo, int SiteID)
        {
            try
            {
                int MinBatchNo = 0;
                string GetDeletedBatchLists = GetDeletedBatchList(MergedBatchNo);
                string[] DeletedBatchNos = GetDeletedBatchLists.Split(',');
                for (int i = 0; i <= DeletedBatchNos.Length; i++)
                {
                    MinBatchNo = MinBatchNo == 0 ? Convert.ToInt32(DeletedBatchNos[i]) : Math.Min(Convert.ToInt32(DeletedBatchNos[i]), MinBatchNo);
                    DeMergeBatchcheck(Convert.ToInt32(DeletedBatchNos[i]), MergedBatchNo);
                }
                ProcessNegativeNetcheck(MinBatchNo, SiteID, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public void AuditMergeBatch(int MergeBatchvalue,int DeletedBatchNo,int SiteId)
        {
            try
            {
                business.InsertAuditData(new Audit.Transport.Audit_History
                {
                    EnterpriseModuleName = ModuleNameEnterprise.AssetTemplate,
                    Audit_Screen_Name = "MergeBatchDetails",
                    Audit_Desc = "Batch No: " + DeletedBatchNo + "  Merged With  Batch No:" + MergeBatchvalue + "-->" + MergeBatchvalue + "",
                    AuditOperationType = OperationType.MODIFY,
                    Audit_Field = "Asset Number",
                    Audit_New_Vl = DeletedBatchNo.ToString(),
                    Audit_Old_Vl = MergeBatchvalue.ToString(),
                    Audit_User_ID = 1,
                    Audit_User_Name = "admin"
                }, false);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error While Adding Audit Log for Template Update: Error Message:" + ex.Message, LogManager.enumLogLevel.Error);
            }
        }
        

    }

}
