using BMC.BusinessClasses.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMC.BusinessClasses
{
    public static class UtitlityObjectFactory
    {
        private static BmcUtilitiesExportImport _BmcUtilitiesExportImport;

        public static Interfaces.IBMCUtilities GetAlertFactoryObject()
        {
            if (_BmcUtilitiesExportImport == null)
                _BmcUtilitiesExportImport = new BmcUtilitiesExportImport();

            return _BmcUtilitiesExportImport;
        }

        public static Interfaces.IBMCUtilities GetEnterpriseFactoryObject(bool value)
        {
            if (_BmcUtilitiesExportImport == null)
                _BmcUtilitiesExportImport = new BmcUtilitiesExportImport(value);

            return _BmcUtilitiesExportImport;
        }
    }
}
