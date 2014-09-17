USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExceptionTickets]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExceptionTickets]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.usp_UpdateExceptionTickets  
 @iAmount DECIMAL(20, 2),  
 @Collection_No INT ,  
 @strBarcode VARCHAR(50),  
 @Installation_No INT ,  
 @UseriD INT  
AS  
BEGIN  
 EXECUTE [dbo].[usp_UpdateExceptionTicketsInternal]   
 @iAmount  
 ,@Collection_No  
 ,@strBarcode  
 ,@Installation_No  
 ,@UseriD  
 ,1  
END

GO

