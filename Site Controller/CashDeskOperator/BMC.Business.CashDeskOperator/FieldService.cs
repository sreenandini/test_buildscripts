using System;
using System.Data;
using System.Xml;
using BMC.DBInterface.CashDeskOperator;
using BMC.Common.ExceptionManagement;
using BMC.Business.CashDeskOperator.WebServices;
using BMC.Transport;

namespace BMC.Business.CashDeskOperator
{
    public class FieldService
    {
        FieldServiceDataAccess fieldServiceDataAccess = new FieldServiceDataAccess();

        /// <summary>
        /// Returns the datatable with the list of Current Calls.
        /// </summary>
        /// <returns>Datatable</returns>        

        public DataTable GetCurrentServiceCalls(string SiteCode, string StartBarPos, string LastBarPos)
        {
            Proxy WebService = new Proxy(Settings.SiteCode);
            XmlDocument DocServiceCall = new XmlDocument();
            XmlNodeList NodeListServiceCall;
            string OutputXml = string.Empty;
            string DateLogged = string.Empty;
            string Position = string.Empty;
            string AssetNo = string.Empty;
            string Game = string.Empty;
            string Fault = string.Empty;
            string Downtime = string.Empty;
            string CallStatus = string.Empty;
            string Status = string.Empty;
            DataTable dtServiceCalls = new DataTable();

            try
            {
                OutputXml = WebService.GetCurrentServiceCalls(SiteCode, StartBarPos, LastBarPos);
                if (OutputXml != string.Empty)
                {
                    DocServiceCall.LoadXml(OutputXml);
                    NodeListServiceCall = DocServiceCall.DocumentElement.GetElementsByTagName("ServiceCall");

                    dtServiceCalls.Columns.Add("Date Logged", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Pos", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Asset No", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Game", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Fault", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Downtime", System.Type.GetType("System.String"));
                    dtServiceCalls.Columns.Add("Call Status", System.Type.GetType("System.String"));

                    foreach (XmlNode Node in NodeListServiceCall)
                    {
                        DateLogged = Node.ChildNodes[1].InnerXml;
                        Position = Node.ChildNodes[2].InnerXml;
                        AssetNo = Node.ChildNodes[3].InnerXml;
                        //Downtime = "00:00";
                        Downtime = Node.ChildNodes[5].InnerXml;

                        if (Node.ChildNodes[4].InnerXml == string.Empty)
                            Game = "Unknown";
                        else
                            Game = Node.ChildNodes[4].InnerXml;

                        if (Node.ChildNodes[6].InnerXml.Trim() != string.Empty)
                        {
                            switch (Node.ChildNodes[6].InnerXml.Trim())
                            {
                                case "1":
                                    CallStatus = "Logged";
                                    break;
                                case "2":
                                    CallStatus = "Viewed";
                                    break;
                                case "3":
                                    CallStatus = "Despatched";
                                    break;
                                case "4":
                                    CallStatus = "Accepted";
                                    break;
                                case "5":
                                    CallStatus = "Enroute";
                                    break;
                                case "6":
                                    CallStatus = "At Site";
                                    break;
                                case "7":
                                    CallStatus = "Received";
                                    break;
                                case "8":
                                    CallStatus = "Completed";
                                    break;
                                case "9":
                                    CallStatus = "Rejected";
                                    break;
                            }
                        }

                        Status = "S";
                        if (Node.ChildNodes[7].InnerXml.Trim() == "1")
                            Status = "S";

                        if (Node.ChildNodes[8].InnerXml.Trim() == "1")
                            Status = "M";

                        if (Node.ChildNodes[9].InnerXml.Trim() == "1")
                            Status = "A";

                        if (Node.ChildNodes[10].InnerXml.Trim() != string.Empty)
                            Fault = Node.ChildNodes[12].InnerXml.Trim();
                        else
                            Fault = "Unknown";

                        DataRow Row = dtServiceCalls.NewRow();
                        Row.SetField("Date Logged", DateLogged);
                        Row.SetField("Pos", Position);
                        Row.SetField("Asset No", AssetNo);
                        Row.SetField("Game", Game);
                        Row.SetField("Fault", Fault);
                        Row.SetField("Downtime", Downtime);
                        Row.SetField("Call Status", CallStatus);

                        dtServiceCalls.Rows.Add(Row);
                    }
                }

                return dtServiceCalls;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return new DataTable();
            }
        }

        /// <summary>
        /// Returns the site code.
        /// </summary>
        /// <returns>String</returns>
        [Obsolete]
        public string GetCurrentSiteCode()
        {
            return fieldServiceDataAccess.GetCurrentSiteCode();
        }
        /// <summary>
        /// Returns the list of Bar Position Names for the site.
        /// </summary>
        /// <returns>String</returns>
        [Obsolete]
        public string GetCurrentBarPositionNames()
        {
            string BarPosName = string.Empty;
            try
            {
                BarPosName = fieldServiceDataAccess.GetCurrentBarPositionNames();
                string[] BarPosNamesArray = BarPosName.Split(',');
                string LastBarPosName = BarPosNamesArray[BarPosNamesArray.Length - 1];
                int BarPosNo = (fieldServiceDataAccess.GetCurrentBarPosCount() / 10);

                if (BarPosNo > 5)
                {
                    BarPosNo = 5;
                }

                BarPosName = BarPosNo.ToString() + "," + LastBarPosName;
                return BarPosName;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return string.Empty;
            }
        }

        public DataTable GetCashdeskServiceFaults()
        {
            try
            {
                Proxy BGSWSProxy = new Proxy(Settings.SiteCode);
                return BGSWSProxy.GetCashDeskServiceFaults();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return null;
            }
        }

        public string LogSiteEvent(int InstallationNumber, int FaultType)
        {
            Proxy BGSWSProxy = new Proxy(Settings.SiteCode);

            try
            {
                string eventXml = fieldServiceDataAccess.PrepareCashDeskEvent(InstallationNumber, FaultType);

                return BGSWSProxy.LogSiteEvent(AppendUserDetails(eventXml, BMC.Common.clsSecurity.UserID.ToString()));
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return string.Empty;
            }
        }

        private string AppendUserDetails(string eventXml, string userId)
        {
            try
            {
                int idx = eventXml.IndexOf("</Event>");

                if (idx >= 0)
                {
                    string newElement = "<UserId>" + userId + "</UserId>";
                    eventXml = eventXml.Insert(idx, newElement);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return eventXml;
        }

        public DataTable GetOpenServiceCalls(string site_Code, string bar_Pos)
        {
            try
            {
                Proxy bgsProxy = new Proxy(Settings.SiteCode);
                return bgsProxy.GetOpenServiceCalls(site_Code, bar_Pos);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public DataTable GetServiceNotes(string JobID)
        {
            try
            {
                Proxy proxy = new Proxy(Settings.SiteCode);
                return proxy.GetServiceNotes(JobID);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public DataTable GetRemedies()
        {
            try
            {
                Proxy proxy = new Proxy(Settings.SiteCode);
                return proxy.GetRemedies();
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return new DataTable();
            }
        }

        public DataTable GetPositionList()
        {
            return fieldServiceDataAccess.GetPositionList();
        }

        public int EscalateServiceCall(string JobID, int UserID)
        {
            try
            {
                Proxy proxy = new Proxy(Settings.SiteCode);
                return proxy.EscalateService(JobID, UserID);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }

        public int InsertServiceNotes(string JobID, string Notes, string User)
        {
            try
            {
                Proxy proxy = new Proxy(Settings.SiteCode);
                return proxy.InsertServiceNotes(JobID, Notes, User);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }

        public int CloseServiceCall(int ServiceID, string JobID, int Remedy, int User, string Notes)
        {
            try
            {
                Proxy proxy = new Proxy(Settings.SiteCode);
                return proxy.CloseServiceCall(ServiceID, JobID, Remedy, User, Notes);
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return -1;
            }
        }
    }
}
