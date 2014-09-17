/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 02/04/2013 12:28:48 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetViewSiteStatusInfo]    Script Date: 02/07/2013 14:56:27 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetViewSiteStatusInfo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetViewSiteStatusInfo]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_GetViewSiteStatusInfo]    Script Date: 02/07/2013 14:56:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
Exec [rsp_GetViewSiteStatusInfo] '1010'
* 
*/
CREATE PROCEDURE [dbo].[rsp_GetViewSiteStatusInfo]
@Site_Code VARCHAR(50) = NULL, 
@UserName VARCHAR(50) = ''
AS
BEGIN
	SET NOCOUNT ON -- <<< ADO likes this when using temp tables     
	DECLARE @Hourly  VARCHAR(150),
	        @Region  VARCHAR(50)
	
	SELECT @Hourly = HourlyNotRun
	FROM   [Site] s
	WHERE  Site_Code = @Site_Code
	--SELECT @Region = System_Parameter_Region_Culture FROM System_Parameters  
	
	SELECT @Region = UL.CultureInfo
	FROM   [User] U WITH (NOLOCK)
	       LEFT JOIN UserLanguages UL WITH (NOLOCK)
	            ON  U.DateCulture = UL.LanguageID
	WHERE  UserName = @UserName 
	
	IF ISNULL(@Region,'') = ''
	BEGIN
		SELECT @Region = System_Parameter_Region_Culture FROM System_Parameters
	END
	
	SELECT @Hourly AS HourlyNotRun,
	       @Region AS Region
	
	RETURN @@ERROR
END
GO

