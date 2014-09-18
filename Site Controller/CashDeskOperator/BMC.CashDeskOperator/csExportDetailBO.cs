using System;
using System.Collections.Generic;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ExportDetailBO
    {
        #region Private Variables
        ExportDetailBiz _ExportDetailBiz = new ExportDetailBiz();
        #endregion

        #region Constructor
        public ExportDetailBO() { }
        #endregion        
        
        #region Public Function
        public List<UnExportedData> ReadUnExportedData(string sExport_Type)
        {
            List<UnExportedData> rsltUnExportedData = null;            

            try
            {
                rsltUnExportedData = _ExportDetailBiz.ReadUnExportedData(sExport_Type);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltUnExportedData;
        }        

        public List<StatusType> GetStatusTypes()
        {
            List<StatusType> rsltStatusType = null;            

            try
            {
                rsltStatusType = _ExportDetailBiz.GetStatusType();
            }

            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltStatusType;
        }

        public void UpdateUnExportedData(int iStatus, string sExportIDs)
        {
            try
            {
                _ExportDetailBiz.UpdateUnExportedData(iStatus, sExportIDs);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        #endregion
    }
}
