USE Enterprise
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetPromoRedeemedTickets]    Script Date: 04/03/2012 11:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetPromoExpiredTickets]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetPromoExpiredTickets]
GO

USE Enterprise
GO
CREATE FUNCTION [dbo].[udf_GetPromoExpiredTickets](@iPromotionalTicketID int)
RETURNS TABLE
AS RETURN
(
	SELECT NoOfTicketExpired = COUNT(v.dtExpire), 
    ExpiredAmount = (SUM(v.iAmount) / 100.00)
	FROM Voucher v 
	--INNER JOIN PromotionalTickets Prom 
	--ON v.iVoucherID=Prom.VoucherID   
	--INNER JOIN Promotions Pr
	--ON Pr.HQ_ID=Prom.PromotionalID
	where (v.iVoucherID IN (SELECT DISTINCT PT.VoucherID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
	AND v.dtExpire<=getDate() 
	and v.strVoucherStatus IS NULL)
    AND v.iSiteID IN (SELECT DISTINCT PT.SiteID FROM PromotionalTickets PT where PT.PromotionalID=@iPromotionalTicketID)
)
