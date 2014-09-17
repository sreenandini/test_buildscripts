USE Enterprise
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_Vault_GetDeviceDetailsForExport'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_Vault_GetDeviceDetailsForExport
END
GO

CREATE PROCEDURE dbo.rsp_Vault_GetDeviceDetailsForExport
	@Vault_ID INT
AS
	/*****************************************************************************************************    
DESCRIPTION : PROC Description      
CREATED DATE: PROC CreateDate    
MODULE  : PROC used in Modules     
CHANGE HISTORY :    
------------------------------------------------------------------------------------------------------    
AUTHOR     DESCRIPTON          MODIFIED DATE    
------------------------------------------------------------------------------------------------------    

*****************************************************************************************************/    

BEGIN
	DECLARE @xml VARCHAR(MAX)  
	DECLARE @xmlNGA XML     
	DECLARE @NGADevice_ID INT     
	
	SELECT @NGADevice_ID = NGADevice_ID
	FROM   tVault_Devices WITH(NOLOCK)
	WHERE  Vault_ID = @Vault_ID    
	
	SELECT @xmlNGA = (
	           SELECT td.NGADevice_ID,
	                  td.Name,
	                  td.Device_Type,
	                  td.[Description],
	                  Installation_No,
	                  Site_ID,
	                  Create_Date,
	                  Assigned_To_Site,
	                  [Start_Date],
	                  End_Date,
	                  Start_User,
	                  End_User
	           FROM   tNGADevices td WITH(NOLOCK)
	                  INNER JOIN tNGAInstallations WITH(NOLOCK)
	                       ON  td.NGADevice_ID = tNGAInstallations.NGADevice_ID
	                       AND tNGAInstallations.End_Date IS NULL 
	           WHERE  td.NGADevice_ID = ISNULL(@NGADevice_ID, td.NGADevice_ID) 
	                  FOR XML PATH('Installations_Info'),ELEMENTS 
	                  ,ROOT('NGADevice')
	       )
	
	DECLARE @cassetteXMl XML    
	SET @cassetteXMl = (
	        SELECT Cassette_ID,
	               Vault_ID,
	               Cassette_Name,
	               [Type],
	               Denom,
	               IsActive,
	               AlertLevel,
	               StandardFillAmount,
	               --MinFillAmount,
	               MaxFillAmount,
	               --MinBleedAmount,
	               --MaxBleedAmount,      
	               [Description],
	               Created_Date,
	               Modified_Date
	        FROM   tVault_Cassettes WITH(NOLOCK)
	        WHERE  Vault_ID = @Vault_ID 
	               FOR XML PATH('Cassette_Info') 
	               ,ELEMENTS 
	               ,ROOT('Cassettes')
	    )
	
	SELECT @xml = (
	           SELECT Vault_ID,
	                  [Name],
	                  Serial_NO,
	                  [Active],
	                  Site_ID,
	                  Alert_Level,
	                  Created_Date,
	                  End_Date,
	                  M.Manufacturer_Name,
	                  M.Manufacturer_ID,
	                  Type_Prefix,
	                  Capacity,
	                  IsWebServiceEnabled,
	                  NoofCoinHopper,
	                  NoofCassettes,
	                  ISNULl(AutoAdjustEnabled,0) AutoAdjustEnabled, 
					  ISNULL(FillRejection,0)  FillRejection,
	                  @xmlNGA,
	                  @cassetteXMl,
	                  ISNULL(StandardFillAmount,1000.00) StandardFillAmount
	           FROM   tVault_Devices td WITH(NOLOCK)
	                  INNER JOIN Manufacturer M
	                       ON  M.Manufacturer_ID = td.Manufacturer_ID
	           WHERE  Vault_id = @Vault_ID 
	                  FOR XML RAW , ROOT('Vault')
	       )
	
	SELECT @xml
END
GO