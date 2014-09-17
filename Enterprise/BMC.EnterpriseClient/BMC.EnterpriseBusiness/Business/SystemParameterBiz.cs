using System;
using System.Collections.Generic;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    public class SystemParameterBiz
    {

        #region Data Member

        private static SystemParameterBiz _SysParameterBusiness;

        #endregion //Data Member

        #region Constructor

        public SystemParameterBiz()
        {
        }

        #endregion //Constructor



        #region Static Methods

        public static SystemParameterBiz CreateInstance()
        {
            if (_SysParameterBusiness == null)
                _SysParameterBusiness = new SystemParameterBiz();

            return _SysParameterBusiness;
        }

        #endregion //Static Methods

        public bool GeSystemParameterSettings()
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {

                    List<rsp_GetSystemSettingsResult> ListSetting = DataContext.GetSystemParameterSettings().ToList();
                    SystemParameterEntity.AutoGenerateStockCode = ListSetting[0].AutoGenerateStockCode;
                    
                }
               
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return SystemParameterEntity.AutoGenerateStockCode;
        }
    }
}