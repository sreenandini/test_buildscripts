using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using BMC.CoreLib.Diagnostics;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace BMC.CoreLib.Net
{
    public interface INetSocketHandler : IDisposable
    {
        Socket InternalSocket { get; }
        Encoding Encoding { get; set; }
        bool IsConnected { get; }
        IPEndPoint LocalEndPoint { get; }
        EndPoint RemoteEndPoint { get; }

        int SendTimeout { get; set; }
        int ReceiveTimeout { get; set; }
        int PollTimeout { get; set; }

        event SocketReceivedBytesHandler ReceivedBytes;

        bool Start();
        bool Stop();
        INetSocketHandler Clone();

        int Write(string data);
        int Write(byte[] buffer);
        int Write(byte[] buffer, int offset, int size);

        int WriteTo(string data, ref EndPoint remoteEndPoint);
        int WriteTo(byte[] buffer, ref EndPoint remoteEndPoint);
        int WriteTo(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint);

        string Read();
        int Read(byte[] buffer);
        int Read(byte[] buffer, int offset, int size);

        string ReadFrom(ref EndPoint remoteEndPoint);
        int ReadFrom(byte[] buffer, ref EndPoint remoteEndPoint);
        int ReadFrom(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint);

        IAsyncResult BeginReadFrom(ref EndPoint remoteEndPoint);
        IAsyncResult BeginReadFrom(byte[] buffer, ref EndPoint remoteEndPoint);
        IAsyncResult BeginReadFrom(byte[] buffer, int offset, int size, ref EndPoint remoteEndPoint);
        int EndReadFrom(IAsyncResult asyncResult, ref EndPoint remoteEndPoint);

        void WriteSocketLog(string status);
        void WriteSocketLogV(string status, params object[] values);
        void WriteSocketLog(ModuleProc PROC, string status);
        void WriteSocketLogV(ModuleProc PROC, string status, params object[] values);
    }

    public interface INetSocketHandlerSsl : INetSocketHandler
    {
        event RemoteCertificateValidationCallback RemoteCertificateValidation;

        SslProtocols SslProtocol { get; set; }

        SslStream GetSslStream(string targetHost, X509CertificateCollection clientCertificates);

        void CloseSslStream();
    }

    public delegate void SocketClientConnectedHandler(INetSocketHandler server, INetSocketHandler client);
    public delegate bool SocketReceivedBytesHandler(INetSocketHandler client, byte[] data);
}
