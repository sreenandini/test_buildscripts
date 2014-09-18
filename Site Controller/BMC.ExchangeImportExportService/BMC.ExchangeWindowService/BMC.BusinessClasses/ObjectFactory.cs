using BMC.BusinessClasses.BusinessLogic;

namespace BMC.BusinessClasses
{
    public static class ObjectFactory
    {
        private static BMCExchangeImportExport _bmcExchangeImportExport;

        public static Interfaces.IBMCExchangeImportExport GetExchangeImportExportObject()
        {
            if (_bmcExchangeImportExport == null)
                _bmcExchangeImportExport = new BMCExchangeImportExport();
            return _bmcExchangeImportExport;
        }

    }
}
