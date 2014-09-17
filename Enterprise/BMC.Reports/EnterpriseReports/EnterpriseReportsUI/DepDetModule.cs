using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using BMC.DataAccess;
using System.Data;
using BMC.Common.ConfigurationManagement;
//using HQDefaultReports.Configuration;
//using HQDefaultReports;
using BMC.EnterpriseReportsDataAccess;
using Microsoft.VisualBasic;

namespace BMC.EnterpriseReportsUI
{
    public sealed class DepDetModule
    {
        #region "Declarations"
            //static float PassNBV, PassDepreciationPerWeek, PassPurchasePrice, TheFirstFig, TheSecondFig;
            //static string TheStartDate, TheEndDate;
        #endregion

        public static float ReturnMachineDepreciation(long Machine_ID, string MachineInDepotDate, string MachineOutOfDepotDate, string ReportStartDate, string ReportEndDate)
        {

            float PassNBV = 0.0F;
            float PassDepreciationPerWeek = 0.0F;
            float PassPurchasePrice = 0.0F;

            string TheStartDate;
            string TheEndDate;

            float TheFirstFig = 0.0F;
            float TheSecondFig = 0.0F;


            if (IsDate(MachineInDepotDate))
            {
                if (DateTime.Parse(ReportStartDate) > DateTime.Parse(MachineInDepotDate))
                {
                    TheStartDate = DateTime.Parse(ReportStartDate).ToString();
                }
                else
                {
                    TheStartDate = DateTime.Parse(MachineInDepotDate).ToString();
                }
            }
            else
            {
                TheStartDate = DateTime.Parse(ReportStartDate).ToString();
            }

            if (IsDate(MachineOutOfDepotDate))
            {
                if (DateTime.Parse(ReportEndDate) < DateTime.Parse(MachineOutOfDepotDate))
                {
                    TheEndDate = DateTime.Parse(ReportEndDate).ToString();
                }
                else
                {
                    TheEndDate = DateTime.Parse(MachineOutOfDepotDate).ToString();
                }
            }
            else
            {
                TheEndDate = DateTime.Parse(ReportEndDate).ToString();
            }

            if (DateTime.Parse(TheStartDate) < DateTime.Parse(TheEndDate))
            {

                //Use the GetDepreciationDetailsFromMachineID function from HQ
                //Work out the NBV at the latest date (MachineInDepotDate and ReportStartDate)
                //Work out the NBV at the earliest date (MachineOutOfDepotDate and ReportEndDate)
                //Find the difference between the 2, which will be the depreciation across the period

                if (GetDepreciationDetailsFromMachineID(Machine_ID, ref PassNBV, ref PassDepreciationPerWeek,
                    ref PassPurchasePrice, TheStartDate))
                {
                    TheFirstFig = PassNBV;
                }
                else
                {
                    //ermmm what do I do here?
                }

                if (GetDepreciationDetailsFromMachineID(Machine_ID, ref PassNBV, ref PassDepreciationPerWeek,
                    ref PassPurchasePrice, TheEndDate))
                {
                    TheSecondFig = PassNBV;
                }
                else
                {
                    //ermmm what do I do here?
                }

                return (TheFirstFig - TheSecondFig);
            }
            else
            {
                return 0;
            }
        }

      


            public static bool GetDepreciationDetailsFromMachineID(long MachineID, ref float NBV, ref float DepreciationPerWeek, ref float PurchasePrice)
            {
                return GetDepreciationDetailsFromMachineID(MachineID, ref NBV, ref DepreciationPerWeek, ref PurchasePrice, "");
            }

            //INSTANT C# NOTE: Overloaded method(s) are created above to convert the following method having optional parameters:
            //ORIGINAL LINE: Public Function GetDepreciationDetailsFromMachineID(MachineID As Long, ByRef NBV As Single, ByRef DepreciationPerWeek As Single, ByRef PurchasePrice As Single, Optional DateTo As String = "") As Boolean
        public static bool GetDepreciationDetailsFromMachineID(long MachineID, ref float NBV, ref float DepreciationPerWeek, ref float PurchasePrice, string DateTo)
        {
            bool bDPW = false;
            int DaysLeft = 0;
            float YearsLeft = 0F;
            float Depreciation_Perc = 0F;
            float Purchase_Price = 0F;
            float Residual_Value = 0F;
            string Date_To = null;


                SqlParameter[] parames = new SqlParameter[1];
                parames[0] = new SqlParameter("@machineID", MachineID);

                DataTable dt_depdet = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure,
                    "GetDepDetailsFromMachID", parames).Tables[0];

                if (dt_depdet.Rows.Count > 0)
                {
                    foreach (DataRow drRow in dt_depdet.Rows)
                    {
                        if (Microsoft.VisualBasic.Information.IsDate(DateTo))
                        {
                            Date_To = DateTo;
                        }
                        else if (drRow["Machine_End_Date"] + "" == "")
                        {
                            Date_To = System.DateTime.Now.ToString("dd/MMM/yyyy");
                        }
                        else
                        {
                            Date_To = drRow["Machine_End_Date"].ToString() + "";
                        }

                        if (Information.IsDate(drRow["Machine_Depreciation_Start_Date"])
                            && Information.IsDate(Date_To) &&
                            (!(drRow["Depreciation_Policy_Details_ID"] == null)))
                        {
                            if (drRow["Machine_Original_Purchase_Price"] != DBNull.Value)
                            {
                                Purchase_Price = float.Parse(drRow["Machine_Original_Purchase_Price"].ToString());
                            }
                            if (drRow["Depreciation_Policy_Residual_Value"] != DBNull.Value)
                            {
                                Residual_Value = float.Parse(drRow["Depreciation_Policy_Residual_Value"].ToString());
                            }

                            DaysLeft = int.Parse(DateAndTime.DateDiff("d",
                                drRow["Machine_Depreciation_Start_Date"], Date_To,
                                FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1).ToString());
                            if (DaysLeft > 1 && Residual_Value < Purchase_Price)
                            {

                                YearsLeft = float.Parse((DaysLeft / 365.0).ToString());

                                foreach (DataRow row in dt_depdet.Rows)
                                {
                                    if (YearsLeft > (float.Parse(row["Depreciation_Policy_Details_Duration"].ToString()) / 12))
                                    {
                                        Depreciation_Perc = Depreciation_Perc +
                                            float.Parse(row["Depreciation_Policy_Details_Percentage"].ToString());
                                        YearsLeft = YearsLeft -
                                           (float.Parse(row["Depreciation_Policy_Details_Duration"].ToString()) / 12);
                                        //End of the File.
                                        bDPW = true;
                                    }
                                    else
                                    {
                                        Depreciation_Perc = Depreciation_Perc +
                                            (float.Parse(row["Depreciation_Policy_Details_Percentage"].ToString())
                                            * (YearsLeft /
                                            (float.Parse(row["Depreciation_Policy_Details_Duration"].ToString()) / 12)));
                                        break;
                                    }
                                }

                                PurchasePrice = Purchase_Price;
                                NBV = Purchase_Price - ((Depreciation_Perc / 100) * (Purchase_Price - Residual_Value));

                                if (dt_depdet.Rows.Count == 0 || bDPW == true)
                                {
                                    //End of the File.
                                    DepreciationPerWeek = 0F;
                                }
                                else
                                {
                                    DepreciationPerWeek = (((Purchase_Price - Residual_Value) *
                                        float.Parse(drRow["Depreciation_Policy_Details_Percentage"].ToString()) / 100) * 7F) /
                                        (float.Parse(drRow["Depreciation_Policy_Details_Duration"].ToString()) * 30.4F);
                                }

                            }
                            else
                            {

                                PurchasePrice = Purchase_Price;
                                NBV = Purchase_Price;
                                DepreciationPerWeek = 0F;

                            }

                        }
                        else
                        {

                            PurchasePrice = 0F;
                            DepreciationPerWeek = 0F;
                            NBV = 0F;

                        }
                    }
                }
                else
                {

                    PurchasePrice = 0F;
                    DepreciationPerWeek = 0F;
                    NBV = 0F;

                }

                dt_depdet.Dispose();
                dt_depdet = null;
            return true;
        }
        public static bool IsDate(string inValue)
        {
            //Check if the string is a valid date
            bool result = false;
            try
            {
                DateTime myDT = DateTime.Parse(inValue);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }


        public static bool IsNumber(string sNumber)
        {
            //Check if the string is a valid number
            bool Result = true;
            for (int i = 0; i < sNumber.Length; i++)
            {
                if (!Char.IsNumber(sNumber, i))
                {
                    Result = false;
                }
            }
            return Result;
        }
  
        public static string VerifySQL(string TheSQL)
        {
            string PreStr, PostStr;
            int i, j, k, l;
            int BracketCount;

            //Replace CDate( with Cast

            i = TheSQL.ToUpper().IndexOf("CDATE(");
            while (i > 0)
            {
                PreStr = TheSQL.Substring(0, (i - 1));
                PostStr = TheSQL.Substring(i + 5);
                j = 1;
                BracketCount = 1;
                while (j <= PostStr.Length && BracketCount > 0)
                {

                    switch (PostStr.Substring(j, 1))
                    {
                        case "(":
                            {
                                BracketCount = BracketCount + 1;
                                break;
                            }
                        case ")":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                    }
                    j++;
                }
                if (BracketCount == 0)
                {
                    TheSQL = PreStr + " Cast" + PostStr.Substring(0, j - 1) + " AS DATETIME) " + PostStr.Substring(j);
                }

                i = TheSQL.ToUpper().IndexOf("CDATE(");

            }

            //Replace DateDiff('d' with DateDiff(d
            i = TheSQL.ToUpper().IndexOf("DATEDIFF('");
            while (i > 0)
            {

                PreStr = TheSQL.Substring(0, i - 1);
                PostStr = TheSQL.Substring(i + 9);
                j = 1;
                BracketCount = 1;
                while (j <= PostStr.Length && BracketCount > 0)
                {

                    string check_str = PostStr.Substring(j, 1);
                    switch (check_str)
                    {
                        case "'":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                    }
                    j++;
                }

                if (BracketCount == 0)
                {
                    string temp = PostStr.Substring(1, j - 2) + ",";
                    TheSQL = PreStr + " Datediff(" + temp + PostStr.Substring(j);
                }

                i = TheSQL.ToUpper().IndexOf("DATEDIFF('");

            }

            //Replace DateDiff('d' with DateDiff(d
            i = TheSQL.ToUpper().IndexOf("DATEADD('");
            while (i > 0)
            {
                PreStr = TheSQL.Substring(0, i - 1);
                PostStr = TheSQL.Substring(i + 8);
                j = 1;
                BracketCount = 1;
                while (j <= PostStr.Length && BracketCount > 0)
                {
                    switch (PostStr.Substring(j, 1))
                    {
                        case "'":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                    }
                    j++;
                }

                if (BracketCount == 0)
                {
                    TheSQL = PreStr + " Dateadd(" + PostStr.Substring(1, j - 3) + PostStr.Substring(j);
                }

                i = TheSQL.ToUpper().IndexOf("DATEADD('");

            }

            //Replace "= True" with "<> 0"
            TheSQL = TheSQL.Replace("= True", "<> 0");
            TheSQL = TheSQL.Replace("=True", "<> 0");
            TheSQL = TheSQL.Replace("= False", "= 0");
            TheSQL = TheSQL.Replace("=False", "= 0");

            //Replace "IIF(" with "CASE WHEN"
            i = TheSQL.ToUpper().IndexOf("IIF(");
            while (i > 0)
            {

                PreStr = TheSQL.Substring(0, i - 1);
                PostStr = TheSQL.Substring(i + 4);
                PreStr = PreStr + " (CASE WHEN ";

                l = 0;//'Comma position
                k = 1; //'Counter
                BracketCount = 0;
                while (k <= PostStr.Length && l == 0)
                {

                    switch (PostStr.Substring(k, 1))
                    {
                        case "(":
                            {
                                BracketCount = BracketCount + 1;
                                break;
                            }
                        case ")":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                        case ",":
                            {
                                if (BracketCount == 0) { l = k; }
                                break;
                            }
                    }
                    k++;

                }
                PreStr = PreStr + PostStr.Substring(1, l - 1) + " THEN ";

                j = l + 1; //'starting pos
                k = j; //'Counter
                l = 0; //'Comma position
                BracketCount = 0;
                while (k <= PostStr.Length && l == 0)
                {

                    switch (PostStr.Substring(k, 1))
                    {
                        case "(":
                            {
                                BracketCount = BracketCount + 1;
                                break;
                            }
                        case ")":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                        case ",":
                            {
                                if (BracketCount == 0) { l = k - j; }
                                break;
                            }
                    }
                    k++;
                }
                PreStr = PreStr + PostStr.Substring(j, l) + " ELSE ";

                j = l + j + 1; //starting pos
                k = j; //Counter
                BracketCount = 1;
                while (k <= PostStr.Length && BracketCount > 0)
                {

                    switch (PostStr.Substring(k, 1))
                    {
                        case "(":
                            {
                                BracketCount = BracketCount + 1;
                                break;
                            }
                        case ")":
                            {
                                BracketCount = BracketCount - 1;
                                break;
                            }
                    }
                    k++;

                }

                PreStr = PreStr + PostStr.Substring(j, k - (j + 1));

                PreStr = PreStr + " END) ";

                PreStr = PreStr + PostStr.Substring(k - 1, 1);

                TheSQL = PreStr + " " + PostStr.Substring(k + 1);

                i = TheSQL.ToUpper().IndexOf("IIF(");

            }

            return TheSQL;
        }
    }
}
