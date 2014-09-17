using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        #region Methods

        [Function(Name = "dbo.esp_Calculate_Collection_Negative_Net")]
        public ISingleResult<esp_Calculate_Collection_Negative_NetResult> esp_Calculate_Collection_Negative_Net([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, [Parameter(Name = "Retailer_Share_Percentage", DbType = "Decimal")] System.Nullable<decimal> dRetailerPercenage)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, batch_No, dRetailerPercenage);
            return ((ISingleResult<esp_Calculate_Collection_Negative_NetResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_Insert_Collection_Batch_Advance")]
        public int usp_Insert_Collection_Batch_Advance([Parameter(Name = "BatchNo", DbType = "Int")] System.Nullable<int> batchNo, [Parameter(Name = "Collection_Batch_Advance_Value", DbType = "Float")] System.Nullable<double> collection_Batch_Advance_Value)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchNo, collection_Batch_Advance_Value);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_PerformLiquidationForProfitShare")]
        public int usp_PerformLiquidationForProfitShare([Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> site_Id, [Parameter(Name = "Batch_No", DbType = "Int")] System.Nullable<int> batch_No, [Parameter(Name = "ProfitShareGroupId", DbType = "Int")] System.Nullable<int> profitShareGroupId, [Parameter(Name = "ExpenseShareGroupId", DbType = "Int")] System.Nullable<int> expenseShareGroupId, [Parameter(Name = "ExpenseShareAmount", DbType = "Decimal")] System.Nullable<decimal> expenseShareAmount, [Parameter(Name = "WriteOffAmount", DbType = "Decimal")] System.Nullable<decimal> writeOffAmount, [Parameter(Name = "PayPeriodId", DbType = "Int")] System.Nullable<int> payPeriodId, [Parameter(Name = "CarriedForwardExpense", DbType = "Decimal")] System.Nullable<decimal> carriedForwardExpense)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_Id, batch_No, profitShareGroupId, expenseShareGroupId, expenseShareAmount, writeOffAmount, payPeriodId, carriedForwardExpense);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_LiquidationShareDetails")]
        public ISingleResult<rsp_LiquidationShareDetailsResult> rsp_LiquidationShareDetails([Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId, [Parameter(Name = "BatchId", DbType = "Int")] System.Nullable<int> batchId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId, batchId);
            return ((ISingleResult<rsp_LiquidationShareDetailsResult>)(result.ReturnValue));
        }

        public partial class esp_Calculate_Collection_Negative_NetResult
        {

            private System.Nullable<decimal> _Batch_Negative_Net;

            public esp_Calculate_Collection_Negative_NetResult()
            {
            }

            [Column(Storage = "_Batch_Negative_Net", DbType = "Int")]
            public System.Nullable<decimal> Batch_Negative_Net
            {
                get
                {
                    return this._Batch_Negative_Net;
                }
                set
                {
                    if ((this._Batch_Negative_Net != value))
                    {
                        this._Batch_Negative_Net = value;
                    }
                }
            }
        }

        public partial class rsp_LiquidationShareDetailsResult
        {

            private System.Nullable<System.DateTime> _CollectionPerformedDate;

            private string _SiteName;

            private System.Nullable<decimal> _MeterIn;

            private System.Nullable<decimal> _NetAmount;

            private System.Nullable<double> _RetailerSharePercentage;

            private System.Nullable<decimal> _BalanceDue;

            private string _Retailer;

            private System.Nullable<decimal> _RetailerNegativeNet;

            private System.Nullable<decimal> _MeterOut;

            private System.Nullable<decimal> _TicketPaid;

            private System.Nullable<decimal> _AdvanceToRetailer;

            private System.Nullable<decimal> _RetailerShareAfterAdjustment;

            private System.Nullable<decimal> _RetailerNetRevenue;

            public rsp_LiquidationShareDetailsResult()
            {
            }

            [Column(Storage = "_CollectionPerformedDate", DbType = "DateTime")]
            public System.Nullable<System.DateTime> CollectionPerformedDate
            {
                get
                {
                    return this._CollectionPerformedDate;
                }
                set
                {
                    if ((this._CollectionPerformedDate != value))
                    {
                        this._CollectionPerformedDate = value;
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

            [Column(Storage = "_RetailerSharePercentage", DbType = "Float")]
            public System.Nullable<double> RetailerSharePercentage
            {
                get
                {
                    return this._RetailerSharePercentage;
                }
                set
                {
                    if ((this._RetailerSharePercentage != value))
                    {
                        this._RetailerSharePercentage = value;
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

            [Column(Storage = "_Retailer", DbType = "VarChar(50)")]
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
        #endregion //Methods
    }
}
