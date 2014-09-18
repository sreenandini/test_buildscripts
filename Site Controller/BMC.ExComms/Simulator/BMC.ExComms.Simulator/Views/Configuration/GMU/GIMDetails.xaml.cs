using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
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
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Simulator.Models;

namespace BMC.ExComms.Simulator.Views.Configuration.GMU
{
    /// <summary>
    /// Interaction logic for GIMDetails.xaml
    /// </summary>
    public partial class GIMDetails : UserControl
    {
        private UdpClient _socket = new UdpClient();
        private IPEndPoint _ep = new IPEndPoint(IPAddress.Loopback, 11112);

        public GIMDetails()
        {
            this.InitializeComponent();
        }
        
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            using (ILogMethod method = Log.LogMethod("", "Method"))
            {
                try
                {
                    string input =
                        "[C0][A8][02][10][05][A2][BC][17][B1][00][01][00][01][00][5E][17][5C][01][5A][01][05][30][30][30][30][32][02][05][31][32][33][34][35][03][02][42][39][04][0C][30][30][30][30][30][30][30][31][32][33][34][35][05][06][00][16][80][01][47][4D][06][03][36][30][31][07][2B][4F][63][74][20][31][38][20][32][30][31][33][20][31][35][3A][35][32][3A][31][37][20][56][65][72][2D][33][30][30][2E][30][35][2E][31][34][61][20][4F][70][74][69][6F][6E][73][C4]";
                    FFMsg_G2H g2h = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H, FreeformHelper.GetBufferFromHexString(input));
                    IFreeformEntity_MsgTgt tgt = g2h.Targets[0];

                    FFMsg_G2H msg = FreeformEntityFactory.CreateEntity<FFMsg_G2H>(FF_FlowDirection.G2H,
                        new FFCreateEntityRequest_G2H()
                        {
                            Command = FF_AppId_G2H_Commands.ResponseRequest,
                            MessageType = FF_AppId_G2H_MessageTypes.FreeForm,
                            SessionID = FF_AppId_SessionIds.GIM,
                            TransactionID = 0xB1,
                            IPAddress = "192.168.2.16"
                        });
                    FFTgt_B2B_GIM gim = new FFTgt_B2B_GIM()
                    {
                        GIMData = new FFTgt_G2H_GIM_GameIDInfo()
                        {
                            AssetNumber = txtAssetNumber.Text,
                            GMUNumber = txtGMUNumber.Text,
                            SerialNumber = txtSerialNumber.Text,
                            ManufacturerID = txtManufacturerID.Text,
                            MACAddress = txtMACAddress.Text,
                            SASVersion = txtSASVersion.Text,
                            GMUVersion = txtGMUVersion.Text,
                        }
                    };
                    msg.AddTarget(gim);

                    byte[] bytes = FreeformEntityFactory.CreateBuffer(FF_FlowDirection.G2H, msg);
                    string data = FreeformEntityFactory.CreateBufferHexString(FF_FlowDirection.G2H, msg);
                    _socket.Send(bytes, bytes.Length, _ep);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }
}