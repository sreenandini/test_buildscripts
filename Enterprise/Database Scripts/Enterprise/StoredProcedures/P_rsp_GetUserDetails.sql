USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUserDetails]
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
   Kalaiyarasan.P             7-Jun-2012         Created               This SP is used to get User details   
                                                                         
*/  
CREATE PROCEDURE rsp_GetUserDetails @SecurityUserID as int  
       
AS  
BEGIN
	

SELECT SecurityUserID  
      ,WindowsUserName  
      ,UserName  
      ,Password  
      ,LanguageID  
      ,CurrencyCulture  
      ,DateCulture  
      ,ChangePassword  
      ,CreatedDate  
      ,PasswordChangeDate  
      ,isReset  
      ,isLocked  
  FROM [USER] WHERE SecurityUserID =@SecurityUserID  

  
--Exec  rsp_GetUserDetails 1    
  END

GO

