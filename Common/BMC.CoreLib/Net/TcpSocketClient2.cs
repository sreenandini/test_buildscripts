using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using BMC.CoreLib.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace BMC.CoreLib.Net
{
    public class TcpSocketClient2 : TcpSocketClient
    {
        protected int _sendRetryCount = 1;

        public TcpSocketClient2(int portNo)
            : base(portNo) { this.Initialize(); }

        public TcpSocketClient2(int portNo, string remoteAddress)
            : base(portNo, remoteAddress) { this.Initialize(); }

        public TcpSocketClient2(EndPoint remoteEndpoint)
            : base(remoteEndpoint) { this.Initialize(); }

        private void Initialize()
        {
            _pollTimeout = 1000;
        }

        public int SendRetryCount
        {
            get { return _sendRetryCount; }
            set { _sendRetryCount = Math.Min(1, value); }
        }

        public override int PollTimeout
        {
            get { return _pollTimeout; }
            set { _pollTimeout = Math.Min(1, value); }
        }

        protected override int WriteInternal(ModuleProc PROC, byte[] buffer, int offset, int size)
        {
            bool result = false;
            int bytesSent = 0;
            int socketPollTimeout = (_pollTimeout * 1000);

            for (int i = 0; i < _sendRetryCount; i++)
            {
                Log.Info(PROC, "Retry Count : " + i.ToString());

                try
                {
                    if (_handler == null)
                    {
                        if (this.Start())
                        {
                            this.WriteSocketLog(PROC, "Socket connected freshly.");
                        }
                    }

                    if (_handler == null)
                    {
                        this.WriteSocketLog(PROC, "Socket.Poll : Unable to connect cmp server.");
                        continue;
                    }

                    // detect the socket disconnection
                    if (!_socket.Poll(socketPollTimeout, SelectMode.SelectWrite))
                    {
                        this.WriteSocketLog(PROC, "Socket.Poll : Connection was closed.");
                        this.Start();
                    }
                    else
                    {
                        //this.WriteSocketLog(PROC, "Socket.Poll : Succeeded.");
                        this.WriteSocketLog(PROC, "Socket.Poll (Write) : Succeeded.");
                        bool poolStatus = _socket.Poll(socketPollTimeout, SelectMode.SelectRead);
                        this.WriteSocketLog(PROC, "Socket.Poll (Read) : " + (poolStatus ? "Succeeded" : "Failed") + ".");

                        if (poolStatus)
                        {
                            byte[] buffer2 = new byte[RECEIVE_BUFFER_SIZE];
                            int bytesRead = 0;
                            string received = string.Empty;
                            SocketError readError = SocketError.Success;
                            try
                            {
                                //_socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, 0);
                                bytesRead = _socket.Receive(buffer2, 0, buffer2.Length, SocketFlags.None, out readError);
                                received = ", Received Error Code : " + readError.ToString();

                                if (bytesRead > 0)
                                {
                                    received += ", Received Data : " + this.Encoding.GetString(buffer2, 0, bytesRead);
                                }
                                //_socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.ReceiveBuffer, 0);
                            }
                            catch (Exception ex)
                            {
                                Log.Info(PROC, "Socket.Read Error : " + ex.Message);
                            }

                            this.WriteSocketLog(PROC, "Socket.Read : Received Bytes => " + bytesRead.ToString() + received);
                            this.Start();
                        }
                    }
#if DEBUG && TESTING                        
                        Thread.Sleep(30000);
#endif

                    if (_handler == null)
                    {
                        this.WriteSocketLog(PROC, "Socket.Send : Unable to connect the server.");
                        continue;
                    }

                    // valid connection
                    SocketError socketErrorCode = SocketError.Success;
                    bytesSent = _socket.Send(buffer, 0, buffer.Length, SocketFlags.None, out socketErrorCode);

                    // success
                    if (bytesSent > 0 && (socketErrorCode == SocketError.Success))
                    {
                        this.WriteSocketLog(PROC, "*** (" + bytesSent.ToString() + ") bytes was successfully sent to socket.");
                        if (this.HasReceivedBytesEvent)
                        {
                            result = this.ReadResponse(PROC);
                        }
                        else
                        {
                            result = true;
                        }

                        if (_sendRetryCount == 1)
                        {
                            break;
                        }
                        else
                        {
                            if (result) break;
                        }
                    }
                    else
                    {
                        this.WriteSocketLog(PROC, "Socket Error Occured : " + socketErrorCode.ToString() +
                                                           ", IsConnected : " + _handler.Connected);
                        this.Stop();
                    }
                }
                catch (Exception ex)
                {
                    this.WriteSocketLog(PROC, "Exception Type : " + ex.GetType().FullName);
                    Log.Exception(PROC, ex);
                    this.Stop();
                }
                finally
                {
                    if (!result)
                    {
                        Thread.Sleep(100);
                    }
                }
            }

            return (result ? bytesSent : 0);
        }

        private bool ReadResponse(ModuleProc PROC)
        {
            bool result = false;

            try
            {
                byte[] buffer = new byte[RECEIVE_BUFFER_SIZE];
                SocketError socketErrorCode = SocketError.Success;
                int bytesReceived = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None, out socketErrorCode);
                if (bytesReceived > 0 && (socketErrorCode == SocketError.Success))
                {
                    byte[] bufferCopy = new byte[bytesReceived];
                    Buffer.BlockCopy(buffer, 0, bufferCopy, 0, bufferCopy.Length);
                    result = this.OnReceivedBytes(this, bufferCopy);
                }
                else
                {
                    this.WriteSocketLog(PROC, "No response received. (" + socketErrorCode.ToString() + ")");
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
