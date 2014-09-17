USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetProfitShareDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetProfitShareDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Author:		Aishwarrya v s
-- Create date: 15th October 2012
-- Description:	Get ProfitShare Details
-- =============================================
CREATE PROCEDURE rsp_GetProfitShareDetails
AS
BEGIN
	SELECT P.ProfitShareId,
	       S.ShareHolderName,
	       P.ProfitSharePercentage,
	       P.ProfitShareDescription
	FROM   ProfitShare P WITH(NOLOCK)
	       INNER JOIN ShareHolders S WITH(NOLOCK)
	            ON  P.ShareHolderId = S.ShareHolderId
END

GO

