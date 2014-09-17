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

        [Function(Name = "dbo.rsp_BPSiteJackpot")]
        public ISingleResult<rsp_BPSiteJackpotResult>BPSiteJackpot([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_BPSiteJackpotResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_BPSitePricePerPlay")]
        public ISingleResult<rsp_BPSitePricePerPlayResult>BPSitePricePerPlay([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_BPSitePricePerPlayResult>)(result.ReturnValue));
        }
     

        [Function(Name = "dbo.rsp_BPSitePercentagePayout")]
        public ISingleResult<rsp_BPSitePercentagePayoutResult> BPSitePercentagePayout([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_BPSitePercentagePayoutResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBPTermsgroup")]
        public ISingleResult<rsp_GetBPTermsgroupResult>GetBPTermsgroup([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_GetBPTermsgroupResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBPDefaultDetails")]
        public ISingleResult<rsp_GetBPDefaultDetailsResult>GetBPDefaultDetails([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_GetBPDefaultDetailsResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_getBPAccessKey")]
        public ISingleResult<rsp_getBPAccessKeyResult>getBPAccessKey([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_getBPAccessKeyResult>)(result.ReturnValue));
        }
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateBarPositionTermsGroup")]
        public int UpdateBarPositionTermsGroup(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PricePerPlay", DbType = "Bit")] System.Nullable<bool> pricePerPlay,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PricePerPlayValue", DbType = "Int")] System.Nullable<int> pricePerPlayValue,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Jackpot", DbType = "Bit")] System.Nullable<bool> jackpot,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "JackpotValue", DbType = "Int")] System.Nullable<int> jackpotValue,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Payout", DbType = "Bit")] System.Nullable<bool> payout,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "PayoutValue", DbType = "Int")] System.Nullable<int> payoutValue,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccessKey", DbType = "Bit")] System.Nullable<bool> accessKey,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "AccessKeyValue", DbType = "Int")] System.Nullable<int> accessKeyValue,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroup", DbType = "Bit")] System.Nullable<bool> termsGroup,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroupId", DbType = "Int")] System.Nullable<int> termsGroupId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroupPastId", DbType = "Int")] System.Nullable<int> termsGroupPastId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroupFutureId", DbType = "Int")] System.Nullable<int> termsGroupFutureId,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroupChangeOverPastDate", DbType = "DateTime")] System.Nullable<System.DateTime> termsGroupChangeOverPastDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsGroupChangeOverFutureDate", DbType = "DateTime")] System.Nullable<System.DateTime> termsGroupChangeOverFutureDate,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Audit_User_ID", DbType = "Int")] System.Nullable<int> audit_User_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Audit_User_Name", DbType = "VarChar(50)")] string audit_User_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Audit_ModuleName", DbType = "VarChar(50)")] string audit_ModuleName,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Audit_ModuleID", DbType = "Int")] System.Nullable<int> audit_ModuleID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID, siteID, pricePerPlay, pricePerPlayValue, jackpot, jackpotValue, payout, payoutValue, accessKey, accessKeyValue, termsGroup, termsGroupId, termsGroupPastId, termsGroupFutureId, termsGroupChangeOverPastDate, termsGroupChangeOverFutureDate, audit_User_ID, audit_User_Name, audit_ModuleName, audit_ModuleID);
            return ((int)(result.ReturnValue));
        }

	}
    public partial class rsp_BPSitePercentagePayoutResult
    {

        private string _Site_Percentage_Payout;

        public rsp_BPSitePercentagePayoutResult()
        {
        }

        [Column(Storage = "_Site_Percentage_Payout", DbType = "VarChar(50)")]
        public string Site_Percentage_Payout
        {
            get
            {
                return this._Site_Percentage_Payout;
            }
            set
            {
                if ((this._Site_Percentage_Payout != value))
                {
                    this._Site_Percentage_Payout = value;
                }
            }
        }
    }
    public partial class rsp_GetBPTermsgroupResult
    {

        private System.Nullable<int> _Terms_Group_ID;

        private string _Terms_Group_Name;

        public rsp_GetBPTermsgroupResult()
        {
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Name", DbType = "VarChar(50)")]
        public string Terms_Group_Name
        {
            get
            {
                return this._Terms_Group_Name;
            }
            set
            {
                if ((this._Terms_Group_Name != value))
                {
                    this._Terms_Group_Name = value;
                }
            }
        }
    }

    

    public partial class rsp_BPSiteJackpotResult
    {

        private string _Site_Jackpot;

        public rsp_BPSiteJackpotResult()
        {
        }

        [Column(Storage = "_Site_Jackpot", DbType = "VarChar(50)")]
        public string Site_Jackpot
        {
            get
            {
                return this._Site_Jackpot;
            }
            set
            {
                if ((this._Site_Jackpot != value))
                {
                    this._Site_Jackpot = value;
                }
            }
        }
    }

    public partial class rsp_BPSitePricePerPlayResult
    {

        private string _Site_Price_Per_Play;

        public rsp_BPSitePricePerPlayResult()
        {
        }

        [Column(Storage = "_Site_Price_Per_Play", DbType = "VarChar(50)")]
        public string Site_Price_Per_Play
        {
            get
            {
                return this._Site_Price_Per_Play;
            }
            set
            {
                if ((this._Site_Price_Per_Play != value))
                {
                    this._Site_Price_Per_Play = value;
                }
            }
        }
    }
    public partial class rsp_GetBPDefaultDetailsResult
    {

        private int _Bar_Position_Invoice_Period;

        private string _Site_Code;

        private string _Site_Name;

        private System.Nullable<int> _Site_ID;

        private string _Bar_Position_Name;

        private string _Bar_Position_Company_Position_Code;

        private string _Bar_Position_End_Date;

        private System.Nullable<int> _Machine_Type_ID;

        private string _Bar_Position_Location;

        private System.Nullable<int> _Zone_ID;

        private string _Bar_Position_Supplier_Site_Code;

        private string _Bar_Position_Supplier_Position_Code;

        private string _Bar_Position_Image_Reference;

        private System.Nullable<bool> _Bar_Position_Price_Per_Play_Default;

        private string _Bar_Position_Price_Per_Play;

        private System.Nullable<bool> _Bar_Position_Jackpot_Default;

        private string _Bar_Position_Jackpot;

        private System.Nullable<bool> _Bar_Position_Percentage_Payout_Default;

        private string _Bar_Position_Percentage_Payout;

        private System.Nullable<bool> _Terms_Group_ID_Default;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<bool> _Access_Key_ID_Default;

        private System.Nullable<int> _Access_Key_ID;

        private System.Nullable<int> _Depot_ID;

        private string _Depot_Name;

        private System.Nullable<int> _Operator_ID;

        private string _Operator_Name;

        private string _Machine_Name;

        private string _Machine_BACTA_Code;

        private string _Machine_Stock_No;

        private string _Installation_End_Date;

        private System.Nullable<int> _Bar_Position_Category;

        private System.Nullable<bool> _Bar_Position_Override_Licence;

        private System.Nullable<bool> _Bar_Position_Override_Shares;

        private System.Nullable<bool> _Bar_Position_Override_Rent;

        private System.Nullable<float> _Bar_Position_Rent;

        private System.Nullable<float> _Bar_Position_Rent_Previous;

        private System.Nullable<float> _Bar_Position_Rent_Future;

        private string _Bar_Position_Rent_Past_Date;

        private string _Bar_Position_Rent_Future_Date;

        private System.Nullable<float> _Bar_Position_Supplier_Share;

        private System.Nullable<float> _Bar_Position_Site_Share;

        private System.Nullable<float> _Bar_Position_Owners_Share;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share;

        private System.Nullable<float> _Bar_Position_Supplier_Share_Previous;

        private System.Nullable<float> _Bar_Position_Site_Share_Previous;

        private System.Nullable<float> _Bar_Position_Owners_Share_Previous;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share_Previous;

        private System.Nullable<int> _Bar_Position_Collection_Period;

        private string _Terms_Group_Past_Changeover_Date;

        private System.Nullable<int> _Terms_Group_Past_ID;

        private System.Nullable<float> _Bar_Position_Supplier_Share_Future;

        private System.Nullable<float> _Bar_Position_Site_Share_Future;

        private System.Nullable<float> _Bar_Position_Owners_Share_Future;

        private System.Nullable<float> _Bar_Position_Secondary_Owners_Share_Future;

        private string _Bar_Position_Share_Past_Date;

        private string _Bar_Position_Share_Future_Date;

        private System.Nullable<float> _Bar_Position_Licence_Charge;

        private System.Nullable<float> _Bar_Position_Licence_Previous;

        private System.Nullable<float> _Bar_Position_Licence_Future;

        private string _Bar_Position_Licence_Past_Date;

        private string _Bar_Position_Licence_Future_Date;

        private System.Nullable<bool> _Bar_Position_Use_Terms;

        private string _Bar_Position_Collection_Day;

        private System.Nullable<bool> _Bar_Position_Use_Site_Share_For_Secondary_Brewery;

        private string _Terms_Group_Changeover_Date;

        private System.Nullable<int> _Terms_Group_Future_ID;

        private System.Nullable<bool> _Bar_Position_Prize_LOS;

        private System.Nullable<int> _Bar_Position_Rent_Schedule_ID;

        private System.Nullable<int> _Bar_Position_Share_Schedule_ID;

        private System.Nullable<bool> _Bar_Position_Override_Rent_Schedule;

        private System.Nullable<bool> _Bar_Position_Override_Share_Schedule;

        private System.Nullable<bool> _Bar_Position_Override_Rent_From_Schedule_To_Rent;

        private System.Nullable<bool> _Bar_Position_Override_Rent_From_Rent_To_Schedule;

        private string _Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

        private string _Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

        private System.Nullable<int> _Bar_Position_Rent_Schedule_ID_From;

        private System.Nullable<bool> _Bar_Position_Disable_EDI_Export;

        public rsp_GetBPDefaultDetailsResult()
        {
        }

        [Column(Storage = "_Bar_Position_Invoice_Period", DbType = "Int NOT NULL")]
        public int Bar_Position_Invoice_Period
        {
            get
            {
                return this._Bar_Position_Invoice_Period;
            }
            set
            {
                if ((this._Bar_Position_Invoice_Period != value))
                {
                    this._Bar_Position_Invoice_Period = value;
                }
            }
        }

        [Column(Storage = "_Site_Code", DbType = "VarChar(50)")]
        public string Site_Code
        {
            get
            {
                return this._Site_Code;
            }
            set
            {
                if ((this._Site_Code != value))
                {
                    this._Site_Code = value;
                }
            }
        }

        [Column(Storage = "_Site_Name", DbType = "VarChar(50)")]
        public string Site_Name
        {
            get
            {
                return this._Site_Name;
            }
            set
            {
                if ((this._Site_Name != value))
                {
                    this._Site_Name = value;
                }
            }
        }

        [Column(Storage = "_Site_ID", DbType = "Int")]
        public System.Nullable<int> Site_ID
        {
            get
            {
                return this._Site_ID;
            }
            set
            {
                if ((this._Site_ID != value))
                {
                    this._Site_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Name", DbType = "VarChar(50)")]
        public string Bar_Position_Name
        {
            get
            {
                return this._Bar_Position_Name;
            }
            set
            {
                if ((this._Bar_Position_Name != value))
                {
                    this._Bar_Position_Name = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Company_Position_Code", DbType = "VarChar(6)")]
        public string Bar_Position_Company_Position_Code
        {
            get
            {
                return this._Bar_Position_Company_Position_Code;
            }
            set
            {
                if ((this._Bar_Position_Company_Position_Code != value))
                {
                    this._Bar_Position_Company_Position_Code = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_End_Date", DbType = "VarChar(30)")]
        public string Bar_Position_End_Date
        {
            get
            {
                return this._Bar_Position_End_Date;
            }
            set
            {
                if ((this._Bar_Position_End_Date != value))
                {
                    this._Bar_Position_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Machine_Type_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Location", DbType = "VarChar(50)")]
        public string Bar_Position_Location
        {
            get
            {
                return this._Bar_Position_Location;
            }
            set
            {
                if ((this._Bar_Position_Location != value))
                {
                    this._Bar_Position_Location = value;
                }
            }
        }

        [Column(Storage = "_Zone_ID", DbType = "Int")]
        public System.Nullable<int> Zone_ID
        {
            get
            {
                return this._Zone_ID;
            }
            set
            {
                if ((this._Zone_ID != value))
                {
                    this._Zone_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Supplier_Site_Code", DbType = "VarChar(8)")]
        public string Bar_Position_Supplier_Site_Code
        {
            get
            {
                return this._Bar_Position_Supplier_Site_Code;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Site_Code != value))
                {
                    this._Bar_Position_Supplier_Site_Code = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Supplier_Position_Code", DbType = "VarChar(6)")]
        public string Bar_Position_Supplier_Position_Code
        {
            get
            {
                return this._Bar_Position_Supplier_Position_Code;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Position_Code != value))
                {
                    this._Bar_Position_Supplier_Position_Code = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Image_Reference", DbType = "VarChar(50)")]
        public string Bar_Position_Image_Reference
        {
            get
            {
                return this._Bar_Position_Image_Reference;
            }
            set
            {
                if ((this._Bar_Position_Image_Reference != value))
                {
                    this._Bar_Position_Image_Reference = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Price_Per_Play_Default", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Price_Per_Play_Default
        {
            get
            {
                return this._Bar_Position_Price_Per_Play_Default;
            }
            set
            {
                if ((this._Bar_Position_Price_Per_Play_Default != value))
                {
                    this._Bar_Position_Price_Per_Play_Default = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Price_Per_Play", DbType = "VarChar(50)")]
        public string Bar_Position_Price_Per_Play
        {
            get
            {
                return this._Bar_Position_Price_Per_Play;
            }
            set
            {
                if ((this._Bar_Position_Price_Per_Play != value))
                {
                    this._Bar_Position_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Jackpot_Default", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Jackpot_Default
        {
            get
            {
                return this._Bar_Position_Jackpot_Default;
            }
            set
            {
                if ((this._Bar_Position_Jackpot_Default != value))
                {
                    this._Bar_Position_Jackpot_Default = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Jackpot", DbType = "VarChar(50)")]
        public string Bar_Position_Jackpot
        {
            get
            {
                return this._Bar_Position_Jackpot;
            }
            set
            {
                if ((this._Bar_Position_Jackpot != value))
                {
                    this._Bar_Position_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Percentage_Payout_Default", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Percentage_Payout_Default
        {
            get
            {
                return this._Bar_Position_Percentage_Payout_Default;
            }
            set
            {
                if ((this._Bar_Position_Percentage_Payout_Default != value))
                {
                    this._Bar_Position_Percentage_Payout_Default = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Percentage_Payout", DbType = "VarChar(50)")]
        public string Bar_Position_Percentage_Payout
        {
            get
            {
                return this._Bar_Position_Percentage_Payout;
            }
            set
            {
                if ((this._Bar_Position_Percentage_Payout != value))
                {
                    this._Bar_Position_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Terms_Group_ID_Default
        {
            get
            {
                return this._Terms_Group_ID_Default;
            }
            set
            {
                if ((this._Terms_Group_ID_Default != value))
                {
                    this._Terms_Group_ID_Default = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_ID_Default", DbType = "Bit")]
        public System.Nullable<bool> Access_Key_ID_Default
        {
            get
            {
                return this._Access_Key_ID_Default;
            }
            set
            {
                if ((this._Access_Key_ID_Default != value))
                {
                    this._Access_Key_ID_Default = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public System.Nullable<int> Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this._Access_Key_ID = value;
                }
            }
        }

        [Column(Storage = "_Depot_ID", DbType = "Int")]
        public System.Nullable<int> Depot_ID
        {
            get
            {
                return this._Depot_ID;
            }
            set
            {
                if ((this._Depot_ID != value))
                {
                    this._Depot_ID = value;
                }
            }
        }

        [Column(Storage = "_Depot_Name", DbType = "VarChar(50)")]
        public string Depot_Name
        {
            get
            {
                return this._Depot_Name;
            }
            set
            {
                if ((this._Depot_Name != value))
                {
                    this._Depot_Name = value;
                }
            }
        }

        [Column(Storage = "_Operator_ID", DbType = "Int")]
        public System.Nullable<int> Operator_ID
        {
            get
            {
                return this._Operator_ID;
            }
            set
            {
                if ((this._Operator_ID != value))
                {
                    this._Operator_ID = value;
                }
            }
        }

        [Column(Storage = "_Operator_Name", DbType = "VarChar(50)")]
        public string Operator_Name
        {
            get
            {
                return this._Operator_Name;
            }
            set
            {
                if ((this._Operator_Name != value))
                {
                    this._Operator_Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_Name", DbType = "VarChar(50)")]
        public string Machine_Name
        {
            get
            {
                return this._Machine_Name;
            }
            set
            {
                if ((this._Machine_Name != value))
                {
                    this._Machine_Name = value;
                }
            }
        }

        [Column(Storage = "_Machine_BACTA_Code", DbType = "VarChar(50)")]
        public string Machine_BACTA_Code
        {
            get
            {
                return this._Machine_BACTA_Code;
            }
            set
            {
                if ((this._Machine_BACTA_Code != value))
                {
                    this._Machine_BACTA_Code = value;
                }
            }
        }

        [Column(Storage = "_Machine_Stock_No", DbType = "VarChar(50)")]
        public string Machine_Stock_No
        {
            get
            {
                return this._Machine_Stock_No;
            }
            set
            {
                if ((this._Machine_Stock_No != value))
                {
                    this._Machine_Stock_No = value;
                }
            }
        }

        [Column(Storage = "_Installation_End_Date", DbType = "VarChar(30)")]
        public string Installation_End_Date
        {
            get
            {
                return this._Installation_End_Date;
            }
            set
            {
                if ((this._Installation_End_Date != value))
                {
                    this._Installation_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Category", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Category
        {
            get
            {
                return this._Bar_Position_Category;
            }
            set
            {
                if ((this._Bar_Position_Category != value))
                {
                    this._Bar_Position_Category = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Licence", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Licence
        {
            get
            {
                return this._Bar_Position_Override_Licence;
            }
            set
            {
                if ((this._Bar_Position_Override_Licence != value))
                {
                    this._Bar_Position_Override_Licence = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Shares", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Shares
        {
            get
            {
                return this._Bar_Position_Override_Shares;
            }
            set
            {
                if ((this._Bar_Position_Override_Shares != value))
                {
                    this._Bar_Position_Override_Shares = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent
        {
            get
            {
                return this._Bar_Position_Override_Rent;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent != value))
                {
                    this._Bar_Position_Override_Rent = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent
        {
            get
            {
                return this._Bar_Position_Rent;
            }
            set
            {
                if ((this._Bar_Position_Rent != value))
                {
                    this._Bar_Position_Rent = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent_Previous
        {
            get
            {
                return this._Bar_Position_Rent_Previous;
            }
            set
            {
                if ((this._Bar_Position_Rent_Previous != value))
                {
                    this._Bar_Position_Rent_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Rent_Future
        {
            get
            {
                return this._Bar_Position_Rent_Future;
            }
            set
            {
                if ((this._Bar_Position_Rent_Future != value))
                {
                    this._Bar_Position_Rent_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Rent_Past_Date
        {
            get
            {
                return this._Bar_Position_Rent_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Rent_Past_Date != value))
                {
                    this._Bar_Position_Rent_Past_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Rent_Future_Date
        {
            get
            {
                return this._Bar_Position_Rent_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Rent_Future_Date != value))
                {
                    this._Bar_Position_Rent_Future_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share
        {
            get
            {
                return this._Bar_Position_Supplier_Share;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share != value))
                {
                    this._Bar_Position_Supplier_Share = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Site_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share
        {
            get
            {
                return this._Bar_Position_Site_Share;
            }
            set
            {
                if ((this._Bar_Position_Site_Share != value))
                {
                    this._Bar_Position_Site_Share = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Owners_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share
        {
            get
            {
                return this._Bar_Position_Owners_Share;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share != value))
                {
                    this._Bar_Position_Owners_Share = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Secondary_Owners_Share", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share != value))
                {
                    this._Bar_Position_Secondary_Owners_Share = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Supplier_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share_Previous
        {
            get
            {
                return this._Bar_Position_Supplier_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share_Previous != value))
                {
                    this._Bar_Position_Supplier_Share_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Site_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share_Previous
        {
            get
            {
                return this._Bar_Position_Site_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Site_Share_Previous != value))
                {
                    this._Bar_Position_Site_Share_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Owners_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share_Previous
        {
            get
            {
                return this._Bar_Position_Owners_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share_Previous != value))
                {
                    this._Bar_Position_Owners_Share_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Secondary_Owners_Share_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Previous
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share_Previous;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share_Previous != value))
                {
                    this._Bar_Position_Secondary_Owners_Share_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Collection_Period", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Collection_Period
        {
            get
            {
                return this._Bar_Position_Collection_Period;
            }
            set
            {
                if ((this._Bar_Position_Collection_Period != value))
                {
                    this._Bar_Position_Collection_Period = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Past_Changeover_Date", DbType = "VarChar(30)")]
        public string Terms_Group_Past_Changeover_Date
        {
            get
            {
                return this._Terms_Group_Past_Changeover_Date;
            }
            set
            {
                if ((this._Terms_Group_Past_Changeover_Date != value))
                {
                    this._Terms_Group_Past_Changeover_Date = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Past_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_Past_ID
        {
            get
            {
                return this._Terms_Group_Past_ID;
            }
            set
            {
                if ((this._Terms_Group_Past_ID != value))
                {
                    this._Terms_Group_Past_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Supplier_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Supplier_Share_Future
        {
            get
            {
                return this._Bar_Position_Supplier_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Supplier_Share_Future != value))
                {
                    this._Bar_Position_Supplier_Share_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Site_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Site_Share_Future
        {
            get
            {
                return this._Bar_Position_Site_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Site_Share_Future != value))
                {
                    this._Bar_Position_Site_Share_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Owners_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Owners_Share_Future
        {
            get
            {
                return this._Bar_Position_Owners_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Owners_Share_Future != value))
                {
                    this._Bar_Position_Owners_Share_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Secondary_Owners_Share_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Secondary_Owners_Share_Future
        {
            get
            {
                return this._Bar_Position_Secondary_Owners_Share_Future;
            }
            set
            {
                if ((this._Bar_Position_Secondary_Owners_Share_Future != value))
                {
                    this._Bar_Position_Secondary_Owners_Share_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Share_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Share_Past_Date
        {
            get
            {
                return this._Bar_Position_Share_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Share_Past_Date != value))
                {
                    this._Bar_Position_Share_Past_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Share_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Share_Future_Date
        {
            get
            {
                return this._Bar_Position_Share_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Share_Future_Date != value))
                {
                    this._Bar_Position_Share_Future_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Licence_Charge", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Charge
        {
            get
            {
                return this._Bar_Position_Licence_Charge;
            }
            set
            {
                if ((this._Bar_Position_Licence_Charge != value))
                {
                    this._Bar_Position_Licence_Charge = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Licence_Previous", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Previous
        {
            get
            {
                return this._Bar_Position_Licence_Previous;
            }
            set
            {
                if ((this._Bar_Position_Licence_Previous != value))
                {
                    this._Bar_Position_Licence_Previous = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Licence_Future", DbType = "Real")]
        public System.Nullable<float> Bar_Position_Licence_Future
        {
            get
            {
                return this._Bar_Position_Licence_Future;
            }
            set
            {
                if ((this._Bar_Position_Licence_Future != value))
                {
                    this._Bar_Position_Licence_Future = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Licence_Past_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Licence_Past_Date
        {
            get
            {
                return this._Bar_Position_Licence_Past_Date;
            }
            set
            {
                if ((this._Bar_Position_Licence_Past_Date != value))
                {
                    this._Bar_Position_Licence_Past_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Licence_Future_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Licence_Future_Date
        {
            get
            {
                return this._Bar_Position_Licence_Future_Date;
            }
            set
            {
                if ((this._Bar_Position_Licence_Future_Date != value))
                {
                    this._Bar_Position_Licence_Future_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Use_Terms", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Use_Terms
        {
            get
            {
                return this._Bar_Position_Use_Terms;
            }
            set
            {
                if ((this._Bar_Position_Use_Terms != value))
                {
                    this._Bar_Position_Use_Terms = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Collection_Day", DbType = "VarChar(30)")]
        public string Bar_Position_Collection_Day
        {
            get
            {
                return this._Bar_Position_Collection_Day;
            }
            set
            {
                if ((this._Bar_Position_Collection_Day != value))
                {
                    this._Bar_Position_Collection_Day = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Use_Site_Share_For_Secondary_Brewery", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Use_Site_Share_For_Secondary_Brewery
        {
            get
            {
                return this._Bar_Position_Use_Site_Share_For_Secondary_Brewery;
            }
            set
            {
                if ((this._Bar_Position_Use_Site_Share_For_Secondary_Brewery != value))
                {
                    this._Bar_Position_Use_Site_Share_For_Secondary_Brewery = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Changeover_Date", DbType = "VarChar(30)")]
        public string Terms_Group_Changeover_Date
        {
            get
            {
                return this._Terms_Group_Changeover_Date;
            }
            set
            {
                if ((this._Terms_Group_Changeover_Date != value))
                {
                    this._Terms_Group_Changeover_Date = value;
                }
            }
        }

        [Column(Storage = "_Terms_Group_Future_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_Future_ID
        {
            get
            {
                return this._Terms_Group_Future_ID;
            }
            set
            {
                if ((this._Terms_Group_Future_ID != value))
                {
                    this._Terms_Group_Future_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Prize_LOS", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Prize_LOS
        {
            get
            {
                return this._Bar_Position_Prize_LOS;
            }
            set
            {
                if ((this._Bar_Position_Prize_LOS != value))
                {
                    this._Bar_Position_Prize_LOS = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID
        {
            get
            {
                return this._Bar_Position_Rent_Schedule_ID;
            }
            set
            {
                if ((this._Bar_Position_Rent_Schedule_ID != value))
                {
                    this._Bar_Position_Rent_Schedule_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Share_Schedule_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Share_Schedule_ID
        {
            get
            {
                return this._Bar_Position_Share_Schedule_ID;
            }
            set
            {
                if ((this._Bar_Position_Share_Schedule_ID != value))
                {
                    this._Bar_Position_Share_Schedule_ID = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Rent_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_Schedule != value))
                {
                    this._Bar_Position_Override_Rent_Schedule = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Share_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Share_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Share_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Share_Schedule != value))
                {
                    this._Bar_Position_Override_Share_Schedule = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent_From_Schedule_To_Rent", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Schedule_To_Rent
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Schedule_To_Rent;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Schedule_To_Rent != value))
                {
                    this._Bar_Position_Override_Rent_From_Schedule_To_Rent = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent_From_Rent_To_Schedule", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Override_Rent_From_Rent_To_Schedule
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Rent_To_Schedule;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Rent_To_Schedule != value))
                {
                    this._Bar_Position_Override_Rent_From_Rent_To_Schedule = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent_From_Schedule_To_Rent_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Override_Rent_From_Schedule_To_Rent_Date
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date != value))
                {
                    this._Bar_Position_Override_Rent_From_Schedule_To_Rent_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Override_Rent_From_Rent_To_Schedule_Date", DbType = "VarChar(30)")]
        public string Bar_Position_Override_Rent_From_Rent_To_Schedule_Date
        {
            get
            {
                return this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;
            }
            set
            {
                if ((this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date != value))
                {
                    this._Bar_Position_Override_Rent_From_Rent_To_Schedule_Date = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Rent_Schedule_ID_From", DbType = "Int")]
        public System.Nullable<int> Bar_Position_Rent_Schedule_ID_From
        {
            get
            {
                return this._Bar_Position_Rent_Schedule_ID_From;
            }
            set
            {
                if ((this._Bar_Position_Rent_Schedule_ID_From != value))
                {
                    this._Bar_Position_Rent_Schedule_ID_From = value;
                }
            }
        }

        [Column(Storage = "_Bar_Position_Disable_EDI_Export", DbType = "Bit")]
        public System.Nullable<bool> Bar_Position_Disable_EDI_Export
        {
            get
            {
                return this._Bar_Position_Disable_EDI_Export;
            }
            set
            {
                if ((this._Bar_Position_Disable_EDI_Export != value))
                {
                    this._Bar_Position_Disable_EDI_Export = value;
                }
            }
        }
    }

    public partial class rsp_getBPAccessKeyResult
    {

        private System.Nullable<int> _Access_Key_ID;

        private string _Access_Key_Name;

        public rsp_getBPAccessKeyResult()
        {
        }

        [Column(Storage = "_Access_Key_ID", DbType = "Int")]
        public System.Nullable<int> Access_Key_ID
        {
            get
            {
                return this._Access_Key_ID;
            }
            set
            {
                if ((this._Access_Key_ID != value))
                {
                    this._Access_Key_ID = value;
                }
            }
        }

        [Column(Storage = "_Access_Key_Name", DbType = "VarChar(50)")]
        public string Access_Key_Name
        {
            get
            {
                return this._Access_Key_Name;
            }
            set
            {
                if ((this._Access_Key_Name != value))
                {
                    this._Access_Key_Name = value;
                }
            }
        }
    }

        }
