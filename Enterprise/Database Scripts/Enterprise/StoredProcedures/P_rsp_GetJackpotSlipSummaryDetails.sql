USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetJackpotSlipSummaryDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetJackpotSlipSummaryDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description: Get Jackpot Slip Summary Details for Reports        
-- Inputs:                  
--                
-- Outputs:     Result Set - Jackpot Summary Details  
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Kirubakar S	12/05/2010	Created
-- Yoganandh P  21/05/2010  Modified to include asset number  
-- Yoganandh P  28/05/2010	Modified to return Handpay as Attendantpay  
-- Yoganandh P	01/06/2010	Modified date time fetching logic
-- Yoganandh P	04/06/2010	Modified to include un-processed jackpot information also
-- Yoganandh P	16/06/2010	Modified to include Status - "Voided" and Jackpot Type - "Manual"
-- Yoganandh P	08/07/2010	Modified to exclude 'Shortpay' and Ordered by Date & Time
-- Yoganandh P	21/01/2011	Modified to fetch Treasury_User as EmpName
-- Jisha Lenu	03/03/2011	Modified the TreasuryDateTime as Null. Fix for #96,033  
-- Akshaya Sree 20/05/2014  Added Region,Area and district filter
------------------------------------------------------------------------------------------------------        
 
CREATE PROCEDURE rsp_GetJackpotSlipSummaryDetails
	@Company INT =0,
	@SubCompany INT =0,
	@Region INT =0,
	@Area INT =0,
	@District INT =0,
	@Site INT =0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@ShowHandpay BIT ,
	@ShowJackpot BIT,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	IF @Company = 0
	    SET @Company = NULL
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	IF @Site = 0
	    SET @Site = NULL 
	
	SELECT *
	FROM   (
	           SELECT tS.Site_Name AS 'SiteName',
	                  --tT.Treasury_ID AS Sequence,    
	                  tT.HQ_Treasury_No AS Sequence,
	                  CASE 
	                       WHEN tT.Treasury_Reason_Code = 0 THEN CAST(tT.Treasury_Date + ' ' + tT.Treasury_Time AS DATETIME)
	                       ELSE tt.Treasury_VoidedDate
	                  END AS 'TreasuryDateTime',
	                  -- CAST(tT.Treasury_Date + ' ' + tT.Treasury_Time AS DateTime) AS 'TreasuryDateTime',         
	                  tT.Treasury_Actual_Date AS ActualTreasuryDate,
	                  tM.Machine_Manufacturers_Serial_No AS Slot,
	                  tM.Machine_Stock_No AS AssetNo,
	                  CAST(tI.Installation_Price_Per_Play / 100.0 AS FLOAT) AS 
	                  Denom,
	                  CASE 
	                       WHEN (
	                                tT.Treasury_Type = 'Progressive'
	                                OR tT.Treasury_Type = 'Prog'
	                                OR tT.Treasury_Type =
	                                   'AttendantPay Progressive'
	                            )
	           AND tT.IsManualAttendantPay = 0 THEN 'Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 0 THEN 'Mystery Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Progressive'
	                   OR tT.Treasury_Type = 'Prog'
	                   OR tT.Treasury_Type = 'AttendantPay Progressive'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 
	               'Manual Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Mystery Jackpot' 
	               ELSE tT.Treasury_Type 
	               END AS 'JackpotType',
	           ISNULL(tT.UserID, 0) AS EmpId,
	           ISNULL(tU.UserName,'') AS EmpName,
	           CASE tT.Treasury_Reason_Code
	                WHEN 0 THEN (tT.Treasury_Amount)
	                ELSE -(tT.Treasury_Amount)
	           END AS SlipAmount,
	           CASE tT.Treasury_Reason_Code
	                WHEN 0 THEN 'Processed'
	                ELSE 'Voided'
	           END AS 'Status' 
	           FROM 
	           Treasury_Entry tT 
	           INNER JOIN 
	           Installation tI ON tT.Installation_ID = tI.Installation_ID 
	           INNER JOIN 
	           MACHINE tM ON tI.Machine_ID = tM.Machine_ID 
	           INNER JOIN 
	           Bar_Position tB ON tB.Bar_Position_ID = tI.Bar_Position_ID 
	           INNER JOIN 
	           SITE tS ON tS.Site_ID = tB.Site_ID 
	           LEFT OUTER JOIN 
	           [User] tU ON	           
	           tT.UserID = tU.SecurityUserID
	           INNER JOIN Sub_Company SC ON tS.Sub_Company_ID = SC.Sub_Company_ID 
	           INNER JOIN Company C ON c.Company_ID = SC.Company_ID 
	           WHERE 
	           (
	               CONVERT(VARCHAR, tT.Treasury_Actual_Date, 20) BETWEEN CONVERT(VARCHAR, @startdate, 20) 
	               AND CONVERT(VARCHAR, @enddate, 20)
	               AND CONVERT(
	                       VARCHAR,
	                       CAST(Treasury_Date + ' ' + Treasury_time AS DATETIME),
	                       20
	                   ) <= CONVERT(VARCHAR, @enddate, 20)
	           )
	           AND (
	                   (@ShowHandpay = 1)
	                   OR (
	                          @ShowHandpay <> 1
	                          AND treasury_type IN ('AttendantPay Jackpot', 
	                                               'Handpay Jackpot', 
	                                               'Mystery Jackpot', 
	                                               'Progressive', 'Prog', 
	                                               'AttendantPay Progressive')
	                      )
	               )
	           AND (
	                   (@ShowJackpot = 1)
	                   OR (
	                          @ShowJackpot <> 1
	                          AND treasury_type IN ('AttendantPay Credit', 'Handpay Credit')
	                      )
	               )
	           AND UPPER(tT.Treasury_Type) <> 'FLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'DEFLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'SHORTPAY'
	           AND UPPER(tT.Treasury_Type) <> 'Offline Voucher-Shortpay'
	           AND ISNULL(@Site, tS.Site_id) = tS.Site_ID
	           AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	           AND ISNULL(@SubCompany, tS.Sub_Company_Id) = tS.Sub_Company_Id
	           AND ISNULL(@Region, tS.Sub_Company_Region_ID) = tS.Sub_Company_Region_ID
	           AND ISNULL(@Area, tS.Sub_Company_Area_ID) = tS.Sub_Company_Area_ID
	           AND ISNULL(@District, tS.Sub_Company_District_ID) = tS.Sub_Company_District_ID  
	           AND       
	               (
	                   (tT.Treasury_Reason_Code = 0 AND tT.Treasury_Amount >= 0)
	                   OR (tT.Treasury_Reason_Code <> 0 AND tT.Treasury_Amount <= 0)
	               )
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND tS.Site_ID IN (SELECT DATA
	                                      FROM   fnSplit(@SiteIDList, ','))
	               )
	               
	               UNION  
	               SELECT tS.Site_Name AS 'SiteName',
	                      --tT.Treasury_ID AS Sequence,    
	                      tT.HQ_Treasury_No AS Sequence,
	                      CAST(tT.Treasury_Date + ' ' + tT.Treasury_Time AS DATETIME) AS 
	                      'TreasuryDateTime',
	                      -- CAST(tT.Treasury_Date + ' ' + tT.Treasury_Time AS DateTime) AS 'TreasuryDateTime',         
	                      tT.Treasury_Actual_Date AS ActualTreasuryDate,
	                      tM.Machine_Manufacturers_Serial_No AS Slot,
	                      tM.Machine_Stock_No AS AssetNo,
	                      CAST(tI.Installation_Price_Per_Play / 100.0 AS FLOAT) AS 
	                      Denom,
	                      CASE 
	                           WHEN (
	                                    tT.Treasury_Type = 'Progressive'
	                                    OR tT.Treasury_Type = 'Prog'
	                                    OR tT.Treasury_Type =
	                                       'AttendantPay Progressive'
	                                )
	           AND tT.IsManualAttendantPay = 0 THEN 'Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 0 THEN 'Mystery Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Progressive'
	                   OR tT.Treasury_Type = 'Prog'
	                   OR tT.Treasury_Type = 'AttendantPay Progressive'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 
	               'Manual Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Mystery Jackpot' 
	               ELSE tT.Treasury_Type 
	               END AS 'JackpotType',
	           ISNULL(tT.UserID, 0) AS EmpId,
	           ISNULL(tU.UserName,'') AS EmpName,
	           (tT.Treasury_Amount) AS SlipAmount,
	           'Processed' AS 'Status' 
	           FROM 
	           Treasury_Entry tT 
	           INNER JOIN 
	           Installation tI ON tT.Installation_ID = tI.Installation_ID 
	           INNER JOIN 
	           MACHINE tM ON tI.Machine_ID = tM.Machine_ID 
	           INNER JOIN 
	           Bar_Position tB ON tB.Bar_Position_ID = tI.Bar_Position_ID 
	           INNER JOIN 
	           SITE tS ON tS.Site_ID = tB.Site_ID 
	           LEFT OUTER JOIN 
	           [User] tU ON 
	           tT.UserID = tU.SecurityUserID
	           INNER JOIN Sub_Company SC ON tS.Sub_Company_ID = SC.Sub_Company_ID 
	           INNER JOIN Company C ON c.Company_ID = SC.Company_ID 
	           WHERE 
	           (
	               CONVERT(VARCHAR, tT.Treasury_Actual_Date, 20) BETWEEN CONVERT(VARCHAR, @startdate, 20) 
	               AND CONVERT(VARCHAR, @enddate, 20)
	           )
	           AND (
	                   (
	                       CONVERT(VARCHAR, @enddate, 20) > CONVERT(
	                           VARCHAR,
	                           CAST(Treasury_Date + ' ' + Treasury_time AS DATETIME),
	                           20
	                       )
	                   )
	                   AND (
	                           CONVERT(VARCHAR, @enddate, 20) < CONVERT(VARCHAR, Treasury_VoidedDate, 20)
	                       )
	               )
	           AND (
	                   (@ShowHandpay = 1)
	                   OR (
	                          @ShowHandpay <> 1
	                          AND treasury_type IN ('AttendantPay Jackpot', 
	                                               'Handpay Jackpot', 
	                                               'Mystery Jackpot', 
	                                               'Progressive', 'Prog', 
	                                               'AttendantPay Progressive')
	                      )
	               )
	           AND (
	                   (@ShowJackpot = 1)
	                   OR (
	                          @ShowJackpot <> 1
	                          AND treasury_type IN ('AttendantPay Credit', 'Handpay Credit')
	                      )
	               )
	           AND UPPER(tT.Treasury_Type) <> 'FLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'DEFLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'SHORTPAY'
	           AND UPPER(tT.Treasury_Type) <> 'Offline Voucher-Shortpay'
	           AND ISNULL(@Site, tS.Site_id) = tS.Site_ID
	           AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	           AND ISNULL(@SubCompany, tS.Sub_Company_Id) = tS.Sub_Company_Id
	           AND ISNULL(@Region, tS.Sub_Company_Region_ID) = tS.Sub_Company_Region_ID
	           AND ISNULL(@Area, tS.Sub_Company_Area_ID) = tS.Sub_Company_Area_ID
	           AND ISNULL(@District, tS.Sub_Company_District_ID) = tS.Sub_Company_District_ID 
	           AND tT.Treasury_Amount >= 0
	           AND tT.Treasury_Reason_Code <> 0
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND tS.Site_ID IN (SELECT DATA
	                                      FROM   fnSplit(@SiteIDList, ','))
	               )
	               
	               UNION    
	               
	               SELECT tS.Site_Name AS 'SiteName',
	                      0 AS Sequence,
	                      NULL AS 'TreasuryDateTime',
	                      tE.TE_Date AS ActualTreasuryDate,
	                      tM.Machine_Manufacturers_Serial_No AS Slot,
	                      tM.Machine_Stock_No AS AssetNo,
	                      CAST(tI.Installation_Price_Per_Play / 100.0 AS FLOAT) AS 
	                      Denom,
	                      CASE tE.TE_HP_Type
	                           WHEN 'Prog' THEN 'Progressive Jackpot'
	                           WHEN 'Handpay' THEN 'Attendantpay Credit'
	                           WHEN 'Jackpot' THEN 'Attendantpay Jackpot'
	                           WHEN 'Mystery' THEN 'Mystery Jackpot'
	                           ELSE tE.TE_HP_Type
	                      END AS 'JackpotType',
	                      0 AS EmpId,
	                      '' AS EmpName,
	                      dbo.compute_decimal(tE.TE_Value) AS SlipAmount,
	                      'Pending' AS STATUS
	               FROM   Ticket_Exception tE
	                      INNER JOIN Installation tI
	                           ON  tE.TE_Installation_No = tI.Installation_ID
	                      INNER JOIN MACHINE tM
	                           ON  tI.Machine_ID = tM.Machine_ID
	                      INNER JOIN Bar_Position tB
	                           ON  tB.Bar_Position_ID = tI.Bar_Position_ID
	                      INNER JOIN SITE tS
	                           ON  tS.Site_ID = tB.Site_ID
	                      INNER JOIN Sub_Company SC
	                           ON  tS.Sub_Company_ID = SC.Sub_Company_ID
	                      INNER JOIN Company C
	                           ON  C.Company_ID = SC.Company_ID
	               WHERE  CONVERT(VARCHAR, tE.TE_Date, 20) >= @StartDate
	                      AND CONVERT(VARCHAR, tE.TE_Date, 20) <= @EndDate
	                      AND ISNULL(@Site, tS.Site_id) = tS.Site_ID
	                      AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	                      AND ISNULL(@SubCompany, tS.Sub_Company_Id) = tS.Sub_Company_Id
	                      AND ISNULL(@Region, tS.Sub_Company_Region_ID) = tS.Sub_Company_Region_ID
	                      AND ISNULL(@Area, tS.Sub_Company_Area_ID) = tS.Sub_Company_Area_ID
	                      AND ISNULL(@District, tS.Sub_Company_District_ID) = tS.Sub_Company_District_ID
	                       
	                      AND (
	                              @SiteIDList IS NOT NULL
	                              AND tS.Site_ID IN (SELECT DATA
	                                                 FROM   fnSplit(@SiteIDList, ','))
	                          )
	                      AND tE.TE_Status_Final_Actual IS NULL
	                      AND tE.TE_TicketNumber IS NULL
	                      AND (
	                              (@ShowHandpay = 1)
	                              OR (
	                                     @ShowHandpay <> 1
	                                     AND te_hp_type IN ('Jackpot', 'Mystery', 
	                                                       'PROG', 
	                                                       'Attendantpay Progressive', 
	                                                       'Attendantpay jackpot')
	                                 )
	                          )
	                      AND (
	                              (@ShowJackpot = 1)
	                              OR (
	                                     @ShowJackpot <> 1
	                                     AND te_hp_type IN ('AttendantPay Credit', 'Handpay')
	                                 )
	                          )
	               UNION
	               SELECT tS.Site_Name AS 'SiteName',
	                      0 AS Sequence,
	                      NULL AS 'TreasuryDateTime',
	                      tT.Treasury_Actual_Date AS ActualTreasuryDate,
	                      tM.Machine_Manufacturers_Serial_No AS Slot,
	                      tM.Machine_Stock_No AS AssetNo,
	                      CAST(tI.Installation_Price_Per_Play / 100.0 AS FLOAT) AS 
	                      Denom,
	                      CASE 
	                           WHEN (
	                                    tT.Treasury_Type = 'Progressive'
	                                    OR tT.Treasury_Type = 'Prog'
	                                    OR tT.Treasury_Type =
	                                       'AttendantPay Progressive'
	                                )
	           AND tT.IsManualAttendantPay = 0 THEN 'Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 0 THEN 'Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 0 THEN 'Mystery Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Progressive'
	                   OR tT.Treasury_Type = 'Prog'
	                   OR tT.Treasury_Type = 'AttendantPay Progressive'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Progressive Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Jackpot'
	                   OR tT.Treasury_Type = 'Handpay Jackpot'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 
	               'Manual Attendantpay Jackpot' 
	               WHEN(
	                   tT.Treasury_Type = 'Attendantpay Credit'
	                   OR tT.Treasury_Type = 'Handpay Credit'
	               )
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Attendantpay Credit' 
	               WHEN tT.Treasury_Type = 'Mystery Jackpot'
	           AND tT.IsManualAttendantPay = 1 THEN 'Manual Mystery Jackpot' 
	               ELSE tT.Treasury_Type 
	               END AS 'JackpotType',
	           0 AS EmpId,
	           '' AS EmpName,
	           tT.Treasury_Amount AS SlipAmount,
	           'Pending' AS 'Status' 
	           FROM 
	           Treasury_Entry tT 
	           INNER JOIN 
	           Installation tI ON tT.Installation_ID = tI.Installation_ID 
	           INNER JOIN 
	           MACHINE tM ON tI.Machine_ID = tM.Machine_ID 
	           INNER JOIN 
	           Bar_Position tB ON tB.Bar_Position_ID = tI.Bar_Position_ID 
	           INNER JOIN 
	           SITE tS ON tS.Site_ID = tB.Site_ID 
	           LEFT OUTER JOIN 
	           [User] tU ON tT.Treasury_User = tU.UserName 
	           INNER JOIN Sub_Company SC ON tS.Sub_Company_ID = SC.Sub_Company_ID 
	           INNER JOIN Company C ON C.Company_ID = SC.Company_ID 
	           WHERE 
	           (
	               tT.Treasury_Actual_Date BETWEEN @startdate AND @enddate
	               AND CONVERT(
	                       VARCHAR,
	                       CAST(Treasury_Date + ' ' + Treasury_time AS DATETIME),
	                       20
	                   ) > CONVERT(VARCHAR, @enddate, 20)
	           )
	           AND (
	                   (@ShowHandpay = 1)
	                   OR (
	                          @ShowHandpay <> 1
	                          AND treasury_type IN ('AttendantPay Jackpot', 
	                                               'Handpay Jackpot', 
	                                               'Mystery Jackpot', 
	                                               'Progressive', 'Prog', 
	                                               'AttendantPay Progressive')
	                      )
	               )
	           AND (
	                   (@ShowJackpot = 1)
	                   OR (
	                          @ShowJackpot <> 1
	                          AND treasury_type IN ('AttendantPay Credit', 'Handpay Credit')
	                      )
	               )
	           AND UPPER(tT.Treasury_Type) <> 'FLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'DEFLOAT'
	           AND UPPER(tT.Treasury_Type) <> 'SHORTPAY'
	           AND ISNULL(@Site, tS.Site_id) = tS.Site_ID
	           AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	           AND ISNULL(@SubCompany, tS.Sub_Company_Id) = tS.Sub_Company_Id
	           AND ISNULL(@Region, tS.Sub_Company_Region_ID) = tS.Sub_Company_Region_ID
	           AND ISNULL(@Area, tS.Sub_Company_Area_ID) = tS.Sub_Company_Area_ID
	           AND ISNULL(@District, tS.Sub_Company_District_ID) = tS.Sub_Company_District_ID
	               
	             
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND tS.Site_ID IN (SELECT DATA
	                                      FROM   fnSplit(@SiteIDList, ','))
	               )
	           AND tT.Treasury_Amount >= 0
	       ) tJSD
	ORDER BY
	       TreasuryDateTime ASC
END
GO

