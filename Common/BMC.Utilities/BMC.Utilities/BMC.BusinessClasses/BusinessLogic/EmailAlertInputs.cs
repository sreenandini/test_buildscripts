using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using BMC.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.BusinessClasses.BusinessLogic
{
    public class EmailAlertInputs:IDisposable
    {
       

        public EmailAlertInputs()
        { }

        /// <summary>
        /// get teh list of email subscribers for sending alerts.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, MailMessageGroup> GetEmailSubScribers()
        {
             Dictionary<string ,MailMessageGroup> dEmailConfigurations = null;
             try
             {
                 using (DataHelper context = new DataHelper(DatabaseHelper.GetConnectionString()))
                 {
                     List<EmailSubscriberDetailsResult> subscribers = context.GetEmailAlertSubscribers(string.Empty).ToList();

                     dEmailConfigurations = (from s in subscribers
                                             group s by s into g
                                             select new // Selection criteria
                                             {
                                                 key = g.Key.TypeName,
                                                 val = new MailMessageGroup
                                                 {
                                                     FromAddress = g.Key.FromMail,
                                                     ToList = g.Key.ToMail,
                                                     CCList = g.Key.CCMail,
                                                     BCCList = g.Key.BCCMail,
                                                     Encoding = System.Text.Encoding.ASCII.ToString(),
                                                     Subject = g.Key.SUBJECT
                                                 }
                                             }).ToDictionary(k => k.key, v => v.val);

                 }
             }
             catch (Exception ex)
             { ExceptionManager.Publish(ex); }
             finally
             { }
            return dEmailConfigurations;
        }

        public void Dispose()
        {
           
        }
    }
}
