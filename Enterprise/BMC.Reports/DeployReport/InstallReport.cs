
#region [Using]

using System;
using System.IO;
using DeployReport.ReportExecution2005;

#endregion [Using]

namespace Bally.DeployReport
{
	class InstallReport
	{

        #region " Created By "

        /// <summary>
        /// /////////////////////////////////////////////
        /// Created By    :Sudhanshu Kumar Pandey
        /// Purpose       :For Deploying Report on Target Report Server
        /// Date          :26-Sep-2006
        /// Modified Date :27-sep-2006

        #endregion

        /// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
        {
           
             if (args.Length == 3) //for command line args
             {
                string server="";
                string uid ="";
                string pwd=""; 
                try
                {
                     server = args[0].ToString();
                     uid = args[1].ToString();
                     pwd = args[2].ToString();
                }
                catch (Exception ex)
                {
                  Console.WriteLine(ex.Message);
                }
                ReportInstaller _installer = new ReportInstaller(server, uid, pwd);
            }
            else //for directly running exe 
            {
                ReportInstaller _installer = new ReportInstaller();
                Console.WriteLine("Reports Deployment finished.");
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }                  
           
		}
	}
}