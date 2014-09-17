USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExceptionTicketsInternal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExceptionTicketsInternal]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.[usp_UpdateExceptionTicketsInternal]
 @iAmount DECIMAL(20, 2),  
 @Collection_No INT ,  
 @strBarcode VARCHAR(50),  
 @Installation_No INT ,  
 @UseriD INT,  
 @CanRedeemCollectionCompleted BIT = NULL  
AS  
BEGIN  
 DECLARE @retDeclaredTicketID        INT  
   
 DECLARE @collection_date_performed  DATETIME  
 SET @CanRedeemCollectionCompleted = COALESCE(@CanRedeemCollectionCompleted, 0)  
   
 -- UPDATE Inserted tickets in collection  
 -- EXEC dbo.usp_UpdateDeclaredTicketValueFromVariance @strBarcode,  
 --      @iAmount,  
 --      @Collection_No,  
 --      @Installation_No  
   
 -- Preserve the old status for Rollback  
 IF (@CanRedeemCollectionCompleted = 0)  
 BEGIN  
     IF NOT EXISTS(  
            SELECT 1  
            FROM   Collection_ExceptionVoucher EV  
            WHERE  EV.Collection_No = @Collection_No  
                   AND EV.strBarcode = @strBarcode  
        )  
     BEGIN  
         INSERT INTO [dbo].[Collection_ExceptionVoucher]  
           (  
             [Collection_No],  
             [strBarCode],  
             [Installation_No],  
             [strVoucherStatus],  
             [iPayDeviceID],  
             [dtPaid]  
           )  
         SELECT @Collection_No,  
                @strBarcode,  
                @Installation_No,  
                strVoucherStatus,  
                iPayDeviceID,  
                dtPaid  
         FROM   Ticketing..VOUCHER  
         WHERE  strBarcode = @strBarcode  
     END  
 END  
   
 UPDATE VOUCHER  
 SET    strVoucherStatus = 'PD',  
        iPayDeviceID = ISNUll(iPayDeviceID,ErrDeviceID),  
        dtPaid = ISNUll(dtPaid,ErrTime),  
        iRedeemCollectionCompleted = (  
            CASE @CanRedeemCollectionCompleted  
                 WHEN 0 THEN iRedeemCollectionCompleted  
                 ELSE 1  
            END  
        )  
 WHERE  strBarcode = @strBarcode  
   
 SELECT @collection_date_performed = Collection_Date_Of_Collection  
 FROM   COLLECTION  
 WHERE  collection_id = @Collection_No  
   
 EXECUTE [dbo].[usp_InsertDeclaredTicket]   
 @strBarcode  
 ,@iAmount  
 ,@UseriD  
 ,NULL  
 ,NULL  
 ,@Installation_No  
 ,@Collection_No  
 ,@retDeclaredTicketID OUTPUT  
END   

GO

