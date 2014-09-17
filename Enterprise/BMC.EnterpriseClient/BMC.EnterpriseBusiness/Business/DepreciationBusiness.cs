using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System.ComponentModel;


namespace BMC.EnterpriseBusiness.Business
{
    public class DepreciationBusiness
    {
        private static DepreciationBusiness _depreciation;

        public static DepreciationBusiness CreateInstance()
        {
            if (_depreciation == null)
                _depreciation = new DepreciationBusiness();

            return _depreciation;
        }

        #region Data Load Methods

        public BindingList<DepreciationEntity> LoadDepreciationPolicy(int? DepreciationPolicyID)
        {
            try
            {

                BindingList<DepreciationEntity> obcoll = new BindingList<DepreciationEntity>();
                List<rsp_GetDepreciationPolicyResult> DepreciationPolicyList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepreciationPolicyList = DataContext.GetDepreciationPolicy(DepreciationPolicyID).ToList();
                }
                foreach (rsp_GetDepreciationPolicyResult obj in DepreciationPolicyList)
                {
                    obcoll.Add(new DepreciationEntity
                     {
                         Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                         Depreciation_Policy_Description = obj.Depreciation_Policy_Description,
                         Depreciation_Policy_Residual_Value = obj.Depreciation_Policy_Residual_Value
                     });

                }

                return obcoll;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DepreciationEntity> LoadDepreciationPoliciesDetails(int Depreciation_Policy_ID)
        {
            try
            {

                List<DepreciationEntity> obcoll = null;
                List<rsp_GetDepreciationPolicyDetailsResult> DepList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepList = DataContext.GetDepreciationPolicyDetails(Depreciation_Policy_ID).ToList<rsp_GetDepreciationPolicyDetailsResult>();
                }

                obcoll = (from obj in DepList
                          select new DepreciationEntity
                          {
                              Depreciation_Policy_ID = obj.Depreciation_Policy_ID,
                              Depreciation_Policy_Details_ID = obj.Depreciation_Policy_Details_ID,
                              Depreciation_Policy_Description = obj.Depreciation_Policy_Description,
                              Depreciation_Policy_Residual_Value = obj.Depreciation_Policy_Residual_Value,
                              Depreciation_Policy_Details_Period = obj.Depreciation_Policy_Details_Period,
                              Depreciation_Policy_Details_Duration = obj.Depreciation_Policy_Details_Duration,
                              Depreciation_Policy_Details_Percentage = obj.Depreciation_Policy_Details_Percentage
                          }).ToList<DepreciationEntity>();

                return obcoll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GetDepreciationPolicyPercentResult GetDepreciationPolicyPercent(int Depreciation_Policy_ID, int Depreciation_Policy_Details_ID)
        {
            try
            {

                GetDepreciationPolicyPercentResult obcoll = null;
                List<rsp_GetDepreciationPolicyPercentResult> DepList;
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DepList = DataContext.GetDepreciationPolicyPercent(Depreciation_Policy_ID, Depreciation_Policy_Details_ID).ToList();
                }
                if (DepList.Count > 0)
                {
                    obcoll = new GetDepreciationPolicyPercentResult { TotalDrop = DepList[0].TotalDrop };
                }
                return obcoll;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Data Update Methods

        public bool DeleteDepreciationPolicy(int Depreciation_Policy_ID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (DataContext.DeleteDepreciationPolicy(Depreciation_Policy_ID) >= 0);
                }

            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool IsDepreciationPolicyExists(string DepreciationPolicyName)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (bool)DataContext.IsDepreciationPolicyExists(DepreciationPolicyName);
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool InsertDepreciationPolicy(string DepreciationPolicyName, int Depreciation_Policy_Residual_Value, ref int Depreciation_Policy_ID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? Depreciation_PolicyID = 0;
                    DataContext.InsertDepreciationPolicy(DepreciationPolicyName, Depreciation_Policy_Residual_Value, ref Depreciation_PolicyID);
                    if (Depreciation_PolicyID.HasValue && Depreciation_PolicyID.Value > 0)
                    {
                        Depreciation_Policy_ID = Depreciation_PolicyID.Value;
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool UpdateDepreciationPolicy(int Depreciation_Policy_ID, string DepreciationPolicyName, float Depreciation_Policy_Residual_Value)
        {

            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateDepreciationPolicy(Depreciation_Policy_ID, DepreciationPolicyName, Depreciation_Policy_Residual_Value) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool InsertDepreciationDetails(int Depreciation_Policy_ID, int Depreciation_Policy_Details_Period, int Depreciation_Policy_Details_Duration, int Depreciation_Policy_Details_Percentage, ref int Depreciation_PD_ID)
        {
            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    int? Depreciation_PolicyDetailsID = 0;
                    DataContext.InsertDepreciationDetails(Depreciation_Policy_ID, Depreciation_Policy_Details_Period, Depreciation_Policy_Details_Duration, Depreciation_Policy_Details_Percentage, ref Depreciation_PolicyDetailsID);
                    if (Depreciation_PolicyDetailsID.HasValue && Depreciation_PolicyDetailsID.Value > 0)
                    {
                        Depreciation_PD_ID = Depreciation_PolicyDetailsID.Value;
                        retVal = true;
                    }
                }

            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool UpdateDepreciationDetails(int Depreciation_PD_ID, int Depreciation_Policy_Details_Duration, int Depreciation_Policy_Details_Percentage)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateDepreciationDetails(Depreciation_PD_ID, Depreciation_Policy_Details_Duration, Depreciation_Policy_Details_Percentage) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool UpdateDepreciationUseDefault(bool Depreciation_Policy_UseDefault)
        {

            bool retVal = false;
            try
            {

                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    if (DataContext.UpdateDepreciationUseDefault(Depreciation_Policy_UseDefault) >= 0)
                    {
                        retVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        public bool DeleteDepreciationDetails(int Depreciation_PD_ID)
        {
            bool retVal = false;
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    retVal = (DataContext.DeleteDepreciationDetails(Depreciation_PD_ID) >= 0);
                }

            }
            catch (Exception ex)
            {
                retVal = false;
                throw ex;
            }
            return retVal;
        }

        #endregion

    }
}
