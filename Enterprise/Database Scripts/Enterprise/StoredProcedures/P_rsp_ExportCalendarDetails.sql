USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportCalendarDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportCalendarDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
 *	this stored procedure is to export all the calendar related details to the Exchange
 *
 *	Change History:
 *	
 *	Sudarsan S		20-05-2008		created
 *  Sudarsan S		11-07-2008		modified since the usp_export_history was changed
 *  Sudarsan S		18-07-2008		handled for Operator calendar also
*/
--EXEC dbo.rsp_ExportCalendarDetails 2, 'S-CALENDAR'
CREATE PROCEDURE [dbo].[rsp_ExportCalendarDetails]
	@Site_ID	VARCHAR(50),
	@Calendar_Type	VARCHAR(20)
AS

BEGIN

DECLARE @iCalendar_ID	INT
DECLARE @iOperator_ID	INT
DECLARE @Site	XML
DECLARE @Calendar_Details	XML
DECLARE @Calendar	XML
DECLARE @Calendar_Period	XML
DECLARE @Calendar_Week	XML
DECLARE @Operator	XML
DECLARE @Operator_Details XML
DECLARE @Actual_Site_Code	VARCHAR(50)

--SELECT @EH_ID = EH_ID FROM dbo.Export_History WHERE EH_Reference1 = @Site_Code AND EH_STATUS IS NULL AND EH_Type = 'CALENDAR'

--Fetch the calendar ID which need to be exported

SELECT @Actual_Site_Code = ISNULL(Site_Code, '') FROM dbo.Site WHERE Site_ID = @Site_ID

IF @Calendar_Type = 'S-CALENDAR'
BEGIN
	SELECT @iCalendar_ID = SCC.Calendar_ID FROM Site S
		INNER JOIN Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID
		INNER JOIN Sub_Company_Calendar SCC ON SC.Sub_Company_ID = SCC.Sub_Company_ID
			WHERE S.Site_Code = @Actual_Site_Code
			AND SCC.Sub_Company_Calendar_Active = 1
END
ELSE
BEGIN
	SELECT @iCalendar_ID = OC.Calendar_ID, @iOperator_ID = O.Operator_ID FROM Site S
		INNER JOIN Depot D ON S.Depot_ID = D.Depot_ID
		INNER JOIN Operator O ON D.Supplier_ID = O.Operator_ID
		INNER JOIN Operator_Calendar OC ON O.Operator_ID = OC.Operator_ID
			WHERE S.Site_Code = @Actual_Site_Code
			AND OC.Operator_Calendar_Active = 1
END

IF @iCalendar_ID IS NULL
	RETURN -1

SET @Calendar_Period = (SELECT CP.Calendar_Period_ID, CP.Calendar_ID, CP.Calendar_Period_Number, CONVERT(DATETIME, CP.Calendar_Period_Start_Date, 103) AS Calendar_Period_Start_Date, CONVERT(DATETIME, CP.Calendar_Period_End_Date, 103) AS Calendar_Period_End_Date FROM dbo.Calendar_Period CP
							WHERE CP.Calendar_ID = @iCalendar_ID FOR XML PATH('PERIOD'), TYPE, ELEMENTS XSINIL, ROOT('PERIODS'))

SET @Calendar_Week = (SELECT CW.Calendar_Week_ID, CW.Calendar_ID, CW.Calendar_Week_Number, CONVERT(DATETIME, CW.Calendar_Week_Start_Date, 103) AS Calendar_Week_Start_Date, 
							CONVERT(DATETIME, CW.Calendar_Week_End_Date, 103) AS Calendar_Week_End_Date, CW.Calendar_Period_ID FROM dbo.Calendar_Week CW 
							WHERE CW.Calendar_ID = @iCalendar_ID FOR XML PATH('WEEK'), TYPE, ELEMENTS XSINIL, ROOT('WEEKS')) 

SET @Calendar = (SELECT C.Calendar_ID, C.Calendar_Description, CONVERT(DATETIME, C.Calendar_Year_Start_Date, 103) AS Calendar_Year_Start_Date, CONVERT(DATETIME, C.Calendar_Year_End_Date, 103) AS Calendar_Year_End_Date, @Calendar_Period, @Calendar_Week FROM dbo.Calendar C WHERE C.Calendar_ID = @iCalendar_ID FOR XML PATH('CALENDAR'), TYPE, ELEMENTS XSINIL)


SET @Calendar_Details = (SELECT SCC.*, @Calendar FROM dbo.Sub_Company_Calendar SCC 
							INNER JOIN dbo.Sub_Company SC ON SCC.Sub_Company_ID = SC.Sub_Company_ID
							INNER JOIN dbo.Site S ON SC.Sub_Company_ID = S.Sub_Company_ID
							WHERE SCC.Calendar_ID = @iCalendar_ID AND S.Site_ID = @Site_ID FOR XML PATH('SITECALENDAR_DETAILS'), TYPE, ELEMENTS XSINIL)

SET @Operator_Details = (SELECT OC.*, @Calendar FROM dbo.Operator_Calendar OC
							WHERE OC.Calendar_ID = @iCalendar_ID AND OC.Operator_ID = @iOperator_ID FOR XML PATH('OPCALENDAR_DETAILS'), TYPE, ELEMENTS XSINIL)

SET @Site = (SELECT S.Site_Code, C.Calendar_ID, @Calendar_Details
				FROM Site S
				INNER JOIN Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID
				INNER JOIN Sub_Company_Calendar SCC ON SC.Sub_Company_ID = SCC.Sub_Company_ID
				INNER JOIN Calendar C ON SCC.Calendar_ID = C.Calendar_ID
				WHERE S.Site_Code = @Actual_Site_Code AND C.Calendar_ID = @iCalendar_ID FOR XML PATH('SITE'), TYPE, ELEMENTS XSINIL, ROOT('SITECALENDAR'))

SET @Operator = (SELECT O.Operator_Name, S.Site_Code, C.Calendar_ID, @Operator_Details
				FROM Site S
				INNER JOIN Depot D ON D.Depot_ID = S.Depot_ID
				INNER JOIN Operator O ON D.Supplier_ID = O.Operator_ID
				INNER JOIN Operator_Calendar OC ON O.Operator_ID = OC.Operator_ID
				INNER JOIN Calendar C ON C.Calendar_ID = OC.Calendar_ID
				WHERE S.Site_Code = @Actual_Site_Code AND C.Calendar_ID = @iCalendar_ID FOR XML PATH('OPERATOR'), TYPE, ELEMENTS XSINIL, ROOT('OPCALENDAR'))

--SET @Result = (SELECT TOP 1 @Site_Code AS Site_Code, 'CALENDAR' AS [Type], @EH_ID AS EH_ID, @Site As SiteXML FROM dbo.Site FOR XML PATH('SITE'), TYPE, ELEMENTS, ROOT('CALENDARDETAILS'))
SELECT CASE WHEN @Calendar_Type = 'S-CALENDAR' THEN @Site ELSE @Operator END
--SELECT @Result


END


GO

