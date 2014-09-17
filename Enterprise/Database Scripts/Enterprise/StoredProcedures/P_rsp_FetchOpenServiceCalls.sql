USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchOpenServiceCalls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchOpenServiceCalls]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------- 
--
-- Description: Fetches the open Service records that are yet to be closed
--
-- Inputs:     Site Code, Bar Pos Name
-- Outputs:     
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Sudarsan S	 20/08/2009   Created
-- rsp_FetchOpenServiceCalls '1001', '002'
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_FetchOpenServiceCalls]
	@Site_Code	VARCHAR(10),
	@Bar_Pos	VARCHAR(20)
AS

BEGIN

	DECLARE @IsServiceCallFeatureFull VARCHAR(10)
	
	SET @IsServiceCallFeatureFull = 'false'
	
	SELECT @IsServiceCallFeatureFull = LOWER(Setting_Value)
	FROM   Setting
	WHERE  Setting_Name = 'IsServiceCallFeatureFull' 

SELECT  S.Service_Received AS LoggedDate,
						CASE 
	                       WHEN (
	                                ISNULL(cg.Call_Group_Downtime, 0) = 1
	                                OR ISNULL(@IsServiceCallFeatureFull, 'false') 
	                                   = 'false'
	                            ) THEN CASE 
	                                        WHEN ISNULL(SSOH.Standard_Opening_Hours_ID, 0) 
	                                             > 0 THEN dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, S.Service_Received, 101),
	                                                 SSOH.Standard_Opening_Hours_Open_Monday,
	                                                 SSOH.Standard_Opening_Hours_Open_Tuesday,
	                                                 SSOH.Standard_Opening_Hours_Open_Wednesday,
	                                                 SSOH.Standard_Opening_Hours_Open_Thursday,
	                                                 SSOH.Standard_Opening_Hours_Open_Friday,
	                                                 SSOH.Standard_Opening_Hours_Open_Saturday,
	                                                 SSOH.Standard_Opening_Hours_Open_Sunday,
	                                                 NULL
	                                             )
	                                        ELSE dbo.fnServiceGetDownTime(
	                                                 CONVERT(DATETIME, S.Service_Received, 101),
	                                                 St.Site_Open_Monday,
	                                                 St.Site_Open_Tuesday,
	                                                 St.Site_Open_Wednesday,
	                                                 St.Site_Open_Thursday,
	                                                 St.Site_Open_Friday,
	                                                 St.Site_Open_Saturday,
	                                                 St.Site_Open_Sunday,
	                                                 NULL
	                                             )
	                                   END
	                       ELSE '00:00'
	                  END AS DownTime,
					--CASE WHEN ISNULL(SSOH.Standard_Opening_Hours_ID, 0) > 0 THEN
					--	dbo.fnServiceGetDownTime(CONVERT(DATETIME, S.Service_Received, 101), SSOH.Standard_Opening_Hours_Open_Monday, 
					--	SSOH.Standard_Opening_Hours_Open_Tuesday, SSOH.Standard_Opening_Hours_Open_Wednesday, SSOH.Standard_Opening_Hours_Open_Thursday, 
					--	SSOH.Standard_Opening_Hours_Open_Friday, SSOH.Standard_Opening_Hours_Open_Saturday, SSOH.Standard_Opening_Hours_Open_Sunday, NULL)
					--ELSE
					--	dbo.fnServiceGetDownTime(CONVERT(DATETIME, S.Service_Received, 101), St.Site_Open_Monday, St.Site_Open_Tuesday, 
					--	St.Site_Open_Wednesday, St.Site_Open_Thursday, St.Site_Open_Friday, St.Site_Open_Saturday, St.Site_Open_Sunday, NULL)
					--END AS DownTime,
					
			CAST(S.Service_Allocated_Job_No AS VARCHAR) + '/' + CAST(S.Service_Visit_No AS VARCHAR) AS JobID,
			CASE S.Call_Status_ID
				WHEN 1 THEN 'Logged'
				WHEN 2 THEN 'Viewed'
				WHEN 3 THEN 'Despatched'
				WHEN 4 THEN 'Accepted'
				WHEN 5 THEN 'Enroute'
				WHEN 6 THEN 'At Site'
				WHEN 7 THEN 'Received'
				WHEN 8 THEN 'Completed'
				WHEN 9 THEN 'Rejected'
				ELSE		'' END AS Call_Status,
			ISNULL(CG.Call_Group_Reference,'UnAssignedGroup') + '-' + CF.Call_Fault_Description AS Fault,
			CASE WHEN EXISTS(SELECT 1 FROM dbo.Service_Notes (NOLOCK) WHERE Service_ID = S.Service_Allocated_Job_No)
				THEN 1
			ELSE 0 END AS IsTrue,
			ISNULL(S.IsEscalated, 0) AS IsHighlighted
	  FROM  dbo.Service S WITH (NOLOCK) 
 LEFT JOIN  dbo.Call_Fault CF WITH (NOLOCK) ON S.Call_Fault_ID = CF.Call_Fault_ID
 LEFT JOIN  dbo.Call_Group CG WITH (NOLOCK) ON S.Call_Group_ID = CG.Call_Group_ID
INNER JOIN  dbo.Site St WITH (NOLOCK) ON St.Site_ID = S.Site_ID
INNER JOIN  dbo.Bar_Position B WITH (NOLOCK) ON S.Bar_Position_ID = B.Bar_Position_ID
 LEFT JOIN  dbo.Standard_Opening_Hours SSOH WITH (NOLOCK) ON St.Standard_Opening_Hours_ID = SSOH.Standard_Opening_Hours_ID
 LEFT JOIN  dbo.Zone Z WITH (NOLOCK) ON S.Zone_ID = Z.Zone_ID
 LEFT JOIN  dbo.Standard_Opening_Hours ZSOH WITH (NOLOCK) ON Z.Standard_Opening_Hours_ID = ZSOH.Standard_Opening_Hours_ID
 
	 WHERE  St.Site_Code = @Site_Code AND B.Bar_Position_Name = @Bar_Pos
ORDER BY  Cast((S.Service_Received) AS DATETIME)  Desc

END



GO

