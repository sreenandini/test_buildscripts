using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using System.Windows;
using Audit.Transport;

namespace BMC.Business.CashDeskOperator
{
    #region Cash Dispenser Interface

    public delegate void BmcCashDispenserFinishedHandler(IBmcCashDispenser sender, IBmcCashDispenseresult result);
    public delegate void BmcCashDispenserStatusChangeHandler(IBmcCashDispenser sender, string Status);
    public interface IBmcCashDispenseresult {
         Result Result { get; set; }
    }

    public interface IBmcCashDispenser
    {
        ModuleName _Ctype { get; set; }
        UIElement ParentElement { get; set; }
     
        Result Dispense(string ValidationNo, string AssetNo, int Amount);
        event BmcCashDispenserFinishedHandler Finished;
        event BmcCashDispenserStatusChangeHandler StatusChanged;
    }

    #endregion

   

    #region Result
    public class Result
    {
        
        public bool IsSuccess { get; set; }
        public Error error { get; set; }
    }
    #endregion

    #region Error
    public class Error
    {
        public int? Code { get; set; }
        public string Message { get; set; }
        public string MessageType { get; set; }
    }
    #endregion

    #region Abstract Base BmcCashDispenserBase
    public abstract class BmcCashDispenserBase : IBmcCashDispenser
    {
        protected BmcCashDispenserBase(UIElement parent, ModuleName Ctype)
        {
            this.ParentElement = parent;
            this._Ctype = Ctype;
          
        }

        #region IBmcCashDispenser Members
        public ModuleName _Ctype { get; set; }
       
        public UIElement ParentElement
        {
            get;
            set;
        }

        public abstract Result Dispense(string ValidationNo, string AssetNo, int Amount);

        public abstract event BmcCashDispenserFinishedHandler Finished;
        public abstract event BmcCashDispenserStatusChangeHandler StatusChanged;
        #endregion
    }
    #endregion

}
