using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Entities
{
    public interface IMoficatioHistory<T>
    {
        List<ChangeHistory> GetUpdatedFields(T InitialValues);
    }
    public class ChangeHistory
    {
        public string ProperyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
    public class AdminCompanyResult
    {
        public List<TermsEntity> TermsEntitys { get; set; }
        public List<AccessKeyEntity> AccessKeyEntitys { get; set; }
        public List<StaffEntity> StaffEntitys { get; set; }
        public CompanyEntity CompanyEntity { get; set; }
    }

    public class TermsEntity
    {

        public int Terms_Group_ID { get; set; }

        public string Terms_Group_Name { get; set; }

    }
    public class AccessKeyEntity
    {

        public int Access_Key_ID { get; set; }

        public string Access_Key_Name { get; set; }

        public string Access_Key_Ref { get; set; }

        public string Access_Key_Manufacturer { get; set; }

        public string Access_Key_Type { get; set; }


    }
    public class StaffEntity
    {

        public int Staff_ID { get; set; }

        public string Staff_Last_Name { get; set; }

        public string Staff_First_Name { get; set; }


    }
    public class CompanyEntity : IMoficatioHistory<CompanyEntity>
    {

        public string Company_Name { get; set; }

        public string Company_Switchboard_Phone_No { get; set; }

        public string Company_Address_1 { get; set; }

        public string Company_Address_2 { get; set; }

        public string Company_Address_3 { get; set; }

        public string Company_Address_4 { get; set; }

        public string Company_Address_5 { get; set; }

        public string Company_Postcode { get; set; }

        public string Company_Contact_Name { get; set; }

        public string Company_Contact_Phone_No { get; set; }

        public string Company_Contact_Email_Address { get; set; }

        public string Company_Price_Per_Play { get; set; }

        public string Company_Jackpot { get; set; }

        public string Company_Percentage_Payout { get; set; }

        public int Terms_Group_ID { get; set; }

        public int Access_Key_ID { get; set; }

        public int Staff_ID { get; set; }

        public string Company_Invoice_Name { get; set; }

        public string Company_Invoice_Address { get; set; }

        public string Company_Invoice_Postcode { get; set; }

        public List<ChangeHistory> GetUpdatedFields(CompanyEntity InitialValue)
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
    public partial class UpdateCompanyDetails
        {
            public int CompanyID { get; set; }
        }
    public class CompanyDecendants
        {
            public int ID { get; set; }
            public string FieldName { get; set; }
            public String Type { get; set; }
        }

    
}
