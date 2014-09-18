using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DataAccess;
using System.Data.SqlClient;
using System.Data;
using BMC.Common.Utilities;

namespace BMC.DBInterface.CashDeskOperator
{
   public  class DeclarationDataAccess
    {
       string strConn = CommonDataAccess.ExchangeConnectionString;
       string strTicketConn = CommonDataAccess.TicketingConnectionString;

       public DeclarationDataAccess()
       {
            strConn = CommonDataAccess.ExchangeConnectionString;
            strTicketConn = CommonDataAccess.TicketingConnectionString;
       }
       
       public DeclarationDataAccess(string Connstring, string strTicketingCon)
       {
           strConn = Connstring;
           strTicketConn = strTicketingCon; 
       }
        public DataSet GetTicketVarianceByPos(int Collection_batch_No)
        {
            SqlParameter[] oParams=new SqlParameter[1] ;
            oParams[0]=new SqlParameter("Collection_Batch_No"  ,Collection_batch_No);
            return SqlHelper.ExecuteDataset(strConn, "rsp_GeTicketVarByPOS", oParams);   
        }
        public DataSet GetExceptionTicketsByPos(string Bar_Pos_Name, int CollectionNo)
        {
            SqlParameter[] oParams = new SqlParameter[2];
            oParams[0] = new SqlParameter("Bar_Pos_Name", Bar_Pos_Name);
            oParams[1] = new SqlParameter("@Collection_Batch_No", CollectionNo);
            return SqlHelper.ExecuteDataset(strTicketConn, "rsp_GetExceptionTicketsByPos", oParams);
        }
        public int DeclareExceptionTicket(int Collection_No, string strBarcode, int Installation_No, int UseriD, decimal  iAmount)
        {
            SqlParameter[] oParams = new SqlParameter[5];
            oParams[0] = new SqlParameter("iAmount", iAmount);
            oParams[1] = new SqlParameter("Collection_No", Collection_No);
            oParams[2] = new SqlParameter("strBarcode", strBarcode);
            oParams[3] = new SqlParameter("Installation_No", Installation_No);
            oParams[4] = new SqlParameter("UseriD", UseriD);

            return SqlHelper.ExecuteNonQuery(strConn, "usp_UpdateExceptionTickets", oParams);
        }

        public int DeclareExceptionTicketAsPaid(int Collection_No, string strBarcode, int Installation_No, int UseriD, decimal iAmount)
        {
            SqlParameter[] oParams = new SqlParameter[5];
            oParams[0] = new SqlParameter("iAmount", iAmount);
            oParams[1] = new SqlParameter("Collection_No", Collection_No);
            oParams[2] = new SqlParameter("strBarcode", strBarcode);
            oParams[3] = new SqlParameter("Installation_No", Installation_No);
            oParams[4] = new SqlParameter("UseriD", UseriD);

            return SqlHelper.ExecuteNonQuery(strConn, "usp_UpdateExceptionTicketsAsPaid", oParams);
        }


        public int usp_UpdatePPTicketsAsPaid(string strTickets, string strAsset)
        {
            SqlParameter[] oParams = new SqlParameter[2];
            oParams[0] = new SqlParameter("Tickets", strTickets);
            oParams[1] = new SqlParameter("Asset", strAsset);
            return SqlHelper.ExecuteNonQuery(strTicketConn, "usp_UpdatePPTicketsAsPaid_Counter", oParams);
        }


        public void AddCollectionToFullCollection(string c500, string c200, string c100, string c50, string c20, string c10,
          string c5, string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {

            SqlParameter[] oParams = new SqlParameter[16];
            oParams[0] = new SqlParameter("Cash_Collected_50000p", c500);
            oParams[1] = new SqlParameter("Cash_Collected_20000p",c200);
            oParams[2] = new SqlParameter("Cash_Collected_10000p", c100);
            oParams[3] = new SqlParameter("Cash_Collected_5000p", c50);
            oParams[4] = new SqlParameter("Cash_Collected_2000p", c20);
            oParams[5] = new SqlParameter("Cash_Collected_1000p", c10);
            oParams[6] = new SqlParameter("Cash_Collected_500p", c5);
            oParams[7] = new SqlParameter("Cash_Collected_200p", c2);
            oParams[8] = new SqlParameter("Cash_Collected_100p", c1);
            oParams[9] = new SqlParameter("Cash_Collected_50p",SqlDbType.Real);
            oParams[10] = new SqlParameter("Cash_Collected_20p", SqlDbType.Real);
            oParams[11] = new SqlParameter("Cash_Collected_10p", SqlDbType.Real);
            oParams[12] = new SqlParameter("Cash_Collected_5p", SqlDbType.Real);
            oParams[13] = new SqlParameter("Cash_Collected_2p", SqlDbType.Real);
            oParams[14] = new SqlParameter("Cash_Collected_1p", SqlDbType.Real);
            oParams[15] = new SqlParameter("CollectionNo", collectionNo);

            if (!Common.Utilities.ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("US"))
            {
                if (tokenValue == 100)
                    oParams[8].Value = coins.GetFloatFromString();
                else if (tokenValue == 200) //en-GB
                    oParams[7].Value = coins.GetFloatFromString();
                else
                {
                    oParams[7].Value = 0;
                    oParams[8].Value = 0;
                }
            }
            if (tokenValue == 50)
                oParams[9].Value = coins.GetFloatFromString();
            if (tokenValue == 20)
                oParams[10].Value = coins.GetFloatFromString();
            if (tokenValue == 25)
                oParams[12].Value = coins.GetFloatFromString();
            if (tokenValue == 10)
                oParams[11].Value = coins.GetFloatFromString();
            if (tokenValue == 5)
                oParams[12].Value = coins.GetFloatFromString();
            if (tokenValue == 2)
                oParams[13].Value = coins.GetFloatFromString();
            if (tokenValue == 1)
                oParams[14].Value = coins.GetFloatFromString();

            SqlHelper.ExecuteNonQuery(strConn, "usp_DeclareCollection_Counter", oParams);
        }

        public void AddCollectionToPartCollection(string c500, string c200, string c100, string c50, string c20, string c10,
          string c5, string c2, string c1, string coins, string ticketsIn, int tokenValue, int collectionNo)
        {

            SqlParameter[] oParams = new SqlParameter[16];
            oParams[0] = new SqlParameter("Cash_Collected_50000p", c500);
            oParams[1] = new SqlParameter("Cash_Collected_20000p", c200);
            oParams[2] = new SqlParameter("Cash_Collected_10000p", c100);
            oParams[3] = new SqlParameter("Cash_Collected_5000p", c50);
            oParams[4] = new SqlParameter("Cash_Collected_2000p", c20);
            oParams[5] = new SqlParameter("Cash_Collected_1000p", c10);
            oParams[6] = new SqlParameter("Cash_Collected_500p", c5);
            oParams[7] = new SqlParameter("Cash_Collected_200p", c2);
            oParams[8] = new SqlParameter("Cash_Collected_100p", c1);
            oParams[9] = new SqlParameter("Cash_Collected_50p", SqlDbType.Real);
            oParams[10] = new SqlParameter("Cash_Collected_20p", SqlDbType.Real);
            oParams[11] = new SqlParameter("Cash_Collected_10p", SqlDbType.Real);
            oParams[12] = new SqlParameter("Cash_Collected_5p", SqlDbType.Real);
            oParams[13] = new SqlParameter("Cash_Collected_2p", SqlDbType.Real);
            oParams[14] = new SqlParameter("Cash_Collected_1p", SqlDbType.Real);
            oParams[15] = new SqlParameter("CollectionNo", collectionNo);

            if (!Common.Utilities.ExtensionMethods.CurrentSiteCulture.ToUpper().Contains("US"))
            {
                if (tokenValue == 100)
                    oParams[8].Value = coins.GetFloatFromString();
                else if (tokenValue == 200) //en-GB
                    oParams[7].Value = coins.GetFloatFromString();
                else
                {
                    oParams[7].Value = 0;
                    oParams[8].Value = 0;
                }
            }
            if (tokenValue == 50)
                oParams[9].Value = coins.GetFloatFromString();
            if (tokenValue == 20)
                oParams[10].Value = coins.GetFloatFromString();
            if (tokenValue == 25)
                oParams[12].Value = coins.GetFloatFromString();
            if (tokenValue == 10)
                oParams[11].Value = coins.GetFloatFromString();
            if (tokenValue == 5)
                oParams[12].Value = coins.GetFloatFromString();
            if (tokenValue == 2)
                oParams[13].Value = coins.GetFloatFromString();
            if (tokenValue == 1)
                oParams[14].Value = coins.GetFloatFromString();

            SqlHelper.ExecuteNonQuery(strConn, "usp_Part_DeclareCollection_Counter", oParams);
        }


         public int InsertDeclaredTickets(string VoucherXML, int User, int Installation_no , int collectionNo)
        {
            SqlParameter[] oParams = new SqlParameter[4];
            oParams[0] = new SqlParameter("Voucher", VoucherXML);
            oParams[1] = new SqlParameter("User",User);
            oParams[2] = new SqlParameter("Inserted_Installation_ID", Installation_no);
            oParams[3] = new SqlParameter("Inserted_Collection_ID", collectionNo);
            return  int.Parse(SqlHelper.ExecuteScalar(strConn, "usp_InsertDeclaredTicket_Counter", oParams).ToString());
           
             
        }
     }
}
