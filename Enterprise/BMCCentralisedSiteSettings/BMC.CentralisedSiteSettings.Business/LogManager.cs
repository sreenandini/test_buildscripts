using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace BMC.CentralisedSiteSettings.Business
{
    public static class LogManager
    {
        public static void WriteLog(string strText)
        {
            FileInfo fiInfo;
            StreamWriter swWriter = null;
            string strFilePath;

            try
            {
                strFilePath = ConfigurationSettings.AppSettings["LogPath"].ToString();
                fiInfo = new FileInfo(strFilePath);
                swWriter = new StreamWriter(strFilePath, true);
                if (fiInfo.Exists)
                {
                    if (fiInfo.Length >= 1024*100)
                    {
                        fiInfo.CopyTo(strFilePath.Remove(strFilePath.Length - 5, 4) + DateTime.Now.ToString("dd-MMM-yyyy hh.mm.ss") + ".txt");
                        fiInfo.Delete();
                    }
                }
                swWriter.Write(DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + "\t");
                swWriter.WriteLine(strText);
            }
            catch (Exception)
            {
                
            }
            finally
            {
                strFilePath = null;
                fiInfo = null;
                if (swWriter != null)
                {
                    swWriter.Dispose();
                }
            }
        }
    }
}
