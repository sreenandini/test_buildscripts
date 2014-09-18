using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient; 
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms ;
using Microsoft.Win32;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CConfiguration.xaml
    /// </summary>
    public partial class CConfiguration : Window
    {
        string sReturnString;
        string sConnStr;
        public string sSQLConnStr;
        public bool isconnectionOK;
        DialogResult iMsgBoxResult;
        string RegConStr;

        public CConfiguration()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            bool bConTest;
            sConnStr = MakeConnectionString ();

            bConTest = TestConnection(sConnStr);

            if (bConTest == true)
            {
                MessageBox.showBox("Test Connection Successfull");
            }
            else
            {
                MessageBox.showBox("Test Connection Failed");
            }
        }
        public string MakeConnectionString()
        {
            string sReturnString;
            int lentxtinstance=txtInstance.MaxLength ;

            sReturnString = "SERVER=" + txtServer.Text;
            if (lentxtinstance > 0)
            {
                sReturnString = sReturnString + "\"" + txtInstance.Text; 
            }
            sReturnString = sReturnString + ";UID=" + txtUserName.Text;
            sReturnString = sReturnString + ";PWD=" + txtPassword.Password ;
            sReturnString = sReturnString + ";DATABASE=" + txtDBName.Text;
            sReturnString = sReturnString + ";Connection Timeout=" + txtConnectionTimeout.Text;

            return sReturnString;
        }
        public bool TestConnection(string SConnectionstring)
        {
            SqlConnection sConnection = new SqlConnection(SConnectionstring );
            bool returnflag=false ;
            if (sConnection.ConnectionString != null)
            {
                try
                {
                    sConnection.Open();
                    //MessageBox.showBox("connection opened");
                    sConnection.Close();
                    returnflag = true;
                }
                catch (Exception ex)
                {
                    MessageBox.showBox(ex.ToString ());
                }
            }
            else
            {
                //MessageBox.showBox("Connection Closed");
                returnflag = false;
            }
            return returnflag;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 bool bRegUpdated;
                bool bRegUpdatedWebPath;
                
                sSQLConnStr = "";

                sSQLConnStr = MakeConnectionString();

                if (sSQLConnStr.Length == 0)
                {
                    MessageBox.showBox("Please enter the server details");
                    return;
                }
                else
                {
                    isconnectionOK = TestConnection(sSQLConnStr);
                    iMsgBoxResult = (DialogResult) 1;
                    if (isconnectionOK == false)
                    {
                      //iMsgBoxResult=(DialogResult) MessageBox.showBox("Unable to connect to DB. Would you like to save these settings?", MessageBoxButton.OKCancel);
                    }

                    if (iMsgBoxResult == 0)
                    {
                        return;
                    }
                }

                bRegUpdated = SetRegSetting(sSQLConnStr);
                if (bRegUpdated == true)
                {
                    MessageBox.showBox("Settings for ExchangeDB successfully set in registry");
                    this.Close();
                }
                else
                {
                    MessageBox.showBox("Settings for ExchangeDB Not set in registry");
                }
            }
            catch (Exception ex)
            {

                MessageBox.showBox(ex.Message);
            }
           
        }
        public bool SetRegSetting(string subkey)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(subkey))
                {
                    return false;
                }
                BMCRegistryHelper.SetRegKeyValue("Cashmaster", "SQLConnectCDO", RegistryValueKind.String, subkey);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            
            return true;
           
        }
    }
}
