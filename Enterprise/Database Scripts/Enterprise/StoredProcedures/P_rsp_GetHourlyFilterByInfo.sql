/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 25/02/13 7:14:23 PM
 ************************************************************/

/* ================================================================================= 
 * Purpose	:	rsp_GetHourlyFilterByInfo
 * Author	:	A.Vinod Kumar
 * Created  :	25/02/2013
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 25/02/2013  	A.Vinod Kumar    Initial Version
 * ================================================================================= 
*/
-- Use the database
USE Enterprise
GO

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetHourlyFilterByInfo'
   )
    DROP PROCEDURE dbo.rsp_GetHourlyFilterByInfo
GO

-- EXEC dbo.rsp_GetHourlyFilterByInfo 3
CREATE PROCEDURE dbo.rsp_GetHourlyFilterByInfo
(@FilterBy INT, @FilterById INT = NULL)
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	-- Position
	IF (@FilterBy = 0)
	BEGIN
	    SELECT Bar_Position_ID AS FilterById,
	           Bar_Position_Name AS FilterByName
	    FROM   Bar_Position
	    WHERE  Bar_Position_ID = ISNULL(@FilterById, Bar_Position_ID)
	    ORDER BY
	           Bar_Position_Name
	END-- Site
	ELSE 
	IF (@FilterBy = 1)
	BEGIN
	    SELECT Site_ID AS FilterById,
	           Site_Name AS FilterByName
	    FROM   SITE
	    WHERE  Site_ID = ISNULL(@FilterById, Site_ID)
	    ORDER BY
	           Site_Name
	END-- Zone
	ELSE 
	IF (@FilterBy = 2)
	BEGIN
	    SELECT Zone_ID AS FilterById,
	           Zone_Name AS FilterByName
	    FROM   [Zone]
	    WHERE  Site_ID = ISNULL(@FilterById, Site_ID)
	    ORDER BY
	           Zone_Name
	END-- Category
	ELSE 
	IF (@FilterBy = 3)
	BEGIN
	    SELECT DISTINCT Machine_Type.Machine_Type_ID AS FilterById,
	           Machine_Type_Code AS FilterByName
	    FROM   (
	               (
	                   (
	                       Bar_Position INNER JOIN Installation ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID
	                   ) INNER JOIN MACHINE ON Installation.Machine_ID = MACHINE.Machine_ID
	               ) INNER JOIN Machine_Type ON MACHINE.Machine_Category_ID = Machine_Type.Machine_Type_ID
	           )
	    WHERE  Bar_Position.Site_ID = ISNULL(@FilterById, Bar_Position.Site_ID)
	    ORDER BY
	           Machine_Type_Code
	END
	
	-- END
	SET NOCOUNT OFF
END
GO