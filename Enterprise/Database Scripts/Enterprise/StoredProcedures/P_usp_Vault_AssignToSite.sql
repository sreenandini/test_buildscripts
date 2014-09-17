USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'usp_Vault_AssignToSite'
   )
    DROP PROCEDURE dbo.usp_Vault_AssignToSite
GO

CREATE PROCEDURE dbo.usp_Vault_AssignToSite
	@xml XML,
	 --Audit details 
	 /*------------------------*/
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150)
	 /*------------------------*/
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: 26-aug-2012
MODULE		: PROC used in vault creation screen , Assigning to site  
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------


*****************************************************************************************************/
BEGIN
	--AUDIT DETAILS  
	
	/*
	'<Vaults>
	<Vault NGADevice_ID="" Site_ID=""/>
	</Vaults>'
	*/
	BEGIN TRAN 
	DECLARE @docHandle INT   
	EXEC sp_xml_PrepareDocument @docHandle OUTPUT,
	     @xml
	
	DECLARE @tempInstallation TABLE
	        (
	            NGADevice_ID INT,
	            Site_ID INT,
	            User_No INT,
	            StarDate DATETIME,
	            AssignToSiteDate DATETIME
	        ) 
	
	DECLARE @CurrDatetime DATETIME
	SET @CurrDatetime = GETDATE()
	
	
	INSERT INTO @tempInstallation
	  (
	    NGADevice_ID,
	    Site_ID,
	    User_No,
	    StarDate,
	    AssignToSiteDate
	  )
	SELECT A.NGADevice_ID,
	       A.Site_ID,
	       @User_ID,
	       @CurrDatetime,
	       @CurrDatetime
	FROM   OPENXML(@docHandle, 'Vaults/Vault', 1) WITH 
	       (NGADevice_ID INT '@NGADevice_ID', Site_ID INT '@Site_ID') AS A    
	
	
	EXEC sp_xml_removedocument @docHandle 
	
	INSERT INTO tNGAInstallations
	  (
	    NGADevice_ID,
	    Site_ID,
	    Create_Date,
	    Assigned_To_Site
	  )
	SELECT NGADevice_ID,
	       Site_ID,
	       StarDate,
	       AssignToSiteDate
	FROM   @tempInstallation
	
	
	INSERT INTO Export_History
	  (
	    -- EH_ID -- this column value is auto-generated,    
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Status,
	    EH_Export_Date,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       Manufacturer_ID,
		   'MANUFACTURER_DETAILS',
	       NULL,
	       NULL,
	       s.Site_Code
	FROM   @tempInstallation temp
	       INNER JOIN tVault_Devices tvd
	            ON  tvd.NGADevice_ID = temp.NGADevice_ID
	       INNER JOIN SITE s
	            ON  s.Site_ID = temp.Site_ID
	            		
			 
	
	  
	INSERT INTO Export_History
	  (
	    -- EH_ID -- this column value is auto-generated,    
	    EH_Date,
	    EH_Reference1,
	    EH_Type,
	    EH_Status,
	    EH_Export_Date,
	    EH_Site_Code
	  )
	SELECT GETDATE(),
	       tvd.Vault_ID,
	       'VAULTDEVICE',
	       NULL,
	       NULL,
	       s.Site_Code
	FROM   @tempInstallation temp
	       INNER JOIN tVault_Devices tvd
	            ON  tvd.NGADevice_ID = temp.NGADevice_ID
	       INNER JOIN SITE s
	            ON  s.Site_ID = temp.Site_ID
	
	
	IF @@ERROR <> 0
	    GOTO err
	
	--AUDIT ANY CHANGES------------------------------------------------------------------------------------ 
	DECLARE @staff_id INT  
	DECLARE @staff_Name VARCHAR(100)
	SET @staff_id = @User_ID -- CHANGED FROM USER ID TO STAFFID FROM CLIENT 
	
	SELECT @staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  Staff_ID = @User_ID
	
	DECLARE @Audit_DESC VARCHAR(MAX)
	
	
	SELECT @Audit_DESC = ISNULL(@Audit_DESC, '') + ' ' +
	       'Vault (ID:  ' + CAST(temp.NGADevice_ID AS VARCHAR(10))
	       + '  Name:  ' + td.[Name]
	       + ') Assigned to Site (Code: ' + CAST(s.Site_Code AS VARCHAR(10)) 
	       + ' Name: ' + s.Site_Name + ' )'
	FROM   @tempInstallation temp
	       INNER JOIN tVault_Devices td WITH(NOLOCK)
	            ON  td.NGADevice_ID = temp.NGADevice_ID
	       INNER JOIN SITE s WITH(NOLOCK)
	            ON  s.Site_ID = temp.Site_ID
	
	SELECT @Audit_DESC
	IF (ISNULL(@Audit_DESC, '') <> '')
	BEGIN
	    EXEC [usp_InsertAuditData] 
	         @staff_id,
	         @staff_Name,
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
	
	IF @@ERROR <> 0
	    GOTO err
	
	--Error Handling 
	COMMIT TRAN
	RETURN 
	
	err: 
	ROLLBACK TRAN
END
GO