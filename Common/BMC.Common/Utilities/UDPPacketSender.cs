using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using BMC.Common.ConfigurationManagement;
using BMC.Common.LogManagement;

namespace BMC.Common.Utilities
{
    class UDPPacketSender
    {
        private UdpClient Connection = null;

        public UDPPacketSender()
        {

            try
            {
                int iPort = int.Parse(ConfigManager.Read("Port"));
                string sIP = ConfigManager.Read("ServerIP");
                Connection = new UdpClient(sIP, iPort);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Error);
            }
        }

        public void Send(string strMessage)
        {
            try
            {
                Byte[] objData = System.Text.Encoding.ASCII.GetBytes(strMessage);
                Connection.Send(objData, objData.Length);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.ToString(), LogManager.enumLogLevel.Warning);
            }
        }


    }
}
