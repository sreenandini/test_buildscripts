USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDeclaredTicket]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDeclaredTicket]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [rsp_GetDeclaredTicket]
  @Collection_ID    int,    
  @Installation_ID  int  
AS
BEGIN
  SELECT   
  ID = CT_ID,  
  [Value] = CT_Value,  
  [BarCode] = CT_Barcode  
    FROM Collection_Ticket  
   WHERE CT_Inserted_Collection_ID = @Collection_ID  
     AND CT_Inserted_Installation_ID = @Installation_ID
END

GO

