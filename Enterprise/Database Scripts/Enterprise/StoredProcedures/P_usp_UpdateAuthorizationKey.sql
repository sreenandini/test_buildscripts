USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateAuthorizationKey]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateAuthorizationKey]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.usp_UpdateAuthorizationKey
	@SiteID INT,
	@UserId INT,
	@Type INT,  -- 1 - FactoryReset,2 - SiteRecovery,3 - NewSite
	@TransactionKey VARCHAR(200)
AS
/*****************************************************************************************************
DESCRIPTION		:	Update the Transaction Key for the given site.
					Possible values for @Type : 1 - FactoryReset
												2 - SiteRecovery
												3 - NewSite
CREATED DATE	:
MODULE			:	Site Maintenance in Enterprise Client      

CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR							MODIFIED DATE	DESCRIPTON
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON
	
	DECLARE @iExpiryValue INT

	SELECT @iExpiryValue = Setting_Value
	FROM   Setting WITH (NOLOCK)
	WHERE  Setting_Name = 'AUTHORIZATION_KEY_EXPIRY_HOURS'
	
	SET @iExpiryValue = ISNull(@iExpiryValue,1)
	
	
	IF EXISTS(SELECT 1 FROM [Site] s WHERE s.Site_ID=@SiteID AND ISNULL(s.Site_Setting_Profile_ID,0)=0)
	BEGIN
		RETURN -100---Checks site setting applied for that site
	END
	IF EXISTS(
	       SELECT 1
	       FROM   TransactionKeys TK WITH (NOLOCK)
	              JOIN [TransactionFlag] TF WITH (NOLOCK)
	                   ON  TF.[TransactionFlagid] = TK.[TransactionFlagid]
	       WHERE  TK.SiteID = @SiteID
	              AND TF.[TransactionFlagid] = @Type
	              AND ISNULL(TK.[Void], 0) = 0
	              AND TK.[ExpiryDate] > TK.[CreatedDate]
	   )
	BEGIN
	    UPDATE TransactionKeys
	    SET    [Siteid] = @SiteID,
	           [TransactionKey] = @TransactionKey,
	           [ModifiedDate] = GETDATE(),
	           [Userid] = @UserId,
	           [ExpiryDate] = DATEADD(hh, @iExpiryValue, GETDATE())
	    FROM   [TransactionKeys] TK
	           JOIN [TransactionFlag] TF WITH (NOLOCK)
	                ON  TF.[TransactionFlagid] = TK.[TransactionFlagid]
	    WHERE  TK.SiteId = @SiteID
	           AND TF.[TransactionFlagid] = @Type
	           AND ISNULL(TK.[Void], 0) = 0
	           AND TK.[ExpiryDate] > TK.[CreatedDate]
	END
	ELSE
	BEGIN
	    INSERT INTO TransactionKeys
	      (
	        [Siteid],
	        [TransactionKey],
	        [TransactionFlagid],
	        [CreatedDate],
	        [ModifiedDate],
	        [ExpiryDate],
	        [Userid],
	        [Void]
	      )
	    VALUES
	      (
	        @SiteID,
	        @TransactionKey,
	        @Type,
	        GETDATE(),
	        '',
	        DATEADD(hh, @iExpiryValue, GETDATE()),
	        @UserId,
	        0
	      )
	END
END

GO

