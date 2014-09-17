USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsp_DeleteDeclaredTicket]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dsp_DeleteDeclaredTicket]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dsp_DeleteDeclaredTicket        
        
  @Ticket_ID        int = NULL,        
  @Installation_ID  int = NULL,        
  @Collection_ID    int = NULL        
          
AS                
        
 UPDATE       
  Voucher       
 Set       
  iRedeemCollectionCompleted = NULL      
 WHERE       
  strBarcode =(Select CT_Barcode collate database_default from Collection_Ticket where CT_ID=@Ticket_ID)     
    
    
  DELETE         
        
    FROM Collection_Ticket        
        
   WHERE (         
           (         
             @Installation_ID IS NOT NULL         
             AND        
             CT_Inserted_Installation_ID = @Installation_ID        
           )        
           OR         
           ( @Installation_ID IS NULL )        
         )        
        
     AND (         
           (         
             @Collection_ID IS NOT NULL         
             AND        
             CT_Inserted_Collection_ID = @Collection_ID        
           )        
           OR         
           ( @Collection_ID IS NULL )        
         )        
        
     AND (         
           (         
             @Ticket_ID IS NOT NULL         
             AND        
             CT_ID = @Ticket_ID        
           )        
           OR         
           ( @Ticket_ID IS NULL )        
         )        
  
 -- Update the Ticket Declaration Value for MANUAL mode  
 EXEC dbo.usp_UpdateCollectionTicketDeclaration @Collection_ID, @Installation_ID  
   
RETURN @@ERROR

GO

