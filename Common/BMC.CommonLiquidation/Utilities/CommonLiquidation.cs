using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.CommonLiquidation.Utilities
{
    public class LiquidationUtility
    {
        public ISingleResult<ReadLiquidationEntity> GetReadLiquidationRecords(string sConnectionString, int? iSiteId)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetReadLiquidationRecords(iSiteId);
        }

        public List<CommonLiquidationEntity> GetReadLiquidation(string sConnectionString, int? iSiteId, DateTime minDateTime, DateTime maxDateTime)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            List<CommonLiquidationEntity> lstCommonLiquidationEntity = new List<CommonLiquidationEntity>();

            foreach (CommonReadLiquidation objCommonReadLiquidation in commonLiquidationDataContext.GetReadLiquidation(iSiteId, minDateTime, maxDateTime).ToList())
            {
                lstCommonLiquidationEntity.Add(objCommonReadLiquidation as CommonLiquidationEntity);
            }
            return lstCommonLiquidationEntity;
        }

        public ISingleResult<ReadLiquidationDetails> GetReadLiquidationDetails(string sConnectionString, int? iSiteId, DateTime minDateTime, DateTime maxDateTime)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetReadLiquidationDetails(iSiteId, minDateTime, maxDateTime);
        }

        public decimal CalculateRetailerNegativeNet(string sConnectionString, int? iSiteId, decimal _profitSharePercentage)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            List<CalclateNagativeNet> lstCalclateNagativeNet = new List<CalclateNagativeNet>();
            lstCalclateNagativeNet = commonLiquidationDataContext.Calculate_Read_Negative_Net(iSiteId, _profitSharePercentage).ToList();
            
            if (lstCalclateNagativeNet != null && lstCalclateNagativeNet.Count > 0)
                return lstCalclateNagativeNet[0].Retailer_Negative_Net;

            return 0.0M;
        }

        public int SaveLiquidation(string sConnectionString, int? iSiteId, CommonLiquidationEntity objCommonLiquidationEntity)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.SaveReadLiquidation(objCommonLiquidationEntity.Collection_No,
                                                                    objCommonLiquidationEntity.Read_No,
                                                                    iSiteId,
                                                                    objCommonLiquidationEntity.Liquidation_Date,
                                                                    objCommonLiquidationEntity.ProfitShareGroupId,
                                                                    objCommonLiquidationEntity.ExpenseShareGroupID,
                                                                    objCommonLiquidationEntity.ExpenseShareAmount,
                                                                    objCommonLiquidationEntity.WriteOffAmount,
                                                                    objCommonLiquidationEntity.PayPeriodId,
                                                                    objCommonLiquidationEntity.Gross,
                                                                    objCommonLiquidationEntity.Tickets_Expected,
                                                                    objCommonLiquidationEntity.Net_Percentage,
                                                                    objCommonLiquidationEntity.Retailer_Negative_Net,
                                                                    objCommonLiquidationEntity.Percentage_Setting,
                                                                    objCommonLiquidationEntity.Tickets_Paid,
                                                                    objCommonLiquidationEntity.Advance_To_Retailer,
                                                                    objCommonLiquidationEntity.FixedExpenseAmount,
                                                                    objCommonLiquidationEntity.CarriedForwardExpense,
                                                                    objCommonLiquidationEntity.Retailer_Share,
                                                                    objCommonLiquidationEntity.RetailerShareBeforeFixedExpense,
                                                                    objCommonLiquidationEntity.Balance_Due,
                                                                    objCommonLiquidationEntity.Retailer,
                                                                    objCommonLiquidationEntity.PrevCarriedForwardExpense);
        }

        public ISingleResult<ProfitShareGroup> GetProfitShareGroupList(string sConnectionString)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetProfitShareGroupList();
        }

        public ISingleResult<ExpenseShareGroup> GetExpenseShareGroupList(string sConnectionString)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetExpenseShareGroupList();
        }

        public ISingleResult<PayPeriods> GetPayPeriods(string sConnectionString, int? iSiteId)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetPayPeriods(iSiteId);
        }

        public List<CommonLiquidationEntity> GetLiquidationSummary(string sConnectionString, int Batch_No, int iSite_Id)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            List<CommonLiquidationEntity> lstCommonLiquidationEntity = new List<CommonLiquidationEntity>();
            foreach (CommonCollectionLiquidation objCollectionLiquidationEntity in commonLiquidationDataContext.GetLiquidationSummary(Batch_No, iSite_Id).ToList())
            {
                lstCommonLiquidationEntity.Add(objCollectionLiquidationEntity as CommonLiquidationEntity);
            }
            return lstCommonLiquidationEntity;
        }

        public ISingleResult<ReadLiquidationReportRecords> GetLiquidationDetailForReport(string sConnectionString, int? iSiteId, int iNoOfrecords)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetReadLiquidationReportRecords(iSiteId, iNoOfrecords);
        }
        public ISingleResult<LiquidationDetailForReport> GetLiquidationDetailReportRecords(string sConnectionString, int? iBatchId, int? iReadId)
        {
            CommonLiquidationDataContext commonLiquidationDataContext = new CommonLiquidationDataContext(sConnectionString);
            return commonLiquidationDataContext.GetLiquidationDetailForReport(iBatchId, iReadId);
        }
    }

    public partial class CommonLiquidationDataContext : System.Data.Linq.DataContext
    {
        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        #endregion

        public CommonLiquidationDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        [Function(Name = "dbo.rsp_GetReadliquidationRecords")]
        public ISingleResult<ReadLiquidationEntity> GetReadLiquidationRecords([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id);
            return ((ISingleResult<ReadLiquidationEntity>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Get_LiquidationSummary_ForRead")]
        public ISingleResult<CommonReadLiquidation> GetReadLiquidation([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate, [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, startDate, endDate);
            return ((ISingleResult<CommonReadLiquidation>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetReadLiquidationDetails")]
        public ISingleResult<ReadLiquidationDetails> GetReadLiquidationDetails([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate, [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, startDate, endDate);
            return ((ISingleResult<ReadLiquidationDetails>)(result.ReturnValue));
        }

        [Function(Name="dbo.usp_SaveReadLiquidation")]
		public int SaveReadLiquidation(
					[Parameter(Name="Collection_Batch_No", DbType="Int")] System.Nullable<int> collection_Batch_No, 
					[Parameter(Name="Read_No", DbType="Int")] System.Nullable<int> read_No,
                    [Parameter(Name="Site_Id", DbType = "Int")] System.Nullable<int> site_Id,
					[Parameter(Name="Liquidation_Date", DbType="DateTime")] System.Nullable<System.DateTime> liquidation_Date, 
					[Parameter(Name="ProfitShareGroup_Id", DbType="Int")] System.Nullable<int> profitShareGroup_Id, 
					[Parameter(Name="ExpenseShareGroupId", DbType="Int")] System.Nullable<int> expenseShareGroupId, 
					[Parameter(Name="ExpenseShareAmount", DbType="Decimal")] System.Nullable<decimal> expenseShareAmount, 
					[Parameter(Name="WriteOffAmount", DbType="Decimal")] System.Nullable<decimal> writeOffAmount, 
					[Parameter(Name="PayPeriod_Id", DbType="Int")] System.Nullable<int> payPeriod_Id, 
					[Parameter(Name="MeterIn", DbType="Decimal")] System.Nullable<decimal> meterIn, 
					[Parameter(Name="MeterOut", DbType="Decimal")] System.Nullable<decimal> meterOut, 
					[Parameter(Name="RetailerShareBeforeAdjustment", DbType="Decimal")] System.Nullable<decimal> retailerShareBeforeAdjustment, 
					[Parameter(Name="RetailerNegativeNet", DbType="Decimal")] System.Nullable<decimal> retailerNegativeNet,
                    [Parameter(Name="RetailerSharePercentage", DbType = "Decimal")] System.Nullable<decimal> retailerSharePercentage,
					[Parameter(Name="TicketPaid", DbType="Decimal")] System.Nullable<decimal> ticketPaid, 
					[Parameter(Name="AdvanceToRetailer", DbType="Decimal")] System.Nullable<decimal> advanceToRetailer, 
					[Parameter(Name="FixedExpenseAmount", DbType="Decimal")] System.Nullable<decimal> fixedExpenseAmount, 
					[Parameter(Name="CarriedForwardExpense", DbType="Decimal")] System.Nullable<decimal> carriedForwardExpense, 
                    [Parameter(Name="Retailer_Share", DbType = "Decimal")] System.Nullable<decimal> retailer_Share,
                    [Parameter(Name="RetailerShareBeforeFixedExpense", DbType = "Decimal")] System.Nullable<decimal> retailerShareBeforeFixedExpense,
                    [Parameter(Name="BalanceDue", DbType="Decimal")] System.Nullable<decimal> balanceDue,
                    [Parameter(Name="Retailer", DbType="Decimal")] System.Nullable<decimal> retailer,
                    [Parameter(Name="PrevCarriedForwardExpense", DbType="Decimal")] System.Nullable<decimal> prevCarriedForwardExpense
            )
		{
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_Batch_No, read_No, site_Id, liquidation_Date, profitShareGroup_Id, expenseShareGroupId, expenseShareAmount, writeOffAmount, payPeriod_Id, meterIn, meterOut, retailerShareBeforeAdjustment, retailerNegativeNet, retailerSharePercentage, ticketPaid, advanceToRetailer, fixedExpenseAmount, carriedForwardExpense, retailer_Share, retailerShareBeforeFixedExpense, balanceDue, retailer, prevCarriedForwardExpense);
			return ((int)(result.ReturnValue));
		}

        [Function(Name = "dbo.esp_Calculate_Read_Negative_Net")]
        public ISingleResult<CalclateNagativeNet> Calculate_Read_Negative_Net([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "Retailer_Percentage", DbType = "Decimal")] System.Nullable<decimal> retailer_Percentage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, retailer_Percentage);
            return ((ISingleResult<CalclateNagativeNet>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetProfitShareGroupList")]
        public ISingleResult<ProfitShareGroup> GetProfitShareGroupList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ProfitShareGroup>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetExpenseShareGroupList")]
        public ISingleResult<ExpenseShareGroup> GetExpenseShareGroupList()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ExpenseShareGroup>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPayPeriods")]
        public ISingleResult<PayPeriods> GetPayPeriods([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id);
            return ((ISingleResult<PayPeriods>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetLiquidationSummary_ProfitShare")]
        public ISingleResult<CommonCollectionLiquidation> GetLiquidationSummary([Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, [Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batch_No, site_Id);
            return ((ISingleResult<CommonCollectionLiquidation>)(result.ReturnValue));
        }

        //[Function(Name = "dbo.usp_PerformLiquidationForProfitShare")]
        //public int PerformLiquidationForProfitShare([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, [Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "ExpenseShareAmount", DbType = "Decimal")] System.Nullable<decimal> expenseShareAmount, [Parameter(Name = "WriteOffAmount", DbType = "Decimal")] System.Nullable<decimal> writeOffAmount, [Parameter(Name = "PayPeriodId", DbType = "Int")] System.Nullable<int> payPeriodId, [Parameter(Name = "CarriedForwardExpense", DbType = "Decimal")] System.Nullable<decimal> carriedForwardExpense, [Parameter(Name = "RetailerExpenseShareAmount", DbType = "Decimal")] System.Nullable<decimal> retailerExpenseShareAmount)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, batch_No, profitShareGroupId, expenseShareGroupId, expenseShareAmount, writeOffAmount, payPeriodId, carriedForwardExpense);
        //    return ((int)(result.ReturnValue));
        //}
        [Function(Name = "dbo.usp_PerformLiquidationForProfitShare")]
        public int PerformLiquidationForProfitShare(
                                                    [Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, 
                                                    [Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, 
                                                    [Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, 
                                                    [Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, 
                                                    [Parameter(Name = "ExpenseShareAmount", DbType = "Decimal")] System.Nullable<decimal> expenseShareAmount, 
                                                    [Parameter(Name = "WriteOffAmount", DbType = "Decimal")] System.Nullable<decimal> writeOffAmount, 
                                                    [Parameter(Name = "PayPeriodId", DbType = "Int")] System.Nullable<int> payPeriodId,
                                                    [Parameter(Name = "MeterIn", DbType = "Decimal")] System.Nullable<decimal> meterIn,
                                                    [Parameter(Name = "MeterOut", DbType = "Decimal")] System.Nullable<decimal> meterOut,
                                                    [Parameter(Name = "RetailerShareBeforeAdjustment", DbType = "Decimal")] System.Nullable<decimal> retailerShareBeforeAdjustment,
                                                    [Parameter(Name = "RetailerNegativeNet", DbType = "Decimal")] System.Nullable<decimal> retailerNegativeNet,
                                                    [Parameter(Name = "RetailerSharePercentage", DbType = "Decimal")] System.Nullable<decimal> retailerSharePercentage,
                                                    [Parameter(Name = "TicketPaid", DbType = "Decimal")] System.Nullable<decimal> ticketPaid,
                                                    [Parameter(Name = "AdvanceToRetailer", DbType = "Decimal")] System.Nullable<decimal> advanceToRetailer,
                                                    [Parameter(Name = "Retailer_Share", DbType = "Decimal")] System.Nullable<decimal> retailer_Share,
                                                    [Parameter(Name = "BalanceDue", DbType = "Decimal")] System.Nullable<decimal> balanceDue,
                                                    [Parameter(Name = "Retailer", DbType = "Decimal")] System.Nullable<decimal> retailer,
                                                    [Parameter(Name = "RetailerShareBeforeFixedExpense", DbType = "Decimal")] System.Nullable<decimal> retailerShareBeforeFixedExpense,
                                                    [Parameter(Name = "CarriedForwardExpense", DbType = "Decimal")] System.Nullable<decimal> carriedForwardExpense, 
                                                    [Parameter(Name = "RetailerExpenseShareAmount", DbType = "Decimal")] System.Nullable<decimal> retailerExpenseShareAmount, 
                                                    [Parameter(Name = "FixedExpenseAmount", DbType = "Decimal")] System.Nullable<decimal> fixedExpenseAmount, 
                                                    [Parameter(Name = "PrevCarriedForwardExpense", DbType = "Decimal")] System.Nullable<decimal> prevCarriedForwardExpense)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, batch_No, profitShareGroupId, expenseShareGroupId, expenseShareAmount, writeOffAmount, payPeriodId, meterIn, meterOut, retailerShareBeforeAdjustment, retailerNegativeNet, retailerSharePercentage, ticketPaid, advanceToRetailer, retailer_Share, balanceDue, retailer, retailerShareBeforeFixedExpense, carriedForwardExpense, retailerExpenseShareAmount, fixedExpenseAmount, prevCarriedForwardExpense);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteSetting")]
        public int rsp_GetSiteSetting([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "SettingMaster_Name", DbType = "VarChar(100)")] string settingMaster_Name, [Parameter(Name = "Setting_Value", DbType = "VarChar(500)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, settingMaster_Name, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSetting")]
        public int rsp_GetSetting([Parameter(Name = "Setting_ID", DbType = "Int")] System.Nullable<int> setting_ID, [Parameter(Name = "Setting_Name", DbType = "VarChar(100)")] string setting_Name, [Parameter(Name = "Setting_Default", DbType = "VarChar(100)")] string setting_Default, [Parameter(Name = "Setting_Value", DbType = "VarChar(100)")] ref string setting_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), setting_ID, setting_Name, setting_Default, setting_Value);
            setting_Value = ((string)(result.GetParameterValue(3)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetReadLiquidationReportRecords")]
        public ISingleResult<ReadLiquidationReportRecords>GetReadLiquidationReportRecords([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "NumberOfRecords", DbType = "Int")] System.Nullable<int> numberOfRecords)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, numberOfRecords);
            return ((ISingleResult<ReadLiquidationReportRecords>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_Report_GetLiquidationDetail")]
        public ISingleResult<LiquidationDetailForReport> GetLiquidationDetailForReport([Parameter(Name = "BatchId", DbType = "Int")] System.Nullable<int> batchId, [Parameter(Name = "ReadId", DbType = "Int")] System.Nullable<int> readId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchId, readId);
            return ((ISingleResult<LiquidationDetailForReport>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CheckLiquidationPerformed")]
        public int CheckLiquidationPerformed([Parameter(Name = "BatchID", DbType = "Int")] System.Nullable<int> batchID, [Parameter(Name = "IsLiquidationPerformed", DbType = "Int")] ref System.Nullable<int> isLiquidationPerformed)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchID, isLiquidationPerformed);
            isLiquidationPerformed = ((System.Nullable<int>)(result.GetParameterValue(1)));
            return ((int)(result.GetParameterValue(1)));
        }
        //[Function(Name = "dbo.rsp_GetOperatorExpenseSharePercentage")]
        //public decimal GetOperatorExpenseSharePercentage([Parameter(Name = "ExpenseShareGroupID", DbType = "Int")] System.Nullable<int> expenseShareGroupID, [Parameter(Name = "OperatorExpSharePercentage", DbType = "Decimal")] ref System.Nullable<decimal> operatorExpSharePercentage)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupID, operatorExpSharePercentage);
        //    operatorExpSharePercentage = ((System.Nullable<decimal>)(result.GetParameterValue(1)));
        //    return ((decimal)(result.GetParameterValue(1)));
        //}
        [Function(Name = "dbo.rsp_GetOperatorExpenseSharePercentage")]
        public ISingleResult<rsp_GetOperatorExpenseSharePercentageResult> GetOperatorExpenseSharePercentage([Parameter(Name = "ExpenseShareGroupID", DbType = "Int")] System.Nullable<int> expenseShareGroupID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupID);
            return ((ISingleResult<rsp_GetOperatorExpenseSharePercentageResult>)(result.ReturnValue));
        }

        //[Function(Name = "dbo.rsp_GetFixedExpenseAmount")]
        //public int GetFixedExpenseAmount([Parameter(Name = "ExpenseShareGroupID", DbType = "Int")] System.Nullable<int> expenseShareGroupID, [Parameter(Name = "ExpenseShareAmount", DbType = "Decimal(18,2)")] System.Nullable<decimal> expenseShareAmount, [Parameter(Name = "FixedExpenseAmount", DbType = "Decimal(18,2)")] ref System.Nullable<decimal> fixedExpenseAmount)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), expenseShareGroupID, expenseShareAmount, fixedExpenseAmount);
        //    fixedExpenseAmount = ((System.Nullable<decimal>)(result.GetParameterValue(2)));
        //    return ((int)(result.ReturnValue));
        //}
    }

    public class ReadLiquidationEntity
    {

        private int _Read_No;

        private System.Nullable<System.DateTime> _Read_Date;

        private System.Nullable<decimal> _CashIn;

        private System.Nullable<decimal> _CashOut;

        private System.Nullable<decimal> _CashTake;

        private System.Nullable<decimal> _Total_Coins_In;

        private System.Nullable<decimal> _Handpay;

        private System.Nullable<decimal> _Tickets_In;

        private System.Nullable<decimal> _Tickets_Out;

        public ReadLiquidationEntity()
        {
        }

        [Column(Storage = "_Read_No", DbType = "Int NOT NULL")]
        public int Read_No
        {
            get
            {
                return this._Read_No;
            }
            set
            {
                if ((this._Read_No != value))
                {
                    this._Read_No = value;
                }
            }
        }

        [Column(Storage = "_Read_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Read_Date
        {
            get
            {
                return this._Read_Date;
            }
            set
            {
                if ((this._Read_Date != value))
                {
                    this._Read_Date = value;
                }
            }
        }

        [Column(Storage = "_CashIn", DbType = "Decimal")]
        public System.Nullable<decimal> CashIn
        {
            get
            {
                return this._CashIn.GetValueOrDefault();
            }
            set
            {
                if ((this._CashIn != value))
                {
                    this._CashIn = value;
                }
            }
        }

        [Column(Storage = "_CashOut", DbType = "Decimal")]
        public System.Nullable<decimal> CashOut
        {
            get
            {
                return this._CashOut.GetValueOrDefault();
            }
            set
            {
                if ((this._CashOut != value))
                {
                    this._CashOut = value;
                }
            }
        }

        [Column(Storage = "_CashTake", DbType = "Decimal")]
        public System.Nullable<decimal> CashTake
        {
            get
            {
                return this._CashTake.GetValueOrDefault();
            }
            set
            {
                if ((this._CashTake != value))
                {
                    this._CashTake = value;
                }
            }
        }

        [Column(Storage = "_Total_Coins_In", DbType = "Decimal")]
        public System.Nullable<decimal> Total_Coins_In
        {
            get
            {
                return this._Total_Coins_In.GetValueOrDefault();
            }
            set
            {
                if ((this._Total_Coins_In != value))
                {
                    this._Total_Coins_In = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Decimal")]
        public System.Nullable<decimal> Handpay
        {
            get
            {
                return this._Handpay.GetValueOrDefault();
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [Column(Storage = "_Tickets_In", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_In
        {
            get
            {
                return this._Tickets_In.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_In != value))
                {
                    this._Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Out", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_Out
        {
            get
            {
                return this._Tickets_Out.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Out != value))
                {
                    this._Tickets_Out = value;
                }
            }
        }
    }

    public class ReadLiquidationDetails
    {

        private int _Read_No;

        private string _Bar_Pos_Name;

        private System.Nullable<System.DateTime> _Read_Date;

        private System.Nullable<decimal> _CashIn;

        private System.Nullable<decimal> _CashOut;

        private System.Nullable<decimal> _CashTake;

        private System.Nullable<decimal> _Total_Coins_In;

        private System.Nullable<decimal> _Handpay;

        private System.Nullable<decimal> _Tickets_In;

        private System.Nullable<decimal> _Tickets_Out;

        public ReadLiquidationDetails()
        {
        }

        [Column(Storage = "_Read_No", DbType = "Int NOT NULL")]
        public int Read_No
        {
            get
            {
                return this._Read_No;
            }
            set
            {
                if ((this._Read_No != value))
                {
                    this._Read_No = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [Column(Storage = "_Read_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Read_Date
        {
            get
            {
                return this._Read_Date;
            }
            set
            {
                if ((this._Read_Date != value))
                {
                    this._Read_Date = value;
                }
            }
        }

        [Column(Storage = "_CashIn", DbType = "Decimal")]
        public System.Nullable<decimal> CashIn
        {
            get
            {
                return this._CashIn.GetValueOrDefault();
            }
            set
            {
                if ((this._CashIn != value))
                {
                    this._CashIn = value;
                }
            }
        }

        [Column(Storage = "_CashOut", DbType = "Decimal")]
        public System.Nullable<decimal> CashOut
        {
            get
            {
                return this._CashOut.GetValueOrDefault();
            }
            set
            {
                if ((this._CashOut != value))
                {
                    this._CashOut = value;
                }
            }
        }

        [Column(Storage = "_CashTake", DbType = "Decimal")]
        public System.Nullable<decimal> CashTake
        {
            get
            {
                return this._CashTake.GetValueOrDefault();
            }
            set
            {
                if ((this._CashTake != value))
                {
                    this._CashTake = value;
                }
            }
        }

        [Column(Storage = "_Total_Coins_In", DbType = "Decimal")]
        public System.Nullable<decimal> Total_Coins_In
        {
            get
            {
                return this._Total_Coins_In.GetValueOrDefault();
            }
            set
            {
                if ((this._Total_Coins_In != value))
                {
                    this._Total_Coins_In = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Decimal")]
        public System.Nullable<decimal> Handpay
        {
            get
            {
                return this._Handpay.GetValueOrDefault();
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [Column(Storage = "_Tickets_In", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_In
        {
            get
            {
                return this._Tickets_In.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_In != value))
                {
                    this._Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Out", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_Out
        {
            get
            {
                return this._Tickets_Out.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Out != value))
                {
                    this._Tickets_Out = value;
                }
            }
        }
    }

    public abstract class CommonLiquidationEntity
    {

        public CommonLiquidationEntity()
        {
        }

        public virtual System.Nullable<int> Collection_No { get; set; }
        public virtual System.Nullable<int> Read_No { get; set; }
        public virtual string Retailer_Name { get; set; }
        public virtual System.Nullable<System.DateTime> Liquidation_Date { get; set; }
        public virtual System.Nullable<decimal> Gross { get; set; }
        public virtual System.Nullable<decimal> Tickets_Expected { get; set; }
        public virtual System.Nullable<decimal> Net { get; set; }
        public virtual System.Nullable<decimal> Net_Percentage { get; set; }
        public virtual System.Nullable<decimal> Percentage_Setting { get; set; }
        public virtual System.Nullable<decimal> Retailer { get; set; }
        public virtual System.Nullable<decimal> Retailer_Negative_Net { get; set; }
        public virtual System.Nullable<decimal> Retailer_Share { get; set; }
        public virtual System.Nullable<decimal> Tickets_Paid { get; set; }
        public virtual System.Nullable<decimal> Advance_To_Retailer { get; set; }
        public virtual System.Nullable<decimal> Balance_Due { get; set; }
        public virtual System.Nullable<decimal> RetailerShareBeforeFixedExpense { get; set; }
        public virtual int ProfitShareGroupId { get; set; }
        public virtual int ExpenseShareGroupID { get; set; }
        public virtual decimal ExpenseSharePercentage { get; set; }
        public virtual decimal ExpenseShareAmount { get; set; }
        public virtual decimal WriteOffAmount { get; set; }
        public virtual int PayPeriodId { get; set; }
        public virtual System.Nullable<decimal> FixedExpenseAmount { get; set; }
        public virtual System.Nullable<decimal> PrevCarriedForwardExpense { get; set; }
        public virtual System.Nullable<decimal> Negative_Net { get; set; }
        public virtual System.Nullable<decimal> RetailerExpenseShareAmount { get; set; }
        public virtual System.Nullable<decimal> RetailerNetRevenue { get; set; }
        public virtual decimal CarriedForwardExpense { get; set; }
    }

    public class CommonReadLiquidation : CommonLiquidationEntity, INotifyPropertyChanged
    {
        private string _Retailer_Name;

        private System.Nullable<int> _Collection_No { get; set; }
        
        private System.Nullable<int> _Read_No { get; set; }

        private System.Nullable<System.DateTime> _Liquidation_Date;

        private System.Nullable<decimal> _Gross;

        private System.Nullable<decimal> _Tickets_Expected;

        private System.Nullable<decimal> _Net;

        private System.Nullable<decimal> _Net_Percentage;

        private System.Nullable<decimal> _Percentage_Setting;

        private System.Nullable<decimal> _Retailer;

        private System.Nullable<decimal> _Retailer_Negative_Net;

        private System.Nullable<decimal> _Retailer_Share;

        private System.Nullable<decimal> _Tickets_Paid;

        private System.Nullable<decimal> _Advance_To_Retailer;

        private System.Nullable<decimal> _RetailerShareBeforeFixedExpense;

        private System.Nullable<decimal> _Balance_Due;

        private int _ProfitShareGroupId;

        private int _ExpenseShareGroupID;

        private decimal _ExpenseSharePercentage;

        private decimal _ExpenseShareAmount;

        private decimal _WriteOffAmount;

        private int _PayPeriodId;

        private System.Nullable<decimal> _FixedExpenseAmount;

        private System.Nullable<decimal> _PrevCarriedForwardExpense;

        private System.Nullable<decimal> _Negative_Net;

        private System.Nullable<decimal> _RetailerExpenseShareAmount;

        private System.Nullable<decimal> _RetailerNetRevenue;

        private System.Nullable<decimal> _ExpenseShareAmountOfOperator;

        private decimal _CarriedForwardExpense;

        public CommonReadLiquidation()
        {
        }

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public override System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_Read_No", DbType = "Int")]
        public override System.Nullable<int> Read_No
        {
            get
            {
                return this._Read_No;
            }
            set
            {
                if ((this._Read_No != value))
                {
                    this._Read_No = value;
                }
            }
        }

        [Column(Storage = "_Retailer_Name", DbType = "VarChar(50)")]
        public override string Retailer_Name
        {
            get
            {
                return this._Retailer_Name;
            }
            set
            {
                if ((this._Retailer_Name != value))
                {
                    this._Retailer_Name = value;
                }
            }
        }

        [Column(Storage = "_Liquidation_Date", DbType = "DateTime")]
        public override System.Nullable<System.DateTime> Liquidation_Date
        {
            get
            {
                return this._Liquidation_Date;
            }
            set
            {
                if ((this._Liquidation_Date != value))
                {
                    this._Liquidation_Date = value;
                }
            }
        }

        [Column(Storage = "_Gross", DbType = "Decimal")]
        public override System.Nullable<decimal> Gross
        {
            get
            {
                return this._Gross.GetValueOrDefault();
            }
            set
            {
                if ((this._Gross != value))
                {
                    this._Gross = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Expected", DbType = "Decimal")]
        public override System.Nullable<decimal> Tickets_Expected
        {
            get
            {
                return this._Tickets_Expected.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Expected != value))
                {
                    this._Tickets_Expected = value;
                }
            }
        }

        [Column(Storage = "_Net", DbType = "Decimal")]
        public override System.Nullable<decimal> Net
        {
            get
            {
                return this._Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Net != value))
                {
                    this._Net = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Net"));
                }
            }
        }

        [Column(Storage = "_Net_Percentage", DbType = "Decimal")]
        public override System.Nullable<decimal> Net_Percentage
        {
            get
            {
                _Net_Percentage = Net * Percentage_Setting.GetValueOrDefault();
                return _Net_Percentage.GetValueOrDefault();
            }
            set
            {
                if ((this._Net_Percentage != value))
                {
                    this._Net_Percentage = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Net_Percentage"));
                }
            }
        }

        [Column(Storage = "_Percentage_Setting", DbType = "Decimal")]
        public override System.Nullable<decimal> Percentage_Setting
        {
            get
            {
                return this._Percentage_Setting.GetValueOrDefault() / 100;
            }
            set
            {
                if ((this._Percentage_Setting != value))
                {
                    this._Percentage_Setting = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Percentage_Setting"));
                }
            }
        }

        [Column(Storage = "_Retailer", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer
        {
            get
            {
                _Retailer = (RetailerShareBeforeFixedExpense - CarriedForwardExpense);
                return _Retailer;
            }
            set
            {
                if ((this._Retailer != value))
                {
                    this._Retailer = value;
                }
            }
        }

        [Column(Storage = "_Retailer_Negative_Net", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer_Negative_Net
        {
            get
            {
                return this._Retailer_Negative_Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Retailer_Negative_Net != value))
                {
                    this._Retailer_Negative_Net = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Retailer_Negative_Net"));
                }
            }
        }

        [Column(Storage = "_Retailer_Share", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer_Share
        {
            get
            {
                _Retailer_Share = Net_Percentage + Retailer_Negative_Net;
                return _Retailer_Share.GetValueOrDefault();
            }
            set
            {
                if ((this._Retailer_Share != value))
                {
                    this._Retailer_Share = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Retailer_Share"));
                }
            }
        }

        [Column(Storage = "_Tickets_Paid", DbType = "Decimal")]
        public override System.Nullable<decimal> Tickets_Paid
        {
            get
            {
                return this._Tickets_Paid.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Paid != value))
                {
                    this._Tickets_Paid = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Tickets_Paid"));
                }
            }
        }

        [Column(Storage = "_Advance_To_Retailer", DbType = "Decimal")]
        public override System.Nullable<decimal> Advance_To_Retailer
        {
            get
            {
                return this._Advance_To_Retailer.GetValueOrDefault();
            }
            set
            {
                if ((this._Advance_To_Retailer != value))
                {
                    this._Advance_To_Retailer = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Advance_To_Retailer"));
                }
            }
        }

        [Column(Storage = "_Balance_Due", DbType = "Decimal")]
        public override System.Nullable<decimal> Balance_Due
        {
            get
            {
                if (Retailer_Share > 0)
                    _Balance_Due = ((Net - Net_Percentage) + (Advance_To_Retailer - Retailer_Negative_Net) - (ExpenseShareAmountOfOperator));
                else
                    _Balance_Due = ((Net + Advance_To_Retailer) - (ExpenseShareAmountOfOperator));

                return _Balance_Due.GetValueOrDefault();
            }
            set
            {
                if ((this._Balance_Due != value))
                {
                    this._Balance_Due = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Balance_Due"));
                }
            }
        }

        [Column(Storage = "_RetailerShareBeforeFixedExpense", DbType = "Decimal")]
        public override System.Nullable<decimal> RetailerShareBeforeFixedExpense
        {
            get
            {
                if (Retailer_Share > 0)
                    _RetailerShareBeforeFixedExpense = (Retailer_Share - Balance_Due);
                else
                    _RetailerShareBeforeFixedExpense = 0 - (Balance_Due);

                return _RetailerShareBeforeFixedExpense.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerShareBeforeFixedExpense != value))
                {
                    this._RetailerShareBeforeFixedExpense = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RetailerShareBeforeFixedExpense"));
                }
            }
        }

        [Column(Storage = "_ProfitShareGroupId", DbType = "Int NOT NULL")]
        public override int ProfitShareGroupId
        {
            get
            {
                return this._ProfitShareGroupId;
            }
            set
            {
                if ((this._ProfitShareGroupId != value))
                {
                    this._ProfitShareGroupId = value;
                }
            }
        }

        [Column(Storage = "_ExpenseShareGroupID", DbType = "Int NOT NULL")]
        public override int ExpenseShareGroupID
        {
            get
            {
                return this._ExpenseShareGroupID;
            }
            set
            {
                if ((this._ExpenseShareGroupID != value))
                {
                    this._ExpenseShareGroupID = value;
                }
            }
        }

        [Column(Storage = "_ExpenseSharePercentage", DbType = "Decimal NOT NULL")]
        public override decimal ExpenseSharePercentage
        {
            get
            {
                return this._ExpenseSharePercentage / 100;
            }
            set
            {
                if ((this._ExpenseSharePercentage != value))
                {
                    this._ExpenseSharePercentage = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseSharePercentage"));
                }
            }
        }

        [Column(Storage = "_ExpenseShareAmount", DbType = "Decimal NOT NULL")]
        public override decimal ExpenseShareAmount
        {
            get
            {
                return this._ExpenseShareAmount;
            }
            set
            {
                if ((this._ExpenseShareAmount != value))
                {
                    this._ExpenseShareAmount = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseShareAmount"));
                }
            }
        }

        [Column(Storage = "_WriteOffAmount", DbType = "Decimal NOT NULL")]
        public override decimal WriteOffAmount
        {
            get
            {
                return this._WriteOffAmount;
            }
            set
            {
                if ((this._WriteOffAmount != value))
                {
                    this._WriteOffAmount = value;
                }
            }
        }

        [Column(Storage = "_PayPeriodId", DbType = "Int NOT NULL")]
        public override int PayPeriodId
        {
            get
            {
                return this._PayPeriodId;
            }
            set
            {
                if ((this._PayPeriodId != value))
                {
                    this._PayPeriodId = value;
                }
            }
        }

        [Column(Storage = "_FixedExpenseAmount", DbType = "DECIMAL")]
        public override System.Nullable<decimal> FixedExpenseAmount
        {
            get
            {
                _FixedExpenseAmount = (ExpenseShareAmount * ExpenseSharePercentage);
                return _FixedExpenseAmount.GetValueOrDefault();
            }
            set
            {
                if ((this._FixedExpenseAmount != value))
                {
                    this._FixedExpenseAmount = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FixedExpenseAmount"));
                }
            }
        }

        [Column(Storage = "_PrevCarriedForwardExpense", DbType = "DECIMAL")]
        public override System.Nullable<decimal> PrevCarriedForwardExpense
        {
            get
            {
                return this._PrevCarriedForwardExpense.GetValueOrDefault();
            }
            set
            {
                if ((this._PrevCarriedForwardExpense != value))
                {
                    this._PrevCarriedForwardExpense = value;
                }
            }
        }

        [Column(Storage = "_Negative_Net", DbType = "DECIMAL")]
        public override System.Nullable<decimal> Negative_Net
        {
            get
            {
                return this._Negative_Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Negative_Net != value))
                {
                    this._Negative_Net = value;
                }
            }
        }

        [Column(Storage = "_RetailerExpenseShareAmount", DbType = "DECIMAL")]
        public override System.Nullable<decimal> RetailerExpenseShareAmount
        {
            get
            {
                return this._RetailerExpenseShareAmount.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerExpenseShareAmount != value))
                {
                    this._RetailerExpenseShareAmount = value;
                }
            }
        }
        [Column(Storage = "_RetailerNetRevenue", DbType = "DECIMAL")]
        public override System.Nullable<decimal> RetailerNetRevenue
        {
            get
            {
                return this._RetailerNetRevenue.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerNetRevenue != value))
                {
                    this._RetailerNetRevenue = value;
                }
            }
        }

        [Column(Storage = "_ExpenseShareAmountOfOperator", DbType = "DECIMAL")]
        public System.Nullable<decimal> ExpenseShareAmountOfOperator
        {
            get
            {
                return (this._ExpenseShareAmount - this.FixedExpenseAmount.GetValueOrDefault());
            }
            set
            {
                if ((this._ExpenseShareAmountOfOperator != value))
                {
                    this._ExpenseShareAmountOfOperator = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseShareAmountOfOperator"));
                }
            }
        }

        [Column(Storage = "_CarriedForwardExpense", DbType = "DECIMAL")]
        public override decimal CarriedForwardExpense
        {
            get
            {
                return (FixedExpenseAmount + PrevCarriedForwardExpense - WriteOffAmount).GetValueOrDefault();
            }
            set
            {
                if ((this._CarriedForwardExpense != value))
                {
                    this._CarriedForwardExpense = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CarriedForwardExpense"));
                }
            }
        }
        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class CalclateNagativeNet
    {

        private decimal _Retailer_Negative_Net;

        [Column(Storage = "_Retailer_Negative_Net", DbType = "DECIMAL NOT NULL")]
        public decimal Retailer_Negative_Net
        {
            get
            {
                return this._Retailer_Negative_Net;
            }
            set
            {
                if ((this._Retailer_Negative_Net != value))
                {
                    this._Retailer_Negative_Net = value;
                }
            }
        }
    }

    public partial class ProfitShareGroup
    {

        private int _ProfitShareGroupID;

        private string _ProfitShareGroupName;

        private double _ProfitSharePercentage;

        public ProfitShareGroup()
        {
        }

        [Column(Storage = "_ProfitShareGroupID", DbType = "Int NOT NULL")]
        public int ProfitShareGroupID
        {
            get
            {
                return this._ProfitShareGroupID;
            }
            set
            {
                if ((this._ProfitShareGroupID != value))
                {
                    this._ProfitShareGroupID = value;
                }
            }
        }

        [Column(Storage = "_ProfitShareGroupName", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string ProfitShareGroupName
        {
            get
            {
                return this._ProfitShareGroupName;
            }
            set
            {
                if ((this._ProfitShareGroupName != value))
                {
                    this._ProfitShareGroupName = value;
                }
            }
        }

        [Column(Storage = "_ProfitSharePercentage", DbType = "Float NOT NULL")]
        public double ProfitSharePercentage
        {
            get
            {
                return this._ProfitSharePercentage;
            }
            set
            {
                if ((this._ProfitSharePercentage != value))
                {
                    this._ProfitSharePercentage = value;
                }
            }
        }
    }

    public partial class ExpenseShareGroup
    {

        private int _ExpenseShareGroupID;

        private string _ExpenseShareGroupName;

        private double _ExpenseSharePercentage;

        public ExpenseShareGroup()
        {
        }

        [Column(Storage = "_ExpenseShareGroupID", DbType = "Int NOT NULL")]
        public int ExpenseShareGroupID
        {
            get
            {
                return this._ExpenseShareGroupID;
            }
            set
            {
                if ((this._ExpenseShareGroupID != value))
                {
                    this._ExpenseShareGroupID = value;
                }
            }
        }

        [Column(Storage = "_ExpenseShareGroupName", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
        public string ExpenseShareGroupName
        {
            get
            {
                return this._ExpenseShareGroupName;
            }
            set
            {
                if ((this._ExpenseShareGroupName != value))
                {
                    this._ExpenseShareGroupName = value;
                }
            }
        }

        [Column(Storage = "_ExpenseSharePercentage", DbType = "Float NOT NULL")]
        public double ExpenseSharePercentage
        {
            get
            {
                return this._ExpenseSharePercentage;
            }
            set
            {
                if ((this._ExpenseSharePercentage != value))
                {
                    this._ExpenseSharePercentage = value;
                }
            }
        }
    }

    public partial class PayPeriods
    {

        private int _Calendar_Period_ID;

        private System.Nullable<System.DateTime> _Calendar_Period_Start_Date;

        private System.Nullable<System.DateTime> _Calendar_Period_End_Date;

        private string _Calendar_Period;

        public PayPeriods()
        {
        }

        [Column(Storage = "_Calendar_Period_ID", DbType = "Int NOT NULL")]
        public int Calendar_Period_ID
        {
            get
            {
                return this._Calendar_Period_ID;
            }
            set
            {
                if ((this._Calendar_Period_ID != value))
                {
                    this._Calendar_Period_ID = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Period_Start_Date
        {
            get
            {
                return this._Calendar_Period_Start_Date;
            }
            set
            {
                if ((this._Calendar_Period_Start_Date != value))
                {
                    this._Calendar_Period_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period_End_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Calendar_Period_End_Date
        {
            get
            {
                return this._Calendar_Period_End_Date;
            }
            set
            {
                if ((this._Calendar_Period_End_Date != value))
                {
                    this._Calendar_Period_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Calendar_Period", DbType = "VarChar(153)")]
        public string Calendar_Period
        {
            get
            {
                return this._Calendar_Period;
            }
            set
            {
                if ((this._Calendar_Period != value))
                {
                    this._Calendar_Period = value;
                }
            }
        }
    }

    public class CollectionLiquidationEntity
    {

        private int _Read_No;

        private string _Bar_Pos_Name;

        private System.Nullable<System.DateTime> _Read_Date;

        private System.Nullable<decimal> _CashIn;

        private System.Nullable<decimal> _CashOut;

        private System.Nullable<decimal> _CashTake;

        private System.Nullable<decimal> _Total_Coins_In;

        private System.Nullable<decimal> _Handpay;

        private System.Nullable<decimal> _Tickets_In;

        private System.Nullable<decimal> _Tickets_Out;

        public CollectionLiquidationEntity()
        {
        }

        [Column(Storage = "_Read_No", DbType = "Int NOT NULL")]
        public int Read_No
        {
            get
            {
                return this._Read_No;
            }
            set
            {
                if ((this._Read_No != value))
                {
                    this._Read_No = value;
                }
            }
        }

        [Column(Storage = "_Bar_Pos_Name", DbType = "VarChar(50)")]
        public string Bar_Pos_Name
        {
            get
            {
                return this._Bar_Pos_Name;
            }
            set
            {
                if ((this._Bar_Pos_Name != value))
                {
                    this._Bar_Pos_Name = value;
                }
            }
        }

        [Column(Storage = "_Read_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Read_Date
        {
            get
            {
                return this._Read_Date;
            }
            set
            {
                if ((this._Read_Date != value))
                {
                    this._Read_Date = value;
                }
            }
        }

        [Column(Storage = "_CashIn", DbType = "Decimal")]
        public System.Nullable<decimal> CashIn
        {
            get
            {
                return this._CashIn;
            }
            set
            {
                if ((this._CashIn != value))
                {
                    this._CashIn = value;
                }
            }
        }

        [Column(Storage = "_CashOut", DbType = "Decimal")]
        public System.Nullable<decimal> CashOut
        {
            get
            {
                return this._CashOut;
            }
            set
            {
                if ((this._CashOut != value))
                {
                    this._CashOut = value;
                }
            }
        }

        [Column(Storage = "_CashTake", DbType = "Decimal")]
        public System.Nullable<decimal> CashTake
        {
            get
            {
                return this._CashTake;
            }
            set
            {
                if ((this._CashTake != value))
                {
                    this._CashTake = value;
                }
            }
        }

        [Column(Storage = "_Total_Coins_In", DbType = "Decimal")]
        public System.Nullable<decimal> Total_Coins_In
        {
            get
            {
                return this._Total_Coins_In;
            }
            set
            {
                if ((this._Total_Coins_In != value))
                {
                    this._Total_Coins_In = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Decimal")]
        public System.Nullable<decimal> Handpay
        {
            get
            {
                return this._Handpay;
            }
            set
            {
                if ((this._Handpay != value))
                {
                    this._Handpay = value;
                }
            }
        }

        [Column(Storage = "_Tickets_In", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_In
        {
            get
            {
                return this._Tickets_In;
            }
            set
            {
                if ((this._Tickets_In != value))
                {
                    this._Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Out", DbType = "Decimal")]
        public System.Nullable<decimal> Tickets_Out
        {
            get
            {
                return this._Tickets_Out;
            }
            set
            {
                if ((this._Tickets_Out != value))
                {
                    this._Tickets_Out = value;
                }
            }
        }
    }

    public class CommonCollectionLiquidation : CommonLiquidationEntity, INotifyPropertyChanged
    {

        private string _Retailer_Name;

        private System.Nullable<int> _Collection_No { get; set; }

        private System.Nullable<int> _Read_No { get; set; }

        private System.Nullable<System.DateTime> _Liquidation_Date;

        private System.Nullable<decimal> _Gross;

        private System.Nullable<decimal> _Tickets_Expected;

        private System.Nullable<decimal> _Net;

        private System.Nullable<decimal> _Net_Percentage;

        private System.Nullable<decimal> _Percentage_Setting;

        private System.Nullable<decimal> _Retailer;

        private System.Nullable<decimal> _Retailer_Negative_Net;

        private System.Nullable<decimal> _Retailer_Share;

        private System.Nullable<decimal> _Tickets_Paid;

        private System.Nullable<decimal> _Advance_To_Retailer;

        private System.Nullable<decimal> _Balance_Due;

        private System.Nullable<decimal> _RetailerShareBeforeFixedExpense;

        private int _ProfitShareGroupId;

        private int _ExpenseShareGroupID;

        private decimal _ExpenseSharePercentage;

        private decimal _ExpenseShareAmount;

        private decimal _WriteOffAmount;

        private int _PayPeriodId;

        private System.Nullable<decimal> _FixedExpenseAmount;

        private System.Nullable<decimal> _PrevCarriedForwardExpense;

        private System.Nullable<decimal> _Negative_Net;

        private System.Nullable<decimal> _RetailerExpenseShareAmount;

        private System.Nullable<decimal> _RetailerNetRevenue;

        private decimal _CarriedForwardExpense;

        private System.Nullable<decimal> _ExpenseShareAmountOfOperator;

        public CommonCollectionLiquidation()
        {
        }

        [Column(Storage = "_Collection_No", DbType = "Int")]
        public override System.Nullable<int> Collection_No
        {
            get
            {
                return this._Collection_No;
            }
            set
            {
                if ((this._Collection_No != value))
                {
                    this._Collection_No = value;
                }
            }
        }

        [Column(Storage = "_Read_No", DbType = "Int")]
        public override System.Nullable<int> Read_No
        {
            get
            {
                return this._Read_No;
            }
            set
            {
                if ((this._Read_No != value))
                {
                    this._Read_No = value;
                }
            }
        }

        [Column(Storage = "_Retailer_Name", DbType = "VarChar(50)")]
        public override string Retailer_Name
        {
            get
            {
                return this._Retailer_Name;
            }
            set
            {
                if ((this._Retailer_Name != value))
                {
                    this._Retailer_Name = value;
                }
            }
        }

        [Column(Storage = "_Liquidation_Date", DbType = "DateTime")]
        public override System.Nullable<System.DateTime> Liquidation_Date
        {
            get
            {
                return this._Liquidation_Date;
            }
            set
            {
                if ((this._Liquidation_Date != value))
                {
                    this._Liquidation_Date = value;
                }
            }
        }

        [Column(Storage = "_Gross", DbType = "Decimal")]
        public override System.Nullable<decimal> Gross
        {
            get
            {
                return this._Gross.GetValueOrDefault();
            }
            set
            {
                if ((this._Gross != value))
                {
                    this._Gross = value;
                }
            }
        }

        [Column(Storage = "_Tickets_Expected", DbType = "Decimal")]
        public override System.Nullable<decimal> Tickets_Expected
        {
            get
            {
                return this._Tickets_Expected.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Expected != value))
                {
                    this._Tickets_Expected = value;
                }
            }
        }

        [Column(Storage = "_Net", DbType = "Decimal")]
        public override System.Nullable<decimal> Net
        {
            get
            {
                return this._Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Net != value))
                {
                    this._Net = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Net"));
                }
            }
        }

        [Column(Storage = "_Net_Percentage", DbType = "Decimal")]
        public override System.Nullable<decimal> Net_Percentage
        {
            get
            {
                return Net * Percentage_Setting;
            }
            set
            {
                if ((this._Net_Percentage != value))
                {
                    this._Net_Percentage = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Net_Percentage"));
                }
            }
        }

        [Column(Storage = "_Percentage_Setting", DbType = "Decimal")]
        public override System.Nullable<decimal> Percentage_Setting
        {
            get
            {
                return this._Percentage_Setting.GetValueOrDefault() / 100;
            }
            set
            {
                if ((this._Percentage_Setting != value))
                {
                    this._Percentage_Setting = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Percentage_Setting"));
                }
            }
        }

        [Column(Storage = "_Retailer", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer
        {
            get
            {
                _Retailer = (RetailerShareBeforeFixedExpense - CarriedForwardExpense);

                return _Retailer;
            }
            set
            {
                if ((this._Retailer != value))
                {
                    this._Retailer = value;
                }
            }
        }

        [Column(Storage = "_Retailer_Negative_Net", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer_Negative_Net
        {
            get
            {
                return this._Retailer_Negative_Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Retailer_Negative_Net != value))
                {
                    this._Retailer_Negative_Net = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Retailer_Negative_Net"));
                }
            }
        }

        [Column(Storage = "_Retailer_Share", DbType = "Decimal")]
        public override System.Nullable<decimal> Retailer_Share
        {
            get
            {
                _Retailer_Share = (Net_Percentage) + Retailer_Negative_Net;
                return _Retailer_Share.GetValueOrDefault();
            }
            set
            {
                if ((this._Retailer_Share != value))
                {
                    this._Retailer_Share = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Retailer_Share"));
                }
            }
        }

        [Column(Storage = "_Tickets_Paid", DbType = "Decimal")]
        public override System.Nullable<decimal> Tickets_Paid
        {
            get
            {
                return this._Tickets_Paid.GetValueOrDefault();
            }
            set
            {
                if ((this._Tickets_Paid != value))
                {
                    this._Tickets_Paid = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Tickets_Paid"));
                }
            }
        }

        [Column(Storage = "_Advance_To_Retailer", DbType = "Decimal")]
        public override System.Nullable<decimal> Advance_To_Retailer
        {
            get
            {
                return this._Advance_To_Retailer.GetValueOrDefault();
            }
            set
            {
                if ((this._Advance_To_Retailer != value))
                {
                    this._Advance_To_Retailer = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Advance_To_Retailer"));
                }
            }
        }

        [Column(Storage = "_Balance_Due", DbType = "Decimal")]
        public override System.Nullable<decimal> Balance_Due
        {
            get
            {
                if (Retailer_Share > 0)
                    _Balance_Due = ((Net - Net_Percentage) + (Advance_To_Retailer - Retailer_Negative_Net) - (ExpenseShareAmountOfOperator));
                else
                    _Balance_Due = ((Net + Advance_To_Retailer) - (ExpenseShareAmountOfOperator));

                return _Balance_Due.GetValueOrDefault();
            }
            set
            {
                if ((this._Balance_Due != value))
                {
                    this._Balance_Due = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Balance_Due"));
                }
            }
        }

        [Column(Storage = "_RetailerShareBeforeFixedExpense", DbType = "Decimal")]
        public override System.Nullable<decimal> RetailerShareBeforeFixedExpense
        {
            get
            {
                if (Retailer_Share > 0)
                    _RetailerShareBeforeFixedExpense = (Retailer_Share - Balance_Due);
                else
                    _RetailerShareBeforeFixedExpense = 0 - (Balance_Due);

                return _RetailerShareBeforeFixedExpense.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerShareBeforeFixedExpense != value))
                {
                    this._RetailerShareBeforeFixedExpense = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RetailerShareBeforeFixedExpense"));
                }
            }
        }

        [Column(Storage = "_ProfitShareGroupId", DbType = "Int NOT NULL")]
        public override int ProfitShareGroupId
        {
            get
            {
                return this._ProfitShareGroupId;
            }
            set
            {
                if ((this._ProfitShareGroupId != value))
                {
                    this._ProfitShareGroupId = value;
                }
            }
        }

        [Column(Storage = "_ExpenseShareGroupID", DbType = "Int NOT NULL")]
        public override int ExpenseShareGroupID
        {
            get
            {
                return this._ExpenseShareGroupID;
            }
            set
            {
                if ((this._ExpenseShareGroupID != value))
                {
                    this._ExpenseShareGroupID = value;
                }
            }
        }

        [Column(Storage = "_ExpenseSharePercentage", DbType = "Decimal NOT NULL")]
        public override decimal ExpenseSharePercentage
        {
            get
            {
                return this._ExpenseSharePercentage / 100;
            }
            set
            {
                if ((this._ExpenseSharePercentage != value))
                {
                    this._ExpenseSharePercentage = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseSharePercentage"));
                }
            }
        }

        [Column(Storage = "_ExpenseShareAmount", DbType = "Decimal NOT NULL")]
        public override decimal ExpenseShareAmount
        {
            get
            {
                return this._ExpenseShareAmount;
            }
            set
            {
                if ((this._ExpenseShareAmount != value))
                {
                    this._ExpenseShareAmount = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseShareAmount"));
                }
            }
        }

        [Column(Storage = "_WriteOffAmount", DbType = "Decimal NOT NULL")]
        public override decimal WriteOffAmount
        {
            get
            {
                return this._WriteOffAmount;
            }
            set
            {
                if ((this._WriteOffAmount != value))
                {
                    this._WriteOffAmount = value;
                }
            }
        }

        [Column(Storage = "_PayPeriodId", DbType = "Int NOT NULL")]
        public override int PayPeriodId
        {
            get
            {
                return this._PayPeriodId;
            }
            set
            {
                if ((this._PayPeriodId != value))
                {
                    this._PayPeriodId = value;
                }
            }
        }

        [Column(Storage = "_FixedExpenseAmount", DbType = "DECIMAL")]
        public override System.Nullable<decimal> FixedExpenseAmount
        {
            get
            {
                _FixedExpenseAmount = (ExpenseShareAmount * ExpenseSharePercentage);
                return _FixedExpenseAmount.GetValueOrDefault();
            }
            set
            {
                if ((this._FixedExpenseAmount != value))
                {
                    this._FixedExpenseAmount = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("FixedExpenseAmount"));
                }
            }
        }

        [Column(Storage = "_PrevCarriedForwardExpense", DbType = "DECIMAL")]
        public override System.Nullable<decimal> PrevCarriedForwardExpense
        {
            get
            {
                return this._PrevCarriedForwardExpense.GetValueOrDefault();
            }
            set
            {
                if ((this._PrevCarriedForwardExpense != value))
                {
                    this._PrevCarriedForwardExpense = value;
                }
            }
        }

        [Column(Storage = "_Negative_Net", DbType = "DECIMAL")]
        public override System.Nullable<decimal> Negative_Net
        {
            get
            {
                return this._Negative_Net.GetValueOrDefault();
            }
            set
            {
                if ((this._Negative_Net != value))
                {
                    this._Negative_Net = value;
                }
            }
        }
        [Column(Storage = "_RetailerExpenseShareAmount", DbType = "DECIMAL")]
        public override System.Nullable<decimal> RetailerExpenseShareAmount
        {
            get
            {
                return this._RetailerExpenseShareAmount.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerExpenseShareAmount != value))
                {
                    this._RetailerExpenseShareAmount = value;
                }
            }
        }

        [Column(Storage = "_RetailerNetRevenue", DbType = "DECIMAL")]
        public override System.Nullable<decimal> RetailerNetRevenue
        {
            get
            {
                return this._RetailerNetRevenue.GetValueOrDefault();
            }
            set
            {
                if ((this._RetailerNetRevenue != value))
                {
                    this._RetailerNetRevenue = value;
                    if(PropertyChanged!=null)
                         PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RetailerNetRevenue"));
                }
            }
        }

        [Column(Storage = "_ExpenseShareAmountOfOperator", DbType = "DECIMAL")]
        public System.Nullable<decimal> ExpenseShareAmountOfOperator
        {
            get
            {
                return (this._ExpenseShareAmount - this.FixedExpenseAmount.GetValueOrDefault());
            }
            set
            {
                if ((this._ExpenseShareAmountOfOperator != value))
                {
                    this._ExpenseShareAmountOfOperator = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ExpenseShareAmountOfOperator"));
                }
            }
        }

        [Column(Storage = "_CarriedForwardExpense", DbType = "DECIMAL")]
        public override decimal CarriedForwardExpense
        {
            get
            {
                return (FixedExpenseAmount + PrevCarriedForwardExpense - WriteOffAmount).GetValueOrDefault();
            }
            set
            {
                if ((this._CarriedForwardExpense != value))
                {
                    this._CarriedForwardExpense = value;
                    if (PropertyChanged != null)
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CarriedForwardExpense"));
                }
            }
        }

        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }

    public partial class ReadLiquidationReportRecords
	{
		
		private int _LiquidationId;

        private string _SiteLiquidationId;
		
		private System.Nullable<int> _ReadId;

        private System.Nullable<System.DateTime> _Read_Date;
		
		private System.Nullable<System.DateTime> _LiquidationDate;
		
		private string _Calendar_Period;
		
		private System.Nullable<decimal> _Gross;
		
		private System.Nullable<decimal> _TicketsExpected;
		
		private System.Nullable<decimal> _Net;
		
		private System.Nullable<decimal> _RetailerNegativeNet;
		
		private System.Nullable<decimal> _TicketPaid;
		
		private System.Nullable<decimal> _AdvanceToRetailer;
		
		private string _Retailer;
		
		private System.Nullable<decimal> _BalanceDue;
		
		private System.Nullable<decimal> _RetailerNetRevenue;

        public ReadLiquidationReportRecords()
		{
		}

        [Column(Storage = "_Read_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Read_Date
        {
            get
            {
                return this._Read_Date;
            }
            set
            {
                if ((this._Read_Date != value))
                {
                    this._Read_Date = value;
                }
            }
        }

        [Column(Storage = "_LiquidationId", DbType = "Int NOT NULL")]
        public int LiquidationId
        {
            get
            {
                return this._LiquidationId;
            }
            set
            {
                if ((this._LiquidationId != value))
                {
                    this._LiquidationId = value;
                }
            }
        }

        [Column(Storage = "_SiteLiquidationId", DbType = "VarChar(20)")]
        public string SiteLiquidationId
		{
			get
			{
                return this._SiteLiquidationId;
			}
			set
			{
                if ((this._SiteLiquidationId != value))
				{
                    this._SiteLiquidationId = value;
				}
			}
		}

		[Column(Storage="_ReadId", DbType="Int")]
		public System.Nullable<int> ReadId
		{
			get
			{
				return this._ReadId;
			}
			set
			{
				if ((this._ReadId != value))
				{
					this._ReadId = value;
				}
			}
		}
		
		[Column(Storage="_LiquidationDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> LiquidationDate
		{
			get
			{
				return this._LiquidationDate;
			}
			set
			{
				if ((this._LiquidationDate != value))
				{
					this._LiquidationDate = value;
				}
			}
		}
		
		[Column(Storage="_Calendar_Period", DbType="NVarChar(73)")]
		public string Calendar_Period
		{
			get
			{
				return this._Calendar_Period;
			}
			set
			{
				if ((this._Calendar_Period != value))
				{
					this._Calendar_Period = value;
				}
			}
		}
		
		[Column(Storage="_Gross", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> Gross
		{
			get
			{
				return this._Gross;
			}
			set
			{
				if ((this._Gross != value))
				{
					this._Gross = value;
				}
			}
		}
		
		[Column(Storage="_TicketsExpected", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> TicketsExpected
		{
			get
			{
				return this._TicketsExpected;
			}
			set
			{
				if ((this._TicketsExpected != value))
				{
					this._TicketsExpected = value;
				}
			}
		}
		
		[Column(Storage="_Net", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> Net
		{
			get
			{
				return this._Net;
			}
			set
			{
				if ((this._Net != value))
				{
					this._Net = value;
				}
			}
		}
		
		[Column(Storage="_RetailerNegativeNet", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> RetailerNegativeNet
		{
			get
			{
				return this._RetailerNegativeNet;
			}
			set
			{
				if ((this._RetailerNegativeNet != value))
				{
					this._RetailerNegativeNet = value;
				}
			}
		}
		
		[Column(Storage="_TicketPaid", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> TicketPaid
		{
			get
			{
				return this._TicketPaid;
			}
			set
			{
				if ((this._TicketPaid != value))
				{
					this._TicketPaid = value;
				}
			}
		}
		
		[Column(Storage="_AdvanceToRetailer", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> AdvanceToRetailer
		{
			get
			{
				return this._AdvanceToRetailer;
			}
			set
			{
				if ((this._AdvanceToRetailer != value))
				{
					this._AdvanceToRetailer = value;
				}
			}
		}
		
		[Column(Storage="_Retailer", DbType="VarChar(50)")]
		public string Retailer
		{
			get
			{
				return this._Retailer;
			}
			set
			{
				if ((this._Retailer != value))
				{
					this._Retailer = value;
				}
			}
		}
		
		[Column(Storage="_BalanceDue", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> BalanceDue
		{
			get
			{
				return this._BalanceDue;
			}
			set
			{
				if ((this._BalanceDue != value))
				{
					this._BalanceDue = value;
				}
			}
		}
		
		[Column(Storage="_RetailerNetRevenue", DbType="Decimal(0,0)")]
		public System.Nullable<decimal> RetailerNetRevenue
		{
			get
			{
				return this._RetailerNetRevenue;
			}
			set
			{
				if ((this._RetailerNetRevenue != value))
				{
					this._RetailerNetRevenue = value;
				}
			}
		}
	}

    public partial class LiquidationDetailForReport
    {

        private int _LiquidationId;

        private string _SiteName;

        private string _Period_End;

        private System.Nullable<decimal> _WriteOffAmount;

        private System.Nullable<decimal> _MeterIn;

        private System.Nullable<decimal> _MeterOut;

        private System.Nullable<decimal> _NetAmount;

        private System.Nullable<decimal> _RetailerShareBeforeAdjustment;

        private System.Nullable<decimal> _RetailerNegativeNet;

        private System.Nullable<decimal> _RetailerShareAfterAdjustment;

        private System.Nullable<decimal> _TicketPaid;

        private System.Nullable<decimal> _AdvanceToRetailer;

        private System.Nullable<decimal> _BalanceDue;

        private System.Nullable<decimal> _RetailerShareBeforeFixedExpense;

        private System.Nullable<decimal> _FixedExpenseAmount;

        private System.Nullable<decimal> _CarriedForwardExpense;

        private System.Nullable<decimal> _RetailerNetRevenue;

        public LiquidationDetailForReport()
        {
        }

        [Column(Storage = "_LiquidationId", DbType = "Int NOT NULL")]
        public int LiquidationId
        {
            get
            {
                return this._LiquidationId;
            }
            set
            {
                if ((this._LiquidationId != value))
                {
                    this._LiquidationId = value;
                }
            }
        }

        [Column(Storage = "_SiteName", DbType = "VarChar(50)")]
        public string SiteName
        {
            get
            {
                return this._SiteName;
            }
            set
            {
                if ((this._SiteName != value))
                {
                    this._SiteName = value;
                }
            }
        }

        [Column(Storage = "_Period_End", DbType = "NVarChar(24)")]
        public string Period_End
        {
            get
            {
                return this._Period_End;
            }
            set
            {
                if ((this._Period_End != value))
                {
                    this._Period_End = value;
                }
            }
        }

        [Column(Storage = "_WriteOffAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> WriteOffAmount
        {
            get
            {
                return this._WriteOffAmount;
            }
            set
            {
                if ((this._WriteOffAmount != value))
                {
                    this._WriteOffAmount = value;
                }
            }
        }

        [Column(Storage = "_MeterIn", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MeterIn
        {
            get
            {
                return this._MeterIn;
            }
            set
            {
                if ((this._MeterIn != value))
                {
                    this._MeterIn = value;
                }
            }
        }

        [Column(Storage = "_MeterOut", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> MeterOut
        {
            get
            {
                return this._MeterOut;
            }
            set
            {
                if ((this._MeterOut != value))
                {
                    this._MeterOut = value;
                }
            }
        }

        [Column(Storage = "_NetAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> NetAmount
        {
            get
            {
                return this._NetAmount;
            }
            set
            {
                if ((this._NetAmount != value))
                {
                    this._NetAmount = value;
                }
            }
        }

        [Column(Storage = "_RetailerShareBeforeAdjustment", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RetailerShareBeforeAdjustment
        {
            get
            {
                return this._RetailerShareBeforeAdjustment;
            }
            set
            {
                if ((this._RetailerShareBeforeAdjustment != value))
                {
                    this._RetailerShareBeforeAdjustment = value;
                }
            }
        }

        [Column(Storage = "_RetailerNegativeNet", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RetailerNegativeNet
        {
            get
            {
                return this._RetailerNegativeNet;
            }
            set
            {
                if ((this._RetailerNegativeNet != value))
                {
                    this._RetailerNegativeNet = value;
                }
            }
        }

        [Column(Storage = "_RetailerShareAfterAdjustment", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RetailerShareAfterAdjustment
        {
            get
            {
                return this._RetailerShareAfterAdjustment;
            }
            set
            {
                if ((this._RetailerShareAfterAdjustment != value))
                {
                    this._RetailerShareAfterAdjustment = value;
                }
            }
        }

        [Column(Storage = "_TicketPaid", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> TicketPaid
        {
            get
            {
                return this._TicketPaid;
            }
            set
            {
                if ((this._TicketPaid != value))
                {
                    this._TicketPaid = value;
                }
            }
        }

        [Column(Storage = "_AdvanceToRetailer", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> AdvanceToRetailer
        {
            get
            {
                return this._AdvanceToRetailer;
            }
            set
            {
                if ((this._AdvanceToRetailer != value))
                {
                    this._AdvanceToRetailer = value;
                }
            }
        }

        [Column(Storage = "_BalanceDue", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> BalanceDue
        {
            get
            {
                return this._BalanceDue;
            }
            set
            {
                if ((this._BalanceDue != value))
                {
                    this._BalanceDue = value;
                }
            }
        }

        [Column(Storage = "_RetailerShareBeforeFixedExpense", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RetailerShareBeforeFixedExpense
        {
            get
            {
                return this._RetailerShareBeforeFixedExpense;
            }
            set
            {
                if ((this._RetailerShareBeforeFixedExpense != value))
                {
                    this._RetailerShareBeforeFixedExpense = value;
                }
            }
        }

        [Column(Storage = "_FixedExpenseAmount", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> FixedExpenseAmount
        {
            get
            {
                return this._FixedExpenseAmount;
            }
            set
            {
                if ((this._FixedExpenseAmount != value))
                {
                    this._FixedExpenseAmount = value;
                }
            }
        }

        [Column(Storage = "_CarriedForwardExpense", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> CarriedForwardExpense
        {
            get
            {
                return this._CarriedForwardExpense;
            }
            set
            {
                if ((this._CarriedForwardExpense != value))
                {
                    this._CarriedForwardExpense = value;
                }
            }
        }

        [Column(Storage = "_RetailerNetRevenue", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> RetailerNetRevenue
        {
            get
            {
                return this._RetailerNetRevenue;
            }
            set
            {
                if ((this._RetailerNetRevenue != value))
                {
                    this._RetailerNetRevenue = value;
                }
            }
        }
    }

    public partial class rsp_GetOperatorExpenseSharePercentageResult
    {

        private System.Nullable<decimal> _OperatorExpSharePercentage;

        public rsp_GetOperatorExpenseSharePercentageResult()
        {
        }

        [Column(Storage = "_OperatorExpSharePercentage", DbType = "Decimal(0,0)")]
        public System.Nullable<decimal> OperatorExpSharePercentage
        {
            get
            {
                return this._OperatorExpSharePercentage;
            }
            set
            {
                if ((this._OperatorExpSharePercentage != value))
                {
                    this._OperatorExpSharePercentage = value;
                }
            }
        }
    }
}
