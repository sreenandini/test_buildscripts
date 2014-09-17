USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUserGroupDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUserGroupDetails]
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
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get UserGroup details     
                                                                             
*/    
    
CREATE PROCEDURE rsp_GetUserGroupDetails
AS
BEGIN
	SELECT User_Group_ID,
	       User_Group_Name,
	       HQ_User_Access_ID
	FROM   User_Group ORDER BY User_Group_Name
END

GO

