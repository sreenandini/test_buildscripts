USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_BatchWinLoss]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].rsp_Report_BatchWinLoss
GO
  
CREATE PROCEDURE rsp_Report_BatchWinLoss(@BatchNo INT, @SiteID INT, @IsWeek BIT)
AS
	--- =======================================================================
	---
	--- Revision History
	---
	---Aishwarrya V S 09/05/13 Created
	-- exec rsp_Report_BatchWinLoss 0
	---------------------------------------------------------------------------   
BEGIN
	SET NOCOUNT ON  
	
	DECLARE @sAddShortpay VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	SELECT VW.Batch_ID,
	       VW.Collection_Date_Of_Collection,
	       VW.Site_ID,
	       VW.PosName,
	       VW.MachineName,
	       VW.BatchRef,
	       ISNULL(VW.Cash_Collected_1p,0) AS [Cash_Collected_1p],
	       ISNULL(VW.Cash_Collected_2P,0) Cash_Collected_2P,
	       ISNULL(VW.Cash_Collected_5P,0) Cash_Collected_5P,
	       ISNULL(VW.Cash_Collected_10P,0) Cash_Collected_10P,
	       ISNULL(VW.Cash_Collected_20P,0) Cash_Collected_20P,
	       ISNULL(VW.Cash_Collected_50P,0) Cash_Collected_50P,
	       ISNULL(VW.Cash_Collected_100P,0) Cash_Collected_100P,
	       ISNULL(VW.Cash_Collected_200P,0) Cash_Collected_200P,
	       ISNULL(VW.Cash_Collected_500P,0) Cash_Collected_500P,
	       ISNULL(VW.Cash_Collected_1000P,0) Cash_Collected_1000P,
	       ISNULL(VW.Cash_Collected_2000P,0) Cash_Collected_2000P,
	       ISNULL(VW.Cash_Collected_5000P,0) Cash_Collected_5000P,
	       ISNULL(VW.Cash_Collected_10000p,0) Cash_Collected_10000p,
	       VW.Refills,
	       VW.Refunds,
	       VW.DecHandpay,
	       VW.RDC_Coins_Out,
	       VW.Declared_Tickets,
	       VW.Tickets_Printed + (
	           CASE 
	                WHEN ISNULL(@sAddShortpay, 'True') = 'True' THEN VW.Shortpay
	                ELSE 0
	           END
	       ) AS Tickets_Printed,
	       VW.Shortpay,
	       VW.Void,
	       VW.Progressive_Value_Declared,
	       (CAST(ISNULL(VW.DecEftIn,0) AS FLOAT)/100) AS DecEftIn,
	       VW.DecWinLoss AS DecWinLoss,
	       B.Batch_Date,
	       B.Batch_User_Name,
	       S.Site_Code,
	       S.Site_Name,
	       MAC.Machine_Stock_No,
	       MT.Machine_Type_Code,
	       Calendar_Week.Calendar_Week_Number,
	       Calendar_Week.Calendar_Week_Start_Date,
	       Calendar_Week.Calendar_Week_End_Date,
	       VW.batch_name	       
	FROM   VW_CollectionData VW WITH(NOLOCK)
	       LEFT OUTER JOIN SITE S WITH(NOLOCK)
	            ON  VW.Site_ID = S.Site_ID
	       INNER JOIN Installation I WITH(NOLOCK)
	            ON  VW.Installation_ID = I.Installation_ID
	       LEFT OUTER JOIN Batch B WITH(NOLOCK)
	            ON  VW.Batch_ID = B.Batch_ID
	       LEFT JOIN dbo.Calendar_Week Calendar_Week WITH(NOLOCK)
	            ON  VW.Week_ID = Calendar_Week.Calendar_Week_ID
	       INNER JOIN MACHINE MAC WITH(NOLOCK)
	            ON  I.Machine_ID = MAC.Machine_ID
	       INNER JOIN Machine_Type MT WITH(NOLOCK)
	            ON  MAC.Machine_Category_ID = MT.Machine_Type_ID
	WHERE  Vw.Site_ID = @SiteID
	       AND (
	               (@IsWeek = 1 AND Vw.Week_ID = @BatchNo)
	               OR (@IsWeek = 0 AND Vw.Batch_ID = @BatchNo)
	           )
	ORDER BY
	       MT.Machine_Type_Code ASC,
	       VW.POSNAME ASC
END
GO


