USE Enterprise
GO

IF OBJECT_ID('dbo.udf_GetRouteNames') IS NOT NULL
BEGIN
DROP function dbo.udf_GetRouteNames
END
GO

-- select dbo.udf_GetRouteNames(70)
-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		J Venkatesh Kumar
-- Create date: 21/2/2014
-- Description:	To combine the routes for a bar position
-- =============================================
CREATE FUNCTION dbo.udf_GetRouteNames 
(
	-- Add the parameters for the function here
	@Bar_Position_ID INT
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result VARCHAR(MAX)

	-- Add the T-SQL statements to compute the return value here
	SELECT @result = COALESCE(@result + ', ', '') + R.Route_Name
	FROM Bar_Position   BP INNER JOIN [Route_Member] RM ON RM.Bar_Position_ID = bp.Bar_Position_ID
	INNER JOIN [Route] R ON R.Route_ID = RM.Route_ID
	WHERE bp.Bar_Position_ID = @Bar_Position_ID

	-- Return the result of the function
	RETURN @Result

END
GO

