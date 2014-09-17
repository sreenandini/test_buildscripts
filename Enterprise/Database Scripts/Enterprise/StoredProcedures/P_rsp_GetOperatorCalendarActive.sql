USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetOperatorCalendarActive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetOperatorCalendarActive]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_GetOperatorCalendarActive
(
	@OperatorId INT
)
AS
/*****************************************************************************************************
DESCRIPTION : To get the Operator Calendar Active Field  
CREATED DATE: 06.2.2013
MODULE            :Calendar      
CHANGE HISTORY :
*****************************************************************************************************/

BEGIN

SELECT     oc.Operator_Calendar_ID,
			calendar.Calendar_ID,
	       oc.Operator_Calendar_Active,
	       Calendar.Calendar_Description,
	       Calendar.Calendar_Year_Start_Date,
	       Calendar.Calendar_Year_End_Date,
	       CASE 
	            WHEN oc.Operator_Calendar_Active = 0 THEN 
	            	CAST(Calendar.Calendar_Description AS VARCHAR(50)) 
	                 + CAST(Calendar.Calendar_Year_Start_Date AS VARCHAR(30)) 
	                + ' to ' + CAST(Calendar.Calendar_Year_End_Date AS VARCHAR(30))
	            ELSE CAST(Calendar.Calendar_Description AS VARCHAR(50))    +  '[Active]'   +
	                 + CAST(Calendar.Calendar_Year_Start_Date AS VARCHAR(30)) 
	                + ' to ' + CAST(Calendar.Calendar_Year_End_Date AS VARCHAR(30))
	       END AS Calendar_History
	FROM   Operator_Calendar oc
	       INNER JOIN Calendar
	            ON  oc.Calendar_ID = Calendar.Calendar_ID
	WHERE  Operator_ID = @OperatorId
	ORDER BY
	      Operator_Calendar_ID DESC
END

GO

