USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnableorDisableSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnableorDisableSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_EnableorDisableSite]
(
    @Site_id          INT,
    @IsEnable         BIT,
    @Result           INT OUTPUT,
    @ErrorMessage     VARCHAR(250) OUTPUT
)
AS
	/*****************************************************************************************************
DESCRIPTION		: Enable or Disable the Site
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
	BEGIN
	    DECLARE @Type VARCHAR(50)
	    
	    
	    SELECT @Type = CASE 
	                        WHEN @IsEnable = 1 THEN 'ENABLESITE'
	                        WHEN @IsEnable = 0 THEN 'DISABLESITE'
	                   END
	    
	    BEGIN TRANSACTION 
	    
	    UPDATE SITE
	    SET    Site_Enabled = @IsEnable
	    WHERE  Site_ID = @Site_id
	    
	    IF @@ERROR > 0
	    BEGIN
	        PRINT @@ERROR 
	        ROLLBACK TRANSACTION   
	        SET @Result = -6 -- Return -6 for failed to update site table
	        SET @ErrorMessage = 'Failed to '+@Type +' the Site'
	        RETURN
	    END
	    
	    EXEC usp_Export_History
	         @Site_id,
	         @Type,
	         @Site_id
	    
	    IF @@ERROR > 0
	    BEGIN
	        PRINT @@ERROR 
	        ROLLBACK TRANSACTION   
	        SET @Result = -2 -- Return -2 for unknown error
	        SET @ErrorMessage = 
	            'Export Failed for the Site.\r\n Please try again Later..'
	        RETURN
	    END
	    
	    COMMIT TRANSACTION
	END
END

GO

