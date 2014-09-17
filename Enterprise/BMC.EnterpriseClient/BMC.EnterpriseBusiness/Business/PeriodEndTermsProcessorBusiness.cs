using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using BMC.EnterpriseDataAccess;

namespace BMC.EnterpriseBusiness.Business
{
    public class PeriodEndTermsProcessorBusiness
    {
        private static PeriodEndTermsProcessorBusiness _periodEndTermsProcessorBusiness;

        /// <summary>
        /// create an instance for the class.
        /// </summary>
        /// <returns></returns>
        public static PeriodEndTermsProcessorBusiness CreateInstance()
        {
            if (_periodEndTermsProcessorBusiness == null)
            {
                _periodEndTermsProcessorBusiness = new PeriodEndTermsProcessorBusiness();
            }
            return _periodEndTermsProcessorBusiness;
        }

        public List<AvailableSchedules> GetAvailableSchedules()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetAvailableSchedules().ToList();
            }
        }

        public DateTime? GetFirstOpenPeriodEnd(string periodEndDate)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return (DataContext.GetFirstOpenPeriodEndDate(periodEndDate).First() as PeriodEndDate).myDate;
            }
        }

        public List<SubCompanyDetails> GetSubCompanyResult(string periodEndDate)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetSubCompanyResult(periodEndDate).ToList();
            }
        }

        public List<CompanyExceptionCollection> GetCompanyExceptionCollection(int periodEndId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetCompanyExceptionCollection(periodEndId).ToList();
            }
        }

        public int UpdateCascadeSubCompany(string cascadeOptions, string value, string cascadeType, string subCompanyId, char setAsDefault, int userId, string userName, int moduleId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.CascadeUpdateSubCompany(cascadeOptions, value, cascadeType, Convert.ToInt32(subCompanyId), setAsDefault, userId, userName, moduleId, string.Empty);
            }
        }

        public List<CollectionIds> GetPeriodEndCollections(int? periodEndId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetCollectionsForPeriodEnd(periodEndId).ToList();
            }
        }

        public int ConfirmPeriodEnd(int? periodEndId, int? subCompanyId, int? periodEndDocNo)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.ConfirmPeriodEnd(periodEndId, subCompanyId, periodEndDocNo);
            }

        }

        public int BatchExportHistory(string reference, string type, int? siteId)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.usp_Export_History(reference, type, siteId);
            }

        }

        public int CreatePeriodEndDocNo(ref string period_End_Doc_no)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.CreatePeriodEndDocNo(ref period_End_Doc_no);
            }
        }
    }
}
