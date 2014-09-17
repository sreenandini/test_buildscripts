USE ENTERPRISE
GO
 
IF EXISTS (
       SELECT *
       FROM   DBO.SYSOBJECTS
       WHERE  id = OBJECT_ID(N'[dbo].[usp_UpdateSiteUserDetails]')
              AND OBJECTPROPERTY(id, N'IsProcedure') = 1
   )
BEGIN
    DROP PROCEDURE [dbo].[usp_UpdateSiteUserDetails]
END
GO

CREATE PROCEDURE usp_UpdateSiteUserDetails
	@UserIDs VARCHAR(MAX),
	@SiteID INT,
	@SiteName VARCHAR(200),
	@Audit_User_ID INT,
	@Audit_User_Name VARCHAR(50),
	@Audit_Module_ID INT,
	@Audit_ModuleName VARCHAR(200)
AS
BEGIN
	BEGIN TRAN   
	
	DECLARE @audit TABLE (USERID INT, Operation CHAR(1)) 
	
	-- CLEAN ALL USERS FOR SITE     
	DELETE 
	FROM   UserSite_lnk 
	       OUTPUT DELETED.SecurityUserID,
	       'D' 
	       INTO @audit
	WHERE  SiteID = @SiteID    
	
	IF @@ERROR <> 0
	    GOTO ERR 
	
	-- ASSIGN USERS FOR SITE     
	INSERT INTO UserSite_lnk 
	       OUTPUT INSERTED.SecurityUserID,'I' 
	       INTO @audit
	SELECT intitem,
	       @SiteID
	FROM   dbo.Fn_GetIntTableFromStringList(@UserIDs) 
	
	IF @@ERROR <> 0
	    GOTO ERR 
	
	/*--------------------------AUDIT------------------------------*/
	
	--DELETE UNMODIFIED RECORDS FROM TEMP TABLE 
	DELETE 
	FROM   @audit
	WHERE  userid IN (SELECT B.userid
	                  FROM   @audit B
	                  GROUP BY
	                         B.USERID
	                  HAVING COUNT(B.userid) > 1)
	
	
	-- AUDIT CHANGES 
	INSERT INTO dbo.Audit_History
	SELECT GETDATE(),
	       @Audit_User_ID,
	       @Audit_User_Name,
	       @Audit_Module_ID,
	       @Audit_ModuleName,
	       'Site User Administration',
	       CASE 
	            WHEN a.Operation = 'D' THEN u.UserName + ' (''' + CAST(a.Userid AS VARCHAR(10))
	                 + ''') Removed From Site ' + @SiteName
	            ELSE u.UserName + ' (''' + CAST(a.Userid AS VARCHAR(10))
	                 + ''') Assigned To Site ' + @SiteName
	       END,
	       NULL,
	       'USER',
	       CASE 
	            WHEN a.Operation = 'D' THEN 'TRUE'
	            ELSE NULL
	       END,
	       CASE 
	            WHEN a.Operation = 'I' THEN 'TRUE'
	            ELSE NULL
	       END,
	       CASE 
	            WHEN a.Operation = 'I' THEN 'ADD'
	            ELSE 'DELETE'
	       END
	FROM   @audit A
	       INNER JOIN [user] U
	            ON  a.USERID = u.SecurityUserID
	
	IF @@ERROR <> 0
	    GOTO ERR
	
	DECLARE @Site_Code VARCHAR(50)  
	SELECT @Site_Code = Site_Code
	FROM   dbo.Site
	WHERE  Site_ID = @SiteID 
	
	
	/* --------------------------EXPORT TO SITE------------------------------
	* ADDUSER
	* REMOVEUSER
	* USERROLE
	*/
	
	-- EXPORT ADD/REMOVE USERS TO SITE
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       UserID,
	       CASE 
	            WHEN Operation = 'D' THEN 'REMOVEUSER'
	            ELSE 'ADDUSER'
	       END,
	       @Site_Code
	FROM   @audit
	
	IF @@ERROR <> 0
	    GOTO ERR
	
	--EXPORT USERROLE FOR NEW USERS
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       UserID,
	       'USERROLE',
	       @Site_Code
	FROM   @audit
	WHERE  operation = 'I'
	
	
	INSERT INTO dbo.Export_History
	  (
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       UserID,
	       'Userdetails',
	       @Site_Code
	FROM   @audit
	WHERE  operation = 'I'
		
	
	IF @@ERROR <> 0
	    GOTO ERR
	
	
	COMMIT TRAN 
	RETURN 0
	
	
	ERR: 
	ROLLBACK TRAN
END
GO