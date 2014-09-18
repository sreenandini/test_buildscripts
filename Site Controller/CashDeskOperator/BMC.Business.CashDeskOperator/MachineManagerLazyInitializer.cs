using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Business.CashDeskOperator;

namespace BMC.Business.CashDeskOperator
{
    public class MachineManagerLazyInitializer
    {
        MachineManagerInterface machineManagerInterface = null;
        private object _objLock = new object();

        public MachineManagerLazyInitializer() { }

        public MachineManagerInterface GetMachineManager()
        {
            if (machineManagerInterface == null)
            {
                lock (_objLock)
                {
                    if (machineManagerInterface == null)
                    {
                        machineManagerInterface = new MachineManagerInterface();
                    }
                }
            }
            return machineManagerInterface;
        }

        public void ReleaseMachineManager()
        {
            if (machineManagerInterface != null)
            {
                lock (_objLock)
                {
                    if (machineManagerInterface != null)
                    {
                        machineManagerInterface.Dispose();
                        machineManagerInterface = null;
                    }
                }
            }
        }
    }
}
