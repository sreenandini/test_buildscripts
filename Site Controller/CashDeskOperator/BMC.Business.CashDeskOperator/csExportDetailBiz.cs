using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public class ExportDetailBiz
    {
        private ExportDetailDA _ExportDetailDataAccess = new ExportDetailDA(CommonDataAccess.ExchangeConnectionString);

        public List<UnExportedData> ReadUnExportedData(string sExport_Type)
        {
            List<UnExportedData> rsltUnExportedData = null;
            ISingleResult<rsp_ReadUnExportedDataResult> result = null;
            
            try
            {
                result = _ExportDetailDataAccess.ReadUnExportedData(sExport_Type);

                rsltUnExportedData = (from o in result
                                   select new UnExportedData()
                                   {
                                       ID = o.ID,
                                       Date = o.Date,
                                       ExportType = o.ExportType,
                                       Reference= o.Reference,
                                       Status = o.Status,
                                       IsSelected = false
                                   }).ToList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltUnExportedData;
        }        

        public List<StatusType> GetStatusType()
        {
            List<StatusType> rsltStatusType = null;
            ISingleResult<rsp_GetStatusTypeResult> result = null;
            
            try
            {
                result = _ExportDetailDataAccess.GetStatusType();

                rsltStatusType = (from o in result
                                  select new StatusType()
                                      {
                                          Type = o.Type,
                                          Description=o.Description
                                      }).ToList();
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
                _ExportDetailDataAccess.UpdateUnExportedData(iStatus, sExportIDs);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
    }
}