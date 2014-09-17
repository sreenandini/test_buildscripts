using BMC.DataExportToSite.BusinessLogic;

namespace BMC.DataExportToSite
{
    public class ObjectFactory
    {
        private static BMCEnterpriseExport _bmcEnterpriseImportExport;
        public static Interfaces.IBMCEnterpriseExport GetEnterpriseFactoryObject()
        {
            if (_bmcEnterpriseImportExport == null)
                _bmcEnterpriseImportExport = new BMCEnterpriseExport();

            return _bmcEnterpriseImportExport;
        }

        public static Interfaces.IBMCEnterpriseExport GetEnterpriseFactoryObject(bool value)
        {
            if (_bmcEnterpriseImportExport == null)
                _bmcEnterpriseImportExport = new BMCEnterpriseExport(value);

            return _bmcEnterpriseImportExport;
        }
    }
}
