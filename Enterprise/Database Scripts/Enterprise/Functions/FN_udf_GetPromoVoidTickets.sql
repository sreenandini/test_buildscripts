USE Enterprise
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetPromoVoidTickets]   Script Date: 04/03/2012 11:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetPromoVoidTickets]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetPromoVoidTickets]
GO

USE Enterprise
GO

CREATE FUNCTION [dbo].[udf_GetPromoVoidTickets](@iPromotionalTicketID int)
RETURNS TABLE
AS RETURN
(
	SELECT NoOfTicketsVoid = COUNT (v.strVoucherStatus),
			VoidAmount = (SUM(v.iAmount) /100.00)
	FROM Voucher v 
	where (v.iVoucherID IN (SELECT DISTINCT PT.VoucherID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
	AND v.strVoucherStatus='VD' AND dtVoid IS NOT NULL)
	AND v.iSiteID IN (SELECT DISTINCT PT.SiteID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
	
	--inner join PromotionalTickets Prom 
	--ON v.iVoucherID=Prom.VoucherID
	--inner join Promotions P
	--ON P.HQ_ID=Prom.PromotionalID
	
	--where P.HQ_ID = @iPromotionalTicketID
)
