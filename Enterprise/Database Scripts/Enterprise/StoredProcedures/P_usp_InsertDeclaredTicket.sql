USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertDeclaredTicket]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertDeclaredTicket]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [usp_InsertDeclaredTicket]
 @Barcode VARCHAR(50),  
 @Value MONEY,  
 @User INT,  
 @Printed_Installation_ID INT = NULL,  
 @Printed_Collection_ID INT = NULL,  
 @Inserted_Installation_ID INT = NULL,  
 @Inserted_Collection_ID INT = NULL,  
 @retDeclaredTicketID INT OUTPUT  
AS  
 SET DATEFORMAT DMY   
   
   
 DECLARE @date   DATETIME,  
         @error  INT  
   
 SET @date = GETDATE()    
   
 DECLARE @CanInsert BIT  
 SET @CanInsert = 1  
 SET @retDeclaredTicketID = 0  
   
 IF (  
        @Inserted_Installation_ID IS NOT NULL  
        AND @Inserted_Collection_ID IS NOT NULL  
    )  
 BEGIN  
     IF EXISTS(  
            SELECT 1  
            FROM   [dbo].[collection_Ticket]  
            WHERE  CT_Inserted_Installation_ID = @Inserted_Installation_ID  
                   AND CT_Inserted_Collection_ID = @Inserted_Collection_ID  
                   AND CT_Barcode = @Barcode  
        )  
     BEGIN  
         SET @CanInsert = 0  
     END  
 END  
   
 IF (@CanInsert = 1)  
 BEGIN  
     INSERT INTO Collection_Ticket  
       (  
         CT_Barcode,  
         CT_Value,  
         CT_Declared_Date,  
         CT_Printed_Installation_ID,  
         CT_Printed_Collection_ID,  
         CT_Inserted_Installation_ID,  
         CT_Inserted_Collection_ID,  
         CT_User_ID  
       )  
     VALUES  
       (  
         @Barcode,  
         @Value,  
         @date,  
         @Printed_Installation_ID,  
         @Printed_Collection_ID,  
         @Inserted_Installation_ID,  
         @Inserted_Collection_ID,  
         @User  
       )     
       
     SET @error = @@ERROR    
     IF (@error = 0)  
         SET @retDeclaredTicketID = SCOPE_IDENTITY()--IDENT_CURRENT('Collection_Ticket')   
       
     -- Update the Ticket Declaration Value for MANUAL mode  
     EXEC dbo.usp_UpdateCollectionTicketDeclaration @Inserted_Collection_ID,  
          @Inserted_Installation_ID  
 END  
   
 -- return error (if any)  
 --    
 RETURN @error

GO

