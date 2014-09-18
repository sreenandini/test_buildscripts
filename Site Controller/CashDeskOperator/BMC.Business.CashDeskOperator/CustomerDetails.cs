using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
using BMC.Transport;
using System.Globalization;
namespace BMC.Business.CashDeskOperator
{
  public  class CustomerDetailsBusiness
    {
      CustomerDataAccess oCustomerDataAccess = new CustomerDataAccess("");    
      public CustomerDetailsBusiness()
        {
        }
        public int InsertCustomer(BMC.Transport.Customer oCustomerDetailsEntity)
        {                               
            return Convert.ToInt32(oCustomerDataAccess.InsertCustomerDetails(oCustomerDetailsEntity.Title,oCustomerDetailsEntity.FirstName,
                oCustomerDetailsEntity.MiddleName,oCustomerDetailsEntity.LastName,oCustomerDetailsEntity.ADDRESS1,oCustomerDetailsEntity.ADDRESS2,
                oCustomerDetailsEntity.ADDRESS3,oCustomerDetailsEntity.PinCode,oCustomerDetailsEntity.BankAccNo));
            
        }
        public List<SearchCustomerDetailsResult> SearchCustomer(BMC.Transport.Customer oCustomerDetailsEntity)
        {

            return oCustomerDataAccess.rsp_SearchCustomerDetails(oCustomerDetailsEntity.FirstName,
                oCustomerDetailsEntity.LastName, oCustomerDetailsEntity.PinCode, oCustomerDetailsEntity.BankAccNo).ToList();

        }
        public long RecentCustomerID()
        {
            return oCustomerDataAccess.RecentCustomerID();
        }
    }
}
