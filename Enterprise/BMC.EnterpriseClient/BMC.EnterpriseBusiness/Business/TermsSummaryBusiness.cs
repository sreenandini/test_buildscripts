using System.Collections.Generic;
using System.Linq;
using BMC.EnterpriseDataAccess;
using BMC.Common;

namespace BMC.EnterpriseBusiness.Business
{
    public class TermsSummaryBusiness
    {
        string AnyValue =ResourceExtensions.GetResourceTextByKey(null,"Key_Any");
        string NoneValue = ResourceExtensions.GetResourceTextByKey(null, "Key_NoneHyphen");     
   
        private static TermsSummaryBusiness _termsSummary;

        /// <summary>
        /// create an instance for the class.
        /// </summary>
        /// <returns></returns>
        public static TermsSummaryBusiness CreateInstance()
        {
            if (_termsSummary == null)
            {
                _termsSummary = new TermsSummaryBusiness();
            }
            return _termsSummary;
        }


        public List<SubCompanyNames> GetSubCompanyNames()
        {
            List<SubCompanyNames> subCompaniesList = new List<SubCompanyNames>();
            subCompaniesList.Insert(0, new SubCompanyNames
            {
                SubCompanyId = 0,
                SubCompanyName = AnyValue
            });
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                subCompaniesList.AddRange(DataContext.GetSubCompanyNames().ToList());
                return subCompaniesList;
            }
        }


        public List<OperatorNames> GetOperatorNames()
        {
            List<OperatorNames> operatorNames = new List<OperatorNames>();
            operatorNames.Insert(0, new OperatorNames
            {
                OperatorId = 0,
                OperatorName = AnyValue
            });
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                operatorNames.AddRange(DataContext.GetOperatorNames().ToList());
                return operatorNames;
            }
        }


        public List<DepotNames> GetDepotNames(int supplierId)
        {
            List<DepotNames> depotNames = new List<DepotNames>();
            depotNames.Insert(0, new DepotNames
            {
                DepotId = 0,
                DepotName = AnyValue
            });
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                depotNames.AddRange(DataContext.GetDepotNames(supplierId).ToList());
                return depotNames;
            }
        }

        public List<MachineTypes> GetMachineTypes()
        {
            List<MachineTypes> machineTypes = new List<MachineTypes>();
            machineTypes.Insert(0, new MachineTypes
            {
                MachineTypeId = 0,
                MachineTypeCode = AnyValue
            });
            machineTypes.Insert(0, new MachineTypes
            {
                MachineTypeId = -1,
                MachineTypeCode = NoneValue
            });
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                machineTypes.AddRange(DataContext.GetMachineTypes().ToList());
                return machineTypes;
            }
        }

        public List<TermsSummaryList> GetTermsSummaryList(int operatorId, int depotId, int machineId, int subCompanyId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetTermsSummaryList(operatorId, depotId, machineId, subCompanyId).ToList();
            }
        }

        public int UpdateTermsShareAndRent(int id, float supplier, float rent, float site, float owner, float licence)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.UpdateTermsShareAndRent(id, supplier, rent, site, owner, licence);
            }
        }
    }

}

