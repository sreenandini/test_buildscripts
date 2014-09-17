USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertExport_Data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertExport_Data]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.usp_InsertExport_Data
@Source_ID INT, 
@EH_Type VARCHAR(100), 
@Current_Value VARCHAR(300), 
@Prev_Value VARCHAR(300)
AS
BEGIN

	INSERT INTO  Export_Data(Source_ID,EH_Type,Current_Value,Prev_Value,dtCreatedTime)
	VALUES (@Source_ID,@EH_Type,@Current_Value,@Prev_Value,GETDATE() )
	
END


GO

