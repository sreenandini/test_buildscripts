USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CreateBarPositionTemplate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CreateBarPositionTemplate]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_CreateBarPositionTemplate
	@BarPositionTemplateID INT,
	@StartBarPositionName VARCHAR(50),
	@LastBarPositionName VARCHAR(50)
	--@BulkCopyStatus INT OUTPUT
AS
BEGIN
	
	DECLARE @BarPositionName      VARCHAR(50)
	DECLARE @BarPositionCount     INT
	DECLARE @LatestTempID         INT
	DECLARE @Site_ID              INT
	DECLARE @iBarPosition		  INT
	
	SET @StartBarPositionName = RIGHT('000' + CAST(@StartBarPositionName AS VARCHAR(3)),3)
	SET @LastBarPositionName = RIGHT('000' + CAST(@LastBarPositionName AS VARCHAR(3)),3)
	
	EXEC rsp_GetBarPositionByIDForBulkCopy @BarPositionTemplateID
	
	SELECT TOP 1 @Site_ID = site_ID FROM ##TempBarPositionBulkCopy
	
	
	CREATE TABLE #BarPosTemp
	(
		[Temp_ID]                                                [int] IDENTITY(1, 1) NOT NULL,
		[Site_ID]                                                [int] NULL,
		[Zone_ID]                                                [int] NULL DEFAULT((0)),
		[Access_Key_ID]                                          [int] NULL DEFAULT((0)),
		[Access_Key_ID_Default]                                  [bit] NULL,
		[Terms_Group_ID]                                         [int] NULL DEFAULT((0)),
		[Terms_Group_Changeover_Date]                            [varchar](30) NULL,
		[Terms_Group_Future_ID]                                  [int] NULL DEFAULT((0)),
		[Terms_Group_Past_Changeover_Date]                       [varchar](30) NULL,
		[Terms_Group_Past_ID]                                    [int] NULL DEFAULT((0)),
		[Terms_Group_ID_Default]                                 [bit] NULL,
		[Duty_ID]                                                [int] NULL DEFAULT((0)),
		[Depot_ID]                                               [int] NULL DEFAULT((0)),
		[Machine_Type_ID]                                        [int] NULL DEFAULT((0)),
		[Bar_Position_Name]                                      [varchar](50) NULL,
		[Bar_Position_Location]                                  [varchar](50) NULL,
		[Bar_Position_Start_Date]                                [varchar](30) NULL,
		[Bar_Position_End_Date]                                  [varchar](30) NULL,
		[Bar_Position_Collection_Day]                            [varchar](30) NULL,
		[Bar_Position_Price_Per_Play]                            [varchar](50) NULL,
		[Bar_Position_Price_Per_Play_Default]                    [bit] NULL,
		[Bar_Position_Jackpot]                                   [varchar](50) NULL,
		[Bar_Position_Jackpot_Default]                           [bit] NULL,
		[Bar_Position_Percentage_Payout]                         [varchar](50) NULL,
		[Bar_Position_Percentage_Payout_Default]                 [bit] NULL,
		[Bar_Position_Last_Collection_Date]                      [varchar](30) NULL,
		[Bar_Position_Collection_Rent_Paid_Until]                [varchar](30) NULL DEFAULT('22/10/1999'),
		[Bar_Position_Collection_Period]                         [int] NULL DEFAULT((7)),
		[Bar_Position_Supplier_AMEDIS_Code]                      [varchar](4) NULL,
		[Bar_Position_Supplier_Depot_AMEDIS_Code]                [varchar](4) NULL,
		[Bar_Position_Supplier_Site_Code]                        [varchar](8) NULL,
		[Bar_Position_Supplier_Position_Code]                    [varchar](6) NULL,
		[Bar_Position_Supplier_Area]                             [varchar](50) NULL,
		[Bar_Position_Supplier_Service_Area]                     [varchar](50) NULL,
		[Bar_Position_Company_Position_Code]                     [varchar](6) NULL,
		[Bar_Position_Company_Target]                            [real] NULL DEFAULT((0)),
		[Bar_Position_Collection_Frequency]                      [int] NULL DEFAULT((0)),
		[Bar_Position_Image_Reference]                           [varchar](50) NULL,
		[Bar_Position_Machine_Type_AMEDIS_Code]                  [int] NULL DEFAULT((0)),
		[Bar_Position_Rent]                                      [real] NULL DEFAULT((0)),
		[Bar_Position_Rent_Previous]                             [real] NULL DEFAULT((0)),
		[Bar_Position_Rent_Future]                               [real] NULL DEFAULT((0)),
		[Bar_Position_Rent_Past_Date]                            [varchar](30) NULL,
		[Bar_Position_Rent_Future_Date]                          [varchar](30) NULL,
		[Bar_Position_Supplier_Share]                            [real] NULL DEFAULT((0)),
		[Bar_Position_Site_Share]                                [real] NULL DEFAULT((0)),
		Bar_Position_Owners_Share								 [real] NULL DEFAULT((0)),
		[Bar_Position_Secondary_Owners_Share]                    [real] NULL DEFAULT((0)),
		[Bar_Position_Supplier_Share_Previous]                   [real] NULL DEFAULT((0)),
		[Bar_Position_Site_Share_Previous]                       [real] NULL DEFAULT((0)),
		[Bar_Position_Owners_Share_Previous]                     [real] NULL DEFAULT((0)),
		[Bar_Position_Secondary_Owners_Share_Previous]           [real] NULL DEFAULT((0)),
		[Bar_Position_Supplier_Share_Future]                     [real] NULL DEFAULT((0)),
		[Bar_Position_Site_Share_Future]                         [real] NULL DEFAULT((0)),
		[Bar_Position_Owners_Share_Future]                       [real] NULL DEFAULT((0)),
		[Bar_Position_Secondary_Owners_Share_Future]             [real] NULL DEFAULT((0)),
		[Bar_Position_Share_Past_Date]                           [varchar](30) NULL,
		[Bar_Position_Share_Future_Date]                         [varchar](30) NULL,
		[Bar_Position_Licence_Charge]                            [real] NULL DEFAULT((0)),
		[Bar_Position_Licence_Previous]                          [real] NULL DEFAULT((0)),
		[Bar_Position_Licence_Future]                            [real] NULL DEFAULT((0)),
		[Bar_Position_Licence_Past_Date]                         [varchar](30) NULL,
		[Bar_Position_Licence_Future_Date]                       [varchar](30) NULL,
		[Bar_Position_Use_Terms]                                 [bit] NULL DEFAULT((0)),
		[Bar_Position_TX_Collection]                             [bit] NULL,
		[Bar_Position_TX_Collection_Use_Default]                 [bit] NULL DEFAULT((1)),
		[Bar_Position_TX_Movement]                               [bit] NULL,
		[Bar_Position_TX_Movement_Use_Default]                   [bit] NULL DEFAULT((1)),
		[Bar_Position_TX_EDC]                                    [bit] NULL,
		[Bar_Position_TX_EDC_Use_Detault]                        [bit] NULL DEFAULT((1)),
		[Bar_Position_TX_Format]                                 [int] NULL DEFAULT((0)),
		[Bar_Position_TX_Format_Use_Default]                     [bit] NULL DEFAULT((1)),
		[Bar_Position_RX_Collection]                             [bit] NULL,
		[Bar_Position_RX_Collection_Use_Default]                 [bit] NULL DEFAULT((1)),
		[Bar_Position_RX_Movement]                               [bit] NULL,
		[Bar_Position_RX_Movement_Use_Default]                   [bit] NULL DEFAULT((1)),
		[Bar_Position_RX_EDC]                                    [bit] NULL,
		[Bar_Position_RX_EDC_Use_Detault]                        [bit] NULL DEFAULT((1)),
		[Bar_Position_RX_Format]                                 [int] NULL DEFAULT((0)),
		[Bar_Position_RX_Format_Use_Default]                     [bit] NULL DEFAULT((1)),
		[Bar_Position_Net_Target]                                [real] NULL DEFAULT((0)),
		[Bar_Position_Below_Net_Target_Counter]                  [int] NULL DEFAULT((0)),
		[Bar_Position_Below_Company_Target_Counter]              [int] NULL DEFAULT((0)),
		[Bar_Position_Security_Required]                         [bit] NULL DEFAULT((0)),
		[Bar_Position_Site_Has_Cashbox_Keys]                     [bit] NULL DEFAULT((0)),
		[Bar_Position_Site_Has_FreePlay_Access]                  [bit] NULL,
		[Bar_Position_Override_Rent]                             [bit] NULL DEFAULT((0)),
		[Bar_Position_Override_Shares]                           [bit] NULL DEFAULT((0)),
		[Bar_Position_Override_Licence]                          [bit] NULL DEFAULT((0)),
		[Bar_Position_Category]                                  [int] NULL DEFAULT((0)),
		[Bar_Position_PPL_Charge]                                [real] NULL DEFAULT((0)),
		[Bar_Position_PPL_Previous]                              [real] NULL DEFAULT((0)),
		[Bar_Position_PPL_Future]                                [real] NULL DEFAULT((0)),
		[Bar_Position_PPL_Past_Date]                             [varchar](30) NULL,
		[Bar_Position_PPL_Future_Date]                           [varchar](30) NULL,
		[Bar_Position_Float_Issued]                              [int] NULL DEFAULT((0)),
		[Bar_Position_Float_Recovered]                           [int] NULL DEFAULT((0)),
		[Bar_Position_Use_Site_Share_For_Secondary_Brewery]      [bit] NULL DEFAULT((0)),
		[Bar_Position_Prize_LOS]                                 [bit] NULL DEFAULT((0)),
		[Bar_Position_Rent_Schedule_ID]                          [int] NULL DEFAULT((0)),
		[Bar_Position_Share_Schedule_ID]                         [int] NULL DEFAULT((0)),
		[Bar_Position_Override_Rent_Schedule]                    [bit] NULL DEFAULT((0)),
		[Bar_Position_Override_Share_Schedule]                   [bit] NULL DEFAULT((0)),
		[Bar_Position_Last_Collection_ID]                        [int] NULL DEFAULT((0)),
		[Bar_Position_Override_Rent_From_Schedule_To_Rent]       [bit] NULL DEFAULT((0)),
		[Bar_Position_Override_Rent_From_Rent_To_Schedule]       [bit] NULL DEFAULT((0)),
		[Bar_Position_Override_Rent_From_Schedule_To_Rent_Date]  [varchar](30) 
		NULL,
		[Bar_Position_Override_Rent_From_Rent_To_Schedule_Date]  [varchar](30) 
		NULL,
		[Bar_Position_Rent_Schedule_ID_From]                     [int] NULL DEFAULT((0)),
		[Bar_Position_Disable_EDI_Export]                        [bit] NULL DEFAULT((0)),
		[Bar_Position_Invoice_Period]                            [int] NOT NULL DEFAULT((0)),
		[Bar_Position_Machine_Enabled]                           [int] NULL,
		[Bar_Position_Note_Acceptor_Enabled]                     [int] NULL,
		[Bar_Position_Machine_Enabled_Date]                      [datetime] NULL,
		[Bar_Position_IsEnable]								     [bit] NULL DEFAULT((0))	
	)
	
	SET @BarPositionName = @StartBarPositionName
	SET @iBarPosition = CAST(@BarPositionName AS NUMERIC(3))
	
	WHILE @iBarPosition <= CAST(@LastBarPositionName AS NUMERIC(3))
	BEGIN
	    SELECT @BarPositionCount = COUNT(*)
	    FROM   Bar_Position bp
	    WHERE  CAST(bp.Bar_Position_Name AS NUMERIC(3)) = cast(@BarPositionName AS NUMERIC(3)) AND Site_ID = @Site_ID  
	    
	    IF @BarPositionCount = 0
	    BEGIN
	        INSERT INTO #BarPosTemp
	          (
	            Site_ID,
	            Zone_ID,
	            Access_Key_ID,
	            Access_Key_ID_Default,
	            Terms_Group_ID,
	            Terms_Group_Changeover_Date,
	            Terms_Group_Future_ID,
	            Terms_Group_Past_Changeover_Date,
	            Terms_Group_Past_ID,
	            Terms_Group_ID_Default,
	            Duty_ID,
	            Depot_ID,
	            Machine_Type_ID,
	            Bar_Position_Name,
	            Bar_Position_Location,
	            Bar_Position_Start_Date,
	            Bar_Position_End_Date,
	            Bar_Position_Collection_Day,
	            Bar_Position_Price_Per_Play,
	            Bar_Position_Price_Per_Play_Default,
	            Bar_Position_Jackpot,
	            Bar_Position_Jackpot_Default,
	            Bar_Position_Percentage_Payout,
	            Bar_Position_Percentage_Payout_Default,
	            Bar_Position_Last_Collection_Date,
	            Bar_Position_Collection_Rent_Paid_Until,
	            Bar_Position_Collection_Period,
	            Bar_Position_Supplier_AMEDIS_Code,
	            Bar_Position_Supplier_Depot_AMEDIS_Code,
	            Bar_Position_Supplier_Site_Code,
	            Bar_Position_Supplier_Position_Code,
	            Bar_Position_Supplier_Area,
	            Bar_Position_Supplier_Service_Area,
	            Bar_Position_Company_Position_Code,
	            Bar_Position_Company_Target,
	            Bar_Position_Collection_Frequency,
	            Bar_Position_Image_Reference,
	            Bar_Position_Machine_Type_AMEDIS_Code,
	            Bar_Position_Rent,
	            Bar_Position_Rent_Previous,
	            Bar_Position_Rent_Future,
	            Bar_Position_Rent_Past_Date,
	            Bar_Position_Rent_Future_Date,
	            Bar_Position_Supplier_Share,
	            Bar_Position_Site_Share,
	            Bar_Position_Owners_Share,
	            Bar_Position_Secondary_Owners_Share,
	            Bar_Position_Supplier_Share_Previous,
	            Bar_Position_Site_Share_Previous,
	            Bar_Position_Owners_Share_Previous,
	            Bar_Position_Secondary_Owners_Share_Previous,
	            Bar_Position_Supplier_Share_Future,
	            Bar_Position_Site_Share_Future,
	            Bar_Position_Owners_Share_Future,
	            Bar_Position_Secondary_Owners_Share_Future,
	            Bar_Position_Share_Past_Date,
	            Bar_Position_Share_Future_Date,
	            Bar_Position_Licence_Charge,
	            Bar_Position_Licence_Previous,
	            Bar_Position_Licence_Future,
	            Bar_Position_Licence_Past_Date,
	            Bar_Position_Licence_Future_Date,
	            Bar_Position_Use_Terms,
	            Bar_Position_TX_Collection,
	            Bar_Position_TX_Collection_Use_Default,
	            Bar_Position_TX_Movement,
	            Bar_Position_TX_Movement_Use_Default,
	            Bar_Position_TX_EDC,
	            Bar_Position_TX_EDC_Use_Detault,
	            Bar_Position_TX_Format,
	            Bar_Position_TX_Format_Use_Default,
	            Bar_Position_RX_Collection,
	            Bar_Position_RX_Collection_Use_Default,
	            Bar_Position_RX_Movement,
	            Bar_Position_RX_Movement_Use_Default,
	            Bar_Position_RX_EDC,
	            Bar_Position_RX_EDC_Use_Detault,
	            Bar_Position_RX_Format,
	            Bar_Position_RX_Format_Use_Default,
	            Bar_Position_Net_Target,
	            Bar_Position_Below_Net_Target_Counter,
	            Bar_Position_Below_Company_Target_Counter,
	            Bar_Position_Security_Required,
	            Bar_Position_Site_Has_Cashbox_Keys,
	            Bar_Position_Site_Has_FreePlay_Access,
	            Bar_Position_Override_Rent,
	            Bar_Position_Override_Shares,
	            Bar_Position_Override_Licence,
	            Bar_Position_Category,
	            Bar_Position_PPL_Charge,
	            Bar_Position_PPL_Previous,
	            Bar_Position_PPL_Future,
	            Bar_Position_PPL_Past_Date,
	            Bar_Position_PPL_Future_Date,
	            Bar_Position_Float_Issued,
	            Bar_Position_Float_Recovered,
	            Bar_Position_Use_Site_Share_For_Secondary_Brewery,
	            Bar_Position_Prize_LOS,
	            Bar_Position_Rent_Schedule_ID,
	            Bar_Position_Share_Schedule_ID,
	            Bar_Position_Override_Rent_Schedule,
	            Bar_Position_Override_Share_Schedule,
	            Bar_Position_Last_Collection_ID,
	            Bar_Position_Override_Rent_From_Schedule_To_Rent,
	            Bar_Position_Override_Rent_From_Rent_To_Schedule,
	            Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
	            Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
	            Bar_Position_Rent_Schedule_ID_From,
	            Bar_Position_Disable_EDI_Export,
	            Bar_Position_Invoice_Period,
	            Bar_Position_Machine_Enabled,
	            Bar_Position_Note_Acceptor_Enabled,
	            Bar_Position_Machine_Enabled_Date,
	            Bar_Position_IsEnable
	          )
	        SELECT *
	        FROM   ##TempBarPositionBulkCopy
	        
	        SET @LatestTempID = CAST(SCOPE_IDENTITY() AS INT)
	        
	        
	        UPDATE #BarPosTemp
	        SET    Bar_Position_Name = @BarPositionName
	        WHERE  Temp_ID = @LatestTempID
	        
	        SET @iBarPosition = CAST(@BarPositionName AS NUMERIC(3)) + 1
	        
	        IF @iBarPosition <= 999
	        BEGIN
	        	SET @BarPositionName = RIGHT('000' + CAST(@iBarPosition AS VARCHAR(3)),3)
	        END	        
	    END
	    ELSE
	    BEGIN
	    	SET @iBarPosition = CAST(@BarPositionName AS NUMERIC(3)) + 1
	        
	        IF @iBarPosition <= 999
	        BEGIN
	        	SET @BarPositionName = RIGHT('000' + CAST(@iBarPosition AS VARCHAR(3)),3)
	        END
	    END
	END

	INSERT INTO Bar_Position
	  (
	    Site_ID,
	    Zone_ID,
	    Access_Key_ID,
	    Access_Key_ID_Default,
	    Terms_Group_ID,
	    Terms_Group_Changeover_Date,
	    Terms_Group_Future_ID,
	    Terms_Group_Past_Changeover_Date,
	    Terms_Group_Past_ID,
	    Terms_Group_ID_Default,
	    Duty_ID,
	    Depot_ID,
	    Machine_Type_ID,
	    Bar_Position_Name,
	    Bar_Position_Location,
	    Bar_Position_Start_Date,
	    Bar_Position_End_Date,
	    Bar_Position_Collection_Day,
	    Bar_Position_Price_Per_Play,
	    Bar_Position_Price_Per_Play_Default,
	    Bar_Position_Jackpot,
	    Bar_Position_Jackpot_Default,
	    Bar_Position_Percentage_Payout,
	    Bar_Position_Percentage_Payout_Default,
	    Bar_Position_Last_Collection_Date,
	    Bar_Position_Collection_Rent_Paid_Until,
	    Bar_Position_Collection_Period,
	    Bar_Position_Supplier_AMEDIS_Code,
	    Bar_Position_Supplier_Depot_AMEDIS_Code,
	    Bar_Position_Supplier_Site_Code,
	    Bar_Position_Supplier_Position_Code,
	    Bar_Position_Supplier_Area,
	    Bar_Position_Supplier_Service_Area,
	    Bar_Position_Company_Position_Code,
	    Bar_Position_Company_Target,
	    Bar_Position_Collection_Frequency,
	    Bar_Position_Image_Reference,
	    Bar_Position_Machine_Type_AMEDIS_Code,
	    Bar_Position_Rent,
	    Bar_Position_Rent_Previous,
	    Bar_Position_Rent_Future,
	    Bar_Position_Rent_Past_Date,
	    Bar_Position_Rent_Future_Date,
	    Bar_Position_Supplier_Share,
	    Bar_Position_Site_Share,
	    Bar_Position_Owners_Share,
	    Bar_Position_Secondary_Owners_Share,
	    Bar_Position_Supplier_Share_Previous,
	    Bar_Position_Site_Share_Previous,
	    Bar_Position_Owners_Share_Previous,
	    Bar_Position_Secondary_Owners_Share_Previous,
	    Bar_Position_Supplier_Share_Future,
	    Bar_Position_Site_Share_Future,
	    Bar_Position_Owners_Share_Future,
	    Bar_Position_Secondary_Owners_Share_Future,
	    Bar_Position_Share_Past_Date,
	    Bar_Position_Share_Future_Date,
	    Bar_Position_Licence_Charge,
	    Bar_Position_Licence_Previous,
	    Bar_Position_Licence_Future,
	    Bar_Position_Licence_Past_Date,
	    Bar_Position_Licence_Future_Date,
	    Bar_Position_Use_Terms,
	    Bar_Position_TX_Collection,
	    Bar_Position_TX_Collection_Use_Default,
	    Bar_Position_TX_Movement,
	    Bar_Position_TX_Movement_Use_Default,
	    Bar_Position_TX_EDC,
	    Bar_Position_TX_EDC_Use_Detault,
	    Bar_Position_TX_Format,
	    Bar_Position_TX_Format_Use_Default,
	    Bar_Position_RX_Collection,
	    Bar_Position_RX_Collection_Use_Default,
	    Bar_Position_RX_Movement,
	    Bar_Position_RX_Movement_Use_Default,
	    Bar_Position_RX_EDC,
	    Bar_Position_RX_EDC_Use_Detault,
	    Bar_Position_RX_Format,
	    Bar_Position_RX_Format_Use_Default,
	    Bar_Position_Net_Target,
	    Bar_Position_Below_Net_Target_Counter,
	    Bar_Position_Below_Company_Target_Counter,
	    Bar_Position_Security_Required,
	    Bar_Position_Site_Has_Cashbox_Keys,
	    Bar_Position_Site_Has_FreePlay_Access,
	    Bar_Position_Override_Rent,
	    Bar_Position_Override_Shares,
	    Bar_Position_Override_Licence,
	    Bar_Position_Category,
	    Bar_Position_PPL_Charge,
	    Bar_Position_PPL_Previous,
	    Bar_Position_PPL_Future,
	    Bar_Position_PPL_Past_Date,
	    Bar_Position_PPL_Future_Date,
	    Bar_Position_Float_Issued,
	    Bar_Position_Float_Recovered,
	    Bar_Position_Use_Site_Share_For_Secondary_Brewery,
	    Bar_Position_Prize_LOS,
	    Bar_Position_Rent_Schedule_ID,
	    Bar_Position_Share_Schedule_ID,
	    Bar_Position_Override_Rent_Schedule,
	    Bar_Position_Override_Share_Schedule,
	    Bar_Position_Last_Collection_ID,
	    Bar_Position_Override_Rent_From_Schedule_To_Rent,
	    Bar_Position_Override_Rent_From_Rent_To_Schedule,
	    Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
	    Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
	    Bar_Position_Rent_Schedule_ID_From,
	    Bar_Position_Disable_EDI_Export,
	    Bar_Position_Invoice_Period,
	    Bar_Position_Machine_Enabled,
	    Bar_Position_Note_Acceptor_Enabled,
	    Bar_Position_Machine_Enabled_Date,
	    Bar_Position_IsEnable  
	  )
	SELECT 
		 Site_ID,
	    Zone_ID,
	    Access_Key_ID,
	    Access_Key_ID_Default,
	    Terms_Group_ID,
	    Terms_Group_Changeover_Date,
	    Terms_Group_Future_ID,
	    Terms_Group_Past_Changeover_Date,
	    Terms_Group_Past_ID,
	    Terms_Group_ID_Default,
	    Duty_ID,
	    Depot_ID,
	    Machine_Type_ID,
	    Bar_Position_Name,
	    Bar_Position_Location,
	    Bar_Position_Start_Date,
	    Bar_Position_End_Date,
	    Bar_Position_Collection_Day,
	    Bar_Position_Price_Per_Play,
	    Bar_Position_Price_Per_Play_Default,
	    Bar_Position_Jackpot,
	    Bar_Position_Jackpot_Default,
	    Bar_Position_Percentage_Payout,
	    Bar_Position_Percentage_Payout_Default,
	    Bar_Position_Last_Collection_Date,
	    Bar_Position_Collection_Rent_Paid_Until,
	    Bar_Position_Collection_Period,
	    Bar_Position_Supplier_AMEDIS_Code,
	    Bar_Position_Supplier_Depot_AMEDIS_Code,
	    Bar_Position_Supplier_Site_Code,
	    Bar_Position_Supplier_Position_Code,
	    Bar_Position_Supplier_Area,
	    Bar_Position_Supplier_Service_Area,
	    Bar_Position_Company_Position_Code,
	    Bar_Position_Company_Target,
	    Bar_Position_Collection_Frequency,
	    Bar_Position_Image_Reference,
	    Bar_Position_Machine_Type_AMEDIS_Code,
	    Bar_Position_Rent,
	    Bar_Position_Rent_Previous,
	    Bar_Position_Rent_Future,
	    Bar_Position_Rent_Past_Date,
	    Bar_Position_Rent_Future_Date,
	    Bar_Position_Supplier_Share,
	    Bar_Position_Site_Share,
	    Bar_Position_Owners_Share,
	    Bar_Position_Secondary_Owners_Share,
	    Bar_Position_Supplier_Share_Previous,
	    Bar_Position_Site_Share_Previous,
	    Bar_Position_Owners_Share_Previous,
	    Bar_Position_Secondary_Owners_Share_Previous,
	    Bar_Position_Supplier_Share_Future,
	    Bar_Position_Site_Share_Future,
	    Bar_Position_Owners_Share_Future,
	    Bar_Position_Secondary_Owners_Share_Future,
	    Bar_Position_Share_Past_Date,
	    Bar_Position_Share_Future_Date,
	    Bar_Position_Licence_Charge,
	    Bar_Position_Licence_Previous,
	    Bar_Position_Licence_Future,
	    Bar_Position_Licence_Past_Date,
	    Bar_Position_Licence_Future_Date,
	    Bar_Position_Use_Terms,
	    Bar_Position_TX_Collection,
	    Bar_Position_TX_Collection_Use_Default,
	    Bar_Position_TX_Movement,
	    Bar_Position_TX_Movement_Use_Default,
	    Bar_Position_TX_EDC,
	    Bar_Position_TX_EDC_Use_Detault,
	    Bar_Position_TX_Format,
	    Bar_Position_TX_Format_Use_Default,
	    Bar_Position_RX_Collection,
	    Bar_Position_RX_Collection_Use_Default,
	    Bar_Position_RX_Movement,
	    Bar_Position_RX_Movement_Use_Default,
	    Bar_Position_RX_EDC,
	    Bar_Position_RX_EDC_Use_Detault,
	    Bar_Position_RX_Format,
	    Bar_Position_RX_Format_Use_Default,
	    Bar_Position_Net_Target,
	    Bar_Position_Below_Net_Target_Counter,
	    Bar_Position_Below_Company_Target_Counter,
	    Bar_Position_Security_Required,
	    Bar_Position_Site_Has_Cashbox_Keys,
	    Bar_Position_Site_Has_FreePlay_Access,
	    Bar_Position_Override_Rent,
	    Bar_Position_Override_Shares,
	    Bar_Position_Override_Licence,
	    Bar_Position_Category,
	    Bar_Position_PPL_Charge,
	    Bar_Position_PPL_Previous,
	    Bar_Position_PPL_Future,
	    Bar_Position_PPL_Past_Date,
	    Bar_Position_PPL_Future_Date,
	    Bar_Position_Float_Issued,
	    Bar_Position_Float_Recovered,
	    Bar_Position_Use_Site_Share_For_Secondary_Brewery,
	    Bar_Position_Prize_LOS,
	    Bar_Position_Rent_Schedule_ID,
	    Bar_Position_Share_Schedule_ID,
	    Bar_Position_Override_Rent_Schedule,
	    Bar_Position_Override_Share_Schedule,
	    Bar_Position_Last_Collection_ID,
	    Bar_Position_Override_Rent_From_Schedule_To_Rent,
	    Bar_Position_Override_Rent_From_Rent_To_Schedule,
	    Bar_Position_Override_Rent_From_Schedule_To_Rent_Date,
	    Bar_Position_Override_Rent_From_Rent_To_Schedule_Date,
	    Bar_Position_Rent_Schedule_ID_From,
	    Bar_Position_Disable_EDI_Export,
	    Bar_Position_Invoice_Period,
	    Bar_Position_Machine_Enabled,
	    Bar_Position_Note_Acceptor_Enabled,
	    Bar_Position_Machine_Enabled_Date,
	    Bar_Position_IsEnable
	FROM   #BarPosTemp
	
	DROP TABLE #BarPosTemp
END







/*

select Bar_Position_Name, * from Bar_Position order by 1 desc
Select max(Bar_Position_ID)+1 From Bar_Position

SELECT COUNT(*) FROM Bar_Position bp WHERE bp.Bar_Position_Name = CAST('100' AS NUMERIC(3))

SELECT * FROM Bar_Position WHERE Bar_Position_Name='100'


DECLARE @BulkCopyStatus int
EXEC rsp_CreateBarPositionTemplate 2,508,515
SELECT @BulkCopyStatus

DELETE FROM Bar_Position WHERE Bar_Position_ID BETWEEN 283434 AND 283538

SELECT * FROM ##TempBarPositionBulkCopy 283433 282595


*/


GO

