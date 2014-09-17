using BMC.Common.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class AlertViewModel
    {

    }

  
    public interface ICommand
    {
         void Execute();
         List<AlertAuditEntity> ExecuteAlerts();
         void UnExecute();

    }

    public class Command<T> : ICommand
    {
        private T target;
        private Action<T> command;
        private Func<T, List<AlertAuditEntity>> commandAlert;

        public Command(T target, Action<T> command, Func<T, List<AlertAuditEntity>> commandAlert)
        {
            this.target = target;
            this.command = command;
            this.commandAlert = commandAlert;

        }

        public void Execute()
        {
            command(target);
        }

        public List<AlertAuditEntity> ExecuteAlerts()
        {
            return commandAlert(target);
        }


        public void UnExecute()
        {
            throw new NotImplementedException();
        }

    }

    public class SimpleRemoteControl
    {
        private ICommand slot;

        public void SetCommand(ICommand command)
        {
            slot = command;
        }

        public List<AlertAuditEntity> ButtonWasPressed()
        {
             return slot.ExecuteAlerts();
        }
    }

    

    public class Alert
    {
        public AlertSystemBiz _alertBiz = null;

        public Alert()
        {
            _alertBiz = AlertSystemBiz.CreateInstance();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AlertAuditEntity> GetAlertDetails(int ID, string SiteCode,bool IsProcessed)
        {
            List<AlertAuditEntity> lstAudit = null;
            try
            {
                lstAudit = _alertBiz.GetEmailAlertDetails(ID,SiteCode,IsProcessed);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new List<AlertAuditEntity>();
            }

            return lstAudit;
        }
    }

    public static class MyExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, Func<bool> breakOn)
        {
            foreach (var item in enumerable)
            {
                action(item);
                if (breakOn())
                {
                    break;
                }
            }
        }
    }

}
