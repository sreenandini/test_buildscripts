SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportSiteForSiteSetupFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportSiteForSiteSetupFromXML]
GO
/*
 *Purpose: To import the site details from the enterprise database to the exchange 
 * Change History: 
 *  Vineetha Mathew		26-05-2008		Created
*/

CREATE PROCEDURE [dbo].[usp_ImportSiteForSiteSetupFromXML]
	@doc	VARCHAR(MAX),
	@IsSuccess	INT	OUTPUT
AS

BEGIN

DECLARE @idoc	INT
DECLARE @iOperator_ID	INT
DECLARE @iDepot_ID	INT
DECLARE @vcSite_Code	VARCHAR(50)
DECLARE @vcDepot_Name	VARCHAR(50)
DECLARE @vcOperator_Name	VARCHAR(50)
DECLARE @iSite_ID	INT
DECLARE @vcZone_Name	VARCHAR(50)
DECLARE @iCount	INT
DECLARE @iZone_No	INT

SET @IsSuccess = 0

SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc

	DECLARE @PositionTable	TABLE
	(
		Sno	INT IDENTITY(1,1),
		Bar_Position_ID	INT,
		Bar_Pos_Name	VARCHAR(100),
		Location	VARCHAR(500),
		BP_Start_Date	VARCHAR(30),
		BP_End_Date	VARCHAR(30),
		Collection_Day	VARCHAR(30),
		Depot_Name	VARCHAR(50),
		Zone_Name	VARCHAR(50),
		Zone_Description	VARCHAR(100),
		Zone_Start_Date	VARCHAR(30),
		Zone_End_Date	VARCHAR(30),
		Zone_No		INT
	)

		EXEC sp_xml_preparedocument @idoc OUTPUT, @doc


		INSERT INTO dbo.Operator (Name, Address, PostCode, Depot_Phone, Fax, Email, Contact)
		SELECT Name, Address, PostCode, Depot_Phone, Fax, Email, Contact
				FROM OPENXML(@idoc, './SITESETUP/SITE/OPERATORS/OPERATOR', 2)	WITH
					(
						Name	VARCHAR(50)	'./Operator_Name', 
						Address	VARCHAR(500)	'./Operator_Address',
						PostCode	VARCHAR(50)	'./Operator_PostCode',
						Depot_Phone	VARCHAR(50)	'./Operator_Depot_Phone',
						Fax	VARCHAR(50)	'./Operator_Fax',
						Email	VARCHAR(50)	'./Operator_EMail',
						Contact	VARCHAR(50)	'./Operator_Contact'
					)
		WHERE Name NOT IN (SELECT Name FROM dbo.Operator)

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -1		-- failed while inserting a record into the Operator table
			GOTO Err
		END

		UPDATE O
			SET O.Address = A.Address,
				O.PostCode = A.PostCode,
				O.Depot_Phone = A.Depot_Phone,
				O.Fax = A.Fax,
				O.Email = A.Email,
				O.Contact = A.Contact
		FROM  dbo.Operator O	
	INNER JOIN OPENXML(@idoc, './SITESETUP/SITE/OPERATORS/OPERATOR', 2)	WITH
					(
						Name	VARCHAR(50)	'./Operator_Name', 
						Address	VARCHAR(500)	'./Operator_Address',
						PostCode	VARCHAR(50)	'./Operator_PostCode',
						Depot_Phone	VARCHAR(50)	'./Operator_Depot_Phone',
						Fax	VARCHAR(50)	'./Operator_Fax',
						Email	VARCHAR(50)	'./Operator_EMail',
						Contact	VARCHAR(50)	'./Operator_Contact'
					) AS A ON O.Name = A.Name

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -11		-- failed while updating a record into the Operator table
			GOTO Err
		END


		INSERT INTO dbo.Depot(Depot_Name, Depot_Address, Depot_Postcode, Depot_Contact_Name, Depot_AMEDIS_Depot_Code, Depot_Reference, Depot_Service, Depot_Financial_Code)
		SELECT * FROM OPENXML(@idoc, './SITESETUP/SITE/DEPOTS/DEPOT', 2) WITH
			(
				Depot_Name	VARCHAR(50)	'./Depot_Name',
				Depot_Address	VARCHAR(500)	'./Depot_Address',
				Depot_Postcode	VARCHAR(10)	'./Depot_Postcode',
				Depot_Contact_Name	VARCHAR(50)	'./Depot_Contact_Name',
				Depot_AMEDIS_Depot_Code	VARCHAR(4)	'./Depot_AMEDIS_Depot_Code',
				Depot_Reference	VARCHAR(20)	'./Depot_Reference',
				Depot_Service	BIT	'./Depot_Service',
				Depot_Financial_Code	VARCHAR(20)	'./Depot_Financial_Code'
			)
		WHERE Depot_Name NOT IN (SELECT Depot_Name FROM dbo.Depot)

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -2		-- failed while inserting a record into the Depot table
			GOTO Err
		END		

	UPDATE D
		SET D.Depot_Address = A.Depot_Address,
			D.Depot_Postcode = A.Depot_Postcode,
			D.Depot_Contact_Name = A.Depot_Contact_Name,
			D.Depot_AMEDIS_Depot_Code = A.Depot_AMEDIS_Depot_Code,
			D.Depot_Reference = A.Depot_Reference,
			D.Depot_Service = A.Depot_Service,
			D.Depot_Financial_Code = A.Depot_Financial_Code
		FROM dbo.Depot D INNER JOIN OPENXML(@idoc, './SITESETUP/SITE/DEPOTS/DEPOT', 2) WITH
			(
				Depot_Name	VARCHAR(50)	'./Depot_Name',
				Depot_Address	VARCHAR(500)	'./Depot_Address',
				Depot_Postcode	VARCHAR(10)	'./Depot_Postcode',
				Depot_Contact_Name	VARCHAR(50)	'./Depot_Contact_Name',
				Depot_AMEDIS_Depot_Code	VARCHAR(4)	'./Depot_AMEDIS_Depot_Code',
				Depot_Reference	VARCHAR(20)	'./Depot_Reference',
				Depot_Service	BIT	'./Depot_Service',
				Depot_Financial_Code	VARCHAR(20)	'./Depot_Financial_Code'
			) A ON D.Depot_Name = A.Depot_Name


	IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -22		-- failed while updating the records in the Depot table
			GOTO Err
		END	
	
		UPDATE D
			SET D.Supplier_ID = O.Operator_No
		FROM dbo.Depot D 
	INNER JOIN OPENXML(@idoc, './SITESETUP/SITE/DEPOTS/DEPOT', 2) WITH (Depot_Name	VARCHAR(50)	'./Depot_Name', Operator_Name VARCHAR(50) './Operator_Name') A ON D.Depot_Name = A.Depot_Name
	INNER JOIN dbo.Operator O ON O.Name = A.Operator_Name

	IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -222		-- failed while updating the operator_id into the Depot table
			GOTO Err
		END		

--		SELECT @iDepot_ID = Depot_ID FROM dbo.Depot WHERE Depot_Name = @vcDepot_Name


		IF NOT EXISTS(SELECT * FROM dbo.Site)
		BEGIN
			INSERT INTO dbo.Site
							(
								Code,
								Name,
								Phone,
								Fax,
								EMail,
								Manager,
								Open_Mon,
								Open_Tue,
								Open_Wed,
								Open_Thu,
								Open_Fri,
								Open_Sat,
								Open_Sun,
								Site_Address_1,
								Site_Address_2,
								Site_Address_3,
								Site_Address_4,
								Site_Address_5,
								Site_PostCode,
								Site_Supplier_Code,
								Region,
								SiteEvent
							)

			SELECT * FROM OPENXML(@idoc, './SITESETUP/SITE', 2)	WITH
					(
								Code	VARCHAR(50)	'./Site_Code',
								Name	VARCHAR(50)	'./Site_Name',
								Phone	VARCHAR(50)	'./Site_Phone_No',
								Fax	VARCHAR(50)	'./Site_Fax_No',
								EMail	VARCHAR(50)	'./Site_Email_Address',
								Manager	VARCHAR(50)	'./Site_Manager',
							    Open_Mon VARCHAR(96) './Standard_Opening_Hours_Open_Monday',    
								Open_Tue VARCHAR(96) './Standard_Opening_Hours_Open_Tuesday',    
								Open_Wed VARCHAR(96) './Standard_Opening_Hours_Open_Wednesday',    
								Open_Thu VARCHAR(96) './Standard_Opening_Hours_Open_Thursday',    
								Open_Fri VARCHAR(96) './Standard_Opening_Hours_Open_Friday',    
								Open_Sat VARCHAR(96) './Standard_Opening_Hours_Open_Saturday',    
								Open_Sun VARCHAR(96) './Standard_Opening_Hours_Open_Sunday',
								Site_Address_1	VARCHAR(50)	'./Site_Address_1',
								Site_Address_2	VARCHAR(50)	'./Site_Address_2',
								Site_Address_3	VARCHAR(50)	'./Site_Address_3',
								Site_Address_4	VARCHAR(50)	'./Site_Address_4',
								Site_Address_5	VARCHAR(50)	'./Site_Address_5',
								Site_PostCode	VARCHAR(50)	'./Site_Postcode',
								Site_Supplier_Code	VARCHAR(50)	'./Site_Supplier_Code',
								Region	VARCHAR(10)	'./Region',
								TransactionFlagName VARCHAR(200)	'./TransactionFlagName'
					) 
		END

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -3		-- failed while inserting a record into the Site table
			GOTO Err
		END

		UPDATE S
			SET S.Name = A.Name,
				S.Phone = A.Phone,
				S.Fax = A.Fax,
				S.EMail = A.EMail,
				S.Manager = A.Manager,
				S.Open_Mon = A.Open_Mon,
				S.Open_Tue = A.Open_Tue,
				S.Open_Wed = A.Open_Wed,
				S.Open_Thu = A.Open_Thu,
				S.Open_Fri = A.Open_Fri,
				S.Open_Sat = A.Open_Sat,
				S.Open_Sun = A.Open_Sun,
				S.Site_Address_1 = A.Site_Address_1,
				S.Site_Address_2 = A.Site_Address_2,
				S.Site_Address_3 = A.Site_Address_3,
				S.Site_Address_4 = A.Site_Address_4,
				S.Site_Address_5 = A.Site_Address_5,
				S.Site_PostCode = A.Site_PostCode,
				S.Site_Supplier_Code = A.Site_Supplier_Code,
				S.Region = A.Region,
				S.SiteEvent=A.TransactionFlagName
		FROM	dbo.Site S
	INNER JOIN	OPENXML(@idoc, './SITESETUP/SITE', 2)	WITH
					(
								Code	VARCHAR(50)	'./Site_Code',
								Name	VARCHAR(50)	'./Site_Name',
								Phone	VARCHAR(50)	'./Site_Phone_No',
								Fax	VARCHAR(50)	'./Site_Fax_No',
								EMail	VARCHAR(50)	'./Site_Email_Address',
								Manager	VARCHAR(50)	'./Site_Manager',
								Open_Mon VARCHAR(96) './Standard_Opening_Hours_Open_Monday',    
								Open_Tue VARCHAR(96) './Standard_Opening_Hours_Open_Tuesday',    
								Open_Wed VARCHAR(96) './Standard_Opening_Hours_Open_Wednesday',    
								Open_Thu VARCHAR(96) './Standard_Opening_Hours_Open_Thursday',    
								Open_Fri VARCHAR(96) './Standard_Opening_Hours_Open_Friday',    
								Open_Sat VARCHAR(96) './Standard_Opening_Hours_Open_Saturday',    
								Open_Sun VARCHAR(96) './Standard_Opening_Hours_Open_Sunday',  
								Site_Address_1	VARCHAR(50)	'./Site_Address_1',
								Site_Address_2	VARCHAR(50)	'./Site_Address_2',
								Site_Address_3	VARCHAR(50)	'./Site_Address_3',
								Site_Address_4	VARCHAR(50)	'./Site_Address_4',
								Site_Address_5	VARCHAR(50)	'./Site_Address_5',
								Site_PostCode	VARCHAR(50)	'./Site_Postcode',
								Site_Supplier_Code	VARCHAR(50)	'./Site_Supplier_Code',
								Region	VARCHAR(10)	'./Region',
								TransactionFlagName  VARCHAR(200)	'./TransactionFlagName'
					) A ON S.Code = A.Code

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -33		-- failed while updating a record into the Site table
			GOTO Err
		END

		SELECT @iSite_ID = Site_No, @vcSite_Code = Code FROM dbo.Site

		UPDATE S SET S.Depot_ID = D.Depot_ID FROM OPENXML(@idoc, 'SITESETUP/SITE', 2) WITH (Depot_Name	VARCHAR(50)	'./Depot_Name', Code	VARCHAR(50)	'./Site_Code') AS A
			LEFT JOIN dbo.Depot D ON D.Depot_Name = A.Depot_Name
			INNER JOIN dbo.Site S ON S.Code = A.Code

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -4
			GOTO Err
		END
/*
		INSERT INTO @PositionTable (Bar_Position_ID, Bar_Pos_Name, Location, BP_Start_Date, BP_End_Date, Collection_Day, Depot_Name, Zone_Name, Zone_Description, Zone_Start_Date, Zone_End_Date)
		SELECT * FROM OPENXML(@idoc, './SITESETUP/SITE/POSITION', 2)	WITH
				(
						Bar_Position_ID	INT	'./Bar_Position_ID',
						Bar_Pos_Name	VARCHAR(100)	'./Bar_Position_Name',
						Location	VARCHAR(500)	'./Bar_Position_Location',
						BP_Start_Date	VARCHAR(30)	'./Bar_Position_Start_Date',
						BP_End_Date	VARCHAR(30)	'./Bar_Position_End_Date',
						Collection_Day	VARCHAR(30)	'./Bar_Position_Collection_Day',
						Depot_Name	VARCHAR(50)	'./Depot_Name',
						Zone_Name	VARCHAR(50)	'./ZONE/Zone_Name',
						Zone_Description	VARCHAR(100)	'./ZONE/Zone_Description',
						Zone_Start_Date	VARCHAR(30)	'./ZONE/Zone_Start_Date',
						Zone_End_Date	VARCHAR(30)	'./ZONE/Zone_End_Date'
				)
	
		EXEC sp_xml_removedocument	@idoc

		SELECT @iCount = COUNT(*) FROM @PositionTable

		INSERT INTO dbo.Zone(Zone_Name, Zone_Description, Zone_Start_Date, Zone_End_Date)
		SELECT DISTINCT Zone_Name, 
				Zone_Description, 
				Zone_Start_Date, 
				Zone_End_Date
		FROM @PositionTable WHERE ISNULL(Zone_Name, '') <> '' AND Zone_Name NOT IN (SELECT Zone_Name FROM dbo.Zone)

		WHILE @iCount > 0
		BEGIN
			SELECT @vcZone_Name	 = Zone_Name FROM @PositionTable WHERE Sno = @iCount
			
			SELECT @iZone_No = Zone_No FROM dbo.Zone WHERE Zone_Name = @vcZone_Name

			UPDATE @PositionTable
				SET Zone_No = @iZone_No
			WHERE Sno = @iCount

			SET @iCount = @iCount - 1

		END

		INSERT INTO dbo.Bar_Position
				(Site_No,
				Depot_ID,
				HQ_Bar_Position_ID, 
				Bar_Pos_Name, 
				Zone_No, 
				Location, 
				Start_Date, 
				End_Date, 
				Collection_Day)

		SELECT @iSite_ID,
				D.Depot_ID,
				PT.Bar_Position_ID, 
				PT.Bar_Pos_Name, 
				PT.Zone_No, 
				PT.Location, 
				PT.BP_Start_Date, 
				CASE WHEN ISNULL(PT.BP_End_Date, '') = '' THEN NULL ELSE PT.BP_End_Date END, 
				PT.Collection_Day
		FROM @PositionTable	PT 
   LEFT JOIN dbo.Depot D ON PT.Depot_Name = D.Depot_Name --WHERE PT.Bar_Pos_Name NOT IN (SELECT Bar_Pos_Name FROM dbo.Bar_Position)
   LEFT JOIN dbo.Bar_Position B ON PT.Bar_Pos_Name = B.Bar_Pos_Name
	   WHERE ISNULL(B.Bar_Pos_Name, '') = ''

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -5		-- failed while inserting a record into the bar_position table
			GOTO Err
		END

		UPDATE BP
			SET BP.Depot_ID = D.Depot_ID,
				BP.HQ_Bar_Position_ID = PT.Bar_Position_ID,
				BP.Zone_No = PT.Zone_No,
				BP.Location = PT.Location,
				BP.Start_Date = PT.BP_Start_Date,
				BP.End_Date = CASE WHEN ISNULL(PT.BP_End_Date, '') = '' THEN NULL ELSE PT.BP_End_Date END,
				BP.Collection_Day = PT.Collection_Day
		FROM	dbo.Bar_Position BP INNER JOIN @PositionTable PT ON BP.Bar_Pos_Name = PT.Bar_Pos_Name
	LEFT JOIN	dbo.Depot D ON PT.Depot_Name = D.Depot_Name

		IF @@Error <> 0
		BEGIN
			SET @IsSuccess = -55		-- failed while inserting a record into the bar_position table
			GOTO Err
		END
    */
	/*	update the Setting table also based on the region	*/
		IF EXISTS(SELECT * FROM dbo.Setting WHERE Setting_Name = 'Region')
		BEGIN
			UPDATE dbo.Setting
				SET Setting_Value = (SELECT ISNULL(Region, 'UK') FROM dbo.Site)
			WHERE Setting_Name = 'REGION'
		END
		ELSE
		BEGIN
			INSERT INTO dbo.Setting
			SELECT 'Region', Region FROM dbo.Site
		END

		INSERT INTO dbo.Export_History
		SELECT GETDATE(), @vcSite_Code, 'CONNECTIONUPDATE', NULL, NULL


	RETURN 0

Err:
	RETURN -99
END