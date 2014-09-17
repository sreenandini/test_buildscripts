using System.Web.Services.Protocols;

namespace BMC.EnterpriseWebService
{
    /// <summary>
    /// Summary description for AuthenticationInformation
    /// </summary>
    public class AuthenticationInformation : SoapHeader
    {
        public string EnterprisePassKey;
        public string ExchangePassKey;
        public string SiteCode;
    }
}