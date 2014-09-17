USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMaintenanceSessionSelectedAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMaintenanceSessionSelectedAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/****
Version History
----------------------------------------
Anil		28 Oct 2010		Created
----------------------------------------
***/

CREATE PROCEDURE [dbo].[rsp_GetMaintenanceSessionSelectedAsset]
(
@FromDate DateTime='2010-05-21 00:00:00.000' , 
@ToDate DateTime ='2011-06-22 00:00:00.000' , 
@SiteID INT =2, 
@Authorization INT=0,
@Machine_Stock_No varchar(50)='LC0008',
@CurrentInstallationOnly BIT =0,
@SessionAcrossAllSite BIT=0
)
AS
BEGIN

	IF (@SessionAcrossAllSite=0)
	BEGIN
		SELECT 
			tMS.ID, 
			tS.Site_Name, 
			tS.Site_ID as Site_ID,
			tM.Machine_Stock_No AS [Asset No], 
			CASE tMS.isAuthorized WHEN 0 Then 'Non Authorized' ELSE 'Authorized' END AS [Type], 
			right('0' + cast(datediff(hh, tMS.CreatedOn,tMS.ClosedOn) as varchar),2) + ':' + right('0' + cast(datediff(mi, tMS.CreatedOn,tMS.ClosedOn) % 60 as varchar),2) as [Duration],
			tMS.CreatedOn AS StartDate,  
			tMS.ClosedOn AS EndDate, 
			tU.UserName,
			Category = '',
			Reason = '',
			Comment = '',
			[Meters Change] = CASE dbo.fn_HasMeterChanged(tMS.ID, @SiteID) WHEN 1 THEN 'Yes' Else 'No' END
		FROM MaintenanceSession tMS
		INNER JOIN SITE tS ON tMS.Site_ID = tS.Site_ID
		INNER JOIN INSTALLATION tI ON tI.Installation_ID = tMS.Installation_No 
		INNER JOIN MACHINE tM ON tM.Machine_ID = tI.Machine_ID
		LEFT OUTER JOIN [USER] tU ON tU.SecurityUserID = tMS.ClosedBy
		WHERE tS.Site_ID = @SiteID --AND IsSessionOpen = 0 AND tMS.SITE_ID = @SiteID 
		AND tMS.CreatedOn BETWEEN @FromDate AND @ToDate
		AND ((@Authorization = 2 AND isAuthorized = 0) OR (@Authorization = 1 AND isAuthorized = 1) OR (@Authorization = 0))
		AND tM.Machine_Stock_No=@Machine_Stock_No
		AND ((@CurrentInstallationOnly=1 AND tI.Installation_End_Date is null) OR (@CurrentInstallationOnly=0))

	END
	ELSE
	BEGIN
		SELECT 
			tMS.ID, 
			tS.Site_Name, 
			tS.Site_ID as Site_ID,
			tM.Machine_Stock_No AS [Asset No], 
			CASE tMS.isAuthorized WHEN 0 Then 'Non Authorized' ELSE 'Authorized' END AS [Type], 
			right('0' + cast(datediff(hh, tMS.CreatedOn,tMS.ClosedOn) as varchar),2) + ':' + right('0' + cast(datediff(mi, tMS.CreatedOn,tMS.ClosedOn) % 60 as varchar),2) as [Duration],
			tMS.CreatedOn AS StartDate,  
			tMS.ClosedOn AS EndDate, 
			tU.UserName,
			Category = '',
			Reason = '',
			Comment = '',
			[Meters Change] = CASE dbo.fn_HasMeterChanged(tMS.ID, @SiteID) WHEN 1 THEN 'Yes' Else 'No' END
		FROM MaintenanceSession tMS
		INNER JOIN SITE tS ON tMS.Site_ID = tS.Site_ID
		INNER JOIN INSTALLATION tI ON tI.Installation_ID = tMS.Installation_No 
		INNER JOIN MACHINE tM ON tM.Machine_ID = tI.Machine_ID
		LEFT OUTER JOIN [USER] tU ON tU.SecurityUserID = tMS.ClosedBy
		WHERE --IsSessionOpen = 0  
		--AND 
		tMS.CreatedOn BETWEEN @FromDate AND @ToDate
		AND ((@Authorization = 2 AND isAuthorized = 0) OR (@Authorization = 1 AND isAuthorized = 1) OR (@Authorization = 0))		
		AND tM.Machine_Stock_No=@Machine_Stock_No
		AND ((@CurrentInstallationOnly=1 AND tI.Installation_End_Date is null) OR (@CurrentInstallationOnly=0))	
	END

END


GO

