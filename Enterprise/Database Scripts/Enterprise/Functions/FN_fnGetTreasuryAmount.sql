USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetTreasuryAmount]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetTreasuryAmount]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- ===================================================================================================================================
-- SELECT dbo.fnGetTreasuryAmount(10, 'Refills')
-- -----------------------------------------------------------------------------------------------------------------------------------
--
-- returns the sum of treasuries for the given type
-- 

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 17/07/2009	Sudarsan S	Created
-- ===================================================================================================================================

CREATE FUNCTION dbo.fnGetTreasuryAmount
(
	@Collection_ID	INT,
	@Type			VARCHAR(50)
)
RETURNS REAL
AS

BEGIN

DECLARE @Amount REAL

SELECT @Amount = SUM(Treasury_Amount) FROM dbo.Treasury_Entry WHERE Collection_ID = @Collection_ID 
					AND Treasury_Type = @Type

RETURN ISNULL(@Amount, 0)

END


GO

