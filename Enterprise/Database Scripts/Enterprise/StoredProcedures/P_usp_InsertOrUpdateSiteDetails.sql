USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_InsertOrUpdateSiteDetails]    
(  
	@Message900Doc  Varchar(max)  
)  
AS
BEGIN  
    
	DECLARE @handle INT       
	DECLARE @SiteID INT  
	DECLARE @DepotID INT  
	DECLARE @SITE_CODE INT      
	DECLARE @AAMSCode Varchar(12)      
	DECLARE @OperationType Varchar(2)      
	DECLARE @VenueType int
	DECLARE @COMPANYID INT     
	DECLARE @SupplierID int
	DECLARE @SUBCOMPANYID INT 
	DECLARE @UserID Varchar(50)
	DECLARE @UserName Varchar(50)
	DECLARE @Date DateTime
	DECLARE @SiteName Varchar(50)
	DECLARE @DepotName Varchar(50)
	DECLARE @Description Varchar(MAX)
    
	EXEC sp_xml_preparedocument @handle OUTPUT, @Message900Doc       
    
	CREATE TABLE #Temp      
	(        
		OperationType				VARCHAR(1),      
		RegionCode					VARCHAR(2),         
		VenueCode					int,      
		LocationId					VARCHAR(12),      
		Company						VARCHAR(1),       
		VenueTaxCode				VARCHAR(16),       
		VenueType					int,           
		VenueTradeName				VARCHAR(50),       
		Toponynm					int,           
		Address						VARCHAR(40) ,      
		StreetNumber				VARCHAR(7) ,      
		PostCode					VARCHAR(5) ,      
		County						VARCHAR(2) ,      
		Muncipality					VARCHAR(40) ,      
		CadastralCode				VARCHAR(4) ,      
		PhoneNumber					VARCHAR(17),       
		VenueSurface				int ,          
		VLTMaxNumber				int ,        
		GamingSystemConnectionType	int,           
		ConnectionIpAddress			VARCHAR(15) ,      
		Status						int ,      
		ActivationDate				DateTime,      
		DeletionDate				DateTime,      
		LastUpdateDate				DateTime
	)        
    
	SELECT       
		OperationType,          
		RegionCode,          
		VenueCode,          
		LocationId,          
		Company,         
		VenueTaxCode,          
		VenueType,          
		VenueTradeName,         
		Toponynm,         
		Address,   
		StreetNumber,          
		PostCode,          
		County,          
		Muncipality,        
		CadastralCode,         
		PhoneNumber ,        
		VenueSurface,             
		VLTMaxNumber,         
		GamingSystemConnectionType,      
		ConnectionIpAddress,      
		Status,         
		ActivationDate,       
		DeletionDate,       
		LastUpdateDate
	INTO #AD        
	FROM       
		OPENXML (@handle,'./Message900',2)          
	WITH       
		#temp      
   
	EXEC sp_xml_removedocument @handle        
    
	SELECT @AAMSCode = LocationID, @OperationType = OperationType, @SITE_CODE = VenueCode, @VenueType=VenueType FROM #AD        
  
	IF (@VenueType=9) --Warehouse
	BEGIN
		SET @OperationType = @OperationType + 'D'
	END
	ELSE
	BEGIN
		SET @OperationType = @OperationType + 'S'
	END
   
	IF(@OperationType='IS')      
	BEGIN     
		IF EXISTS (SELECT 1 FROM Site S INNER JOIN #AD T ON S.SITE_CODE=CAST(T.VenueCode  AS VARCHAR(10)))
		BEGIN
		  --Print 'Site already inserted'
		  RETURN -1
		END
		IF ((SELECT COUNT(*) FROM COMPANY)  = 0)    
		BEGIN    
			INSERT INTO [Enterprise].[dbo].[Company]    
			([Staff_ID]    
			,[Terms_Group_ID]    
			,[Access_Key_ID]    
			,[Company_Model_Set_ID]    
			,[Company_Name]    
			,[Company_Address]    
			,[Company_Postcode]    
			,[Company_Switchboard_Phone_No]    
			,[Company_Contact_Name]    
			,[Company_Contact_Phone_No]    
			,[Company_Contact_Email_Address]    
			,[Company_Invoice_Address]    
			,[Company_Invoice_Postcode]    
			,[Company_Invoice_Name]    
			,[Company_Price_Per_Play]    
			,[Company_Jackpot]    
			,[Company_Percentage_Payout]    
			,[Company_Start_Date]    
			,[Company_End_Date]    
			,[Company_Memo]    
			,[Company_AMEDIS_Code]    
			,[Company_AMEDIS_Operational_Code]    
			,[Company_ANA_Number]    
			,[Company_AutoCreate_Position]    
			,[Company_Logo_Reference]    
			,[Company_Trade_Type]    
			,[Company_Address_1]    
			,[Company_Address_2]    
			,[Company_Address_3]    
			,[Company_Address_4]    
			,[Company_Address_5]    
			,[Company_TX_Collection]    
			,[Company_TX_Movement]    
			,[Company_TX_EDC]    
			,[Company_TX_Format]    
			,[Company_RX_Collection]    
			,[Company_RX_Movement]    
			,[Company_RX_EDC]    
			,[Company_RX_Format]    
			,[Company_AMEDIS_Variants])    
			VALUES (    
			0      ,0      ,0      ,0      ,'SISAL'      ,null      ,''      ,''    
			,''      ,''      ,''      ,''      ,''      ,''      ,null      ,null    
			,null      ,null      ,null      ,null      ,null      ,null      ,null    
			,null      ,null      ,null      ,null      ,null      ,null      ,null    
			,null      ,null      ,null      ,null      ,0      ,null      ,null    
			,null      ,0      ,null )    
		END     
	   
		SELECT  TOP 1 @COMPANYID= Company_ID FROM COmpany    
    
		IF ((SELECT COUNT(*) FROM SUB_COMPANY WHERE Company_ID = @COMPANYID) = 0)    
		BEGIN    
			INSERT INTO [Enterprise].[dbo].[Sub_Company]    
			([Staff_ID]    
			,[Staff_ID_Default]    
			,[Company_ID]    
			,[Access_Key_ID]    
			,[Access_Key_ID_Default]    
			,[Terms_Group_ID]    
			,[Terms_Group_ID_Default]    
			,[Calendar_ID]    
			,[Company_Model_Set_ID]    
			,[Sub_Company_Name]    
			,[Sub_Company_Address]    
			,[Sub_Company_Postcode]    
			,[Sub_Company_Switchboard_Phone_No]    
			,[Sub_Company_Contact_Name]    
			,[Sub_Company_Contact_Phone_No]    
			,[Sub_Company_Contact_Email_Address]    
			,[Sub_Company_Invoice_Address]    
			,[Sub_Company_Invoice_Postcode]    
			,[Sub_Company_Invoice_Name]    
			,[Sub_Company_Price_Per_Play]    
			,[Sub_Company_Price_Per_Play_Default]    
			,[Sub_Company_Jackpot]    
			,[Sub_Company_Jackpot_Default]    
			,[Sub_Company_Percentage_Payout]    
			,[Sub_Company_Percentage_Payout_Default]    
			,[Sub_Company_Start_Date]    
			,[Sub_Company_End_Date]    
			,[Sage_Account_Ref]    
			,[Sub_Company_Memo]    
			,[Sub_Company_ANA_Number]    
			,[SLA]    
			,[Sub_Company_Logo_Reference]    
			,[Sub_Company_Trade_Type]    
			,[Sub_Company_Use_Split_Rents]    
			,[Sub_Company_Address_1]    
			,[Sub_Company_Address_2]    
			,[Sub_Company_Address_3]    
			,[Sub_Company_Address_4]    
			,[Sub_Company_Address_5]    
			,[Sub_Company_AMEDIS_Code]    
			,[Sub_Company_AMEDIS_Operational_Code]    
			,[Sub_Company_Validate_Terms]    
			,[Sub_Company_Validate_Terms_Variance]    
			,[Sub_Company_Suppress_Docket_Print]    
			,[Sub_Company_Post_Print_Dockets]    
			,[Sub_Company_Docket_Type]    
			,[Sub_Company_TX_Collection]    
			,[Sub_Company_TX_Collection_Use_Default]    
			,[Sub_Company_TX_Movement]    
			,[Sub_Company_TX_Movement_Use_Default]    
			,[Sub_Company_TX_EDC]    
			,[Sub_Company_TX_EDC_Use_Detault]    
			,[Sub_Company_TX_Format]    
			,[Sub_Company_TX_Format_Use_Default]    
			,[Sub_Company_RX_Collection]    
			,[Sub_Company_RX_Collection_Use_Default]    
			,[Sub_Company_RX_Movement]    
			,[Sub_Company_RX_Movement_Use_Default]    
			,[Sub_Company_RX_EDC]    
			,[Sub_Company_RX_EDC_Use_Detault]    
			,[Sub_Company_RX_Format]    
			,[Sub_Company_RX_Format_Use_Default]    
			,[Sub_Company_Period_End_Use_Date_Of_Collection]    
			,[Sub_Company_Income_Ledger_Code]    
			,[Sub_Company_Royalty_Ledger_Code]    
			,[Sub_Company_Default_Opening_Hours_ID]    
			,[Sub_Company_Account_Name]    
			,[Sub_Company_Sort_Code]    
			,[Sub_Company_Account_No]    
			,[Sub_Company_EDI_Outbox]    
			,[Sub_Company_Leisure_Data_Brewary_Code]    
			,[Sub_Company_Force_Leisure_Data_To_Enterprise])    
			VALUES (    
			0    
			,1    
			,@COMPANYID    
			,0    
			,1    
			,0    
			,1    
			,0    
			,0    
			,'SISAL SUBCOMPANY'    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,0    
			,1    
			,0    
			,1    
			,0    
			,1    
			,null    
			,null    
			,null    
			,null    
			,null    
			,0    
			,null    
			,null    
			,0    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,null    
			,0    
			,0    
			,0    
			,0    
			,0    
			,null    
			,1    
			,null    
			,1    
			,null    
			,1    
			,0    
			,1    
			,null    
			,1    
			,null    
			,1    
			,null    
			,1    
			,0    
			,1    
			,0    
			,null    
			,null    
			,0    
			,null    
			,null    
			,null    
			,null    
			,null    
			,0 )    
		END    
    
		SELECT @SUBCOMPANYID = Sub_Company_ID FROM SUB_COMPANY WHERE Company_ID = @COMPANYID    
    
		INSERT INTO Site      
		(      
			Site_Region_Code,      
			Site_Code,      
			Site_Fiscal_Code,      
			Site_Location_Type,      
			Site_Name,      
			Site_Toponym,      
			Site_Address_1,      
			Site_Street_Number,      
			Site_Postcode,      
			Site_Province,      
			Site_Municipality,      
			Site_Cadastral_Code,      
			Site_Phone_No,      
			Site_Area,      
			Site_MaxNumber_VLT,      
			Site_Connection_Type,      
			Site_Connection_IPAddress,      
			Site_AAMS_Status,      
			Site_Start_Date,    
			Site_Modified_Date,    
			Region,    
			Sub_Company_ID,
			Site_Setting_Profile_ID,
			Site_ZonaRice
			)      
			SELECT       
			RegionCode ,          
			CAST(VenueCode AS VARCHAR(10))  ,        
			-- CASE WHEN Company <> 'Y' THEN VenueTaxCode ELSE '' END,       
			VenueTaxCode,
			--If the Company field = 'N'  then the next field is the VAT Number, otherwise it will contain the venue owner's fiscal code      
			VenueType  ,         
			VenueTradeName ,         
			Toponynm  ,         
			Address   ,        
			StreetNumber ,          
			PostCode  ,          
			County   ,          
			Muncipality  ,        
			CadastralCode ,         
			PhoneNumber  ,        
			VenueSurface     ,             
			VLTMaxNumber ,         
			GamingSystemConnectionType ,      
			ConnectionIpAddress  ,      
			Status      ,         
			ActivationDate   ,    
			LastUpdateDate,    
			'it-IT'   ,    
			@SUBCOMPANYID,  
			1,
			Site_Region_Code + ' ' + Site_Code
		FROM       
			#AD        
    
		SET @SiteID = Scope_Identity()      
    
		INSERT INTO BMC_AAMS_Details       
		(BAD_Reference_ID, BAD_Asset_Serial_No, BAD_AAMS_Entity_Type, BAD_AAMS_Code, BAD_AAMS_Status, BAD_Verification_Status, BAD_Entity_Current_Status,       
		BAD_Entity_Floor_Controller_Status, BAD_Entity_Command, BAD_Is_Warehouse, BAD_Updated_Date, BAD_Comments, BAD_Game_Name)      
		VALUES       
		( @SiteID,  NULL, 2, @AAMSCode, 1, 1, NULL,0,NULL,0,NULL,NULL,NULL )      
	END      
	ELSE IF (@OperationType='MS')      
	BEGIN      
		IF NOT EXISTS (SELECT 1 FROM SITE S INNER JOIN #AD T ON S.SITE_CODE=CAST(T.VenueCode  AS VARCHAR(10)))
		BEGIN
            RETURN -1
		END

		IF EXISTS(SELECT 1 from SITE S INNER JOIN #AD T ON S.SITE_CODE=CAST(T.VenueCode  AS VARCHAR(10)) AND S.SITE_CLOSED=1)
		BEGIN
			RETURN -1
		END

		UPDATE S  SET 
			S.Site_Region_Code= T.RegionCode,      
			S.Site_Code=T.VenueCode,         
			S.Site_Fiscal_Code =  T.VenueTaxCode,      
			S.Site_Location_Type = T.VenueType,       
			S.Site_Name = T.VenueTradeName,      
			S.Site_Toponym = T.Toponynm,      
			S.Site_Address_1 = T.Address,      
			S.Site_Street_Number = T.StreetNumber,      
			S.Site_Postcode = T.PostCode,      
			S.Site_Province = T.County ,      
			S.Site_Municipality = T.Muncipality,      
			S.Site_Cadastral_Code = T.CadastralCode,      
			S.Site_Phone_No = T.PhoneNumber,      
			S.Site_Area = T.VenueSurface,      
			S.Site_MaxNumber_VLT = T.VLTMaxNumber,      
			S.Site_ConnType = T.GamingSystemConnectionType,      
			S.Site_Connection_IPAddress=T.ConnectionIpAddress,      
			S.Site_AAMS_Status = T.Status,      
			S.Site_Start_Date = T.ActivationDate,      
			S.Site_Modified_Date = T.LastUpdateDate , 
			S.Site_ZonaRice =T.RegionCode + ' ' + T.VenueCode
		FROM       
			Site S      
			INNER JOIN #AD T       
			ON S.Site_Code=T.VenueCode       
		SELECT @SiteID = Site_ID FROM Site S       
		INNER JOIN #AD T ON S.Site_Code=T.VenueCode       
	END 
	ELSE IF (@OperationType='DS')   --Site Closure   
	BEGIN
		SELECT @SiteID = S.Site_ID FROM Site S       
		INNER JOIN #AD T ON S.Site_Code=CAST(T.VenueCode  AS VARCHAR(10))   

		IF NOT EXISTS (SELECT 1 FROM SITE S INNER JOIN #AD T ON S.SITE_CODE=CAST(T.VenueCode  AS VARCHAR(10)))
		BEGIN
            RETURN -1
		END

		IF EXISTS(SELECT 1 from SITE S INNER JOIN #AD T ON S.SITE_CODE=CAST(T.VenueCode  AS VARCHAR(10)) AND S.SITE_CLOSED=1)
		BEGIN
			RETURN -1
		END

		DECLARE @XML XML
		DECLARE @SITE_STATUS XML
		DECLARE @Site_Closed_Date DateTime		

		SET @Site_Closed_Date = (SELECT T.DeletionDate FROM #AD T JOIN Site S ON S.SITE_CODE= CAST(T.VenueCode AS VARCHAR(10)))

		SET @XML  = (SELECT I.Installation_ID, I.Bar_Position_ID FROM Installation I 
		LEFT JOIN Bar_Position B on I.Bar_Position_Id = B.Bar_Position_Id
		INNER JOIN Site S on B.Site_Id = S.Site_Id      
		WHERE S.Site_ID = @SiteID AND I.Installation_End_Date IS NULL FOR XML  AUTO)

		SET @SITE_STATUS = (SELECT SITE_STATUS from site S WHERE S.Site_ID = @SiteID)
		SET @XML = CONVERT (VARCHAR(max), @SITE_STATUS) + CONVERT (VARCHAR(max), @XML)


		UPDATE Site 
		SET 
		Site_Closed = 1 , 
		Site_Closed_Date = @Site_Closed_Date, 
		Site_Status_Id = 1 , 
		Site_Inactive_Date = getdate(),
		Site_Termination_Status = CONVERT (VARCHAR(max), @SITE_STATUS) + CONVERT (VARCHAR(max), @XML)
		WHERE SITE_ID = @SiteID

		--START - UPDATE AUDITHISTORY 
		DECLARE @OpenVLTs Varchar(MAX)
		
		SET @UserID = (SELECT SecurityUserID FROM [USER] WHERE UPPER(UserName)='ADMIN')
		SET @UserName = (SELECT UserName FROM [USER] WHERE UPPER(UserName)='ADMIN')
		SET @Date = getdate()
		SET @SiteName = (Select Site_Name FROM site where Site_ID=@SiteID)
		SET @OpenVLTs = ' '
			  DECLARE tempCursor CURSOR FOR 
				   SELECT BAD_AAMS_Code FROM BMC_AAMS_Details join Machine on BAD_Reference_ID = Machine_ID where Machine_Status_Flag!=0 and Machine_ModelTypeID!=2
			  OPEN tempCursor
			  DECLARE @value As VARCHAR(30) 
			  FETCH NEXT FROM tempCursor INTO @value
			  WHILE @@FETCH_STATUS = 0
			  BEGIN
			  Set @OpenVLTs = @OpenVLTs +' '+@value
			  FETCH NEXT FROM tempCursor INTO @value
			  END
			  CLOSE tempCursor
			  DEALLOCATE tempCursor
		SET @Description = (SELECT 'Site:'+@SiteName+'Closed (Using Message900). [Site Closed]:0->1'+'Open VLTs are '+ @OpenVLTs )
		EXEC usp_CreateAuditHistory  
		@Date,
		@UserID,
		@UserName,
		509,
		'',
		'Site',
		@Description,
		'',
		'Site_Closed',
		'0',
		'1',
		'MODIFY'
		--END - UPDATE AUDITHISTORY 

		DECLARE @MachineID AS INT  

		SELECT @MachineID = ISNULL(NGA_Machine_ID,0) FROM Site WHERE Site_ID = @SiteID
		IF @MachineID <> 0   
		BEGIN  
			--Remove the NGA Asset for the site.  
			UPDATE Site  
			SET NGA_Machine_ID = NULL  
			WHERE  Site_ID = @SiteID  

			--Reset the status of the NGA Asset to In USE.  
			UPDATE Machine  
			SET Machine_Status_Flag = 0 -- In Stock  
			WHERE Machine_ID = @MachineID  

			UPDATE BMC_AAMS_Details  
			SET BAD_AAMS_Status = 0  
			WHERE BAD_Reference_ID = @MachineID  
			AND BAD_AAMS_Entity_Type = 3  
		END
		--exec usp_Export_History '<SiteID>','SITESETTINGS',<SiteID>
	END
	ELSE IF (@OperationType='ID')
	BEGIN
		--IF DEPOT ALREADY EXISTS THEN DONT DO ANYTHING - JUST EXIT
		IF EXISTS (SELECT 1 FROM DEPOT D INNER JOIN #AD T ON D.DEPOT_CODE=T.VenueCode)
		BEGIN
		  RETURN -1
		END
		
		--IF THERE IS NO DATA IN OPERATOR TABLE THEN CREATE A DEFAULT OPERATOR
		IF ((SELECT COUNT(*) FROM OPERATOR) = 0)    
		BEGIN    
			Print 'Came inside'
			INSERT INTO [Operator]
				([Calendar_ID]
				,[Operator_Name]
				,[Operator_Start_Date])
			VALUES
				(0
				,'SISAL'
				,CONVERT(VARCHAR(11), GETDATE(), 106))
		END
		--GET THE ID OF THE FIRST CREATED OPERATOR. OPERATOR IS MANDATORY TO CREATE A DEPOT
		--IF THERE IS NO OPERATOR(SUPPLIER_ID) THEN USING UI WE CAN NOT VIEW THE DEPOT
		SELECT  TOP 1 @SupplierID= OPERATOR_ID FROM OPERATOR


		INSERT INTO [Depot]
			([Depot_Name]
			,[Depot_Code]
			,[Depot_Address]
			,[Depot_Postcode]
			,[Depot_Contact_Name]
			,[Depot_AMEDIS_Depot_Code]
			,[Supplier_ID]
			,[Depot_Reference]
			,[Depot_Service]
			,[Depot_Financial_Code]
			,[Depot_Account_Name]
			,[Depot_Sort_Code]
			,[Depot_Account_No]
			,[Depot_Phone_Number]
			,[Depot_Street_Number]
			,[Depot_Province]
			,[Depot_Municipality]
			,[Depot_Cadastral_Code]
			,[Depot_Area]
			,[Depot_Location_Type]
			,[Depot_Toponym]
			,[Depot_Closed]
			,[Depot_ActivationDate]
			,[Depot_LastUpdateDate]
			,[Depot_Status])

		SELECT
			VenueTradeName
			,VenueCode
			,Address
			,PostCode
			,''
			,''
			,@SupplierID
			,''
			,0
			,VenueTaxCode
			,NULL
			,NULL
			,NULL
			,PhoneNumber
			,StreetNumber
			,County
			,Muncipality
			,CadastralCode
			,VenueSurface
			,0
			,Toponynm
			,0
			,ActivationDate	
			,LastUpdateDate
			,Status
		FROM
			#AD

		SET @DepotID = Scope_Identity()  

		--POPULATE THE BMC_AAMS_DETAILS - WHICH ACUTALLY STORES THE AAMS APPROVAL DETAILS
		--BAD_AAMS_STATUS=1 MEANS - ITS APPROVED BY AAMS. SISAL ASKS TO CREATE DEPOT/SITE ONCE ITS APPROVED BY AAMS
		--FOR DEPOT ENTITY_TYPE SHOULD BE 2/FOR 
		INSERT INTO BMC_AAMS_Details       
		(BAD_Reference_ID, BAD_Asset_Serial_No, BAD_AAMS_Entity_Type, BAD_AAMS_Code, BAD_AAMS_Status, BAD_Verification_Status, BAD_Entity_Current_Status,       
		BAD_Entity_Floor_Controller_Status, BAD_Entity_Command, BAD_Is_Warehouse, BAD_Updated_Date, BAD_Comments, BAD_Game_Name)      
		VALUES       
		(@DepotID,  NULL, 2, @AAMSCode, 1, 1, NULL,0,NULL,1,getdate(),'New Depot created using Message900',NULL )      
	END
	ELSE IF (@OperationType='MD')
	BEGIN
		--IF GIVEN DEPOT DOES NOT EXISTS THEN DO NOTHING - JUST EXIT
		IF NOT EXISTS (SELECT 1 FROM DEPOT D INNER JOIN #AD T ON D.DEPOT_CODE=T.VenueCode)
		BEGIN
            RETURN -1
		END

		--IF GIVEN DEPOT EXISTS BUT CLOSED THEN DO NOTHING - JUST EXIT
		IF EXISTS(SELECT 1 from DEPOT D INNER JOIN #AD T ON D.DEPOT_CODE=T.VenueCode AND D.DEPOT_CLOSED=1)
		BEGIN
			RETURN -1
		END

		UPDATE [Depot]
			SET 
				[Depot_Name]				= T.VenueTradeName
				,[Depot_Address]			= T.Address
				,[Depot_Postcode]			= T.PostCode
				,[Depot_Financial_Code]		= T.VenueTaxCode
				,[Depot_Phone_Number]		= T.PhoneNumber
				,[Depot_Street_Number]		= T.StreetNumber
				,[Depot_Province]			= T.County
				,[Depot_Municipality]		= T.Muncipality
				,[Depot_Cadastral_Code]		= T.CadastralCode
				,[Depot_Area]				= T.VenueSurface
				,[Depot_Toponym]			= T.Toponynm
				,[Depot_Code]				= T.VenueCode
				,[Depot_ActivationDate]		= T.ActivationDate	
				,[Depot_LastUpdateDate]		= T.LastUpdateDate					
				,[Depot_Status]				= T.Status
		FROM       
			DEPOT D      
			INNER JOIN #AD T       
			ON D.Depot_Code=T.VenueCode   
	END
	ELSE IF (@OperationType='DD')
	BEGIN
		--IF GIVEN DEPOT EXISTS THEN DO NOTHING - JUST EXIT
		IF NOT EXISTS (SELECT 1 FROM DEPOT D INNER JOIN #AD T ON D.DEPOT_CODE=T.VenueCode)
		BEGIN
            RETURN -1
		END

		--IF GIVEN DEPOT EXISTS BUT CLOSED THEN DO NOTHING - JUST EXIT
		IF EXISTS(SELECT 1 from DEPOT D INNER JOIN #AD T ON D.DEPOT_CODE=T.VenueCode AND D.DEPOT_CLOSED=1)
		BEGIN
			RETURN -1
		END
		
		DECLARE @DeleteionDate DateTime
		SELECT @DepotID = D.Depot_ID, @DeleteionDate=T.DeletionDate FROM Depot D       
			INNER JOIN #AD T ON D.Depot_Code=T.VenueCode
 
		UPDATE 
			[Depot] 
		SET 
			[Depot_Closed]		= 1,
			[Depot_DeletionDate]= @DeleteionDate
		WHERE Depot_ID=@DepotID

		SET @UserID = (SELECT SecurityUserID FROM [USER] WHERE UPPER(UserName)='ADMIN')
		SET @UserName = (SELECT UserName FROM [USER] WHERE UPPER(UserName)='ADMIN')
		SET @Date = @DeleteionDate
		SET @DepotName = (SELECT Depot_Name FROM Depot WHERE Depot_ID=@DepotID)
		SET @Description = (SELECT 'Depot:'+ @DepotName +' Closed Using Message900')

		--POPULATE AUDIT HISTORY TABLE
		EXEC usp_CreateAuditHistory  
			@Date,
			@UserID,
			@UserName,
			509,
			'',
			'Depot',
			@Description,
			'',
			'Depot_Closed',
			'0',
			'1',
			'MODIFY'

	END
	ELSE
	BEGIN
		RETURN -1 
	END
	DROP TABLE #Temp      
	RETURN	1
END

GO

