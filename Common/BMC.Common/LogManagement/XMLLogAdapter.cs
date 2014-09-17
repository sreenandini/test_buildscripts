using System;
using System.Collections.Generic;
using System.Text;
using BMC.Common.Interfaces;
namespace BMC.Common.LogManagement
{
    /// <summary>
    /// This class has functions to Enter Logs in XML
    /// Revision History:
    /// Date 04-Jan-2008    Created     Rakesh Marwaha
    /// </summary>
    class XMLLogAdapter : ILoggingAdapter
    {
        #region ILoggingAdapter Members

        public void WriteLog(string message, int level)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void ILoggingAdapter.WriteLog(string fileName, string message, int level)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
