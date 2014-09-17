USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CRMExportRoute]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CRMExportRoute]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].usp_CRMExportRoute
@SiteID int 
AS
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Insert an entry into Export_History for route export
======================================================================================
Modification History
	Developer		      Date		Modification 					
======================================================================================
1) K.Karthicksundar			  		Created 	
2)	
**************************************************************************************/
BEGIN
	DECLARE @SiteCode VARCHAR(100)
	Select @SiteCode=Site_Code FROM SITE WHERE SITE_ID=@SiteID
	EXEC usp_InsertExportHistory 0,'ROUTE',@SiteCode
END

GO

