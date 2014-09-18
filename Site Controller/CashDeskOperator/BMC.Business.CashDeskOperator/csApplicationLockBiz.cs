using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public class ApplicationLockBiz
    {
        private ApplicationLockDataAccess _ApplicationLockDataAccess = new ApplicationLockDataAccess(CommonDataAccess.ExchangeConnectionString);

        public List<LockDetails> GetLockDetails(string lock_Application,string lock_Type)
        {
            List<LockDetails> rsltLockDetails = null;
            ISingleResult<rsp_GetLockDetailsResult> result = null;
            try
            {
                result = _ApplicationLockDataAccess.GetLockDetails(lock_Application, lock_Type);

                rsltLockDetails = (from o in result
                                   select new LockDetails()
                                   {
                                       Lock_ID = o.Lock_ID,
                                       Lock_Application = o.Lock_Application,
                                       Lock_Created = o.Lock_Created,
                                       Lock_Identifier = o.Lock_Identifier,
                                       Lock_Machine = o.Lock_Machine,
                                       Lock_Type = o.Lock_Type,
                                       Lock_User = o.Lock_User
                                   }).ToList();
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
                _ApplicationLockDataAccess.UpdateAppLockState(Lock_IDs);
            }
            catch (Exception Ex)
            {                
                ExceptionManager.Publish(Ex);
            }
        }

        public ApplicationLock GetLockTypes()
        {
            ApplicationLock rsltApplicationLock = null;
            try
            {
                var result = _ApplicationLockDataAccess.GetLockTypes();
                var lstApplicationTypes = result.GetResult<ApplicationTypes>().ToList<ApplicationTypes>();
                var lstLockTypes = result.GetResult<LockTypes>().ToList<LockTypes>();
                rsltApplicationLock = new ApplicationLock();
                rsltApplicationLock.ApplicationType = lstApplicationTypes;
                rsltApplicationLock.LockType = lstLockTypes;
            }
            catch (Exception Ex)
            {                
                ExceptionManager.Publish(Ex);
            }

            return rsltApplicationLock;
        }
    }
}
