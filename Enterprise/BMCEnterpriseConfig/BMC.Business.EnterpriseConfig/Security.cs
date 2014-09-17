/**===============================================================
Class name : MeterHistoryAuthenticate
Purpose    : Authenticate Whether the user is valid and whether the user has rights to access.
 * 
*********************Revision History***************************
Anuradha     27/05/08  Created -- Authenticate Whether the user is valid and whether the user has rights to access
/******===============================================================**/

using System;
using System.Collections.Generic;
using System.Text;
using BMC.DataAccess;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Win32;
using System.Data;
using System.Security.Cryptography;

namespace BMC.Business.EnterpriseConfig
{
    public class SecurityAuthenticate 
    {

        /// <summary>
        ///  This class authenticates the user is valid.
        /// </summary> 
        /// 
        #region "Declarations"

        DataTable objUserDt = null;
        string strMyText, strMyKey = "", strResult = "";
        int iCurrentKeyPos;
        public string strUserRole = "";
        #endregion

        /// <summary>
        ///  This function authenticates the user credentials.
        /// </summary>
        /// <returns>boolean</returns>

        public bool Authenticate(DataTable objUserDt)
        {
            if (objUserDt.Rows.Count > 0)
            {
                //Check whether user has admin rights to use the tool.
                Authorize(objUserDt);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  This function authorizes the user credentials to check if the user has admin rights.
        /// </summary>
        /// <returns>boolean</returns>

        public bool Authorize(DataTable objdt)
        {
            if (objdt.Rows.Count > 0)
            {
                foreach (DataRow row in objdt.Rows)
                {
                    if (row["USER_LEVEL_NO"].ToString().ToUpper().Equals("1") &&
                        row["USER_LEVEL_DESCRIPTION"].ToString().ToUpper().Equals("ADMINISTRATOR"))
                    {
                        strUserRole = row["USER_LEVEL_DESCRIPTION"].ToString();
                        return true;
                    }
                    else
                    {
                        strUserRole = row["USER_LEVEL_DESCRIPTION"].ToString();
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///  Function checks whether the user is authenticated and authorized.
        /// </summary> 
        ///  Revision History
        ///   Anuradha      Created         26/05/08
        /// <param name="objUserDet>MeterHistoryProperty</param>
        /// <returns>boolean</returns>

        public bool ValidateUser(SecurityProperty objUserDetails)
        {
            //  SecurityProperty objUserDetails = (SecurityProperty)objUserDet;
            if (objUserDetails.UserName.ToUpper() != "BALLY")
            {
                objUserDetails.Password = Encode(objUserDetails.Password, "geoffrey" + objUserDetails.UserName);
                objUserDt = SecurityDBBuilder.UserDetails(objUserDetails).Tables[0];
                return Authenticate(objUserDt);
            }
            else if (objUserDetails.UserName.ToUpper() == "BALLY")
            {
                clsSuperUserPassword objSuperUser = new clsSuperUserPassword();
                string strPass = objSuperUser.CreateSuperUserPassword();
                if (objUserDetails.Password == strPass)
                {
                    strUserRole = "administrator";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private string Encode(string strpwd, string Key)
        {
            //Encode the password return the string.
            if (String.IsNullOrEmpty(strpwd) == true) { return null; }

            strMyText = strpwd;

            while (strMyKey.Length < (strMyText.Length * 2 + 200))
            {
                strMyKey = strMyKey + Key;
            }

            int val = Asc(strMyKey.Substring(0, 1));
            int valu1 = Asc(strMyKey.Substring(strMyKey.Length - 1));
            iCurrentKeyPos = 60 + val - valu1;

            for (int i = 0; i < strMyText.Length; i++)
            {
                val = Asc(strMyText.Substring(i, 1));
                int val1 = Asc(strMyKey.Substring(iCurrentKeyPos - 1, 1));
                int temp = (100 + val) - val1;
                //(100 + mytext) - mykey) for each character
                strResult = strResult + temp.ToString("000");
                //  082114084114101
                //  082114084114101 082114084114101
                iCurrentKeyPos++;
            }

            return strResult;
        }


        /// <summary>
        ///  Function to decrypt the password text.
        /// </summary> 
        ///  Revision History
        ///   Anuradha      Created         26/05/08
        /// <param name="thetext">string</param>
        /// <param name="theKey">string</param>
        /// <returns>DeEncrypted string</returns>
        private string Decode(string thetext, string theKey)
        {
            try
            {

                //##mh2000 when running check for invalid call or procedure error.
                strMyText = thetext;
                if (String.IsNullOrEmpty(theKey) == true)
                {
                    return "";
                }

                while (strMyKey.Length < (strMyText.Length * 2 + 100))
                {
                    strMyKey = strMyKey + theKey;
                }

                iCurrentKeyPos = 60 + (Asc(theKey.Substring(0, 1)) - Asc(theKey.Substring(theKey.Length - 1)));

                for (int i = 1; i < strMyText.Length; i = i + 3)
                {

                    if (((Convert.ToInt32(strMyText.Substring(i, 3)) + Asc(strMyKey.Substring(iCurrentKeyPos, 1))) - 100) > 0)
                    {

                        strResult = strResult + Chr((Convert.ToInt32(strMyText.Substring(i, 3))
                              + Asc(strMyKey.Substring(iCurrentKeyPos, 1))) - 100);
                        iCurrentKeyPos++;
                    }

                }
            }
            catch
            {

            }
            return strResult;
        }


        /// <summary>
        ///  Function to retrieve the char value for the ascii value
        /// </summary> 
        ///  Revision History
        ///   Anuradha      Created         26/05/08
        /// <param name="p_intByte>int</param>
        /// <returns>Char value</returns>
        internal static string Chr(int p_intByte)
        {
            if ((p_intByte < 0) || (p_intByte > 255))
            {
                throw new ArgumentOutOfRangeException("p_intByte", p_intByte,
                "Must be between 0 and 255.");
            }
            byte[] bytBuffer = new byte[] { (byte)p_intByte };
            return Encoding.GetEncoding(1252).GetString(bytBuffer);
        }

        /// <summary>
        ///  Function to retrieve the ascii value for the char value
        /// </summary> 
        ///  Revision History
        ///   Anuradha      Created         26/05/08
        /// <param name="p_strChar>string</param>
        /// <returns>Ascii value</returns>
        internal static int Asc(string p_strChar)
        {
            if (p_strChar.Length != 1)
            {
                throw new ArgumentOutOfRangeException("p_strChar", p_strChar,
                "Must be a single character.");
            }
            char[] chrBuffer = { Convert.ToChar(p_strChar) };
            byte[] bytBuffer = Encoding.GetEncoding(1252).GetBytes(chrBuffer);
            return (int)bytBuffer[0];
        }

    }
}
