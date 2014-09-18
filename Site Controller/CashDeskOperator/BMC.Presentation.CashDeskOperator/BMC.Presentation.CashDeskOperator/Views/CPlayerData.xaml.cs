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
using BMC.CashDeskOperator.BusinessObjects;
using BMC.Transport;
using System.Globalization;
using System.Reflection;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.Presentation.POS.Helper_classes;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using System.Threading;
using BMC.PlayerGateway.Gateway;
using System.Configuration;
using BMC.PlayerGateway.SDT.Messages;
using BMC.CoreLib.Concurrent;

namespace BMC.Presentation
{
    /// <summary>
    /// Interaction logic for CAFTSetting.xaml
    /// </summary>
    /// 
    delegate void UnLockUIControls();
    public partial class CPlayerData : UserControl, IDisposable
    {
        List<rsp_GetPTRatingsResult> oPlayerCardDetails;
        PlayerData oPlayerData = new PlayerData();
        DateTime StartDate = DateTime.Now;
        DateTime EndDate = DateTime.Now;
        ManualResetEvent _Reset = new ManualResetEvent(false);
        IPlayerGateway oGateWay;
        private string s_KeyText = string.Empty;

        public CPlayerData()
        {
            InitializeComponent();
            cmbMessageTypes.ItemsSource = GetMessageTypes();
            LogManager.WriteLog("CPlayerData[GateWay Initializing]", LogManager.enumLogLevel.Debug);
            try
            {
                BMC.PlayerGateway.SharedData.ActiveLogger.WriteToExternalLog += new BMC.PlayerGateway.WriteToExternalLogHandler(ActiveLogger_WriteToExternalLog);
                BMC.PlayerGateway.GatewaySettings.GlobalExecutorService = ExecutorServiceFactory.CreateExecutorService();
                oGateWay = GatewayFactory.GetGateway(GatewayType.SDT);
                BMC.PlayerGateway.GatewaySettings.ConnectionString = oCommonUtilities.CreateInstance().GetConnectionString();
                oGateWay.Initialize(true);
                oGateWay.SocketSenderPT.Initialize(Settings.PT_GATEWAY_IP, Settings.SDT_SendPTPortNo);
                oGateWay.SocketSenderCA.Initialize(Settings.PT_GATEWAY_IP, Settings.SDT_SendCAPortNo);                
                LogManager.WriteLog("CPlayerData[GateWay Initialized]", LogManager.enumLogLevel.Debug);
            }
            catch(Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        void ActiveLogger_WriteToExternalLog(string formattedMessage, BMC.PlayerGateway.Diagnostics.LogEntryType type, object extra)
        {
            LogManager.WriteLog(formattedMessage, LogManager.enumLogLevel.Info);  
        }

        private List<String>  GetMessageTypes()
        {
            try
            {
                return ConfigurationManager.AppSettings["Player_RatingMessages"].Split(',').ToList<String>();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);  
            }
            return new String[]{"All","PT77","PT66","PT65","PT10"}.ToList<string>() ;
        }


        private void btn_GetMessages_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (StartDate > EndDate)
                {
                    MessageBox.ShowBox("MessageID282", BMC_Icon.Information);
                    return;
                }

                if (StartDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID283", BMC_Icon.Information);
                    return;
                }

                if (EndDate > System.DateTime.Now)
                {
                    MessageBox.ShowBox("MessageID284", BMC_Icon.Information);
                    return;
                }


                if (dtpStartDate.SelectedDate != null && dtpEndDate.SelectedDate != null)
                {
                    StartDate = new DateTime(((DateTime)dtpStartDate.SelectedDate).Year,
                        ((DateTime)dtpStartDate.SelectedDate).Month,
                        ((DateTime)dtpStartDate.SelectedDate).Day,
                        tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute,
                        tmpStartTime.SelectedSecond);

                    EndDate = new DateTime(((DateTime)dtpEndDate.SelectedDate).Year,
                        ((DateTime)dtpEndDate.SelectedDate).Month,
                        ((DateTime)dtpEndDate.SelectedDate).Day,
                        dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute,
                        dtpEndtime.SelectedSecond);
                }
                //if (txtCardNUmber.Text.Trim() == string.Empty)
                //{
                //    MessageBox.ShowBox("MessageID393", BMC_Icon.Information);
                //    return;
                //}
                oPlayerCardDetails = oPlayerData.GetPlayerDataByCard(txtCardNUmber.Text, StartDate, EndDate, cmbMessageTypes.Text, !chk_FailedOnly.IsChecked.Value  );
                lvPlayerData.ItemsSource = oPlayerCardDetails;

                if (oPlayerCardDetails != null)
                {
                    if (oPlayerCardDetails.Count == 0)
                    {
                        MessageBox.ShowBox("MessageID394", BMC_Icon.Information);
                        return;
                    }
                }

            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }

        }
        private void dtpStartDate_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
                txtStartDate.Text = Convert.ToDateTime(StartDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {

                ExceptionManager.Publish(ex);
            }
        }
        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
                txtEndDate.Text = Convert.ToDateTime(EndDate).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
            }
            catch (StackOverflowException ex)
            {
                ExceptionManager.Publish(ex);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void dtpEndtime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dt.Year, dt.Month, dt.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void tmpStartTime_SelectedTimeChanged(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpStartDate.SelectedDate = DateTime.Now.Date;
                dtpEndDate.SelectedDate = DateTime.Now.Date;

                tmpStartTime.SelectedHour = DateTime.Parse(Settings.DailyAutoReadTime).Hour;
                tmpStartTime.SelectedMinute = DateTime.Parse(Settings.DailyAutoReadTime).Minute;
                tmpStartTime.SelectedSecond = DateTime.Parse(Settings.DailyAutoReadTime).Second;
                this.dtpStartDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.dtpStartDate_SelectedDateChanged);
                if (DateTime.Now < DateTime.Parse(string.Format("{0} {1}", DateTime.Now.ToString("MM/dd/yyyy"), Settings.DailyAutoReadTime)))
                {
                    if (ExtensionMethods.CurrentDateCulture == "en-US")
                        dtpStartDate.Text = DateTime.Now.Date.AddDays(-1).ToString("m", new CultureInfo(ExtensionMethods.CurrentDateCulture));
                    else
                        dtpStartDate.Text = DateTime.Now.Date.AddDays(-1).ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));

                }
                else
                {
                    if (ExtensionMethods.CurrentDateCulture == "en-US")
                        dtpStartDate.Text = DateTime.Now.Date.ToString("m", new CultureInfo(ExtensionMethods.CurrentDateCulture));
                    else
                        dtpStartDate.Text = DateTime.Now.Date.ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));
                }

                if (ExtensionMethods.CurrentDateCulture == "en-US")
                    dtpEndDate.Text = DateTime.Now.Date.ToString("m", new CultureInfo(ExtensionMethods.CurrentDateCulture));
                else
                    dtpEndDate.Text = DateTime.Now.Date.ToString("d", new CultureInfo(ExtensionMethods.CurrentDateCulture));

                DateTime dt = Convert.ToDateTime(dtpStartDate.SelectedDate);
                StartDate = new DateTime(dt.Year, dt.Month, dt.Day, tmpStartTime.SelectedHour, tmpStartTime.SelectedMinute, tmpStartTime.SelectedSecond);

                txtStartDate.Text = StartDate.Date.ToShortDateString();

                DateTime dtEnd = Convert.ToDateTime(dtpEndDate.SelectedDate);
                EndDate = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, dtpEndtime.SelectedHour, dtpEndtime.SelectedMinute, dtpEndtime.SelectedSecond);
                txtEndDate.Text = EndDate.Date.ToShortDateString();


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void btn_Send_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (oPlayerCardDetails != null)
                {
                    oPlayerCardDetails.ForEach(c => { c.UIProcess = "Process Initiated"; });
                    btn_GetMessages.IsEnabled = false;
                    btn_Send.IsEnabled = false;
                    ThreadStart tsProcessdata = new ThreadStart(ProcessData);
                    Thread trdProcessor = new Thread(tsProcessdata);
                    trdProcessor.Start();  
                    
                }
            }
            catch (Exception Ex)
            {

                ExceptionManager.Publish(Ex);
            }
            finally
            {
               
            }
        }

        private void txtCardNUmber_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            txtCardNUmber.Text = DisplayKeyboard(txtCardNUmber.Text);
            txtCardNUmber.SelectionStart = txtCardNUmber.Text.Length;
        }

        void objKeyboard_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((NumberPadWind)sender).DialogResult == true)
            {
                s_KeyText = ((NumberPadWind)sender).ValueText;
            }
        }

        private string DisplayKeyboard(string KeyText)
        {
            s_KeyText = "";
            BMC.Presentation.NumberPadWind objKeyboard = new NumberPadWind();
            objKeyboard.Closing += new System.ComponentModel.CancelEventHandler(objKeyboard_Closing);
            objKeyboard.ValueText = KeyText;
            objKeyboard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }

        void ProcessData()
        {
            PlayerData oPlayerData=new PlayerData();

            int iPDResendInterval;

            try
            {
                iPDResendInterval = int.Parse(ConfigurationManager.AppSettings["PDResendInterval"].ToString());
            }
            catch(Exception Ex)
            {
                LogManager.WriteLog("Error getting from config key[PDResendInterval] setting to default 50 milliseconds" + Ex.Message, LogManager.enumLogLevel.Error);
                iPDResendInterval=50;
            }

            try
            {
                foreach (rsp_GetPTRatingsResult PlayerRating in oPlayerCardDetails)
                {
                    if (!_Reset.WaitOne(iPDResendInterval))
                    {
                        try
                        {
                            LogManager.WriteLog("CPlayerData.ProcessData[Sending] " + PlayerRating.Sno.ToString(), LogManager.enumLogLevel.Debug);
                            BmcMessageBase message = BmcMessageFactory.CreateMessage(PlayerRating.RawData);

                            if(message == null) {
                                LogManager.WriteLog("CPlayerData.ProcessData:[Message Formatting] Invalid message stored.", LogManager.enumLogLevel.Info);
                                PlayerRating.UIProcess = "Failed";
                                PlayerRating.SendStatus = false;
                                break;
                            }

                            RawMessageResponse oResponse = oGateWay.SendRawMessage(new RawMessageRequest(message.ToRawData(), PlayerRating.Sno));
                            if (!oResponse.Succeeded)
                            {
                                LogManager.WriteLog("CPlayerData.ProcessData:[Error sending] " + PlayerRating.Sno.ToString(), LogManager.enumLogLevel.Debug);
                                PlayerRating.UIProcess = "Failed";
                                PlayerRating.SendStatus = false;
                                oPlayerData.UpdateRating_Status(PlayerRating.Sno, false);
                                break;
                            }
                            else
                            {
                                PlayerRating.UIProcess = "Sent";
                                PlayerRating.SendStatus = true;
                                oPlayerData.UpdateRating_Status(PlayerRating.Sno, true);
                            }
                        }
                        catch
                        {
                            LogManager.WriteLog("CPlayerData.ProcessData:[Error sending] " + PlayerRating.Sno.ToString(), LogManager.enumLogLevel.Debug);
                            PlayerRating.UIProcess = "Failed";
                            PlayerRating.SendStatus = false;
                            oPlayerData.UpdateRating_Status(PlayerRating.Sno, false);
                            throw;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
            finally
            {
                    Dispatcher.Invoke(new UnLockUIControls(UnLockUICOntrols), null);
            }
        }
        void UnLockUICOntrols()
        {

            btn_GetMessages.IsEnabled = true;
            btn_Send.IsEnabled = true;
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
                        
                    },
                    (c) =>
                    {
                    });

                    BMC.PlayerGateway.SharedData.ActiveLogger.WriteToExternalLog -= ActiveLogger_WriteToExternalLog;
                    BMC.PlayerGateway.GatewaySettings.GlobalExecutorService.Shutdown();
                    IDisposable gateway = oGateWay as IDisposable;
                    if (gateway != null)
                    {
                        gateway.Dispose();
                    }
                    BMC.PlayerGateway.GatewaySettings.GlobalExecutorService = null;
                    this.WriteLog("CPlayerData objects are released successfully.");

                }
                disposed = true;
            }
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CPlayerData"/> is reclaimed by garbage collection.
        /// </summary>
        ~CPlayerData()
        {
            Dispose(false);
        }

        #endregion
    }
}