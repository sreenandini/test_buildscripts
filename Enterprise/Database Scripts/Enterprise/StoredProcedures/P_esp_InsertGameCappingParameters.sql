USE Enterprise
GO

IF EXISTS (SELECT * FROM sys.objects o WHERE o.[name]='esp_InsertGameCappingParameters' AND o.[type]='P')
BEGIN
	DROP PROCEDURE esp_InsertGameCappingParameters
END
GO

CREATE PROCEDURE esp_InsertGameCappingParameters(
    @CapReleaseOnPlayerCardIn  BIT,
    @ReserveGameForPlayer      BIT,
    @ReserveGameForEmployee    BIT,
    @MintsToExpire             INT,
    @Site                      INT,
    @Staff_ID                  INT,
    @Module_ID                 INT,
    @Module_Name               VARCHAR(150),
    @Screen_Name               VARCHAR(150)
)
AS
BEGIN
	DECLARE @Audit       TABLE (
	            N_CapReleaseOnPlayerCardIn BIT,
	            O_CapReleaseOnPlayerCardIn BIT,
	            N_ReserveGameForPlayer BIT,
	            O_ReserveGameForPlayer BIT,
	            N_ReserveGameForEmployee BIT,
	            O_ReserveGameForEmployee BIT,
	            N_MintsToExpire INT,
	            O_MintsToExpire INT
	        )
	
	DECLARE @SiteCode    VARCHAR(20)
	
	DECLARE @Staff_Name  VARCHAR(100)
	DECLARE @Audit_DESC  VARCHAR(MAX)	
	
	SELECT @Staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  UserTableID = @Staff_ID
	
	SELECT @SiteCode = site_code
	FROM   SITE
	WHERE  site_id = @Site
	
	IF NOT EXISTS(
	       SELECT 1
	       FROM   GameCapping
	       WHERE  SITE = @SiteCode
	   )
	BEGIN
	    INSERT INTO GameCapping
	    VALUES
	      (
	        @CapReleaseOnPlayerCardIn,
	        @ReserveGameForPlayer,
	        @ReserveGameForEmployee,
	        @MintsToExpire,
	        @SiteCode
	      )
	    
	    SET @Audit_DESC = 'SiteCode = ' + @SiteCode
			+ ' ,CapReleaseOnPlayerCardIn = ' + CAST(@CapReleaseOnPlayerCardIn AS VARCHAR(10)) 
	        + ' ,ReserveGameForPlayer = ' + CAST(@ReserveGameForPlayer AS VARCHAR(10))
	        + ' ,ReserveGameForEmployee = ' + CAST(@ReserveGameForEmployee AS VARCHAR(10))
	        + ' ,MintsToExpire = ' + CAST(@MintsToExpire AS VARCHAR(10))
	    
	    EXEC [usp_InsertAuditData] 
	         @Staff_ID,
	         @Staff_Name,
	         @Module_ID,
	         @Module_Name,
	         @Screen_Name,
	         '',
	         '',
	         '',
	         'TRUE',
	         @Audit_DESC,
	         'ADD'
	END
	ELSE
	BEGIN
	    UPDATE GameCapping
	    SET    CapReleaseOnPlayerCardIn = @CapReleaseOnPlayerCardIn,
	           ReserveGameForPlayer = @ReserveGameForPlayer,
	           ReserveGameForEmployee = @ReserveGameForEmployee,
	           MintsToExpire = @MintsToExpire
	           OUTPUT INSERTED.CapReleaseOnPlayerCardIn,
	           DELETED.CapReleaseOnPlayerCardIn,
	           INSERTED.ReserveGameForPlayer,
	           DELETED.ReserveGameForPlayer,
	           INSERTED.ReserveGameForEmployee,
	           DELETED.ReserveGameForEmployee,
	           INSERTED.MintsToExpire,
	           DELETED.MintsToExpire 
	           INTO @Audit
	    WHERE  SITE = @SiteCode
	    
	    SELECT @Audit_DESC = (
	               CASE 
	                    WHEN O_CapReleaseOnPlayerCardIn <>  N_CapReleaseOnPlayerCardIn THEN 
	                         'Updated [CapReleaseOnPlayerCardIn] from ' +
	                         CAST(O_CapReleaseOnPlayerCardIn AS VARCHAR(10)) + 
	                         ' TO  ' +
	                         CAST(N_CapReleaseOnPlayerCardIn AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN O_ReserveGameForPlayer <> N_ReserveGameForPlayer THEN 
	                         ' Updated [ReserveGameForPlayer]  from ' + CAST(O_ReserveGameForPlayer AS VARCHAR(10))
	                         + ' TO  ' + CAST(N_ReserveGameForPlayer AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN O_ReserveGameForEmployee <> 
	                         N_ReserveGameForEmployee THEN 
	                         ' Updated [ReserveGameForEmployee]  from ' + CAST(O_ReserveGameForEmployee AS VARCHAR(10))
	                         + ' TO  ' + CAST(N_ReserveGameForEmployee AS VARCHAR(10))
	                    ELSE ''
	               END
	           ) + ' ' + (
	               CASE 
	                    WHEN O_MintsToExpire <> N_MintsToExpire THEN 
	                         ' Updated [MintsToExpire]  from ' + CAST(O_MintsToExpire AS VARCHAR(10))
	                         + ' TO  ' + CAST(N_MintsToExpire AS VARCHAR(10))
	                    ELSE ''
	               END
	           )
	    FROM   @Audit
	    
	    IF (@Audit_DESC <> '')
	    BEGIN
	        SET @Audit_DESC = 'Site : ' + @SiteCode + ' ' + @Audit_DESC 
	        
	        EXEC [usp_InsertAuditData] 
	             @Staff_ID,
	             @Staff_Name,
	             @Module_ID,
	             @Module_Name,
	             @Screen_Name,
	             '',
	             '',
	             '',
	             '',
	             @Audit_DESC,
	             'MODIFY'
	    END
	END
END
GO