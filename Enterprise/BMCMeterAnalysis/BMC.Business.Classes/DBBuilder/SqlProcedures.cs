using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Business.Classes.DBBuilder
{
    class SqlProcedures
    {
        public const string CONST_CATEGORY_QUERY = "SELECT Game_Category_ID, Game_Category_Name  FROM Game_category ORDER BY Game_Category_Name";
        public const string CONST_OPERATOR_QUERY = "SELECT Operator_ID, Operator_Name FROM Operator ORDER BY Operator_Name";
        public const string CONST_TYPE_QUERY = "SELECT Machine_Type_ID, Machine_Type_Code FROM Machine_Type WHERE IsNonGamingAssetType = 0 and isnull(Machine_Type_Code,'') <> ''  ORDER BY Machine_Type_Code";
        public const string CONST_MANUFACTURER_QUERY = "SELECT Manufacturer_ID, Manufacturer_Name FROM Manufacturer ORDER BY Manufacturer_Name ";

        public const string CONST_DEPOT_PROC = "rsp_GetDepotList";
        public const string CONST_MACHINE_CLASS_PROC = "rsp_GetMachineClassList";
        public const string CONST_SITE_DETAILS_PROC = "rsp_GetSiteDetails";
        public const string CONST_LOAD_GRID_PROC = "rsp_GetMeterAnalysisData";
        public const string CONST_LOAD_GRAPH_PROC = "rsp_GetMeterAnalysisGraphData";
    }
}
