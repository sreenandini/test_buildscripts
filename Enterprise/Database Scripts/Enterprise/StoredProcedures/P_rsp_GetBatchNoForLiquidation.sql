USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetBatchNoForLiquidation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetBatchNoForLiquidation]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetBatchNoForLiquidation
	@SiteCode INT
AS
BEGIN
	DECLARE @LiquidationID              INT
	DECLARE @Liquidation_Start_BatchId  INT
	
	SELECT TOP 1 @LiquidationID = ISNULL(LD.CollectionBatchId, 0)
	FROM   LiquidationDetails LD
	       INNER JOIN [Site] S
	            ON  S.Site_ID = LD.SiteId
	WHERE  LD.CollectionBatchId IS NOT NULL
	       AND S.Site_Code = @SiteCode
	ORDER BY
	       LD.LiquidationId DESC
	
	SELECT TOP 1 @Liquidation_Start_BatchId = Batch_ID
	FROM   Batch B
	       INNER JOIN [Site] S ON S.Site_Code = SUBSTRING(B.Batch_ref, 1, CHARINDEX(',', B.Batch_ref) -1)
	WHERE  B.Liquidation_StartBatch = 1
		AND S.Site_Code = @SiteCode
	ORDER BY Batch_ID DESC
	
	SELECT DISTINCT 
	       B.Batch_ID,
	       CONVERT(DATETIME, B.Batch_Date),
	       CONVERT(
	           VARCHAR(10),
	           RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5)))
	       ) + '-' + B.batch_name AS Collection_Batch_Name,
	       SUBSTRING(B.Batch_ref, 1, 4) AS SiteCode
	FROM   Batch B
	       INNER JOIN COLLECTION C
	            ON  C.Batch_ID = B.Batch_ID
	WHERE  @SiteCode = SUBSTRING(B.Batch_ref, 1, 4)
	       AND ISNULL(B.Batch_Declared, 0) = 1
	       AND B.Batch_ID > ISNULL(@LiquidationID, 0)
	       AND B.Batch_ID > ISNULL(@Liquidation_Start_BatchId, 0)
	ORDER BY
	       Batch_ID ASC
END
GO

