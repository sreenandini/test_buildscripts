USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetFixedExpenseAmount]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetFixedExpenseAmount]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_GetFixedExpenseAmount  
@ExpenseShareGroupID INT,  
@ExpenseShareAmount DECIMAL,  
@FixedExpenseAmount DECIMAL (18,2) OUTPUT  
AS  
BEGIN  
   
 DECLARE @PreviousFixedExpense DECIMAL  
 DECLARE @PreviousCarriedForwardExpense DECIMAL  
 DECLARE @PreviousWriteOff DECIMAL  
 DECLARE @CarriedForwardExpense  DECIMAL(18,2)  
 DECLARE @Count INT  
 DECLARE @RetailerExpenseSharePercentage AS FLOAT  
 DECLARE @RetailerFixedExpenseAmount AS FLOAT  
   
 --Carried Forward Expense --L  
   
   
 SET @PreviousFixedExpense = 0  
 SET @PreviousCarriedForwardExpense = 0  
 SET @PreviousWriteOff = 0  
 SET @CarriedForwardExpense=0  
 SET @Count=0  
   
 SELECT @Count = COUNT(*)  
 FROM   LiquidationDetails   
   
 IF (@Count > 0)  
 BEGIN  
     SELECT TOP 1 @PreviousFixedExpense = ISNULL(FixedExpenseAmount, 0)  
     FROM   LiquidationDetails  
     ORDER BY  
            LiquidationId DESC  
       
     SELECT TOP 1 @PreviousCarriedForwardExpense = ISNULL(CarriedForwardExpense, 0)  
     FROM   LiquidationDetails  
     ORDER BY  
            LiquidationId DESC  
       
     SELECT TOP 1 @PreviousWriteOff = ISNULL(WriteOffAmount, 0)  
     FROM   LiquidationDetails  
     ORDER BY  
            LiquidationId DESC  
       
     SET @CarriedForwardExpense = CAST(  
             (@PreviousFixedExpense + @PreviousCarriedForwardExpense) -@PreviousWriteOff   
             AS DECIMAL(18, 2)  
         )  
 END  
   
 -- FixedExpense Amount  
   
 SELECT @RetailerExpenseSharePercentage=ExpenseSharePercentage     
 FROM  
  ExpenseShare WHERE ShareHolderId=3 AND ExpenseShareGroupId=@ExpenseShareGroupID  
    
  SET @RetailerFixedExpenseAmount=CAST(((@ExpenseShareAmount * @RetailerExpenseSharePercentage)/100.00) AS DECIMAL(18,2)) --K  
    
   
  SET @FixedExpenseAmount=(@CarriedForwardExpense+@RetailerFixedExpenseAmount)

   
END  