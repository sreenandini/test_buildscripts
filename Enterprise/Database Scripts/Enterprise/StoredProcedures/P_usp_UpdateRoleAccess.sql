USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateRoleAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateRoleAccess]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*    
* Revision History    
*     
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description    
  Kalaiyarasan.P              05-Sep-2012         Created               This SP is used to Update Staff_Depot details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE usp_UpdateRoleAccess
	@SecurityUserID INT,
	@UserLevel VARCHAR(100)
AS
BEGIN
	DECLARE @SecurityRoleID INT
	SELECT @SecurityRoleID = SecurityRoleID FROM [Role]WHERE RoleName = @UserLevel
	IF (ISNULL( @SecurityRoleID,0) <> 0)
	BEGIN
	    IF NOT EXISTS (  
	       SELECT 1 FROM UserRole_lnk WHERE SecurityUserID = @SecurityUserID
	       )
	    BEGIN
	        INSERT INTO UserRole_lnk ( SecurityUserID, SecurityRoleID ) VALUES ( @SecurityUserID, @SecurityRoleID  )
	    END
	    ELSE
	    BEGIN
	        UPDATE UserRole_lnk
	        SET    SecurityRoleID = @SecurityRoleID
	        WHERE  SecurityUserID = @SecurityUserID
	    END    
	    
	    INSERT INTO dbo.Export_History ( EH_Date, EH_Reference1, EH_Type, EH_Site_Code )
	    SELECT DISTINCT GETDATE(), @SecurityUserID, 'USERROLE', S.Site_Code FROM usersite_lnk LEFT JOIN SITE S ON S.site_id = usersite_lnk.siteid WHERE securityuserid = @SecurityUserID
	    
	    DECLARE @UserSitelnkCount INT
	    SELECT @UserSitelnkCount = COUNT(SecurityUserID) FROM UserSite_lnk usk INNER JOIN Site s on s.Site_ID = usk.SiteID WHERE usk.SecurityUserID = @SecurityUserID
	    
	    INSERT INTO dbo.Export_History ( EH_Date, EH_Reference1, EH_Type, EH_Site_Code )
	    SELECT DISTINCT GETDATE(), @SecurityUserID,
		    CASE WHEN ISNULL(st.Staff_Terminated,0)=1 THEN  'TERMINATEUSER'		    
			WHEN  @UserSitelnkCount > 0 THEN 'UPDATEUSER' 
			END ,
			 S.Site_Code 
	    FROM VW_Enterprise_usersite_lnk  ul
		inner join staff st	On ul.SecurityUserID = st.UserTableID 
	    LEFT JOIN SITE S ON S.site_id = ul.siteid 
	    WHERE securityuserid = @SecurityUserID
	    
	    
	    DECLARE @IsEmployeeCardTrackingEnabled BIT
	    
	    SELECT @IsEmployeeCardTrackingEnabled = CASE WHEN Setting_Value = 'TRUE' THEN  1  ELSE  0 END
		FROM Setting WHERE Setting_Name = 'IsEmployeeCardTrackingEnabled'

		IF @IsEmployeeCardTrackingEnabled = 1	
		BEGIN
			IF EXISTS (SELECT 1 FROM tblEmployeeCardDetails WHERE UserID = @SecurityUserID)
			BEGIN
				INSERT INTO dbo.Export_History ( EH_Date, EH_Reference1, EH_Type, EH_Site_Code )
				SELECT DISTINCT GETDATE(), @SecurityUserID, 'Userdetails', S.Site_Code FROM VW_Enterprise_usersite_lnk LEFT JOIN SITE S ON S.site_id = VW_Enterprise_usersite_lnk.siteid WHERE securityuserid = @SecurityUserID	
			END
		END			
			
	END
END


GO

