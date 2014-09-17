USE Enterprise
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetPromoRedeemedTickets]    Script Date: 04/03/2012 11:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetPromoRedeemedTickets]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetPromoRedeemedTickets]
GO

USE Enterprise
GO

CREATE FUNCTION [dbo].[udf_GetPromoRedeemedTickets](@iPromotionalTicketID int)
RETURNS TABLE
AS RETURN
(
	SELECT NoOfRedeemed = COUNT (v.strVoucherStatus),
			RedeemedAmount = (SUM(ISNULL(v.iAmount,0.0)) /100.00)
	FROM Voucher v 
	where (v.iVoucherID IN (SELECT DISTINCT PT.VoucherID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
	AND v.strVoucherStatus='PD' AND dtPaid IS NOT NULL)
	AND v.iSiteID IN (SELECT DISTINCT PT.SiteID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
	--inner join PromotionalTickets Prom 
	--ON v.iVoucherID=Prom.VoucherID
	--inner join Promotions P
	--ON P.HQ_ID=Prom.PromotionalID
	
	--where P.HQ_ID = @iPromotionalTicketID
	
	
)






