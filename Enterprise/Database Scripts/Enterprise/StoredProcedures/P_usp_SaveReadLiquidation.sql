USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_SaveReadLiquidation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_SaveReadLiquidation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_SaveReadLiquidation      
-- -----------------------------------------------------------------      
--   
-- To save read based liquidation  
--   
-- -----------------------------------------------------------------  
-- Revision History             
--   
-- 25/04/2012 Dinesh Rathinavel Created  
--   
-- =================================================================  
  
  
CREATE PROCEDURE [dbo].[usp_SaveReadLiquidation]  
	@Collection_Batch_No INT,  
	@Read_No INT,
	@Site_Id INT,
	@Liquidation_Date DATETIME,  
	@ProfitShareGroup_Id INT,  
	@ExpenseShareGroupId INT,  
	@ExpenseShareAmount DECIMAL(18, 2) = NULL,  
	@WriteOffAmount DECIMAL(18, 2) = NULL,  
	@PayPeriod_Id INT,  
	@MeterIn DECIMAL(18, 2),  
	@MeterOut DECIMAL(18, 2),  
	@RetailerShareBeforeAdjustment DECIMAL(18, 2),  
	@RetailerSharePercentage FLOAT,
	@RetailerNegativeNet DECIMAL(18, 2),
	@TicketPaid DECIMAL(18, 2),  
	@AdvanceToRetailer DECIMAL(18, 2),  
	@FixedExpenseAmount DECIMAL(18, 2),  
	@CarriedForwardExpense DECIMAL(18, 2),  
	@Retailer_Share DECIMAL(18, 2),
	@RetailerShareBeforeFixedExpense DECIMAL(18, 2),
	@BalanceDue DECIMAL(18, 2),
	@Retailer DECIMAL(18, 2),
	@PrevCarriedForwardExpense DECIMAL(18, 2)
AS    
BEGIN  
 
	SET NOCOUNT ON;
	
	DECLARE @LiquidationId INT
	
	INSERT INTO [dbo].[LiquidationDetails]  
		([CollectionBatchId]  
		,[ReadId]
		,[SiteId]
		,[LiquidationPerformedDate]  
		,[ProfitShareGroupId]  
		,[ExpenseShareGroupId]  
		,[ExpenseShareAmount]  
		,[WriteOffAmount]  
		,[PayPeriodId]  
		,[MeterIn]  
		,[MeterOut]  
		,[RetailerShareBeforeAdjustment]  
		,[RetailerNegativeNet]  
		,[TicketPaid]  
		,[AdvanceToRetailer]  
		,[FixedExpenseAmount]  
		,[CarriedForwardExpense]  
		,[Negative_Net]
		,[RetailerShareBeforeFixedExpense]
		,[BalanceDue]
		,[Retailer]
		,[PrevCarriedForwardExpense])
	VALUES  
		(@Collection_Batch_No,  
		@Read_No,
		@Site_Id,
		@Liquidation_Date,
		@ProfitShareGroup_Id,
		@ExpenseShareGroupId,
		@ExpenseShareAmount,
		@WriteOffAmount,
		@PayPeriod_Id,
		@MeterIn, 
		@MeterOut,
		@RetailerShareBeforeAdjustment,
		@RetailerNegativeNet,  
		@TicketPaid,  
		@AdvanceToRetailer,  
		@FixedExpenseAmount,
		@CarriedForwardExpense,
		@Retailer_Share, --Negative Net
		@RetailerShareBeforeFixedExpense,
		@BalanceDue,
		@Retailer,
		@PrevCarriedForwardExpense)

	SELECT @LiquidationId = SCOPE_IDENTITY()
	
	IF @LiquidationId IS NOT NULL
	BEGIN

		EXEC usp_Export_History @LiquidationId, 'LIQUIDATIONDETAILS', @Site_ID

		INSERT INTO LiquidationShareDetails
		(
			LiquidationId,
			ShareHolderId,
			ShareHolderName,
			ExpenseShareAmount
		)
		SELECT
			@LiquidationId,
			SH.ShareHolderId,
			SH.ShareHolderName,
			((ES.ExpenseSharePercentage / 100) * @ExpenseShareAmount)
		FROM ShareHolders SH
			INNER JOIN ExpenseShare ES ON SH.ShareHolderId = ES.ShareHolderId
		WHERE ES.ExpenseShareGroupId = @ExpenseShareGroupId
		AND ES.SysDelete=0
		AND SH.SysDelete = 0

		IF @@ROWCOUNT > 0  
		BEGIN
		
			DECLARE @Site_Code Varchar (50)  
			SELECT @Site_Code = Site_Code FROM dbo.Site WHERE Site_ID = @Site_id
			
			INSERT INTO Export_History  
			(  
				 EH_Date,  
				 EH_Reference1,  
				 EH_Type,
				 EH_Site_Code
			)
			(
				SELECT 
					GETDATE(), 
					LSD.LiquidationShareId, 
					'LIQUIDATIONSHAREDETAILS', 
					@Site_Code
				FROM LiquidationShareDetails LSD
				WHERE LSD.LiquidationId = @LiquidationId
			)
		
		END

	END

END  

GO

