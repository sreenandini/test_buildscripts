using System;
using System.Runtime.CompilerServices;
using BMC.CoreLib;
namespace BMC.Guardian.Transport
{
    
    [Serializable]
    public class LastProcessedDetails
    {
        public string BMCVersion{ get; set; }
        public string DateTime{ get; set; }
        public string DBVersion{ get; set; }
        public string ExportRecordsToProcess{ get; set; }
        public string LastDropCreated{ get; set; }
        public string LastHourCreated{ get; set; }
        public string LastReadCreated{ get; set; }
        public string LastRecordExported{ get; set; }
        public string HourlyReadHour { get; set; }
        public string Site_Code { get; set; }
        public string ReadDate { get; set; }
        public string ReadTime { get; set;  }
        public string LastHourlyDate { get; set; }
    }

    public class SiteStatusDetails : LastProcessedDetails
    {
        public bool IsSiteDown { get; set; }
        public string Region { get; set; }
        public string MissedHourlies { get; set; }
        
        public bool IsHourlyRun
        {
            get
            {
                return Convert.ToString(System.DateTime.Now.Hour).Equals(HourlyReadHour);  //&& string.IsNullOrEmpty(MissedHourlies);
            }
        }

        public bool IsReadRun
        {
            get
            {

                try
                {
                    if (string.IsNullOrEmpty(ReadDate))
                        return false;
                    if (Convert.ToInt32(ReadTime.Split(':')[0]) > System.DateTime.Now.Hour
                            || (Convert.ToInt32(ReadTime.Split(':')[0]) == System.DateTime.Now.Hour
                            && Convert.ToInt32(ReadTime.Split(':')[1]) >= System.DateTime.Now.Minute))
                        return Convert.ToInt32(System.DateTime.Now.Date.Subtract(Convert.ToDateTime(ReadDate)).TotalDays) <= 1;
                    else
                        return Convert.ToInt32(System.DateTime.Now.Date.Subtract(Convert.ToDateTime(ReadDate)).TotalDays) == 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
            
        public bool Status
        {
            get
            {
                return !IsSiteDown && IsHourlyRun && IsReadRun;
            }
        }
    }

    public class UserEntity : BMC.CoreLib.DisposableObject
    {
        public UserEntity() { }

        public int SecurityUserID { get; set; }
        public string WindowsUserName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LanguageID { get; set; }
        public int CurrencyCulture { get; set; }
        public int DateCulture { get; set; }
        public bool ChangePassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PasswordChangeDate { get; set; }
        public bool IsReset { get; set; }
        public bool IsLocked { get; set; }
        public int StaffID { get; set; }
        public string RoleName { get; set; }
        public int SecurityRoleID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string DisplayName
        {
            get { return string.Format("{0} , {1}", First_Name, Last_Name); }
            private set { }
        }
    }
}

