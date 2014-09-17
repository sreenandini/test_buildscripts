using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using BMC.Common.LogManagement;

namespace HFXVH
{
    public class Ticket
    {
        [DllImport("sdgcrc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Getcrc(ref byte param1, long param2);
        [DllImport("sdgcrc.dll")]
        public static extern int Getchk(ref byte param1, int param2);
        [DllImport("kernel32")]
        private static extern void CopyMemory(
         System.IntPtr Destination,  // pointer to address of copy destination
         System.IntPtr Source, // pointer to address of block to copy
         System.Int32 Length        // size, in bytes, of block to copy
       );

        /// <summary>
        /// It is used for Check CRC for a given bar code
        /// </summary>
        /// <param name="sBarcode">BarCode </param>
        /// <param name="lValue">Ticket amount</param>
        /// <param name="SiteCode">Optional</param>
        /// <param name="Datapak">Optional</param>
        /// <returns></returns>
        public bool CheckCRC(string sBarcode, long lValue, string SiteCode, string Datapak)
        {
            try
            {
                if (sBarcode.Length == 8)
                {
                    Datapak = string.Concat(Datapak, "0000");
                    sBarcode = "1" + SiteCode.Substring(SiteCode.Length - 3, 3) + Datapak.Substring(0, 5) + "0" + sBarcode.Substring(0, 4) + sBarcode.Substring(sBarcode.Length - 4, 4);
                }
                return (CreateCRC(sBarcode, lValue) == sBarcode);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CheckCRC Message:" + ex.Message, LogManager.enumLogLevel.Error);
                return false;
            }
        }

        private string CreateCRC(string sBarCode, long lAmount)
        {

            try
            {
                int crcRetVal = 0;
                byte[] bArr = new byte[41];

                GetAmt(lAmount, bArr);
                byte[] bArr1 = Encoding.ASCII.GetBytes(sBarCode.Substring(0, 1));
                bArr[20] = bArr1[0];
                for (int i = 1; i <= 13; i++)
                {
                    bArr[i + 20] = Encoding.ASCII.GetBytes(sBarCode.Substring(i, 1))[0];

                }
                //Get CRC Amount from sdgcrc.dll
                crcRetVal = Getcrc(ref bArr[0], 34);
                string formattedCRCVal = crcRetVal.ToString("D3");

                for (int i = 0; i < formattedCRCVal.Length; i++)
                {
                    byte[] bArr2 = Encoding.ASCII.GetBytes(formattedCRCVal.Substring(i, 1));
                    bArr[i + 34] = bArr2[0];
                }
                //Get Char from sdgcrc.dll
                char ctemp = Convert.ToChar(Getchk(ref bArr[20], 17));
                return (sBarCode.Substring(0, 14) + formattedCRCVal + ctemp);
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("CreateCRC Message:" + ex.Message, LogManager.enumLogLevel.Error);
                return "";
            }
        }

        private void GetAmt(long Amt, byte[] Outbuf)
        {
            try
            {
                byte[] aArr = new byte[4];
                IntPtr Destinptr;
                IntPtr sourceptr;
                GCHandle myGC = GCHandle.Alloc(aArr, GCHandleType.Pinned);
                GCHandle myGC1 = GCHandle.Alloc(Amt, GCHandleType.Pinned);

                Destinptr = myGC.AddrOfPinnedObject();
                sourceptr = myGC1.AddrOfPinnedObject();
                CopyMemory(Destinptr, sourceptr, 4);

                Outbuf[16] = aArr[3];
                Outbuf[17] = aArr[2];
                Outbuf[18] = aArr[1];
                Outbuf[19] = aArr[0];
                myGC1.Free();
                myGC.Free();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetAmt Message:" + ex.Message, LogManager.enumLogLevel.Error);

            }

        }
    }
}
