USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetAutoCloseIDs]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetAutoCloseIDs]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ===================================================================================================================================
-- SELECT dbo.fnGetAutoCloseIDs('REMDEY')
-- -----------------------------------------------------------------------------------------------------------------------------------
--
-- returns the ID of the corresponding table
-- 

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 04/09/2008	Sudarsan S	Created
-- ===================================================================================================================================

CREATE FUNCTION dbo.fnGetAutoCloseIDs(
@Type VARCHAR(10)
)
RETURNS INT
AS

BEGIN

DECLARE @ReturnValue	INT

IF @Type = 'REMEDY'
	SELECT @ReturnValue = Call_Remedy_ID FROM dbo.Call_Remedy WHERE Call_Remedy_Reference = 9999
ELSE
	SELECT @ReturnValue = Staff_ID FROM dbo.Staff WHERE Staff_First_Name LIKE '%ENGINEER%' OR Staff_Last_Name LIKE '%ENGINEER%'


RETURN ISNULL(@ReturnValue, 0)

END


GO

