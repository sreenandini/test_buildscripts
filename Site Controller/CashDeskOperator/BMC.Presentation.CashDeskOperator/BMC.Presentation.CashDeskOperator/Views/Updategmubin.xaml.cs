using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using BMC.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Transport;

#if def
BIN file upgrade
snmpset -Os -c mc300 -v 1 [MC300 IP] 1.3.6.1.4.1.25848.1.1.1.1  s [bin file name]:[Machine IP where TFTP server is running]
Example: >snmpset -Os -c mc300 -v 1 10.2.108.18 1.3.6.1.4.1.25848.1.1.1.1  s d908305b.bin:10.2.108.76 

Options file Upgrade
>snmpset -Os -c mc300 -v 1 [MC300 IP] 1.3.6.1.4.1.25848.1.1.1.1  s options.opt:[Machine IP where TFTP server is running]
Example: >snmpset -Os -c mc300 -v 1 10.2.108.18 1.3.6.1.4.1.25848.1.1.1.1  s options.opt:10.2.108.76

Reboot MC300
>snmpset -Os -c mc300 -v 1 [MC300 IP] 1.3.6.1.4.1.25848.1.1.1.7 i 1
Example: >snmpset -Os -c mc300 -v 1 10.2.108.18 1.3.6.1.4.1.25848.1.1.1.7 i 1
//string _sArguments = "C:\\usr\\bin\\snmpset -Os -c mc300 -v 1 " + ipAddress + " 1.3.6.1.4.1.25848.1.1.1.1  s " + _sBinLocation + ":" + _sTFTPServerIP;


snmpget -Os -c mc300 -v 1 10.0.90.130 1.3.6.1.4.1.25848.1.1.1.5
Output: enterprises.25848.1.1.1.5 = STRING: "n908305d Ver-300.05.04n Nov 10 2011 22:33:17 0001 jackpot fix, met 00002 0 CA75"


#endif

namespace BMC.Presentation.POS.Views
{
    /// <summary>
    /// Interaction logic for UpdateGMUBin.xaml
    /// </summary>
    public partial class UpdateGMIpin : UserControl
    {

        static object _lock = new object();
        private string s_KeyText = string.Empty;
        string _sBinLocation = string.Empty;
        string _soptionfile = string.Empty;
        string _sTFTPServerIP = string.Empty;
        string _SourcePath = string.Empty;
        string _retry = string.Empty;
        string _timeout = string.Empty;

        private ObservableCollection<GetGMUPosDetailsResult> _source = null;

        private ListCollectionView _sourceView = null;

        public UpdateGMIpin()
        {
            InitializeComponent();

            _source = new ObservableCollection<GetGMUPosDetailsResult>();
            _sourceView = new ListCollectionView(_source);
            Gmiupdatebin.ItemsSource = _sourceView;
        }

        Process pr_TFTP = null;

        /// <summary>
        /// Textbox validation using Regex
        /// </summary>
        private static Regex reg_IP = new Regex(@"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)",
                                        RegexOptions.IgnoreCase
                                        | RegexOptions.CultureInvariant
                                        | RegexOptions.IgnorePatternWhitespace
                                        | RegexOptions.Compiled);

        /// <summary>
        /// cmd process for GMUBin,get Version,OptionFile,Reboot
        /// </summary>

        const string strVersion = "{0}snmpget -Os -r {2} -t {3} -c mc300 -v 1 {1} 1.3.6.1.4.1.25848.1.1.1.5";
        const string strReboot = "{0}snmpset -Os -c mc300 -v 1 {1} 1.3.6.1.4.1.25848.1.1.1.7 i 1";
        const string strGMUBin = "{0}snmpset -Os -c mc300 -v 1 {1} 1.3.6.1.4.1.25848.1.1.1.1  s {2}:{3}";
        const string strOptionFile = "{0}snmpset -Os -c mc300 -v 1 {1} 1.3.6.1.4.1.25848.1.1.1.1  s {2}:{3}";

        /// <summary>
        /// UserControl_Loaded 
        /// app config values for
        /// TFTPServerIP
        /// BinLocation
        /// SourcePath
        /// retry
        /// timeout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("GetGMUBinLocation from config file", LogManager.enumLogLevel.Info);
                _soptionfile = Common.ConfigurationManagement.ConfigManager.Read("GMUoptionfilelocation");
                _sBinLocation = Common.ConfigurationManagement.ConfigManager.Read("GMUBinLocation");
                _sTFTPServerIP = Common.ConfigurationManagement.ConfigManager.Read("TFTPServerIP");
                _SourcePath = Common.ConfigurationManagement.ConfigManager.Read("GMUBINAPPLICATION");
                _retry = Common.ConfigurationManagement.ConfigManager.Read("GMURETRY");
                _timeout = Common.ConfigurationManagement.ConfigManager.Read("GMUTIMEOUT");
                processTFTPserver();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID483", BMC_Icon.Warning);
            }

        }



        /// <summary>
        /// GetVersionList from GMU  using cmd 
        /// </summary>
        /// <param name="strIPlist"></param>
        private void GetVersionList(string strIPlist)
        {
            try
            {
                LogManager.WriteLog("Inside GetVersionList ,GetVersion from GMU", LogManager.enumLogLevel.Info);
                         
                _source.Clear();
                GMUsettings.CreateInstance().GetUpdateGMISetting(strIPlist, _source,
                    (g) =>
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            lblStatus.Text = "Analysing GMU IP Address : " + g.IP;
                        }), null);
                        return DoGMUOperation(g, strVersion, GMUOperations.GetVersion.ToString());
                    },
                    () =>
                    {
                        btnfindgmu.IsEnabled = true;
                        lblStatus.Visibility = Visibility.Hidden;
                        if (_source.Count == 0)
                        {
                            MessageBox.ShowBox("MessageID486", BMC_Icon.Information);
                        }
                    });

                //List<GetGMUPosDetailsResult> lst_GMU = GMUsettings.CreateInstance().GetUpdateGMISetting(strIPlist);

                //if (lst_GMU == null)
                //    return;

                //Generic(ref lst_GMU, strVersion, GMUOperations.GetVersion.ToString());

                //if (lst_GMU.Count > 0)
                //{
                //    lst_GMU = lst_GMU.FindAll(o => o.Status != "Timeout");
                //    //Gmiupdatebin.ItemsSource = lst_GMU;
                //}

                //if (lst_GMU.Count == 0)
                //{
                //    MessageBox.ShowBox("MessageID486", BMC_Icon.Information);
                //}


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID484", BMC_Icon.Error);

            }

        }
        /// <summary>
        /// status Message for BurnGMUNBin,BurnOptionFile,GMU Reboot
        /// </summary>
        /// <param name="lst_GMU"></param>
        /// <param name="CmdArg"></param>
        /// <param name="MethodName"></param>

        //void Generic(ref List<GetGMUPosDetailsResult> lst_GMU, string CmdArg, string MethodName)
        void Generic(string CmdArg, string MethodName)
        {
            try
            {

                foreach (GetGMUPosDetailsResult GMU in _source)
                {
                    this.DoGMUOperation(GMU, CmdArg, MethodName);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private bool DoGMUOperation(GetGMUPosDetailsResult GMU, string CmdArg, string MethodName)
        {
            bool result = false;

            try
            {
                if (GMU.IP != null)
                {
                    string _sArguments = "";
                    string ErrorStatus = "Timeout";
                    switch (MethodName)
                    {
                        case "BurnGMUNBin":
                            _sArguments = string.Format(CmdArg, _SourcePath, GMU.IP, _sBinLocation, _sTFTPServerIP);
                            LogManager.WriteLog("UpdateGMIpin->Generic:BurnGMUNBin PARAMS[POS-" + GMU.BarPostion + " ]:" + _sArguments, LogManager.enumLogLevel.Debug);
                            ErrorStatus = "Burn GMU Bin Failed";
                          
                            break;
                        case "BurnOptionFile":
                            _sArguments = string.Format(CmdArg, _SourcePath, GMU.IP, _soptionfile, _sTFTPServerIP);
                            LogManager.WriteLog("UpdateGMIpin->Generic:BurnOptionFile PARAMS[POS-" + GMU.BarPostion + " ]:" + _sArguments, LogManager.enumLogLevel.Debug);
                            ErrorStatus = "Burn Option File Failed";
                            break;
                        case "Reboot":
                            _sArguments = string.Format(CmdArg, _SourcePath, GMU.IP);
                            LogManager.WriteLog("UpdateGMIpin->Generic:Reboot PARAMS[POS-" + GMU.BarPostion + " ]:" + _sArguments, LogManager.enumLogLevel.Debug);
                            ErrorStatus = "Reboot Failed";
                            break;
                        case "GetVersion":
                            _sArguments = string.Format(CmdArg, _SourcePath, GMU.IP, _retry, _timeout);
                            LogManager.WriteLog("UpdateGMIpin->Generic:GetVersion PARAMS[POS-" + GMU.BarPostion + " ]:" + _sArguments, LogManager.enumLogLevel.Debug);
                            break;

                    }

                    string _sOutput = "";
                    string _sError = "";
                    ExecuteProcess(_sArguments, ref _sOutput, ref _sError);
                    result = _sError.Equals(string.Empty);
                    GMU.Status = result ? _sOutput : ErrorStatus;
                    LogManager.WriteLog(MethodName + ": IP :" + GMU.IP + " Status :" + (_sError.Equals("") ? _sOutput : _sError), LogManager.enumLogLevel.Info);
                }
                else
                {
                    GMU.IP = "IP Address Not Found";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return result;
        }
             

        /// <summary>
        /// Execute cmd Process with Error and Arguments 
        ///  Execute cmd Process start here
        /// </summary>
        /// <param name="_sArguments"></param>
        /// <param name="_sOutput"></param>
        /// <param name="_sError"></param>

        private void ExecuteProcess(string _sArguments, ref  string _sOutput, ref string _sError)
        {
            try
            {
                LogManager.WriteLog("ExecuteProcess from GMU Login", LogManager.enumLogLevel.Info);
                ProcessStartInfo si = new ProcessStartInfo("cmd", " /c " + _sArguments);
                Process ps = new Process();
                si.RedirectStandardError = true;
                si.RedirectStandardOutput = true;
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                ps.StartInfo = si;
                ps.Start();
                _sOutput = ps.StandardOutput.ReadToEnd();
                _sError = ps.StandardError.ReadToEnd();
                ps.WaitForExit();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
        }

        private void processTFTPserver()
        {
            try
            {
                string _TFTPserverpath = Common.ConfigurationManagement.ConfigManager.Read("TFTPserverpath");
                pr_TFTP = new Process();
                pr_TFTP.StartInfo.FileName = _TFTPserverpath;
                pr_TFTP.Start();
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (pr_TFTP != null)
            {
                pr_TFTP.Kill();
            }
        }
        /// <summary>
        /// RebootGMU process
        /// </summary>

        private void RebootGMU()
        {
            try
            {
                LogManager.WriteLog("inside RebootGMU", LogManager.enumLogLevel.Info);

                //List<GetGMUPosDetailsResult> lst_GMU = (List<GetGMUPosDetailsResult>)Gmiupdatebin.ItemsSource;
                List<GetGMUPosDetailsResult> lst_GMU = _source.ToList<GetGMUPosDetailsResult>();
                if (lst_GMU.Count != 0)
                {
                    List<GetGMUPosDetailsResult> lst_GMUChecked = lst_GMU.FindAll(obj => obj.IsChecked.Value.Equals(true));

                    if (lst_GMUChecked.Count > 0)
                    {
                        //Generic(ref lst_GMUChecked, strReboot, GMUOperations.Reboot.ToString());
                        Generic(strReboot, GMUOperations.Reboot.ToString());
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID477", BMC_Icon.Information);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID480", BMC_Icon.Error);
            }

        }

        /// <summary>
        /// BurnGMUBin process
        /// </summary>

        private void BurnGMUBin()
        {

            try
            {
                LogManager.WriteLog("inside BurnGMUNBin", LogManager.enumLogLevel.Info);

                //List<GetGMUPosDetailsResult> lst_GMU = (List<GetGMUPosDetailsResult>)Gmiupdatebin.ItemsSource;
                List<GetGMUPosDetailsResult> lst_GMU = _source.ToList<GetGMUPosDetailsResult>();
                if (lst_GMU.Count != 0)
                {
                    List<GetGMUPosDetailsResult> lst_GMUChecked = lst_GMU.FindAll(obj => obj.IsChecked.Value.Equals(true));
                    if (lst_GMUChecked.Count > 0)
                    {
                        //Generic(ref lst_GMUChecked, strGMUBin, GMUOperations.BurnGMUNBin.ToString());
                        Generic(strGMUBin, GMUOperations.BurnGMUNBin.ToString());
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID477", BMC_Icon.Information);
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID478", BMC_Icon.Error);

            }

        }

        /// <summary>
        /// BurnOptionFile Process 
        /// </summary>
        private void BurnOptionFile()
        {

            try
            {
                LogManager.WriteLog("inside BurnOptionFile", LogManager.enumLogLevel.Info);

                //List<GetGMUPosDetailsResult> lst_GMU = (List<GetGMUPosDetailsResult>)Gmiupdatebin.ItemsSource;
                List<GetGMUPosDetailsResult> lst_GMU = _source.ToList<GetGMUPosDetailsResult>();
                if (lst_GMU.Count != 0)
                {
                    List<GetGMUPosDetailsResult> lst_GMUChecked = lst_GMU.FindAll(obj => obj.IsChecked.Value.Equals(true));

                    if (lst_GMUChecked.Count > 0)
                    {
                        //Generic(ref lst_GMUChecked, strOptionFile, GMUOperations.BurnOptionFile.ToString());
                        Generic(strOptionFile, GMUOperations.BurnOptionFile.ToString());
                    }
                }
                else
                {
                    MessageBox.ShowBox("MessageID477", BMC_Icon.Information);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

                MessageBox.ShowBox("MessageID479", BMC_Icon.Error);

            }


        }

        /// <summary>
        /// Get StartIP and end  EndIP from  textbox
        /// </summary>
        /// <param name="StartIP"></param>
        /// <param name="EndIP"></param>
        /// <returns></returns>
        private clsIPList GetIPlist(string StartIP, string EndIP)
        {



            clsIPList IPs = new clsIPList();
            try
            {
                LogManager.WriteLog("inside GetIPlist", LogManager.enumLogLevel.Info);
                int intAddress = BitConverter.ToInt32(IPAddress.Parse(StartIP).GetAddressBytes(), 0);

                int stIPValue = IPAddress.NetworkToHostOrder((int)IPAddress.Parse(StartIP).Address);
                stIPValue = intAddress;
                int endIPValue = IPAddress.NetworkToHostOrder((int)IPAddress.Parse(EndIP).Address);
                endIPValue = BitConverter.ToInt32(IPAddress.Parse(EndIP).GetAddressBytes(), 0);
                int AddIPVal = (int)IPAddress.Parse("0.0.0.1").Address;


                //while (IPAddressToLongBackwards(StartIP) <= IPAddressToLongBackwards(EndIP))
                while (stIPValue <= endIPValue)
                {

                    IPs.Add(StartIP);
                    stIPValue = stIPValue + AddIPVal;
                    StartIP = new IPAddress(BitConverter.GetBytes(stIPValue)).ToString();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID482", BMC_Icon.Error);


            }

            return IPs;

        }

        /// <summary>
        /// check box click event for select all IP in GMU IP list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void chkHeader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogManager.WriteLog("inside chkHeader_Click", LogManager.enumLogLevel.Info);

                if (chkHeader.IsChecked ?? false)
                {
                    foreach (GetGMUPosDetailsResult item in Gmiupdatebin.ItemsSource)
                    {
                        item.IsChecked = true;
                    }
                }
                else
                {
                    foreach (GetGMUPosDetailsResult item in Gmiupdatebin.ItemsSource)
                    {
                        item.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        private void txtStartIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            if (!Settings.OnScreenKeyboard)
                return;
            txtStartIP.Text = DisplayKeyboard(txtStartIP.Text, string.Empty);
            txtStartIP.SelectionStart = txtStartIP.Text.Length;

        }

        private void txtEndIP_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!Settings.OnScreenKeyboard)
                return;
            txtEndIP.Text = DisplayKeyboard(txtEndIP.Text, string.Empty);
            txtEndIP.SelectionStart = txtEndIP.Text.Length;
        }
        /// <summary>
        /// keyboard display for enter IP in text box
        /// </summary>
        /// <param name="keyText"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string DisplayKeyboard(string keyText, string type)
        {
            s_KeyText = "";

            var objKeyboard = new KeyboardInterface();
            if (type == "Pwd")
            {
                objKeyboard.IsPwd = true;
            }
            objKeyboard.Closing += ObjKeyboardClosing;
            objKeyboard.KeyString = keyText;
            Point locationFromScreen = this.PointToScreen(new Point(0, 0));
            PresentationSource source = PresentationSource.FromVisual(this);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(locationFromScreen);
            objKeyboard.Top = targetPoints.Y + this.Height / 2;
            objKeyboard.Left = targetPoints.X;
            objKeyboard.ShowDialog();
            return s_KeyText;
        }
        /// <summary>
        /// KeyboardClosing objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjKeyboardClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (((KeyboardInterface)sender).DialogResult == true)
            {
                s_KeyText = ((KeyboardInterface)sender).KeyString;
            }
            ((KeyboardInterface)sender).Closing -= ObjKeyboardClosing;
        }
        /// <summary>
        /// Find Gmu IP list button click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfindgmu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnfindgmu.IsEnabled = false;
                lblStatus.Visibility = Visibility.Visible;

                LogManager.WriteLog("inside btnfindgmu_Click", LogManager.enumLogLevel.Info);
                string ErrorMsg = "";

                if (ValidateControls(ref ErrorMsg))
                {
                    clsIPList lst_IPs = GetIPlist(txtStartIP.Text, txtEndIP.Text);
                    GetVersionList(lst_IPs.ToString());
                }
                else
                {
                    MessageBox.ShowBox(ErrorMsg, BMC_Icon.Error, true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                MessageBox.ShowBox("MessageID485", BMC_Icon.Error);
            }
        }
        /// <summary>
        /// valadation for textbox
        /// </summary>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>

        private bool ValidateControls(ref string ErrorMsg)
        {
            bool retVal = true;
            try
            {
                if (String.IsNullOrEmpty(txtStartIP.Text))
                {
                    ErrorMsg = "Please enter Start IP";
                    retVal = false;
                }
                else if (!reg_IP.IsMatch(txtStartIP.Text))
                {
                    ErrorMsg = "Please enter a valid Start IP";
                    retVal = false;
                }

                else if (String.IsNullOrEmpty(txtEndIP.Text))
                {
                    ErrorMsg = "Please enter End IP";
                    retVal = false;

                }

                else if (!reg_IP.IsMatch(txtEndIP.Text))
                {
                    ErrorMsg = "Please enter a valid End IP";
                    retVal = false;
                }
                else
                {
                    long stIPValue = IPAddress.Parse(txtStartIP.Text).Address;

                    long endIPValue = IPAddress.Parse(txtEndIP.Text).Address;
                    if (stIPValue > endIPValue)
                    {
                        ErrorMsg = "End IP must be greater than or equal to Start IP";
                        retVal = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                ErrorMsg = "Unable to Validate Controls";
                retVal = false;
            }
            return retVal;
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            RebootGMU();
        }

        private void btnburnoption_Click(object sender, RoutedEventArgs e)
        {
            BurnOptionFile();
        }

        private void btnburnbin_Click(object sender, RoutedEventArgs e)
        {
            BurnGMUBin();
        }
    }

    public class clsIPList : List<string>
    {
        public override string ToString()
        {

            StringBuilder strBuild = new StringBuilder();
            try
            {
                foreach (string str in this)
                {
                    strBuild.Append(str + ",");
                }
                if (strBuild.Length > 0)
                {
                    strBuild = strBuild.Remove(strBuild.Length - 1, 1);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strBuild.ToString();
        }
    }

    enum GMUOperations
    {
        GetVersion,
        BurnGMUNBin,
        BurnOptionFile,
        Reboot
    }
}
