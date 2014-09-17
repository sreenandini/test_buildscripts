USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ASPNET_SP_MAPUserToSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ASPNET_SP_MAPUserToSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[ASPNET_SP_MAPUserToSite] (@UserID INT, @SiteID INT)    
AS    
BEGIN    
	IF NOT EXISTS (SELECT * FROM UserSite_lnk WHERE SecurityUserID = @UserID AND SiteID = @SiteID)    
	BEGIN    
	 INSERT INTO UserSite_lnk (SecurityUserID, SiteID) VALUES (@UserID, @SiteID)	     
	END
	
	DECLARE @IsEmployeeCardTrackingEnabled BIT
	    
	    SELECT @IsEmployeeCardTrackingEnabled = CASE WHEN Setting_Value = 'TRUE' THEN  1  ELSE  0 END
		FROM Setting WHERE Setting_Name = 'IsEmployeeCardTrackingEnabled'

		IF @IsEmployeeCardTrackingEnabled = 1	
		BEGIN
			INSERT INTO dbo.Export_History ( EH_Date, EH_Reference1, EH_Type, EH_Site_Code )
			SELECT DISTINCT GETDATE(),
			       tecd.EmployeeCardNumber,
			       'MASTERCARDENABLE',
			       X.Site_Code
			FROM (
			         SELECT SecurityUserID,
			                Site_Code
			         FROM   usersite_lnk
			                LEFT JOIN SITE S
			                     ON  S.site_id = usersite_lnk.siteid
			     )X CROSS APPLY tblEmployeeCardDetails tecd
			WHERE  tecd.IsMasterCard = 1 AND X.SecurityUserID = @UserID
				
			IF EXISTS (SELECT 1 FROM tblEmployeeCardDetails WHERE UserID = @UserID)
			BEGIN
			INSERT INTO dbo.Export_History
				  (
				    EH_Date,
				    EH_Reference1,
				    EH_Type,
				    EH_Site_Code
				  )
				SELECT DISTINCT GETDATE(),
				       @UserID,
				       'Userdetails',
				       S.Site_Code
				FROM   usersite_lnk
				       LEFT JOIN SITE S
				            ON  S.site_id = usersite_lnk.siteid
				WHERE  securityuserid = @UserID	
			END
		END	
END

GO

