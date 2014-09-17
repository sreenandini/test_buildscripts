USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_DiscardExceptionVoucherChanges]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_DiscardExceptionVoucherChanges]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_DiscardExceptionVoucherChanges]
	(@Site_ID INT,
	 @Collection_No INT, 
	 @Installation_No INT, 
	 @CommitChanges BIT)  
AS  
BEGIN  
 SET NOCOUNT ON  
 -- BEGIN  
   
 DECLARE @TDMValue VARCHAR(1000)  

 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'TicketDeclarationMethod', @TDMValue OUTPUT
 IF @TDMValue IS NULL OR @TDMValue = ''
	SET @TDMValue = 'AUTO'
   
 -- Declared values are calculated here from TicketsIn for TicketDeclarationMethod = AUTO  
 IF (@TDMValue = 'MANUAL' AND @CommitChanges = 1)
 BEGIN  
     -- Discard the Collection_Tickets  
     DELETE CT  
     FROM   dbo.[Collection_Ticket] CT  
            INNER JOIN [dbo].[Collection_ExceptionVoucher] CEV  
                 ON  CT.CT_Inserted_Installation_ID = CEV.Installation_No  
                 AND CT.CT_Inserted_Collection_ID = CEV.Collection_No  
       
     -- Revert the Voucher details to old state  
     UPDATE VC  
     SET    VC.strVoucherStatus = CEV.strVoucherStatus,  
            VC.iPayDeviceID = CEV.iPayDeviceID,  
            VC.dtPaid = CEV.dtPaid  
     FROM   [dbo].[Voucher] VC  
            INNER JOIN [dbo].[Collection_ExceptionVoucher] CEV  
                 ON  VC.strBarcode = CEV.strBarcode  
     WHERE  CEV.Collection_No = @Collection_No  
            AND CEV.Installation_No = @Installation_No  
       
     -- Update the Ticket Declaration Value for MANUAL mode  
     EXEC [dbo].[usp_UpdateCollectionTicketDeclaration] @Collection_No,  
          @Installation_No  
 END  
       
    -- Discard the collected details  
    DELETE   
    FROM   [dbo].[Collection_ExceptionVoucher]  
    WHERE  Collection_No = @Collection_No  
           AND Installation_No = @Installation_No  
   
 -- END  
 SET NOCOUNT OFF  
END

GO

