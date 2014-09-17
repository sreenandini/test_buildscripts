using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Diagnostics;
using BMC.EBSComms.Contracts.Dto;

namespace BMC.EBSComms.Server
{
    public partial class EBSCommServer
    {
        internal bool Parse_s2sBody(s2sMessage target, object source)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "Parse_s2sBody");
            bool result = default(bool);
            s2sBody s2s = source as s2sBody;

            try
            {
                this.InvokeWork_FromEBS(target, s2s);
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }

            return result;
        }
    }
}
