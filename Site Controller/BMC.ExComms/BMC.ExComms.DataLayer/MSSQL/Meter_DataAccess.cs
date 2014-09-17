using BMC.Common.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.ExComms.DataLayer.MSSQL
{
    public partial class ExCommsDataContext
    {
        public bool UpdateFloorFinancialsFromMeter(
                                                        int? installationNo,
                                                        int? rdc_Cancelled_Credits,
                                                        int? rdc_Bill_100000,
                                                        int? Coin_in,
                                                        int? Coin_Out,
                                                        int? Coin_Drop,
                                                        int? jackpot,
                                                        int? games_Bet,
                                                        int? games_Won,
                                                        int? door_Open,
                                                        int? power_Reset,
                                                        int? rdc_Bill_1,
                                                        int? rdc_Bill_5,
                                                        int? rdc_Bill_10,
                                                        int? rdc_Bill_20,
                                                        int? rdc_Bill_50, 
                                                        int? rdc_Bill_100,
                                                        int? rdc_Bill_250,
                                                        int? rdc_Bill_10000,
                                                        int? rdc_Bill_20000,
                                                        int? rdc_Bill_25000,
                                                        int? rdc_Bill_50000,
                                                        int? rdc_Games_Lost,
                                                        int? rdc_Current_Credits,
                                                        int? external_Credit,
                                                        int? rdc_True_Coin_In,
                                                        int? rdc_True_Coin_Out,
                                                        int? handpay,
                                                        int? rdc_Tickets_Inserted_Cashable_Value,
                                                        int? rdc_Tickets_Printed_Cashable_Value,
                                                        int? rdc_Tickets_Inserted_NonCashable_Value,
                                                        int? rdc_Tickets_Printed_NonCashable_Value,
                                                        int? rdc_Tickets_Inserted_Cashable_Qty,
                                                        int? rdc_Tickets_Printed_Cashable_Qty,
                                                        int? rdc_Tickets_Inserted_NonCashable_Qty,
                                                        int? rdc_Tickets_Printed_NonCashable_Qty,
                                                        int? prog_Win,
                                                        int? prog_Win_Handpay,
                                                        int? rdc_Mystery_Machine_Win,
                                                        int? rdc_Mystery_Attendant_Win,
                                                        int? rdc_Promo_Cashable_EFT_In,
                                                        int? rdc_Promo_Cashable_EFT_Out,
                                                        int? rdc_NonCashable_EFT_In,
                                                        int? rdc_NonCashable_EFT_Out,
                                                        int? rdc_Cashable_EFT_In,
                                                        int? rdc_Cashable_EFT_Out,
                                                        int? rdc_Bill_200,
                                                        int? rdc_Bill_500,
                                                        int? Notes,
                                                        int? rdc_Bill_2,
                                                        int? rdc_Games_Since_Power_Up,
                                                        int? vtp,
                                                        string source
                                                  )
        {
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    UpdateFloorFinancialsFromMetersResult obj = DataContext.usp_UpdateFloorFinancialsFromMeters(
                                                                        installationNo,
                                                                        rdc_Cancelled_Credits,
                                                                        rdc_Bill_100000,
                                                                        Coin_in,
                                                                        Coin_Out,
                                                                        Coin_Drop,
                                                                        jackpot,
                                                                        games_Bet,
                                                                        games_Won,
                                                                        door_Open,
                                                                        power_Reset,
                                                                        rdc_Bill_1,
                                                                        rdc_Bill_5,
                                                                        rdc_Bill_10,
                                                                        rdc_Bill_20,
                                                                        rdc_Bill_50,
                                                                        rdc_Bill_100,
                                                                        rdc_Bill_250,
                                                                        rdc_Bill_10000,
                                                                        rdc_Bill_20000,
                                                                        rdc_Bill_25000,
                                                                        rdc_Bill_50000,
                                                                        rdc_Games_Lost,
                                                                        rdc_Current_Credits,
                                                                        external_Credit,
                                                                        rdc_True_Coin_In,
                                                                        rdc_True_Coin_Out,
                                                                        handpay,
                                                                        rdc_Tickets_Inserted_Cashable_Value,
                                                                        rdc_Tickets_Printed_Cashable_Value,
                                                                        rdc_Tickets_Inserted_NonCashable_Value,
                                                                        rdc_Tickets_Printed_NonCashable_Value,
                                                                        rdc_Tickets_Inserted_Cashable_Qty,
                                                                        rdc_Tickets_Printed_Cashable_Qty,
                                                                        rdc_Tickets_Inserted_NonCashable_Qty,
                                                                        rdc_Tickets_Printed_NonCashable_Qty,
                                                                        prog_Win,
                                                                        prog_Win_Handpay,
                                                                        rdc_Mystery_Machine_Win,
                                                                        rdc_Mystery_Attendant_Win,
                                                                        rdc_Promo_Cashable_EFT_In,
                                                                        rdc_Promo_Cashable_EFT_Out,
                                                                        rdc_NonCashable_EFT_In,
                                                                        rdc_NonCashable_EFT_Out,
                                                                        rdc_Cashable_EFT_In,
                                                                        rdc_Cashable_EFT_Out,
                                                                        rdc_Bill_200,
                                                                        rdc_Bill_500,
                                                                        Notes,
                                                                        rdc_Bill_2,
                                                                        rdc_Games_Since_Power_Up,
                                                                        vtp,
                                                                        source
                                                                   ).FirstOrDefault();

                    if (obj.Column1 == 0)
                        return true;

                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        public bool ResetCompVerificationRequestStatus(int installationNo, string type)
		{
            try
            {
                using (ExCommsSQLDataAccess DataContext = this.GetDataContext())
                {
                    DataContext.ResetCompVerificationRequestStatus(installationNo, type);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
		}

    }

    public partial class ExCommsSQLDataAccess : System.Data.Linq.DataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateFloorFinancialsFromMeters")]
        public ISingleResult<UpdateFloorFinancialsFromMetersResult> usp_UpdateFloorFinancialsFromMeters(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_CANCELLED_CREDITS", DbType = "Int")] System.Nullable<int> rDC_CANCELLED_CREDITS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_100000", DbType = "Int")] System.Nullable<int> rDC_BILL_100000,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "COINS_IN", DbType = "Int")] System.Nullable<int> cOINS_IN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "COINS_OUT", DbType = "Int")] System.Nullable<int> cOINS_OUT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "COIN_DROP", DbType = "Int")] System.Nullable<int> cOIN_DROP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "JACKPOT", DbType = "Int")] System.Nullable<int> jACKPOT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GAMES_BET", DbType = "Int")] System.Nullable<int> gAMES_BET,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "GAMES_WON", DbType = "Int")] System.Nullable<int> gAMES_WON,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Door_Open", DbType = "Int")] System.Nullable<int> door_Open,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Power_Reset", DbType = "Int")] System.Nullable<int> power_Reset,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_1", DbType = "Int")] System.Nullable<int> rDC_BILL_1,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_5", DbType = "Int")] System.Nullable<int> rDC_BILL_5,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_10", DbType = "Int")] System.Nullable<int> rDC_BILL_10,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_20", DbType = "Int")] System.Nullable<int> rDC_BILL_20,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_50", DbType = "Int")] System.Nullable<int> rDC_BILL_50,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_100", DbType = "Int")] System.Nullable<int> rDC_BILL_100,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_250", DbType = "Int")] System.Nullable<int> rDC_BILL_250,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_10000", DbType = "Int")] System.Nullable<int> rDC_BILL_10000,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_20000", DbType = "Int")] System.Nullable<int> rDC_BILL_20000,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_25000", DbType = "Int")] System.Nullable<int> rDC_BILL_25000,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_50000", DbType = "Int")] System.Nullable<int> rDC_BILL_50000,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_GAMES_LOST", DbType = "Int")] System.Nullable<int> rDC_GAMES_LOST,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_CURRENT_CREDITS", DbType = "Int")] System.Nullable<int> rDC_CURRENT_CREDITS,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "EXTERNAL_CREDIT", DbType = "Int")] System.Nullable<int> eXTERNAL_CREDIT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TRUE_COIN_IN", DbType = "Int")] System.Nullable<int> rDC_TRUE_COIN_IN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TRUE_COIN_OUT", DbType = "Int")] System.Nullable<int> rDC_TRUE_COIN_OUT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "HANDPAY", DbType = "Int")] System.Nullable<int> hANDPAY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_INSERTED_CASHABLE_VALUE", DbType = "Int")] System.Nullable<int> rDC_TICKETS_INSERTED_CASHABLE_VALUE,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_PRINTED_CASHABLE_VALUE", DbType = "Int")] System.Nullable<int> rDC_TICKETS_PRINTED_CASHABLE_VALUE,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_INSERTED_NONCASHABLE_VALUE", DbType = "Int")] System.Nullable<int> rDC_TICKETS_INSERTED_NONCASHABLE_VALUE,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_PRINTED_NONCASHABLE_VALUE", DbType = "Int")] System.Nullable<int> rDC_TICKETS_PRINTED_NONCASHABLE_VALUE,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_INSERTED_CASHABLE_QTY", DbType = "Int")] System.Nullable<int> rDC_TICKETS_INSERTED_CASHABLE_QTY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_PRINTED_CASHABLE_QTY", DbType = "Int")] System.Nullable<int> rDC_TICKETS_PRINTED_CASHABLE_QTY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_INSERTED_NONCASHABLE_QTY", DbType = "Int")] System.Nullable<int> rDC_TICKETS_INSERTED_NONCASHABLE_QTY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_TICKETS_PRINTED_NONCASHABLE_QTY", DbType = "Int")] System.Nullable<int> rDC_TICKETS_PRINTED_NONCASHABLE_QTY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PROG_WIN", DbType = "Int")] System.Nullable<int> pROG_WIN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PROG_WIN_HANDPAY", DbType = "Int")] System.Nullable<int> pROG_WIN_HANDPAY,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_MYSTERY_MACHINE_WIN", DbType = "Int")] System.Nullable<int> rDC_MYSTERY_MACHINE_WIN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_MYSTERY_ATTENDANT_WIN", DbType = "Int")] System.Nullable<int> rDC_MYSTERY_ATTENDANT_WIN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_Promo_Cashable_EFT_IN", DbType = "Int")] System.Nullable<int> rDC_Promo_Cashable_EFT_IN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_Promo_Cashable_EFT_OUT", DbType = "Int")] System.Nullable<int> rDC_Promo_Cashable_EFT_OUT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_NonCashable_EFT_IN", DbType = "Int")] System.Nullable<int> rDC_NonCashable_EFT_IN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_NonCashable_EFT_OUT", DbType = "Int")] System.Nullable<int> rDC_NonCashable_EFT_OUT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_Cashable_EFT_IN", DbType = "Int")] System.Nullable<int> rDC_Cashable_EFT_IN,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_Cashable_EFT_OUT", DbType = "Int")] System.Nullable<int> rDC_Cashable_EFT_OUT,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_200", DbType = "Int")] System.Nullable<int> rDC_BILL_200,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_500", DbType = "Int")] System.Nullable<int> rDC_BILL_500,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Notes", DbType = "Int")] System.Nullable<int> notes,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_BILL_2", DbType = "Int")] System.Nullable<int> rDC_BILL_2,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "RDC_GAMES_SINCE_POWER_UP", DbType = "Int")] System.Nullable<int> rDC_GAMES_SINCE_POWER_UP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "VTP", DbType = "Int")] System.Nullable<int> vTP,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Source", DbType = "VarChar(10)")] string source)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_No, rDC_CANCELLED_CREDITS, rDC_BILL_100000, cOINS_IN, cOINS_OUT, cOIN_DROP, jACKPOT, gAMES_BET, gAMES_WON, door_Open, power_Reset, rDC_BILL_1, rDC_BILL_5, rDC_BILL_10, rDC_BILL_20, rDC_BILL_50, rDC_BILL_100, rDC_BILL_250, rDC_BILL_10000, rDC_BILL_20000, rDC_BILL_25000, rDC_BILL_50000, rDC_GAMES_LOST, rDC_CURRENT_CREDITS, eXTERNAL_CREDIT, rDC_TRUE_COIN_IN, rDC_TRUE_COIN_OUT, hANDPAY, rDC_TICKETS_INSERTED_CASHABLE_VALUE, rDC_TICKETS_PRINTED_CASHABLE_VALUE, rDC_TICKETS_INSERTED_NONCASHABLE_VALUE, rDC_TICKETS_PRINTED_NONCASHABLE_VALUE, rDC_TICKETS_INSERTED_CASHABLE_QTY, rDC_TICKETS_PRINTED_CASHABLE_QTY, rDC_TICKETS_INSERTED_NONCASHABLE_QTY, rDC_TICKETS_PRINTED_NONCASHABLE_QTY, pROG_WIN, pROG_WIN_HANDPAY, rDC_MYSTERY_MACHINE_WIN, rDC_MYSTERY_ATTENDANT_WIN, rDC_Promo_Cashable_EFT_IN, rDC_Promo_Cashable_EFT_OUT, rDC_NonCashable_EFT_IN, rDC_NonCashable_EFT_OUT, rDC_Cashable_EFT_IN, rDC_Cashable_EFT_OUT, rDC_BILL_200, rDC_BILL_500, notes, rDC_BILL_2, rDC_GAMES_SINCE_POWER_UP, vTP, source);
            return ((ISingleResult<UpdateFloorFinancialsFromMetersResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_ResetCompVerificationRequestStatus")]
        public void ResetCompVerificationRequestStatus([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "InstallationNo", DbType = "Int")] System.Nullable<int> installationNo, [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Type", DbType = "VarChar(50)")] string type)
        {
            this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installationNo, type);
        }
    }

    public partial class UpdateFloorFinancialsFromMetersResult
    {

        private System.Nullable<double> _Column1;

        public UpdateFloorFinancialsFromMetersResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_Column1", DbType = "Float")]
        public System.Nullable<double> Column1
        {
            get
            {
                return this._Column1;
            }
            set
            {
                if ((this._Column1 != value))
                {
                    this._Column1 = value;
                }
            }
        }
    }
}
