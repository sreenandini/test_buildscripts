using BMC.BusinessClasses.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.BusinessClasses.Interfaces
{
    public interface IBMCUtilities
    {
        bool ProcessEmailAlertData();
        bool ResetAlertProcessHistory();
        void ProcessAutoCalendar();
        void Stop();
    }
    public interface IAlertEngine
    {
        bool SendMail(AlertEntity alert);
    }
}
