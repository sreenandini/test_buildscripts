USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_ecGetCollectionEvents'
   )
    DROP PROCEDURE dbo.rsp_ecGetCollectionEvents
GO

CREATE PROCEDURE dbo.rsp_ecGetCollectionEvents
	@Collection_ID INT =0
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	-- EVENTS
	SELECT * FROM (SELECT 'DOOR' [Type],
	       DE.Door_Event_ID AS Event_ID,
	       DE.Door_Event_Date AS Event_Date,
	       DE.Door_Event_Time AS EVENT_Time,
	       DE.Door_Event_Duration AS Duration,
	       (SELECT DISTINCT DF.Datapak_Fault_Text FROM Datapak_Fault DF WHERE DF.Datapak_Fault_Code = 20 AND DE.Door_Event_Type = DF.Datapak_Fault_Supplementary_Code) AS Description
	       --
	       --		Door_Event.*,
	       --
	       --       Datapak_Fault.Datapak_Fault_Text
	FROM   Door_Event DE
		INNER JOIN [Collection] C ON C.Collection_ID = DE.Collection_ID
	WHERE  
	       C.Collection_ID = @Collection_ID
	       AND C.Declaration = 1
	UNION ALL 
	SELECT 'Fault' [Type],
	       Fault_Event_ID AS Event_ID,
	       Fault_Event_Date AS Event_Date,
	       Fault_Event_Time AS EVENT_Time,
	       0 AS Duration,
	       Fault_Event_Description AS Description
	       --Fault_Event.*
	FROM   Fault_Event FE
		INNER JOIN [Collection] C ON C.Collection_ID = FE.Collection_ID
	WHERE  C.Collection_ID = @Collection_ID
		AND C.Declaration = 1
	UNION ALL 
	SELECT 'Power' [Type],
	       Power_Event_ID Event_ID,
	       Power_Event_Date Event_Date,
	       Power_Event_Time EVENT_Time,
	       Power_Event_Duration Duration,
	       '' Description
	       --Power_Event.*
	FROM   Power_Event PE
		INNER JOIN [Collection] C ON C.Collection_ID = PE.Collection_ID
	WHERE  C.Collection_ID = @Collection_ID
		AND C.Declaration = 1
	) A ORDER BY [Type]
END
GO

GO



