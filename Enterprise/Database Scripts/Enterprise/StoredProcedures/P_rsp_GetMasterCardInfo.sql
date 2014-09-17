USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMasterCardInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMasterCardInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON


GO

CREATE PROCEDURE rsp_GetMasterCardInfo(@EmpCardNumber varchar(20), @SiteCode VARCHAR(10))  
AS  

DECLARE @UserID INT

SELECT @UserID =isnull(UserID,0) FROM tblEmployeeCardDetails WHERE EmployeeCardNumber =@EmpCardNumber


 SELECT DISTINCT Employeecardnumber,  
        IsMasterCard  
 FROM   tblEmployeeCardDetails tecd  
        LEFT   JOIN [USER] U  
             ON  tecd.UserID = u.SecurityUserID  
        LEFT JOIN UserSite_lnk usl  
             ON  u.SecurityUserID = usl.SecurityUserID  
        LEFT JOIN [Site] s  
             ON  s.Site_ID = usl.SiteID  
        
 WHERE    ((@EmpCardNumber =0) OR (@EmpCardNumber <> 0 AND  EmployeeCardNumber =@EmpCardNumber ))
   And    
      ((@UserID =0) or (@UserID <> 0 and  ( tecd.UserID = @UserID)))   
      --AND s.Site_Code = @SiteCode
              
            FOR XML PATH('MasterInfo'),   
            ROOT('MasterCard')         
         

GO

