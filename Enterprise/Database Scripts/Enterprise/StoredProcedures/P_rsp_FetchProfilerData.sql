USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_FetchProfilerData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_FetchProfilerData]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
--
-- Description: fetch the unprocessed data from enterprise Export/Import
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		31/03/2010		Created
-- Anil				03/12/2010		Modifed to fix CR 91290
-- rsp_FetchProfilerData '', '','Export'
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE dbo.rsp_FetchProfilerData 
	@Site_Code	VARCHAR(50) = '',
	@Type	VARCHAR(50) = '',
	@ExpImp		VARCHAR(20) = ''
AS
BEGIN

	IF @ExpImp = 'Export'

		SELECT S.Site_Code, S.Site_Name, E.EH_ID AS ID, E.EH_Date AS Date, E.EH_Reference1 AS Reference, ISNULL(EHT.EH_Type_Desc,E.EH_Type) AS ExpImpType, 
				CASE ISNULL(E.EH_Status, 0) WHEN 0 THEN 'UnProcessed'
					WHEN -1 THEN 'Failed'
					ELSE 'Pending Export' END AS Status
			 FROM dbo.Export_History E
		INNER JOIN dbo.Site S ON E.EH_Site_Code = S.Site_Code
		LEFT JOIN dbo.Export_History_Types EHT ON EHT.EH_Type_Ref = E.EH_Type 		
		WHERE ISNULL(EH_Status, '0') NOT IN ('100', '200', '300')
		AND (@Site_Code = '' OR EH_Site_Code = @Site_Code)
		AND (@Type = '' OR EH_Type = @Type)
		ORDER BY EH_ID
	
	ELSE

		SELECT S.Site_Code, S.Site_Name, I.IH_ID AS ID, I.IH_Received_Date AS Date, CAST(I.IH_Details AS VARCHAR(MAX)) AS Reference, ISNULL(IHT.IH_Type_Desc, I.IH_Type) AS ExpImpType,
				CASE ISNULL(I.IH_Status, 0) WHEN  0 THEN 'UnProcessed'
					WHEN -1 THEN 'Failed'
					ELSE 'Pending Import' END AS Status
		FROM dbo.Import_History I
		INNER JOIN dbo.Site S ON I.IH_Site_Code = S.Site_Code	
		LEFT JOIN dbo.Import_History_Types IHT ON IHT.IH_Type_Ref = I.IH_Type 	
		WHERE ISNULL(IH_Status, 0) NOT IN (100, 200, 300)
		AND (@Site_Code = '' OR IH_Site_Code = @Site_Code)
		AND (@Type = '' OR IH_Type = @Type)
		ORDER BY IH_ID		

END

GO

