/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 14/08/13 16:04:51
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCashierTransactionsDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCashierTransactionsDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetCashierTransactionsDetails]
	@isCDMPaid BIT,
	@isCDMPrinted BIT,
	@isHandPay BIT,
	@isShortPay BIT,
	@isVoidVoucher BIT,
	@isJackpot BIT,
	@isProgressive BIT,
	@isVoid BIT,
	@isMachinePaid BIT,
	@isMachinePrinted BIT,
	@isActive BIT,
	@isVoidCancel BIT,
	@isExpired BIT,
	@isException BIT,
	@isLiability BIT,
	@isPromo BIT,
	@isNonCashableIN BIT,
	@isNonCashableOut BIT,
	@startdate DATETIME,
	@enddate DATETIME ,
	@SITE INT = 0 ,
	@Route_No INT = 0,
	@isOffline BIT
AS
BEGIN
	
	SET DATEFORMAT ymd 
	DECLARE @Site_Code VARCHAR(50)    
	IF (@SITE <> 0)
	BEGIN
	    SELECT @Site_Code = site_code
	    FROM   SITE
	    WHERE  Site_id = @SITE
	END    
	
	DECLARE @NONCashable  VARCHAR(500)                    
	DECLARE @Cashable     VARCHAR(500)                    
	
	EXEC rsp_GetSetting 0,
	     'PROMO_TICKET_CODE',
	     '8',
	     @Cashable OUTPUT
	
	EXEC rsp_GetSetting 0,
	     'PROMO_TICKET_CODE_NONCASH',
	     '7',
	     @NONCashable OUTPUT        
	
	SELECT Trans_Type,
	       PrintAsset,
	       PrintSiteCode,
	       PrintPosition,
	       PrintedDate,
	       PaidAsset,
	       PaidPosition,
	       PaidDate,
	       Amount,
	       Ticket
	FROM   (
	           SELECT --treasury_date,  isnull(treasury_reason,'')as treasury_reason,
	                  --zone_name,   
	                  CASE 
	                       WHEN Treasury_Type = 'Progressive'
	           AND IsManualAttendantPay = 0
	           AND treasury_reason_code <> 0 THEN 'VOID (Progressive Jackpot)' 
	               WHEN Treasury_Type = 'Shortpay'
	           AND IsManualAttendantPay = 0
	           AND @isVoid = 1
	           AND treasury_reason = 'NEGATIVE TREASURY ENTRY' THEN 
	               'VOID (Shortpay)'
	               WHEN Treasury_Type = 'AttendantPay Jackpot'
	           AND IsManualAttendantPay = 0
	           AND treasury_reason_code <> 0 THEN 'VOID (Attendantpay Jackpot)' 
	               WHEN Treasury_Type = 'AttendantPay Credit'
	           AND IsManualAttendantPay = 0
	           AND treasury_reason_code <> 0 THEN 'VOID (Attendantpay Credit)' 
	               WHEN Treasury_Type = 'Mystery Jackpot'
	           AND IsManualAttendantPay = 0
	           AND treasury_reason_code <> 0 THEN 'VOID (Mystery Jackpot)' 
	               WHEN Treasury_Type = 'Progressive'
	           AND IsManualAttendantPay = 1
	           AND treasury_reason_code <> 0 THEN 
	               'VOID (Manual Progressive Jackpot)' 
	               WHEN Treasury_Type = 'AttendantPay Jackpot'
	           AND IsManualAttendantPay = 1
	           AND treasury_reason_code <> 0 THEN 
	               'VOID (Manual Attendantpay Jackpot)' 
	               WHEN Treasury_Type = 'AttendantPay Credit'
	           AND IsManualAttendantPay = 1
	           AND treasury_reason_code <> 0 THEN 
	               'VOID (Manual Attendantpay Credit)' 
	               WHEN Treasury_Type = 'Mystery Jackpot'
	           AND IsManualAttendantPay = 1
	           AND treasury_reason_code <> 0 THEN 
	               'VOID (Manual Mystery Jackpot)' 
	               WHEN Treasury_Type = 'Offline Voucher-Shortpay'
	           AND IsManualAttendantPay = 0
	           AND treasury_reason_code <> 0
	           AND @isVoid = 1
	           AND treasury_reason = 'NEGATIVE TREASURY ENTRY' THEN 
	               'VOID (Offline Voucher-Shortpay)' 
	               WHEN Treasury_Type = 'Progressive'
	           AND IsManualAttendantPay = 0 THEN 'Progressive Jackpot' 
	               WHEN Treasury_Type = 'AttendantPay Jackpot'
	           AND IsManualAttendantPay = 0 THEN 'Attendantpay Jackpot' 
	               WHEN Treasury_Type = 'Shortpay'
	           AND IsManualAttendantPay = 0
	           AND @isShortpay = 1 THEN 'ShortPay' 
	               WHEN Treasury_Type = 'AttendantPay Credit'
	           AND IsManualAttendantPay = 0 THEN 'Attendantpay Credit' 
	               WHEN Treasury_Type = 'Mystery Jackpot'
	           AND IsManualAttendantPay = 0 THEN 'Mystery Jackpot' 
	               WHEN Treasury_Type = 'Progressive'
	           AND IsManualAttendantPay = 1 THEN 'Manual Progressive Jackpot' 
	               WHEN Treasury_Type = 'AttendantPay Jackpot'
	           AND IsManualAttendantPay = 1 THEN 'Manual Attendantpay Jackpot' 
	               WHEN Treasury_Type = 'AttendantPay Credit'
	           AND IsManualAttendantPay = 1 THEN 'Manual Attendantpay Credit' 
	               WHEN Treasury_Type = 'Mystery Jackpot'
	           AND IsManualAttendantPay = 1 THEN 'Manual Mystery Jackpot' 
	               WHEN Treasury_Type = 'Offline Voucher-Shortpay'
	           AND IsManualAttendantPay = 0 
	               THEN 'Offline Voucher-Shortpay' 
	               ELSE Treasury_Type 
	               END AS Trans_Type,
	           --Treasury_Type,      
	           Machine_Stock_No AS PrintAsset,
	           @Site_Code AS PrintSiteCode,
	           Bar_Position_Name AS PrintPosition,
	           Treasury_Actual_Date AS PrintedDate,
	           Machine_Stock_No AS PaidAsset,
	           Bar_Position_Name AS PaidPosition,
	           treasury_date + ' ' + treasury_time AS PaidDate,
	           --treasury_reason_code,
	           --dbo.fnGetGameName(installation.installation_no)  as machine_name,     
	           treasury_amount AS Amount,
	           '' AS Ticket 
	           --Treasury_Temp               
	           FROM treasury_entry T 
	           INNER JOIN installation I 
	           ON T.installation_ID = I .installation_ID
	           AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @StartDate AND @EndDate 
	               INNER JOIN bar_position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
	           AND (BP.Site_ID = @SITE) 
	               LEFT JOIN Route_Member RM ON RM.Bar_Position_ID = Bp.Bar_Position_ID 
	               LEFT JOIN [zone] Z ON BP.Zone_ID = Z.Zone_ID 
	               INNER JOIN MACHINE M ON I.Machine_ID = M.Machine_ID 
	               INNER JOIN machine_class MC ON M.Machine_Class_ID = MC.Machine_Class_ID 
	               WHERE 
	               (@Route_No = 0 OR RM.Route_ID = @Route_No)
	           AND (
	                   (
	                       @isHandPay = 1
	                       AND treasury_reason_code = 0
	                       AND treasury_type = 'AttendantPay Credit'
	                   )
	                   OR (
	                          @isShortpay = 1
	                          AND treasury_type IN ('ShortPay')
	                          AND treasury_reason_code <> 0
	                          AND COALESCE(treasury_membership_no, 0) <> -99
	                      )
	                   OR (
	                          @isVoid = 1
	                          AND treasury_type = 'ShortPay'
	                          AND treasury_reason_code <> 0
	                          AND treasury_membership_no = -99
	                          AND treasury_reason = 'NEGATIVE TREASURY ENTRY'
	                      )
	                   OR (
	                          @isOffline = 1
	                          AND @isvoid = 0
	                          AND treasury_reason_code = 0
	                          AND treasury_type = 'Offline Voucher-Shortpay'
	                      )
	                   OR (
	                          (@isVoid = 1 OR @isVoid = 0)
	                          AND @isOffline = 1
	                          AND treasury_reason_code = 0
	                          AND treasury_type = 'Offline Voucher-Shortpay'
	                      )
	                   OR (
	                          @isVoid = 1
	                          AND treasury_type = 'Offline Voucher-Shortpay'
	                          AND treasury_reason_code <> 0
	                          AND treasury_reason = 'NEGATIVE TREASURY ENTRY'
	                      )
	                   OR (
	                          @isJackpot = 1
	                          AND treasury_reason_code = 0
	                          AND treasury_type = 'AttendantPay Jackpot'
	                      )
	                   OR (
	                          @isProgressive = 1
	                          AND treasury_reason_code = 0
	                          AND treasury_type = 'Progressive'
	                      )
	                   OR (
	                          @isVoid = 1
	                          AND treasury_reason_code <> 0
	                          AND treasury_reason = 'NEGATIVE TREASURY ENTRY'
	                      )
	               )
	               --ORDER BY
	               --treasury_reason,Treasury_Type,Trans_Type,treasury_date       
	               UNION ALL  
	               
	               
	               SELECT CASE 
	                           WHEN @isCDMPrinted = 1
	           AND TV.iDeviceID IS NOT NULL
	           AND ID.strSerial IN (SELECT TIW1.Site_Workstation
	                                FROM   SiteWorkstations TIW1) 
	               THEN 'CashDesk Issued Voucher' 
	               WHEN @isMachinePrinted = 1
	           AND ISNULL(Ticket_Type, 0) = 0
	           AND TV.iDeviceID IS NOT NULL
	           AND ID.strSerial IN (SELECT EM1.Machine_Stock_No
	                                FROM   MACHINE EM1 INNER JOIN Installation I ON I.Machine_ID = EM1.Machine_ID
	           INNER JOIN Bar_Position BP ON bp.Bar_Position_ID =i.Bar_Position_ID 
	           LEFT JOIN Route_Member rm On rm.Bar_Position_ID= bp.Bar_Position_ID WHERE 
	           (@Route_No = 0 OR RM.Route_ID = @Route_No)) 
	               THEN 'Cashable Machine Printed Voucher' 
	               WHEN @isNonCashableOut = 1
	           AND ISNULL(Ticket_Type, 0) = 1
	           AND TV.iDeviceID IS NOT NULL
	           AND ID.strSerial IN (SELECT EM1.Machine_Stock_No t
	                                FROM   MACHINE EM1 INNER JOIN Installation I ON I.Machine_ID = EM1.Machine_ID
	           INNER JOIN Bar_Position BP ON bp.Bar_Position_ID =i.Bar_Position_ID 
	           LEFT JOIN Route_Member rm On rm.Bar_Position_ID= bp.Bar_Position_ID WHERE 
	           (@Route_No = 0 OR RM.Route_ID = @Route_No)) 
	               THEN 'Non Cashable Machine Printed Voucher' 
	               END AS Trans_Type,
	           ID.strSerial AS PrintAsset,
	           TV.isiteid AS PrintSiteCode,
	           [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	           PrintPosition,
	           -- isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,    
	           TV.dtPrinted AS PrintedDate,
	           CASE 
	                WHEN strVoucherStatus IS NOT NULL
	           AND strVoucherStatus = 'PD' THEN COALESCE(PD.strSerial, '') 
	               WHEN strVoucherStatus IS NOT NULL
	           AND strVoucherStatus = 'LT' THEN 'N/A' 
	               ELSE '' END
	               AS PaidAsset,
	           CASE 
	                WHEN strVoucherStatus IS NOT NULL
	           AND strVoucherStatus = 'PD' THEN ISNULL(
	                   [dbo].FnGetPositionForSerial(PD.strSerial, PD.Site_Code),
	                   'CASHDESK'
	               ) 
	               WHEN strVoucherStatus IS NOT NULL
	           AND strVoucherStatus = 'LT' THEN 'N/A'
	               ELSE '' END AS PaidPosition,
	           --[dbo].FnGetPositionForSerial(PD.strSerial,PD.Site_Code)  as PaidPosition,
	           TV.dtPaid AS PaidDate,
	           dbo.compute_decimal(TV.iAmount) AS Amount,
	           TV.strBarcode AS Ticket 
	           FROM 
	           voucher TV 
	           LEFT OUTER JOIN Device PD ON TV.ipaydeviceid = PD.ideviceid
	           AND TV.iPaySiteID = PD.Site_Code
	           AND PD.Site_Code = PD.iSiteID
	           AND PD.Site_Code = @Site_Code
	               LEFT OUTER JOIN Device ID ON TV.ideviceid = ID.ideviceid
	           AND TV.iSiteID = ID.Site_Code
	           AND ID.Site_Code = ID.iSiteID 
	               -- LEFT OUTER JOIN SiteWorkstations TIW ON TIW.Site_Workstation=PD.strSerial
	               -- LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial
	               -- LEFT OUTER JOIN Machine EM ON EM.Machine_Stock_No=PD.strSerial
	               -- LEFT OUTER JOIN Machine EM1 ON EM1.Machine_Stock_No=ID.strSerial
	               -- LEFT OUTER JOIN installation EI ON EM.Machine_ID=EI.Machine_ID
	               --	AND TV.dtPaid between EI.Installation_Start_Date+' '+EI.Installation_Start_Time
	               --			AND LTRIM (RTRIM(isnull(EI.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI.Installation_End_Time,'')  ))
	               -- LEFT OUTER JOIN installation EI1 ON EM1.Machine_ID=EI1.Machine_ID
	               --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	               --			AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))
	               -- LEFT OUTER JOIN bar_position EB ON EI.Bar_Position_ID=EB.Bar_Position_ID  AND (@SITE=0 OR  EB.Site_ID=@SITE)
	               -- LEFT OUTER JOIN bar_position EB1 ON EI1.Bar_Position_ID=EB1.Bar_Position_ID  AND (@SITE=0 OR  EB1.Site_ID=@SITE)      
	               WHERE 
	               
	               (
	                   TV.iSiteID = @Site_Code
	                   AND (ISNULL(strVoucherStatus, '') NOT IN ('NA'))
	                   AND (
	                           TV.IPAYSITEID IS NULL
	                           OR TV.IPAYSITEID = @Site_Code
	                           OR strVoucherStatus = 'LT'
	                       )
	                   AND (PD.iSITEID = @Site_Code OR ID.iSITEID = @Site_Code)
	               )
	           AND (
	                   (
	                       --CDMPrint  
	                       @isCDMPrinted = 1
	                       AND TV.dtPrinted BETWEEN @startDate AND @endDate
	                       AND TV.iDeviceID IS NOT NULL
	                       AND ID.strSerial IN (SELECT TIW1.Site_Workstation
	                                            FROM   SiteWorkstations TIW1)
	                   )
	                   OR (
	                          --isMachinePrinted  
	                          @isMachinePrinted = 1
	                          AND TV.dtPrinted BETWEEN @startDate AND @endDate
	                          AND ISNULL(strVoucherStatus, '') <> 'NA'
	                          AND ISNULL(Ticket_Type, 0) = 0
	                          AND TV.iDeviceID IS NOT NULL
	                          AND ID.strSerial IN (SELECT EM1.Machine_Stock_No
	                                               FROM   MACHINE EM1
	                                                      INNER JOIN 
	                                                           Installation i
	                                                           ON  em1.Machine_ID = 
	                                                               i.Machine_ID
	                                                      INNER JOIN 
	                                                           Bar_Position bp
	                                                           ON  bp.Bar_Position_ID = 
	                                                               i.Bar_Position_ID
	                                                      LEFT JOIN 
	                                                           Route_Member RM
	                                                           ON  RM.Bar_Position_ID = 
	                                                               bp.Bar_Position_ID
	                                               WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No))
	                      )
	                   OR (
	                          --isNonCashableOut  
	                          @isNonCashableOut = 1
	                          AND TV.dtPrinted BETWEEN @startDate AND @endDate
	                          AND ISNULL(Ticket_Type, 0) = 1
	                          AND TV.iDeviceID IS NOT NULL
	                          AND ID.strSerial IN (SELECT EM1.Machine_Stock_No
	                                               FROM   MACHINE EM1
	                                                      INNER JOIN 
	                                                           Installation i
	                                                           ON  em1.Machine_ID = 
	                                                               i.Machine_ID
	                                                      INNER JOIN 
	                                                           Bar_Position bp
	                                                           ON  bp.Bar_Position_ID = 
	                                                               i.Bar_Position_ID
	                                                      LEFT JOIN 
	                                                           Route_Member RM
	                                                           ON  RM.Bar_Position_ID = 
	                                                               bp.Bar_Position_ID
	                                               WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No))
	                      )
	               ) 
	               
	               UNION ALL  
	               
	               
	               SELECT CASE 
	                           WHEN @isCDMPaid = 1
	           AND TV.strvoucherStatus = 'PD'
	           AND TV.ipaydeviceid IS NOT NULL
	           AND PD.strSerial IN (SELECT TIW1.Site_Workstation
	                                FROM   SiteWorkstations TIW1) 
	               THEN 'CashDesk Claimed Voucher' 
	               WHEN @isMachinePaid = 1
	           AND TV.strvoucherStatus = 'PD'
	           AND ISNULL(Ticket_Type, 0) = 0
	           AND TV.ipaydeviceid IS NOT NULL
	           AND PD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                FROM   MACHINE EM1
	                                       INNER JOIN Installation i
	                                            ON  em1.Machine_ID = i.Machine_ID
	                                       INNER JOIN Bar_Position bp
	                                            ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                                       LEFT JOIN Route_Member RM
	                                            ON  RM.Bar_Position_ID = bp.Bar_Position_ID
	                                WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No)) 
	               THEN 'Cashable Machine Claimed Voucher' 
	               WHEN @isNonCashableIN = 1
	           AND TV.strvoucherStatus = 'PD'
	           AND ISNULL(Ticket_Type, 0) = 1
	           AND TV.ipaydeviceid IS NOT NULL
	           AND PD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                FROM   MACHINE EM1
	                                       INNER JOIN Installation i
	                                            ON  em1.Machine_ID = i.Machine_ID
	                                       INNER JOIN Bar_Position bp
	                                            ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                                       LEFT JOIN Route_Member RM
	                                            ON  RM.Bar_Position_ID = bp.Bar_Position_ID
	                                WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No)) 
	               THEN 'Non Cashable Machine Claimed Voucher' 
	               END AS Trans_Type,
	           CASE 
	                WHEN TV.iSiteID = @Site_Code THEN ID.strSerial
	                ELSE 'N/A'
	           END AS PrintAsset,
	           TV.isiteid AS PrintSiteCode,
	           CASE 
	                WHEN TV.iSiteID = @Site_Code THEN [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code)
	                ELSE 'N/A'
	           END AS PrintPosition,
	           TV.dtPrinted AS PrintedDate,
	           COALESCE(PD.strSerial, '') AS PaidAsset,
	           [dbo].FnGetPositionForSerial(PD.strSerial, PD.Site_Code) AS 
	           PaidPosition,
	           --TV.iPaysiteid,
	           TV.dtPaid AS PaidDate,
	           dbo.compute_decimal(TV.iAmount) AS Amount,
	           TV.strBarcode AS Ticket 
	           FROM 
	           voucher TV 
	           LEFT OUTER JOIN Device PD ON TV.ipaydeviceid = PD.ideviceid
	           AND TV.iPaySiteID = PD.Site_Code
	           AND PD.Site_Code = PD.iSiteID
	           AND PD.Site_Code = @Site_Code
	           AND TV.strvoucherStatus = 'PD'--(TV.IPAYSITEID=@Site_Code AND PD.iSITEID=@Site_Code )                           
	               LEFT OUTER JOIN Device ID ON TV.ideviceid = ID.ideviceid
	           AND TV.iSiteID = ID.Site_Code
	           AND ID.Site_Code = ID.iSiteID 
	               WHERE 
	               (TV.IPAYSITEID = @Site_Code AND (PD.Site_Code = @Site_Code))--OR ID.Site_Code=@Site_Code))
	           AND --CDMPAID  
	               @isCDMPaid = 1
	           AND TV.strvoucherStatus = 'PD'
	           AND TV.ipaysiteid = @Site_Code
	           AND PD.iSITEID = @Site_Code
	           AND TV.ipaydeviceid IS NOT NULL
	           AND PD.strSerial IN (SELECT TIW1.Site_Workstation
	                                FROM   SiteWorkstations TIW1)
	           AND TV.dtPaid BETWEEN @startDate AND @endDate
	           OR (
	                  --isMachinePaid  
	                  @isMachinePaid = 1
	                  AND TV.strvoucherStatus = 'PD'
	                  AND TV.ipaysiteid = @Site_Code
	                  AND PD.iSITEID = @Site_Code
	                  AND TV.dtPaid BETWEEN @startDate AND @endDate
	                  AND ISNULL(Ticket_Type, 0) = 0
	                  AND TV.ipaydeviceid IS NOT NULL
	                  AND PD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                       FROM   MACHINE EM1
	                                              INNER JOIN Installation i
	                                                   ON  em1.Machine_ID = i.Machine_ID
	                                              INNER JOIN Bar_Position bp
	                                                   ON  bp.Bar_Position_ID = 
	                                                       i.Bar_Position_ID
	                                              LEFT JOIN Route_Member RM
	                                                   ON  RM.Bar_Position_ID = 
	                                                       bp.Bar_Position_ID
	                                       WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No))
	              )
	           OR (
	                  --isNonCashableIN  
	                  @isNonCashableIN = 1
	                  AND TV.strvoucherStatus = 'PD'
	                  AND TV.ipaysiteid = @Site_Code
	                  AND PD.iSITEID = @Site_Code
	                  AND TV.dtPaid BETWEEN @startDate AND @endDate
	                  AND ISNULL(Ticket_Type, 0) = 1
	                  AND TV.ipaydeviceid IS NOT NULL
	                  AND PD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                       FROM   MACHINE EM1
	                                              INNER JOIN Installation i
	                                                   ON  em1.Machine_ID = i.Machine_ID
	                                              INNER JOIN Bar_Position bp
	                                                   ON  bp.Bar_Position_ID = 
	                                                       i.Bar_Position_ID
	                                              LEFT JOIN Route_Member RM
	                                                   ON  RM.Bar_Position_ID = 
	                                                       bp.Bar_Position_ID
	                                       WHERE  (@Route_No = 0 OR RM.Route_ID = @Route_No))
	              ) 
	              UNION ALL
	              
	              --Active Cashable Vouchers          
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable Active Voucher'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Active Voucher'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     NULL AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                              --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial   
	                              
	                     INNER  JOIN MACHINE EM1
	                          ON  EM1.Machine_Stock_No = ID.strSerial
	                     INNER JOIN installation EI1
	                          ON  EM1.Machine_ID = EI1.Machine_ID  
	                              --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              --AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))  
	                              AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              AND CASE 
	                                       WHEN EI1.Installation_End_Date IS NOT 
	                                            NULL THEN LTRIM(
	                                                RTRIM(
	                                                    ISNULL(
	                                                        EI1.Installation_End_Date,
	                                                        CONVERT(VARCHAR, GETDATE(), 113)
	                                                    ) + ' ' + ISNULL(EI1.Installation_End_Time, '')
	                                                )
	                                            )
	                                       ELSE GETDATE()
	                                  END   
	                     INNER JOIN bar_position EB1
	                          ON  EI1.Bar_Position_ID = EB1.Bar_Position_ID
	                     LEFT JOIN Route_Member RM
	                          ON  RM.Bar_Position_ID = eb1.Bar_Position_ID
	                          AND (@SITE = 0 OR EB1.Site_ID = @SITE)
	              WHERE  @isActive = 1
	                     AND COALESCE(strVoucherStatus, '') = ''
	                     AND dtExpire > @enddate
	                     AND dtPrinted BETWEEN @startdate AND @enddate --AND ISNULL(Ticket_Type, 0) = 0
	                     AND ID.iDeviceID IN (SELECT iDeviceId
	                                          FROM   Device
	                                          WHERE  iSiteID = @Site_Code)
	                     AND (TV.iSITEID = @Site_Code) 
	                    AND (@Route_No = 0 OR RM.Route_ID = @Route_No)
	              UNION ALL 
	              --VOID Vouchers
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable VOID Voucher'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable VOID Voucher'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     TV.dtVoid AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) * -1 AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     LEFT OUTER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                              --LEFT OUTER JOIN Device ID ON TV.ideviceid=ID.ideviceid AND TV.IPAYSITEID=ID.Site_Code  AND (@SITE=0 OR (TV.IPAYSITEID=@Site_Code AND ID.Site_Code=@Site_Code ))
	                              --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial
	                              --LEFT OUTER JOIN Machine EM1 ON EM1.Machine_Stock_No=ID.strSerial
	                              --LEFT OUTER JOIN installation EI1 ON EM1.Machine_ID=EI1.Machine_ID
	                              --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              --  AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))
	                              --LEFT OUTER JOIN bar_position EB1 ON EI1.Bar_Position_ID=EB1.Bar_Position_ID  AND (@SITE=0 OR  EB1.Site_ID=@SITE)
	              WHERE  (
	                         @isVoidVoucher = 1
	                         AND (
	                                 strVoucherStatus = 'VD'
	                                 AND dtVoid BETWEEN @startdate AND @enddate
	                             ) --AND ISNULL(Ticket_Type, 0) = 0
	                         AND ID.iDeviceID IN (SELECT iDeviceId
	                                              FROM   Device
	                                              WHERE  (@SITE = 0 OR (ISITEID = @Site_Code)))
	                         AND (@SITE = 0 OR (TV.ISITEID = @Site_Code))
	                     ) 
	              
	              UNION ALL 
	              --Cancelled Vouchers
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable Cancelled Voucher'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Cancelled Voucher'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     NULL AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                              --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial   
	                              
	                     INNER  JOIN MACHINE EM1
	                          ON  EM1.Machine_Stock_No = ID.strSerial
	                     INNER JOIN installation EI1
	                          ON  EM1.Machine_ID = EI1.Machine_ID 
	                              --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              --AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))  
	                              
	                     INNER JOIN bar_position EB1
	                          ON  EI1.Bar_Position_ID = EB1.Bar_Position_ID
	                     LEFT JOIN Route_Member RM
	                          ON  RM.Bar_Position_ID = eb1.Bar_Position_ID
	                          AND (@SITE = 0 OR EB1.Site_ID = @SITE)
	              WHERE  @isVoidCancel = 1
	                     AND (
	                             strVoucherStatus = 'NA'
	                             AND dtPrinted BETWEEN @startdate AND @enddate
	                         ) --AND ISNULL(Ticket_Type, 0) = 0
	                     AND ID.iDeviceID IN (SELECT iDeviceId
	                                          FROM   Device
	                                          WHERE  (@SITE = 0 OR (ISITEID = @Site_Code)))
	                     AND (@SITE = 0 OR (TV.ISITEID = @Site_Code)) 
	              
	              UNION ALL 
	              --Expired Vouchers
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable Expired Voucher'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Expired Voucher'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     TV.dtExpire AS PaidDate,	--Display Expired Date incase of an Expired Ticket   
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                              --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial   
	                              
	                     INNER  JOIN MACHINE EM1
	                          ON  EM1.Machine_Stock_No = ID.strSerial
	                     INNER JOIN installation EI1
	                          ON  EM1.Machine_ID = EI1.Machine_ID 
	                              --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              --AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))
	                              AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              AND CASE 
	                                       WHEN EI1.Installation_End_Date IS NOT 
	                                            NULL THEN LTRIM(
	                                                RTRIM(
	                                                    ISNULL(
	                                                        EI1.Installation_End_Date,
	                                                        CONVERT(VARCHAR, GETDATE(), 113)
	                                                    ) + ' ' + ISNULL(EI1.Installation_End_Time, '')
	                                                )
	                                            )
	                                       ELSE GETDATE()
	                                  END   
	                              
	                     INNER JOIN bar_position EB1
	                          ON  EI1.Bar_Position_ID = EB1.Bar_Position_ID
	                     LEFT JOIN Route_Member RM
	                          ON  RM.Bar_Position_ID = eb1.Bar_Position_ID
	                          AND (@SITE = 0 OR EB1.Site_ID = @SITE)
	              WHERE  @isExpired = 1
	                     AND strVoucherStatus IS NULL
	                     AND dtExpire <= GETDATE()
	                     AND dtExpire BETWEEN @startdate AND @enddate
	                     AND ISNULL(Ticket_Type, 0) = 0
	                     AND ID.iDeviceID IN (SELECT iDeviceId
	                                          FROM   Device
	                                          WHERE  (@SITE = 0 OR (ISITEID = @Site_Code)))
	                     AND (@SITE = 0 OR TV.ISITEID = @Site_Code)
	              UNION ALL
	              --Voucher Exceptions
	              --Ticket In Exceptions          
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable Voucher IN Exception'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Voucher IN Exception'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     NULL AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                              --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial   
	                              
	                     INNER  JOIN MACHINE EM1
	                          ON  EM1.Machine_Stock_No = ID.strSerial
	                     INNER JOIN installation EI1
	                          ON  EM1.Machine_ID = EI1.Machine_ID 
	                              --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	                              --AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))  
	                              
	                     INNER JOIN bar_position EB1
	                          ON  EI1.Bar_Position_ID = EB1.Bar_Position_ID
	                     LEFT JOIN Route_Member RM
	                          ON  RM.Bar_Position_ID = eb1.Bar_Position_ID
	                          AND (@SITE = 0 OR EB1.Site_ID = @SITE)
	              WHERE  @isException = 1
	                     AND (
	                             (TV.IPAYSITEID IS NULL OR TV.IPAYSITEID = @Site_Code)
	                             AND TV.ISITEID = @Site_Code
	                         )
	                     AND (
	                             (strVoucherStatus = 'PP')
	                             OR (strVoucherStatus IS NULL AND errcode <> 0)
	                         )
	                     AND dtPrinted BETWEEN @startdate AND @enddate 
	              --  AND ISNULL(Ticket_Type, 0) = 0
	              --  AND ErrDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	              --						AND (@SITE=0 OR (Site_Code=@Site_Code)))      
	              
	              
	              
	              
	              UNION ALL 
	              --Ticket Out Exception          
	              SELECT CASE 
	                          WHEN ISNULL(TE_Ticket_Type, 0) = 0 THEN 
	                               'Cashable Voucher OUT Exception'
	                          WHEN ISNULL(TE_Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Voucher OUT Exception'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     --isnull(B.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     TE_Date AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     NULL AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   ticket_exception TE
	                     INNER JOIN Voucher TV
	                          ON  TE.TE_TicketNumber = TV.StrBarCode
	                              --LEFT OUTER JOIN Device ID ON TV.ideviceid=ID.ideviceid AND TV.IPAYSITEID=ID.Site_Code  AND (@SITE=0 OR (TV.IPAYSITEID=@Site_Code AND ID.Site_Code=@Site_Code ))                              
	                              
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID
	                     INNER JOIN Installation I
	                          ON  TE_Installation_No = I.Installation_ID 
	                              --AND TE.TE_Date between I.Installation_Start_Date+' '+I.Installation_Start_Time
	                              --   AND LTRIM(RTRIM(isnull(I.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(I.Installation_End_Time,'')  ))
	                              --INNER JOIN Machine M ON M.Machine_ID=I.Machine_ID    
	                              AND TV.dtPrinted between I.Installation_Start_Date+' '+I.Installation_Start_Time
	                              AND CASE 
	                                       WHEN I.Installation_End_Date IS NOT 
	                                            NULL THEN LTRIM(
	                                                RTRIM(
	                                                    ISNULL(
	                                                        I.Installation_End_Date,
	                                                        CONVERT(VARCHAR, GETDATE(), 113)
	                                                    ) + ' ' + ISNULL(I.Installation_End_Time, '')
	                                                )
	                                            )
	                                       ELSE GETDATE()
	                                  END 
	                              
	                     INNER JOIN bar_position B
	                          ON  I.Bar_Position_ID = B.Bar_Position_ID
	                     LEFT JOIN Route_Member RM
	                          ON  RM.Bar_Position_ID = B.Bar_Position_ID
	                          AND (@SITE = 0 OR B.Site_ID = @SITE)
	              WHERE  @isException = 1
	                     AND te_status = 'N'
	                     AND TE_Installation_No <> 0
	                     AND TE_TicketNumber IS NOT NULL
	                     AND TE_Date BETWEEN @startdate AND @enddate
	                     AND TE_Site_ID = @Site_Code 
	              UNION ALL
	              SELECT CASE 
	                          WHEN ISNULL(Ticket_Type, 0) = 0 THEN 
	                               'Cashable Voucher Liability'
	                          WHEN ISNULL(Ticket_Type, 0) = 1 THEN 
	                               'Non Cashable Voucher Liability'
	                     END AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     '' AS PaidAsset,
	                     '' AS PaidPosition,
	                     NULL AS PaidDate,
	                     dbo.compute_decimal(TV.iAmount) AS Amount,
	                     TV.strBarcode AS Ticket
	              FROM   voucher TV
	                     INNER JOIN Device ID
	                          ON  TV.ideviceid = ID.ideviceid
	                          AND TV.iSiteID = ID.Site_Code
	                          AND ID.Site_Code = ID.iSiteID 
	                             
	                              
	         --            INNER  JOIN MACHINE EM1
	         --                 ON  EM1.Machine_Stock_No = ID.strSerial
	         --            INNER JOIN installation EI1
	         --                 ON  EM1.Machine_ID = EI1.Machine_ID 
								  --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	         --                     AND CASE 
	         --                              WHEN EI1.Installation_End_Date IS NOT 
	         --                                   NULL THEN LTRIM(
	         --                                       RTRIM(
	         --                                           ISNULL(
	         --                                               EI1.Installation_End_Date,
	         --                                               CONVERT(VARCHAR, GETDATE(), 113)
	         --                                           ) + ' ' + ISNULL(EI1.Installation_End_Time, '')
	         --                                       )
	         --                                   )
	         --                              ELSE GETDATE()
	         --                         END 
	         --            INNER JOIN bar_position EB1
	         --                 ON  EI1.Bar_Position_ID = EB1.Bar_Position_ID
	         --            LEFT JOIN Route_Member RM
	         --                 ON  RM.Bar_Position_ID = eb1.Bar_Position_ID
	         --                 AND (@SITE = 0 OR EB1.Site_ID = @SITE)
	              WHERE  @isLiability = 1
	                     AND (
	                             (
	                                 strVoucherStatus = 'PD'
	                                 AND dtPaid BETWEEN @startdate AND @enddate
	                                 AND dtPrinted < @startdate --AND ISNULL(Ticket_Type, 0) = 0
	                                 AND TV.iPayDeviceID IN (SELECT iDeviceId
	                                                         FROM   Device
	                                                         WHERE  (@SITE = 0 OR (iSiteID = @Site_Code)))
	                                 AND (@SITE = 0 OR (TV.IPAYSITEID = @Site_Code))
	                             )
	                             OR (
	                                    dtPrinted BETWEEN @startdate AND @enddate
	                                    AND COALESCE(strVoucherStatus, '') NOT IN ('VD', 'NA', 'LT')
	                                    AND (dtPaid IS NULL OR dtPaid > @enddate) --AND ISNULL(Ticket_Type, 0) = 0
	                                    AND TV.iDeviceID IN (SELECT iDeviceId
	                                                         FROM   Device
	                                                         WHERE  (@SITE = 0 OR (iSiteID = @Site_Code)))
	                                    AND (
	                                            (TV.IPAYSITEID IS NULL OR TV.IPAYSITEID = @Site_Code)
	                                            AND TV.ISITEID = @Site_Code
	                                        )
	                                )
	                         ) 
	              
	              UNION ALL
	              SELECT 'Promo Cashable Voucher' AS Trans_Type,
	                     ID.strSerial AS PrintAsset,
	                     TV.isiteid AS PrintSiteCode,
	                     [dbo].FnGetPositionForSerial(ID.strSerial, ID.Site_Code) AS 
	                     PrintPosition,
	                     --isnull(EB1.Bar_Position_Name,'CASHDESK') as PrintPosition,      
	                     TV.dtPrinted AS PrintedDate,
	                     COALESCE(PD.strSerial, '') AS PaidAsset,
	                     CASE 
	                          WHEN strVoucherStatus IS NOT NULL
	           AND strVoucherStatus = 'PD' THEN ISNULL(
	                   [dbo].FnGetPositionForSerial(PD.strSerial, PD.Site_Code),
	                   'CASHDESK'
	               ) 
	               ELSE '' END AS PaidPosition,
	           TV.dtPaid AS PaidDate,
	           dbo.compute_decimal(TV.iAmount) AS Amount,
	           TV.strBarcode AS Ticket 
	           FROM 
	           voucher TV 
	           LEFT OUTER JOIN Device PD ON TV.ipaydeviceid = PD.ideviceid
	           AND TV.iPaySiteID = PD.Site_Code
	           AND PD.Site_Code = PD.iSiteID
	           AND PD.Site_Code = @Site_Code 
	               LEFT OUTER JOIN Device ID ON TV.ideviceid = ID.ideviceid
	           AND TV.iSiteID = ID.Site_Code
	           AND ID.Site_Code = ID.iSiteID 
	               --LEFT OUTER JOIN SiteWorkstations TIW1 ON TIW1.Site_Workstation=ID.strSerial   
	               INNER JOIN MACHINE EM1 ON EM1.Machine_Stock_No = ID.strSerial 
	               INNER JOIN installation EI1 ON EM1.Machine_ID = EI1.Machine_ID 
	               --AND TV.dtPrinted between EI1.Installation_Start_Date+' '+EI1.Installation_Start_Time
	               --AND LTRIM(RTRIM(isnull(EI1.Installation_End_Date,convert(varchar, getdate(), 113))+' '+isnull(EI1.Installation_End_Time,'')  ))  
	               INNER JOIN bar_position EB1 ON EI1.Bar_Position_ID = EB1.Bar_Position_ID 
	               LEFT JOIN Route_Member RM ON RM.Bar_Position_ID = eb1.Bar_Position_ID
	           AND (@SITE = 0 OR EB1.Site_ID = @SITE) 
	               WHERE 
	               @isPromo = 1
	           AND strVoucherStatus = 'PD'
	           AND LEFT(strBarCode, 1) IN (@NONCashable, @Cashable)
	           AND dtPaid BETWEEN @startdate AND @enddate
	           AND TV.iPayDeviceID IN (SELECT iDeviceId
	                                   FROM   Device
	                                   WHERE  (@SITE = 0 OR (iSiteID = @Site_Code)))
	           AND (TV.IPAYSITEID = @Site_Code)
	       ) COMBINEDTABLE(
	           Trans_Type,
	           PrintAsset,
	           PrintSiteCode,
	           PrintPosition,
	           PrintedDate,
	           PaidAsset,
	           PaidPosition,
	           PaidDate,
	           Amount,
	           Ticket
	       )
	ORDER BY
	       Trans_Type ASC,
	       PrintedDate DESC
END
GO

