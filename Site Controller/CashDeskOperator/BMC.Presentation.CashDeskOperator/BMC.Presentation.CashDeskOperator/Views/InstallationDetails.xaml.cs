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
using BMC.CashDeskOperator;
using System.Data.Linq;
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using BMC.Common.LogManagement;
using BMC.Presentation.POS.Helper_classes;
using HC = BMC.Presentation.Helper_classes;


namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for InstallationDetails.xaml
    /// </summary>
    public partial class InstallationDetails : UserControl
    {
        string Pos;
        public InstallationDetails()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DisplayAll();

            try
            {
                if (Settings.StackerFeature == false)
                {
                    lstInstallationDetails.Columns[lstInstallationDetails.Columns.Count -1].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in InstallationDetails -> UserControl_Loaded", LogManager.enumLogLevel.Error);   
            }
        }
        private void DisplayAll()
        {
            try
            {
                InstallationDataContext InstallationDC = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                List<GetInstallationDetailsForCDOResult> _lstInstallationDetails = InstallationDC.GetInstallationDetailsForCDO().ToList();
                lstInstallationDetails.DataContext = _lstInstallationDetails;

                if (_lstInstallationDetails.Count > 0)
                {
                    lstInstallationDetails.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception in InstallationDetails, DisplayAll() : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void lstInstallationDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    lstPaytable.DataContext = null;
                    var k = (e.AddedItems[0] as GetInstallationDetailsForCDOResult);
                    Pos = k.Bar_Pos_Name;
                    InstallationDataContext InstallationDC = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                    List<GetGameDetailsForPosResult> _lstInstallationGames = new List<GetGameDetailsForPosResult>();
                    _lstInstallationGames =  InstallationDC.GetGameDetailsForPos(Pos).ToList();
                    lstInstallationGames.DataContext = _lstInstallationGames;

                    if (_lstInstallationGames.Count > 0)
                    {
                        lstInstallationGames.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception in InstallationDetails, lstInstallationDetails_SelectionChanged() : " + ex.Message, LogManager.enumLogLevel.Error);
            }
        }

        private void lstInstallationGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                {
                    var k = (e.AddedItems[0] as GetGameDetailsForPosResult);
                    int? gameTitleID = k.Game_Title_Id;
                    InstallationDataContext InstallationDC = new InstallationDataContext(oCommonUtilities.CreateInstance().GetConnectionString());
                    lstPaytable.DataContext = InstallationDC.GetPaytableDetailsForGame(gameTitleID, Pos);

                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Exception in InstallationDetails, lstInstallationGames_SelectionChanged() : " + ex.Message, LogManager.enumLogLevel.Error);
            }
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);
                        this.lstInstallationDetails.SelectionChanged -= (this.lstInstallationDetails_SelectionChanged);
                        this.lstInstallationGames.SelectionChanged -= (this.lstInstallationGames_SelectionChanged);
                    },
                    (c) =>
                    {
                    });
                    LogManager.WriteLog("|=> InstallationDetails objects are released successfully.", LogManager.enumLogLevel.Info);
                }
                disposed = true;
            }
        }

        ~InstallationDetails()
        {
            Dispose(false);
        }

        #endregion
    }
}
