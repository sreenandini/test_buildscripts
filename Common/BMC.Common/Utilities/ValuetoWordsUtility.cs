using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Globalization;

namespace BMC.Common.Utilities
{
    public class ValuetoWords
    {
        string[] arrSmallValues ={"One","Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten","Eleven",
                                    "Twelve","Thirteen","Fourteen","Fifteen","Sixteen","Seventeen","Eighteen","Ninteen"};

        string[] arrBigValues = { "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public string ConvertValueToWords(double byVal, string sRegion)
        {
            string sConverted = string.Empty;
            //string sVal = byVal.ToString("#####0'.'#0");
            string sVal = byVal.ToString("##.#0",new CultureInfo("en-GB"));

            char[] sep = { '.' };
            string[] vItems = sVal.Split(sep);

            if (sVal.IndexOf(".", 0) > 0 && vItems[0] != "0")
            {
                if (vItems.GetUpperBound(0) == 0)
                {
                    if (sRegion == "en-GB")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Pound(s) ";
                    }
                    else if (sRegion == "en-US")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Dollar(s) ";
                    }
                    else if (sRegion == "it-IT")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Euro(s) ";

                    }


                }
                if (vItems.GetUpperBound(0) == 1)
                {
                    if (sRegion == "en-GB")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Pound(s) ";
                    }
                    else if (sRegion == "en-US")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Dollar(s) ";
                    }
                    else if (sRegion == "it-IT")
                    {
                        sConverted = ConvertNum(Convert.ToString(vItems[0])) + " Euro(s) ";
                    }
                    if (vItems[1].ToString() == "00")
                    {
                        vItems[1] = "";
                    }

                    if (sRegion == "en-GB")
                    {
                        sConverted = sConverted + "and " + ConvertNum(Convert.ToString(vItems[1])) + " Pence ";
                    }
                    else if (sRegion == "en-US")
                    {
                        sConverted = sConverted + "and " + ConvertNum(Convert.ToString(vItems[1])) + " Cent(s) ";
                    }
                    else if (sRegion == "it-IT")
                    {
                        sConverted = sConverted + "and " + ConvertNum(Convert.ToString(vItems[1])) + " Cent(s) ";
                    }
                }


            }
            else
            {
                if (sRegion == "en-GB")
                {
                    sConverted = ConvertNum(Convert.ToString(vItems[1])) + " Pence ";
                }
                else if (sRegion == "en-US")
                {
                    sConverted = ConvertNum(Convert.ToString(vItems[1])) + " Cents ";
                }
                else if (sRegion == "it-IT")
                {
                    sConverted = ConvertNum(Convert.ToString(vItems[1])) + " Cents ";

                }

            }

            return sConverted;

        }
        public string ConvertNum(string sNum)
        {
            string sWords = string.Empty;
            string sProcessNumber = string.Empty;
            string sChunk = string.Empty;

            if (sNum.Length > 6)
            {
                sWords = " Amount is too Large ";
                return sWords;
            }
            if (sNum.Length == 0)
            {
                sWords = " No ";
                return sWords;
            }

            sProcessNumber = Right("000000" + sNum, 6);
            sChunk = Left(sProcessNumber, 3);
            if (Convert.ToInt16(sChunk) > 0)
            {
                sWords = sWords + ParseChunk(sChunk).TrimStart() + " Thousand ";//" " +
            }
            sChunk = Right(sProcessNumber, 3);

            if (Convert.ToInt16(sChunk) > 0)
            {
                sWords = sWords + ParseChunk(sChunk).TrimStart();//+ " "
            }
            return sWords;
        }
        public string ParseChunk(string sChunk)
        {
            string sdigits;
            string sWords = string.Empty;
            int iLeftdigit;
            int iRightdigit;

            sdigits = Left(sChunk, 1);

            if (Convert.ToInt16(sdigits) > 0)
            {
                sWords = sWords + " " + arrSmallValues[Convert.ToInt16(sdigits) - 1] + " Hundred ";
            }
            sdigits = Mid(sChunk, 1, 2);
            if (Convert.ToInt16(sdigits) > 19)
            {
                iLeftdigit = Convert.ToInt16(Mid(sChunk, 1, 1));
                iRightdigit = Convert.ToInt16(Mid(sChunk, 2, 1));

                sWords = sWords + "  " + arrBigValues[iLeftdigit - 1];
                if (iRightdigit > 0)
                {
                    sWords = sWords + " " + arrSmallValues[iRightdigit - 1];
                }
            }
            else
            {
                if (Convert.ToInt16(sdigits) > 0)
                {
                    sWords = sWords + " " + arrSmallValues[Convert.ToInt16(sdigits) - 1];
                }
            }
            return sWords;

        }
        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }
        public static string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }
        public static string Mid(string param, int startIndex, int length)
        {

            string result = param.Substring(startIndex, length);
            return result;
        }




        public string ConvertValueToWords(long p, string p_2)
        {
            throw new NotImplementedException();
        }
    }
}
