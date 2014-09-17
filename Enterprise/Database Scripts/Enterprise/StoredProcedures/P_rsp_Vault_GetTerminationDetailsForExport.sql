USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'rsp_Vault_GetTerminationDetailsForExport'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetTerminationDetailsForExport
GO
CREATE PROCEDURE dbo.rsp_Vault_GetTerminationDetailsForExport
	@NGADevice_ID INT
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

	DECLARE @TerminateOut VARCHAR(MAX)
	DECLARE @Vault_EndDeviceOnTerminate  VARCHAR(10)
	
	SELECT @Vault_EndDeviceOnTerminate = setting_value
	FROM   setting WITH(NOLOCK)
	WHERE  Setting_Name = 'Vault_EndDeviceOnTerminate'
	
	SET @Vault_EndDeviceOnTerminate = ISNULL(@Vault_EndDeviceOnTerminate, 'True')
	
	SELECT @TerminateOut =
	(
	SELECT TOP 1 tn.NGADevice_ID,
	       tn.End_Date,
	       tn.End_User,
	       td.[Description],
	       tn.Installation_No,
	       @Vault_EndDeviceOnTerminate Vault_EndDeviceOnTerminate
	FROM   tNGAInstallations tn WITH(NOLOCK)
	       INNER JOIN tNGAdevices td WITH(NOLOCK)
	            ON  tn.NGADevice_ID = td.NGADevice_ID
	WHERE  tn.NGADevice_ID = @NGADevice_ID
		   AND tn.End_Date is not null  	
	ORDER BY tn.Installation_No DESC
	       FOR XML RAW('Device'),ROOT('Vault')
	)
	
	SELECT @TerminateOut
	
END
GO

GO

