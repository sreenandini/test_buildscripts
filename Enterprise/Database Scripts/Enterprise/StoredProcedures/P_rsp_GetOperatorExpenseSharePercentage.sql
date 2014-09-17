USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetOperatorExpenseSharePercentage]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetOperatorExpenseSharePercentage]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetOperatorExpenseSharePercentage    
@ExpenseShareGroupID INT 
AS    
BEGIN    
 SELECT cast(isnull(ExpenseSharePercentage,0) as decimal(18,2)) AS OperatorExpSharePercentage  FROM ExpenseShare WHERE ShareHolderId=2 AND ExpenseShareGroupId=@ExpenseShareGroupID    
END  