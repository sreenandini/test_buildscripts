using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.EnterpriseDataAccess;
using BMC.Common.Utilities;
using BMC.Common.ExceptionManagement;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace BMC.EnterpriseBusiness.Business
{
    public class AlertSystemBiz : IAlert
    {
        static AlertSystemBiz _alertBiz = null;


        public static AlertSystemBiz CreateInstance()
        {
            if (_alertBiz == null)
                _alertBiz = new AlertSystemBiz();

            return _alertBiz;
        }

        /// <summary>
        /// get the email alert types.
        /// </summary>
        /// <returns></returns>
        public List<AlertTypes> GetAlertTypes()
        {
            List<AlertTypes> lstAlerts = null;
            try
            {

                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    lstAlerts = new List<AlertTypes>();
                    foreach (GetAlertTypesResult result in context.GetAlertTypes())
                        lstAlerts.Add(new AlertTypes { AlertTypeID = result.ID, AlertTypeName = result.AlertTypeName });
                }
            }
            catch (Exception ex)
            { ExceptionManager.Publish(ex); lstAlerts = new List<AlertTypes>(); }

            return lstAlerts;

        }

        /// <summary>
        /// Save the email details for subscribers.
        /// </summary>
        /// <param name="eEntity"></param>
        public void SaveEmailAlerts(List<EmailAlertEntity> eEntity, List<AlertLink> dList, string UserName, int UserId, string Oldvalue, string NewValue)
        {
            try
            {

                string sXML = Serialize(eEntity, "MailSubscribers");
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    context.InsertEmailAlertSubscribers(sXML, Serialize(dList,"LinkList"), UserName, UserId, Oldvalue, NewValue);
                }

            }
            catch (Exception ex)
            {
                    ExceptionManager.Publish(ex);
            }
        }

        private string Serialize(object typevalue,string RootElement)
        {
            StringWriter stringwriter = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(stringwriter, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typevalue.GetType(), new XmlRootAttribute(RootElement));

                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, typevalue, namespaces);
            }
            return stringwriter.ToString();
        }
        /// <summary>
        /// get the email subscribers.
        /// </summary>
        /// <returns>EmailAlertEntity</returns>
        public List<EmailAlertEntity> GetEmailSubscribers(string AlertTypeName)
        {
            List<EmailAlertEntity> lstEmailsubscribers = null;
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    lstEmailsubscribers = new List<EmailAlertEntity>();
                    //create a list of email subscribers.
                    foreach (EmailSubscriberDetailsResult result in context.GetEmailSubscriberDetails(AlertTypeName).ToList())
                        lstEmailsubscribers.Add(new EmailAlertEntity
                        {
                            AlertTypeId = Convert.ToInt32(result.ID),
                            AlertTypeName = result.TypeName,
                            Subject = result.SUBJECT,
                            ToMail = result.ToMail,
                            CCMail = result.CCMail,
                            BCCMail = result.BCCMail,
                            FromMail= result.FromMail
                        });
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                lstEmailsubscribers = new List<EmailAlertEntity>();
            }

            return lstEmailsubscribers;
        }

        /// <summary>
        /// Get the active sites associated with the user.
        /// </summary>
        /// <param name="SecurityUserID"></param>
        /// <returns></returns>

        public List<SiteEntity> GetSites(int? SecurityUserID)
        {
            List<SiteEntity> lSiteDetails = null;
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    List<rsp_GetActiveSitesForUserResult> lstSites = context.GetActiveSitesForUser(SecurityUserID).ToList();

                    lSiteDetails = (from s in lstSites
                                    group s by s into g
                                    select new SiteEntity
                                    {
                                        SiteID = g.Key.Site_ID,
                                        SiteCode = g.Key.Site_Code,
                                        SiteName = g.Key.Site_Name
                                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                lSiteDetails = new List<SiteEntity>();
            }

            return lSiteDetails;
        }

        /// <summary>
        /// get teh email alert details.
        /// </summary>
        /// <returns></returns>

        public List<AlertAuditEntity> GetEmailAlertDetails(int ID,string SiteCode,bool IsProcessed)
        {
            List<AlertAuditEntity> lSiteDetails = null;
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    List<EmailAlertAuditDetailsResult> lstSites = context.GetEmailAlertAuditDetails(ID,SiteCode,IsProcessed).ToList();

                    lSiteDetails = (from s in lstSites
                                    group s by s into g
                                    select new AlertAuditEntity
                                    {
                                        AlertType = g.Key.AlertType,
                                        Content=g.Key.Content,
                                        Date =   (g.Key.Date == null) ? string.Empty : g.Key.Date.ToString(),
                                        Result = g.Key.Result,
                                        SiteCode = g.Key.SiteCode,
                                        SiteName = g.Key.SiteName,
                                        Status = g.Key.Status,
                                        RowColor = g.Key.Status == -1 ? "Lavender" : "White"
                                        
                                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                lSiteDetails = new List<AlertAuditEntity>();
            }

            return lSiteDetails;
        }

        /// <summary>
        /// save the mailserver information.
        /// </summary>
        /// <param name="serverInfo"></param>
        public void SaveMailServerInfo(MailServer serverInfo,string UserName, int UserId, string Oldvalue, string NewValue)
        {
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                    context.InsertMailServerInfo(serverInfo.ServerName, (bool)serverInfo.EnableSSL, serverInfo.UserID, 
                        serverInfo.Password,Convert.ToInt32(serverInfo.Port), UserName,  UserId,  Oldvalue,  NewValue);

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        /// <summary>
        /// get the mail server details
        /// </summary>
        /// <returns>Mail server</returns>
        public MailServer GetMailServerInfo()
        {
           MailServer lMailServer = null;
            try
            {
                using (EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
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
        /// get setting value.
        /// </summary>
        /// <param name="SettingName"></param>
        /// <returns></returns>
        public string GetSetting(string SettingName)
        {
            string setting = string.Empty;
            try
            {
                EnterpriseDataContext context = new EnterpriseDataContext(DatabaseHelper.GetConnectionString());

                context.GetSetting(0, SettingName, string.Empty, ref setting);
                setting = string.IsNullOrEmpty(setting) ? "False" : "True";
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return setting;
        }


        public bool UpdateStatusForPending()
        {
            bool bResult = false;
            try
            {
                using (EnterpriseDataContext helper = new EnterpriseDataContext(DatabaseHelper.GetConnectionString()))
                {
                    helper.UpdateEmailAlertHistoryStatus(null, "Cancelled", 500);
                    bResult = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
           
            return bResult;
        }
    }

    public interface IAlert
    {
        List<AlertTypes> GetAlertTypes();
        void SaveEmailAlerts(List<EmailAlertEntity> eEntity, List<AlertLink> dList, string UserName, int UserId, string Oldvalue, string NewValue);
        List<EmailAlertEntity> GetEmailSubscribers(string AlertTypeName);
        void SaveMailServerInfo(MailServer serverInfo, string UserName, int UserId, string Oldvalue, string NewValue);
        MailServer GetMailServerInfo();
        string GetSetting(string Settingname);
        bool UpdateStatusForPending();
    }
}
