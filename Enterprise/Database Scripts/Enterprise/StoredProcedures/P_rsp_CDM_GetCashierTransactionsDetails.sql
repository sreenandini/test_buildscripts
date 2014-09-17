USE [Enterprise]
 GO
 
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CDM_GetCashierTransactionsDetails]') AND type in (N'P', N'PC'))
 DROP PROCEDURE [dbo].[rsp_CDM_GetCashierTransactionsDetails]
 GO
 
USE [Enterprise]
 GO
 
 SET ANSI_NULLS ON
 GO
 
 SET QUOTED_IDENTIFIER ON
 GO
 
 /******************************************************************************
 	* Screen Name : CCashDeskManager
 	 Example : 	EXEC rsp_CDM_GetCashierTransactionsDetails  '2012-01-01 00:00:00.000','2012-03-01 23:00:00.000',null,0,0,0	,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0  

 ******************************************************************************/
 
 /*
 exec rsp_CDM_GetCashierTransactionsDetails 
 @startdate='2014-07-09 18:21:54.000',
 @enddate='2014-07-09 18:21:54.000',
 @Site=3,
 @Route_No=0,
 @User_No=0,
 @isCDMPaid=1,
 @isCDMPrinted=1,
 @isHandPay=1,
 @isShortpay=1,
 @isVoidVoucher=1,
 @isJackpot=1,
 @isProgressive=1,
 @isVoid=1,
 @isMachinePaid=1,
 @isMachinePrinted=1,
 @isActive=1,
 @isVoidCancel=1,
 @isExpired=1,
 @isException=1,
 @isLiability=1,
 @isPromo=0,
 @isNonCashableIN=1,
 @isNonCashableOut=1,
 @isOffline=1
 
 
 exec rsp_CDM_GetCashierTransactionsDetails @startdate='2014-07-09 18:21:54',@enddate='2014-07-09 18:21:54',@Site=3,@Route_No=0,
 @User_No=0,@isCDMPaid=0,@isCDMPrinted=1,@isHandPay=0,@isShortpay=0,@isVoidVoucher=0,@isJackpot=0,@isProgressive=0,@isVoid=0,
 @isMachinePaid=0,@isMachinePrinted=1,@isActive=1,@isVoidCancel=0,
 @isExpired=0,@isException=0,@isLiability=1,@isPromo=0,@isNonCashableIN=0,@isNonCashableOut=0,@isOffline=0
 SELECT * FROM DEVICETABLETEMP
 SELECT * FROM Temp_CDM
 
 
 */
 CREATE PROCEDURE dbo.rsp_CDM_GetCashierTransactionsDetails
 	@startdate DATETIME,
 	@enddate DATETIME,
 	@Site INT ,
 	@Route_No INT = 0 ,
 	@User_No INT = NULL,
 	@isCDMPaid BIT,
 	@isCDMPrinted BIT,
 	@isHandPay BIT,
 	@isShortpay BIT,
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
 	@isOffline BIT
 AS
 BEGIN
 	 
 	 DECLARE @CDMShowAllActiveAsLiable VARCHAR(100)    
 	  DECLARE @CDMIgnoreDeviceForGeneral VARCHAR(100)
	 SET DATEFORMAT ymd   
	 DECLARE @Site_Code VARCHAR(50)      
	 IF (@Site <> 0)  
	 BEGIN  
		 SELECT @Site_Code = site_code  
		 FROM   SITE  
		 WHERE  Site_id = @Site  
	 END    
    
       DECLARE @NONCashable  VARCHAR(500)                              
       DECLARE @Cashable     VARCHAR(500)                              
        
        
      EXEC dbo.rsp_GetSiteSetting @Site,'CDMShowAllActiveAsLiable',@CDMShowAllActiveAsLiable OUTPUT   
            
      EXEC dbo.rsp_GetSiteSetting @Site,'CDMIgnoreDeviceForGeneral',@CDMIgnoreDeviceForGeneral OUTPUT  
             
      EXEC  dbo.rsp_GetSetting 0,      
            'PROMO_TICKET_CODE',      
            '8',      
            @Cashable OUTPUT        
             
      EXEC  dbo.rsp_GetSetting 0,      
            'PROMO_TICKET_CODE_NONCASH',      
            '7',      
            @NONCashable OUTPUT              
      
     SET @CDMShowAllActiveAsLiable= ISNULL(@CDMShowAllActiveAsLiable,'False')
     SET @CDMIgnoreDeviceForGeneral= ISNULL(@CDMIgnoreDeviceForGeneral,'True')        
                 
   	/* 
 		*The @typemap holds the data for the transaction types and its source
 		*If any new type is being added, added its type in this temp table
 		*The "iSummaryType" is the Source under which the transaction type is associated.
 		*The "iTran_Type" will be used for figuring the transaction type
 	*/          
       
 		--Filter data . Delete unwanted at final query 
 		DECLARE @Filter TABLE (FilterList INT )
 		INSERT INTO @Filter VALUES(-1)
 		
	 	IF ISNULL(@isCDMPaid ,0)=0
	 		INSERT INTO @Filter VALUES(9)
	 		
 		IF ISNULL(@isCDMPrinted ,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(10)
 			INSERT INTO @Filter VALUES(45)
 			INSERT INTO @Filter VALUES(46)
 		END 
 			
 		IF ISNULL(@isHandPay,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(1)
 			INSERT INTO @Filter VALUES(2)
 		END 
 		
 		IF ISNULL(@isShortpay,0)=0
 			 INSERT INTO @Filter VALUES(7)
 			 
 		IF ISNULL(@isVoidVoucher ,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(38)
 			INSERT INTO @Filter VALUES(39)
 		END 
 		
 		IF ISNULL(@isJackpot,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(3)
 			INSERT INTO @Filter VALUES(4)
 		END
 		
 		IF ISNULL(@isProgressive ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(5)
 			INSERT INTO @Filter VALUES(6)
 		END		
 		
 		IF ISNULL(@isVoid,0)=0
 		BEGIN
			INSERT INTO @Filter VALUES(28)
			INSERT INTO @Filter VALUES(29)
			INSERT INTO @Filter VALUES(30)
			INSERT INTO @Filter VALUES(31)
			INSERT INTO @Filter VALUES(32)
			INSERT INTO @Filter VALUES(33)
			INSERT INTO @Filter VALUES(34)
			INSERT INTO @Filter VALUES(35)
			INSERT INTO @Filter VALUES(36)
			INSERT INTO @Filter VALUES(37)
		END 	
		
 		IF ISNULL(@isMachinePaid ,0)=0
			INSERT INTO @Filter VALUES(11)
 
 		IF ISNULL(@isMachinePrinted ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(12)
 		END 
 		
 		IF ISNULL(@isActive ,0)=0
 		BEGIN	
 			INSERT INTO @Filter VALUES(13)
			INSERT INTO @Filter VALUES(14)
 		END
 		
 		IF ISNULL(@isVoidCancel ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(15)
 			INSERT INTO @Filter VALUES(16)
 			INSERT INTO @Filter VALUES(40)
 			INSERT INTO @Filter VALUES(41)
 		END 	
 		
 		IF ISNULL(@isExpired ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(17)
 			INSERT INTO @Filter VALUES(18)
 		END 	
 		
 		IF ISNULL(@isException ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(19)
 			INSERT INTO @Filter VALUES(20)
 			INSERT INTO @Filter VALUES(21)
 			INSERT INTO @Filter VALUES(22)
 		END 
 			
 		IF ISNULL(@isLiability ,0)=0
 		BEGIN
 			INSERT INTO @Filter VALUES(23)
 			INSERT INTO @Filter VALUES(24)
 		END 
 		
 		IF ISNULL(@isPromo ,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(25)
 		END 
 		
 		IF ISNULL(@isNonCashableIN ,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(26)
 		END	
 		
 		IF ISNULL(@isNonCashableOut ,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(27)
 		END 
 		
 		IF ISNULL(@isOffline,0)=0
 		BEGIN 
 			INSERT INTO @Filter VALUES(8)
 		END 	

              
   DECLARE @typemap TABLE(iSummaryType INT, iTran_Type INT ,Summary_Type VARCHAR(100), Trans_Type VARCHAR(100) )    
   INSERT INTO @typemap VALUES(1,1,'HandPayAmount','Attendantpay Credit')    
   INSERT INTO @typemap VALUES(1,2,'HandPayAmount','Manual Attendantpay Credit')    
   INSERT INTO @typemap VALUES(2,3,'JackpotAmount','Attendantpay Jackpot')    
   INSERT INTO @typemap VALUES(2,4,'JackpotAmount','Manual Attendantpay Jackpot')    
   INSERT INTO @typemap VALUES(3,5,'ProgAmount','Progressive Jackpot')    
   INSERT INTO @typemap VALUES(3,6,'ProgAmount','Manual Progressive Jackpot')    
   INSERT INTO @typemap VALUES(4,7,'ShortPayAmount','ShortPay')    
   INSERT INTO @typemap VALUES(5,8,'OfflineVoucherAmount','Offline Voucher-Shortpay')    
 
   INSERT INTO @typemap VALUES(6,9,'CDPaidAmount','CashDesk Claimed Voucher')    
   INSERT INTO @typemap VALUES(7,10,'CDPrintedAmount','CashDesk Issued Voucher')    
   INSERT INTO @typemap VALUES(8,11,'MCPaidAmount','Cashable Machine Claimed Voucher')    
   INSERT INTO @typemap VALUES(9,12,'MCPrintAmount','Cashable Machine Printed Voucher')    
   INSERT INTO @typemap VALUES(10,13,'ActiveCashableVoucherAmount','Cashable Active Voucher')    
   INSERT INTO @typemap VALUES(10,14,'ActiveCashableVoucherAmount','Non Cashable Active Voucher')    
       
   INSERT INTO @typemap VALUES(11,15,'CancelledAmount','Cashable Cancelled Voucher')    
   INSERT INTO @typemap VALUES(11,16,'CancelledAmount','Non Cashable Cancelled Voucher')    
   INSERT INTO @typemap VALUES(12,17,'ExpiredAmount','Cashable Expired Voucher')    
   INSERT INTO @typemap VALUES(12,18,'ExpiredAmount','Non Cashable Expired Voucher')    
   INSERT INTO @typemap VALUES(13,19,'TicketInExceptionAmount','Cashable Voucher IN Exception')    
   INSERT INTO @typemap VALUES(13,20,'TicketInExceptionAmount','Non Cashable Voucher IN Exception')    
   INSERT INTO @typemap VALUES(14,21,'TicketOutExceptionAmount','Cashable Voucher OUT Exception')    
   INSERT INTO @typemap VALUES(14,22,'TicketOutExceptionAmount','Non Cashable Voucher OUT Exception')    
   INSERT INTO @typemap VALUES(15,23,'CashableVoucherLiabilityAmount','Cashable Voucher Liability')    
   INSERT INTO @typemap VALUES(15,24,'CashableVoucherLiabilityAmount','Non Cashable Voucher Liability')    
   INSERT INTO @typemap VALUES(16,25,'PromoCashableAmount','Promo Cashable Voucher')    
   INSERT INTO @typemap VALUES(17,26,'NonCashableINAmount','Non Cashable Machine Claimed Voucher')    
   INSERT INTO @typemap VALUES(18,27,'NonCashableOutAmount' ,'Non Cashable Machine Printed Voucher')    
     
   INSERT INTO @typemap VALUES(19,28,'VoidAmount','VOID (Progressive Jackpot)')    
   INSERT INTO @typemap VALUES(19,29,'VoidAmount','VOID (Shortpay)')    
   INSERT INTO @typemap VALUES(19,30,'VoidAmount','VOID (Attendantpay Jackpot)')    
   INSERT INTO @typemap VALUES(19,31,'VoidAmount','VOID (Attendantpay Credit)')    --HandPayAmount
   INSERT INTO @typemap VALUES(19,32,'VoidAmount','VOID (Mystery Jackpot)')    
   INSERT INTO @typemap VALUES(19,33,'VoidAmount','VOID (Offline Voucher-Shortpay)')    
   INSERT INTO @typemap VALUES(19,34,'VoidAmount','VOID (Manual Progressive Jackpot)')    
   INSERT INTO @typemap VALUES(19,35,'VoidAmount','VOID (Manual Attendantpay Jackpot)')    
   INSERT INTO @typemap VALUES(19,36,'VoidAmount','VOID (Manual Attendantpay Credit)')    --HandPayAmount 
   INSERT INTO @typemap VALUES(19,37,'VoidAmount','VOID (Manual Mystery Jackpot)')    
   
   INSERT INTO @typemap VALUES(20,38,'VoidVoucherAmount','Cashable VOID Voucher(CD)')    
   INSERT INTO @typemap VALUES(20,39,'VoidVoucherAmount','Non Cashable VOID Voucher(CD)')    
   INSERT INTO @typemap VALUES(21,40,'VoidTicketsAmount','Cashable Void Voucher')    
   INSERT INTO @typemap VALUES(21,41,'VoidTicketsAmount','Non Cashable Void Voucher')
   
   INSERT INTO @typemap VALUES(22,42,'Mystery Jackpot','Mystery Jackpot')
   INSERT INTO @typemap VALUES(22,43,'Mystery Jackpot','Manual Mystery Jackpot')
   INSERT INTO @typemap VALUES(23,44,'defloat','defloat')
   --PROMO 
   INSERT INTO @typemap VALUES(7,45,'CDPrintedAmount','Promo Cashable CashDesk Issued Voucher')
   INSERT INTO @typemap VALUES(7,46,'CDPrintedAmount','Promo Non Cashable CashDesk Issued Voucher') 
 
     DECLARE @RouteMembers TABLE (Stock_No VARCHAR(200),Machine_no INT)
 
 	
 	IF (ISNUll(@Route_No,0) <>0)
 	BEGIN
 		INSERT INTO @RouteMembers
		SELECT m.Machine_Stock_NO,m.machine_id
		FROM   Route_Member rm WITH(NOLOCK)
		INNER JOIN ROUTE r WITH(NOLOCK)
		ON  rm.Route_ID = r.Route_ID 
		INNER JOIN Installation i
		ON i.Bar_Position_ID=rm.Bar_Position_ID 
		INNER JOIN [Machine] m 
		ON m.Machine_id=i.Machine_id
		WHERE r.Route_id=@Route_No
		AND i.Installation_End_Date IS NULL
		AND r.Site_ID= @Site
 	END 
 	
       --GET ALL GAMING AND CDO DETAILS        
       DECLARE @DeviceTable TABLE (      
                   strSerial VARCHAR(100),    
                  Installation_ID INT,      
                   dType INT,      
                  bar_position_name VARCHAR(30),      
                   ideviceid INT      
               ) --dType (0-CDO, 1 - GAMING ASSET)      
              
       --GET GAMING ASSETS      
       INSERT INTO @DeviceTable      
      SELECT M.Machine_Stock_NO COLLATE       
              database_default,      
             I.Installation_ID,    
              1,      
             bp.bar_position_name,      
              d.iDeviceID     
      FROM    dbo.Machine M WITH(NOLOCK)      
             INNER JOIN  installation i      
                  ON  m.Machine_ID = i.Machine_ID      
              INNER JOIN (      
                      SELECT MAX(Installation_ID) Installation_ID,      
                             Machine_ID      
                      FROM    Installation  WITH(NOLOCK)     
                       GROUP BY      
                             Machine_ID      
                   ) NI      
                  ON  i.Installation_ID = ni.Installation_ID      
             INNER JOIN  bar_position bp      
                  ON  bp.bar_position_ID = i.bar_position_ID      
              INNER JOIN device d WITH(NOLOCK)      
                  ON  d.strSerial = M.Machine_Stock_NO COLLATE database_default     
    WHERE bp.Site_ID = @Site      
	AND ( (ISNUll(@Route_no,0) =0)
       OR m.Machine_id IN (Select Machine_no FROM  @RouteMembers ))
           
                              
       DELETE     
       FROM   @DeviceTable    
      WHERE  Installation_ID NOT IN (SELECT MAX(Installation_ID)    
                                                    FROM   @DeviceTable    
                                                    GROUP BY    
                                                               strSerial)               
       
       
             
       INSERT INTO @DeviceTable      
      SELECT DISTINCT ts.Site_Workstation COLLATE       
              database_default,0,      
              0,      
              'CASHDESK',      
              d.iDeviceID      
      FROM    dbo.SiteWorkstations ts WITH(NOLOCK)      
              INNER JOIN device d WITH(NOLOCK)      
                  ON  d.strSerial = ts.Site_Workstation COLLATE database_default   
                  AND d.Site_Code = @Site_Code 
   
           
                                
       --      
       --SELECT * FROM @DeviceTable      
       DECLARE @temp TABLE      
               (      
                   iTrans_Type INT,
                   Trans_Type VARCHAR(200),      
                   PrintAsset VARCHAR(100),      
                   PrintSiteCode VARCHAR(10),      
                   PrintPosition VARCHAR(100),      
                   PrintedDate DATETIME,      
                   PaidAsset VARCHAR(100),      
                   PaidPosition VARCHAR(10),      
                   PaidDate DATETIME,      
                   Amount DECIMAL(18, 2),      
                   Ticket VARCHAR(100),      
                   Userid INT ,      
                   Stock_NO_ROUTE VARCHAR(100),    
                   Summary_Type VARCHAR(100)    
               )
               
         --TREASURY    
       IF (@isHandPay=1 OR 
 			@isShortpay =1 OR
 			@isJackpot =1 OR
 			@isProgressive =1 OR
 			@isVoid =1 OR  
 			@isOffline=1
       )  
       INSERT INTO @temp      
       SELECT --treasury_date,  isnull(treasury_reason,'')as treasury_reason,      
                   --zone_name, 
                  
            CASE WHEN (IsManualAttendantPay = 0) THEN      
                        CASE      
                         --VOID      
                         WHEN (Treasury_Type = 'Progressive' OR Treasury_Type = 'Prog')  AND treasury_amount < 0  THEN 28 --'VOID (Progressive Jackpot)'      
                         WHEN Treasury_Type = 'Shortpay'  AND treasury_amount < 0    THEN 29 -- 'VOID (Shortpay)'      
                         WHEN (Treasury_Type = 'AttendantPay Jackpot' OR Treasury_Type = 'Handpay Jackpot') AND treasury_amount < 0 THEN 30 --'VOID (Attendantpay Jackpot)'      
                         WHEN (Treasury_Type = 'AttendantPay Credit' OR Treasury_Type = 'Handpay Credit') AND treasury_amount < 0 THEN 31 --'VOID (Attendantpay Credit)'      
                         WHEN Treasury_Type = 'Mystery Jackpot' AND treasury_amount < 0 THEN 32 -- 'VOID (Mystery Jackpot)'      
                         WHEN Treasury_Type = 'Offline Voucher-Shortpay' AND  treasury_amount < 0  AND treasury_reason = 'NEGATIVE TREASURY ENTRY' THEN 33 --'VOID (Offline Voucher-Shortpay)'      
                   --ACTUAL      
                         WHEN(Treasury_Type = 'Progressive' OR Treasury_Type = 'Prog') AND Treasury_Reason_Code=0  THEN 5 --'Progressive Jackpot'      
 						WHEN Treasury_Type = 'Shortpay' AND ISNULL(Treasury_Membership_No,0) <> -99  THEN 7 --'ShortPay'      
                   		WHEN(Treasury_Type = 'AttendantPay Jackpot' OR Treasury_Type = 'Handpay Jackpot') AND Treasury_Reason_Code=0  THEN 3 --'Attendantpay Jackpot'      
                   		WHEN (treasury_type='AttendantPay Credit' OR treasury_type='AttendantPay Credit' or treasury_type='Handpay Credit') AND Treasury_Reason_Code=0  AND  treasury_amount > 0      THEN 1 --'Attendantpay Credit'      
                   		WHEN Treasury_Type = 'Mystery Jackpot' AND Treasury_Reason_Code=0  THEN 42 --'Mystery Jackpot'      
                   		WHEN Treasury_Type = 'Offline Voucher-Shortpay'  AND ISNULL(Treasury_Membership_No,0) <> -99  AND Treasury_Reason_Code=0 THEN 8 --'Offline Voucher-Shortpay'      
                   ELSE -1
                         END      
 			 ELSE      
                        CASE      
                        --VOID      
                         WHEN(Treasury_Type = 'Progressive' OR Treasury_Type = 'Prog') AND treasury_amount < 0  THEN 34 --'VOID (Manual Progressive Jackpot)'      
                         WHEN(Treasury_Type = 'AttendantPay Jackpot' OR Treasury_Type = 'Handpay Jackpot') AND treasury_amount < 0  THEN 35 --'VOID (Manual Attendantpay Jackpot)'      
                         WHEN(Treasury_Type = 'AttendantPay Credit' OR Treasury_Type = 'Handpay Credit') AND treasury_amount < 0  THEN 36 --'VOID (Manual Attendantpay Credit)'      
                         WHEN Treasury_Type = 'Mystery Jackpot' AND treasury_amount < 0 THEN 37 --'VOID (Manual Mystery Jackpot)'      
                         -- ACTUAL      
 						WHEN(Treasury_Type = 'Progressive' OR Treasury_Type = 'Prog') AND Treasury_Reason_Code=0  THEN 6 --'Manual Progressive Jackpot'      
 						WHEN(Treasury_Type = 'AttendantPay Jackpot' OR Treasury_Type = 'Handpay Jackpot') AND Treasury_Reason_Code=0 THEN 4 --'Manual Attendantpay Jackpot'      
 						WHEN(Treasury_Type = 'AttendantPay Credit' OR Treasury_Type = 'Handpay Credit') AND Treasury_Reason_Code=0  THEN 2 --'Manual Attendantpay Credit'      
 						WHEN Treasury_Type = 'Mystery Jackpot' AND IsManualAttendantPay = 1   AND Treasury_Reason_Code=0 THEN 43 --'Manual Mystery Jackpot'      
 						ELSE -1
                   END      
             END     
             ,NULL  
              ,              
             Machine_Stock_NO COLLATE DATABASE_DEFAULT AS PrintAsset,      
              @Site_Code AS PrintSiteCode,      
             bar_position_name AS PrintPosition,      
              Treasury_Actual_Date AS PrintedDate,      
              'CASHDESK' COLLATE DATABASE_DEFAULT AS PaidAsset,      
              'CASHDESK' COLLATE DATABASE_DEFAULT AS PaidPosition,      
            --  Bar_Pos_Name COLLATE DATABASE_DEFAULT AS PaidPosition,      
              treasury_date + ' ' + treasury_time AS PaidDate,      
              treasury_amount AS Amount,      
              ISNULL(T.Treasury_TicketNumber,'') COLLATE DATABASE_DEFAULT AS Ticket,      
              CASE WHEN Treasury_Type = 'Shortpay' AND ISNULL(Treasury_Membership_No,0) <> -99 
              THEN
				AuthorizedUser_No
              ELSE
          	UserID  
	    END AS User_No,      
            Machine_Stock_NO ,    
             null    
              --Treasury_Temp      
      FROM    treasury_entry T WITH(NOLOCK)      
             INNER JOIN  installation I WITH(NOLOCK)      
                  ON  T.Installation_ID = I .Installation_ID      
                  AND CAST(treasury_date + ' ' + Treasury_Time AS DATETIME) BETWEEN @StartDate AND @EndDate      
             INNER JOIN  bar_position BP WITH(NOLOCK)   
                  ON  I.bar_position_ID = BP.bar_position_ID      
             INNER JOIN  MACHINE M WITH(NOLOCK)      
               ON  I.Machine_ID = M.Machine_ID      
      WHERE  bp.site_id = @Site     
           AND	((ISNULL(@route_no,0)=0) 
			OR m.machine_ID IN (SELECT machine_no FROM @RouteMembers))
	
		DELETE FROM @temp WHERE iTrans_Type=-1
		 
        -- PRINTED VOUCHER   
       IF ISNULL(@isCDMPrinted,0)=1 OR ISNUll(@isMachinePrinted,0)=1    OR ISNULL(@isNonCashableOut,0)=1
       INSERT INTO @temp      
       SELECT CASE WHEN pt.VoucherID IS NOT NULL AND ISNULL(Ticket_Type, 0) = 0 THEN 45 --'Promo Cashable CashDesk Issued Voucher'  
 				  WHEN pt.VoucherID IS NOT NULL AND ISNULL(Ticket_Type, 0) = 1 THEN 46 --'Promo Non Cashable CashDesk Issued Voucher'
                   WHEN TV.iDeviceID IS NOT NULL AND ID.dType = 0 THEN 10 --'CashDesk Issued Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 0 AND TV.iDeviceID IS NOT NULL       
                        AND ID.dType = 1 THEN 12 --'Cashable Machine Printed Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1      
                        AND TV.iDeviceID IS NOT NULL      
                        AND ID.dType = 1 THEN 27 --'Non Cashable Machine Printed Voucher'      
              END ,NULL,      
              --ID.strSerial COLLATE DATABASE_DEFAULT as PrintAsset,      
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial COLLATE      
                        DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             ID.bar_position_name PrintPosition,
              --isnull(EB1.Bar_Pos_Name,'CASHDESK') COLLATE DATABASE_DEFAULT as PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              CASE       
                   WHEN strVoucherStatus IS NOT NULL      
                        AND strVoucherStatus = 'PD' THEN COALESCE(PD.strSerial, '')      
                   WHEN strVoucherStatus IS NOT NULL      
                        AND strVoucherStatus = 'LT' THEN 'N/A'      
                   ELSE ''      
              END AS PaidAsset,      
              CASE       
                   WHEN strVoucherStatus IS NOT NULL      
                       AND strVoucherStatus = 'PD' THEN PD.bar_position_name
                   WHEN strVoucherStatus IS NOT NULL      
                        AND strVoucherStatus = 'LT' THEN 'N/A'      
                   ELSE ''      
              END AS PaidPosition,      
              TV.dtPaid AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
             VoucherIssuedUser AS VoucherIssuedUser,
              ID.strSerial ,    
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              LEFT OUTER JOIN  @DeviceTable PD      
                   ON  TV.ipaydeviceid = PD.ideviceid      
              LEFT OUTER  JOIN   @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid 
               LEFT OUTER JOIN PromotionalTickets pt
               ON tv.iVoucherID=pt.VoucherID 
                AND TV.iSiteID  = pt.SiteId
                     
       WHERE  TV.iSITEID = @Site_Code      
              AND TV.dtPrinted BETWEEN @startDate AND @endDate      
              AND ISNULL(TV.StrVoucherStatus, '') NOT IN ('NA','VD')      
              AND TV.iDeviceID IS NOT NULL      
 			AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers))
 
       --CLAIMED VOUCHERS        
       IF ISNULL(@isCDMPaid,0)=1 OR ISNUll(@isMachinePaid,0)=1 OR ISNULL(@isNonCashableIN,0)=1 
       INSERT INTO @temp      
       SELECT CASE       
                   WHEN PD.dType = 0 THEN 9 --'CashDesk Claimed Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 0      
                        AND PD.dType = 1 THEN 11 --'Cashable Machine Claimed Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1      
                        AND PD.dType = 1 THEN 26 --'Non Cashable Machine Claimed Voucher'      
              END ,  NULL,       
              --ID.strSerial COLLATE DATABASE_DEFAULT as PrintAsset,      
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial COLLATE      
           DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
              CASE       
                  WHEN TV.iSITEID = @Site_Code THEN ID.bar_position_name 
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              COALESCE(PD.strSerial, '') COLLATE DATABASE_DEFAULT AS PaidAsset,      
              CASE       
                   WHEN strVoucherStatus IS NOT NULL      
                       AND strVoucherStatus = 'PD' THEN pd.bar_position_name 
                        COLLATE DATABASE_DEFAULT      
                   ELSE ''      
              END COLLATE DATABASE_DEFAULT AS PaidPosition,      
              TV.dtPaid AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,
             VoucherRedeemedUser AS VoucherRedeemedUser,
              pd.strSerial ,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
               LEFT OUTER JOIN @DeviceTable PD
                   ON  TV.ipaydeviceid = PD.ideviceid
              LEFT OUTER JOIN @DeviceTable ID
                   ON  TV.ideviceid = ID.ideviceid
       WHERE  TV.iPaySiteID = @Site_Code      
              AND TV.strvoucherStatus = 'PD'      
              AND TV.dtPaid BETWEEN @startDate AND @endDate      
              AND TV.ipaydeviceid IS NOT NULL       
 			  AND ((ISNULL(@route_no,0)=0) 
 			 OR (
 			        (
 			            pd.dType = 1
 			            AND pd.strSerial IN (SELECT Stock_No
 			                                 FROM   @RouteMembers)
 			        )
 			        OR (
 			               pd.dType = 0
 			               AND id.strSerial IN (SELECT Stock_No
 			                                    FROM   @RouteMembers)
 			           )
 			    ))  
 			
         --Active Cashable Vouchers      
	  IF ISNUll(@isActive,0)=1 
       INSERT INTO @temp                  
       SELECT CASE       
                   WHEN ISNULL(Ticket_Type, 0) = 0 THEN 13 --'Cashable Active Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 14 --'Non Cashable Active Voucher'      
              END , NULL,        
              --ID.strSerial COLLATE DATABASE_DEFAULT  as PrintAsset,            
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             ID.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              NULL AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
              id.strSerial  ,    
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
      WHERE  COALESCE(strVoucherStatus, '') = ''
              AND dtExpire > @enddate      
              AND dtPrinted BETWEEN @startdate AND @enddate       
              AND ( id.dType=1  OR @CDMIgnoreDeviceForGeneral ='True' )          
 			AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers))
             
       --VOID Vouchers   (@isVoidVoucher)    -- CASH DESK VOID     
       IF ISNULl(@isVoidVoucher,0)=1 
       INSERT INTO @temp      
       SELECT CASE       
                   WHEN ISNULL(Ticket_Type, 0) = 0 THEN 38 --'Cashable VOID Voucher(CD)'      
                   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 39 --'Non Cashable VOID Voucher(CD)'      
              END ,NULL,         
              --ID.strSerial  COLLATE DATABASE_DEFAULT as PrintAsset,          
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             ID.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              TV.dtVoid AS PaidDate,      
              -(CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00) AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
             iVoucherVoidUser AS iVoucherVoidUser,
              id.strSerial ,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
       WHERE  COALESCE(strVoucherStatus, '') = 'VD'      
              AND dtVoid BETWEEN @startdate AND @enddate      
              AND TV.iSITEID = @Site_Code       
 			AND id.dType= 0      
 			AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers))
 			
       --Cancelled Vouchers    
        IF ISNULl(@isVoidCancel,0)=1      
       INSERT INTO @temp       
       SELECT CASE       
                   WHEN ISNULL(Ticket_Type, 0) = 0 THEN 15 --'Cashable Cancelled Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 16 --'Non Cashable Cancelled Voucher'      
              END ,   NULL,      
              --ID.strSerial  COLLATE DATABASE_DEFAULT as PrintAsset,            
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              NULL AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
              id.strSerial,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
       WHERE  strVoucherStatus = 'NA'      
              AND dtPrinted BETWEEN @startdate AND @enddate    
             AND id.dType=1   
 			AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers))   
 			
       --START VOIDTICKETSAMOUNT    (@isVoidCancel)        
       IF ISNULL(@isVoidCancel,0)=1
       INSERT INTO @temp      
       SELECT CASE       
                   WHEN ISNULL(Ticket_Type, 0) = 0 THEN 40 --'Cashable Void Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 41 --'Non Cashable Void Voucher'      
              END , NULL,        
              --ID.strSerial  COLLATE DATABASE_DEFAULT as PrintAsset,              
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              NULL AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
              id.strSerial,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
       WHERE  strVoucherStatus = 'VD'      
              AND dtPrinted BETWEEN @startdate AND @enddate       
 			AND id.dtype=1     
      		AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers)) 
 			     
       --END VOIDTICKETSAMOUNT            
             
       --Expired Vouchers          
       IF ISNULL(@isExpired,0)=1 
       INSERT INTO @temp      
       SELECT CASE       
        WHEN ISNULL(Ticket_Type, 0) = 0 THEN 17 --'Cashable Expired Voucher'      
                   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 18 --'Non Cashable Expired Voucher'      
              END ,  NULL,       
              --ID.strSerial  COLLATE DATABASE_DEFAULT as PrintAsset,           
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              TV.dtExpire AS PaidDate,     --Display Expired Date incase of an Expired Ticket     
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode AS Ticket,      
              NULL,      
              id.strSerial,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
       WHERE  strVoucherStatus IS NULL      
              AND dtExpire <= GETDATE()      
              AND dtExpire BETWEEN @startdate AND @enddate       
                   AND ( id.dType=1  OR @CDMIgnoreDeviceForGeneral ='True' )               
             AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers)) 
             
       --Voucher Exceptions      
       --Ticket In Exceptions        
       IF ISNULL(@isException,0)=1
       INSERT INTO @temp                  
       SELECT CASE       
 				WHEN ISNULL(Ticket_Type, 0) = 0 THEN 19 --'Cashable Voucher IN Exception'      
                 WHEN ISNULL(Ticket_Type, 0) = 1 THEN 20 --'Non Cashable Voucher IN Exception'      
              END AS Trans_Type,  NULL,       
              --ID.strSerial COLLATE DATABASE_DEFAULT  as PrintAsset,           
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              NULL AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
             id.strSerial,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                  AND TV.iSiteID = @Site_Code
       WHERE  dtPrinted BETWEEN @startdate AND @enddate
 			 AND ((ISNULL(@route_no,0)=0) 
 			 OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers))       
              AND (      
                      (strVoucherStatus = 'PP')      
                      OR (strVoucherStatus IS NULL AND errcode <> 0)      
                  )      
             AND ((TV.iPaySiteID IS NULL OR TV.iPaySiteID = @Site_Code))       
                          
             
       --Ticket Out Exception    
       IF ISNULL(@isException,0)=1   
       INSERT INTO @temp                   
       SELECT CASE       
                   WHEN ISNULL(TE_Ticket_Type, 0) = 0 THEN 21 --'Cashable Voucher OUT Exception'      
                   WHEN ISNULL(TE_Ticket_Type, 0) = 1 THEN 22 --'Non Cashable Voucher OUT Exception'      
              END , NULL,      
              --ID.strSerial  COLLATE DATABASE_DEFAULT as PrintAsset,           
              CASE       
                   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
                        COLLATE DATABASE_DEFAULT      
                   ELSE 'N/A'      
              END AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TE_Date AS PrintedDate,      
              'N/A' AS PaidAsset,      
              'N/A' AS PaidPosition,      
              NULL AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
             id.strSerial,
             NULL
      FROM   ticket_exception TE WITH(NOLOCK)
              INNER JOIN Voucher TV      
                   ON  TE.TE_TicketNumber = TV.StrBarCode COLLATE       
                       DATABASE_DEFAULT      
              INNER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
                   AND TV.iSITEID = @Site_Code      
       WHERE  TE_Date BETWEEN @startdate AND @enddate      
              AND te_status = 'N'      
              AND TE_Installation_No <> 0      
              AND TE_TicketNumber IS NOT NULL        
              AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers)) 
       
       -- Voucher Liability        
       IF ISNULL(@isLiability,0)=1
       BEGIN  
			IF (ISNULL(@CDMShowAllActiveAsLiable, 'False') <> 'True')
			   BEGIN
				   INSERT INTO @temp      
				   SELECT CASE       
							   WHEN ISNULL(Ticket_Type, 0) = 0 THEN 23 --'Cashable Voucher Liability'      
							   WHEN ISNULL(Ticket_Type, 0) = 1 THEN 24 --'Non Cashable Voucher Liability'      
						  END , NULL,     
						  --ID.strSerial COLLATE DATABASE_DEFAULT  as PrintAsset,           
						  CASE       
							   WHEN TV.iSITEID = @Site_Code THEN ID.strSerial       
									COLLATE DATABASE_DEFAULT      
							   ELSE 'N/A'      
						  END AS PrintAsset,      
						 TV.isiteid AS PrintSiteCode,      
						 ID.bar_position_name 
						  COLLATE DATABASE_DEFAULT AS PrintPosition,      
						  TV.dtPrinted AS PrintedDate,      
						  'N/A' AS PaidAsset,      
						  'N/A' AS PaidPosition,      
						  NULL AS PaidDate,      
						  CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
						  TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
						   NULL,      
						  id.strSerial ,
					  NULL     
				   FROM   voucher TV WITH(NOLOCK)      
						  INNER JOIN @DeviceTable ID      
							   ON  TV.ideviceid = ID.ideviceid      
				   WHERE  dtPrinted BETWEEN @startdate AND @enddate      
						  AND COALESCE(strVoucherStatus, '') NOT IN ('VD', 'NA', 'LT')      
						  AND (dtPaid IS NULL OR dtPaid > @enddate)      
						  AND TV.iSiteID = @Site_Code
						  and (id.dtype=1 OR @CDMIgnoreDeviceForGeneral='True')    
						  AND ((ISNULL(@route_no,0)=0) 
 					OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers)) 
			   END
			   ELSE 
			   BEGIN
			   		INSERT INTO @temp
			   		SELECT CASE 
			   		            WHEN ISNULL(Ticket_Type, 0) = 0 THEN 23 --'Cashable Voucher Liability'
			   		            WHEN ISNULL(Ticket_Type, 0) = 1 THEN 24 --'Non Cashable Voucher Liability'
			   		       END,
			   		       NULL,
			   		       --ID.strSerial COLLATE DATABASE_DEFAULT  as PrintAsset,             
			   		       CASE 
			   		            WHEN TV.iSITEID = @Site_Code THEN ID.strSerial 
			   		                 COLLATE DATABASE_DEFAULT
			   		            ELSE 'N/A'
			   		       END AS PrintAsset,
			   		       TV.isiteid AS PrintSiteCode,
			   		       ID.bar_position_name 
			   		       COLLATE DATABASE_DEFAULT AS PrintPosition,
			   		       TV.dtPrinted AS PrintedDate,
			   		       'N/A' AS PaidAsset,
			   		       'N/A' AS PaidPosition,
			   		       NULL AS PaidDate,
			   		       CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,
			   		       TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,
			   		       NULL,
			   		       id.strSerial,
			   		       NULL
			   		FROM   voucher TV WITH(NOLOCK)
			   		       INNER JOIN @DeviceTable ID
			   		            ON  TV.ideviceid = ID.ideviceid
			   		WHERE  strVoucherStatus IS NULL
			   		       AND TV.iSiteID = @Site_Code
			   		       AND (
			   		               (ISNULL(@route_no, 0) = 0)
			   		               OR ID.strSerial IN (SELECT Stock_No
			   		                                   FROM   @RouteMembers)
			   		           ) 
			   END 		
		END 	   
		
      --Promo Cashable Voucher 
      /* 
      IF ISNULL(@isPromo,0)=1 
       INSERT INTO @temp      
       SELECT 25 ,--'Promo Cashable Voucher' AS 
 			 null Trans_Type,      
              ID.strSerial COLLATE DATABASE_DEFAULT AS PrintAsset,      
              TV.isiteid AS PrintSiteCode,      
             id.bar_position_name 
              COLLATE DATABASE_DEFAULT AS PrintPosition,      
              TV.dtPrinted AS PrintedDate,      
              COALESCE(PD.strSerial, '') COLLATE DATABASE_DEFAULT AS PaidAsset,      
              CASE       
                   WHEN strVoucherStatus IS NOT NULL       
                       AND strVoucherStatus = 'PD' THEN pd.bar_position_name 
                        COLLATE DATABASE_DEFAULT      
                   ELSE ''      
              END COLLATE DATABASE_DEFAULT AS PaidPosition,      
              TV.dtPaid AS PaidDate,      
              CAST(TV.iAmount AS DECIMAL(18, 2)) / 100.00 AS Amount,      
              TV.strBarcode COLLATE DATABASE_DEFAULT AS Ticket,      
              NULL,      
             id.strSerial,
             NULL
       FROM   voucher TV WITH(NOLOCK)      
              LEFT OUTER JOIN @DeviceTable PD      
                   ON  TV.ipaydeviceid = PD.ideviceid      
              LEFT OUTER JOIN @DeviceTable ID      
                   ON  TV.ideviceid = ID.ideviceid      
       WHERE  dtPaid BETWEEN @startdate AND @enddate      
              AND strVoucherStatus = 'PD'      
              AND LEFT(strBarCode, 1) IN (@NONCashable, @Cashable)      
              AND TV.iPaySiteID = @Site_Code       
               AND ((ISNULL(@route_no,0)=0) 
 			OR  ID.strSerial IN (SELECT Stock_No FROM @RouteMembers)) 
*/ 			
 			   
   	IF ((ISNULL(@User_No,0) > 0) )--AND ISNULL(@Route_No,0) = 0)
    BEGIN
       DELETE FROM @temp WHERE  ISNULL(Userid,0) <> @User_No
    END

    UPDATE t    
    SET    t.Summary_Type = tp.Summary_Type,
 		  t.trans_type = tp.Trans_Type    
    FROM   @temp t    
           INNER JOIN @typemap tp    
           ON t.iTrans_Type=tp.iTran_Type    
      
   
      
        SELECT PrintAsset,
               PrintSiteCode,
               PrintPosition,
               PrintedDate,
               PaidAsset,
               PaidPosition,
               PaidDate,
               Amount,
               Ticket,
               Userid,
               stock_no_ROUTE,
               Summary_Type,
               Trans_Type
        FROM   @temp
        WHERE iTrans_Type NOT IN (SELECT filterlist FROM   @Filter)
        ORDER BY
               Trans_Type ASC,
               PrintedDate DESC   
                  
        
 END        
 GO
