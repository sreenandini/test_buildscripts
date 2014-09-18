using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;

namespace BMC.CashDeskOperator.BusinessObjects
{
    public partial class IssueTicketBusinessObject : IIssueTicket
    {
        #region Private Variables
        private IssueTicket issueTicket = new IssueTicket();
        #endregion

        #region Constructor
        private IssueTicketBusinessObject() { }
        #endregion

        #region Public Function
        public PrintTicketErrorCodes IssueTicket(IssueTicketEntity IssueTicketEntity)
        {
            PrintTicketErrorCodes ErrorCode=PrintTicketErrorCodes.Failure;
            Voucher voucher = new Voucher();

            try
            {
                if ((ErrorCode = issueTicket.CreateTicket(IssueTicketEntity, ref voucher)) == PrintTicketErrorCodes.Success)
                {
                    voucher.Value = IssueTicketEntity.dblValue;
                    voucher.SBarCode = IssueTicketEntity.BarCode;
                    voucher.Header = IssueTicketEntity.TicketHeader;
                  
                    (oCommonUtilities.CreateInstance()).PrintCommonReceipt(voucher);
                }                
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return ErrorCode;
        }
        #endregion


        public string GetPrinterInformation()
        {
            return issueTicket.GetPrinterInformation();                        
        }
        

        #region
        public static IIssueTicket CreateInstance()
        {
            return new IssueTicketBusinessObject();
        }
        #endregion
    }
}
