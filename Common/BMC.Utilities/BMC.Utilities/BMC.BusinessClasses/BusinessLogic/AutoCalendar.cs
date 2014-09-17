using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.Common.ConfigurationManagement;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class AutoCalendar
    {
        public bool CheckAndUpdateAutoCalendar()
        {
            LogManager.WriteLog("AC - > inside CheckAndUpdateAutoCalendar method", LogManager.enumLogLevel.Info);
            try
            {
                bool result = true;
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    result = Convert.ToBoolean(context.CheckAndUpdateAutoCalendar());
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool CheckAndUpdateAlertRecurrence()
        {
            LogManager.WriteLog("AC - > inside CheckAndUpdateAlertRecurrence method", LogManager.enumLogLevel.Info);
            try
            {
                bool result = true;
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    result = Convert.ToBoolean(context.CheckAndUpdateAlertRecurrence());
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool CheckAndCreateCalendar()
        {
            LogManager.WriteLog("AC - > inside CheckAndCreateCalendar method", LogManager.enumLogLevel.Info);
            try
            {
                bool result = true;
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    result = Convert.ToBoolean(context.CheckAndCreateCalendar());
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool CheckAndUpdateNewCalendar(int MinDays)
        {
            LogManager.WriteLog("AC - > inside CheckAndUpdateNewCalendar method", LogManager.enumLogLevel.Info);
            try
            {
                bool result = true;
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    result = Convert.ToBoolean(context.CheckAndUpdateNewCalendar(MinDays));
                }
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }
    }
}
