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
using BMC.Common.Utilities;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.ExComms.Contracts.Proxies;

namespace MonitorClientTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static string GetServerName()
        {
            return BMCRegistryHelper.GetRegKeyValue(@"Cashmaster\Exchange", "Default_Server_Ip");

            //var key = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(@"Software\Honeyframe\Cashmaster\Exchange", true);
            //if (key != null) return key.GetValue("Default_Server_Ip").ToString();
            //throw new InvalidDataException("Registry key not Set.  Default_Server_IP not configured");
        }

        public int AddUDPToListWithoutWait(int installatioNo, int barPositionPortNo)
        {
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            message.Targets.Add(new MonTgt_H2G_Client_AddUDPToList
            {
                ServerIP = GetServerName(),
                Port = barPositionPortNo,
                PollingID = 0,
                Type = 7
            });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
        }

        public int RemoveUDPFromListWithoutWait(int installatioNo)
        {
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            message.Targets.Add(new MonTgt_H2G_Client_RemoveUDPFromList
            {
                InstallationNo = installatioNo,
            });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
        }

        public int EnableDisableMachine(int installatioNo, bool enable)
        {
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            message.Targets.Add(new MonTgt_H2G_Client_EnableDisableMachine
            {
                EnableDisable = enable,
            });
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
        }

        public int EnableDisableAFT(int installatioNo, bool enable)
        {
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            MonitorEntity_MsgTgt target = null;
            if (enable) target = new MonTgt_H2G_EFT_SystemEnable();
            else target = new MonTgt_H2G_EFT_SystemDisable();
            message.Targets.Add(target);
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
        }

        public int TotalGames(int installatioNo)
        {
            MonMsg_H2G message = new MonMsg_H2G()
            {
                InstallationNo = installatioNo,
            };
            message.Targets.Add(new MonTgt_H2G_LP_TotalGames());
            return ExMonServer4MonClientProxyFactory.Get().ProcessH2GMessage(message) ? 0 : -1;
        }

        private void btnAddToUDPList_Click(object sender, RoutedEventArgs e)
        {
            int r = this.AddUDPToListWithoutWait(27, 14);
        }

        private void btnRemoveToUDPList_Click(object sender, RoutedEventArgs e)
        {
            int r = this.RemoveUDPFromListWithoutWait(27);
        }

        private void btnEnableMachine_Click(object sender, RoutedEventArgs e)
        {
            int r = this.EnableDisableMachine(27, true);
        }

        private void btnDisableMachine_Click(object sender, RoutedEventArgs e)
        {
            int r = this.EnableDisableMachine(27, false);
        }

        private void btnEnableAFT_Click(object sender, RoutedEventArgs e)
        {
            int r = this.EnableDisableAFT(27, true);
        }

        private void btnDisableAFT_Click(object sender, RoutedEventArgs e)
        {
            int r = this.EnableDisableAFT(27, false);
        }

        private void btnLPTotalGames_Click(object sender, RoutedEventArgs e)
        {
            int r = this.TotalGames(27);
        }
    }
}
