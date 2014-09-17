/************************************************************
 * exec rsp_Report_WeeklyDropException 48, 722
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_WeeklyDropException]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_WeeklyDropException]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_Report_WeeklyDropException(@siteId INT, @WeekId INT)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	SELECT SITE.site_id,
	       SITE.Site_Code,
	       SITE.Site_Name,
	       MACHINE.Machine_Stock_No,
	       VW_CollectionData.Week_ID,
	       VW_CollectionData.PosName,
	       VW_CollectionData.MachineName,
	       VW_CollectionData.Defloat,
	       VW_CollectionData.Refills,
	       VW_CollectionData.Refunds,
	       VW_CollectionData.VTP,
	       VW_CollectionData.PercentageOut,
	       VW_CollectionData.PercentageIn,
	       VW_CollectionData.DecHandpay,
	       VW_CollectionData.Handpay_Var,
	       VW_CollectionData.Declared_Coins,
	       VW_CollectionData.Declared_Notes,
	       VW_CollectionData.Declared_Tickets,
	       VW_CollectionData.Note_Var,
	       VW_CollectionData.Net_Coin,
	       VW_CollectionData.Coin_Var,
	       VW_CollectionData.Ticket_Var + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE VW_CollectionData.Shortpay END) AS Ticket_Var,
	       VW_CollectionData.Progressive_Value_Declared,
	       VW_CollectionData.Progressive_Value_Variance,
	       VW_CollectionData.DeclaredTicketPrintedValue + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN VW_CollectionData.Shortpay ELSE 0 END) AS DeclaredTicketPrintedValue,
	       VW_CollectionData.DecWinLoss AS DecWinLoss,
	       VW_CollectionData.MeterWinLoss,
	       Calendar_Week.Calendar_Week_Number,
	       Calendar_Week.Calendar_Week_Start_Date,
	       Calendar_Week.Calendar_Week_End_Date,
	       VW_CollectionData.Shortpay,
	       Machine_Type.Machine_Type_Code
	FROM   (
	           (
	               (
	                   (
	                       dbo.Site SITE INNER JOIN dbo.VW_CollectionData 
	                       VW_CollectionData ON
	                       SITE.Site_ID = VW_CollectionData.Site_ID
	                   )
	                   INNER JOIN Installation Installation ON
	                   VW_CollectionData.Installation_ID = Installation.Installation_ID
	               )
	               LEFT OUTER JOIN dbo.Calendar_Week Calendar_Week ON
	               VW_CollectionData.Week_ID = Calendar_Week.Calendar_Week_ID
	           )
	           INNER JOIN MACHINE MACHINE ON
	           Installation.Machine_ID = MACHINE.Machine_ID
	       )
	       INNER JOIN Machine_Type Machine_Type
	            ON  MACHINE.Machine_Category_ID = Machine_Type.Machine_Type_ID
	WHERE  VW_CollectionData.Site_ID = @siteId
	       AND VW_CollectionData.Week_ID = @weekid
	ORDER BY
	       Machine_Type.Machine_Type_Code ASC,
	       VW_CollectionData.PosName ASC
END
GO


