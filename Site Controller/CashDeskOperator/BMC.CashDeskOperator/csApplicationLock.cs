using System;
using System.Collections.Generic;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public class ApplicationLockObject
    {
        #region Private Variables
        ApplicationLockBiz _ApplicationLockBiz = new ApplicationLockBiz();
        #endregion

        #region Constructor
        public ApplicationLockObject() { }
        #endregion        
        
        #region Public Function
        public List<LockDetails> GetLockDetails(string lock_Application, string lock_Type)
        {
            List<LockDetails> rsltLockDetails = null;            
            try
            {
                rsltLockDetails = _ApplicationLockBiz.GetLockDetails(lock_Application, lock_Type);                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            return rsltLockDetails;
        }

        public void UpdateAppLockState(string Lock_IDs)
        {
            try
            {
                _ApplicationLockBiz.UpdateAppLockState(Lock_IDs);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public ApplicationLock GetLockTypes()
        {
            return _ApplicationLockBiz.GetLockTypes();
        }
        #endregion
    }
}
