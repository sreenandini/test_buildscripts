using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class TermsBusiness
    {
        private static TermsBusiness _termsBusiness;

        public TermsBusiness() { }

        /// <summary>
        /// To create a new instance for TermsBusiness and return the created instance
        /// </summary>
        /// <returns></returns>
        public static TermsBusiness CreateInstance()
        {
            if (_termsBusiness == null)
                _termsBusiness = new TermsBusiness();

            return _termsBusiness;
        }

        public List<ShareSchedules> GetAllShareSchedules()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetShareScheduleInfo().ToList();
            }
        }

        public List<RentSchedules> GetAllRentSchedules()
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.GetRentScheduleInfo().ToList();
            }
        }

        public TermsProfileResult GetTermsProfileInfoForTermsCalculation(int? termsProfileID)
        {
            TermsProfileResult _termsProfileResult = null;

            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                _termsProfileResult = DataContext.GetTermsProfileInfoForTermsCalculation(termsProfileID).SingleOrDefault();                
            }

            return _termsProfileResult;
        }

        public int UpdateCollectionDetailsWithTermsResults(int? terms_Profile_ID, string terms_Profile_Name, int? terms_Profile_Partners_Supplier_Index, bool? terms_Profile_Partners_Supplier_Use, int? terms_Profile_Partners_Supplier_Cash_Destination,
            int? terms_Profile_Partners_Supplier_Deferred_Remittance, int? terms_Profile_Partners_Supplier_Type, float? terms_Profile_Partners_Supplier_Value, bool? terms_Profile_Partners_Supplier_Value_Guaranteed,
            float terms_Profile_Partners_Supplier_Share, bool? terms_Profile_Partners_Supplier_Share_Guaranteed, int? terms_Profile_Partners_Supplier_Share_Schedule, int? terms_Profile_Partners_Supplier_Rent_Schedule,
            int? terms_Profile_Partners_Supplier_Guarantor, int? terms_Profile_Partners_Supplier_Guarantor_Percentage, int? terms_Profile_Partners_Site_Index, bool? terms_Profile_Partners_Site_Use, int? terms_Profile_Partners_Site_Cash_Destination,
            int? terms_Profile_Partners_Site_Deferred_Remittance, int? terms_Profile_Partners_Site_Type, float? terms_Profile_Partners_Site_Value, bool? terms_Profile_Partners_Site_Value_Guaranteed, float? terms_Profile_Partners_Site_Share,
            bool? terms_Profile_Partners_Site_Share_Guaranteed, int? terms_Profile_Partners_Site_Guarantor, int? terms_Profile_Partners_Site_Guarantor_Percentage, int? terms_Profile_Partners_Group_Index, bool? terms_Profile_Partners_Group_Use,
            int? terms_Profile_Partners_Group_Cash_Destination, int? terms_Profile_Partners_Group_Deferred_Remittance, int? terms_Profile_Partners_Group_Type, float? terms_Profile_Partners_Group_Value, bool? terms_Profile_Partners_Group_Value_Guaranteed,
            float? terms_Profile_Partners_Group_Share, bool? terms_Profile_Partners_Group_Share_Guaranteed, int? terms_Profile_Partners_Group_Guarantor, int? terms_Profile_Partners_Group_Guarantor_Percentage, int? terms_Profile_Partners_Sec_Group_Index,
            bool? terms_Profile_Partners_Sec_Group_Use, int? terms_Profile_Partners_Sec_Group_Cash_Destination, int? terms_Profile_Partners_Sec_Group_Deferred_Remittance, int? terms_Profile_Partners_Sec_Group_Type,
            float? terms_Profile_Partners_Sec_Group_Value, bool? terms_Profile_Partners_Sec_Group_Value_Guaranteed, float? terms_Profile_Partners_Sec_Group_Share, bool? terms_Profile_Partners_Sec_Group_Share_Guaranteed,
            int? terms_Profile_Partners_Sec_Group_Guarantor, int? terms_Profile_Partners_Sec_Group_Guarantor_Percentage, int? terms_Profile_VAT_Output_Index, bool? terms_Profile_VAT_Output_Use, int? terms_Profile_VAT_Output_Cash_Destination,
            int? terms_Profile_VAT_Output_Deferred_Remittance, int? terms_Profile_VAT_Supplier_Index, bool? terms_Profile_VAT_Supplier_Use, int? terms_Profile_VAT_Supplier_Cash_Destination, int? terms_Profile_VAT_Supplier_Deferred_Remittance,
            int? terms_Profile_VAT_Supplier_Paid_By, int? terms_Profile_VAT_Supplier_Guarantor, int? terms_Profile_VAT_Site_Index, bool? terms_Profile_VAT_Site_Use, int? terms_Profile_VAT_Site_Cash_Destination,
            int? terms_Profile_VAT_Site_Deferred_Remittance, int? terms_Profile_VAT_Site_Paid_By, int? terms_Profile_VAT_Site_Guarantor, int? terms_Profile_VAT_Group_Index, bool? terms_Profile_VAT_Group_Use, int? terms_Profile_VAT_Group_Cash_Destination,
            int? terms_Profile_VAT_Group_Deferred_Remittance, int? terms_Profile_VAT_Group_Paid_By, int? terms_Profile_VAT_Group_Guarantor, int? terms_Profile_VAT_Sec_Group_Index, bool? terms_Profile_VAT_Sec_Group_Use,
            int? terms_Profile_VAT_Sec_Group_Cash_Destination, int? terms_Profile_VAT_Sec_Group_Deferred_Remittance, int? terms_Profile_VAT_Sec_Group_Paid_By, int? terms_Profile_VAT_Sec_Group_Guarantor, int? terms_Profile_GPT_Index,
            int? terms_Profile_GPT_Use, int? terms_Profile_GPT_Cash_Destination, int? terms_Profile_GPT_Deferred_Remittance, int? terms_Profile_Other_Licence_Index, bool? terms_Profile_Other_Licence_Use, bool? terms_Profile_Other_Licence_Vat,
            int? terms_Profile_Other_Licence_Cash_Destination, int? terms_Profile_Other_Licence_Deferred_Remittance, float? terms_Profile_Other_Licence_Charge, int? terms_Profile_Other_Licence_Paid_By, int? terms_Profile_Other_Licence_Guarantor,
            int? terms_Profile_Other_Licence_Frequency, int? terms_Profile_Other_Prize_Index, bool? terms_Profile_Other_Prize_Use, bool? terms_Profile_Other_Prize_Vat, int? terms_Profile_Other_Prize_Cash_Destination,
            int? terms_Profile_Other_Prize_Deferred_Remittance, float? terms_Profile_Other_Prize_Charge, int? terms_Profile_Other_Prize_Paid_By, int? terms_Profile_Other_Prize_Guarantor, int? terms_Profile_Other_Prize_Frequency,
            int? terms_Profile_Other_Consultancy_Index, bool? terms_Profile_Other_Consultancy_Use, bool? terms_Profile_Other_Consultancy_Vat, int? terms_Profile_Other_Consultancy_Cash_Destination, int? terms_Profile_Other_Consultancy_Deferred_Remittance,
            float? terms_Profile_Other_Consultancy_Charge, int? terms_Profile_Other_Consultancy_Paid_By, int? terms_Profile_Other_Consultancy_Guarantor, int? terms_Profile_Other_Consultancy_Frequency, int? terms_Profile_Other_Royalty_Index,
            bool? terms_Profile_Other_Royalty_Use, bool? terms_Profile_Other_Royalty_Vat, int? terms_Profile_Other_Royalty_Cash_Destination, int? terms_Profile_Other_Royalty_Deferred_Remittance, float? terms_Profile_Other_Royalty_Charge,
            int? terms_Profile_Other_Royalty_Paid_By, int? terms_Profile_Other_Royalty_Guarantor, int? terms_Profile_Other_Royalty_Frequency, int? terms_Profile_Other_Other1_Index, string terms_Profile_Other_Other1_Name,
            bool? terms_Profile_Other_Other1_Use, bool? terms_Profile_Other_Other1_Vat, int? terms_Profile_Other_Other1_Cash_Destination, int? terms_Profile_Other_Other1_Deferred_Remittance, float? terms_Profile_Other_Other1_Charge,
            int? terms_Profile_Other_Other1_Paid_By, int? terms_Profile_Other_Other1_Guarantor, int? terms_Profile_Other_Other1_Frequency, int? terms_Profile_Other_Other2_Index, string terms_Profile_Other_Other2_Name, bool? terms_Profile_Other_Other2_Use,
            bool? terms_Profile_Other_Other2_Vat, int? terms_Profile_Other_Other2_Cash_Destination, int? terms_Profile_Other_Other2_Deferred_Remittance, float? terms_Profile_Other_Other2_Charge, int? terms_Profile_Other_Other2_Paid_By,
            int? terms_Profile_Other_Other2_Guarantor, int? terms_Profile_Other_Other2_Frequency)
        {
            using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
            {
                return DataContext.UpdateTermsInfoForTermsProfileID(terms_Profile_ID, terms_Profile_Name, terms_Profile_Partners_Supplier_Index, terms_Profile_Partners_Supplier_Use, terms_Profile_Partners_Supplier_Cash_Destination,
                    terms_Profile_Partners_Supplier_Deferred_Remittance, terms_Profile_Partners_Supplier_Type, terms_Profile_Partners_Supplier_Value, terms_Profile_Partners_Supplier_Value_Guaranteed, terms_Profile_Partners_Supplier_Share,
                    terms_Profile_Partners_Supplier_Share_Guaranteed, terms_Profile_Partners_Supplier_Share_Schedule, terms_Profile_Partners_Supplier_Rent_Schedule, terms_Profile_Partners_Supplier_Guarantor, terms_Profile_Partners_Supplier_Guarantor_Percentage,
                    terms_Profile_Partners_Site_Index, terms_Profile_Partners_Site_Use, terms_Profile_Partners_Site_Cash_Destination, terms_Profile_Partners_Site_Deferred_Remittance, terms_Profile_Partners_Site_Type, terms_Profile_Partners_Site_Value,
                    terms_Profile_Partners_Site_Value_Guaranteed, terms_Profile_Partners_Site_Share, terms_Profile_Partners_Site_Share_Guaranteed, terms_Profile_Partners_Site_Guarantor, terms_Profile_Partners_Site_Guarantor_Percentage,
                    terms_Profile_Partners_Group_Index, terms_Profile_Partners_Group_Use, terms_Profile_Partners_Group_Cash_Destination, terms_Profile_Partners_Group_Deferred_Remittance, terms_Profile_Partners_Group_Type, terms_Profile_Partners_Group_Value,
                    terms_Profile_Partners_Group_Value_Guaranteed, terms_Profile_Partners_Group_Share, terms_Profile_Partners_Group_Share_Guaranteed, terms_Profile_Partners_Group_Guarantor, terms_Profile_Partners_Group_Guarantor_Percentage,
                    terms_Profile_Partners_Sec_Group_Index, terms_Profile_Partners_Sec_Group_Use, terms_Profile_Partners_Sec_Group_Cash_Destination, terms_Profile_Partners_Sec_Group_Deferred_Remittance, terms_Profile_Partners_Sec_Group_Type,
                    terms_Profile_Partners_Sec_Group_Value, terms_Profile_Partners_Sec_Group_Value_Guaranteed, terms_Profile_Partners_Sec_Group_Share, terms_Profile_Partners_Sec_Group_Share_Guaranteed, terms_Profile_Partners_Sec_Group_Guarantor,
                    terms_Profile_Partners_Sec_Group_Guarantor_Percentage, terms_Profile_VAT_Output_Index, terms_Profile_VAT_Output_Use, terms_Profile_VAT_Output_Cash_Destination, terms_Profile_VAT_Output_Deferred_Remittance,
                    terms_Profile_VAT_Supplier_Index, terms_Profile_VAT_Supplier_Use, terms_Profile_VAT_Supplier_Cash_Destination, terms_Profile_VAT_Supplier_Deferred_Remittance, terms_Profile_VAT_Supplier_Paid_By, terms_Profile_VAT_Supplier_Guarantor,
                    terms_Profile_VAT_Site_Index, terms_Profile_VAT_Site_Use, terms_Profile_VAT_Site_Cash_Destination, terms_Profile_VAT_Site_Deferred_Remittance, terms_Profile_VAT_Site_Paid_By, terms_Profile_VAT_Site_Guarantor, terms_Profile_VAT_Group_Index,
                    terms_Profile_VAT_Group_Use, terms_Profile_VAT_Group_Cash_Destination, terms_Profile_VAT_Group_Deferred_Remittance, terms_Profile_VAT_Group_Paid_By, terms_Profile_VAT_Group_Guarantor, terms_Profile_VAT_Sec_Group_Index,
                    terms_Profile_VAT_Sec_Group_Use, terms_Profile_VAT_Sec_Group_Cash_Destination, terms_Profile_VAT_Sec_Group_Deferred_Remittance, terms_Profile_VAT_Sec_Group_Paid_By, terms_Profile_VAT_Sec_Group_Guarantor, terms_Profile_GPT_Index,
                    terms_Profile_GPT_Use, terms_Profile_GPT_Cash_Destination, terms_Profile_GPT_Deferred_Remittance, terms_Profile_Other_Licence_Index, terms_Profile_Other_Licence_Use, terms_Profile_Other_Licence_Vat,
                    terms_Profile_Other_Licence_Cash_Destination, terms_Profile_Other_Licence_Deferred_Remittance, terms_Profile_Other_Licence_Charge, terms_Profile_Other_Licence_Paid_By, terms_Profile_Other_Licence_Guarantor,
                    terms_Profile_Other_Licence_Frequency, terms_Profile_Other_Prize_Index, terms_Profile_Other_Prize_Use, terms_Profile_Other_Prize_Vat, terms_Profile_Other_Prize_Cash_Destination, terms_Profile_Other_Prize_Deferred_Remittance,
                    terms_Profile_Other_Prize_Charge, terms_Profile_Other_Prize_Paid_By, terms_Profile_Other_Prize_Guarantor, terms_Profile_Other_Prize_Frequency, terms_Profile_Other_Consultancy_Index, terms_Profile_Other_Consultancy_Use,
                    terms_Profile_Other_Consultancy_Vat, terms_Profile_Other_Consultancy_Cash_Destination, terms_Profile_Other_Consultancy_Deferred_Remittance, terms_Profile_Other_Consultancy_Charge, terms_Profile_Other_Consultancy_Paid_By,
                    terms_Profile_Other_Consultancy_Guarantor, terms_Profile_Other_Consultancy_Frequency, terms_Profile_Other_Royalty_Index, terms_Profile_Other_Royalty_Use, terms_Profile_Other_Royalty_Vat, terms_Profile_Other_Royalty_Cash_Destination,
                    terms_Profile_Other_Royalty_Deferred_Remittance, terms_Profile_Other_Royalty_Charge, terms_Profile_Other_Royalty_Paid_By, terms_Profile_Other_Royalty_Guarantor, terms_Profile_Other_Royalty_Frequency, terms_Profile_Other_Other1_Index,
                    terms_Profile_Other_Other1_Name, terms_Profile_Other_Other1_Use, terms_Profile_Other_Other1_Vat, terms_Profile_Other_Other1_Cash_Destination, terms_Profile_Other_Other1_Deferred_Remittance, terms_Profile_Other_Other1_Charge,
                    terms_Profile_Other_Other1_Paid_By, terms_Profile_Other_Other1_Guarantor, terms_Profile_Other_Other1_Frequency, terms_Profile_Other_Other2_Index, terms_Profile_Other_Other2_Name, terms_Profile_Other_Other2_Use,
                    terms_Profile_Other_Other2_Vat, terms_Profile_Other_Other2_Cash_Destination, terms_Profile_Other_Other2_Deferred_Remittance, terms_Profile_Other_Other2_Charge, terms_Profile_Other_Other2_Paid_By, terms_Profile_Other_Other2_Guarantor,
                    terms_Profile_Other_Other2_Frequency);
            }
        }
    }
}
