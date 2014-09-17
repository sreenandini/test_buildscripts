USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffByDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffByDepot]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get Staff details   
                                                                        based on Depot_ID. 
--Exec  rsp_GetStaffByDepot 5 
*/  
CREATE PROCEDURE rsp_GetStaffByDepot  @Depot_ID int 
       
AS 
BEGIN
   SELECT St.Staff_ID,
		  St.Staff_First_Name,
		  St.Staff_Last_Name 
     FROM Staff St LEFT JOIN Staff_Depot SD
       ON St.Staff_ID = SD.Staff_ID 
	WHERE SD.Depot_ID= @Depot_ID
	ORDER BY  St.Staff_First_Name
END


GO

