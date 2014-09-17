using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{

    public class AdminSubCompanyResult
    {
        public List<SubCompanyRegionEntity> RegionEntities { get; set; }
        public SubCompanyEntity SubCompanyEntity { get; set; }
        public CompanyDefaultsEntity CompanyDefaultsEntity { get; set; }
        public List<TermsEntity> TermsEntities { get; set; }
        public List<SubCompanyAccessEntity> AccessEntities { get; set; }
        public List<SubCompanyCompanyEntity> CompanyEntities { get; set; }
        public List<SubCompanyModelEntity> ModelEntities { get; set; }
        public List<SubCompanyHourEntity> HoursEntities { get; set; }
        public List<SubCompanyStaffEntity> StaffEntities { get; set; }
        public List<SubCompanyJackpotEntity> JackpotEntities { get; set; }
    }

    public class SubCompanyEntity
    {
        public string Sub_Company_Name { get; set; }

        public System.Nullable<int> Company_ID { get; set; }

        public string Sub_Company_Switchboard_Phone_No { get; set; }

        public string Sub_Company_Address_1 { get; set; }

        public string Sub_Company_Address_2 { get; set; }

        public string Sub_Company_Address_3 { get; set; }

        public string Sub_Company_Address_4 { get; set; }

        public string Sub_Company_Address_5 { get; set; }

        public string Sub_Company_Postcode { get; set; }

        public string Sub_Company_ANA_Number { get; set; }

        public string Sub_Company_Income_Ledger_Code { get; set; }

        public string Sage_Account_Ref { get; set; }

        public System.Nullable<int> Company_Model_Set_ID { get; set; }

        public string Sub_Company_Trade_Type { get; set; }

        public string Sub_Company_Trade_Type1 { get; set; }

        public string Sub_Company_Contact_Name { get; set; }

        public string Sub_Company_Contact_Phone_No { get; set; }

        public string Sub_Company_Contact_Email_Address { get; set; }

        public System.Nullable<bool> Sub_Company_Use_Split_Rents { get; set; }

        public System.Nullable<bool> Sub_Company_Price_Per_Play_Default { get; set; }

        public string Sub_Company_Price_Per_Play { get; set; }

        public System.Nullable<bool> Sub_Company_Jackpot_Default { get; set; }

        public string Sub_Company_Jackpot { get; set; }

        public System.Nullable<bool> Sub_Company_Percentage_Payout_Default { get; set; }

        public string Sub_Company_Percentage_Payout { get; set; }

        public System.Nullable<bool> Terms_Group_ID_Default { get; set; }

        public System.Nullable<int> Terms_Group_ID { get; set; }

        public System.Nullable<bool> Access_Key_ID_Default { get; set; }

        public System.Nullable<int> Access_Key_ID { get; set; }

        public System.Nullable<bool> Staff_ID_Default { get; set; }

        public System.Nullable<int> Staff_ID { get; set; }

        public System.Nullable<int> Sub_Company_Default_Opening_Hours_ID { get; set; }

        public string Sub_Company_Invoice_Name { get; set; }

        public string Sub_Company_Invoice_Address { get; set; }

        public string Sub_Company_Invoice_Postcode { get; set; }

        public string Sub_Company_Account_Name { get; set; }

        public string Sub_Company_Account_No { get; set; }

        public string Sub_Company_Sort_Code { get; set; }
            
        public List<ChangeHistory> GetUpdatedFields(SubCompanyEntity InitialValue)
        {

            List<ChangeHistory> lst = new List<ChangeHistory>();
            try
            {
                if (InitialValue == null)
                    return lst;
                foreach (var prop in this.GetType().GetProperties())
                {
                    if (prop.Name.ToUpper().Contains("_ID")) continue;
                    if (!Convert.ToString(prop.GetValue(this, null)).Equals(Convert.ToString(prop.GetValue(InitialValue, null))) && Convert.ToString(prop.GetValue(InitialValue, null)).ToLower() != "false")
                    {
                        lst.Add(new ChangeHistory() { NewValue = Convert.ToString(prop.GetValue(this, null)), OldValue = Convert.ToString(prop.GetValue(InitialValue, null)), ProperyName = prop.Name });
                    }                    
                }
            }
            catch (Exception ex)
            {
                
            }
            return lst;
        }

        public List<ChangeHistory> GetInitialFields()
        {
            List<ChangeHistory> lst = new List<ChangeHistory>();
            try
            {

                foreach (var prop in this.GetType().GetProperties())
                {
                    if (prop.Name.ToUpper().Contains("_ID")) continue;
                    if (Convert.ToString(prop.GetValue(this, null)) != string.Empty && Convert.ToString(prop.GetValue(this, null)).ToLower() != "false")
                    {
                        lst.Add(new ChangeHistory() { NewValue = Convert.ToString(prop.GetValue(this, null)), OldValue = "", ProperyName = prop.Name });
                    }
                }
            }

            catch (Exception ex)
            {
            }
            return lst;

        }
    }

    public class SubCompanyRegionEntity
    {
        public int Sub_Company_ID { get; set; }

        public int Sub_Company_Region_ID { get; set; }

        public string Sub_Company_Region_Name { get; set; }

        public string Sub_Company_Region_Description { get; set; }

        public System.Nullable<int> Staff_ID { get; set; }

        public string Staff_Last_Name { get; set; }

        public string Staff_First_Name { get; set; }
    }

    public class SubCompanyAccessEntity
    {
        public System.Nullable<int> Access_Key_ID { get; set; }

        public string Access_Key_Name { get; set; }
    }

    public class SubCompanyStaffEntity
    {

        public string StaffName { get; set; }

        public int Staff_ID { get; set; }
    }

    public class SubCompanyCompanyEntity
    {

        public int Company_ID { get; set; }

        public string Company_Name { get; set; }

    }

    public class SubCompanyHourEntity
    {

        public int Standard_Opening_Hours_ID { get; set; }

        public string Standard_Opening_Hours_Description { get; set; }
    }

    public class SubCompanyJackpotEntity
    {

        public string JackpotValue { get; set; }
    }

    public class SubCompanyModelEntity
    {

        public int Company_Model_Set_ID { get; set; }

        public string Company_Model_Set_Description { get; set; }
    }

    public class SubCompanyAreaEntity
    {
        public int Sub_Company_Area_ID { get; set; }

        public int Sub_Company_Region_ID { get; set; }

        public string Sub_Company_Area_Name { get; set; }

        public string Sub_Company_Area_Description { get; set; }

        public System.Nullable<int> Staff_ID { get; set; }

        public string Staff_Last_Name { get; set; }

        public string Staff_First_Name { get; set; }
    }

    public class SubCompanyDistrictEntity
    {
        public int Sub_Company_District_ID { get; set; }

        public int Sub_Company_Area_ID { get; set; }

        public string Sub_Company_District_Name { get; set; }

        public string Sub_Company_District_Description { get; set; }

        public System.Nullable<int> Staff_ID { get; set; }

        public string Staff_Last_Name { get; set; }

        public string Staff_First_Name { get; set; }
    }

    public class CompanyDefaultsEntity
    {

        public string Company_Percentage_Payout { get; set; }

        public string Company_Price_Per_Play { get; set; }

        public string Company_Jackpot { get; set; }

        public System.Nullable<int> Terms_Group_ID { get; set; }

        public System.Nullable<int> Access_Key_ID { get; set; }

        public System.Nullable<int> Staff_ID { get; set; }
    }

}
