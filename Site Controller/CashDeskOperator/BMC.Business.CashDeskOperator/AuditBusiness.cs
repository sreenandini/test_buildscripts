using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
using BMC.Transport;
using System.Globalization;
using System.Reflection;
using BMC.Common.ExceptionManagement;
using Audit.Transport;
namespace BMC.Business.CashDeskOperator
{
   public class AuditBusiness
    {
       AuditDataContext oAuditDataAccess = new AuditDataContext("");
        public AuditBusiness()
        {
        }
        public IEnumerable<FillModules> GetModulesList()
        {
            //return oAuditDataAccess.GetModulesList();
            FillModules oFillModules = null;
            List<FillModules> Olist = null;
            try
            {
                    Olist = new List<FillModules>();
                    FillModules objAudit = new FillModules();
                    objAudit.Audit_Module_Name = "ALL";
                    objAudit.Audit_Module_ID = 0;
                    Olist.Add(objAudit);
                    
                    foreach (ModuleID AH in Enum.GetValues(typeof(ModuleID)))
                    {
                        oFillModules = new FillModules();
                        if (Settings.Client != null && Settings.Client.ToLower() == "winchells")
                        {
                            if (AH.ToString().ToLower() != ModuleID.MachineMaintenance.ToString().ToLower())
                            {
                                oFillModules.Audit_Module_ID = Convert.ToInt32(AH);
                                oFillModules.Audit_Module_Name = AH.ToString();
                                Olist.Add(oFillModules);
                            }
                        }
                        else
                        {
                            oFillModules.Audit_Module_ID = Convert.ToInt32(AH);
                            oFillModules.Audit_Module_Name = AH.ToString();
                            Olist.Add(oFillModules);
                        }
                           
                    }
                   
                }

            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                Olist = null;
            }
            return Olist;
        }
        public List<GetAuditDetailsResult> GetAuditDetails(DateTime fromDate,DateTime ToDate,string ModuleID,int Rows)
        {
            return oAuditDataAccess.rsp_GetAuditDetails(fromDate, ToDate, ModuleID,Rows).ToList();

        }

        public DataSet GetAuditTrailReport(DateTime fromDate, DateTime ToDate, string ModuleID)
        {

          return CollectionExtensions.ToDataSet<GetAuditDetailsResult>
              (oAuditDataAccess.rsp_GetAuditDetails(fromDate, ToDate, ModuleID, 0), "AuditTrail");

        }

        public IEnumerable<GetAFTAuditDetailsResult> GetAFTAuditData(DateTime startDate, DateTime endDate, int Rows)
        {
            return oAuditDataAccess.GetAFTAuditDetails(startDate, endDate, Rows);
        }


        public DataSet GetAFTAuditTrailReport(DateTime fromDate, DateTime toDate)
        {
            return CollectionExtensions.ToDataSet<GetAFTAuditDetailsResult>
              (oAuditDataAccess.GetAFTAuditDetails(fromDate, toDate, 0), "AFTAuditTrail");
        }
    }

   public static class CollectionExtensions
   {
       public static DataSet ToDataSet<T>(this IEnumerable<T> collection, string dataTableName)
       {
           if (collection == null)
           {
               throw new ArgumentNullException("collection");
           }

           if (string.IsNullOrEmpty(dataTableName))
           {
               throw new ArgumentNullException("dataTableName");
           }

           DataSet data = new DataSet("NewDataSet");
           data.Tables.Add(FillDataTable(dataTableName, collection));
           return data;
       }

       private static DataTable FillDataTable<T>(string tableName,IEnumerable<T> collection)
       {
           PropertyInfo[] properties = typeof(T).GetProperties();

           DataTable dt = CreateDataTable<T>(tableName,
           collection, properties);

           IEnumerator<T> enumerator = collection.GetEnumerator();
           while (enumerator.MoveNext())
           {
               dt.Rows.Add(FillDataRow<T>(dt.NewRow(),
              enumerator.Current, properties));
           }

           return dt;
       }

       private static DataRow FillDataRow<T>(DataRow dataRow,T item, PropertyInfo[] properties)
       {
           foreach (PropertyInfo property in properties)
           {
               dataRow[property.Name.ToString()] = property.GetValue(item, null);
           }

           return dataRow;
       }

       private static DataTable CreateDataTable<T>(string tableName,IEnumerable<T> collection, PropertyInfo[] properties)
       {
           DataTable dt = new DataTable(tableName);

           foreach (PropertyInfo property in properties)
           {
               dt.Columns.Add(property.Name.ToString());
           }

           return dt;
       }
   }
}
