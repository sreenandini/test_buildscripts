USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Export_Calendar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Export_Calendar]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Export_Calendar]
(
    @Reference1     VARCHAR(50),
    @Site_id        INT,
    @Result         INT OUTPUT,
    @ErrorMessage   VARCHAR(250) OUTPUT
)
AS
	/*****************************************************************************************************
DESCRIPTION		: Procedure to Export Calendar Information to the site 
CREATED DATE	: 2012-10-30 06:03:51.280
MODULE          : FrmAdminSite(Comm's Tab)   
CHANGE HISTORY	:
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                            MODIFIED DATE
------------------------------------------------------------------------------------------------------
Venkatesan Haridass                Enterprise Rewrite                                     2012-10-30
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/
BEGIN
	SET @Result = 0 -- Return 0 is success
	SET @ErrorMessage =''
	IF NOT EXISTS (
	       SELECT 1
	       FROM   [Site] s
	       WHERE  s.Site_ID = @Site_id
	              AND ISNULL(s.Site_Code, '') <> ''
	   )
	BEGIN
	    SET @Result = -1 -- Return -1 for site code empty/null
	    SET @ErrorMessage = 'MSG_ENTER_SITE_CODE'
	END
	ELSE 
	IF NOT EXISTS (
	       SELECT TOP 1 1 
	       FROM   dbo.Calendar C
	              INNER JOIN dbo.Sub_Company_Calendar SCC
	                   ON  C.Calendar_ID = SCC.Calendar_ID
	              INNER JOIN dbo.Site S
	                   ON  SCC.Sub_Company_ID = S.Sub_Company_ID
	       WHERE  SCC.Sub_Company_Calendar_Active = 1
	              AND S.Site_ID = @Site_id
	   )
	BEGIN
	    SET @Result = -4 -- Return -4 if There is no Active Calendar for the site 
	    SET @ErrorMessage = 'MSG_SET_CALENDAR_SITE'
	END
	ELSE
	BEGIN
	    EXECUTE [dbo]

.[usp_Export_History] 
	    @Reference1
	    ,'S-CALENDAR'
	    ,@Site_id
	    IF @@ERROR > 0
	    BEGIN
	        PRINT @@ERROR    
	        SET @Result = -2 -- Return -2 for unknown error
	        SET @ErrorMessage = 'MSG_EXPORT_FAILED'
	    END
	END
END

GO

