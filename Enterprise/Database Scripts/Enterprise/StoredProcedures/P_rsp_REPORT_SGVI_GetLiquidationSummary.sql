USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_SGVI_GetLiquidationSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_SGVI_GetLiquidationSummary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_SGVI_GetLiquidationSummary]   
@Site_Id INT, 
@Batch_No INT = 0    
AS      
  
  SET DATEFORMAT dmy   
  SET NOCOUNT ON  
  
  DECLARE @RetailNegativeNet AS FLOAT,  
          @SETTINGVALUE      AS FLOAT  
  
 -- the below declaration was added by Sudarsan S on 08-09-2008  
  DECLARE @CollectionBatchAdvance FLOAT  
  DECLARE @Handpay REAL  
  DECLARE @JACKPOT REAL  
 -- chnage ends  
  
  IF @Batch_No = 0   
    RETURN (0) -- shouldn't happen  
  
  --setting value is 0.22 for SGVI_Batch_Net_Value  
  EXEC [dbo].[rsp_GetSiteSetting] @Site_ID, 'SGVI_Batch_Net_Value', @SETTINGVALUE OUTPUT
	SELECT @SETTINGVALUE = COALESCE (@SETTINGVALUE, 0.22)
  
SELECT @RetailNegativeNet=ISNULL(BATCH_NEGATIVE_NET, 0) FROM BATCH WHERE BATCH_ID =
	(SELECT max(BATCH_ID) from batch where Batch_Id< @Batch_No )

  IF (@RetailNegativeNet >= 0  OR @RetailNegativeNet IS NULL)
    SET @RetailNegativeNet = 0       
  
  
-- For populating Collection_Batch_Advance based on jackpot and handpay values if it is  > 5000  
  
  -- test for "Collection_Batch_Advance" being null, should be null the first time we get the data   
  SELECT @CollectionBatchAdvance = CB.Collection_Batch_Advance  
    FROM Batch CB  
   WHERE CB.Batch_Id = @Batch_No  
  
  -- if null populate the value with all hp/jp over 5000  
  If @CollectionBatchAdvance is null  
  BEGIN  
    -- get value of handpay  
    SELECT @Handpay = SUM(Treasury_Amount)  
      FROM Treasury_Entry T  
      JOIN VW_S_Collectiondata VWCD ON  VWCD.Collection_Id = T.Collection_ID  
     WHERE T.TREASURY_TYPE IN ('Handpay Credit', 'Attendantpay Credit') -- included the type Attendantpay Credit to calculate Advance to Retailer value
       AND VWCD.Batch_Id = @Batch_No  
       And Treasury_Amount > 5000  
  
    -- get value of jackpot  
    SELECT @JACKPOT = SUM(Treasury_Amount)  
      FROM Treasury_Entry T  
      JOIN VW_S_Collectiondata VWCD ON  VWCD.Collection_Id = T.Collection_ID
     WHERE T.TREASURY_TYPE IN ('Handpay Jackpot', 'Attendantpay Jackpot', 'Mystery Jackpot', 'PROGRESSIVE', 'PROG') -- -- included the type Attendantpay Jackpot to calculate Advance to Retailer value
       AND VWCD.Batch_Id = @Batch_No  
       AND Treasury_Amount > 5000  
  
    SET @CollectionBatchAdvance = COALESCE(@Handpay,0) + COALESCE(@JACKPOT,0)  
  
    -- set the initial value  
    UPDATE Batch  
       SET Collection_Batch_Advance = @CollectionBatchAdvance  
     WHERE Batch_Id=@Batch_No  
  
  END  
--Ends Collection_Batch_Advance  
  
    SELECT VWCD.Batch_Id as BatchNo,VWCD.[Collection_Performed_Date] AS Date_Collected, --Batch Date  
           VWCD.[Site_Name] AS Retailer_Name, --Retailer Name  
         SUM(VWCD.[CashIn]- VWCD.[RDC_Total_Coinage_In]) AS Gross,--1 Gross (Meters In)  
         SUM(VWCD.[CashOut]) AS Tickets_Expected,--2 Tickets Expected (Meters Out)  
         SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) AS Net ,--3 Net  
         (SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE) AS Net_Percentage,--4 Net x Percentage  
         @SETTINGVALUE AS Percentage_Setting,  
         @RetailNegativeNet AS Retail_Negative_Net,--5 Retailers Negative Net   
         ((SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE)+ @RetailNegativeNet) AS Retailer_Share,--6 Retailers Share(4+5)  
         SUM(VWCD.[CashOut]) AS Tickets_Paid,--7 Tickets Paid(2)  
         COALESCE(CB.Collection_Batch_Advance,0) AS Advance_To_Retailer,--8 Advance to Retailer   
         CASE   
          WHEN ((SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE)+ @RetailNegativeNet) > 0 THEN       
           ((SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE)+ @RetailNegativeNet)+(SUM(VWCD.[CashOut])-COALESCE(CB.Collection_Batch_Advance,0))  
          Else      
           (SUM(VWCD.[CashOut])-COALESCE(CB.Collection_Batch_Advance,0))  
          END  as Retailer,--9 --if Retailer's Share>0 then Retailer=(6+7-8) else <0 then Retailer=(7-8)  
           CASE   
           WHEN ((SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE)+ @RetailNegativeNet) > 0 THEN --if Retailer's Share>0 =(3-4)+(8-5) else <0 =(3+8)  
           (SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In])-(SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In]) * @SETTINGVALUE)) + (COALESCE(CB.Collection_Batch_Advance,0)-@RetailNegativeNet)  
          Else  
           (SUM(VWCD.[CashTake]- VWCD.[RDC_Total_Coinage_In])+COALESCE(CB.Collection_Batch_Advance,0))  
           END as Balance_Due --10 if Retailer's Share>0 then Balance_Due=(6+7-8) else <0 then Balance_Due=(7-8)  
  
      FROM VW_S_Collectiondata VWCD  
  
      JOIN Batch CB   
        ON CB.Batch_Id = VWCD.Batch_Id 
  
     WHERE CB.Batch_Id = @Batch_No  
  
  GROUP BY VWCD.[Collection_Performed_Date],  
           VWCD.[Site_Name],  
           CB.Collection_Batch_Advance ,
VWCD.Batch_Id

GO

