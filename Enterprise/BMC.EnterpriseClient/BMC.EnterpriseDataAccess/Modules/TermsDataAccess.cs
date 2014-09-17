using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BMC.EnterpriseDataAccess
{
    public partial class EnterpriseDataContext
    {
        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetTermsInfoForTermsProfileID")]
        public ISingleResult<TermsProfileResult> GetTermsProfileInfoForTermsCalculation([global::System.Data.Linq.Mapping.ParameterAttribute(Name = "TermsProfileID", DbType = "Int")] System.Nullable<int> termsProfileID)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), termsProfileID);
            return ((ISingleResult<TermsProfileResult>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.usp_UpdateTermsInfoForTermsProfileID")]
        public int UpdateTermsInfoForTermsProfileID(
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_ID", DbType = "Int")] System.Nullable<int> terms_Profile_ID,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Name", DbType = "VarChar(50)")] string terms_Profile_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Supplier_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Type", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Type,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Value", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Supplier_Value,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Value_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Supplier_Value_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Share", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Supplier_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Supplier_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Share_Schedule", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Share_Schedule,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Rent_Schedule", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Rent_Schedule,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Supplier_Guarantor_Percentage", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Supplier_Guarantor_Percentage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Site_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Type", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Type,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Value", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Site_Value,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Value_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Site_Value_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Share", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Site_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Site_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Site_Guarantor_Percentage", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Site_Guarantor_Percentage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Group_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Type", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Type,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Value", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Group_Value,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Value_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Group_Value_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Share", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Group_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Group_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Group_Guarantor_Percentage", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Group_Guarantor_Percentage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Sec_Group_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Type", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Type,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Value", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Sec_Group_Value,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Value_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Sec_Group_Value_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Share", DbType = "Real")] System.Nullable<float> terms_Profile_Partners_Sec_Group_Share,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Share_Guaranteed", DbType = "Bit")] System.Nullable<bool> terms_Profile_Partners_Sec_Group_Share_Guaranteed,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Partners_Sec_Group_Guarantor_Percentage", DbType = "Int")] System.Nullable<int> terms_Profile_Partners_Sec_Group_Guarantor_Percentage,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Output_Index", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Output_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Output_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_VAT_Output_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Output_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Output_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Output_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Output_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Index", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Supplier_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_VAT_Supplier_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Supplier_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Supplier_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Supplier_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Supplier_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Supplier_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Index", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Site_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_VAT_Site_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Site_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Site_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Site_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Site_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Site_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Index", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Group_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_VAT_Group_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Group_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Group_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Group_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Group_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Group_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Index", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Sec_Group_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_VAT_Sec_Group_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Sec_Group_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Sec_Group_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Sec_Group_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_VAT_Sec_Group_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_VAT_Sec_Group_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_GPT_Index", DbType = "Int")] System.Nullable<int> terms_Profile_GPT_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_GPT_Use", DbType = "Int")] System.Nullable<int> terms_Profile_GPT_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_GPT_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_GPT_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_GPT_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_GPT_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Licence_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Licence_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Licence_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Licence_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Licence_Frequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Prize_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Prize_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Prize_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Prize_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Prize_Frequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Consultancy_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Consultancy_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Consultancy_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Consultancy_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Consultancy_Frequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Royalty_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Royalty_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Royalty_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Royalty_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Royalty_Frequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Name", DbType = "VarChar(50)")] string terms_Profile_Other_Other1_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Other1_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Other1_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Other1_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other1_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other1_Frequency,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Index", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Index,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Name", DbType = "VarChar(50)")] string terms_Profile_Other_Other2_Name,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Use", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Other2_Use,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Vat", DbType = "Bit")] System.Nullable<bool> terms_Profile_Other_Other2_Vat,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Cash_Destination", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Cash_Destination,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Deferred_Remittance", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Deferred_Remittance,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Charge", DbType = "Real")] System.Nullable<float> terms_Profile_Other_Other2_Charge,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Paid_By", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Paid_By,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Guarantor", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Guarantor,
                    [global::System.Data.Linq.Mapping.ParameterAttribute(Name = "Terms_Profile_Other_Other2_Frequency", DbType = "Int")] System.Nullable<int> terms_Profile_Other_Other2_Frequency)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), terms_Profile_ID, terms_Profile_Name, terms_Profile_Partners_Supplier_Index, terms_Profile_Partners_Supplier_Use, terms_Profile_Partners_Supplier_Cash_Destination, terms_Profile_Partners_Supplier_Deferred_Remittance, terms_Profile_Partners_Supplier_Type, terms_Profile_Partners_Supplier_Value, terms_Profile_Partners_Supplier_Value_Guaranteed, terms_Profile_Partners_Supplier_Share, terms_Profile_Partners_Supplier_Share_Guaranteed, terms_Profile_Partners_Supplier_Share_Schedule, terms_Profile_Partners_Supplier_Rent_Schedule, terms_Profile_Partners_Supplier_Guarantor, terms_Profile_Partners_Supplier_Guarantor_Percentage, terms_Profile_Partners_Site_Index, terms_Profile_Partners_Site_Use, terms_Profile_Partners_Site_Cash_Destination, terms_Profile_Partners_Site_Deferred_Remittance, terms_Profile_Partners_Site_Type, terms_Profile_Partners_Site_Value, terms_Profile_Partners_Site_Value_Guaranteed, terms_Profile_Partners_Site_Share, terms_Profile_Partners_Site_Share_Guaranteed, terms_Profile_Partners_Site_Guarantor, terms_Profile_Partners_Site_Guarantor_Percentage, terms_Profile_Partners_Group_Index, terms_Profile_Partners_Group_Use, terms_Profile_Partners_Group_Cash_Destination, terms_Profile_Partners_Group_Deferred_Remittance, terms_Profile_Partners_Group_Type, terms_Profile_Partners_Group_Value, terms_Profile_Partners_Group_Value_Guaranteed, terms_Profile_Partners_Group_Share, terms_Profile_Partners_Group_Share_Guaranteed, terms_Profile_Partners_Group_Guarantor, terms_Profile_Partners_Group_Guarantor_Percentage, terms_Profile_Partners_Sec_Group_Index, terms_Profile_Partners_Sec_Group_Use, terms_Profile_Partners_Sec_Group_Cash_Destination, terms_Profile_Partners_Sec_Group_Deferred_Remittance, terms_Profile_Partners_Sec_Group_Type, terms_Profile_Partners_Sec_Group_Value, terms_Profile_Partners_Sec_Group_Value_Guaranteed, terms_Profile_Partners_Sec_Group_Share, terms_Profile_Partners_Sec_Group_Share_Guaranteed, terms_Profile_Partners_Sec_Group_Guarantor, terms_Profile_Partners_Sec_Group_Guarantor_Percentage, terms_Profile_VAT_Output_Index, terms_Profile_VAT_Output_Use, terms_Profile_VAT_Output_Cash_Destination, terms_Profile_VAT_Output_Deferred_Remittance, terms_Profile_VAT_Supplier_Index, terms_Profile_VAT_Supplier_Use, terms_Profile_VAT_Supplier_Cash_Destination, terms_Profile_VAT_Supplier_Deferred_Remittance, terms_Profile_VAT_Supplier_Paid_By, terms_Profile_VAT_Supplier_Guarantor, terms_Profile_VAT_Site_Index, terms_Profile_VAT_Site_Use, terms_Profile_VAT_Site_Cash_Destination, terms_Profile_VAT_Site_Deferred_Remittance, terms_Profile_VAT_Site_Paid_By, terms_Profile_VAT_Site_Guarantor, terms_Profile_VAT_Group_Index, terms_Profile_VAT_Group_Use, terms_Profile_VAT_Group_Cash_Destination, terms_Profile_VAT_Group_Deferred_Remittance, terms_Profile_VAT_Group_Paid_By, terms_Profile_VAT_Group_Guarantor, terms_Profile_VAT_Sec_Group_Index, terms_Profile_VAT_Sec_Group_Use, terms_Profile_VAT_Sec_Group_Cash_Destination, terms_Profile_VAT_Sec_Group_Deferred_Remittance, terms_Profile_VAT_Sec_Group_Paid_By, terms_Profile_VAT_Sec_Group_Guarantor, terms_Profile_GPT_Index, terms_Profile_GPT_Use, terms_Profile_GPT_Cash_Destination, terms_Profile_GPT_Deferred_Remittance, terms_Profile_Other_Licence_Index, terms_Profile_Other_Licence_Use, terms_Profile_Other_Licence_Vat, terms_Profile_Other_Licence_Cash_Destination, terms_Profile_Other_Licence_Deferred_Remittance, terms_Profile_Other_Licence_Charge, terms_Profile_Other_Licence_Paid_By, terms_Profile_Other_Licence_Guarantor, terms_Profile_Other_Licence_Frequency, terms_Profile_Other_Prize_Index, terms_Profile_Other_Prize_Use, terms_Profile_Other_Prize_Vat, terms_Profile_Other_Prize_Cash_Destination, terms_Profile_Other_Prize_Deferred_Remittance, terms_Profile_Other_Prize_Charge, terms_Profile_Other_Prize_Paid_By, terms_Profile_Other_Prize_Guarantor, terms_Profile_Other_Prize_Frequency, terms_Profile_Other_Consultancy_Index, terms_Profile_Other_Consultancy_Use, terms_Profile_Other_Consultancy_Vat, terms_Profile_Other_Consultancy_Cash_Destination, terms_Profile_Other_Consultancy_Deferred_Remittance, terms_Profile_Other_Consultancy_Charge, terms_Profile_Other_Consultancy_Paid_By, terms_Profile_Other_Consultancy_Guarantor, terms_Profile_Other_Consultancy_Frequency, terms_Profile_Other_Royalty_Index, terms_Profile_Other_Royalty_Use, terms_Profile_Other_Royalty_Vat, terms_Profile_Other_Royalty_Cash_Destination, terms_Profile_Other_Royalty_Deferred_Remittance, terms_Profile_Other_Royalty_Charge, terms_Profile_Other_Royalty_Paid_By, terms_Profile_Other_Royalty_Guarantor, terms_Profile_Other_Royalty_Frequency, terms_Profile_Other_Other1_Index, terms_Profile_Other_Other1_Name, terms_Profile_Other_Other1_Use, terms_Profile_Other_Other1_Vat, terms_Profile_Other_Other1_Cash_Destination, terms_Profile_Other_Other1_Deferred_Remittance, terms_Profile_Other_Other1_Charge, terms_Profile_Other_Other1_Paid_By, terms_Profile_Other_Other1_Guarantor, terms_Profile_Other_Other1_Frequency, terms_Profile_Other_Other2_Index, terms_Profile_Other_Other2_Name, terms_Profile_Other_Other2_Use, terms_Profile_Other_Other2_Vat, terms_Profile_Other_Other2_Cash_Destination, terms_Profile_Other_Other2_Deferred_Remittance, terms_Profile_Other_Other2_Charge, terms_Profile_Other_Other2_Paid_By, terms_Profile_Other_Other2_Guarantor, terms_Profile_Other_Other2_Frequency);
            return ((int)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetShareScheduleInfo")]
        public ISingleResult<ShareSchedules> GetShareScheduleInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<ShareSchedules>)(result.ReturnValue));
        }

        [global::System.Data.Linq.Mapping.FunctionAttribute(Name = "dbo.rsp_GetRentScheduleInfo")]
        public ISingleResult<RentSchedules> GetRentScheduleInfo()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<RentSchedules>)(result.ReturnValue));
        }
    }

    public partial class TermsProfileResult
    {

        private int _Terms_Profile_ID;

        private System.Nullable<int> _Terms_Group_ID;

        private System.Nullable<int> _Machine_Type_ID;

        private string _Terms_Profile_Name;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Supplier_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Supplier_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Supplier_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Share_Schedule;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Rent_Schedule;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Supplier_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Site_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Site_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Site_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Site_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Group_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Group_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Group_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Group_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Index;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Use;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Type;

        private System.Nullable<float> _Terms_Profile_Partners_Sec_Group_Value;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Value_Guaranteed;

        private System.Nullable<float> _Terms_Profile_Partners_Sec_Group_Share;

        private System.Nullable<bool> _Terms_Profile_Partners_Sec_Group_Share_Guaranteed;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Partners_Sec_Group_Guarantor_Percentage;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Output_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Output_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Supplier_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Supplier_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Site_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Site_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Group_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Index;

        private System.Nullable<bool> _Terms_Profile_VAT_Sec_Group_Use;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Deferred_Remittance;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Paid_By;

        private System.Nullable<int> _Terms_Profile_VAT_Sec_Group_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Licence_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Licence_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Licence_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Licence_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Prize_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Prize_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Prize_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Prize_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Consultancy_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Consultancy_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Consultancy_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Consultancy_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Index;

        private System.Nullable<bool> _Terms_Profile_Other_Royalty_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Royalty_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Royalty_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Royalty_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Index;

        private string _Terms_Profile_Other_Other1_Name;

        private System.Nullable<bool> _Terms_Profile_Other_Other1_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Other1_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Other1_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Other1_Frequency;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Index;

        private string _Terms_Profile_Other_Other2_Name;

        private System.Nullable<bool> _Terms_Profile_Other_Other2_Use;

        private System.Nullable<bool> _Terms_Profile_Other_Other2_Vat;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Cash_Destination;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Deferred_Remittance;

        private System.Nullable<float> _Terms_Profile_Other_Other2_Charge;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Paid_By;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Guarantor;

        private System.Nullable<int> _Terms_Profile_Other_Other2_Frequency;

        private System.Nullable<bool> _Terms_Profile_London_Rent_Use;

        private System.Nullable<float> _Terms_Profile_London_Rent_Charge;

        private int _Terms_Profile_GPT_Index;

        private int _Terms_Profile_GPT_Use;

        private int _Terms_Profile_GPT_Cash_Destination;

        private int _Terms_Profile_GPT_Deferred_Remittance;

        private string _Machine_Type_Code;

        private string _Terms_Group_Name;

        public TermsProfileResult()
        {
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_ID", DbType = "Int NOT NULL")]
        public int Terms_Profile_ID
        {
            get
            {
                return this._Terms_Profile_ID;
            }
            set
            {
                if ((this._Terms_Profile_ID != value))
                {
                    this._Terms_Profile_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_ID", DbType = "Int")]
        public System.Nullable<int> Terms_Group_ID
        {
            get
            {
                return this._Terms_Group_ID;
            }
            set
            {
                if ((this._Terms_Group_ID != value))
                {
                    this._Terms_Group_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_ID", DbType = "Int")]
        public System.Nullable<int> Machine_Type_ID
        {
            get
            {
                return this._Machine_Type_ID;
            }
            set
            {
                if ((this._Machine_Type_ID != value))
                {
                    this._Machine_Type_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Name
        {
            get
            {
                return this._Terms_Profile_Name;
            }
            set
            {
                if ((this._Terms_Profile_Name != value))
                {
                    this._Terms_Profile_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Index != value))
                {
                    this._Terms_Profile_Partners_Supplier_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Use != value))
                {
                    this._Terms_Profile_Partners_Supplier_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Supplier_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Supplier_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Type != value))
                {
                    this._Terms_Profile_Partners_Supplier_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Supplier_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Value != value))
                {
                    this._Terms_Profile_Partners_Supplier_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Supplier_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Supplier_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Supplier_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Share_Schedule", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Share_Schedule
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Share_Schedule;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Share_Schedule != value))
                {
                    this._Terms_Profile_Partners_Supplier_Share_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Rent_Schedule", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Rent_Schedule
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Rent_Schedule;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Rent_Schedule != value))
                {
                    this._Terms_Profile_Partners_Supplier_Rent_Schedule = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Supplier_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Supplier_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Supplier_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Supplier_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Supplier_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Supplier_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Index != value))
                {
                    this._Terms_Profile_Partners_Site_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Use != value))
                {
                    this._Terms_Profile_Partners_Site_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Site_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Site_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Type != value))
                {
                    this._Terms_Profile_Partners_Site_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Site_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Value != value))
                {
                    this._Terms_Profile_Partners_Site_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Site_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Site_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Share != value))
                {
                    this._Terms_Profile_Partners_Site_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Site_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Site_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Site_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Site_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Site_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Site_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Site_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Site_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Index != value))
                {
                    this._Terms_Profile_Partners_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Use != value))
                {
                    this._Terms_Profile_Partners_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Type != value))
                {
                    this._Terms_Profile_Partners_Group_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Group_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Value != value))
                {
                    this._Terms_Profile_Partners_Group_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Group_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Group_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Share != value))
                {
                    this._Terms_Profile_Partners_Group_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Group_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Group_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Group_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Group_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Group_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Group_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Group_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Index
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Index != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Use
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Use != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Type", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Type
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Type;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Type != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Type = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Value", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Sec_Group_Value
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Value;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Value != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Value = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Value_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Value_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Value_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Share", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Partners_Sec_Group_Share
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Share;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Share != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Share = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Share_Guaranteed", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Partners_Sec_Group_Share_Guaranteed
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Share_Guaranteed = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Guarantor != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Partners_Sec_Group_Guarantor_Percentage", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Partners_Sec_Group_Guarantor_Percentage
        {
            get
            {
                return this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage;
            }
            set
            {
                if ((this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage != value))
                {
                    this._Terms_Profile_Partners_Sec_Group_Guarantor_Percentage = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Index != value))
                {
                    this._Terms_Profile_VAT_Output_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Output_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Use != value))
                {
                    this._Terms_Profile_VAT_Output_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Output_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Output_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Output_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Output_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Output_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Output_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Index != value))
                {
                    this._Terms_Profile_VAT_Supplier_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Supplier_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Use != value))
                {
                    this._Terms_Profile_VAT_Supplier_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Supplier_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Supplier_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Supplier_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Supplier_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Supplier_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Supplier_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Supplier_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Supplier_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Index != value))
                {
                    this._Terms_Profile_VAT_Site_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Site_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Use != value))
                {
                    this._Terms_Profile_VAT_Site_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Site_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Site_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Site_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Site_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Site_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Site_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Site_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Site_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Index != value))
                {
                    this._Terms_Profile_VAT_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Group_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Use != value))
                {
                    this._Terms_Profile_VAT_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Group_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Group_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Index
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Index;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Index != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_VAT_Sec_Group_Use
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Use;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Use != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Cash_Destination != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Paid_By
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Paid_By != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_VAT_Sec_Group_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_VAT_Sec_Group_Guarantor
        {
            get
            {
                return this._Terms_Profile_VAT_Sec_Group_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_VAT_Sec_Group_Guarantor != value))
                {
                    this._Terms_Profile_VAT_Sec_Group_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Index
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Index != value))
                {
                    this._Terms_Profile_Other_Licence_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Licence_Use
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Use != value))
                {
                    this._Terms_Profile_Other_Licence_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Licence_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Vat != value))
                {
                    this._Terms_Profile_Other_Licence_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Licence_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Licence_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Licence_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Charge != value))
                {
                    this._Terms_Profile_Other_Licence_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Paid_By != value))
                {
                    this._Terms_Profile_Other_Licence_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Guarantor != value))
                {
                    this._Terms_Profile_Other_Licence_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Licence_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Licence_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Licence_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Licence_Frequency != value))
                {
                    this._Terms_Profile_Other_Licence_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Index
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Index != value))
                {
                    this._Terms_Profile_Other_Prize_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Prize_Use
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Use != value))
                {
                    this._Terms_Profile_Other_Prize_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Prize_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Vat != value))
                {
                    this._Terms_Profile_Other_Prize_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Prize_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Prize_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Prize_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Charge != value))
                {
                    this._Terms_Profile_Other_Prize_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Paid_By != value))
                {
                    this._Terms_Profile_Other_Prize_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Guarantor != value))
                {
                    this._Terms_Profile_Other_Prize_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Prize_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Prize_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Prize_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Prize_Frequency != value))
                {
                    this._Terms_Profile_Other_Prize_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Index
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Index != value))
                {
                    this._Terms_Profile_Other_Consultancy_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Consultancy_Use
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Use != value))
                {
                    this._Terms_Profile_Other_Consultancy_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Consultancy_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Vat != value))
                {
                    this._Terms_Profile_Other_Consultancy_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Consultancy_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Consultancy_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Consultancy_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Charge != value))
                {
                    this._Terms_Profile_Other_Consultancy_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Paid_By != value))
                {
                    this._Terms_Profile_Other_Consultancy_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Guarantor != value))
                {
                    this._Terms_Profile_Other_Consultancy_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Consultancy_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Consultancy_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Consultancy_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Consultancy_Frequency != value))
                {
                    this._Terms_Profile_Other_Consultancy_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Index
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Index != value))
                {
                    this._Terms_Profile_Other_Royalty_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Royalty_Use
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Use != value))
                {
                    this._Terms_Profile_Other_Royalty_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Royalty_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Vat != value))
                {
                    this._Terms_Profile_Other_Royalty_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Royalty_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Royalty_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Royalty_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Charge != value))
                {
                    this._Terms_Profile_Other_Royalty_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Paid_By != value))
                {
                    this._Terms_Profile_Other_Royalty_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Guarantor != value))
                {
                    this._Terms_Profile_Other_Royalty_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Royalty_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Royalty_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Royalty_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Royalty_Frequency != value))
                {
                    this._Terms_Profile_Other_Royalty_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Index
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Index != value))
                {
                    this._Terms_Profile_Other_Other1_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Other_Other1_Name
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Name;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Name != value))
                {
                    this._Terms_Profile_Other_Other1_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other1_Use
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Use != value))
                {
                    this._Terms_Profile_Other_Other1_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other1_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Vat != value))
                {
                    this._Terms_Profile_Other_Other1_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Other1_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Other1_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Other1_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Charge != value))
                {
                    this._Terms_Profile_Other_Other1_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Paid_By != value))
                {
                    this._Terms_Profile_Other_Other1_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Guarantor != value))
                {
                    this._Terms_Profile_Other_Other1_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other1_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other1_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Other1_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other1_Frequency != value))
                {
                    this._Terms_Profile_Other_Other1_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Index", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Index
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Index;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Index != value))
                {
                    this._Terms_Profile_Other_Other2_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Name", DbType = "VarChar(50)")]
        public string Terms_Profile_Other_Other2_Name
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Name;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Name != value))
                {
                    this._Terms_Profile_Other_Other2_Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other2_Use
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Use;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Use != value))
                {
                    this._Terms_Profile_Other_Other2_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Vat", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_Other_Other2_Vat
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Vat;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Vat != value))
                {
                    this._Terms_Profile_Other_Other2_Vat = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Cash_Destination", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Cash_Destination != value))
                {
                    this._Terms_Profile_Other_Other2_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Deferred_Remittance", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Deferred_Remittance != value))
                {
                    this._Terms_Profile_Other_Other2_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_Other_Other2_Charge
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Charge;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Charge != value))
                {
                    this._Terms_Profile_Other_Other2_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Paid_By", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Paid_By
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Paid_By;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Paid_By != value))
                {
                    this._Terms_Profile_Other_Other2_Paid_By = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Guarantor", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Guarantor
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Guarantor;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Guarantor != value))
                {
                    this._Terms_Profile_Other_Other2_Guarantor = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_Other_Other2_Frequency", DbType = "Int")]
        public System.Nullable<int> Terms_Profile_Other_Other2_Frequency
        {
            get
            {
                return this._Terms_Profile_Other_Other2_Frequency;
            }
            set
            {
                if ((this._Terms_Profile_Other_Other2_Frequency != value))
                {
                    this._Terms_Profile_Other_Other2_Frequency = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_London_Rent_Use", DbType = "Bit")]
        public System.Nullable<bool> Terms_Profile_London_Rent_Use
        {
            get
            {
                return this._Terms_Profile_London_Rent_Use;
            }
            set
            {
                if ((this._Terms_Profile_London_Rent_Use != value))
                {
                    this._Terms_Profile_London_Rent_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_London_Rent_Charge", DbType = "Real")]
        public System.Nullable<float> Terms_Profile_London_Rent_Charge
        {
            get
            {
                return this._Terms_Profile_London_Rent_Charge;
            }
            set
            {
                if ((this._Terms_Profile_London_Rent_Charge != value))
                {
                    this._Terms_Profile_London_Rent_Charge = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Index", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Index
        {
            get
            {
                return this._Terms_Profile_GPT_Index;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Index != value))
                {
                    this._Terms_Profile_GPT_Index = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Use", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Use
        {
            get
            {
                return this._Terms_Profile_GPT_Use;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Use != value))
                {
                    this._Terms_Profile_GPT_Use = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Cash_Destination", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Cash_Destination
        {
            get
            {
                return this._Terms_Profile_GPT_Cash_Destination;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Cash_Destination != value))
                {
                    this._Terms_Profile_GPT_Cash_Destination = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Profile_GPT_Deferred_Remittance", DbType = "Int NOT NULL")]
        public int Terms_Profile_GPT_Deferred_Remittance
        {
            get
            {
                return this._Terms_Profile_GPT_Deferred_Remittance;
            }
            set
            {
                if ((this._Terms_Profile_GPT_Deferred_Remittance != value))
                {
                    this._Terms_Profile_GPT_Deferred_Remittance = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Machine_Type_Code", DbType = "VarChar(50)")]
        public string Machine_Type_Code
        {
            get
            {
                return this._Machine_Type_Code;
            }
            set
            {
                if ((this._Machine_Type_Code != value))
                {
                    this._Machine_Type_Code = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Terms_Group_Name", DbType = "VarChar(50)")]
        public string Terms_Group_Name
        {
            get
            {
                return this._Terms_Group_Name;
            }
            set
            {
                if ((this._Terms_Group_Name != value))
                {
                    this._Terms_Group_Name = value;
                }
            }
        }
    }

    public partial class ShareSchedules
    {

        private int _Share_Schedule_ID;

        private string _Share_Schedule_Name;

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_ID", DbType = "Int NOT NULL")]
        public int Share_Schedule_ID
        {
            get
            {
                return this._Share_Schedule_ID;
            }
            set
            {
                if ((this._Share_Schedule_ID != value))
                {
                    this._Share_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Share_Schedule_Name", DbType = "VarChar(50)")]
        public string Share_Schedule_Name
        {
            get
            {
                return this._Share_Schedule_Name;
            }
            set
            {
                if ((this._Share_Schedule_Name != value))
                {
                    this._Share_Schedule_Name = value;
                }
            }
        }
    }

    public partial class RentSchedules
    {

        private int _Rent_Schedule_ID;

        private string _Rent_Schedule_Name;

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_ID", DbType = "Int NOT NULL")]
        public int Rent_Schedule_ID
        {
            get
            {
                return this._Rent_Schedule_ID;
            }
            set
            {
                if ((this._Rent_Schedule_ID != value))
                {
                    this._Rent_Schedule_ID = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Rent_Schedule_Name", DbType = "VarChar(50)")]
        public string Rent_Schedule_Name
        {
            get
            {
                return this._Rent_Schedule_Name;
            }
            set
            {
                if ((this._Rent_Schedule_Name != value))
                {
                    this._Rent_Schedule_Name = value;
                }
            }
        }
    }
}
