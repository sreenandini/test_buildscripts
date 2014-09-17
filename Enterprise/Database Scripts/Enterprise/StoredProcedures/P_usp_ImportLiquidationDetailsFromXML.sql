USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportLiquidationDetailsFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportLiquidationDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].usp_ImportLiquidationDetailsFromXML
	@doc VARCHAR(MAX),
	@IsSuccess INT OUTPUT,
	@HQ_Liquidation_Id INT OUTPUT,
	@Liquidation_Id INT OUTPUT
AS
BEGIN
	PRINT ('Working')
	DECLARE @idoc INT  
	
	SET @IsSuccess = 0  
	
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc  
	
	DECLARE @LiquidationId INT
	DECLARE @SiteBatchId INT
	DECLARE @HQ_Batch_ID INT
	
	DECLARE @Site_Read_ID INT 
	DECLARE @HQ_Read_ID INT
	DECLARE @HQ_installation_ID INT
	DECLARE @Read_Date DATETIME
	DECLARE @Site_Code VARCHAR(10)
	DECLARE @Site_Id INT
	DECLARE @Liquidation_StartBatch BIT
	
	SELECT @LiquidationId = LiquidationId,
		   @SiteBatchId = CollectionBatchId,
		   @Site_Read_ID = ReadId,
		   @HQ_installation_ID = HQ_installation_ID,
		   @Read_Date = Read_Date,
		   @Site_Code = SiteId,
		   @Liquidation_StartBatch = Liquidation_StartBatch
	FROM   OPENXML(@idoc, './/LiquidationDetails/LiquidationDetails', 2) WITH 
	       (LiquidationId INT './LiquidationId',
	       CollectionBatchId INT './CollectionBatchId',
	       ReadId INT './ReadId',
	       HQ_installation_ID INT './HQ_installation_ID',
	       Read_Date DATETIME './Read_Date',
	       SiteId INT './SiteId',
	       Liquidation_StartBatch BIT './Liquidation_StartBatch')
	       
	SET @Liquidation_Id = @LiquidationId
	
	SELECT @Site_Id = Site_ID FROM [Site] S WHERE LTRIM(RTRIM(S.Site_Code)) = LTRIM(RTRIM(@Site_Code))
	       
	IF @SiteBatchId IS NOT NULL
	BEGIN
		SELECT @HQ_Batch_ID = Batch_ID FROM Batch WHERE RIGHT(Batch_Ref, LEN(Batch_Ref) - LEN(LEFT(Batch_Ref, 5))) = @SiteBatchId
	END
	
	IF @Liquidation_StartBatch = 1
	BEGIN
		UPDATE Batch SET Liquidation_StartBatch = @Liquidation_StartBatch WHERE Batch_ID = @HQ_Batch_ID
	END
	
	IF @Site_Read_ID IS NOT NULL
	BEGIN
		SELECT @HQ_Read_ID = Read_ID 
		FROM [Read] R
			INNER JOIN Installation I ON I.Installation_ID = R.Installation_ID
		WHERE I.Installation_ID = @HQ_installation_ID
			AND R.ReadDate = @Read_Date
	END
	
	IF NOT EXISTS(
	       SELECT LiquidationId
	       FROM   LiquidationDetails
	       WHERE  HQ_ID = @LiquidationId
	   )
	BEGIN
	    INSERT INTO dbo.LiquidationDetails
	      (
	        HQ_ID,
	        SiteId,
	        CollectionBatchId,
	        ReadId,
	        SiteName,
	        LiquidationPerformedDate,
	        CollectionPerformedDate,
	        ProfitShareGroupId,
	        ExpenseShareGroupId,
	        ExpenseShareAmount,
	        WriteOffAmount,
	        PayPeriodId,
	        MeterIn,
	        MeterOut,
	        BalanceDue,
	        RetailerShareBeforeAdjustment,
	        RetailerNegativeNet,
	        RetailerSharePercentage,
	        TicketPaid,
	        AdvanceToRetailer,
	        Retailer,
	        FixedExpenseAmount,
	        RetailerShareBeforeFixedExpense,
	        Negative_Net,
	        CarriedForwardExpense,
	        RetailerExpenseShareAmount,
	        PrevCarriedForwardExpense
	      )
	    SELECT 
			HQ_ID,
			@Site_Id,
			@HQ_Batch_ID,
			@HQ_Read_ID,
			SiteName,
			LiquidationPerformedDate,
	        CollectionPerformedDate,
			ProfitShareGroupId,
			ExpenseShareGroupId,
			ExpenseShareAmount,
			WriteOffAmount,
			PayPeriodId,
			MeterIn,
			MeterOut,
			BalanceDue,
			RetailerShareBeforeAdjustment,
			RetailerNegativeNet,
			RetailerSharePercentage,
			TicketPaid,
			AdvanceToRetailer,
			Retailer,
			FixedExpenseAmount,
			RetailerShareBeforeFixedExpense,
			Negative_Net,
			CarriedForwardExpense,
			RetailerExpenseShareAmount,
			PrevCarriedForwardExpense
	    FROM   OPENXML(
	               @idoc,
	               './/LiquidationDetails/LiquidationDetails',
	               2
	    ) WITH 
	           (
	               HQ_ID INT './LiquidationId',
	               SiteName VARCHAR(50) './SiteName',
	               LiquidationPerformedDate DATETIME './LiquidationPerformedDate',
				   CollectionPerformedDate DATETIME './CollectionPerformedDate',
	               ProfitShareGroupId INT './ProfitShareGroupId',
	               ExpenseShareGroupId INT './ExpenseShareGroupId',
	               ExpenseShareAmount DECIMAL(18, 2) './ExpenseShareAmount',
	               WriteOffAmount DECIMAL(18, 2) './WriteOffAmount',
	               PayPeriodId INT './PayPeriodId',
	               MeterIn DECIMAL(18, 2) './MeterIn',
	               MeterOut DECIMAL(18, 2) './MeterOut',
	               BalanceDue DECIMAL(18, 2) './BalanceDue',
	               RetailerShareBeforeAdjustment DECIMAL(18, 2) './RetailerShareBeforeAdjustment',
	               RetailerNegativeNet DECIMAL(18, 2)'./RetailerNegativeNet',
	               RetailerSharePercentage FLOAT './RetailerSharePercentage',
	               TicketPaid DECIMAL(18, 2)'./TicketPaid',
	               AdvanceToRetailer DECIMAL(18, 2)'./AdvanceToRetailer',
	               Retailer VARCHAR(50) './Retailer',
	               FixedExpenseAmount DECIMAL(18, 2) './FixedExpenseAmount',
	               RetailerShareBeforeFixedExpense DECIMAL(18, 2) './RetailerShareBeforeFixedExpense',
	               Negative_Net DECIMAL(18, 2) './Negative_Net',
	               CarriedForwardExpense DECIMAL(18, 2)'./CarriedForwardExpense',
	               RetailerExpenseShareAmount DECIMAL(18, 2) './RetailerExpenseShareAmount',
				   PrevCarriedForwardExpense  DECIMAL(18, 2) './PrevCarriedForwardExpense'
	           )  
	    
	    SELECT @HQ_Liquidation_Id  = SCOPE_IDENTITY()
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the shareholder table
	    END
	END
	ELSE
	BEGIN
	    UPDATE LD
	    SET    LD.HQ_ID = A.HQ_ID,
	           LD.SiteId = @Site_Id,
	           LD.CollectionBatchId = @HQ_Batch_ID,
	           LD.ReadId = @HQ_Read_ID,
	           LD.SiteName = A.SiteName,
	           LD.LiquidationPerformedDate = A.LiquidationPerformedDate,
	           LD.CollectionPerformedDate = A.CollectionPerformedDate,
	           LD.ProfitShareGroupId = A.ProfitShareGroupId,
	           LD.ExpenseShareGroupId = A.ExpenseShareGroupId,
	           LD.ExpenseShareAmount = A.ExpenseShareAmount,
	           LD.WriteOffAmount = A.WriteOffAmount,
	           LD.PayPeriodId = A.PayPeriodId,
	           LD.MeterIn = A.MeterIn,
	           LD.MeterOut = A.MeterOut,
	           LD.BalanceDue = A.BalanceDue,
	           LD.RetailerShareBeforeAdjustment = A.RetailerShareBeforeAdjustment,
	           LD.RetailerNegativeNet = A.RetailerNegativeNet,
	           LD.RetailerSharePercentage = A.RetailerSharePercentage,
	           LD.TicketPaid = A.TicketPaid,
	           LD.AdvanceToRetailer = A.AdvanceToRetailer,
	           LD.Retailer = A.Retailer,
	           LD.FixedExpenseAmount = A.FixedExpenseAmount,
	           LD.RetailerShareBeforeFixedExpense = A.RetailerShareBeforeFixedExpense,
	           LD.Negative_Net = A.Negative_Net,
	           LD.CarriedForwardExpense = A.CarriedForwardExpense,
	           LD.RetailerExpenseShareAmount = A.RetailerExpenseShareAmount,
	           LD.PrevCarriedForwardExpense = A.PrevCarriedForwardExpense
	    FROM   LiquidationDetails LD
	           INNER JOIN OPENXML(
	                    @idoc,
	                    './/LiquidationDetails/LiquidationDetails',
	                    2
	                ) WITH 
	                (
	                    HQ_ID INT './LiquidationId',
	                    SiteId INT './SiteId',
	                    SiteName VARCHAR(50) './SiteName',
	                    LiquidationPerformedDate DATETIME './LiquidationPerformedDate',
	                    CollectionPerformedDate DATETIME './CollectionPerformedDate',
	                    ProfitShareGroupId INT './ProfitShareGroupId',
	                    ExpenseShareGroupId INT './ExpenseShareGroupId',
	                    ExpenseShareAmount DECIMAL(18, 2) './ExpenseShareAmount',
	                    WriteOffAmount DECIMAL(18, 2) './WriteOffAmount',
	                    PayPeriodId INT './PayPeriodId',
	                    MeterIn DECIMAL(18, 2) './MeterIn',
	                    MeterOut DECIMAL(18, 2) './MeterOut',
	                    BalanceDue DECIMAL(18, 2) './BalanceDue',
	                    RetailerShareBeforeAdjustment DECIMAL(18, 2) 
	                    './RetailerShareBeforeAdjustment',
	                    RetailerNegativeNet DECIMAL(18, 2)
	                    './RetailerNegativeNet',
	                    RetailerSharePercentage FLOAT './RetailerSharePercentage',
	                    TicketPaid DECIMAL(18, 2)'./TicketPaid',
	                    AdvanceToRetailer DECIMAL(18, 2)'./AdvanceToRetailer',
	                     Retailer VARCHAR(50) './Retailer',
	                    FixedExpenseAmount DECIMAL(18, 2)'./FixedExpenseAmount',
	                    RetailerShareBeforeFixedExpense DECIMAL(18,2) './RetailerShareBeforeFixedExpense',
	                    Negative_Net DECIMAL(18,2) './Negative_Net',
	                    CarriedForwardExpense DECIMAL(18, 2) './CarriedForwardExpense',
	                    RetailerExpenseShareAmount DECIMAL(18, 2) './RetailerExpenseShareAmount',
	                    PrevCarriedForwardExpense DECIMAL(18, 2) './PrevCarriedForwardExpense'
	                )A
	                ON  LD.HQ_ID = A.HQ_ID
	END
	
	IF @@Error <> 0
	BEGIN
	    SET @IsSuccess = -1 -- failed while updating the records in the ExpenseShareGroup table
	END
	
	EXEC sp_xml_removedocument @idoc
END 

GO

