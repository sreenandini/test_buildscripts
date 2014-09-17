USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetTicketsOutByMachineBatch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetTicketsOutByMachineBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetTicketsOutByMachineBatch] (@BatchNo INT, @InstallationNo INT)
RETURNS MONEY
AS
BEGIN
DECLARE @Ticketsout Money
select @Ticketsout = isnull(sum(ct_value),0)  from collection_ticket where ct_printed_collection_id= @BatchNo
        and ct_printed_installation_id = @InstallationNo

RETURN @Ticketsout

END

GO

