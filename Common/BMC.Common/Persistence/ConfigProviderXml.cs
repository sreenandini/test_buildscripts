#define CFG_MUTEX
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;

namespace BMC.Common.Persistence
{
    internal class ConfigProviderXml : ConfigProviderBase
    {
        //private IsolatedStorageFile _storage = null;
        private string _directory = string.Empty;
        private string _fileName = string.Empty;
        private bool _isSavedByMe = false;
        private FileModificationWatcher _fileWathcer = null;

        private static XmlReaderSettings _xrSettings = null;
        private static XmlWriterSettings _xwSettings = null;

        private const string X_E_ROOT = "Configuration";
        private const string X_E_SECTION = "Section";
        private const string X_E_KEYVALUE = "KeyValue";

        private const string X_A_ID = "id";
        private const string X_A_KEY = "key";
        private const string X_A_VALUE = "value";

        private XName XN_ROOT = XName.Get(X_E_ROOT);
        private XName XN_SECTION = XName.Get(X_E_SECTION);
        private XName XN_KEYVALUE = XName.Get(X_E_KEYVALUE);

        private XName XN_ID = XName.Get(X_A_ID);
        private XName XN_KEY = XName.Get(X_A_KEY);
        private XName XN_VALUE = XName.Get(X_A_VALUE);

        private DateTime _fileModifiedTime = DateTime.MinValue;
        private readonly string FILE_NAME = string.Empty;
        private bool _isLoading = false;

#if CFG_MUTEX
        private Mutex _mutexFile = null;
        private string _mutexName = string.Empty;
#endif

        private class XmlSectionKeyValue
        {
            public string Section { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }

        }

        static ConfigProviderXml()
        {
            _xrSettings = new XmlReaderSettings()
            {
                CloseInput = true,
            };
            _xwSettings = new XmlWriterSettings()
            {
                CloseOutput = true,
                Indent = true
            };
        }

        internal ConfigProviderXml(string fileName)
        {
            //_storage = IsolatedStorageFile.GetMachineStoreForAssembly();
            _directory = Environment.GetEnvironmentVariable("BMCConfigPath", EnvironmentVariableTarget.Machine);
            FILE_NAME = "BMCApp.xml";
            _fileName = Path.Combine(_directory, FILE_NAME);
            _fileWathcer = new FileModificationWatcher(_fileName);
            _fileWathcer.FileModified += OnFileWathcer_FileModified;
            _fileWathcer.StartMonitoring();
#if CFG_MUTEX
            _mutexName = "Mutex_" + _mutexName;
            this.InitMutex();
#endif
        }

        private void InitMutex()
        {
#if CFG_MUTEX
            bool isNew = false;
            _mutexFile = new Mutex(false, _mutexName, out isNew);
            if (!isNew)
            {
                _mutexFile.Dispose();
                _mutexFile = Mutex.OpenExisting(_mutexName);
            }
#endif
        }

        private void AcquireMutex()
        {
#if CFG_MUTEX
            _mutexFile.WaitOne();
#endif
        }

        private void ReleaseMutex()
        {
#if CFG_MUTEX
            _mutexFile.ReleaseMutex();
#endif
        }

        public override void LoadInternal()
        {
            try
            {
                _isLoading = true;
                this.AcquireMutex();

                this.PerformWork(FileMode.Open, FileAccess.Read, ReadFromFile);
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
            finally
            {
                this.ReleaseMutex();
                if (_isSavedByMe) _isSavedByMe = false;
                if (_isLoading) _isLoading = false;
                _fileModifiedTime = this.GetFileModifiedTime();
            }
        }

        private void ReadFromFile(Stream st)
        {
            try
            {
                if (st.Length > X_E_ROOT.Length)
                {
#if NET4
                        XElement xRoot = XElement.Load(st);
#else
                    System.Xml.XmlReader xrw = System.Xml.XmlReader.Create(st, _xrSettings);
                    XElement xRoot = XElement.Load(xrw);
#endif

                    if (xRoot != null && xRoot.Name == XN_ROOT)
                    {
                        var allValues = (from x in xRoot.Elements(XN_SECTION)
                                         from y in x.Elements(XN_KEYVALUE)
                                         select new XmlSectionKeyValue()
                                         {
                                             Section = x.Attribute(XN_ID).Value,
                                             Key = y.Attribute(XN_KEY).Value,
                                             Value = y.Attribute(XN_VALUE).Value
                                         });
                        foreach (var allValue in allValues)
                        {
                            try
                            {
                                this.SetValue(allValue.Section, allValue.Key, allValue.Value, false);
                            }
                            catch (Exception ex)
                            {
                                EventLogExceptionAdapter.WriteException(ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }

        private DateTime GetFileModifiedTime()
        {
            DateTime fileModifiedTime = DateTime.MinValue;
            try
            {
                fileModifiedTime = File.GetLastWriteTime(_fileName);
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
            return fileModifiedTime;
        }

        protected override void ReloadIfModified()
        {
            if (_isLoading) return;
            try
            {
                // file modified time CR# 193726
                DateTime fileModifiedTimeNew = this.GetFileModifiedTime();
                bool isModified = (_fileModifiedTime.Ticks != fileModifiedTimeNew.Ticks);
                if (isModified)
                {
                    LogManager.WriteLog(string.Format("{0} has been modified outside of this application at [{1}].", FILE_NAME, fileModifiedTimeNew.ToString("dd/MM/yyyy HH:mm:ss.fff")),
                           LogManager.enumLogLevel.Warning);
                    _fileModifiedTime = fileModifiedTimeNew;
                    this.Load();
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }

        public override void Save()
        {
            try
            {

                //Drive  AvailableFreeSpace <= 102400 [100Mb] Not update configration xml
                DriveInfo DrivesDetails = new DriveInfo(Path.GetPathRoot(System.Environment.GetEnvironmentVariable("WINDIR")).ToString());
                if (DrivesDetails.AvailableFreeSpace <= 102400)
                {
                    LogManager.WriteLog("Unable to update configration xml due to low disk space.", LogManager.enumLogLevel.Info);
                    return;
                }
                _isSavedByMe = true;
                this.AcquireMutex();

                this.PerformWork(FileMode.Create, FileAccess.Write, WriteToFile);
            }
            catch (Exception ex)
            {
                
                EventLogExceptionAdapter.WriteException(ex);
            }
            finally
            {
                this.ReleaseMutex();
            }
        }

        private void WriteToFile(Stream st)
        {
            try
            {
                XElement xRoot = new XElement(XN_ROOT);
                foreach (var pair1 in _storeValues)
                {
                    XElement xSection = new XElement(XN_SECTION);
                    XAttribute xId = new XAttribute(XN_ID, pair1.Key);
                    xSection.Add(xId);
                    xRoot.Add(xSection);

                    foreach (var pair2 in pair1.Value)
                    {
                        XElement xKeyValue = new XElement(XN_KEYVALUE);
                        XAttribute xKey = new XAttribute(XN_KEY, pair2.Key);
                        xKeyValue.Add(xKey);
                        XAttribute xValue = new XAttribute(XN_VALUE, pair2.Value);
                        xKeyValue.Add(xValue);
                        xSection.Add(xKeyValue);
                    }
                }

#if NET4
                    xRoot.Save(st);
#else
                System.Xml.XmlWriter xrw = System.Xml.XmlWriter.Create(st, _xwSettings);
                xRoot.Save(st);
#endif
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
        }
        
        private void PerformWork(FileMode mode, FileAccess access, Action<Stream> doWork)
        {
            Stream st = null;

            try
            {
                if (string.IsNullOrWhiteSpace(_directory)) throw new Exception("BMCConfigPath not set");

                if (!File.Exists(_fileName))
                {
                    access = FileAccess.Write;
                    st = new FileStream(_fileName, FileMode.Create, access, FileShare.Read);
                }
                else
                {
                    int count = 0;
                    do
                    {
                        try
                        {
                            st = new FileStream(_fileName, mode, access, FileShare.Read);
                            break;
                        }
                        catch (System.IO.IOException)
                        {
                            count++;
                            System.Threading.Thread.Sleep(100);
                        }
                    } while (count < 10);
                }
                if (st != null)
                    doWork(st);
                else
                {
                    throw new Exception("Unable to open the file : " + _fileName);
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
            finally
            {
                if (st != null)
                {
                    st.Dispose();
                }
            }
        }

        internal override void InitializeToDefaultValues(IConfigKeyValuePair keyValue, string sectionName, string keyName)
        {
            base.InitializeToDefaultValues(keyValue, sectionName, keyName);
            if (_storeValues.ContainsKey(sectionName))
            {
                var section = _storeValues[sectionName];
                if (section.ContainsKey(keyName))
                {
                    var keyValue2 = section[keyName];
                    keyValue2.SetObjectValue(keyValue.GetDefaultObjectValue());
                }
            }
        }

        void OnFileWathcer_FileModified(FileModificationWatcher watcher)
        {
            try
            {
                if (!_isSavedByMe)
                {
                    this.Load();
                }
            }
            catch (Exception ex)
            {
                EventLogExceptionAdapter.WriteException(ex);
            }
            finally
            {
                if (_isSavedByMe) _isSavedByMe = false;
            }
        }
    }
}
