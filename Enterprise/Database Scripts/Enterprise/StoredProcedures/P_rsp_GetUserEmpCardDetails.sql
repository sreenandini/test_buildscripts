USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetUserEmpCardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetUserEmpCardDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 CREATE PROCEDURE rsp_GetUserEmpCardDetails        
 (@UserID INT)      
 AS      
 BEGIN      
 SET NOCOUNT ON     
   
 SELECT isnull(tecd.EmployeeCardNumber,'') AS EmpCardNumber,  
        ISNULL(IsSingleCardEmployee, 1) AS IsSingleCardEmployee,  
        tecd.UserID,  
        CASE   
             WHEN userid IS NULL THEN 'NotAssigned'  
             ELSE 'Assigned'  
        END AS Mapped,  
        CASE   
             WHEN userid IS NULL THEN 0  
             ELSE 1  
        END AS IsAssigned  
 FROM   [user]  
        LEFT JOIN tblEmployeeCardDetails tecd  
             ON  tecd.employeecardnumber  IN (SELECT DATA  
                                              FROM   dbo.fnSplit ([user].EmpCardNumber, ','))  
 WHERE  SecurityUserID = @UserID  
END      

GO

