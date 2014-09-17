USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOtherCollectionDetailsfromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOtherCollectionDetailsfromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
 *	this stored procedure is to insert the Treasury, Part_Collection and events record into the corresponding
 * tables 
 *
 *	Change History:
 *	
 *	Sudarsan S		20-05-2008		created
 *	modified to insert the records for the corresponding collection_id on 04-06-2008
 *  modified to update the events table and Part_Collection tabe if these records already exists
 *  Anuradha J	added Treasury_Actual_Date
 *  Sudarsan S	14/07/09	added code to update collection id alone
 *  GBabu	08/12/10	Updated the collection id properly
*/
CREATE PROCEDURE [dbo].[usp_InsertOtherCollectionDetailsfromXML]  
  @doc VARCHAR(MAX),  
  @IsSuccess VARCHAR(500) OUTPUT  
AS  
  
BEGIN  
SET NOCOUNT ON 
  
DECLARE @iDoc INT  
DECLARE @iCount INT  
DECLARE @iBatch_No INT  
DECLARE @vcSite_Code VARCHAR(50)  
DECLARE @iCollection_ID INT
DECLARE @Site_Id INT
  
  
DECLARE @iTreasury_No INT  
DECLARE @iPart_Collection_No INT  
DECLARE @iDoor_Event_No INT  
DECLARE @iPower_Event_No INT  
DECLARE @iFault_Event_No INT  
DECLARE @iAFT_TransactionNo INT    
DECLARE @iCashIn1P INT  
DECLARE @iCollectionTicket INT  
DECLARE @iCollectionTicket_Print INT  

DECLARE @InstallationNo  INT  -- HQ INSTALLATION FROM EXCHANGE

SET @iCollectionTicket_Print = NULL

DECLARE @CollTable TABLE  
(  
 Collection_ID INT ,  
 Installation_ID INT ,
 batch_No Int 
)  
  
CREATE TABLE #Treasury  
(  
 Installation_ID INT,  
 Treasury_Date VARCHAR(30),  
 Treasury_Time VARCHAR(50),  
 Treasury_Actual_Date DATETIME  
)  
  
  
CREATE TABLE #Door   
(  
 Collection_ID INT,  
 Installation_ID INT,  
 Duration FLOAT,  
 Door_Date DATETIME,  
-- Door_Time VARCHAR(8),  
 Door_Event_Type VARCHAR(50),  
 Key_Owner VARCHAR(50),  
 Door_Polled BIT,  
 Door_Cleared_By VARCHAR(50),  
 Door_Cleared_Date DATETIME  
-- Door_Cleared_Time VARCHAR(50)  
)  
  
DECLARE @Power TABLE  
(  
 Collection_ID INT,  
 Installation_ID INT,  
 Duration FLOAT,  
 Power_Date DATETIME,  
 VTP INT,  
 Power_Polled BIT,  
 Power_Cleared_By INT,  
 Power_Cleared_Date DATETIME  
)  
  
DECLARE @Fault TABLE  
(  
 Collection_ID INT,  
 Installation_ID INT,  
 Fault_Source INT,  
 Fault_ID INT,  
 Fault_Notes VARCHAR(100),  
 Fault_Description VARCHAR(50),  
 Fault_Date DATETIME,   
 Fault_Cleared_By INT,  
 Fault_Cleared_Date DATETIME  
)  
  
    
DECLARE @AFT_Transaction TABLE    
(    
Collection_No int,  
TransactionID bigint,  
Installation_No int,  
Player_ID int,  
WAT_Out float,  
Promo_Cashable_EFT_OUT float,  
NonCashable_EFT_OUT float,  
Transaction_Date datetime,  
Transaction_Type varchar(50),  
TransferID bigint,  
AccountType varchar(50),  
TransactionStatus bit,  
SiteCode varchar(50)  
) 
  
DECLARE @CashIn1P TABLE    
(    
Collection_No int,  
CASH_IN_1P_ID int,  
CASH_IN_1P int,  
CASH_OUT_1P int,
Installation_No int,  
Process varchar(30)  
) 

DECLARE @Collection_Ticket TABLE    
(
Collection_No int,
[HQ_Installation_No] [INT],
[CT_Barcode] [varchar](50)  NULL,
[CT_Value] [money] NULL,
[CT_Declared_Date] [datetime] NULL,
[CT_Printed_Installation_ID] [int] NULL,
[CT_Printed_Collection_ID] [int] NULL,
[CT_Inserted_Installation_ID] [int] NULL,
[CT_Inserted_Collection_ID] [int] NULL,
[CT_User_ID] [int] NULL,
[HQ_CT_ID] [int] NULL,
[CT_IsPromotionalTicket] [BIT] NULL,
[CT_TicketType] INT NULL,
[CT_Source] VARCHAR(50) NULL,
[CT_VoucherStatus] CHAR(3) NULL

)  

DECLARE @Collection_Ticket_Print TABLE    
(
[HQ_Installation_No] INT,
Collection_No int,
[CT_Barcode] [varchar](50)  NULL,
[CT_Value] [money] NULL,
[CT_Declared_Date] [datetime] NULL,
[CT_Printed_Installation_ID] [int] NULL,
[CT_Printed_Collection_ID] [int] NULL,
[CT_Inserted_Installation_ID] [int] NULL,
[CT_Inserted_Collection_ID] [int] NULL,
[CT_User_ID] [int] NULL,
[HQ_CT_ID] [int] NULL,
[CT_IsPromotionalTicket] [BIT] NULL,
[CT_TicketType] INT NULL,
[CT_Source] VARCHAR(50) NULL,
[CT_VoucherStatus] CHAR(3) NULL
) 
  
 SET @IsSuccess = 'SUCCESS'  
  
 SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  
  
 EXEC sp_xml_PrepareDocument @idoc OUTPUT, @doc  
  
 SELECT @iBatch_No = Collection_Batch_No,
		@vcSite_Code = Site_Code,
		@InstallationNo  =InstallationNo 
 FROM OPENXML(@idoc, './CollectionDetails/Site', 2) WITH  
   (Collection_Batch_No INT './Collection_Batch_No',  
    Site_Code VARCHAR(20) './Site_Code'  ,
		InstallationNo INT  './InstallationNo' 
   )  

IF EXISTS(SELECT 1 FROM [Merged_Batch_Details] M WHERE M.Merged_Batch_ID = @iBatch_No OR M.Deleted_Batch_ID = @iBatch_No)
	RETURN 0

SELECT @Site_Id = Site_ID FROM [Site] WHERE Site_Code = @vcSite_Code
   
DECLARE @CentDeclEnabled VARCHAR(10)
EXEC rsp_GetSiteSetting @Site_Id, 'CentralizedDeclaration', @CentDeclEnabled OUTPUT
 
/* the below select statement is written just to know if there  
 are any records corresponding to them. */ 
 
IF UPPER(LTRIM(RTRIM(@CentDeclEnabled))) = 'TRUE'
BEGIN 
	SELECT @iTreasury_No = Treasury_No,   
	   @iPart_Collection_No = Part_Collection_No,  
	   @iDoor_Event_No = Door_Event_No,  
	   @iPower_Event_No = Power_Event_No,  
	   @iFault_Event_No = Fault_Event_No,   
	   @iAFT_TransactionNo = AFT_Transaction_No,  
	   @iCashIn1P = Cash_In_1P_No, 
	   @iCollectionTicket_Print = Print_CT_No,
	   @iCollectionTicket = CT_No
	 FROM OPENXML(@idoc, './CollectionDetails', 2) WITH  
	  (  
	   Treasury_No INT './TreasuryDetails/Treasury_Detail/Treasury_No',  
	   Part_Collection_No INT './PartCollections/Part_Collection/Part_Collection_No',  
	   Door_Event_No INT './DoorEvents/Door_Event/Door_Event_No',  
	   Power_Event_No INT './PowerEvents/Power_Event/Power_Event_No',  
	   Fault_Event_No INT './FaultEvents/Fault_Event/Fault_Event_No',  
	   AFT_Transaction_No INT 'AFTTransactions/AFT_Tran/Transaction_ID',  
	   Cash_In_1P_No INT 'CashIn1P/Cash_In_1P/CASH_IN_1P_ID',
	   Print_CT_No INT 'CollectionTicket_Print/Col_Ticket_Print/CT_ID',
	   CT_No INT 'CollectionTicket/Col_Ticket/CT_ID'
	  ) 
END
ELSE
BEGIN
	 SELECT @iTreasury_No = Treasury_No,   
	   @iPart_Collection_No = Part_Collection_No,  
	   @iDoor_Event_No = Door_Event_No,  
	   @iPower_Event_No = Power_Event_No,  
	   @iFault_Event_No = Fault_Event_No,   
	   @iAFT_TransactionNo = AFT_Transaction_No,  
	   @iCashIn1P = Cash_In_1P_No, 
	   @iCollectionTicket = CT_No
	 FROM OPENXML(@idoc, './CollectionDetails', 2) WITH  
	  (  
	   Treasury_No INT './TreasuryDetails/Treasury_Detail/Treasury_No',  
	   Part_Collection_No INT './PartCollections/Part_Collection/Part_Collection_No',  
	   Door_Event_No INT './DoorEvents/Door_Event/Door_Event_No',  
	   Power_Event_No INT './PowerEvents/Power_Event/Power_Event_No',  
	   Fault_Event_No INT './FaultEvents/Fault_Event/Fault_Event_No',  
	   AFT_Transaction_No INT 'AFTTransactions/AFT_Tran/Transaction_ID',  
	   Cash_In_1P_No INT 'CashIn1P/Cash_In_1P/CASH_IN_1P_ID',
	   CT_No INT 'CollectionTicket/Col_Ticket/CT_ID'
	  ) 
END
  
	--IF HQ INSTALLTION NUMBER IS NOT FOUND IN XML 
	IF ISNULL(@InstallationNo,-1)= -1 
	BEGIN 
		SET @IsSuccess = 'No InstallationNo Exists in XML --[CollectionDetails/Site/InstallationNo]'---8 -- No collection_id exists  
		GOTO Err 
	END 

-- SELECT @iCollection_ID = Collection_ID   
--   FROM dbo.Collection C   
-- INNER JOIN dbo.Batch B ON C.Batch_ID = B.Batch_ID   
-- WHERE SUBSTRING(B.Batch_Ref, CHARINDEX(',', B.Batch_Ref) + 1, LEN(B.Batch_Ref) - CHARINDEX(',', B.Batch_Ref)) = @iBatch_No  
  
 INSERT INTO @CollTable  
 SELECT C.Collection_ID, C.Installation_ID,B.Batch_ID  
 FROM dbo.Collection C  
 INNER JOIN dbo.Batch B ON C.Batch_ID = B.Batch_ID   
 WHERE B.Batch_Ref = @vcSite_Code + ',' + CAST(@iBatch_No AS VARCHAR)   AND C.Installation_ID=@InstallationNo
  
-- IF @iCollection_ID IS NULL  
 IF NOT EXISTS(SELECT 1 FROM @CollTable)  
 BEGIN  
  SET @IsSuccess = 'No corresponding collection_id exists'---8 -- No collection_id exists  
  GOTO Err  
 END  
  
 IF @iTreasury_No IS NOT NULL  
 BEGIN  
  
--  INSERT INTO @Treasury  
--  SELECT * FROM OPENXML(@idoc, './CollectionDetails/TreasuryDetails/Treasury_Detail', 2) WITH  
--   (  
--    Installation_ID INT './HQ_Installation_No',  
--    [User_Name] VARCHAR(50) './User_Name',  
--    Treasury_Date VARCHAR(30) './Treasury_Date',  
--    Treasury_Time VARCHAR(50) './Treasury_Time',  
--    Treasury_Amount REAL './Treasury_Amount',  
--    Treasury_Reason VARCHAR(200) './Treasury_Reason',  
--    Treasury_Allocated BIT './Treasury_Allocated',  
--    Treasury_Type VARCHAR(50) './Treasury_Type',  
--    Treasury_Temp BIT './Treasury_Temp',  
--    Treasury_Docket_No VARCHAR(50) './Treasury_Docket_No',  
--    Treasury_Breakdown_2000p REAL './Treasury_Breakdown_2000p',  
--    Treasury_Breakdown_1000p REAL './Treasury_Breakdown_1000p',  
--    Treasury_Breakdown_500p REAL './Treasury_Breakdown_500p',  
--    Treasury_Breakdown_200p REAL './Treasury_Breakdown_200p',  
--    Treasury_Breakdown_100p REAL './Treasury_Breakdown_100p',  
--    Treasury_Breakdown_50p REAL './Treasury_Breakdown_50p',  
--    Treasury_Breakdown_20p REAL './Treasury_Breakdown_20p',  
--    Treasury_Breakdown_10p REAL './Treasury_Breakdown_10p',  
--    Treasury_Breakdown_5p REAL './Treasury_Breakdown_5p',  
--    Treasury_Breakdown_2p REAL './Treasury_Breakdown_2p',  
--    Treasury_Float_Issued_By INT './Treasury_Float_Issued_By',  
--    Treasury_Float_Recovered_Total REAL './Treasury_Float_Recovered_Total',  
--    Treasury_Float_Recovered_2000p REAL './Treasury_Float_Recovered_2000p',  
--    Treasury_Float_Recovered_1000p REAL './Treasury_Float_Recovered_1000p',  
--    Treasury_Float_Recovered_500p REAL './Treasury_Float_Recovered_500p',  
--    Treasury_Float_Recovered_200p REAL './Treasury_Float_Recovered_200p',  
--    Treasury_Float_Recovered_100p REAL './Treasury_Float_Recovered_100p',  
--    Treasury_Float_Recovered_50p REAL './Treasury_Float_Recovered_50p',  
--    Treasury_Float_Recovered_20p REAL './Treasury_Float_Recovered_20p',  
--    Treasury_Float_Recovered_10p REAL './Treasury_Float_Recovered_10p',  
--    Treasury_Float_Recovered_5p REAL './Treasury_Float_Recovered_5p',  
--    Treasury_Float_Recovered_2p REAL './Treasury_Float_Recovered_2p',  
--    Treasury_Membership_No VARCHAR(50) './Treasury_Membership_No',  
--    Treasury_Actual_Date DATETIME './Treasury_Actual_Date'  
--   ) --AS A  
--   INNER JOIN @CollTable C ON A.Installation_ID = C.Installation_ID  
  
  INSERT INTO #Treasury  
  SELECT Installation_ID, CONVERT(VARCHAR, Treasury_Date, 106), CONVERT(VARCHAR, Treasury_Date, 108), Treasury_Actual_Date FROM OPENXML(@idoc, './CollectionDetails/TreasuryDetails/Treasury_Detail', 2) WITH  
   (  
    Installation_ID INT './HQ_Installation_No',  
    Treasury_Date DATETIME './Treasury_Date',  
--    Treasury_Time VARCHAR(50) './Treasury_Time',  
    Treasury_Actual_Date DATETIME './Treasury_Actual_Date'  
--    Treasury_Type VARCHAR(50) './Treasury_Type'  
   )  
  
   UPDATE TE  
      SET TE.Collection_ID = C.Collection_ID  
     FROM dbo.Treasury_Entry TE  
  INNER JOIN #Treasury T ON T.Installation_ID = TE.Installation_ID AND T.Treasury_Date = TE.Treasury_Date AND T.Treasury_Time = TE.Treasury_Time AND ISNULL(TE.Treasury_Actual_Date, '') = ISNULL(T.Treasury_Actual_Date, '')  
  INNER JOIN @CollTable C ON TE.Installation_ID = C.Installation_ID  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while updating Collection_ID in Treasury_Entry'---9  -- error while inserting into Trerasury_Entry  
    GOTO Err  
   END  
  
/* no need to insert as all the treasuries would have reached enterprise long before  
  
  INSERT INTO dbo.Treasury_Entry  
   SELECT C.Collection_ID, T.*  
     FROM  @Treasury T --dbo.#TempTreasury T  
  INNER JOIN  @CollTable C ON C.Installation_ID = T.Installation_ID  
   LEFT JOIN  dbo.Treasury_Entry TE ON T.Installation_ID = TE.Installation_ID AND T.Treasury_Date = TE.Treasury_Date AND T.Treasury_Time = TE.Treasury_Time AND T.Treasury_Type = TE.Treasury_Type  
    WHERE  ISNULL(TE.Installation_ID, 0) = 0 AND ISNULL(TE.Treasury_Date, '') = '' AND ISNULL(TE.Treasury_Time, '') = '' AND ISNULL(TE.Treasury_Type, '') = ''  
  
--    WHERE  CONVERT(VARCHAR(20), T.Installation_ID) + ',' + T.Treasury_Date + ',' + T.Treasury_Time + ',' + T.Treasury_Type NOT IN  
--      (SELECT CONVERT(VARCHAR(20), Installation_ID) + ',' + Treasury_Date + ',' + Treasury_Time + ',' + Treasury_Type FROM dbo.Treasury_Entry)  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting into Treasury_Entry'---9  -- error while inserting into Trerasury_Entry  
    GOTO Err  
   END  
  
 change ends  */  
  
 END  
  
 IF @iPart_Collection_No IS NOT NULL  
 BEGIN  
--  INSERT INTO dbo.Part_Collection  
  SELECT C.Collection_ID, A.* INTO dbo.#TempPart_Collection FROM OPENXML(@idoc, './CollectionDetails/PartCollections/Part_Collection', 2) WITH  
   (  
    Installation_ID INT './HQ_Installation_No',  
    User_No VARCHAR(50) './User_Name',  
    Part_Collection_Date DATETIME './Part_Collection_Date',  
--    Part_Collection_Time VARCHAR(50) './Part_Collection_Time',  
    Part_Collection_Declared BIT './Part_Collection_Declared',  
    Part_Collection_CashCollected REAL './Part_Collection_CashCollected', 
    Part_Collection_Cash_Collected_1p REAL './Part_Collection_Cash_Collected_1p',   
    Part_Collection_Cash_Collected_2p REAL './Part_Collection_Cash_Collected_2p',  
    Part_Collection_Cash_Collected_5p REAL './Part_Collection_Cash_Collected_5p',  
    Part_Collection_Cash_Collected_10p REAL './Part_Collection_Cash_Collected_10p',  
    Part_Collection_Cash_Collected_20p REAL './Part_Collection_Cash_Collected_20p',  
    Part_Collection_Cash_Collected_50p REAL './Part_Collection_Cash_Collected_50p',  
    Part_Collection_Cash_Collected_100p REAL './Part_Collection_Cash_Collected_100p',  
    Part_Collection_Cash_Collected_200p REAL './Part_Collection_Cash_Collected_200p',  
    Part_Collection_Cash_Collected_500p REAL './Part_Collection_Cash_Collected_500p',  
    Part_Collection_Cash_Collected_1000p REAL './Part_Collection_Cash_Collected_1000p',  
    Part_Collection_Cash_Collected_2000p REAL './Part_Collection_Cash_Collected_2000p',  
    Part_Collection_Cash_Collected_5000p REAL './Part_Collection_Cash_Collected_5000p',  
    Part_Collection_Cash_Collected_10000p REAL './Part_Collection_Cash_Collected_10000p',  
    Part_Collection_Cash_Collected_20000p REAL './Part_Collection_Cash_Collected_20000p',  
    Part_Collection_Cash_Collected_50000p REAL './Part_Collection_Cash_Collected_50000p',  
    Part_Collection_Cash_Collected_100000p REAL './Part_Collection_Cash_Collected_100000p',  
    Part_Collection_CashRefills REAL './Part_Collection_CashRefills',  
    Part_Collection_Cash_Refills_2p REAL './Part_Collection_Cash_Refills_2p',  
    Part_Collection_Cash_Refills_5p REAL './Part_Collection_Cash_Refills_5p',  
    Part_Collection_Cash_Refills_10p REAL './Part_Collection_Cash_Refills_10p',  
    Part_Collection_Cash_Refills_20p REAL './Part_Collection_Cash_Refills_20p',  
    Part_Collection_Cash_Refills_50p REAL './Part_Collection_Cash_Refills_50p',  
    Part_Collection_Cash_Refills_100p REAL './Part_Collection_Cash_Refills_100p',  
    Part_Collection_Cash_Refills_200p REAL './Part_Collection_Cash_Refills_200p',  
    Part_Collection_Cash_Refills_500p REAL './Part_Collection_Cash_Refills_500p',  
    Part_Collection_Cash_Refills_1000p REAL './Part_Collection_Cash_Refills_1000p',  
    Part_Collection_Cash_Refills_2000p REAL './Part_Collection_Cash_Refills_2000p',  
    Part_Collection_Cash_Refills_5000p REAL './Part_Collection_Cash_Refills_5000p',  
    Part_Collection_Cash_Refills_10000p REAL './Part_Collection_Cash_Refills_10000p',  
    Part_Collection_Cash_Refills_20000p REAL './Part_Collection_Cash_Refills_20000p',  
    Part_Collection_Cash_Refills_50000p REAL './Part_Collection_Cash_Refills_50000p',  
    Part_Collection_Cash_Refills_100000p REAL './Part_Collection_Cash_Refills_100000p',  
    Part_Collection_TokensCollected REAL './Part_Collection_TokensCollected',  
    Part_Collection_TokenRefills REAL './Part_Collection_TokenRefills',  
    Part_Collection_CounterCashIn INT './Part_Collection_CounterCashIn',  
    Part_Collection_CounterCashOut INT './Part_Collection_CounterCashOut',  
    Part_Collection_CounterTokensIn INT './Part_Collection_CounterTokensIn',  
    Part_Collection_CounterTokensOut INT './Part_Collection_CounterTokensOut',  
    Part_Collection_CounterJackpots INT './Part_Collection_CounterJackpots',  
    Part_Collection_PreviousCounterCashIn INT './Part_Collection_PreviousCounterCashIn',  
    Part_Collection_PreviousCounterCashOut INT './Part_Collection_PreviousCounterCashOut',  
    Part_Collection_PreviousCounterTokensIn INT './Part_Collection_PreviousCounterTokensIn',  
    Part_Collection_PreviousCounterTokensOut INT './Part_Collection_PreviousCounterTokensOut',  
    Part_Collection_PreviousCollectionDate VARCHAR(30) './Part_Collection_PreviousCollectionDate'  
   ) AS A  
   INNER JOIN @CollTable C ON A.Installation_ID = C.Installation_ID  
  
   IF @@Error <> 0  
   BEGIN  
    SET @ISSuccess = 'error while inserting in to TempPart_Collection table'---10 -- error while inserting in to Part_Collection table  
    GOTO Err  
   END  
  
   INSERT INTO dbo.Part_Collection  
   SELECT T.Collection_ID,  
     T.Installation_ID,  
     T.User_No,  
     CONVERT(VARCHAR, T.Part_Collection_Date, 106),  
     CONVERT(VARCHAR, T.Part_Collection_Date, 108),  
     T.Part_Collection_Declared,  
     T.Part_Collection_CashCollected, 
     T.Part_Collection_Cash_Collected_2p,  
     T.Part_Collection_Cash_Collected_5p,  
     T.Part_Collection_Cash_Collected_10p,  
     T.Part_Collection_Cash_Collected_20p,  
     T.Part_Collection_Cash_Collected_50p,  
     T.Part_Collection_Cash_Collected_100p,  
     T.Part_Collection_Cash_Collected_200p,  
     T.Part_Collection_Cash_Collected_500p,  
     T.Part_Collection_Cash_Collected_1000p,  
     T.Part_Collection_Cash_Collected_2000p,  
     T.Part_Collection_Cash_Collected_5000p,  
     T.Part_Collection_Cash_Collected_10000p,  
     T.Part_Collection_Cash_Collected_20000p,  
     T.Part_Collection_Cash_Collected_50000p,  
     T.Part_Collection_Cash_Collected_100000p,  
     T.Part_Collection_CashRefills,  
     T.Part_Collection_Cash_Refills_2p,  
     T.Part_Collection_Cash_Refills_5p,  
     T.Part_Collection_Cash_Refills_10p,  
     T.Part_Collection_Cash_Refills_20p,  
     T.Part_Collection_Cash_Refills_50p,  
     T.Part_Collection_Cash_Refills_100p,  
     T.Part_Collection_Cash_Refills_200p,  
     T.Part_Collection_Cash_Refills_500p,  
     T.Part_Collection_Cash_Refills_1000p,  
     T.Part_Collection_Cash_Refills_2000p,  
     T.Part_Collection_Cash_Refills_5000p,  
     T.Part_Collection_Cash_Refills_10000p,  
     T.Part_Collection_Cash_Refills_20000p,  
     T.Part_Collection_Cash_Refills_50000p,  
     T.Part_Collection_Cash_Refills_100000p,  
     T.Part_Collection_TokensCollected,  
     T.Part_Collection_TokenRefills,  
     T.Part_Collection_CounterCashIn,  
     T.Part_Collection_CounterCashOut,  
     T.Part_Collection_CounterTokensIn,  
     T.Part_Collection_CounterTokensOut,  
     T.Part_Collection_CounterJackpots,  
     T.Part_Collection_PreviousCounterCashIn,  
     T.Part_Collection_PreviousCounterCashOut,  
     T.Part_Collection_PreviousCounterTokensIn,  
     T.Part_Collection_PreviousCounterTokensOut,  
     T.Part_Collection_PreviousCollectionDate,
     T.Part_Collection_Cash_Collected_1p  
     FROM dbo.#TempPart_Collection T  
   LEFT JOIN dbo.Part_Collection PT (NOLOCK) ON T.Collection_ID = PT.Collection_ID AND CONVERT(VARCHAR, T.Part_Collection_Date, 106) = PT.Part_Collection_Date   
      AND CONVERT(VARCHAR, T.Part_Collection_Date, 108) = PT.Part_Collection_Time  
    WHERE PT.Collection_ID IS NULL AND PT.Part_Collection_Date IS NULL AND PT.Part_Collection_Time IS NULL  
--CAST(Collection_ID AS VARCHAR) + ',' + Part_Collection_Date + Part_Collection_Time  
--     NOT IN (SELECT CAST(Collection_ID AS VARCHAR) + ',' + Part_Collection_Date + Part_Collection_Time FROM dbo.Part_Collection)  
  
   IF @@Error <> 0  
   BEGIN  
    SET @ISSuccess = 'error while inserting in to Part_Collection table'---10 -- error while inserting in to Part_Collection table  
    GOTO Err  
   END  
  
   UPDATE PC  
    SET PC.Installation_ID = T.Installation_ID,  
     PC.Part_Collection_User = T.User_No,  
     PC.Part_Collection_Date = CONVERT(VARCHAR, T.Part_Collection_Date, 106),  
     PC.Part_Collection_Time = CONVERT(VARCHAR, T.Part_Collection_Date, 108),  
     PC.Part_Collection_Declared = T.Part_Collection_Declared,  
     PC.Part_Collection_CashCollected = T.Part_Collection_CashCollected,   
     PC.Part_Collection_Cash_Collected_1p = T.Part_Collection_Cash_Collected_1p,   
     PC.Part_Collection_Cash_Collected_2p = T.Part_Collection_Cash_Collected_2p,  
     PC.Part_Collection_Cash_Collected_5p = T.Part_Collection_Cash_Collected_5p,  
     PC.Part_Collection_Cash_Collected_10p = T.Part_Collection_Cash_Collected_10p,  
     PC.Part_Collection_Cash_Collected_20p = T.Part_Collection_Cash_Collected_20p,  
     PC.Part_Collection_Cash_Collected_50p = T.Part_Collection_Cash_Collected_50p,  
     PC.Part_Collection_Cash_Collected_100p = T.Part_Collection_Cash_Collected_100p,  
     PC.Part_Collection_Cash_Collected_200p = T.Part_Collection_Cash_Collected_200p,  
     PC.Part_Collection_Cash_Collected_500p = T.Part_Collection_Cash_Collected_500p,  
     PC.Part_Collection_Cash_Collected_1000p = T.Part_Collection_Cash_Collected_1000p,  
  PC.Part_Collection_Cash_Collected_2000p = T.Part_Collection_Cash_Collected_2000p,  
     PC.Part_Collection_Cash_Collected_5000p = T.Part_Collection_Cash_Collected_5000p,  
     PC.Part_Collection_Cash_Collected_10000p = T.Part_Collection_Cash_Collected_10000p,  
     PC.Part_Collection_Cash_Collected_20000p = T.Part_Collection_Cash_Collected_20000p,  
     PC.Part_Collection_Cash_Collected_50000p = T.Part_Collection_Cash_Collected_50000p,  
     PC.Part_Collection_Cash_Collected_100000p = T.Part_Collection_Cash_Collected_100000p,  
     PC.Part_Collection_CashRefills = T.Part_Collection_CashRefills,  
     PC.Part_Collection_Cash_Refills_2p = T.Part_Collection_Cash_Refills_2p,  
     PC.Part_Collection_Cash_Refills_5p = T.Part_Collection_Cash_Refills_5p,  
     PC.Part_Collection_Cash_Refills_10p = T.Part_Collection_Cash_Refills_10p,  
     PC.Part_Collection_Cash_Refills_20p = T.Part_Collection_Cash_Refills_20p,  
     PC.Part_Collection_Cash_Refills_50p = T.Part_Collection_Cash_Refills_50p,  
     PC.Part_Collection_Cash_Refills_100p = T.Part_Collection_Cash_Refills_100p,  
     PC.Part_Collection_Cash_Refills_200p = T.Part_Collection_Cash_Refills_200p,  
     PC.Part_Collection_Cash_Refills_500p = T.Part_Collection_Cash_Refills_500p,  
     PC.Part_Collection_Cash_Refills_1000p = T.Part_Collection_Cash_Refills_1000p,  
     PC.Part_Collection_Cash_Refills_2000p = T.Part_Collection_Cash_Refills_2000p,  
     PC.Part_Collection_Cash_Refills_5000p = T.Part_Collection_Cash_Refills_5000p,  
     PC.Part_Collection_Cash_Refills_10000p = T.Part_Collection_Cash_Refills_10000p,  
     PC.Part_Collection_Cash_Refills_20000p = T.Part_Collection_Cash_Refills_20000p,  
     PC.Part_Collection_Cash_Refills_50000p = T.Part_Collection_Cash_Refills_50000p,  
     PC.Part_Collection_Cash_Refills_100000p = T.Part_Collection_Cash_Refills_100000p,  
     PC.Part_Collection_TokensCollected = T.Part_Collection_TokensCollected,  
     PC.Part_Collection_TokenRefills = T.Part_Collection_TokenRefills,  
     PC.Part_Collection_CounterCashIn = T.Part_Collection_CounterCashIn,  
     PC.Part_Collection_CounterCashOut = T.Part_Collection_CounterCashOut,  
     PC.Part_Collection_CounterTokensIn = T.Part_Collection_CounterTokensIn,  
     PC.Part_Collection_CounterTokensOut = T.Part_Collection_CounterTokensOut,  
     PC.Part_Collection_CounterJackpots = T.Part_Collection_CounterJackpots,  
     PC.Part_Collection_PreviousCounterCashIn = T.Part_Collection_PreviousCounterCashIn,  
     PC.Part_Collection_PreviousCounterCashOut = T.Part_Collection_PreviousCounterCashOut,  
     PC.Part_Collection_PreviousCounterTokensIn = T.Part_Collection_PreviousCounterTokensIn,  
     PC.Part_Collection_PreviousCounterTokensOut = T.Part_Collection_PreviousCounterTokensOut,  
     PC.Part_Collection_PreviousCollectionDate = T.Part_Collection_PreviousCollectionDate  
   FROM dbo.Part_Collection PC   
  INNER JOIN dbo.#TempPart_Collection T ON PC.Collection_ID = T.Collection_ID AND PC.Part_Collection_Date = CONVERT(VARCHAR, T.Part_Collection_Date, 106) AND PC.Part_Collection_Time = CONVERT(VARCHAR, T.Part_Collection_Date, 108)  
  
   IF @@Error <> 0  
   BEGIN  
    SET @ISSuccess = 'error while updating the Part_Collection table'---10 -- error while inserting in to Part_Collection table  
    GOTO Err  
   END  
  
 END  
  
 IF @iDoor_Event_No IS NOT NULL  
 BEGIN  
  INSERT INTO #Door  
  SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/DoorEvents/Door_Event') WITH  
     (  
       Installation_ID INT './HQ_Installation_No',  
       Duration FLOAT './Duration',  
       Door_Date DATETIME './Door_Date',  
--       Door_Time VARCHAR(8) './Door_Time',  
       Door_Event_Type VARCHAR(50) './Door_Event_Type',  
       Key_Owner VARCHAR(50) './Key_Owner',  
       Door_Polled BIT './Door_Polled',  
       Door_Cleared_By VARCHAR(50) './Door_Cleared_By',  
       Door_Cleared_Date DATETIME './Door_Cleared_Date'  
--       Door_Cleared_Time VARCHAR(50) './Door_Cleared_Time'  
     ) AS A  
   INNER JOIN @CollTable C ON A.Installation_ID = C.Installation_ID  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting in to TempDoor table'---11 -- error while inserting in to Door_Event table  
    GOTO Err  
   END  
  
  INSERT INTO dbo.Door_Event(  
        Collection_ID,  
        Installation_ID,  
        Door_Event_Duration,  
        Door_Event_Date,  
        Door_Event_Time,  
        Door_Event_Type,  
        Door_Event_Key_Owner,  
        Door_Event_Polled,  
        Door_Cleared_By,  
        Door_Cleared_Date,  
        Door_Cleared_Time  
         )  
  
  SELECT  D.Collection_ID,  
    D.Installation_ID,  
    D.Duration,  
    CONVERT(VARCHAR, D.Door_Date, 106),  
    CONVERT(VARCHAR, D.Door_Date, 108),  
    D.Door_Event_Type,  
    D.Key_Owner,  
    D.Door_Polled,  
    D.Door_Cleared_By,  
    CONVERT(VARCHAR, D.Door_Cleared_Date, 106),  
    CONVERT(VARCHAR, D.Door_Cleared_Date, 108)  
  FROM #Door D  
		LEFT JOIN dbo.Door_Event DE ON D.Installation_ID = DE.Installation_ID AND CONVERT(VARCHAR, D.Door_Date, 106) = DE.Door_Event_Date AND CONVERT(VARCHAR, D.Door_Date, 108) = DE.Door_Event_Time AND D.Duration = DE.Door_Event_Duration AND D.Door_Event_Type = DE.Door_Event_Type
      WHERE ISNULL(DE.Collection_ID, 0) = 0 AND ISNULL(DE.Door_Event_Date, '') = '' AND ISNULL(DE.Door_Event_Time, '') = '' AND ISNULL(DE.Door_Event_Duration, 0) = 0 AND ISNULL(DE.Door_Event_Type, '') = ''  
  
  IF @@Error <> 0  
  BEGIN  
   SET @IsSuccess = 'error while inserting in to Door_Event table'---11 -- error while inserting in to Door_Event table  
   GOTO Err  
  END  
  
  UPDATE D  
   SET D.Installation_ID = T.Installation_ID,  
    D.Door_Event_Duration = T.Duration,  
    D.Door_Event_Date = CONVERT(VARCHAR, T.Door_Date, 106),  
    D.Door_Event_Time = CONVERT(VARCHAR, T.Door_Date, 108),  
    D.Door_Event_Type = T.Door_Event_Type,  
    D.Door_Event_Key_Owner = T.Key_Owner,  
    D.Door_Event_Polled = T.Door_Polled,  
    D.Door_Cleared_By = T.Door_Cleared_By,  
    D.Door_Cleared_Date = CONVERT(VARCHAR, T.Door_Cleared_Date, 106),  
    D.Door_Cleared_Time = CONVERT(VARCHAR, T.Door_Cleared_Date, 108)  
  FROM dbo.Door_Event D  
  INNER JOIN #Door T ON D.Installation_ID = T.Installation_ID AND D.Door_Event_Duration = T.Duration  
    AND D.Door_Event_Date = CONVERT(VARCHAR, T.Door_Date, 106) AND D.Door_Event_Time = CONVERT(VARCHAR, T.Door_Date, 108) AND D.Door_Event_Type = T.Door_Event_Type  
  
  IF @@Error <> 0  
  BEGIN  
   SET @IsSuccess = 'error while updating Door_Event table'---11 -- error while inserting in to Door_Event table  
   GOTO Err  
  END  
  
 END  
  
 IF @iPower_Event_No IS NOT NULL  
 BEGIN  
   INSERT INTO @Power  
   SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/PowerEvents/Power_Event') WITH  
     (  
       Installation_ID INT './HQ_Installation_No',  
       Duration FLOAT './Duration',  
       Power_Date DATETIME './Power_Date',  
--       Power_Time VARCHAR(50) './Power_Time',  
       VTP INT './VTP',  
       Power_Polled BIT './Power_Polled',  
       Power_Cleared_By INT './Power_Cleared_By',  
       Power_Cleared_Date DATETIME './Power_Cleared_Date'  
--       Power_Cleared_Time VARCHAR(50) './Power_Cleared_Time'  
     ) AS A  
   INNER JOIN @CollTable C ON A.Installation_ID = C.Installation_ID  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting in to TempPower table'---12 -- error while inserting in to Power_Event table  
    GOTO Err  
   END  
  
   INSERT INTO dbo.Power_Event  
       (  
        Collection_ID,  
        Installation_ID,  
        Power_Event_Duration,  
        Power_Event_Date,  
        Power_Event_Time,  
        Power_Event_VTP,  
        Power_Event_Polled,  
        Power_Cleared_By,  
        Power_Cleared_Date,  
        Power_Cleared_Time  
       )  
  
   SELECT  P.Collection_ID,  
     P.Installation_ID,  
     P.Duration,  
     CONVERT(VARCHAR, P.Power_Date, 106),  
     CONVERT(VARCHAR, P.Power_Date, 108),  
     P.VTP,  
     P.Power_Polled,  
     P.Power_Cleared_By,  
     CONVERT(VARCHAR, P.Power_Cleared_Date, 106),  
     CONVERT(VARCHAR, P.Power_Cleared_Date, 108)  
    FROM @Power P  
   LEFT JOIN dbo.Power_Event PE ON P.Installation_ID = PE.Installation_ID AND P.Duration = PE.Power_Event_Duration AND CONVERT(VARCHAR, P.Power_Date, 106) = PE.Power_Event_Date AND CONVERT(VARCHAR, P.Power_Date, 108) = PE.Power_Event_Time  
   WHERE PE.Installation_ID IS NULL AND PE.Power_Event_Duration IS NULL AND PE.Power_Event_Date IS NULL AND PE.Power_Event_Time IS NULL  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting in to Power_Event table'---12 -- error while inserting in to Power_Event table  
    GOTO Err  
   END  
  
   UPDATE P  
    SET P.Installation_ID = T.Installation_ID,  
     P.Power_Event_Duration = T.Duration,  
     P.Power_Event_Date = CONVERT(VARCHAR, T.Power_Date, 106),  
     P.Power_Event_Time = CONVERT(VARCHAR, T.Power_Date, 108),  
     P.Power_Event_VTP = T.VTP,  
     P.Power_Event_Polled = T.Power_Polled,  
     P.Power_Cleared_By = T.Power_Cleared_By,  
     P.Power_Cleared_Date = CONVERT(VARCHAR, T.Power_Cleared_Date, 106),  
     P.Power_Cleared_Time = CONVERT(VARCHAR, T.Power_Cleared_Date, 108)  
   FROM dbo.Power_Event P   
   INNER JOIN @Power T ON P.Installation_ID = T.Installation_ID AND P.Power_Event_Date = CONVERT(VARCHAR, T.Power_Date, 106)  
      AND P.Power_Event_Time = CONVERT(VARCHAR, T.Power_Date, 108) AND P.Power_Event_Duration = T.Duration  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while updating the Power_Event table'---12 -- error while inserting in to Power_Event table  
    GOTO Err  
   END  
  
  
 END  
  
 IF @iFault_Event_No IS NOT NULL  
 BEGIN  
   INSERT INTO @Fault  
   SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/FaultEvents/Fault_Event') WITH  
     (  
       Installation_ID INT './HQ_Installation_No',  
       Fault_Source INT './Fault_Source',  
       Fault_ID INT './Fault_ID',  
       Fault_Notes VARCHAR(100) './Fault_Notes',  
       Fault_Description VARCHAR(50) './Fault_Description',  
       Fault_Date DATETIME './Fault_Date',  
--       Fault_Time VARCHAR(50) './Fault_Time',  
       Fault_Cleared_By INT './Fault_Cleared_By',  
       Fault_Cleared_Date DATETIME './Fault_Cleared_Date'  
--       Fault_Cleared_Time VARCHAR(50) './Fault_Cleared_Time'  
     ) AS A  
   INNER JOIN @CollTable C ON A.Installation_ID = C.Installation_ID  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting in to TempFault table'---13 -- error while inserting in to Fault_Event table  
    GOTO Err  
   END  
  
   INSERT INTO dbo.Fault_Event  
       (  
        Collection_ID,  
        Installation_ID,  
        Fault_Event_Source,  
        Fault_Event_Code,  
        Fault_Event_Notes,  
        Fault_Event_Description,  
        Fault_Event_Date,  
        Fault_Event_Time,  
        Fault_Cleared_By,  
        Fault_Cleared_Date,  
        Fault_Cleared_Time  
       )  
  
   SELECT  F.Collection_ID,  
     F.Installation_ID,  
     F.Fault_Source,  
     F.Fault_ID,  
     F.Fault_Notes,  
     F.Fault_Description,  
     CONVERT(VARCHAR, F.Fault_Date, 106),  
     CONVERT(VARCHAR, F.Fault_Date, 108),  
     F.Fault_Cleared_By,  
     CONVERT(VARCHAR, F.Fault_Cleared_Date, 106),  
     CONVERT(VARCHAR, F.Fault_Cleared_Date, 108)  
     FROM @Fault F  
   LEFT JOIN dbo.Fault_Event FE ON F.Installation_ID = FE.Installation_ID AND CONVERT(VARCHAR, F.Fault_Date, 106) = FE.Fault_Event_Date AND CONVERT(VARCHAR, F.Fault_Date, 108) = FE.Fault_Event_Time  
   WHERE ISNULL(FE.Installation_ID, 0) = 0 AND ISNULL(FE.Fault_Event_Date, '') = '' AND ISNULL(FE.Fault_Event_Time, '') = ''  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while inserting in to Fault_Table table'---13 -- error while inserting in to Fault_Event table  
    GOTO Err  
   END  
  
   UPDATE  F  
    SET F.Installation_ID = T.Installation_ID,  
     F.Fault_Event_Source = T.Fault_Source,  
     F.Fault_Event_Code = T.Fault_ID,  
     F.Fault_Event_Notes = T.Fault_Notes,  
     F.Fault_Event_Description = T.Fault_Description,  
     F.Fault_Event_Date = CONVERT(VARCHAR, T.Fault_Date, 106),  
     F.Fault_Event_Time = CONVERT(VARCHAR, T.Fault_Date, 108),  
     F.Fault_Cleared_By = T.Fault_Cleared_By,  
     F.Fault_Cleared_Date = CONVERT(VARCHAR, T.Fault_Cleared_Date, 106),  
     F.Fault_Cleared_Time = CONVERT(VARCHAR, T.Fault_Cleared_Date, 108)  
   FROM dbo.Fault_Event F   
   INNER JOIN @Fault T ON F.Installation_ID = T.Installation_ID AND F.Fault_Event_Date = CONVERT(VARCHAR, T.Fault_Date, 106) AND F.Fault_Event_Time = CONVERT(VARCHAR, T.Fault_Date, 108)  
  
   IF @@Error <> 0  
   BEGIN  
    SET @IsSuccess = 'error while updating the Fault_Table table'---13 -- error while inserting in to Fault_Event table  
    GOTO Err  
   END  
  
 END  
  
IF @iAFT_TransactionNo IS NOT NULL    
 BEGIN    
   INSERT INTO @AFT_Transaction    
   SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/AFTTransactions/AFT_Tran') WITH    
     (    
        TransactionID bigint './Transaction_ID',     
  Installation_No int  './HQ_Installation_No',  
  Player_ID int './Player_ID',  
  WAT_Out float './WatAmt',  
  Promo_Cashable_EFT_OUT float './CashableAmt',  
  NonCashable_EFT_OUT float './NonCashableAmt',  
  Transaction_Date datetime './Transaction_Date',  
  Transaction_Type varchar(50) './Transaction_Type',  
  TransferID bigint './TransferID',  
  AccountType varchar(50) './AccountType',  
  TransactionStatus bit './Transaction_ID',  
  SiteCode varchar(50)'./SiteCode'  
     ) AS A    
   INNER JOIN @CollTable C ON A.Installation_No = C.Installation_ID    
    
  
  
   IF @@Error <> 0    
   BEGIN    
    SET @IsSuccess = 'error while inserting in to TempAFTTransaction table'---13 -- error while inserting in to Fault_Event table    
    GOTO Err    
   END    
    
--   INSERT INTO dbo.AFT_Transactions    
--       (    
--  Installation_No,  
--  Player_ID,  
--  Collection_No,  
--  WAT_Out,  
--  Promo_Cashable_EFT_OUT,  
--  NonCashable_EFT_OUT,  
--  Transaction_Date,  
--  Transaction_Type,  
--  TransferID,  
--  AccountType,  
--  TransactionStatus,  
--  SiteCode  
--       )    
--    
--   SELECT    
--  AFT.Installation_No,  
--  AFT.Player_ID,  
--  AFT.Collection_No,  
--  AFT.WAT_Out,  
--  AFT.Promo_Cashable_EFT_OUT,  
--  AFT.NonCashable_EFT_OUT,  
--  AFT.Transaction_Date,  
--  AFT.Transaction_Type,  
--  AFT.TransferID,  
--  AFT.AccountType,  
--  AFT.TransactionStatus,  
--  AFT.SiteCode  
--     FROM @AFT_Transaction AFT   
--   LEFT JOIN dbo.AFT_Transactions FE ON AFT.Installation_No= FE.Installation_No AND   
--  CONVERT(VARCHAR, AFT.Transaction_Date, 106) = FE.Transaction_Date   
--   WHERE ISNULL(FE.Installation_no, 0) = 0 AND ISNULL(FE.Transaction_Date, '') = ''  
--    
--   IF @@Error <> 0    
--   BEGIN    
--    SET @IsSuccess = 'error while inserting in to AFT_Transaction table'---13 -- error while inserting in to Fault_Event table    
--    GOTO Err    
--   END    
--    
   UPDATE  AFT    
    SET   
  AFT.Collection_No =  T.Collection_No  
   FROM dbo.AFT_Transactions AFT     
   INNER JOIN @AFT_Transaction T ON AFT.Installation_No = T.Installation_No and AFT.TransferID = T.TransferID and AFT.SiteCode =T.SiteCode  
  AND AFT.Transaction_Date = T.Transaction_Date  
    
   IF @@Error <> 0    
   BEGIN    
    SET @IsSuccess = 'error while updating the AFT Transaction table'---13 -- error while inserting in to Fault_Event table    
    GOTO Err    
   END    
    
 END    
  
 IF @iCashIn1P IS NOT NULL    
 BEGIN    
   	INSERT INTO @CashIn1P    
   	SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/CashIn1P/Cash_In_1P') WITH    
     	(    
        	Cash_In_1P_ID int './CASH_IN_1P_ID',     
  	Cash_In_1P int  './CASH_IN_1P',  
  	CASH_OUT_1P int './CASH_OUT_1P',
  	Installation_No int './HQ_Installation_No',    
  	Process varchar(20) './Process'  
     	) AS A    
   	INNER JOIN @CollTable C ON A.Installation_No = C.Installation_ID    
    
   	IF @@Error <> 0    
   	BEGIN    
    		SET @IsSuccess = 'error while inserting in to CashIn1P table' -- error while inserting in to Cash In 1P table  
    		GOTO Err    
   	END      
  	
	IF NOT EXISTS (select  tCashIn1P.Collection_No 
   					FROM dbo.Cash_In_1P tCashIn1P  
   					INNER JOIN @CashIn1P tCashIn 
					ON tCashIn1P.Installation_No = tCashIn.Installation_No 
					and tCashIn1P.HQ_CASH_IN_1P_ID = tCashIn.CASH_IN_1P_ID
		)
	BEGIN 
		INSERT INTO Cash_In_1P 
		SELECT Cash_In_1P, CASH_OUT_1P, Installation_No, 0, Process, Cash_In_1P_ID FROM @CashIn1P  
 		IF @@Error <> 0    
   		BEGIN    
    		SET @IsSuccess = 'error while updating the CashIn1P table'  -- error while updating in to Cash In 1P table  
    		GOTO Err    
   		END   
	END 

   	UPDATE tCashIn1P    
    	SET   
  	tCashIn1P.Collection_No =  tCashIn.Collection_No  
   	FROM dbo.Cash_In_1P tCashIn1P  
   	INNER JOIN @CashIn1P tCashIn 
	ON tCashIn1P.Installation_No = tCashIn.Installation_No 
	and tCashIn1P.HQ_CASH_IN_1P_ID = tCashIn.CASH_IN_1P_ID    
	
  
   	IF @@Error <> 0    
   	BEGIN    
    		SET @IsSuccess = 'error while updating the CashIn1P table'  -- error while updating in to Cash In 1P table  
    		GOTO Err    
   	END    
	

	-- Update collection as the calculation is done in the trigger
	UPDATE  COLL  SET Dud_Coins_100p = Dud_Coins_100p 
	FROM COLLECTION COLL INNER JOIN @CollTable CollTable 
	ON CollTable.Installation_ID = COLL.Installation_ID AND CollTable.batch_No = COLL.Batch_ID 
	WHERE 	COLL.Installation_ID=@InstallationNo

 END  
  
IF UPPER(LTRIM(RTRIM(@CentDeclEnabled))) = 'TRUE'
BEGIN
	 IF @iCollectionTicket IS NOT NULL    
	 BEGIN    
   		INSERT INTO @Collection_Ticket    
   		SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/CollectionTicket/Col_Ticket') WITH    
     		(    
     			[HQ_Installation_No] [INT] './HQ_Installation_No',
        		[CT_Barcode] [varchar](50) './CT_Barcode',
				[CT_Value] [money] './CT_Value',
				[CT_Declared_Date] [datetime] './CT_Declared_Date',
				[CT_Printed_Installation_ID] [int] './CT_Printed_Installation_ID',
				[CT_Printed_Collection_ID] [int] './CT_Printed_Collection_ID',
				[CT_Inserted_Installation_ID] [int] './CT_Inserted_Installation_ID',
				[CT_Inserted_Collection_ID] [int] './CT_Inserted_Collection_ID',
				[CT_User_ID] [int] './CT_User_ID',
				[HQ_CT_ID] [int] './CT_ID',
				[CT_IsPromotionalTicket] [BIT] './CT_IsPromotionalTicket',
				[CT_TicketType] [INT] './CT_TicketType',
				[CT_Source] VARCHAR(50) './CT_Source',
				[CT_VoucherStatus] CHAR(3) './CT_VoucherStatus'
     		) AS A    
   		INNER JOIN @CollTable C ON A.[HQ_Installation_No] = C.Installation_ID    
	    
   		IF @@Error <> 0    
   		BEGIN    
    			SET @IsSuccess = 'error while inserting in to collection_ticket table' -- error while inserting in to Cash In 1P table  
    			GOTO Err    
   		END 
	 END
	 
	 IF @iCollectionTicket_Print IS NOT NULL    
	 BEGIN    
	   	INSERT INTO @Collection_Ticket    
	   	SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/CollectionTicket_Print/Col_Ticket_Print') WITH    
	     	(   
	     		[HQ_Installation_No] [INT] './HQ_Installation_No',
	        	[CT_Barcode] [varchar](50) './CT_Barcode',
				[CT_Value] [money] './CT_Value',
				[CT_Declared_Date] [datetime] './CT_Declared_Date',
				[CT_Printed_Installation_ID] [int] './CT_Printed_Installation_ID',
				[CT_Printed_Collection_ID] [int] './CT_Printed_Collection_ID',
				[CT_Inserted_Installation_ID] [int] './CT_Inserted_Installation_ID',
				[CT_Inserted_Collection_ID] [int] './CT_Inserted_Collection_ID',
				[CT_User_ID] [int] './CT_User_ID',
				[HQ_CT_ID] [int] './CT_ID',
				[CT_IsPromotionalTicket] [BIT] './CT_IsPromotionalTicket',
				[CT_TicketType] [INT] './CT_TicketType',
				[CT_Source] VARCHAR(50) './CT_Source' ,
				[CT_VoucherStatus] CHAR(3) './CT_VoucherStatus'
	     	) AS A    
	   	INNER JOIN @CollTable C ON A.[HQ_Installation_No] = C.Installation_ID    
    
	   	IF @@Error <> 0    
	   	BEGIN    
	    		SET @IsSuccess = 'error while inserting in to collection_ticket table' -- error while inserting in to Cash In 1P table  
	    		GOTO Err    
	   	END 
	 END
 
	 IF @iCollectionTicket IS NOT NULL OR @iCollectionTicket_Print IS NOT NULL 
	 BEGIN
		 DECLARE @Bar_code VARCHAR(50)
		 DECLARE @Value MONEY
		 DECLARE @DecDate DATETIME
		 DECLARE temp_Cursor CURSOR FOR     
		 SELECT CT_Barcode, CT_Value, CT_Declared_Date FROM @Collection_Ticket
		 OPEN temp_Cursor      
		 FETCH NEXT FROM temp_Cursor INTO @Bar_code, @Value, @DecDate
		 WHILE @@FETCH_STATUS = 0    
		 BEGIN    
			 IF NOT EXISTS(SELECT 1 FROM Collection_Ticket C_T WHERE C_T.CT_Barcode = @Bar_code AND C_T.CT_Value = @Value AND C_T.CT_Declared_Date = @DecDate)      
			 BEGIN
				INSERT INTO Collection_Ticket 
				SELECT 
					CT.[CT_Barcode],
					CT.[CT_Value],
					CT.[CT_Declared_Date],
					CASE 
						WHEN CT.[CT_Printed_Installation_ID] IS NULL
							THEN CT.[CT_Printed_Installation_ID]
							ELSE C.Installation_ID
						END AS [Printed_Installation_ID],
					CASE 
						WHEN CT.[CT_Printed_Collection_ID] IS NULL
							THEN CT.[CT_Printed_Collection_ID]
							ELSE C.Collection_ID
						END AS Printed_Collection_ID,
					CASE 
						WHEN CT.[CT_Inserted_Installation_ID] IS NULL
							THEN CT.[CT_Inserted_Installation_ID]
							ELSE C.Installation_ID
						END AS [Inserted_Installation_ID],
					CASE 
						WHEN CT.[CT_Inserted_Collection_ID] IS NULL
						THEN CT.[CT_Inserted_Collection_ID]
						ELSE C.Collection_ID
						END AS [Inserted_Collection_ID],
					CT.[CT_User_ID],
					CT.[HQ_CT_ID],
					CT.[CT_IsPromotionalTicket],
					CT.[CT_TicketType],
					CT.[CT_Source],
					CT.[CT_VoucherStatus] 
				FROM @Collection_Ticket CT
					INNER JOIN @CollTable C ON C.[Installation_ID] = CT.[HQ_Installation_No]
				WHERE
					CT.CT_Barcode = @Bar_code
					AND CT.CT_Value = @Value
					AND CT.CT_Declared_Date = @DecDate
				
				IF @@Error <> 0
				BEGIN
					SET @IsSuccess = 'error while updating the collection_ticket table'  -- error while updating in to Cash In 1P table
				END
			END
			FETCH NEXT FROM temp_Cursor INTO @Bar_code, @Value, @DecDate
		 END
		 CLOSE temp_Cursor
		 DEALLOCATE temp_Cursor
	  END
 END
 ELSE
 BEGIN
	
	IF @iCollectionTicket IS NOT NULL    
	 BEGIN    
   		INSERT INTO @Collection_Ticket    
   		SELECT C.Collection_ID, A.* FROM OPENXML(@idoc, './CollectionDetails/CollectionTicket/Col_Ticket') WITH    
     		(    
     			[HQ_Installation_No] [INT] './HQ_Installation_No',
        		[CT_Barcode] [varchar](50) './CT_Barcode',
				[CT_Value] [money] './CT_Value',
				[CT_Declared_Date] [datetime] './CT_Declared_Date',
				[CT_Printed_Installation_ID] [int] './CT_Printed_Installation_ID',
				[CT_Printed_Collection_ID] [int] './CT_Printed_Collection_ID',
				[CT_Inserted_Installation_ID] [int] './HQ_Installation_No',
				[CT_Inserted_Collection_ID] [int] './CT_Inserted_Collection_ID',
				[CT_User_ID] [int] './CT_User_ID',
				[HQ_CT_ID] [int] './CT_ID',
				[CT_IsPromotionalTicket] [BIT] './CT_IsPromotionalTicket',
				[CT_TicketType] [INT] './CT_TicketType',
				[CT_Source] VARCHAR(50) './CT_Source',  
				[CT_VoucherStatus] CHAR(3) './CT_VoucherStatus'  
     		) AS A    
   		INNER JOIN @CollTable C ON A.[CT_Inserted_Installation_ID] = C.Installation_ID    
	    
   		IF @@Error <> 0    
   		BEGIN    
    			SET @IsSuccess = 'error while inserting in to collection_ticket table' -- error while inserting in to Cash In 1P table  
   		END 
	 END
 
-- INSERT INTO Collection_Ticket SELECT CTT.[CT_Barcode],CTT.[CT_Value],CTT.[CT_Declared_Date],CTT.[CT_Printed_Installation_ID],CTT.[CT_Printed_Collection_ID],CTT.[CT_Inserted_Installation_ID], CTT.[CT_Inserted_Collection_ID],CTT.[CT_User_ID],CTT.[HQ_CT_ID]  FROM collection_ticket CT      
--    INNER JOIN @Collection_Ticket CTT       
--    ON CT.CT_Barcode <> CTT.CT_Barcode  
             
	 DECLARE @Barcode VARCHAR(100)  
	 DECLARE tempCursor CURSOR FOR     
	 SELECT CT_Barcode FROM @Collection_Ticket  
	 OPEN tempCursor      
	 FETCH NEXT FROM tempCursor INTO @Barcode    
	 WHILE @@FETCH_STATUS = 0    
	 BEGIN    
		 IF NOT EXISTS(SELECT 1 FROM collection_ticket CT WHERE CT.CT_Barcode = @Barcode )      
		 BEGIN       
		  INSERT INTO Collection_Ticket SELECT 
		  [CT_Barcode],
		  [CT_Value],
		  [CT_Declared_Date],
		  [CT_Printed_Installation_ID],
		  [CT_Printed_Collection_ID],
		  [CT_Inserted_Installation_ID], 
		  [CT_Inserted_Collection_ID],
		  [CT_User_ID],
		  [HQ_CT_ID],
		  [CT_IsPromotionalTicket],
		  [CT_TicketType],
		  [CT_Source],
		  [CT_VoucherStatus]  
		  FROM @Collection_Ticket WHERE CT_Barcode = @Barcode
 			IF @@Error <> 0    
   			BEGIN    
    				SET @IsSuccess = 'error while updating the collection_ticket table'  -- error while updating in to Cash In 1P table  
   			END   
   			UPDATE Collection_Ticket    
   			 SET   
  			Collection_Ticket.[CT_Inserted_Collection_ID] =  TempCollTic.Collection_No  
   			FROM dbo.Collection_Ticket ColTic  
   			INNER JOIN @Collection_Ticket TempCollTic ON ColTic.CT_Inserted_Installation_ID = TempCollTic.CT_Inserted_Installation_ID 
		  and TempCollTic.HQ_CT_ID = ColTic.HQ_CT_ID  WHERE  ColTic.CT_Barcode =   @Barcode AND  TempCollTic.CT_Barcode =   @Barcode  
		    
   			IF @@Error <> 0    
   			BEGIN
    				SET @IsSuccess = 'error while updating the collection_ticket table'  -- error while updating in to Cash In 1P table
			END
		 END
	 FETCH NEXT FROM tempCursor INTO @Barcode
	 END
	 CLOSE tempCursor
	 DEALLOCATE tempCursor
 END      
EXEC sp_xml_RemoveDocument @idoc  
  
RETURN 0  
  
Err:  
RETURN -99  
  
END  

GO

