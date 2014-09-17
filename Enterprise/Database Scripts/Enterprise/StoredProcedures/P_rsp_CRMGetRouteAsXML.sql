USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CRMGetRouteAsXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CRMGetRouteAsXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC rsp_CRMGetRouteAsXML
@SiteCode INT 
/************************************************************************************
Used In(Module) : Export Service
Created Date	:
Description		: Returns Routes & route members as XML to export to Exchange
======================================================================================
Modification History
	Developer		        Modification 								
======================================================================================
1) K.Karthicksundar			Created 	
2)	
**************************************************************************************/
AS
BEGIN 
	DECLARE @RouteXML AS VARCHAR(MAX)
	DECLARE @RouteMemberXML AS VARCHAR(MAX)
	DECLARE @SiteID int 
	
	SELECT @SiteID = site_id  FROM Site  
	WHERE Site_Code=@SiteCode
	

	SELECT @RouteXML=(
		SELECT	Route_ID  AS [NO],
				Route_Name AS [NM],
				Active AS [ACT],
				[User_id] AS USR	
		from [route] 
		WHERE Site_id=@SiteID ORDER BY Route_ID
		FOR XML RAW('R'),ROOT('RS')   -- R-> Route. ->Routes
	)
	SELECT @RouteMemberXML=
	(
		SELECT	R1.Route_Member_ID  AS [RMID],
				R1.Route_ID AS [RTID],
				B.Bar_Position_Name AS [BP]
		from [route_member]  R1
		INNER JOIN BAR_POSITION B ON  R1.Bar_Position_ID=B.Bar_Position_ID   	
		INNER JOIN [ROUTE] R2 ON  R1.Route_ID = R2.Route_ID AND R2.Site_ID=@SiteID ORDER BY R1.Route_Member_ID
		FOR XML RAW('RM'),ROOT('RMS') -- RM-> RouteMember. RMS ->RoutesMembers
	)
	SELECT CAST('<RMINFO>'+ ISNULL(@RouteXML,'') + ISNULL(@RouteMemberXML,'') +'</RMINFO>' AS XML)  

END 




GO

