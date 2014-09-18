using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using BMC.Common.ExceptionManagement;
using BMC.Common.LogManagement;
using BMC.Common.ConfigurationManagement;

namespace BMC.ExchangeTicketExportService
{
    public partial class ExchangeTicketExportService : ServiceBase
    {        
        public static bool bTicketProcessing = false;
        public static bool bCVProcessing = false;
        string strConnectionString = String.Empty;
        System.Timers.Timer timerBMCTicketExport = new System.Timers.Timer();
        //System.Timers.Timer timerCVExport = new System.Timers.Timer();

        public ExchangeTicketExportService()
        {
            InitializeComponent();

            try
            {
                //Get the Exchange connection string.
                strConnectionString = DataAccess.GetExchangeConnectionString();

                //Stop and Start the SQLDependency to rergster the connection string.
                //SqlDependency.Stop(strConnectionString);
                //SqlDependency.Start(strConnectionString);

                //LogManager.WriteLog("Started SQL Dependency", LogManager.enumLogLevel.Info);
            }
            catch (Exception exBMCTicketingBackupService)
            {
                ExceptionManager.Publish(exBMCTicketingBackupService);
            }

            //Set the Ticket timer.
            timerBMCTicketExport.Elapsed += new System.Timers.ElapsedEventHandler(timerBMCTicketExport_Elapsed);
            timerBMCTicketExport.Enabled = true;
            try
            {
                timerBMCTicketExport.Interval = Convert.ToDouble(ConfigurationSettings.AppSettings["TimerinMilliSeconds"]);
            }
            catch (Exception ex)
            {
                timerBMCTicketExport.Interval = Convert.ToDouble(60000);
            }
            
            //if (BusinessClass.IsRegulatoryEnabled())
            //{
            //    //Set the CV timer.
            //    timerCVExport.Elapsed += new System.Timers.ElapsedEventHandler(timerCVExport_Elapsed);
            //    timerCVExport.Enabled = true;
            //    try
            //    {
            //        timerCVExport.Interval = Convert.ToDouble(ConfigurationSettings.AppSettings["CVTimerinMilliSeconds"]);
            //    }
            //    catch (Exception ex)
            //    {
            //        timerCVExport.Interval = Convert.ToDouble(30000);
            //    }
            //    timerCVExport.Start();
            //}
            //else
            //{
            //    LogManager.WriteLog("CV Timer Not Started. Regulatory Not Enabled.", LogManager.enumLogLevel.Info);
            //}
        }

        void timerBMCTicketExport_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LogManager.WriteLog("Ticket Export Timer Elapsed --> " + DateTime.Now.ToShortTimeString() + ". Currently processing - " + bTicketProcessing.ToString(), LogManager.enumLogLevel.Info);

            if (!bTicketProcessing)
            {
                bTicketProcessing = true;
                timerBMCTicketExport.Enabled = false;

                //Call the export method to export the required details.
                bool bStatus = BusinessClass.ExportTicketDetails();

                timerBMCTicketExport.Enabled = true;
                bTicketProcessing = false;
            }
        }

        void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //LogManager.WriteLog("SQL Dependency Triggered. Currently processing - " + bCVProcessing.ToString(), LogManager.enumLogLevel.Info);

            //if (!bTicketProcessing)
            //{
            //    bTicketProcessing = true;

            //    SqlDependency objSQLDep = (SqlDependency)sender;

            //    //Remove the registration to skip further firing of events till this activity is complete.
            //    objSQLDep.OnChange -= sqlDependency_OnChange;

            //    //Call the export method to export the required details.
            //    bool bStatus = BusinessClass.ExportTicketDetails();

            //    //Register the SQLDependency again.
            //    RegisterSQLDependency();

            //    bTicketProcessing = false;
            //}
        }

        protected void RegisterSQLDependency()
        {
            //try
            //{                
            //    SqlConnection conn = new SqlConnection(strConnectionString);
            //    SqlCommand command = new SqlCommand("rsp_CheckTicketExportTable", conn);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Notification = null;

            //    //Set the SQLDepencdency to the SQLCommand object.
            //    SqlDependency sqlDependency = new SqlDependency(command);

            //    sqlDependency.OnChange += new OnChangeEventHandler(sqlDependency_OnChange);

            //    //Execute the command.
            //    conn.Open();
            //    command.ExecuteReader(CommandBehavior.CloseConnection);

            //    LogManager.WriteLog("RegisterSQLDependency", LogManager.enumLogLevel.Info);
               
            //}
            //catch (Exception exRegisterSQLDependency)
            //{
            //    ExceptionManager.Publish(exRegisterSQLDependency);
            //}           
        }

        protected override void OnStart(string[] args)
        {
            timerBMCTicketExport.Start();
            LogManager.WriteLog("Service Started Successfully...", LogManager.enumLogLevel.Info);
        }

        protected override void OnStop()
        {
          //  SqlDependency.Stop(strConnectionString);
            BusinessClass.m_reset.Set();
            timerBMCTicketExport.Stop();
            //timerCVExport.Stop();
        }

        //void timerCVExport_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    LogManager.WriteLog("CV Timer Elapsed --> " + DateTime.Now.ToShortTimeString() + ". Currently processing - " + bCVProcessing.ToString(), LogManager.enumLogLevel.Info);

        //    if (!bCVProcessing)
        //    {
        //        bCVProcessing = true;
        //        timerCVExport.Enabled = false;

        //        //Call the export method to export the required details.
        //        bool bStatus = BusinessClass.ExportCVDetails();

        //        timerCVExport.Enabled = true;
        //        bCVProcessing = false;
        //    }
        //}
    }
}
