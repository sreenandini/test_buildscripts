USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffName]
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
                                                                        based on Staff_ID.    
 --Exec  rsp_GetStaffName null
*/    
CREATE PROCEDURE rsp_GetStaffName
	@Staff_ID INT = NULL,
	@Staff_IsARepresentative BIT = NULL,
	@Staff_Terminated BIT = NULL
AS
BEGIN
	SELECT Staff_ID,
	       Staff_First_Name,
	       Staff_Last_Name,
	       UserTableID
	FROM   Staff
	WHERE  Staff_ID = COALESCE(@Staff_ID, Staff_ID)
	       AND Staff_IsARepresentative = COALESCE(@Staff_IsARepresentative, Staff_IsARepresentative)
	       AND Staff_Terminated = COALESCE(@Staff_Terminated, Staff_Terminated) 
	       ORDER BY Staff_Last_Name,Staff_First_Name
END

GO

