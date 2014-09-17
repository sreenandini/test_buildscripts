USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetCommonData'
   )
    DROP PROCEDURE dbo.rsp_GetCommonData
GO

CREATE PROCEDURE dbo.rsp_GetCommonData
	@SiteCode VARCHAR(20),
	@DataType VARCHAR(50),
	@XMLDATA VARCHAR(MAX) =null
AS
	/*****************************************************************************************************
DESCRIPTION	: For getting any data from exchange
CREATED DATE: 26-Jun-2013
MODULE		: Enterprise Web Service.	
CHANGE HISTORY : 
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SET NOCOUNT ON 
	DECLARE @Site_ID INT
	--For data correction after 11.4 to 12.4 Migration . LCI/RANK
	IF @DataType = 'MANUFACTURER'
	BEGIN
	    SELECT Manufacturer_ID,
	           Manufacturer_Name--,
	           --Manufacturer_Service_Contact,
	           --Manufacturer_Service_EMail,
	           --Manufacturer_Service_Tel,
	           --Manufacturer_Service_Address,
	           --Manufacturer_Service_Postcode,
	           --Manufacturer_Sales_Contact,
	           --Manufacturer_Sales_EMail,
	           --Manufacturer_Sales_Tel,
	           --Manufacturer_Sales_Address,
	           --Manufacturer_Sales_Postcode,
	           --Manufacturer_Code,
	           --Manufacturer_Coins_In_Meter_Used,
	           --Manufacturer_Coins_Out_Meter_Used,
	           --Manufacturer_Coin_Drop_Meter_Used,
	           --Manufacturer_Handpay_Meter_Used,
	           --Manufacturer_External_Credits_Meter_Used,
	           --Manufacturer_Games_Bet_Meter_Used,
	           --Manufacturer_Games_Won_Meter_Used,
	           --Manufacturer_Notes_Meter_Used,
	           --Manufacturer_Single_Coin_Build,
	           --Manufacturer_Handpay_Added_To_Coin_Out
	    FROM   dbo.MANUFACTURER  WITH(NOLOCK)  FOR XML PATH('MF'), ROOT('MFS')  ,TYPE, ELEMENTS XSINIL
	    RETURN   
	END
	
	--For data correction after 11.4 to 12.4 Migration . LCI/RANK	
	IF @DataType = 'SITEUSERDETAILS'
	BEGIN

		
		SELECT @Site_ID = Site_Id FROM [Site] WHERE Site_Code = @SiteCode
	    
	    --To export Super User Role links
	    INSERT INTO EXPORT_HISTORY
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           SecurityRoleID,
	           'ROLEACCESSLINK',
	           @SiteCode
	    FROM   [Role]
	    WHERE  RoleName = 'Super User'
	    
	    -- To export user details to site which is mapped to it
	    IF EXISTS (SELECT 1 FROM UserSite_lnk WHERE SiteID = @Site_ID)
	    BEGIN
			INSERT INTO EXPORT_HISTORY
			  (
				EH_Date,
				EH_Reference1,
				EH_Type,
				EH_Site_Code
			  )
			SELECT GETDATE(),
				   SecurityUserID,
				   'USERROLE',
				   @SiteCode
			FROM   UserSite_lnk
			WHERE  SiteID = @Site_ID
		    
			INSERT INTO EXPORT_HISTORY
			  (
				EH_Date,
				EH_Reference1,
				EH_Type,
				EH_Site_Code
			  )
			SELECT GETDATE(),
				   SecurityUserID,
				   'ADDUSER',
				   @SiteCode
			FROM   UserSite_lnk
			WHERE  SiteID = @Site_ID
	    END
	END
	
	IF @DataType = 'HQINSTALLATION'
	BEGIN
		DECLARE @Machine_No  INT
		DECLARE @Installation_No INT
		DECLARE @Stock_No    VARCHAR(50)
		DECLARE @InstallationDate DATETIME		
		DECLARE @Installation_StartDate VARCHAR(30)
		DECLARE @Installation_StartTime  VARCHAR(50)
		DECLARE @data        VARCHAR(MAX)
		DECLARE @idoc        INT
		
		SET @data = CAST(@XMLDATA AS VARCHAR(MAX)) 
		
		SET @data = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @data 
		
		/*--Create an internal representation of the XML document. */        
		EXEC sp_xml_preparedocument @idoc OUTPUT,
		     @data
		
		--<XmlData><Type>HQINSTALLATION</Type><Data>lc0002</Data></XmlData>
		SELECT @Stock_No = Stock_No,
		       @InstallationDate = InstallationDate,
		       @Installation_No = Installation_No
		FROM   OPENXML(@idoc, './XmlData', 2) WITH 
		       (
		           Stock_No VARCHAR(50) './Data',
		           InstallationDate DATETIME './InstallationDate',
		           Installation_No INT './Installation_No'
		       ) [Xml]
		
		SELECT @Installation_StartDate = CONVERT(VARCHAR(20), @InstallationDate, 106),
		       @Installation_StartTime = CONVERT(VARCHAR(20), @InstallationDate, 108)

		SELECT @Stock_No AS 'Stock_No',
		       Installation_ID AS 'HQ_Installation_No',
		       @Installation_No AS Installation_No
		FROM   Installation
		WHERE  Machine_ID = (
		           SELECT Machine_ID
		           FROM   MACHINE
		           WHERE  Machine_Stock_No = @Stock_No
		                  AND ISNULL(Machine_End_Date, '') = ''
		       )
		       AND Installation_Start_Date = @Installation_StartDate
		       AND Installation_Start_Time = @Installation_StartTime
		           FOR XML PATH(''), ELEMENTS, ROOT('Installation')
		
		RETURN
	END
	
	IF @DataType = 'VAULTDEVICE'
	BEGIN
	    SELECT @Site_ID = Site_Id
	    FROM   [Site] WHERE site_code= @SiteCode
	    
	    SELECT CAST(
	               (SELECT tn.Installation_No,
	                      tn.NGADevice_ID
	               FROM   tngainstallations tn
	                      INNER JOIN tVault_Devices td
	                           ON  tn.NGADevice_ID = td.NGADevice_ID
	               WHERE  td.Site_ID = @Site_ID
	                      AND tn.end_date IS NULL
	                          FOR XML RAW('INSTALLATION'),ROOT('NGA')) AS VARCHAR(MAX)
	    )
	   RETURN   
	END 
	IF @dataType='MIGRATIONCOMPLETE'
	BEGIN
		SELECT @Site_ID = Site_Id FROM [Site] WHERE Site_Code = @SiteCode
			 --UPDATE Exchangeversion
		DECLARE @VersionNo VARCHAR(100)
		SET @data = @XMLDATA
		
		    
		  UPDATE ExchangeVersionHistory 
		  SET
			VersionHistory= ISNULL(VersionHistory,CurrentVersion) + '->' + @data, 
			CurrentVersion=@data,
			LastUpdated=GETDATE(),
			PendingUpdate=0
		  WHERE Site_Id=@Site_ID 	

		IF @Site_ID IS NOT NULL 
			EXEC usp_Export_History @Site_ID,'SITESETTINGS',@Site_ID
		
	END 	
	
	 IF @dataType = 'ROUTEMIGRATION114'
	 BEGIN
	     DECLARE @Routedata VARCHAR(MAX)  
	     
	     SELECT @Site_ID = Site_Id
	     FROM   [Site]
	     WHERE  Site_Code = @SiteCode  
	     
	     SET @Routedata = CAST(@XMLDATA AS VARCHAR(MAX))   
	     
	     SET @data = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @Routedata   
	     
	     EXEC usp_Route_UpdateRouteFromXML @Site_ID,@data
	     
	     RETURN
	 END
	
	--ALWAYS SELECT SOME DATA to return back to Exchange 
	SELECT ''
END
GO

-- =============================================
-- Example to execute the stored procedure
-- =============================================

GO




