USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_DailyCashDeskCollectionConsolidated]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_DailyCashDeskCollectionConsolidated]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_REPORT_DailyCashDeskCollectionConsolidated  
	@dtStartDate as datetime,  -- passed in as gaming date  
	@dtEndDate   as datetime,   -- passed in as gaming date  
	@SITE as int ,
	@RouteNo INT = 0,
	@UserNo INT = 0
AS  
  
set dateformat dmy  
set nocount on  
  
IF 1=0 BEGIN    
    SET FMTONLY OFF    
END    
    
    DECLARE @RouteName VARCHAR(50)
	IF (@RouteNo = 0)
	  SET @RouteName='ALL'
	ELSE
	  SELECT  @RouteName= Route_name FROM [ROUTE] WHERE route_id = @RouteNo
	  
	DECLARE @UserName VARCHAR(100)
	IF @UserNo = 0 
		SET @UserName = 'ALL'
	ELSE
		SELECT @UserName = S.Staff_Last_Name + ' ' + S.Staff_First_Name 
		FROM [Staff] S
		WHERE S.UserTableID = @UserNo
  
select site_name = site.Site_Name,  
       z.zone_name,  
       posname = bp.bar_position_name,  
       cat.machine_type_code,  
       machinename = mc.Machine_Name,  
       m.Machine_Stock_No,  
         
       cash_collected_5000p = COALESCE ( SUM(vwc.cash_collected_5000p) , 0 ),   
       cash_collected_2000p = COALESCE ( SUM(vwc.cash_collected_2000p), 0 ),   
       cash_collected_1000p = COALESCE ( SUM(vwc.cash_collected_1000p), 0 ),   
       cash_collected_500p = COALESCE ( SUM(vwc.cash_collected_500p), 0 ),   
       net_coin = COALESCE ( SUM(vwc.net_coin), 0 ),  
  
       -- find 1st and last collection date ( gaming day ) for dates entered..  
       claimed_in_cashdesk = COALESCE ( ( select sum( cast(iamount as float)/100)    
                                            from voucher v  
  
                                            join device paydevice  
                                              on v.iPaydeviceid = paydevice.ideviceid  
  
                                            join device printdevice  
                                              on v.ideviceid = printdevice.ideviceid  
  
                                            join siteworkstations tiw  
                                              on tiw.Site_Workstation = paydevice.strserial  
   
                                           WHERE printdevice.strserial = m.Machine_Stock_No 
													AND ((ISNULL(@UserNo, 0) = 0) OR (VoucherRedeemedUser = @UserNo)) 
                                                   and v.dtpaid between dateadd( hh, 24+7, ( SELECT MIN( cic.Previous_Collection_Date)   
                                                                           from collection cic  
                                                                           join installation cici  
      on cic.installation_ID = cici.installation_ID  
                                                                          where cici.machine_ID = i.machine_ID  
                                                                            and cic.Collection_Date_Of_Collection BETWEEN @dtStartDate AND @dtEndDate) )  
                     
         
                                                                   and dateadd( hh, 7, ( SELECT MAX( cic.Collection_Date_Of_Collection)    
                                                                           from collection cic  
                                                    join installation cici  
                                                                             on cic.installation_ID = cici.installation_ID  
                                                                          where cici.machine_ID = i.machine_ID  
                                                                            and cic.Collection_Date_Of_Collection BETWEEN @dtStartDate AND @dtEndDate) )  
                                        ), 0 ),  
  
       -- find 1st and last collection date ( gaming day ) for dates entered..  
       claimed_in_cashdesk_qty = COALESCE ( ( select count(*)   
                                                from voucher v  
  
                                                join device paydevice  
                                                  on v.iPaydeviceid = paydevice.ideviceid  
     
                                                 join device printdevice  
                                                   on v.ideviceid = printdevice.ideviceid  
   
                                                 join siteworkstations tiw  
                                                   on tiw.Site_Workstation  = paydevice.strserial  
     
                                                WHERE printdevice.strserial = m.Machine_Stock_No
												  AND ((ISNULL(@UserNo, 0) = 0) OR (VoucherRedeemedUser = @UserNo))
                                                  and v.dtpaid between dateadd( hh, 24+7, ( SELECT MIN(cic.Previous_Collection_Date)   
                                                         from collection cic  
                                                                           join installation cici  
                                                                             on cic.installation_ID = cici.installation_ID  
                                                                          where cici.machine_ID = i.machine_ID  
                                                                            and cic.Collection_Date BETWEEN @dtStartDate AND @dtEndDate))  
  
                                                                   and dateadd( hh, 7, ( SELECT MAX(cic.Collection_Date_Of_Collection)    
                                                                           from collection cic  
                                                                           join installation cici  
                                                                             on cic.installation_ID = cici.installation_ID  
                                                                          where cici.machine_ID = i.machine_ID  
                                                                            and cic.Collection_Date BETWEEN @dtStartDate AND @dtEndDate) )  
                                                ), 0 ),  
  
       Shortpays = COALESCE ( SUM(vwc.shortpay) , 0 ),   
       Handpays = COALESCE ( SUM(vwc.decHandpay) , 0 ),   
       Refills = COALESCE ( SUM(vwc.Refills) , 0 ),   
       Refunds = COALESCE ( SUM(vwc.Refunds) , 0 ),   
       Void = COALESCE ( SUM(vwc.Void) , 0 ),   
       Expired = COALESCE ( SUM(vwc.Expired) , 0 ),   
  
       --printed_by_machine = COALESCE ( SUM(vwc.TicketsPrinted) , 0 ),  
       printed_by_machine = COALESCE ( SUM(vwc.Tickets_Printed) , 0 ),  
   
       claimed_by_machine = COALESCE ( SUM(vwc.Declared_Tickets) , 0 ),  
   
       wl_cashtake = COALESCE ( SUM(vwc.cashtake) , 0 ),  
       wl_takevar = COALESCE ( SUM(vwc.rdc_take) , 0 ),  
  
       RDCCashIn = Sum(RDC_Notes_In) + sum(RDC_Coins_In),   
       RDCCashOut = Sum(RDC_Coins_In) +SUM (RDC_Coins_out ) ,  
       Progressive_Value_Declared=COALESCE ( SUM(vwc.Progressive_value_declared) , 0 )  
  
  
  INTO #tmpTable  
  
  from installation i  
  
  join machine m  
    on m.machine_ID = i.machine_ID  
  
  join bar_position bp  
    on bp.bar_position_ID = i.bar_position_ID AND bp.Site_ID=@SITE
  Left JOIN Route_Member RM 
  ON RM.Bar_Position_ID = bp.Bar_Position_ID 
  join site  
    on bp.site_ID = site.site_ID  
  
  join machine_type cat  
    on cat.machine_type_id = m.machine_category_ID  
  
  join machine_class mc  
    on mc.Machine_Class_ID = m.Machine_Class_ID  
  
left join zone z  
       on z.zone_ID = bp.zone_ID
  
  join vw_collectiondata vwc  
    on ( vwc.installation_ID = i.installation_ID   
         and vwc.collection_date between dateadd( dd, -1, @dtStartDate ) and dateadd( dd, -1, @dtEndDate ) 
    AND (@RouteNo = 0 OR Rm.Route_ID = @RouteNo )  )
    INNER JOIN [User] U ON U.UserName = vwc.UserName AND ((ISNULL(@UserNo, 0) = 0) OR (SecurityUserID = @UserNo)) 
  
GROUP BY    
       site.Site_Name,  
       z.zone_name,  
       bp.bar_position_name,  
       cat.machine_type_code,  
       mc.Machine_Name,  
       m.Machine_Stock_No,  
       i.machine_ID,  
       i.installation_ID,  
       i.Installation_End_Date  
  
  -- calculate and export  
  --  
  SELECT --max(st), max(en),  
         site_name,  
         zone_name,  
         posname,  
         machine_type_code,  
         machinename,  
         Machine_Stock_No as stock_no,  
         cash_collected_5000p = SUM(cash_collected_5000p),   
         cash_collected_2000p = SUM(cash_collected_2000p),   
         cash_collected_1000p = SUM(cash_collected_1000p),   
         cash_collected_500p = SUM(cash_collected_500p),   
         net_coin = SUM(net_coin)- SUM(Shortpays),  
         
--  printed = SUM(Printed_by_Machine),  
--         claimed = SUM(Claimed_by_Machine),  
   
        claimed_in_cashdesk = MAX(claimed_in_cashdesk),   
        claimed_in_cashdesk_qty = MAX(claimed_in_cashdesk_qty),  
  
         Shortpays = SUM(Shortpays),  
         Handpays = SUM(Handpays),   
         Refills = SUM(Refills),   
         Refunds = SUM(Refunds),  
         Void = SUM(Void),  
         Expired = SUM(Expired),  
         Liability = SUM(Printed_by_Machine) - MAX(claimed_in_cashdesk) - SUM(Claimed_by_Machine)  - SUM(Shortpays),  
         wl_cashtake = SUM(wl_cashtake),  
         wl_takevar = SUM(wl_takevar),  
  
         RDCCashIn = SUM ( RDCCashIn ),  
         RDCCashOut = SUM ( RDCCashOut ),  
         RDCCashTake = SUM ( RDCCashIn ) - SUM ( RDCCashOut ),  
   Progressive_Value_Declared=SUM(Progressive_Value_Declared),
   @UserName AS UserName,
   @RouteName AS RouteName  
  
    FROM #tmpTable  
  
GROUP BY site_name,  
         zone_name,  
         posname,  
         machine_type_code,  
         machinename,  
         Machine_Stock_No  
  
order by posname  


GO

