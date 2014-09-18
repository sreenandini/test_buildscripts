using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DBInterface.CashDeskOperator;
using System.Data;

namespace BMC.Business.CashDeskOperator
{
    
    public class DeclarationBiz
    {
        DeclarationDataAccess oDeclarationDB = null;
        public DeclarationBiz()
        {
             oDeclarationDB = new DeclarationDataAccess();
        }
        public DeclarationBiz(string ExchangeConn, string TicketingConn)
        {
             oDeclarationDB = new DeclarationDataAccess(ExchangeConn,TicketingConn);
        }
        public DataSet GetTicketVarianceByPos(int Collection_batch_No)

        {
           return  oDeclarationDB.GetTicketVarianceByPos(Collection_batch_No);  
        }
        public DataSet GetExceptionTicketsByPos(string Bar_Pos_Name, int CollectionNo)
        {
            return oDeclarationDB.GetExceptionTicketsByPos(Bar_Pos_Name, CollectionNo);
        }
        public void DeclareExceptionTicket(int Collection_No, string strBarcode, int Installation_No, int UseriD,decimal  iAmount)
        {
            oDeclarationDB.DeclareExceptionTicket(Collection_No, strBarcode, Installation_No, UseriD, iAmount);
        }
        public void DeclareExceptionTicketAsPaid(int Collection_No, string strBarcode, int Installation_No, int UseriD, decimal iAmount)
        {
            oDeclarationDB.DeclareExceptionTicketAsPaid(Collection_No, strBarcode, Installation_No, UseriD, iAmount);
        }
        public void UpdatePPTicketsAsPaid(string strTickets, string strAsset)
        {
            oDeclarationDB.usp_UpdatePPTicketsAsPaid(strTickets, strAsset);
        }
        public void AddCollectionToFullCollection(string c500, string c200, string c100, string c50, string c20, string c10,
       string c5, string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {
            oDeclarationDB.AddCollectionToFullCollection(c500, c200, c100, c50, c20, c10,
                c5, c2, c1, coins, ticketsIn,tokenValue, collectionNo);
        }
        public void AddCollectionToPartCollection(string c500, string c200, string c100, string c50, string c20, string c10,
   string c5, string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {
            oDeclarationDB.AddCollectionToPartCollection(c500, c200, c100, c50, c20, c10,
                c5, c2, c1, coins, ticketsIn, tokenValue, collectionNo);
        }
        public int  InsertDeclaredTickets(string VoucherXML, int User, int Installation_no, int collectionNo)
        {
            return oDeclarationDB.InsertDeclaredTickets( VoucherXML,  User,  Installation_no,  collectionNo);
        }
    }
}
