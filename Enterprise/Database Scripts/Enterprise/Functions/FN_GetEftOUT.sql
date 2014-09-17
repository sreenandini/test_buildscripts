USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEftOUT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetEftOUT]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  FUNCTION [dbo].[GetEftOUT] (@collection_ID INT)  
/**********************************
 EFT OUT 
**********************************/
RETURNS Float  
AS  
BEGIN  
	DECLARE @Amount float  
	IF (@collection_ID >0)  
	BEGIN  
		SELECT @Amount = ISNULL(SUM(ISNULL(Promo_Cashable_EFT_OUT,0) +ISNULL(NonCashable_EFT_OUT,0) + ISNULL(WAT_Out,0)),0) 
		FROM AFT_TRANSACTIONS	
		WHERE  Transaction_Type='Deposit Complete'
		AND Collection_No=@collection_ID
	END  
	ELSE  
	BEGIN 
		 SET @Amount = 0  
	END 

	RETURN @Amount  
END		

GO

