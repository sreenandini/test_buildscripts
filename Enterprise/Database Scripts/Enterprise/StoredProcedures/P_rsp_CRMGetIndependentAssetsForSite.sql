USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_CRMGetIndependentAssetsForSite]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_CRMGetIndependentAssetsForSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.rsp_CRMGetIndependentAssetsForSite 
(@Site_id INT, @route_No INT = NULL) 
/************************************************************************************
Used In(Module) : Route Manager
Created Date	:
Description		: Returns all Assets which not  assigend to routes
======================================================================================
Modification History
	Developer		        Modification 								
======================================================================================
1) K.Karthicksundar			Created 	
2)	
**************************************************************************************/
AS
BEGIN
	BEGIN
	
		IF @route_No=0 
			SET @route_No=NULL 
			
		SELECT DISTINCT 
		       BP.Bar_Position_ID,
		       Bar_Position_Name,
		       ISNULL(M.MACHINE_MANUFACTURERS_SERIAL_NO, '') 
		       MACHINE_MANUFACTURERS_SERIAL_NO,
		       ISNULL(MACHINE_STOCK_NO, '') MACHINE_STOCK_NO
		FROM   SITE S
		       INNER JOIN dbo.Bar_Position BP
		            ON  S.Site_ID = BP.Site_ID
		            AND BP.Bar_Position_ID NOT IN (SELECT RM.Bar_Position_ID --DONOT INCLUDE ASSIGNED ASSETS
		                                           FROM   Route_Member RM
		                                                  INNER JOIN dbo.[ROUTE] 
		                                                       R
		                                                       ON  RM.Route_ID = 
		                                                           R.Route_ID
		                                                       AND Site_ID = @Site_id
		                                                       AND RM.Route_ID = 
		                                                           ISNULL(@route_No,-1))
		       LEFT OUTER JOIN dbo.installation I
		            ON  BP.Bar_Position_ID = I.Bar_Position_ID
		            AND I.Installation_End_Date IS NULL
		       LEFT OUTER JOIN dbo.Machine M
		            ON  M.Machine_ID = I.Machine_ID
		       LEFT OUTER JOIN Machine_Class MC
		            ON  M.Machine_Class_ID = MC.Machine_Class_ID
		       LEFT OUTER JOIN MAchine_Type MT
		            ON  MT.Machine_Type_ID = MC.Machine_Type_ID
		WHERE  S.Site_id = @Site_id
		ORDER BY
		       Bar_Position_Name
	END
END
GO

