USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffDepot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffDepot]
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
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get Staff Depot details     
                                                                        based on Staff_ID.    
*/    
CREATE PROCEDURE rsp_GetStaffDepot
	@Staff_ID INT
AS
BEGIN

	SELECT Depot_ID FROM Staff_Depot WHERE Staff_ID = @Staff_ID 
	       --Exec  rsp_GetStaffDepot 2
END

GO

