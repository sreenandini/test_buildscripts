using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using Audit.Transport;
using Audit.DBBuilder;
using System.Data.Linq;
using BMC.Security;

namespace Audit.BusinessClasses
{
    public class AuditViewerBusiness
    {
        //AuditDataContext Adc = null;
        private Func<AuditDataContext> _createContext = null;

        [ThreadStatic]
        private static AuditViewerBusiness _AuditInstance;
        private static object _instanceLock = new object();
        private static string _connectionStringStatic = string.Empty;

        private string _connectionString = string.Empty;
        private System.Data.SqlClient.SqlConnection _connection = null; 

        private static AuditViewerBusiness AuditInstance
        {
            get
            {
                if (_AuditInstance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_AuditInstance == null)
                        {
                            _AuditInstance = new AuditViewerBusiness(_connectionStringStatic);
                        }
                    }
                }
                return _AuditInstance;
            }
        }

        public AuditViewerBusiness()
        {
        }

        public AuditViewerBusiness(System.Data.SqlClient.SqlConnection connection)
        {
           //Adc = new AuditDataContext(connection);
            _connection = connection;
            _createContext = () => { return new AuditDataContext(_connection); };
        }

        public AuditViewerBusiness(string sConnection)
        {
            //Adc = new AuditDataContext(sConnection);
            _connectionString = sConnection;
            _createContext = () => { return new AuditDataContext(_connectionString); };            
        }

        public static void CreateInstance(string sConnectionString)
        {
            _connectionStringStatic = sConnectionString;
            //_AuditInstance = new AuditViewerBusiness(sConnectionString);
        }

        public List<AuditModules> GetModulesList()
        {
            using (AuditDataContext Adc = _createContext())
            {
                return Adc.GetModulesList();
            }
        }

        // required for those apps ( enterprise) which don't use security class
        public int InsertAuditData(Audit_History objAH, bool NoLoginInfo)
        {
            using (AuditDataContext Adc = _createContext())
            {
                return Adc.InsertAuditData(objAH.Audit_User_ID, objAH.Audit_User_Name, objAH.Audit_Module_ID, objAH.Audit_Module_Name, objAH.Audit_Screen_Name,
                                    objAH.Audit_Slot, objAH.Audit_Field, objAH.Audit_Old_Vl, objAH.Audit_New_Vl, objAH.Audit_Desc, objAH.Audit_Operation_Type);
            }
        }

        public static int InsertAuditData(Audit_History objAH)
        {
            return AuditInstance.InsertAuditData_internal(objAH);
        }

        internal int InsertAuditData_internal(Audit_History objAH)
        {
            using (AuditDataContext Adc = _createContext())
            {
                if (SecurityHelper.CurrentUser != null && SecurityHelper.CurrentUser.SecurityUserID != 0 && SecurityHelper.CurrentUser.UserName != null)
                {
                    objAH.Audit_User_ID = SecurityHelper.CurrentUser.SecurityUserID;
                    objAH.Audit_User_Name = SecurityHelper.CurrentUser.DisplayName;
                }

                return Adc.InsertAuditData(objAH.Audit_User_ID, objAH.Audit_User_Name, objAH.Audit_Module_ID, objAH.Audit_Module_Name, objAH.Audit_Screen_Name,
                                        objAH.Audit_Slot, objAH.Audit_Field, objAH.Audit_Old_Vl, objAH.Audit_New_Vl, objAH.Audit_Desc, objAH.Audit_Operation_Type);
            }
        }

        public int[] InsertAuditData(Audit_History[] objAH)
        {
            using (AuditDataContext Adc = _createContext())
            {
                int[] nReturnValue = new int[objAH.Length];

                for (int nCount = 0; nCount < objAH.Length; nCount++)
                {
                    objAH[nCount].Audit_User_ID = SecurityHelper.CurrentUser.SecurityUserID;
                    objAH[nCount].Audit_User_Name = SecurityHelper.CurrentUser.DisplayName;

                    nReturnValue[nCount] = Adc.InsertAuditData(objAH[nCount].Audit_User_ID, objAH[nCount].Audit_User_Name, objAH[nCount].Audit_Module_ID, objAH[nCount].Audit_Module_Name, objAH[nCount].Audit_Screen_Name,
                                            objAH[nCount].Audit_Slot, objAH[nCount].Audit_Field, objAH[nCount].Audit_Old_Vl, objAH[nCount].Audit_New_Vl, objAH[nCount].Audit_Desc, objAH[nCount].Audit_Operation_Type);
                }
                return nReturnValue;
            }
        }

        public ISingleResult<Audit_History> GetAuditDetails([Parameter(Name = "FromDate", DbType = "DateTime")] System.Nullable<System.DateTime> fromDate, [Parameter(Name = "ToDate", DbType = "DateTime")] System.Nullable<System.DateTime> toDate, [Parameter(Name = "ModuleID", DbType = "VarChar(50)")] string moduleID)
        {
            using (AuditDataContext Adc = _createContext())
            {
                return Adc.GetAuditDetails(fromDate, toDate, moduleID);
            }
        }

        public rsp_GetInitialSettingsResult GetSettings()
        {
            using (AuditDataContext Adc = _createContext())
            {
                return Adc.GetInitialSettings().SingleOrDefault();
            }
        }

    }
}
