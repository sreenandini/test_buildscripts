using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using System.IO.IsolatedStorage;
using BMC.CoreLib.Diagnostics;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using BMC.CoreLib.Configuration;
using BMC.CoreLib.Cryptography;
using System.Data.SqlClient;
using BMC.CoreLib.Registry;
using BMC.EnterpriseBusiness.Business;
using BMC.EnterpriseClient.Views;
using BMC.EnterpriseBusiness.Entities;
using System.Windows.Documents;
using Microsoft.Win32;
using BMC.Common.Utilities;

namespace BMC.EnterpriseClient
{
    [Serializable]
    public class AppSettings : DisposableObject
    {
        [NonSerialized]
        private static IsolatedStorageFile _storage = null;
        [NonSerialized]
        private const string FILE_NAME = "APPSETTINGS.XML";
        UserEntity _user = null;

        static AppSettings()
        {
            _storage = IsolatedStorageFile.GetUserStoreForAssembly();
        }

        public AppSettings()
        {
            int i = 0;
        }

        private static void PerformWork(FileAccess access, Action<Stream> doWork)
        {
            ModuleProc PROC = new ModuleProc("AppSettings", "PerformSchemaWork");
            Stream st = null;

            try
            {
                // load the schema details
                string fileName = FILE_NAME;
                string[] schemaFile = _storage.GetFileNames(fileName);
                if (schemaFile == null || schemaFile.Length == 0)
                {
                    access = FileAccess.Write;
                    st = new IsolatedStorageFileStream(fileName, FileMode.Create, access, _storage);
                }
                else
                {
                    FileMode mode = FileMode.Open;
                    if (access == FileAccess.Write) mode = FileMode.Create;
                    st = new IsolatedStorageFileStream(fileName, mode, access, _storage);
                }
                doWork(st);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (st != null)
                {
                    st.Dispose();
                }
            }
        }

        private static AppSettings Load()
        {
            ModuleProc PROC = new ModuleProc("AppSettings", "LoadSchemaDetails");

            if (_current != null) return _current;

            try
            {
                PerformWork(FileAccess.Read, (s) =>
                {
                    try
                    {
                        _current = Extensions.ReadXmlObject<AppSettings>(s) as AppSettings;
                    }
                    catch (Exception ex)
                    {
                        Log.Exception(PROC, ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return _current;
        }

        public void Save()
        {
            ModuleProc PROC = new ModuleProc("AppSettings", "LoadSchemaDetails");
            try
            {
                UserGroupBiz obj = new UserGroupBiz();
                int UserId = AppGlobals.Current.UserId;
                System.Nullable<bool> SuperUser = null;
                obj.UpdateSuperUser(UserId, ref SuperUser);
                AppGlobals.Current.SuperUser = Convert.ToBoolean(SuperUser);

            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            PerformWork(FileAccess.Write, (s) =>
            {
                try
                {
                    Extensions.WriteXmlObject<AppSettings>(this, s);
                }
                catch (Exception ex)
                {
                    Log.Exception(PROC, ex);
                }
            });
        }

        private static AppSettings _current = null;
        private static object _currentLock = new object();

        public static AppSettings Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_currentLock)
                    {
                        if (_current == null)
                        {
                            Load();
                            if (_current == null)
                            {
                                _current = new AppSettings();
                            }
                        }
                    }
                }
                return _current;
            }
        }

        [XmlElement("LastSavedUser")]
        public string LastSavedUser { get; set; }

        [XmlElement("HideToolbar")]
        public bool HideToolbar { get; set; }

        [XmlElement("HideStatusbar")]
        public bool HideStatusbar { get; set; }
    }

    public class AppConfigStore : ConfigStore
    {
        private ICryptoHelper _crypto = CryptoHelperFactory.Create(CryptoType.TripleDES);

        public AppConfigStore()
        {
            this.Reload();
        }

        public void Reload()
        {
            ConfigStoreManager.GetPropertyValues(this);
            this.SQLConnect = DatabaseHelper.GetConnectionString();
        }

        public string SQLConnect { get; set; }

        [ConfigAppSetting("Splash", typeof(bool))]
        public bool Splash { get; set; }

        public string SQLConnectDecrypted
        {
            get
            {
                if (this.SQLConnect.IsEmpty()) return string.Empty;
                return _crypto.Decrypt(this.SQLConnect);
            }
        }

        public bool IsValidSQLConnect()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(this.SQLConnectDecrypted);
                return (!builder.ConnectionString.IsEmpty());
            }
            catch { return false; }
        }

        public bool TestSQLConnect()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(this.SQLConnectDecrypted);
                using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT 1", con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch { return false; }
        }
    }
    public static class RdcHQ
    {
        private const string APP_NAME = "BMC Enterprise";

        static RdcHQ()
        {
            VBRegistry.CreateKeyIfNotExists(APP_NAME);
        }

        public static void CreateSubKeyIfNotExists(string key)
        {
            VBRegistry.CreateKeyIfNotExists(APP_NAME + "\\" + key);
        }

        public static IEnumerable<string> GetAllSettings(string section)
        {
            return VBRegistry.GetAllSettings(APP_NAME, section);
        }
        public static void SaveSetting(string section, string key, string setting)
        {
            VBRegistry.SaveSetting(APP_NAME, section, key, setting);
        }
        public static string GetSetting(string section, string key, object defaultValue)
        {
            CreateSubKeyIfNotExists(section);
            return VBRegistry.GetSetting(APP_NAME, section, key, defaultValue);
        }
    }
}
