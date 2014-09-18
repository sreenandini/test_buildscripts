using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Business.CashDeskOperator;
using BMC.Transport;


namespace BMC.CashDeskOperator
{
    public class Declaration
    {
        DeclarationBiz oDeclareationBiz = null;
        public Declaration()
        {
            oDeclareationBiz = new DeclarationBiz();
        }

        public Declaration(string ExchangeConn, string TicketingConn)
        {
            oDeclareationBiz = new DeclarationBiz(ExchangeConn, TicketingConn);
        }

        public DataSet GetTicketVarianceByPos(int Collection_batch_No)
        {
            return oDeclareationBiz.GetTicketVarianceByPos(Collection_batch_No);
        }
        public List<ExceptionVouchers> GetExceptionTicketsByPos(string Bar_Pos_Name, int  CollectioNo)
        {
            DataSet Ods = oDeclareationBiz.GetExceptionTicketsByPos(Bar_Pos_Name, CollectioNo);
            List<ExceptionVouchers> lstVoucher = (from voucher in Ods.Tables[0].AsEnumerable()
                                                  select new ExceptionVouchers()
                                                 {
                                                     IsChecked = false,
                                                     strBarcode = voucher.Field<string>("strBarcode"),
                                                     iAmount = voucher.Field<decimal>("iAmount"),
                                                     ErrCode = voucher.Field<int>("ErrCode"),
                                                     Device = voucher.Field<string>("Device"),
                                                     Collection_No = voucher.Field<int>("Collection_No"),
                                                     Installation_no = voucher.Field<int>("Installation_no"),
                                                     Error_Description = voucher.Field<string>("Error_Description")
                                                 }).ToList<ExceptionVouchers>();

            return lstVoucher;

        }
        public void DeclareExceptionTicket(int Collection_No, string strBarcode, int Installation_No, int UseriD,decimal  iAmount)
        {
             oDeclareationBiz.DeclareExceptionTicket(Collection_No, strBarcode, Installation_No, UseriD,iAmount);
        }
        public void DeclareExceptionTicketAsPaid(int Collection_No, string strBarcode, int Installation_No, int UseriD, decimal iAmount)
        {
            oDeclareationBiz.DeclareExceptionTicketAsPaid(Collection_No, strBarcode, Installation_No, UseriD, iAmount);
        }
        public void UpdatePPTicketsAsPaid(string strTickets, string strAsset)
        {
            oDeclareationBiz.UpdatePPTicketsAsPaid(strTickets, strAsset);
        }

        public void AddCollectionToFullCollection(string c500, string c200, string c100, string c50, string c20, string c10,
       string c5, string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {
            oDeclareationBiz.AddCollectionToFullCollection(c500, c200, c100, c50, c20, c10,
                c5, c2, c1, coins, ticketsIn, tokenValue, collectionNo);
        }

        public int  InsertDeclaredTickets(string VoucherXML, int User, int Installation_no, int collectionNo)
        {
            return oDeclareationBiz.InsertDeclaredTickets(VoucherXML, User, Installation_no, collectionNo);
        }
    }
}
