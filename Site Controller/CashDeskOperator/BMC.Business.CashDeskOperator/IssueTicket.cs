using System;
using System.Xml;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.Utilities;
using BMC.DBInterface.CashDeskOperator;
using TCKPrint;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using Gen2PrinterLib;
using System.Windows;
using PrinterCommLib;
using System.Globalization;

namespace BMC.Business.CashDeskOperator
{

    /// <summary>
    /// Returns the bar code number for the ticket number entered.
    /// </summary>
    /// <param name="ObjCDOEntity"></param>
    /// <returns></returns>

    public partial class IssueTicket
    {
        //DBBuilder IssueTicketDBAccess = new DBBuilder();                      
        XmlDocument XMLDocument = new XmlDocument();
        private string TicketNumber;
        int Amount;
        IssueTicketDataAccess issueTicketDB = new IssueTicketDataAccess();


        public PrintTicketErrorCodes CreateTicket(IssueTicketEntity CDOEntity, ref Voucher voucher)
        {
            PrintTicketErrorCodes ErrorCode = PrintTicketErrorCodes.Failure;
            try
            {
                if ((ErrorCode = Generate(CDOEntity)) == PrintTicketErrorCodes.Success)
                {
                    if ((ErrorCode = issueTicketDB.SaveTicketIssueDetails(CDOEntity)) == PrintTicketErrorCodes.Success)
                        TicketIssueCreate(CDOEntity, ref voucher);

                    return PrintTicketErrorCodes.Success;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ErrorCode;
        }
        /// <summary>
        /// Generate bar code number for the ticket number entered.
        /// </summary>
        /// <param name="_ObjCDOEntity"></param>
        /// <returns></returns>

        private PrintTicketErrorCodes Generate(IssueTicketEntity IssueTicketEntity)
        {
            PrintTicketErrorCodes ErrorCode;
            try
            {
                if ((ErrorCode = issueTicketDB.TicketCreateRequest(IssueTicketEntity)) == PrintTicketErrorCodes.Success)
                    if ((ErrorCode = PrintTicket(IssueTicketEntity)) == PrintTicketErrorCodes.Success)
                        return issueTicketDB.TicketPrintConfirmed(IssueTicketEntity);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return PrintTicketErrorCodes.Exception;
            }
            return ErrorCode;
        }
        private PrintTicketErrorCodes PrintTicket(IssueTicketEntity IssueTicketEntity)
        {
            //bool ShouldPrintTicket = false;

            ValuetoWords ValueToWord = new ValuetoWords();


            string ValueAsWords;

            XmlNode XMLNode;

            try
            {
                XMLDocument = new XmlDocument();
                XMLDocument.LoadXml("<Ticket><Doc></Doc></Ticket>");
                XMLNode = (XmlNode)XMLDocument.SelectSingleNode("Ticket/Doc");
                //ValueAsWords = ValueToWord.ConvertValueToWords(IssueTicketEntity.dblValue, Settings.Region); //IssueTicketDBAccess.Region().ToString());
                if (IssueTicketEntity.TicketHeader == null)
                    IssueTicketEntity.TicketHeader = "CASH DESK VOUCHER";
                XMLNode.AppendChild(AddNode("TicketHeader", IssueTicketEntity.TicketHeader));
                ValueAsWords = ValueToWord.ConvertValueToWords(IssueTicketEntity.dblValue, ExtensionMethods.CurrentSiteCulture);
                LogManager.WriteLog("ValueasWords:" + ValueAsWords, LogManager.enumLogLevel.Debug);

                XMLNode.AppendChild(AddNode("BarcodeID", IssueTicketEntity.BarCode));

                string sCon = Convert.ToInt64(IssueTicketEntity.BarCode).ToString("#-##-###-##-######-###-#");
                if (Settings.PrintTicket_EncryptDigits)
                {
                    string sEnc = sCon.Substring(0, 18) + "-***-" + sCon.Substring(23, 1);
                    LogManager.WriteLog("BarcodeID:" + sEnc, LogManager.enumLogLevel.Debug);
                    XMLNode.AppendChild(AddNode("FormattedID", sEnc));
                }
                else
                {
                    //XMLNode.AppendChild(AddNode("BarcodeID", IssueTicketEntity.BarCode));
                    LogManager.WriteLog("BarcodeID:" + sCon, LogManager.enumLogLevel.Debug);
                    XMLNode.AppendChild(AddNode("FormattedID", sCon));
                }

                XMLNode.AppendChild(AddNode("PrintLocation", Settings.PrintHeaderFormat)); //IssueTicketDBAccess.VoucherSite().ToString()));


                string sCurrency = Convert.ToDecimal(IssueTicketEntity.dblValue).GetUniversalCurrencyFormatWithSymbol();

                if (sCurrency.StartsWith("€") && Settings.VoucherPrinterName == "ITHACA950")
                {
                    sCurrency = sCurrency.Replace("€", Convert.ToChar(164).ToString());
                }
                else if (sCurrency.StartsWith("€") && Settings.VoucherPrinterName == "COUPONEXPRESS")
                {
                    sCurrency = sCurrency.Replace("€", "GBP");
                }
                //CR 85479 Fix
                else if (sCurrency.StartsWith("£") && (Settings.VoucherPrinterName == "COUPONEXPRESS" || Settings.VoucherPrinterName == "ITHACA850"))
                {
                    sCurrency = sCurrency.Replace("£", "GBP");
                }
                else if ((sCurrency.StartsWith("£") || sCurrency.StartsWith("€")) && Settings.VoucherPrinterName == "ITHACA850")
                {
                    sCurrency = Convert.ToDecimal(IssueTicketEntity.dblValue).GetUniversalCurrencyFormat();
                }
                else
                {
                    sCurrency = sCurrency.Replace("€", "EUR");
                }


                XMLNode.AppendChild(AddNode("FormattedAmount", sCurrency));

                LogManager.WriteLog("FormattedAmount:" + sCurrency, LogManager.enumLogLevel.Debug);
                // CommonUtilities.GetCurrency(IssueTicketEntity.dblValue).ToString()
                XMLNode.AppendChild(AddNode("WordAmount", ValueAsWords));
                //XMLNode.AppendChild(AddNode("Date", IssueTicketEntity.Date.GetUniversalDateFormat()));
                //LogManager.WriteLog("Date:" + IssueTicketEntity.Date.GetUniversalDateTimeFormat(), LogManager.enumLogLevel.Debug);
                Settings.TimeZoneID = Settings.TimeZoneID.Replace("_", " ");
                XMLNode.AppendChild(AddNode("Date", (Convert.ToDateTime(IssueTicketEntity.Date,new CultureInfo(ExtensionMethods.CurrentDateCulture))).ToString()));
                //  XMLNode.AppendChild(AddNode("Date", Settings.TimeZoneID.GetZoneDateinUTCFormat()));
                LogManager.WriteLog("Date:" + Settings.TimeZoneID.GetZoneDateinUTCFormat(), LogManager.enumLogLevel.Debug);
                //XMLNode.AppendChild(AddNode("Time", DateTime.Now.ToString("HH:mm:ss")));
                //LogManager.WriteLog("Time:" + DateTime.Now.ToString("HH:mm:ss"), LogManager.enumLogLevel.Debug);
                XMLNode.AppendChild(AddNode("VoucherID", Convert.ToString(IssueTicketEntity.TicketID)));
                LogManager.WriteLog("VoucherID:" + Convert.ToString(IssueTicketEntity.TicketID), LogManager.enumLogLevel.Debug);

                string sVoidString = string.Empty;
                //if (Settings.VoucherPrinterName.ToUpper() == "ITHACA950")
                //{
                //    sVoidString = Settings.Ticket_Expire + " " + Application.Current.FindResource("Days") as string;
                //}
                //else
                //{

                //}

                if (IssueTicketEntity.TicketHeader == "CASH DESK VOUCHER")
                {

                    sVoidString = Application.Current.FindResource("VoucherVoid") as string + " "
                                            + Settings.Ticket_Expire + " " + Application.Current.FindResource("Days") as string;
                }
                else if (IssueTicketEntity.TicketHeader == "PLAYABLE VOUCHER" || IssueTicketEntity.TicketHeader == "CASHABLE PROMO VOUCHER")
                {

                    sVoidString = Application.Current.FindResource("VoucherVoid") as string + " " + (Convert.ToDateTime(IssueTicketEntity.VoidDate,new CultureInfo(ExtensionMethods.CurrentDateCulture))).ToString();
                }
                LogManager.WriteLog("Ticket Header in Issue Ticket : " + IssueTicketEntity.TicketHeader, LogManager.enumLogLevel.Info);
                LogManager.WriteLog("Void String : " + sVoidString,LogManager.enumLogLevel.Info);

                XMLNode.AppendChild(AddNode("VoucherVoid", sVoidString));
                XMLDocument.ChildNodes[0].AppendChild(XMLNode);



                LogManager.WriteLog("XMLDoc:" + XMLDocument.OuterXml.ToString(), LogManager.enumLogLevel.Debug);


                switch (Settings.VoucherPrinterName.ToUpper())
                {
                    case "ITHACA850":
                        {
                            ITCKPrint iTicketInfo = new CIth850(Settings.PrinterPort);

                            TicketInfo structTicketInfo;
                            structTicketInfo.XMLLayout = "BallyIssueTicket";
                            structTicketInfo.XMLData = XMLDocument.OuterXml;

                            string ErrorMsg = "";
                            if (!iTicketInfo.Connect(ref ErrorMsg))
                            {
                                LogManager.WriteLog(ErrorMsg, LogManager.enumLogLevel.Info);
                                return PrintTicketErrorCodes.OpenCOMPortFailure;
                            }
                            if (iTicketInfo.Pinfo.Valid)
                            {
                                iTicketInfo.PrintTicket(structTicketInfo);
                                //ShouldPrintTicket = true;
                                LogManager.WriteLog("PrintTicket:" + "Completed with Success", LogManager.enumLogLevel.Info);
                                return PrintTicketErrorCodes.Success;
                            }
                            else
                            {
                                if (iTicketInfo.OutOfPaper)
                                {
                                    //ShouldPrintTicket = false;
                                    LogManager.WriteLog("PrintTicket : Printer Status - OutOfPaper", LogManager.enumLogLevel.Debug);
                                    LogManager.WriteLog("PrintTicket:" + "Completed with Failure", LogManager.enumLogLevel.Info);
                                    return PrintTicketErrorCodes.OutofPaper;

                                }
                            }
                            break;
                        }
                    case "ITHACA950":
                        {
                            LogManager.WriteLog("ITHACA950 Printer", LogManager.enumLogLevel.Debug);

                            //new Printer
                            //PrinterCommLib.PrinterCommIntClass oPrinterCommIntClass = new PrinterCommLib.PrinterCommIntClass();

                            PrinterCommLib.IPrinterCommInt oPrinterCommIntClass = new PrinterCommLib.PrinterCommInt();

                            LogManager.WriteLog("OpenSerialCom", LogManager.enumLogLevel.Debug);

                            LogManager.WriteLog("Printer Port = " + Settings.PrinterPort, LogManager.enumLogLevel.Info);

                            int iRetValue = oPrinterCommIntClass.OpenSerialCom(Settings.PrinterPort);//oPrinterCommIntClass.OpenSerialCom("COM1"); 
                            if (iRetValue != 0)
                            {
                                oPrinterCommIntClass.CloseSerialCom();
                                LogManager.WriteLog("PrintVoucher Failed. Unable to Open COM Port ", LogManager.enumLogLevel.Debug);
                                return PrintTicketErrorCodes.OpenCOMPortFailure;
                            }

                            int iPrintRetValue = oPrinterCommIntClass.PrintTicket(XMLDocument.OuterXml);

                            if (iPrintRetValue != 0)
                            {
                                oPrinterCommIntClass.CloseSerialCom();
                                LogManager.WriteLog("PrintVoucher Failed. Print Ticket Function Failed", LogManager.enumLogLevel.Debug);
                                return PrintTicketErrorCodes.PrintTicketFailure;
                            }

                            oPrinterCommIntClass.CloseSerialCom();

                            //ShouldPrintTicket = true;
                            LogManager.WriteLog("PrintVoucher:" + "Completed with Success", LogManager.enumLogLevel.Info);
                            return PrintTicketErrorCodes.Success;
                        }
                    case "COUPONEXPRESS":
                        {
                            LogManager.WriteLog("COUPONEXPRESS Printer", LogManager.enumLogLevel.Debug);
                            //Gen2PrinterIntClass oGen2PrinterIntClass = new Gen2PrinterIntClass();
                            Gen2PrinterLib.Gen2PrinterInt oGen2PrinterIntClass = new Gen2PrinterLib.Gen2PrinterInt();
                            LogManager.WriteLog("Printer Port = " + Settings.PrinterPort, LogManager.enumLogLevel.Info);

                            int PrinterDelayTime = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("PrinterDelayTime"));
                            int LoopCount = Convert.ToInt32(BMC.Common.ConfigurationManagement.ConfigManager.Read("PrinterDelayloopCount"));

                            int iRetValue = oGen2PrinterIntClass.OpenSerialCom(Settings.PrinterPort);//oPrinterCommIntClass.OpenSerialCom("COM1"); 



                            if (iRetValue != 0)
                            {
                                oGen2PrinterIntClass.CloseSerialCom();
                                int i = 0;
                                bool bSuccess = false;
                                while (i++ < LoopCount) /*Variable Parameter*/
                                {
                                    if ((iRetValue = oGen2PrinterIntClass.OpenSerialCom(Settings.PrinterPort)) != 0)
                                    {
                                        System.Threading.Thread.Sleep(PrinterDelayTime);
                                        oGen2PrinterIntClass.CloseSerialCom();
                                        System.Threading.Thread.Sleep(10); /*grace sleep before opening the port*/
                                    }
                                    else
                                    {
                                        bSuccess = true;
                                        break;
                                    }
                                }
                                if (bSuccess == false)
                                {
                                    oGen2PrinterIntClass.CloseSerialCom();
                                    switch (iRetValue)
                                    {
                                        case 257:
                                            {
                                                LogManager.WriteLog("eVoltErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eVoltErr;
                                            }
                                        case 258:
                                            {
                                                LogManager.WriteLog("eHeadErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eHeadErr;
                                            }
                                        case 260:
                                            {
                                                LogManager.WriteLog("ePaperOut", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePaperOut;
                                            }
                                        case 264:
                                            {
                                                LogManager.WriteLog("ePlatenUP", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePlatenUP;
                                            }
                                        case 272:
                                            {
                                                LogManager.WriteLog("eSysErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eSysErr;
                                            }
                                        case 288:
                                            {
                                                LogManager.WriteLog("eBusy", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eBusy;
                                            }
                                        case 513:
                                            {
                                                LogManager.WriteLog("eJobMemOF", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eJobMemOF;
                                            }
                                        case 514:
                                            {
                                                LogManager.WriteLog("eBufOF", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eBufOF;
                                            }
                                        case 516:
                                            {
                                                LogManager.WriteLog("eLibLoadErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eLibLoadErr;
                                            }
                                        case 520:
                                            {
                                                LogManager.WriteLog("ePRDataErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePRDataErr;
                                            }
                                        case 528:
                                            {
                                                LogManager.WriteLog("eLibRefErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eLibRefErr;
                                            }
                                        case 544:
                                            {
                                                LogManager.WriteLog("eTempErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eTempErr;
                                            }
                                        case 1025:
                                            {
                                                LogManager.WriteLog("eMissingSupplyIndex", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eMissingSupplyIndex;
                                            }
                                        case 1026:
                                            {
                                                LogManager.WriteLog("ePrinterOffline", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePrinterOffline;
                                            }
                                        case 1028:
                                            {
                                                LogManager.WriteLog("eFlashProgErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eFlashProgErr;
                                            }
                                        case 1032:
                                            {
                                                LogManager.WriteLog("ePaperInChute", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePaperInChute;
                                            }
                                        case 1040:
                                            {
                                                LogManager.WriteLog("ePrintLibCorr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePrintLibCorr;
                                            }
                                        case 1056:
                                            {
                                                LogManager.WriteLog("eCmdErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eCmdErr;
                                            }
                                        case 2049:
                                            {
                                                LogManager.WriteLog("ePaperLow", LogManager.enumLogLevel.Debug);
                                                //return PrintTicketErrorCodes.ePaperLow;
                                                break;
                                            }
                                        case 2050:
                                            {
                                                LogManager.WriteLog("ePaperJam", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.ePaperJam;
                                            }
                                        case 2052:
                                            {
                                                LogManager.WriteLog("eCurrentErr", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eCurrentErr;
                                            }
                                        case 2056:
                                            {
                                                LogManager.WriteLog("eJournalPrint", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eJournalPrint;
                                            }
                                        case 2064:
                                            {
                                                LogManager.WriteLog("eNone1", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eNone1;
                                            }
                                        case 2080:
                                            {
                                                LogManager.WriteLog("eNone2", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.eNone2;
                                            }
                                        default:
                                            {
                                                LogManager.WriteLog("General Failure", LogManager.enumLogLevel.Debug);
                                                return PrintTicketErrorCodes.Failure;
                                            }
                                    }

                                }
                            }

                            int iPrintRetValue = oGen2PrinterIntClass.PrintTicket(XMLDocument.OuterXml);

                            if (iPrintRetValue != 0)
                            {
                                oGen2PrinterIntClass.CloseSerialCom();
                                LogManager.WriteLog("PrintVoucher Failed. Print Ticket Function Failed", LogManager.enumLogLevel.Debug);
                                return PrintTicketErrorCodes.PrintTicketFailure;
                            }



                            oGen2PrinterIntClass.CloseSerialCom();

                            //ShouldPrintTicket = true;
                            LogManager.WriteLog("PrintVoucher:" + "Completed with Success", LogManager.enumLogLevel.Info);
                            return PrintTicketErrorCodes.Success;

                        }
                    default:
                        {
                            return PrintTicketErrorCodes.InvalidPrinterName;
                        }

                }

            }
            catch (Exception ex)
            {
                //ShouldPrintTicket = false;
                LogManager.WriteLog("PrintVoucher:" + "Completed with Failure", LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);
                return PrintTicketErrorCodes.Exception;
            }
            return PrintTicketErrorCodes.Success;
        }

        private XmlNode AddNode(string Name, string Value)
        {
            XmlNode Node = null;
            try
            {
                Node = XMLDocument.CreateNode(XmlNodeType.Element, Name, "");
                Node.InnerText = Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return Node;
        }

        public void TicketIssueCreate(IssueTicketEntity issueTicketEntity, ref Voucher voucherEntity)
        {
            try
            {
                if (GetTITOTicketInformation(issueTicketEntity, ref voucherEntity) == true)
                {
                    TicketNumber = issueTicketEntity.FullTicketNumber;
                    Amount = Convert.ToInt32(issueTicketEntity.dblValue);
                    voucherEntity.FullTicketNumber = issueTicketEntity.BarCode;
                    voucherEntity.Value = issueTicketEntity.dblValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public bool GetTITOTicketInformation(IssueTicketEntity issueTicketEntity, ref Voucher VoucherEntity)
        {
            bool HasGotTITOTicketInformation = false;

            try
            {
                if (issueTicketEntity.BarCode.Length == 18)
                {

                    if (issueTicketDB.GetTicketInfo(issueTicketEntity, ref VoucherEntity) == true)
                        if (issueTicketDB.GetMachineDetailsFromAsset(ref VoucherEntity) == true)
                            HasGotTITOTicketInformation = true;

                    HasGotTITOTicketInformation = true;
                }
                else
                    HasGotTITOTicketInformation = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return HasGotTITOTicketInformation;
        }

        [Obsolete]
        public void Epson_TicketIssueReceipt(IssueTicketEntity IssueTicketEntity)
        {
            try
            {
                TicketNumber = IssueTicketEntity.FullTicketNumber;
                //Voucher.FullTicketNumber = IssueTicketEntity.FullTicketNumber;
                //Voucher.Value = IssueTicketEntity.dblValue;
                Amount = int.Parse(IssueTicketEntity.dblValue.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }

        }


        public string GetPrinterInformation()
        {
            string sSerialInfo = "<DEFAULT>";

            if (Settings.VoucherPrinterName.ToUpper() == "COUPONEXPRESS")
            {
                LogManager.WriteLog("COUPONEXPRESS Printer", LogManager.enumLogLevel.Debug);
                //Gen2PrinterIntClass oGen2PrinterIntClass = new Gen2PrinterIntClass();
                Gen2PrinterLib.Gen2PrinterInt oGen2PrinterIntClass = new Gen2PrinterInt();
                LogManager.WriteLog("Printer Port = " + Settings.PrinterPort, LogManager.enumLogLevel.Info);
                int iRetValue = oGen2PrinterIntClass.OpenSerialCom(Settings.PrinterPort);//oPrinterCommIntClass.OpenSerialCom("COM1"); 
                if (iRetValue == 0)
                {
                    oGen2PrinterIntClass.GetSerialNumber(out sSerialInfo);
                    LogManager.WriteLog("oGen2PrinterIntClass.OpenSerialCom Success", LogManager.enumLogLevel.Info);
                }
                else
                    LogManager.WriteLog("oGen2PrinterIntClass.OpenSerialCom Error", LogManager.enumLogLevel.Info);
                oGen2PrinterIntClass.CloseSerialCom();
            }
            return sSerialInfo;
        }
    }
}
