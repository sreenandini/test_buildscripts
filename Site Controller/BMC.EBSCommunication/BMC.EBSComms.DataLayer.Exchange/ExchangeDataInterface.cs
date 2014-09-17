using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using BMC.Common.ConfigurationManagement;
using BMC.Common.Persistence;
using BMC.Common.Utilities;
using BMC.CoreLib;

namespace BMC.EBSComms.DataLayer.Exchange
{
    [Export(typeof(IDataInterface))]
    [Export("DataInterface", typeof(IDataInterface))]
    public partial class ExchangeDataInterface : DataInterfaceBase, IDataInterface
    {
        public ExchangeDataInterface()
        {
            _connectionString = DatabaseHelper.GetExchangeConnectionString();
        }
    }
}
