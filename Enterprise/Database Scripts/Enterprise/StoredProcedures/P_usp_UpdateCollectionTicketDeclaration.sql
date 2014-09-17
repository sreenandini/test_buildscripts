USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateCollectionTicketDeclaration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateCollectionTicketDeclaration]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateCollectionTicketDeclaration  
	(@Site_ID INT,
	 @Collection_No INT, 
	 @Installation_No INT)
AS  
BEGIN  
 SET NOCOUNT ON  
 -- BEGIN  
   
 DECLARE @TDMValue VARCHAR(1000)  
 EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'TicketDeclarationMethod', @TDMValue OUTPUT
 IF @TDMValue IS NULL OR @TDMValue = ''
	SET @TDMValue = 'AUTO'
   
 -- Declared values are calculated here from TicketsIn for TicketDeclarationMethod = AUTO  
 IF (@TDMValue = 'MANUAL')  
 BEGIN  
     DECLARE @DeclaredTicketValue  REAL  
     DECLARE @DeclaredTicketQty    INT  
       
     SELECT @DeclaredTicketValue = COALESCE(SUM(ct_value), 0),  
            @DeclaredTicketQty = COALESCE(COUNT(tCT.CT_ID), 0)  
     FROM   [dbo].[COLLECTION] tC  
            INNER JOIN [dbo].[collection_ticket] tCT  
                 ON  tC.Collection_id = ct_inserted_collection_id  
     WHERE  tC.Collection_id = @Collection_No  
            AND tCT.ct_inserted_installation_id = @Installation_No  
       
     SET @DeclaredTicketValue = COALESCE(@DeclaredTicketValue, 0)  
     SET @DeclaredTicketQty = COALESCE(@DeclaredTicketQty, 0)  
       
     UPDATE CT  
     SET    CT.DeclaredTicketValue = @DeclaredTicketValue,  
            CT.DeclaredTicketQty = @DeclaredTicketQty  
     FROM   [dbo].[COLLECTION] CT  
     WHERE  CT.Collection_id = @Collection_No  
 END   
   
 -- END  
 SET NOCOUNT OFF  
END

GO

