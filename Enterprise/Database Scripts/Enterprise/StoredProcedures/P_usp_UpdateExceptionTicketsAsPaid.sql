USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateExceptionTicketsAsPaid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateExceptionTicketsAsPaid]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC dbo.[usp_UpdateExceptionTicketsAsPaid]
 @iAmount DECIMAL(20, 2),
 @Collection_No INT,
 @strBarcode VARCHAR(50),
 @Installation_No INT,
 @UseriD INT
AS
BEGIN
 EXECUTE [usp_UpdateExceptionTicketsInternal]
 @iAmount
 ,@Collection_No
 ,@strBarcode
 ,@Installation_No
 ,@UseriD
 ,0  
END

GO

