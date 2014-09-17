using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public class CommonUtility
    {
        public long VerifyValidNumberLong(string TheNumber)
        {
            bool numeric = false;
            long value = 0;
            try
            {
                numeric = isNumeric(TheNumber);
                if (numeric == true)
                {
                    value = Int64.Parse(TheNumber);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return value;
        }
        public bool isNumeric(String TheNumber)
        {
            Double result;
            return double.TryParse(TheNumber, out result);
        }
    }
}
