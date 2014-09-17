USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetTicketsInByCollectionNo]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_GetTicketsInByCollectionNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [fn_GetTicketsInByCollectionNo] (@Collection_No INT, @InstallationNo INT)  
RETURNS MONEY  
AS  
BEGIN  
DECLARE @TicketsIn Money  
select @TicketsIn = isnull(sum(ct_value),0) from collection_ticket tCT
Inner Join Collection tC On tC.Collection_Id = ct_inserted_collection_id
where Collection_Id = @Collection_No    
        and ct_inserted_installation_id = @InstallationNo    
  
RETURN @TicketsIn  
  
END

GO

