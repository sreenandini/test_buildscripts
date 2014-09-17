/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/15/2014 6:13:23 PM
 ************************************************************/

USE Enterprise
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 11:48:48 AM
 ************************************************************/

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'esp_InsertMailServerInfo'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE esp_InsertMailServerInfo
END
GO

/*  
* Revision History  
*   
* Anuradha   Created    29 May 2014  
*   
* save the mail server information
*/  
--exec esp_InsertMailServerInfo 'localhost','false','','',0

CREATE PROCEDURE esp_InsertMailServerInfo
	@ServerName VARCHAR(50),
	@EnableSSL BIT,
	@UID VARCHAR(50),
	@PWD VARCHAR(50),
	@Port INT,
	@UserName VARCHAR(50),
	@UserId INT,
	@Oldvalue VARCHAR(500),
	@NewValue VARCHAR(500)
AS
BEGIN
	SET NOCOUNT ON
	
	BEGIN TRAN
	DELETE 
	FROM   MailServerInfo
	
	INSERT INTO MailServerInfo
	  (
	    MSI_ServerName,
	    MSI_Port,
	    MSI_EnableSSL,
	    MSI_UID,
	    MSI_PWD
	  )
	VALUES
	  (
	    @ServerName,
	    @Port,
	    @EnableSSL,
	    @UID,
	    @PWD
	  )
	  
	  
SELECT   Site_code ,Site_id,site_name into #tempSites from SITE 
   
   INSERT INTO Export_History
   (
   
   	EH_Reference1,
   	EH_Type,
  	EH_Site_Code
   )
  SELECT 
   0,
   'MailServerInfo',
   Site_Code
  FROM #tempSites
	
	EXEC usp_InsertAuditData @User_ID =@UserId,
	@User_Name=@UserName,
	@Module_ID= 35,
	@Module_Name='MailServerInfo',
	@Screen_Name='Service Calls',
	@Slot='',
	@Aud_Field='MailServer',
	@Old_Value=@Oldvalue,
	@New_Value=@NewValue,
	@Aud_Desc='Mail Server details inserted successfully',
	@Operation_Type='Add'
	
	IF @@ERROR = 0
	    COMMIT TRAN
	ELSE
	    ROLLBACK TRAN
END