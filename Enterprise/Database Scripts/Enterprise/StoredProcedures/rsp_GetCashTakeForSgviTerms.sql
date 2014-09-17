USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCashTakeForSgviTerms]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCashTakeForSgviTerms]
GO

CREATE PROCEDURE [dbo].[rsp_GetCashTakeForSgviTerms](@CollectionId INT)
AS
BEGIN
	SELECT SUM(
	           (VW.Declared_Tickets + VW.Declared_Notes) -(VW.DecHandpay + VW.TicketOut)
	       ) Cash_Take
	FROM   dbo.VW_CollectionData VW WITH(NOLOCK)
	       LEFT JOIN dbo.Collection_Details CD WITH(NOLOCK)
	            ON  VW.Collection_ID = CD.Collection_ID
	WHERE  VW.Collection_ID = @CollectionId
END
GO
