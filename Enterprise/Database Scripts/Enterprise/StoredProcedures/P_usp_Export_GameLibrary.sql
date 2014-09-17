USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Export_GameLibrary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Export_GameLibrary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_Export_GameLibrary]
(
    @Site_id          INT,
    @Result           INT OUTPUT,
    @ErrorMessage     VARCHAR(250) OUTPUT
)
AS
	/*****************************************************************************************************
DESCRIPTION		: Procedure to Export Game Library Information to the site 
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
	
	DECLARE @Site_Code VARCHAR(50)
	
	SET @Result = 0 -- Return 0 is success
	SET @ErrorMessage =''
	
	SET @Site_Code = ''
	
	SELECT @Site_Code = Site_Code FROM [Site] WHERE Site_ID = @Site_id
	
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
	       SELECT 1
	       FROM   dbo.Game_Library gl
	   )
	BEGIN
	    SET @Result = -5 -- Return -5 If there is no Game Library
	    SET @ErrorMessage = 'No Games to Export.'
	END
	ELSE
	BEGIN
		IF @Site_Code <> ''
		BEGIN
			INSERT INTO Export_History
			  (
				EH_Date,
				EH_Reference1,
				EH_Type,
				EH_Site_Code
			  )
			SELECT GETDATE(),
				   Game_Category_ID,
				   'GAMECATEGORY',
				   @Site_Code
			FROM   Game_Category
			WHERE  Game_Category_Name <> 'Unassigned Category'
		END
		
	    INSERT INTO Export_History
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           MG_Game_ID,
	           'GAMELIBRARY',
	           Site_Code
	    FROM   dbo.[Site] s
	           INNER JOIN dbo.Game_Library gl
	                ON  gl.Site_ID = s.Site_ID
	    WHERE  s.Site_ID = @Site_id
	    
	    IF @@ERROR > 0
	    BEGIN
	        PRINT @@ERROR    
	        SET @Result = -2 -- Return -2 for unknown error
	        SET @ErrorMessage = 'Export Failed for the Site.\r\n Please try again Later..'
	    END
	END
END

GO

