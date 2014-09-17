using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stacker;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
namespace Stacker.Business
{
    public class StackerBusiness
    {
        public List<StackerEntity> GetStackerDetails()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<StackerEntity> result = new List<StackerEntity>();
                List<rsp_GetStackerDetailsResult> dsStacker = DataContext.GetStackerDetails(false).ToList();
                foreach (rsp_GetStackerDetailsResult stacker in dsStacker)
                {
                    result.Add(new StackerEntity()
                    {
                        StackerDescription = stacker.StackerDescription,
                        StackerID = stacker.Stacker_Id,
                        StackerName = stacker.StackerName,
                        StackerSize = Convert.ToInt32(stacker.StackerSize),
                        StackerStatus = stacker.StackerStatus
                    });
                }
                return result;
            }
        }
        public void AddStackerDetails(string StackerName, int StackerSize, bool StackerStatus, string StackerDescription)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_InsertStackerDetails(StackerName, StackerSize, StackerDescription, StackerStatus);
            }
        }
        public bool DeleteStacker(int StackerId)
        {
            bool result = false;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int? iResult=0;
                DataContext.usp_DeleteStackerDetails(StackerId,ref iResult);
                result = (iResult.Value == 1);
            }
            return result;
        }
        public void EditStackerDetails(int _StackerID, string StackerName, int StackerSize, bool StackerStatus, string StackerDescription)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_UpdateStackerDetails(_StackerID, StackerName, StackerSize, StackerDescription, StackerStatus);
            }
        }
        public int IsNameExists(String strStackerName, ref int? blNameExists, int stackerID)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.rsp_CheckStackerNameExists(strStackerName, ref blNameExists,stackerID);
            }
        }

        public Int32 IsStackerInUse(int StackerId)
        {
            Int32? iResult = 0;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.IsStackerInUse(StackerId, ref iResult);
            }
            return Convert.ToInt32(iResult);
        }
        //public int CheckStackerInUse(int StkID, ref int? IsStackerInUse)
        //{
        //    using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
        //    {
        //        return DataContext.IsStackerInUse(StkID, ref IsStackerInUse);
        //    }
        //}

    }
}
