USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_BGS_VoucherInformation_ByNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_BGS_VoucherInformation_ByNumber]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****
Version History
----------------------------------------
Kirubakar S		28 May 2010		Created
----------------------------------------
***/
CREATE PROCEDURE rsp_BGS_VoucherInformation_ByNumber    
    
   @Barcode   varchar(20)    
    
AS    
    
   select PrintDevice = printdevice.strserial,    
    
          PayDevice = paydevice.strserial,    
    
          voucher.strBarCode,    
          voucher.iAmount,    
          strVoucherStatus,    
          voucher.dtPaid,    
          voucher.dtPrinted,    
    
          strDeviceType = paydevice.strDeviceType    
    
/**CASE WHEN paydevice.strDeviceType IS NULL THEN    
                               pltDevice.strDeviceType    
                             ELSE    
                               paydevice.strDeviceType    
                             END    
**/    
    
    
     from voucher (NOLOCK)    
    
left join device as printdevice  (NOLOCK)    
       on printdevice.ideviceid = voucher.ideviceid     
    
left join device as paydevice (NOLOCK)     
       on paydevice.ideviceid = voucher.ipaydeviceid     
    
    WHERE voucher.strbarcode = @barcode 


GO

