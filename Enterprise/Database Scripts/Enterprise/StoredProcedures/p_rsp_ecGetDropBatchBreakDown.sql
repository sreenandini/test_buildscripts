USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_ecGetDropBatchBreakDown'
   )
    DROP PROCEDURE dbo.rsp_ecGetDropBatchBreakDown
GO
--rsp_ecGetDropBatchBreakDown 1,1

CREATE PROCEDURE dbo.rsp_ecGetDropBatchBreakDown
	@Site_ID  INT = 0,
	@Batch_ID INT = 0
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	DECLARE @IsAFTIncludedInCalculation BIT
	DECLARE @sAddShortpay               VARCHAR(10)
	
	SELECT @sAddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut' 
	
	SELECT @IsAFTIncludedInCalculation = CASE 
	                                          WHEN Setting_value = 'True' THEN 1
	                                          ELSE 0
	                                     END
	FROM   Setting
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'  
	
	
	
	SELECT DISTINCT CD.IsSAS,
	       CD.Collection_ID,
	       CD.Installation_ID,
	       ISNULL(ZN.Zone_Name,'') Zone_Name,
	       CD.PosName,
	       CD.MachineName,
	       CD.StockNo,
	       --CD.Collection_Days,
	       --DecWinOrLoss----------------------------------------
	       Declared_Notes + DecTicketBalance - DecHandpay + Net_Coin + (
	           CASE 
	                WHEN @IsAFTIncludedInCalculation = 1 THEN (CD.DecEftIn / 100.00)
	                     -(CD.DecEftOut / 100.00)
	                ELSE 0
	           END
	       ) AS DecWinOrLoss,
	       --MeterWinOrLoss----------------------------------------
	       RDC_Notes + (DecTicketBalance -Ticket_Var) -
	       RDCHandpay + RDC_Coins + (
	           CASE 
	                WHEN @IsAFTIncludedInCalculation = 1 THEN (CD.EftIn / 100.00) 
	                     -(CD.EftOut / 100.00)
	                ELSE 0
	           END
	       ) AS MeterWinOrLoss,
	       --TakeVariance (DecWinOrLoss - MeterWinOrLoss)----------------------------------------
	       --DecWinOrLoss
	       (
	           Declared_Notes + DecTicketBalance - DecHandpay + Net_Coin + (
	               CASE 
	                    WHEN @IsAFTIncludedInCalculation = 1 THEN (CD.DecEftIn / 100.00)
	                         -(CD.DecEftOut / 100.00)
	                    ELSE 0
	               END
	           )
	       ) -
	       --MeterWinOrLoss
	       (
	           RDC_Notes + (DecTicketBalance -Ticket_Var) -
	           RDCHandpay + RDC_Coins + (
	               CASE 
	                    WHEN @IsAFTIncludedInCalculation = 1 THEN (CD.EftIn / 100.00) 
	                         -(CD.EftOut / 100.00)
	                    ELSE 0
	               END
	           )
	       ) AS TakeVariance,
	       CD.Handle,
	       --nCasino ( (HANDLE-MeterWinOrLoss)/HANDLE  ) *100)
	       CASE 
	            WHEN CD.Handle > 0 THEN (
	                     (
	                         CD.Handle -
	                         --START MeterWinOrLoss
	                         (
	                             RDC_Notes + (DecTicketBalance -Ticket_Var) -
	                             RDCHandpay + RDC_Coins + (
	                                 CASE 
	                                      WHEN @IsAFTIncludedInCalculation = 1 THEN (CD.EftIn / 100.00) 
	                                           -(CD.EftOut / 100.00)
	                                      ELSE 0
	                                 END
	                             )
	                         )--END MeterWinOrLoss
	                     ) / CD.Handle
	                 ) 
	                 * 100.00
	            ELSE 0.00
	       END AS nCasino,
	       ---nHold (100-nCasino)
	       -- nCasino start 
	       (
	           100 -(
	               CASE 
	                    WHEN CD.Handle > 0 THEN (
	                             (
	                                 CD.Handle -
	                                 --START MeterWinOrLoss
	                                 (
	                                     RDC_Notes + (DecTicketBalance -Ticket_Var) 
	                                     -
	                                     RDCHandpay + RDC_Coins + (
	                                         CASE 
	                                              WHEN @IsAFTIncludedInCalculation 
	                                                   =
	                                                   1 THEN (CD.EftIn / 100.00) 
	                                                   -(CD.EftOut / 100.00)
	                                              ELSE 0
	                                         END
	                                     )
	                                 )--END MeterWinOrLoss
	                             ) / CD.Handle
	                         ) 
	                         * 100.00
	                    ELSE 0.00
	               END 
	               -- nCasino END
	           )
	       ) AS nHold,
	       CD.Declared_Coins,
	       CD.DeFloat,
	       CD.Refills,
	       CD.Refunds,
	       CD.Net_Coin,
	       CD.Datapak_ID,
	       cd.RDC_Coins,
	       CD.RDC_Notes,
	       CD.VTP,
	       CD.Collection_EDC_Status,
	       CD.Coin_Var,
	       CD.Declared_Notes,
	       CD.Note_Var,	       
	       (CD.DecTicketBalance + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE CD.Shortpay END)) DecTicketBalance,
	       CD.Shortpay,
	       CD.Void,
	       (CD.DecTicketBalance - CD.Ticket_Var) AS RDCTickets,
	       (CD.Ticket_Var + (CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE CD.Shortpay END)) Ticket_Var,
	       CD.DecHandpay,
	       CD.RDCHandpay,
	      -- CD.Handpay_Var,
	      CD.DecHandpay-CD.RDCHandpay AS Handpay_Var, 
	       CD.Progressive_Value_Declared,
	       CD.Progressive_Value_Meter,
	       CD.Progressive_Value_Variance,
	       CD.Collection_Total_Power_Duration,
	       CD.Total_Fault_Events,
	       CD.Total_Door_Events,
	       CD.Total_Power_Events,
	       ----- EXISTING -----
	       --CD.*,
	       CDLS.Collection_Days,
	       
	--ISNULL((CASE WHEN ISNULL(CT.CT_IsPromotionalTicket,0) = 1 THEN 
	--        CASE WHEN CT.CT_TicketType=0 THEN
	--			CAST(ISNULL(CT.CT_Value,0.0) AS REAL)
	--		END
	--END),0) AS PromoCashableIn,
	
	ISNULL(CD.PromoCashableIn, 0) AS PromoCashableIn,
	
	--ISNULL((CASE WHEN ISNULL(CT.CT_IsPromotionalTicket,0) = 1 THEN 
	--        CASE WHEN CT.CT_TicketType=1 THEN
	--			CAST(ISNULL(CT.CT_Value,0.0) AS REAL)
	--		END
	--END),0) AS PromoNonCashableIn
	ISNULL(CD.PromoNonCashableIn, 0) AS PromoNonCashableIn
	
	FROM   VW_CollectionData CD
	       INNER JOIN Bar_Position
	            ON  CD.Bar_Position_ID = Bar_Position.Bar_Position_ID
	       LEFT JOIN [Zone] ZN
	            ON  Bar_Position.Zone_ID = ZN.Zone_ID
	                --       JOIN SITE WITH (NOLOCK)
	                --            ON  CD.Site_Id = SITE.Site_ID
	                --       JOIN Sub_Company WITH (NOLOCK)
	                --            ON  SITE.Sub_Company_ID = Sub_Company.Sub_Company_ID
	                --       JOIN Company WITH (NOLOCK)
	                --            ON  Company.Company_ID = Sub_Company.Company_ID
	                --       LEFT JOIN Sub_Company_Area WITH (NOLOCK)
	                --            ON  SITE.Sub_Company_Area_ID = Sub_Company_Area.Sub_Company_Area_ID
	                --       LEFT JOIN Sub_Company_District WITH (NOLOCK)
	                --            ON  SITE.Sub_Company_District_ID = Sub_Company_District.Sub_Company_District_ID
	                --       LEFT JOIN Sub_Company_Region WITH (NOLOCK)
	                --            ON  SITE.Sub_Company_Region_ID = Sub_Company_Region.Sub_Company_Region_ID
	                
	       INNER JOIN VW_CollectionDetails CDLS
	            ON  CDLS.Collection_ID = CD.Collection_ID
	       LEFT Join Collection_Ticket CT
	            ON CT.CT_Inserted_Collection_ID=CD.Collection_ID
	WHERE  CD.site_ID = @Site_ID
	       AND CD.Batch_ID =@Batch_ID
	ORDER BY
	       CD.PosName
END
GO
