USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'usp_Vault_TerminateDevice'
   )
    DROP PROCEDURE dbo.usp_Vault_TerminateDevice
GO
CREATE PROCEDURE dbo.usp_Vault_TerminateDevice
	@Vault_ID INT,
	@User_ID INT,
	@Module_ID INT,
	@Module_Name VARCHAR(150),
	@Screen_Name VARCHAR(150),
	@Description VARCHAR(150)
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: Exchange Export Service	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	DECLARE @TerminationDate DATETIME 
	DECLARE @NGADevice_ID INT
	DECLARE @Site_ID INT 
	DECLARE @Vault_Name VARCHAR(100)
	
	DECLARE @Vault_EndDeviceOnTerminate  VARCHAR(10)
	
	SELECT @Vault_EndDeviceOnTerminate = setting_value
	FROM   setting WITH(NOLOCK)
	WHERE  Setting_Name = 'Vault_EndDeviceOnTerminate'
	
	SET @Vault_EndDeviceOnTerminate = ISNULL(@Vault_EndDeviceOnTerminate, 'True')
	
	SET @TerminationDate = GETDATE()
	BEGIN TRAN 
	--Remove device 
	UPDATE tVault_Devices
	SET    [Active] = 0,
	       [site_id] = 0,
	       End_Date = CASE 
	                       WHEN @Vault_EndDeviceOnTerminate = 'True' THEN @TerminationDate
	                       ELSE NULL
	                  END,
	       @Vault_Name = NAME,
	       @NGADevice_ID = NGADevice_ID
	WHERE  Vault_ID = @Vault_ID
	IF @@ERROR<>0 
	 GOTO ERR
	
	--Remove NGA Installation
	UPDATE tngainstallations
	SET    -- NGADevice_ID = ? -- this column value is auto-generated
	       End_Date = @TerminationDate,
	       End_User = @User_ID,
	       @Site_ID = Site_ID
	WHERE  NGADevice_ID = @NGADevice_ID
	AND End_Date IS NULL
	
	IF @@ERROR<>0 
	 GOTO ERR
	 
	UPDATE tNGADevices
	SET
		-- NGADevice_ID = ? -- this column value is auto-generated
		[Description] = ISNUll([Description],'') + '  [Termination Reason]: ' + @Description
	WHERE NGADevice_ID	=@NGADevice_ID
		
	 
	--Update Exchange 
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
	       @NGADevice_ID,
	       'TERMINATEVAULT',
	       NULL,
	       NULL,
	       Site_Code
	FROM   SITE WITH (NOLOCK)
	WHERE  site_id = @Site_ID      
	IF @@ERROR<>0 
	 GOTO ERR
	
	--AUDITING CHANGES
	DECLARE @staff_id INT    
	DECLARE @staff_Name VARCHAR(100)     
	SET @staff_id = @User_ID -- CHANGED FROM USER ID TO STAFFID FROM CLIENT   
	
	SELECT @staff_Name = ISNULL(Staff_First_Name, '')
	FROM   Staff
	WHERE  Staff_ID = @User_ID
	
	DECLARE @Audit_DESC VARCHAR(MAX)
	
	SET @Audit_DESC = 'Terminate Vault NAME= ' + @Vault_Name +
	    ' ,Reason=' + @Description
	
	EXEC [usp_InsertAuditData] 
	     @staff_id,
	     @staff_Name,
	     @Module_ID,
	     @Module_Name,
	     @Screen_Name,
	     '',
	     '',
	     '',
	     'TRUE',
	     @Audit_DESC,
		 'MODIFY'
    IF @@ERROR<>0 
	 GOTO ERR
 	 
  COMMIT TRAN 
  RETURN 0  
 
 ERR:
	ROLLBACK TRAN 		 
	RETURN -1 		 
END
GO

GO

