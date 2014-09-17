/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 25/02/13 12:24:55 PM
 ************************************************************/

/* ================================================================================= 
 * Purpose	:	rsp_GetHourlyStatisticsTypes
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
              AND SPECIFIC_NAME = N'rsp_GetHourlyStatisticsTypes'
   )
    DROP PROCEDURE dbo.rsp_GetHourlyStatisticsTypes
GO

-- create the procedure
CREATE PROCEDURE dbo.rsp_GetHourlyStatisticsTypes
AS
BEGIN
	SET NOCOUNT ON
	-- BEGIN
	
	SELECT HST_ID,
	       HST_Type,
	       REPLACE(HST_Desc, '_', ' ') AS HST_Desc
	FROM   Hourly_Statistics_Types
	ORDER BY
	       HST_Desc ASC
	
	-- END
	SET NOCOUNT OFF
END
GO