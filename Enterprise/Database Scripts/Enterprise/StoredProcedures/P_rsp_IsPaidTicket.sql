USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_IsPaidTicket]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_IsPaidTicket]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_IsPaidTicket]  
 @BarCode varchar(40),  
 @Installation_No INT,  
 @Count INT OUTPUT,  
 @Amt INT OUTPUT    
AS    
    
BEGIN    
  
 SELECT @Amt = iAmount, @Count = iVoucherID  
 FROM Voucher V     
 INNER JOIN Device D ON V.iPayDeviceID = D.iDeviceID    
 INNER JOIN Machine M ON D.strSerial = M.Machine_Stock_No COLLATE DATABASE_DEFAULT    
 INNER JOIN installation I ON I.Machine_ID = M.Machine_ID    
 WHERE V.strBarCode = @BarCode    
 AND I.Installation_ID = @Installation_No    
 AND ISNULL(V.iRedeemCollectionCompleted,'0') = '0'    
 AND ( V.strVoucherstatus = 'PD'  
 OR (V.strVoucherstatus = 'PP' AND V.ErrDeviceID IS NOT NULL AND V.ErrCode IS NOT NULL)  
 OR (V.strVoucherstatus = 'PP' AND V.iPayDeviceID IS NOT NULL))    
  
END

GO

