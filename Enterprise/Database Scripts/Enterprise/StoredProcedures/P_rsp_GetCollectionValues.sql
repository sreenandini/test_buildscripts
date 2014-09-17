USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCollectionValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCollectionValues]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- rsp_GetCollectionValues
-- -----------------------------------------------------------------
-- 
-- To get collection values for Declaration.
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 10/07/2012 Dinesh Rathinavel Created
-- 
-- =================================================================

CREATE PROCEDURE [dbo].[rsp_GetCollectionValues]
	@Collection_ID INT
AS
BEGIN
	
	SET NOCOUNT ON
	

	SELECT
		ISNULL(C.Cash_Collected_100000P/100,0) as Cash_Collected_100000P,
		ISNULL(C.Cash_Collected_50000P/100,0) as Cash_Collected_50000P,
		ISNULL(C.Cash_Collected_20000P/100,0) as Cash_Collected_20000P,
		ISNULL(C.Cash_Collected_10000P/100,0) as Cash_Collected_10000P,
		ISNULL(C.Cash_Collected_5000P/100,0) as Cash_Collected_5000P,
		ISNULL(C.Cash_Collected_2000p/100,0) as Cash_Collected_2000P,
		ISNULL(C.Cash_Collected_1000P/100,0) as Cash_Collected_1000P,
		ISNULL(C.Cash_Collected_500P/100,0) as Cash_Collected_500P,
		ISNULL(C.Cash_Collected_200P/100,0) as Cash_Collected_200P,
		ISNULL(C.Cash_Collected_100P/100,0)as   Cash_Collected_100P,
		
		ISNULL(CAST(C.Cash_Collected_50p/100.0 AS DECIMAL(20,2)), 0) as  Cash_Collected_50p,
		ISNULL(CAST(C.Cash_Collected_20p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_20p,
		ISNULL(CAST(C.Cash_Collected_10p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_10p,
		ISNULL(CAST(C.Cash_Collected_5p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_5p,
		ISNULL(CAST(C.Cash_Collected_2p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_2p,
		ISNULL(CAST(C.Cash_Collected_1p/100.0 AS DECIMAL(20,2)), 0) as Cash_Collected_1p,
		C.DeclaredTicketValue,
		
		ISNULL(C.Cash_Collected_100000P/(1000 * 100),0) as Cash_Collected_100000P_Qty,
		ISNULL(C.Cash_Collected_50000P/(500 * 100),0) as Cash_Collected_50000P_Qty,
		ISNULL(C.Cash_Collected_20000P/(200 * 100),0) as Cash_Collected_20000P_Qty,
		ISNULL(C.Cash_Collected_10000P/(100 * 100),0) as Cash_Collected_10000P_Qty,
		ISNULL(C.Cash_Collected_5000P/(50 * 100),0) as Cash_Collected_5000P_Qty,
		ISNULL(C.Cash_Collected_2000p/(20 * 100),0) as Cash_Collected_2000P_Qty,
		ISNULL(C.Cash_Collected_1000P/(10 * 100),0) as Cash_Collected_1000P_Qty,
		ISNULL(C.Cash_Collected_500P/(5 * 100),0) as Cash_Collected_500P_Qty,
		ISNULL(C.Cash_Collected_200P/(2 * 100),0) as Cash_Collected_200P_Qty,
		ISNULL(C.Cash_Collected_100P/100,0)as   Cash_Collected_100P_Qty,
		
		I.Installation_Token_Value
	FROM
		[Collection] C
		INNER JOIN [Installation] I ON I.[Installation_ID] = C.[Installation_ID]
	WHERE
		C.[Collection_ID] = @Collection_ID

END

GO

