using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    partial class EnterpriseDataContext
    {
        protected override void Dispose(bool disposing)
        {
            //if (System.Transactions.Transaction.Current != null)
            //{
            //    if (this.Transaction != null)
            //    {
            //        this.Transaction.Commit();
            //    }
            //}
            base.Dispose(disposing);
        }

        public override void SubmitChanges(System.Data.Linq.ConflictMode failureMode)
        {
            base.SubmitChanges(failureMode);
        }

      
    }
}
