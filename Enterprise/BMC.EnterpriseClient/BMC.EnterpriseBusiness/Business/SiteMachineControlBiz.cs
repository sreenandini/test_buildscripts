using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;
using Audit.Transport;
namespace BMC.EnterpriseBusiness.Business
{
    public class SiteMachineControlBiz
    {
        private static SiteMachineControlBiz _MachineControlBiz;

        public static SiteMachineControlBiz CreateInstance()
        {
            if (_MachineControlBiz == null)
                _MachineControlBiz = new SiteMachineControlBiz();

            return _MachineControlBiz;
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public List<GetMachineControlDetails> GetLstMachineControlDetails(int SiteID,System.Windows.Forms.ImageList Img_list)
        {
            List<GetMachineControlDetails> objMachineControlEntityList = null;
            List<rsp_getsitemachinedetailsResult> MachineContolList;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    MachineContolList = DataContext.GetSiteMachineDetails(SiteID).ToList();
                }

                if (MachineContolList == null) return null;

                objMachineControlEntityList = (from obj in MachineContolList
                          select new GetMachineControlDetails
                          {
                              Position = obj.bar_position_name ,
                              bar_position_id = obj.bar_position_id,
                              bar_position_machine_enabled = obj.bar_position_machine_enabled,                              
                              Current = obj.current_status,
                              Change = obj.current_change,
                              installation_id = obj.installation_id,
                              GameTitle = obj.machine_name,
                              Asset = obj.machine_stock_no,
                              Image_Index = Img_list.Images[(obj.current_change == SiteMachineControlType.PENDING.ToString()) ? 0 : (obj.current_status == SiteMachineControlType.ENABLED.ToString()) ? 1 : 2],
                              Display_Status=obj.current_status,
                              Previous_Status = obj.current_status
                
                          }).ToList<GetMachineControlDetails>();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return objMachineControlEntityList;
        }        

        public int InsertExportHistory(string Reference, string Type, int id)
        {
            int retval = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retval = DataContext.usp_Export_History(Reference, Type, id);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return retval;
        }

        public int UpdateBarPositionForMachineContol(string Eh_Reference, string Eh_Type, int Bar_Pos_Status)
        {
            int retval = 0;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retval = DataContext.UpdateBarPositionForMachineControl(Eh_Reference, Eh_Type, Bar_Pos_Status);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return retval;
        }
    }
}
