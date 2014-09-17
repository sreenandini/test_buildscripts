#region [Using]
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using DeployReport.ReportService;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.Utilities;
#endregion [Using]

namespace Bally.DeployReport
{
	#region [Summary]
	/// <summary>
	/// Summary description for ReportInstaller.
	/// </summary>
	#endregion [Summary]


    #region " Created By "

    /// <summary>
    /// /////////////////////////////////////////////
    /// Created By    :Sudhanshu Kumar Pandey
    /// Purpose       :For Deploying Report on Target Report Server
    /// Date          :26-Sep-2006
    /// Modified Date :27-sep-2006
    /// Vineetha Mathew     03-Jul-2009 Modified To deploy reports based on Region
    /// Jisha Lenu George   07-Jan-2011 Modified to deploy the reports only for the US region

    #endregion


	[RunInstaller(true)]
	public class ReportInstaller : System.Configuration.Install.Installer
	{
		#region [Private Variables]
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Variable to hold the database provider type.
		/// </summary>
		private string _extension = string.Empty;

		/// <summary>
		/// Variable to hold the Servername to fetch the data for the report.
		/// </summary>
		private string _serverName = string.Empty;

		/// <summary>
		/// Variable to hold the DataSourceLocation in the report server for
		/// the deployed report.
		/// </summary>
		private string _datasourceLocation = string.Empty;

		/// <summary>
		/// Variable to hold the parent folder name in the report server to 
		/// deploy the report.
		/// </summary>
		private string _parent = string.Empty;

		/// <summary>
		/// Variable to hold the folder name in the report server to deploy the 
		/// report.
		/// </summary>
		private string _folder = string.Empty;

        /// <summary>
        /// Variable to hold the folder name in the report server to deploy the 
        /// report.
        /// </summary>
        private string _deployfolder = string.Empty;


		/// <summary>
		/// Variable to hold the ConnectionString for each report.
		/// </summary>
		private string _connectString = string.Empty;

		/// <summary>
		/// Variable to hold the webservice client to access the reporting server.
		/// </summary>
		private ReportingService2005 _rs 
			= new ReportingService2005();

		/// <summary>
		/// Variable to hold the database name to fetch the data for the report.
		/// </summary>
		private string _dbName = string.Empty;

		/// <summary>
		/// Variable to hold the datasource name used by the report.
		/// </summary>
		private string _datasource = string.Empty;

        ///// <summary>
        ///// Variable to hold the region used by the report.
        ///// </summary>
        //private string _RegionType = string.Empty;
		/// <summary>
		/// Variable to hold the full path of the report to be deployed.
		/// </summary>
		private string _reportPath = string.Empty;
       
        /// <summary>
        /// Variable to hold the user id
        /// </summary>
        private string _userID = string.Empty;
        
        /// <summary>
        /// Variable to hold the password
        /// </summary>
        private string _pWD = string.Empty;

        /// <summary>
        /// Variable to hold the password
        /// </summary>
        private string _MIMEType = string.Empty;

        /// <summary>
        /// Variable to hold the password
        /// </summary>
        private bool _IsReport = true;
        
        /// <summary>
        /// Variable to hold the web service for reorting service
        /// </summary>
        private string _strWebServicePath = string.Empty;

        /// <summary>
        /// Variable to hold the web service URL for reporting service
        /// </summary>
        private string _strWebServiceName = string.Empty;

		/// <summary>
		/// Variable to hold the properties of finditems.
		/// </summary>
		private CatalogItem[] _returnedItems;

		/// <summary>
		/// Collection to hold the reports to deploy.
		/// </summary>
		private ArrayList ConnectionStrings = new ArrayList();
		#endregion [Private Variables]

		#region [Constructor]
		/// <summary>
		/// Default Constructor.
		/// </summary>
		public ReportInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();

            //take the server name,user id and password from command line 
            Console.WriteLine("Enter Server Name: ");
            this._serverName = Console.ReadLine();
            Console.WriteLine("Enter Report Server User Name: ");
            this._userID = Console.ReadLine();
            Console.WriteLine("Enter Report Server Password:");
            this._pWD = Console.ReadLine(); 

			this._extension = this.GetValueFromConfig("appSettings", "Extension");
		    this._datasourceLocation = this.GetValueFromConfig("appSettings", "DataSourceLocation");
			this._parent = this.GetValueFromConfig("appSettings", "Parent");
            this._strWebServiceName = this.GetValueFromConfig("appSettings", "WebServiceName");
            
            this._folder = GetSetting("ReportFolder", "BMCReports");
            this._deployfolder = this.GetValueFromConfig("appSettings", "Foldername");
            
            //this._strWebServicePath = this.GetValueFromConfig("appSettings", "WebServicepath");
            this._strWebServicePath = GetSetting("ReportServerURL","localhost");
            if (this._strWebServicePath != null && this._strWebServicePath != string.Empty && this._strWebServicePath.ToUpper().ToString() != "[REPORTSERVERNAME]")
            {
                this._strWebServicePath = this.GetValueFromConfig("appSettings", "protocol").Trim() + this._strWebServicePath.Trim() +  this._strWebServiceName.Trim();
            }
            else
            {
                MessageBox.Show("Please set the value for 'WebServicepath' in config file","Deploy Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
			// Set security credentials for web service client authorization.
			_rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            _rs.Url = this._strWebServicePath;
           // _rs.Url = "http://localhost/ReportServer/ReportService2005.asmx";
         try
         {
         	// Create report folder to drop report in.
			if ( this._folder.Length > 0 && this._parent.Length > 0)
			{			
				// Create folder if not exists.
				if (this.CheckExist(ItemTypeEnum.Folder, 
					this._parent, this._folder) == false)
				{
					_rs.CreateFolder( this._folder, this._parent, null);        	
				}
			}
			else
			{
				// Log the error.				
			}
            // Create data source folder to drop data source in.
            if (this._datasourceLocation.Length > 0 && this._datasourceLocation.Length > 0)
            {
                // Create folder if not exists.
                if (this.CheckExist(ItemTypeEnum.Folder ,
                    this._parent, this._datasourceLocation) == false)
                {
                    _rs.CreateFolder(this._datasourceLocation, this._parent, null);
                }
            }
            else
            {
                // Log the error.				
            }
           }
          catch (System.Net.WebException e)
          {
                Console.WriteLine("This program is expected to throw WebException on successful run." +
                                    "\n\nException Message :" + e.Message);
                if (e.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((System.Net.HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((System.Net.HttpWebResponse)e.Response).StatusDescription);
                }
           }
           catch (Exception e)
           {
                Console.WriteLine(e.Message);
           }
			this.GetReportsToDeploy("ReportDetails");
			this.Deploy();
		}

        public ReportInstaller(string server,string uid,string pwd)
        {
            // This call is required by the Designer.
            InitializeComponent();

            //take the server name,user id and password from command line as per hari suggestion --12-oct-2006
            this._serverName = server;
            this._userID = uid;
            this._pWD = pwd;

            this._extension = this.GetValueFromConfig("appSettings", "Extension");
            this._datasourceLocation = this.GetValueFromConfig("appSettings", "DataSourceLocation");
            this._parent = this.GetValueFromConfig("appSettings", "Parent");
            this._strWebServiceName = this.GetValueFromConfig("appSettings", "WebServiceName");

            //CR#$203689            
            this._folder = GetSetting("ReportFolder", "BMCReports"); //"BMCUSReports";
            this._deployfolder = "Reports";
            
            this._strWebServicePath = this.GetValueFromConfig("appSettings", "WebServicepath");
            if (this._strWebServicePath != null && this._strWebServicePath != string.Empty && this._strWebServicePath.ToUpper().ToString() != "[REPORTSERVERNAME]")
            {                
                this._strWebServicePath = this.GetValueFromConfig("appSettings", "protocol").Trim() + this._strWebServicePath.Trim() + this._strWebServiceName.Trim();
            }
            else
            {
                MessageBox.Show(" Please set the value for 'WebServicepath' in config file", "Deploy Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Set security credentials for web service client authorization.
            _rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            _rs.Url = this._strWebServicePath;
            // _rs.Url = "http://localhost/ReportServer/ReportService2005.asmx";
            try
            {
                // Create report folder to drop report in.
                if (this._folder.Length > 0 && this._parent.Length > 0)
                {
                    // Create folder if not exists.
                    if (this.CheckExist(ItemTypeEnum.Folder,
                        this._parent, this._folder) == false)
                    {
                        _rs.CreateFolder(this._folder, this._parent, null);
                    }
                }
                else
                {
                    // Log the error.				
                }
                // Create data source folder to drop data source in.
                if (this._datasourceLocation.Length > 0 && this._datasourceLocation.Length > 0)
                {
                    // Create folder if not exists.
                    if (this.CheckExist(ItemTypeEnum.Folder,
                        this._parent, this._datasourceLocation) == false)
                    {                       
                        _rs.CreateFolder(this._datasourceLocation, this._parent, null);
                    }
                }
                else
                {
                    // Log the error.				
                }
            }
            catch (System.Net.WebException e)
            {
                Console.WriteLine("This program is expected to throw WebException on successful run." +
                                    "\n\nException Message :" + e.Message);
                if (e.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    Console.WriteLine("Status Code : {0}", ((System.Net.HttpWebResponse)e.Response).StatusCode);
                    Console.WriteLine("Status Description : {0}", ((System.Net.HttpWebResponse)e.Response).StatusDescription);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            this.GetReportsToDeploy("ReportDetails");
            this.Deploy();
        }


		#endregion [Constructor]

		#region [Dispose]
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion [Dispose]

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
		
		#region [Deploy]
		/// <summary>
		/// Deploys the report to the reporting server.
		/// </summary>
		private void Deploy()
		
        {  
            foreach (object _report in this.ConnectionStrings)
            {
                // Get the connection string for the report to deploy.
                this._connectString = _report.ToString();

                // Continue deploying report if report details provided.
                string _reportName="";
                try
                {
                    if (this._connectString.Length > 0)
                    {
                        // Set report details.
                        //this.SetReportInfo();
                        this.SetFileInfo();

                        // Open the report (rdl) file and read the data into the stream.
                        byte[] _reportDefinition;
                        Warning[] _warnings;

                        FileStream _stream = File.OpenRead(Application.StartupPath + "\\" + this._deployfolder + "\\" + this._reportPath);

                        _reportDefinition = new Byte[_stream.Length];
                        _stream.Read(_reportDefinition, 0, (int)_stream.Length);
                        _stream.Close();

                       // Create the report into the server.
                        _reportName = this._reportPath.Substring(
                            this._reportPath.LastIndexOf("\\") + 1);

                        //for property --new 
                        //Property retrieveProp = new Property();
                        //retrieveProp.Name = "Description";
                        //Property[] props = new Property[1];
                        //props[0] = retrieveProp;

                        //Property[] properties = null;
                        //try
                        //{
                        //    properties = _rs.GetProperties(_reportName.Remove
                        //                      (_reportName.Length - 4, 4), props);
                        //    foreach (Property prop in properties)
                        //    {
                        //        // Writes the description to the console.
                        //        Console.WriteLine(prop.Value);
                        //    }
                        //}
                        //catch (System.Web.Services.Protocols.SoapException e)
                        //{
                        //    Console.WriteLine(e.Detail.InnerXml.ToString());
                        //}
                        //end for property

                        // Create data source and add their references
                        DataSource dSource = new DataSource();
                        DataSourceDefinition dDefinition = new DataSourceDefinition();
                        dSource.Item = dDefinition;
                        dDefinition.Extension = this._extension;
                        dDefinition.ConnectString = @"Data Source=" + this._serverName
                            + @";Initial Catalog=" + this._dbName + @";Connect Timeout=60";// +";userid=sa;pwd=sa";
                        dDefinition.ImpersonateUserSpecified = true;
                        dDefinition.Prompt = null;
                        dDefinition.WindowsCredentials = false;
                        dDefinition.CredentialRetrieval = CredentialRetrievalEnum.Store;
                        dDefinition.UserName = _userID;
                        dDefinition.Password = _pWD;                 
                        dSource.Name = this._datasource;

                        try
                        {
                            if (this.CheckExist(ItemTypeEnum.DataSource, this._parent,

                                this._datasourceLocation + "/" + this._datasource) == false)
                            {
                                _rs.CreateDataSource(this._datasource, @"/" +
                                this._datasourceLocation, true, dDefinition, null);

                            }                         
                        }
                        catch (System.Web.Services.Protocols.SoapException _exception)
                        {
                            //throw _exception;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        //create report now 
                        try
                        {
                            if (_IsReport)
                            {
                                // warnings = rs.CreateReport(name, "/Samples", false, definition, null);
                                _warnings = (Warning[])_rs.CreateReport(_reportName.Remove
                                                  (_reportName.Length - 4, 4), this._parent + this._folder, true,
                                                  _reportDefinition, null);

                                if (_warnings != null)
                                {
                                    //foreach (Warning warning in _warnings) //suppressing message
                                    //{
                                    //   Console.WriteLine(warning.Message);
                                    //MessageBox.Show(warning.Message);
                                    //}
                                }
                                else
                                    Console.WriteLine("Report: {0} created successfully with no warnings", _reportName);
                            }
                            else
                            {
                                _rs.CreateResource(_reportName, this._parent + this._folder, true,
                                    _reportDefinition, this._MIMEType, null);
                            }
                        }
                        catch (System.Web.Services.Protocols.SoapException e)
                        {
                            Console.WriteLine(e.Detail.InnerXml.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        // Report and Datasource created, now fix up datasource reference to
                        // make sure report points at correct dataset.
                        try
                        {
                            if (_IsReport)
                            {
                                DataSourceReference reference = new DataSourceReference();
                                DataSource ds = new DataSource();
                                reference.Reference = @"/" + this._datasourceLocation + @"/"
                                    + this._datasource;
                                DataSource[] dsarray = _rs.GetItemDataSources(this._parent +
                                    this._folder + "/" + _reportName.Remove
                                    (_reportName.Length - 4, 4));
                                ds = dsarray[0];
                                ds.Item = (DataSourceReference)reference;
                                _rs.SetItemDataSources(this._parent +
                                    this._folder + "/" + _reportName.Remove
                                    (_reportName.Length - 4, 4), dsarray);
                            }
                        }
                        catch (Exception _exception)
                        {
                            // throw (_exception);
                            Console.WriteLine(_exception.Message);
                            //break;
                        }
                        Console.WriteLine("*****************");
                        Console.WriteLine(_reportName + " Deployed Successfully.");
                        Console.WriteLine("*****************");
                    }
                }
                catch
                {
                    Console.WriteLine("*****************");
                    Console.WriteLine(_reportName + " Deployment Not Successful.");
                    Console.WriteLine("*****************");
                }
            }
		}
		#endregion [Deploy]

		#region [GetValueFromConfig]
		/// <summary>
		/// This function reads a setting from the config file.Empty string 
		/// returned if no setting was found. Raise error if config can not be 
		/// read
		/// </summary>
		/// <param name="sectionName">This is the section name in the config 
		/// file</param>
		/// <param name="keyName">This is the name of the key in the specfied 
		/// section in the config file</param>
		/// <returns>string</returns>
		private string GetValueFromConfig(string sectionName, string keyName)
		{
			string _value = string.Empty;
			try
			{
				// Read the configuration settings from the configuration file
				NameValueCollection _section = (NameValueCollection)ConfigurationSettings.GetConfig( sectionName);

				// If there is a key return , else return empty string
				if (_section != null && _section[keyName] != null)
				{
					_value = _section[keyName];
				}
			}
			catch (Exception _exception)
			{
				// Consume the error, so that the service keeps running,
				// but log the error.
				//EventLog.WriteEntry( Assembly.GetExecutingAssembly().FullName, 
				//	_exception.Message, EventLogEntryType.Error);
                Console.WriteLine(_exception.Message);
			}
			return _value;
		}
		#endregion [GetValueFromConfig]

		#region [CheckFolder]
		/// <summary>
		/// Checks if the folder exists or not.
		/// </summary>		
		/// <param name="path">Parent folder path</param>
		/// <param name="folderName">Name of the folder to search</param>
		/// <returns>True if found, else false.</returns>
		private bool CheckExist(ItemTypeEnum type, string path, string folderName)
		{
			string _path = path + folderName;

			// Condition criteria.
            try
            {
                SearchCondition[] conditions;

                // Condition criteria.			
                SearchCondition condition = new SearchCondition();
                condition.Condition = ConditionEnum.Contains;
                condition.ConditionSpecified = true;
                condition.Name = "Name";
                condition.Value = "";
                conditions = new SearchCondition[1];
                conditions[0] = condition;

                this._returnedItems = _rs.FindItems(path, BooleanOperatorEnum.Or,
                    conditions);

                // Iterate thro' each report properties to get the path.
                foreach (CatalogItem item in _returnedItems)
                {
                    if (item.Type == type)
                    {
                        if (item.Path == _path)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
			return false;
		}
		#endregion [CheckFolder]

		#region [SetFileInfo]

        private void SetFileInfo()
        {
            string[] _temp = this._connectString.Split(";".ToCharArray());
            string[] _properties = new string[_temp.Length];

            this._IsReport = true;

            // Get the deployment info for the report.
            for (int _count = 0; _count < _temp.Length; _count++)
            {
                _properties = _temp[_count].Split("=".ToCharArray());

                switch (_properties[0].ToUpper())
                {
                    case "DBNAME":
                        this._dbName = _properties[1];
                        break;
                    case "DATASOURCE":
                        this._datasource = _properties[1];
                        break;
                    case "REPORTPATH":
                        this._reportPath = _properties[1];
                        break;
                    case "MIMETYPE":
                        this._IsReport = false;
                        this._MIMEType = _properties[1];
                        break;
                    //case "REGION":
                    //    this._RegionType = _properties[1];
                    //    break;
                }

            }
        }

		#endregion [SetFileInfo]

		#region [GetReportsToDeploy]
		/// <summary>
		/// Gets all the tags under the section.
		/// </summary>
		private void GetReportsToDeploy(string sectionName)
		{	
			try
			{
				// Read the configuration settings from the configuration file
				NameValueCollection _section = (NameValueCollection)
					ConfigurationSettings.GetConfig( sectionName);
				this.ConnectionStrings.Clear();

				if (_section != null && _section.HasKeys() == true)
				{
					for (int _count = 0; _count < _section.Keys.Count; _count++)
					{
						this.ConnectionStrings.Add( _section[_section.Keys[_count]]);
					}
				}
			}
			catch (Exception _exception)
			{
				throw _exception;
			}			
		}
		#endregion [GetReportsToDeploy]

        #region [DB Settings]
       private static string GetSetting(string sConfigValue, string sDefaultValue)
        {
            try
            {
                var configParam = new SqlParameter[3];
                configParam[0] = new SqlParameter
                {
                    ParameterName = "Setting_Name",
                    Value = sConfigValue,
                    Direction = ParameterDirection.Input
                };


                configParam[1] = new SqlParameter
                {
                    ParameterName = "Setting_Default",
                    Value = sDefaultValue,
                    Direction = ParameterDirection.Input
                };

                configParam[2] = new SqlParameter
                {
                    ParameterName = "Setting_Value",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.VarChar,
                    Size = 500
                };

                SqlHelper.ExecuteScalar(DatabaseHelper.GetConnectionString(), CommandType.StoredProcedure, "rsp_GetSetting", configParam);
                var settingValue = configParam[2].Value.ToString();
                return settingValue;
            }
            catch
            {
                return sDefaultValue;
            }
        }
        #endregion
        #region Unused methods

        ///// <summary>
        ///// Sets report definition such as databasename to connect, datasource 
        ///// of the report and path for the report.
        ///// </summary>
        //private void SetReportInfo()
        //{
        //    string[] _temp = this._connectString.Split( ";".ToCharArray());
        //    string[] _properties = new string[_temp.Length];

        //    for (int _count = 0; _count < _temp.Length; _count++)
        //    {					
        //        _properties[_count] = _temp[_count].Split("=".ToCharArray())[1];
        //    }

        //    // Get the deployment info for the report.
        //    if (_properties.Length == 3)
        //    {
        //        this._dbName = _properties[0];
        //        this._datasource = _properties[1];
        //        this._reportPath = _properties[2];
        //        //this._RegionType = _properties[3];

        //    }
        //}

        //#region Find region from regional settings
        //public static string GetRegion()
        //{
        //    string strRegion = string.Empty;
        //    // set currency format

        //    string curCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();

        //    System.Globalization.RegionInfo RegionFormat = new System.Globalization.RegionInfo(curCulture);

        //    strRegion = RegionFormat.EnglishName;

        //    return strRegion;
        //}
        //#endregion 

        //#region Find region from site table
        //public  string GetRegionDB()
        //{
        //    string strRegion = string.Empty;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection("SERVER=" + this._serverName + ";UID=" + this._userID + ";PWD=" + this._pWD + ";DATABASE=Enterprise;Connection Timeout=60"))
        //        {
        //            DataSet dsGetRegion = BMC.DataAccess.SqlHelper.ExecuteDataset(con, CommandType.StoredProcedure, "rsp_GetSiteDetails");
        //            if (dsGetRegion != null)
        //            {
        //                foreach (DataRow row in dsGetRegion.Tables[0].Rows)
        //                {
        //                    if (row["region"] == DBNull.Value)
        //                    {
        //                        strRegion = GetRegion();
        //                        if (strRegion.ToUpper() == "UNITED STATES")
        //                        {
        //                            strRegion = "US";
        //                        }
        //                        else if (strRegion.ToUpper() == "UNITED KINGDOM")
        //                        {
        //                            strRegion = "UK";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        strRegion = row["region"].ToString();

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception _exception)
        //    {
        //        throw _exception;
        //    }	
        //    return strRegion;

        //}
        //#endregion 
        #endregion
    }
}