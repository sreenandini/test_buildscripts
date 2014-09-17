using BMC.BusinessClasses.Interfaces;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using BMC.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class AlertEngine : IAlertEngine,IDisposable
    {

        #region IAlertEngine Members

        public bool SendMail(AlertEntity alert)
        {
            bool status = false;
            string strStatus = string.Empty;

            try
            {
                if (alert.ServerInfo != null)
                {

                    System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(alert.ServerInfo.ServerName);


                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                    message.IsBodyHtml = alert.MessageGroup.IsBodyHtml;
                    message.From = new System.Net.Mail.MailAddress(alert.MessageGroup.FromAddress);
                    message.BodyEncoding = System.Text.Encoding.ASCII;

                    if (!string.IsNullOrEmpty(alert.ServerInfo.Port))
                    {
                        int portAddress = 0;

                        if (int.TryParse(alert.ServerInfo.Port, out portAddress))
                        {
                            mailClient.Port = portAddress;
                        }
                    }


                    mailClient.EnableSsl = alert.ServerInfo.EnableSSL;

                    if (!string.IsNullOrEmpty(alert.ServerInfo.UserID) && !string.IsNullOrEmpty(alert.ServerInfo.Password))
                    {
                        mailClient.UseDefaultCredentials = false;
                        mailClient.Credentials = new System.Net.NetworkCredential(alert.ServerInfo.UserID,
                            SiteLicensingCryptoHelper.Decrypt(alert.ServerInfo.Password, "B411y51T"));
                    }

                    if (!string.IsNullOrEmpty(alert.ServerInfo.PickupFolder))
                    {
                        if (!System.IO.Directory.Exists(alert.ServerInfo.PickupFolder))
                        {
                            System.IO.Directory.CreateDirectory(alert.ServerInfo.PickupFolder);

                        }
                        mailClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        mailClient.PickupDirectoryLocation = alert.ServerInfo.PickupFolder;

                    }

                    LogManager.WriteLog("Started Processing", LogManager.enumLogLevel.Info);

                    string[] ToList = alert.MessageGroup.ToList.Split(';');
                    string[] ccList = alert.MessageGroup.CCList.Split(';');
                    string[] bccList = alert.MessageGroup.BCCList.Split(';');
                    if (ToList != null)
                    {
                        for (int index = 0; index < ToList.Length; index++)
                        {
                            if (!string.IsNullOrEmpty(ToList[index]))
                                message.To.Add(ToList[index]);
                        }

                        for (int index = 0; index < ccList.Length; index++)
                        {
                            if (!string.IsNullOrEmpty(ccList[index]))
                                message.CC.Add(ccList[index]);
                        }

                        for (int index = 0; index < bccList.Length; index++)
                        {
                            if (!string.IsNullOrEmpty(bccList[index]))
                                message.Bcc.Add(bccList[index]);
                        }


                        message.Body = alert.MessageGroup.MsgContent;
                        message.Subject = alert.MessageGroup.Subject;
                        if (!string.IsNullOrEmpty(alert.MessageGroup.AttachementPath))
                        {
                            alert.MessageGroup.AttachmentType = alert.MessageGroup.AttachmentType.Replace("*yyyyMMdd", "*" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd"));
                            string[] files = Directory.GetFiles(alert.MessageGroup.AttachementPath, alert.MessageGroup.AttachmentType, SearchOption.AllDirectories);

                            LogManager.WriteLog("Adding Attachments Processing", LogManager.enumLogLevel.Info);
                            foreach (string sourceFile in files)
                            {
                                message.Attachments.Add(new System.Net.Mail.Attachment(sourceFile));
                            }
                            LogManager.WriteLog("Adding Attachments Completed", LogManager.enumLogLevel.Info);
                        }
                        mailClient.Send(message);
                        LogManager.WriteLog("Message Sent", LogManager.enumLogLevel.Info);
                        status = true;
                        strStatus = "Email Message Sent Successfully";

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                status = false;
            }

           
            return status;
        }


        public static MailServer GetMailServer()
        {
            MailServer lMailServer = null;
            try
            {
                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    MailServerInfoResult lServer = context.GetMailServerInfo().ToList().First();

                    lMailServer = (
                                    new MailServer
                                    {
                                        EnableSSL = (bool)lServer.EnableSSL,
                                        Password = lServer.PWD,
                                        Port = lServer.Port.ToString(),
                                        ServerName = lServer.ServerName,
                                        UserID = lServer.UID

                                    });

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                lMailServer = new MailServer();
            }

            return lMailServer;
        }
        /// <summary>
        /// get setting value from setting table.
        /// </summary>
        /// <param name="SettingName"></param>
        /// <returns></returns>
        public static string GetSetting(string SettingName)
        {
            string strSetting = string.Empty;

            try
            {

                using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                {
                    context.GetSetting(0, SettingName, string.Empty, ref strSetting);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return strSetting;
        }
        #endregion

        public void Dispose()
        {
            this.Dispose();
        }
    }
}