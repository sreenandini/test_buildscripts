USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_PerformLiquidationForProfitShare]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_PerformLiquidationForProfitShare]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--- For Profit Share        
        
  
  
--- For Profit Share          
          
CREATE PROCEDURE [dbo].[usp_PerformLiquidationForProfitShare]
	@Site_Id INT = 0,
	@Batch_No INT = 0, 
	 
	 --Added Parameters for Profit Share -Starts
	@ProfitShareGroupId INT,
	@ExpenseShareGroupId INT,
	@ExpenseShareAmount DECIMAL(18, 2),
	@WriteOffAmount DECIMAL(18, 2),
	@PayPeriodId INT,
	@MeterIn DECIMAL(18, 2),
	@MeterOut DECIMAL(18, 2),
	@RetailerShareBeforeAdjustment DECIMAL(18, 2),
	@RetailerNegativeNet DECIMAL(18, 2),
	@RetailerSharePercentage FLOAT,
	@TicketPaid DECIMAL(18, 2),
	@AdvanceToRetailer DECIMAL(18, 2),
	@Retailer_Share DECIMAL(18, 2),
	@BalanceDue DECIMAL(18, 2),
	@Retailer DECIMAL(18, 2),
	@RetailerShareBeforeFixedExpense DECIMAL(18, 2),
	@CarriedForwardExpense DECIMAL(18, 2),
	@RetailerExpenseShareAmount DECIMAL(18, 2),
	@FixedExpenseAmount DECIMAL(18,2),
	@PrevCarriedForwardExpense DECIMAL(18,2)
	 --Ends
AS
BEGIN
	SET DATEFORMAT dmy               
	SET NOCOUNT ON              
	
	DECLARE @LiquidationId INT
	DECLARE @Site_Code VARCHAR (50)
	DECLARE @Site_Name VARCHAR
	

	IF @Batch_No = 0
	    RETURN (0) -- shouldn't happen  
	    
	SELECT 
		@Site_Code = Site_Code,
		@Site_Name = Site_Name
	FROM dbo.[Site] WHERE Site_ID = @Site_id AND SiteStatus = 'FULLYCONFIGURED'
	
	
	INSERT INTO LiquidationDetails
	  (
	    SiteId,
	    CollectionBatchId,
	    ReadId,
	    CollectionPerformedDate,
	    SiteName,
	    ProfitShareGroupId,
	    ExpenseShareGroupId,
	    ExpenseShareAmount,
	    WriteOffAmount,
	    PayPeriodId,
	    MeterIn,
	    MeterOut,
	    RetailerShareBeforeAdjustment,
	    RetailerNegativeNet,
	    RetailerSharePercentage,
	    TicketPaid,
	    AdvanceToRetailer,
	    Negative_Net,
	    Retailer,
	    BalanceDue,	
	    RetailerShareBeforeFixedExpense,
	    FixedExpenseAmount,
	    CarriedForwardExpense,
	    RetailerExpenseShareAmount,
	    PrevCarriedForwardExpense
	  )
	SELECT @Site_Id,
	       @Batch_No,
	       NULL,
	       B.batch_Date_Performed AS Date, --Batch Date
	       @Site_Name AS Retailer_Name,	--Retailer Name 
	       @ProfitShareGroupId,
	       @ExpenseShareGroupId,
	       @ExpenseShareAmount,
	       @WriteOffAmount,
	       @PayPeriodId,
	       @MeterIn,
		   @MeterOut,
		   @RetailerShareBeforeAdjustment,
		   @RetailerNegativeNet,
		   @RetailerSharePercentage,
		   @TicketPaid,
		   @AdvanceToRetailer,
		   @Retailer_Share, --Negative_Net
		   @Retailer,
		   @BalanceDue,
		   @RetailerShareBeforeFixedExpense,
		   @FixedExpenseAmount,
		   @CarriedForwardExpense,
		   @RetailerExpenseShareAmount,
		   @PrevCarriedForwardExpense
	FROM   Batch B
	WHERE  B.Batch_Id = @Batch_No
	
	SET @LiquidationId = CAST(SCOPE_IDENTITY() AS INT)   
	
	IF @LiquidationId IS NOT NULL
	BEGIN
	    INSERT INTO Export_History
	      (
	        EH_Date,
	        EH_Reference1,
	        EH_Type,
	        EH_Site_Code
	      )
	    SELECT GETDATE(),
	           @LiquidationId,
	           'LIQUIDATIONDETAILS',
	           @Site_Code
	END  
	
	
	IF @LiquidationId IS NOT NULL
	BEGIN
	    INSERT INTO LiquidationShareDetails
	      (
	        LiquidationId,
	        ShareHolderId,
	        ShareHolderName,
	        ExpenseShareAmount
	      )
	    SELECT @LiquidationId,
	           SH.ShareHolderId,
	           SH.ShareHolderName,
	           ((ES.ExpenseSharePercentage / 100) * @ExpenseShareAmount)
	    FROM   ShareHolders SH
	           INNER JOIN ExpenseShare ES
	                ON  SH.ShareHolderId = ES.ShareHolderId
	    WHERE  ES.ExpenseShareGroupId = @ExpenseShareGroupId
			AND ES.SysDelete=0
			AND SH.SysDelete = 0
	    
	    IF @@ROWCOUNT > 0
	    BEGIN
	         
			SELECT @Site_Code = Site_Code FROM dbo.Site WHERE Site_ID = @Site_id AND SiteStatus = 'FULLYCONFIGURED'
	        
            INSERT INTO Export_History
              (
                EH_Date,
                EH_Reference1,
                EH_Type,
                EH_Site_Code
              )
            SELECT GETDATE(),
                   LSD.LiquidationShareId,
                   'LIQUIDATIONSHAREDETAILS',
                   @Site_Code
            FROM   LiquidationShareDetails LSD
				WHERE LSD.LiquidationId = @LiquidationId

	    END
	END
END

GO
