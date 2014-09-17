using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace BMC.Common.Utilities
{
    public class Certificate
    {
        private static X509Certificate _clientCertificate;

        public static string IssuerName = string.Empty;

        public static X509Certificate ClientCertificate
        {
            get
            {
                if (_clientCertificate == null)
                    _clientCertificate = new X509Certificate();
                return _clientCertificate;
            }
        }

        public class ByPassCertificatePolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint sp, X509Certificate cert,
            WebRequest request, int problem)
            {
                var validationResult = true;
                                
                if (IssuerName != "S@SDFJ872JASD==")
                    if (!cert.Issuer.ToUpper().Contains(IssuerName.ToUpper().Trim())) return false;

                var chain = new X509Chain();


                chain.Build(new X509Certificate2(cert));

                foreach (X509ChainElement e in chain.ChainElements)
                {
                    foreach (X509ChainStatus s in e.ChainElementStatus)
                    {
                        if (((X509ChainStatusFlags.Revoked | X509ChainStatusFlags.NotTimeValid
                            | X509ChainStatusFlags.NotSignatureValid | X509ChainStatusFlags.InvalidExtension
                            | X509ChainStatusFlags.NotValidForUsage | X509ChainStatusFlags.Cyclic) & s.Status) == s.Status)
                        {
                            validationResult = false;
                        }
                    }
                }

                return validationResult;
            }
        }



    }
}
