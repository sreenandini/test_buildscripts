USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEmployeeName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEmployeeName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeName]
(
 @CardNumber INT  = 0
)
AS
BEGIN
IF @CardNumber = 0 SET @CardNumber=NULL
SELECT 
DISTINCT St.Staff_First_Name + ' ' + St.Staff_Last_Name As EmployeeName
FROM
Staff St
INNER JOIN [User] U on St.UserTableID = U.SecurityUserID
INNER JOIN tblEmployeeCardDetails ED ON ED.UserID = U.SecurityUserID 
WHERE  
      ( ( @CardNumber IS NULL )       
         OR    
           ( @CardNumber IS NOT NULL     
             AND    
             ED.EmployeeCardNumber = @CardNumber    
           )    
      )

END

GO

