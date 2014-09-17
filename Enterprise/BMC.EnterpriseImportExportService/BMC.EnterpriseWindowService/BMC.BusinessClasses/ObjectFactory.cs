using BMC.BusinessClasses.BusinessLogic;

namespace BMC.BusinessClasses
{
    public static class ObjectFactory
    {
        private static BMCEnterpriseImportExport _bmcEnterpriseImportExport;

        public static Interfaces.IBMCEnterpriseExportImport GetEnterpriseFactoryObject()
        {
            if (_bmcEnterpriseImportExport == null)
                _bmcEnterpriseImportExport = new BMCEnterpriseImportExport();
            
            return _bmcEnterpriseImportExport;
        }

        public static Interfaces.IBMCEnterpriseExportImport GetEnterpriseFactoryObject(bool value)
        {
            if (_bmcEnterpriseImportExport == null)
                _bmcEnterpriseImportExport = new BMCEnterpriseImportExport(value);

            return _bmcEnterpriseImportExport;
        }
    }

    public static class UtitlityObjectFactory
    {
        private static BmcUtilitiesExportImport _BmcUtilitiesExportImport;

        public static Interfaces.IBMCUtilities GetEnterpriseFactoryObject(bool value)
        {
            if (_BmcUtilitiesExportImport == null)
                _BmcUtilitiesExportImport = new BmcUtilitiesExportImport(value);

            return _BmcUtilitiesExportImport;
        }
    }
}
