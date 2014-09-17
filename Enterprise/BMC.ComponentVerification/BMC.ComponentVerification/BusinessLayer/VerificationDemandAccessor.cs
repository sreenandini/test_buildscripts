using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ComponentVerification.DataAccess;
using System.Data.SqlClient;
using BMC.ComponentVerification.BusinessEntities;

namespace BMC.ComponentVerification.BusinessLayer
{
    class VerificationDemandAccessor
    {
        DataRetriever dr;
        public VerificationDemandAccessor()
        {
            dr = new DataRetriever(new SqlConnection(DbUtilities.GetConnectionString()));
        }

        public List<SiteDetailsData> GetAllSites()
        {
            var siteDetails = dr.GetAllSiteForVerification();
            var lstSiteDetails = new List<SiteDetailsData>();
            foreach (SiteDetailsData data in siteDetails)
                lstSiteDetails.Add(data);
            return lstSiteDetails;
        }

        public List<MachineDetailsData> GetAllMachinesForSite(int ? siteId)
        {
            var macDetails = dr.GetAllMachineForSite(siteId);
            var lstMachineDetails = new List<MachineDetailsData>();
            foreach (MachineDetailsData data in macDetails)
                lstMachineDetails.Add(data);
            return lstMachineDetails;
        }

        public List<MachineCompDetails> GetAllComponentsForMachine(string strSerial)
        {
            var compDetails = dr.GetAllComponentForMachine(strSerial);
            var lstCompDetails = new List<MachineCompDetails>();
            foreach(MachineCompDetails data in compDetails)
                lstCompDetails.Add(data);
            return lstCompDetails;
        }

        public int InsertComponentVerificationData(int iCompID, string strMacSerialNo)
        {
            dr.InsertComponentVerificationRecord(strMacSerialNo, iCompID, 2);
            return 1;
        }

        public int ? CheckComponentStatus(int iCompID, string strSerialNo)
        {
            int ? result = 0;
            dr.CheckComponentVerificationStatus(strSerialNo, iCompID, ref result);
            return result;
        }

        public List<ComponentVerificationData> GetCompVerificationRecordForExport(string strSerial, int iCompID, int iVerificationType)
        {
            var lstDetails = new List<ComponentVerificationData>(); 
            var result = dr.GetCompVerificationRecordForExport(strSerial, iCompID, iVerificationType);
            foreach (ComponentVerificationData data in result)
            {
                lstDetails.Add(data);
            }
            return lstDetails;
        }
    }
}
