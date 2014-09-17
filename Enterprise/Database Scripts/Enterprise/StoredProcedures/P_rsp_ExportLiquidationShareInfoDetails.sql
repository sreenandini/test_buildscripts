USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportLiquidationShareInfoDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportLiquidationShareInfoDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_ExportLiquidationShareInfoDetails]
(
@LiquidationShareId INT
)
AS   
BEGIN  
SELECT LiquidationShareId,
		LiquidationId,	
		ShareHolderName,	
		ProfitShareGroupId,
		ShareHolderId,	
		ProfitShareAmont,	
		ExpenseShareAmount
	FROM LiquidationShareDetails 
	WHERE LiquidationShareId=@LiquidationShareId     
 
 FOR XML AUTO, ELEMENTS, ROOT('LiquidationShareDetails')  
END  

GO

