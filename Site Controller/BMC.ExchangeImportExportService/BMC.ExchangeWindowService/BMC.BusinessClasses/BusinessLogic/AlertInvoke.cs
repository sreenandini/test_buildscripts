using BMC.Common.ExceptionManagement;
using BMC.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BMC.BusinessClasses.BusinessLogic
{
   
  public interface IAlert
    {
        List<AlertDetails> GetAlertDetails();

        void Init();

        void DoWork();

        void UnInit();

    }
}
