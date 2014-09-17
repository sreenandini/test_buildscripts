USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportLiquidationShareDetailsFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportLiquidationShareDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].usp_ImportLiquidationShareDetailsFromXML
	@doc VARCHAR(MAX),
	@IsSuccess INT OUTPUT,
	@HQ_LiquidationShare_Id INT OUTPUT,
	@LiquidationShare_Id INT OUTPUT
AS
BEGIN
	PRINT ('Working')
	DECLARE @idoc INT  
	
	SET @IsSuccess = 0  
	
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc  
	
	DECLARE @LiquidationShareId INT
	DECLARE @Site_Liquidation_ID INT
	DECLARE @Liquidation_ID INT
	
	SELECT @LiquidationShareId = LiquidationShareId,
		   @Site_Liquidation_ID = LiquidationID
	FROM   OPENXML(
	           @idoc,
	           './/LiquidationShareDetails/LiquidationShareDetails',
	           2
	       ) WITH 
	       (LiquidationShareId INT './LiquidationShareId',
	        LiquidationID INT './LiquidationId')
	        
	SELECT @LiquidationShare_Id = @LiquidationShareId
	SELECT @Liquidation_ID = LiquidationId FROM LiquidationDetails WHERE HQ_ID = @Site_Liquidation_ID
	
	IF NOT EXISTS(
	       SELECT LiquidationShareId
	       FROM   LiquidationShareDetails
	       WHERE  HQ_ID = @LiquidationShareId
	   )
	BEGIN
	    INSERT INTO dbo.LiquidationShareDetails
	      (
	        HQ_ID,
	        LiquidationID,
	        ShareHolderName,
	        ProfitShareGroupId,
	        ShareHolderId,
	        ProfitShareAmont,
	        ExpenseShareAmount
	      )
	    SELECT 
			HQ_ID,
			@Liquidation_ID,
			ShareHolderName,
			ProfitShareGroupId,
			ShareHolderId,
			ProfitShareAmont,
			ExpenseShareAmount
	    FROM   OPENXML(
	               @idoc,
	               './/LiquidationShareDetails/LiquidationShareDetails',
	               2
	           ) WITH 
	           (
	               HQ_ID INT './LiquidationShareId',
	               ShareHolderName VARCHAR(50) './ShareHolderName',
	               ProfitShareGroupId INT './ProfitShareGroupId',
	               ShareHolderId INT './ShareHolderId',
	               ProfitShareAmont DECIMAL(18, 2) './ProfitShareAmont',
	               ExpenseShareAmount DECIMAL(18, 2) './ExpenseShareAmount'
	           )  
	    
	    SELECT @HQ_LiquidationShare_Id = SCOPE_IDENTITY()
	    
	    IF @@Error <> 0
	    BEGIN
	        SET @IsSuccess = -1 -- failed while updating the records in the shareholder table
	    END
	END
	ELSE
	BEGIN
	    UPDATE LSD
	    SET    LSD.HQ_ID = A.HQ_ID,
	           LSD.LiquidationID = @Liquidation_ID,
	           LSD.ShareHolderName=A.ShareHolderName,
	           LSD.ProfitShareGroupId = A.ProfitShareGroupId,
	           LSD.ShareHolderId = A.ShareHolderId,
	           LSD.ProfitShareAmont = A.ProfitShareAmont,
	           LSD.ExpenseShareAmount = A.ExpenseShareAmount
	    FROM   LiquidationShareDetails LSD
	           INNER JOIN OPENXML(
	                    @idoc,
	                    './/LiquidationShareDetails/LiquidationShareDetails',
	                    2
	                ) WITH 
	                (
	                    HQ_ID INT './LiquidationShareId',
	                    ShareHolderName VARCHAR(50) './ShareHolderName',
	                    ProfitShareGroupId INT './ProfitShareGroupId',
	                    ShareHolderId INT './ShareHolderId',
	                    ProfitShareAmont DECIMAL(18, 2) './ProfitShareAmont',
	                    ExpenseShareAmount DECIMAL(18, 2) './ExpenseShareAmount'
	                )A
	                ON  LSD.HQ_ID = A.HQ_ID
	END
	
	IF @@Error <> 0
	BEGIN
	    SET @IsSuccess = -1 -- failed while updating the records in the ExpenseShareGroup table
	END
	
	EXEC sp_xml_removedocument @idoc
END 

GO

