using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProfitShareGroup;
using BMC.EnterpriseDataAccess;
using BMC.EnterpriseBusiness.Entities;
using BMC.Common.LogManagement;

namespace ProfitShareGroup.Business
{
  public  class ProfitShareGroupBusiness
    {
        public List<ProfitShareGroupEntity> GetProfitShareGroup()
        {
            //call mehod to db layer
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {                
                List<ProfitShareGroupEntity> result = new List<ProfitShareGroupEntity>();
                List<rsp_GetProfitShareDetailResult> dsProfitShareGroup = DataContext.rsp_GetProfitShareDetail().ToList();//sp name
                foreach (rsp_GetProfitShareDetailResult ProfitShareGroup in dsProfitShareGroup)
                {
                    result.Add(new ProfitShareGroupEntity()
                    {
                        ProfitShareGroupId = ProfitShareGroup.ProfitShareGroupId,
                        ProfitShareGroupName =ProfitShareGroup .ProfitShareGroupName ,
                        ProfitSharePercentage = ProfitShareGroup.ProfitSharePercentage ,
                        ProfitShareGroupDescription =ProfitShareGroup .ProfitShareGroupDescription       
                    }
                    );
                }
                return result;
            }
        }

        public int InsertProfitShareGroup(int ProfitShareGroupId, string ProfitShareGroupName, double ProfitSharePercentage, string ProfitShareGroupDescription,ref int? lbPSGroupId)
        {

            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                int iResult = 0;
                iResult = DataContext.usp_InsertProfitShareGroupDetails(ProfitShareGroupName, ProfitSharePercentage, ProfitShareGroupDescription);
                return iResult;
            }
        }

      public bool DeleteProfitShareGroup(int ProfitShareGroupId)
      {
          bool result = false;
          using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
          {
              int? iResult = 0;
              DataContext.usp_DeleteProfitShareGroup(ProfitShareGroupId, ref iResult);
              result = (iResult.Value == 1);
          }
          return result;

      }
      public void EditProfitShareGroup(int ProfitShareGroupId, string ProfitShareGroupName, double ProfitSharePercentage,string ProfitShareGroupDescription)
      {
          try
          {

              using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
              {
                 // DataContext.usp_UpdateProfitShareGroupDetails(ProfitShareGroupId, ProfitShareGroupName, ProfitSharePercentage, ProfitShareGroupDescription);

              }
          }
          catch (Exception ex)
          {
              LogManager.WriteLog("Error:Editing" + ex.Message, LogManager.enumLogLevel.Error);
          }
              
      }
 
    }
  }
