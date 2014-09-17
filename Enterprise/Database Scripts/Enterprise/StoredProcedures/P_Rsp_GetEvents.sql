USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rsp_GetEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Rsp_GetEvents]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Rsp_GetEvents] 
	@startDate datetime,
	@endDate datetime,
	@siteID	int,
	@priority	int,
	@eventType	int,
	@showautoclosed int
AS

SET DATEFORMAT DMY
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	--select * from datapak_fault
	SET NOCOUNT ON;

	If @siteID=0 Set @siteID = Null
	If @priority=0 Set @priority = Null
	If @eventType=5 Set @eventType = Null

--		Event Type codes -	1 = Door; 2 = Commus;	3 = Power; 4 = General Fault;

	select site.site_ID,site.site_name Site_name,bar_position.bar_position_name Position,machine_class.Machine_Name Game_Title,
	Event.Evt_DateTime Date_and_time_of_event,coalesce(Call_Fault_Description,'Undefined') as Details_of_the_event,
	COALESCE(call_group.Call_Group_Reference,'Undefined') AS Description_of_event,
	datapak_fault.Datapak_Fault_Auto_Log_Service_Call_Critical as Priority,
	datapak_fault.type Type_of_event,
	case Event.Evt_Auto_Closed when 1 then 'Yes' else 'No' end as 'Event_Auto_Closed'
	 from installation 
	inner join Machine on installation.machine_id=machine.machine_id
	inner join machine_class on machine_class.machine_class_id=machine.machine_class_id
	inner join bar_position on installation.Bar_Position_ID=bar_position.Bar_Position_ID  
	inner join site on site.site_Id=bar_position.site_Id  
	inner join event on installation.installation_Id=event.evt_installation_Id
	inner join datapak_fault ON datapak_fault.datapak_fault_code=event.evt_fault_source and  datapak_fault.datapak_fault_supplementary_code=event.evt_fault_type
    left  join Call_Fault ON  Datapak_Fault.Call_Fault_ID = Call_Fault.Call_Fault_ID
    LEFT JOIN 	Call_Group ON  Call_Group.Call_Group_ID = Call_Fault.Call_Group_ID    

	Where 

		Event.Evt_DateTime >= @startDate
	AND	Event.Evt_DateTime <= @EndDate

	AND (@showautoclosed = 1 OR (@showautoclosed = 0 AND Evt_Auto_Closed = @showautoclosed))

     AND ( ( @siteID IS NULL )   
         OR
           ( @siteID IS NOT NULL 
             AND
              site.site_ID= @siteID 
           )
         )

     AND ((@priority IS NULL)  
		OR
           ( @priority=1 
             AND
             datapak_fault.Datapak_Fault_Auto_Log_Service_Call_Critical=1
           )
		OR
			( @priority=2 
             AND
             datapak_fault.Datapak_Fault_Auto_Log_Service_Call_Critical!=1
           )
         )
--	AND ( ( @eventType IS NULL )   
--         OR
--           ( @eventType IS NOT NULL 
--             AND
--              datapak_fault.type= @eventType 
--           )
--         )
	AND (  ( @eventType IS NULL )
			OR
				( @eventType = 4 
					AND datapak_fault.type IS NULL
				)
			OR
				(
					datapak_fault.type= @eventType
				)
			 )

Order By Event.Evt_DateTime desc
        
END


GO

