USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportInsertedCollectionDeclaration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportInsertedCollectionDeclaration]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportInsertedCollectionDeclaration]
 @Site_Code      VARCHAR(50),  
 @Batch_Id       INT  
  
AS  
BEGIN  
  
 SET NOCOUNT ON  
  
 DECLARE @DecColl XML  
 DECLARE @Param XML  
  
 DECLARE @TotalInstallaitons   INT  
 DECLARE @MaxCollectionExportSize INT  
 DECLARE @NoOfIteration    INT  
 DECLARE @tempBatch     TABLE (SNo int IDENTITY(1,1), Collection_ID int, Batch_ID int)  
 DECLARE @iIndex      INT  
 DECLARE @TempExportData    TABLE(Data XML)  
  
 SET @iIndex=0  
 EXEC rsp_GetSetting 0, 'MaxCollectionExportSize', 50, @MaxCollectionExportSize OUTPUT  
  
 INSERT INTO @TempBatch  
 SELECT  
  [Collection_ID],  
  [Batch_ID]  
 FROM [Collection] C  
  INNER JOIN Installation I ON I.Installation_ID = C.Installation_ID  
 WHERE Batch_ID = @Batch_Id  
  
  
 SELECT @TotalInstallaitons = COUNT(SNo) FROM @TempBatch  
  
 PRINT @TotalInstallaitons  
 PRINT @MaxCollectionExportSize  
  
 --CALCULATE TOTAL NUMBER OF ITERATIONS  
 SELECT @NoOfIteration= @TotalInstallaitons / @MaxCollectionExportSize  +  
   CASE WHEN @TotalInstallaitons%@MaxCollectionExportSize > 0 THEN 1 ELSE 0 END  
  
  
 WHILE (@iIndex < @NoOfIteration)  
 BEGIN  
  INSERT INTO @TempExportData  
   SELECT  
   (  
    SELECT  
       RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5))) AS Batch_ID, -- Get site batch id --B.[Batch_ID],
       B.Batch_Declared,
       I.[Installation_Float_Status],  
       T.[Treasury_ID],
       T.[UserID],  
       T.[Treasury_Type],  
       T.[Treasury_Reason],  
       T.[Treasury_Amount],  
       T.[Treasury_Allocated],  
       RIGHT(C.Collection_Ref, LEN(C.Collection_Ref) - LEN(LEFT(C.Collection_Ref, 5))) AS Collection_ID,
       I.[Installation_ID],  
       [Declaration],  
       [Week_ID],  
       [Period_ID],  
       [Operator_Week_ID],  
       [Operator_Period_ID],  
       [CollectionNoDoorEvents],  
       [CollectionTotalDurationDoor],  
       [CollectionNoPowerEvents],  
       [CollectionTotalDurationPower],  
       [CollectionNoFaultEvents],  
       [Treasury_Total],  
       [Treasury_Refills],  
       [Treasury_Repayments],  
       [Treasury_Tokens],  
       [Collection_Treasury_Handpay],  
       [Collection_Treasury_Defloat],  
       [Cashcollected],  
       [CashRefills],  
       [TokensCollected],  
       [TokenRefills],  
       [Cash_Collected_1p]
       [Cash_Collected_2p],  
       [Cash_Collected_5p],  
       [Cash_Collected_10p],  
       [Cash_Collected_20p],  
       [Cash_Collected_50p],  
       [Cash_Collected_100p],  
       [Cash_Collected_200p],  
       [Cash_Collected_500p],  
       [Cash_Collected_1000p],  
       [Cash_Collected_2000p],  
       [Cash_Collected_5000p],  
       [Cash_Collected_10000p],  
       [Cash_Collected_20000p],  
       [Cash_Collected_50000p],  
       [Cash_Collected_100000p],  
       [Collection_PoP_Actual],  
       [Collection_PoP_Configured],  
       [Collection_NetEx],  
       [Collection_VAT_Rate],  
       [Collection_Meters_CoinsIn],  
       [Collection_Meters_CoinsOut],  
       [Collection_Meters_CoinsDrop],  
       [Collection_Meters_Handpay],  
       [Collection_Meters_ExternalCredit],  
       [Collection_Meters_GamesBet],  
       [Collection_Meters_GamesWon],  
       [Collection_Meters_Notes],  
       [CounterCashIn],  
       [PreviousCounterCashIn],  
       [CounterCashOut],  
       [PreviousCounterCashOut],  
       [CounterTokensIn],  
       [PreviousCounterTokensIn],  
       [CounterTokensOut],  
       [PreviousCounterTokensOut],  
       [CounterPrize],  
       [PreviousCounterPrize],  
       [CounterJukeBoxPlay],  
       [PreviousCounterJukebox],  
       [CounterTournamentPlay],  
       [PreviousCounterTournament],  
       [CounterRefill],  
       [PreviousCounterRefills],  
       [DeclaredTicketValue],  
       [DeclaredTicketQty],  
       [DeclaredTicketPrintedValue],  
       [DeclaredTicketPrintedQty],  
       [Progressive_Value_Declared],
       C.[User_Name]
       
    FROM [Batch] B  
     INNER JOIN [Collection] C ON C.[Batch_ID] = B.[Batch_ID]  
     INNER JOIN [Installation] I ON I.[Installation_ID] = C.[Installation_ID]  
     INNER JOIN [Bar_Position] BP ON BP.[Bar_Position_ID] = I.[Bar_Position_ID]  
     INNER JOIN [Site] S ON S.[Site_ID] = BP.[Site_ID]  
     LEFT JOIN [Treasury_Entry] T ON T.[Collection_ID] = C.[Collection_ID]  
     AND C.Collection_ID IN  
        (SELECT Collection_ID  
         FROM @TempBatch  
         WHERE SNo BETWEEN  
          (@iIndex * @MaxCollectionExportSize) + 1 AND (@MaxCollectionExportSize + (@iIndex * @MaxCollectionExportSize))  
        )  
    WHERE B.[Batch_ID] = @Batch_Id  
       AND S.[Site_Code] = LTRIM(RTRIM(@Site_Code))  
    ORDER BY C.[Collection_ID]  
    --FOR XML AUTO,ELEMENTS ,ROOT('Batch'))  
    FOR XML PATH('DECCOLL'), TYPE, ELEMENTS, ROOT('DECCOLLECTIONS')  
  )  
  
  SET @iIndex = @iIndex + 1  
 END  
  
 SELECT * FROM @TempExportData  
END  

GO

