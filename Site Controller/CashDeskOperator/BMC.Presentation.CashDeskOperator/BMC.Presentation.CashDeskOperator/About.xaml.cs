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

namespace BMC.Presentation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class About : Window , IDisposable
    {
        #region Declarations
        private readonly oCommonUtilities _oCommonutilities = oCommonUtilities.CreateInstance();
        private System.Threading.Thread thread;

        #endregion Declarations

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public About()
        {
            this.InitializeComponent();
            // Insert code required on object creation below this point.
        }
        #endregion Constructor


        /// <summary>
        /// Load Event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ReportsBusinessObject objVersion = new ReportsBusinessObject();
            string sVersion, sName, sCopyRight, sDescription, sCompanyName, sProductName, sProductVersion;
            objVersion.GetVersion_SiteName(out sVersion, out sName);
            objVersion.GetSplashDetails(out sCopyRight, out sDescription, out sCompanyName, out sProductName, out sProductVersion);
            txtVersion.Text = sVersion;
            CopyRight.Text = "© " + DateTime.Now.Year + " Bally Technologies Inc. All Rights Reserved";
            CompanyName.Text = "Company Name : " + sCompanyName;
            ProductName.Text = "Product Name : " + sProductName;
            ProductVersion.Text = "Product Version : " + sProductVersion;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region IDisposable Members

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.CleanupWPFObjectTopControls((i) =>
                    {
                        this.Window.Loaded -= (this.Window_Loaded);
                        this.btnExit.Click -= (this.btnExit_Click);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> About objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~About()
        {
            Dispose(false);
        }

        #endregion
    }
}