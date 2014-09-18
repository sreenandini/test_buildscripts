using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BMC.Transport.CashDeskOperatorEntity;
using BMC.Transport;
using BMC.Business.CashDeskOperator;
using BMC.Common.ExceptionManagement;
#region BMC.CashDeskOperator.BusinessObjects

public partial class RedeemTicket
{
    public voucherDTO IssueTicketToCage(IssueTicketEntity IssueTicketEntity, voucherDTO ovoucherDTO)
    {
        PrintTicketErrorCodes ErrorCode = PrintTicketErrorCodes.Failure;
        CageVoucher voucher = new CageVoucher();

        try
        {
            if ((ErrorCode = issueTicket.CreateTicketForCage(IssueTicketEntity, ref voucher)) == PrintTicketErrorCodes.Success)
            {
                ovoucherDTO.barcode = IssueTicketEntity.BarCode;
                ovoucherDTO.amountType = amountTypeEnum.CASHABLE;
                ovoucherDTO.errorCodeId = voucher.ErrorCode;
                ovoucherDTO.tktStatus = voucher.tktStatus;
                ovoucherDTO.tktStatusId = voucher.tktStatusID;
                ovoucherDTO.expiryDays = voucher.expiryDays;
                ovoucherDTO.expireDate = voucher.expireDate;
                ovoucherDTO.effectiveDate = voucher.DatePrinted;
                //ovoucherDTO.expireDateSpecified = true;
                //ovoucherDTO.effectiveDateSpecified = true; 

                //SMSResponseInfo.ExpiryDate = (BDateTime)voucherDToObj.expireDate;
                //SMSResponseInfo.ValidDays = voucherDToObj.expiryDays;
                //SMSResponseInfo.TicketStatus = voucherDToObj.tktStatus;
                //SMSResponseInfo.ErrorCode = voucherDToObj.errorCodeId;
                //listResponseInfo.Add(SMSResponseInfo);
                //TenderInfo tempTenderInfo = new TenderInfo();
                //item.Copy(tempTenderInfo);
                //(oCommonUtilities.CreateInstance()).PrintCommonReceipt(voucher);
            }
        }
        catch (Exception ex)
        {
            ExceptionManager.Publish(ex);
        }

        return ovoucherDTO;
    }
    public voucherDTO[] searchVouchers(string partialBarcode, int siteId, long amount, int maxCount)
    {
        return issueTicket.SearchVoucher(partialBarcode, siteId, amount, maxCount);
    }
    public voucherDTO redeemRequestVoucher(voucherDTO request)
    {
        return issueTicket.redeemRequestVoucher(request);
    }
} 
#endregion