using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Common.Interfaces;
using System.Xml;
using BMC.Common.LogManagement;
using BMC.Common.ExceptionManagement;
using System.IO.Ports;

namespace TCKPrint
{
    public class CIth850 : ITCKPrint
    {

        private ITHACA850 oPrt = null;

        string Tag = "";
        string _PortNO = "";
        int C_GS = 29;
        int C_ESC = 27;
        string _CurrentVersion = "";
        char _CarriageReturn = Convert.ToChar(13);
        /// <summary>
        /// Initialize a new instance of CIth850 class
        /// </summary>
        /// <param name="PortNO">ex COM1:</param>
        public CIth850(string PortNO)
        {
            _PortNO = PortNO;
            oPrt = new ITHACA850();

        }

        /// <summary>
        /// Connect to ITHCA 850 Printer
        /// </summary>
        /// <param name="ErrorMsg"></param>
        /// <returns></returns>
        public bool Connect(ref string ErrorMsg)
        {
            bool retVal = false;
            try
            {
                ////  Read from the Initialization file
                Pinfo = new PrinterStatus();
                if (oPrt.IsOpen)
                {
                    oPrt.ClosePort();
                    //oPrt.Dispose();
                }

                oPrt.PortName = _PortNO;
                oPrt.BaudRate = 9600;
                oPrt.DataBits = 8;
                oPrt.StopBits = System.IO.Ports.StopBits.One;
                oPrt.WriteTimeout = 500;  /*in Milli Seconds */
                oPrt.ReadTimeout = 4000;  /*in Milli Seconds*/
                oPrt.Handshake = System.IO.Ports.Handshake.None;

                try
                {
                    _CurrentVersion = BMC.Common.ConfigurationManagement.ConfigManager.Read("ITHACAFRIMWAREVERSION");
                    oPrt.OpenPort();
                    retVal = true;

                }
                catch (Exception ex)
                {
                    Pinfo.StatusInt = -1;
                    Pinfo.Valid = false;
                    ErrorMsg = "OpenPort failed to open " + _PortNO + ", make sure no other devices are using this port.";
                    LogManager.WriteLog("ITHACA850 --> Error Occured:" + ErrorMsg, LogManager.enumLogLevel.Info);
                    LogManager.WriteLog("ITHACA850 --> Error Occured:" + ex.Message, LogManager.enumLogLevel.Info);
                }
                if (oPrt.IsOpen)
                {
                    byte[] Data = null;

                    switch (_CurrentVersion)
                    {
                        case "PP8522":
                            Data = new byte[] { 29, 83 };
                            break;
                        case "PP8576": //New Version
                            Data = new byte[] { 29, 122 };
                            break;
                        default:
                            Data = new byte[] { 29, 122 };
                            break;

                    }

                    LogManager.WriteLog("<1>", LogManager.enumLogLevel.Info);
                    oPrt.Write(Data, 0, Data.Length);
                    LogManager.WriteLog("<2>", LogManager.enumLogLevel.Info);
                    int result = oPrt.ReadByte();
                   
                    //GS S – Return Printer Status [ref pageno:141 in Itchaca 950]
                    if ((Convert.ToByte(result & 0xFF) & (Byte)(1 << 0)) == (Byte)(1 << 0) || (_CurrentVersion == "PP8576" && result == 62)) /*Is printer ready*/
                    {

                        Pinfo.StatusInt = 0;
                        Pinfo.StatusString = "Printer Ready";
                        Pinfo.Valid = true;
                        LogManager.WriteLog("ITHACA850 Printer Ready Version No: " + _CurrentVersion, LogManager.enumLogLevel.Info);
                    }
                    else
                    {
                        
                         if ((Convert.ToByte(result & 0xFF) & (Byte)(1 << 5)) == (Byte)(1 << 5))
                        {
                            /*Out of ticket*/
                            Pinfo.StatusInt = -1;
                            Pinfo.StatusString = "Out Of Paper";
                            Pinfo.Valid = false;
                            LogManager.WriteLog("ITHACA850 Version No:" + _CurrentVersion + " --> Out Of Paper", LogManager.enumLogLevel.Info);
                        }
                        if (oPrt.IsOpen)
                        {
                            oPrt.ClosePort();
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ErrorMsg = "Unable to Connect";
                LogManager.WriteLog("<3>", LogManager.enumLogLevel.Info);
                LogManager.WriteLog("ITHACA850 --> Error Occured:" + ex.Message, LogManager.enumLogLevel.Info);
                Pinfo.StatusInt = -1;
                Pinfo.Valid = false;
                if (oPrt.IsOpen)
                {
                    oPrt.ClosePort();
                }

            }
            return retVal;
        }




        private char GS
        {
            get
            {
                return Convert.ToChar(C_GS);
            }
        }
        private char ESC
        {
            get
            {
                return Convert.ToChar(C_ESC);
            }
        }
        private string REQ_STATUS
        {
            get
            {
                return (GS + "z");
            }
        }
        private string INIT_PTR
        {
            get
            {
                return ((char)C_ESC + "@");
            }
        }

        #region ITCKPrint Members

        /// <summary>
        /// Checks for Out Of Paper
        /// </summary>
        public bool OutOfPaper
        {
            get
            {
                bool retVal = false;
                if (this.Pinfo != null)
                {
                    retVal = !(this.Pinfo.Valid);
                }
                return retVal;
            }
        }

        /// <summary>
        /// Print a new ticket
        /// </summary>
        /// <param name="eTicket">pass TicketInfo struct</param>
        /// <returns></returns>
        public bool PrintTicket(TicketInfo eTicket)
        {
            bool retVal = false;
            try
            {

                if (eTicket.XMLLayout.ToUpper() == "BALLYISSUETICKET")
                {
                    CXMLParse parse = new CXMLParse();
                    parse.LoadXml(eTicket.XMLData);
                    LogManager.WriteLog("Start Printing...", LogManager.enumLogLevel.Debug);

                    switch (_CurrentVersion)
                    {
                        case "PP8522":
                            retVal = BallyTicketPP8522(parse);
                            break;
                        case "PP8576": //New Version
                            retVal = BallyTicketPP8576(parse);
                            break;
                        default:
                            retVal = BallyTicketPP8576(parse);
                            break;

                    }

                    LogManager.WriteLog("End Printing", LogManager.enumLogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);

            }
            return retVal;
        }

        private bool BallyTicketPP8522(CXMLParse parse)
        {
            bool retVal = false;
            try
            {
                int nWidth = 62;
                string stext = "";
                int nOffset = 0;

                ExecMacro(1, null, false);     // set start
                stext = parse.GTagData("TicketHeader").ToString();

                // stext = "CASH DESK VOUCHER";
                nOffset = ((nWidth - stext.Length) / 2);

                ExecMacro(320, stext.PadLeft((stext.Length + 5), ' '), false);

                SendCmd(GS + "!" + Convert.ToChar(0));          // Select character size = 1W/1H"
                SendCmd(ESC + "G" + Convert.ToChar(1));       // Set emphasized print"
                SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(0)); // Set absolute vertical position = 0"

                stext = parse.GTagData("PrintLocation");
                nOffset = (nWidth - stext.Length) / 2;
                SendCmd(DynamicText("".PadLeft(nOffset, ' ') + stext, false));
                SendCmd(DynamicText("", false));

                //  clear settings
                SendCmd(GS + "!" + Convert.ToChar(0));          // Select character size = 1W/1H"
                SendCmd(ESC + "G" + Convert.ToChar(1));       // Set emphasized print"
                SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(0)); // Set absolute vertical position = 0"

                stext = parse.GTagData("Date") + " " + parse.GTagData("Time") + "  VOUCHER: " + parse.GTagData("VoucherID");
                nOffset = ((nWidth - stext.Length) / 2);

                SendCmd(DynamicText("".PadLeft(nOffset, ' ') + stext, false));

                ExecMacro(318, parse.GTagData("BarcodeID"), false);

                SendCmd(GS + "!" + Convert.ToChar(0));          // Select character size = 1W/1H"
                SendCmd(ESC + "G" + Convert.ToChar(1));       // Set emphasized print"
                SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(0)); // Set absolute vertical position = 0"

                SendCmd(DynamicText("".PadLeft(13, ' ') + "VALIDATION: " + parse.GTagData("FormattedID"), false));
                // SendCmd(DynamicText("", false));
                stext = parse.GTagData("WordAmount"); //"Three Hundred Ten Pounds Twenty One Pence"

                nOffset = (nWidth - stext.Length) / 2;
                nOffset = (nOffset < 0) ? 0 : nOffset;
                SendCmd(DynamicText("".PadLeft(nOffset, ' ') + stext, false));
                SendCmd(DynamicText("", false));
                ExecMacro(320, "".PadLeft(5, ' ') + "Amount .... " + parse.GTagData("FormattedAmount"), false);
                // SendCmd(DynamicText("", false));
                SendCmd(GS + "!" + Convert.ToChar(0));          // Select character size = 1W/1H"
                SendCmd(ESC + "G" + Convert.ToChar(1));       // Set emphasized print"
                SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(0)); // Set absolute vertical position = 0"

                stext = parse.GTagData("VoucherVoid");
                nOffset = (nWidth - stext.Length) / 2;
                nOffset = (nOffset < 0) ? 0 : nOffset;
                SendCmd(DynamicText("".PadLeft(nOffset, ' ') + stext, true));

                retVal = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ITHACA850 --> Error Occured:" + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);

            }
            finally
            {
                if (oPrt.IsOpen)
                {
                    oPrt.ClosePort();
                }

            }
            return retVal;
        }

        private bool BallyTicketPP8576(CXMLParse parse)
        {
            bool retVal = false;
            try
            {


                ExecMacro(1, null, false);     // set start

                /* Voucher Name *(i.e. Cash Desk Voucher)*/
                string _sData = parse.GTagData("TicketHeader").Trim();
                _sData += _sData.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _VoucherName = Encoding.ASCII.GetBytes(_sData);

                VoucherName();
                SendSerialdata(_VoucherName);

                /* Site Name */
                string _sLocation = parse.GTagData("PrintLocation").Trim();
                _sLocation += _sLocation.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bLocation = Encoding.ASCII.GetBytes(_sLocation);
                LocationField();
                SendSerialdata(_bLocation);


                /*Datetime field*/
                string _sDatetimeValid = (parse.GTagData("Date") + " " + parse.GTagData("Time") + "  VOUCHER: " + parse.GTagData("VoucherID")).Trim();
                _sDatetimeValid += _sDatetimeValid.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bDatetimeValid = Encoding.ASCII.GetBytes(_sDatetimeValid);
                DateTimeCmd();
                SendSerialdata(_bDatetimeValid);

                /* Validation */
                string _sValidateData = ("VALIDATION:" + parse.GTagData("FormattedID")).Trim();
                _sValidateData += _sValidateData.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bValidateData = Encoding.ASCII.GetBytes(_sValidateData);
                ValidateField();
                SendSerialdata(_bValidateData);

                /* Display FormattedID In Vertical Mode */
                string _sFormattedIDV = parse.GTagData("FormattedID").Trim();
                _sFormattedIDV += _sFormattedIDV.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bFormattedIDV = Encoding.ASCII.GetBytes(_sFormattedIDV);
                FormattedIDVertical();
                SendSerialdata(_bFormattedIDV);

                /*BarCode field*/
                string _sBarCode = parse.GTagData("BarcodeID").Trim();
                _sBarCode += _sBarCode.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bBarCode = Encoding.ASCII.GetBytes(_sBarCode);
                BarCodeCmd();
                SendSerialdata(_bBarCode);

                /*Word Amount(i.e. Twelve Dollars and Fifty Five Cents )*/
                string _sWordAmount = parse.GTagData("WordAmount").Trim();
                _sWordAmount += _sWordAmount.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bWordAmount = Encoding.ASCII.GetBytes(_sWordAmount);
                WordAmount();
                SendSerialdata(_bWordAmount);

                /*FormattedAmount (i.e. $2.55 )*/
                string _sBoldAmount = parse.GTagData("FormattedAmount").Trim();
                _sBoldAmount += _sBoldAmount.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString();
                byte[] _bBoldAmount = Encoding.ASCII.GetBytes(_sBoldAmount);
                BoldAmount();
                SendSerialdata(_bBoldAmount);

                /*VoucherVoid (i.e. Voucher void after 10 days )*/
                string _sVoucherAfter = parse.GTagData("VoucherVoid").Trim();
                _sVoucherAfter += _sVoucherAfter.Contains(_CarriageReturn) ? string.Empty : _CarriageReturn.ToString(); 
                byte[] _bVoucherAfter = Encoding.ASCII.GetBytes(_sVoucherAfter);
                VoucherAfter();
                SendSerialdata(_bVoucherAfter);


                byte[] _bdata = { 12 };
                SendSerialdata(_bdata); /*initiate a Print*/


                retVal = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ITHACA850 --> Error Occured:" + ex.Message, LogManager.enumLogLevel.Info);
                ExceptionManager.Publish(ex);

            }
            finally
            {
                if (oPrt.IsOpen)
                {
                    oPrt.ClosePort();
                }

            }
            return retVal;
        }

        private bool SendCmd(string Data)
        {
            int result = 0;
            if (Tag != "NOCOMM")
            {
                // result = oPrt.SendString(Data, Data.Length);             
                oPrt.Write(Data);

            }
            return (result == 1);
        }

        private bool SendCmd(byte[] Data)
        {
            int result = 0;
            if (Tag != "NOCOMM")
            {
                // result = oPrt.SendString(Data, Data.Length);             
                oPrt.Write(Data, 0, Data.Length);

            }
            return (result == 1);
        }

        private string Macro(int MacroNum)
        {
            //  Built In Macros
            return (GS + ("O" + ((char)(MacroNum))));
        }

        private string DynamicText(string strText, bool FF)
        {
            //string n = (strText + (FF ? Convert.ToChar(12) : Convert.ToChar(13)));
            return (strText + (FF ? Convert.ToChar(12) : Convert.ToChar(13)));
        }

        private bool ExecMacro(int MacroNum, string Text, bool FF)
        {
            bool retVal = false;
            try
            {
                if (MacroNum > 400)
                {
                    SendCmd(Macro(MacroNum));
                }
                else
                {
                    byte[] bytear = null;
                    switch (MacroNum)
                    {
                        case 1:
                            SendCmd(INIT_PTR); //  Reset to Power-up
                            SendCmd(GS + "V" + Convert.ToChar(1)); //  Set Print Orientation
                            break;

                        case 318:
                            //  Predefined Macro 18 Setup for centered barcode field
                            //  Command Explanation
                            SendCmd(ESC + "t" + Convert.ToChar(3));
                            ////  Set Print Direction in page mode = D"
                            SendCmd(ESC + "G" + Convert.ToChar(0));
                            ////  Clear emphasized print"
                            //commented for to send in byte array instead of string
                            //SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(220));
                            //  Set absolute vertical position = 220"
                            //bytear = new byte[] { 29, Convert.ToByte('$'), 0, 220 };
                            //SendCmd(bytear);

                            bytear = new byte[] { 29, Convert.ToByte('A'), 0, 240 };
                            SendCmd(bytear);
                            //SendCmd(GS + "A" + Convert.ToChar(0) + Convert.ToChar(240));
                            //  Starting Position of Bar Code = 240"
                            SendCmd(GS + "W" + Convert.ToChar(4) + Convert.ToChar(9));
                            //  Set Bar Code Element Width Thin = 6, Thick = 10"
                            SendCmd(GS + "h" + Convert.ToChar(120));
                            //  Set Bar Code Height = 120", extended from 100 to 180 - cpt
                            SendCmd(GS + "k" + Convert.ToChar(7) + Convert.ToChar(18));
                            //  Print Bar Code (Interleaved 2 of 5, 18 characters)"
                            //  Follow with 18 bar code data characters!                            
                            break;
                        case 320:
                            //  Predefined Macro 20 Setup for numeric dollar amount field
                            //  Command Explanation
                            SendCmd(ESC + "t" + Convert.ToChar(1));//1
                            //  Set Print Direction in page mode = B"
                            SendCmd(ESC + "!" + Convert.ToChar(3));
                            //  Select print mode = 14x24, 12 CPI
                            SendCmd(GS + "!" + ((char)(17)));
                            //  Select character size = 2W/2H
                            SendCmd(ESC + "G" + Convert.ToChar(1));
                            //  Set emphasized print"
                            // SendCmd(GS + "$" + Convert.ToChar(0) + Convert.ToChar(220));
                            bytear = new byte[] { 29, Convert.ToByte('$'), 0, 220 };

                            SendCmd(bytear);

                            ////  Set absolute vertical position = 220"
                            bytear = new byte[] { 29, Convert.ToByte('F'), 1, 0, 0, 3, 192 };
                            SendCmd(bytear);
                            //SendCmd(GS + "F" + Convert.ToChar(1) + Convert.ToChar(0) + Convert.ToChar(0) + Convert.ToChar(3) + Convert.ToChar(192));
                            //  Set field (Center, 0, 960)"
                            //  Follow with dynamic text, terminated with  + chr(13)
                            break;


                    }
                }
                if ((Text != null && Text != ""))
                {
                    SendCmd(DynamicText(Text, FF));
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return retVal;
        }

        #region PP8576Members
        void VoucherName()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('!'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('!'), 17 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('$'), 0, 60 };
            SendSerialdata(_bData);

            //29, Convert.ToByte('F'), 1-Centering, 0-Start Postion, 0, 3, 192 
            _bData = new byte[] { 29, Convert.ToByte('F'), 1, 0, 0, 3, 192 };
            SendSerialdata(_bData);

        }

        void LocationField()
        {
            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('!'), 2 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('$'), 0, 110 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('F'), 1, 0, 0, 3, 192 };
            SendSerialdata(_bData);
        }
        void ValidateField()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('!'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('$'), 1, 86 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('F'), 0, 0, 192, 3, 192 };
            SendSerialdata(_bData);

        }

        void DateTimeCmd()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('!'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('$'), 0, 165 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('F'), 0, 0, 192, 3, 192 };
            SendSerialdata(_bData);

        }


        void BarCodeCmd()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('G'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('$'), 0, 220 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('A'), 1, 34 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('W'), 4, 8 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('h'), 100 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('k'), 7, 18 };
            SendSerialdata(_bData);

        }

        void WordAmount()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 27, Convert.ToByte('G'), 0 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('$'), 1, 120 };
            SendSerialdata(_bData);

            _bData = new byte[] { 29, Convert.ToByte('F'), 1, 0, 96, 3, 192 };
            SendSerialdata(_bData);


        }


        void BoldAmount()
        {
            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('!'), 1 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('!'), 0x22 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('$'), 1, 194 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('F'), 1, 0, 96, 3, 192 };
            SendSerialdata(_bData);
        }

        void VoucherAfter()
        {
            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 3 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('!'), 1 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('G'), 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('$'), 1, 215 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('F'), 1, 0, 96, 3, 192 };
            SendSerialdata(_bData);
        }

        void FormattedIDVertical()
        {

            byte[] _bData = new byte[] { 27, Convert.ToByte('t'), 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('!'), 2 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('!'), 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 27, Convert.ToByte('G'), 1 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('$'), 0, 0 };
            SendSerialdata(_bData);
            _bData = new byte[] { 29, Convert.ToByte('F'), 0x81, 0, 0, 1, 244 };
            SendSerialdata(_bData);
        }

        int SendSerialdata(byte[] bData)
        {
            int _iRet = 0;
            try
            {
                oPrt.Write(bData, 0, bData.Length);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in Writing Data:" + ex.Message, LogManager.enumLogLevel.Info);
                _iRet = -1;
            }

            return _iRet;
        }
        #endregion

        public PrinterStatus Pinfo
        {
            get;
            set;
        }


        #endregion
    }

    internal class CXMLParse
    {
        XmlDocument xdoc = null;
        public void LoadXml(string Xml)
        {
            xdoc = new XmlDocument();

            xdoc.LoadXml(Xml);
        }

        public string GTagData(string NodeName)
        {
            string retVal = "";
            XmlNode xnode = xdoc.SelectSingleNode("//" + NodeName);
            if (xnode != null)
            {
                retVal = xnode.InnerText;
            }
            return retVal;
        }

    }

    internal class ITHACA850 : SerialPort
    {
        public void OpenPort()
        {
            base.Open();
            GC.SuppressFinalize(this.BaseStream);
        }

        public void ClosePort()
        {
            GC.ReRegisterForFinalize(this.BaseStream);
            base.Close();
        }

    }
}
