using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.CoreLib.WcfHelper.Helpers
{
    public static class AddressHelper
    {
        private static string _pathName = string.Empty;

        static AddressHelper()
        {
            _pathName = "BMC";
        }

        public static void SetBasePath(string pathName)
        {
            _pathName = pathName;
        }

        public static void SetBasePathExchange()
        {
            SetBasePath("BMC/Exchange");
        }

        private static Uri GetUri(UriBuilder builder, string path)
        {
            builder.Path = path;
            return builder.Uri;
        }

        public static Uri CreateHttpAddress(string hostName, int portNo)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeHttp, hostName, portNo), _pathName);
        }

        public static Uri CreateHttpAddress(string hostName, int portNo, string pathName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeHttp, hostName, portNo), pathName);
        }

        public static Uri CreateHttpsAddress(string hostName, int portNo, string pathName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeHttps, hostName, portNo), pathName);
        }

        public static Uri CreateHttpAddress(string hostName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeHttp, hostName), _pathName);
        }

        public static Uri CreateHttpAddress(string hostName, string pathName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeHttp, hostName), pathName);
        }

        public static Uri CreateTcpAddress(string hostName, int portNo)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeNetTcp, hostName, portNo), _pathName);
        }

        public static Uri CreateTcpAddress(string hostName, int portNo, string pathName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeNetTcp, hostName, portNo), pathName);
        }

        public static Uri CreateTcpAddress(string hostName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeNetTcp, hostName), _pathName);
        }

        public static Uri CreateTcpAddress(string hostName, string pathName)
        {
            return GetUri(new UriBuilder(Uri.UriSchemeNetTcp, hostName), pathName);
        }
    }
}
