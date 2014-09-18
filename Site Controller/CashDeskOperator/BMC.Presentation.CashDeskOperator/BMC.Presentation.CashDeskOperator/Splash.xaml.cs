using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BMC.Common.ExceptionManagement;
using BMC.CashDeskOperator.BusinessObjects;
using Microsoft.Win32;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.Utilities;

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class Splash : Window,  IDisposable
	{
        #region Declarations
        private readonly oCommonUtilities _oCommonutilities = oCommonUtilities.CreateInstance();
        private System.Threading.Thread thread;

        #endregion Declarations

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public Splash()
		{
            MessageBox.parentOwner = this;
			this.InitializeComponent();
			// Insert code required on object creation below this point.
		}
        #endregion Constructor

        #region Events

        /// <summary>
        /// Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportsBusinessObject objVersion = new ReportsBusinessObject();
            
            if (String.IsNullOrEmpty(oCommonUtilities.CreateInstance().GetConnectionString()))
            {
                MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                App.Current.Shutdown();
            }


            string sVersion, sName, sCopyRight, sDescription, sCompanyName, sProductName, sProductVersion;
            objVersion.GetVersion_SiteName(out sVersion, out sName);
            objVersion.GetSplashDetails(out sCopyRight, out sDescription, out sCompanyName, out sProductName, out sProductVersion);
            txtVersion.Text = sVersion;
            CopyRight.Text = "© " + DateTime.Now.Year + " Bally Technologies Inc. All Rights Reserved";
            CompanyName.Text = "Company Name : " + sCompanyName;
            ProductName.Text = "Product Name : " + sProductName;
            ProductVersion.Text =  "Product Version : " + sProductVersion;

            //Start a new thread for the Pre - Requisite Checks.
            if (thread == null)
            {
                thread = new System.Threading.Thread(
                    new System.Threading.ThreadStart(
                        delegate()
                        {
                            System.Windows.Threading.DispatcherOperation
                              dispatcherOp = txtStatus.Dispatcher.BeginInvoke(
                              System.Windows.Threading.DispatcherPriority.Normal,
                              new Action(
                                UpdateStatus
                            ));
                        }
      ));

                thread.Start();
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Method to check all the prerequisite conditions.
        /// </summary>
        private void UpdateStatus()
        {
            try
            {
                txtStatus.Text = "Connecting to Database.";                
                if (_oCommonutilities.SQLConnectionExists())
                {
                    txtStatus.Text = "Verifying Site Details.";
                    if (_oCommonutilities.GetSiteDetails() != null)
                    {
                        if (!new ReportsBusinessObject().IsResetOccuredAndCompleted())
                        {
                            MessageBox.ShowBox("MessageID549", BMC_Icon.Error);
                            App.Current.Shutdown();
                        }

                        if (_oCommonutilities.GetSiteDetails().Tables[0].Rows.Count > 0)
                            if (
                                _oCommonutilities.GetSiteDetails().Tables[0].Rows[0]["SiteStatus"].ToString().ToUpper() ==
                                "PARTIALLYCONFIGURED")
                                if (
                                    _oCommonutilities.GetSiteDetails().Tables[0].Rows[0]["SiteEvent"].ToString().ToUpper() ==
                                    "RECOVERY")
                                {
                                    txtStatus.Text = "Starting Site - Recovery Process.";
                                    new SiteCheckPoints("RECOVERY",
                                                        int.Parse(
                                                            _oCommonutilities.GetSiteDetails().Tables[0].Rows[0]["Code"].
                                                                ToString())).Show();
                                }
                                else
                                {
                                    txtStatus.Text = "Starting Site - New Configuration Process.";
                                    new SiteCheckPoints("NEW",
                                                        int.Parse(
                                                            _oCommonutilities.GetSiteDetails().Tables[0].Rows[0]["Code"].
                                                                ToString())).Show();
                                }
                            else
                            {
                                txtStatus.Text = "Site Verification Complete.";
                                new Login().Show();
                            }
                        else
                        {
                            try
                            {
                                BMC.Common.ConfigurationManagement.ConfigManager.SetConfigurationMode(BMC.Common.ConfigurationManagement.ConfigManager.ConfigurationMode.AppConfig);
                                if (BMCRegistryHelper.IsExchangeClient())
                                {
                                    txtStatus.Text = "Site Verification Complete.";
                                    new ClientMessage().Show();
                                }
                                else if (BMCRegistryHelper.IsExchangeServer())
                                {
                                    txtStatus.Text = "Site Verification Complete.";
                                    new SiteSetup().Show();
                                }

                                else
                                {
                                    txtStatus.Text = "Site Verification Complete.";
                                    new Login().Show();
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.Publish(ex);
                                MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                                App.Current.Shutdown();
                            }
                        }
                    }
                    else
                    {
                        txtStatus.Text = "Site Verification Complete.";
                        new Login().Show();
                    }
                }
                else
                {
                    txtStatus.Text = "Database Connection - Failed.";
                    MessageBox.ShowBox("MessageID201");
                    Application.Current.Shutdown();
                }
            }
            catch (System.Data.SqlClient.SqlException sqEx)
            {
                ExceptionManager.Publish(sqEx);
                MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                App.Current.Shutdown();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID1", BMC_Icon.Error);
                App.Current.Shutdown();
            }

            this.Close();
        }

        #endregion Methods

        #region IDisposable Members

        /// <summary>
        /// Variable used to identity whether this object is already disposed or not.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        // events
                        this.Window.Loaded -= (this.Window_Loaded);
                        this.txtStatus.Loaded -= (this.Window_Loaded);
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("Splash objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="Splash"/> is reclaimed by garbage collection.
        /// </summary>
        ~Splash()
        {
            Dispose(false);
        }

        #endregion
    }
}