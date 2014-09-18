using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Transport;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BMC.Common.LogManagement;
using BMC.CashDeskOperator.BusinessObjects;
using System.Data;
using BMC.Presentation.POS.Helper_classes;

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Initialize a new instance of CVaultDeclaration
    /// </summary>
    public partial class CVaultDeclaration : UserControl
    {
        

        #region Constructor

        public CVaultDeclaration()
        {
            InitializeComponent();
            
        }
        
        #endregion

        #region LoadMethods
        /// <summary>
        /// Load vault drop details from table[tVault_Drops]
        /// </summary>
        private void LoadVaultDropDetails()
        {
            try
            {
                LogManager.WriteLog("CVaultDeclaration->LoadVaultDropDetails()", LogManager.enumLogLevel.Debug);
                List<GetUndeclaredVaultDrops> lst_vdrops = Vault.CreateInstance().GetUndeclaredDrops(false);
                if (lst_vdrops != null)
                {
                    this.DataContext = lst_vdrops;
                }
               
                if (lst_vdrops != null && lst_vdrops.Count > 0 && lst_vdrops[0].Drop_ID>0)
                {
                    lstVaultDrop.ItemsSource = lst_vdrops;

                }
                else
                {
                   
                    lstVaultDrop.ItemsSource = null;
                    btnDeclare.Visibility = Visibility.Hidden;
                    btnPrint.Visibility = Visibility.Hidden;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }
        #endregion

        #region Events

        private void btnDeclare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CVaultDeclaration->btnDeclare_Click()", LogManager.enumLogLevel.Debug);
                CVaultCashEntry v_entry = new CVaultCashEntry();
                v_entry.Owner = Window.GetWindow(this);
                v_entry.ShowDialog();
                LoadVaultDropDetails();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //btnDeclare.Visibility = Settings.CentralizedVaultDeclaration ? Visibility.Hidden : Visibility.Visible;

                if (!Settings.CentralizedVaultDeclaration && Security.SecurityHelper.HasAccess("BMC.Presentation.CFillVault.btnVaultDeclaration"))
                {
                    btnDeclare.Visibility = Visibility.Visible;
                }
                else
                {
                    btnDeclare.Visibility = Visibility.Hidden;
                }
                LogManager.WriteLog("CVaultDeclaration->UserControl_Loaded()", LogManager.enumLogLevel.Debug);
                LoadVaultDropDetails();

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        } 
      

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("CVaultDeclaration->btnPrint_Click()", LogManager.enumLogLevel.Debug);
                CreateOutstandingVaultdropReport();
            }
            catch(Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        #endregion

        private void CreateOutstandingVaultdropReport()
        {
            try
            {               
                using (CReportViewer cReportViewer = new CReportViewer())
                {
                    LogManager.WriteLog("Report data fetched successfully from database", LogManager.enumLogLevel.Info);
                    cReportViewer.ShowOutstandingVaultReport(false);
                    cReportViewer.SetOwner(Window.GetWindow(this));
                    cReportViewer.Show();
                }
                LogManager.WriteLog("Show Undeclared Drop Report Successfull", LogManager.enumLogLevel.Info);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
       
    }
}
