using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using BMC.Business.CashDeskOperator;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using BMC.CoreLib.WPF;
using BMC.CoreLib.Win32;
using BMC.Common.ExceptionManagement;
namespace BMC.CashDeskOperator
{

    public class TicketsHelper
    {
        private readonly TicketingDataContext _TicketingDataContext;

        public TicketsHelper()
        {
            _TicketingDataContext = new TicketingDataContext(new SqlConnection(CommonUtilities.TicketConnectionString));

        }
        public TicketsHelper(string TicketConst)
        {
            _TicketingDataContext = new TicketingDataContext(new SqlConnection(TicketConst));

        }    
        public IMultipleResults GetVouchers(string sTickets, string sAsset)
        {
            return _TicketingDataContext.GetValidInvalidVouchers(sTickets, sAsset);
        }

        public IMultipleResults GetValidInvalidVouchers_Counter(string sTickets, string sAsset,int Collection_no,int Installation_No)
        {
            return _TicketingDataContext.GetValidInvalidVouchers_Counter(sTickets, sAsset,Collection_no,Installation_No);
        }       

        public ISingleResult<ValidateVoidVoucherResult> ValidateVoidVoucher(string ticketNumber, ref int? nResult)
        {
            return _TicketingDataContext.ValidateVoidVoucher(ticketNumber, ref nResult);
        }

        public decimal ValidateVoidVoucher(string ticketNumber, int userNo, ref int? nResult)
        {
            decimal dAmount = 0;

            try
            {
                // is tis printed ticket
                if (VoucherHelper.IsTISPrintedTicketPrefix(ticketNumber))
                {
                    // if the tis printed ticket available in local db
                    bool isTISTicketAvailable = VoucherHelper.IsTISPrintedTicket(ticketNumber);
                    if (!isTISTicketAvailable)
                    {
                        // wait worst case 10 secs to get the response from TIS
                        int count = 10;
                        string message = "Waiting for receiving data from TIS...";
                        WPFExtensions.ShowAsyncDialog(null, message, null, 1, count,
                            (o) =>
                            {
                                IAsyncProgress2 o2 = o as IAsyncProgress2;

                                // failure case - hit the tis communication interface and get the ticket
                                try
                                {
                                    o2.UpdateStatusProgress(5, message);
                                    VoucherHelper.SendTISRedeemTicketQuery(ticketNumber, userNo);
                                    o2.UpdateStatusProgress(10, message);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            });
                    }
                }

                // Success/failure case - ok proceed with voiding
                foreach (var obj in this.ValidateVoidVoucher(ticketNumber, ref nResult))
                {
                    dAmount = Convert.ToDecimal(obj.iAmount) / 100;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

            return dAmount;
        }

        public ISingleResult<UpdateVoidVoucherResult> UpdateVoidVoucher(string ticketNumber, string workStation, System.Nullable<int> iVoucherVoidUser, string notes)
        {
            return _TicketingDataContext.UpdateVoidVoucher(ticketNumber, workStation, iVoucherVoidUser, notes);
        }
    }

    public class ExchangeHelper
    {
        private readonly ExchangeDataContext _ExchangeDataContext;

        public ExchangeHelper()
        {
            _ExchangeDataContext = new ExchangeDataContext(new SqlConnection(CommonUtilities.ExchangeConnectionString));

        }
        public ExchangeHelper(string ExchangeConst)
        {
            _ExchangeDataContext = new ExchangeDataContext(new SqlConnection(ExchangeConst));

        }
        public string GetGameName(string sAsset)
        {
            return _ExchangeDataContext.FnGetGameNameforAsset(sAsset);
        }
        public IMultipleResults GetDeclaredTicketByCollection(int Collection_No,int  Installation_ID)
        {
              return _ExchangeDataContext.GetDeclaredTicketByCollection(Collection_No,Installation_ID); 
        }
    }


    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Ticketing")]
    internal class TicketingDataContext : System.Data.Linq.DataContext
    {
        public TicketingDataContext(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }
        
        [Function(Name = "dbo.rsp_GetValidInvalidVouchers"),
        ResultType(typeof(ParentVoucher.ValidVouchers)),
        ResultType(typeof(ParentVoucher.InValidVouchers)),
        ResultType(typeof(ParentVoucher.ValidVouchersQty)),
        ResultType(typeof(ParentVoucher.InValidVouchersQty)),]

        public IMultipleResults GetValidInvalidVouchers([Parameter(Name = "Tickets", DbType = "VarChar(MAX)")] string tickets, [Parameter(Name = "Asset", DbType = "VarChar(50)")] string asset)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tickets, asset);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetValidInvalidVouchers_Counter"),
        ResultType(typeof(ParentVoucher.ValidVouchers)),    
        ResultType(typeof(ParentVoucher.InValidVouchers)),
        ResultType(typeof(ParentVoucher.ValidVouchersQty)),
        ResultType(typeof(ParentVoucher.InValidVouchersQty)),]

        public IMultipleResults GetValidInvalidVouchers_Counter([Parameter(Name = "Tickets", DbType = "VarChar(MAX)")] string tickets, [Parameter(Name = "Asset", DbType = "VarChar(50)")] string asset, [Parameter(Name = "Collection_no", DbType = "Int")] int Collection_no,[Parameter(Name = "Installation_No", DbType = "Int")] int Installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), tickets, asset,Collection_no,Installation_No);
            return ((IMultipleResults)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_ValidateVoidVoucher")]
        public ISingleResult<ValidateVoidVoucherResult> ValidateVoidVoucher([Parameter(Name = "TicketNumber", DbType = "Char(32)")] string ticketNumber, [Parameter(Name = "Result", DbType = "Int")] ref System.Nullable<int> result)
        {
            IExecuteResult result1 = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ticketNumber, result);
            result = ((System.Nullable<int>)(result1.GetParameterValue(1)));
            return ((ISingleResult<ValidateVoidVoucherResult>)(result1.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateVoidVoucher")]
        public ISingleResult<UpdateVoidVoucherResult> UpdateVoidVoucher([Parameter(Name = "TicketNumber", DbType = "Char(32)")] string ticketNumber, [Parameter(Name = "WorkStation", DbType = "VarChar(50)")] string workStation, [Parameter(DbType = "Int")] System.Nullable<int> iVoucherVoidUser, [Parameter(Name = "Notes", DbType = "VarChar(100)")] string notes)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), ticketNumber, workStation, iVoucherVoidUser, notes);
            return ((ISingleResult<UpdateVoidVoucherResult>)(result.ReturnValue));
        }
    }


    [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Exchange")]
    internal class ExchangeDataContext : System.Data.Linq.DataContext
    {
        public ExchangeDataContext(IDbConnection dbConnection)
            : base(dbConnection)
        {
        }

        [Function(Name = "dbo.FnGetGameNameforAsset", IsComposable = true)]
        public string FnGetGameNameforAsset([Parameter(Name = "Asset", DbType = "VarChar(50)")] string asset)
        {
            return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), asset).ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDeclaredTicketByCollection_Counter"),
        ResultType(typeof(ParentVoucher.ValidVouchers)),
        ResultType(typeof(ParentVoucher.ValidVouchers))]
        public IMultipleResults GetDeclaredTicketByCollection([Parameter(Name = "Collection_ID ", DbType = "Int")] int Collection_No, [Parameter(Name = "Installation_ID", DbType = "Int")] int Installation_ID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), Collection_No, Installation_ID);
            return ((IMultipleResults)(result.ReturnValue));
        }

    }

    
    public partial class ParentVoucher
    {

        public partial class ValidVouchers
        {

            private string _strBarcode;

            private System.Nullable<int> _iAmount;

            public ValidVouchers()
            {
            }

            [Column(Storage = "_strBarcode", DbType = "VarChar(32)")]
            public string strBarcode
            {
                get
                {
                    return this._strBarcode;
                }
                set
                {
                    if ((this._strBarcode != value))
                    {
                        this._strBarcode = value;
                    }
                }
            }

            [Column(Storage = "_iAmount", DbType = "Int")]
            public System.Nullable<int> iAmount
            {
                get
                {
                    return this._iAmount;
                }
                set
                {
                    if ((this._iAmount != value))
                    {
                        this._iAmount = value;
                    }
                }
            }
        }

        public partial class InValidVouchers : INotifyPropertyChanged
        {

            private string _strBarcode;
			private System.Nullable<int> _Reasonid;
			private System.Nullable<bool> _CanUpdate;
	        private System.Nullable<bool> _IsSelected;

        [Column(Storage = "_strBarcode", DbType = "VarChar(32)")]
        public string strBarcode
		{
			get
			{
                return this._strBarcode;
			}
			set
			{
                if ((this._strBarcode != value))
				{
                    this._strBarcode = value;
				}
			}
		}
		
		[Column(Storage="_Reasonid", DbType="Int")]
		public System.Nullable<int> Reasonid
		{
			get
			{
				return this._Reasonid;
			}
			set
			{
				if ((this._Reasonid != value))
				{
					this._Reasonid = value;
				}
			}
		}
		
		[Column(Storage="_CanUpdate", DbType="Bit")]
		public System.Nullable<bool> CanUpdate
		{
			get
			{
				return this._CanUpdate;
			}
			set
			{
				if ((this._CanUpdate != value))
				{
					this._CanUpdate = value;
				}
			}
		}
        [Column(Storage = "_IsSelected", DbType = "Bit")]
        public System.Nullable<bool> IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                if ((this._IsSelected != value))
                {
                    this._IsSelected = value;
                    if(PropertyChanged!=null) PropertyChanged(this , new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        }

        public partial class ValidVouchersQty
        {
            private System.Nullable<int> _Total;
            private System.Nullable<int> _Quantity;

            public ValidVouchersQty()
            {
            }


            [Column(Storage = "_Total", DbType = "Int")]
            public System.Nullable<int> Total
            {
                get
                {
                    return this._Total;
                }
                set
                {
                    if ((this._Total != value))
                    {
                        this._Total = value;
                    }
                }
            }

            [Column(Storage = "_Quantity", DbType = "Int")]
            public System.Nullable<int> Quantity
            {
                get
                {
                    return this._Quantity;
                }
                set
                {
                    if ((this._Quantity != value))
                    {
                        this._Quantity = value;
                    }
                }
            }
        }

        public partial class InValidVouchersQty
        {
            
            private System.Nullable<int> _Quantity;

            public InValidVouchersQty()
            {
            }


           [Column(Storage = "_Quantity", DbType = "Int")]
            public System.Nullable<int> Quantity
            {
                get
                {
                    return this._Quantity;
                }
                set
                {
                    if ((this._Quantity != value))
                    {
                        this._Quantity = value;
                    }
                }
            }
        }
    }    

    public partial class ValidateVoidVoucherResult
    {

        private int _iAmount;

        public ValidateVoidVoucherResult()
        {
        }

        [Column(Storage = "_iAmount", DbType = "Int NOT NULL")]
        public int iAmount
        {
            get
            {
                return this._iAmount;
            }
            set
            {
                if ((this._iAmount != value))
                {
                    this._iAmount = value;
                }
            }
        }
    }

    public partial class UpdateVoidVoucherResult
    {

        private System.Nullable<int> _iTransactionNo;

        private int _TE_ID;

        public UpdateVoidVoucherResult()
        {
        }

        [Column(Storage = "_iTransactionNo", DbType = "Int")]
        public System.Nullable<int> iTransactionNo
        {
            get
            {
                return this._iTransactionNo;
            }
            set
            {
                if ((this._iTransactionNo != value))
                {
                    this._iTransactionNo = value;
                }
            }
        }

        [Column(Storage = "_TE_ID", DbType = "Int NOT NULL")]
        public int TE_ID
        {
            get
            {
                return this._TE_ID;
            }
            set
            {
                if ((this._TE_ID != value))
                {
                    this._TE_ID = value;
                }
            }
        }
    }
}
