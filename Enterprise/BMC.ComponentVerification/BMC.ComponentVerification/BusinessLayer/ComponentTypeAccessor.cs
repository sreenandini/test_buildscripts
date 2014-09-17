using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BMC.ComponentVerification.DataAccess;
using BMC.ComponentVerification.BusinessEntities;
using System.ComponentModel;

namespace BMC.ComponentVerification.BusinessLayer
{
    public class ComponentTypeAccessor
    {
        DataRetriever dr;
        public ComponentTypeAccessor()
        {
            dr = new DataRetriever(new SqlConnection(DbUtilities.GetConnectionString()));
        }

        public List<ComponentTypesData> GetComponentTypes()
        {
            List<ComponentTypesData> lstComponentTypes = new List<ComponentTypesData>();            
            var result = dr.GetDefaultComponentTypes();
            foreach(ComponentTypesData ctdata in result)
                   lstComponentTypes.Add(ctdata);
            return lstComponentTypes;            
        }

        public int SaveComponentType(string strCName, string strCDesc)
        {
            int result = dr.InsertComponentType(strCName, strCDesc);
            return result;
        }

        public List<AllComponentDetailsData> GetAllComponentDetails()
        {
            var lstCDetails = new List<AllComponentDetailsData>();
            var lstResult = dr.GetAllComponentDetails();
            foreach (var detail in lstResult)
                lstCDetails.Add(detail);
            return lstCDetails;
        }

        public List<VerificationCompDetailsData> GetVerificationComponentDetails(DateTime dtFrom, DateTime dtTo, string strCompType, string strVerType, string strSiteName, string strSerial, string strCompName)
        {
            var details = dr.GetVerificationComponentDetails(dtFrom, dtTo, strCompType, strVerType, strSiteName, strSerial, strCompName);
            var verData = new List<VerificationCompDetailsData>();
            foreach (VerificationCompDetailsData data in details)
                verData.Add(data);
            return verData;
        }

        public List<VerificationTypesData> GetVerificationTypes()
        {
            List<VerificationTypesData> lstVerTypes = new List<VerificationTypesData>();
            var result = dr.GetDefaultVerificationTypes();
            foreach (VerificationTypesData ctdata in result)
                lstVerTypes.Add(ctdata);
            return lstVerTypes;
        }

    }
}
