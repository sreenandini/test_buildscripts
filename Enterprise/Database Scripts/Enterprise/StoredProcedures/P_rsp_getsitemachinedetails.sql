USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getsitemachinedetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getsitemachinedetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------   
--  
--  Get the details for machine control   
  
-- INPUT  @Site_Code - Site for which machine control commands to be sent  
-- ===================================================================================  
--   
-- Revision History  
--   
-- Siva        04/09/08  Created  
--   
--------------------------------------------------------------------------------------   
CREATE PROCEDURE [dbo].[rsp_getsitemachinedetails](  
@Site_Id int  
)  
AS  
  
select bar_position_machine_enabled,
installation_id,
bar_position.bar_position_id,
bar_position_name,
machine_stock_no,
( 
CASE
WHEN  
machine_name = 'MULTI GAME' 
THEN 
ISNULL(MGMP.Multigamename,'MULTI GAME')                
 ELSE   
machine_name 
 END
 )   AS
machine_name
, 
current_change = case when bar_position_machine_enabled IN (100,101)  then  'PENDING'   
        else ''  end,  
current_status = case when bar_position_machine_enabled = 100  then 'DISABLE'   
      when bar_position_machine_enabled = 101  then 'ENABLE'   
      when bar_position_machine_enabled = 1  then 'ENABLED'   
      when bar_position_machine_enabled = 0  then 'DISABLED'   
     else 'ENABLED'  end  
from Installation   
join Bar_position ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID   
join Machine ON Installation.Machine_ID = Machine.Machine_ID   
join Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID   
LEFT JOIN MultiGameMapping MGMP ON MGMP.MachineID=Machine.Machine_ID
where Site_Id=@Site_Id and installation_end_date is null 
ORDER BY Bar_position.Bar_Position_Name
--and coalesce(datapak_id,0) <> 0  


GO

