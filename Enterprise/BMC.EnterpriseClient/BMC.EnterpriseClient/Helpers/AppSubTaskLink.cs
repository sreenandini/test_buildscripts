using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.CoreLib;
using BMC.Common.ExceptionManagement;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BMC.CoreLib.Diagnostics;
using BMC.Common.Interfaces;

namespace BMC.EnterpriseClient.Helpers
{
    public enum ToolLinkType
    {
        InlineForm = 0,
        ExternalExe = 1,
        CustomAction = 2,
        ExternalExeEmbedded = 3
    }

    public delegate void TaskAddedHandler(AppSubTaskLink link);
    public delegate void ExternalProcessOpenedHandler(AppSubTaskLink link);
    public delegate void ExternalProcessClosedHandler(AppSubTaskLink link);

    public class AppSubTaskLinks : Dictionary<object, AppSubTaskLink> { }

    public class AppSubTaskLink : MarshalByRefObject
    {
        private ProcessActivator _activator = null;

        private AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName)
        {
            this.SourceItem = sourceItem;
            this.AccessName = accessName;
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, Func<object, Form> createForm)
            : this(sourceItem, accessName)
        {
            this.CreateForm = createForm;
            this.LinkType = ToolLinkType.InlineForm;
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, string externalFilePath)
            : this(sourceItem, accessName)
        {
            if (!Path.IsPathRooted(externalFilePath))
                externalFilePath = Path.Combine(Extensions.GetStartupDirectory(), externalFilePath);

            this.LinkType = ToolLinkType.ExternalExe;
            if (this.SourceItem is ToolStripButton)
            {
                ((ToolStripButton)this.SourceItem).CheckOnClick = false;
            }
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, string externalFilePath, string externalFileParams)
            : this(sourceItem, accessName, externalFilePath)
        {
            _activator = new ProcessActivator(ProcessActivatorType.External, externalFilePath, externalFileParams);
            this.InitProcessActivator();
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, string externalFilePath, Func<string[]> externalFileParamsFunc)
            : this(sourceItem, accessName, externalFilePath)
        {
            _activator = new ProcessActivator(ProcessActivatorType.External, externalFilePath, externalFileParamsFunc);
            this.InitProcessActivator();
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, string externalFilePath, Func<string[]> externalFileParamsFunc, bool ignoreSpace)
            : this(sourceItem, accessName, externalFilePath)
        {
            _activator = new ProcessActivator(ProcessActivatorType.External, externalFilePath, externalFileParamsFunc)
             {
                 IgnoreSpace = ignoreSpace
             };
            this.InitProcessActivator();
        }

        public AppSubTaskLink(object sourceItem, Func<AppGlobals, bool> accessName, Action customAction)
            : this(sourceItem, accessName)
        {
            this.CustomAction = customAction;
            this.LinkType = ToolLinkType.CustomAction;
        }

        private void InitProcessActivator()
        {
            if (!Path.IsPathRooted(_activator.ExternalFilePath))
                _activator.ExternalFilePath = Path.Combine(Extensions.GetStartupDirectory(), _activator.ExternalFilePath);
            _activator.ProcessStarted += new ProcessStartedHandler(this.OnProcess_Started);
            _activator.ProcessExited += new EventHandler(this.OnProcess_Exited);
            _activator.Initialize();
            _linkType = _activator.ActivatorType == ProcessActivatorType.Embedded ? ToolLinkType.ExternalExeEmbedded : ToolLinkType.ExternalExe;
        }

        public object SourceItem { get; private set; }
        public Func<AppGlobals, bool> AccessName { get; private set; }

        public Func<object, Form> CreateForm { get; private set; }
        public Form Instance { get; set; }
        internal DataGridViewRow InstanceRow { get; set; }

        public event ExternalProcessOpenedHandler ExternalProcessOpened = null;
        public event ExternalProcessClosedHandler ExternalProcessClosed = null;

        public Action CustomAction { get; private set; }
        public object InlineFormTag { get; set; }

        public ProcessActivator Activator
        {
            get { return _activator; }
            set { _activator = value; }
        }

        public bool IsExternalFileExists
        {
            get
            {
                return _activator.IsExternalFileExists;
            }
        }

        private ToolLinkType _linkType = ToolLinkType.InlineForm;

        public ToolLinkType LinkType
        {
            get { return _linkType; }
            set
            {
                _linkType = value;
            }
        }

        internal void ActiveExternalProcess()
        {
            _activator.Activate();
        }

        private void OnProcess_Started(ProcessActivator activator)
        {
            if (this.SourceItem is ToolStripButton)
            {
                ((ToolStripButton)this.SourceItem).CheckState = CheckState.Checked;
            }
            else if (this.SourceItem is Button)
            {
                //((Button)this.SourceItem).Enabled = false;
            }

            if (this.ExternalProcessOpened != null)
            {
                this.ExternalProcessOpened(this);
            }
            else
            {
                if (activator.Metadata != null &&
                    activator.Metadata.NonMdiClient)
                {
                    this.ShowChildForm(activator);
                }
            }
        }

        public void ShowChildForm(ProcessActivator activator)
        {
            Form frmChild = activator.EmbedForm;

            // gets the last opened form
            Form ownerForm = null;
            if (Application.OpenForms != null &&
                Application.OpenForms.Count > 0)
            {
                try {
                	ownerForm = Application.OpenForms[Application.OpenForms.Count - 1];
				} catch { }
            }

            frmChild.ShowInTaskbar = false;
            frmChild.Load += new EventHandler(OnChild_Load);
            frmChild.FormClosing += new FormClosingEventHandler(OnChild_FormClosing);
            frmChild.StartPosition = FormStartPosition.CenterScreen;
            bool isDialog = false;

            if (activator.Metadata != null)
            {
                if (activator.Metadata.Model)
                    isDialog = true;
            }

            if (isDialog)
            {
                if (ownerForm != null)
                    frmChild.ShowDialog(ownerForm);
                else
                    frmChild.ShowDialog();
            }
            else
            {
                frmChild.Show();
            }
        }

        void OnChild_Load(object sender, EventArgs e)
        {

        }

        void OnChild_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        void OnProcess_Exited(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem toolStripItem = this.SourceItem as ToolStripItem;
                if (toolStripItem != null)
                {
                    if (toolStripItem.Owner.InvokeRequired)
                    {
                        toolStripItem.Owner.Invoke(new Action(() =>
                        {
                            this.OnProcess_Exited(sender, e);
                        }));
                    }
                    else
                    {
                        this.OnExternalProcessClosed();
                        if (toolStripItem is ToolStripButton)
                        {
                            ((ToolStripButton)this.SourceItem).CheckState = CheckState.Unchecked;
                        }
                    }
                }
                else if (this.SourceItem is Button)
                {
                    Button buttonItem = this.SourceItem as Button;
                    if (buttonItem.InvokeRequired)
                    {
                        buttonItem.Invoke(new Action(() =>
                        {
                            this.OnProcess_Exited(sender, e);
                        }));
                    }
                    else
                    {
                        this.OnExternalProcessClosed();
                        //((Button)this.SourceItem).Enabled = true;
                    }
                }
                else
                {
                    this.OnExternalProcessClosed();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void OnExternalProcessClosed()
        {
            if (this.ExternalProcessClosed != null)
            {
                this.ExternalProcessClosed(this);
            }
        }

        internal void CloseExternalProcess()
        {
            try
            {
                _activator.Kill();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void Close()
        {
            this.Close(false);
        }

        public void Close(bool suppressConfirmMessageBox)
        {
            try
            {
                if (this.LinkType == ToolLinkType.InlineForm &&
                            this.Instance != null)
                {
                    if (suppressConfirmMessageBox)
                    {
                        IBMCExtendedForm exForm = this.Instance as IBMCExtendedForm;
                        if (exForm != null)
                        {
                            ((IBMCExtendedForm)this.Instance).SuppressConfirmMessageBox = true;
                        }
                    }

                    this.Instance.Close();
                    if (this.Instance != null &&
                        this.Instance.IsDisposed)
                    {
                        this.Instance = null;
                    }
                }
                else if (this.LinkType == ToolLinkType.ExternalExe ||
                        this.LinkType == ToolLinkType.ExternalExeEmbedded)
                {
                    this.CloseExternalProcess();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public override string ToString()
        {
            if (this.SourceItem != null)
            {
                ToolStripItem toolItem = this.SourceItem as ToolStripItem;
                if (toolItem != null)
                {
                    return toolItem.Name;
                }
            }

            return base.ToString();
        }
    }
}
