USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetWeeklyLiquidationBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetWeeklyLiquidationBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetWeeklyLiquidationBatch  
(@Company as int,  
@SubCompany as Int)        
AS        
BEGIN        
SELECT PE.Statement_No  , MAX(Period_End_ID) AS [Period_End_ID] , (CAST(PE.Statement_No AS VARCHAR(10))+ ', ' + CONVERT(VARCHAR(12), MAX(Period_End_Final_Date), 106)) AS [Period_End_Description]           
FROM Period_End PE            
Inner Join Sub_Company Sc On Sc.Sub_Company_Id = PE.Sub_Company_ID            
Inner join Company C on C.Company_ID = Sc.Company_ID            
where PE.Statement_No not in (0,-1) and SC.Sub_Company_ID  = CASE WHEN (0 = @SubCompany) THEN SC.Sub_Company_ID ELSE @SubCompany END       
GROUP BY PE.Statement_No    
ORDER BY PE.Statement_No DESC
END

GO

