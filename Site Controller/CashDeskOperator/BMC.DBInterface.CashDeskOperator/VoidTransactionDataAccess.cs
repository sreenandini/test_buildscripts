using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;

namespace BMC.DBInterface.CashDeskOperator
{
    public class VoidTransactionDataAccess
    {
        #region "Private Variables"
        #endregion
        
        #region "Public Properties"
        #endregion
        
        #region "Private Function"
        #endregion
        
        #region "Public Function"

        public int VoidTreasuryNegGen(VoidTranCreate VoidTranCreate)
        {
            //DataBaseServiceHandler.AddParameter<DateTime>("dDate", System.Data.DbType.DateTime, VoidTranCreate.Date),
            //DataBaseServiceHandler.AddParameter<string>("dTime", System.Data.DbType.String, ""),
            SqlParameter outputValue =
                DataBaseServiceHandler.AddParameter<int>("OutVal", System.Data.DbType.Int32, 0, System.Data.ParameterDirection.Output);

            try
            {
                //DataBaseServiceHandler.ConnectionString = CommonDataAccess.ExchangeConnectionString;
                DataBaseServiceHandler.ExecuteNonQuery(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_USP_CREATEVOIDTREASURY,
                    DataBaseServiceHandler.AddParameter<string>("TreasuryNo", System.Data.DbType.String, VoidTranCreate.TreasuryID),
                   DataBaseServiceHandler.AddParameter<DateTime>("VoidedDate",System.Data.DbType.DateTime,VoidTranCreate.Date),
                    DataBaseServiceHandler.AddParameter<string>("UserNo", System.Data.DbType.String, VoidTranCreate.UserNo),
                    outputValue);

                return int.Parse(outputValue.Value.ToString());
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);                
            }
            return 99;
        }

        public DataSet FillVoidTransactionList()
        {
            DataSet dsFill = new DataSet();

            try
            {
                dsFill = SqlHelper.ExecuteDataset(CommonDataAccess.ExchangeConnectionString, CommandType.StoredProcedure, DBConstants.CONSTANT_RSP_GETVOIDTRANSACTIONLIST);                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);                
            }
            return dsFill;
        }

        #endregion

        #region "Public Static Function"
        #endregion

    }
}
