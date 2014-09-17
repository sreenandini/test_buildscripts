USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CRMGetRoutesBySiteID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CRMGetRoutesBySiteID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  PROC dbo.rsp_CRMGetRoutesBySiteID  
@Site_ID int
AS
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Returns all routes for a site
======================================================================================
Modification History
	Developer		      Date		Modification 					
======================================================================================
1) K.Karthicksundar			  		Created 	
2)	
**************************************************************************************/
BEGIN 
	SELECT Route_ID AS Route_No,Route_Name,Site_ID,Active ,'0' as  CanDelete ,'0' AS Modified
	FROM dbo.[Route]
	WHERE Site_id=@Site_ID
END


GO

