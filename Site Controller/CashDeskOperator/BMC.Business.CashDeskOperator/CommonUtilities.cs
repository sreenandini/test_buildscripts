using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using BMC.Transport;
using BMC.Transport.CashDeskOperatorEntity;
using Microsoft.Win32;
using System.Collections.Generic;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.Security.Interfaces;


namespace BMC.Business.CashDeskOperator
{
    public class CommonUtilities
    {
        private string PrintType;
        string VoidType = string.Empty;
        CommonDataAccess commonDataAccess = new CommonDataAccess();
        Voucher voucher = new Voucher();
        RTOnlineTicketDetail oRTOnlineTicketDetail = new RTOnlineTicketDetail();
        
        RTOnlineReceiptDetail oRTOnlineReceiptDetail = new RTOnlineReceiptDetail();
        OfflineTicket oOfflineTicket = new OfflineTicket();
        float yPos;
        float leftMargin;
        float topMargin;
        Font printFont;
        char cpad1 = '_';
        char cpad2 = '=';
        char cpad3 = '-';
        char ctab = ' ';
        int number = 50;
        int number1 = 15;
        String line = null;
        int pNumber;
        double m_currRequiresHeadCashierSig, m_currRequiresManagerSig;
        private bool isVoided = false;
        private MachineDetail machineDetail = new MachineDetail();
        public IUser currentUser = null;
        public string Header = string.Empty;

        public CommonUtilities()
        {

        }
        public static DataSet SiteInformation
        { 
            get
            {
                return CommonDataAccess.SiteDetail;
            }
        }
        public static string ExchangeConnectionString
        {
            get
            {
                return ConnectionStringHelper.ExchangeConnectionString;                
            }
        }
        //
        public static string SiteConnectionString(string sExchangeConnectionString)
        {
            return ConnectionStringHelper.GetExchangeConnectionString(sExchangeConnectionString);
        }
        //
        public static string TicketingConnectionString(string sTicketingConnectionString)
        {
            return ConnectionStringHelper.GetTicketConnectionString(sTicketingConnectionString);
        }

        //
        //public static string SetCurrentExchangeConnectionString()
        //{
        //    return ConnectionStringHelper.SetCurrentExchangeConnectionString();
        //}

        //public static string SetCurrentTicketConnectionString()
        //{
        //    return ConnectionStringHelper.SetCurrentTicketConnectionString();
        //}
        
        public static string TicketConnectionString
        {
            get
            {
                return ConnectionStringHelper.TicketingConnectionString;
            }
        }
        public static string CMPConnectionString
        {
            get
            {
                return CommonDataAccess.CMPConnectionString;
            }
        }
        public static string TicketLocationCode
        {
            get
            {
                return CommonDataAccess.TicketLocationCode;
            }
        }
        public static DataSet SettingInformation
        {
            get
            {
                return CommonDataAccess.InitialSettings;
            }
        }

        public static DataSet AppSettingInformation
        {
            get
            {
                return CommonDataAccess.AppInitialSettings;
            }
        }

        public static void UpdateSettings(string SettingName, string SettingValue)
        {
            CommonDataAccess.UpdateSettings(SettingName, SettingValue);
        }

        public static void UpdateAppSettingsSortOrder(string AppSettingKey, string AppSettingValue)
        {
            CommonDataAccess.UpdateAppSettings_SortOrder(AppSettingKey, AppSettingValue);
        }

        public static void UpdateTicketExpire(int value)
        {
            CommonDataAccess.UpdateTicketExpire(value);
        }

        public static void UpdateGMUSiteCodeStatus(int Installation_No, int Status)
        {
            CommonDataAccess.UpdateGMUSiteCodeStatus(Installation_No, Status);
        }

        public static bool SQLConnectionExists
        {
            get
            {
                return CommonDataAccess.TestSQLConnection(CommonDataAccess.ExchangeConnectionString);
            }
        }

        public static bool UserBasedPositionExists
        {
            get
            {
                return CommonDataAccess.UserBasedPositionExists(BMC.Security.SecurityHelper.CurrentUser.SecurityUserID);
            }
        }

        public static List<Employeecarddata> GetEmployeeCardPollingData()
        {
            DataTable dtEmpcard = CommonDataAccess.GetEmployeeCardPollingData();
            List<Employeecarddata> empcardData = new List<Employeecarddata>();
            foreach (DataRow row in dtEmpcard.Rows)
            {
                 empcardData.Add( new Employeecarddata{
                     EmployeeCard= row["EmployeeCard"].ToString(),
                     //EMPCardEDType=Convert.ToInt32( row["EMPCard_ED_Type"]),
                     EmployeeFlags=row["EmployeeFlags"].ToString(),
                     Installation_No =Convert.ToInt32(row["Installation_No"])});
                     //PollingStatus =Convert.ToBoolean(row["Polling_Status"])
            }
            return empcardData;
        }

        public static void UpdateEmployeeCardPolling(string Employeecardnumber,int InstallationNo)
        {
             CommonDataAccess.UpdateEmployeeCardPolling(Employeecardnumber, InstallationNo);
        }

        public DataTable GetInstallationList()
        {
            return CommonDataAccess.GetInstallationList();
        }

        public void GetMachineDetailsTreasury(string TreasuryNumber)
        {
            DataSet machineDetailTreasury;

            try
            {
                machineDetailTreasury = commonDataAccess.GetMachineDetailsTreasury(TreasuryNumber);
                foreach (DataTable dt in machineDetailTreasury.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        machineDetail.BarPositionName = machineDetailTreasury.Tables[0].Rows[0]["Bar_Pos_Name"].ToString();
                        machineDetail.MachineName = machineDetailTreasury.Tables[0].Rows[0]["Name"].ToString();
                        machineDetail.StockNumber = machineDetailTreasury.Tables[0].Rows[0]["Stock_No"].ToString();
                        machineDetail.Value = machineDetailTreasury.Tables[0].Rows[0]["Treasury_Amount"].ToString();
                        machineDetail.TreasuryNumber = TreasuryNumber;
                        break;
                    }
                }
                
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        public DataTable GetUserRoles(string UserName, string Password)
        {          
                return (new CommonDataAccess()).GetUserRoles(UserName, Password);            
        }

        //public bool SetRegistryEntries(Dictionary<string, string> dictSetregistry, string strPath)
        //{

        //    RegistryKey Regkeyinner;
        //   // RegistryKey RegKey;
        //    string[] strSubKeysarray = null;
        //    string[] strKeyValueNamesarray = null;
        //    string strKey = string.Empty;
        //    string strRoute = string.Empty;
        //    string[] strCheckRoute = null;
        //    string strSubRoute = string.Empty;

        //    try
        //    {
        //       // RegKey = BMCRegistryHelper.GetRegLocalMachine().OpenSubKey(strPath, true);
        //        strRoute = strPath.Substring(strPath.LastIndexOf("\\") + 1);
        //        foreach (KeyValuePair<string, string> KVPServer in dictSetregistry)
        //        {
        //            strCheckRoute = KVPServer.Key.Split('\\');
        //            strSubRoute = strCheckRoute[strCheckRoute.Length - 3];
        //            if (strRoute == strSubRoute)
        //            {
        //                BMCRegistryHelper.SetRegKeyValue(strPath,strCheckRoute[strCheckRoute.Length - 1],RegistryValueKind.String,KVPServer.Value);

        //            }
        //            else
        //            {
        //                strSubKeysarray = RegKey.GetSubKeyNames();
        //                if (strSubKeysarray.Length > 0)
        //                {
        //                    strKeyValueNamesarray = RegKey.GetValueNames();
        //                    strKey = KVPServer.Key.Substring(KVPServer.Key.LastIndexOf("\\") + 1);
        //                    foreach (string strRegKey in strSubKeysarray)
        //                    {
        //                        if (strSubRoute == strRegKey)
        //                        {
        //                            Regkeyinner = RegKey.OpenSubKey(strRegKey, true);
        //                            Regkeyinner.SetValue(strKey, KVPServer.Value);
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    RegKey.SetValue(KVPServer.Key, KVPServer.Value);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.WriteLog("SetRegistryEntries" + ex.Message.ToString() + ex.Source.ToString(), LogManager.enumLogLevel.Error);
        //        ExceptionManager.Publish(ex);
        //        return false;
        //    }
        //    return true;
        //}

        #region Print Functionality

        public void PrintFunction()
        {
            PrintDocument pd = new PrintDocument();
            pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            pd.Print();
        }

        public void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
        {
            GetCommonPrintValues(ev);
        }

        public void GetCommonValues(bool isVoidedTransaction, string Type, string treasury)
        {
            bool bEnablePrint = false;
            bool bCustTxn = false;
            PrintType = Type;
            isVoided = isVoidedTransaction;
            GetMachineDetailsTreasury(treasury);

            if (PrintType.ToUpper() == "REFILLS")
            {
                pNumber = 53;
                bEnablePrint = Settings.EnableRefillReceipt;
            }
            else if (PrintType.ToUpper() == "ATTENDANTPAY JACKPOT" || PrintType.ToUpper() == "MYSTERY JACKPOT" || PrintType.ToUpper() == "MANUAL ATTENDANTPAY JACKPOT" || PrintType.ToUpper() == "MANUAL MYSTERY JACKPOT")
            {
                pNumber = 42;
                if ((PrintType.ToUpper() == "ATTENDANTPAY JACKPOT"))
                {
                    PrintType = "Attendant Pay Jackpot";
                }
                else if ((PrintType.ToUpper() == "MANUAL ATTENDANTPAY JACKPOT"))
                {
                    PrintType = "Manual Attendant Pay Jackpot";
                }  
                bEnablePrint = Settings.EnableHandpayReceipt;
                bCustTxn = Settings.EnableCustomerReceipt;
            }
            else if (PrintType.ToUpper() == "REFUNDS")
            {
                pNumber = 53;
                bEnablePrint = Settings.EnableRefundReceipt;
                bCustTxn = Settings.EnableCustomerReceipt;
            }
            else if (PrintType.ToUpper() == "PROGRESSIVE" || PrintType.ToUpper() == "PROGRESSIVE JACKPOT" || PrintType.ToUpper() == "MANUAL PROGRESSIVE JACKPOT")
            {
                pNumber = 47;
                bEnablePrint = Settings.EnableProgReceipt;
                bCustTxn = Settings.EnableCustomerReceipt;
            }
            else if (PrintType.ToUpper() == "SHORTPAY")
            {
                pNumber = 49;
                bEnablePrint = Settings.EnableShortPayReceipt;
                bCustTxn = Settings.EnableCustomerReceipt;
            }
            else if (PrintType.ToUpper() == "ATTENDANTPAY CREDIT" || PrintType.ToUpper() == "MANUAL ATTENDANTPAY CREDIT")
            {
                if ((PrintType.ToUpper() == "ATTENDANTPAY CREDIT"))
                {
                    PrintType = "Attendant Pay Credit";
                }
                else if ((PrintType.ToUpper() == "MANUAL ATTENDANTPAY CREDIT"))
                {
                    PrintType = "Manual Attendant Pay Credit";
                }                 
               pNumber = 44;                
               bEnablePrint = Settings.EnableHandpayReceipt;
               bCustTxn = Settings.EnableCustomerReceipt;                
            }
            else if (PrintType.ToUpper() == "VOUCHER ISSUE")
            {
                bEnablePrint = Settings.EnableIssueReceipt;
            }
            else if (PrintType.ToUpper() == "VOUCHER REDEMPTION")
            {
                pNumber = 44;
                bEnablePrint = Settings.EnableVoucher;
            }
            else if (PrintType.ToUpper() == "OFFLINE VOUCHER REDEMPTION")
            {
                pNumber = 44;
                bEnablePrint = Settings.EnableVoucher;
            }
            if (isVoidedTransaction)
            {
                VoidType = PrintType;
                PrintType = "Void Transaction";
                bCustTxn = Settings.EnableCustomerReceipt;
            }
            if (bEnablePrint)
                PrintFunction();
            if (bCustTxn)
                PrintFunction();
        }

        public void GetCommonValues(Voucher voucherData)
        {
            voucher = voucherData;

            Header = voucher.Header;

            if (Settings.EnableIssueReceipt)
            {
                PrintType = "Voucher Issue";
                BMC.Common.LogManagement.LogManager.WriteLog("Inside CommonUtilites Settings.EnableIssueReceipt=: " +
                    Settings.EnableIssueReceipt.ToString() + "object ticketdetail=:" + voucher.ToString()
                    , BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);

                PrintFunction();
            }
        }

        public void GetCommonValues(RTOnlineTicketDetail RTOnlineTicketDetail, RTOnlineReceiptDetail RTOnlineReceiptDetail)
        {
            oRTOnlineTicketDetail = RTOnlineTicketDetail;
            oRTOnlineReceiptDetail = RTOnlineReceiptDetail;
            
            
            
            if (Settings.EnableVoucher)
                PrintType = "VOUCHER REDEMPTION";
            BMC.Common.LogManagement.LogManager.WriteLog("Inside CommonUtilites Settings.EnableVoucher=: "+
                Settings.EnableVoucher.ToString() + "object ticketdetail=:" + oRTOnlineTicketDetail.ToString()
                + "object ReceiptDetail=:" + RTOnlineReceiptDetail.ToString(), BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            PrintFunction();
        }

        //Add method for offline receipt print
        public void GetCommonValues(RTOnlineTicketDetail RTOnlineTicketDetail, RTOnlineReceiptDetail RTOnlineReceiptDetail,string OfflineHeaderText)
        {
            oRTOnlineTicketDetail = RTOnlineTicketDetail;
            oRTOnlineReceiptDetail = RTOnlineReceiptDetail;
            if (Settings.EnableVoucher)
                PrintType = "OFFLINE VOUCHER REDEMPTION";
            BMC.Common.LogManagement.LogManager.WriteLog("Inside CommonUtilites Settings.EnableVoucher=: " +
                Settings.EnableVoucher.ToString() + "object ticketdetail=:" + oRTOnlineTicketDetail.ToString()
                + "object ReceiptDetail=:" + RTOnlineReceiptDetail.ToString(), BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            PrintFunction();
        }



        public void GetCommonValues(OfflineTicket OfflineTicket, int treasuryNo)
        {
            oOfflineTicket = OfflineTicket;            
            if (Settings.EnableVoucher)
                PrintType = "OFFLINE REDEMPTION";
            BMC.Common.LogManagement.LogManager.WriteLog("Inside CommonUtilites Settings.EnableVoucher=: " +
             Settings.EnableVoucher.ToString() + "object offlineticket=:" + oOfflineTicket.ToString()
             , BMC.Common.LogManagement.LogManager.enumLogLevel.Debug);
            GetMachineDetailsTreasury(treasuryNo.ToString());
            PrintFunction();
        }

        public void GetCommonPrintValues(System.Drawing.Printing.PrintPageEventArgs ev)
        {
           
            printFont = new Font("Courier", 12, FontStyle.Bold);
            yPos = 0;
            //leftMargin = ev.MarginBounds.Left;
            leftMargin = 5;
            topMargin = ev.MarginBounds.Top;
            int SignatureTopMarginBase = 0;
            int SignatureInterval = 0;
            int SignDisplayCount = 0;
            bool PrintSequence = false;

           
            if ((Settings.HeadCashierSig == "") || (Settings.HeadCashierSig == null))
            {
                Settings.HeadCashierSig = "10000";
            }

            if ((Settings.ManagerSig == "") || (Settings.HeadCashierSig == null))
            {
                Settings.HeadCashierSig = "200000";
            }

            if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                SignatureInterval = 8;
            else
                SignatureInterval = 4;

            m_currRequiresHeadCashierSig = Convert.ToDouble(Settings.HeadCashierSig) / 100;
            m_currRequiresManagerSig = Convert.ToDouble(Settings.ManagerSig) / 100;

            try
            {
                //line = "Cash Desk" + Environment.NewLine + PrintType + " Receipt." + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                //yPos = topMargin + (printFont.GetHeight(ev.Graphics));
                //ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                if (isVoided)
                {
                    line = "Cash Desk" + Environment.NewLine + PrintType + " Receipt for " + VoidType + "." + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    yPos = topMargin + (printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                else
                {
                    if(Header.ToUpper().Trim()=="CASH DESK VOUCHER")
                    line = "Cash Desk" + Environment.NewLine + PrintType + " Receipt." + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    else if (Header.ToUpper().Trim() == "PLAYABLE VOUCHER" || (Header.ToUpper().Trim() == "CASHABLE PROMO VOUCHER"))
                        line = "Promotional" + Environment.NewLine + PrintType + " Receipt." + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                    yPos = topMargin + (printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }

                printFont = new Font("Courier", 10, FontStyle.Regular);

                line = Settings.SiteName + Environment.NewLine + Settings.SiteCode + Environment.NewLine + "".PadRight(number - 10, cpad1) + Environment.NewLine
                    + PrintType + " Details" + Environment.NewLine + "".PadRight(number - 10, cpad2);
                yPos = topMargin + (4 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                System.Globalization.DateTimeFormatInfo cultureInfo = new System.Globalization.CultureInfo(ExtensionMethods.CurrentDateCulture, false).DateTimeFormat;
                line = "Date/Time:".PadRight(number1 + 5, ctab) + System.DateTime.Now.ToString(cultureInfo.ShortDatePattern) + " " + System.DateTime.Now.ToString(cultureInfo.ShortTimePattern);
                yPos = topMargin + (10 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                line = "Workstation:".PadRight(number1+5, ctab) + System.Environment.MachineName.ToString();
                yPos = topMargin + (11 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                int user_no=0;
                user_no = oRTOnlineTicketDetail.AuthorizedUser_No;
                if (currentUser == null && user_no == 0)
                {
                    line = "User:".PadRight(number1 + 8, ctab) + BMC.Security.SecurityHelper.CurrentUser.UserName;
                }
                else if (currentUser != null)
                {
                    line = "User:".PadRight(number1 + 8, ctab) +  currentUser.UserName;
                }
                else
                {
                    line = "User:".PadRight(number1 + 8, ctab) + oRTOnlineTicketDetail.RedeemedUser;
                }
                 
                if (PrintType.ToUpper().Equals("VOUCHER REDEMPTION") || PrintType.ToUpper().Equals("OFFLINE VOUCHER REDEMPTION"))
                {
                    if (PrintType.ToUpper().Equals("OFFLINE VOUCHER REDEMPTION"))
                    {
                      //  line += System.Environment.NewLine + "Voucher Number".PadRight(number1, ctab) + oOfflineTicket.TicketBarCode + System.Environment.NewLine;
                        line += System.Environment.NewLine + "Voucher Number".PadRight(number1, ctab) + oRTOnlineTicketDetail.TicketString + System.Environment.NewLine;
                        yPos = topMargin + (12 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                        //CR 85479 Fix
                       // line = "Value".PadRight(number1+5, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(oOfflineTicket.PayableValue / 100) + Environment.NewLine;
                        line = "Value:".PadRight(number1 + 5, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(oRTOnlineTicketDetail.TicketValue / 100) + Environment.NewLine;
                        yPos = topMargin + (14 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                   }
                    else
                    {
                        line += System.Environment.NewLine + "Voucher Number:".PadRight(number1, ctab) + oRTOnlineTicketDetail.TicketString + System.Environment.NewLine;
                        yPos = topMargin + (12 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                        line = "Value:".PadRight(number1+5, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(oRTOnlineTicketDetail.TicketValue / 100) + Environment.NewLine;
                        yPos = topMargin + (14 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    }

                    line = "Printed at:".PadRight(number1, ctab) + oRTOnlineReceiptDetail.DeviceBarPosition + "".PadRight(5, ctab) + oRTOnlineReceiptDetail.PrintDevice + Environment.NewLine
                                + "".PadRight(number1 + number1 + 7, cpad1) ;
                    yPos = topMargin + (16 * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());                  
                }
                else if (PrintType.ToUpper().Equals("VOUCHER ISSUE"))
                {
                    line += System.Environment.NewLine + "Voucher Number:".PadRight(number1, ctab) + voucher.SBarCode.ToString() + System.Environment.NewLine;
                    yPos = topMargin + (12 * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                    line = "Value:".PadRight(number1+5, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(voucher.Value) + Environment.NewLine;
                    yPos = topMargin + (14 * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                    if (Header.ToUpper().Trim() == "CASHABLE PROMO VOUCHER")
                    {
                        line = "Type".PadRight(number1 + 5, ctab) + "Cashable" + Environment.NewLine;
                        yPos = topMargin + (15 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    }
                    else if (Header.ToUpper().Trim() == "PLAYABLE VOUCHER")
                    {
                        line = "Type".PadRight(number1 + 5, ctab) + "Non-Cashable" + Environment.NewLine;
                        yPos = topMargin + (15 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    }
                }
                else
                {   
                    yPos = topMargin + (12 * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                    line = "Value:".PadRight(number1+5, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(Convert.ToDouble(machineDetail.Value)) + Environment.NewLine;
                    yPos = topMargin + (15 * printFont.GetHeight(ev.Graphics));
                    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                }
                if (!PrintType.ToUpper().Equals("VOUCHER REDEMPTION") && !PrintType.ToUpper().Equals("OFFLINE VOUCHER REDEMPTION") && !PrintType.ToUpper().Equals("VOUCHER ISSUE"))
                {
                    SignatureTopMarginBase = 21;
                    SignDisplayCount = 1;
                    
                    PrintProcessedDetailsSection(ev, 16, pNumber);

                    if (!isVoided)
                    {
                        PrintSignatureArea(ev, SignatureTopMarginBase, "Customer", false);

                        if (Convert.ToDouble(machineDetail.Value) < m_currRequiresHeadCashierSig)
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            SignDisplayCount += 1;
                            if (!Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            PrintSequenceNo(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount));
                            SignDisplayCount += 1;
                        }
                        else if ((Convert.ToDouble(machineDetail.Value) >= m_currRequiresHeadCashierSig) && (Convert.ToDouble(machineDetail.Value) < m_currRequiresManagerSig))
                        {

                            PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            SignDisplayCount += 1;
                            PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            SignDisplayCount += 1;
                            PrintSequence = true;
                        }
                        else
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            SignDisplayCount += 1;
                            if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                SignDisplayCount += 1;
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            PrintSequence = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(machineDetail.Value.Contains("-") ? machineDetail.Value.Substring(1) : machineDetail.Value) < m_currRequiresHeadCashierSig)
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            PrintSequence = true;
                            if (!Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            
                        }
                        else if ((Convert.ToDouble(machineDetail.Value.Contains("-") ? machineDetail.Value.Substring(1) : machineDetail.Value) >= m_currRequiresHeadCashierSig) && (Convert.ToDouble(machineDetail.Value.Contains("-") ? machineDetail.Value.Substring(1) : machineDetail.Value) < m_currRequiresManagerSig))
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            SignDisplayCount += 1;
                            PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            PrintSequence = true;
                        }
                        else
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                            if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                SignDisplayCount += 1;
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                            }
                            PrintSequence = true;
                        }
                    }
                }
                else
                {
                    if (Header.ToUpper().Trim() == "PLAYABLE VOUCHER" || (Header.ToUpper().Trim() == "CASHABLE PROMO VOUCHER"))
                        SignatureTopMarginBase = 25;
                    else
                        SignatureTopMarginBase = 23;
                    SignDisplayCount = 1;

                    if (PrintType.ToUpper().Equals("VOUCHER ISSUE"))
                    {
                        if (Header.ToUpper().Trim() == "PLAYABLE VOUCHER" || (Header.ToUpper().Trim() == "CASHABLE PROMO VOUCHER"))
                        PrintIssueReceiptDetails(ev, 17);
                        else
                            PrintIssueReceiptDetails(ev, 15);

                        if (!isVoided)
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase, "Customer", false);

                            if (Convert.ToDouble(voucher.Value) < m_currRequiresHeadCashierSig)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                if (!Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                else
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 39);
                            }
                            else if ((Convert.ToDouble(voucher.Value) >= m_currRequiresHeadCashierSig) && (Convert.ToDouble(voucher.Value) < m_currRequiresManagerSig))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                //PrintSequenceNo(ev, 39);
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                    SignDisplayCount += 1;
                                }
                                else
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 47);
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(voucher.Value) < m_currRequiresHeadCashierSig)
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                //PrintSequenceNo(ev, 31);
                            }
                            else if ((Convert.ToDouble(voucher.Value) >= m_currRequiresHeadCashierSig) && (Convert.ToDouble(voucher.Value) < m_currRequiresManagerSig))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                //PrintSequenceNo(ev, 31);
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                    SignDisplayCount += 1;
                                }
                                else
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 39);
                            }
                        }
                    }
                    else
                    {
                        SignatureTopMarginBase = 19;
                        if (!isVoided)
                        {
                            PrintSignatureArea(ev, SignatureTopMarginBase, "Customer", false);

                            if (Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) < Convert.ToDouble(Settings.HeadCashierSig))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                if (!Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                else
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Approver", Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 39);
                            }

                            else if ((Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) >= Convert.ToDouble(Settings.HeadCashierSig)) && (Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) < Convert.ToDouble(Settings.ManagerSig)))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                //PrintSequenceNo(ev, 39);
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                SignDisplayCount += 1;
                                if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                    SignDisplayCount += 1;
                                }
                                else
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 47);
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) < Convert.ToDouble(Settings.HeadCashierSig))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                //PrintSequenceNo(ev, 31);
                            }
                            else if ((Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) >= Convert.ToDouble(Settings.HeadCashierSig)) && (Convert.ToDouble(oRTOnlineTicketDetail.TicketValue) < Convert.ToDouble(Settings.ManagerSig)))
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Gaming Manager", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                //PrintSequenceNo(ev, 31);
                            }
                            else
                            {
                                PrintSignatureArea(ev, SignatureTopMarginBase, "Cashier", !Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE);
                                if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
                                {
                                    PrintSignatureArea(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount), "Security", false);
                                    SignDisplayCount += 1;
                                }
                                //PrintSequenceNo(ev, 39);
                            }
                        }
                    }
                }

                if (PrintSequence)
                    PrintSequenceNo(ev, SignatureTopMarginBase + (SignatureInterval * SignDisplayCount));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            //return ev;
        }        

        public void PrintProcessedDetailsSection(PrintPageEventArgs ev, int iNumber, int alignNumber)
        {
            //string strTreasuryNo = machineDetail.TreasuryNumber;

            printFont = new Font("Courier", 10, FontStyle.Regular);

            //GetMachineDetailsTreasury(strTreasuryNo);

            //line = PrintType + " On".PadRight(pNumber - PrintType.Length) + machineDetail.BarPositionName + Environment.NewLine
            //    + "".PadRight(59, ctab) + machineDetail.MachineName + Environment.NewLine
            //    + "".PadRight(59, ctab) + machineDetail.StockNumber + Environment.NewLine
            //    + "".PadRight(number, cpad1);
            string[] arPrint = PrintType.Split(new char[] { ' ' });
            string strPrint;
            if (arPrint.Length > 1) { strPrint = arPrint[0] + Environment.NewLine + Convert.ToString(arPrint[1].Contains("Transaction") ? "Txn" : arPrint[1]); }
            else strPrint = arPrint[0] + Environment.NewLine;

            line = strPrint + " On".PadRight(14) + machineDetail.BarPositionName
                + "".PadRight(5, ctab) + machineDetail.StockNumber + Environment.NewLine
                + "".PadRight(number1 + number1 + 7, cpad1);
            yPos = topMargin + (iNumber * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
        }

        public void PrintSignatureArea(PrintPageEventArgs ev, int topLine, string strPrintText, bool ShowID)
        {
            if (Settings.SHOW_NAME_IN_RECEPIT_SIGNATURE)
            {
                line = strPrintText + " Name:" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "".PadRight(60, cpad3)
                    + Environment.NewLine + strPrintText + " Signature:" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "".PadRight(60, cpad3)
                    + Environment.NewLine;
            }
            else
            {
                line = strPrintText + " Signature" + (ShowID == true ? " / ID:" : ":") + Environment.NewLine + Environment.NewLine + Environment.NewLine + "".PadRight(60, cpad3)
                    + Environment.NewLine;
            }
            yPos = topMargin + (topLine * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
        }

        public void PrintIssueReceiptDetails(PrintPageEventArgs ev, int topLine)
        {
            int num = BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(voucher.Value).Length;
            num = (num > 7) ? ((num > 8) ? num + 1 : num) : num - 1;

            line = "".PadRight(number, cpad1) + System.Environment.NewLine
                + "Cash".PadRight((58 - num), ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(voucher.Value) + Environment.NewLine //+ voucher.Value.ToString("##.#0")
                + "Cheque ".PadRight(52, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(0.00) + Environment.NewLine
                + "Card".PadRight(55, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(0.00) + Environment.NewLine
                + "Points".PadRight(55, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(0.00) + Environment.NewLine
                + "Other".PadRight(55, ctab) + BMC.Business.CashDeskOperator.CommonUtilities.GetCurrency(0.00) + Environment.NewLine + "".PadRight(number, cpad1);
            yPos = topMargin + (topLine * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
        }

        private void PrintSequenceNo(PrintPageEventArgs ev, int topLine)
        {
            line = string.Format("{0}{1}     -     {2}", System.Environment.NewLine, "Sequence No", machineDetail.TreasuryNumber);
            yPos = topMargin + (topLine * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
        }

        #endregion

        #region GetCurrency

        public static string GetCurrency(double strUnitCashValue)
        {
            string strCashValue = string.Empty;
            // set currency format

            string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

            System.Globalization.NumberFormatInfo currencyFormat = new System.Globalization.CultureInfo(curCulture).NumberFormat;

            currencyFormat.CurrencyNegativePattern = 1;

            strCashValue = Convert.ToDecimal(strUnitCashValue).GetUniversalCurrencyFormatWithSymbolForRecipts();

            return strCashValue;
        }
        #endregion                  

        #region Get site service status
        public static string GetSiteServiceStatus()
        {
            return CommonDataAccess.GetSiteServiceStatus();
        }
        #endregion
    }
}
