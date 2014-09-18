using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BMC.VaultInterface.Business.Model;
using System.Data.SqlClient;
using BMC.DataAccess;
using BMC.Common.LogManagement;
using System.Data;
using BMC.Common.ConfigurationManagement;
using Microsoft.Win32;
using BMC.Common.ExceptionManagement;
using System.Xml.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Configuration;

namespace BMC.VaultWebService
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://ballytech.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VaultService : System.Web.Services.WebService
    {
        private static string StrConnectString;
        string[] str_STMAllowedEvents = null;
        string str_Manufacturer = "Bally";
        public VaultService()
        {
            try
            {
                str_STMAllowedEvents = ConfigurationManager.AppSettings["STM_AllowedEvents"].ToString().Split(',');
                str_Manufacturer = ConfigurationManager.AppSettings["Vault_Manufacturer"].ToString();//Vault_Manufacturer
            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }
        }

        [WebMethod]
        public Response ProcessEvents(EventModel request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: ProcessEvents -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();

                XmlSerializer s = new XmlSerializer(typeof(EventModel));
                s.Serialize(outStream, request);
                string xml = outStream.ToString();
                LogManager.WriteLog("VaultService: ProcessEvents Xml" + xml.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();
                using (SqlConnection oConn = new SqlConnection(GetConnectionString()))
                {

                    try
                    {
                        oConn.Open();

                        if (request.DeviceID.IsNullOrEmpty())
                        {
                            oResponse.ErrCode = 1;
                            oResponse.ErrDesc = "Invalid DeviceID";
                        }
                        if (request.SiteID.ToString().Length < 4)
                        {
                            oResponse.ErrCode = 2;
                            oResponse.ErrDesc = "Invalid SiteID(Must be a integer with 4 digits)";
                        }
                        if (request.EventID < 1)
                        {
                            oResponse.ErrCode = 3;
                            oResponse.ErrDesc = "Invalid EventID";
                        }
                        if (request.EventDescription.IsNullOrEmpty())
                        {
                            oResponse.ErrCode = 4;
                            oResponse.ErrDesc = "Invalid EventDescription";
                        }


                        foreach (CassetteDetails cd in request.CassetteDetail)
                        {

                            if (cd.CassetteNumber.IsNullOrEmpty())
                            {
                                oResponse.ErrCode = 6;
                                oResponse.ErrDesc = "Invalid CassetteNumber";
                                break;
                            }

                            if (cd.CassetteLevel < 0)
                            {
                                oResponse.ErrCode = 7;
                                oResponse.ErrDesc = "Invalid CassetteLevel";
                                break;
                            }
                            if (cd.CassetteDenom < 1)
                            {
                                oResponse.ErrCode = 8;
                                oResponse.ErrDesc = "Invalid CassetteDenom";
                                break;
                            }

                            if (decimal.Parse( cd.CassetteLevel.ToString()) %decimal.Parse( cd.CassetteDenom.ToString()) > 0 )
                            {
                                oResponse.ErrCode = 8;
                                oResponse.ErrDesc = "Amount did not match CassetteDenom";
                                break;
                            }

                            if (cd.CassetteDescription.IsNullOrEmpty())
                            {
                                oResponse.ErrCode = 9;
                                oResponse.ErrDesc = "Invalid CassetteDescription";
                                break;
                            }
                        }


                        if (oResponse.ErrCode != 0)
                        {
                            LogManager.WriteLog("ProcessEvents Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                            return oResponse;
                        }

                        //var sqlParameters = new SqlParameter[7];
                        //sqlParameters[0] = new SqlParameter("@DeviceID", request.DeviceID);
                        //sqlParameters[1] = new SqlParameter("@SiteID", request.SiteID);
                        //sqlParameters[2] = new SqlParameter("@EventID", request.EventID);
                        //sqlParameters[3] = new SqlParameter("@EventDescription", request.EventDescription);
                        //sqlParameters[4] = new SqlParameter("@EventDateTime", request.EventDateTime);
                        //sqlParameters[5] = new SqlParameter("@VaultEventID", SqlDbType.Int);
                        //sqlParameters[5].Direction = ParameterDirection.Output;
                        //sqlParameters[6] = new SqlParameter("@xml", xml);

                        //DataSet dtVaultEvent = SqlHelper.ExecuteDataset(oConn, CommandType.StoredProcedure, "usp_UpdateEventsInfo", sqlParameters);
                        var sqlParameters = new SqlParameter[4];
                        sqlParameters[0] = new SqlParameter("@EventId", request.EventID);
                        sqlParameters[1] = new SqlParameter("@EventDescription", request.EventDescription);
                        sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                        sqlParameters[3] = new SqlParameter("@Device", request.DeviceID);
                        int retval = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                        if (retval == 1)
                        {
                            LogManager.WriteLog("Event Updated Successfully eventID:" + request.EventID, LogManager.enumLogLevel.Info);
                        }
                        else
                        {
                            LogManager.WriteLog("Event Update failed for  eventID:" + request.EventID, LogManager.enumLogLevel.Info);
                            if (retval == -701)
                            {
                                oResponse.ErrCode = 701;
                                oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                            }
                            else if (retval == -702)
                            {
                                oResponse.ErrCode = 702;
                                oResponse.ErrDesc = "Feature not enabled";

                            }
                            else
                            {
                                oResponse.ErrCode = -100;
                                oResponse.ErrDesc = "Unable to process request";
                            }
                        }



                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                    }
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in reading Event Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";

            }
            LogManager.WriteLog("ProcessEvents Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }

        [WebMethod]
        public Response ProcessHandpays(HandpayModel request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: ProcessHandpays -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(HandpayModel));

                s.Serialize(outStream, request);
                LogManager.WriteLog("VaultService: Handpays Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();


                if (request.Asset.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid Asset";
                }
                if (request.RedeemedAsset.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Redeemed Asset";
                }
                if (request.SiteID.ToString().Length < 4)
                {
                    oResponse.ErrCode = 3;
                    oResponse.ErrDesc = "Invalid SiteID(Must be a integer with 4 digits)";
                }
                if (request.Amount < 1)
                {

                    oResponse.ErrCode = 4;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }

                if (request.Type.GetType() != typeof(HandpayTypeModel))
                {
                    oResponse.ErrCode = 7;
                    oResponse.ErrDesc = "Invalid Type";
                }

                //case -5:
                //    oResponse.ErrCode = 5;
                //    oResponse.ErrDesc = "Invalid RedeemDateTime";
                //    break;
                //case -6:
                //    oResponse.ErrCode = 6;
                //    oResponse.ErrDesc = "Invalid GeneratedDateTime";
                //    break;

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("ProcessHandpays Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;

                }

                var sqlParameters = new SqlParameter[4];

                sqlParameters[0] = new SqlParameter("@EventId", (int)request.Type);
                sqlParameters[1] = new SqlParameter("@EventDescription", request.Type.ToString().ToUpper());
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", request.RedeemedAsset);
                int retval = int.Parse( SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());
                //var sqlParameters = new SqlParameter[7];

                //sqlParameters[0] = new SqlParameter("@Asset", request.Asset);
                //sqlParameters[1] = new SqlParameter("@RedeemedAsset", request.RedeemedAsset);
                //sqlParameters[2] = new SqlParameter("@SiteID", request.SiteID);
                //sqlParameters[3] = new SqlParameter("@Amount", request.Amount);
                //sqlParameters[4] = new SqlParameter("@RedeemDateTime", request.RedeemDateTime);
                //sqlParameters[5] = new SqlParameter("@GeneratedDateTime", request.GeneratedDateTime);
                //sqlParameters[6] = new SqlParameter("@Type", request.Type);

                //int iStatus = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateHandpayInfo", sqlParameters).ToString());
                //if (iStatus == -1)
                //{
                //    oResponse.ErrCode = 1;
                //    oResponse.ErrDesc = "Invalid Asset";
                //}
                //if (iStatus == 0)
                //{
                //    LogManager.WriteLog("Handpay Updated Successfully Asset:" + request.Asset + " Amount: " + request.Amount, LogManager.enumLogLevel.Info);
                //}
                if (retval == 1)
                {
                    LogManager.WriteLog("Handpay Updated Successfully Asset:" + request.Asset + " Amount: " + request.Amount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Handpay Updated Failed Asset:" + request.Asset + " Amount: " + request.Amount, LogManager.enumLogLevel.Info);
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in reading Handpay Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }
            LogManager.WriteLog("ProcessHandpays Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }


        [WebMethod]
        public Response ProcessVouchers(VoucherModel request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: ProcessVouchers -- Entered", LogManager.enumLogLevel.Info);

            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(VoucherModel));
                s.Serialize(outStream, request);
                LogManager.WriteLog("VaultService: Vouchers Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);

                outStream.Close();

                if (request.BarCode.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid BarCode";
                }

                if (request.Amount < 1)
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }
                if (request.SiteID.ToString().Length < 4)
                {
                    oResponse.ErrCode = 3;
                    oResponse.ErrDesc = "Invalid SiteID(Must be a integer with 4 digits)";
                }
                //case -4:
                //    oResponse.ErrCode = 4;
                //    oResponse.ErrDesc = "Invalid RedeemedDateTime";
                //    break;
                if (request.RedeemedAsset.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 5;
                    oResponse.ErrDesc = "Invalid Redeemed Asset";
                }
                if (request.PrintedAsset.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 6;
                    oResponse.ErrDesc = "Invalid Printed Asset";
                }
                //case -7:
                //    oResponse.ErrCode = 7;
                //    oResponse.ErrDesc = "Invalid PrintedDateTime";
                //    break;
                //case -8:
                //    oResponse.ErrCode = 8;
                //    oResponse.ErrDesc = "Invalid ExpiryDate";
                //    break;
                if (request.Type.GetType() != typeof(VoucherTypeModel))
                {
                    oResponse.ErrCode = 9;
                    oResponse.ErrDesc = "Invalid Type";

                }
                //case -10:
                //    oResponse.ErrCode = 10;
                //    oResponse.ErrDesc = "Duplicate Entry";
                //    break;

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("ProcessVouchers Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;
                }

                //var sqlParameters = new SqlParameter[9];

                //sqlParameters[0] = new SqlParameter("@BarCode", request.BarCode);
                //sqlParameters[1] = new SqlParameter("@Amount", request.Amount);
                //sqlParameters[2] = new SqlParameter("@SiteID", request.SiteID);
                //sqlParameters[3] = new SqlParameter("@PaidDateTime", request.RedeemedDateTime);
                //sqlParameters[4] = new SqlParameter("@RedeemedAsset", request.RedeemedAsset);
                //sqlParameters[5] = new SqlParameter("@PrintedAsset", request.PrintedAsset);
                //sqlParameters[6] = new SqlParameter("@PrintedDateTime", request.PrintedDateTime);
                //sqlParameters[7] = new SqlParameter("@ExpiryDate", request.ExpiryDate);
                //sqlParameters[8] = new SqlParameter("@Ticket_Type", request.Type);

                var sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@EventId", (int)request.Type);
                sqlParameters[1] = new SqlParameter("@EventDescription", request.Type.ToString().ToUpper());
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", request.RedeemedAsset);
                int retval =  int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                //if (SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_UpdateVoucherInfo", sqlParameters) < 0)
                if (retval == 1)
                {
                    LogManager.WriteLog("Voucher Updated Successfully BarCode:" + request.BarCode + " Amount: " + request.Amount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    LogManager.WriteLog("Unable to update Voucher:" + request.BarCode + " Amount: " + request.Amount, LogManager.enumLogLevel.Error);
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }
                }
                LogManager.WriteLog("ProcessVouchers Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in reading Voucher Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }

            return oResponse; ;
        }


        private string ProcessEventsXml(DataTable dtEvents)
        {

            XElement xml_stack = null;
            try
            {
                foreach (DataRow dr in dtEvents.Rows)
                {
                    xml_stack =
                                   new XElement("BMCRequest",
                                   new XElement("Source", str_Manufacturer),
                                   new XElement("BMCVersion", dr["BMCVersion"]),
                                   new XElement("ExceptionCode", dr["ExceptionCode"]),
                                   new XElement("OperatorId", dr["OperatorId"]),
                                   new XElement("SubCode", dr["SubCode"]),
                                   new XElement("Company", dr["Company"] ?? string.Empty),
                                   new XElement("Region", dr["Region"] ?? string.Empty),
                                   new XElement("Area", dr["Area"] ?? string.Empty),
                                   new XElement("SiteId", dr["SiteId"].ToString()),
                                   new XElement("SiteName", dr["SiteName"] ?? string.Empty),
                                   new XElement("Asset", dr["Asset"] ?? string.Empty),
                                   new XElement("Stand", dr["Stand"] ?? string.Empty),
                                   new XElement("Cassettes", dr["CassetteDetail"].ToString()),
                                   new XElement("MessageDateTime", string.Format("{0:G}", Convert.ToDateTime(dr["MessageDateTime"]))));
                    break;
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ProcessEventsXml :" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

            }
            return xml_stack != null ? xml_stack.ToString() : null;

        }

        private string UpdateEventXml(string strEventsXml, string CassetteDetails)
        {
            XmlDocument Xdoc = new XmlDocument();
            try
            {

                Xdoc.LoadXml(strEventsXml);
                XmlNode XCasset = Xdoc.SelectSingleNode("//Cassettes");
                if (XCasset != null)
                {
                    XCasset.InnerText = CassetteDetails;
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UpdateEventXml :" + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);

            }
            return Xdoc.InnerXml;
        }

        [WebMethod]
        public Response ProcessMessages(string XMLRequest)
        {

            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: ProcessEvents -- Entered", LogManager.enumLogLevel.Info);
            try
            {

                LogManager.WriteLog("VaultService: ProcessEvents Xml" + XMLRequest, LogManager.enumLogLevel.Info);

                using (SqlConnection oConn = new SqlConnection(GetConnectionString()))
                {

                    try
                    {
                        oConn.Open();

                        //if (request.DeviceID.IsNullOrEmpty())
                        //{
                        //    oResponse.ErrCode = 1;
                        //    oResponse.ErrDesc = "Invalid DeviceID";
                        //}
                        //if (request.SiteID.ToString().Length < 4)
                        //{
                        //    oResponse.ErrCode = 2;
                        //    oResponse.ErrDesc = "Invalid SiteID(Must be a integer with 4 digits)";
                        //}
                        //if (request.EventID < 1)
                        //{
                        //    oResponse.ErrCode = 3;
                        //    oResponse.ErrDesc = "Invalid EventID";
                        //}
                        //if (request.EventDescription.IsNullOrEmpty())
                        //{
                        //    oResponse.ErrCode = 4;
                        //    oResponse.ErrDesc = "Invalid EventDescription";
                        //}

                        if (oResponse.ErrCode != 0)
                        {
                            LogManager.WriteLog("ProcessMessages Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                            return oResponse;
                        }

                        var sqlParameters = new SqlParameter[1];

                        sqlParameters[0] = new SqlParameter("@doc", XMLRequest);

                        int retval = SqlHelper.ExecuteNonQuery(oConn, CommandType.StoredProcedure, "rsp_Vault_UpdateMessagesFromXML", sqlParameters);

                        if (retval == 1)
                        {
                            LogManager.WriteLog("Messages Updated Successfully ", LogManager.enumLogLevel.Info);
                           
                        }
                        else
                        {
                            LogManager.WriteLog("Messages Update Failed", LogManager.enumLogLevel.Info);
                            oResponse.ErrCode = -100;
                            oResponse.ErrDesc = "Unable to update vault messages";
                        }



                    }
                    catch (Exception Ex)
                    {
                        ExceptionManager.Publish(Ex);
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to update vault messages";
                    }
                }


            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in reading Vault Messages Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";

            }
           
            return oResponse;
        }

        [WebMethod]
        public Response Fill(FillRequest Request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: FillVault -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(FillRequest));

                s.Serialize(outStream, Request);
                LogManager.WriteLog("VaultService: FillVault Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();

                if (Request.EventID == 0)
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid EventID ";
                }
                //else if (Request.DeviceID == 0)
                //{
                //    oResponse.ErrCode = 1;
                //    oResponse.ErrDesc = "Invalid DeviceID";
                //}
                else if (Request.DeviceName.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Device Name";
                }
                else if (Request.TotalTransactionAmount < 0)
                {
                    oResponse.ErrCode = 4;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }

                else if (Request.CassetteDetails == null || Request.CassetteDetails.Count == 0)
                {
                    oResponse.ErrCode = 5;
                    oResponse.ErrDesc = "cassette details not found";
                }



                foreach (Cassette cte in Request.CassetteDetails)
                {
                    if (cte.CassetteName.IsNullOrEmpty())
                    {
                        oResponse.ErrCode = 6;
                        oResponse.ErrDesc = "Cassette name invalid.";
                        break;
                    }
                    if (cte.Denom == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "Cassette denom invalid.";
                        break;
                    }

                    if (cte.Amount == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "cassette amount invalid.";
                        break;
                    }

                }

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("Fill Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;

                }

                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@EventId",Request.EventID);
                sqlParameters[1] = new SqlParameter("@EventDescription", "FILLWEBREQUEST");
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", Request.DeviceSerialNumber);
                int retval =  int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                if (retval > 0)
                {
                    LogManager.WriteLog("FillVault Updated Successfully Device:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }

                    
                    LogManager.WriteLog("FillVault Updated Failed Asset:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in updating  FillVault Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }
            LogManager.WriteLog("FillVault Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }
        [WebMethod]
        public Response Bleed(BleedRequest Request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: Bleed -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(BleedRequest));

                s.Serialize(outStream, Request);
                LogManager.WriteLog("VaultService: Bleed Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();

                if (Request.EventID == 0)
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid EventID ";
                }
                //else if (Request.DeviceID == 0)
                //{
                //    oResponse.ErrCode = 1;
                //    oResponse.ErrDesc = "Invalid DeviceID";
                //}
                else if (Request.DeviceName.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Device Name";
                }
                else if (Request.TotalTransactionAmount < 0)
                {
                    oResponse.ErrCode = 4;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }

                else if (Request.CassetteDetails == null || Request.CassetteDetails.Count == 0)
                {
                    oResponse.ErrCode = 5;
                    oResponse.ErrDesc = "cassette details not found";
                }



                foreach (Cassette cte in Request.CassetteDetails)
                {
                    if (cte.CassetteName.IsNullOrEmpty())
                    {
                        oResponse.ErrCode = 6;
                        oResponse.ErrDesc = "Cassette name invalid.";
                        break;
                    }
                    if (cte.Denom == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "Cassette denom invalid.";
                        break;
                    }

                    if (cte.Amount == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "cassette amount invalid.";
                        break;
                    }

                }

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("Bleed Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;

                }

                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@EventId", Request.EventID);
                sqlParameters[1] = new SqlParameter("@EventDescription", "BLEEDWEBREQUEST");
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", Request.DeviceSerialNumber);
                int retval = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                if (retval > 0)
                {
                    LogManager.WriteLog("Bleed Updated Successfully Device:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }
                    LogManager.WriteLog("Bleed Updated Failed Asset:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in updating  Bleed Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }
            LogManager.WriteLog("Bleed Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }

        [WebMethod]
        public Response Adjustment(AdjustmentRequest Request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: Adjustment -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(AdjustmentRequest));

                s.Serialize(outStream, Request);
                LogManager.WriteLog("VaultService: Adjustment Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();


                if (Request.EventID == 0)
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid EventID ";
                }
                //else if (Request.DeviceID == 0)
                //{
                //    oResponse.ErrCode = 1;
                //    oResponse.ErrDesc = "Invalid DeviceID";
                //}
                else if (Request.DeviceName.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Device Name";
                }
                else if (Request.TotalTransactionAmount < 0)
                {
                    oResponse.ErrCode = 4;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }

                else if (Request.CassetteDetails == null || Request.CassetteDetails.Count == 0)
                {
                    oResponse.ErrCode = 5;
                    oResponse.ErrDesc = "cassette details not found";
                }



                foreach (Cassette cte in Request.CassetteDetails)
                {
                    if (cte.CassetteName.IsNullOrEmpty())
                    {
                        oResponse.ErrCode = 6;
                        oResponse.ErrDesc = "Cassette name invalid.";
                        break;
                    }
                    if (cte.Denom == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "Cassette denom invalid.";
                        break;
                    }

                    //if (cte.Amount == 0)
                    //{
                    //    oResponse.ErrCode = 5;
                    //    oResponse.ErrDesc = "cassette amount invalid.";
                    //    break;
                    //}

                }

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("Adjustment Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;

                }

                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@EventId", Request.EventID);
                sqlParameters[1] = new SqlParameter("@EventDescription", "ADJUSTMENTWEBREQUEST");
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", Request.DeviceSerialNumber);
                int retval = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                if (retval > 0)
                {
                    LogManager.WriteLog("Adjustment Updated Successfully Device:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }
                    LogManager.WriteLog("Adjustment Updated Failed Asset:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in updating  Adjustment Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }
            LogManager.WriteLog("Adjustment Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }
        [WebMethod]
        public Response Drop(DropRequest Request)
        {
            Response oResponse = new Response();
            LogManager.WriteLog("VaultService: Drop -- Entered", LogManager.enumLogLevel.Info);
            try
            {
                StringWriter outStream = new StringWriter();
                XmlSerializer s = new XmlSerializer(typeof(DropRequest));

                s.Serialize(outStream, Request);
                LogManager.WriteLog("VaultService: Drop Xml" + outStream.ToString(), LogManager.enumLogLevel.Info);
                outStream.Close();

                if (Request.EventID == 0)
                {
                    oResponse.ErrCode = 1;
                    oResponse.ErrDesc = "Invalid EventID ";
                }
                //else if (Request.DeviceID == 0)
                //{
                //    oResponse.ErrCode = 1;
                //    oResponse.ErrDesc = "Invalid DeviceID";
                //}
                else if (Request.DeviceName.IsNullOrEmpty())
                {
                    oResponse.ErrCode = 2;
                    oResponse.ErrDesc = "Invalid Device Name";
                }
                else if (Request.TotalTransactionAmount < 0)
                {
                    oResponse.ErrCode = 4;
                    oResponse.ErrDesc = "Invalid Amount(Must be greater than 0)";
                }

                else if (Request.CassetteDetails == null || Request.CassetteDetails.Count == 0)
                {
                    oResponse.ErrCode = 5;
                    oResponse.ErrDesc = "cassette details not found";
                }

              

                foreach (Cassette cte in Request.CassetteDetails)
                {
                    if (cte.CassetteName.IsNullOrEmpty())
                    {
                        oResponse.ErrCode = 6;
                        oResponse.ErrDesc = "Cassette name invalid.";
                        break;
                    }
                    if (cte.Denom == 0)
                    {
                        oResponse.ErrCode = 5;
                        oResponse.ErrDesc = "Cassette denom invalid.";
                        break;
                    }

                    //if (cte.Amount == 0)
                    //{
                    //    oResponse.ErrCode = 5;
                    //    oResponse.ErrDesc = "cassette amount invalid.";
                    //    break;
                    //}

                }

                if (oResponse.ErrCode != 0)
                {
                    LogManager.WriteLog("Drop Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
                    return oResponse;

                }

                SqlParameter[] sqlParameters = new SqlParameter[4];
                sqlParameters[0] = new SqlParameter("@EventId", Request.EventID);
                sqlParameters[1] = new SqlParameter("@EventDescription", "DROPWEBREQUEST");
                sqlParameters[2] = new SqlParameter("@XML", outStream.ToString());
                sqlParameters[3] = new SqlParameter("@Device", Request.DeviceSerialNumber);
                int retval = int.Parse(SqlHelper.ExecuteScalar(GetConnectionString(), CommandType.StoredProcedure, "usp_Vault_UpdateGenericMessage", sqlParameters).ToString());

                if (retval > 0)
                {
                    LogManager.WriteLog("Drop Updated Successfully Device:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }
                else
                {
                    if (retval == -701)
                    {
                        oResponse.ErrCode = 701;
                        oResponse.ErrDesc = "Vault not enrolled/invalid serial number";

                    }
                    else if (retval == -702)
                    {
                        oResponse.ErrCode = 702;
                        oResponse.ErrDesc = "Feature not enabled";

                    }
                    else
                    {
                        oResponse.ErrCode = -100;
                        oResponse.ErrDesc = "Unable to process request";
                    }
                    LogManager.WriteLog("Drop Updated Failed Asset:" + Request.DeviceName + " Amount: " + Request.TotalTransactionAmount, LogManager.enumLogLevel.Info);
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteLog("Error in updating  Drop Info", LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                oResponse.ErrCode = -100;
                oResponse.ErrDesc = "Error Occured";
            }
            LogManager.WriteLog("Drop Response: " + oResponse.ToString(), LogManager.enumLogLevel.Debug);
            return oResponse;
        }

        #region DB
        private static string GetConnectionString()
        {
            return BMC.Common.Utilities.DatabaseHelper.GetExchangeConnectionString();
        }

        public bool ExportToSTM(string Type, string Site_Code, string XmlMsg)
        {
            LogManager.WriteLog("ExportToSTM Xml-->" + XmlMsg, LogManager.enumLogLevel.Info);
            try
            {

                SqlParameter[] sqlparams = new SqlParameter[4];
                sqlparams[0] = new SqlParameter("Type", Type);
                sqlparams[1] = new SqlParameter("ClientID", 1);
                sqlparams[2] = new SqlParameter("Site_Code", Site_Code);
                sqlparams[3] = new SqlParameter("XmlMessage", XmlMsg);
                int result = SqlHelper.ExecuteNonQuery(GetConnectionString(), "usp_STM_Export_History", sqlparams);

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportToSTM Failed Type:" + Type + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }
        public bool ExportToSTM(string Type, string Site_Code, string XmlMsg, SqlTransaction oTran)
        {
            LogManager.WriteLog("ExportToSTM Xml-->" + XmlMsg, LogManager.enumLogLevel.Info);
            try
            {

                SqlParameter[] sqlparams = new SqlParameter[4];
                sqlparams[0] = new SqlParameter("Type", Type);
                sqlparams[1] = new SqlParameter("ClientID", 1);
                sqlparams[2] = new SqlParameter("Site_Code", Site_Code);
                sqlparams[3] = new SqlParameter("XmlMessage", XmlMsg);
                int result = SqlHelper.ExecuteNonQuery(oTran, "usp_STM_Export_History", sqlparams);

                return result > 0;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("ExportToSTM Failed Type:" + Type + ex.Message, LogManager.enumLogLevel.Error);
                ExceptionManager.Publish(ex);
                return false;
            }
        }

        #endregion
    }
    #region ValidationMethods
    public static class Validations
    {

        public static bool IsNullOrEmpty(this string str)
        {
            try
            {
                if (str == null || str.Trim() == string.Empty)
                {
                    return true;
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
                return true;
            }
            return false;
        }

    }
    #endregion


    public class TransactionRequest
    {


        [XmlElement("EventID")]
        public int EventID;

        [XmlElement("DeviceID")]
        public int DeviceID;

        [XmlElement("DeviceSerialNumber")]
        public string  DeviceSerialNumber;

        [XmlElement("DeviceManufacturer")]
        public string DeviceManufacturer;

        [XmlElement("DeviceName")]
        public string DeviceName;

        [XmlElement("TotalTransactionAmount")]
        public int TotalTransactionAmount;

        [XmlElement("TransactionDescription")]
        public string TransactionDescription;
        //Type;

        [XmlElement("CassetteDetails", Type = typeof(Cassette))]
        public List<Cassette> CassetteDetails { get; set; }

    }

    [Serializable]
    [XmlRoot("Cassette")]
    public class Cassette
    {
        [XmlElement("CassetteID")]
        public int CassetteID;

        [XmlElement("CassetteName")]
        public string CassetteName;

        [XmlElement("Denom")]
        public int Denom;

        [XmlElement("Amount")]
        public int Amount;

        //[XmlElement("CurrentBalance")]
        //public int CurrentBalance;
    }

    [Serializable]
    [XmlRoot("FillType")]
    public enum FillType
    {
        FILL=1,
        STANDARDFILL
    }

    [Serializable]
    [XmlRoot("BleedType")]
    public enum BleedType
    {
       BLEED=1
    }

    [Serializable]
    [XmlRoot("AdjustmentType")]
    public enum AdjustmentType
    {
        POSITIVE=1,
        NEGATIVE
    }

    [Serializable]
    [XmlRoot("DropType")]
    public enum DropType
    {
        STANDARD=1,
        FINAL
    }

    [Serializable]
    [XmlRoot("FillRequest")]
    public class FillRequest : TransactionRequest
    {
       [XmlElement("Type", Type = typeof(FillType))]
        public FillType Type;
    }

    [Serializable]
    [XmlRoot("BleedRequest")]
    public class BleedRequest : TransactionRequest
    {
        [XmlElement("Type", Type = typeof(BleedType))]
        public BleedType Type;
    }

    [Serializable]
    [XmlRoot("AdjustmentRequest")]
    public class AdjustmentRequest : TransactionRequest
    {
        [XmlElement("Type", Type = typeof(AdjustmentType))]
        public AdjustmentType Type;
    }

    [Serializable]
    [XmlRoot("DropRequest")]
    public class DropRequest : TransactionRequest
    {
        [XmlElement("Type", Type = typeof(DropType))]
        public DropType Type;
    }


}
