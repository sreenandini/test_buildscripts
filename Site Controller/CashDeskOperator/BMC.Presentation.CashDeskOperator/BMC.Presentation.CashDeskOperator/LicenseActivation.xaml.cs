using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Presentation.POS.Views;
using BMC.Security;
using BMC.Transport;
using System.Data;
using Microsoft.Win32;
using Audit.BusinessClasses;
using Audit.Transport;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Utilities;
using BMC.Common;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for SiteSetup.xaml
    /// </summary>
    public partial class LicenseActivation : Window  
    {
        #region DataMember

        private static LicenseActivation instance = null;
        private string strKeyText = string.Empty;
        private bool bLaunchedFromLoginScreen = false;
        private SiteLicensingConfiguration oSiteLicensingConfiguration = SiteLicensingConfiguration.SiteLicensingConfigurationInstance;

        #endregion //DataMember

        #region Constructor

        public LicenseActivation()
        {
            InitializeComponent();
            txtLicenseKey.Focus();
            bLaunchedFromLoginScreen = false;
        }

        public LicenseActivation(string sMessageString)
        {
            InitializeComponent();
            this.lblMessage.Text = sMessageString;
            txtLicenseKey.Focus();
            bLaunchedFromLoginScreen = true;
        }

        public static LicenseActivation LicenseActivationInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LicenseActivation();
                }
                return instance;
            }
        }

        #endregion

         #region Events

        private void btnActivate_Click(object sender, RoutedEventArgs e)
         {
             try
             {
                 if (txtLicenseKey.Text.Trim().Length <= 0)
                 {
                     LogManager.WriteLog("License key is empty", LogManager.enumLogLevel.Error);
                     MessageBox.ShowBox("MessageID453", BMC_Icon.Error); //License Key should not be empty
                     txtLicenseKey.Focus();
                     return;
                 }
                 
                 int iLKeyResult = 0;
                 statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID496") as string;//Verifying with Enterprise server...
                 btnActivate.IsEnabled = false;
                 btnClose.IsEnabled = false;

                 LogManager.WriteLog("Check whether License Key is valid.", LogManager.enumLogLevel.Info);
                 try
                 {
                     Cursor = System.Windows.Input.Cursors.Wait;
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID496") as string;//Verifying with Enterprise server...
                     statusbaritempbStatus.Value = 40;
                     Audit("Trying to activate the new License");
                     iLKeyResult = oSiteLicensingConfiguration.CheckSiteLicenseKey(txtLicenseKey.Text.Trim(), Settings.SiteCode.Trim(), SecurityHelper.CurrentUser.UserName);
                 }
                 catch (Exception exError)
                 {
                     iLKeyResult = -1;
                     ExceptionManager.Publish(exError);
                 }

                 ValidateLicenseKey(iLKeyResult,true);
             }
             catch (Exception exError)
             {
                 ExceptionManager.Publish(exError);
             }
             finally
             {
                 Cursor = System.Windows.Input.Cursors.Arrow;
                 statusbaritempbStatus.Value = 0;
                 btnActivate.IsEnabled = true;
                 btnClose.IsEnabled = true;
             }
         }

         private void Window_Loaded(object sender, RoutedEventArgs e)
         {
             ClearContents();
         }

         private void btnExit_Click(object sender, RoutedEventArgs e)
         {
             CloseScreen();
         }

         private void btnClose_Click(object sender, RoutedEventArgs e)
         {
             CloseScreen();
         }

         private void txtLicenseKey_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
         {
             if (!BMC.Transport.Settings.OnScreenKeyboard)
                 return;
             txtLicenseKey.Text = DisplayKeyboard(txtLicenseKey.Text, string.Empty);
             txtLicenseKey.SelectAll();
         }

         #region Keyboard events

         void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
         {
             if (((KeyboardInterface)sender).DialogResult == true)
             {
                 strKeyText = ((KeyboardInterface)sender).KeyString;
             }
         }

         private string DisplayKeyboard(string KeyText, string type)
         {
             strKeyText = "";
             KeyboardInterface objKeyboard = new KeyboardInterface();
             if (type == "Pwd")
             {
                 objKeyboard.IsPwd = true;
             }
             objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
             objKeyboard.KeyString = KeyText;
             Point locationFromScreen = this.PointToScreen(new Point(0, 0));
             PresentationSource source = PresentationSource.FromVisual(this);
             System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
             objKeyboard.Top = targetPoints.Y + LicenseActivation.LicenseActivationInstance.Height / 2;
             objKeyboard.Left = targetPoints.X;
             objKeyboard.Owner = GetWindow(this);
             objKeyboard.ShowDialogEx(this);
             return strKeyText;
         }

         #endregion

         #endregion //Events

         #region Methods

         private void Audit(string sDesc)
         {
             AuditViewerBusiness.InsertAuditData(new Audit.Transport.Audit_History
             {
                 AuditModuleName = ModuleName.SiteLicensing,
                 Audit_Screen_Name = "LicenseActivation",
                 Audit_Desc = sDesc,
                 AuditOperationType = OperationType.ADD
             });
         }

         private void ClearContents()
         {
             txtLicenseKey.Text = string.Empty;
             statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID495") as string;//Ready
             statusbaritempbStatus.Value = 0;
         }

         private void CloseScreen()
         {
             //System.Windows.Forms.DialogResult dr;
             //dr = MessageBox.ShowBox("MessageID461", BMC_Icon.Question, BMC_Button.YesNo); //Do you want to close the license activation ?

             //if (dr.ToString() == "No")
             //{
             //    return;
             //}
             //else
             //{
                 CloseOrShutdown();
             //}
         }

         private void CloseOrShutdown()
         {
             //if (bLaunchedFromLoginScreen)
             //{
             //    Login objLogin = new Login();
             //    this.Close();
             //    objLogin.Show();
             //}
             //else
             //{
             //    this.DialogResult = false;
             //    this.Close();
             //}
             this.Close();
         }

         private void ActivateLicense()
         {
             int iLKeyResult;
             try
                 {
                     Cursor = System.Windows.Input.Cursors.Wait;
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID492") as string;//Verifying with Exchange server...
                     statusbaritempbStatus.Value = 40;
                     iLKeyResult = oSiteLicensingConfiguration.CheckLicenseKey(txtLicenseKey.Text.Trim(), SecurityHelper.CurrentUser.UserName);
                 }
                 catch (Exception exError)
                 {
                     iLKeyResult = -1;
                     ExceptionManager.Publish(exError);
                 }
                 this. ValidateLicenseKey(iLKeyResult,false);
             }

         private void ValidateLicenseKey(int iLKeyResult, bool isEnterprise)
         {
             switch (iLKeyResult)
             {
                 case -1:
                     if (isEnterprise)
                     {
                         ActivateLicense();
                     }
                     else
                     {
                         statusbaritempbStatus.Value = 0;
                         MessageBox.ShowBox("MessageID454", BMC_Icon.Error); //License key verification has been failed. Please try again!
                         statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                         Audit("License key verification has been failed.");
                     }
                     break;
                 case 0:
                     statusbaritempbStatus.Value = 0;
                     MessageBox.ShowBox("MessageID455", BMC_Icon.Error); //License key is not valid!
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                     Audit("License key is not valid!");
                     break;
                 case 1:
                     statusbaritempbStatus.Value = 80;
                     oSiteLicensingConfiguration.UpdateLicenseStaus(txtLicenseKey.Text.Trim(), Convert.ToInt32(SiteLicensing.LicenseKeyStatus.Active), clsSecurity.UserID);
                     MessageBox.ShowBox("MessageID456", BMC_Icon.Information); //License key registered successfully.
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID494") as string;//Success
                     Audit("License key registered successfully.");
                     statusbaritempbStatus.Value = 100;
                     CloseOrShutdown();
                     break;
                 case -2:
                     statusbaritempbStatus.Value = 0;
                     MessageBox.ShowBox("MessageID457", BMC_Icon.Error); //There is no License available for this site
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                     Audit("There is no License available for this site");
                     break;
                 case -3:
                     if (isEnterprise)
                     {
                         ActivateLicense();
                     }
                     else
                     {
                         statusbaritempbStatus.Value = 0;
                         MessageBox.ShowBox("MessageID458", BMC_Icon.Error); //License has been already activated.
                         statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                         Audit("License has been already activated.");
                     }
                     break;

                 case -4:
                     statusbaritempbStatus.Value = 0;
                     MessageBox.ShowBox("MessageID459", BMC_Icon.Error); //License has been expired.
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                     Audit("License has been expired.");
                     break;
                 case -5:
                     statusbaritempbStatus.Value = 0;
                     MessageBox.ShowBox("MessageID460", BMC_Icon.Error); //License has been cancelled.
                     statusbaritemlblStatus.Text = Application.Current.FindResource("MessageID493") as string;//Failed
                     Audit("This License has been cancelled.");
                     break;
             }
         }
         

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
                         this.txtLicenseKey.PreviewMouseUp -= (this.txtLicenseKey_PreviewMouseUp);
                         this.btnActivate.Click -= (this.btnActivate_Click);
                         this.btnClose.Click -= (this.btnClose_Click);
                         this.btnExit.Click -= (this.btnExit_Click);
                     },
                     (c) =>
                     {
                     });
                     this.WriteLog("License Activation objects are released successfully.");

                 }
                 disposed = true;
             }
         }

         /// <summary>
         /// Releases unmanaged resources and performs other cleanup operations before the
         /// <see cref="SiteSetup"/> is reclaimed by garbage collection.
         /// </summary>
         ~LicenseActivation()
         {
             Dispose(false);
         }

         #endregion

         #endregion //Methods
    }
}
