USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetEmployeeCardID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetEmployeeCardID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetEmployeeCardID]
(
 @Site INT  = 0,
 @UserID INT = 0
)
AS
BEGIN

 
IF @Site=0 SET @Site=NULL   
IF @UserID=0 SET @UserID=NULL

SELECT 
DISTINCT (cast(EmployeeCardNumber as Varchar(20))) as EmpCardId
FROM	
	tblEmployeeCardDetails ED
	LEFT JOIN tblEmployeeCardSessions ES ON ED.EMPLOYEECARDNUMBER = ES.EMPCARDID		
	LEFT JOIN [SITE] S ON S.SITE_CODE= ES.SITECODE	
WHERE  
	(( @UserID IS NULL)
		 OR
		   ( @UserID IS NOT NULL
			 AND	
			 ES.UserID = @UserID
		   )
		 OR
		   (
			 @UserID IS NOT NULL
			 AND
			 ED.UserID = @UserID
		   )
	  )
	  AND 
      ( @Site IS NULL OR S.SITE_ID = @Site       	
      )	   		 	
      	  
END

--Exec rsp_GetEmployeeCardIDTEMP 0,0
--Exec rsp_GetEmployeeCardIDTEMP 0,1
--Exec rsp_GetEmployeeCardIDTEMP 1,0
--Exec rsp_GetEmployeeCardIDTEMP 2,0

GO

