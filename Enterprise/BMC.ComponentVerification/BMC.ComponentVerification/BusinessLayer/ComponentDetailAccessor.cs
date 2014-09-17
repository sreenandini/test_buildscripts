using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ComponentVerification.DataAccess;
using System.Data.SqlClient;
using BMC.ComponentVerification.BusinessEntities;

namespace BMC.ComponentVerification.BusinessLayer
{
    class ComponentDetailAccessor
    {
        DataRetriever dr;
        
        public ComponentDetailAccessor()
        {
            dr = new DataRetriever(new SqlConnection(DbUtilities.GetConnectionString()));
        }


        public List<AlgorithmTypesData> GetAlgorithmTypes(int iCompID, int iNew)
        {
            //Dictionary<int, AlgorithmTypesData> lstAlgorithmTypes = new Dictionary<int, AlgorithmTypesData>();
            var lstATypes = new List<AlgorithmTypesData>();
            var result = dr.GetAlgorithmTypes(iCompID, iNew);
            foreach (var atdata in result)
                lstATypes.Add(atdata);
            return lstATypes;
        }

        public int ? SaveComponetDetails(string strCName, string strSerial, int nCompType, int nAlgoType, string strSeed, string strHash)
        {
            int? result = 0;
            dr.InsertComponentDetails(strCName, strSerial, nCompType, nAlgoType, strSeed, strHash,ref result);            
            return result;
        }


        public int ? ModifyComponentDetails(string strCName, string strSerial, int nCompType, int nAlgoType, string strSeed, string strHash)
        {
            int ? result = 0;
            dr.UpdateComponentDetails(strCName, strSerial, nCompType, nAlgoType, strSeed, strHash, ref result);
            return result;
        }


        public List<ComponentDetailsData> GetComponentDetails(int nCompId)
        {
            var cdata = dr.GetComponentDetails(nCompId);
            var lstData = new List<ComponentDetailsData>();
            foreach (ComponentDetailsData data in cdata)
                lstData.Add(data);
            return lstData;
        }

        public int InsertComponentDetailsEHRecord(int ? @compId)
        {
            var comp = dr.InsertComponentDetailsEHRecord(@compId,"ALL");
            return comp;
        }       
    }
}
