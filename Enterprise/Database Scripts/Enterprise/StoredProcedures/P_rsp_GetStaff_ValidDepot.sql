USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaff_ValidDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaff_ValidDepot]
GO


USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetStaff_ValidDepot
AS
BEGIN 

select 
SD.Depot_ID as DepotId,
Staff_First_Name+' '+Staff_Last_Name as Representative
from Staff st
INNER JOIN Staff_Depot SD ON St.Staff_ID = SD.Staff_ID 
order by DepotId,Representative
END
GO 

