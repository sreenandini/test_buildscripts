using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.CoreLib.Concurrent;
using BMC.CoreLib.Win32;
using System.Threading;
using BMC.CoreLib;
using BMC.Common;
namespace BMC.EnterpriseClient.Views
{
    public partial class AboutForm : Form
    {
        private bool _isSplash = false;

        public AboutForm(AboutFormTypes formType)
        {
            this.FormType = formType;
            _isSplash = (formType == AboutFormTypes.Splash);
            InitializeComponent();
            setTagProperty();
            btnOK.Visible = (formType == AboutFormTypes.About);
            lblStatus.Visible = _isSplash;
            this.ShowInTaskbar = _isSplash;
            this.FormBorderStyle = FormBorderStyle.None;
            if (_isSplash)
            {
                this.Text = Extensions.AppTitle;
            }
        }

        private void setTagProperty()
        {
            this.btnOK.Tag = "Key_OKCaption";            
            this.lblStatus.Tag = "Key_LoadingDot";

            // Copyright set in LoadProductVersion()
            // this.lblCopyright.Tag = "Key_Copyrights";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public AboutFormTypes FormType { get; private set; }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            
            try
            {
                this.ResolveResources();
                if (this.FormType == AboutFormTypes.About)
                {
                    this.LoadProductVersion();
                }
                else
                {
                    this.LoadSplashDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void LoadSplashDetails()
        {
            AppGlobals gbl = AppGlobals.Current;

            try
            {
                new Future(() =>
                {
                    bool configResult = true;

                    if (gbl.Config.SQLConnectDecrypted.IsEmpty())
                    {
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_SPLASH_NO_CONN_STRING"));
                        configResult = this.ShowConnectionConfigForm();
                    }
                    else
                    {
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_SPLASH_CONNET_TO_DB"));
                        Thread.Sleep(500);

                        bool connStatus = gbl.Config.TestSQLConnect();
                        this.SetStatus(connStatus ? this.GetResourceTextByKey(1,"MSG_SPLASH_CONNET_TO_DB_SUCCESS") : this.GetResourceTextByKey(1,"MSG_SPLASH_CONNET_TO_DB_FAILURE"));
                        Thread.Sleep(500);

                        if (!connStatus)
                        {
                            DialogResult dlgResult = DialogResult.No;
                            this.CrossThreadInvoke(new Action(() =>
                            {
                                dlgResult = this.ShowQuestionMessageBox(this.GetResourceTextByKey(1,"MSG_SPLASH_CONNET_CONN_CONFIG"),this.Text);
                            }));
                            if (dlgResult == DialogResult.No)
                            {
                                configResult = false;
                            }
                            else
                            {
                                configResult = this.ShowConnectionConfigForm();
                            }
                        }
                    }
                    Thread.Sleep(500);

                    AppEntryPoint.Current.IsSplashCanceled = !configResult;
                    if (configResult)
                    {
                        Thread.Sleep(500);
                        this.CrossThreadInvoke(new Action(() =>
                        {
                            this.LoadProductVersion();
                        }));
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_SPLASH_LOAD1"));
                        Thread.Sleep(500);
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_SPLASH_LOAD2"));
                        Thread.Sleep(500);
                        this.SetStatus(this.GetResourceTextByKey(1,"MSG_SPLASH_LOAD3"));
                        Thread.Sleep(500);
                    }
                    this.CloseThis();
                });
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void CloseThis()
        {
            this.CrossThreadInvoke(new Action(() =>
            {
                this.Close();
            }));
        }

        private bool ShowConnectionConfigForm()
        {
            bool result = false;
            try
            {
                this.CrossThreadInvoke(new Action(() =>
                {
                    BMC.CoreLib.Win32.Win32Extensions.ShowDialogExResultAndDestroy<ConnectionConfigForm>(new ConnectionConfigForm(), this,
                           null,
                           (f) =>
                           {

                           });
                }));
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                AppGlobals.Current.Config.Reload();
                result = AppGlobals.Current.Config.TestSQLConnect();
            }
            return result;
        }

        private void SetStatus(string message)
        {
            this.CrossThreadInvoke(new Action(() =>
                    {
                        lblStatus.Text = message;
                    }));
        }

        private void LoadProductVersion()
        {
            try
            {
                ProductVersionEntity entity = AppGlobals.Current.BusinessAdmin.GetProductVersion();
                lblAppName.Text = this.GetResourceTextByKey("Key_ProductBally");
                lblVersion.Text = entity.Version;
                lblCompany.Text = this.GetResourceTextByKey("Key_CompanyBally");
                lblCopyright.Text = this.GetResourceTextByKey("Key_CopyrightSymbol") + " " + DateTime.Now.Year + " " + lblCompany.Text + ". " + this.GetResourceTextByKey("Key_Copyrights");
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
    }
}
