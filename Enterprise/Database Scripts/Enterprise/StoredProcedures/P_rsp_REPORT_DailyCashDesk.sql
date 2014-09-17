--exec rsp_Report_DailyCashDesk @startdate='2014-01-01 19:16:23',@enddate='2014-04-04 19:16:22.390',@Site=55,@Region='', @RouteNo = 0, @UserNo = 0
--SELECT * FROM [Site] s
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_DailyCashDesk]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_DailyCashDesk]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/***
rsp_REPORT_DailyCashDesk		Kirubakar S		08 JUN 2010
***/
CREATE PROCEDURE [dbo].[rsp_REPORT_DailyCashDesk]
	@startdate AS DATETIME, -- passed in as gaming date                  
	@enddate AS DATETIME, -- passed in as gaming date                  
	@Site AS INT, 
	@Region AS VARCHAR(10) ,
	@RouteNo INT = 0,
	@UserNo INT
	
	AS                    
	
	SET DATEFORMAT dmy                    
	SET NOCOUNT ON                   
	
	DECLARE @end_of_gaming_day		DATETIME
	DECLARE @start_of_gaming_day	DATETIME
	DECLARE @RouteName				VARCHAR(50)
	DECLARE @IncludeVoucherClaimed	VARCHAR(10)
	
	IF 1=0 BEGIN          
    SET FMTONLY OFF          
	END 
	
	DECLARE @Site_Code            VARCHAR(50)  
	SELECT @Site_Code = Site_Code
	FROM   [Site]
	WHERE  Site_ID = @Site  
	
	SET @end_of_gaming_day = @enddate            
	SET @start_of_gaming_day = @startdate        
	
	IF (@RouteNo = 0)
	BEGIN
		SET @RouteNo = NULL
		SET @RouteName = 'ALL'	  
	END
	ELSE
	BEGIN
		SELECT @RouteName = Route_Name
		FROM   [Route]
		WHERE  Route_ID = @RouteNo
	END
	  
	DECLARE @UserName VARCHAR(100)
	IF @UserNo = 0 
		SET @UserName = 'ALL'
	ELSE
		SELECT @UserName = S.Staff_Last_Name + ' ' + S.Staff_First_Name 
		FROM [Staff] S
		WHERE S.UserTableID = @UserNo
	
	DECLARE @RouteMembers  TABLE (Stock_No VARCHAR(200), Machine_no INT) 
	IF (ISNULL(@RouteNo, 0) <> 0)
	BEGIN
	    INSERT INTO @RouteMembers
	    SELECT m.machine_stock_no ,
	           m.machine_id
	    FROM   Route_Member rm WITH(NOLOCK)
	           INNER JOIN ROUTE r WITH(NOLOCK)
	                ON  rm.Route_id = r.Route_id
	           INNER JOIN Installation i
	                ON  i.Bar_Position_ID = rm.Bar_Position_ID
	           INNER JOIN [Machine] m
	                ON  m.machine_id = i.machine_id
	    WHERE  r.Route_id = @RouteNo
	           AND i.Installation_End_Date IS NULL
	END
	  
	EXEC [rsp_GetSiteSetting] @Site, 'IncludeVoucherClaimedInSlotInCDMReport', @IncludeVoucherClaimed
	
	SELECT i.installation_id,
	       site_name = SITE.Site_name,
	       z.zone_name,
	       posname = bp.bar_position_name,
	       cat.machine_type_code,
	       machinename = mc.Machine_name,
	       m.Machine_stock_no,
	       cash_collected_50000p = COALESCE(SUM(vwc.cash_collected_50000p), 0),
	       cash_collected_20000p = COALESCE(SUM(vwc.cash_collected_20000p), 0),
	       cash_collected_10000p = COALESCE(SUM(vwc.cash_collected_10000p), 0),
	       cash_collected_5000p = COALESCE(SUM(vwc.cash_collected_5000p), 0),
	       cash_collected_2000p = COALESCE(SUM(vwc.cash_collected_2000p), 0),
	       cash_collected_1000p = COALESCE(SUM(vwc.cash_collected_1000p), 0),
	       cash_collected_500p = COALESCE(SUM(vwc.cash_collected_500p), 0),
	       cash_collected_100p = CASE 
	                                  WHEN @Region = 'AR' THEN COALESCE(SUM(vwc.cash_collected_200p), 0)
	                                  ELSE COALESCE(SUM(vwc.cash_collected_100p), 0)
	                             END,
	       net_coin = COALESCE(
	           (
	                    (
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_1p), 0) AS FLOAT) 
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_2p), 0) AS FLOAT) 
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_5p), 0) AS FLOAT) 
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_10p), 0) AS FLOAT) 
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_20p), 0) AS FLOAT) 
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_50p), 0) AS FLOAT)
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_100p), 0) AS FLOAT)
	                             +
	                             CAST(ISNULL(SUM(vwc.Cash_Collected_200p), 0) AS FLOAT)
	                         )
	           ),
	           0
	       ) 
	       INTO #tmpTable
	FROM   installation i
	       JOIN MACHINE m
	            ON  m.machine_id = i.machine_id
	       JOIN bar_position bp
	            ON  bp.bar_position_ID = i.bar_position_ID
	            AND bp.site_id = @Site
	       --LEFT JOIN Route_Member RM
	       --     ON  RM.Bar_Position_ID = bp.Bar_Position_ID
	       JOIN SITE
	            ON  bp.site_id = SITE.site_id
	       JOIN machine_type cat
	            ON  cat.machine_type_id = m.machine_category_id
	       JOIN machine_class mc
	            ON  mc.machine_class_id = m.machine_class_id
	       LEFT JOIN zone z
	            ON  z.zone_id = bp.zone_id
	       LEFT JOIN vw_collectiondata vwc
	            ON  vwc.installation_id = i.installation_id
					AND vwc.BatchDateTime BETWEEN @start_of_gaming_day AND @end_of_gaming_day
		   LEFT JOIN [User] U ON  vwc.[UserName] = U.[UserName]
	WHERE
			(
	           ( ( i.installation_End_Date IS NULL and cast ( i.installation_start_date + ' ' + i.installation_start_time as datetime ) < @startdate )              
				OR (( cast ( i.installation_End_Date + ' ' + i.installation_End_Time as datetime ) BETWEEN @Startdate and @enddate)             
				and (cast ( i.installation_start_date + ' ' + i.installation_start_time  as datetime ) < @startdate)  )            
				OR ( cast ( i.installation_start_date + ' ' + i.installation_start_time  as datetime ) BETWEEN @Startdate and @enddate and i.installation_end_date is null )              
				OR ( cast ( i.installation_start_date + ' ' + i.installation_start_time  as datetime ) BETWEEN @Startdate and @enddate and cast ( i.installation_End_Date + ' ' + i.installation_End_Time as datetime ) > @enddate)              
				OR ( cast ( i.installation_start_date + ' ' + i.installation_start_time  as datetime ) BETWEEN @Startdate and @enddate and cast ( i.installation_end_Date + ' ' + i.installation_end_time as datetime ) BETWEEN @Startdate and @enddate )              
				OR ( cast ( i.installation_start_date + ' ' + i.installation_start_time  as datetime ) < @startdate and cast ( i.installation_end_date + ' ' + i.installation_end_time as datetime ) > @enddate) )                   
        
	           AND BP.Bar_Position_End_Date IS NULL
	           --AND m.Machine_End_Date IS NULL
	           AND SITE.site_id = @Site	           
	           AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	           AND ((ISNULL(@UserNo, 0) = 0) OR (U.SecurityUserID = @UserNo)) 
	       )
	GROUP BY
	       SITE.Site_name,
	       z.zone_name,
	       bp.bar_position_name,
	       cat.machine_type_code,
	       mc.Machine_name,
	       m.Machine_stock_no,
	       i.installation_id,
	       i.installation_end_date            
	
	
	
	SELECT i.installation_id,
	       Shortpays = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type IN ('SHORTPAY', 'Offline Voucher-Shortpay')
	                      AND Treasury_Reason <> 'NEGATIVE TREASURY ENTRY'
	                      AND ISNULL(Treasury_Membership_No, 0) <> -99
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo)) 
	           ),
	           0
	       ),
	       Handpays = COALESCE(
	           (
	               SELECT ISNULL(SUM(Treasury_Amount), 0)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type IN ('Attendantpay Credit', 'Attendantpay JACKPOT', 'Handpay Credit', 'Handpay Jackpot')
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND treasury_reason_code = 0
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       Refills = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type = 'REFILL'
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       Refunds = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type = 'REFUND'
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))	                      
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       Void = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type = 'VOID'
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       Expired = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type = 'EXPIRED'
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       Progressive_Value_Declared = COALESCE(
	           (
	               SELECT SUM(Treasury_Amount)
	               FROM   Treasury_Entry T --New Progressive
	                      
	                      INNER JOIN installation Ins
	                           ON  Ins.installation_id = t.Installation_ID
	                      INNER JOIN Bar_Position bp
	                           ON  bp.Bar_Position_ID = Ins.Bar_Position_ID
	                      --LEFT  JOIN Route_Member rm
	                      --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	               WHERE  Treasury_Type IN ('Progressive', 'Prog')
	                      AND CAST(Treasury_Date + ' ' + Treasury_Time AS DATETIME) 
	                          BETWEEN @startdate AND @enddate
	                      AND treasury_reason_code = 0
	                      AND T.installation_id = i.installation_id
	                      AND t.Collection_ID <> 0
	                      AND ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (T.UserID = @UserNo))
	           ),
	           0
	       ),
	       claimed_in_cashdesk = COALESCE(
	           (
	               SELECT SUM(CAST(iamount AS FLOAT)) / 100
	               FROM   voucher v
	                      JOIN device paydevice
	                           ON  v.iPaydeviceid = paydevice.ideviceid
	                      JOIN device printdevice
	                           ON  v.ideviceid = printdevice.ideviceid
	                      JOIN siteworkstations tiw
	                           ON  tiw.Site_Workstation = paydevice.strserial
	               WHERE  printdevice.strserial = m.Machine_Stock_No
	                      AND (
	                              (
	                                  v.dtpaid BETWEEN CAST(
	                                      ins.Installation_Start_Date + ' ' +
	                                      ins.Installation_Start_Time AS 
	                                      DATETIME
	                                  ) AND CAST(
	                                      ins.Installation_End_Date + ' ' + ins.Installation_End_Time 
	                                      AS DATETIME
	                                  )
	                              )
	                              OR (
	                                     v.dtpaid > CAST(
	                                         ins.Installation_Start_Date + ' ' +
	                                         ins.Installation_Start_Time AS 
	                                         DATETIME
	                                     )
	                                     AND ins.Installation_End_Date IS NULL
	                                 )
	                          )
	                      AND v.ipaysiteid = @Site_Code
	                      AND v.strVoucherStatus = 'PD'
	                      AND v.dtpaid BETWEEN @startdate AND @enddate
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (v.VoucherRedeemedUser = @UserNo))
	           ),
	           0
	       ),
	       printed_by_machine = COALESCE(
	           (
	               SELECT SUM(CAST(vp.iamount AS FLOAT) / 100)
	               FROM   voucher vp
	               WHERE  vp.ideviceid IN (SELECT ideviceid
	                                       FROM   device
	                                       WHERE  device.strserial = m.Machine_Stock_No)
	                      AND (
	                              vp.iSiteID = @Site_Code
	                              AND (vp.iPaySiteID IS NULL OR vp.iPaySiteID = @Site_Code)
	                          )
	                      AND COALESCE(vp.strVoucherStatus, '') NOT IN ('LT', 'NA')
	                      AND vp.dtPrinted BETWEEN @startdate AND @enddate
	           ),
	           0
	       ),
	       claimed_by_machine = COALESCE(
	           (
	               SELECT SUM(CAST(vc.iamount AS FLOAT) / 100)
	               FROM   voucher vc
	                      JOIN device tmppaydevice
	                           ON  vc.ipaydeviceid = tmppaydevice.ideviceid
	               WHERE  tmppaydevice.strserial = m.Machine_Stock_No
	                      AND (
	                              (
	                                  vc.dtpaid BETWEEN CAST(
	                                      ins.Installation_Start_Date + ' ' +
	                                      ins.Installation_Start_Time AS 
	                                      DATETIME
	                                  ) AND CAST(
	                                      ins.Installation_End_Date + ' ' + ins.Installation_End_Time 
	                                      AS DATETIME
	                                  )
	                              )
	                              OR (
	                                     vc.dtpaid > CAST(
	                                         ins.Installation_Start_Date + ' ' +
	                                         ins.Installation_Start_Time AS 
	                                         DATETIME
	                                     )
	                                     AND ins.Installation_End_Date IS NULL
	                                 )
	                          )
	                      AND vc.strVoucherStatus = 'PD'
	                      AND vc.dtpaid BETWEEN @startdate AND @enddate
	                      AND vc.ipaysiteid = @Site_Code
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (vc.VoucherRedeemedUser = @UserNo))
	           ),
	           0
	       ),
	       claimed_in_cashdesk_qty = COALESCE(
	           (
	               SELECT COUNT(*)
	               FROM   voucher v
	                      JOIN device paydevice
	                           ON  v.iPaydeviceid = paydevice.ideviceid
	                      JOIN device printdevice
	                           ON  v.ideviceid = printdevice.ideviceid
	                      JOIN siteworkstations tiw
	                           ON  tiw.Site_Workstation = paydevice.strserial
	               WHERE  printdevice.strserial = m.Machine_Stock_No
	                      AND (
	                              (
	                                  v.dtpaid BETWEEN CAST(
	                                      ins.Installation_Start_Date + ' ' +
	                                      ins.Installation_Start_Time AS 
	                                      DATETIME
	                                  ) AND CAST(
	                                      ins.Installation_End_Date + ' ' + ins.Installation_End_Time 
	                                      AS DATETIME
	                                  )
	                              )
	                              OR (
	                                     v.dtpaid > CAST(
	                                         ins.Installation_Start_Date + ' ' +
	                                         ins.Installation_Start_Time AS 
	                                         DATETIME
	                                     )
	                                     AND ins.Installation_End_Date IS NULL
	                                 )
	                          )
	                      AND v.ipaysiteid = @Site_Code
	                      AND v.strVoucherStatus = 'PD'
	                      AND v.dtpaid BETWEEN @startdate AND @enddate
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (v.VoucherRedeemedUser = @UserNo))
	           ),
	           0
	        ),
	        Void_Voucher_Amount =COALESCE(
	           (
	               SELECT CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100.00	                  
	           FROM   voucher
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtVoid BETWEEN @startdate AND @enddate
	                  ) 
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT 
	                                                                Site_Workstation
	                                                         FROM   dbo.SiteWorkstations
	                                                         WHERE  (@SITE = 0 OR (site_ID = @SITE)))
	                                           AND (@SITE = 0 OR (iSiteid = @Site_Code)))
	           ),
	           0
	       )       
	       INTO #tmpTable1
	FROM   #tmpTable i
	       JOIN installation ins
	            ON  i.installation_id = ins.installation_id
	       INNER JOIN Bar_Position bp
	            ON  bp.Bar_Position_ID = ins.Bar_Position_ID
	            AND bp.Site_ID = @Site
	       INNER JOIN SITE
	            ON  bp.site_id = SITE.site_id
	       --LEFT JOIN Route_Member Rm
	       --     ON  rm.Bar_Position_ID = bp.Bar_Position_ID
	       JOIN MACHINE m
	            ON  m.machine_id = ins.machine_id   
	                     
	WHERE ((ISNULL(@Routeno, 0) = 0) OR  (m.machine_id IN (SELECT Machine_no FROM @RouteMembers)))
		AND bp.Bar_Position_End_Date IS NULL
	
	SELECT #tmpTable.site_name,
	       #tmpTable.zone_name,
	       #tmpTable.posname,
	       #tmpTable.machine_type_code,
	       --   #tmpTable.machinename,              
	       CASE #tmpTable.machinename
	            WHEN 'Auto Detected' THEN 'Multi Game'
	            ELSE #tmpTable.machinename
	       END AS machinename,
	       #tmpTable.machine_stock_no,
	       #tmpTable.cash_collected_50000p,
	       #tmpTable.cash_collected_20000p,
	       #tmpTable.cash_collected_10000p,
	       #tmpTable.cash_collected_5000p,
	       #tmpTable.cash_collected_2000p,
	       #tmpTable.cash_collected_1000p,
	       #tmpTable.cash_collected_500p,
	       #tmpTable.cash_collected_100p,
	       #tmpTable.net_coin,
	       0 claimed_in_cashdesk,	--= SUM(claimed_in_cashdesk),              
	       0 claimed_in_cashdesk_qty,	-- = SUM(claimed_in_cashdesk_qty),              
	       #tmpTable1.Shortpays,
	       #tmpTable1.Handpays,
	       #tmpTable1.Refills,
	       #tmpTable1.Refunds,
	       #tmpTable1.Void,
	       #tmpTable1.Expired,
	       #tmpTable1.printed_by_machine,
	       #tmpTable1.Claimed_by_machine,
	       #tmpTable1.Progressive_Value_Declared,
	       #tmpTable1.Void_Voucher_Amount
	       INTO #TEMP
	FROM   #tmpTable
	       JOIN #tmpTable1
	            ON  #tmpTable.installation_id = #tmpTable1.installation_id 
	
	UNION 
	
	-- find information from the cashdesk point of view                    
	SELECT site_name = SITE.Site_name,
	       zone_name = '',
	       posname = '',
	       machine_type_code = 'CASHDESK',
	       machinename = 'CASHDESK',
	       Machine_Stock_No = '',
	       cash_collected_50000p = 0,
	       cash_collected_20000p = 0,
	       cash_collected_10000p = 0,
	       cash_collected_5000p = 0,
	       cash_collected_2000p = 0,
	       cash_collected_1000p = 0,
	       cash_collected_500p = 0,
	       cash_collected_100p = 0,
	       net_coin = 0,
	       claimed_in_cashdesk = 
				CASE WHEN @IncludeVoucherClaimed = 'True' 
					THEN
						   COALESCE(
							   (
								   SELECT SUM(CAST(ISNULL(iamount, 0) AS FLOAT) / 100)
								   FROM   voucher v
										  --JOIN device paydevice
										  --     ON  v.iPaydeviceid = paydevice.ideviceid
										  --JOIN device printdevice
										  --     ON  v.ideviceid = printdevice.ideviceid
										  --JOIN siteworkstations mytiw
										  --     ON  mytiw.Site_Workstation = paydevice.strserial
								   WHERE  --printdevice.strserial = itiw.Site_Workstation                        
										  /*and*/ v.ipaysiteid = @Site_Code
										  AND v.strVoucherStatus = 'PD'
										  AND v.dtpaid BETWEEN @startdate AND @enddate
										  AND ((ISNULL(@UserNo, 0) = 0) OR (v.VoucherRedeemedUser = @UserNo))
							   ),
							   0
									)
					ELSE
						COALESCE(
							   (
								   SELECT SUM(CAST(ISNULL(iamount, 0) AS FLOAT) / 100)
								   FROM   voucher v
										  JOIN device paydevice
										       ON  v.iPaydeviceid = paydevice.ideviceid
										  JOIN device printdevice
										       ON  v.ideviceid = printdevice.ideviceid
										  JOIN siteworkstations mytiw
										       ON  mytiw.Site_Workstation = paydevice.strserial
								   WHERE  --printdevice.strserial = itiw.Site_Workstation                        
										  /*and*/ v.ipaysiteid = @Site_Code
										  AND v.strVoucherStatus = 'PD'
										  AND v.dtpaid BETWEEN @startdate AND @enddate
										  AND ((ISNULL(@UserNo, 0) = 0) OR (v.VoucherRedeemedUser = @UserNo))
							   ),
							   0
							) 
					 END,
	       claimed_in_cashdesk_qty = COALESCE(
	           (
	               SELECT COUNT(*)
	               FROM   voucher v
	                      JOIN device paydevice
	                           ON  v.iPaydeviceid = paydevice.ideviceid
	                      JOIN device printdevice
	                           ON  v.ideviceid = printdevice.ideviceid
	                      JOIN siteworkstations tiw
	                           ON  tiw.Site_Workstation = paydevice.strserial
	               WHERE  --printdevice.strserial = itiw.Site_Workstation   
	                      /*and */v.dtPaid BETWEEN @startdate AND @enddate
	                      AND v.ipaysiteid = @Site_Code
	                      AND v.strVoucherStatus = 'PD'
	                      AND ((ISNULL(@UserNo, 0) = 0) OR (v.VoucherRedeemedUser = @UserNo))
	           ),
	           0
	       ),
	       Shortpays = 0,
	       Handpays = 0,
	       Refills = 0,
	       Refunds = 0,
	       Void = 0,
	       Expired = 0,
	       printed_by_machine = COALESCE(
	           (
	               SELECT SUM(CAST(iamount AS FLOAT) / 100)
	               FROM   voucher v
	                      JOIN device printdevice
	                           ON  v.ideviceid = printdevice.ideviceid
	                      JOIN siteworkstations mytiw
	                           ON  mytiw.Site_Workstation = printdevice.strserial
	               WHERE  printdevice.strserial = mytiw.Site_Workstation
	                      AND v.dtprinted BETWEEN @startdate AND @enddate
	                      AND (
	                              v.iSiteID = @Site_Code
	                              AND (v.iPaySiteID IS NULL OR v.iPaySiteID = @Site_Code)
	                          )
	                      AND COALESCE(v.strVoucherStatus, '') NOT IN ('LT', 'NA')
	           ),
	           0
	       ),
	       -- this is the same as claimed by cashdesk                    
	       claimed_by_machine = COALESCE(
	           (
	               SELECT SUM(CAST(iamount AS FLOAT) / 100)
	               FROM   voucher v
	                      JOIN device paydevice
	                           ON  v.iPaydeviceid = paydevice.ideviceid
	                      JOIN device printdevice
	                           ON  v.ideviceid = printdevice.ideviceid
	                      JOIN siteworkstations mytiw
	                           ON  mytiw.Site_Workstation = paydevice.strserial
	               WHERE  printdevice.strserial = mytiw.Site_Workstation
	                      AND v.ipaysiteid = @Site_Code
	                      AND v.strVoucherStatus = 'PD'
	                      AND v.dtpaid BETWEEN @startdate AND @enddate
	           ),
	           0
	       ),
	       Progressive_Value_Declared = 0,
	      Void_Voucher_Amount = COALESCE(
	           (
	               SELECT CAST(ISNULL(SUM(iAmount), 0)AS DECIMAL(10, 2)) / 100.00	                  
	           FROM   voucher
	           WHERE  (
	                      strVoucherStatus = 'VD'
	                      AND dtVoid BETWEEN @startdate AND @enddate
	                  ) 
	                  AND iDeviceID IN (SELECT iDeviceId
	                                    FROM   Device
	                                    WHERE  strSerial IN (SELECT 
	                                                                Site_Workstation
	                                                         FROM   dbo.SiteWorkstations
	                                                         WHERE  (@SITE = 0 OR (site_ID = @SITE)))
	                                           AND (@SITE = 0 OR (iSiteid = @Site_Code)))
	           ),
	           0
	       )       
	FROM   SITE,
	       Siteworkstations itiw 
	
	-- calculate and export
	--                    
	SELECT site_name,
	       zone_name,
	       posname,
	       machine_type_code,
	       machinename,
	       Machine_Stock_No AS stock_no,
	       cash_collected_50000p = SUM(cash_collected_50000p),
	       cash_collected_20000p = SUM(cash_collected_20000p),
	       cash_collected_10000p = SUM(cash_collected_10000p),
	       cash_collected_5000p = SUM(cash_collected_5000p),
	       cash_collected_2000p = SUM(cash_collected_2000p),
	       cash_collected_1000p = SUM(cash_collected_1000p),
	       cash_collected_500p = SUM(cash_collected_500p),
	       cash_collected_100p = SUM(cash_collected_100p),
	       net_coin = SUM(net_coin),
	       claimed_in_cashdesk = SUM(claimed_in_cashdesk),	--= SUM(claimed_in_cashdesk),                    
	       claimed_in_cashdesk_qty = SUM(claimed_in_cashdesk_qty),	-- = SUM(claimed_in_cashdesk_qty),                    
	       printed_by_machine = MAX(Printed_By_Machine),
	       Claimed_by_machine = MAX(Claimed_by_machine),
	       Shortpays = SUM(Shortpays),
	       Handpays = SUM(Handpays),
	       Refills = SUM(Refills),
	       Refunds = SUM(Refunds),
	       Void = SUM(Void),
	       Expired = SUM(Expired),
	       Liability = MAX(Printed_by_Machine) 
	       - MAX(claimed_in_cashdesk) 
	       - CASE 
	              WHEN machine_type_code = 'CASHDESK' THEN 0
	              ELSE MAX(Claimed_by_Machine)
	         END 
	       - SUM(Void),
	       Progressive_Value_Declared = SUM(Progressive_Value_Declared),
	       Void_Voucher_Amount = CAST(CASE WHEN machine_type_code = 'CASHDESK' THEN SUM(Void_Voucher_Amount) ELSE 0 END AS DECIMAL(18, 2)),
	       @RouteName AS RouteName,
	       @UserName AS UserName
	FROM   #TEMP
	WHERE  Site_name = (
	           SELECT site_name
	           FROM   [site]
	           WHERE  SITE.Site_ID = @Site
	       )
	GROUP BY
	       site_name,
	       zone_name,
	       posname,
	       machine_type_code,
	       machinename,
	       machine_stock_no
	ORDER BY
	       posname 
	
	
	
	DROP TABLE #TEMP 
	DROP TABLE #tmpTable 
	DROP TABLE #tmpTable1      
	
	
GO
