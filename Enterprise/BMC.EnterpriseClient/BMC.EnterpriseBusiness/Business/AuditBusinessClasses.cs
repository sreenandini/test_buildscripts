using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Audit.Transport;
using Audit.DBBuilder;
using System.Data.Linq;
using BMC.Security;

namespace BMC.EnterpriseBusiness.Business
{
    public class AuditBusiness
    {
        //AuditDataContext Adc = null;
        private static AuditBusiness _AuditInstance;
        private static string _connectionString = string.Empty;

        public AuditBusiness()
        {
        }

        //public AuditBusiness(System.Data.SqlClient.SqlConnection connection)
        //{
        //   //Adc = new AuditDataContext(connection);
        //}

        public AuditBusiness(string sConnection)
        {
            //Adc = new AuditDataContext(sConnection);
            _connectionString = sConnection;
        }

        public static void CreateInstance(string sConnectionString)
        {
            _AuditInstance = new AuditBusiness(sConnectionString);
        }

        private static AuditDataContext DataContext
        {
            get { return new AuditDataContext(_connectionString); }
        }

        public List<AuditModules> GetModulesList()
        {
            using (AuditDataContext Adc = AuditBusiness.DataContext)
            {
                return Adc.GetModulesList();
            }
        }

        // required for those apps ( enterprise) which don't use security class
        public int InsertAuditData(Audit_History objAH, bool NoLoginInfo)
        {
            using (AuditDataContext Adc = AuditBusiness.DataContext)
            {
                return Adc.InsertAuditData(objAH.Audit_User_ID, objAH.Audit_User_Name, objAH.Audit_Module_ID, objAH.Audit_Module_Name, objAH.Audit_Screen_Name,
                                    objAH.Audit_Slot, objAH.Audit_Field, objAH.Audit_Old_Vl, objAH.Audit_New_Vl, objAH.Audit_Desc, objAH.Audit_Operation_Type);
            }
        }


        public static int InsertAuditData(Audit_History objAH)
        {
            using (AuditDataContext Adc = AuditBusiness.DataContext)
            {
                return _AuditInstance.InsertAuditData_internal(objAH);
            }
        }


        internal int InsertAuditData_internal(Audit_History objAH)
        {
            if (SecurityHelper.CurrentUser != null && SecurityHelper.CurrentUser.SecurityUserID != 0 && SecurityHelper.CurrentUser.UserName != null)
            {
                objAH.Audit_User_ID = SecurityHelper.CurrentUser.SecurityUserID;
                objAH.Audit_User_Name = SecurityHelper.CurrentUser.DisplayName;
            }

            using (AuditDataContext Adc = AuditBusiness.DataContext)
            {
                return Adc.InsertAuditData(objAH.Audit_User_ID, objAH.Audit_User_Name, objAH.Audit_Module_ID, objAH.Audit_Module_Name, objAH.Audit_Screen_Name,
                                    objAH.Audit_Slot, objAH.Audit_Field, objAH.Audit_Old_Vl, objAH.Audit_New_Vl, objAH.Audit_Desc, objAH.Audit_Operation_Type);
            }
        }

        public int[] InsertAuditData(Audit_History[] objAH)
        {
            int[] nReturnValue = new int[objAH.Length];

            for (int nCount = 0; nCount < objAH.Length; nCount++)
            {
                objAH[nCount].Audit_User_ID = SecurityHelper.CurrentUser.SecurityUserID;
                objAH[nCount].Audit_User_Name = SecurityHelper.CurrentUser.DisplayName;

                using (AuditDataContext Adc = AuditBusiness.DataContext)
                {
                    nReturnValue[nCount] = Adc.InsertAuditData(objAH[nCount].Audit_User_ID, objAH[nCount].Audit_User_Name, objAH[nCount].Audit_Module_ID, objAH[nCount].Audit_Module_Name, objAH[nCount].Audit_Screen_Name,
                                        objAH[nCount].Audit_Slot, objAH[nCount].Audit_Field, objAH[nCount].Audit_Old_Vl, objAH[nCount].Audit_New_Vl, objAH[nCount].Audit_Desc, objAH[nCount].Audit_Operation_Type);
                }
            }
            return nReturnValue;

        }


        public ISingleResult<Audit_History> GetAuditDetails([Parameter(Name = "FromDate", DbType = "DateTime")] System.Nullable<System.DateTime> fromDate, [Parameter(Name = "ToDate", DbType = "DateTime")] System.Nullable<System.DateTime> toDate, [Parameter(Name = "ModuleID", DbType = "VarChar(50)")] string moduleID)
        {
            using (AuditDataContext Adc = AuditBusiness.DataContext)
            {
                return Adc.GetAuditDetails(fromDate, toDate, moduleID);
            }
        }

    }
}
