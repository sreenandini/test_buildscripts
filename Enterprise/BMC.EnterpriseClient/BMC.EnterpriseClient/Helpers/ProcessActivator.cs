using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using BMC.Common.Interfaces;
using System.Diagnostics;
using BMC.Common.ExceptionManagement;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Remoting;

namespace BMC.EnterpriseClient.Helpers
{
    public enum ProcessActivatorType
    {
        External = 0,
        Embedded = 1
    }

    public delegate void ProcessStartedHandler(ProcessActivator activator);

    public class ProcessActivator : MarshalByRefObject
    {
        private Process _process = null;
        private AppDomain _ad = null;
        private Form _embedForm = null;
        private ProcessActivatorType _activatorType = ProcessActivatorType.External;
        AppInvokeEntryPointResult _epr = null;

        public ProcessActivatorType ActivatorType
        {
            get { return _activatorType; }
            set { _activatorType = value; }
        }

        private const UInt32 WM_QUIT = 0x0012;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        static ProcessActivator() { }

        public ProcessActivator(ProcessActivatorType activatorType)
        {
            _activatorType = activatorType;
        }

        public ProcessActivator(ProcessActivatorType activatorType, string externalFilePath, string externalFileParams)
            : this(activatorType)
        {
            this.ExternalFilePath = externalFilePath;
            this.ExternalFileParams = externalFileParams;
        }

        public ProcessActivator(ProcessActivatorType activatorType, string externalFilePath, Func<string[]> externalFileParamsFunc)
            : this(activatorType)
        {
            this.ExternalFilePath = externalFilePath;
            this.ExternalFileParamsFunc = externalFileParamsFunc;
        }

        public string ExternalFilePath { get; internal set; }
        public string ExternalFileParams { get; private set; }
        public bool IgnoreSpace { get; set; }
        public Func<string[]> ExternalFileParamsFunc { get; private set; }
        public bool PrefixExePath { get; set; }

        public Form EmbedForm
        {
            get { return _embedForm; }
            set { _embedForm = value; }
        }

        public bool IsExternalFileExists
        {
            get
            {
                return File.Exists(this.ExternalFilePath);
            }
        }

        public Process ExternalProcess { get { return _process; } }

        public event EventHandler ProcessExited;

        private void OnProcessExited(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "OnProcessExited");

            try
            {
                if (this.ProcessExited != null)
                {
                    this.ProcessExited(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public event ProcessStartedHandler ProcessStarted;

        private void OnProcessStarted()
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "OnProcessExited");

            try
            {
                if (this.ProcessStarted != null)
                {
                    this.ProcessStarted(this);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Initialize()
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "Initialize");

            try
            {
                var exes = EmbeddedExeMetadatas.Current.Exes;
                string fileName = Path.GetFileName(this.ExternalFilePath);
                string startupDir = Extensions.GetStartupDirectory();
                if (!startupDir.IsEmpty() && 
                    startupDir[startupDir.Length - 1] != '\\')
                {
                    startupDir += '\\';
                }
                string fileNameWithDir = this.ExternalFilePath.Replace(startupDir, "");                
                var found = (from e in exes
                             where e.Name.IgnoreCaseCompare(fileName) ||
                                    (e.Names != null && e.Names.Length > 1 && e.Name.IgnoreCaseCompare(fileNameWithDir))                                    
                             select e).FirstOrDefault();
                if (found != null)
                {
                    this.Metadata = found;
                    this.ActivatorType = found.ToolType;
                    this.PrefixExePath = found.PrefixExePath;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public EmbeddedExeMetadata Metadata { get; set; }

        public bool IsNonMdiClient
        {
            get
            {
                return (this.ActivatorType == ProcessActivatorType.Embedded &&
                    this.Metadata != null &&
                    this.Metadata.NonMdiClient);
            }
        }

        public void Activate()
        {
            switch (_activatorType)
            {
                case ProcessActivatorType.External:
                    this.ActiveExternalProcess();
                    break;

                case ProcessActivatorType.Embedded:
                    this.ActivateEmbeddedProcess();
                    break;

                default:
                    break;
            }
        }

        private void ActiveExternalProcess()
        {
            try
            {
                BMC.CoreLib.WMI.Win32.Process p = null;

                if (_process != null)
                {
                    p = (from s in BMC.CoreLib.WMI.Win32.Process.GetInstances().OfType<BMC.CoreLib.WMI.Win32.Process>()
                         where s.ExecutablePath.IgnoreCaseCompare(this.ExternalFilePath) &&
                                s.ProcessId == _process.Id
                         select s).FirstOrDefault();
                }

                if (p != null)
                {
                    _process = Process.GetProcessById((int)p.ProcessId);
                    int processId = _process.Id;
                    ProcessMutexHelper.ActivePresiousInstance(processId);
                }
                else
                {
                    _process = new Process();
                    string arguments = this.ExternalFileParams;
                    if (this.ExternalFileParamsFunc != null)
                    {
                        arguments = string.Join(this.IgnoreSpace ? "" : " ", this.ExternalFileParamsFunc());
                    }

                    _process.StartInfo = new ProcessStartInfo(this.ExternalFilePath, arguments)
                    {
                        UseShellExecute = true,
                    };
                    _process.EnableRaisingEvents = true;
                    _process.Exited += new EventHandler(this.OnProcessExited);
                    _process.Start();
                    _process.Refresh();
                    this.OnProcessStarted();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void ActivateEmbeddedProcess()
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "ActiveExternalProcessInEmbeddedMode");

            try
            {
                // if the form already loaded
                if (_embedForm != null)
                {
                    try
                    {
                        _embedForm.SuspendLayout();
                        _embedForm.Activate();
                    }
                    finally
                    {
                        _embedForm.ResumeLayout();
                    }
                }
                else
                {
                    // create the app domain
                    string fileName = Path.GetFileName(this.ExternalFilePath);
                    string configFile = this.ExternalFilePath + ".config";
                    AppDomainSetup ads = new AppDomainSetup()
                    {
                        ConfigurationFile = configFile,
                        ApplicationBase = Path.GetDirectoryName(this.ExternalFilePath)
                    };
                    _ad = AppDomain.CreateDomain(fileName, null, ads);
                    //_ad.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                    //// load the exe into the app domain
                    //AssemblyName name = AssemblyName.GetAssemblyName(this.ExternalFilePath);
                    //Assembly asm = _ad.Load(name);

                    // create the instance of app invoke entry point factory
                    Type factoryType = typeof(AppInvokeEntryPointFactory);
                    AppInvokeEntryPointFactory factory = _ad.CreateInstanceAndUnwrap(factoryType.Assembly.FullName, factoryType.FullName) as AppInvokeEntryPointFactory;
                    //IAppInvokeEntryPoint ep = factory.FindForm(_ad, this.ExternalFilePath, configFile);
                    AppInvokeEntryPointResult epr = factory.FindForm(_ad, this.ExternalFilePath, configFile);
                    IAppInvokeEntryPoint ep = null;
                    _epr = epr;

                    // now try to unwrap the assembly from the default app domain
                    try
                    {
                        ep = epr.Instance.Unwrap() as IAppInvokeEntryPoint;
                    }
                    catch (Exception ex) { }

                    // load the assemblies from the new domain
                    if (ep == null)
                    {
                        // first load from the primary assembly
                        try
                        {
                            if (epr.LoadedAssemblies.ContainsKey(epr.AssemblyName))
                            {
                                string asmPath = epr.LoadedAssemblies[epr.AssemblyName];
                                Assembly asmLoad = Assembly.LoadFrom(asmPath);
                                ep = asmLoad.CreateInstance(epr.TypeName) as IAppInvokeEntryPoint;
                            }
                        }
                        catch (Exception ex) { }

                        if (ep == null)
                        {
                            foreach (var asmPair in epr.LoadedAssemblies)
                            {
                                Assembly asmLoad = (from a in AppDomain.CurrentDomain.GetAssemblies()
                                                    where a.GetName().Name.IgnoreCaseCompare(asmPair.Key)
                                                    select a).FirstOrDefault();
                                if (asmLoad == null)
                                {
                                    try
                                    {
                                        asmLoad = Assembly.LoadFrom(asmPair.Value);
                                    }
                                    catch (Exception ex) { }
                                }

                                //try to load the type one more type
                                if (asmLoad != null &&
                                    ep == null)
                                {
                                    try
                                    {
                                        ep = epr.Instance.Unwrap() as IAppInvokeEntryPoint;
                                    }
                                    catch (Exception ex) { }
                                }

                                // loaded successfully
                                if (ep != null) break;
                            }
                        }
                    }

                    // invoke the entry point
                    if (ep != null)
                    {
                        string arguments = this.ExternalFileParams;
                        if (this.ExternalFileParamsFunc != null)
                        {
                            arguments = string.Join(this.IgnoreSpace ? "" : " ", this.ExternalFileParamsFunc());
                        }
                        List<string> argumentsArray = new List<string>(arguments.Split(new char[] { ' ' }));
                        if (this.PrefixExePath)
                        {
                            argumentsArray.Insert(0, this.ExternalFilePath);
                        }

                        //this.LoadExtraAssemblies(epr);
                        try
                        {
                            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                            ep.DisplayEntryForm(argumentsArray.ToArray(), (f) =>
                            {
                                _embedForm = f;
                                f.FormClosed += new FormClosedEventHandler(OnForm_Closed);
                            });
                        }
                        finally
                        {
                            AppDomain.CurrentDomain.AssemblyResolve -= (CurrentDomain_AssemblyResolve);
                        }
                        this.OnProcessStarted();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
                BMC.CoreLib.Win32.Win32Extensions.ShowErrorMessageBox(ex.Message);
            }
        }

        private void LoadExtraAssemblies(AppInvokeEntryPointResult epr)
        {
            ModuleProc PROC = new ModuleProc("Process Activator", "LoadExtraAssemblies");

            try
            {
                if (epr != null)
                {
                    Assembly[] adAssemblies = AppDomain.CurrentDomain.GetAssemblies();
                    var adNonMatchedAssemblies = (from a in epr.LoadedAssemblies.OfType<KeyValuePair<string, string>>()
                                                  join b in adAssemblies on a.Key equals b.FullName
                                                  into c
                                                  from d in c.DefaultIfEmpty()
                                                  where d == null
                                                  select a).ToArray();
                    if (adNonMatchedAssemblies.Length > 0)
                    {
                        foreach (var adNonMatchedAssembly in adNonMatchedAssemblies)
                        {
                            //load the non matched assembly
                            try
                            {
                                Assembly asm = Assembly.LoadFrom(adNonMatchedAssembly.Value);
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            ModuleProc PROC = new ModuleProc("", "OnNewDomain_AssemblyResolve");
            Assembly result = null;

            try
            {
                // check the assembly from the current app domain
                AssemblyName asmNameRef = new AssemblyName(args.Name);
                string asmName = asmNameRef.FullName;

                result = (from a in AppDomain.CurrentDomain.GetAssemblies()
                          where a.FullName.IgnoreCaseCompare(asmName)
                          select a).FirstOrDefault();
                if (result == null)
                {
                    // load the assembly from the newly created app domain
                    if (_epr != null &&
                        _epr.LoadedAssemblies.ContainsKey(asmName))
                    {
                        result = Assembly.LoadFrom(_epr.LoadedAssemblies[asmName]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        void OnForm_Closed(object sender, FormClosedEventArgs e)
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "OnForm_Closing");

            try
            {
                // close all opened forms from this assembly
                this.CloseOpenedForms(sender as Form);

                // unload the app domain
                AppDomain.Unload(_ad);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _ad = null;
                _embedForm = null;
                _epr = null;
                this.OnProcessExited(this, EventArgs.Empty);
            }
        }

        private void CloseOpenedForms(Form entryForm)
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "CloseOpenedForms");

            try
            {
                Assembly asmEntry = entryForm.GetType().Assembly;
                var forms = (from f in Application.OpenForms.OfType<Form>()
                             where f.GetType().Assembly == asmEntry
                             select f).ToArray();
                if (forms != null &&
                    forms.Length > 0)
                {
                    for (int i = forms.Length - 1; i >= 0; i--)
                    {
                        forms[i].Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public void Kill()
        {
            switch (_activatorType)
            {
                case ProcessActivatorType.External:
                    this.KillExternalProcess();
                    break;

                case ProcessActivatorType.Embedded:
                    this.KillEmbeddedProcess();
                    break;

                default:
                    break;
            }
        }

        private void KillExternalProcess()
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "KillExternalProcess");

            try
            {
                if (this.ExternalProcess != null)
                {
                    IntPtr mwh = this.ExternalProcess.MainWindowHandle;
                    if (mwh != IntPtr.Zero)
                    {
                        PostMessage(mwh, WM_QUIT, IntPtr.Zero, IntPtr.Zero);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                _process = null;
            }
        }

        private void KillEmbeddedProcess()
        {
            ModuleProc PROC = new ModuleProc("ProcessActivator", "KillEmbeddedProcess");

            try
            {
                if (_ad != null && _embedForm != null)
                {
                    _embedForm.Close();
                    if (_embedForm != null &&
                        _embedForm.IsDisposed)
                    {
                        _embedForm = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        public string UniqueKey
        {
            get
            {
                return this.ExternalFilePath;
            }
        }
    }
}
