USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetStaffCustomerAccess]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetStaffCustomerAccess]
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
  Kalaiyarasan.P              25-May-2012         Created               This SP is used to get StaffCustomerAccess details     
                                                                             
*/    
    
CREATE PROCEDURE rsp_GetStaffCustomerAccess  @Staff_ID as int    
         
AS    
SELECT Staff_Customer_Access_ID  
      ,Staff_ID  
      ,Customer_Access_ID  
  FROM Staff_Customer_Access WHERE Staff_ID = @Staff_ID


GO

