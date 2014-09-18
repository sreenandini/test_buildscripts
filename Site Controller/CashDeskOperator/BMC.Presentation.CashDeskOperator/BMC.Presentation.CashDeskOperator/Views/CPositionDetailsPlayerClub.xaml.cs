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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.CashDeskOperator;
using IconImage = System.Drawing.Icon;
using System.Drawing;
using System.IO;
using System.Windows.Threading;
using BMC.Transport;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.ExceptionManagement;
using BMC.Business.CashDeskOperator;
using System.Data;
using System.Globalization;
using System.Configuration;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CPositionDetailsPlayerClub.xaml
    /// </summary>
    public partial class CPositionDetailsPlayerClub : UserControl, IDisposable
    {
        #region "Declaration"
        private int _InstallationNo = 0;
        public string image1 = "Resources/BallyIcon.ico";
        public DispatcherTimer Timer;
        int _ScreenRefreshTime = 10;
        #endregion
        public CPositionDetailsPlayerClub()
        {
            InitializeComponent();
        }

        public CPositionDetailsPlayerClub(int InstallationNo)
        {
            InitializeComponent();
            this._InstallationNo = InstallationNo;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _ScreenRefreshTime = Convert.ToInt32(ConfigurationManager.AppSettings["PlayerClubScreenRefreshTime"]);
            _ScreenRefreshTime = (_ScreenRefreshTime > 0 ? _ScreenRefreshTime : 10);
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.Zero;
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Start();
        }

        private void LoadPlayerDetailsFromCMP(int InstallationNo)
        {
            try
            {
                PlayerClubBiz objPlayerClub = new PlayerClubBiz();
                DataTable dtPlayerClub = new DataTable();
                dtPlayerClub = objPlayerClub.RetrievePlayerDetails_CMP(InstallationNo);
                if (dtPlayerClub.Rows.Count > 0)
                {
                    txtVIP.Text = dtPlayerClub.Rows[0]["VIPFlag"].ToString();
                    txtPlayerName.Text = dtPlayerClub.Rows[0]["FirstName"].ToString() + "," + dtPlayerClub.Rows[0]["LastName"].ToString();
                    txtAccountNumber.Text = dtPlayerClub.Rows[0]["CardNumber"].ToString();
                    txtTimeInPlay.Text = dtPlayerClub.Rows[0]["TimeInPlay"].ToString();
                    txtClubState.Text = dtPlayerClub.Rows[0]["ClubState"].ToString();
                    txtClubStatus.Text = dtPlayerClub.Rows[0]["ClubStatus"].ToString();
                    txtBirthday.Text = dtPlayerClub.Rows[0]["Birthday"].ToString();
                    txtHostName.Text = dtPlayerClub.Rows[0]["HostCode"].ToString();

                    //if Permission is to given to view the details in bonus screen the below will be displayed
                    if (Security.SecurityHelper.HasAccess("BMC.CPositionDetailsPlayerClub.PlayerClubBonus"))
                    {
                        txtBonusPoints.Text = dtPlayerClub.Rows[0]["BonusPoints"].ToString();
                        txtBonusMultiPlayer.Text = dtPlayerClub.Rows[0]["BonusMultiplier"].ToString();
                        txtBonusEffectDate.Text = dtPlayerClub.Rows[0]["BonusEffectDate"].ToString();
                        txtBonusEffectTime.Text = dtPlayerClub.Rows[0]["BonusEffectTime"].ToString();
                        txtBonusExpireTime.Text = dtPlayerClub.Rows[0]["BonusExpireTime"].ToString();
                    }
                    else
                    {
                        txtBonusPoints.Text = "-";
                        txtBonusMultiPlayer.Text = "-";
                        txtBonusEffectDate.Text = "-";
                        txtBonusEffectTime.Text = "-";
                        txtBonusExpireTime.Text = "-";
                        lblStatus.Content = "User : " + Security.SecurityHelper.CurrentUser.UserName + " is not authorized to view the details";
                    }
                }
                else
                {
                    txtVIP.Text = "-";
                    txtPlayerName.Text = "-";
                    txtAccountNumber.Text = "-";
                    txtClubState.Text = "-";
                    txtTimeInPlay.Text = "-";
                    txtClubStatus.Text = "-";
                    txtBirthday.Text = "-";
                    txtHostName.Text = "-";

                    if (Security.SecurityHelper.HasAccess("BMC.CPositionDetailsPlayerClub.PlayerClubBonus"))
                    {
                        txtBonusPoints.Text = "-";
                        txtBonusMultiPlayer.Text = "-";
                        txtBonusEffectDate.Text = "-";
                        txtBonusEffectTime.Text = "-";
                        txtBonusExpireTime.Text = "-";
                    }
                    else
                    {
						txtBonusPoints.Text = "-";
                        txtBonusMultiPlayer.Text = "-";
                        txtBonusEffectDate.Text = "-";
                        txtBonusEffectTime.Text = "-";
                        txtBonusExpireTime.Text = "-";
                        lblStatus.Content = string.Format(Application.Current.FindResource("CPositionDetailsPlayerClub_xaml_Status").ToString(), Security.SecurityHelper.CurrentUser.UserName.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (Settings.IsKioskRequired)
            {
                LoadPlayerDetails();
                Timer.Interval = new TimeSpan(0, 0, _ScreenRefreshTime);
            }
            else
            {
                LoadPlayerDetailsFromCMP(_InstallationNo);
                Timer.Interval = new TimeSpan(0, 0, _ScreenRefreshTime);
            }
        }

        private void LoadPlayerDetails()
        {
            IPlayerClub objCDO = PlayerClubBusinessObject.CreateInstance();
            Dictionary<string, string> _PlayerDetail = objCDO.RetrievePlayerInfoFromEPI(_InstallationNo);

            if (_PlayerDetail != null && _PlayerDetail.Count > 0)
            {
                if (Convert.ToBoolean(_PlayerDetail["IsEPIAvailable"]))
                {
                    txtAccountNumber.Text = _PlayerDetail["AccountNumber"];
                    txtPlayerName.Text = _PlayerDetail["DisplayName"];
                    txtClubState.Text = _PlayerDetail["ClubState"];
                    txtClubStatus.Text = _PlayerDetail["ClubStatus"];
                    txtTimeInPlay.Text = _PlayerDetail["PlayerTimeOfPlay"];
                }

                else
                {
                    txtAccountNumber.Text = "None";
                    txtPlayerName.Text = "None";
                    txtClubStatus.Text = "None";
                    txtTimeInPlay.Text = "None";
                    txtClubState.Text = "None";
                }
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
                        this.UserControl.Loaded -= (this.UserControl_Loaded);

                        if (this.Timer != null)
                        {
                            this.Timer.Tick -= new EventHandler(Timer_Tick);
                            this.Timer.Stop();
                        }
                    },
                    (c) =>
                    {
                    });
                    this.WriteLog("CPositionDetailsPlayerClub objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPositionDetailsPlayerClub"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPositionDetailsPlayerClub()
        {
            Dispose(false);
        }

        #endregion

        private void chkPlayerClub_Checked(object sender, RoutedEventArgs e)
        {
            EnableDisableControls();
        }

        private void chkPlayerClubBonus_Checked(object sender, RoutedEventArgs e)
        {
            EnableDisableControls();
        }

        private void EnableDisableControls()
        {
            if (chkPlayerClub.IsChecked == true)
            {
                spPlayerClub.Visibility = Visibility.Visible;
                spPlayerClubBonus.Visibility = Visibility.Collapsed;
            }
            else
            {
                spPlayerClub.Visibility = Visibility.Collapsed;
                spPlayerClubBonus.Visibility = Visibility.Visible;
            }
        }
    }
}
