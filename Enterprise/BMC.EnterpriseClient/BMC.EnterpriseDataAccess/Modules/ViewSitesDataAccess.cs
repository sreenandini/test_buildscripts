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
        [Function(Name = "dbo.rsp_GetVSSiteDetails")]
        public ISingleResult<rsp_GetVSSiteDetailsResult> rsp_GetVSSiteDetails([Parameter(Name = "SiteID", DbType = "Int")] System.Nullable<int> siteID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteID);
            return ((ISingleResult<rsp_GetVSSiteDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetVSSiteTree")]
        public ISingleResult<rsp_GetVSSiteTreeResult> rsp_GetVSSiteTree(
                    [Parameter(Name = "UserId", DbType = "Int")] System.Nullable<int> userId,
                    [Parameter(Name = "ActiveSites", DbType = "Bit")] System.Nullable<bool> activeSites,
                    [Parameter(Name = "Search", DbType = "VarChar(512)")] string search,
                    [Parameter(Name = "HasSupplier", DbType = "Bit")] System.Nullable<bool> hasSupplier,
                    [Parameter(Name = "HasAddress", DbType = "Bit")] System.Nullable<bool> hasAddress,
                    [Parameter(Name = "HasSiteRep", DbType = "Bit")] System.Nullable<bool> hasSiteRep,
                    [Parameter(Name = "BarPositionId", DbType = "Int")] System.Nullable<int> barPositionId,
                    [Parameter(Name = "CompanyID", DbType = "Int")] System.Nullable<int> companyID,
                    [Parameter(Name = "Sub_Company_ID", DbType = "Int")] System.Nullable<int> sub_Company_ID,
                    [Parameter(Name = "Sub_Company_Region_ID", DbType = "Int")] System.Nullable<int> sub_Company_Region_ID,
                    [Parameter(Name = "Sub_Company_Area_ID", DbType = "Int")] System.Nullable<int> sub_Company_Area_ID,
                    [Parameter(Name = "Sub_Company_District_ID", DbType = "Int")] System.Nullable<int> sub_Company_District_ID,
                    [Parameter(Name = "Operator_ID", DbType = "Int")] System.Nullable<int> operator_ID,
                    [Parameter(Name = "Depot_ID", DbType = "Int")] System.Nullable<int> depot_ID,
                    [Parameter(Name = "Machine_Type_ID", DbType = "Int")] System.Nullable<int> machine_Type_ID,
                    [Parameter(Name = "Manufacturer_ID", DbType = "Int")] System.Nullable<int> manufacturer_ID,
                    [Parameter(Name = "SiteRepID", DbType = "Int")] System.Nullable<int> siteRepID,
                    [Parameter(Name = "ExcludeVacant", DbType = "Bit")] System.Nullable<bool> excludeVacant,
                    [Parameter(Name = "ModelSearch", DbType = "VarChar(512)")] string modelSearch,
                    [Parameter(Name = "PayoutPercentageFrom", DbType = "Real")] System.Nullable<float> payoutPercentageFrom,
                    [Parameter(Name = "PayoutPercentageTo", DbType = "Real")] System.Nullable<float> payoutPercentageTo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), userId, activeSites, search, hasSupplier, hasAddress, hasSiteRep, barPositionId, companyID, sub_Company_ID, sub_Company_Region_ID, sub_Company_Area_ID, sub_Company_District_ID, operator_ID, depot_ID, machine_Type_ID, manufacturer_ID, siteRepID, excludeVacant, modelSearch, payoutPercentageFrom, payoutPercentageTo);
            return ((ISingleResult<rsp_GetVSSiteTreeResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetCurrentInstallations")]
        public ISingleResult<rsp_GetCurrentInstallationsResult> rsp_GetCurrentInstallations([Parameter(DbType = "Int")] System.Nullable<int> site_id, [Parameter(Name = "BarPosID", DbType = "Int")] System.Nullable<int> barPosID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site_id, barPosID);
            return ((ISingleResult<rsp_GetCurrentInstallationsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteViewerColls")]
        public ISingleResult<rsp_GetSiteViewerCollsResult> rsp_GetSiteViewerColls([Parameter(DbType = "Int")] System.Nullable<int> records, [Parameter(DbType = "Int")] System.Nullable<int> weeks, [Parameter(DbType = "Int")] System.Nullable<int> periods, [Parameter(DbType = "Int")] System.Nullable<int> bar_position, [Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), records, weeks, periods, bar_position, site);
            return ((ISingleResult<rsp_GetSiteViewerCollsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteViewerCollsBatch")]
        public ISingleResult<rsp_GetSiteViewerCollsBatchResult> rsp_GetSiteViewerCollsBatch([Parameter(Name = "Weeks", DbType = "Int")] System.Nullable<int> weeks, [Parameter(Name = "Periods", DbType = "Int")] System.Nullable<int> periods, [Parameter(Name = "Site", DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), weeks, periods, site);
            return ((ISingleResult<rsp_GetSiteViewerCollsBatchResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteViewerCollsBatchData")]
        public ISingleResult<rsp_GetSiteViewerCollsBatchDataResult> rsp_GetSiteViewerCollsBatchData([Parameter(Name = "BatchId", DbType = "Int")] System.Nullable<int> batchId, [Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchId, site);
            return ((ISingleResult<rsp_GetSiteViewerCollsBatchDataResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSiteViewerCollsBatchWeek")]
        public ISingleResult<rsp_GetSiteViewerCollsBatchWeekResult> rsp_GetSiteViewerCollsBatchWeek([Parameter(Name = "WeekID", DbType = "Int")] System.Nullable<int> weekID, [Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), weekID, site);
            return ((ISingleResult<rsp_GetSiteViewerCollsBatchWeekResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetAreaDetails")]
        public ISingleResult<rsp_GetAreaDetailsResult> rsp_GetAreaDetails([Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> companyID, [Parameter(DbType = "Int")] System.Nullable<int> subcompany)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region, companyID, subcompany);
            return ((ISingleResult<rsp_GetAreaDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDistrictDetails")]
        public ISingleResult<rsp_GetDistrictDetailsResult> rsp_GetDistrictDetails([Parameter(DbType = "Int")] System.Nullable<int> region, [Parameter(DbType = "Int")] System.Nullable<int> area, [Parameter(DbType = "Int")] System.Nullable<int> subcompany_id, [Parameter(DbType = "Int")] System.Nullable<int> companyID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), region, area, subcompany_id, companyID);
            return ((ISingleResult<rsp_GetDistrictDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetVSStaffDetails")]
        public ISingleResult<rsp_GetVSStaffDetailsResult> rsp_GetVSStaffDetails()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<rsp_GetVSStaffDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepoDetails")]
        public ISingleResult<rsp_GetDepoDetailsResult> rsp_GetDepoDetails([Parameter(DbType = "Int")] System.Nullable<int> site)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), site);
            return ((ISingleResult<rsp_GetDepoDetailsResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetManufacturer")]
        public ISingleResult<rsp_GetManufacturerResult> rsp_GetManufacturer([Parameter(Name = "Manufacturer_Name", DbType = "VarChar(50)")] string manufacturer_Name)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), manufacturer_Name);
            return ((ISingleResult<rsp_GetManufacturerResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyRegionsForFilter")]
        public ISingleResult<rsp_GetSubCompanyRegionsForFilterResult> rsp_GetSubCompanyRegionsForFilter([Parameter(Name = "SubCompanyId", DbType = "Int")] System.Nullable<int> subCompanyId, [Parameter(Name = "CompanyId", DbType = "Int")] System.Nullable<int> companyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), subCompanyId, companyId);
            return ((ISingleResult<rsp_GetSubCompanyRegionsForFilterResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_AssetGameReport")]
        public ISingleResult<rsp_AssetGameReportResult> GetAssetGameReport([Parameter(Name = "BarPosition_ID", DbType = "Int")] System.Nullable<int> barPosition_ID, [Parameter(Name = "Site_Id", DbType = "Int")] System.Nullable<int> Site_Id, [Parameter(Name = "StartDate", DbType = "DateTime")] System.Nullable<System.DateTime> startDate, [Parameter(Name = "EndDate", DbType = "DateTime")] System.Nullable<System.DateTime> endDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), barPosition_ID, Site_Id,startDate, endDate);
            return ((ISingleResult<rsp_AssetGameReportResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetLastBatchId")]
        public ISingleResult<rsp_GetLastBatchIdResult> rsp_GetLastBatchId([Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId, [Parameter(Name = "Weeks", DbType = "Int")] System.Nullable<int> weeks, [Parameter(Name = "Periods", DbType = "Int")] System.Nullable<int> periods)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId, weeks, periods);
            return ((ISingleResult<rsp_GetLastBatchIdResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetLastWeekId")]
        public ISingleResult<rsp_GetLastWeekIdResult> rsp_GetLastWeekId([Parameter(Name = "SiteId", DbType = "Int")] System.Nullable<int> siteId, [Parameter(Name = "Weeks", DbType = "Int")] System.Nullable<int> weeks)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), siteId, weeks);
            return ((ISingleResult<rsp_GetLastWeekIdResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_MultiGameLibrary")]
        public ISingleResult<rsp_MultiGameLibraryResult> rsp_MultiGameLibrary([Parameter(Name = "Installation_id", DbType = "Int")] System.Nullable<int> installation_id)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), installation_id);
            return ((ISingleResult<rsp_MultiGameLibraryResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetPaytableForGame")]
        public ISingleResult<rsp_GetPaytableForGameResult> rsp_GetPaytableForGame([Parameter(Name = "GroupID", DbType = "Int")] System.Nullable<int> groupID, [Parameter(Name = "Installation_No", DbType = "Int")] System.Nullable<int> installation_No)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupID, installation_No);
            return ((ISingleResult<rsp_GetPaytableForGameResult>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_VSDeleteCollectionBatch")]
        public int usp_VSDeleteCollectionBatch([Parameter(Name = "BatchID", DbType = "Int")] System.Nullable<int> batchID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), batchID);
            return ((int)(result.ReturnValue));
        }
        [Function(Name = "dbo.usp_GetRemarkscollection")]
        public int GetRemarkscollection([Parameter(Name = "Collection_ID", DbType = "Int")] System.Nullable<int> collection_ID, [Parameter(Name = "Remarks", DbType = "NText")] string remarks)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), collection_ID, remarks);
            return ((int)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepreciationPolicyInfo")]
        public ISingleResult<rsp_GetDepreciationPolicyInfoResult> GetDepreciationPolicyInfo([Parameter(Name = "MachineID", DbType = "Int")] System.Nullable<int> machineID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), machineID);
            return ((ISingleResult<rsp_GetDepreciationPolicyInfoResult>)(result.ReturnValue));
        }
    }

    public partial class rsp_GetVSSiteDetailsResult
    {

        private System.Nullable<int> _Sub_Company_ID;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private string _Site_Address_4;

        private string _Site_Address_5;

        private string _Site_Code;

        private string _Site_Name;

        private string _Company_Name;

        private string _Sub_Company_Name;

        private string _Site_Manager;

        private string _Site_Postcode;

        private string _Site_Reference;

        private string _Site_Phone_No;

        private string _Sub_Company_Area_Name;

        private string _Sub_Company_Region_Name;

        private string _Sub_Company_District_Name;

        private string _Site_Trade_Type;

        private string _Site_Memo;

        private string _Sec_Brewery_Name;

        private string _Staff_Last_Name;

        private string _Staff_First_Name;

        private string _Site_Start_Date;

        public rsp_GetVSSiteDetailsResult()
        {
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

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
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

        [Column(Storage = "_Sub_Company_Area_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Area_Name
        {
            get
            {
                return this._Sub_Company_Area_Name;
            }
            set
            {
                if ((this._Sub_Company_Area_Name != value))
                {
                    this._Sub_Company_Area_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Region_Name
        {
            get
            {
                return this._Sub_Company_Region_Name;
            }
            set
            {
                if ((this._Sub_Company_Region_Name != value))
                {
                    this._Sub_Company_Region_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_District_Name", DbType = "VarChar(50)")]
        public string Sub_Company_District_Name
        {
            get
            {
                return this._Sub_Company_District_Name;
            }
            set
            {
                if ((this._Sub_Company_District_Name != value))
                {
                    this._Sub_Company_District_Name = value;
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

        [Column(Storage = "_Sec_Brewery_Name", DbType = "VarChar(50)")]
        public string Sec_Brewery_Name
        {
            get
            {
                return this._Sec_Brewery_Name;
            }
            set
            {
                if ((this._Sec_Brewery_Name != value))
                {
                    this._Sec_Brewery_Name = value;
                }
            }
        }

        [Column(Storage = "_Staff_Last_Name", DbType = "VarChar(50)")]
        public string Staff_Last_Name
        {
            get
            {
                return this._Staff_Last_Name;
            }
            set
            {
                if ((this._Staff_Last_Name != value))
                {
                    this._Staff_Last_Name = value;
                }
            }
        }

        [Column(Storage = "_Staff_First_Name", DbType = "VarChar(50)")]
        public string Staff_First_Name
        {
            get
            {
                return this._Staff_First_Name;
            }
            set
            {
                if ((this._Staff_First_Name != value))
                {
                    this._Staff_First_Name = value;
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
    }

    public partial class rsp_GetVSSiteTreeResult
    {

        private int _Company_ID;

        private string _Company_Name;

        private int _Sub_Company_ID;

        private string _Sub_Company_Name;

        private int _Site_ID;

        private string _Site_Name;

        private string _Site_Code;

        private string _Site_Address_1;

        private string _Site_Address_2;

        private string _Site_Address_3;

        private int _Site_Status_Id;

        private System.Nullable<System.DateTime> _Site_Inactive_Date;

        private string _WebURL;

        public rsp_GetVSSiteTreeResult()
        {
        }

        [Column(Storage = "_Company_ID", DbType = "Int NOT NULL")]
        public int Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
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

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
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

        [Column(Storage = "_Site_Status_Id", DbType = "Int NOT NULL")]
        public int Site_Status_Id
        {
            get
            {
                return this._Site_Status_Id;
            }
            set
            {
                if ((this._Site_Status_Id != value))
                {
                    this._Site_Status_Id = value;
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

        [Column(Storage = "_WebURL", DbType = "VarChar(50)")]
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
    }

    public partial class rsp_GetCurrentInstallationsResult
    {

        private int _Bar_Position_ID;

        private System.Nullable<int> _Bar_Position;

        private string _Bar_Position_Name;

        private string _Bar_Position_End_Date;

        private string _Bar_Pos_Type_Code;

        private string _Bar_Position_Supplier_Position_Code;

        private string _Bar_Position_Supplier_Site_Code;

        private System.Nullable<bool> _Bar_Position_Use_Terms;

        private System.Nullable<float> _Bar_Position_Net_Target;

        private string _Bar_Position_Price_Per_Play;

        private System.Nullable<int> _Bar_Position_Category;

        private System.Nullable<float> _Bar_Position_Company_Target;

        private string _Bar_Position_Collection_Day;

        private string _Bar_Position_Company_Position_Code;

        private string _Bar_Position_Jackpot;

        private string _Bar_Position_Percentage_Payout;

        private System.Nullable<int> _Bar_Position_Machine_Enabled;

        private System.Nullable<int> _Bar_Position_Note_Acceptor_Enabled;

        private string _Zone_Name;

        private System.Nullable<int> _installation_token_value;

        private System.Nullable<int> _Installation_ID;

        private string _Installation_Start_Date;

        private string _Installation_End_Date;

        private System.Nullable<bool> _Installation_Change_Flag;

        private string _Installation_BACTA_Code_Override;

        private System.Nullable<int> _Installation_Price_Per_Play;

        private System.Nullable<int> _Installation_Jackpot_Value;

        private System.Nullable<float> _Installation_Percentage_Payout;

        private System.Nullable<int> _Datapak_ID;

        private System.Nullable<int> _Installation_RDC_Datapak_Version;

        private System.Nullable<int> _Installation_RDC_Datapak_Type;

        private System.Nullable<int> _Machine_ID;

        private string _Machine_Name;

        private string _Machine_Stock_No;

        private System.Nullable<bool> _Machine_Test;

        private string _Machine_Manufacturers_Serial_No;

        private string _Machine_Alternative_Serial_Numbers;

        private string _Machine_BACTA_Code;

        private System.Nullable<bool> _Machine_Due_In_Stock;

        private string _Machine_Due_In_Stock_Date;

        private string _Machine_Class_Model_Code;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private string _Machine_Type_Code;

        private string _Operator_Name;

        private string _Depot_Name;

        private System.Nullable<int> _Depot_ID;

        private string _Installation_RDC_Machine_Code;

        private string _Installation_RDC_Secondary_Machine_Code;

        private System.Nullable<int> _Standard_Opening_Hours_ID;

        private string _Standard_Opening_Hours_Description;

        private System.Nullable<int> _Installation_Datapak_SecondaryMachineCode_Version;

        private System.Nullable<char> _Installation_Datapak_SecondaryMachineCode_CashOrToken;

        private System.Nullable<int> _Installation_Datapak_SecondaryMachineCode_PercentagePayout;

        private string _Installation_Datapak_SecondaryMachineCode_Type;

        private System.Nullable<int> _Installation_Datapak_SecondaryMachineCode_PriceOfPlay;

        private string _Installation_Datapak_SecondaryMachineCode_CoinSystem;

        private string _Installation_Datapak_SecondaryMachineCode_Dataport;

        private System.Nullable<int> _Installation_Datapak_SecondaryMachineCode_Jackpot;

        private System.Nullable<int> _Installation_Datapak_FirmwareVersion;

        private System.Nullable<int> _Installation_Datapak_FirmwareRevision;

        private string _Installation_Status;

        private string _Manufacturer_Name;

        private string _GMUNo;

        private System.Nullable<bool> _IsMultiGame;

        public rsp_GetCurrentInstallationsResult()
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

        [Column(Storage = "_Bar_Position", DbType = "Int")]
        public System.Nullable<int> Bar_Position
        {
            get
            {
                return this._Bar_Position;
            }
            set
            {
                if ((this._Bar_Position != value))
                {
                    this._Bar_Position = value;
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

        [Column(Storage = "_Bar_Pos_Type_Code", DbType = "VarChar(50)")]
        public string Bar_Pos_Type_Code
        {
            get
            {
                return this._Bar_Pos_Type_Code;
            }
            set
            {
                if ((this._Bar_Pos_Type_Code != value))
                {
                    this._Bar_Pos_Type_Code = value;
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

        [Column(Storage = "_installation_token_value", DbType = "Int")]
        public System.Nullable<int> installation_token_value
        {
            get
            {
                return this._installation_token_value;
            }
            set
            {
                if ((this._installation_token_value != value))
                {
                    this._installation_token_value = value;
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

        [Column(Storage = "_Installation_Start_Date", DbType = "VarChar(30)")]
        public string Installation_Start_Date
        {
            get
            {
                return this._Installation_Start_Date;
            }
            set
            {
                if ((this._Installation_Start_Date != value))
                {
                    this._Installation_Start_Date = value;
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

        [Column(Storage = "_Installation_Change_Flag", DbType = "Bit")]
        public System.Nullable<bool> Installation_Change_Flag
        {
            get
            {
                return this._Installation_Change_Flag;
            }
            set
            {
                if ((this._Installation_Change_Flag != value))
                {
                    this._Installation_Change_Flag = value;
                }
            }
        }

        [Column(Storage = "_Installation_BACTA_Code_Override", DbType = "VarChar(8)")]
        public string Installation_BACTA_Code_Override
        {
            get
            {
                return this._Installation_BACTA_Code_Override;
            }
            set
            {
                if ((this._Installation_BACTA_Code_Override != value))
                {
                    this._Installation_BACTA_Code_Override = value;
                }
            }
        }

        [Column(Storage = "_Installation_Price_Per_Play", DbType = "Int")]
        public System.Nullable<int> Installation_Price_Per_Play
        {
            get
            {
                return this._Installation_Price_Per_Play;
            }
            set
            {
                if ((this._Installation_Price_Per_Play != value))
                {
                    this._Installation_Price_Per_Play = value;
                }
            }
        }

        [Column(Storage = "_Installation_Jackpot_Value", DbType = "Int")]
        public System.Nullable<int> Installation_Jackpot_Value
        {
            get
            {
                return this._Installation_Jackpot_Value;
            }
            set
            {
                if ((this._Installation_Jackpot_Value != value))
                {
                    this._Installation_Jackpot_Value = value;
                }
            }
        }

        [Column(Storage = "_Installation_Percentage_Payout", DbType = "Real")]
        public System.Nullable<float> Installation_Percentage_Payout
        {
            get
            {
                return this._Installation_Percentage_Payout;
            }
            set
            {
                if ((this._Installation_Percentage_Payout != value))
                {
                    this._Installation_Percentage_Payout = value;
                }
            }
        }

        [Column(Storage = "_Datapak_ID", DbType = "Int")]
        public System.Nullable<int> Datapak_ID
        {
            get
            {
                return this._Datapak_ID;
            }
            set
            {
                if ((this._Datapak_ID != value))
                {
                    this._Datapak_ID = value;
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Datapak_Version", DbType = "Int")]
        public System.Nullable<int> Installation_RDC_Datapak_Version
        {
            get
            {
                return this._Installation_RDC_Datapak_Version;
            }
            set
            {
                if ((this._Installation_RDC_Datapak_Version != value))
                {
                    this._Installation_RDC_Datapak_Version = value;
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Datapak_Type", DbType = "Int")]
        public System.Nullable<int> Installation_RDC_Datapak_Type
        {
            get
            {
                return this._Installation_RDC_Datapak_Type;
            }
            set
            {
                if ((this._Installation_RDC_Datapak_Type != value))
                {
                    this._Installation_RDC_Datapak_Type = value;
                }
            }
        }

        [Column(Storage = "_Machine_ID", DbType = "Int")]
        public System.Nullable<int> Machine_ID
        {
            get
            {
                return this._Machine_ID;
            }
            set
            {
                if ((this._Machine_ID != value))
                {
                    this._Machine_ID = value;
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

        [Column(Storage = "_Machine_Test", DbType = "Bit")]
        public System.Nullable<bool> Machine_Test
        {
            get
            {
                return this._Machine_Test;
            }
            set
            {
                if ((this._Machine_Test != value))
                {
                    this._Machine_Test = value;
                }
            }
        }

        [Column(Storage = "_Machine_Manufacturers_Serial_No", DbType = "VarChar(50)")]
        public string Machine_Manufacturers_Serial_No
        {
            get
            {
                return this._Machine_Manufacturers_Serial_No;
            }
            set
            {
                if ((this._Machine_Manufacturers_Serial_No != value))
                {
                    this._Machine_Manufacturers_Serial_No = value;
                }
            }
        }

        [Column(Storage = "_Machine_Alternative_Serial_Numbers", DbType = "VarChar(50)")]
        public string Machine_Alternative_Serial_Numbers
        {
            get
            {
                return this._Machine_Alternative_Serial_Numbers;
            }
            set
            {
                if ((this._Machine_Alternative_Serial_Numbers != value))
                {
                    this._Machine_Alternative_Serial_Numbers = value;
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

        [Column(Storage = "_Machine_Due_In_Stock", DbType = "Bit")]
        public System.Nullable<bool> Machine_Due_In_Stock
        {
            get
            {
                return this._Machine_Due_In_Stock;
            }
            set
            {
                if ((this._Machine_Due_In_Stock != value))
                {
                    this._Machine_Due_In_Stock = value;
                }
            }
        }

        [Column(Storage = "_Machine_Due_In_Stock_Date", DbType = "VarChar(30)")]
        public string Machine_Due_In_Stock_Date
        {
            get
            {
                return this._Machine_Due_In_Stock_Date;
            }
            set
            {
                if ((this._Machine_Due_In_Stock_Date != value))
                {
                    this._Machine_Due_In_Stock_Date = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_Model_Code", DbType = "VarChar(50)")]
        public string Machine_Class_Model_Code
        {
            get
            {
                return this._Machine_Class_Model_Code;
            }
            set
            {
                if ((this._Machine_Class_Model_Code != value))
                {
                    this._Machine_Class_Model_Code = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
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

        [Column(Storage = "_Installation_RDC_Machine_Code", DbType = "VarChar(10)")]
        public string Installation_RDC_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Machine_Code != value))
                {
                    this._Installation_RDC_Machine_Code = value;
                }
            }
        }

        [Column(Storage = "_Installation_RDC_Secondary_Machine_Code", DbType = "VarChar(20)")]
        public string Installation_RDC_Secondary_Machine_Code
        {
            get
            {
                return this._Installation_RDC_Secondary_Machine_Code;
            }
            set
            {
                if ((this._Installation_RDC_Secondary_Machine_Code != value))
                {
                    this._Installation_RDC_Secondary_Machine_Code = value;
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

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_Version", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_SecondaryMachineCode_Version
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_Version;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_Version != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_Version = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_CashOrToken", DbType = "VarChar(1) NULL")]
        public System.Nullable<char> Installation_Datapak_SecondaryMachineCode_CashOrToken
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_CashOrToken;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_CashOrToken != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_CashOrToken = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_PercentagePayout", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_SecondaryMachineCode_PercentagePayout
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_PercentagePayout;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_PercentagePayout != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_PercentagePayout = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_Type", DbType = "VarChar(5)")]
        public string Installation_Datapak_SecondaryMachineCode_Type
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_Type;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_Type != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_Type = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_PriceOfPlay", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_SecondaryMachineCode_PriceOfPlay
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_PriceOfPlay;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_PriceOfPlay != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_PriceOfPlay = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_CoinSystem", DbType = "VarChar(5)")]
        public string Installation_Datapak_SecondaryMachineCode_CoinSystem
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_CoinSystem;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_CoinSystem != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_CoinSystem = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_Dataport", DbType = "VarChar(5)")]
        public string Installation_Datapak_SecondaryMachineCode_Dataport
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_Dataport;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_Dataport != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_Dataport = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_SecondaryMachineCode_Jackpot", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_SecondaryMachineCode_Jackpot
        {
            get
            {
                return this._Installation_Datapak_SecondaryMachineCode_Jackpot;
            }
            set
            {
                if ((this._Installation_Datapak_SecondaryMachineCode_Jackpot != value))
                {
                    this._Installation_Datapak_SecondaryMachineCode_Jackpot = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_FirmwareVersion", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_FirmwareVersion
        {
            get
            {
                return this._Installation_Datapak_FirmwareVersion;
            }
            set
            {
                if ((this._Installation_Datapak_FirmwareVersion != value))
                {
                    this._Installation_Datapak_FirmwareVersion = value;
                }
            }
        }

        [Column(Storage = "_Installation_Datapak_FirmwareRevision", DbType = "Int")]
        public System.Nullable<int> Installation_Datapak_FirmwareRevision
        {
            get
            {
                return this._Installation_Datapak_FirmwareRevision;
            }
            set
            {
                if ((this._Installation_Datapak_FirmwareRevision != value))
                {
                    this._Installation_Datapak_FirmwareRevision = value;
                }
            }
        }

        [Column(Storage = "_Installation_Status", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string Installation_Status
        {
            get
            {
                return this._Installation_Status;
            }
            set
            {
                if ((this._Installation_Status != value))
                {
                    this._Installation_Status = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer_Name", DbType = "VarChar(50)")]
        public string Manufacturer_Name
        {
            get
            {
                return this._Manufacturer_Name;
            }
            set
            {
                if ((this._Manufacturer_Name != value))
                {
                    this._Manufacturer_Name = value;
                }
            }
        }

        [Column(Storage = "_GMUNo", DbType = "VarChar(50)")]
        public string GMUNo
        {
            get
            {
                return this._GMUNo;
            }
            set
            {
                if ((this._GMUNo != value))
                {
                    this._GMUNo = value;
                }
            }
        }

        [Column(Storage = "_IsMultiGame", DbType = "Bit")]
        public System.Nullable<bool> IsMultiGame
        {
            get
            {
                return this._IsMultiGame;
            }
            set
            {
                if ((this._IsMultiGame != value))
                {
                    this._IsMultiGame = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteViewerCollsResult
    {

        private System.Nullable<int> _Collection_Source;

        private System.Nullable<bool> _Batch_Received_All;

        private int _Batch_ID;

        private System.Nullable<int> _Period_End_ID;

        private System.Nullable<bool> _Collection_Terms_Invalid;

        private System.Nullable<bool> _COLLECTION_REPLACEMENT;

        private System.Nullable<bool> _Collection_Terms_Invalid1;

        private System.Nullable<bool> _Collection_Terms_Invalid_Ignore;

        private System.Nullable<bool> _Collection_Processed_Through_Terms;

        private int _EDI_Import_Log_ID;

        private int _Collection_ID;

        private string _Collection_Date;

        private string _Machine_Name;

        private string _GameName;

        private string _Operator_Name;

        private System.Nullable<int> _Depot_ID;

        private string _Depot_Name;

        private int _Collection_Days;

        private System.Nullable<double> _Cash_Take;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<float> _PercentageIn;

        private System.Nullable<double> _PercentageOut;

        private float _Collection_declared_Notes;

        private System.Nullable<float> _Refunds;

        private System.Nullable<float> _Net_Coin;

        private float _Collection_Ticket_Balance;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _Shortpay;

        private float _Declared_Notes;

        private float _Note_Var;

        private System.Nullable<float> _Coin_Var;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<float> _Net_Coin1;

        private System.Nullable<double> _DecHandpay1;

        private float _RDC_Notes;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _Ticket_Var;

        private System.Nullable<double> _Handpay_Var;

        private System.Nullable<double> _Take_Var;

        private System.Nullable<double> _DecTicketBalance1;

        private float _RDC_Coins;

        private System.Nullable<double> _Void;

        private float _RDC_Ticket_Balance;

        private System.Nullable<double> _WinLossVar;

        private float _Collection_Handpay_Var;

        private System.Nullable<double> _RDC_Take;

        private float _Refills;

        private System.Nullable<float> _Collection_Supplier_Share;

        private System.Nullable<float> _Collection_Company_Share;

        private System.Nullable<float> _Collection_Location_Share;

        private System.Nullable<float> _Collection_Other_Share;

        private System.Nullable<float> _Collection_AMLD;

        private string _Remarks;

        private System.Nullable<int> _Period_Id;

        private System.Nullable<int> _Period_End_ID1;

        private int _Lag;

        private System.Nullable<float> _TargetVariance;

        private System.Nullable<double> _RDCIn;

        private System.Nullable<double> _RDCOut;

        private float _RDCCash;

        private float _RDCVar;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _DecEftOut;

        private string _Secondary_Brewery_Name;

        private System.Nullable<int> _Secondary_Sub_Company_Period_End_ID;

        private float _RDC_Coins_Out;

        private System.Nullable<int> _Week_End_ID;

        private System.Nullable<double> _VTP;

        private string _Sub_Company_Name;

        private string _Company_Name;

        private System.Nullable<double> _MeterCashIn;

        private System.Nullable<double> _MeterCashOut;

        private System.Nullable<double> _Handle;

        private System.Nullable<float> _PIndex;

        private int _PacePrev9Days;

        private int _PacePrev9Cash;

        private int _PaceDays;

        private int _PaceCash;

        private float _Collection_GPT;

        private float _Collection_FOBT_Stakes;

        private float _Collection_FOBT_Payout;

        private int _Collection_Transactions;

        private string _Machine_Start_Date;

        public rsp_GetSiteViewerCollsResult()
        {
        }

        [Column(Storage = "_Collection_Source", DbType = "Int")]
        public System.Nullable<int> Collection_Source
        {
            get
            {
                return this._Collection_Source;
            }
            set
            {
                if ((this._Collection_Source != value))
                {
                    this._Collection_Source = value;
                }
            }
        }

        [Column(Storage = "_Batch_Received_All", DbType = "Bit")]
        public System.Nullable<bool> Batch_Received_All
        {
            get
            {
                return this._Batch_Received_All;
            }
            set
            {
                if ((this._Batch_Received_All != value))
                {
                    this._Batch_Received_All = value;
                }
            }
        }

        [Column(Storage = "_Batch_ID", DbType = "Int NOT NULL")]
        public int Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }

        [Column(Storage = "_Period_End_ID", DbType = "Int")]
        public System.Nullable<int> Period_End_ID
        {
            get
            {
                return this._Period_End_ID;
            }
            set
            {
                if ((this._Period_End_ID != value))
                {
                    this._Period_End_ID = value;
                }
            }
        }

        [Column(Storage = "_Collection_Terms_Invalid", DbType = "Bit")]
        public System.Nullable<bool> Collection_Terms_Invalid
        {
            get
            {
                return this._Collection_Terms_Invalid;
            }
            set
            {
                if ((this._Collection_Terms_Invalid != value))
                {
                    this._Collection_Terms_Invalid = value;
                }
            }
        }

        [Column(Storage = "_COLLECTION_REPLACEMENT", DbType = "Bit")]
        public System.Nullable<bool> COLLECTION_REPLACEMENT
        {
            get
            {
                return this._COLLECTION_REPLACEMENT;
            }
            set
            {
                if ((this._COLLECTION_REPLACEMENT != value))
                {
                    this._COLLECTION_REPLACEMENT = value;
                }
            }
        }

        [Column(Storage = "_Collection_Terms_Invalid1", DbType = "Bit")]
        public System.Nullable<bool> Collection_Terms_Invalid1
        {
            get
            {
                return this._Collection_Terms_Invalid1;
            }
            set
            {
                if ((this._Collection_Terms_Invalid1 != value))
                {
                    this._Collection_Terms_Invalid1 = value;
                }
            }
        }

        [Column(Storage = "_Collection_Terms_Invalid_Ignore", DbType = "Bit")]
        public System.Nullable<bool> Collection_Terms_Invalid_Ignore
        {
            get
            {
                return this._Collection_Terms_Invalid_Ignore;
            }
            set
            {
                if ((this._Collection_Terms_Invalid_Ignore != value))
                {
                    this._Collection_Terms_Invalid_Ignore = value;
                }
            }
        }

        [Column(Storage = "_Collection_Processed_Through_Terms", DbType = "Bit")]
        public System.Nullable<bool> Collection_Processed_Through_Terms
        {
            get
            {
                return this._Collection_Processed_Through_Terms;
            }
            set
            {
                if ((this._Collection_Processed_Through_Terms != value))
                {
                    this._Collection_Processed_Through_Terms = value;
                }
            }
        }

        [Column(Storage = "_EDI_Import_Log_ID", DbType = "Int NOT NULL")]
        public int EDI_Import_Log_ID
        {
            get
            {
                return this._EDI_Import_Log_ID;
            }
            set
            {
                if ((this._EDI_Import_Log_ID != value))
                {
                    this._EDI_Import_Log_ID = value;
                }
            }
        }

        [Column(Storage = "_Collection_ID", DbType = "Int NOT NULL")]
        public int Collection_ID
        {
            get
            {
                return this._Collection_ID;
            }
            set
            {
                if ((this._Collection_ID != value))
                {
                    this._Collection_ID = value;
                }
            }
        }

        [Column(Storage = "_Collection_Date", DbType = "VarChar(30)")]
        public string Collection_Date
        {
            get
            {
                return this._Collection_Date;
            }
            set
            {
                if ((this._Collection_Date != value))
                {
                    this._Collection_Date = value;
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

        [Column(Storage = "_GameName", DbType = "VarChar(50)")]
        public string GameName
        {
            get
            {
                return this._GameName;
            }
            set
            {
                if ((this._GameName != value))
                {
                    this._GameName = value;
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

        [Column(Storage = "_Depot_ID", DbType = "INT")]
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

        [Column(Storage = "_Collection_Days", DbType = "Int NOT NULL")]
        public int Collection_Days
        {
            get
            {
                return this._Collection_Days;
            }
            set
            {
                if ((this._Collection_Days != value))
                {
                    this._Collection_Days = value;
                }
            }
        }

        [Column(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Float")]
        public System.Nullable<double> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [Column(Storage = "_PercentageIn", DbType = "Real")]
        public System.Nullable<float> PercentageIn
        {
            get
            {
                return this._PercentageIn;
            }
            set
            {
                if ((this._PercentageIn != value))
                {
                    this._PercentageIn = value;
                }
            }
        }

        [Column(Storage = "_PercentageOut", DbType = "Float")]
        public System.Nullable<double> PercentageOut
        {
            get
            {
                return this._PercentageOut;
            }
            set
            {
                if ((this._PercentageOut != value))
                {
                    this._PercentageOut = value;
                }
            }
        }

        [Column(Storage = "_Collection_declared_Notes", DbType = "Real NOT NULL")]
        public float Collection_declared_Notes
        {
            get
            {
                return this._Collection_declared_Notes;
            }
            set
            {
                if ((this._Collection_declared_Notes != value))
                {
                    this._Collection_declared_Notes = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Real")]
        public System.Nullable<float> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin", DbType = "Real")]
        public System.Nullable<float> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_Collection_Ticket_Balance", DbType = "Real NOT NULL")]
        public float Collection_Ticket_Balance
        {
            get
            {
                return this._Collection_Ticket_Balance;
            }
            set
            {
                if ((this._Collection_Ticket_Balance != value))
                {
                    this._Collection_Ticket_Balance = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay", DbType = "Float")]
        public System.Nullable<double> DecHandpay
        {
            get
            {
                return this._DecHandpay;
            }
            set
            {
                if ((this._DecHandpay != value))
                {
                    this._DecHandpay = value;
                }
            }
        }

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_Declared_Notes", DbType = "Real NOT NULL")]
        public float Declared_Notes
        {
            get
            {
                return this._Declared_Notes;
            }
            set
            {
                if ((this._Declared_Notes != value))
                {
                    this._Declared_Notes = value;
                }
            }
        }

        [Column(Storage = "_Note_Var", DbType = "Real NOT NULL")]
        public float Note_Var
        {
            get
            {
                return this._Note_Var;
            }
            set
            {
                if ((this._Note_Var != value))
                {
                    this._Note_Var = value;
                }
            }
        }

        [Column(Storage = "_Coin_Var", DbType = "Real")]
        public System.Nullable<float> Coin_Var
        {
            get
            {
                return this._Coin_Var;
            }
            set
            {
                if ((this._Coin_Var != value))
                {
                    this._Coin_Var = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin1", DbType = "Real")]
        public System.Nullable<float> Net_Coin1
        {
            get
            {
                return this._Net_Coin1;
            }
            set
            {
                if ((this._Net_Coin1 != value))
                {
                    this._Net_Coin1 = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay1", DbType = "Float")]
        public System.Nullable<double> DecHandpay1
        {
            get
            {
                return this._DecHandpay1;
            }
            set
            {
                if ((this._DecHandpay1 != value))
                {
                    this._DecHandpay1 = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Real NOT NULL")]
        public float RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_Ticket_Var", DbType = "Float")]
        public System.Nullable<double> Ticket_Var
        {
            get
            {
                return this._Ticket_Var;
            }
            set
            {
                if ((this._Ticket_Var != value))
                {
                    this._Ticket_Var = value;
                }
            }
        }

        [Column(Storage = "_Handpay_Var", DbType = "Float")]
        public System.Nullable<double> Handpay_Var
        {
            get
            {
                return this._Handpay_Var;
            }
            set
            {
                if ((this._Handpay_Var != value))
                {
                    this._Handpay_Var = value;
                }
            }
        }

        [Column(Storage = "_Take_Var", DbType = "Float")]
        public System.Nullable<double> Take_Var
        {
            get
            {
                return this._Take_Var;
            }
            set
            {
                if ((this._Take_Var != value))
                {
                    this._Take_Var = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance1", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance1
        {
            get
            {
                return this._DecTicketBalance1;
            }
            set
            {
                if ((this._DecTicketBalance1 != value))
                {
                    this._DecTicketBalance1 = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Real NOT NULL")]
        public float RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_RDC_Ticket_Balance", DbType = "Real NOT NULL")]
        public float RDC_Ticket_Balance
        {
            get
            {
                return this._RDC_Ticket_Balance;
            }
            set
            {
                if ((this._RDC_Ticket_Balance != value))
                {
                    this._RDC_Ticket_Balance = value;
                }
            }
        }

        [Column(Storage = "_WinLossVar", DbType = "Float")]
        public System.Nullable<double> WinLossVar
        {
            get
            {
                return this._WinLossVar;
            }
            set
            {
                if ((this._WinLossVar != value))
                {
                    this._WinLossVar = value;
                }
            }
        }

        [Column(Storage = "_Collection_Handpay_Var", DbType = "Float")]
        public float Collection_Handpay_Var
        {
            get
            {
                return this._Collection_Handpay_Var;
            }
            set
            {
                if ((this._Collection_Handpay_Var != value))
                {
                    this._Collection_Handpay_Var = value;
                }
            }
        }

        [Column(Storage = "_RDC_Take", DbType = "Float")]
        public System.Nullable<double> RDC_Take
        {
            get
            {
                return this._RDC_Take;
            }
            set
            {
                if ((this._RDC_Take != value))
                {
                    this._RDC_Take = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Real NOT NULL")]
        public float Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_Collection_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Collection_Supplier_Share
        {
            get
            {
                return this._Collection_Supplier_Share;
            }
            set
            {
                if ((this._Collection_Supplier_Share != value))
                {
                    this._Collection_Supplier_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Company_Share", DbType = "Real")]
        public System.Nullable<float> Collection_Company_Share
        {
            get
            {
                return this._Collection_Company_Share;
            }
            set
            {
                if ((this._Collection_Company_Share != value))
                {
                    this._Collection_Company_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Location_Share", DbType = "Real")]
        public System.Nullable<float> Collection_Location_Share
        {
            get
            {
                return this._Collection_Location_Share;
            }
            set
            {
                if ((this._Collection_Location_Share != value))
                {
                    this._Collection_Location_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_Other_Share", DbType = "Real")]
        public System.Nullable<float> Collection_Other_Share
        {
            get
            {
                return this._Collection_Other_Share;
            }
            set
            {
                if ((this._Collection_Other_Share != value))
                {
                    this._Collection_Other_Share = value;
                }
            }
        }

        [Column(Storage = "_Collection_AMLD", DbType = "Real")]
        public System.Nullable<float> Collection_AMLD
        {
            get
            {
                return this._Collection_AMLD;
            }
            set
            {
                if ((this._Collection_AMLD != value))
                {
                    this._Collection_AMLD = value;
                }
            }
        }

        [Column(Storage = "_Remarks", DbType = "NText", UpdateCheck = UpdateCheck.Never)]
        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                if ((this._Remarks != value))
                {
                    this._Remarks = value;
                }
            }
        }

        [Column(Storage = "_Period_Id", DbType = "Int")]
        public System.Nullable<int> Period_Id
        {
            get
            {
                return this._Period_Id;
            }
            set
            {
                if ((this._Period_Id != value))
                {
                    this._Period_Id = value;
                }
            }
        }

        [Column(Storage = "_Period_End_ID1", DbType = "Int")]
        public System.Nullable<int> Period_End_ID1
        {
            get
            {
                return this._Period_End_ID1;
            }
            set
            {
                if ((this._Period_End_ID1 != value))
                {
                    this._Period_End_ID1 = value;
                }
            }
        }

        [Column(Storage = "_Lag", DbType = "Int NOT NULL")]
        public int Lag
        {
            get
            {
                return this._Lag;
            }
            set
            {
                if ((this._Lag != value))
                {
                    this._Lag = value;
                }
            }
        }

        [Column(Storage = "_TargetVariance", DbType = "Real")]
        public System.Nullable<float> TargetVariance
        {
            get
            {
                return this._TargetVariance;
            }
            set
            {
                if ((this._TargetVariance != value))
                {
                    this._TargetVariance = value;
                }
            }
        }

        [Column(Storage = "_RDCIn", DbType = "Float")]
        public System.Nullable<double> RDCIn
        {
            get
            {
                return this._RDCIn;
            }
            set
            {
                if ((this._RDCIn != value))
                {
                    this._RDCIn = value;
                }
            }
        }

        [Column(Storage = "_RDCOut", DbType = "Float")]
        public System.Nullable<double> RDCOut
        {
            get
            {
                return this._RDCOut;
            }
            set
            {
                if ((this._RDCOut != value))
                {
                    this._RDCOut = value;
                }
            }
        }

        [Column(Storage = "_RDCCash", DbType = "Real NOT NULL")]
        public float RDCCash
        {
            get
            {
                return this._RDCCash;
            }
            set
            {
                if ((this._RDCCash != value))
                {
                    this._RDCCash = value;
                }
            }
        }

        [Column(Storage = "_RDCVar", DbType = "Real NOT NULL")]
        public float RDCVar
        {
            get
            {
                return this._RDCVar;
            }
            set
            {
                if ((this._RDCVar != value))
                {
                    this._RDCVar = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }

        [Column(Storage = "_Secondary_Brewery_Name", DbType = "VarChar(50)")]
        public string Secondary_Brewery_Name
        {
            get
            {
                return this._Secondary_Brewery_Name;
            }
            set
            {
                if ((this._Secondary_Brewery_Name != value))
                {
                    this._Secondary_Brewery_Name = value;
                }
            }
        }

        [Column(Storage = "_Secondary_Sub_Company_Period_End_ID", DbType = "Int")]
        public System.Nullable<int> Secondary_Sub_Company_Period_End_ID
        {
            get
            {
                return this._Secondary_Sub_Company_Period_End_ID;
            }
            set
            {
                if ((this._Secondary_Sub_Company_Period_End_ID != value))
                {
                    this._Secondary_Sub_Company_Period_End_ID = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Real NOT NULL")]
        public float RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Week_End_ID", DbType = "Int")]
        public System.Nullable<int> Week_End_ID
        {
            get
            {
                return this._Week_End_ID;
            }
            set
            {
                if ((this._Week_End_ID != value))
                {
                    this._Week_End_ID = value;
                }
            }
        }

        [Column(Storage = "_VTP", DbType = "Float")]
        public System.Nullable<double> VTP
        {
            get
            {
                return this._VTP;
            }
            set
            {
                if ((this._VTP != value))
                {
                    this._VTP = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Name
        {
            get
            {
                return this._Sub_Company_Name;
            }
            set
            {
                if ((this._Sub_Company_Name != value))
                {
                    this._Sub_Company_Name = value;
                }
            }
        }

        [Column(Storage = "_Company_Name", DbType = "VarChar(50)")]
        public string Company_Name
        {
            get
            {
                return this._Company_Name;
            }
            set
            {
                if ((this._Company_Name != value))
                {
                    this._Company_Name = value;
                }
            }
        }

        [Column(Storage = "_MeterCashIn", DbType = "Float")]
        public System.Nullable<double> MeterCashIn
        {
            get
            {
                return this._MeterCashIn;
            }
            set
            {
                if ((this._MeterCashIn != value))
                {
                    this._MeterCashIn = value;
                }
            }
        }

        [Column(Storage = "_MeterCashOut", DbType = "Float")]
        public System.Nullable<double> MeterCashOut
        {
            get
            {
                return this._MeterCashOut;
            }
            set
            {
                if ((this._MeterCashOut != value))
                {
                    this._MeterCashOut = value;
                }
            }
        }

        [Column(Storage = "_Handle", DbType = "Float")]
        public System.Nullable<double> Handle
        {
            get
            {
                return this._Handle;
            }
            set
            {
                if ((this._Handle != value))
                {
                    this._Handle = value;
                }
            }
        }

        [Column(Storage = "_PIndex", DbType = "Real")]
        public System.Nullable<float> PIndex
        {
            get
            {
                return this._PIndex;
            }
            set
            {
                if ((this._PIndex != value))
                {
                    this._PIndex = value;
                }
            }
        }

        [Column(Storage = "_PacePrev9Days", DbType = "Int NOT NULL")]
        public int PacePrev9Days
        {
            get
            {
                return this._PacePrev9Days;
            }
            set
            {
                if ((this._PacePrev9Days != value))
                {
                    this._PacePrev9Days = value;
                }
            }
        }

        [Column(Storage = "_PacePrev9Cash", DbType = "Int NOT NULL")]
        public int PacePrev9Cash
        {
            get
            {
                return this._PacePrev9Cash;
            }
            set
            {
                if ((this._PacePrev9Cash != value))
                {
                    this._PacePrev9Cash = value;
                }
            }
        }

        [Column(Storage = "_PaceDays", DbType = "Int NOT NULL")]
        public int PaceDays
        {
            get
            {
                return this._PaceDays;
            }
            set
            {
                if ((this._PaceDays != value))
                {
                    this._PaceDays = value;
                }
            }
        }

        [Column(Storage = "_PaceCash", DbType = "Int NOT NULL")]
        public int PaceCash
        {
            get
            {
                return this._PaceCash;
            }
            set
            {
                if ((this._PaceCash != value))
                {
                    this._PaceCash = value;
                }
            }
        }

        [Column(Storage = "_Collection_GPT", DbType = "Real NOT NULL")]
        public float Collection_GPT
        {
            get
            {
                return this._Collection_GPT;
            }
            set
            {
                if ((this._Collection_GPT != value))
                {
                    this._Collection_GPT = value;
                }
            }
        }

        [Column(Storage = "_Collection_FOBT_Stakes", DbType = "Real NOT NULL")]
        public float Collection_FOBT_Stakes
        {
            get
            {
                return this._Collection_FOBT_Stakes;
            }
            set
            {
                if ((this._Collection_FOBT_Stakes != value))
                {
                    this._Collection_FOBT_Stakes = value;
                }
            }
        }

        [Column(Storage = "_Collection_FOBT_Payout", DbType = "Real NOT NULL")]
        public float Collection_FOBT_Payout
        {
            get
            {
                return this._Collection_FOBT_Payout;
            }
            set
            {
                if ((this._Collection_FOBT_Payout != value))
                {
                    this._Collection_FOBT_Payout = value;
                }
            }
        }

        [Column(Storage = "_Collection_Transactions", DbType = "Int NOT NULL")]
        public int Collection_Transactions
        {
            get
            {
                return this._Collection_Transactions;
            }
            set
            {
                if ((this._Collection_Transactions != value))
                {
                    this._Collection_Transactions = value;
                }
            }
        }

        [Column(Storage = "_Machine_Start_Date", DbType = "VarChar(50)")]
        public string Machine_Start_Date
        {
            get
            {
                return this._Machine_Start_Date;
            }
            set
            {
                if ((this._Machine_Start_Date != value))
                {
                    this._Machine_Start_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteViewerCollsBatchDataResult
    {

        private string _BatchDate;

        private string _Batch_Date_performed;

        private string _Batch_Time_performed;

        private string _Batch_Name;

        private System.Nullable<int> _Batch_ID;

        private string _BatchRef;

        private System.Nullable<int> _BatchCount;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<double> _Notes;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _Coins;

        private System.Nullable<double> _TicktesIn;

        private System.Nullable<double> _TicktesOut;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _Handpay;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _NotesVar;

        private System.Nullable<double> _CoinVar;

        private System.Nullable<double> _TicketVar;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<double> _Take_Var;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<double> _MeterCash;

        private System.Nullable<double> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<float> _BatchAdj;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _RDC_Tickets_In;

        private System.Nullable<double> _RDC_Tickets_Out;

        private System.Nullable<double> _MeterHandpay;

        private System.Nullable<double> _Ticket;

        private System.Nullable<double> _RDC_Take;

        private System.Nullable<double> _RDC_Coins;

        private System.Nullable<double> _Hopperchange;

        private System.Nullable<double> _RDC_Coins_Out;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _Cash_Take;

        private System.Nullable<double> _WinLossVar;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _DecEftOut;

        private System.Nullable<double> _PromoCashableAmount;

        private System.Nullable<double> _PromoNonCashableAmount;

        public rsp_GetSiteViewerCollsBatchDataResult()
        {
        }

        [Column(Storage = "_BatchDate", DbType = "VarChar(30)")]
        public string BatchDate
        {
            get
            {
                return this._BatchDate;
            }
            set
            {
                if ((this._BatchDate != value))
                {
                    this._BatchDate = value;
                }
            }
        }

        [Column(Storage = "_Batch_Date_performed", DbType = "VarChar(30)")]
        public string Batch_Date_performed
        {
            get
            {
                return this._Batch_Date_performed;
            }
            set
            {
                if ((this._Batch_Date_performed != value))
                {
                    this._Batch_Date_performed = value;
                }
            }
        }

        [Column(Storage = "_Batch_Time_performed", DbType = "VarChar(50)")]
        public string Batch_Time_performed
        {
            get
            {
                return this._Batch_Time_performed;
            }
            set
            {
                if ((this._Batch_Time_performed != value))
                {
                    this._Batch_Time_performed = value;
                }
            }
        }

        [Column(Storage = "_Batch_Name", DbType = "VarChar(50)")]
        public string Batch_Name
        {
            get
            {
                return this._Batch_Name;
            }
            set
            {
                if ((this._Batch_Name != value))
                {
                    this._Batch_Name = value;
                }
            }
        }

        [Column(Storage = "_Batch_ID", DbType = "Int")]
        public System.Nullable<int> Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }

        [Column(Storage = "_BatchRef", DbType = "VarChar(50)")]
        public string BatchRef
        {
            get
            {
                return this._BatchRef;
            }
            set
            {
                if ((this._BatchRef != value))
                {
                    this._BatchRef = value;
                }
            }
        }

        [Column(Storage = "_BatchCount", DbType = "Int")]
        public System.Nullable<int> BatchCount
        {
            get
            {
                return this._BatchCount;
            }
            set
            {
                if ((this._BatchCount != value))
                {
                    this._BatchCount = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Float")]
        public System.Nullable<double> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [Column(Storage = "_Notes", DbType = "Float")]
        public System.Nullable<double> Notes
        {
            get
            {
                return this._Notes;
            }
            set
            {
                if ((this._Notes != value))
                {
                    this._Notes = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_Coins", DbType = "Float")]
        public System.Nullable<double> Coins
        {
            get
            {
                return this._Coins;
            }
            set
            {
                if ((this._Coins != value))
                {
                    this._Coins = value;
                }
            }
        }

        [Column(Storage = "_TicktesIn", DbType = "Float")]
        public System.Nullable<double> TicktesIn
        {
            get
            {
                return this._TicktesIn;
            }
            set
            {
                if ((this._TicktesIn != value))
                {
                    this._TicktesIn = value;
                }
            }
        }

        [Column(Storage = "_TicktesOut", DbType = "Float")]
        public System.Nullable<double> TicktesOut
        {
            get
            {
                return this._TicktesOut;
            }
            set
            {
                if ((this._TicktesOut != value))
                {
                    this._TicktesOut = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin", DbType = "Float")]
        public System.Nullable<double> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Float")]
        public System.Nullable<double> Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Float")]
        public System.Nullable<double> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Float")]
        public System.Nullable<double> Handpay
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

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_NotesVar", DbType = "Float")]
        public System.Nullable<double> NotesVar
        {
            get
            {
                return this._NotesVar;
            }
            set
            {
                if ((this._NotesVar != value))
                {
                    this._NotesVar = value;
                }
            }
        }

        [Column(Storage = "_CoinVar", DbType = "Float")]
        public System.Nullable<double> CoinVar
        {
            get
            {
                return this._CoinVar;
            }
            set
            {
                if ((this._CoinVar != value))
                {
                    this._CoinVar = value;
                }
            }
        }

        [Column(Storage = "_TicketVar", DbType = "Float")]
        public System.Nullable<double> TicketVar
        {
            get
            {
                return this._TicketVar;
            }
            set
            {
                if ((this._TicketVar != value))
                {
                    this._TicketVar = value;
                }
            }
        }

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_Take_Var", DbType = "Float")]
        public System.Nullable<double> Take_Var
        {
            get
            {
                return this._Take_Var;
            }
            set
            {
                if ((this._Take_Var != value))
                {
                    this._Take_Var = value;
                }
            }
        }

        [Column(Storage = "_RDCRefill", DbType = "Float")]
        public System.Nullable<double> RDCRefill
        {
            get
            {
                return this._RDCRefill;
            }
            set
            {
                if ((this._RDCRefill != value))
                {
                    this._RDCRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterCash", DbType = "Float")]
        public System.Nullable<double> MeterCash
        {
            get
            {
                return this._MeterCash;
            }
            set
            {
                if ((this._MeterCash != value))
                {
                    this._MeterCash = value;
                }
            }
        }

        [Column(Storage = "_MeterRefill", DbType = "Float")]
        public System.Nullable<double> MeterRefill
        {
            get
            {
                return this._MeterRefill;
            }
            set
            {
                if ((this._MeterRefill != value))
                {
                    this._MeterRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterVar", DbType = "Float")]
        public System.Nullable<double> MeterVar
        {
            get
            {
                return this._MeterVar;
            }
            set
            {
                if ((this._MeterVar != value))
                {
                    this._MeterVar = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Float")]
        public System.Nullable<double> RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_BatchAdj", DbType = "Real")]
        public System.Nullable<float> BatchAdj
        {
            get
            {
                return this._BatchAdj;
            }
            set
            {
                if ((this._BatchAdj != value))
                {
                    this._BatchAdj = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay", DbType = "Float")]
        public System.Nullable<double> DecHandpay
        {
            get
            {
                return this._DecHandpay;
            }
            set
            {
                if ((this._DecHandpay != value))
                {
                    this._DecHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_In", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_In
        {
            get
            {
                return this._RDC_Tickets_In;
            }
            set
            {
                if ((this._RDC_Tickets_In != value))
                {
                    this._RDC_Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_Out
        {
            get
            {
                return this._RDC_Tickets_Out;
            }
            set
            {
                if ((this._RDC_Tickets_Out != value))
                {
                    this._RDC_Tickets_Out = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Float")]
        public System.Nullable<double> MeterHandpay
        {
            get
            {
                return this._MeterHandpay;
            }
            set
            {
                if ((this._MeterHandpay != value))
                {
                    this._MeterHandpay = value;
                }
            }
        }

        [Column(Storage = "_Ticket", DbType = "Float")]
        public System.Nullable<double> Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                if ((this._Ticket != value))
                {
                    this._Ticket = value;
                }
            }
        }

        [Column(Storage = "_RDC_Take", DbType = "Float")]
        public System.Nullable<double> RDC_Take
        {
            get
            {
                return this._RDC_Take;
            }
            set
            {
                if ((this._RDC_Take != value))
                {
                    this._RDC_Take = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Float")]
        public System.Nullable<double> RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_Hopperchange", DbType = "Float")]
        public System.Nullable<double> Hopperchange
        {
            get
            {
                return this._Hopperchange;
            }
            set
            {
                if ((this._Hopperchange != value))
                {
                    this._Hopperchange = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_Expired", DbType = "Float")]
        public System.Nullable<double> Expired
        {
            get
            {
                return this._Expired;
            }
            set
            {
                if ((this._Expired != value))
                {
                    this._Expired = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Declared", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Declared
        {
            get
            {
                return this._Progressive_Value_Declared;
            }
            set
            {
                if ((this._Progressive_Value_Declared != value))
                {
                    this._Progressive_Value_Declared = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }

        [Column(Storage = "_WinLossVar", DbType = "Float")]
        public System.Nullable<double> WinLossVar
        {
            get
            {
                return this._WinLossVar;
            }
            set
            {
                if ((this._WinLossVar != value))
                {
                    this._WinLossVar = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }
        [Column(Storage = "_PromoCashableAmount", DbType = "Float")]
        public System.Nullable<double> PromoCashableAmount
        {
            get
            {
                return this._PromoCashableAmount;
            }
            set
            {
                if ((this._PromoCashableAmount != value))
                {
                    this._PromoCashableAmount = value;
                }
            }
        }
        [Column(Storage = "_PromoNonCashableAmount", DbType = "Float")]
        public System.Nullable<double> PromoNonCashableAmount
        {
            get
            {
                return this._PromoNonCashableAmount;
            }
            set
            {
                if ((this._PromoNonCashableAmount != value))
                {
                    this._PromoNonCashableAmount = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteViewerCollsBatchResult
    {

        private string _BatchDate;

        private string _Batch_Date_performed;

        private string _Batch_Time_performed;

        private string _Batch_Name;

        private System.Nullable<int> _Batch_ID;

        private string _BatchRef;

        private System.Nullable<int> _BatchCount;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<double> _Notes;

        private System.Nullable<double> _DecTicketBalance;

        private System.Nullable<double> _Coins;

        private System.Nullable<double> _TicktesIn;

        private System.Nullable<double> _TicktesOut;

        private System.Nullable<double> _Net_Coin;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _Handpay;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _NotesVar;

        private System.Nullable<double> _CoinVar;

        private System.Nullable<double> _TicketVar;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<double> _TakeVar;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<double> _RDCVar;

        private System.Nullable<double> _MeterCash;

        private System.Nullable<double> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<float> _BatchAdj;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _RDC_Tickets_In;

        private System.Nullable<double> _RDC_Tickets_Out;

        private System.Nullable<double> _MeterHandpay;

        private System.Nullable<double> _Ticket;

        private System.Nullable<double> _RDC_Take;

        private System.Nullable<double> _Cash_Take;

        private System.Nullable<double> _WinLossVar;

        private System.Nullable<double> _RDC_Coins;

        private System.Nullable<double> _HopperChange;

        private System.Nullable<double> _RDC_Coins_Out;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _DecEftOut;

        public rsp_GetSiteViewerCollsBatchResult()
        {
        }

        [Column(Storage = "_BatchDate", DbType = "VarChar(30)")]
        public string BatchDate
        {
            get
            {
                return this._BatchDate;
            }
            set
            {
                if ((this._BatchDate != value))
                {
                    this._BatchDate = value;
                }
            }
        }

        [Column(Storage = "_Batch_Date_performed", DbType = "VarChar(30)")]
        public string Batch_Date_performed
        {
            get
            {
                return this._Batch_Date_performed;
            }
            set
            {
                if ((this._Batch_Date_performed != value))
                {
                    this._Batch_Date_performed = value;
                }
            }
        }

        [Column(Storage = "_Batch_Time_performed", DbType = "VarChar(50)")]
        public string Batch_Time_performed
        {
            get
            {
                return this._Batch_Time_performed;
            }
            set
            {
                if ((this._Batch_Time_performed != value))
                {
                    this._Batch_Time_performed = value;
                }
            }
        }

        [Column(Storage = "_Batch_Name", DbType = "VarChar(50)")]
        public string Batch_Name
        {
            get
            {
                return this._Batch_Name;
            }
            set
            {
                if ((this._Batch_Name != value))
                {
                    this._Batch_Name = value;
                }
            }
        }

        [Column(Storage = "_Batch_ID", DbType = "Int")]
        public System.Nullable<int> Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }

        [Column(Storage = "_BatchRef", DbType = "VarChar(50)")]
        public string BatchRef
        {
            get
            {
                return this._BatchRef;
            }
            set
            {
                if ((this._BatchRef != value))
                {
                    this._BatchRef = value;
                }
            }
        }

        [Column(Storage = "_BatchCount", DbType = "Int")]
        public System.Nullable<int> BatchCount
        {
            get
            {
                return this._BatchCount;
            }
            set
            {
                if ((this._BatchCount != value))
                {
                    this._BatchCount = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Float")]
        public System.Nullable<double> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [Column(Storage = "_Notes", DbType = "Float")]
        public System.Nullable<double> Notes
        {
            get
            {
                return this._Notes;
            }
            set
            {
                if ((this._Notes != value))
                {
                    this._Notes = value;
                }
            }
        }

        [Column(Storage = "_DecTicketBalance", DbType = "Float")]
        public System.Nullable<double> DecTicketBalance
        {
            get
            {
                return this._DecTicketBalance;
            }
            set
            {
                if ((this._DecTicketBalance != value))
                {
                    this._DecTicketBalance = value;
                }
            }
        }

        [Column(Storage = "_Coins", DbType = "Float")]
        public System.Nullable<double> Coins
        {
            get
            {
                return this._Coins;
            }
            set
            {
                if ((this._Coins != value))
                {
                    this._Coins = value;
                }
            }
        }

        [Column(Storage = "_TicktesIn", DbType = "Float")]
        public System.Nullable<double> TicktesIn
        {
            get
            {
                return this._TicktesIn;
            }
            set
            {
                if ((this._TicktesIn != value))
                {
                    this._TicktesIn = value;
                }
            }
        }

        [Column(Storage = "_TicktesOut", DbType = "Float")]
        public System.Nullable<double> TicktesOut
        {
            get
            {
                return this._TicktesOut;
            }
            set
            {
                if ((this._TicktesOut != value))
                {
                    this._TicktesOut = value;
                }
            }
        }

        [Column(Storage = "_Net_Coin", DbType = "Float")]
        public System.Nullable<double> Net_Coin
        {
            get
            {
                return this._Net_Coin;
            }
            set
            {
                if ((this._Net_Coin != value))
                {
                    this._Net_Coin = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Float")]
        public System.Nullable<double> Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Float")]
        public System.Nullable<double> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_Handpay", DbType = "Float")]
        public System.Nullable<double> Handpay
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

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_NotesVar", DbType = "Float")]
        public System.Nullable<double> NotesVar
        {
            get
            {
                return this._NotesVar;
            }
            set
            {
                if ((this._NotesVar != value))
                {
                    this._NotesVar = value;
                }
            }
        }

        [Column(Storage = "_CoinVar", DbType = "Float")]
        public System.Nullable<double> CoinVar
        {
            get
            {
                return this._CoinVar;
            }
            set
            {
                if ((this._CoinVar != value))
                {
                    this._CoinVar = value;
                }
            }
        }

        [Column(Storage = "_TicketVar", DbType = "Float")]
        public System.Nullable<double> TicketVar
        {
            get
            {
                return this._TicketVar;
            }
            set
            {
                if ((this._TicketVar != value))
                {
                    this._TicketVar = value;
                }
            }
        }

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_TakeVar", DbType = "Float")]
        public System.Nullable<double> TakeVar
        {
            get
            {
                return this._TakeVar;
            }
            set
            {
                if ((this._TakeVar != value))
                {
                    this._TakeVar = value;
                }
            }
        }

        [Column(Storage = "_RDCRefill", DbType = "Float")]
        public System.Nullable<double> RDCRefill
        {
            get
            {
                return this._RDCRefill;
            }
            set
            {
                if ((this._RDCRefill != value))
                {
                    this._RDCRefill = value;
                }
            }
        }

        [Column(Storage = "_RDCVar", DbType = "Float")]
        public System.Nullable<double> RDCVar
        {
            get
            {
                return this._RDCVar;
            }
            set
            {
                if ((this._RDCVar != value))
                {
                    this._RDCVar = value;
                }
            }
        }

        [Column(Storage = "_MeterCash", DbType = "Float")]
        public System.Nullable<double> MeterCash
        {
            get
            {
                return this._MeterCash;
            }
            set
            {
                if ((this._MeterCash != value))
                {
                    this._MeterCash = value;
                }
            }
        }

        [Column(Storage = "_MeterRefill", DbType = "Float")]
        public System.Nullable<double> MeterRefill
        {
            get
            {
                return this._MeterRefill;
            }
            set
            {
                if ((this._MeterRefill != value))
                {
                    this._MeterRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterVar", DbType = "Float")]
        public System.Nullable<double> MeterVar
        {
            get
            {
                return this._MeterVar;
            }
            set
            {
                if ((this._MeterVar != value))
                {
                    this._MeterVar = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Float")]
        public System.Nullable<double> RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_BatchAdj", DbType = "Real")]
        public System.Nullable<float> BatchAdj
        {
            get
            {
                return this._BatchAdj;
            }
            set
            {
                if ((this._BatchAdj != value))
                {
                    this._BatchAdj = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay", DbType = "Float")]
        public System.Nullable<double> DecHandpay
        {
            get
            {
                return this._DecHandpay;
            }
            set
            {
                if ((this._DecHandpay != value))
                {
                    this._DecHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_In", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_In
        {
            get
            {
                return this._RDC_Tickets_In;
            }
            set
            {
                if ((this._RDC_Tickets_In != value))
                {
                    this._RDC_Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_Out
        {
            get
            {
                return this._RDC_Tickets_Out;
            }
            set
            {
                if ((this._RDC_Tickets_Out != value))
                {
                    this._RDC_Tickets_Out = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Float")]
        public System.Nullable<double> MeterHandpay
        {
            get
            {
                return this._MeterHandpay;
            }
            set
            {
                if ((this._MeterHandpay != value))
                {
                    this._MeterHandpay = value;
                }
            }
        }

        [Column(Storage = "_Ticket", DbType = "Float")]
        public System.Nullable<double> Ticket
        {
            get
            {
                return this._Ticket;
            }
            set
            {
                if ((this._Ticket != value))
                {
                    this._Ticket = value;
                }
            }
        }

        [Column(Storage = "_RDC_Take", DbType = "Float")]
        public System.Nullable<double> RDC_Take
        {
            get
            {
                return this._RDC_Take;
            }
            set
            {
                if ((this._RDC_Take != value))
                {
                    this._RDC_Take = value;
                }
            }
        }

        [Column(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }

        [Column(Storage = "_WinLossVar", DbType = "Float")]
        public System.Nullable<double> WinLossVar
        {
            get
            {
                return this._WinLossVar;
            }
            set
            {
                if ((this._WinLossVar != value))
                {
                    this._WinLossVar = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Float")]
        public System.Nullable<double> RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_HopperChange", DbType = "Float")]
        public System.Nullable<double> HopperChange
        {
            get
            {
                return this._HopperChange;
            }
            set
            {
                if ((this._HopperChange != value))
                {
                    this._HopperChange = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_Expired", DbType = "Float")]
        public System.Nullable<double> Expired
        {
            get
            {
                return this._Expired;
            }
            set
            {
                if ((this._Expired != value))
                {
                    this._Expired = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Declared", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Declared
        {
            get
            {
                return this._Progressive_Value_Declared;
            }
            set
            {
                if ((this._Progressive_Value_Declared != value))
                {
                    this._Progressive_Value_Declared = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }
    }

    public partial class rsp_GetSiteViewerCollsBatchWeekResult
    {

        private System.Nullable<int> _Week_ID;

        private System.Nullable<int> _WeekNumber;

        private string _StartDate;

        private string _EndDate;

        private System.Nullable<int> _WeekCount;

        private System.Nullable<double> _CashCollected;

        private System.Nullable<double> _CashCollected1;

        private System.Nullable<double> _Notes;

        private System.Nullable<double> _Coins;

        private System.Nullable<double> _TicktesIn;

        private System.Nullable<double> _TicktesOut;

        private System.Nullable<double> _Refills;

        private System.Nullable<double> _Refunds;

        private System.Nullable<double> _AttendantPay;

        private System.Nullable<double> _Shortpay;

        private System.Nullable<double> _NotesVar;

        private System.Nullable<double> _CoinVar;

        private System.Nullable<double> _VoucherVar;

        private System.Nullable<double> _HandpayVar;

        private System.Nullable<double> _TakeVar;

        private System.Nullable<double> _RDCRefill;

        private System.Nullable<double> _MeterCash;

        private System.Nullable<double> _MeterRefill;

        private System.Nullable<double> _MeterVar;

        private System.Nullable<double> _RDC_Notes;

        private System.Nullable<float> _BatchAdj;

        private System.Nullable<double> _DecHandpay;

        private System.Nullable<double> _RDCHandpay;

        private System.Nullable<double> _RDC_Tickets_In;

        private System.Nullable<double> _RDC_Tickets_Out;

        private System.Nullable<double> _MeterHandpay;

        private System.Nullable<double> _Voucher;

        private System.Nullable<double> _RDC_Take;

        private System.Nullable<double> _Cash_Take;

        private System.Nullable<double> _RDC_Coins;

        private System.Nullable<double> _Hopperchange;

        private System.Nullable<double> _RDC_Coins_Out;

        private System.Nullable<double> _Void;

        private System.Nullable<double> _Expired;

        private System.Nullable<double> _WinLossVar;

        private System.Nullable<double> _DecEftIn;

        private System.Nullable<double> _DecEftOut;

        private System.Nullable<double> _Progressive_Value_Declared;

        private System.Nullable<double> _Progressive_Value_Variance;

        private System.Nullable<double> _EftIn;

        private System.Nullable<double> _EftOut;

        private System.Nullable<double> _PromoCashableIn;

        private System.Nullable<double> _PromoNonCashableIn;

        public rsp_GetSiteViewerCollsBatchWeekResult()
        {
        }

        [Column(Storage = "_Week_ID", DbType = "Int")]
        public System.Nullable<int> Week_ID
        {
            get
            {
                return this._Week_ID;
            }
            set
            {
                if ((this._Week_ID != value))
                {
                    this._Week_ID = value;
                }
            }
        }

        [Column(Storage = "_WeekNumber", DbType = "Int")]
        public System.Nullable<int> WeekNumber
        {
            get
            {
                return this._WeekNumber;
            }
            set
            {
                if ((this._WeekNumber != value))
                {
                    this._WeekNumber = value;
                }
            }
        }

        [Column(Storage = "_StartDate", DbType = "VarChar(30)")]
        public string StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                if ((this._StartDate != value))
                {
                    this._StartDate = value;
                }
            }
        }

        [Column(Storage = "_EndDate", DbType = "VarChar(30)")]
        public string EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if ((this._EndDate != value))
                {
                    this._EndDate = value;
                }
            }
        }

        [Column(Storage = "_WeekCount", DbType = "Int")]
        public System.Nullable<int> WeekCount
        {
            get
            {
                return this._WeekCount;
            }
            set
            {
                if ((this._WeekCount != value))
                {
                    this._WeekCount = value;
                }
            }
        }

        [Column(Storage = "_CashCollected", DbType = "Float")]
        public System.Nullable<double> CashCollected
        {
            get
            {
                return this._CashCollected;
            }
            set
            {
                if ((this._CashCollected != value))
                {
                    this._CashCollected = value;
                }
            }
        }

        [Column(Storage = "_CashCollected1", DbType = "Float")]
        public System.Nullable<double> CashCollected1
        {
            get
            {
                return this._CashCollected1;
            }
            set
            {
                if ((this._CashCollected1 != value))
                {
                    this._CashCollected1 = value;
                }
            }
        }

        [Column(Storage = "_Notes", DbType = "Float")]
        public System.Nullable<double> Notes
        {
            get
            {
                return this._Notes;
            }
            set
            {
                if ((this._Notes != value))
                {
                    this._Notes = value;
                }
            }
        }

        [Column(Storage = "_Coins", DbType = "Float")]
        public System.Nullable<double> Coins
        {
            get
            {
                return this._Coins;
            }
            set
            {
                if ((this._Coins != value))
                {
                    this._Coins = value;
                }
            }
        }

        [Column(Storage = "_TicktesIn", DbType = "Float")]
        public System.Nullable<double> TicktesIn
        {
            get
            {
                return this._TicktesIn;
            }
            set
            {
                if ((this._TicktesIn != value))
                {
                    this._TicktesIn = value;
                }
            }
        }

        [Column(Storage = "_TicktesOut", DbType = "Float")]
        public System.Nullable<double> TicktesOut
        {
            get
            {
                return this._TicktesOut;
            }
            set
            {
                if ((this._TicktesOut != value))
                {
                    this._TicktesOut = value;
                }
            }
        }

        [Column(Storage = "_Refills", DbType = "Float")]
        public System.Nullable<double> Refills
        {
            get
            {
                return this._Refills;
            }
            set
            {
                if ((this._Refills != value))
                {
                    this._Refills = value;
                }
            }
        }

        [Column(Storage = "_Refunds", DbType = "Float")]
        public System.Nullable<double> Refunds
        {
            get
            {
                return this._Refunds;
            }
            set
            {
                if ((this._Refunds != value))
                {
                    this._Refunds = value;
                }
            }
        }

        [Column(Storage = "_AttendantPay", DbType = "Float")]
        public System.Nullable<double> AttendantPay
        {
            get
            {
                return this._AttendantPay;
            }
            set
            {
                if ((this._AttendantPay != value))
                {
                    this._AttendantPay = value;
                }
            }
        }

        [Column(Storage = "_Shortpay", DbType = "Float")]
        public System.Nullable<double> Shortpay
        {
            get
            {
                return this._Shortpay;
            }
            set
            {
                if ((this._Shortpay != value))
                {
                    this._Shortpay = value;
                }
            }
        }

        [Column(Storage = "_NotesVar", DbType = "Float")]
        public System.Nullable<double> NotesVar
        {
            get
            {
                return this._NotesVar;
            }
            set
            {
                if ((this._NotesVar != value))
                {
                    this._NotesVar = value;
                }
            }
        }

        [Column(Storage = "_CoinVar", DbType = "Float")]
        public System.Nullable<double> CoinVar
        {
            get
            {
                return this._CoinVar;
            }
            set
            {
                if ((this._CoinVar != value))
                {
                    this._CoinVar = value;
                }
            }
        }

        [Column(Storage = "_VoucherVar", DbType = "Float")]
        public System.Nullable<double> VoucherVar
        {
            get
            {
                return this._VoucherVar;
            }
            set
            {
                if ((this._VoucherVar != value))
                {
                    this._VoucherVar = value;
                }
            }
        }

        [Column(Storage = "_HandpayVar", DbType = "Float")]
        public System.Nullable<double> HandpayVar
        {
            get
            {
                return this._HandpayVar;
            }
            set
            {
                if ((this._HandpayVar != value))
                {
                    this._HandpayVar = value;
                }
            }
        }

        [Column(Storage = "_TakeVar", DbType = "Float")]
        public System.Nullable<double> TakeVar
        {
            get
            {
                return this._TakeVar;
            }
            set
            {
                if ((this._TakeVar != value))
                {
                    this._TakeVar = value;
                }
            }
        }

        [Column(Storage = "_RDCRefill", DbType = "Float")]
        public System.Nullable<double> RDCRefill
        {
            get
            {
                return this._RDCRefill;
            }
            set
            {
                if ((this._RDCRefill != value))
                {
                    this._RDCRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterCash", DbType = "Float")]
        public System.Nullable<double> MeterCash
        {
            get
            {
                return this._MeterCash;
            }
            set
            {
                if ((this._MeterCash != value))
                {
                    this._MeterCash = value;
                }
            }
        }

        [Column(Storage = "_MeterRefill", DbType = "Float")]
        public System.Nullable<double> MeterRefill
        {
            get
            {
                return this._MeterRefill;
            }
            set
            {
                if ((this._MeterRefill != value))
                {
                    this._MeterRefill = value;
                }
            }
        }

        [Column(Storage = "_MeterVar", DbType = "Float")]
        public System.Nullable<double> MeterVar
        {
            get
            {
                return this._MeterVar;
            }
            set
            {
                if ((this._MeterVar != value))
                {
                    this._MeterVar = value;
                }
            }
        }

        [Column(Storage = "_RDC_Notes", DbType = "Float")]
        public System.Nullable<double> RDC_Notes
        {
            get
            {
                return this._RDC_Notes;
            }
            set
            {
                if ((this._RDC_Notes != value))
                {
                    this._RDC_Notes = value;
                }
            }
        }

        [Column(Storage = "_BatchAdj", DbType = "Real")]
        public System.Nullable<float> BatchAdj
        {
            get
            {
                return this._BatchAdj;
            }
            set
            {
                if ((this._BatchAdj != value))
                {
                    this._BatchAdj = value;
                }
            }
        }

        [Column(Storage = "_DecHandpay", DbType = "Float")]
        public System.Nullable<double> DecHandpay
        {
            get
            {
                return this._DecHandpay;
            }
            set
            {
                if ((this._DecHandpay != value))
                {
                    this._DecHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDCHandpay", DbType = "Float")]
        public System.Nullable<double> RDCHandpay
        {
            get
            {
                return this._RDCHandpay;
            }
            set
            {
                if ((this._RDCHandpay != value))
                {
                    this._RDCHandpay = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_In", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_In
        {
            get
            {
                return this._RDC_Tickets_In;
            }
            set
            {
                if ((this._RDC_Tickets_In != value))
                {
                    this._RDC_Tickets_In = value;
                }
            }
        }

        [Column(Storage = "_RDC_Tickets_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Tickets_Out
        {
            get
            {
                return this._RDC_Tickets_Out;
            }
            set
            {
                if ((this._RDC_Tickets_Out != value))
                {
                    this._RDC_Tickets_Out = value;
                }
            }
        }

        [Column(Storage = "_MeterHandpay", DbType = "Float")]
        public System.Nullable<double> MeterHandpay
        {
            get
            {
                return this._MeterHandpay;
            }
            set
            {
                if ((this._MeterHandpay != value))
                {
                    this._MeterHandpay = value;
                }
            }
        }

        [Column(Storage = "_Voucher", DbType = "Float")]
        public System.Nullable<double> Voucher
        {
            get
            {
                return this._Voucher;
            }
            set
            {
                if ((this._Voucher != value))
                {
                    this._Voucher = value;
                }
            }
        }

        [Column(Storage = "_RDC_Take", DbType = "Float")]
        public System.Nullable<double> RDC_Take
        {
            get
            {
                return this._RDC_Take;
            }
            set
            {
                if ((this._RDC_Take != value))
                {
                    this._RDC_Take = value;
                }
            }
        }

        [Column(Storage = "_Cash_Take", DbType = "Float")]
        public System.Nullable<double> Cash_Take
        {
            get
            {
                return this._Cash_Take;
            }
            set
            {
                if ((this._Cash_Take != value))
                {
                    this._Cash_Take = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins", DbType = "Float")]
        public System.Nullable<double> RDC_Coins
        {
            get
            {
                return this._RDC_Coins;
            }
            set
            {
                if ((this._RDC_Coins != value))
                {
                    this._RDC_Coins = value;
                }
            }
        }

        [Column(Storage = "_Hopperchange", DbType = "Float")]
        public System.Nullable<double> Hopperchange
        {
            get
            {
                return this._Hopperchange;
            }
            set
            {
                if ((this._Hopperchange != value))
                {
                    this._Hopperchange = value;
                }
            }
        }

        [Column(Storage = "_RDC_Coins_Out", DbType = "Float")]
        public System.Nullable<double> RDC_Coins_Out
        {
            get
            {
                return this._RDC_Coins_Out;
            }
            set
            {
                if ((this._RDC_Coins_Out != value))
                {
                    this._RDC_Coins_Out = value;
                }
            }
        }

        [Column(Storage = "_Void", DbType = "Float")]
        public System.Nullable<double> Void
        {
            get
            {
                return this._Void;
            }
            set
            {
                if ((this._Void != value))
                {
                    this._Void = value;
                }
            }
        }

        [Column(Storage = "_Expired", DbType = "Float")]
        public System.Nullable<double> Expired
        {
            get
            {
                return this._Expired;
            }
            set
            {
                if ((this._Expired != value))
                {
                    this._Expired = value;
                }
            }
        }

        [Column(Storage = "_WinLossVar", DbType = "Float")]
        public System.Nullable<double> WinLossVar
        {
            get
            {
                return this._WinLossVar;
            }
            set
            {
                if ((this._WinLossVar != value))
                {
                    this._WinLossVar = value;
                }
            }
        }

        [Column(Storage = "_DecEftIn", DbType = "Float")]
        public System.Nullable<double> DecEftIn
        {
            get
            {
                return this._DecEftIn;
            }
            set
            {
                if ((this._DecEftIn != value))
                {
                    this._DecEftIn = value;
                }
            }
        }

        [Column(Storage = "_DecEftOut", DbType = "Float")]
        public System.Nullable<double> DecEftOut
        {
            get
            {
                return this._DecEftOut;
            }
            set
            {
                if ((this._DecEftOut != value))
                {
                    this._DecEftOut = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Declared", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Declared
        {
            get
            {
                return this._Progressive_Value_Declared;
            }
            set
            {
                if ((this._Progressive_Value_Declared != value))
                {
                    this._Progressive_Value_Declared = value;
                }
            }
        }

        [Column(Storage = "_Progressive_Value_Variance", DbType = "Float")]
        public System.Nullable<double> Progressive_Value_Variance
        {
            get
            {
                return this._Progressive_Value_Variance;
            }
            set
            {
                if ((this._Progressive_Value_Variance != value))
                {
                    this._Progressive_Value_Variance = value;
                }
            }
        }

        [Column(Storage = "_EftIn", DbType = "Float")]
        public System.Nullable<double> EftIn
        {
            get
            {
                return this._EftIn;
            }
            set
            {
                if ((this._EftIn != value))
                {
                    this._EftIn = value;
                }
            }
        }

        [Column(Storage = "_EftOut", DbType = "Float")]
        public System.Nullable<double> EftOut
        {
            get
            {
                return this._EftOut;
            }
            set
            {
                if ((this._EftOut != value))
                {
                    this._EftOut = value;
                }
            }
        }

        [Column(Storage = "_PromoCashableIn", DbType = "Float")]
        public System.Nullable<double> PromoCashableIn
        {
            get
            {
                return this._PromoCashableIn;
            }
            set
            {
                if ((this._PromoCashableIn != value))
                {
                    this._PromoCashableIn = value;
                }
            }
        }
       [Column(Storage = "_PromoNonCashableIn", DbType = "Float")]
        public System.Nullable<double> PromoNonCashableIn
        {
            get
            {
                return this._PromoNonCashableIn;
            }
            set
            {
                if ((this._PromoNonCashableIn != value))
                {
                    this._PromoNonCashableIn = value;
                }
            }
        }
    }

    public partial class rsp_GetAreaDetailsResult
    {

        private int _Sub_Company_Area_ID;

        private string _Sub_Company_Area_Name;

        private System.Nullable<int> _Company_ID;

        private int _Sub_Company_ID;

        public rsp_GetAreaDetailsResult()
        {
        }

        [Column(Storage = "_Sub_Company_Area_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Area_ID
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

        [Column(Storage = "_Sub_Company_Area_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Area_Name
        {
            get
            {
                return this._Sub_Company_Area_Name;
            }
            set
            {
                if ((this._Sub_Company_Area_Name != value))
                {
                    this._Sub_Company_Area_Name = value;
                }
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int")]
        public System.Nullable<int> Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
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
    }

    public partial class rsp_GetDistrictDetailsResult
    {

        private int _Sub_Company_District_ID;

        private string _Sub_Company_District_Name;

        private System.Nullable<int> _Company_ID;

        private int _Sub_Company_ID;

        public rsp_GetDistrictDetailsResult()
        {
        }

        [Column(Storage = "_Sub_Company_District_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_District_ID
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

        [Column(Storage = "_Sub_Company_District_Name", DbType = "VarChar(50)")]
        public string Sub_Company_District_Name
        {
            get
            {
                return this._Sub_Company_District_Name;
            }
            set
            {
                if ((this._Sub_Company_District_Name != value))
                {
                    this._Sub_Company_District_Name = value;
                }
            }
        }

        [Column(Storage = "_Company_ID", DbType = "Int")]
        public System.Nullable<int> Company_ID
        {
            get
            {
                return this._Company_ID;
            }
            set
            {
                if ((this._Company_ID != value))
                {
                    this._Company_ID = value;
                }
            }
        }

        [Column(Storage = "_Sub_Company_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_ID
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
    }

    public partial class rsp_GetVSStaffDetailsResult
    {

        private int _Staff_ID;

        private string _Staff_Name;

        public rsp_GetVSStaffDetailsResult()
        {
        }

        [Column(Storage = "_Staff_ID", DbType = "Int NOT NULL")]
        public int Staff_ID
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

        [Column(Storage = "_Staff_Name", DbType = "VarChar(101)")]
        public string Staff_Name
        {
            get
            {
                return this._Staff_Name;
            }
            set
            {
                if ((this._Staff_Name != value))
                {
                    this._Staff_Name = value;
                }
            }
        }
    }

    public partial class rsp_GetDepoDetailsResult
    {

        private int _Depot_ID;

        private string _Depot_Name;

        public rsp_GetDepoDetailsResult()
        {
        }

        [Column(Storage = "_Depot_ID", DbType = "Int NOT NULL")]
        public int Depot_ID
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
    }

    public partial class rsp_GetSubCompanyRegionsForFilterResult
    {

        private int _Sub_Company_Region_ID;

        private string _Sub_Company_Region_Name;

        public rsp_GetSubCompanyRegionsForFilterResult()
        {
        }

        [Column(Storage = "_Sub_Company_Region_ID", DbType = "Int NOT NULL")]
        public int Sub_Company_Region_ID
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

        [Column(Storage = "_Sub_Company_Region_Name", DbType = "VarChar(50)")]
        public string Sub_Company_Region_Name
        {
            get
            {
                return this._Sub_Company_Region_Name;
            }
            set
            {
                if ((this._Sub_Company_Region_Name != value))
                {
                    this._Sub_Company_Region_Name = value;
                }
            }
        }
    }

    public partial class rsp_AssetGameReportResult
    {

        private System.Nullable<int> _Bar_Position_ID;

        private string _Machine_Stock_No;

        private string _MG_Game_Manufacturer_Name;

        private string _Game_Name;

        private string _MG_Alias_Game_Name;

        private System.Nullable<double> _Handle;

        private System.Nullable<double> _NetWin;

        private System.Nullable<double> _DailyWin;

        private System.Nullable<double> _AvgBet;

        private System.Nullable<int> _Played;

        private System.Nullable<double> _TotalBet;

        private System.Nullable<double> _TotalWon;

        private System.Nullable<double> _Theo;

        private System.Nullable<double> _Actual;

        private System.Nullable<double> _Variance;

        public rsp_AssetGameReportResult()
        {
        }

        [Column(Storage = "_Bar_Position_ID", DbType = "Int")]
        public System.Nullable<int> Bar_Position_ID
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

        [Column(Storage = "_MG_Game_Manufacturer_Name", DbType = "VarChar(50)")]
        public string MG_Game_Manufacturer_Name
        {
            get
            {
                return this._MG_Game_Manufacturer_Name;
            }
            set
            {
                if ((this._MG_Game_Manufacturer_Name != value))
                {
                    this._MG_Game_Manufacturer_Name = value;
                }
            }
        }

        [Column(Storage = "_Game_Name", DbType = "VarChar(100)")]
        public string Game_Name
        {
            get
            {
                return this._Game_Name;
            }
            set
            {
                if ((this._Game_Name != value))
                {
                    this._Game_Name = value;
                }
            }
        }

        [Column(Storage = "_MG_Alias_Game_Name", DbType = "VarChar(100)")]
        public string MG_Alias_Game_Name
        {
            get
            {
                return this._MG_Alias_Game_Name;
            }
            set
            {
                if ((this._MG_Alias_Game_Name != value))
                {
                    this._MG_Alias_Game_Name = value;
                }
            }
        }

        [Column(Storage = "_Handle", DbType = "Float")]
        public System.Nullable<double> Handle
        {
            get
            {
                return this._Handle;
            }
            set
            {
                if ((this._Handle != value))
                {
                    this._Handle = value;
                }
            }
        }

        [Column(Storage = "_NetWin", DbType = "Float")]
        public System.Nullable<double> NetWin
        {
            get
            {
                return this._NetWin;
            }
            set
            {
                if ((this._NetWin != value))
                {
                    this._NetWin = value;
                }
            }
        }

        [Column(Storage = "_DailyWin", DbType = "Float")]
        public System.Nullable<double> DailyWin
        {
            get
            {
                return this._DailyWin;
            }
            set
            {
                if ((this._DailyWin != value))
                {
                    this._DailyWin = value;
                }
            }
        }

        [Column(Storage = "_AvgBet", DbType = "Float")]
        public System.Nullable<double> AvgBet
        {
            get
            {
                return this._AvgBet;
            }
            set
            {
                if ((this._AvgBet != value))
                {
                    this._AvgBet = value;
                }
            }
        }

        [Column(Storage = "_Played", DbType = "Int")]
        public System.Nullable<int> Played
        {
            get
            {
                return this._Played;
            }
            set
            {
                if ((this._Played != value))
                {
                    this._Played = value;
                }
            }
        }

        [Column(Storage = "_TotalBet", DbType = "Float")]
        public System.Nullable<double> TotalBet
        {
            get
            {
                return this._TotalBet;
            }
            set
            {
                if ((this._TotalBet != value))
                {
                    this._TotalBet = value;
                }
            }
        }

        [Column(Storage = "_TotalWon", DbType = "Float")]
        public System.Nullable<double> TotalWon
        {
            get
            {
                return this._TotalWon;
            }
            set
            {
                if ((this._TotalWon != value))
                {
                    this._TotalWon = value;
                }
            }
        }

        [Column(Storage = "_Theo", DbType = "Float")]
        public System.Nullable<double> Theo
        {
            get
            {
                return this._Theo;
            }
            set
            {
                if ((this._Theo != value))
                {
                    this._Theo = value;
                }
            }
        }

        [Column(Storage = "_Actual", DbType = "Float")]
        public System.Nullable<double> Actual
        {
            get
            {
                return this._Actual;
            }
            set
            {
                if ((this._Actual != value))
                {
                    this._Actual = value;
                }
            }
        }

        [Column(Storage = "_Variance", DbType = "Float")]
        public System.Nullable<double> Variance
        {
            get
            {
                return this._Variance;
            }
            set
            {
                if ((this._Variance != value))
                {
                    this._Variance = value;
                }
            }
        }
    }

    public partial class rsp_GetLastWeekIdResult
    {

        private int _Calendar_Week_ID;

        private System.Nullable<System.DateTime> _Week_Start_Date;

        public rsp_GetLastWeekIdResult()
        {
        }

        [Column(Storage = "_Calendar_Week_ID", DbType = "Int NOT NULL")]
        public int Calendar_Week_ID
        {
            get
            {
                return this._Calendar_Week_ID;
            }
            set
            {
                if ((this._Calendar_Week_ID != value))
                {
                    this._Calendar_Week_ID = value;
                }
            }
        }

        [Column(Storage = "_Week_Start_Date", DbType = "DateTime")]
        public System.Nullable<System.DateTime> Week_Start_Date
        {
            get
            {
                return this._Week_Start_Date;
            }
            set
            {
                if ((this._Week_Start_Date != value))
                {
                    this._Week_Start_Date = value;
                }
            }
        }
    }

    public partial class rsp_GetLastBatchIdResult
    {

        private int _Batch_ID;

        public rsp_GetLastBatchIdResult()
        {
        }

        [Column(Storage = "_Batch_ID", DbType = "Int NOT NULL")]
        public int Batch_ID
        {
            get
            {
                return this._Batch_ID;
            }
            set
            {
                if ((this._Batch_ID != value))
                {
                    this._Batch_ID = value;
                }
            }
        }
    }

    public partial class rsp_MultiGameLibraryResult
    {

        private int _MG_Group_ID;

        private string _MG_Alias_Game_Name;

        private string _Game_Category_Name;

        private string _Manufacturer;

        public rsp_MultiGameLibraryResult()
        {
        }

        [Column(Storage = "_MG_Group_ID", DbType = "Int NOT NULL")]
        public int MG_Group_ID
        {
            get
            {
                return this._MG_Group_ID;
            }
            set
            {
                if ((this._MG_Group_ID != value))
                {
                    this._MG_Group_ID = value;
                }
            }
        }

        [Column(Storage = "_MG_Alias_Game_Name", DbType = "VarChar(100)")]
        public string MG_Alias_Game_Name
        {
            get
            {
                return this._MG_Alias_Game_Name;
            }
            set
            {
                if ((this._MG_Alias_Game_Name != value))
                {
                    this._MG_Alias_Game_Name = value;
                }
            }
        }

        [Column(Storage = "_Game_Category_Name", DbType = "VarChar(50)")]
        public string Game_Category_Name
        {
            get
            {
                return this._Game_Category_Name;
            }
            set
            {
                if ((this._Game_Category_Name != value))
                {
                    this._Game_Category_Name = value;
                }
            }
        }

        [Column(Storage = "_Manufacturer", DbType = "VarChar(50)")]
        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                if ((this._Manufacturer != value))
                {
                    this._Manufacturer = value;
                }
            }
        }
    }

    public partial class rsp_GetPaytableForGameResult
    {

        private string _PaytableID;

        private System.Nullable<double> _Payout;

        private double _TheoreticalPayout;

        private double _MaxBet;

        public rsp_GetPaytableForGameResult()
        {
        }

        [Column(Storage = "_PaytableID", DbType = "VarChar(100)")]
        public string PaytableID
        {
            get
            {
                return this._PaytableID;
            }
            set
            {
                if ((this._PaytableID != value))
                {
                    this._PaytableID = value;
                }
            }
        }

        [Column(Storage = "_Payout", DbType = "Float")]
        public System.Nullable<double> Payout
        {
            get
            {
                return this._Payout;
            }
            set
            {
                if ((this._Payout != value))
                {
                    this._Payout = value;
                }
            }
        }

        [Column(Storage = "_TheoreticalPayout", DbType = "Float NOT NULL")]
        public double TheoreticalPayout
        {
            get
            {
                return this._TheoreticalPayout;
            }
            set
            {
                if ((this._TheoreticalPayout != value))
                {
                    this._TheoreticalPayout = value;
                }
            }
        }

        [Column(Storage = "_MaxBet", DbType = "Float NOT NULL")]
        public double MaxBet
        {
            get
            {
                return this._MaxBet;
            }
            set
            {
                if ((this._MaxBet != value))
                {
                    this._MaxBet = value;
                }
            }
        }
    }

    public partial class rsp_GetDepreciationPolicyInfoResult
    {

        private string _Machine_End_Date;

        private System.Nullable<decimal> _Machine_Original_Purchase_Price;

        private string _Machine_Depreciation_Start_Date;

        private System.Nullable<int> _Depreciation_Policy_ID;

        private System.Nullable<int> _Machine_Class_ID;

        private System.Nullable<int> _Depreciation_Policy_Details_ID;

        private System.Nullable<float> _Depreciation_Policy_Residual_Value;

        private System.Nullable<int> _Depreciation_Policy_Details_Duration;

        private System.Nullable<int> _Depreciation_Policy_Details_Percentage;

        public rsp_GetDepreciationPolicyInfoResult()
        {
        }

        [Column(Storage = "_Machine_End_Date", DbType = "VarChar(30)")]
        public string Machine_End_Date
        {
            get
            {
                return this._Machine_End_Date;
            }
            set
            {
                if ((this._Machine_End_Date != value))
                {
                    this._Machine_End_Date = value;
                }
            }
        }

        [Column(Storage = "_Machine_Original_Purchase_Price", DbType = "Money")]
        public System.Nullable<decimal> Machine_Original_Purchase_Price
        {
            get
            {
                return this._Machine_Original_Purchase_Price;
            }
            set
            {
                if ((this._Machine_Original_Purchase_Price != value))
                {
                    this._Machine_Original_Purchase_Price = value;
                }
            }
        }

        [Column(Storage = "_Machine_Depreciation_Start_Date", DbType = "VarChar(30)")]
        public string Machine_Depreciation_Start_Date
        {
            get
            {
                return this._Machine_Depreciation_Start_Date;
            }
            set
            {
                if ((this._Machine_Depreciation_Start_Date != value))
                {
                    this._Machine_Depreciation_Start_Date = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_ID", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_ID
        {
            get
            {
                return this._Depreciation_Policy_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_ID != value))
                {
                    this._Depreciation_Policy_ID = value;
                }
            }
        }

        [Column(Storage = "_Machine_Class_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Class_ID
        {
            get
            {
                return this._Machine_Class_ID;
            }
            set
            {
                if ((this._Machine_Class_ID != value))
                {
                    this._Machine_Class_ID = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_Details_ID", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_Details_ID
        {
            get
            {
                return this._Depreciation_Policy_Details_ID;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_ID != value))
                {
                    this._Depreciation_Policy_Details_ID = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_Residual_Value", DbType = "Real")]
        public System.Nullable<float> Depreciation_Policy_Residual_Value
        {
            get
            {
                return this._Depreciation_Policy_Residual_Value;
            }
            set
            {
                if ((this._Depreciation_Policy_Residual_Value != value))
                {
                    this._Depreciation_Policy_Residual_Value = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_Details_Duration", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_Details_Duration
        {
            get
            {
                return this._Depreciation_Policy_Details_Duration;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Duration != value))
                {
                    this._Depreciation_Policy_Details_Duration = value;
                }
            }
        }

        [Column(Storage = "_Depreciation_Policy_Details_Percentage", DbType = "Int")]
        public System.Nullable<int> Depreciation_Policy_Details_Percentage
        {
            get
            {
                return this._Depreciation_Policy_Details_Percentage;
            }
            set
            {
                if ((this._Depreciation_Policy_Details_Percentage != value))
                {
                    this._Depreciation_Policy_Details_Percentage = value;
                }
            }
        }
    }
}
