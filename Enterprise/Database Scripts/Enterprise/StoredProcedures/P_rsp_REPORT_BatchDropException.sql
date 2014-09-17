USE [Enterprise] 
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_BatchDropException]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_BatchDropException]
GO
--- =======================================================================  
---   
--- Revision History  
---   
---Aishwarrya V S 09/05/13 Created  
-- exec rsp_REPORT_BatchDropException 2,1 

---------------------------------------------------------------------------
  
CREATE PROCEDURE rsp_REPORT_BatchDropException(@BatchID INT, @SiteID INT)
AS
BEGIN
	DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	SELECT VW.Batch_ID,
	       VW.PosName,
	       VW.MachineName,
	       VW.BatchRef,
	       VW.Refills,
	       VW.Refunds,
	       VW.VTP,
	       VW.PercentageIn,
	       VW.PercentageOut,
	       VW.DecHandpay,
	       VW.Handpay_Var,
	       VW.Declared_Coins,
	       VW.Declared_Notes,
	       VW.Note_Var,
	       VW.Net_Coin,
	       VW.Coin_Var,
	       VW.Ticket_Balance + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE VW.Shortpay END) AS Ticket_Balance,
	       VW.Ticket_Var + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE VW.Shortpay END) AS Ticket_Var,
	       VW.Progressive_Value_Declared,
	       VW.Progressive_Value_Variance,
	       VW.DecWinLoss AS DecWinLoss,
	       VW.MeterWinLoss,
	       B.Batch_Date,
	       B.Batch_User_Name,
	       S.Site_ID,
	       S.Site_Code,
	       S.Site_Name,
	       VW.Shortpay,
	       MT.Machine_Type_Code
	FROM   VW_CollectionData VW
	       LEFT OUTER JOIN SITE S
	            ON  VW.Site_ID = S.Site_ID
	       INNER JOIN Installation I
	            ON  VW.Installation_ID = I.Installation_ID
	       LEFT OUTER JOIN Batch B
	            ON  VW.Batch_ID = B.Batch_ID
	       INNER JOIN MACHINE M
	            ON  I.Machine_ID = M.Machine_ID
	       INNER JOIN Machine_Type MT
	            ON  M.Machine_Category_ID = MT.Machine_Type_ID
	WHERE  (
	           (@BatchID = 0)
	           OR (@BatchID != 0 AND VW.Batch_ID = @BatchID)
	       )
	       AND ((@SiteID = 0) OR (@SiteID != 0 AND VW.Batch_ID = @BatchID))
	ORDER BY
	       MT.Machine_Type_Code ASC,
	       VW.PosName ASC
END

