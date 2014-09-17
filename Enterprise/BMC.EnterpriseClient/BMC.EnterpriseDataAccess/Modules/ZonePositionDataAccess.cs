using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Reflection;
using System.Data;
using BMC.DataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Data.SqlClient;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        //Check whether the Barposition name exist or not
        [Function(Name = "dbo.rsp_GetBarPosition")]
        public int rsp_GetBarPosition([Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId, [Parameter(Name = "BarPositionName", DbType = "VarChar(50)")] string barPositionName, [Parameter(Name = "CheckCount", DbType = "Int")] ref System.Nullable<int> checkCount, [Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId, barPositionName, checkCount, barPositionID);
            checkCount = ((System.Nullable<int>)(result.GetParameterValue(2)));
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.rsp_GetSite")]
        public ISingleResult<rsp_GetSiteResult> rsp_GetSite([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetSiteResult>)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetLatestBarPositionID")]
        public ISingleResult<rsp_GetLatestBarPositionIDResult> rsp_GetLatestBarPositionID()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetLatestBarPositionIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBarPositionExtension")]
        public ISingleResult<rsp_GetBarPositionExtensionResult> rsp_GetBarPositionExtension([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_GetBarPositionExtensionResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_InsertOrUpdateBarPositionExtension")]
        public int InsertOrUpdateBarPositionExtension([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID, [Parameter(Name = "Image", DbType = "Image")] System.Data.Linq.Binary image, [Parameter(DbType = "Bit")] System.Nullable<bool> isDelete)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID, image, isDelete);
            return ((int)(result.ReturnValue));
        }

        public partial class rsp_GetBarPositionExtensionResult
        {

            private int _Bar_Position_ID;

            private System.Data.Linq.Binary _Bar_Position_Image;

            public rsp_GetBarPositionExtensionResult()
            {
            }

            [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
            public int Bar_Position_ID
            {
                get
                {
                    return this._Bar_Position_ID;
                }
                set
                {
                    if ((this._Bar_Position_ID != value))
                    {
                        this._Bar_Position_ID = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Image", DbType = "Image NOT NULL", CanBeNull = false)]
            public System.Data.Linq.Binary Bar_Position_Image
            {
                get
                {
                    return this._Bar_Position_Image;
                }
                set
                {
                    if ((this._Bar_Position_Image != value))
                    {
                        this._Bar_Position_Image = value;
                    }
                }
            }
        }
        public partial class rsp_GetBarPositionByIDResult
        {

            private int _Bar_Position_ID;

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _Zone_ID;

            private System.Nullable<int> _Access_Key_ID;

            private System.Nullable<bool> _Access_Key_ID_Default;

            private System.Nullable<int> _Terms_Group_ID;

            private string _Terms_Group_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Future_ID;

            private string _Terms_Group_Past_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Past_ID;

            private System.Nullable<bool> _Terms_Group_ID_Default;

            private System.Nullable<int> _Duty_ID;

            private System.Nullable<int> _Depot_ID;

            private System.Nullable<int> _Machine_Type_ID;

            private string _Bar_Position_Name;

            private string _Bar_Position_Location;

            private string _Bar_Position_Start_Date;

            private string _Bar_Position_End_Date;

            private string _Bar_Position_Collection_Day;

            private string _Bar_Position_Price_Per_Play;

            private System.Nullable<bool> _Bar_Position_Price_Per_Play_Default;

            private string _Bar_Position_Jackpot;

            private System.Nullable<bool> _Bar_Position_Jackpot_Default;

            private string _Bar_Position_Percentage_Payout;

            private System.Nullable<bool> _Bar_Position_Percentage_Payout_Default;

            private string _Bar_Position_Last_Collection_Date;

            private string _Bar_Position_Collection_Rent_Paid_Until;

            private System.Nullable<int> _Bar_Position_Collection_Period;

            private string _Bar_Position_Supplier_AMEDIS_Code;

            private string _Bar_Position_Supplier_Depot_AMEDIS_Code;

            private string _Bar_Position_Supplier_Site_Code;

            private string _Bar_Position_Supplier_Position_Code;

            private string _Bar_Position_Supplier_Area;

            private string _Bar_Position_Supplier_Service_Area;

            private string _Bar_Position_Company_Position_Code;

            private System.Nullable<float> _Bar_Position_Company_Target;

            private System.Nullable<int> _Bar_Position_Collection_Frequency;

            private string _Bar_Position_Image_Reference;

            private System.Nullable<int> _Bar_Position_Machine_Type_AMEDIS_Code;

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

            private System.Nullable<bool> _Bar_Position_TX_Collection;

            private System.Nullable<bool> _Bar_Position_TX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_Movement;

            private System.Nullable<bool> _Bar_Position_TX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_EDC;

            private System.Nullable<bool> _Bar_Position_TX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_TX_Format;

            private System.Nullable<bool> _Bar_Position_TX_Format_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Collection;

            private System.Nullable<bool> _Bar_Position_RX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Movement;

            private System.Nullable<bool> _Bar_Position_RX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_EDC;

            private System.Nullable<bool> _Bar_Position_RX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_RX_Format;

            private System.Nullable<bool> _Bar_Position_RX_Format_Use_Default;

            private System.Nullable<float> _Bar_Position_Net_Target;

            private System.Nullable<int> _Bar_Position_Below_Net_Target_Counter;

            private System.Nullable<int> _Bar_Position_Below_Company_Target_Counter;

            private System.Nullable<bool> _Bar_Position_Security_Required;

            private System.Nullable<bool> _Bar_Position_Site_Has_Cashbox_Keys;

            private System.Nullable<bool> _Bar_Position_Site_Has_FreePlay_Access;

            private System.Nullable<bool> _Bar_Position_Override_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Shares;

            private System.Nullable<bool> _Bar_Position_Override_Licence;

            private System.Nullable<int> _Bar_Position_Category;

            private System.Nullable<float> _Bar_Position_PPL_Charge;

            private System.Nullable<float> _Bar_Position_PPL_Previous;

            private System.Nullable<float> _Bar_Position_PPL_Future;

            private string _Bar_Position_PPL_Past_Date;

            private string _Bar_Position_PPL_Future_Date;

            private System.Nullable<int> _Bar_Position_Float_Issued;

            private System.Nullable<int> _Bar_Position_Float_Recovered;

            private System.Nullable<bool> _Bar_Position_Use_Site_Share_For_Secondary_Brewery;

            private System.Nullable<bool> _Bar_Position_Prize_LOS;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID;

            private System.Nullable<int> _Bar_Position_Share_Schedule_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_Schedule;

            private System.Nullable<bool> _Bar_Position_Override_Share_Schedule;

            private System.Nullable<int> _Bar_Position_Last_Collection_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Schedule_To_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Rent_To_Schedule;

            private string _Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

            private string _Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID_From;

            private System.Nullable<bool> _Bar_Position_Disable_EDI_Export;

            private int _Bar_Position_Invoice_Period;

            private System.Nullable<int> _Bar_Position_Machine_Enabled;

            private System.Nullable<int> _Bar_Position_Note_Acceptor_Enabled;

            private System.Nullable<System.DateTime> _Bar_Position_Machine_Enabled_Date;

            public rsp_GetBarPositionByIDResult()
            {
            }

            [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
            public int Bar_Position_ID
            {
                get
                {
                    return this._Bar_Position_ID;
                }
                set
                {
                    if ((this._Bar_Position_ID != value))
                    {
                        this._Bar_Position_ID = value;
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

            [Column(Storage = "_Duty_ID", DbType = "Int")]
            public System.Nullable<int> Duty_ID
            {
                get
                {
                    return this._Duty_ID;
                }
                set
                {
                    if ((this._Duty_ID != value))
                    {
                        this._Duty_ID = value;
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

            [Column(Storage = "_Bar_Position_Start_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Start_Date
            {
                get
                {
                    return this._Bar_Position_Start_Date;
                }
                set
                {
                    if ((this._Bar_Position_Start_Date != value))
                    {
                        this._Bar_Position_Start_Date = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Last_Collection_Date
            {
                get
                {
                    return this._Bar_Position_Last_Collection_Date;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_Date != value))
                    {
                        this._Bar_Position_Last_Collection_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")]
            public string Bar_Position_Collection_Rent_Paid_Until
            {
                get
                {
                    return this._Bar_Position_Collection_Rent_Paid_Until;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Rent_Paid_Until != value))
                    {
                        this._Bar_Position_Collection_Rent_Paid_Until = value;
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

            [Column(Storage = "_Bar_Position_Supplier_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_AMEDIS_Code = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Depot_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_Depot_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_Depot_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Depot_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_Depot_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_Supplier_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Area != value))
                    {
                        this._Bar_Position_Supplier_Area = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Service_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Service_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Service_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Service_Area != value))
                    {
                        this._Bar_Position_Supplier_Service_Area = value;
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

            [Column(Storage = "_Bar_Position_Company_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Company_Target
            {
                get
                {
                    return this._Bar_Position_Company_Target;
                }
                set
                {
                    if ((this._Bar_Position_Company_Target != value))
                    {
                        this._Bar_Position_Company_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Frequency", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Collection_Frequency
            {
                get
                {
                    return this._Bar_Position_Collection_Frequency;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Frequency != value))
                    {
                        this._Bar_Position_Collection_Frequency = value;
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

            [Column(Storage = "_Bar_Position_Machine_Type_AMEDIS_Code", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Type_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Machine_Type_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Type_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Machine_Type_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_TX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection
            {
                get
                {
                    return this._Bar_Position_TX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection != value))
                    {
                        this._Bar_Position_TX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_TX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement
            {
                get
                {
                    return this._Bar_Position_TX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement != value))
                    {
                        this._Bar_Position_TX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_TX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC
            {
                get
                {
                    return this._Bar_Position_TX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC != value))
                    {
                        this._Bar_Position_TX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_TX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_TX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_TX_Format
            {
                get
                {
                    return this._Bar_Position_TX_Format;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format != value))
                    {
                        this._Bar_Position_TX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format_Use_Default != value))
                    {
                        this._Bar_Position_TX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection
            {
                get
                {
                    return this._Bar_Position_RX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection != value))
                    {
                        this._Bar_Position_RX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_RX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement
            {
                get
                {
                    return this._Bar_Position_RX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement != value))
                    {
                        this._Bar_Position_RX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_RX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC
            {
                get
                {
                    return this._Bar_Position_RX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC != value))
                    {
                        this._Bar_Position_RX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_RX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_RX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_RX_Format
            {
                get
                {
                    return this._Bar_Position_RX_Format;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format != value))
                    {
                        this._Bar_Position_RX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format_Use_Default != value))
                    {
                        this._Bar_Position_RX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Net_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Net_Target
            {
                get
                {
                    return this._Bar_Position_Net_Target;
                }
                set
                {
                    if ((this._Bar_Position_Net_Target != value))
                    {
                        this._Bar_Position_Net_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Net_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Net_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Net_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Net_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Net_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Company_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Company_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Company_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Company_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Company_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Security_Required", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Security_Required
            {
                get
                {
                    return this._Bar_Position_Security_Required;
                }
                set
                {
                    if ((this._Bar_Position_Security_Required != value))
                    {
                        this._Bar_Position_Security_Required = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_Cashbox_Keys", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_Cashbox_Keys
            {
                get
                {
                    return this._Bar_Position_Site_Has_Cashbox_Keys;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_Cashbox_Keys != value))
                    {
                        this._Bar_Position_Site_Has_Cashbox_Keys = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_FreePlay_Access", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_FreePlay_Access
            {
                get
                {
                    return this._Bar_Position_Site_Has_FreePlay_Access;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_FreePlay_Access != value))
                    {
                        this._Bar_Position_Site_Has_FreePlay_Access = value;
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

            [Column(Storage = "_Bar_Position_PPL_Charge", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Charge
            {
                get
                {
                    return this._Bar_Position_PPL_Charge;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Charge != value))
                    {
                        this._Bar_Position_PPL_Charge = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Previous", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Previous
            {
                get
                {
                    return this._Bar_Position_PPL_Previous;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Previous != value))
                    {
                        this._Bar_Position_PPL_Previous = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Future
            {
                get
                {
                    return this._Bar_Position_PPL_Future;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future != value))
                    {
                        this._Bar_Position_PPL_Future = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Past_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Past_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Past_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Past_Date != value))
                    {
                        this._Bar_Position_PPL_Past_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Future_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Future_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future_Date != value))
                    {
                        this._Bar_Position_PPL_Future_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Issued", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Issued
            {
                get
                {
                    return this._Bar_Position_Float_Issued;
                }
                set
                {
                    if ((this._Bar_Position_Float_Issued != value))
                    {
                        this._Bar_Position_Float_Issued = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Recovered", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Recovered
            {
                get
                {
                    return this._Bar_Position_Float_Recovered;
                }
                set
                {
                    if ((this._Bar_Position_Float_Recovered != value))
                    {
                        this._Bar_Position_Float_Recovered = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_ID", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Last_Collection_ID
            {
                get
                {
                    return this._Bar_Position_Last_Collection_ID;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_ID != value))
                    {
                        this._Bar_Position_Last_Collection_ID = value;
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

            [Column(Storage = "_Bar_Position_Machine_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Enabled
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled != value))
                    {
                        this._Bar_Position_Machine_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Note_Acceptor_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Note_Acceptor_Enabled
            {
                get
                {
                    return this._Bar_Position_Note_Acceptor_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Note_Acceptor_Enabled != value))
                    {
                        this._Bar_Position_Note_Acceptor_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Machine_Enabled_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Bar_Position_Machine_Enabled_Date
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled_Date;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled_Date != value))
                    {
                        this._Bar_Position_Machine_Enabled_Date = value;
                    }
                }
            }
        }
        [Function(Name = "dbo.USP_InsertBarPosition")]
        public int USP_InsertBarPosition(
                    [Parameter(Name = "Bar_Position_Name", DbType = "VarChar(50)")] string bar_Position_Name,
                    [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID,
                    [Parameter(Name = "Bar_Position_Start_Date", DbType = "VarChar(30)")] string bar_Position_Start_Date,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Bar_Position_Rent_Past_Date", DbType = "VarChar(30)")] string bar_Position_Rent_Past_Date,
                    [Parameter(Name = "Bar_Position_Share_Past_Date", DbType = "VarChar(30)")] string bar_Position_Share_Past_Date,
                    [Parameter(Name = "Bar_Position_Licence_Past_Date", DbType = "VarChar(30)")] string bar_Position_Licence_Past_Date,
                    [Parameter(Name = "Bar_Position_Rent_Future_Date", DbType = "VarChar(30)")] string bar_Position_Rent_Future_Date,
                    [Parameter(Name = "Bar_Position_Share_Future_Date", DbType = "VarChar(30)")] string bar_Position_Share_Future_Date,
                    [Parameter(Name = "Bar_Position_Licence_Future_Date", DbType = "VarChar(30)")] string bar_Position_Licence_Future_Date,
                    [Parameter(Name = "Bar_Position_Use_Terms", DbType = "Bit")] System.Nullable<bool> bar_Position_Use_Terms,
                    [Parameter(Name = "Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")] string bar_Position_Last_Collection_Date,
                    [Parameter(Name = "Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")] string bar_Position_Collection_Rent_Paid_Until,
                    [Parameter(Name = "Bar_Position_Price_Per_Play", DbType = "VarChar(50)")] string bar_Position_Price_Per_Play,
                    [Parameter(Name = "Bar_Position_Price_Per_Play_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Price_Per_Play_Default,
                    [Parameter(Name = "Bar_Position_Jackpot", DbType = "VarChar(50)")] string bar_Position_Jackpot,
                    [Parameter(Name = "Bar_Position_Jackpot_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Jackpot_Default,
                    [Parameter(Name = "Bar_Position_Percentage_Payout", DbType = "VarChar(50)")] string bar_Position_Percentage_Payout,
                    [Parameter(Name = "Bar_Position_Percentage_Payout_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Percentage_Payout_Default,
                    [Parameter(Name = "Access_Key_ID", DbType = "Int")] System.Nullable<int> access_Key_ID,
                    [Parameter(Name = "Access_Key_ID_Default", DbType = "Bit")] System.Nullable<bool> access_Key_ID_Default,
                    [Parameter(Name = "Terms_Group_ID", DbType = "Int")] System.Nullable<int> terms_Group_ID,
                    [Parameter(Name = "Terms_Group_ID_Default", DbType = "Bit")] System.Nullable<bool> terms_Group_ID_Default,
                    [Parameter(Name = "BarPositionId", DbType = "Int")] ref System.Nullable<int> barPositionId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_Name, site_ID, bar_Position_Start_Date, depot_ID, bar_Position_Rent_Past_Date, bar_Position_Share_Past_Date, bar_Position_Licence_Past_Date, bar_Position_Rent_Future_Date, bar_Position_Share_Future_Date, bar_Position_Licence_Future_Date, bar_Position_Use_Terms, bar_Position_Last_Collection_Date, bar_Position_Collection_Rent_Paid_Until, bar_Position_Price_Per_Play, bar_Position_Price_Per_Play_Default, bar_Position_Jackpot, bar_Position_Jackpot_Default, bar_Position_Percentage_Payout, bar_Position_Percentage_Payout_Default, access_Key_ID, access_Key_ID_Default, terms_Group_ID, terms_Group_ID_Default, barPositionId);
            barPositionId = ((System.Nullable<int>)(result.GetParameterValue(23)));
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_CreateBarPositionTemplate")]
        public ISingleResult<rsp_CreateBarPositionTemplateResult> rsp_CreateBarPositionTemplate([Parameter(Name = "BarPositionTemplateID", DbType = "Int")] System.Nullable<int> barPositionTemplateID, [Parameter(Name = "StartBarPositionName", DbType = "VarChar(50)")] string startBarPositionName, [Parameter(Name = "LastBarPositionName", DbType = "VarChar(50)")] string lastBarPositionName)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionTemplateID, startBarPositionName, lastBarPositionName);
            return ((ISingleResult<rsp_CreateBarPositionTemplateResult>)(result.ReturnValue));
        }

        //[Function(Name = "dbo.usp_InsertNewZone")]
        //public int usp_InsertNewZone([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "Standard_Opening_Hours_ID", DbType = "Int")] System.Nullable<int> standard_Opening_Hours_ID, [Parameter(Name = "ZoneName", DbType = "VarChar(50)")] string zoneName, [Parameter(Name = "ZoneID", DbType = "Int")] ref System.Nullable<int> zoneID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, standard_Opening_Hours_ID, zoneName, zoneID);
        //    zoneID = ((System.Nullable<int>)(result.GetParameterValue(3)));
        //    return ((int)(result.ReturnValue));
        //   // return ((ISingleResult<usp_InsertNewZoneResult>)(result.ReturnValue));
        //}
        [Function(Name = "dbo.usp_InsertNewZone")]
        public int usp_InsertNewZone([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "Standard_Opening_Hours_ID", DbType = "Int")] System.Nullable<int> standard_Opening_Hours_ID, [Parameter(Name = "ZoneName", DbType = "VarChar(50)")] string zoneName, [Parameter(Name = "PromotionEnabled", DbType = "Bit")] System.Nullable<bool> promotionEnabled, [Parameter(Name = "ZoneID", DbType = "Int")] ref System.Nullable<int> zoneID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID, standard_Opening_Hours_ID, zoneName, promotionEnabled, zoneID);
            zoneID = ((System.Nullable<int>)(result.GetParameterValue(4)));
            return ((int)(result.ReturnValue));
          //  return ((ISingleResult<usp_InsertNewZoneResult>)(result.ReturnValue));
        }
        //[Function(Name = "dbo.usp_UpdateZoneName")]
        //public int usp_UpdateZoneName([Parameter(Name = "ZoneName", DbType = "VarChar(50)")] string zoneName, [Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID, [Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "OpenHourID", DbType = "Int")] System.Nullable<int> openHourID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneName, zoneID, siteID, openHourID);
        //    return ((int)(result.ReturnValue));
        //}
        [Function(Name = "dbo.usp_UpdateZoneName")]
        public int usp_UpdateZoneName([Parameter(Name = "ZoneName", DbType = "VarChar(50)")] string zoneName, [Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID, [Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID, [Parameter(Name = "PromotionEnabled", DbType = "Bit")] System.Nullable<bool> promotionEnabled, [Parameter(Name = "OpenHourID", DbType = "Int")] System.Nullable<int> openHourID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneName, zoneID, siteID, promotionEnabled, openHourID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.rsp_GetZoneDetailsBySiteID")]
        public ISingleResult<rsp_GetZoneDetailsBySiteIDResult> rsp_GetZoneDetailsBySiteID([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetZoneDetailsBySiteIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_Export_History")]
        public int usp_Export_History([Parameter(Name = "Reference1", DbType = "VarChar(50)")] string reference1,
                                      [Parameter(Name = "Type", DbType = "VarChar(30)")] string type,
                                      [Parameter(Name = "Site_id", DbType = "Int")] Nullable<int> site_id)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reference1,
                                                      type, site_id);
            return ((int)(result.ReturnValue));
        }


        [Function(Name = "dbo.usp_DeleteZone")]
        public int usp_DeleteZone([Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), zoneID);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBarPositionDetailsBySiteID")]
        public ISingleResult<rsp_GetBarPositionDetailsBySiteIDResult> rsp_GetBarPositionDetailsBySiteID([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetBarPositionDetailsBySiteIDResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBarPositionDetailsById")]
        public ISingleResult<rsp_GetBarPositionDetailsByIdResult> rsp_GetBarPositionDetailsById([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_GetBarPositionDetailsByIdResult>)(result.ReturnValue));
        }
        //[Function(Name = "dbo.rsp_GetOpeningHours")]
        //public ISingleResult<rsp_GetOpeningHoursResult> rsp_GetOpeningHours()
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
        //    return ((ISingleResult<rsp_GetOpeningHoursResult>)(result.ReturnValue));
        //}

        //[Function(Name = "dbo.rsp_SelectZones")]
        //public ISingleResult<rsp_SelectZonesResult> SelectZones([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
        //    return ((ISingleResult<rsp_SelectZonesResult>)(result.ReturnValue));
        //}
        [Function(Name = "dbo.rsp_SelectZones")]
        public ISingleResult<rsp_SelectZonesResult> SelectZones([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_SelectZonesResult>)(result.ReturnValue));
        }
        public DataTable rsp_GetOpeningHours()
        {
            DataSet dsOpeningHours = new DataSet();
            try
            {


                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetOpeningHours", dsOpeningHours, new string[] { "OpeningHoursList" });

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            if (dsOpeningHours.Tables.Count > 0)
                return dsOpeningHours.Tables[0];
            else
                return null;
        }

        public DataTable LoadZones(int SiteID)
        {
            DataSet dsZone = new DataSet();
            try
            {
                SqlParameter[] oParams = new SqlParameter[1];
                oParams[0] = new SqlParameter("SiteID", SiteID);


                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_SelectZones", dsZone, new string[] { "Zone" }, oParams);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            if (dsZone.Tables.Count > 0)
                return dsZone.Tables[0];
            else
                return null;
        }

        public DataTable GetZoneDetailsBySiteID(int SiteID)
        {
            DataSet dsZone = new DataSet();
            try
            {
                SqlParameter[] oParams = new SqlParameter[1];
                oParams[0] = new SqlParameter("SiteID", SiteID);


                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetZoneDetailsBySiteID", dsZone, new string[] { "SiteID" }, oParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (dsZone.Tables.Count > 0)
                return dsZone.Tables[0];
            else
                return null;
        }
        public DataTable GetTermsGroupData()
        {
            DataSet dsTermsGroup = new DataSet();
            try
            {
                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetTermsGroup", dsTermsGroup, new string[] { "TermsGroup" } );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (dsTermsGroup.Tables.Count > 0)
                return dsTermsGroup.Tables[0];
            else
                return null;
        }
        public DataTable GetOperatorForPosition()
        {
            DataSet dsOperator = new DataSet();
            try
            {
                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetOperatorForPosition", dsOperator, new string[] { "OpertorList" });//, new string[] { "" }, );
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (dsOperator.Tables.Count > 0)
                return dsOperator.Tables[0];
            else
                return null;
        }
        public DataTable GetMachineType()
        {
            DataSet dsMachineType = new DataSet();
            try
            {
                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetLstMachineType", dsMachineType, new string[] { "MachineType" });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (dsMachineType.Tables.Count > 0)
                return dsMachineType.Tables[0];
            else
                return null;

        }

        public DataTable GetDepotListForPosition(int supplierid)
        {
            DataSet dsDepot = new DataSet();
            try
            {
                SqlParameter[] oParams = new SqlParameter[1];
                oParams[0] = new SqlParameter("SupplierID", supplierid);

                SqlHelper.FillDataset(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetDepotListForPosition", dsDepot, new string[] { "Depot" }, oParams);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            if (dsDepot.Tables.Count > 0)
                return dsDepot.Tables[0];
            else
                return null;
        }
        //[Function(Name="dbo.usp_UpdateBarPositionDetails")]
        //public int usp_UpdateBarPositionDetails(
        //            [Parameter(Name="Bar_Position_Name", DbType="VarChar(50)")] string bar_Position_Name, 
        //            [Parameter(Name="Bar_Position_Company_Position_Code", DbType="VarChar(6)")] string bar_Position_Company_Position_Code, 
        //            [Parameter(Name="Bar_Position_Location", DbType="VarChar(50)")] string bar_Position_Location, 
        //            [Parameter(Name="Zone_ID", DbType="Int")] System.Nullable<int> zone_ID, 
        //            [Parameter(Name="Bar_Position_Invoice_Period", DbType="Int")] System.Nullable<int> bar_Position_Invoice_Period, 
        //            [Parameter(Name="Machine_Type_ID", DbType="Int")] System.Nullable<int> machine_Type_ID, 
        //            [Parameter(Name="Bar_Position_Collection_Period", DbType="Int")] System.Nullable<int> bar_Position_Collection_Period, 
        //            [Parameter(Name="Bar_Position_Collection_Day", DbType="VarChar(30)")] string bar_Position_Collection_Day, 
        //            [Parameter(Name="Depot_ID", DbType="Int")] System.Nullable<int> depot_ID, 
        //            [Parameter(Name="Bar_Position_Image_Reference", DbType="VarChar(50)")] string bar_Position_Image_Reference, 
        //            [Parameter(Name="Bar_Position_End_Date", DbType="VarChar(30)")] string bar_Position_End_Date, 
        //            [Parameter(Name="Bar_Position_Supplier_Site_Code", DbType="VarChar(8)")] string bar_Position_Supplier_Site_Code, 
        //            [Parameter(Name="Bar_Position_Supplier_Position_Code", DbType="VarChar(6)")] string bar_Position_Supplier_Position_Code, 
        //            [Parameter(Name="Bar_Position_Use_Terms", DbType="Bit")] System.Nullable<bool> bar_Position_Use_Terms, 
        //            [Parameter(Name="Bar_Position_Override_Rent_From_Schedule_To_Rent", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Rent_From_Schedule_To_Rent, 
        //            [Parameter(Name="Bar_Position_Override_Rent_From_Schedule_To_Rent_Date", DbType="VarChar(30)")] string bar_Position_Override_Rent_From_Schedule_To_Rent_Date, 
        //            [Parameter(Name="Bar_Position_Override_Rent_From_Rent_To_Schedule", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Rent_From_Rent_To_Schedule, 
        //            [Parameter(Name="Bar_Position_Override_Rent_From_Rent_To_Schedule_Date", DbType="VarChar(30)")] string bar_Position_Override_Rent_From_Rent_To_Schedule_Date, 
        //            [Parameter(Name="Bar_Position_Rent_Schedule_ID_From", DbType="Int")] System.Nullable<int> bar_Position_Rent_Schedule_ID_From, 
        //            [Parameter(Name="Bar_Position_Override_Rent", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Rent, 
        //            [Parameter(Name="Bar_Position_Override_Rent_Schedule", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Rent_Schedule, 
        //            [Parameter(Name="Bar_Position_Rent_Schedule_ID", DbType="Int")] System.Nullable<int> bar_Position_Rent_Schedule_ID, 
        //            [Parameter(Name="Bar_Position_Override_Shares", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Shares, 
        //            [Parameter(Name="Bar_Position_Override_Licence", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Licence, 
        //            [Parameter(Name="Bar_Position_Rent_Previous", DbType="Real")] System.Nullable<float> bar_Position_Rent_Previous, 
        //            [Parameter(Name="Bar_Position_Rent", DbType="Real")] System.Nullable<float> bar_Position_Rent, 
        //            [Parameter(Name="Bar_Position_Rent_Future", DbType="Real")] System.Nullable<float> bar_Position_Rent_Future, 
        //            [Parameter(Name="Bar_Position_Supplier_Share_Previous", DbType="Real")] System.Nullable<float> bar_Position_Supplier_Share_Previous, 
        //            [Parameter(Name="Bar_Position_Supplier_Share", DbType="Real")] System.Nullable<float> bar_Position_Supplier_Share, 
        //            [Parameter(Name="Bar_Position_Supplier_Share_Future", DbType="Real")] System.Nullable<float> bar_Position_Supplier_Share_Future, 
        //            [Parameter(Name="Bar_Position_Site_Share_Previous", DbType="Real")] System.Nullable<float> bar_Position_Site_Share_Previous, 
        //            [Parameter(Name="Bar_Position_Site_Share", DbType="Real")] System.Nullable<float> bar_Position_Site_Share, 
        //            [Parameter(Name="Bar_Position_Site_Share_Future", DbType="Real")] System.Nullable<float> bar_Position_Site_Share_Future, 
        //            [Parameter(Name="Bar_Position_Owners_Share_Previous", DbType="Real")] System.Nullable<float> bar_Position_Owners_Share_Previous, 
        //            [Parameter(Name="Bar_Position_Owners_Share", DbType="Real")] System.Nullable<float> bar_Position_Owners_Share, 
        //            [Parameter(Name="Bar_Position_Owners_Share_Future", DbType="Real")] System.Nullable<float> bar_Position_Owners_Share_Future, 
        //            [Parameter(Name="Bar_Position_Secondary_Owners_Share_Previous", DbType="Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share_Previous, 
        //            [Parameter(Name="Bar_Position_Secondary_Owners_Share", DbType="Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share, 
        //            [Parameter(Name="Bar_Position_Secondary_Owners_Share_Future", DbType="Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share_Future, 
        //            [Parameter(Name="Bar_Position_Licence_Previous", DbType="Real")] System.Nullable<float> bar_Position_Licence_Previous, 
        //            [Parameter(Name="Bar_Position_Licence_Charge", DbType="Real")] System.Nullable<float> bar_Position_Licence_Charge, 
        //            [Parameter(Name="Bar_Position_Licence_Future", DbType="Real")] System.Nullable<float> bar_Position_Licence_Future, 
        //            [Parameter(Name="Bar_Position_Rent_Past_Date", DbType="VarChar(30)")] string bar_Position_Rent_Past_Date, 
        //            [Parameter(Name="Bar_Position_Rent_Future_Date", DbType="VarChar(30)")] string bar_Position_Rent_Future_Date, 
        //            [Parameter(Name="Bar_Position_Share_Past_Date", DbType="VarChar(30)")] string bar_Position_Share_Past_Date, 
        //            [Parameter(Name="Bar_Position_Share_Future_Date", DbType="VarChar(30)")] string bar_Position_Share_Future_Date, 
        //            [Parameter(Name="Bar_Position_Licence_Past_Date", DbType="VarChar(30)")] string bar_Position_Licence_Past_Date, 
        //            [Parameter(Name="Bar_Position_Licence_Future_Date", DbType="VarChar(30)")] string bar_Position_Licence_Future_Date, 
        //            [Parameter(Name="Bar_Position_Use_Site_Share_For_Secondary_Brewery", DbType="Bit")] System.Nullable<bool> bar_Position_Use_Site_Share_For_Secondary_Brewery, 
        //            [Parameter(Name="Bar_Position_Prize_LOS", DbType="Bit")] System.Nullable<bool> bar_Position_Prize_LOS, 
        //            [Parameter(Name="Bar_Position_Override_Share_Schedule", DbType="Bit")] System.Nullable<bool> bar_Position_Override_Share_Schedule, 
        //            [Parameter(Name="Bar_Position_Share_Schedule_ID", DbType="Int")] System.Nullable<int> bar_Position_Share_Schedule_ID, 
        //            [Parameter(Name="Bar_Position_Disable_EDI_Export", DbType="Bit")] System.Nullable<bool> bar_Position_Disable_EDI_Export)
        //{
        //    IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_Name, bar_Position_Company_Position_Code, bar_Position_Location, zone_ID, bar_Position_Invoice_Period, machine_Type_ID, bar_Position_Collection_Period, bar_Position_Collection_Day, depot_ID, bar_Position_Image_Reference, bar_Position_End_Date, bar_Position_Supplier_Site_Code, bar_Position_Supplier_Position_Code, bar_Position_Use_Terms, bar_Position_Override_Rent_From_Schedule_To_Rent, bar_Position_Override_Rent_From_Schedule_To_Rent_Date, bar_Position_Override_Rent_From_Rent_To_Schedule, bar_Position_Override_Rent_From_Rent_To_Schedule_Date, bar_Position_Rent_Schedule_ID_From, bar_Position_Override_Rent, bar_Position_Override_Rent_Schedule, bar_Position_Rent_Schedule_ID, bar_Position_Override_Shares, bar_Position_Override_Licence, bar_Position_Rent_Previous, bar_Position_Rent, bar_Position_Rent_Future, bar_Position_Supplier_Share_Previous, bar_Position_Supplier_Share, bar_Position_Supplier_Share_Future, bar_Position_Site_Share_Previous, bar_Position_Site_Share, bar_Position_Site_Share_Future, bar_Position_Owners_Share_Previous, bar_Position_Owners_Share, bar_Position_Owners_Share_Future, bar_Position_Secondary_Owners_Share_Previous, bar_Position_Secondary_Owners_Share, bar_Position_Secondary_Owners_Share_Future, bar_Position_Licence_Previous, bar_Position_Licence_Charge, bar_Position_Licence_Future, bar_Position_Rent_Past_Date, bar_Position_Rent_Future_Date, bar_Position_Share_Past_Date, bar_Position_Share_Future_Date, bar_Position_Licence_Past_Date, bar_Position_Licence_Future_Date, bar_Position_Use_Site_Share_For_Secondary_Brewery, bar_Position_Prize_LOS, bar_Position_Override_Share_Schedule, bar_Position_Share_Schedule_ID, bar_Position_Disable_EDI_Export);
        //    return ((int)(result.ReturnValue));
        //}
        [Function(Name = "dbo.usp_UpdateBarPosDetails")]
        public int usp_UpdateBarPosDetails([Parameter(Name = "BarPositionName", DbType = "VarChar(50)")] string barPositionName, [Parameter(Name = "ZoneID", DbType = "Int")] System.Nullable<int> zoneID, [Parameter(Name = "Supplier_AMEDIS_Code", DbType = "VarChar(4)")] string supplier_AMEDIS_Code, [Parameter(Name = "DepotID", DbType = "Int")] System.Nullable<int> depotID, [Parameter(Name = "CollectionDay", DbType = "VarChar(30)")] string collectionDay, [Parameter(Name = "Machine_Type_AMEDIS_Code", DbType = "Int")] System.Nullable<int> machine_Type_AMEDIS_Code)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionName, zoneID, supplier_AMEDIS_Code, depotID, collectionDay, machine_Type_AMEDIS_Code);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetBarPositionInfo")]
        public ISingleResult<rsp_GetBarPositionInfoResult> GetBarPositionInfo([Parameter(Name = "BarPositionID", DbType = "Int")] System.Nullable<int> barPositionID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPositionID);
            return ((ISingleResult<rsp_GetBarPositionInfoResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.Usp_InsertOrUpdateBarPosition")]
        public int InsertOrUpdateBarPosition(
                    [Parameter(Name = "Bar_Position_ID", DbType = "Int")] System.Nullable<int> bar_Position_ID,
                    [Parameter(Name = "Site_ID", DbType = "Int")] System.Nullable<int> site_ID,
                    [Parameter(Name = "Zone_ID", DbType = "Int")] System.Nullable<int> zone_ID,
                    [Parameter(Name = "Access_Key_ID", DbType = "Int")] System.Nullable<int> access_Key_ID,
                    [Parameter(Name = "Access_Key_ID_Default", DbType = "Bit")] System.Nullable<bool> access_Key_ID_Default,
                    [Parameter(Name = "Terms_Group_ID", DbType = "Int")] System.Nullable<int> terms_Group_ID,
                    [Parameter(Name = "Terms_Group_Changeover_Date", DbType = "VarChar(30)")] string terms_Group_Changeover_Date,
                    [Parameter(Name = "Terms_Group_Future_ID", DbType = "Int")] System.Nullable<int> terms_Group_Future_ID,
                    [Parameter(Name = "Terms_Group_Past_Changeover_Date", DbType = "VarChar(30)")] string terms_Group_Past_Changeover_Date,
                    [Parameter(Name = "Terms_Group_Past_ID", DbType = "Int")] System.Nullable<int> terms_Group_Past_ID,
                    [Parameter(Name = "Terms_Group_ID_Default", DbType = "Bit")] System.Nullable<bool> terms_Group_ID_Default,
                    [Parameter(Name = "Duty_ID", DbType = "Int")] System.Nullable<int> duty_ID,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> machine_Type_ID,
                    [Parameter(Name = "Bar_Position_Name", DbType = "VarChar(50)")] string bar_Position_Name,
                    [Parameter(Name = "Bar_Position_Location", DbType = "VarChar(50)")] string bar_Position_Location,
                    [Parameter(Name = "Bar_Position_Start_Date", DbType = "VarChar(30)")] string bar_Position_Start_Date,
                    [Parameter(Name = "Bar_Position_End_Date", DbType = "VarChar(30)")] string bar_Position_End_Date,
                    [Parameter(Name = "Bar_Position_Collection_Day", DbType = "VarChar(30)")] string bar_Position_Collection_Day,
                    [Parameter(Name = "Bar_Position_Price_Per_Play", DbType = "VarChar(50)")] string bar_Position_Price_Per_Play,
                    [Parameter(Name = "Bar_Position_Price_Per_Play_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Price_Per_Play_Default,
                    [Parameter(Name = "Bar_Position_Jackpot", DbType = "VarChar(50)")] string bar_Position_Jackpot,
                    [Parameter(Name = "Bar_Position_Jackpot_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Jackpot_Default,
                    [Parameter(Name = "Bar_Position_Percentage_Payout", DbType = "VarChar(50)")] string bar_Position_Percentage_Payout,
                    [Parameter(Name = "Bar_Position_Percentage_Payout_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_Percentage_Payout_Default,
                    [Parameter(Name = "Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")] string bar_Position_Last_Collection_Date,
                    [Parameter(Name = "Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")] string bar_Position_Collection_Rent_Paid_Until,
                    [Parameter(Name = "Bar_Position_Collection_Period", DbType = "Int")] System.Nullable<int> bar_Position_Collection_Period,
                    [Parameter(Name = "Bar_Position_Supplier_AMEDIS_Code", DbType = "VarChar(4)")] string bar_Position_Supplier_AMEDIS_Code,
                    [Parameter(Name = "Bar_Position_Supplier_Depot_AMEDIS_Code", DbType = "VarChar(4)")] string bar_Position_Supplier_Depot_AMEDIS_Code,
                    [Parameter(Name = "Bar_Position_Supplier_Site_Code", DbType = "VarChar(8)")] string bar_Position_Supplier_Site_Code,
                    [Parameter(Name = "Bar_Position_Supplier_Position_Code", DbType = "VarChar(6)")] string bar_Position_Supplier_Position_Code,
                    [Parameter(Name = "Bar_Position_Supplier_Area", DbType = "VarChar(50)")] string bar_Position_Supplier_Area,
                    [Parameter(Name = "Bar_Position_Supplier_Service_Area", DbType = "VarChar(50)")] string bar_Position_Supplier_Service_Area,
                    [Parameter(Name = "Bar_Position_Company_Position_Code", DbType = "VarChar(6)")] string bar_Position_Company_Position_Code,
                    [Parameter(Name = "Bar_Position_Company_Target", DbType = "Real")] System.Nullable<float> bar_Position_Company_Target,
                    [Parameter(Name = "Bar_Position_Collection_Frequency", DbType = "Int")] System.Nullable<int> bar_Position_Collection_Frequency,
                    [Parameter(Name = "Bar_Position_Image_Reference", DbType = "VarChar(50)")] string bar_Position_Image_Reference,
                    [Parameter(Name = "Bar_Position_Machine_Type_AMEDIS_Code", DbType = "Int")] System.Nullable<int> bar_Position_Machine_Type_AMEDIS_Code,
                    [Parameter(Name = "Bar_Position_Rent", DbType = "Real")] System.Nullable<float> bar_Position_Rent,
                    [Parameter(Name = "Bar_Position_Rent_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Rent_Previous,
                    [Parameter(Name = "Bar_Position_Rent_Future", DbType = "Real")] System.Nullable<float> bar_Position_Rent_Future,
                    [Parameter(Name = "Bar_Position_Rent_Past_Date", DbType = "VarChar(30)")] string bar_Position_Rent_Past_Date,
                    [Parameter(Name = "Bar_Position_Rent_Future_Date", DbType = "VarChar(30)")] string bar_Position_Rent_Future_Date,
                    [Parameter(Name = "Bar_Position_Supplier_Share", DbType = "Real")] System.Nullable<float> bar_Position_Supplier_Share,
                    [Parameter(Name = "Bar_Position_Site_Share", DbType = "Real")] System.Nullable<float> bar_Position_Site_Share,
                    [Parameter(Name = "Bar_Position_Owners_Share", DbType = "Real")] System.Nullable<float> bar_Position_Owners_Share,
                    [Parameter(Name = "Bar_Position_Secondary_Owners_Share", DbType = "Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share,
                    [Parameter(Name = "Bar_Position_Supplier_Share_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Supplier_Share_Previous,
                    [Parameter(Name = "Bar_Position_Site_Share_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Site_Share_Previous,
                    [Parameter(Name = "Bar_Position_Owners_Share_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Owners_Share_Previous,
                    [Parameter(Name = "Bar_Position_Secondary_Owners_Share_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share_Previous,
                    [Parameter(Name = "Bar_Position_Supplier_Share_Future", DbType = "Real")] System.Nullable<float> bar_Position_Supplier_Share_Future,
                    [Parameter(Name = "Bar_Position_Site_Share_Future", DbType = "Real")] System.Nullable<float> bar_Position_Site_Share_Future,
                    [Parameter(Name = "Bar_Position_Owners_Share_Future", DbType = "Real")] System.Nullable<float> bar_Position_Owners_Share_Future,
                    [Parameter(Name = "Bar_Position_Secondary_Owners_Share_Future", DbType = "Real")] System.Nullable<float> bar_Position_Secondary_Owners_Share_Future,
                    [Parameter(Name = "Bar_Position_Share_Past_Date", DbType = "VarChar(30)")] string bar_Position_Share_Past_Date,
                    [Parameter(Name = "Bar_Position_Share_Future_Date", DbType = "VarChar(30)")] string bar_Position_Share_Future_Date,
                    [Parameter(Name = "Bar_Position_Licence_Charge", DbType = "Real")] System.Nullable<float> bar_Position_Licence_Charge,
                    [Parameter(Name = "Bar_Position_Licence_Previous", DbType = "Real")] System.Nullable<float> bar_Position_Licence_Previous,
                    [Parameter(Name = "Bar_Position_Licence_Future", DbType = "Real")] System.Nullable<float> bar_Position_Licence_Future,
                    [Parameter(Name = "Bar_Position_Licence_Past_Date", DbType = "VarChar(30)")] string bar_Position_Licence_Past_Date,
                    [Parameter(Name = "Bar_Position_Licence_Future_Date", DbType = "VarChar(30)")] string bar_Position_Licence_Future_Date,
                    [Parameter(Name = "Bar_Position_Use_Terms", DbType = "Bit")] System.Nullable<bool> bar_Position_Use_Terms,
                    [Parameter(Name = "Bar_Position_TX_Collection", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_Collection,
                    [Parameter(Name = "Bar_Position_TX_Collection_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_Collection_Use_Default,
                    [Parameter(Name = "Bar_Position_TX_Movement", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_Movement,
                    [Parameter(Name = "Bar_Position_TX_Movement_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_Movement_Use_Default,
                    [Parameter(Name = "Bar_Position_TX_EDC", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_EDC,
                    [Parameter(Name = "Bar_Position_TX_EDC_Use_Detault", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_EDC_Use_Detault,
                    [Parameter(Name = "Bar_Position_TX_Format", DbType = "Int")] System.Nullable<int> bar_Position_TX_Format,
                    [Parameter(Name = "Bar_Position_TX_Format_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_TX_Format_Use_Default,
                    [Parameter(Name = "Bar_Position_RX_Collection", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_Collection,
                    [Parameter(Name = "Bar_Position_RX_Collection_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_Collection_Use_Default,
                    [Parameter(Name = "Bar_Position_RX_Movement", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_Movement,
                    [Parameter(Name = "Bar_Position_RX_Movement_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_Movement_Use_Default,
                    [Parameter(Name = "Bar_Position_RX_EDC", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_EDC,
                    [Parameter(Name = "Bar_Position_RX_EDC_Use_Detault", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_EDC_Use_Detault,
                    [Parameter(Name = "Bar_Position_RX_Format", DbType = "Int")] System.Nullable<int> bar_Position_RX_Format,
                    [Parameter(Name = "Bar_Position_RX_Format_Use_Default", DbType = "Bit")] System.Nullable<bool> bar_Position_RX_Format_Use_Default,
                    [Parameter(Name = "Bar_Position_Net_Target", DbType = "Real")] System.Nullable<float> bar_Position_Net_Target,
                    [Parameter(Name = "Bar_Position_Below_Net_Target_Counter", DbType = "Int")] System.Nullable<int> bar_Position_Below_Net_Target_Counter,
                    [Parameter(Name = "Bar_Position_Below_Company_Target_Counter", DbType = "Int")] System.Nullable<int> bar_Position_Below_Company_Target_Counter,
                    [Parameter(Name = "Bar_Position_Security_Required", DbType = "Bit")] System.Nullable<bool> bar_Position_Security_Required,
                    [Parameter(Name = "Bar_Position_Site_Has_Cashbox_Keys", DbType = "Bit")] System.Nullable<bool> bar_Position_Site_Has_Cashbox_Keys,
                    [Parameter(Name = "Bar_Position_Site_Has_FreePlay_Access", DbType = "Bit")] System.Nullable<bool> bar_Position_Site_Has_FreePlay_Access,
                    [Parameter(Name = "Bar_Position_Override_Rent", DbType = "Bit")] System.Nullable<bool> bar_Position_Override_Rent,
                    [Parameter(Name = "Bar_Position_Override_Shares", DbType = "Bit")] System.Nullable<bool> bar_Position_Override_Shares,
                    [Parameter(Name = "Bar_Position_Override_Licence", DbType = "Bit")] System.Nullable<bool> bar_Position_Override_Licence,
                    [Parameter(Name = "Bar_Position_Category", DbType = "Int")] System.Nullable<int> bar_Position_Category,
                    [Parameter(Name = "Bar_Position_PPL_Charge", DbType = "Real")] System.Nullable<float> bar_Position_PPL_Charge,
                    [Parameter(Name = "Bar_Position_PPL_Previous", DbType = "Real")] System.Nullable<float> bar_Position_PPL_Previous,
                    [Parameter(Name = "Bar_Position_PPL_Future", DbType = "Real")] System.Nullable<float> bar_Position_PPL_Future,
                    [Parameter(Name = "Bar_Position_PPL_Past_Date", DbType = "VarChar(30)")] string bar_Position_PPL_Past_Date,
                    [Parameter(Name = "Bar_Position_PPL_Future_Date", DbType = "VarChar(30)")] string bar_Position_PPL_Future_Date,
                    [Parameter(Name = "Bar_Position_Float_Issued", DbType = "Int")] System.Nullable<int> bar_Position_Float_Issued,
                    [Parameter(Name = "Bar_Position_Float_Recovered", DbType = "Int")] System.Nullable<int> bar_Position_Float_Recovered,
                    [Parameter(Name = "Bar_Position_Use_Site_Share_For_Secondary_Brewery", DbType = "Bit")] System.Nullable<bool> bar_Position_Use_Site_Share_For_Secondary_Brewery,
                    [Parameter(Name = "Bar_Position_Prize_LOS", DbType = "Bit")] System.Nullable<bool> bar_Position_Prize_LOS,
                    [Parameter(Name = "Bar_Position_Rent_Schedule_ID", DbType = "Int")] System.Nullable<int> bar_Position_Rent_Schedule_ID,
                    [Parameter(Name = "Bar_Position_IsEnable", DbType = "Bit")] bool Bar_Position_IsEnable
            )
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), bar_Position_ID, site_ID, zone_ID, access_Key_ID, access_Key_ID_Default, terms_Group_ID, terms_Group_Changeover_Date, terms_Group_Future_ID, terms_Group_Past_Changeover_Date, terms_Group_Past_ID, terms_Group_ID_Default, duty_ID, depot_ID, machine_Type_ID, bar_Position_Name, bar_Position_Location, bar_Position_Start_Date, bar_Position_End_Date, bar_Position_Collection_Day, bar_Position_Price_Per_Play, bar_Position_Price_Per_Play_Default, bar_Position_Jackpot, bar_Position_Jackpot_Default, bar_Position_Percentage_Payout, bar_Position_Percentage_Payout_Default, bar_Position_Last_Collection_Date, bar_Position_Collection_Rent_Paid_Until, bar_Position_Collection_Period, bar_Position_Supplier_AMEDIS_Code, bar_Position_Supplier_Depot_AMEDIS_Code, bar_Position_Supplier_Site_Code, bar_Position_Supplier_Position_Code, bar_Position_Supplier_Area, bar_Position_Supplier_Service_Area, bar_Position_Company_Position_Code, bar_Position_Company_Target, bar_Position_Collection_Frequency, bar_Position_Image_Reference, bar_Position_Machine_Type_AMEDIS_Code, bar_Position_Rent, bar_Position_Rent_Previous, bar_Position_Rent_Future, bar_Position_Rent_Past_Date, bar_Position_Rent_Future_Date, bar_Position_Supplier_Share, bar_Position_Site_Share, bar_Position_Owners_Share, bar_Position_Secondary_Owners_Share, bar_Position_Supplier_Share_Previous, bar_Position_Site_Share_Previous, bar_Position_Owners_Share_Previous, bar_Position_Secondary_Owners_Share_Previous, bar_Position_Supplier_Share_Future, bar_Position_Site_Share_Future, bar_Position_Owners_Share_Future, bar_Position_Secondary_Owners_Share_Future, bar_Position_Share_Past_Date, bar_Position_Share_Future_Date, bar_Position_Licence_Charge, bar_Position_Licence_Previous, bar_Position_Licence_Future, bar_Position_Licence_Past_Date, bar_Position_Licence_Future_Date, bar_Position_Use_Terms, bar_Position_TX_Collection, bar_Position_TX_Collection_Use_Default, bar_Position_TX_Movement, bar_Position_TX_Movement_Use_Default, bar_Position_TX_EDC, bar_Position_TX_EDC_Use_Detault, bar_Position_TX_Format, bar_Position_TX_Format_Use_Default, bar_Position_RX_Collection, bar_Position_RX_Collection_Use_Default, bar_Position_RX_Movement, bar_Position_RX_Movement_Use_Default, bar_Position_RX_EDC, bar_Position_RX_EDC_Use_Detault, bar_Position_RX_Format, bar_Position_RX_Format_Use_Default, bar_Position_Net_Target, bar_Position_Below_Net_Target_Counter, bar_Position_Below_Company_Target_Counter, bar_Position_Security_Required, bar_Position_Site_Has_Cashbox_Keys, bar_Position_Site_Has_FreePlay_Access, bar_Position_Override_Rent, bar_Position_Override_Shares, bar_Position_Override_Licence, bar_Position_Category, bar_Position_PPL_Charge, bar_Position_PPL_Previous, bar_Position_PPL_Future, bar_Position_PPL_Past_Date, bar_Position_PPL_Future_Date, bar_Position_Float_Issued, bar_Position_Float_Recovered, bar_Position_Use_Site_Share_For_Secondary_Brewery, bar_Position_Prize_LOS, bar_Position_Rent_Schedule_ID,Bar_Position_IsEnable);
            return ((int)(result.ReturnValue));
        }

        public partial class rsp_GetZoneDetailsBySiteIDResult
        {

            private int _Zone_ID;

            private string _Zone_Name;

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _Standard_Opening_Hours_ID;

            public rsp_GetZoneDetailsBySiteIDResult()
            {
            }

            [Column(Storage = "_Zone_ID", DbType = "Int NOT NULL")]
            public int Zone_ID
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

            [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
            public string Zone_Name
            {
                get
                {
                    return this._Zone_Name;
                }
                set
                {
                    if ((this._Zone_Name != value))
                    {
                        this._Zone_Name = value;
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

            [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
            public System.Nullable<int> Standard_Opening_Hours_ID
            {
                get
                {
                    return this._Standard_Opening_Hours_ID;
                }
                set
                {
                    if ((this._Standard_Opening_Hours_ID != value))
                    {
                        this._Standard_Opening_Hours_ID = value;
                    }
                }
            }
        }

        //public partial class usp_InsertNewZoneResult
        //{

        //    private System.Nullable<int> _Column1;

        //    public usp_InsertNewZoneResult()
        //    {
        //    }

        //    [Column(Storage = "_Column1", DbType = "Int")]
        //    public System.Nullable<int> Column1
        //    {
        //        get
        //        {
        //            return this._Column1;
        //        }
        //        set
        //        {
        //            if ((this._Column1 != value))
        //            {
        //                this._Column1 = value;
        //            }
        //        }
        //    }
        //}
        public partial class usp_InsertNewZoneResult
        {

            private System.Nullable<int> _Column1;

            public usp_InsertNewZoneResult()
            {
            }

            [Column(Storage = "_Column1", DbType = "Int")]
            public System.Nullable<int> Column1
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
        public partial class rsp_GetSiteResult
        {

            private int _Site_ID;

            private string _Site_Reference;

            private System.Nullable<int> _Sub_Company_ID;

            private System.Nullable<int> _Staff_ID;

            private System.Nullable<bool> _Staff_ID_Default;

            private System.Nullable<int> _Access_Key_ID;

            private System.Nullable<bool> _Access_Key_ID_Default;

            private System.Nullable<int> _Terms_Group_ID;

            private System.Nullable<bool> _Terms_Group_ID_Default;

            private System.Nullable<int> _Computer_Build_ID;

            private System.Nullable<int> _Standard_Opening_Hours_ID;

            private System.Nullable<int> _Secondary_Sub_Company_ID;

            private System.Nullable<bool> _Site_London_Rent;

            private string _Site_Supplier_Area;

            private string _Site_Supplier_Service_Area;

            private string _Site_Grade;

            private System.Nullable<int> _Site_Permit_Needed;

            private string _Site_Code;

            private string _Site_Supplier_Code;

            private string _Site_Name;

            private string _Site_Address;

            private string _Site_Postcode;

            private string _Site_Phone_No;

            private string _Site_Fax_No;

            private string _Site_Email_Address;

            private string _Site_Manager;

            private string _Site_Computer_Name;

            private string _Site_Open_Monday;

            private string _Site_Open_Tuesday;

            private string _Site_Open_Wednesday;

            private string _Site_Open_Thursday;

            private string _Site_Open_Friday;

            private string _Site_Open_Saturday;

            private string _Site_Open_Sunday;

            private string _Site_Invoice_Address;

            private string _Site_Invoice_Postcode;

            private string _Site_Invoice_Name;

            private string _Site_Dial_Up_Number;

            private string _Site_Username;

            private string _Site_Password;

            private string _Site_Domain;

            private string _Site_Local_Inbox;

            private string _Site_Local_Outbox;

            private string _Site_Remote_Inbox;

            private string _Site_Remote_Outbox;

            private string _Site_FTPServerAddress;

            private System.Nullable<int> _Site_ConnType;

            private string _Site_Price_Per_Play;

            private System.Nullable<bool> _Site_Price_Per_Play_Default;

            private string _Site_Jackpot;

            private System.Nullable<bool> _Site_Jackpot_Default;

            private string _Site_Percentage_Payout;

            private System.Nullable<bool> _Site_Percentage_Payout_Default;

            private string _Site_Start_Date;

            private string _Site_End_Date;

            private string _Sage_Account_Ref;

            private string _Site_Memo;

            private string _Site_Company_Code;

            private System.Nullable<int> _Site_Previous_Sub_Company_ID;

            private System.Nullable<System.DateTime> _Site_Licensee_Commenced_Date;

            private string _Site_Licensee_Agreement_Type;

            private System.Nullable<int> _Depot_ID;

            private System.Nullable<int> _Service_Depot_ID;

            private System.Nullable<bool> _Site_VAT_Exempt_Flag;

            private System.Nullable<float> _Site_Company_Target;

            private System.Nullable<int> _Site_Company_Barrellage;

            private string _Site_Image_Reference;

            private string _Site_Image_Reference_2;

            private string _Site_Trade_Type;

            private System.Nullable<int> _Sub_Company_Region_ID;

            private System.Nullable<int> _Sub_Company_Area_ID;

            private System.Nullable<int> _Sub_Company_District_ID;

            private string _Site_Address_1;

            private string _Site_Address_2;

            private string _Site_Address_3;

            private string _Site_Address_4;

            private string _Site_Address_5;

            private System.Nullable<bool> _Site_TX_Collection;

            private System.Nullable<bool> _Site_TX_Collection_Use_Default;

            private System.Nullable<bool> _Site_TX_Movement;

            private System.Nullable<bool> _Site_TX_Movement_Use_Default;

            private System.Nullable<bool> _Site_TX_EDC;

            private System.Nullable<bool> _Site_TX_EDC_Use_Detault;

            private System.Nullable<int> _Site_TX_Format;

            private System.Nullable<bool> _Site_TX_Format_Use_Default;

            private System.Nullable<bool> _Site_RX_Collection;

            private System.Nullable<bool> _Site_RX_Collection_Use_Default;

            private System.Nullable<bool> _Site_RX_Movement;

            private System.Nullable<bool> _Site_RX_Movement_Use_Default;

            private System.Nullable<bool> _Site_RX_EDC;

            private System.Nullable<bool> _Site_RX_EDC_Use_Detault;

            private System.Nullable<int> _Site_RX_Format;

            private System.Nullable<bool> _Site_RX_Format_Use_Default;

            private string _NT_Phone_Book_Entry;

            private System.Nullable<int> _Next_Secondary_Sub_Company_ID;

            private string _Site_Secondary_Sub_Company_Changeover;

            private string _Site_GPS_Location;

            private string _Site_Stop_Importing_EDI_On;

            private string _Site_Non_Trading_Period_From;

            private string _Site_Non_Trading_Period_To;

            private System.Nullable<int> _Service_Area_ID;

            private System.Nullable<int> _Service_Supplier_ID;

            private System.Nullable<int> _Next_Sub_Company_ID;

            private string _Next_Sub_Company_Change_Date;

            private string _Previous_Sub_Company_Change_Date;

            private System.Nullable<int> _Previous_Secondary_Sub_Company_ID;

            private string _Previous_Secondary_Sub_Company_Change_Date;

            private System.Nullable<int> _Site_Honeyframe_EDI;

            private System.Nullable<int> _Site_Datapak_Protocol;

            private System.Nullable<bool> _Site_Is_FreeFloat;

            private int _Site_Classification_ID;

            private System.Nullable<System.DateTime> _Site_Licensee_Agreement_End_Date;

            private string _Site_Licence_Number;

            private System.Nullable<short> _Site_Application;

            private string _Region;

            private string _WebURL;

            private string _ConnectionString;

            private System.Xml.Linq.XElement _Site_Status;

            private System.Nullable<System.DateTime> _Last_Updated_Time;

            private bool _Apply_Retailer_Share;

            private System.Nullable<int> _Site_Status_ID;

            private System.Nullable<System.DateTime> _Site_Inactive_Date;

            private System.Nullable<int> _NGA_Machine_ID;

            private System.Nullable<int> _Site_Setting_Profile_ID;

            private string _SiteStatus;

            private string _ExchangeKey;

            private string _Site_Fiscal_Code;

            private string _Site_Street_Number;

            private string _Site_Province;

            private string _Site_Municipality;

            private string _Site_Cadastral_Code;

            private System.Nullable<int> _Site_Area;

            private System.Nullable<int> _Site_Location_Type;

            private System.Nullable<int> _Site_Closed;

            private string _Site_Workstation;

            private System.Nullable<int> _Site_Toponym;

            private string _Site_Closed_Date;

            private System.Nullable<bool> _AFT_Settings_Enabled;

            private bool _Site_Enabled;

            private string _Site_Region_Code;

            private System.Nullable<int> _Site_Connection_Type;

            private System.Nullable<int> _Site_MaxNumber_VLT;

            private string _Site_Connection_IPAddress;

            private System.Nullable<int> _Site_AAMS_Status;

            private System.Nullable<System.DateTime> _Site_Modified_Date;

            private string _Site_Termination_Status;

            private string _Site_ZonaRice;

            private System.Nullable<int> _IsTITOEnabled;

            private System.Nullable<int> _IsNonCashVoucherEnabled;

            private System.Nullable<int> _IsCrossTicketingEnabled;

            private string _TicketingURL;

            private System.Nullable<int> _StackerLimitPercentage;

            public rsp_GetSiteResult()
            {
            }

            [Column(Storage = "_Site_ID", DbType = "Int NOT NULL")]
            public int Site_ID
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

            [Column(Storage = "_Site_Reference", DbType = "VarChar(50)")]
            public string Site_Reference
            {
                get
                {
                    return this._Site_Reference;
                }
                set
                {
                    if ((this._Site_Reference != value))
                    {
                        this._Site_Reference = value;
                    }
                }
            }

            [Column(Storage = "_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Sub_Company_ID
            {
                get
                {
                    return this._Sub_Company_ID;
                }
                set
                {
                    if ((this._Sub_Company_ID != value))
                    {
                        this._Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Staff_ID", DbType = "Int")]
            public System.Nullable<int> Staff_ID
            {
                get
                {
                    return this._Staff_ID;
                }
                set
                {
                    if ((this._Staff_ID != value))
                    {
                        this._Staff_ID = value;
                    }
                }
            }

            [Column(Storage = "_Staff_ID_Default", DbType = "Bit")]
            public System.Nullable<bool> Staff_ID_Default
            {
                get
                {
                    return this._Staff_ID_Default;
                }
                set
                {
                    if ((this._Staff_ID_Default != value))
                    {
                        this._Staff_ID_Default = value;
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

            [Column(Storage = "_Computer_Build_ID", DbType = "Int")]
            public System.Nullable<int> Computer_Build_ID
            {
                get
                {
                    return this._Computer_Build_ID;
                }
                set
                {
                    if ((this._Computer_Build_ID != value))
                    {
                        this._Computer_Build_ID = value;
                    }
                }
            }

            [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
            public System.Nullable<int> Standard_Opening_Hours_ID
            {
                get
                {
                    return this._Standard_Opening_Hours_ID;
                }
                set
                {
                    if ((this._Standard_Opening_Hours_ID != value))
                    {
                        this._Standard_Opening_Hours_ID = value;
                    }
                }
            }

            [Column(Storage = "_Secondary_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Secondary_Sub_Company_ID
            {
                get
                {
                    return this._Secondary_Sub_Company_ID;
                }
                set
                {
                    if ((this._Secondary_Sub_Company_ID != value))
                    {
                        this._Secondary_Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_London_Rent", DbType = "Bit")]
            public System.Nullable<bool> Site_London_Rent
            {
                get
                {
                    return this._Site_London_Rent;
                }
                set
                {
                    if ((this._Site_London_Rent != value))
                    {
                        this._Site_London_Rent = value;
                    }
                }
            }

            [Column(Storage = "_Site_Supplier_Area", DbType = "VarChar(50)")]
            public string Site_Supplier_Area
            {
                get
                {
                    return this._Site_Supplier_Area;
                }
                set
                {
                    if ((this._Site_Supplier_Area != value))
                    {
                        this._Site_Supplier_Area = value;
                    }
                }
            }

            [Column(Storage = "_Site_Supplier_Service_Area", DbType = "VarChar(50)")]
            public string Site_Supplier_Service_Area
            {
                get
                {
                    return this._Site_Supplier_Service_Area;
                }
                set
                {
                    if ((this._Site_Supplier_Service_Area != value))
                    {
                        this._Site_Supplier_Service_Area = value;
                    }
                }
            }

            [Column(Storage = "_Site_Grade", DbType = "VarChar(10)")]
            public string Site_Grade
            {
                get
                {
                    return this._Site_Grade;
                }
                set
                {
                    if ((this._Site_Grade != value))
                    {
                        this._Site_Grade = value;
                    }
                }
            }

            [Column(Storage = "_Site_Permit_Needed", DbType = "Int")]
            public System.Nullable<int> Site_Permit_Needed
            {
                get
                {
                    return this._Site_Permit_Needed;
                }
                set
                {
                    if ((this._Site_Permit_Needed != value))
                    {
                        this._Site_Permit_Needed = value;
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

            [Column(Storage = "_Site_Supplier_Code", DbType = "VarChar(50)")]
            public string Site_Supplier_Code
            {
                get
                {
                    return this._Site_Supplier_Code;
                }
                set
                {
                    if ((this._Site_Supplier_Code != value))
                    {
                        this._Site_Supplier_Code = value;
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

            [Column(Storage = "_Site_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
            public string Site_Address
            {
                get
                {
                    return this._Site_Address;
                }
                set
                {
                    if ((this._Site_Address != value))
                    {
                        this._Site_Address = value;
                    }
                }
            }

            [Column(Storage = "_Site_Postcode", DbType = "VarChar(15)")]
            public string Site_Postcode
            {
                get
                {
                    return this._Site_Postcode;
                }
                set
                {
                    if ((this._Site_Postcode != value))
                    {
                        this._Site_Postcode = value;
                    }
                }
            }

            [Column(Storage = "_Site_Phone_No", DbType = "VarChar(15)")]
            public string Site_Phone_No
            {
                get
                {
                    return this._Site_Phone_No;
                }
                set
                {
                    if ((this._Site_Phone_No != value))
                    {
                        this._Site_Phone_No = value;
                    }
                }
            }

            [Column(Storage = "_Site_Fax_No", DbType = "VarChar(15)")]
            public string Site_Fax_No
            {
                get
                {
                    return this._Site_Fax_No;
                }
                set
                {
                    if ((this._Site_Fax_No != value))
                    {
                        this._Site_Fax_No = value;
                    }
                }
            }

            [Column(Storage = "_Site_Email_Address", DbType = "VarChar(100)")]
            public string Site_Email_Address
            {
                get
                {
                    return this._Site_Email_Address;
                }
                set
                {
                    if ((this._Site_Email_Address != value))
                    {
                        this._Site_Email_Address = value;
                    }
                }
            }

            [Column(Storage = "_Site_Manager", DbType = "VarChar(50)")]
            public string Site_Manager
            {
                get
                {
                    return this._Site_Manager;
                }
                set
                {
                    if ((this._Site_Manager != value))
                    {
                        this._Site_Manager = value;
                    }
                }
            }

            [Column(Storage = "_Site_Computer_Name", DbType = "VarChar(50)")]
            public string Site_Computer_Name
            {
                get
                {
                    return this._Site_Computer_Name;
                }
                set
                {
                    if ((this._Site_Computer_Name != value))
                    {
                        this._Site_Computer_Name = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Monday", DbType = "VarChar(96)")]
            public string Site_Open_Monday
            {
                get
                {
                    return this._Site_Open_Monday;
                }
                set
                {
                    if ((this._Site_Open_Monday != value))
                    {
                        this._Site_Open_Monday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Tuesday", DbType = "VarChar(96)")]
            public string Site_Open_Tuesday
            {
                get
                {
                    return this._Site_Open_Tuesday;
                }
                set
                {
                    if ((this._Site_Open_Tuesday != value))
                    {
                        this._Site_Open_Tuesday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Wednesday", DbType = "VarChar(96)")]
            public string Site_Open_Wednesday
            {
                get
                {
                    return this._Site_Open_Wednesday;
                }
                set
                {
                    if ((this._Site_Open_Wednesday != value))
                    {
                        this._Site_Open_Wednesday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Thursday", DbType = "VarChar(96)")]
            public string Site_Open_Thursday
            {
                get
                {
                    return this._Site_Open_Thursday;
                }
                set
                {
                    if ((this._Site_Open_Thursday != value))
                    {
                        this._Site_Open_Thursday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Friday", DbType = "VarChar(96)")]
            public string Site_Open_Friday
            {
                get
                {
                    return this._Site_Open_Friday;
                }
                set
                {
                    if ((this._Site_Open_Friday != value))
                    {
                        this._Site_Open_Friday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Saturday", DbType = "VarChar(96)")]
            public string Site_Open_Saturday
            {
                get
                {
                    return this._Site_Open_Saturday;
                }
                set
                {
                    if ((this._Site_Open_Saturday != value))
                    {
                        this._Site_Open_Saturday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Open_Sunday", DbType = "VarChar(96)")]
            public string Site_Open_Sunday
            {
                get
                {
                    return this._Site_Open_Sunday;
                }
                set
                {
                    if ((this._Site_Open_Sunday != value))
                    {
                        this._Site_Open_Sunday = value;
                    }
                }
            }

            [Column(Storage = "_Site_Invoice_Address", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
            public string Site_Invoice_Address
            {
                get
                {
                    return this._Site_Invoice_Address;
                }
                set
                {
                    if ((this._Site_Invoice_Address != value))
                    {
                        this._Site_Invoice_Address = value;
                    }
                }
            }

            [Column(Storage = "_Site_Invoice_Postcode", DbType = "VarChar(15)")]
            public string Site_Invoice_Postcode
            {
                get
                {
                    return this._Site_Invoice_Postcode;
                }
                set
                {
                    if ((this._Site_Invoice_Postcode != value))
                    {
                        this._Site_Invoice_Postcode = value;
                    }
                }
            }

            [Column(Storage = "_Site_Invoice_Name", DbType = "VarChar(50)")]
            public string Site_Invoice_Name
            {
                get
                {
                    return this._Site_Invoice_Name;
                }
                set
                {
                    if ((this._Site_Invoice_Name != value))
                    {
                        this._Site_Invoice_Name = value;
                    }
                }
            }

            [Column(Storage = "_Site_Dial_Up_Number", DbType = "VarChar(50)")]
            public string Site_Dial_Up_Number
            {
                get
                {
                    return this._Site_Dial_Up_Number;
                }
                set
                {
                    if ((this._Site_Dial_Up_Number != value))
                    {
                        this._Site_Dial_Up_Number = value;
                    }
                }
            }

            [Column(Storage = "_Site_Username", DbType = "VarChar(50)")]
            public string Site_Username
            {
                get
                {
                    return this._Site_Username;
                }
                set
                {
                    if ((this._Site_Username != value))
                    {
                        this._Site_Username = value;
                    }
                }
            }

            [Column(Storage = "_Site_Password", DbType = "VarChar(50)")]
            public string Site_Password
            {
                get
                {
                    return this._Site_Password;
                }
                set
                {
                    if ((this._Site_Password != value))
                    {
                        this._Site_Password = value;
                    }
                }
            }

            [Column(Storage = "_Site_Domain", DbType = "VarChar(50)")]
            public string Site_Domain
            {
                get
                {
                    return this._Site_Domain;
                }
                set
                {
                    if ((this._Site_Domain != value))
                    {
                        this._Site_Domain = value;
                    }
                }
            }

            [Column(Storage = "_Site_Local_Inbox", DbType = "VarChar(100)")]
            public string Site_Local_Inbox
            {
                get
                {
                    return this._Site_Local_Inbox;
                }
                set
                {
                    if ((this._Site_Local_Inbox != value))
                    {
                        this._Site_Local_Inbox = value;
                    }
                }
            }

            [Column(Storage = "_Site_Local_Outbox", DbType = "VarChar(100)")]
            public string Site_Local_Outbox
            {
                get
                {
                    return this._Site_Local_Outbox;
                }
                set
                {
                    if ((this._Site_Local_Outbox != value))
                    {
                        this._Site_Local_Outbox = value;
                    }
                }
            }

            [Column(Storage = "_Site_Remote_Inbox", DbType = "VarChar(100)")]
            public string Site_Remote_Inbox
            {
                get
                {
                    return this._Site_Remote_Inbox;
                }
                set
                {
                    if ((this._Site_Remote_Inbox != value))
                    {
                        this._Site_Remote_Inbox = value;
                    }
                }
            }

            [Column(Storage = "_Site_Remote_Outbox", DbType = "VarChar(100)")]
            public string Site_Remote_Outbox
            {
                get
                {
                    return this._Site_Remote_Outbox;
                }
                set
                {
                    if ((this._Site_Remote_Outbox != value))
                    {
                        this._Site_Remote_Outbox = value;
                    }
                }
            }

            [Column(Storage = "_Site_FTPServerAddress", DbType = "VarChar(100)")]
            public string Site_FTPServerAddress
            {
                get
                {
                    return this._Site_FTPServerAddress;
                }
                set
                {
                    if ((this._Site_FTPServerAddress != value))
                    {
                        this._Site_FTPServerAddress = value;
                    }
                }
            }

            [Column(Storage = "_Site_ConnType", DbType = "Int")]
            public System.Nullable<int> Site_ConnType
            {
                get
                {
                    return this._Site_ConnType;
                }
                set
                {
                    if ((this._Site_ConnType != value))
                    {
                        this._Site_ConnType = value;
                    }
                }
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

            [Column(Storage = "_Site_Price_Per_Play_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_Price_Per_Play_Default
            {
                get
                {
                    return this._Site_Price_Per_Play_Default;
                }
                set
                {
                    if ((this._Site_Price_Per_Play_Default != value))
                    {
                        this._Site_Price_Per_Play_Default = value;
                    }
                }
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

            [Column(Storage = "_Site_Jackpot_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_Jackpot_Default
            {
                get
                {
                    return this._Site_Jackpot_Default;
                }
                set
                {
                    if ((this._Site_Jackpot_Default != value))
                    {
                        this._Site_Jackpot_Default = value;
                    }
                }
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

            [Column(Storage = "_Site_Percentage_Payout_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_Percentage_Payout_Default
            {
                get
                {
                    return this._Site_Percentage_Payout_Default;
                }
                set
                {
                    if ((this._Site_Percentage_Payout_Default != value))
                    {
                        this._Site_Percentage_Payout_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_Start_Date", DbType = "VarChar(30)")]
            public string Site_Start_Date
            {
                get
                {
                    return this._Site_Start_Date;
                }
                set
                {
                    if ((this._Site_Start_Date != value))
                    {
                        this._Site_Start_Date = value;
                    }
                }
            }

            [Column(Storage = "_Site_End_Date", DbType = "VarChar(30)")]
            public string Site_End_Date
            {
                get
                {
                    return this._Site_End_Date;
                }
                set
                {
                    if ((this._Site_End_Date != value))
                    {
                        this._Site_End_Date = value;
                    }
                }
            }

            [Column(Storage = "_Sage_Account_Ref", DbType = "VarChar(50)")]
            public string Sage_Account_Ref
            {
                get
                {
                    return this._Sage_Account_Ref;
                }
                set
                {
                    if ((this._Sage_Account_Ref != value))
                    {
                        this._Sage_Account_Ref = value;
                    }
                }
            }

            [Column(Storage = "_Site_Memo", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
            public string Site_Memo
            {
                get
                {
                    return this._Site_Memo;
                }
                set
                {
                    if ((this._Site_Memo != value))
                    {
                        this._Site_Memo = value;
                    }
                }
            }

            [Column(Storage = "_Site_Company_Code", DbType = "VarChar(50)")]
            public string Site_Company_Code
            {
                get
                {
                    return this._Site_Company_Code;
                }
                set
                {
                    if ((this._Site_Company_Code != value))
                    {
                        this._Site_Company_Code = value;
                    }
                }
            }

            [Column(Storage = "_Site_Previous_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Site_Previous_Sub_Company_ID
            {
                get
                {
                    return this._Site_Previous_Sub_Company_ID;
                }
                set
                {
                    if ((this._Site_Previous_Sub_Company_ID != value))
                    {
                        this._Site_Previous_Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Licensee_Commenced_Date", DbType = "SmallDateTime")]
            public System.Nullable<System.DateTime> Site_Licensee_Commenced_Date
            {
                get
                {
                    return this._Site_Licensee_Commenced_Date;
                }
                set
                {
                    if ((this._Site_Licensee_Commenced_Date != value))
                    {
                        this._Site_Licensee_Commenced_Date = value;
                    }
                }
            }

            [Column(Storage = "_Site_Licensee_Agreement_Type", DbType = "VarChar(50)")]
            public string Site_Licensee_Agreement_Type
            {
                get
                {
                    return this._Site_Licensee_Agreement_Type;
                }
                set
                {
                    if ((this._Site_Licensee_Agreement_Type != value))
                    {
                        this._Site_Licensee_Agreement_Type = value;
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

            [Column(Storage = "_Service_Depot_ID", DbType = "Int")]
            public System.Nullable<int> Service_Depot_ID
            {
                get
                {
                    return this._Service_Depot_ID;
                }
                set
                {
                    if ((this._Service_Depot_ID != value))
                    {
                        this._Service_Depot_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_VAT_Exempt_Flag", DbType = "Bit")]
            public System.Nullable<bool> Site_VAT_Exempt_Flag
            {
                get
                {
                    return this._Site_VAT_Exempt_Flag;
                }
                set
                {
                    if ((this._Site_VAT_Exempt_Flag != value))
                    {
                        this._Site_VAT_Exempt_Flag = value;
                    }
                }
            }

            [Column(Storage = "_Site_Company_Target", DbType = "Real")]
            public System.Nullable<float> Site_Company_Target
            {
                get
                {
                    return this._Site_Company_Target;
                }
                set
                {
                    if ((this._Site_Company_Target != value))
                    {
                        this._Site_Company_Target = value;
                    }
                }
            }

            [Column(Storage = "_Site_Company_Barrellage", DbType = "Int")]
            public System.Nullable<int> Site_Company_Barrellage
            {
                get
                {
                    return this._Site_Company_Barrellage;
                }
                set
                {
                    if ((this._Site_Company_Barrellage != value))
                    {
                        this._Site_Company_Barrellage = value;
                    }
                }
            }

            [Column(Storage = "_Site_Image_Reference", DbType = "VarChar(255)")]
            public string Site_Image_Reference
            {
                get
                {
                    return this._Site_Image_Reference;
                }
                set
                {
                    if ((this._Site_Image_Reference != value))
                    {
                        this._Site_Image_Reference = value;
                    }
                }
            }

            [Column(Storage = "_Site_Image_Reference_2", DbType = "VarChar(255)")]
            public string Site_Image_Reference_2
            {
                get
                {
                    return this._Site_Image_Reference_2;
                }
                set
                {
                    if ((this._Site_Image_Reference_2 != value))
                    {
                        this._Site_Image_Reference_2 = value;
                    }
                }
            }

            [Column(Storage = "_Site_Trade_Type", DbType = "VarChar(50)")]
            public string Site_Trade_Type
            {
                get
                {
                    return this._Site_Trade_Type;
                }
                set
                {
                    if ((this._Site_Trade_Type != value))
                    {
                        this._Site_Trade_Type = value;
                    }
                }
            }

            [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int")]
            public System.Nullable<int> Sub_Company_Region_ID
            {
                get
                {
                    return this._Sub_Company_Region_ID;
                }
                set
                {
                    if ((this._Sub_Company_Region_ID != value))
                    {
                        this._Sub_Company_Region_ID = value;
                    }
                }
            }

            [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int")]
            public System.Nullable<int> Sub_Company_Area_ID
            {
                get
                {
                    return this._Sub_Company_Area_ID;
                }
                set
                {
                    if ((this._Sub_Company_Area_ID != value))
                    {
                        this._Sub_Company_Area_ID = value;
                    }
                }
            }

            [Column(Storage = "_Sub_Company_District_ID", DbType = "Int")]
            public System.Nullable<int> Sub_Company_District_ID
            {
                get
                {
                    return this._Sub_Company_District_ID;
                }
                set
                {
                    if ((this._Sub_Company_District_ID != value))
                    {
                        this._Sub_Company_District_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Address_1", DbType = "VarChar(50)")]
            public string Site_Address_1
            {
                get
                {
                    return this._Site_Address_1;
                }
                set
                {
                    if ((this._Site_Address_1 != value))
                    {
                        this._Site_Address_1 = value;
                    }
                }
            }

            [Column(Storage = "_Site_Address_2", DbType = "VarChar(50)")]
            public string Site_Address_2
            {
                get
                {
                    return this._Site_Address_2;
                }
                set
                {
                    if ((this._Site_Address_2 != value))
                    {
                        this._Site_Address_2 = value;
                    }
                }
            }

            [Column(Storage = "_Site_Address_3", DbType = "VarChar(50)")]
            public string Site_Address_3
            {
                get
                {
                    return this._Site_Address_3;
                }
                set
                {
                    if ((this._Site_Address_3 != value))
                    {
                        this._Site_Address_3 = value;
                    }
                }
            }

            [Column(Storage = "_Site_Address_4", DbType = "VarChar(50)")]
            public string Site_Address_4
            {
                get
                {
                    return this._Site_Address_4;
                }
                set
                {
                    if ((this._Site_Address_4 != value))
                    {
                        this._Site_Address_4 = value;
                    }
                }
            }

            [Column(Storage = "_Site_Address_5", DbType = "VarChar(50)")]
            public string Site_Address_5
            {
                get
                {
                    return this._Site_Address_5;
                }
                set
                {
                    if ((this._Site_Address_5 != value))
                    {
                        this._Site_Address_5 = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_Collection
            {
                get
                {
                    return this._Site_TX_Collection;
                }
                set
                {
                    if ((this._Site_TX_Collection != value))
                    {
                        this._Site_TX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_Collection_Use_Default
            {
                get
                {
                    return this._Site_TX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Site_TX_Collection_Use_Default != value))
                    {
                        this._Site_TX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_Movement
            {
                get
                {
                    return this._Site_TX_Movement;
                }
                set
                {
                    if ((this._Site_TX_Movement != value))
                    {
                        this._Site_TX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_Movement_Use_Default
            {
                get
                {
                    return this._Site_TX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Site_TX_Movement_Use_Default != value))
                    {
                        this._Site_TX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_EDC
            {
                get
                {
                    return this._Site_TX_EDC;
                }
                set
                {
                    if ((this._Site_TX_EDC != value))
                    {
                        this._Site_TX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_EDC_Use_Detault
            {
                get
                {
                    return this._Site_TX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Site_TX_EDC_Use_Detault != value))
                    {
                        this._Site_TX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Format", DbType = "Int")]
            public System.Nullable<int> Site_TX_Format
            {
                get
                {
                    return this._Site_TX_Format;
                }
                set
                {
                    if ((this._Site_TX_Format != value))
                    {
                        this._Site_TX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Site_TX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_TX_Format_Use_Default
            {
                get
                {
                    return this._Site_TX_Format_Use_Default;
                }
                set
                {
                    if ((this._Site_TX_Format_Use_Default != value))
                    {
                        this._Site_TX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_Collection
            {
                get
                {
                    return this._Site_RX_Collection;
                }
                set
                {
                    if ((this._Site_RX_Collection != value))
                    {
                        this._Site_RX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_Collection_Use_Default
            {
                get
                {
                    return this._Site_RX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Site_RX_Collection_Use_Default != value))
                    {
                        this._Site_RX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_Movement
            {
                get
                {
                    return this._Site_RX_Movement;
                }
                set
                {
                    if ((this._Site_RX_Movement != value))
                    {
                        this._Site_RX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_Movement_Use_Default
            {
                get
                {
                    return this._Site_RX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Site_RX_Movement_Use_Default != value))
                    {
                        this._Site_RX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_EDC
            {
                get
                {
                    return this._Site_RX_EDC;
                }
                set
                {
                    if ((this._Site_RX_EDC != value))
                    {
                        this._Site_RX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_EDC_Use_Detault
            {
                get
                {
                    return this._Site_RX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Site_RX_EDC_Use_Detault != value))
                    {
                        this._Site_RX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Format", DbType = "Int")]
            public System.Nullable<int> Site_RX_Format
            {
                get
                {
                    return this._Site_RX_Format;
                }
                set
                {
                    if ((this._Site_RX_Format != value))
                    {
                        this._Site_RX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Site_RX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Site_RX_Format_Use_Default
            {
                get
                {
                    return this._Site_RX_Format_Use_Default;
                }
                set
                {
                    if ((this._Site_RX_Format_Use_Default != value))
                    {
                        this._Site_RX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_NT_Phone_Book_Entry", DbType = "VarChar(100)")]
            public string NT_Phone_Book_Entry
            {
                get
                {
                    return this._NT_Phone_Book_Entry;
                }
                set
                {
                    if ((this._NT_Phone_Book_Entry != value))
                    {
                        this._NT_Phone_Book_Entry = value;
                    }
                }
            }

            [Column(Storage = "_Next_Secondary_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Next_Secondary_Sub_Company_ID
            {
                get
                {
                    return this._Next_Secondary_Sub_Company_ID;
                }
                set
                {
                    if ((this._Next_Secondary_Sub_Company_ID != value))
                    {
                        this._Next_Secondary_Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Secondary_Sub_Company_Changeover", DbType = "VarChar(10)")]
            public string Site_Secondary_Sub_Company_Changeover
            {
                get
                {
                    return this._Site_Secondary_Sub_Company_Changeover;
                }
                set
                {
                    if ((this._Site_Secondary_Sub_Company_Changeover != value))
                    {
                        this._Site_Secondary_Sub_Company_Changeover = value;
                    }
                }
            }

            [Column(Storage = "_Site_GPS_Location", DbType = "VarChar(50)")]
            public string Site_GPS_Location
            {
                get
                {
                    return this._Site_GPS_Location;
                }
                set
                {
                    if ((this._Site_GPS_Location != value))
                    {
                        this._Site_GPS_Location = value;
                    }
                }
            }

            [Column(Storage = "_Site_Stop_Importing_EDI_On", DbType = "VarChar(10)")]
            public string Site_Stop_Importing_EDI_On
            {
                get
                {
                    return this._Site_Stop_Importing_EDI_On;
                }
                set
                {
                    if ((this._Site_Stop_Importing_EDI_On != value))
                    {
                        this._Site_Stop_Importing_EDI_On = value;
                    }
                }
            }

            [Column(Storage = "_Site_Non_Trading_Period_From", DbType = "VarChar(30)")]
            public string Site_Non_Trading_Period_From
            {
                get
                {
                    return this._Site_Non_Trading_Period_From;
                }
                set
                {
                    if ((this._Site_Non_Trading_Period_From != value))
                    {
                        this._Site_Non_Trading_Period_From = value;
                    }
                }
            }

            [Column(Storage = "_Site_Non_Trading_Period_To", DbType = "VarChar(30)")]
            public string Site_Non_Trading_Period_To
            {
                get
                {
                    return this._Site_Non_Trading_Period_To;
                }
                set
                {
                    if ((this._Site_Non_Trading_Period_To != value))
                    {
                        this._Site_Non_Trading_Period_To = value;
                    }
                }
            }

            [Column(Storage = "_Service_Area_ID", DbType = "Int")]
            public System.Nullable<int> Service_Area_ID
            {
                get
                {
                    return this._Service_Area_ID;
                }
                set
                {
                    if ((this._Service_Area_ID != value))
                    {
                        this._Service_Area_ID = value;
                    }
                }
            }

            [Column(Storage = "_Service_Supplier_ID", DbType = "Int")]
            public System.Nullable<int> Service_Supplier_ID
            {
                get
                {
                    return this._Service_Supplier_ID;
                }
                set
                {
                    if ((this._Service_Supplier_ID != value))
                    {
                        this._Service_Supplier_ID = value;
                    }
                }
            }

            [Column(Storage = "_Next_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Next_Sub_Company_ID
            {
                get
                {
                    return this._Next_Sub_Company_ID;
                }
                set
                {
                    if ((this._Next_Sub_Company_ID != value))
                    {
                        this._Next_Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Next_Sub_Company_Change_Date", DbType = "VarChar(30)")]
            public string Next_Sub_Company_Change_Date
            {
                get
                {
                    return this._Next_Sub_Company_Change_Date;
                }
                set
                {
                    if ((this._Next_Sub_Company_Change_Date != value))
                    {
                        this._Next_Sub_Company_Change_Date = value;
                    }
                }
            }

            [Column(Storage = "_Previous_Sub_Company_Change_Date", DbType = "VarChar(30)")]
            public string Previous_Sub_Company_Change_Date
            {
                get
                {
                    return this._Previous_Sub_Company_Change_Date;
                }
                set
                {
                    if ((this._Previous_Sub_Company_Change_Date != value))
                    {
                        this._Previous_Sub_Company_Change_Date = value;
                    }
                }
            }

            [Column(Storage = "_Previous_Secondary_Sub_Company_ID", DbType = "Int")]
            public System.Nullable<int> Previous_Secondary_Sub_Company_ID
            {
                get
                {
                    return this._Previous_Secondary_Sub_Company_ID;
                }
                set
                {
                    if ((this._Previous_Secondary_Sub_Company_ID != value))
                    {
                        this._Previous_Secondary_Sub_Company_ID = value;
                    }
                }
            }

            [Column(Storage = "_Previous_Secondary_Sub_Company_Change_Date", DbType = "VarChar(30)")]
            public string Previous_Secondary_Sub_Company_Change_Date
            {
                get
                {
                    return this._Previous_Secondary_Sub_Company_Change_Date;
                }
                set
                {
                    if ((this._Previous_Secondary_Sub_Company_Change_Date != value))
                    {
                        this._Previous_Secondary_Sub_Company_Change_Date = value;
                    }
                }
            }

            [Column(Storage = "_Site_Honeyframe_EDI", DbType = "Int")]
            public System.Nullable<int> Site_Honeyframe_EDI
            {
                get
                {
                    return this._Site_Honeyframe_EDI;
                }
                set
                {
                    if ((this._Site_Honeyframe_EDI != value))
                    {
                        this._Site_Honeyframe_EDI = value;
                    }
                }
            }

            [Column(Storage = "_Site_Datapak_Protocol", DbType = "Int")]
            public System.Nullable<int> Site_Datapak_Protocol
            {
                get
                {
                    return this._Site_Datapak_Protocol;
                }
                set
                {
                    if ((this._Site_Datapak_Protocol != value))
                    {
                        this._Site_Datapak_Protocol = value;
                    }
                }
            }

            [Column(Storage = "_Site_Is_FreeFloat", DbType = "Bit")]
            public System.Nullable<bool> Site_Is_FreeFloat
            {
                get
                {
                    return this._Site_Is_FreeFloat;
                }
                set
                {
                    if ((this._Site_Is_FreeFloat != value))
                    {
                        this._Site_Is_FreeFloat = value;
                    }
                }
            }

            [Column(Storage = "_Site_Classification_ID", DbType = "Int NOT NULL")]
            public int Site_Classification_ID
            {
                get
                {
                    return this._Site_Classification_ID;
                }
                set
                {
                    if ((this._Site_Classification_ID != value))
                    {
                        this._Site_Classification_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Licensee_Agreement_End_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Site_Licensee_Agreement_End_Date
            {
                get
                {
                    return this._Site_Licensee_Agreement_End_Date;
                }
                set
                {
                    if ((this._Site_Licensee_Agreement_End_Date != value))
                    {
                        this._Site_Licensee_Agreement_End_Date = value;
                    }
                }
            }

            [Column(Storage = "_Site_Licence_Number", DbType = "VarChar(25)")]
            public string Site_Licence_Number
            {
                get
                {
                    return this._Site_Licence_Number;
                }
                set
                {
                    if ((this._Site_Licence_Number != value))
                    {
                        this._Site_Licence_Number = value;
                    }
                }
            }

            [Column(Storage = "_Site_Application", DbType = "SmallInt")]
            public System.Nullable<short> Site_Application
            {
                get
                {
                    return this._Site_Application;
                }
                set
                {
                    if ((this._Site_Application != value))
                    {
                        this._Site_Application = value;
                    }
                }
            }

            [Column(Storage = "_Region", DbType = "VarChar(10)")]
            public string Region
            {
                get
                {
                    return this._Region;
                }
                set
                {
                    if ((this._Region != value))
                    {
                        this._Region = value;
                    }
                }
            }

            [Column(Storage = "_WebURL", DbType = "VarChar(2000)")]
            public string WebURL
            {
                get
                {
                    return this._WebURL;
                }
                set
                {
                    if ((this._WebURL != value))
                    {
                        this._WebURL = value;
                    }
                }
            }

            [Column(Storage = "_ConnectionString", DbType = "VarChar(200)")]
            public string ConnectionString
            {
                get
                {
                    return this._ConnectionString;
                }
                set
                {
                    if ((this._ConnectionString != value))
                    {
                        this._ConnectionString = value;
                    }
                }
            }

            [Column(Storage = "_Site_Status", DbType = "Xml")]
            public System.Xml.Linq.XElement Site_Status
            {
                get
                {
                    return this._Site_Status;
                }
                set
                {
                    if ((this._Site_Status != value))
                    {
                        this._Site_Status = value;
                    }
                }
            }

            [Column(Storage = "_Last_Updated_Time", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Last_Updated_Time
            {
                get
                {
                    return this._Last_Updated_Time;
                }
                set
                {
                    if ((this._Last_Updated_Time != value))
                    {
                        this._Last_Updated_Time = value;
                    }
                }
            }

            [Column(Storage = "_Apply_Retailer_Share", DbType = "Bit NOT NULL")]
            public bool Apply_Retailer_Share
            {
                get
                {
                    return this._Apply_Retailer_Share;
                }
                set
                {
                    if ((this._Apply_Retailer_Share != value))
                    {
                        this._Apply_Retailer_Share = value;
                    }
                }
            }

            [Column(Storage = "_Site_Status_ID", DbType = "Int")]
            public System.Nullable<int> Site_Status_ID
            {
                get
                {
                    return this._Site_Status_ID;
                }
                set
                {
                    if ((this._Site_Status_ID != value))
                    {
                        this._Site_Status_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Inactive_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Site_Inactive_Date
            {
                get
                {
                    return this._Site_Inactive_Date;
                }
                set
                {
                    if ((this._Site_Inactive_Date != value))
                    {
                        this._Site_Inactive_Date = value;
                    }
                }
            }

            [Column(Storage = "_NGA_Machine_ID", DbType = "Int")]
            public System.Nullable<int> NGA_Machine_ID
            {
                get
                {
                    return this._NGA_Machine_ID;
                }
                set
                {
                    if ((this._NGA_Machine_ID != value))
                    {
                        this._NGA_Machine_ID = value;
                    }
                }
            }

            [Column(Storage = "_Site_Setting_Profile_ID", DbType = "Int")]
            public System.Nullable<int> Site_Setting_Profile_ID
            {
                get
                {
                    return this._Site_Setting_Profile_ID;
                }
                set
                {
                    if ((this._Site_Setting_Profile_ID != value))
                    {
                        this._Site_Setting_Profile_ID = value;
                    }
                }
            }

            [Column(Storage = "_SiteStatus", DbType = "VarChar(300)")]
            public string SiteStatus
            {
                get
                {
                    return this._SiteStatus;
                }
                set
                {
                    if ((this._SiteStatus != value))
                    {
                        this._SiteStatus = value;
                    }
                }
            }

            [Column(Storage = "_ExchangeKey", DbType = "VarChar(300)")]
            public string ExchangeKey
            {
                get
                {
                    return this._ExchangeKey;
                }
                set
                {
                    if ((this._ExchangeKey != value))
                    {
                        this._ExchangeKey = value;
                    }
                }
            }

            [Column(Storage = "_Site_Fiscal_Code", DbType = "VarChar(16)")]
            public string Site_Fiscal_Code
            {
                get
                {
                    return this._Site_Fiscal_Code;
                }
                set
                {
                    if ((this._Site_Fiscal_Code != value))
                    {
                        this._Site_Fiscal_Code = value;
                    }
                }
            }

            [Column(Storage = "_Site_Street_Number", DbType = "VarChar(15)")]
            public string Site_Street_Number
            {
                get
                {
                    return this._Site_Street_Number;
                }
                set
                {
                    if ((this._Site_Street_Number != value))
                    {
                        this._Site_Street_Number = value;
                    }
                }
            }

            [Column(Storage = "_Site_Province", DbType = "VarChar(15)")]
            public string Site_Province
            {
                get
                {
                    return this._Site_Province;
                }
                set
                {
                    if ((this._Site_Province != value))
                    {
                        this._Site_Province = value;
                    }
                }
            }

            [Column(Storage = "_Site_Municipality", DbType = "VarChar(40)")]
            public string Site_Municipality
            {
                get
                {
                    return this._Site_Municipality;
                }
                set
                {
                    if ((this._Site_Municipality != value))
                    {
                        this._Site_Municipality = value;
                    }
                }
            }

            [Column(Storage = "_Site_Cadastral_Code", DbType = "VarChar(15)")]
            public string Site_Cadastral_Code
            {
                get
                {
                    return this._Site_Cadastral_Code;
                }
                set
                {
                    if ((this._Site_Cadastral_Code != value))
                    {
                        this._Site_Cadastral_Code = value;
                    }
                }
            }

            [Column(Storage = "_Site_Area", DbType = "Int")]
            public System.Nullable<int> Site_Area
            {
                get
                {
                    return this._Site_Area;
                }
                set
                {
                    if ((this._Site_Area != value))
                    {
                        this._Site_Area = value;
                    }
                }
            }

            [Column(Storage = "_Site_Location_Type", DbType = "Int")]
            public System.Nullable<int> Site_Location_Type
            {
                get
                {
                    return this._Site_Location_Type;
                }
                set
                {
                    if ((this._Site_Location_Type != value))
                    {
                        this._Site_Location_Type = value;
                    }
                }
            }

            [Column(Storage = "_Site_Closed", DbType = "Int")]
            public System.Nullable<int> Site_Closed
            {
                get
                {
                    return this._Site_Closed;
                }
                set
                {
                    if ((this._Site_Closed != value))
                    {
                        this._Site_Closed = value;
                    }
                }
            }

            [Column(Storage = "_Site_Workstation", DbType = "VarChar(100)")]
            public string Site_Workstation
            {
                get
                {
                    return this._Site_Workstation;
                }
                set
                {
                    if ((this._Site_Workstation != value))
                    {
                        this._Site_Workstation = value;
                    }
                }
            }

            [Column(Storage = "_Site_Toponym", DbType = "Int")]
            public System.Nullable<int> Site_Toponym
            {
                get
                {
                    return this._Site_Toponym;
                }
                set
                {
                    if ((this._Site_Toponym != value))
                    {
                        this._Site_Toponym = value;
                    }
                }
            }

            [Column(Storage = "_Site_Closed_Date", DbType = "VarChar(30)")]
            public string Site_Closed_Date
            {
                get
                {
                    return this._Site_Closed_Date;
                }
                set
                {
                    if ((this._Site_Closed_Date != value))
                    {
                        this._Site_Closed_Date = value;
                    }
                }
            }

            [Column(Storage = "_AFT_Settings_Enabled", DbType = "Bit")]
            public System.Nullable<bool> AFT_Settings_Enabled
            {
                get
                {
                    return this._AFT_Settings_Enabled;
                }
                set
                {
                    if ((this._AFT_Settings_Enabled != value))
                    {
                        this._AFT_Settings_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Site_Enabled", DbType = "Bit NOT NULL")]
            public bool Site_Enabled
            {
                get
                {
                    return this._Site_Enabled;
                }
                set
                {
                    if ((this._Site_Enabled != value))
                    {
                        this._Site_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Site_Region_Code", DbType = "Char(2)")]
            public string Site_Region_Code
            {
                get
                {
                    return this._Site_Region_Code;
                }
                set
                {
                    if ((this._Site_Region_Code != value))
                    {
                        this._Site_Region_Code = value;
                    }
                }
            }

            [Column(Storage = "_Site_Connection_Type", DbType = "Int")]
            public System.Nullable<int> Site_Connection_Type
            {
                get
                {
                    return this._Site_Connection_Type;
                }
                set
                {
                    if ((this._Site_Connection_Type != value))
                    {
                        this._Site_Connection_Type = value;
                    }
                }
            }

            [Column(Storage = "_Site_MaxNumber_VLT", DbType = "Int")]
            public System.Nullable<int> Site_MaxNumber_VLT
            {
                get
                {
                    return this._Site_MaxNumber_VLT;
                }
                set
                {
                    if ((this._Site_MaxNumber_VLT != value))
                    {
                        this._Site_MaxNumber_VLT = value;
                    }
                }
            }

            [Column(Storage = "_Site_Connection_IPAddress", DbType = "VarChar(15)")]
            public string Site_Connection_IPAddress
            {
                get
                {
                    return this._Site_Connection_IPAddress;
                }
                set
                {
                    if ((this._Site_Connection_IPAddress != value))
                    {
                        this._Site_Connection_IPAddress = value;
                    }
                }
            }

            [Column(Storage = "_Site_AAMS_Status", DbType = "Int")]
            public System.Nullable<int> Site_AAMS_Status
            {
                get
                {
                    return this._Site_AAMS_Status;
                }
                set
                {
                    if ((this._Site_AAMS_Status != value))
                    {
                        this._Site_AAMS_Status = value;
                    }
                }
            }

            [Column(Storage = "_Site_Modified_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Site_Modified_Date
            {
                get
                {
                    return this._Site_Modified_Date;
                }
                set
                {
                    if ((this._Site_Modified_Date != value))
                    {
                        this._Site_Modified_Date = value;
                    }
                }
            }

            [Column(Storage = "_Site_Termination_Status", DbType = "VarChar(MAX)")]
            public string Site_Termination_Status
            {
                get
                {
                    return this._Site_Termination_Status;
                }
                set
                {
                    if ((this._Site_Termination_Status != value))
                    {
                        this._Site_Termination_Status = value;
                    }
                }
            }

            [Column(Storage = "_Site_ZonaRice", DbType = "VarChar(10)")]
            public string Site_ZonaRice
            {
                get
                {
                    return this._Site_ZonaRice;
                }
                set
                {
                    if ((this._Site_ZonaRice != value))
                    {
                        this._Site_ZonaRice = value;
                    }
                }
            }

            [Column(Storage = "_IsTITOEnabled", DbType = "Int")]
            public System.Nullable<int> IsTITOEnabled
            {
                get
                {
                    return this._IsTITOEnabled;
                }
                set
                {
                    if ((this._IsTITOEnabled != value))
                    {
                        this._IsTITOEnabled = value;
                    }
                }
            }

            [Column(Storage = "_IsNonCashVoucherEnabled", DbType = "Int")]
            public System.Nullable<int> IsNonCashVoucherEnabled
            {
                get
                {
                    return this._IsNonCashVoucherEnabled;
                }
                set
                {
                    if ((this._IsNonCashVoucherEnabled != value))
                    {
                        this._IsNonCashVoucherEnabled = value;
                    }
                }
            }

            [Column(Storage = "_IsCrossTicketingEnabled", DbType = "Int")]
            public System.Nullable<int> IsCrossTicketingEnabled
            {
                get
                {
                    return this._IsCrossTicketingEnabled;
                }
                set
                {
                    if ((this._IsCrossTicketingEnabled != value))
                    {
                        this._IsCrossTicketingEnabled = value;
                    }
                }
            }

            [Column(Storage = "_TicketingURL", DbType = "VarChar(2000)")]
            public string TicketingURL
            {
                get
                {
                    return this._TicketingURL;
                }
                set
                {
                    if ((this._TicketingURL != value))
                    {
                        this._TicketingURL = value;
                    }
                }
            }

            [Column(Storage = "_StackerLimitPercentage", DbType = "Int")]
            public System.Nullable<int> StackerLimitPercentage
            {
                get
                {
                    return this._StackerLimitPercentage;
                }
                set
                {
                    if ((this._StackerLimitPercentage != value))
                    {
                        this._StackerLimitPercentage = value;
                    }
                }
            }
        }
        public partial class rsp_CreateBarPositionTemplateResult
        {

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _Zone_ID;

            private System.Nullable<int> _Access_Key_ID;

            private System.Nullable<bool> _Access_Key_ID_Default;

            private System.Nullable<int> _Terms_Group_ID;

            private string _Terms_Group_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Future_ID;

            private string _Terms_Group_Past_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Past_ID;

            private System.Nullable<bool> _Terms_Group_ID_Default;

            private System.Nullable<int> _Duty_ID;

            private System.Nullable<int> _Depot_ID;

            private System.Nullable<int> _Machine_Type_ID;

            private string _Bar_Position_Name;

            private string _Bar_Position_Location;

            private string _Bar_Position_Start_Date;

            private string _Bar_Position_End_Date;

            private string _Bar_Position_Collection_Day;

            private string _Bar_Position_Price_Per_Play;

            private System.Nullable<bool> _Bar_Position_Price_Per_Play_Default;

            private string _Bar_Position_Jackpot;

            private System.Nullable<bool> _Bar_Position_Jackpot_Default;

            private string _Bar_Position_Percentage_Payout;

            private System.Nullable<bool> _Bar_Position_Percentage_Payout_Default;

            private string _Bar_Position_Last_Collection_Date;

            private string _Bar_Position_Collection_Rent_Paid_Until;

            private System.Nullable<int> _Bar_Position_Collection_Period;

            private string _Bar_Position_Supplier_AMEDIS_Code;

            private string _Bar_Position_Supplier_Depot_AMEDIS_Code;

            private string _Bar_Position_Supplier_Site_Code;

            private string _Bar_Position_Supplier_Position_Code;

            private string _Bar_Position_Supplier_Area;

            private string _Bar_Position_Supplier_Service_Area;

            private string _Bar_Position_Company_Position_Code;

            private System.Nullable<float> _Bar_Position_Company_Target;

            private System.Nullable<int> _Bar_Position_Collection_Frequency;

            private string _Bar_Position_Image_Reference;

            private System.Nullable<int> _Bar_Position_Machine_Type_AMEDIS_Code;

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

            private System.Nullable<bool> _Bar_Position_TX_Collection;

            private System.Nullable<bool> _Bar_Position_TX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_Movement;

            private System.Nullable<bool> _Bar_Position_TX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_EDC;

            private System.Nullable<bool> _Bar_Position_TX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_TX_Format;

            private System.Nullable<bool> _Bar_Position_TX_Format_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Collection;

            private System.Nullable<bool> _Bar_Position_RX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Movement;

            private System.Nullable<bool> _Bar_Position_RX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_EDC;

            private System.Nullable<bool> _Bar_Position_RX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_RX_Format;

            private System.Nullable<bool> _Bar_Position_RX_Format_Use_Default;

            private System.Nullable<float> _Bar_Position_Net_Target;

            private System.Nullable<int> _Bar_Position_Below_Net_Target_Counter;

            private System.Nullable<int> _Bar_Position_Below_Company_Target_Counter;

            private System.Nullable<bool> _Bar_Position_Security_Required;

            private System.Nullable<bool> _Bar_Position_Site_Has_Cashbox_Keys;

            private System.Nullable<bool> _Bar_Position_Site_Has_FreePlay_Access;

            private System.Nullable<bool> _Bar_Position_Override_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Shares;

            private System.Nullable<bool> _Bar_Position_Override_Licence;

            private System.Nullable<int> _Bar_Position_Category;

            private System.Nullable<float> _Bar_Position_PPL_Charge;

            private System.Nullable<float> _Bar_Position_PPL_Previous;

            private System.Nullable<float> _Bar_Position_PPL_Future;

            private string _Bar_Position_PPL_Past_Date;

            private string _Bar_Position_PPL_Future_Date;

            private System.Nullable<int> _Bar_Position_Float_Issued;

            private System.Nullable<int> _Bar_Position_Float_Recovered;

            private System.Nullable<bool> _Bar_Position_Use_Site_Share_For_Secondary_Brewery;

            private System.Nullable<bool> _Bar_Position_Prize_LOS;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID;

            private System.Nullable<int> _Bar_Position_Share_Schedule_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_Schedule;

            private System.Nullable<bool> _Bar_Position_Override_Share_Schedule;

            private System.Nullable<int> _Bar_Position_Last_Collection_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Schedule_To_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Rent_To_Schedule;

            private string _Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

            private string _Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID_From;

            private System.Nullable<bool> _Bar_Position_Disable_EDI_Export;

            private int _Bar_Position_Invoice_Period;

            private System.Nullable<int> _Bar_Position_Machine_Enabled;

            private System.Nullable<int> _Bar_Position_Note_Acceptor_Enabled;

            private System.Nullable<System.DateTime> _Bar_Position_Machine_Enabled_Date;

            public rsp_CreateBarPositionTemplateResult()
            {
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

            [Column(Storage = "_Duty_ID", DbType = "Int")]
            public System.Nullable<int> Duty_ID
            {
                get
                {
                    return this._Duty_ID;
                }
                set
                {
                    if ((this._Duty_ID != value))
                    {
                        this._Duty_ID = value;
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

            [Column(Storage = "_Bar_Position_Start_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Start_Date
            {
                get
                {
                    return this._Bar_Position_Start_Date;
                }
                set
                {
                    if ((this._Bar_Position_Start_Date != value))
                    {
                        this._Bar_Position_Start_Date = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Last_Collection_Date
            {
                get
                {
                    return this._Bar_Position_Last_Collection_Date;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_Date != value))
                    {
                        this._Bar_Position_Last_Collection_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")]
            public string Bar_Position_Collection_Rent_Paid_Until
            {
                get
                {
                    return this._Bar_Position_Collection_Rent_Paid_Until;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Rent_Paid_Until != value))
                    {
                        this._Bar_Position_Collection_Rent_Paid_Until = value;
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

            [Column(Storage = "_Bar_Position_Supplier_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_AMEDIS_Code = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Depot_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_Depot_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_Depot_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Depot_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_Depot_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_Supplier_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Area != value))
                    {
                        this._Bar_Position_Supplier_Area = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Service_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Service_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Service_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Service_Area != value))
                    {
                        this._Bar_Position_Supplier_Service_Area = value;
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

            [Column(Storage = "_Bar_Position_Company_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Company_Target
            {
                get
                {
                    return this._Bar_Position_Company_Target;
                }
                set
                {
                    if ((this._Bar_Position_Company_Target != value))
                    {
                        this._Bar_Position_Company_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Frequency", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Collection_Frequency
            {
                get
                {
                    return this._Bar_Position_Collection_Frequency;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Frequency != value))
                    {
                        this._Bar_Position_Collection_Frequency = value;
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

            [Column(Storage = "_Bar_Position_Machine_Type_AMEDIS_Code", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Type_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Machine_Type_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Type_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Machine_Type_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_TX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection
            {
                get
                {
                    return this._Bar_Position_TX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection != value))
                    {
                        this._Bar_Position_TX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_TX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement
            {
                get
                {
                    return this._Bar_Position_TX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement != value))
                    {
                        this._Bar_Position_TX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_TX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC
            {
                get
                {
                    return this._Bar_Position_TX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC != value))
                    {
                        this._Bar_Position_TX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_TX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_TX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_TX_Format
            {
                get
                {
                    return this._Bar_Position_TX_Format;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format != value))
                    {
                        this._Bar_Position_TX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format_Use_Default != value))
                    {
                        this._Bar_Position_TX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection
            {
                get
                {
                    return this._Bar_Position_RX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection != value))
                    {
                        this._Bar_Position_RX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_RX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement
            {
                get
                {
                    return this._Bar_Position_RX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement != value))
                    {
                        this._Bar_Position_RX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_RX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC
            {
                get
                {
                    return this._Bar_Position_RX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC != value))
                    {
                        this._Bar_Position_RX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_RX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_RX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_RX_Format
            {
                get
                {
                    return this._Bar_Position_RX_Format;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format != value))
                    {
                        this._Bar_Position_RX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format_Use_Default != value))
                    {
                        this._Bar_Position_RX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Net_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Net_Target
            {
                get
                {
                    return this._Bar_Position_Net_Target;
                }
                set
                {
                    if ((this._Bar_Position_Net_Target != value))
                    {
                        this._Bar_Position_Net_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Net_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Net_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Net_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Net_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Net_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Company_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Company_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Company_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Company_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Company_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Security_Required", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Security_Required
            {
                get
                {
                    return this._Bar_Position_Security_Required;
                }
                set
                {
                    if ((this._Bar_Position_Security_Required != value))
                    {
                        this._Bar_Position_Security_Required = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_Cashbox_Keys", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_Cashbox_Keys
            {
                get
                {
                    return this._Bar_Position_Site_Has_Cashbox_Keys;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_Cashbox_Keys != value))
                    {
                        this._Bar_Position_Site_Has_Cashbox_Keys = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_FreePlay_Access", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_FreePlay_Access
            {
                get
                {
                    return this._Bar_Position_Site_Has_FreePlay_Access;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_FreePlay_Access != value))
                    {
                        this._Bar_Position_Site_Has_FreePlay_Access = value;
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

            [Column(Storage = "_Bar_Position_PPL_Charge", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Charge
            {
                get
                {
                    return this._Bar_Position_PPL_Charge;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Charge != value))
                    {
                        this._Bar_Position_PPL_Charge = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Previous", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Previous
            {
                get
                {
                    return this._Bar_Position_PPL_Previous;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Previous != value))
                    {
                        this._Bar_Position_PPL_Previous = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Future
            {
                get
                {
                    return this._Bar_Position_PPL_Future;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future != value))
                    {
                        this._Bar_Position_PPL_Future = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Past_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Past_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Past_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Past_Date != value))
                    {
                        this._Bar_Position_PPL_Past_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Future_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Future_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future_Date != value))
                    {
                        this._Bar_Position_PPL_Future_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Issued", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Issued
            {
                get
                {
                    return this._Bar_Position_Float_Issued;
                }
                set
                {
                    if ((this._Bar_Position_Float_Issued != value))
                    {
                        this._Bar_Position_Float_Issued = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Recovered", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Recovered
            {
                get
                {
                    return this._Bar_Position_Float_Recovered;
                }
                set
                {
                    if ((this._Bar_Position_Float_Recovered != value))
                    {
                        this._Bar_Position_Float_Recovered = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_ID", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Last_Collection_ID
            {
                get
                {
                    return this._Bar_Position_Last_Collection_ID;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_ID != value))
                    {
                        this._Bar_Position_Last_Collection_ID = value;
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

            [Column(Storage = "_Bar_Position_Machine_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Enabled
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled != value))
                    {
                        this._Bar_Position_Machine_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Note_Acceptor_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Note_Acceptor_Enabled
            {
                get
                {
                    return this._Bar_Position_Note_Acceptor_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Note_Acceptor_Enabled != value))
                    {
                        this._Bar_Position_Note_Acceptor_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Machine_Enabled_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Bar_Position_Machine_Enabled_Date
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled_Date;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled_Date != value))
                    {
                        this._Bar_Position_Machine_Enabled_Date = value;
                    }
                }
            }
        }
        public partial class rsp_GetBarPositionDetailsBySiteIDResult
        {

            private int _Bar_Position_ID;

            private string _Bar_Position_Name;

            private string _Bar_Position_Location;

            private string _Machine_Type_Code;

            private string _Machine_Name;

            private string _Machine_BACTA_Code;

            private string _Machine_Stock_No;

            private System.Nullable<int> _Installation_ID;

            private string _Installation_End_Date;

            private System.Nullable<int> _Zone_ID;

            private string _Zone_Name;

            private string _Route_Name;

            public rsp_GetBarPositionDetailsBySiteIDResult()
            {
            }

            [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
            public int Bar_Position_ID
            {
                get
                {
                    return this._Bar_Position_ID;
                }
                set
                {
                    if ((this._Bar_Position_ID != value))
                    {
                        this._Bar_Position_ID = value;
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

            [Column(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
            public string Machine_Type_Code
            {
                get
                {
                    return this._Machine_Type_Code;
                }
                set
                {
                    if ((this._Machine_Type_Code != value))
                    {
                        this._Machine_Type_Code = value;
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

            [Column(Storage = "_Installation_ID", DbType = "Int")]
            public System.Nullable<int> Installation_ID
            {
                get
                {
                    return this._Installation_ID;
                }
                set
                {
                    if ((this._Installation_ID != value))
                    {
                        this._Installation_ID = value;
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

            [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
            public string Zone_Name
            {
                get
                {
                    return this._Zone_Name;
                }
                set
                {
                    if ((this._Zone_Name != value))
                    {
                        this._Zone_Name = value;
                    }
                }
            }

            [Column(Storage = "_Route_Name", DbType = "VarChar(50)")]
            public string Route_Name
            {
                get
                {
                    return this._Route_Name;
                }
                set
                {
                    if ((this._Route_Name != value))
                    {
                        this._Route_Name = value;
                    }
                }
            }
        }
        public partial class rsp_GetBarPositionDetailsByIdResult
        {

            private int _Bar_Position_ID;

            private System.Nullable<int> _Site_ID;

            private System.Nullable<int> _Zone_ID;

            private System.Nullable<int> _Access_Key_ID;

            private System.Nullable<bool> _Access_Key_ID_Default;

            private System.Nullable<int> _Terms_Group_ID;

            private string _Terms_Group_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Future_ID;

            private string _Terms_Group_Past_Changeover_Date;

            private System.Nullable<int> _Terms_Group_Past_ID;

            private System.Nullable<bool> _Terms_Group_ID_Default;

            private System.Nullable<int> _Duty_ID;

            private System.Nullable<int> _Depot_ID;

            private System.Nullable<int> _Machine_Type_ID;

            private string _Bar_Position_Name;

            private string _Bar_Position_Location;

            private string _Bar_Position_Start_Date;

            private string _Bar_Position_End_Date;

            private string _Bar_Position_Collection_Day;

            private string _Bar_Position_Price_Per_Play;

            private System.Nullable<bool> _Bar_Position_Price_Per_Play_Default;

            private string _Bar_Position_Jackpot;

            private System.Nullable<bool> _Bar_Position_Jackpot_Default;

            private string _Bar_Position_Percentage_Payout;

            private System.Nullable<bool> _Bar_Position_Percentage_Payout_Default;

            private string _Bar_Position_Last_Collection_Date;

            private string _Bar_Position_Collection_Rent_Paid_Until;

            private System.Nullable<int> _Bar_Position_Collection_Period;

            private string _Bar_Position_Supplier_AMEDIS_Code;

            private string _Bar_Position_Supplier_Depot_AMEDIS_Code;

            private string _Bar_Position_Supplier_Site_Code;

            private string _Bar_Position_Supplier_Position_Code;

            private string _Bar_Position_Supplier_Area;

            private string _Bar_Position_Supplier_Service_Area;

            private string _Bar_Position_Company_Position_Code;

            private System.Nullable<float> _Bar_Position_Company_Target;

            private System.Nullable<int> _Bar_Position_Collection_Frequency;

            private string _Bar_Position_Image_Reference;

            private System.Nullable<int> _Bar_Position_Machine_Type_AMEDIS_Code;

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

            private System.Nullable<bool> _Bar_Position_TX_Collection;

            private System.Nullable<bool> _Bar_Position_TX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_Movement;

            private System.Nullable<bool> _Bar_Position_TX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_TX_EDC;

            private System.Nullable<bool> _Bar_Position_TX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_TX_Format;

            private System.Nullable<bool> _Bar_Position_TX_Format_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Collection;

            private System.Nullable<bool> _Bar_Position_RX_Collection_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_Movement;

            private System.Nullable<bool> _Bar_Position_RX_Movement_Use_Default;

            private System.Nullable<bool> _Bar_Position_RX_EDC;

            private System.Nullable<bool> _Bar_Position_RX_EDC_Use_Detault;

            private System.Nullable<int> _Bar_Position_RX_Format;

            private System.Nullable<bool> _Bar_Position_RX_Format_Use_Default;

            private System.Nullable<float> _Bar_Position_Net_Target;

            private System.Nullable<int> _Bar_Position_Below_Net_Target_Counter;

            private System.Nullable<int> _Bar_Position_Below_Company_Target_Counter;

            private System.Nullable<bool> _Bar_Position_Security_Required;

            private System.Nullable<bool> _Bar_Position_Site_Has_Cashbox_Keys;

            private System.Nullable<bool> _Bar_Position_Site_Has_FreePlay_Access;

            private System.Nullable<bool> _Bar_Position_Override_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Shares;

            private System.Nullable<bool> _Bar_Position_Override_Licence;

            private System.Nullable<int> _Bar_Position_Category;

            private System.Nullable<float> _Bar_Position_PPL_Charge;

            private System.Nullable<float> _Bar_Position_PPL_Previous;

            private System.Nullable<float> _Bar_Position_PPL_Future;

            private string _Bar_Position_PPL_Past_Date;

            private string _Bar_Position_PPL_Future_Date;

            private System.Nullable<int> _Bar_Position_Float_Issued;

            private System.Nullable<int> _Bar_Position_Float_Recovered;

            private System.Nullable<bool> _Bar_Position_Use_Site_Share_For_Secondary_Brewery;

            private System.Nullable<bool> _Bar_Position_Prize_LOS;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID;

            private System.Nullable<int> _Bar_Position_Share_Schedule_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_Schedule;

            private System.Nullable<bool> _Bar_Position_Override_Share_Schedule;

            private System.Nullable<int> _Bar_Position_Last_Collection_ID;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Schedule_To_Rent;

            private System.Nullable<bool> _Bar_Position_Override_Rent_From_Rent_To_Schedule;

            private string _Bar_Position_Override_Rent_From_Schedule_To_Rent_Date;

            private string _Bar_Position_Override_Rent_From_Rent_To_Schedule_Date;

            private System.Nullable<int> _Bar_Position_Rent_Schedule_ID_From;

            private System.Nullable<bool> _Bar_Position_Disable_EDI_Export;

            private int _Bar_Position_Invoice_Period;

            private System.Nullable<int> _Bar_Position_Machine_Enabled;

            private System.Nullable<int> _Bar_Position_Note_Acceptor_Enabled;

            private System.Nullable<System.DateTime> _Bar_Position_Machine_Enabled_Date;

            private bool _Bar_Position_IsEnable;

            public rsp_GetBarPositionDetailsByIdResult()
            {
            }

            [Column(Storage = "_Bar_Position_ID", DbType = "Int NOT NULL")]
            public int Bar_Position_ID
            {
                get
                {
                    return this._Bar_Position_ID;
                }
                set
                {
                    if ((this._Bar_Position_ID != value))
                    {
                        this._Bar_Position_ID = value;
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

            [Column(Storage = "_Duty_ID", DbType = "Int")]
            public System.Nullable<int> Duty_ID
            {
                get
                {
                    return this._Duty_ID;
                }
                set
                {
                    if ((this._Duty_ID != value))
                    {
                        this._Duty_ID = value;
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

            [Column(Storage = "_Bar_Position_Start_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Start_Date
            {
                get
                {
                    return this._Bar_Position_Start_Date;
                }
                set
                {
                    if ((this._Bar_Position_Start_Date != value))
                    {
                        this._Bar_Position_Start_Date = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_Date", DbType = "VarChar(30)")]
            public string Bar_Position_Last_Collection_Date
            {
                get
                {
                    return this._Bar_Position_Last_Collection_Date;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_Date != value))
                    {
                        this._Bar_Position_Last_Collection_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Rent_Paid_Until", DbType = "VarChar(30)")]
            public string Bar_Position_Collection_Rent_Paid_Until
            {
                get
                {
                    return this._Bar_Position_Collection_Rent_Paid_Until;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Rent_Paid_Until != value))
                    {
                        this._Bar_Position_Collection_Rent_Paid_Until = value;
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

            [Column(Storage = "_Bar_Position_Supplier_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_AMEDIS_Code = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Depot_AMEDIS_Code", DbType = "VarChar(4)")]
            public string Bar_Position_Supplier_Depot_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Supplier_Depot_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Depot_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Supplier_Depot_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_Supplier_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Area != value))
                    {
                        this._Bar_Position_Supplier_Area = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Supplier_Service_Area", DbType = "VarChar(50)")]
            public string Bar_Position_Supplier_Service_Area
            {
                get
                {
                    return this._Bar_Position_Supplier_Service_Area;
                }
                set
                {
                    if ((this._Bar_Position_Supplier_Service_Area != value))
                    {
                        this._Bar_Position_Supplier_Service_Area = value;
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

            [Column(Storage = "_Bar_Position_Company_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Company_Target
            {
                get
                {
                    return this._Bar_Position_Company_Target;
                }
                set
                {
                    if ((this._Bar_Position_Company_Target != value))
                    {
                        this._Bar_Position_Company_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Collection_Frequency", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Collection_Frequency
            {
                get
                {
                    return this._Bar_Position_Collection_Frequency;
                }
                set
                {
                    if ((this._Bar_Position_Collection_Frequency != value))
                    {
                        this._Bar_Position_Collection_Frequency = value;
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

            [Column(Storage = "_Bar_Position_Machine_Type_AMEDIS_Code", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Type_AMEDIS_Code
            {
                get
                {
                    return this._Bar_Position_Machine_Type_AMEDIS_Code;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Type_AMEDIS_Code != value))
                    {
                        this._Bar_Position_Machine_Type_AMEDIS_Code = value;
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

            [Column(Storage = "_Bar_Position_TX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection
            {
                get
                {
                    return this._Bar_Position_TX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection != value))
                    {
                        this._Bar_Position_TX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_TX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement
            {
                get
                {
                    return this._Bar_Position_TX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement != value))
                    {
                        this._Bar_Position_TX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_TX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC
            {
                get
                {
                    return this._Bar_Position_TX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC != value))
                    {
                        this._Bar_Position_TX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_TX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_TX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_TX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_TX_Format
            {
                get
                {
                    return this._Bar_Position_TX_Format;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format != value))
                    {
                        this._Bar_Position_TX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_TX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_TX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_TX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_TX_Format_Use_Default != value))
                    {
                        this._Bar_Position_TX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection
            {
                get
                {
                    return this._Bar_Position_RX_Collection;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection != value))
                    {
                        this._Bar_Position_RX_Collection = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Collection_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Collection_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Collection_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Collection_Use_Default != value))
                    {
                        this._Bar_Position_RX_Collection_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement
            {
                get
                {
                    return this._Bar_Position_RX_Movement;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement != value))
                    {
                        this._Bar_Position_RX_Movement = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Movement_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Movement_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Movement_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Movement_Use_Default != value))
                    {
                        this._Bar_Position_RX_Movement_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC
            {
                get
                {
                    return this._Bar_Position_RX_EDC;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC != value))
                    {
                        this._Bar_Position_RX_EDC = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_EDC_Use_Detault", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_EDC_Use_Detault
            {
                get
                {
                    return this._Bar_Position_RX_EDC_Use_Detault;
                }
                set
                {
                    if ((this._Bar_Position_RX_EDC_Use_Detault != value))
                    {
                        this._Bar_Position_RX_EDC_Use_Detault = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format", DbType = "Int")]
            public System.Nullable<int> Bar_Position_RX_Format
            {
                get
                {
                    return this._Bar_Position_RX_Format;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format != value))
                    {
                        this._Bar_Position_RX_Format = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_RX_Format_Use_Default", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_RX_Format_Use_Default
            {
                get
                {
                    return this._Bar_Position_RX_Format_Use_Default;
                }
                set
                {
                    if ((this._Bar_Position_RX_Format_Use_Default != value))
                    {
                        this._Bar_Position_RX_Format_Use_Default = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Net_Target", DbType = "Real")]
            public System.Nullable<float> Bar_Position_Net_Target
            {
                get
                {
                    return this._Bar_Position_Net_Target;
                }
                set
                {
                    if ((this._Bar_Position_Net_Target != value))
                    {
                        this._Bar_Position_Net_Target = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Net_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Net_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Net_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Net_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Net_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Below_Company_Target_Counter", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Below_Company_Target_Counter
            {
                get
                {
                    return this._Bar_Position_Below_Company_Target_Counter;
                }
                set
                {
                    if ((this._Bar_Position_Below_Company_Target_Counter != value))
                    {
                        this._Bar_Position_Below_Company_Target_Counter = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Security_Required", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Security_Required
            {
                get
                {
                    return this._Bar_Position_Security_Required;
                }
                set
                {
                    if ((this._Bar_Position_Security_Required != value))
                    {
                        this._Bar_Position_Security_Required = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_Cashbox_Keys", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_Cashbox_Keys
            {
                get
                {
                    return this._Bar_Position_Site_Has_Cashbox_Keys;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_Cashbox_Keys != value))
                    {
                        this._Bar_Position_Site_Has_Cashbox_Keys = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Site_Has_FreePlay_Access", DbType = "Bit")]
            public System.Nullable<bool> Bar_Position_Site_Has_FreePlay_Access
            {
                get
                {
                    return this._Bar_Position_Site_Has_FreePlay_Access;
                }
                set
                {
                    if ((this._Bar_Position_Site_Has_FreePlay_Access != value))
                    {
                        this._Bar_Position_Site_Has_FreePlay_Access = value;
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

            [Column(Storage = "_Bar_Position_PPL_Charge", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Charge
            {
                get
                {
                    return this._Bar_Position_PPL_Charge;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Charge != value))
                    {
                        this._Bar_Position_PPL_Charge = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Previous", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Previous
            {
                get
                {
                    return this._Bar_Position_PPL_Previous;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Previous != value))
                    {
                        this._Bar_Position_PPL_Previous = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future", DbType = "Real")]
            public System.Nullable<float> Bar_Position_PPL_Future
            {
                get
                {
                    return this._Bar_Position_PPL_Future;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future != value))
                    {
                        this._Bar_Position_PPL_Future = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Past_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Past_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Past_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Past_Date != value))
                    {
                        this._Bar_Position_PPL_Past_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_PPL_Future_Date", DbType = "VarChar(30)")]
            public string Bar_Position_PPL_Future_Date
            {
                get
                {
                    return this._Bar_Position_PPL_Future_Date;
                }
                set
                {
                    if ((this._Bar_Position_PPL_Future_Date != value))
                    {
                        this._Bar_Position_PPL_Future_Date = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Issued", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Issued
            {
                get
                {
                    return this._Bar_Position_Float_Issued;
                }
                set
                {
                    if ((this._Bar_Position_Float_Issued != value))
                    {
                        this._Bar_Position_Float_Issued = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Float_Recovered", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Float_Recovered
            {
                get
                {
                    return this._Bar_Position_Float_Recovered;
                }
                set
                {
                    if ((this._Bar_Position_Float_Recovered != value))
                    {
                        this._Bar_Position_Float_Recovered = value;
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

            [Column(Storage = "_Bar_Position_Last_Collection_ID", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Last_Collection_ID
            {
                get
                {
                    return this._Bar_Position_Last_Collection_ID;
                }
                set
                {
                    if ((this._Bar_Position_Last_Collection_ID != value))
                    {
                        this._Bar_Position_Last_Collection_ID = value;
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

            [Column(Storage = "_Bar_Position_Machine_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Machine_Enabled
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled != value))
                    {
                        this._Bar_Position_Machine_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Note_Acceptor_Enabled", DbType = "Int")]
            public System.Nullable<int> Bar_Position_Note_Acceptor_Enabled
            {
                get
                {
                    return this._Bar_Position_Note_Acceptor_Enabled;
                }
                set
                {
                    if ((this._Bar_Position_Note_Acceptor_Enabled != value))
                    {
                        this._Bar_Position_Note_Acceptor_Enabled = value;
                    }
                }
            }

            [Column(Storage = "_Bar_Position_Machine_Enabled_Date", DbType = "DateTime")]
            public System.Nullable<System.DateTime> Bar_Position_Machine_Enabled_Date
            {
                get
                {
                    return this._Bar_Position_Machine_Enabled_Date;
                }
                set
                {
                    if ((this._Bar_Position_Machine_Enabled_Date != value))
                    {
                        this._Bar_Position_Machine_Enabled_Date = value;
                    }
                }
            }


            [Column(Storage = "_Bar_Position_IsEnable", DbType = "Bit")]
            public bool Bar_Position_IsEnable
            {
                get
                {
                    return this._Bar_Position_IsEnable;
                }
                set
                {
                    if ((this._Bar_Position_IsEnable != value))
                    {
                        this._Bar_Position_IsEnable = value;
                    }
                }
            }

        }
        public partial class rsp_GetLatestBarPositionIDResult
        {

            private System.Nullable<int> _BarPositionID;

            public rsp_GetLatestBarPositionIDResult()
            {
            }

            [Column(Storage = "_BarPositionID", DbType = "Int")]
            public System.Nullable<int> BarPositionID
            {
                get
                {
                    return this._BarPositionID;
                }
                set
                {
                    if ((this._BarPositionID != value))
                    {
                        this._BarPositionID = value;
                    }
                }
            }
        }
        public partial class rsp_GetOpeningHoursResult
        {

            private int _Standard_Opening_Hours_ID;

            private string _Standard_Opening_Hours_Description;

            public rsp_GetOpeningHoursResult()
            {
            }

            [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int NOT NULL")]
            public int Standard_Opening_Hours_ID
            {
                get
                {
                    return this._Standard_Opening_Hours_ID;
                }
                set
                {
                    if ((this._Standard_Opening_Hours_ID != value))
                    {
                        this._Standard_Opening_Hours_ID = value;
                    }
                }
            }

            [Column(Storage = "_Standard_Opening_Hours_Description", DbType = "VarChar(50)")]
            public string Standard_Opening_Hours_Description
            {
                get
                {
                    return this._Standard_Opening_Hours_Description;
                }
                set
                {
                    if ((this._Standard_Opening_Hours_Description != value))
                    {
                        this._Standard_Opening_Hours_Description = value;
                    }
                }
            }
        }

        //public partial class rsp_SelectZonesResult
        //{

        //    private int _Zone_ID;

        //    private string _Zone_Name;

        //    private int _AssignedZones;

        //    private int _Standard_Opening_Hours_ID;

        //    public rsp_SelectZonesResult()
        //    {
        //    }

        //    [Column(Storage = "_Zone_ID", DbType = "Int NOT NULL")]
        //    public int Zone_ID
        //    {
        //        get
        //        {
        //            return this._Zone_ID;
        //        }
        //        set
        //        {
        //            if ((this._Zone_ID != value))
        //            {
        //                this._Zone_ID = value;
        //            }
        //        }
        //    }

        //    [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
        //    public string Zone_Name
        //    {
        //        get
        //        {
        //            return this._Zone_Name;
        //        }
        //        set
        //        {
        //            if ((this._Zone_Name != value))
        //            {
        //                this._Zone_Name = value;
        //            }
        //        }
        //    }

        //    [Column(Storage = "_AssignedZones", DbType = "Int NOT NULL")]
        //    public int AssignedZones
        //    {
        //        get
        //        {
        //            return this._AssignedZones;
        //        }
        //        set
        //        {
        //            if ((this._AssignedZones != value))
        //            {
        //                this._AssignedZones = value;
        //            }
        //        }
        //    }
        //    [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int NOT NULL")]
        //    public int Standard_Opening_Hours_ID
        //    {
        //        get
        //        {
        //            return this._Standard_Opening_Hours_ID;
        //        }
        //        set
        //        {
        //            if ((this._Standard_Opening_Hours_ID != value))
        //            {
        //                this._Standard_Opening_Hours_ID = value;
        //            }
        //        }
        //    }
        //}
        public partial class rsp_SelectZonesResult
        {

            private int _Zone_ID;

            private string _Zone_Name;

            private int _AssignedZones;

            private bool _PromotionEnabled;

            private System.Nullable<int> _Standard_Opening_Hours_ID;

            public rsp_SelectZonesResult()
            {
            }

            [Column(Storage = "_Zone_ID", DbType = "Int NOT NULL")]
            public int Zone_ID
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

            [Column(Storage = "_Zone_Name", DbType = "VarChar(50)")]
            public string Zone_Name
            {
                get
                {
                    return this._Zone_Name;
                }
                set
                {
                    if ((this._Zone_Name != value))
                    {
                        this._Zone_Name = value;
                    }
                }
            }

            [Column(Storage = "_AssignedZones", DbType = "Int NOT NULL")]
            public int AssignedZones
            {
                get
                {
                    return this._AssignedZones;
                }
                set
                {
                    if ((this._AssignedZones != value))
                    {
                        this._AssignedZones = value;
                    }
                }
            }

            [Column(Storage = "_PromotionEnabled", DbType = "Bit")]
            public bool PromotionEnabled
            {
                get
                {
                    return this._PromotionEnabled;
                }
                set
                {
                    if ((this._PromotionEnabled != value))
                    {
                        this._PromotionEnabled = value;
                    }
                }
            }

            [Column(Storage = "_Standard_Opening_Hours_ID", DbType = "Int")]
            public System.Nullable<int> Standard_Opening_Hours_ID
            {
                get
                {
                    return this._Standard_Opening_Hours_ID;
                }
                set
                {
                    if ((this._Standard_Opening_Hours_ID != value))
                    {
                        this._Standard_Opening_Hours_ID = value;
                    }
                }
            }
        }
        public partial class rsp_GetBarPositionInfoResult
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

            private bool _Bar_Position_IsEnable;

            public rsp_GetBarPositionInfoResult()
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

            [Column(Storage = "_Bar_Position_IsEnable", DbType = "Bit")]
            public bool Bar_Position_IsEnable
            {
                get
                {
                    return this._Bar_Position_IsEnable;
                }
                set
                {
                    if ((this._Bar_Position_IsEnable != value))
                    {
                        this._Bar_Position_IsEnable = value;
                    }
                }
            }
        }
    }
}



