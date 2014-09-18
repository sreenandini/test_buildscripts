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



namespace BMC.Business.CashDeskOperator
{

    /// <summary>
    /// Returns the bar code number for the ticket number entered.
    /// </summary>
    /// <param name="ObjCDOEntity"></param>
    /// <returns></returns>

    public partial class IssueTicket
    {
        public PrintTicketErrorCodes CreateTicketForCage(IssueTicketEntity CDOEntity, ref CageVoucher voucher)
        {
            PrintTicketErrorCodes ErrorCode = PrintTicketErrorCodes.Failure;
            try
            {
                if ((ErrorCode = GenerateTicketForCage(CDOEntity)) == PrintTicketErrorCodes.Success)
                {
                    if ((ErrorCode = issueTicketDB.SaveTicketIssueDetailsCage(CDOEntity)) == PrintTicketErrorCodes.Success)
                        TicketIssueCreateForCage(CDOEntity, ref voucher);

                    return PrintTicketErrorCodes.Success;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return ErrorCode;
        }
        public voucherDTO[] SearchVoucher(String partialBarcode, int siteId, long amount, int maxCount)
        {
            voucherDTO[] ovoucherDTO = null;
            string LocalTicketorSiteCode;
            string sURL;

            ValidateSiteCode(partialBarcode, out LocalTicketorSiteCode, out sURL);

            if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
            {
                LogManager.WriteLog("IssueTicketCage->SearchVoucher(Invalid Site Code or No rights to access other Site):" + partialBarcode, LogManager.enumLogLevel.Debug);
                return ovoucherDTO;
            }
            else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
            {
                LogManager.WriteLog("IssueTicketCage->Cross ticketing: SearchVoucher Got site URL for:" + partialBarcode, LogManager.enumLogLevel.Debug);

                System.ServiceModel.EndpointAddress objEndpoint = new System.ServiceModel.EndpointAddress(sURL);
                TicketingClient.TicketingServiceReference.TicketingServiceClient objClient = new TicketingClient.TicketingServiceReference.TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);

                //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);
                return objClient.SearchTicketForCage(partialBarcode, Settings.SiteCode);
            }

            return issueTicketDB.SearchTicketForCage(partialBarcode, siteId, amount, maxCount);
        }

        public voucherDTO[] SearchVoucher(String partialBarcode)
        {
            return issueTicketDB.SearchTicketForCage(partialBarcode);
        }

        public voucherDTO[] SearchVoucher(String partialBarcode, string strClientSiteCode)
        {
            return issueTicketDB.SearchTicketForCage(partialBarcode,strClientSiteCode);
        }

        public void ValidateSiteCode(string ticketString, out string LocalTicketorSiteCode, out string sURL)
        {
            LocalTicketorSiteCode = string.Empty;
            sURL = string.Empty;

            string sTicketSiteCode = ticketString.Substring(0, 4);
            LinqDataAccessDataContext objLinq = new LinqDataAccessDataContext(CommonDataAccess.ExchangeConnectionString);

            foreach (var item in objLinq.ValidateSiteCode(sTicketSiteCode))
            {
                LocalTicketorSiteCode = item.SiteCode;
               sURL = item.URL;
             }
        }

        public voucherDTO redeemRequestVoucher(voucherDTO request)
        {
            voucherDTO ovoucherDTO = null;
            string LocalTicketorSiteCode;
            string sURL;
            RTOnlineTicketDetail TicketDetailEntity = new RTOnlineTicketDetail();
            TicketDetailEntity.TicketString = request.barcode;

            ValidateSiteCode(TicketDetailEntity.TicketString, out LocalTicketorSiteCode, out sURL);

            if (sURL.IsNullOrEmpty() || sURL == "INVALID") //Invalid Site Code or No rights to access other Site
            {
                return ovoucherDTO;
            }
            else if (sURL.StartsWith("http")) // WebService Call in case of Different Site Code 
            {
                System.ServiceModel.EndpointAddress objEndpoint = new System.ServiceModel.EndpointAddress(sURL);
                TicketingClient.TicketingServiceReference.TicketingServiceClient objClient = new TicketingClient.TicketingServiceReference.TicketingServiceClient(objEndpoint, LocalTicketorSiteCode);

                //EndpointAddress objEndpoint = new EndpointAddress("http://10.2.108.29/TicketingWCFService/TicketingService.svc"); //sURL
                //TicketingServiceClient objClient = new TicketingServiceClient(objEndpoint, "1001"); //LocalTicketorSiteCode);
                voucherDTO[] ovoucherDTOarr = objClient.SearchTicketForCage(TicketDetailEntity.TicketString,Settings.SiteCode);
                if (ovoucherDTOarr != null)
                {
                    if (ovoucherDTOarr.Length > 0)
                    {
                        ovoucherDTO = ovoucherDTOarr[0];
                    }
                }
                return ovoucherDTO;
            }
            
            return  issueTicketDB.redeemRequestVoucherForCage(request);
        }
        private PrintTicketErrorCodes GenerateTicketForCage(IssueTicketEntity IssueTicketEntity)
        {
            PrintTicketErrorCodes ErrorCode;
            try
            {
                if ((ErrorCode = issueTicketDB.TicketCreateRequestCage(IssueTicketEntity)) == PrintTicketErrorCodes.Success)
                    return issueTicketDB.TicketPrintConfirmed(IssueTicketEntity);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return PrintTicketErrorCodes.Exception;
            }
            return ErrorCode;
        }
        public void TicketIssueCreateForCage(IssueTicketEntity issueTicketEntity, ref CageVoucher voucherEntity)
        {
            try
            {
                if (GetTITOTicketInformationForCage(issueTicketEntity, ref voucherEntity) == true)
                {
                    TicketNumber = issueTicketEntity.FullTicketNumber;
                    Amount = int.Parse(issueTicketEntity.dblValue.ToString());
                    voucherEntity.FullTicketNumber = issueTicketEntity.BarCode;
                    voucherEntity.Value = issueTicketEntity.dblValue;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }
        public bool GetTITOTicketInformationForCage(IssueTicketEntity issueTicketEntity, ref CageVoucher VoucherEntity)
        {
            bool HasGotTITOTicketInformation = false;

            try
            {
                if (issueTicketEntity.BarCode.Length == 18)
                {

                    if (issueTicketDB.GetTicketInfoForCage(issueTicketEntity, ref VoucherEntity) == true)
                        if (issueTicketDB.GetMachineDetailsFromAssetForCage(ref VoucherEntity) == true)
                            HasGotTITOTicketInformation = true;

                    //HasGotTITOTicketInformation = true;
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
        public string CancelTicketCage(string strBarcode)
        {
            return issueTicketDB.CancelTicketCage(strBarcode);
        }
        public bool ValidateUserCage(string UserName, string Password)
        {
            return issueTicketDB.ValidateUserCage(UserName, Password);
        }
    }
}
