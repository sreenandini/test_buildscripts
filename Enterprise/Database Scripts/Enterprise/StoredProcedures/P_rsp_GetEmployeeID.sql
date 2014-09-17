USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEmployeeID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEmployeeID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeID]
(
 @Site INT  = 0
)
AS
BEGIN

 
IF @Site=0 SET @Site=NULL   

SELECT  
DISTINCT
U.SECURITYUSERID, 
U.USERNAME
FROM
[USER] U 
LEFT JOIN [UserSite_lnk] UL ON UL.SECURITYUSERID = U.SECURITYUSERID
LEFT JOIN [SITE] S ON S.SITE_ID= UL.SITEID
 
WHERE

( ( @Site IS NULL )         
   OR      
   ( @Site IS NOT NULL       
     AND      
     UL.SITEID = @Site      
   )      
)  
ORDER BY USERNAME

END

--Exec rsp_GetEmployeeID 1

GO

