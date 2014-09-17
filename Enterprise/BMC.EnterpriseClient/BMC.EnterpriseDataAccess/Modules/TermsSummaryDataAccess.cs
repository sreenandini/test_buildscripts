using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {

        [Function(Name = "dbo.rsp_GetOperatorNames")]
        public ISingleResult<OperatorNames> GetOperatorNames()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<OperatorNames>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetSubCompanyNames")]
        public ISingleResult<SubCompanyNames> GetSubCompanyNames()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<SubCompanyNames>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetDepotNames")]
        public ISingleResult<DepotNames> GetDepotNames([Parameter(Name = "Supplier_Id", DbType = "Int")] int supplierId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), supplierId);
            return ((ISingleResult<DepotNames>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetMachineTypes")]
        public ISingleResult<MachineTypes> GetMachineTypes()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<MachineTypes>)(result.ReturnValue));
        }

        [Function(Name = "dbo.rsp_GetTermsSummaryList")]
        public ISingleResult<TermsSummaryList> GetTermsSummaryList([Parameter(Name = "OperatorId", DbType = "Int")] int operatorId,
            [Parameter(Name = "DepotId", DbType = "Int")] int depotId,
            [Parameter(Name = "MachineId", DbType = "Int")] int machineId,
            [Parameter(Name = "SubCompanyId", DbType = "Int")] int subCompanyId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), operatorId, depotId, machineId, subCompanyId);
            return ((ISingleResult<TermsSummaryList>)(result.ReturnValue));
        }

        [Function(Name = "dbo.usp_UpdateBarPosRentTerms")]
        public int UpdateTermsShareAndRent([Parameter(Name = "BarPosId", DbType = "Int")] int id,
            [Parameter(Name = "BarPosRent", DbType = "Real")] float rent,
            [Parameter(Name = "BarPosSupplierShare", DbType = "Real")] float supplier,
            [Parameter(Name = "BarPosSiteShare", DbType = "Real")] float site,
            [Parameter(Name = "BarPosOwnersShare", DbType = "Real")] float owner,
            [Parameter(Name = "BarPosLicenceCharge", DbType = "Real")] float licence)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, rent, supplier, site, owner, licence);
            return ((int)(result.ReturnValue));
        }
    }

    
    public partial class TermsSummaryList
    {
        private string _SiteName;
        [Column(Name = "Site_Name", Storage = "_SiteName", DbType = "VarChar(50)")]
        public string SiteName { get { return _SiteName; } set { _SiteName = value; } }

        private string _BarPositionName;
        [Column(Name = "Bar_Position_Name", Storage = "_BarPositionName", DbType = "VarChar(50)")]
        public string BarPositionName { get { return _BarPositionName; } set { _BarPositionName = value; } }

        private string _InstallationID;
        [Column(Name = "Installation_ID", Storage = "_InstallationID", DbType = "Int")]
        public string InstallationID { get { return _InstallationID; } set { _InstallationID = value; } }

        private string _InstallationEndDate;
        [Column(Name = "Installation_End_Date", Storage = "_InstallationEndDate", DbType = "VarChar(30)")]
        public string InstallationEndDate { get { return _InstallationEndDate; } set { _InstallationEndDate = value; } }

        private string _InstallationPricePerPlay;
        [Column(Name = "Installation_Price_Per_Play", Storage = "_InstallationPricePerPlay", DbType = "Int")]
        public string InstallationPricePerPlay { get { return _InstallationPricePerPlay; } set { _InstallationPricePerPlay = value; } }

        private string _InstallationJackpotValue;
        [Column(Name = "Installation_Jackpot_Value", Storage = "_InstallationJackpotValue", DbType = "Int")]
        public string InstallationJackpotValue { get { return _InstallationJackpotValue; } set { _InstallationJackpotValue = value; } }

        private string _MachineName;
        [Column(Name = "Machine_Name", Storage = "_MachineName", DbType = "VarChar(50)")]
        public string MachineName { get { return _MachineName; } set { _MachineName = value; } }

        private string _BarPositionSupplierPositionCode;
        [Column(Name = "Bar_Position_Supplier_Position_Code", Storage = "_BarPositionSupplierPositionCode", DbType = "VarChar(50)")]
        public string BarPositionSupplierPositionCode { get { return _BarPositionSupplierPositionCode; } set { _BarPositionSupplierPositionCode = value; } }

        private string _SiteCode;
        [Column(Name = "Site_Code", Storage = "_SiteCode", DbType = "VarChar(50)")]
        public string SiteCode { get { return _SiteCode; } set { _SiteCode = value; } }

        private string _BarPositionSupplierSiteCode;
        [Column(Name = "Bar_Position_Supplier_Site_Code", Storage = "_BarPositionSupplierSiteCode", DbType = "VarChar(8)")]
        public string BarPositionSupplierSiteCode { get { return _BarPositionSupplierSiteCode; } set { _BarPositionSupplierSiteCode = value; } }

        private int _BarPositionUseTerms;
        [Column(Name = "Bar_Position_Use_Terms", Storage = "_BarPositionUseTerms", DbType = "Bit")]
        public int BarPositionUseTerms { get { return _BarPositionUseTerms; } set { _BarPositionUseTerms = value; } }

        private int _BarPositionID;
        [Column(Name = "Bar_Position_ID", Storage = "_BarPositionID", DbType = "Int NOT NULL")]
        public int BarPositionID { get { return _BarPositionID; } set { _BarPositionID = value; } }

        private string _MachineTypeCode;
        [Column(Name = "Bar_Pos_Machine_Type_Code", Storage = "_MachineTypeCode", DbType = "VarChar(50)")]
        public string MachineTypeCode { get { return _MachineTypeCode; } set { _MachineTypeCode = value; } }

        private string _TermsGroupName;
        [Column(Name = "Terms_Group_Name", Storage = "_TermsGroupName", DbType = "VarChar(50)")]
        public string TermsGroupName { get { return _TermsGroupName; } set { _TermsGroupName = value; } }
    }

    public partial class MachineTypes
    {
        private int _MachineTypeId;
        [Column(Name = "Machine_Type_Id", Storage = "_MachineTypeId", DbType = "Int")]
        public int MachineTypeId { get { return _MachineTypeId; } set { _MachineTypeId = value; } }

        private string _MachineTypeCode;
        [Column(Name = "Machine_Type_Code", Storage = "_MachineTypeCode", DbType = "VarChar(50)")]
        public string MachineTypeCode { get { return _MachineTypeCode; } set { _MachineTypeCode = value; } }
    }

    public partial class OperatorNames
    {
        private int _OperatorId;
        [Column(Name = "Operator_ID", Storage = "_OperatorId", DbType = "Int")]
        public int OperatorId { get { return _OperatorId; } set { _OperatorId = value; } }

        private string _OperatorName;
        [Column(Name = "Operator_Name", Storage = "_OperatorName", DbType = "VarChar(50)")]
        public string OperatorName { get { return _OperatorName; } set { _OperatorName = value; } }
    }

    public partial class SubCompanyNames
    {
        private int _SubCompanyId;
        [Column(Name = "Sub_Company_ID", Storage = "_SubCompanyId", DbType = "Int")]
        public int SubCompanyId { get { return _SubCompanyId; } set { _SubCompanyId = value; } }

        private string _SubCompanyName;
        [Column(Name = "Sub_Company_Name", Storage = "_SubCompanyName", DbType = "VarChar(50)")]
        public string SubCompanyName { get { return _SubCompanyName; } set { _SubCompanyName = value; } }
    }

    public partial class DepotNames
    {
        private int _DepotId;
        [Column(Name = "Depot_Id", Storage = "_DepotId", DbType = "Int")]
        public int DepotId { get { return _DepotId; } set { _DepotId = value; } }

        private string _DepotName;
        [Column(Name = "Depot_Name", Storage = "_DepotName", DbType = "VarChar(50)")]
        public string DepotName { get { return _DepotName; } set { _DepotName = value; } }
    }
}
