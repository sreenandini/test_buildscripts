using BMC.CashDeskOperator.BusinessObjects;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Security;
using BMC.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using FreeForm;
using BMC.Business.CashDeskOperator;
using System.Diagnostics;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CGameCapping.xaml
    /// </summary>
    public partial class GameCapping : UserControl
    {
        #region Local Declaration
        
        #region Objects

        GameCappingBO _oGameCapping = new GameCappingBO();
        List<GameCapDetails> _lstGameCapDetails = null;
        
        System.Threading.Thread thRefresh = null;
        
        ManualResetEvent mseResetRefresh = new ManualResetEvent(false);
        ManualResetEvent isRefreshing = new ManualResetEvent(false);

        RleaseGameCap _RleaseGameCap = new RleaseGameCap();
        #endregion Objects

        #region Variables
        int iProcCount = 0;
        #endregion Variables
        
        #endregion Local Declaration
        
        #region Constructor
        public GameCapping()
        {
            InitializeComponent();
            thRefresh = new Thread(new ThreadStart(UpdateTime));
            this.Subscribe();
            //thRefresh.Start();
        }
        #endregion Constructor

        private void Subscribe()
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Closing += MainWindow_Closing;
            }
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            this.Unsubscribe();
        }

        private void Unsubscribe()
        {
            _RleaseGameCap.Shutdown();
            _RleaseGameCap.PauseRelease.Set();
            _RleaseGameCap.StopRelease.Set();
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Closing -= MainWindow_Closing;
            }
        }

        #region GameCapEvents

        /// <summary>
        /// Load the list view with game capped session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("Game Cap Detail :Loading Game Capping Detail UI", LogManager.enumLogLevel.Debug);
                RefreshGameCappingList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        /// <summary>
        /// Exits from game capping screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Unsubscribe();
            //To Stop Timer thread
            mseResetRefresh.Set();
            _RleaseGameCap.StopRelease.Set();
            _RleaseGameCap.PauseRelease.Set();
        }

        /// <summary>
        /// Refresh the list view with latest game cap session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshGameCappingList();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        /// <summary>
        /// Sends release command for selected session in list view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            btnRefresh.Visibility = Visibility.Hidden;
            btnReleaseAll.Visibility = Visibility.Hidden;
            pgStatus.Visibility = Visibility.Visible;

            btn.IsEnabled = false;

            GameCapDetails GCD = (GameCapDetails)btn.DataContext;

            GCD.Status = "Processing";

            iProcCount++;

            Action<GameCapDetails> ReleaseCap = new Action<GameCapDetails>(ReleaseGameCap);
            ReleaseCap.BeginInvoke(GCD, null, null);
        }
        
        /// <summary>
        /// Sends release command for all session in list view 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReleaseAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnRefresh.Visibility = Visibility.Hidden;
                btnReleaseAll.Visibility = Visibility.Hidden;
                pgStatus.Visibility = Visibility.Visible;

                Action ReleaseCap = new Action(ReleaseAllGameCap);
                ReleaseCap.BeginInvoke(null, null);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        /// <summary>
        /// Selects all game cap session for release
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void chk_CheckAllDetails_Checked(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        SelectAllUnExportedDetails(chk_CheckAllDetails.IsChecked.Value);
        //    }
        //    catch (Exception Ex)
        //    {
        //        ExceptionManager.Publish(Ex);
        //    }
        //}

        #endregion GameCapEvents

        #region User Methods

        /// <summary>
        /// Refresh list view with latest game cap sessoin
        /// </summary>
        private void RefreshGameCappingList()
        {
            try
            {                
                //isRefreshing.Reset(); //To hold time refresher in list view

                _lstGameCapDetails = _oGameCapping.GetGameCapDetails();

                lv_GameCappingList.ItemsSource = _lstGameCapDetails;

                if (lv_GameCappingList.Items.Count > 0)
                    btnReleaseAll.Visibility = Visibility.Visible;

                //isRefreshing.Set(); //To release time refresher in list view
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        /// <summary>
        /// Update the time stamp for Elaspsed & Expiry column in list view for each session
        /// </summary>
        private void UpdateTime()
        {
            while (true)
            {   
                //Waits for 5 secs. If window is switched exits from thread.
                if (mseResetRefresh.WaitOne(new TimeSpan(0, 0, 5)))
                    break;

                //Waits for listview to refresh
                isRefreshing.WaitOne();

                try
                {
                    if (_lstGameCapDetails != null)                        
                        foreach (var item in _lstGameCapDetails)
                        {
                            //Not to refresh session for which release command is send
                            if (item.IsEnabled)
                            {
                                item.ElapsedSec = item.ElapsedSec + 5;

                                //Exipry time is udpated only if alert message is recieved from GMU for game session
                                if (item.AlertCame)
                                {
                                    if (item.AlertUnCapSec - 5 >= 0)
                                        item.AlertUnCapSec = item.AlertUnCapSec - 5;
                                    else
                                        item.AlertUnCapSec = 0;
                                }
                            }
                        }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }
        
        /// <summary>
        /// Sends release command for all session in list view
        /// </summary>
        private void ReleaseAllGameCap()
        {
            try
            {
                _lstGameCapDetails.ForEach((x) => { x.IsEnabled = false; });
                
                iProcCount = _lstGameCapDetails.Count;

                foreach (GameCapDetails GCD in _lstGameCapDetails.Where(item=> item.Status == string.Empty).ToList())
                {
                    try
                    {
                        GCD.Status = "Processing";

                        Application.Current.Dispatcher.Invoke(new Action(() => { lv_GameCappingList.ScrollIntoView(GCD); lv_GameCappingList.SelectedItem = GCD; }));

                        ReleaseGameCap(GCD);
                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                        GCD.IsEnabled = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                _lstGameCapDetails.ForEach((x) => { if(x.Status == "Processing") x.IsEnabled = true; });
            }
        }

        /*private void ReleaseAllGameCap()
        {
            try
            {
                _RleaseGameCap.PauseRelease.Reset();
                
                iProcCount = _lstGameCapDetails.Where(item => item.IsEnabled == true).Count();
                
                _RleaseGameCap.GameCapRelease = _lstGameCapDetails.Where(item => item.IsEnabled == true).ToList();

                _RleaseGameCap.PauseRelease.Set();

                //while (iProcCount != _RleaseGameCap.GameCapRelease.Where(item => item.Status == "SEND").Count()) ;

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    btnRefresh.Visibility = Visibility.Visible;
                    pgStatus.Visibility = Visibility.Hidden;
                }));

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);

            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oGCD"></param>
        private void ReleaseGameCap(object oGCD)
        {            
            GameCapDetails GCD = oGCD as GameCapDetails;

            try
            {
                GameCapResult oGameCapResult = _oGameCapping.ValidateGameCapDetails(GCD.InstallationNo);

                if (oGameCapResult != null)
                {
                    if (oGameCapResult.Message == "Success")
                    {
                        //EnableMachine _EnableMachine = new EnableMachine(GCD);
                        _RleaseGameCap.PauseRelease.Reset();
                        _RleaseGameCap.GameCapRelease.Add(GCD);
                        _RleaseGameCap.PauseRelease.Set();
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                GCD.Status = "Failed";
                GCD.IsEnabled = true;
            }
            finally
            {
                if (--iProcCount == 0)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        btnRefresh.Visibility = Visibility.Visible;                        
                        pgStatus.Visibility = Visibility.Hidden;
                    }));
                }
            }
        }

        //void SelectAllUnExportedDetails(bool IsSelected)
        //{
        //    if (_lstGameCapDetails != null)
        //    {
        //        foreach (var item in _lstGameCapDetails)
        //        {
        //            item.IsEnabled = IsSelected;
        //        }
        //    }
        //}
        
        #endregion User Methods        
    }
}
