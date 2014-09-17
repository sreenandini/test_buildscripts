/**===============================================================
* Class name : clsSuperUserPassword
* Purpose   : To Store and retrieve passwords for bally user.
 
*********************Revision History***************************
Anuradha      26/05/08  Modified -- Code To Store and retrieve passwords for bally user
/******===============================================================**/

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace BMC.Business.EnterpriseConfig
{
    public class clsSuperUserPassword
    {
        private string[] SuperUserPasswordList = null;

        /// <summary>
        /// Constructor to create an array of passwords.
        /// </summary>
        /// Revision History
        /// Anuradha    Created     27/05/08

        public clsSuperUserPassword()
        {
            FillPasswordList();
        }

        /// <summary>
        /// Retrieve Super User Password.
        /// </summary>
        /// Revision History
        /// Anuradha    Created     27/05/08
        /// <returns>string</returns>
        /// 
        public string CreateSuperUserPassword()
        {
            long index;
            string szDate = "";
            long remainder = 0;

            szDate = DateTime.Now.ToShortDateString();

            //Get the password from the password array based on date.
            //l = DateTimeManger.DateDiff(DateTimeManger.DateInterval.Week, Convert.ToDateTime("01/01/2000"), Convert.ToDateTime(szDate))
            //+ DateTimeManger.DateDiff(DateTimeManger.DateInterval.Year, Convert.ToDateTime("01/01/2000"), Convert.ToDateTime(szDate));

            index = Microsoft.VisualBasic.DateAndTime.DateDiff("ww", "01/01/2000", szDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, Microsoft.VisualBasic.FirstWeekOfYear.Jan1) +
                Microsoft.VisualBasic.DateAndTime.DateDiff("yyyy", "01/01/2000", szDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, Microsoft.VisualBasic.FirstWeekOfYear.Jan1);

            int iuBound = SuperUserPasswordList.GetUpperBound(0);
            int iLBound = SuperUserPasswordList.GetLowerBound(0);
            long diff = (iuBound - iLBound);


            index = (index % diff);

            //return the password.
            return (SuperUserPasswordList[(int)(index)].ToString().ToLower());
        }


        private void FillPasswordList()
        {
            //Store the list of passwords in an array.
            SuperUserPasswordList = new string[35];


            SuperUserPasswordList[0] = "BigBird";
            SuperUserPasswordList[1] = "Scooby";
            SuperUserPasswordList[2] = "Silver";
            SuperUserPasswordList[3] = "Bugs";
            SuperUserPasswordList[4] = "Felix";
            SuperUserPasswordList[5] = "Chipper";
            SuperUserPasswordList[6] = "Champion";
            SuperUserPasswordList[7] = "Itchy";
            SuperUserPasswordList[8] = "Scratchy";
            SuperUserPasswordList[9] = "Jerry";
            SuperUserPasswordList[10] = "Tom";
            SuperUserPasswordList[11] = "Yogi";
            SuperUserPasswordList[12] = "Spot";
            SuperUserPasswordList[13] = "Salem";
            SuperUserPasswordList[14] = "Rocky";
            SuperUserPasswordList[15] = "Snoopy";
            SuperUserPasswordList[16] = "Eddie";
            SuperUserPasswordList[17] = "Kermit";
            SuperUserPasswordList[18] = "Flipper";
            SuperUserPasswordList[19] = "Lassie";
            SuperUserPasswordList[20] = "TopCat";
            SuperUserPasswordList[21] = "Barney";
            SuperUserPasswordList[22] = "Daffy";
            SuperUserPasswordList[23] = "Goofy";
            SuperUserPasswordList[24] = "Donald";
            SuperUserPasswordList[25] = "Bart";
            SuperUserPasswordList[26] = "Homer";
            SuperUserPasswordList[27] = "Lisa";
            SuperUserPasswordList[28] = "Maggie";
            SuperUserPasswordList[29] = "Charlie";
            SuperUserPasswordList[30] = "Batfink";
            SuperUserPasswordList[31] = "Kenny";
            SuperUserPasswordList[32] = "Eric";
            SuperUserPasswordList[33] = "Dougal";
            SuperUserPasswordList[34] = "Alvin";
        }
    }
}
