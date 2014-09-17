/// Source File Name : MeterAnalysisTransport.cs
/// Description		 : Transport layer for Meter analysis
/// Revision History
/// Author             Date              Description
/// ---------------------------------------------------
/// Madhusudhanan      13/5/08          created
using System;
using System.Collections.Generic;
using System.Text;

namespace BMC.Business.Classes
{
    /// <summary>
    /// Transport layer for meter analysis screen
    /// Revision History:
    /// Date 12-5-08    Created     Madhusudhanan
    /// </summary>
   public class MeterAnalysisTransport
    {
        Int32 iCompanyId, iSubCompanyId, iRegionId, iAreaId, iDistrictId, iSiteId, iOperatorId, iDepotId, iTypeId, iCategoryId, iClassId;
        Int32 iManufacturerId,  iSearchMCId, iSearchInstallationID;
       

       Int16 iPeriodId;
        string strSearchAsset, strActive = string.Empty, strGroupBy, strNoOfRecords,strCriteria;
        DateTime dtStartDate, dtEndDate;

       //constructor
        public MeterAnalysisTransport()
        {

        }

        #region "Public Int properties"

        public int UserID { get; set; }
       public Int32 CompanyID
        { 
            get{return iCompanyId;}
            set { iCompanyId = value; }
        }
       public Int32 SubCompanyID
        {
            get { return iSubCompanyId; }
            set { iSubCompanyId = value; }
        }
       public Int32 RegionID
        {
            get { return iRegionId; }
            set { iRegionId = value; }
        }
       public Int32 AreaID
        {
            get { return iAreaId; }
            set { iAreaId = value; }
        }
       public Int32 DistrictID
        {
            get { return iDistrictId; }
            set { iDistrictId = value; }
        }
       public Int32 SiteID
        {
            get { return iSiteId; }
            set { iSiteId = value; }
        }
       public Int32 OperatorID
        {
            get { return iOperatorId; }
            set { iOperatorId = value; }
        }
       public Int32 DepotID
        {
            get { return iDepotId; }
            set { iDepotId = value; }
        }
       public Int32 TypeID
        {
            get { return iTypeId; }
            set { iTypeId = value; }
        }
       public Int32 CategoryID
        {
            get { return iCategoryId; }
            set { iCategoryId = value; }
        }
       public Int32 ClassID
        {
            get { return iClassId; }
            set { iClassId = value; }
        }

       public Int32 ManufacturerID
        {
            get { return iManufacturerId; }
            set { iManufacturerId = value; }
        }
        public Int16 PeriodID
        {
            get { return iPeriodId; }
            set { iPeriodId = value; }
        }
       public Int32 SearchMCID
        {
            get { return iSearchMCId; }
            set { iSearchMCId = value; }
        }
        public string NoOfRecords
        {
            get { return strNoOfRecords; }
            set { strNoOfRecords = value; }
        }
       public Int32 SearchInstallationID
        {
            get { return iSearchInstallationID; }
            set { iSearchInstallationID = value; }
        }
        private int _SiteStatusID;

        public int SiteStatusID
        {
            get { return _SiteStatusID; }
            set { _SiteStatusID = value; }
        }
	
        #endregion

        #region "Public string properties"
        public string SearchAsset
        { 
        
            get { return strSearchAsset; }
            set { strSearchAsset = value; }
       
        }
        public string Active
        {
            get { return strActive; }
            set { strActive = value; }
        }

        public string GroupByClause
        {
            get { return strGroupBy; }
            set { strGroupBy = value; }
        }

        public string Criteria
        {
            get { return strCriteria; }
            set { strCriteria = value; }
        }
#endregion

        #region "Public DateTime Properties"
        public DateTime StartDate
        {
            get { return dtStartDate; }
            set { dtStartDate = value; }
        }
        public DateTime EndDate
        {
            get { return dtEndDate; }
            set { dtEndDate = value; }
        }
        #endregion
        private string _ActiveSites;

        public string IsOnlyActiveSites
        {
            get { return _ActiveSites; }
            set { _ActiveSites = value; }
        }
	
    }
    
}
