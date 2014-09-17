USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Export_Model]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Export_Model]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Export_Model]
(
    @Reference        VARCHAR(50),
    @Site_id          INT,
    @Result           INT OUTPUT,
    @ErrorMessage     VARCHAR(250) OUTPUT
)
AS
	/*****************************************************************************************************
DESCRIPTION		: Procedure to Export Model Information to the site 
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
	SET @ErrorMessage = ''
	
	IF NOT EXISTS (
	       SELECT 1
	       FROM   [Site] s
	       WHERE  s.Site_ID = @Site_id
	              AND ISNULL(s.Site_Code, '') <> ''
	   )
	BEGIN
	    SET @Result = -1 -- Return -1 for site code empty/null
	    SET @ErrorMessage = 'Please enter a Site Code to Export the Details.'
	END
	ELSE 
	IF NOT EXISTS (
	       SELECT TOP 1             1
	       FROM   Machine_Class     mc
	   )
	BEGIN
	    SET @Result = -3 -- Return -3 If there is no Machine
	    SET @ErrorMessage = 'No Models to Export.'
	END
	ELSE
	BEGIN
	    EXECUTE [dbo].[usp_Export_History] 
	    @Reference
	    ,'MODEL'
	    ,@Site_id
	    IF @@ERROR > 0
	    BEGIN
	        PRINT @@ERROR    
	        SET @Result = -2 -- Return -2 for unknown error
	        SET @ErrorMessage = 
	            'Export Failed for the Site.\r\n Please try again Later..'
	    END
	END
END

GO
