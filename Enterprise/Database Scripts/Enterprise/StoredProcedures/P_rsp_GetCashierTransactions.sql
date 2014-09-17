USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCashierTransactions]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCashierTransactions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC rsp_GetCashierTransactions @startdate='2013-07-31 06:30:00.000',@enddate = '2013-09-07 14:37:39.217',@site =0,@Route_no = 0   
	
CREATE PROCEDURE [dbo].[rsp_GetCashierTransactions]
	@startdate DATETIME ,
	@enddate DATETIME ,
	@SITE INT = 0,
	@Route_No INT = 0,
	@User_No INT = 0
AS
BEGIN
     
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
	
DECLARE @CDPrintedAmount DECIMAL(18,2)
DECLARE @MCPrintedAmount DECIMAL(18,2)
DECLARE @CDClaimedAmount DECIMAL(18,2)
DECLARE @MCClaimedAmount DECIMAL(18,2) 

DECLARE @CDPrintedCount INT
DECLARE @MCPrintedCount INT
DECLARE @CDClaimedCount INT
DECLARE @MCClaimedCount INT


-- Cash Desk Printed Amount

SELECT @CDPrintedAmount=CAST(SUM(ISNULL(iAmount/100.00,0)) AS DECIMAL(18,2)) FROM Voucher V WITH(NOLOCK)   
  WHERE     
  dtPrinted between @startdate AND @enddate
  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherIssuedUser = @User_No))
    --AND ( 
    --( @User IS NULL )                   
    --     OR                
    --       ( @User IS NOT NULL                 
    --         AND                
    --         VoucherIssuedUser = @User                
    --       )                
    --     )    
       
   AND (iSiteID=@Site_Code AND (iPaySiteID is null OR iPaySiteID=@Site_Code OR strVoucherStatus='LT')) AND coalesce (strVoucherStatus,'') NOT IN ('NA', 'VD')                   
  AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in (Select TIW_Name Collate database_default from Exchange..ticket_issue_workstation))
  
-- Cash Desk Printed Count  
  
SELECT @CDPrintedCount = count(1) 
  FROM Voucher V WITH(NOLOCK)
  WHERE     
  dtPrinted between @startdate AND @enddate
  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherIssuedUser = @User_No))
      --AND ( ( @User IS NULL )                   
      --   OR                
      --     ( @User IS NOT NULL                 
      --       AND                
      --       VoucherIssuedUser = @User                
      --     )                
      -- )    
           
  
  AND (iSiteID=@Site_Code AND (iPaySiteID is null OR iPaySiteID=@Site_Code OR strVoucherStatus='LT')) AND coalesce (strVoucherStatus,'') NOT IN ('NA', 'VD')                   
  AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in (Select TIW_Name Collate database_default from Exchange..ticket_issue_workstation))
  
  
  -- Machine Printed Amount

SELECT @MCPrintedAmount=cast(isnull(SUM(cast(iAmount AS DECIMAL(18,2))) ,0.0)as decimal(18,2))/100.00     
  FROM Voucher V WITH(NOLOCK)
  WHERE     
  dtPrinted between @startdate AND @enddate 
  AND isnull(strVoucherStatus,'') NOT IN ('NA','VD')    
    AND (V.iSiteID=@Site_Code AND (iPaySiteID is null OR iPaySiteID=@Site_Code OR strVoucherStatus='LT'))        
  AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No collate database_default from Machine M
						INNER JOIN Installation I WITH(NOLOCK) ON I.Machine_ID = M.Machine_ID
						INNER JOIN Bar_Position BP WITH(NOLOCK) ON I.Bar_Position_ID = BP.Bar_Position_ID
						LEFT JOIN Route_Member RM WITH(NOLOCK) ON RM.Bar_Position_ID = BP.Bar_Position_ID
					))
 
 --Machine Printed Count
 
  SELECT @MCPrintedCount=Count(1)
  FROM Voucher V WITH(NOLOCK)
  WHERE     
  dtPrinted between @startdate AND @enddate AND ISNULL(V.Ticket_Type, 0) = 0 AND isnull(V.strVoucherStatus,'') NOT IN ('NA','VD')
      
  AND (V.iSiteID=@Site_Code AND (V.iPaySiteID is null OR iPaySiteID=@Site_Code OR strVoucherStatus='LT'))
  AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No collate database_default from Machine M
	  					INNER JOIN Installation I WITH(NOLOCK) ON I.Machine_ID = M.Machine_ID
						INNER JOIN Bar_Position BP WITH(NOLOCK) ON I.Bar_Position_ID = BP.Bar_Position_ID
						LEFT JOIN Route_Member RM WITH(NOLOCK) ON RM.Bar_Position_ID = BP.Bar_Position_ID 
					WHERE ((@Route_No =0) OR (Rm.Route_ID = @Route_No))
					))
-- Cash Desk Claimed Amount

 SELECT @CDClaimedAmount= Cast(isnull(Sum(cast(V.iAmount AS DECIMAL(18,2))),0.0) as decimal(18,2))/100.00 
   FROM Voucher V WITH(NOLOCK)
  WHERE strVoucherStatus ='PD'    
   --AND ( ( @User IS NULL )                   
   --      OR                
   --        ( @User IS NOT NULL                 
   --          AND                
   --          VoucherRedeemedUser = @User                
   --        )                
   --      )    
        AND iPaySiteID=@Site_Code                 
  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherRedeemedUser = @User_No))
  AND dtPaid between @startdate AND @enddate    
  AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in (Select Site_Workstation Collate database_default from SiteWorkstations))    
 
 -- Cash Desk Claimed Count
 
  
  SELECT @CDClaimedCount=count(1)    
   FROM Voucher V WITH(NOLOCK)
  WHERE strVoucherStatus ='PD'    
   --AND ( ( @User IS NULL )                   
   --      OR                
   --        ( @User IS NOT NULL                 
   --          AND                
   --          VoucherRedeemedUser = @User                
   --        )                
   --      )    
        AND iPaySiteID=@Site_Code
  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherRedeemedUser = @User_No))
  AND dtPaid between @startdate AND @enddate    
  AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in (Select Site_Workstation Collate database_default from SiteWorkstations))    
 
--  Machine - Claimed Amount


SELECT @MCClaimedAmount=Cast(isnull(Sum(cast(iAmount AS DECIMAL(18,2))),0.0)as decimal(18,2))/100.00      
  FROM Voucher V WITH(NOLOCK)
  WHERE strVoucherStatus ='PD'
  AND dtPaid between @startdate AND @enddate AND ISNULL(Ticket_Type, 0) = 0    
  AND iPaySiteID=@Site_Code
  AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No collate database_default from Machine M WITH(NOLOCK)
							INNER JOIN Installation I WITH(NOLOCK) ON I.Machine_ID  = M.Machine_ID
							INNER JOIN Bar_Position BP WITH(NOLOCK) ON I.Bar_Position_ID = BP.Bar_Position_ID
							LEFT JOIN Route_Member RM WITH(NOLOCK) ON RM.Bar_Position_ID = BP.Bar_Position_ID 
						WHERE ((@Route_No =0) OR (Rm.Route_ID = @Route_No))
					))



-- Machine - Claimed count


 
  SELECT @MCClaimedCount=count(1)    
  FROM Voucher V WITH(NOLOCK)
  WHERE strVoucherStatus ='PD'    
  AND dtPaid between @startdate AND @enddate AND ISNULL(Ticket_Type, 0) = 0    
  AND iPaySiteID=@Site_Code    
  AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No collate database_default from Machine M
						INNER JOIN Installation I WITH(NOLOCK) ON I.Machine_ID = M.Machine_ID
						INNER JOIN Bar_Position BP WITH(NOLOCK) ON I.Bar_Position_ID = BP.Bar_Position_ID
						LEFT JOIN Route_Member RM WITH(NOLOCK) ON RM.Bar_Position_ID = BP.Bar_Position_ID 
					WHERE ((@Route_No =0) OR (Rm.Route_ID = @Route_No))
					))
	
	
	
	
	SELECT CDPaidAmount,
	       CDPaidCount,
	       CDPrintedAmount,
	       CDPrintedCount,
	       HandPayAmount,
	       HandPayCount,
	       ShortPayAmount,
	       ShortPayCount,
	       OfflineVoucherAmount,
     OfflineVoucherCount,   
	       JackpotAmount,
	       JackpotCount,
	       ProgAmount,
	       ProgCount,
	       VoidAmount,
	       VoidCount,
	       MCPaidAmount,
	       MCPaidCount,
	       MCPrintAmount,
	       MCPrintCount,
	       ActiveCashableVoucherAmount,
	       ActiveCashableVoucherCount,
	       VoidTicketsAmount,
	       VoidTicketsCount,
	       VoidVoucherAmount,
	       VoidVoucherCount,
	       CancelledAmount,
	       CancelledCount,
	       ExpiredAmount,
	       ExpiredCount,
	       TicketInExceptionAmount,
	       TicketInExceptionCount,
	       TicketOutExceptionAmount,
	       TicketOutExceptionCount,
	       CashableVoucherLiabilityAmount,
	       CashableVoucherLiabilityCount,
	       PromoCashableAmount,
	       PromoCashableCount,
	       NonCashableINAmount,
	       NonCashableINCount,
	       NonCashableOutAmount,
	       NonCashableOutCount
	FROM   (
	           --CDM Paid    
	           SELECT 'CDPaidAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0) AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher
	           WHERE  strVoucherStatus = 'PD'
					  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherRedeemedUser = @User_No))
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND iPayDeviceID IN (SELECT iDeviceId
	                                       FROM   Device
	                                       WHERE  strSerial IN (SELECT 
	                                                                   Site_Workstation
	                                                            FROM   
	                                                                   SiteWorkstations)
	                                              AND (iSiteid = @Site_Code))
	                  AND iPaySiteID = @Site_Code
	           UNION ALL    
	           SELECT 'CDPaidCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher
	           WHERE  strVoucherStatus = 'PD'
					  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherRedeemedUser = @User_No))
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND iPayDeviceID IN (SELECT iDeviceId
	                                       FROM   Device
	                                       WHERE  strSerial IN (SELECT 
	                                                                   Site_Workstation
	                                                            FROM   
	                                                                   SiteWorkstations)
	                                              AND (iSiteid = @Site_Code))
	                  AND iPaySiteID = @Site_Code
	           UNION ALL 
	           --CDM Printed    
	           SELECT 'CDPrintedAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
					  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherIssuedUser = @User_No))
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT 
	                                                                Site_Workstation
	                                                         FROM   
	                                                                SiteWorkstations)
	                                           AND (iSiteid = @Site_Code))
	                      --AND  ((iSiteID=@Site_Code AND (iPaySiteID IS NULL OR iPaySiteID=@Site_Code)) AND Coalesce(strVoucherStatus,'')not in ('LT','NA'))
	                  AND (
	                          (
	                              iSiteID = @Site_Code
	                              AND (
	                                      iPaySiteID IS NULL
	                                      OR iPaySiteID = @Site_Code
	                                      OR strVoucherStatus = 'LT'
	                                  )
	                          )
	                          AND COALESCE(strVoucherStatus, '')NOT IN ('NA', 'VD')
	                      )
	           UNION ALL     
	           SELECT 'CDPrintedCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
					  AND ((ISNULL(@User_No, 0) = 0) OR (VoucherIssuedUser = @User_No))
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT 
	                                                                Site_Workstation
	                                                         FROM   
	                                                                SiteWorkstations)
	                                           AND (iSiteid = @Site_Code))
	                      --AND  ((iSiteID=@Site_Code AND (iPaySiteID IS NULL OR iPaySiteID=@Site_Code)) AND Coalesce(strVoucherStatus,'')not in ('LT','NA'))
	                  AND (
	                          (
	                              iSiteID = @Site_Code
	                              AND (
	                                      iPaySiteID IS NULL
	                                      OR iPaySiteID = @Site_Code
	                                      OR strVoucherStatus = 'LT'
	                                  )
	                          )
	                          AND COALESCE(strVoucherStatus, '')NOT IN ('NA', 'VD')
	                      )
	           UNION ALL 
	           
	           --handpay    
	           SELECT 'HandPayAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT  JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'AttendantPay Credit' OR treasury_type='Handpay Credit')
	                  AND Treasury_reason_code = 0
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'HandPayCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry 
	                  TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'AttendantPay Credit' OR treasury_type='Handpay Credit')
	                  AND Treasury_reason_code = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL
	           --Shortpays
	           
	           SELECT 'ShortPayAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  treasury_type IN ('Shortpay')
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND ISNULL(Treasury_Membership_No, 0) <> -99
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'ShortPayCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  treasury_type IN ('Shortpay')
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND ISNULL(Treasury_Membership_No, 0) <> -99
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	                  
	        	             --Offline Voucher
	           UNION ALL
	           SELECT 'OfflineVoucherAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  treasury_type IN ('Offline Voucher-Shortpay')
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND ISNULL(Treasury_Membership_No, 0) <> -99
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'OfflineVoucherCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  treasury_type IN ('Offline Voucher-Shortpay')
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND ISNULL(Treasury_Membership_No, 0) <> -99
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	                  
	                  
	           UNION ALL 
	           
	           --jackpot    
	           SELECT 'JackpotAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'Attendantpay Jackpot' OR treasury_type='Handpay Jackpot')
	                  AND Treasury_reason_code = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'JackpotCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'Attendantpay Jackpot' OR treasury_type='Handpay Jackpot')
	                  AND Treasury_reason_code = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL 
	           --prog    
	           SELECT 'ProgAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'Progressive' OR treasury_type = 'Prog')
	                  AND Treasury_reason_code = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'ProgCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  (treasury_type = 'Progressive' OR treasury_type = 'Prog')
	                  AND Treasury_reason_code = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL 
	           
	           --Void    
	           SELECT 'VoidAmount' AS DESCRIPTION,
	                  ISNULL(SUM(Treasury_Amount), 0) * -1 AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	           WHERE  Treasury_Reason = 'NEGATIVE TREASURY ENTRY'
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND ((@Route_No = 0) OR (@Route_No IN 
							(SELECT Route_ID FROM Route_Member RM WHERE Route_ID = @Route_No AND RM.Bar_Position_ID = BP.Bar_Position_ID)))  
	           UNION ALL    
	           SELECT 'VoidCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   treasury_entry TE
	                  INNER JOIN Installation i
	                       ON  Te.Installation_ID = i.Installation_ID
	                  INNER JOIN Bar_Position bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  Rm.Bar_Position_ID = bp.Bar_Position_ID
	           WHERE  Treasury_Reason = 'NEGATIVE TREASURY ENTRY'
	                  AND cast(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @startdate 
	                      AND @enddate
	                  AND ((ISNULL(@User_No, 0) = 0) OR (UserID = @User_No))
	                  AND (@Site = 0 OR SITE_ID = @SITE)
	                  AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL 
	           --MC Paid    
	           SELECT 'MCPaidAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  strVoucherStatus = 'PD'
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 0
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteID = @Site_Code))
	           UNION ALL    
	           SELECT 'MCPaidCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  strVoucherStatus = 'PD'
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 0
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --											AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteID = @Site_Code))
	           UNION ALL 
	           
	           -- MC Printed    
	           SELECT 'MCPrintAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 0
	                  AND ISNULL(strVoucherStatus, '') <> 'NA'
	                 AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                      --AND (@SITE=0 OR ((iSiteID=@Site_Code AND (iPaySiteID IS NULL OR iPaySiteID=@Site_Code)) AND Coalesce(strVoucherStatus,'')<>'LT'))
	                  AND (
	                          @SITE = 0
	                          OR (
	                                 V.iSiteID = @Site_Code
	                                 AND (
	                                         iPaySiteID IS NULL
	                                         OR iPaySiteID = @Site_Code
	                                         OR strVoucherStatus = 'LT'
	                                     )
	                             )
	                      )
	           UNION ALL    
	           SELECT 'MCPrintCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 0
	                  AND ISNULL(strVoucherStatus, '') <> 'NA'
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                      --AND (@SITE=0 OR ((iSiteID=@Site_Code AND (iPaySiteID IS NULL OR iPaySiteID=@Site_Code)) AND Coalesce(strVoucherStatus,'')<>'LT'))
	                  AND (
	                          @SITE = 0
	                          OR (
	                                 V.iSiteID = @Site_Code
	                                 AND (
	                                         iPaySiteID IS NULL
	                                         OR iPaySiteID = @Site_Code
	                                         OR strVoucherStatus = 'LT'
	                                     )
	                             )
	                      )
	           UNION ALL 
	           
	           --Active Cashable Vouchers    
	           SELECT 'ActiveCashableVoucherAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  COALESCE(strVoucherStatus, '') = ''
	                  AND dtExpire > @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  AND dtPrinted BETWEEN @startdate AND @enddate --AND ISNULL(Ticket_Type, 0) = 0  --Need to confirm if it shd be printed in the same period as well
	                                                                --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (V.ISITEID = @Site_Code))
	           UNION ALL    
	           SELECT 'ActiveCashableVoucherCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  COALESCE(strVoucherStatus, '') = ''
	                  AND dtExpire > @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  AND dtPrinted BETWEEN @startdate AND @enddate --AND ISNULL(Ticket_Type, 0) = 0  --Need to confirm if it shd be printed in the same period as well
	                                                                --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (V.ISITEID = @Site_Code))
	           UNION ALL 
	           
	           --Void Tickets    
	           SELECT 'VoidTicketsAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtPrinted BETWEEN @startdate AND @enddate
	                  )
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  --AND ISNULL(Ticket_Type, 0) = 0
	                                              --AND iDeviceID in (Select iDeviceId from Device WHERE strSerial in (select Machine_Stock_No  from Machine ) AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (V.ISITEID = @Site_Code))
	           UNION ALL    
	           SELECT 'VoidTicketsCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtPrinted BETWEEN @startdate AND @enddate
	                  )
	                  AND ((ISNULL(@User_No, 0) = 0) OR (iVoucherVoidUser = @User_No))
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  --AND ISNULL(Ticket_Type, 0) = 0
	                                              -- AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in (select Machine_Stock_No  from Machine ) AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR V.ISITEID = @Site_Code)
	           UNION ALL 
	           
	           --Void Tickets cash desk
	           SELECT 'VoidVoucherAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtVoid BETWEEN @startdate AND @enddate
	                  ) --AND ISNULL(Ticket_Type, 0) = 0
	                  AND ((ISNULL(@User_No, 0) = 0) OR (iVoucherVoidUser = @User_No))
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT 
	                                                                Site_Workstation
	                                                         FROM   dbo.SiteWorkstations
	                                                         WHERE  (@SITE = 0 OR (site_ID = @SITE)))
	                                           AND (@SITE = 0 OR (iSiteid = @Site_Code)))
	           UNION ALL    
	           SELECT 'VoidVoucherCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtVoid BETWEEN @startdate AND @enddate
	                  ) --AND ISNULL(Ticket_Type, 0) = 0
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT S.Site_Workstation
	                                                         FROM   dbo.SiteWorkstations 
	                                                                S
	                                                         WHERE  (@SITE = 0 OR (S.site_ID = @SITE)))
	                                           AND (@SITE = 0 OR (iSiteid = @Site_Code)))
	           
	           UNION ALL 
	           
	           
	           --Cancelled    
	           SELECT 'CancelledAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  (
	                      strVoucherStatus = 'NA'
	                      AND dtPrinted BETWEEN @startdate AND @enddate
	                  )
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  --AND ISNULL(Ticket_Type, 0) = 0
	                                              --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in (select Machine_Stock_No  from Machine ) AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR V.ISITEID = @Site_Code)
	           UNION ALL    
	           SELECT 'CancelledCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  (
	                      strVoucherStatus = 'NA'
	                      AND dtPrinted BETWEEN @startdate AND @enddate
	                  )
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                  --AND ISNULL(Ticket_Type, 0) = 0
	                                              --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in (select Machine_Stock_No  from Machine ) AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR V.ISITEID = @Site_Code) 
	           UNION ALL 
	           --Expired    
	           SELECT 'ExpiredAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  strVoucherStatus IS NULL
	                  AND dtExpire <= GETDATE()
	                  AND dtExpire BETWEEN @startdate AND @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND ISNULL(Ticket_Type, 0) = 0
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR V.ISITEID = @Site_Code) 
	           UNION ALL    
	           SELECT 'ExpiredCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  strVoucherStatus IS NULL
	                  AND dtExpire <= GETDATE()
	                  AND dtExpire BETWEEN @startdate AND @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND ISNULL(Ticket_Type, 0) = 0
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR V.ISITEID = @Site_Code) 
	           UNION ALL 
	           
	           --Voucher Exceptions
	           --Ticket In Exceptions    
	           SELECT 'TicketInExceptionAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  (
	                      (strVoucherStatus = 'PP')
	                      OR (strVoucherStatus IS NULL AND ISNULL(errcode, 0) <> 0)
	                  ) 
	                  --  WHERE strVoucherStatus = 'PP'
	                  AND dtPrinted BETWEEN @startdate AND @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --  AND ISNULL(Ticket_Type, 0) = 0
	                      --  AND ErrDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --						AND (@SITE=0 OR (Site_Code=@Site_Code)))
	                  AND 
	                          V.iSiteID = @Site_Code
	                          AND (iPaySiteID IS NULL OR iPaySiteID = @Site_Code)
	                      
	           
	           UNION ALL    
	           SELECT 'TicketInExceptionCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  (
	                      (strVoucherStatus = 'PP')
	                      OR (strVoucherStatus IS NULL AND ISNULL(errcode, 0) <> 0)
	                  ) 
	                  --  WHERE strVoucherStatus = 'PP'
	                  AND dtPrinted BETWEEN @startdate AND @enddate
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --  AND ISNULL(Ticket_Type, 0) = 0
	                      --  AND ErrDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --						AND (@SITE=0 OR (Site_Code=@Site_Code)))
	                  AND (
	                          V.iSiteID = @Site_Code
	                          AND (iPaySiteID IS NULL OR iPaySiteID = @Site_Code)
	                      )
	           UNION ALL 
	           --Ticket Out Exception    
	           SELECT 'TicketOutExceptionAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(TE_Value), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   ticket_exception TE
					INNER JOIN Installation I
	                       ON  I.Installation_ID = Te.TE_Installation_No 
	                  INNER JOIN Bar_Position Bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  bp.Bar_Position_ID = RM.Bar_Position_ID
	                  INNER JOIN Voucher TV
	                       ON  TE.TE_TicketNumber = TV.StrBarCode
	                  LEFT OUTER JOIN Device ID
	                       ON  TV.ideviceid = ID.ideviceid
	                       AND TV.iSiteid = ID.iSiteid
	                       AND (
	                               @SITE = 0
	                               OR (TV.iSiteid = @Site_Code AND ID.iSiteid = @Site_Code)
	                           )
	           WHERE  te_status = 'N'
	                  AND TE_Installation_No <> 0
	                  AND TE_TicketNumber IS NOT NULL
	                  AND TE_Date BETWEEN @startdate AND @enddate AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	           UNION ALL    
	           SELECT 'TicketOutExceptionCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   ticket_exception TE
	            INNER JOIN Installation I
	                       ON  I.Installation_ID = Te.TE_Installation_No 
	                  INNER JOIN Bar_Position Bp
	                       ON  bp.Bar_Position_ID = i.Bar_Position_ID
	                  LEFT JOIN Route_Member RM
	                       ON  bp.Bar_Position_ID = RM.Bar_Position_ID
	                  INNER JOIN Voucher TV
	                       ON  TE.TE_TicketNumber = TV.StrBarCode
	                  LEFT OUTER JOIN Device ID
	                       ON  TV.ideviceid = ID.ideviceid
	                       AND TV.iSiteid = ID.iSiteid
	                       AND (
	                               @SITE = 0
	                               OR (TV.iSiteid = @Site_Code AND ID.iSiteid = @Site_Code)
	                           )
	           WHERE  te_status = 'N'
	                  AND TE_Installation_No <> 0
	                  AND TE_TicketNumber IS NOT NULL
	                  AND TE_Date BETWEEN @startdate AND @enddate AND (@Route_No = 0 OR Rm.Route_ID = @Route_No) 
	                  
	           UNION ALL 
	           --Cashable Voucher liability 
	           
	           
	           	
 SELECT 'CashableVoucherLiabilityAmount' as Description, CAST((ISNULL(@CDPrintedAmount,0)+ISNULL(@MCPrintedAmount,0))-(ISNULL(@CDClaimedAmount,0)+ISNULL(@MCClaimedAmount,0)) AS DECIMAL(18,2))  as val    
  
  UNION ALL    
  SELECT 'CashableVoucherLiabilityCount' as Description, (ISNULL(@CDPrintedCount,0) + ISNULL(@MCPrintedCount,0) )-(ISNULL(@CDClaimedCount,0)+ISNULL(@MCClaimedCount,0)) as val    
  	
  	   
	        --   SELECT 'CashableVoucherLiabilityAmount' AS DESCRIPTION,
	        --          CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	        --          val
	        --   FROM   voucher V
	        --   WHERE  (
	        --              strVoucherStatus = 'PD'
	        --              AND dtPaid BETWEEN @startdate AND @enddate 
	        --              AND dtPrinted < @startdate --AND ISNULL(Ticket_Type, 0) = 0
	        --                                         --AND iPayDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	        --              AND (@SITE = 0 OR (iPaySiteid = @Site_Code))
	        --          )
	        --          OR  (
	        --                  dtPrinted BETWEEN @startdate AND @enddate
	        --                  AND COALESCE(strVoucherStatus, '') NOT IN ('VD', 'NA', 'LT')
	        --                  AND (dtPaid IS NULL OR dtPaid > @enddate)
							  --AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
									--			INNER JOIN Installation I
									--				   ON  m.Machine_ID = i.Machine_ID
									--			  INNER JOIN Bar_Position Bp
									--				   ON  bp.Bar_Position_ID = i.Bar_Position_ID
									--			  LEFT JOIN Route_Member RM
									--				   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
									--		WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
							  ----AND ISNULL(Ticket_Type, 0) = 0
	        --                                              --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	        --                  AND (
	        --                          @SITE = 0
	        --                          OR (
	        --                                 V.iSiteID = @Site_Code
	        --                                 AND (iPaySiteID IS NULL OR iPaySiteID = @Site_Code)
	        --                          )
	                                  
	        --                      )
	        --              ) 
	        --   UNION ALL    
	        --   SELECT 'CashableVoucherLiabilityCount' AS DESCRIPTION,
	        --          COUNT(1) AS val
	        --   FROM   voucher V
	        --  WHERE  (
	        --              strVoucherStatus = 'PD'
	        --              AND dtPaid BETWEEN @startdate AND @enddate 
	        --              AND dtPrinted < @startdate --AND ISNULL(Ticket_Type, 0) = 0
	        --                                         --AND iPayDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	        --              AND (@SITE = 0 OR (iPaySiteid = @Site_Code))
	        --          )
	        --          OR  (
	        --                  dtPrinted BETWEEN @startdate AND @enddate
	        --                  AND COALESCE(strVoucherStatus, '') NOT IN ('VD', 'NA', 'LT')
	        --                  AND (dtPaid IS NULL OR dtPaid > @enddate)
							  --AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
									--			INNER JOIN Installation I
									--				   ON  m.Machine_ID = i.Machine_ID
									--			  INNER JOIN Bar_Position Bp
									--				   ON  bp.Bar_Position_ID = i.Bar_Position_ID
									--			  LEFT JOIN Route_Member RM
									--				   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
									--		WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
							  ----AND ISNULL(Ticket_Type, 0) = 0
	        --                                              --AND iDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	        --                  AND (
	        --                          @SITE = 0
	        --                          OR (
	        --                                 V.iSiteID = @Site_Code
	        --                                 AND (iPaySiteID IS NULL OR iPaySiteID = @Site_Code)
	        --                             )
	        --                      )
	        --              ) 
	           UNION ALL 
	           --PROMO    
	           
	           
	           SELECT 'PromoCashableAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  strVoucherStatus = 'PD'
	                  AND LEFT(strBarCode, 1) IN (@NONCashable, @Cashable)
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteid = @Site_Code)) 
	           UNION ALL    
	           SELECT 'PromoCashableCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  strVoucherStatus = 'PD'
	                  AND LEFT(strBarCode, 1) IN (@NONCashable, @Cashable)
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteid = @Site_Code)) 
	           UNION ALL    
	           SELECT 'NonCashableINAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  strvoucherstatus = 'PD'
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 1
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteid = @Site_Code)) 
	           
	           UNION ALL    
	           SELECT 'NonCashableINCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  strvoucherstatus = 'PD'
	                  AND dtPaid BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 1
	                  AND v.iPayDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iPayDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (@SITE = 0 OR (iPaySiteid = @Site_Code)) 
	           
	           
	           UNION ALL    
	           SELECT 'NonCashableOutAmount' AS DESCRIPTION,
	                  CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100 AS 
	                  val
	           FROM   voucher V
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 1
	                  AND ISNULL(strVoucherStatus, '') NOT IN ('NA')
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (
	                          @SITE = 0
	                          OR (
	                                 V.iSiteID = @Site_Code
	                                 AND (
	                                         iPaySiteID IS NULL
	                                         OR iPaySiteID = @Site_Code
	                                         OR strVoucherStatus = 'LT'
	                                     )
	                             )
	                      ) 
	           UNION ALL    
	           SELECT 'NonCashableOutCount' AS DESCRIPTION,
	                  COUNT(1) AS val
	           FROM   voucher V
	           WHERE  dtPrinted BETWEEN @startdate AND @enddate
	                  AND ISNULL(Ticket_Type, 0) = 1
	                  AND ISNULL(strVoucherStatus, '') NOT IN ('NA') 
	                  AND v.iDeviceID IN (SELECT d.iDeviceID FROM Device D WHERE D.strSerial IN (SELECT m.Machine_Stock_No FROM MACHINE M
												INNER JOIN Installation I
													   ON  m.Machine_ID = i.Machine_ID
												  INNER JOIN Bar_Position Bp
													   ON  bp.Bar_Position_ID = i.Bar_Position_ID
												  LEFT JOIN Route_Member RM
													   ON  bp.Bar_Position_ID = RM.Bar_Position_ID
											WHERE (@Route_No = 0 OR Rm.Route_ID = @Route_No)) AND (@SITE = 0 OR (D.iSiteid = @Site_Code)))
	                      --AND iDeviceID in(Select iDeviceId from Device WHERE strSerial in(select Machine_Stock_No  from Machine )
	                      --												AND (@SITE=0 OR (iSiteid=@Site_Code)))
	                  AND (
	                          @SITE = 0
	                          OR (
	                                 V.iSiteID = @Site_Code
	                                 AND (
	                                         iPaySiteID IS NULL
	                                         OR iPaySiteID = @Site_Code
	                                         OR strVoucherStatus = 'LT'
	                                     )
	                             )
	                      )
	       ) p 
	       PIVOT(
	           MAX(val) 
	           FOR DESCRIPTION     
	           IN (CDPaidAmount, CDPaidCount, CDPrintedAmount, CDPrintedCount, 
	              HandPayAmount, HandPayCount, ShortPayAmount, ShortPayCount,
	               OfflineVoucherAmount,
     OfflineVoucherCount,
	              JackpotAmount, JackpotCount, ProgAmount, ProgCount, VoidAmount, 
	              VoidCount, MCPaidAmount, MCPaidCount, MCPrintAmount, 
	              MCPrintCount, ActiveCashableVoucherAmount, 
	              ActiveCashableVoucherCount, VoidTicketsAmount, 
	              VoidTicketsCount, VoidVoucherAmount, VoidVoucherCount, 
	              CancelledAmount, CancelledCount, ExpiredAmount, ExpiredCount, 
	              TicketInExceptionAmount, TicketInExceptionCount, 
	              TicketOutExceptionAmount, TicketOutExceptionCount, 
	              CashableVoucherLiabilityAmount, CashableVoucherLiabilityCount, 
	              PromoCashableAmount, PromoCashableCount, NonCashableINAmount, 
	              NonCashableINCount, NonCashableOutAmount, NonCashableOutCount)
	       ) AS pvt
END
GO

