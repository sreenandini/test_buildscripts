using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BMC.DataAccess;
using BMC.Common.ExceptionManagement;
using BMC.Transport.CashDeskOperatorEntity;
using System.Data.SqlClient;

namespace BMC.DBInterface.CashDeskOperator
{
    public class ExceptionVoucherDataAccess
    {
        #region "Private Variables"
        #endregion

        #region "Public Properties"
        #endregion

        #region "Private Function"
        #endregion

        #region "Public Function"

        /// <summary>
        /// Check if Ticket is a PP ticket.
        /// </summary>
        /// <returns>1 if PP ticket; otherwise 0</returns>
        public int IsExceptionVoucher(string TicketNumber)
        {
            try
            {
                SqlParameter[] paramBarcode = new SqlParameter[2];
                paramBarcode[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
                paramBarcode[0].Value = TicketNumber;
                paramBarcode[0].Direction = ParameterDirection.Input;

                paramBarcode[1] = new SqlParameter("@Status", SqlDbType.Int);
                paramBarcode[1].Direction = ParameterDirection.Output;

                int iCount;
                SqlConnection objConnection;
                using (objConnection = new SqlConnection(CommonDataAccess.TicketingConnectionString))
                {
                    SqlCommand objCommand;
                    using (objCommand = objConnection.CreateCommand())
                    {
                        objCommand.Parameters.AddRange(paramBarcode);
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandTimeout = SqlHelper.LoadCommandTimeout();
                        objCommand.CommandText = "rsp_IsExceptionVoucher";
                        objConnection.Open();
                        objCommand.ExecuteNonQuery();
                        objConnection.Close();

                        iCount = (int)objCommand.Parameters["@Status"].Value;                        
                    }//end cmd
                }//end con
                return iCount;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }

        }

        public int MarkExceptionVoucherActive(string TicketNumber)
        {
            try
            {
                SqlParameter[] paramBarcode = new SqlParameter[2];
                paramBarcode[0] = new SqlParameter("@TicketNumber", SqlDbType.VarChar);
                paramBarcode[0].Value = TicketNumber;
                paramBarcode[0].Direction = ParameterDirection.Input;

                paramBarcode[1] = new SqlParameter("@RetVal", SqlDbType.Int);
                paramBarcode[1].Direction = ParameterDirection.Output;

                int iCount;
                SqlConnection objConnection;
                using (objConnection = new SqlConnection(CommonDataAccess.TicketingConnectionString))
                {
                    SqlCommand objCommand;
                    using (objCommand = objConnection.CreateCommand())
                    {
                        objCommand.Parameters.AddRange(paramBarcode);
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.CommandTimeout = SqlHelper.LoadCommandTimeout();
                        objCommand.CommandText = "usp_MarkExceptionVoucherActive";
                        objConnection.Open();
                        objCommand.ExecuteNonQuery();
                        objConnection.Close();

                        iCount = (int)objCommand.Parameters["@RetVal"].Value;
                    }//end cmd                    
                }//end connection
                return iCount;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return -1;
            }
        }
        #endregion
    }
}
