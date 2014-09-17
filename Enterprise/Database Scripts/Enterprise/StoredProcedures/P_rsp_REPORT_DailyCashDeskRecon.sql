USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_DailyCashDeskRecon]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_DailyCashDeskRecon]
GO
--[rsp_REPORT_DailyCashDeskRecon]  '2014-03-04 00:00:00.000' , '2014-03-06 00:00:00.000' ,55,0,0
USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_REPORT_DailyCashDeskRecon(
    @startdate  AS DATETIME,
	@enddate AS DATETIME,
	@Site AS INT,
	@RouteNo INT,
	@UserNo INT
)
AS
BEGIN
	SET DATEFORMAT dmy    
	SET NOCOUNT ON   
	IF 1 = 0
	BEGIN
	    SET FMTONLY OFF
	END
	
	DECLARE @UserName   VARCHAR(50)  
	DECLARE @RouteName  VARCHAR(50)

	DECLARE @Site_Code VARCHAR(50)
	IF (@Site <> 0)
	BEGIN
	    SELECT  @Site_Code=site_code
	    FROM   SITE
	    WHERE  Site_id = @Site
	END 
	


	IF @UserNo = 0
	BEGIN
	    SET @UserName = 'ALL'
	    SET @UserNo = NULL
	END
	ELSE
	BEGIN
	     SELECT @UserName = S.Staff_Last_Name + ' ' + S.Staff_First_Name
	    FROM   [Staff] S
	    WHERE  S.UserTableID = @UserNo
	END          
	
	IF ISNULL(@RouteNo,0)=0 SET @RouteNo = 0
	
	IF @RouteNo = 0
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
	
	DECLARE @Tickets       TABLE
	        (Amount DECIMAL(18, 2), Cnt INT, MACHINE VARCHAR(100))
	
	DECLARE @CDTickets     TABLE
	        (Amount DECIMAL(18, 2), MACHINE VARCHAR(100))
	
	
	DECLARE @Treasury      TABLE
	        (
	            Amount DECIMAL(18, 2),
	            Treasury_Type VARCHAR(200),
	            MACHINE VARCHAR(100)
	        )
	
	DECLARE @RouteMembers  TABLE (Stock_No VARCHAR(200), Machine_no INT) 
	
	--GET ALL GAMING AND CDO DETAILS          
	DECLARE @DeviceTable   TABLE (
	            strSerial VARCHAR(100),
	            Installation_no INT,
	            dType INT,
	            Bar_pos_name VARCHAR(30),
	            ideviceid INT
	        ) --dType (0-CDO, 1 - GAMING ASSET)
	
	
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
	
	INSERT INTO @DeviceTable
	SELECT m.machine_stock_no COLLATE 
	       database_default,
	       I.Installation_id,
	       1,
	       bp.bar_position_name,
	       d.iDeviceID
	FROM   dbo.Machine M WITH(NOLOCK)
	       INNER JOIN installation i
	            ON  m.machine_id = i.machine_id
	       INNER JOIN (
	                SELECT MAX(Installation_id) Installation_No,
	                       machine_id
	                FROM   Installation WITH(NOLOCK)
	                GROUP BY
	                       machine_id
	            ) NI
	            ON  i.Installation_id = ni.Installation_No
	       INNER JOIN bar_position bp
	            ON  bp.Bar_Position_ID = i.Bar_Position_ID
	       INNER JOIN .device d WITH(NOLOCK)
	            ON  d.strSerial = m.machine_stock_no COLLATE database_default
	WHERE  (ISNULL(@Routeno, 0) = 0)
	       OR  m.machine_id IN (SELECT machine_no
	                            FROM   @RouteMembers)
	
	DELETE 
	FROM   @DeviceTable
	WHERE  installation_no NOT IN (SELECT MAX(installation_no)
	                               FROM   @DeviceTable
	                               GROUP BY
	                                      strSerial)
	
	INSERT INTO @DeviceTable
	SELECT DISTINCT ts.Site_Workstation COLLATE 
	       database_default,
	       0,
	       0,
	       'CASHDESK',
	       d.iDeviceID
	FROM   dbo.SiteWorkstations ts WITH(NOLOCK)
	       INNER JOIN .device d WITH(NOLOCK)
	            ON  d.strSerial = ts.Site_Workstation COLLATE database_default      
	WHERE ts.site_ID=@Site            
	
	
	INSERT INTO @Tickets
	SELECT SUM(tv.iamount / 100.00),
	       COUNT(''),
	       id.strSerial
	FROM   voucher TV WITH(NOLOCK)
	       INNER  JOIN @DeviceTable PD
	            ON  TV.ipaydeviceid = PD.ideviceid
	       INNER JOIN @DeviceTable ID
	            ON  TV.ideviceid = ID.ideviceid
	WHERE  TV.strvoucherStatus = 'PD'
		  AND tv.ipaysiteid =@Site_Code 
	       AND TV.dtPaid BETWEEN @startDate AND @endDate
	       AND TV.ipaydeviceid IS NOT NULL
	       AND id.dType = 1
	       AND pd.dType = 0
	      AND (
	           	ISNUll(@UserNo,0)=0
	           	OR VoucherRedeemedUser = @UserNo 
	          )
	GROUP BY
	       id.strSerial             
	
	INSERT INTO @Treasury
	SELECT SUM(amount),
	       Treasury_Type,
	       machine_stock_no
	FROM   (
	           SELECT CASE 
	         	WHEN  T.Treasury_Amount < 0   THEN 'Void'
					WHEN Treasury_Type IN ('SHORTPAY', 'Offline Voucher-Shortpay') AND ISNULL(Treasury_Membership_No,0) <> -99   AND T.Treasury_Amount>0 THEN 'Shortpays'
					WHEN Treasury_Reason_Code = 0 AND Treasury_Type  IN ('Attendantpay Credit', 'Attendantpay JACKPOT', 
					'Handpay Credit', 'Handpay JACKPOT') AND T.Treasury_Amount>0   THEN 'Handpays'
					WHEN Treasury_Reason_Code = 0 AND Treasury_Type = 'REFILL' AND T.Treasury_Amount>0  THEN 'Refills'
					WHEN Treasury_Reason_Code = 0 AND Treasury_Type = 'REFUND' AND T.Treasury_Amount>0   THEN 'Refunds'
					WHEN Treasury_Reason_Code = 0 AND Treasury_Type = 'EXPIRED' AND T.Treasury_Amount>0   THEN 'Expired'
					WHEN Treasury_Reason_Code = 0 AND T.Treasury_Amount>0 AND Treasury_Type  IN ('Progressive', 'PROG') THEN 'Progressive_Value_Declared'
					END Treasury_Type,
					Treasury_Type t,
	           treasury_amount amount,
	           m.machine_stock_no
	           FROM treasury_entry T WITH(NOLOCK)
	           INNER JOIN installation I WITH(NOLOCK)
	           ON T.Installation_id = I .Installation_id
	           AND  CAST(treasury_date + ' ' + Treasury_Time AS DATETIME)  BETWEEN @StartDate AND @EndDate
	               INNER JOIN bar_position BP WITH(NOLOCK)
	               ON I.Bar_Position_ID = BP.Bar_Position_ID
	               INNER JOIN MACHINE M WITH(NOLOCK)
	               ON I.machine_id = M.machine_id 
	           WHERE  bp.site_id = @Site  
			   AND ((treasury_amount > 0
	           AND Treasury_Type IN ('SHORTPAY', 'Offline Voucher-Shortpay', 
	                                'Attendantpay Credit', 
	                                'Attendantpay JACKPOT', 'Handpay Credit', 
	                                'Handpay JACKPOT', 'REFILL', 'REFUND', 
	                                'EXPIRED', 'Progressive', 'PROG')
			   )
	           OR T.Treasury_Amount < 0)
	           AND 
	           (
	           	ISNUll(@Userno,0)=0 
	           	OR
	           		(
         			  (Treasury_Type = 'Shortpay' AND ISNULL(Treasury_Membership_No,0) <> -99  AND  AuthorizedUser_No =@Userno)
						OR   
 					   ( Treasury_Type <> 'Shortpay' AND UserID = @UserNo )
	           		)
	           	)	
	           	 
	       ) x
	GROUP BY
	       machine_stock_no,
	       Treasury_Type 

	DECLARE @OutPut TABLE(
	            site_name VARCHAR(200),
	            zone_name VARCHAR(100),
	            Bar_pos_Name VARCHAR(10),
	            machine_type_code VARCHAR(50),
	            machine_name VARCHAR(50),
	            stock_no VARCHAR(50),
	            claimed_in_cashdesk DECIMAL(30, 2),
	            claimed_in_cashdesk_qty INT,
	            Shortpays DECIMAL(30, 2),
	            Handpays DECIMAL(30, 2),
	            Refills DECIMAL(30, 2),
	            Refunds DECIMAL(30, 2),
	            Void DECIMAL(30, 2),
	            Expired DECIMAL(30, 2),
	            Progressive_Value_Declared DECIMAL(30, 2),
	            printed_by_cashdesk DECIMAL(30, 2),
	            UserName VARCHAR(100),
	            RouteName VARCHAR(100), 
	            UserNo INT 
	        )
	
	
	INSERT INTO @OutPut
	  (
	    site_name,
	    zone_name,
	    Bar_pos_Name,
	    machine_type_code,
	    machine_name,
	    stock_no,
	    claimed_in_cashdesk,
	    claimed_in_cashdesk_qty,
	    Shortpays,
	    Handpays,
	    Refills,
	    Refunds,
	    Void,
	    Expired,
	    Progressive_Value_Declared,
	    printed_by_cashdesk,
	    UserName,
	    RouteName
	  )
	SELECT s.site_name site_name,
	       zone_name,
	       Bar_position_Name,
	       machine_type_code,
	       mc.machine_name,
	       machine_stock_no,
	      (SELECT SUM(amount)FROM   @Tickets ci WHERE  ci.Machine = m.machine_stock_no) claimed_in_cashdesk,
		   (SELECT SUM(cnt)FROM   @Tickets ci WHERE  ci.Machine = m.machine_stock_no) claimed_in_cashdesk_qty,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no AND sh.Treasury_Type = 'Shortpays') Shortpays,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no AND sh.Treasury_Type = 'Handpays') Handpays,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no AND sh.Treasury_Type = 'Refills') Refills,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no  AND sh.Treasury_Type = 'Refunds') Refunds,
		   -1 * (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no  AND sh.Treasury_Type = 'Void') Void,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no  AND sh.Treasury_Type = 'Expired')Expired,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no  AND sh.Treasury_Type = 'Progressive_Value_Declared') Progressive_Value_Declared,
		   (SELECT SUM(amount) FROM @Treasury sh WHERE  sh.Machine = m.machine_stock_no  AND sh.Treasury_Type = 'printed_by_cashdesk') printed_by_cashdesk,
	       @UserName UserName,
	       @RouteName RouteName
	FROM   installation i WITH(NOLOCK)
	       INNER JOIN MACHINE m WITH(NOLOCK)
	            ON  m.machine_id= i.machine_id
	       INNER JOIN (
	                SELECT MACHINE
	                FROM   @Tickets
	                UNION 
	                SELECT MACHINE
	                FROM   @Treasury
	            ) tmac
	            ON  m.machine_stock_no = tmac.MACHINE
	       INNER JOIN bar_position bp WITH(NOLOCK)
	            ON  bp.Bar_Position_ID = i.Bar_Position_ID
	       INNER JOIN [Site] s WITH(NOLOCK)
	            ON  bp.site_id = s.site_id AND s.Site_ID = @Site
	        LEFT OUTER  JOIN machine_type cat WITH(NOLOCK)
	            ON  cat.machine_type_id = m.machine_category_id
	       INNER JOIN  machine_class mc WITH(NOLOCK)
	            ON  mc.machine_class_id = m.machine_class_id
	       LEFT OUTER JOIN zone z WITH(NOLOCK)
	            ON  z.zone_id = bp.zone_id
	WHERE  Installation_id IN (SELECT MAX(Installation_id)
	                           FROM   installation ti
	                                  INNER JOIN MACHINE tm
	                                       ON  ti.machine_id = tm.machine_id
	                           GROUP BY
	                                  tm.machine_stock_no)
	       AND (
	               ISNULL(@RouteNo, 0) = 0
	               OR m.machine_stock_no IN (SELECT r.Stock_No
	                                 FROM   @RouteMembers r)
	           ) 	
	
	
	DELETE 
	FROM   @Tickets
	
	IF (ISNULL(@RouteNo, 0) = 0)
	    INSERT INTO @Tickets
	    SELECT SUM(CAST(iamount / 100.00 AS DECIMAL(18, 2))),
	           COUNT(''),
	           ''
	    FROM   dbo.voucher v WITH(NOLOCK)
	           JOIN @DeviceTable PD 
	                ON  v.iPaydeviceid = PD.ideviceid
	           JOIN @DeviceTable ID 
	                ON  v.ideviceid = ID.ideviceid
	    WHERE  v.strVoucherStatus = 'PD'
	           AND v.iPaySiteID = @Site_Code
	           AND v.dtpaid BETWEEN @startdate AND @enddate
	           AND (ISNULL(@Userno, 0) = 0 OR v.VoucherRedeemedUser = @Userno)
	           AND pd.dType=0 AND id.dType=0 
		
		
	IF (ISNULL(@RouteNo, 0) = 0)
	    INSERT INTO @CDTickets
	    SELECT SUM(CAST(iamount / 100.00 AS DECIMAL(18, 2))),
			''
	    FROM   dbo.voucher v WITH(NOLOCK)
	           JOIN @DeviceTable ID
	                ON  v.ideviceid = ID.ideviceid
	    WHERE  v.dtprinted BETWEEN @startdate AND @enddate
			   AND  v.iSiteID=@Site_Code
	           AND ID.dType = 0
	            AND (
	            		ISNUll(@Userno,0)=0 
	           			OR 
	           			v.VoucherIssuedUser=@Userno 
					)
	    --GROUP BY
	    --       id.strserial    
	
	
	INSERT INTO @OutPut
	  (
	    site_name,
	    zone_name,
	    Bar_pos_Name,
	    machine_type_code,
	    machine_name,
	    stock_no,
	    claimed_in_cashdesk,
	    claimed_in_cashdesk_qty,
	    Shortpays,
	    Handpays,
	    Refills,
	    Refunds,
	    Void,
	    Expired,
	    Progressive_Value_Declared,
	    printed_by_cashdesk,
	    UserName,
	    RouteName
	  )
	SELECT s.site_name,
	       zone_name = '',
	       bar_pos_name = '',
	       machine_type_code = 'CASHDESK',
	       machinename = 'CASHDESK',
	       stock_no = '',
	       t.Amount,
	       t.Cnt,
	       0 Shortpays,
	       0 Handpays,
	       0 Refills,
	       0 Refunds,
	       0 Void,
	       0 Expired,
	       0 Progressive_Value_Declared,
	       (
	           SELECT cd.Amount
	           FROM   @CDTickets cd
	       ),
	       @UserName,
	       @RouteName
	FROM   SITE s,
	       @Tickets t
	WHERE s.Site_ID=@Site 
	
	SELECT site_name,
	       ISNULL(zone_name, '') zone_name,
	       Bar_pos_Name,
	       machine_type_code,
	       machine_name,
	       stock_no,
	       ISNULL(claimed_in_cashdesk, 0.00) claimed_in_cashdesk,
	       ISNULL(claimed_in_cashdesk_qty, 0) claimed_in_cashdesk_qty,
	       ISNULL(Shortpays, 0.00) Shortpays,
	       ISNULL(Handpays, 0.00) Handpays,
	       ISNULL(Refills, 0.00) Refills,
	       ISNULL(Refunds, 0.00) Refunds,
	       ISNULL(Void, 0.00) Void,
	       ISNULL(Expired, 0.00) Expired,
	       ISNULL(Progressive_Value_Declared, 0.00) Progressive_Value_Declared,
	       ISNULL(printed_by_cashdesk, 0.00) printed_by_cashdesk,
	       UserName,
	       RouteName
	FROM   @OutPut
	ORDER BY
	       bar_pos_name
END
GO

