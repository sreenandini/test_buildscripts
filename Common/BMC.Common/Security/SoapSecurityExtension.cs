using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Security.Cryptography;

namespace BMC.Common.Security
{


    public class SoapSecurityExtension : SoapExtension
    {
        Stream oldStream;
        Stream newStream;
        DecryptMode decryptMode;
        EncryptMode encryptMode;
        Target target;

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return attribute;
        }

        public override object GetInitializer(Type t)
        {
            return typeof(SoapSecurityExtension);
        }

        public override void Initialize(object initializer)
        {
            SoapSecurityExtensionAttribute attribute = (SoapSecurityExtensionAttribute)initializer;

            decryptMode = attribute.Decrypt;
            encryptMode = attribute.Encrypt;
            target = attribute.Target;
            return;
        }

        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {

                case SoapMessageStage.BeforeSerialize:
                    break;

                case SoapMessageStage.AfterSerialize:
                    Encrypt();
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    Decrypt();
                    break;

                case SoapMessageStage.AfterDeserialize:
                    break;

                default:
                    throw new Exception("invalid stage");
            }
        }

        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        private void Decrypt()
        {
            MemoryStream decryptedStream = new MemoryStream();

            TextReader reader = new StreamReader(oldStream);
            TextWriter writer = new StreamWriter(decryptedStream);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();

            decryptedStream = DecryptSoap(decryptedStream);

            Copy(decryptedStream, newStream);

            newStream.Position = 0;
        }

        private void Encrypt()
        {
            newStream.Position = 0;

            newStream = EncryptSoap(newStream);

            Copy(newStream, oldStream);
        }

        private byte[] CovertStringToByteArray(string s)
        {
            char[] c = { ' ' };
            string[] ss = s.Split(c);

            byte[] b = new byte[ss.Length];

            for (int i = 0; i < b.Length; i++)
            {
                b[i] = Byte.Parse(ss[i]);
            }

            return b;
        }

        public MemoryStream EncryptSoap(Stream streamToEncrypt)
        {
            streamToEncrypt.Position = 0;
            XmlTextReader reader = new XmlTextReader(streamToEncrypt);
            XmlDocument dom = new XmlDocument();
            dom.Load(reader);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(dom.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            try
            {
                XmlNode soapHeaderNode = dom.SelectSingleNode("//soap:Header", nsmgr);
                soapHeaderNode = soapHeaderNode.FirstChild;
                if (soapHeaderNode != null)
                    soapHeaderNode.InnerXml = CryptographyHelper.Encrypt(soapHeaderNode.InnerXml);
            }
            catch {}
            

            XmlNode soapBodyNode = dom.SelectSingleNode("//soap:Body", nsmgr);
            soapBodyNode = soapBodyNode.FirstChild;

            if (soapBodyNode != null)
                soapBodyNode.InnerXml = CryptographyHelper.Encrypt(soapBodyNode.InnerXml);

            MemoryStream ms = new MemoryStream();
            dom.Save(ms);
            ms.Position = 0;

            return ms;
        }

        public MemoryStream DecryptSoap(Stream streamToDecrypt)
        {
            streamToDecrypt.Position = 0;
            XmlTextReader reader = new XmlTextReader(streamToDecrypt);
            XmlDocument dom = new XmlDocument();
            dom.Load(reader);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(dom.NameTable);
            nsmgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");

            try
            {
                XmlNode soapHeaderNode = dom.SelectSingleNode("//soap:Header", nsmgr);
                soapHeaderNode = soapHeaderNode.FirstChild;
                if (soapHeaderNode != null)
                    soapHeaderNode.InnerXml = CryptographyHelper.Decrypt(soapHeaderNode.InnerXml);
            }
            catch { }
            
            XmlNode soapBodyNode = dom.SelectSingleNode("//soap:Body", nsmgr);
            soapBodyNode = soapBodyNode.FirstChild;
            if (soapBodyNode != null)
                soapBodyNode.InnerXml = CryptographyHelper.Decrypt(soapBodyNode.InnerXml);

            MemoryStream ms = new MemoryStream();
            ms.Position = 0;
            dom.Save(ms);
            ms.Position = 0;

            return ms;
        }

        void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class SoapSecurityExtensionAttribute : SoapExtensionAttribute
    {
        private int priority;
        private EncryptMode encryptionMode = EncryptMode.None;
        private DecryptMode decryptionMode = DecryptMode.None;
        private Target target = Target.Method;

        public override Type ExtensionType
        {
            get { return typeof(SoapSecurityExtension); }
        }

        public override int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        public EncryptMode Encrypt
        {
            get
            {
                return encryptionMode;
            }
            set
            {
                encryptionMode = value;
            }
        }

        public DecryptMode Decrypt
        {
            get
            {
                return decryptionMode;
            }
            set
            {
                decryptionMode = value;
            }
        }

        public Target Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

    }

    public enum DecryptMode
    {
        None,
        Response,
        Request
    }

    public enum EncryptMode
    {
        None,
        Response,
        Request
    }

    public enum Target
    {
        Envelope,
        Body,
        Method
    }

}

