using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.Transport.Enum
{
        public enum ShortpayExceptionCodes
        {
            NormalTreasuryEntry = 210,
            HouseKeepingVoid = 211,
            VoidTicketForShortpay = 212,
            VoidTicketForHandpay = 213,
            voidTicketForJackpotHandpay = 214,
            VoidProcessCompletedAlready = 217,
            NoEntryInTicket_Exception = 215,
            NoMatchingEntry = 216
        }

       
}

namespace BMC.Transport
{
    public enum EnrollmentErrorCodes
    {       
        RemoveFromPollingListFailure,
        DatabaseError,
        EnterpriseWebServiceCommunicationFailure,
        EnterpriseDatabaseError,
             
        AddToPollingListFailure,
        PositionLocked,      
        EnterpriseAssetNotAvailable,
        EnterpriseAssetInUse,

        LockExists,
        LockError,

        Success,

        UpdateToOptionFileParameterFailure,

        ExchangeHostServiceNotRunning
    }


    public enum PrintTicketErrorCodes
    {
        Exception,
        Failure,
        OpenCOMPortFailure,
        PrintTicketFailure,
        OutofPaper,
        Success,
        TicketPrintConfirmationFailure,
        TicketCreateRequestFailure,
        SaveTicketIssueDetailsFailure,
        InvalidPrinterName,
        //CouponExpress Printer Errors
        eVoltErr ,
        eHeadErr ,
        ePaperOut ,
        ePlatenUP ,
        eSysErr ,
        eBusy ,

        eJobMemOF ,
        eBufOF,
        eLibLoadErr,
        ePRDataErr,
        eLibRefErr,
        eTempErr,

        eMissingSupplyIndex ,
        ePrinterOffline ,
        eFlashProgErr ,
        ePaperInChute ,
        ePrintLibCorr ,
        eCmdErr ,

        ePaperLow ,
        ePaperJam ,
        eCurrentErr,
        eJournalPrint ,
        eNone1 ,
        eNone2 

        
    }
}
