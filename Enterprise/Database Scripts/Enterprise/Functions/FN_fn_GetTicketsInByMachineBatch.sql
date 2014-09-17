USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetTicketsInByMachineBatch]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetTicketsInByMachineBatch]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetTicketsInByMachineBatch] (@BatchNo INT, @InstallationNo INT)  
RETURNS MONEY  
AS  
BEGIN  
DECLARE @TicketsIn Money  
select @TicketsIn = isnull(sum(ct_value),0) from collection_ticket tCT
Inner Join Collection tC On tC.Collection_id = ct_inserted_collection_id
where Batch_ID = @BatchNo    
        and ct_inserted_installation_id = @InstallationNo    
  
RETURN @TicketsIn  
  
END

GO

