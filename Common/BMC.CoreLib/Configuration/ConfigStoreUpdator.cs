using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib.Diagnostics;
using BMC.CoreLib;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Runtime.Remoting;
using BMC.CoreLib.Win32;
using System.Threading;
using System.IO.Pipes;

namespace BMC.CoreLib.Configuration
{
    public partial class ConfigStoreUpdator : Form
    {
        private AppDomain _domain = null;
        private ConfigStore _store = null;
        private Assembly[] _assemblies = null;
        private IList<ComboItem2> _implementations = null;

        public ConfigStoreUpdator()
        {
            InitializeComponent();
            _implementations = new List<ComboItem2>();
            cboImplementations.DisplayMember = "Text";
            cboImplementations.ValueMember = "Value";

            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {

        }

        private class ComboItem
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public Process Process { get; set; }
            public BMC.CoreLib.WMI.Win32.Process Process2 { get; private set; }
            public string MainWindowTitle { get; set; }
            public string DisplayName
            {
                get
                {
                    if (this.MainWindowTitle.IsEmpty())
                        return string.Format("{0} ({1:D})", this.Name, this.Id);
                    else
                        return string.Format("{0} ({1:D}) [{2}]", this.Name, this.Id, this.MainWindowTitle);
                }
            }

            public void SetProcessInfo(BMC.CoreLib.WMI.Win32.Process process2)
            {
                this.Process2 = process2;
                string exePath = process2.ExecutablePath;

                try
                {
                    this.AsmName = AssemblyName.GetAssemblyName(exePath);
                    this.DomainSetup = new AppDomainSetup()
                    {
                        ApplicationBase = Path.GetDirectoryName(exePath),
                        ApplicationName = Path.GetFileName(exePath),
                        ConfigurationFile = exePath + ".config",
                        ShadowCopyFiles = "true"
                    };
                }
                catch { }
            }

            public AppDomainSetup DomainSetup { get; private set; }
            public AssemblyName AsmName { get; private set; }
        }

        private class ComboItem2
        {
            public string Text { get; set; }
            public Type Value { get; set; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadProcesses();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!e.Shift && !e.Control && !e.Alt && e.KeyCode == Keys.F5)
            {
                this.LoadProcesses();
            }
        }

        private void LoadProcesses()
        {
            ModuleProc PROC = new ModuleProc("", "LoadProcesses");
            lvwProcesses.Items.Clear();
            int pid = Process.GetCurrentProcess().Id;

            try
            {
                var processes = (from p in Process.GetProcesses()
                                 where p.Id != pid
                                 orderby p.ProcessName
                                 select p);
                var processes2 = BMC.CoreLib.WMI.Win32.Process.GetInstances().OfType<BMC.CoreLib.WMI.Win32.Process>();
                int index = 1;
                foreach (Process process in processes)
                {
                    ComboItem item = new ComboItem()
                    {
                        Id = process.Id,
                        Name = process.ProcessName,
                        Process = process,
                        MainWindowTitle = process.MainWindowTitle
                    };
                    var process2 = (from p in processes2
                                    where p.ProcessId == process.Id
                                    select p).FirstOrDefault();
                    if (process2 != null)
                    {
                        string path = process2.ExecutablePath;
                        item.SetProcessInfo(process2);
                        item.Name = process2.Caption;
                    }

                    ListViewItem item2 = new ListViewItem(index.ToString());
                    item2.SubItems.Add(item.DisplayName);
                    item2.Tag = item;
                    lvwProcesses.Items.Add(item2);
                    index++;
                    Thread.Sleep(1);
                }

                lvwProcesses.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lvwProcesses.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void LoadProcess()
        {
            ModuleProc PROC = new ModuleProc("", "Method");
            propData.SelectedObject = null;
            cboImplementations.DataSource = null;

            try
            {
                if (lvwProcesses.SelectedItems.Count > 0)
                {
                    ComboItem item = lvwProcesses.SelectedItems[0].Tag as ComboItem;
                    if (item.AsmName == null)
                    {
                        this.ShowErrorMessageBox("Not a valid .NET Executable.");
                        return;
                    }

                    if (_domain != null)
                    {
                        AppDomain.Unload(_domain);
                        _domain = null;
                    }
                    if (_store != null)
                    {
                        //_store.Dispose();
                        _store = null;
                    }

                    AppDomain domain = AppDomain.CurrentDomain;
                    Assembly asm = Assembly.LoadFile(item.Process2.ExecutablePath);
                    if (asm != null)
                    {
                        Type[] stores = this.GetInstances(asm);
                        if (stores == null ||
                            stores.Length == 0)
                        {
                            stores = this.GetInstancesFromReferencedAssemblies(asm.GetReferencedAssemblies());
                        }
                        if (stores != null)
                        {
                            _implementations.Clear();
                            foreach (var store in stores)
                            {
                                _implementations.Add(new ComboItem2()
                                {
                                    Text = store.FullName,
                                    Value = store
                                });
                            }
                        }
                        cboImplementations.DisplayMember = "Text";
                        cboImplementations.ValueMember = "Value";
                        cboImplementations.DataSource = _implementations;
                    }
                }

                if (cboImplementations.DataSource != null)
                {
                    cboImplementations.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private Type[] GetInstances(Assembly asm)
        {
            ModuleProc PROC = new ModuleProc("", "Method");
            Type[] result = default(Type[]);
            Type typeOfT = typeof(IConfigStore);

            try
            {
                result = (from t in asm.GetTypes()
                          where typeOfT.IsAssignableFrom(t) == true &&
                                !t.IsAbstract && t.IsClass
                          select t).ToArray();
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private Type[] GetInstancesFromReferencedAssemblies(AssemblyName[] assemblies)
        {
            using (ILogMethod method = Log.LogMethod("", "GetInstancesFromReferencedAssemblies"))
            {
                Type[] result = default(Type[]);
                Assembly pThis = typeof(ConfigStore).Assembly;

                try
                {
                    foreach (var asmName in assemblies)
                    {
                        try
                        {
                            Assembly asm = Assembly.Load(asmName);
                            if (asm.GetName().Name.IgnoreCaseCompare("BMC.CoreLib")) continue;

                            result = this.GetInstances(asm);
                            if (result != null && result.Length > 0) return result;
                        }
                        catch (Exception ex)
                        {
                            method.Exception(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return result;
            }
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            ModuleProc PROC = new ModuleProc("", "Method");
            Assembly result = null;

            try
            {
                if (lvwProcesses.SelectedItems.Count > 0)
                {
                    AssemblyName name = new AssemblyName(args.Name);
                    ComboItem item = lvwProcesses.SelectedItems[0].Tag as ComboItem;
                    if (args.Name.IgnoreCaseCompare(item.AsmName.FullName))
                    {
                        string asmPath = item.Process2.ExecutablePath;
                        result = Assembly.LoadFile(asmPath);
                    }
                    else
                    {
                        string asmPath = Path.Combine(Path.GetDirectoryName(item.Process2.ExecutablePath), name.Name + ".dll");
                        result = Assembly.LoadFile(asmPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }

        private void lvwProcesses_DoubleClick(object sender, EventArgs e)
        {
            this.LoadProcess();
        }

        private void lvwProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            propData.SelectedObject = null;
            _implementations.Clear();
            cboImplementations.DataSource = null;
        }

        private void cboImplementations_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                if (cboImplementations.SelectedIndex != -1)
                {
                    if (lvwProcesses.SelectedItems.Count > 0)
                    {
                        ComboItem item = lvwProcesses.SelectedItems[0].Tag as ComboItem;
                        ComboItem2 configStoreImpl = cboImplementations.SelectedItem as ComboItem2;
                        //_store = _domain.CreateInstanceAndUnwrap(item.AsmName.FullName, configStoreImpl) as ConfigStore;
                        _store = Activator.CreateInstance(configStoreImpl.Value, true) as ConfigStore;
                        ConfigStoreManager.PullValues(_store);
                        propData.SelectedObject = _store;
                        btnSave.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ModuleProc PROC = new ModuleProc("", "Method");

            try
            {
                if (lvwProcesses.SelectedItems.Count > 0)
                {
                    ComboItem item = lvwProcesses.SelectedItems[0].Tag as ComboItem;

                    using (ConfigStoreServiceProxy proxy = new ConfigStoreServiceProxy(item.Process.Id))
                    {
                        proxy.KnownTypes = new Type[] { _store.GetType() };

                        ConfigStore store = _store;// new ConfigStore();
                        ConfigStoreRequest req = new ConfigStoreRequest()
                        {
                            Request = store
                        };
                        ConfigStoreResponse resp = proxy.SendMessage(req);

                        if (resp == null ||
                            proxy.LastExceptionDetail != null ||
                            resp.Response == false)
                        {
                            this.ShowErrorMessageBox("Unable to send the details.");
                            if (proxy.LastExceptionDetail != null)
                            {
                                Log.Info(PROC, proxy.LastExceptionDetail.Message);
                            }
                        }
                        else
                        {
                            this.ShowInfoMessageBox("Details are sent successfully.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
        }
    }
}
