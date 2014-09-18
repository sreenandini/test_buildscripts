using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using System.Data.Linq;
using BMC.Business.CashDeskOperator;
namespace BMC.CashDeskOperator.BusinessObjects
{
    public class CustomerDetails : ICustomerDetails
    {
        CustomerDetailsBusiness oCustomerDetails = new CustomerDetailsBusiness();
        #region ICustomerDetails Members

        public int InsertCustomer(Customer oCustomerDetailsEntity)
        {
           
           return oCustomerDetails.InsertCustomer(oCustomerDetailsEntity);
        }
        public List<SearchCustomerDetailsResult> SearchCustomer(Customer oCustomerDetailsEntity)
        {        
            return oCustomerDetails.SearchCustomer(oCustomerDetailsEntity);
        }
        public long  RecentCustomerID()
        {           
                return oCustomerDetails.RecentCustomerID();          
        }
        #endregion
    }
}
