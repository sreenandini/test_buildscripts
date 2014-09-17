using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareHolder;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;

namespace ShareHolder.Business
{
    public class ShareHolderBusiness
    {
        public List<ShareHolderEntity> GetShareHolders()
        {
            //call method to db layer
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                List<ShareHolderEntity> result = new List<ShareHolderEntity>();
                List<rsp_GetShareHolderResult> dsShareHolder = DataContext.rsp_GetShareHolder().ToList();
                foreach (rsp_GetShareHolderResult ShareHolder in dsShareHolder)
                {
                    result.Add(new ShareHolderEntity()

                    {
                        Id = ShareHolder.ShareHolderId,
                        Name = ShareHolder.ShareHolderName,
                        Description = ShareHolder.ShareHolderDescription,
                        
                        
                    });
                }
                return result;
            }
        }
        public bool DeleteShareHolder(int ShareHolderId)
        {
            bool result = false;
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int? iResult = 0;
                DataContext.usp_DeleteShareHolder(ShareHolderId, ref iResult);
                result = (iResult.Value == 1);
            }
            return result;

        }

        public void EditShareHolder(int ShareHolderId, string ShareHolderName, string ShareHolderDescription)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_UpdateShareHolderDetails(ShareHolderId, ShareHolderName, ShareHolderDescription);
            }
        }
        public void AddShareHolder(string ShareHolderName, string ShareHolderDescription)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.usp_InsertShareHolderDetails(ShareHolderName, ShareHolderDescription);
            }
        }
        public int IsNameExists(String strshareholdername, int shareHolderId, ref int? blNameExists)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.rsp_CheckShareHolderNameExists(strshareholdername, shareHolderId, ref blNameExists);
            }
        }

        public void CheckShareholder_ExistsIn_PSG_Or_ESG(int iShareholderId, ref bool? bExists)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                DataContext.rsp_CheckShareholder_ExistsIn_PSG_Or_ESG(iShareholderId, ref bExists);
            }
        }
    }
}
